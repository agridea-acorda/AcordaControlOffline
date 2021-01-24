using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor
{
    public class SettingsService : ISettingsService
    {
        private readonly IJSRuntime jsRuntime_;
        private readonly HttpClient httpClient_;
        public SettingsService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            jsRuntime_ = jsRuntime;
            httpClient_ = httpClient;
        }

        public async Task<string> Save(Settings settings)
        {
            var uri = new Uri(settings.ApiBaseAddres);
            bool urisAreEqual = Uri.Compare(httpClient_.BaseAddress, uri, UriComponents.HostAndPort | UriComponents.Path, UriFormat.UriEscaped, StringComparison.Ordinal) == 0;
            if (httpClient_.BaseAddress != null && !urisAreEqual)
                return "L'adresse de l'api doit être configurée avant d'effectuer une requête.";

            if (!urisAreEqual) 
                httpClient_.BaseAddress = uri;
            
            await jsRuntime_.InvokeVoidAsync(JsInterop.SetCookie, Settings.CookieName, JsonConvert.SerializeObject(settings));
            return "";

        }

        public async Task<Settings> Read()
        {
            var jsonSettings = await jsRuntime_.InvokeAsync<string>(JsInterop.ReadCookie, Settings.CookieName);
            if (string.IsNullOrWhiteSpace(jsonSettings)) return Settings.Default;
            return JsonConvert.DeserializeObject<Settings>(jsonSettings);
        }
    }

    public interface ISettingsService
    {
        Task<string> Save(Settings settings);
        Task<Settings> Read();
    }
}
