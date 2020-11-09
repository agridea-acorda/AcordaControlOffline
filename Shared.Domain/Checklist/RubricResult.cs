using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class RubricResult : RootResult
    {
        public RubricResult(string conjunctElementCode, string name, string elementCode, string shortName) 
            : base(conjunctElementCode, name, elementCode, shortName)
        { }

        public RubricResult TryAddChild<T>(string sortKey, T child)
            where T: ITreeNode
        {
            base.TryAddChild(sortKey, child);
            return this;
        }
    }
}