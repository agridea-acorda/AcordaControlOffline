namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class PointResult : LeafResult
    {
        public PointResult(ITreeNode parent, string conjunctElementCode, string name, string elementCode, string shortName) 
            : base(parent, conjunctElementCode, name, elementCode, shortName)
        { }

        public Defect Defect { get; private set; }
    }
}