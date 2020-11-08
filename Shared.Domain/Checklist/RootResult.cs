using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class RootResult : Result
    {
        protected RootResult(ITreeNode parent, SortedList<string, ITreeNode> children, string conjunctElementCode, string name, string elementCode, string shortName) 
            : base(null, children, conjunctElementCode, name, elementCode, shortName)
        { }

        protected RootResult(SortedList<string, ITreeNode> children, string conjunctElementCode, string name,
            string elementCode, string shortName)
            : this(null, children, conjunctElementCode, name, elementCode, shortName)
        { }
    }
}