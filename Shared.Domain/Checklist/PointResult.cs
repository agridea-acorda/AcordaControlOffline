using System;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class PointResult : LeafResult
    {
        public PointResult(string conjunctElementCode, string name, string elementCode, string shortName) 
            : base(conjunctElementCode, name, elementCode, shortName)
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