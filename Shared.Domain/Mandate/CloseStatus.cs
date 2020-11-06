using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate {
    public class CloseStatus : ValueObject
    {
        public DateTime? CloseDate { get; }
        public string ClosedBy { get; }
        public bool IsClosed => CloseDate.HasValue;

        public static CloseStatus NotClosed = new CloseStatus(null, "");

        public CloseStatus(DateTime? closeDate, string closedBy)
        {
            if (closeDate.HasValue != !string.IsNullOrWhiteSpace(closedBy))
                throw new InvalidOperationException("Close-date and -by must be either both set or both empty.");

            CloseDate = closeDate;
            ClosedBy = closedBy;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CloseDate;
            yield return ClosedBy;
        }
    }
}