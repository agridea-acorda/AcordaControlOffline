using System;
using System.Collections.Generic;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Combo;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Login
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CantonCode { get; set; }
        public IEnumerable<SelectListItem<string>> ComboCantons { get; set; } = Combo.Combo.Cantons();
        public static LoginModel Empty() => new LoginModel();
    }
}
