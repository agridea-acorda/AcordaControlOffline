using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using CSharpFunctionalExtensions;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api {
    public interface IApiClient
    {
        Task<Result<ViewModel.MandateList.Mandate[]>> FetchMandatesAsync(string uri);
        Task<Result<Mandate>> FetchFarmSummaryAsync(string uri);
    }
}