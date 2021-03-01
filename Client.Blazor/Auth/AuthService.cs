using System;
using System.Net.Http;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Login;
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
        private readonly ISettingsService settingsService_;

        public AuthService(AuthenticationStateProvider authenticationStateProvider,
                           IJSRuntime jsRuntime, 
                           HttpClient httpClient, 
                           ISettingsService settingsService)
        {
            authenticationStateProvider_ = authenticationStateProvider;
            jsRuntime_ = jsRuntime;
            httpClient_ = httpClient;
            settingsService_ = settingsService;
        }

        public async Task Login(LoginModel loginModel)
        {
            string basicAuthToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{loginModel.CantonCode}.{loginModel.Username}:{loginModel.Password}"));
            string role = "inspector"; // todo get role from api POST to /login
            var auth = new AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth(loginModel.Username, role, loginModel.CantonCode, basicAuthToken);
            var settings = await settingsService_.Read();
            await jsRuntime_.InvokeAsync<string>(JsInterop.SetCookie, AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth.CookieName, JsonConvert.SerializeObject(auth), settings.AuthCookieExpiryDays);
            httpClient_.SetBasicAuthToken(auth.Token);
            ((ApiAuthenticationStateProvider)authenticationStateProvider_).MarkUserAsAuthenticated(auth);
        }

        public async Task Logout()
        {
            await jsRuntime_.InvokeAsync<string>(JsInterop.RemoveCookie, AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth.CookieName);
            httpClient_.RemoveAuthToken();
            ((ApiAuthenticationStateProvider)authenticationStateProvider_).MarkUserAsLoggedOut();
        }

        public async Task<AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth> ReadAuthInfo()
        {
            var authCookie = await jsRuntime_.InvokeAsync<string>(JsInterop.ReadCookie, AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth.CookieName);
            if (string.IsNullOrWhiteSpace(authCookie))
            {
                // user is not logged-in
                return AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth.UnknownUser;
            }
            var authData = JsonConvert.DeserializeObject<AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth>(authCookie);
            return authData;
        }
    }

    public interface IAuthService
    {
        Task Login(LoginModel loginModel);
        Task Logout();
        Task<AcordaControlOffline.Shared.ApplicationServices.ViewModel.Auth> ReadAuthInfo();
    }
}
