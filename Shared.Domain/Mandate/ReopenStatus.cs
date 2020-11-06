using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate {
    public class ReopenStatus : ValueObject
    {
        public DateTime? ReopenDate { get; }
        public string ReopenedBy { get; }
        public bool IsReopened => ReopenDate.HasValue;
        
        public static ReopenStatus NotReopened = new ReopenStatus(null, "");
        
        public ReopenStatus(DateTime? reopenDate, string reopenedBy)
        {
            if (reopenDate.HasValue != !string.IsNullOrWhiteSpace(reopenedBy))
                throw new InvalidOperationException("Reopen-date and -by must be either both set or both empty.");
            
            ReopenDate = reopenDate;
            ReopenedBy = reopenedBy;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ReopenDate;
            yield return ReopenedBy;
        }
    }
}