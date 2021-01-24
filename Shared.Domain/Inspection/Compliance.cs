using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection {
    public class Compliance : ValueObject
    {
        public string ActionsOrDocuments { get; }
        public DateTime? DueDate { get; }
        public bool DueDateNotRespected { get; }
        public bool DueDateRespected { get; }
        public bool FurtherInvestigationNeeded { get; }
        public bool IncompleteOrNonCompliant { get; }
        public bool IsPastDeadline => DueDate.HasValue && DateTime.Now > DueDate;
        public bool IsLateOrNotCompliant => DueDateNotRespected || FurtherInvestigationNeeded || IncompleteOrNonCompliant;

        public static Compliance Empty => new Compliance("", null, false, false, false, false);
        public Compliance(string actionsOrDocuments, DateTime? dueDate, bool dueDateNotRespected = false, bool dueDateRespected = false, bool furtherInvestigationNeeded = false, bool incompleteOrNonCompliant = false)
        {
            ActionsOrDocuments = actionsOrDocuments;
            DueDate = dueDate;
            DueDateNotRespected = dueDateNotRespected;
            DueDateRespected = dueDateRespected;
            FurtherInvestigationNeeded = furtherInvestigationNeeded;
            IncompleteOrNonCompliant = incompleteOrNonCompliant;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ActionsOrDocuments;
            yield return DueDate;
            yield return DueDateNotRespected;
            yield return DueDateRespected;
            yield return FurtherInvestigationNeeded;
            yield return IncompleteOrNonCompliant;
        }
    }
}