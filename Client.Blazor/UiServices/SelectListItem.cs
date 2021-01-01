using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
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

    public static class SelectListItemExtensions
    {
        public static SelectListItem<int> AsSelectListItem(this DefectSeriousness seriousness)
        {
            return new SelectListItem<int>(seriousness.Code, seriousness.Name);
        }

        public static SelectListItem<string> AsSelectListItem(this Canton canton)
        {
            return new SelectListItem<string>(canton.Code, canton.Code);
        }
    }

    public class Combo
    {
        public static IEnumerable<SelectListItem<int>> Seriousnesses()
        {
            var combo = new List<SelectListItem<int>>
            {
                DefectSeriousness.Empty.AsSelectListItem(), 
                DefectSeriousness.Small.AsSelectListItem(), 
                DefectSeriousness.Medium.AsSelectListItem(), 
                DefectSeriousness.Serious.AsSelectListItem()
            };
            return combo;
        }

        public static IEnumerable<SelectListItem<string>> Cantons()
        {
            return new List<SelectListItem<string>>
            {
                Canton.GE.AsSelectListItem(),
                Canton.NE.AsSelectListItem(),
                Canton.JU.AsSelectListItem(),
                Canton.VD.AsSelectListItem()
            };
        }
    }
}
