using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api {
    public interface IApiClient
    {
        Task<Result<ViewModel.MandateList.Mandate[]>> FetchMandateListAsync(string uri, int delayInMs = 0);
        Task<Result<Mandate>> FetchMandateDetailAsync(string uri);
        Task<Result<ViewModel.FarmDetail.Farm>> FetchFarmDetailAsync(string uri);
    }
}