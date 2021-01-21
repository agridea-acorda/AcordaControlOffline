using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.recencementAutomation;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api
{
    public class ApiClient : IApiClient
    {
        private const int DefaultDelayInMs = 0;
        private readonly HttpClient httpClient_;
        public ApiClient(HttpClient httpClient)
        {
            Console.WriteLine("Constructing new instance of ApiClient.");
            httpClient_ = httpClient;
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
            var httpResponse = await SendRequest(uri, delayInMs);
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
            catch
            {
                return Result.Failure<T>($"Unknown error occured while fetching typed data at {uri}.");
            }
        }

        private async Task<Result<string>> FetchJsonAsync(string uri, int delayInMs = DefaultDelayInMs)
        {
            var httpResponse = await SendRequest(uri, delayInMs);
            if (httpResponse.Content == null || httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                return Result.Failure<string>("HTTP Response has no content or content is not json.");

            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            try
            {
                return Result.Success(await streamReader.ReadToEndAsync());
            }
            catch
            {
                return Result.Failure<string>($"Unknown error occured while fetching raw json at {uri}.");
            }
        }

        private async Task<HttpResponseMessage> SendRequest(string uri, int delayInMs)
        {
            if (delayInMs > 0)
            {
                await Task.Delay(delayInMs);
            }

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            //string authHeaderValue = "Basic " + basicAuthToken_;
            //request.Headers.Add("Authorization", "Basic " + basicAuthToken_);
            //Console.WriteLine($"ApiClient sending request with basic auth token={{{authHeaderValue}}}");
            var httpResponse = await httpClient_.SendAsync(request);
            httpResponse.EnsureSuccessStatusCode();
            return httpResponse;
        }

        public async Task<Result<byte[]>> FetchPdf(HttpClient httpClient, string uri)
        {
            var client = new AcordaRecensementWebUIAutomation(httpClient, "JU","","",7);
            byte[] data = await client.DownloadFormAAsync();
            //var httpResponse = await SendRequest(uri, DefaultDelayInMs);
            return data;
        }
    }
}
