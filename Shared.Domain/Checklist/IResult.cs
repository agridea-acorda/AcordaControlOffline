using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public interface IResult
    {
        InspectionOutcome Outcome { get; set; }
        string InspectorComment { get; set; }
        string FarmerComment { get; set; }
        string DefectDescription { get; set; }
        double? Size { get; set; }
        DefectSeriousness Seriousness { get; set; }
        //IList<File> Photos { get; set; }
        //IList<File> Attachments { get; set; }
        IList<DefectAction> DefectActions { get; set; }
    }
}