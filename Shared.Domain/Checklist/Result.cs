using System;
using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class Result : ITreeNode, IResult, IProgressable
    {
        protected Result(string conjunctElementCode, string name, string elementCode, string shortName)
        {
            Children = new SortedList<string, ITreeNode>();
            DefectActions = new List<DefectAction>();
            ConjunctElementCode = conjunctElementCode;
            Name = name;
            ElementCode = elementCode;
            ShortName = shortName;
        }

        protected virtual Result SetParent(ITreeNode parent)
        {
            Parent = parent ?? throw new ArgumentNullException();
            return this;
        }

        protected virtual Result TryAddChild(string sortKey, ITreeNode child)
        {
            if (string.IsNullOrWhiteSpace(sortKey))
                throw new ArgumentNullException($"{nameof(sortKey)} must not be empty.");
            
            if (child == null)
                throw new ArgumentNullException();
            
            Children.TryAdd(sortKey, child);

            return this;
        }

        public ITreeNode Parent { get; private set; }
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
        public IList<DefectAction> DefectActions { get; private set; }
        public double Percent { get; private set; }
    }
}