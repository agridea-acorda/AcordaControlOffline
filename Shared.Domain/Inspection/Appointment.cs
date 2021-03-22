using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection {
    public class Appointment : ValueObject
    {
        public Appointment(DateTime? date, DateTime? firstContactDate)
        {
            bool IsEmpty() => !date.HasValue && !firstContactDate.HasValue;
            if (!IsEmpty() && !date.HasValue)
                throw new ArgumentNullException($"{nameof(date)} must not be empty.");

            Date = date;
            FirstContactDate = firstContactDate;
        }
        //public InspectionMode Mode => Date.HasValue ? InspectionMode.Scheduled : InspectionMode.Unscheduled;
        public InspectionMode Mode
        {
            get => Date.HasValue ? InspectionMode.Scheduled : InspectionMode.Unscheduled;
            set
            {
                // do nothing, this is just to bypass the lack of serialization for get-only properties
                var unused = value;
            }
        }
        public DateTime? FirstContactDate { get; }
        public DateTime? Date { get; }

        public static Appointment None => new Appointment(null, null);
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date;
            yield return FirstContactDate;
        }
    }
}