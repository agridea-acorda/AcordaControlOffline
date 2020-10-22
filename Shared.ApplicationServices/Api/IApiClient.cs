using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api {
    public interface IApiClient
    {
        Task<Result<ViewModel.MandateList.Mandate[]>> FetchMandatesAsync(string uri);
        Task<Result<Mandate>> FetchFarmSummaryAsync(string uri);
    }
}