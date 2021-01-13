using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Combo
{
    public class SelectListItem<T> : ValueObject
    {
        public SelectListItem(T value, string text)
        {
            Text = text;
            Value = value;
        }
        public string Text { get; }
        public T Value { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
