using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class RootResult : Result
    {
        protected RootResult(string conjunctElementCode, string name, string elementCode, string shortName) 
            : base(conjunctElementCode, name, elementCode, shortName)
        { }

        protected override Result SetParent(ITreeNode parent)
        {
            return this;
        }
    }
}