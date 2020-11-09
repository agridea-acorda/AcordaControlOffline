using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class LeafResult : Result
    {
        protected LeafResult(string conjunctElementCode, string name, string elementCode, string shortName)
            : base(conjunctElementCode, name, elementCode, shortName)
        { }

        protected override Result TryAddChild(string sortKey, ITreeNode child)
        {
            return this;
        }
    }
}