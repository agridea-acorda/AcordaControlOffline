using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class RubricResult : RootResult
    {
        public RubricResult(string conjunctElementCode, string elementCode, string shortName, string name = "") 
            : base(conjunctElementCode, name, elementCode, shortName)
        { }

        public RubricResult AddChild<T>(string sortKey, T child)
            where T: Result
        {
            base.AddChild(sortKey, child);
            return this;
        }
    }
}