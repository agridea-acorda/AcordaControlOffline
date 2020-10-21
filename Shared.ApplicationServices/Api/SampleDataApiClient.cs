using System;
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
            using var httpResponse = await httpClient_.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.Content == null || httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                return Result.Failure<Mandate[]>("HTTP Response has no content or content is not json.");

            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();
            try
            {
                var data = serializer.Deserialize<Mandate[]>(jsonReader);
                return Result.Success(data);
            }
            catch (JsonReaderException)
            {
                return Result.Failure<Mandate[]>($"Error while deserializing json: {nameof(JsonReaderException)} exception encountered.");
            }
        }

        public Task<Result<FarmSummary>> FetchFarmSummaryAsync(string uri)
        {
            throw new NotImplementedException();
        }
    }
}
