using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection {
    public class Appointment : ValueObject
    {
        public Appointment(DateTime? date, DateTime? firstContactDate, InspectionMode mode)
        {
            bool IsEmpty() => !date.HasValue && !firstContactDate.HasValue;
            if (!IsEmpty() && !date.HasValue)
                throw new ArgumentNullException($"{nameof(date)} must not be empty.");

            Date = date;
            FirstContactDate = firstContactDate;
            Mode = mode;
        }
        //public InspectionMode Mode => Date.HasValue ? InspectionMode.Scheduled : InspectionMode.Unscheduled;
        public InspectionMode Mode { get; set; }
        public DateTime? FirstContactDate { get; }
        public DateTime? Date { get; }

        public static Appointment None => new Appointment(null, null, null);
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date;
            yield return FirstContactDate;
            yield return Mode;
        }
    }
}