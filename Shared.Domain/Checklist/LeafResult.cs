using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class LeafResult : Result
    {
        protected LeafResult(ITreeNode parent, SortedList<string, ITreeNode> children, string conjunctElementCode, string name, string elementCode, string shortName)
            : base(parent, new SortedList<string, ITreeNode>(), conjunctElementCode, name, elementCode, shortName)
        { }

        protected LeafResult(ITreeNode parent, string conjunctElementCode, string name, string elementCode, string shortName)
            : this(parent, new SortedList<string, ITreeNode>(), conjunctElementCode, name, elementCode, shortName)
        { }
    }
}