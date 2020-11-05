using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain
{
    public class InspectionMode : ValueObject
    {
        public static InspectionMode Scheduled => new InspectionMode(Resources.InspectionModeScheduled, 1);
        public static InspectionMode Unscheduled => new InspectionMode(Resources.InspectionModeUnscheduled, 2);
        
        public InspectionMode(string text, int value)
        {
            Text = text;
            Value = value;
        }
        public string Text { get; }
        public int Value { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
