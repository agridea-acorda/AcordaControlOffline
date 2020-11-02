using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Blazored.LocalStorage;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore
{
    public class LocalStorageMandateRepository : IMandateRepository
    {
        public const string Mandates = "mandates";
        public const string MandateDetail = "mandateDetail";

        private readonly ILocalStorageService localStorage_;
        public LocalStorageMandateRepository(ILocalStorageService localStorage)
        {
            localStorage_ = localStorage;
        }

        public async ValueTask SaveMandatesAsync(Mandate[] mandates)
        {
            await localStorage_.SetItemAsync(Mandates, mandates);
        }

        public async ValueTask<Mandate[]> ReadAllMandatesAsync()
        {
            return await localStorage_.GetItemAsync<Mandate[]>(Mandates);
        }

        public async ValueTask<bool> HasMandateAsync(string key)
        {
            return await localStorage_.ContainKeyAsync(key);
        }

        public async ValueTask<ViewModel.MandateDetail.Mandate> ReadMandateAsync(int farmId)
        {
            string key = MandateDetailKey(farmId);
            return await localStorage_.GetItemAsync<ViewModel.MandateDetail.Mandate>(key);
        }

        private static string MandateDetailKey(int farmId)
        {
            return $"{MandateDetail}_{farmId}";
        }
    }
}
