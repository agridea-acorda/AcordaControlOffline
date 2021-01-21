using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model
{
    public class InspectionPdfModel
    {
        #region Properties

        public string CampaignName { get; set; }
        public int CampaignYear { get; set; }
        public string DomainShortName { get; set; }
        public string DomainName { get; set; }
        public string FocaaLogoPath { get; set; }
        public IReadOnlyList<RecapResultListItemModel> InspectionResults { get; set; }
        public FarmModel Farm { get; set; }
        public string ActionsOrDocuments { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DoneOn { get; set; }
        public int DoneInTownZip { get; set; }
        public string DoneInTownName { get; set; }
        public string DoneInTownDisplay => DoneInTownName;
        public bool HasProxy { get; set; }
        public string ProxyName { get; set; }
        public string DoneByInspector { get; set; }
        public string Inspector2 { get; set; }
        public string FarmerSignatureImage { get; set; }
        public string InspectorSignatureImage { get; set; }
        public string Inspector2SignatureImage { get; set; }
        public string CantonCode { get; set; }
        public string CommentForFarmer { get; set; }


        public class FarmModel
        {
            public FarmDisplayModel FarmDisplay { get; }

            public string Email { get; }

            public FarmModel(FarmDisplayModel farmDisplayModel, string email)
            {
                FarmDisplay = farmDisplayModel;
                Email = email;
            }
        }

        public static InspectionPdfModel FromInspection(Inspection.Inspection inspection, FarmDisplayModel farmDisplay, string cantonCode, string logoPath)
        {
            var model = new InspectionPdfModel
            {
                CampaignName = inspection.Campaign.Name,
                CampaignYear = inspection.Campaign.Year,
                DomainShortName = inspection.Domain.ShortName,
                //TODO recuperer les données
                //DomainName = inspection.Domain.Name,
                //FocaaLogoPath = logoPath,
                //InspectionResults = RecapResultListItemModelFactory.All(inspection),
                //ActionsOrDocuments = inspection.ActionsOrDocuments,
                //DueDate = inspection.DueDate,
                //DoneOn = inspection.DoneOn,
                //DoneInTownZip = inspection.DoneInTown?.Zip ?? 0,
                //DoneInTownName = inspection.DoneInTown?.Name,
                //HasProxy = inspection.HasProxy,
                //ProxyName = inspection.ProxyName,
                //DoneByInspector = inspection.DoneByInspector,
                //Inspector2 = inspection.Inspector2,
                //FarmerSignatureImage = inspection.FarmerSignatureImage,
                //InspectorSignatureImage = inspection.InspectorSignatureImage,
                //Inspector2SignatureImage = inspection.Inspector2SignatureImage,
                CantonCode = cantonCode,
                Farm = new FarmModel(farmDisplay, "test@mail.com"/*farmDisplay..Email
                                                              ?? inspection.Farm.ParentList?.FirstOrDefault()?.Person?.Email
                                                              ?? ""*/),
                //CommentForFarmer = inspection.CommentForFarmer
            };
            return model;
        }

        #endregion
    }
}
