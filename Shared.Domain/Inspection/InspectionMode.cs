using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
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
        public string Text { get; set; }
        public int Value { get; set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
            yield return Value;
        }
    }
}
