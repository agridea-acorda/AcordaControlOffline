using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class InspectionOutcome : ValueObject
    {
        public InspectionOutcome(int value, string code, string text)
        {
            Code = code;
            Value = value;
            Text = text;
        }
        public static InspectionOutcome Ok => new InspectionOutcome(0, "Ok", Resources.InspectionOutcomeOk);
        public static InspectionOutcome PartiallyOk => new InspectionOutcome(1, "PartiallyOk", Resources.InspectionOutcomePartiallyOk);
        public static InspectionOutcome NotOk => new InspectionOutcome(2, "NOk", Resources.InspectionOutcomeNotOk);
        public static InspectionOutcome NotInspected => new InspectionOutcome(3, "NotInspected", Resources.InspectionOutcomeNotInspected);
        public static InspectionOutcome NotApplicable => new InspectionOutcome(4, "NotApplicable", Resources.InspectionOutcomeNotApplicable);

        public string Text { get; }
        public int Value { get; }
        public string Code { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
