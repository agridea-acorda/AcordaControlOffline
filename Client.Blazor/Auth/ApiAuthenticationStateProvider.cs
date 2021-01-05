using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.Auth
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private IApiClient apiClient_;
        private IJSRuntime jsRuntime_;

        public ApiAuthenticationStateProvider(IApiClient apiClient, IJSRuntime jsRuntime)
        {
            apiClient_ = apiClient;
            jsRuntime_ = jsRuntime;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authCookie = await jsRuntime_.InvokeAsync<string>(JsInterop.ReadCookie, AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth.CookieName);
            if (string.IsNullOrWhiteSpace(authCookie))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            var authData = JsonConvert.DeserializeObject<AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth>(authCookie);
            apiClient_.SetAuthToken(authData.Token);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authData.Username),
                new Claim(ClaimTypes.Role, authData.Role),
                new Claim(ClaimTypes.StateOrProvince, authData.CantonCode),
            };
            var claimsIdentity = new ClaimsIdentity(claims, "basicAuth");
            return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
        }

        public void MarkUserAsAuthenticated(string username)
        {
            var authenticated = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "basicAuth"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticated)));
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
