using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel
{
    public class Auth: ValueObject
    {
        public const string CookieName = "auth";
        public const string Unauthenticated = "?";
        public Auth(string username, string role, string cantonCode, string token, string organizationName)
        {
            Username = username;
            Role = role;
            CantonCode = cantonCode;
            Token = token;
            OrganizationName = organizationName;
        }
        public static Auth UnknownUser => new Auth(Unauthenticated, Unauthenticated, Canton.None.Code, "", Organization.None.Name);
        public string Username { get; }
        public string CantonCode { get;}
        public string OrganizationName { get; }
        public string Token { get; }
        public string Role { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Username;
            yield return CantonCode;
            yield return OrganizationName;
            yield return Role;
            yield return Token;
        }
    }
}
