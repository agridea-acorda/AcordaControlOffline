using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
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

        public async Task<Result<MergeResult>> SendMergePackage(string uri, MergePackage mergePackage)
        {
            var payload = new Dictionary<string, string>
            {
                {nameof(MergePackage.Mandate), mergePackage.Mandate},
                {nameof(MergePackage.Checklists), mergePackage.Checklists}
            };
            return await PostWithTypedResponseAsync<MergeResult>(uri, payload);
        }

        public async Task<Result<string>> CancelMergePackage(string uri, int id, string state)
        {
            var payload = new Dictionary<string, string>
            {
                {"Id", id.ToString()},
                {"State", state}
            };
            return await PostAsync(uri, payload);
        }

        public async Task<Result<string>> AcknowledgeMerge(string uri, int id)
        {
            var payload = new Dictionary<string, string>
            {
                {"MergePackageId", id.ToString()}
            };
            return await PostAsync(uri, payload);
        }

        private async Task<Result<T>> FetchTypedAsync<T>(string uri, int delayInMs = DefaultDelayInMs)
        {
            var httpResponse = await SendGetRequest(uri, delayInMs);
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
            var httpResponse = await SendGetRequest(uri, delayInMs);
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

        private async Task<HttpResponseMessage> SendGetRequest(string uri, int delayInMs)
        {
            if (delayInMs > 0)
            {
                await Task.Delay(delayInMs);
            }

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var httpResponse = await httpClient_.SendAsync(request);
            httpResponse.EnsureSuccessStatusCode();
            return httpResponse;
        }

        private async Task<HttpResponseMessage> SendPostRequest(string uri, Dictionary<string, string> payload, int delayInMs)
        {
            if (delayInMs > 0)
            {
                await Task.Delay(delayInMs);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, uri) {Content = new FormUrlEncodedContent(payload)};
            var httpResponse = await httpClient_.SendAsync(request);
            httpResponse.EnsureSuccessStatusCode();
            return httpResponse;
        }

        private async Task<Result<string>> PostAsync(string uri, Dictionary<string, string> payload, int delayInMs = DefaultDelayInMs)
        {
            var httpResponse = await SendPostRequest(uri, payload, delayInMs);
            if (!httpResponse.IsSuccessStatusCode)
                return Result.Failure<string>($"HTTP status code is no success: {httpResponse.StatusCode}");

            if (httpResponse.Content == null)
                return Result.Failure<string>("Null content returned from POST request. Expected: string.");

            var contentString = await httpResponse.Content.ReadAsStringAsync();
            return Result.Success(contentString);
        }

        private async Task<Result<T>> PostWithTypedResponseAsync<T>(string uri, Dictionary<string, string> payload, int delayInMs = DefaultDelayInMs)
        {
            var httpResponse = await SendPostRequest(uri, payload, delayInMs);
            if (!httpResponse.IsSuccessStatusCode)
                return Result.Failure<T>($"HTTP status code is no success: {httpResponse.StatusCode}");

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
    }
}
