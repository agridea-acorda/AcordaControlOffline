using System;
using System.Collections.Generic;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Checklist : AggregateRoot
    {

    }

    public interface ITreeNode
    {
        ITreeNode Parent { get; }
        IReadOnlyList<ITreeNode> Children { get; }
        string ConjunctElementCode { get; }
        string Name { get; }
        string ElementCode { get; }
        string ShortName { get; }
    }

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

    public class DefectAction
    {

    }

    public class DefectSeriousness : CodeNameValueObject
    {
        public static DefectSeriousness Empty => new DefectSeriousness(0, "");
        public static DefectSeriousness Small => new DefectSeriousness(1, "Minime");
        public static DefectSeriousness Medium => new DefectSeriousness(2, "Important");
        public static DefectSeriousness Serious => new DefectSeriousness(3, "Grave");
        public DefectSeriousness(int code, string name) : base(code, name) { }

        protected override void ValidateCtorParams(int code, string name)
        {
            bool IsEmpty() => code == 0 && name == "";
            if (IsEmpty()) return;
            base.ValidateCtorParams(code, name);
        }
    }
}
