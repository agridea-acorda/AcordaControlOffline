using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api
{
    public class SampleDataApiClient : IApiClient
    {
        private readonly HttpClient httpClient_;
        public SampleDataApiClient(HttpClient httpClient)
        {
            httpClient_ = httpClient;
        }

        public async Task<Result<Mandate[]>> FetchMandatesAsync(string uri)
        {
            return await FetchJsonDataAsync<Mandate[]>(uri);
        }

        public async Task<Result<FarmSummary>> FetchFarmSummaryAsync(string uri)
        {
            return await FetchJsonDataAsync<FarmSummary>(uri);
        }

        private async Task<Result<T>> FetchJsonDataAsync<T>(string uri)
        {
            using var httpResponse = await httpClient_.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.Content == null || httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                return Result.Failure<T>("HTTP Response has no content or content is not json.");

            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();
            try
            {
                var data = serializer.Deserialize<T>(jsonReader);
                return Result.Success(data);
            }
            catch (JsonReaderException)
            {
                return Result.Failure<T>($"Error while deserializing json: {nameof(JsonReaderException)} exception encountered.");
            }
        }
    }
}
