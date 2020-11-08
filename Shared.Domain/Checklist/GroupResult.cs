using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class GroupResult : Result
    {
        public GroupResult(ITreeNode parent, SortedList<string, ITreeNode> children, string conjunctElementCode, string name, string elementCode, string shortName) 
            : base(parent, children, conjunctElementCode, name, elementCode, shortName)
        { }
    }
}