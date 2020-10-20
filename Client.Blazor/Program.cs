using System;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddHttpClient<IApiClient, SampleDataApiClient>(nameof(SampleDataApiClient),
                                                                          client =>
                                                                          {
                                                                              client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                                                                              // for real api: client.BaseAddress = new Uri(apiSettings.BaseAddress);
                                                                              // for real api: client.DefaultRequestHeaders.Add("api-key", apiSettings.ApiKey);
                                                                          });
            await builder.Build().RunAsync();
        }
    }
}
