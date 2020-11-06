using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate {
    public class FinishStatus : ValueObject
    {
        public string DoneByInspector { get; }
        public DateTime? DoneOn { get; }
        public bool IsFinished => DoneOn.HasValue;

        public static FinishStatus NotFinished => new FinishStatus(null, "");

        public FinishStatus(DateTime? doneOn, string doneByInspector)
        {
            if (doneOn.HasValue != !string.IsNullOrWhiteSpace(DoneByInspector))
                throw new InvalidOperationException("Done-date and -inspector must be either both set or both empty.");

            DoneOn = doneOn;
            DoneByInspector = doneByInspector;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DoneOn;
            yield return DoneByInspector;
        }
    }
}