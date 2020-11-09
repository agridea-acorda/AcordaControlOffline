using System;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class PointResult : LeafResult
    {
        public PointResult(string conjunctElementCode, string elementCode, string shortName, string name = "") 
            : base(conjunctElementCode, elementCode, shortName, name)
        { }

        public Defect Defect { get; private set; }

        public PointResult SetParent<T>(T parent)
            where T : ITreeNode
        {
            base.SetParent(parent);
            return this;
        }

        public PointResult SetDefect(Defect defect)
        {
            Defect = defect ?? throw new ArgumentNullException();
            return this;
        }
    }
}