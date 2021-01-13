using Agridea.Acorda.AcordaControlOffline.Shared.Domain;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Combo
{
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
}