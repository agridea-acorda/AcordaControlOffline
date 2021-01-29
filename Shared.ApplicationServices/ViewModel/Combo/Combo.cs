using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Combo
{
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
                Canton.None.AsSelectListItem(),
                Canton.GE.AsSelectListItem(),
                Canton.NE.AsSelectListItem(),
                Canton.JU.AsSelectListItem(),
                Canton.VD.AsSelectListItem()
            };
        }
    }
}