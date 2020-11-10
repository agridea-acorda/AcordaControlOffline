using System;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class PointResult : LeafResult
    {
        public PointResult(string conjunctElementCode, string elementCode, string shortName, string name = "") 
            : base(conjunctElementCode, elementCode, shortName, name)
        { }

        protected override Result AddChild(string sortKey, ITreeNode<Result> treeNode)
        {
            base.AddChild(sortKey, treeNode);
            return this;
        }

        public Defect Defect { get; private set; }

        public PointResult SetDefect(Defect defect)
        {
            Defect = defect ?? throw new ArgumentNullException();
            return this;
        }
    }
}