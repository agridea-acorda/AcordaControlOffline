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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
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
            //var executingAssembly = Assembly.GetExecutingAssembly();
            //string configFileName = typeof(Program).Namespace + ".Config.appsettings.json";

            //var configRoot = new ConfigurationBuilder()
            //                 .AddJsonStream(executingAssembly.GetManifestResourceStream(configFileName))
            //                 .Build();
            //var config = configRoot.GetSection(nameof(AppConfiguration)).Get<AppConfiguration>();
            //Console.WriteLine($"Config: {{ {nameof(AppConfiguration.ApiEndpoint)}: {config?.ApiEndpoint}, " +
            //                  $"{nameof(AppConfiguration.BaseUrl)}: {config?.BaseUrl}, " +
            //                  $"{nameof(AppConfiguration.IsDev)}: {config?.IsDev} " +
            //                  "}}");

            //builder.Services.AddSingleton(sp => config);

            var config = builder.Services.AddEnvironmentConfiguration<AppConfiguration>(() => 
                new EnvironmentChooser("Dev")
                    .Add("localhost:5000", "Dev", true)
                    .Add("acordacontrolapp.acorda.dev", "Netlify", true)
                    .Add("agridea-acorda.github.io", "GitHub"));

            // api
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(config.ApiEndpoint) });
            builder.Services.AddScoped<IApiClient, ApiClient>();

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

    public static class Configuration
    {
        public static AppConfiguration AddEnvironmentConfiguration<TResource>(
            this IServiceCollection serviceCollection,
            Func<EnvironmentChooser> environmentChooserFactory)
        {
            AppConfiguration config = AppConfiguration.Empty;
            serviceCollection.AddSingleton(s =>
            {
                var environementChooser = environmentChooserFactory();
                var uri = new Uri(s.GetRequiredService<NavigationManager>().Uri);
                string environment = environementChooser.GetCurrent(uri);
                var executingAssembly = Assembly.GetExecutingAssembly();
                string configFileName = !string.IsNullOrWhiteSpace(environment)
                                            ? typeof(Program).Namespace + ".Config.appsettings." + environment + ".json"
                                            : typeof(Program).Namespace + ".Config.appsettings.json";

                var configRoot = new ConfigurationBuilder()
                                 .AddJsonStream(executingAssembly.GetManifestResourceStream(configFileName))
                                 .Build();
                config = configRoot.GetSection(nameof(AppConfiguration)).Get<AppConfiguration>();
                Console.WriteLine($"Config: {{ {nameof(AppConfiguration.ApiEndpoint)}: {config?.ApiEndpoint}, " +
                                  $"{nameof(AppConfiguration.BaseUrl)}: {config?.BaseUrl}, " +
                                  $"{nameof(AppConfiguration.IsDev)}: {config?.IsDev} " +
                                  "}}");

                return config;

                //var ressourceNames = new[]
                //{
                //    assembly.GetName().Name + ".Configuration.appsettings.json",
                //    assembly.GetName().Name + ".Configuration.appsettings." + environment + ".json"
                //};
                //ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                //configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>()
                //{
                //    { "Environment", environment }
                //});
                //Console.WriteLine(string.Join(",", assembly.GetManifestResourceNames()));
                //Console.WriteLine(string.Join(",", ressourceNames));
                //foreach (var resource in ressourceNames)
                //{

                //    if (assembly.GetManifestResourceNames().Contains(resource))
                //    {
                //        configurationBuilder.AddJsonFile(
                //            new InMemoryFileProvider(assembly.GetManifestResourceStream(resource)), resource, false, false);
                //    }
                //}
                //return configurationBuilder.Build();
            });
            return config;
        }
    }
}
