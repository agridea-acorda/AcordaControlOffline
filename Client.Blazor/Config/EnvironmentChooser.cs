using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.Config
{
    /// <summary>  
    /// This class is used for picking the environment given a Uri  
    /// </summary>  
    public class EnvironmentChooser
    {
        private const string EnvQueryStringKey = "env";
        private const string ApiEndpointQueryStringKey = "api";
        private readonly Dictionary<string, Host2EnvironmentMapping> hostMappings = new Dictionary<string, Host2EnvironmentMapping>();

        /// <summary>  
        /// Build a chooser  
        /// </summary>  
        /// <param name="defaultEnvironment">If no environment is found on the domain name or query then this will be returned</param>  
        public EnvironmentChooser(string defaultEnvironment)
        {
            if (string.IsNullOrWhiteSpace(defaultEnvironment))
                throw new ArgumentNullException(nameof(defaultEnvironment), $"{nameof(defaultEnvironment)} parameter is mandatory.");

            DefaultEnvironment = defaultEnvironment;
        }
        public string DefaultEnvironment { get; }

        /// <summary>  
        /// Add a new binding between a hostname and an environment  
        /// </summary>  
        /// <param name="hostName">The hostname that must fully match the uri</param>  
        /// <param name="env">The environement that'll be returned</param>  
        /// <param name="canOverrideEnvironmentFromUrl">If false, we can't override the environement with a "Environment" in the GET parameters</param>  
        public EnvironmentChooser Add(string hostName, string env, bool canOverrideEnvironmentFromUrl = false)
        {
            hostMappings.Add(hostName, new Host2EnvironmentMapping(env, canOverrideEnvironmentFromUrl));
            return this;
        }
        
        /// <summary>  
        /// Get the current environment from the url
        /// </summary>  
        /// <param name="url"></param>  
        public string GetCurrent(Uri url)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(url.Query);
            bool urlContainsEnvironment = parsedQueryString.AllKeys.Contains(EnvQueryStringKey);
            string environmentOverride = parsedQueryString.GetValues(EnvQueryStringKey)?.FirstOrDefault() ?? "";
            if (hostMappings.ContainsKey(url.Authority))
            {
                var hostMapping = hostMappings[url.Authority];
                if (hostMapping.CanOverride && urlContainsEnvironment)
                    return environmentOverride;
                
                return hostMapping.Env;
            }
            return urlContainsEnvironment ? environmentOverride : DefaultEnvironment;
        }

        /// <summary>
        /// Get the api endpoint override from the url if present
        /// </summary>
        /// <param name="url"></param>
        public string GetApiEndpointOverride(Uri url)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(url.Query);
            string apiEndpointOverride = parsedQueryString.GetValues(ApiEndpointQueryStringKey)?.FirstOrDefault() ?? "";
            return apiEndpointOverride;
        }

        public class Host2EnvironmentMapping: ValueObject
        {
            public Host2EnvironmentMapping(string env, bool canOverride)
            {
                Env = env;
                CanOverride = canOverride;
            }
            public string Env { get; }
            public bool CanOverride { get; }
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Env;
            }
        }
    }
}
