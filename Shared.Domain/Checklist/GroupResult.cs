using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class GroupResult : Result
    {
        public GroupResult(string conjunctElementCode, string elementCode, string shortName, string name = "") 
            : base(conjunctElementCode, elementCode, shortName, name)
        { }

        public GroupResult AddChild<T>(string sortKey, T child)
            where T : Result
        {
            base.AddChild(sortKey, child);
            return this;
        }
    }
}