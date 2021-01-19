using System;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Shared;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf
{


    public sealed class InspectionPdf : PdfDocumentBase
    {

        private const string PngImgMarker = "data:image/png;base64";

        private readonly Phrase OutcomeKey = new Phrase
        {
            new Chunk("Validation exigences: ", Fonts.Helvetica8BlackItalic),
            new Chunk(" Oui ", Fonts.Helvetica8Black).SetBackground(Colors.Ok),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("respectées", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
            new Chunk(" Non ", Fonts.Helvetica8Black).SetBackground(Colors.Ko),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("non respectées", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
            new Chunk(" P ", Fonts.Helvetica8Black).SetBackground(Colors.Pok),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("partiellement respectées", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
            new Chunk(" NA ", Fonts.Helvetica8Black).SetBackground(Colors.LightGray),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("non applicables", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
            new Chunk(" NC ", Fonts.Helvetica8Black).SetBackground(Colors.LightGray),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("non contrôlées", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
        };

        private readonly Phrase ColorsKey = new Phrase
        {
            new Chunk("Traitement des rubriques: ", Fonts.Helvetica8BlackItalic),
            new Chunk("     ", Fonts.Helvetica8Black).SetBackground(Colors.AutoSetNa),
            new Chunk(" ", Fonts.Helvetica8Black),
            new Chunk("non inscrit ou non concerné", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("     ", Fonts.Helvetica8Black),
            new Chunk("     ", Fonts.Helvetica8Black).SetBackground(Colors.AutoSetNc),
            new Chunk(" ", Fonts.Helvetica8Black),
            new Chunk("Point de contrôle non ciblé", Fonts.Helvetica8BlackBoldItalic)
        };

        #region Members

        private readonly InspectionPdfModel model_;

        #endregion

        #region Initialization

        public InspectionPdf(InspectionPdfModel model, string username, bool showWatermark = false) : base(username)
        {
            model_ = model;
            Size = PageSize.A4;
            Landscape = false;
            TopMargin = 50;
            BottomMargin = 50;
            LeftMargin = RightMargin = 30;
            Watermark = showWatermark ? WatermarkText : "";
            LightColor = Colors.LightGray;
            DarkColor = Colors.Gray;
            PageNumberPosition = 5;
        }

        #endregion

        #region Services

        protected override void AddBody(PdfWriter writer, Document document)
        {
            document.Add(Title());
            document.Add(FarmAndOrganization());
            document.Add(RubricsSummary());
            document.Add(Signatures());
            document.Add(Objection());
            document.NewPage();
            document.Add(CheckList());
        }

        private AcPdfPTable Title()
        {
            var table = CustomTable(new[] {55f, 45f});
            table.AddCustomCell($"Protocole de constat du contrôle {model_.CampaignYear}", Fonts.Helvetica14BlackBold,
                borderWidth: 0);
            AddImageCellFromFile(table, model_.FocaaLogoPath, 65, 2, 0, Element.ALIGN_RIGHT);
            table.AddCustomCell(model_.DomainName, Fonts.Helvetica18BlackBold, borderWidth: 0);
            return table;
        }

        private AcPdfPTable FarmAndOrganization()
        {
            var table = CustomTable(new[] {45f, 10f, 45f});
            table.AddCustomCell("Exploitation", Fonts.Helvetica12BlackBold, borderWidth: 0, colspan: 2);
            table.AddCustomCell("Organisation ayant effectué le contrôle", Fonts.Helvetica10BlackBold, borderWidth: 0);
            AddFarmCell(table, model_.Farm);
            table.AddCustomCell(" ", borderWidth: 0);
            AddOrganizationCell(table);
            return table;
        }

        private AcPdfPTable RubricsSummary()
        {
            var table = CustomTable(new[] {15f, 35f, 5f, 45f});
            var data = model_.InspectionResults.Where(x => x.ResultType == RecapResultListItemModel.ResultTypes.Rubric);
            table.AddCustomCell("Résumé du contrôle des exigences", Fonts.Helvetica12BlackBold, borderWidth: 0,
                colspan: 4);
            table.AddTitleCell("Règle N°");
            table.AddTitleCell("Exigence");
            table.AddTitleCell("Validation");
            table.AddTitleCell("Commentaire");

            Phrase Comment(InspectionOutcome? outcome, string inspectorComment)
            {
                var result = new Phrase {new Chunk(inspectorComment, Fonts.Helvetica8BlackBoldItalic)};
                if (outcome == InspectionOutcome.NotOk || outcome == InspectionOutcome.PartiallyOk)
                {
                    if (!string.IsNullOrWhiteSpace(inspectorComment))
                        result.Add(new Chunk("\n"));

                    result.Add(new Chunk("Voir détails ci-dessous", Fonts.Helvetica8Black));
                }

                return result;
            }

            foreach (var resultItem in data)
            {
                table.AddCustomCell(resultItem.ConjunctElementCode, Fonts.Helvetica8Black, BackgroundColor(resultItem));
                table.AddCustomCell(resultItem.ShortName, Fonts.Helvetica8Black, BackgroundColor(resultItem));
                table.AddCustomCell(OutcomeString(resultItem.ResultOutcome), Fonts.Helvetica8Black,
                    OutcomeBackgroundColor(resultItem));
                table.AddCustomCell(Comment(resultItem.ResultOutcome, resultItem.ResultInspectorComment),
                    OutcomeDetailsBackgroundColor(resultItem, Colors.White));
            }

            table.AddTitleCell("Remarque générale", colspan: 4);
            table.AddCustomCell(string.IsNullOrWhiteSpace(model_.CommentForFarmer) ? " " : model_.CommentForFarmer,
                colspan: 4, font: Fonts.Helvetica8BlackItalic);
            table.AddTitleCell("Documents à livrer", colspan: 4);
            table.AddCustomCell(
                string.IsNullOrWhiteSpace(model_.ActionsOrDocuments) ? "Aucun" : model_.ActionsOrDocuments, colspan: 4,
                font: Fonts.Helvetica8BlackItalic);
            if (model_.DueDate.HasValue)
                table.AddCustomCell(new Phrase
                    {
                        new Chunk("Délai: ", Fonts.Helvetica8Black),
                        new Chunk(model_.DueDate.Value.ToShortDateString(), Fonts.Helvetica8BlackBoldItalic)
                    },
                    colspan: 4);

            table.AddCustomCell(OutcomeKey, colspan: 4, borderWidth: 0);
            table.AddCustomCell(ColorsKey, colspan: 4, borderWidth: 0);
            return table;
        }

        public AcPdfPTable Signatures()
        {
            var table = CustomTable(new[] {30f, 70f});

            void DisplaySignatureOrEmpty(string signature)
            {
                if (!string.IsNullOrWhiteSpace(signature) && signature.StartsWith(PngImgMarker))
                {
                    AddImageCellFromBase64(table, signature.Split(',')[1], 33);
                }
                else
                {
                    table.AddCustomCell("\n\n\n\n\n");
                }
            }

            table.AddCustomCell(
                "L'exploitant ou son représentant atteste avoir pris connaissance du présent rapport de contrôle.",
                Fonts.Helvetica12BlackBold, colspan: 2, borderWidth: 0);
            table.AddTitleCell(model_.HasProxy
                ? "[  ] L'exploitant ou [x] son représentant"
                : "[x] L'exploitant ou [  ] son représentant");
            table.AddTitleCell("Signature");
            table.AddCustomCell(!string.IsNullOrWhiteSpace(model_.ProxyName)
                ? model_.ProxyName
                : model_.Farm.FarmDisplay.CompleteName);
            DisplaySignatureOrEmpty(model_.FarmerSignatureImage);
            table.AddTitleCell("Contrôleur (ou gérant(e))");
            table.AddTitleCell("Signature");
            table.AddCustomCell(model_.DoneByInspector);
            DisplaySignatureOrEmpty(model_.InspectorSignatureImage);
            if (!string.IsNullOrWhiteSpace(model_.Inspector2) &&
                !string.IsNullOrWhiteSpace(model_.Inspector2SignatureImage))
            {
                table.AddTitleCell("Contrôleur ou préposé additionnel");
                table.AddTitleCell("Signature");
                table.AddCustomCell(model_.Inspector2);
                DisplaySignatureOrEmpty(model_.Inspector2SignatureImage);
            }

            if (model_.DoneOn.HasValue)
                table.AddCustomCell($"Fait à {model_.DoneInTownDisplay} le {model_.DoneOn.Value.ToShortDateString()}",
                    colspan: 2);

            return table;
        }

        private AcPdfPTable Objection()
        {
            Phrase OrganizationPhrase(Organization org)
            {
                return new Phrase
                {
                    new Chunk($"{org.CantonCode}: ", Fonts.Helvetica8BlackBold),
                    new Chunk($"{org.Name}: {org.Address}", Fonts.Helvetica8Black)
                };
            }

            var table = CustomTable(new[] {50f, 50f});
            table.AddCustomCell(
                "En cas de contestation, une réclamation écrite avec les points contestés peut être adressée dans les 3 jours ouvrables suivant le contrôle à l’organisme d’inspection ayant effectué le contrôle:",
                Fonts.Helvetica8BlackBold, colspan: 2, borderWidth: 0);
            table.AddCustomCell(OrganizationPhrase(Organization.Agripige), borderWidth: 0);
            table.AddCustomCell(OrganizationPhrase(Organization.Cobra), borderWidth: 0);
            table.AddCustomCell(OrganizationPhrase(Organization.Anapi), borderWidth: 0);
            table.AddCustomCell(OrganizationPhrase(Organization.Ajapi), borderWidth: 0);
            return table;
        }

        private AcPdfPTable CheckList()
        {
            var data = model_.InspectionResults.OrderBy(x => x.ConjunctElementCode);
            var table = CustomTable(new[] {4f, 4f, 4f, 4f, 40f, 5f, 40f});
            table.AddCustomCell("Résultat détaillé du contrôle des exigences", Fonts.Helvetica12BlackBold,
                borderWidth: 0, colspan: 7);
            table.AddTitleCell("Règle N°", colspan: 4);
            table.AddTitleCell("Exigences");
            table.AddTitleCell("Validation");
            table.AddTitleCell("Commentaire");

            bool isFirstRubric = true;
            foreach (var item in data)
            {
                var font = item.TreeLevel == 0 ? Fonts.Helvetica11BlackBold :
                    item.TreeLevel == 1 ? Fonts.Helvetica10BlackBold :
                    item.TreeLevel == 2 ? Fonts.Helvetica9BlackBold :
                    item.TreeLevel == 3 ? Fonts.Helvetica8BlackBold :
                    Fonts.Helvetica8Black;
                var colspan = 4 - item.TreeLevel;
                float borderWidth = item.ResultType == RecapResultListItemModel.ResultTypes.Point ? 0f : 0.5f;

                switch (item.TreeLevel)
                {
                    case 0 when !isFirstRubric:
                        table.AddCustomCell(" ", font, borderWidth: 0f, colspan: 7);
                        break;
                    case 1:
                    case 2:
                    case 3:
                        table.AddCustomCell("", colspan: item.TreeLevel, borderWidth: 0f);
                        break;
                }

                table.AddCustomCell(item.ElementCode, font, borderWidth: borderWidth, colspan: colspan,
                    backgroundColor: BackgroundColor(item));
                table.AddCustomCell(item.ShortName, font, borderWidth: borderWidth,
                    backgroundColor: BackgroundColor(item));
                table.AddCustomCell(OutcomeString(item.ResultOutcome), borderWidth: borderWidth,
                    backgroundColor: OutcomeBackgroundColor(item));
                AddResultCell(table, item, borderWidth, OutcomeDetailsBackgroundColor(item, Colors.White));
                isFirstRubric = false;
            }

            table.AddCustomCell(" ", colspan: 7, borderWidth: 0); // empty line
            table.AddCustomCell(OutcomeKey, colspan: 7, borderWidth: 0);
            table.AddCustomCell(ColorsKey, colspan: 7, borderWidth: 0);

            return table;
        }

        private void AddOrganizationCell(AcPdfPTable table)
        {
            var cell = DefaultCell(table.ColorDark, 0);

            Phrase OrganizationPhrase(Organization organization)
            {
                bool check = organization.IsCantonalOrgForCanton(model_.CantonCode);
                return new Phrase
                {
                    new Chunk(check ? "[x] " : "[  ] ", Fonts.Helvetica10BlackBold),
                    new Chunk(organization.Name + ": ", Fonts.Helvetica10BlackBold),
                    new Chunk(organization.Address, Fonts.Helvetica10Black)
                };
            }

            cell.AddElement(OrganizationPhrase(Organization.Agripige));
            cell.AddElement(OrganizationPhrase(Organization.Ajapi));
            cell.AddElement(OrganizationPhrase(Organization.Anapi));
            cell.AddElement(OrganizationPhrase(Organization.Cobra));

            table.AddCell(cell);
        }



        private void AddFarmCell(AcPdfPTable table, InspectionPdfModel.FarmModel farm)
        {
            var cell = DefaultCell(table.ColorDark);
            cell.AddElement(new Phrase(farm.FarmDisplay.Ktidb, Fonts.Helvetica12BlackBold));
            cell.AddElement(new Phrase(farm.FarmDisplay.CompleteName, Fonts.Helvetica12BlackBold));
            cell.AddElement(new Phrase(farm.FarmDisplay.Address, Fonts.Helvetica10Black));
            cell.AddElement(new Phrase("Email: " + farm.Email, Fonts.Helvetica10Black));
            table.AddCell(cell);
        }

        private void AddResultCell(AcPdfPTable table, RecapResultListItemModel resultModel, float borderWidth,
            BaseColor backgroundColor = null)
        {
            backgroundColor = backgroundColor ?? Colors.White;
            var cell = DefaultCell(table.ColorDark, borderWidth, backgroundColor);
            if (!string.IsNullOrWhiteSpace(resultModel.ResultInspectorComment))
                cell.AddElement(new Phrase
                {
                    new Chunk("Remarque contrôleur: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.ResultInspectorComment, Fonts.Helvetica8BlackBoldItalic),
                });

            if (!string.IsNullOrWhiteSpace(resultModel.ResultFarmerComment))
                cell.AddElement(new Phrase
                {
                    new Chunk("Remarque exploitant: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.ResultFarmerComment, Fonts.Helvetica8BlackBoldItalic),
                });

            if (resultModel.HasDefect)
                cell.AddElement(new Phrase
                {
                    new Chunk("Manquement dans la liste: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.DefectName, Fonts.Helvetica8BlackBoldItalic),
                });

            if (!string.IsNullOrWhiteSpace(resultModel.ResultDefectDescription))
                cell.AddElement(new Phrase
                {
                    new Chunk("Manquement constaté: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.ResultDefectDescription, Fonts.Helvetica8BlackBoldItalic),
                });

            if (resultModel.ResultSize.HasValue)
                cell.AddElement(new Phrase
                {
                    new Chunk("Ampleur du manquement: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.ResultSize.ToString(), Fonts.Helvetica8BlackBoldItalic),
                });

            if (resultModel.Seriousness!=null) //TODO a verifier
                cell.AddElement(new Phrase
                {
                    new Chunk("Gravité: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.Seriousness?.Name, Fonts.Helvetica8BlackBoldItalic),
                });

            //if (resultModel.Repetition != DefectRepetitions.NoDefect)
            //    cell.AddElement(new Phrase($"Récidive: {resultModel.Repetition.ToDisplayName()}", Fonts.Helvetica8Black));
            
            table.AddCell(cell);
        }

        private static PdfPCell DefaultCell(BaseColor borderColor, float borderWidth = 0.5f,
            BaseColor backgroundColor = null)
        {
            backgroundColor = backgroundColor ?? Colors.White;
            return new PdfPCell
            {
                MinimumHeight = 0.0f,
                BackgroundColor = backgroundColor,
                Colspan = 1,
                Rowspan = 1,
                BorderWidth = borderWidth,
                BorderColor = borderColor,
                VerticalAlignment = 5,
                HorizontalAlignment = 0
            };
        }

        private string OutcomeString(InspectionOutcome? outcome)
        {
            /*if (!outcome.HasValue)
                return "";

            return outcome == InspectionOutcome.Ok ? SpecialCharacters.Check :
                outcome == InspectionOutcome.PartiallyOk ? "P" :
                outcome == InspectionOutcome.NotOk ? "Non" :
                outcome == InspectionOutcome.NotApplicable ? "NA" :
                outcome == InspectionOutcome.NotInspected ? "NC" : "";
            */
            return "";
        }

        private BaseColor OutcomeBackgroundColor(RecapResultListItemModel item, BaseColor defaultColor = null)
        {
            defaultColor = defaultColor ?? BackgroundColor(item, Colors.LightGray);
            var okColor = Colors.Ok;
            var koColor = Colors.Ko;
            var pokColor = Colors.Pok;

            /*switch (item.ResultOutcome)
            {
                case null: return defaultColor;
                case InspectionOutcome.NotOk: return koColor;
                case InspectionOutcome.PartiallyOk: return pokColor;
                case InspectionOutcome.Ok: return okColor;
                case InspectionOutcome.NotApplicable: return defaultColor;
                case InspectionOutcome.NotInspected: return defaultColor;
            }*/

            return defaultColor;
        }

        private BaseColor OutcomeDetailsBackgroundColor(RecapResultListItemModel item, BaseColor defaultColor = null)
        {
            defaultColor = defaultColor ?? BackgroundColor(item, Colors.LightGray);
            var koColor = Colors.Ko;
            var pokColor = Colors.Pok;

            /*switch (item.ResultOutcome)
            {
                case null: return defaultColor;
                case InspectionOutcomes.NOk: return koColor;
                case InspectionOutcomes.PartiallyOk: return pokColor;
                case InspectionOutcomes.Ok: return defaultColor;
                case InspectionOutcomes.NotApplicable: return defaultColor;
                case InspectionOutcomes.NotInspected: return defaultColor;
            */

            return defaultColor;
        }

        private BaseColor BackgroundColor(RecapResultListItemModel item, BaseColor defaultColor = null)
        {
            defaultColor = defaultColor ?? Colors.White;

            /*if (item.HasAutoSetAncestor && item.ResultOutcome == InspectionOutcome.NotApplicable)
                return Colors.AutoSetNa;

            if (item.HasAutoSetAncestor && item.ResultOutcome == InspectionOutcome.NotInspected)
                return Colors.AutoSetNc;
            */
            return defaultColor;
        }

        private void AddImageCellFromFile(AcPdfPTable table,
            string imagePath,
            int scalePercent = 100,
            int rowspan = 1,
            float borderWidth = 0.5f,
            int horizontalAlignment = Element.ALIGN_LEFT)
        {
            if(!String.IsNullOrEmpty(imagePath))
            {
                var image = Image.GetInstance(imagePath);
                image.ScalePercent(scalePercent);
                AddImageCell(table, image, rowspan, borderWidth, horizontalAlignment);
            }

        }

        private void AddImageCellFromBase64(AcPdfPTable table,
            string base64,
            int scalePercent = 100,
            int rowspan = 1,
            float borderWidth = 0.5f,
            int horizontalAlignment = Element.ALIGN_LEFT)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            var image = Image.GetInstance(bytes);
            image.ScalePercent(scalePercent);
            AddImageCell(table, image, rowspan, borderWidth, horizontalAlignment);
        }

        private void AddImageCell(AcPdfPTable table,
            Image image,
            int rowspan = 1,
            float borderWidth = 0.5f,
            int horizontalAlignment = Element.ALIGN_LEFT)
        {
            var cell = new PdfPCell(image, false)
            {
                BorderWidth = borderWidth,
                BorderColor = table.ColorDark,
                Rowspan = rowspan,
                HorizontalAlignment = horizontalAlignment
            };
            table.AddCell(cell);
        }

        #endregion

        #region Helper

        public static string Filename(string domainShortName,
            int year,
            string ktidb)
        {
            return $"Rapport de controle {domainShortName} {year} {ktidb}";
        }

        public static string Filename(string ktidb)
        {
            return $"Rapport de controle {ktidb}";
        }

        #endregion


    }
}
