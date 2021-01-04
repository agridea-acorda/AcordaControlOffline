using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel
{
    public class Auth: ValueObject
    {
        public const string CookieName = "auth";
        public Auth(string username, string role, string cantonCode, string token)
        {
            Username = username;
            Role = role;
            CantonCode = cantonCode;
            Token = token;
        }
        public string Username { get; }
        public string CantonCode { get;}
        public string Token { get; }
        public string Role { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Username;
            yield return CantonCode;
            yield return Role;
            yield return Token;
        }
    }
}
