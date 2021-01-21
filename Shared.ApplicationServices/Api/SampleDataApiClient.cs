using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api
{
    public class SampleDataApiClient : IApiClient
    {
        private const int DefaultDelayInMs = 1000;
        private readonly HttpClient httpClient_;
        public SampleDataApiClient(HttpClient httpClient)
        {
            httpClient_ = httpClient;
        }

        public void SetAuthToken(string basicAuthToken)
        {
            // do nothing
        }

        public async Task<Result<ViewModel.MandateList.Mandate[]>> FetchAllMandatesAsync(string uri)
        {
            return await FetchTypedAsync<ViewModel.MandateList.Mandate[]>(uri);
        }

        public async Task<Result<Mandate>> FetchMandateDetailAsync(string uri)
        {
            return await FetchTypedAsync<Mandate>(uri);
        }

        public async Task<Result<string>> FetchRawJsonAsync(string uri)
        {
            return await FetchJsonAsync(uri);
        }

        public async Task<Result<ViewModel.Farm.Farm>> FetchFarmDetailAsync(string uri)
        {
            return await FetchTypedAsync<ViewModel.Farm.Farm>(uri);
        }

        public async Task<Result<ChecklistSample>> FetchChecklistSampleAsync(string uri)
        {
            return await FetchTypedAsync<ChecklistSample>(uri);
        }

        private async Task<Result<T>> FetchTypedAsync<T>(string uri, int delayInMs = DefaultDelayInMs)
        {
            if (delayInMs > 0)
            {
                await Task.Delay(delayInMs);
            }
            
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

        private async Task<Result<string>> FetchJsonAsync(string uri, int delayInMs = DefaultDelayInMs)
        {
            if (delayInMs > 0)
            {
                await Task.Delay(delayInMs);
            }

            using var httpResponse = await httpClient_.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.Content == null || httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                return Result.Failure<string>("HTTP Response has no content or content is not json.");

            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            try
            {
                return Result.Success(await streamReader.ReadToEndAsync());
            }
            catch (JsonReaderException)
            {
                return Result.Failure<string>($"Error while deserializing json: {nameof(JsonReaderException)} exception encountered.");
            }
        }

        public Task<Result<byte[]>> FetchPdf(HttpClient httpClient, string uri)
        {
            throw new System.NotImplementedException();
        }
    }
}
