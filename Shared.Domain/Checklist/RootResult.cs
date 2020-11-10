using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class RootResult : Result
    {
        protected RootResult(string conjunctElementCode, string name, string elementCode, string shortName = "") 
            : base(conjunctElementCode, elementCode, shortName, name)
        { }

        public override void SetParent(ITreeNode<Result> parent)
        { }
    }
}