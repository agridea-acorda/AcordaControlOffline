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
        public IReadOnlyList<ResultModel> InspectionResults { get; set; }
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


        public static InspectionPdfModel FromDomain(Inspection.Inspection inspection, Farm.Farm farm, Checklist.Checklist checklist, string cantonCode, string logoPath)
        {
            var model = new InspectionPdfModel
            {
                CampaignName = inspection.Campaign.Name,
                CampaignYear = inspection.Campaign.Year,
                DomainShortName = inspection.Domain.ShortName,
                DomainName = inspection.Domain.ShortName,
                FocaaLogoPath = logoPath,
                InspectionResults = ResultModel.FromDomain(checklist),
                ActionsOrDocuments = inspection.Compliance.ActionsOrDocuments,
                DueDate = inspection.Compliance.DueDate,
                DoneOn = inspection.FinishStatus.DoneOn,
                //DoneInTownZip = inspection.FinishStatus.DoneInTown?.Zip ?? 0,
                //DoneInTownName = inspection.FinishStatus.DoneInTown?.Name,
                HasProxy = inspection.FarmerSignature.HasProxy,
                ProxyName = inspection.FarmerSignature.Proxy,
                DoneByInspector = inspection.InspectorSignature.Signatory,
                Inspector2 = inspection.Inspector2Signature.Signatory,
                FarmerSignatureImage = inspection.FarmerSignature.DataUrl,
                InspectorSignatureImage = inspection.InspectorSignature.DataUrl,
                Inspector2SignatureImage = inspection.Inspector2Signature.DataUrl,
                CantonCode = cantonCode,
                Farm = FarmModel.FromDomain(farm),
                CommentForFarmer = inspection.CommentForFarmer
            };
            return model;
        }

        #endregion
    }
}
