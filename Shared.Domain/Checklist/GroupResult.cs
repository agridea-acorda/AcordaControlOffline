using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class GroupResult : Result
    {
        public GroupResult(string conjunctElementCode, string name, string elementCode, string shortName) 
            : base(conjunctElementCode, name, elementCode, shortName)
        { }

        public GroupResult TryAddChild<T>(string sortKey, T child)
            where T : ITreeNode
        {
            base.TryAddChild(sortKey, child);
            return this;
        }

        public GroupResult SetParent<T>(T parent)
            where T: ITreeNode
        {
            base.SetParent(parent);
            return this;
        }
    }
}