using System;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Client.Blazor.Auth;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore;
using Agridea.DomainDrivenDesign;
using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
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
            
            // Acordacontrol api
            builder.Services.AddHttpClient<IApiClient, ApiClient>(nameof(ApiClient),
                                                                          client =>
                                                                          {
                                                                              // sample data in local json file
                                                                              //client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);

                                                                              // real api
                                                                              client.BaseAddress = new Uri(Settings.ApiBaseAddres);
                                                                              //client.DefaultRequestHeaders.Add("api-key", apiSettings.ApiKey);
                                                                          });
            
            // local storage and repository using it
            builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
            builder.Services.AddScoped<IRepository, LocalStorageRepository>();

            // auth
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            // Events
            builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services
                   .AddBlazorise(options =>
                   {
                       options.ChangeTextOnKeyPress = true;
                       options.DelayTextOnKeyPressInterval = 300;
                   })
                   .AddBootstrapProviders()
                   .AddFontAwesomeIcons();

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
