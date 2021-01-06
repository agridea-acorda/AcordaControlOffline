using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.Auth
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRuntime_;
        private readonly HttpClient httpClient_;

        public ApiAuthenticationStateProvider(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            jsRuntime_ = jsRuntime;
            httpClient_ = httpClient;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authCookie = await jsRuntime_.InvokeAsync<string>(JsInterop.ReadCookie, AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth.CookieName);
            if (string.IsNullOrWhiteSpace(authCookie))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            var authData = JsonConvert.DeserializeObject<AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth>(authCookie);
            httpClient_.SetBasicAuthToken(authData.Token);
            var claimsIdentity = GetClaimsFromAuthModel(authData);
            return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
        }

        public void MarkUserAsAuthenticated(AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth authModel)
        {
            var authenticated = new ClaimsPrincipal(GetClaimsFromAuthModel(authModel));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticated)));
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }

        private ClaimsIdentity GetClaimsFromAuthModel(AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth authModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authModel.Username),
                new Claim(ClaimTypes.Role, authModel.Role),
                new Claim(ClaimTypes.StateOrProvince, authModel.CantonCode),
            };
            var claimsIdentity = new ClaimsIdentity(claims, "basicAuth");
            return claimsIdentity;
        }
    }
}
