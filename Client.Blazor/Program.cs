using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Client.Blazor.Auth;
using Agridea.Acorda.AcordaControlOffline.Client.Blazor.Config;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore;
using Agridea.DomainDrivenDesign;
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

            // config
            var config = builder.Services.AddAppConfiguration(
                builder.HostEnvironment.BaseAddress, // this is not the complete url, env overriding vial querystring param won't work
                () => new EnvironmentChooser("Dev")
                    .Add("localhost:5000", "Dev", true)
                    .Add("acordacontrolapp.acorda.dev", "Netlify", true)
                    .Add("test.acordacontrolapp.acorda.dev", "Test", true)
                    .Add("testacordacontrolapp.acorda.ch", "Test", true)
                    .Add("prod.acordacontrolapp.acorda.dev", "Prod")
                    .Add("acordacontrolapp.acorda.ch", "Prod")
                    .Add("agridea-acorda.github.io", "GitHub"));

            // api
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(config.ApiEndpoint) });
            builder.Services.AddScoped<IApiClient, ApiClient>();

            // local storage and repository using it
            //builder.Services.AddBlazoredLocalStorage(conf => conf.JsonSerializerOptions.WriteIndented = false);
            builder.Services.AddScoped<IRepository, LocalForageRepository>();

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

            // Blazorise, Bootatrap, FontAwesome
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

    public static class Configuration
    {
        public static AppConfiguration AddAppConfiguration(this IServiceCollection serviceCollection, string url, Func<EnvironmentChooser> environmentChooserFactory)
        {
            var environementChooser = environmentChooserFactory();
            var uri = new Uri(url);
            string environment = environementChooser.GetCurrent(uri);
            var executingAssembly = Assembly.GetExecutingAssembly();
            string configFileName = !string.IsNullOrWhiteSpace(environment)
                                        ? typeof(Program).Namespace + ".Config.appsettings." + environment + ".json"
                                        : typeof(Program).Namespace + ".Config.appsettings.json";

            var configRoot = new ConfigurationBuilder()
                             .AddJsonStream(executingAssembly.GetManifestResourceStream(configFileName))
                             .Build();
            var config = configRoot.GetSection(nameof(AppConfiguration)).Get<AppConfiguration>();
            Console.WriteLine($"Config: {{ {nameof(AppConfiguration.ApiEndpoint)}: {config?.ApiEndpoint}, " +
                              $"{nameof(AppConfiguration.BaseUrl)}: {config?.BaseUrl}, " +
                              $"{nameof(AppConfiguration.IsDev)}: {config?.IsDev} " +
                              "}}");

            serviceCollection.AddSingleton(s => config);
            return config;
        }
    }
}
