using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public interface IResult
    {
        string ConjunctElementCode { get; }
        string Name { get; }
        string ElementCode { get; }
        string ShortName { get; }

        InspectionOutcome Outcome { get; }
        bool IsAutoSet { get; }
        string InspectorComment { get; }
        string FarmerComment { get; }
        string Unit { get; }
        string Sort { get; }
        Defect Defect { get; }
        DefectSeriousness Seriousness { get; }

        public int? PointId { get; set; }
        public int? DefectId { get; set; }

        public dynamic ComboDefects { get; set; }
        //IList<File> Photos { get; set; }
        //IList<File> Attachments { get; set; }

        IResult SetOutcome(InspectionOutcome outcome);
        IResult SetAuto(bool isAutoSet = true);
        IResult SetInspectorComment(string comment);
        IResult SetFarmerComment(string comment);
        IResult SetDefect(Defect defect, DefectSeriousness seriousness);
        IResult SetDefectId(int? defectId);
    }
}