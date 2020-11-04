using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore {
    public interface IRepository
    {
        ValueTask SaveMandatesAsync(Mandate[] mandates);
        ValueTask<Mandate[]> ReadAllMandatesAsync();
        ValueTask<bool> HasMandateAsync(int farmId);
        ValueTask<ViewModel.MandateDetail.Mandate> ReadMandateAsync(int farmId);
        ValueTask SaveMandateAsync(ViewModel.MandateDetail.Mandate mandate, int id = 0);
    }
}