
using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Shared
{
    public class PdfDocumentBase : PdfBase
    {
        #region Constants

        protected const string CheckSymbol = "\u0033";
        private const float WatermarkFontSize = 80;
        private const float WatermarkOpacity = 0.3f;
        protected const string WatermarkText = "Provisoire";
        public static readonly BaseColor ChangedColor = new BaseColor(247, 240, 177);
        protected static readonly Font.FontFamily DefaultFontFamily = Font.FontFamily.HELVETICA;
        protected static readonly Font DefaultBold = new Font(DefaultFontFamily, 8, Font.BOLD);
        protected static readonly float DefaultBottomMargin = 30f;
        protected static readonly BaseColor DefaultDarkColor = new BaseColor(84, 139, 84);
        protected static readonly Font DefaultHeader1 = new Font(DefaultFontFamily, 12, Font.BOLD);
        protected static readonly Font DefaultHeader2 = new Font(DefaultFontFamily, 10, Font.BOLD);
        protected static readonly Font DefaultHeaderBoldItalic = new Font(DefaultFontFamily, 10, Font.BOLDITALIC);
        protected static readonly Font DefaultItalic = new Font(DefaultFontFamily, 8, Font.ITALIC);
        protected static readonly float DefaultLeftMargin = 30f;
        protected static readonly BaseColor DefaultLightColor = new BaseColor(180, 238, 180);
        protected static readonly Font DefaultNormal = new Font(DefaultFontFamily, 8, Font.NORMAL);
        protected static readonly int DefaultPageNumberDigit = 3;
        protected static readonly float DefaultRightMargin = 30f;
        protected static readonly float DefaultTopMargin = 30f;
        public static readonly BaseColor DeletedColor = new BaseColor(204, 204, 204);

        protected static readonly Font Font6BoldWhite = new Font(Font.FontFamily.HELVETICA,
            6,
            Font.BOLD,
            BaseColor.WHITE);

        public static readonly Font Font6Italic = new Font(DefaultFontFamily, 6f, Font.ITALIC);
        public static readonly Font Font6Normal = new Font(DefaultFontFamily, 6f, Font.NORMAL);
        public static readonly Font Font6Striked = new Font(DefaultFontFamily, 6f, Font.STRIKETHRU);
        private static readonly Font FooterFont = new Font(DefaultFontFamily, 6, Font.NORMAL);
        protected static readonly int HeaderBorder = Rectangle.NO_BORDER;

        #endregion

        #region Members

        private readonly string currentUser_;

        protected readonly Font Font8BoldWhite = new Font(Font.FontFamily.HELVETICA,
            7,
            Font.BOLD,
            BaseColor.WHITE);

        private readonly bool showGeneratedByAndDateInFooter_;
        protected readonly Font Symbols = new Font(Font.FontFamily.ZAPFDINGBATS, 6f);
        protected Image CantonIcon;
        private PdfTemplate footer_;
        protected Image Logo;
        private float pageCountFooterElementFooterHeight_;
        private float pageCountFooterElementWidth_;
        protected int PageNumber;
        private Font headerFont_;

        #endregion

        #region Properties

        public Font Header1 { get; set; }
        public Font Header2 { get; set; }
        public Font HeaderBoldItalic { get; set; }
        public int PageNumberDigit { get; set; }
        public int PageNumberPosition { get; set; }
        public string Watermark { get; set; }

        #endregion

        #region Initialization

        public PdfDocumentBase(string currentUser, bool showGeneratedByAndDateInFooter = true)
        {
            SetPropertiesDefault();
            SetFixedValues();
            currentUser_ = currentUser;
            showGeneratedByAndDateInFooter_ = showGeneratedByAndDateInFooter;
        }

        #endregion

        #region Services

        protected AcPdfPTable CustomTable(float[] relativeWidths, float percentWidth = 100f, int borderWidth = 0)
        {
            var table = new AcPdfPTable(relativeWidths)
            {
                WidthPercentage = percentWidth,
                ColorDark = DarkColor,
                FontNormal = Normal,
                FontBold = Bold,
                ColorLight = LightColor,
                SpacingBefore = 10
            };
            table.DefaultCell.Border = borderWidth;
            return table;
        }

        #endregion

        #region PdfBase

        protected override void StartDocument(PdfWriter writer, Document document)
        {
            footer_ = writer.DirectContent.CreateTemplate(pageCountFooterElementWidth_,
                pageCountFooterElementFooterHeight_);
        }

        protected override void EndDocument(PdfWriter writer, Document document)
        {
            ColumnText.ShowTextAligned(footer_,
                Element.ALIGN_LEFT,
                new Phrase($"{PageNumber}", FooterFont),
                2,
                (pageCountFooterElementFooterHeight_ - FooterFont.Size) / 2,
                0);
        }

        protected override void StartPage(PdfWriter writer, Document document)
        {
            PageNumber++;
        }

        protected override void EndPage(PdfWriter writer, Document document)
        {
            AddHeader(writer, document);
            AddWatermark(writer, document.PageSize);
            AddFooter(writer, document);
        }

        #endregion PdfBase

        #region Hooks

        protected virtual void AddHeader(PdfWriter writer, Document document)
        {
        }

        protected virtual void AddFooter(PdfWriter writer,
            Document document,
            int offsetHeight = 0,
            int absoluteYposFromBottom = -1)
        {
            if (PageNumberDigit <= 0)
                return;
            var userTable = new PdfPTable(1)
            {
                TotalWidth = 400,
                LockedWidth = true
            };
            userTable.DefaultCell.FixedHeight = pageCountFooterElementFooterHeight_;

            if (showGeneratedByAndDateInFooter_)
            {
                userTable.AddCell(
                    new PdfPCell(new Phrase($"Généré le {DateTime.Now:dd.MM.yyyy HH:mm:ss} par {currentUser_} ",
                        FooterFont))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Border = HeaderBorder
                    });
            }

            float ypos = absoluteYposFromBottom >= 0
                ? absoluteYposFromBottom
                : document.BottomMargin - pageCountFooterElementFooterHeight_ - PageNumberPosition - offsetHeight;
            userTable.WriteSelectedRows(0,
                -1,
                document.LeftMargin,
                ypos,
                writer.DirectContent);

            var table = new PdfPTable(new[] {5f, 1f, 5f})
            {
                TotalWidth = pageCountFooterElementWidth_,
                LockedWidth = true
            };
            table.DefaultCell.FixedHeight = pageCountFooterElementFooterHeight_;

            table.AddCell(new PdfPCell(new Phrase($"Page {writer.PageNumber}", FooterFont))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = HeaderBorder
            });
            table.AddCell(new PdfPCell(new Phrase("/", FooterFont))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = HeaderBorder
            });
            table.AddCell(new PdfPCell(Image.GetInstance(footer_)) {Border = HeaderBorder});

            table.WriteSelectedRows(0,
                -1,
                document.PageSize.Width - pageCountFooterElementWidth_,
                ypos,
                writer.DirectContent);
        }

        #endregion Hooks

        #region Helpers

        private void SetPropertiesDefault()
        {
            TopMargin = DefaultTopMargin;
            BottomMargin = DefaultBottomMargin;
            LeftMargin = DefaultLeftMargin;
            RightMargin = DefaultRightMargin;
            LightColor = DefaultLightColor;
            DarkColor = DefaultDarkColor;
            Normal = DefaultNormal;
            Italic = DefaultItalic;
            Bold = DefaultBold;
            HeaderBoldItalic = DefaultHeaderBoldItalic;
            Header1 = DefaultHeader1;
            Header2 = DefaultHeader2;
            PageNumberDigit = DefaultPageNumberDigit;
            PageNumberPosition = 5;
        }

        private void SetFixedValues()
        {
            pageCountFooterElementFooterHeight_ = FooterFont.Size + 4;
            pageCountFooterElementWidth_ = (PageNumberDigit * 2 + 3) * (FooterFont.Size + 2);
            headerFont_ = Header1;
        }

        protected void AddWatermark(PdfWriter writer, Rectangle page)
        {
            if (string.IsNullOrEmpty(Watermark))
                return;

            var watermarkFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
            var watermarkColor = BaseColor.DARK_GRAY;

            var content = writer.DirectContent;
            var gstate = new PdfGState
            {
                FillOpacity = WatermarkOpacity,
                StrokeOpacity = WatermarkOpacity
            };
            content.SaveState();
            content.SetGState(gstate);
            content.BeginText();
            content.SetColorFill(watermarkColor);
            content.SetFontAndSize(watermarkFont, WatermarkFontSize);
            float angle = GetHypotenuseAngleInDegreesFrom(page.Height, page.Width);
            content.ShowTextAligned(PdfContentByte.ALIGN_CENTER,
                Watermark,
                page.Width / 2,
                page.Height / 2,
                angle);
            content.EndText();
            content.RestoreState();
        }

        private static float GetHypotenuseAngleInDegreesFrom(double opposite, double adjacent)
        {
            return (float) (Math.Atan2(opposite, adjacent) * (180 / Math.PI));
        }

        #endregion Helpers

        /*public class PdfDocumentBase : PdfPage
            {
                #region Constants
    
                protected const string CheckSymbol = "\u0033";
                private const float WatermarkFontSize = 80;
                private const float WatermarkOpacity = 0.3f;
                protected const string WatermarkText = "Provisoire";
                public static readonly Color ChangedColor = new DeviceRgb(247, 240, 177);
                protected static readonly string DefaultFontFamily = StandardFonts.HELVETICA;
                protected static readonly string DefaultBold = StandardFonts.HELVETICA_BOLD;//new Font(DefaultFontFamily, 8, Font.BOLD);
                protected static readonly float DefaultBottomMargin = 30f;
                public static readonly Color DefaultDarkColor = new DeviceRgb(84, 139, 84);
                protected static readonly PdfFont DefaultHeader1 = CreateFon(DefaultFontFamily, 12, Font.BOLD);
                protected static readonly PdfFont DefaultHeader2 = new Font(DefaultFontFamily, 10, Font.BOLD);
                protected static readonly PdfFont DefaultHeaderBoldItalic = new Font(DefaultFontFamily, 10, Font.BOLDITALIC);
                protected static readonly PdfFont DefaultItalic = new Font(DefaultFontFamily, 8, Font.ITALIC);
                protected static readonly float DefaultLeftMargin = 30f;
                protected static readonly BaseColor DefaultLightColor = new BaseColor(180, 238, 180);
                protected static readonly PdfFont DefaultNormal = new Font(DefaultFontFamily, 8, Font.NORMAL);
                protected static readonly int DefaultPageNumberDigit = 3;
                protected static readonly float DefaultRightMargin = 30f;
                protected static readonly float DefaultTopMargin = 30f;
                public static readonly BaseColor DeletedColor = new BaseColor(204, 204, 204);
                protected static readonly PdfFont Font6BoldWhite = new Font(Font.FontFamily.HELVETICA,
                                                                         6,
                                                                         Font.BOLD,
                                                                         BaseColor.WHITE);
                public static readonly PdfFont Font6Italic = new Font(DefaultFontFamily, 6f, Font.ITALIC);
                public static readonly PdfFont Font6Normal = new Font(DefaultFontFamily, 6f, Font.NORMAL);
                public static readonly PdfFont Font6Striked = new Font(DefaultFontFamily, 6f, Font.STRIKETHRU);
                private static readonly PdfFont FooterFont = new Font(DefaultFontFamily, 6, Font.NORMAL);
                protected static readonly int HeaderBorder = Rectangle.NO_BORDER;
    
                #endregion
    
                #region Members
    
                private readonly string currentUser_;
                protected readonly Font Font8BoldWhite = new Font(Font.FontFamily.HELVETICA,
                                                                  7,
                                                                  Font.BOLD,
                                                                  BaseColor.WHITE);
                private readonly bool showGeneratedByAndDateInFooter_;
                protected readonly Font Symbols = new Font(Font.FontFamily.ZAPFDINGBATS, 6f);
                protected Image CantonIcon;
                private PdfTemplate footer_;
                protected Image Logo;
                private float pageCountFooterElementFooterHeight_;
                private float pageCountFooterElementWidth_;
                protected int PageNumber;
                private Font headerFont_;
    
                #endregion
    
                #region Properties
    
                public Font Header1 { get; set; }
                public Font Header2 { get; set; }
                public Font HeaderBoldItalic { get; set; }
                public int PageNumberDigit { get; set; }
                public int PageNumberPosition { get; set; }
                public string Watermark { get; set; }
    
                #endregion
    
                #region Initialization
    
                public PdfDocumentBase(string currentUser, bool showGeneratedByAndDateInFooter = true)
                {
                    SetPropertiesDefault();
                    SetFixedValues();
                    currentUser_ = currentUser;
                    showGeneratedByAndDateInFooter_ = showGeneratedByAndDateInFooter;
                }
    
                #endregion
    
                #region Services
    
                protected AcPdfPTable CustomTable(float[] relativeWidths, float percentWidth = 100f, int borderWidth = 0)
                {
                    var table = new AcPdfPTable(relativeWidths)
                    {
                        WidthPercentage = percentWidth,
                        ColorDark = DarkColor,
                        FontNormal = Normal,
                        FontBold = Bold,
                        ColorLight = LightColor,
                        SpacingBefore = 10
                    };
                    table.DefaultCell.Border = borderWidth;
                    return table;
                }
    
                #endregion
    
                #region PdfBase
    
                protected override void StartDocument(PdfWriter writer, Document document)
                {
                    footer_ = writer.DirectContent.CreateTemplate(pageCountFooterElementWidth_, pageCountFooterElementFooterHeight_);
                }
    
                protected override void EndDocument(PdfWriter writer, Document document)
                {
                    ColumnText.ShowTextAligned(footer_,
                                               Element.ALIGN_LEFT,
                                               new Phrase($"{PageNumber}", FooterFont),
                                               2,
                                               (pageCountFooterElementFooterHeight_ - FooterFont.Size) / 2,
                                               0);
                }
    
                protected override void StartPage(PdfWriter writer, Document document)
                {
                    PageNumber++;
                }
    
                protected override void EndPage(PdfWriter writer, Document document)
                {
                    AddHeader(writer, document);
                    AddWatermark(writer, document.PageSize);
                    AddFooter(writer, document);
                }
    
                #endregion PdfBase
    
                #region Hooks
    
                protected virtual void AddHeader(PdfWriter writer, Document document) { }
    
                protected virtual void AddFooter(PdfWriter writer,
                                                 Document document,
                                                 int offsetHeight = 0,
                                                 int absoluteYposFromBottom = -1)
                {
                    if (PageNumberDigit <= 0)
                        return;
                    var userTable = new PdfPTable(1)
                    {
                        TotalWidth = 400,
                        LockedWidth = true
                    };
                    userTable.DefaultCell.FixedHeight = pageCountFooterElementFooterHeight_;
    
                    if (showGeneratedByAndDateInFooter_)
                    {
                        userTable.AddCell(new PdfPCell(new Phrase($"Généré le {DateTime.Now:dd.MM.yyyy HH:mm:ss} par {currentUser_} ", FooterFont))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            Border = HeaderBorder
                        });
                    }
    
                    float ypos = absoluteYposFromBottom >= 0 ? absoluteYposFromBottom : document.BottomMargin - pageCountFooterElementFooterHeight_ - PageNumberPosition - offsetHeight;
                    userTable.WriteSelectedRows(0,
                                                -1,
                                                document.LeftMargin,
                                                ypos,
                                                writer.DirectContent);
    
                    var table = new PdfPTable(new[] { 5f, 1f, 5f })
                    {
                        TotalWidth = pageCountFooterElementWidth_,
                        LockedWidth = true
                    };
                    table.DefaultCell.FixedHeight = pageCountFooterElementFooterHeight_;
    
                    table.AddCell(new PdfPCell(new Phrase($"Page {writer.PageNumber}", FooterFont))
                    {
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Border = HeaderBorder
                    });
                    table.AddCell(new PdfPCell(new Phrase("/", FooterFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Border = HeaderBorder
                    });
                    table.AddCell(new PdfPCell(Image.GetInstance(footer_)) { Border = HeaderBorder });
    
                    table.WriteSelectedRows(0,
                                            -1,
                                            document.PageSize.Width - pageCountFooterElementWidth_,
                                            ypos,
                                            writer.DirectContent);
                }
    
                #endregion Hooks
    
                #region Helpers
    
                private void SetPropertiesDefault()
                {
                    TopMargin = DefaultTopMargin;
                    BottomMargin = DefaultBottomMargin;
                    LeftMargin = DefaultLeftMargin;
                    RightMargin = DefaultRightMargin;
                    LightColor = DefaultLightColor;
                    DarkColor = DefaultDarkColor;
                    Normal = DefaultNormal;
                    Italic = DefaultItalic;
                    Bold = DefaultBold;
                    HeaderBoldItalic = DefaultHeaderBoldItalic;
                    Header1 = DefaultHeader1;
                    Header2 = DefaultHeader2;
                    PageNumberDigit = DefaultPageNumberDigit;
                    PageNumberPosition = 5;
                }
    
                private void SetFixedValues()
                {
                    pageCountFooterElementFooterHeight_ = FooterFont.Size + 4;
                    pageCountFooterElementWidth_ = (PageNumberDigit * 2 + 3) * (FooterFont.Size + 2);
                    headerFont_ = Header1;
                }
    
                protected void AddWatermark(PdfWriter writer, Rectangle page)
                {
                    if (string.IsNullOrEmpty(Watermark))
                        return;
    
                    var watermarkFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
                    var watermarkColor = BaseColor.DARK_GRAY;
    
                    var content = writer.DirectContent;
                    var gstate = new PdfGState
                    {
                        FillOpacity = WatermarkOpacity,
                        StrokeOpacity = WatermarkOpacity
                    };
                    content.SaveState();
                    content.SetGState(gstate);
                    content.BeginText();
                    content.SetColorFill(watermarkColor);
                    content.SetFontAndSize(watermarkFont, WatermarkFontSize);
                    float angle = GetHypotenuseAngleInDegreesFrom(page.Height, page.Width);
                    content.ShowTextAligned(PdfContentByte.ALIGN_CENTER,
                                            Watermark,
                                            page.Width / 2,
                                            page.Height / 2,
                                            angle);
                    content.EndText();
                    content.RestoreState();
                }
    
                private static float GetHypotenuseAngleInDegreesFrom(double opposite, double adjacent)
                {
                    return (float)(Math.Atan2(opposite, adjacent) * (180 / Math.PI));
                }
    
                #endregion Helpers
            }*/
    }

}
