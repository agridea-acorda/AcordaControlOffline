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
        string InspectorComment { get; }
        string FarmerComment { get; }
        Defect Defect { get; }
        DefectSeriousness Seriousness { get; }
        //IList<File> Photos { get; set; }
        //IList<File> Attachments { get; set; }

        IResult SetOutcome(InspectionOutcome outcome);
        IResult SetInspectorComment(string comment);
        IResult SetFarmerComment(string comment);
        IResult SetDefect(Defect defect, DefectSeriousness seriousness);
    }
}