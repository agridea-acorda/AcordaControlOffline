using System.Net.Http;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using CSharpFunctionalExtensions;
using Org.BouncyCastle.Crypto.Generators;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api {
    public interface IApiClient
    {
        //void SetAuthToken(string basicAuthToken);
        Task<Result<ViewModel.MandateList.Mandate[]>> FetchAllMandatesAsync(string uri);
        Task<Result<Mandate>> FetchMandateDetailAsync(string uri);
        Task<Result<string>> FetchRawJsonAsync(string uri);
        Task<Result<ViewModel.Farm.Farm>> FetchFarmDetailAsync(string uri);
        Task<Result<ChecklistSample>> FetchChecklistSampleAsync(string uri);
        Task<Result<byte[]>> FetchPdf(HttpClient client, string uri);
    }
}