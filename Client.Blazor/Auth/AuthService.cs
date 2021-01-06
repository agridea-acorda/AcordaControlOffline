using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Client.Blazor.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider_;
        private readonly IJSRuntime jsRuntime_;
        private readonly HttpClient httpClient_;

        public AuthService(AuthenticationStateProvider authenticationStateProvider,
                           IJSRuntime jsRuntime, 
                           HttpClient httpClient)
        {
            authenticationStateProvider_ = authenticationStateProvider;
            jsRuntime_ = jsRuntime;
            httpClient_ = httpClient;
        }

        public async Task Login(LoginComponent.LoginModel loginModel)
        {
            string basicAuthToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{loginModel.CantonCode}.{loginModel.Username}:{loginModel.Password}"));
            string role = "inspector"; // todo get role from api POST to /login
            var auth = new AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth(loginModel.Username, role, loginModel.CantonCode, basicAuthToken);
            await jsRuntime_.InvokeAsync<string>(JsInterop.SetCookie, AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth.CookieName, JsonConvert.SerializeObject(auth), Settings.AuthCookieExpiryDays);
            httpClient_.SetBasicAuthToken(auth.Token);
            ((ApiAuthenticationStateProvider)authenticationStateProvider_).MarkUserAsAuthenticated(auth);
        }

        public async Task Logout()
        {
            await jsRuntime_.InvokeAsync<string>(JsInterop.RemoveCookie, AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth.CookieName);
            httpClient_.RemoveAuthToken();
            ((ApiAuthenticationStateProvider)authenticationStateProvider_).MarkUserAsLoggedOut();
        }
    }

    public interface IAuthService
    {
        Task Login(LoginComponent.LoginModel loginModel);
        Task Logout();
    }
}
