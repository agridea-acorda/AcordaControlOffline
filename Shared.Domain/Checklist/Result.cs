using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class Result : ITreeNode, IResult, IProgressable
    {
        protected Result(ITreeNode parent, SortedList<string, ITreeNode> children, string conjunctElementCode, string name, string elementCode, string shortName)
        {
            Parent = parent;
            Children = children;
            ConjunctElementCode = conjunctElementCode;
            Name = name;
            ElementCode = elementCode;
            ShortName = shortName;
        }

        public ITreeNode Parent { get; }
        public SortedList<string, ITreeNode> Children { get; }
        public string ConjunctElementCode { get; }
        public string Name { get; }
        public string ElementCode { get; }
        public string ShortName { get; }
        public InspectionOutcome Outcome { get; set; }
        public string InspectorComment { get; set; }
        public string FarmerComment { get; set; }
        public string DefectDescription { get; set; }
        public double? Size { get; set; }
        public DefectSeriousness Seriousness { get; set; }
        public IList<DefectAction> DefectActions { get; set; }
        public double Percent { get; private set; }
    }
}