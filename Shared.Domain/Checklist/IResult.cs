﻿using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public interface IResult
    {
        InspectionOutcome Outcome { get; }
        string InspectorComment { get; }
        string FarmerComment { get; }
        string DefectDescription { get; }
        double? Size { get; }
        DefectSeriousness Seriousness { get; }
        //IList<File> Photos { get; set; }
        //IList<File> Attachments { get; set; }
        IList<DefectAction> DefectActions { get; }
    }
}