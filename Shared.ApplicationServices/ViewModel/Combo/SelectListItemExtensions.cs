﻿using Agridea.Acorda.AcordaControlOffline.Shared.Domain;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

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
            return canton == Canton.None
                       ? new SelectListItem<string>(canton.Code, "")
                       : new SelectListItem<string>(canton.Code, canton.Code);
        }

        public static SelectListItem<string> AsSelectListItem(this Organization org)
        {
            return org == Organization.None
                       ? new SelectListItem<string>(org.Name, "")
                       : new SelectListItem<string>(org.Name, org.Name);
        }
    }
}