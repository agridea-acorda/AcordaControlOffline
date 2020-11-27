using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Signature;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Blazored.LocalStorage;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore
{
    public class LocalStorageRepository : IRepository
    {
        public const string Mandates = "mandates";
        public const string MandateDetail = "mandateDetail";
        public const string Checklist = "checklist";
        public const string ActionsOrDocuments = "actionsOrDocuments";

        private readonly ILocalStorageService localStorage_;
        public LocalStorageRepository(ILocalStorageService localStorage)
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

        public async ValueTask<bool> HasMandateAsync(int farmId)
        {
            string key = MandateDetailKey(farmId);
            return await localStorage_.ContainKeyAsync(key);
        }

        public async ValueTask<ViewModel.MandateDetail.Mandate> ReadMandateAsync(int farmId)
        {
            string key = MandateDetailKey(farmId);
            return await localStorage_.GetItemAsync<ViewModel.MandateDetail.Mandate>(key);
        }

        public async ValueTask SaveMandateAsync(ViewModel.MandateDetail.Mandate mandate, int id = 0)
        {
            id = id == default ? mandate.Farm.Id : id;
            string key = MandateDetailKey(id);
            await localStorage_.SetItemAsync(key, mandate);
        }

        public async ValueTask<ChecklistSample> ReadChecklistSampleAsync()
        {
            return await localStorage_.GetItemAsync<ChecklistSample>(Checklist);
        }

        public async ValueTask SaveChecklistSampleAsync(ChecklistSample checklist)
        {
            await localStorage_.SetItemAsync(Checklist, checklist);
        }

        public async ValueTask SaveChecklistAsync(Checklist checklist, Func<Checklist, string> serialize)
        {
            string json = serialize(checklist);
            await localStorage_.SetItemAsync(ChecklistKey(checklist), json);
        }

        public async ValueTask<Checklist> ReadChecklistAsync(int farmInspectionId, Func<string, Checklist> deserialize)
        {
            string json = await localStorage_.GetItemAsStringAsync(ChecklistKey(farmInspectionId));
            return deserialize(json);
        }

        public async ValueTask<ActionsOrDocumentEditModel> ReadActionsOrDocumentsAsync()
        {
            return await localStorage_.GetItemAsync<ActionsOrDocumentEditModel>(ActionsOrDocuments);
        }

        public async ValueTask SaveActionsOrDocumentsAsync(ActionsOrDocumentEditModel model)
        {
            await localStorage_.SetItemAsync(ActionsOrDocuments, model);
        }

        private static string MandateDetailKey(int farmId)
        {
            return $"{MandateDetail}_{farmId}";
        }

        private static string ChecklistKey(Checklist checklist)
        {
            // int farmInspectionId = checklist.FarmInspectionId; // todo implement this
            int farmInspectionId = 0;
            return ChecklistKey(farmInspectionId);
        }

        private static string ChecklistKey(int farmInspectionId)
        {
            return $"{Checklist}_{farmInspectionId}";
        }
    }
}
