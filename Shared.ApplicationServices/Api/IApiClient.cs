using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel;
using CSharpFunctionalExtensions;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api {
    public interface IApiClient
    {
        Task<Result<Mandate[]>> FetchMandatesAsync(string uri);
        Task<Result<FarmSummary>> FetchFarmSummaryAsync(string uri);
    }
}