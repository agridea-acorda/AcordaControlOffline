using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Combo;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "L'identifiant est obligatoire")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        public string Password { get; set; } 
        [Required(ErrorMessage = "Le canton est obligatoire")]
        public string CantonCode { get; set; }
        public IEnumerable<SelectListItem<string>> ComboCantons { get; set; } = Combo.Combo.Cantons();
        public static LoginModel Empty() => new LoginModel();
    }
}
