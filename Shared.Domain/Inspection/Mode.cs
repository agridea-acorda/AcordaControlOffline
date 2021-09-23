using Agridea.DomainDrivenDesign;
using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class Mode : ValueObject
    {
        public Mode(string text, int value)
        {
            Text = text;
            Value = value;
        }
        public string Text { get; }
        public int Value { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
            yield return Value;
        }

        public static Mode None => new Mode("", 0);
    }
}
