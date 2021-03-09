using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Client.Blazor.Auth;
using Agridea.Acorda.AcordaControlOffline.Client.Blazor.Config;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore;
using Agridea.DomainDrivenDesign;
using Blazored.LocalStorage;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var executingAssembly = Assembly.GetExecutingAssembly();
            string configFileName = typeof(Program).Namespace + ".Config.appsettings.json";

            var configRoot = new ConfigurationBuilder()
                             .AddJsonStream(executingAssembly.GetManifestResourceStream(configFileName))
                             .Build();
            var config = configRoot.GetSection(nameof(AppConfiguration)).Get<AppConfiguration>();

            // api
            // todo find some way to configure the api BaseAddress at runtime by the user.
            builder.Services.AddSingleton(sp =>
            {
                Console.WriteLine($"Config: {{ {nameof(AppConfiguration.ApiEndpoint)}: {config?.ApiEndpoint}, " +
                                  $"{nameof(AppConfiguration.BaseUrl)}: {config?.BaseUrl}, " +
                                  $"{nameof(AppConfiguration.IsDev)}: {config?.IsDev} " +
                                  "}}");
                return new HttpClient { BaseAddress = new Uri(config.ApiEndpoint) };
            });
            builder.Services.AddScoped<IApiClient, ApiClient>();

            // config
            builder.Services.AddSingleton<AppConfiguration>(sp => config);

            // local storage and repository using it
            builder.Services.AddBlazoredLocalStorage(conf => conf.JsonSerializerOptions.WriteIndented = true);
            builder.Services.AddScoped<IRepository, LocalStorageRepository>();

            // (user-)settings
            builder.Services.AddScoped<ISettingsService, SettingsService>();

            // auth
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Events
            builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            // Toasts
            builder.Services.AddBlazoredToast();

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
