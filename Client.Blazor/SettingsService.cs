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

        public async Task Save(Settings settings)
        {
            await jsRuntime_.InvokeAsync<string>(JsInterop.SetCookie, Settings.CookieName, JsonConvert.SerializeObject(settings));
            httpClient_.BaseAddress = new Uri(settings.ApiBaseAddres);
        }

        public async Task<Settings> Read()
        {
            var savedSettings = await jsRuntime_.InvokeAsync<Settings>(JsInterop.ReadCookie, Settings.CookieName);
            return savedSettings ?? Settings.Default;
        }
    }

    public interface ISettingsService
    {
        Task Save(Settings settings);
        Task<Settings> Read();
    }
}
