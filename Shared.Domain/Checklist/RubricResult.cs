using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class RubricResult : RootResult
    {
        public RubricResult(SortedList<string, ITreeNode> children, string conjunctElementCode, string name, string elementCode, string shortName) 
            : base(children, conjunctElementCode, name, elementCode, shortName)
        { }
    }
}