using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class LeafResult : Result
    {
        protected LeafResult(string conjunctElementCode, string elementCode, string shortName, string name = "")
            : base(conjunctElementCode, elementCode, shortName, name)
        { }

        protected override Result AddChild(string sortKey, ITreeNode child)
        {
            return this;
        }
    }
}