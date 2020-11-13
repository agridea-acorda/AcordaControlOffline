using System;
using System.Net.Http;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore;
using Blazored.LocalStorage;
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
            
            // Below is just a temporary hack for the /checklist page, still in demo state.
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            // This is the real api
            builder.Services.AddHttpClient<IApiClient, SampleDataApiClient>(nameof(SampleDataApiClient),
                                                                          client =>
                                                                          {
                                                                              client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                                                                              // for real api: client.BaseAddress = new Uri(apiSettings.BaseAddress);
                                                                              // for real api: client.DefaultRequestHeaders.Add("api-key", apiSettings.ApiKey);
                                                                          });
            
            // local storage and repository using it
            builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
            builder.Services.AddScoped<IRepository, LocalStorageRepository>();
            
            // Attempts at CQRS commands and queries handlers but cannot get it to work
            //builder.Services.AddHandlers();
            //builder.Services.AddTransient<IQueryHandler<MandateListQuery, ValueTask<Mandate[]>>, MandateListQuery.MandateListQueryHandler>(x => new MandateListQuery.MandateListQueryHandler(x.GetService<IRepository>()));
            //builder.Services.AddSingleton<Messages>();
            
            await builder.Build().RunAsync();
        }
    }
}
