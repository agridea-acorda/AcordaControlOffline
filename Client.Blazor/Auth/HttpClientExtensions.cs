using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.Auth
{
    public static class HttpClientExtensions
    {
        public static void SetBasicAuthToken(this HttpClient httpClient, string token)
        {
            Console.WriteLine($"Setting basic auth header: Basic {token}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
        }

        public static void RemoveAuthToken(this HttpClient httpClient)
        {
            Console.WriteLine("Removing auth token");
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
