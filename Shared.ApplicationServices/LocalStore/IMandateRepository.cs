using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore {
    public interface IMandateRepository
    {
        ValueTask SaveMandatesAsync(Mandate[] mandates);
        ValueTask<Mandate[]> ReadAllMandatesAsync();
        ValueTask<bool> HasMandateAsync(string key);
        ValueTask<ViewModel.MandateDetail.Mandate> ReadMandateAsync(int farmId);
    }
}