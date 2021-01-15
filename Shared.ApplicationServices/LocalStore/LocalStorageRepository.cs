using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Farm;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Mandate;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Signature;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Farm;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Blazored.LocalStorage;
using Inspection = Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore
{
    public class LocalStorageRepository : IRepository
    {
        public const string Mandates = "mandates";
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

        public async ValueTask ClearMandatesListAsync()
        {
            await localStorage_.RemoveItemAsync(Mandates);
        }

        public async ValueTask<bool> HasMandateAsync(int farmId)
        {
            string key = MandateDetailKey(farmId);
            return await localStorage_.ContainKeyAsync(key);
        }

        public async ValueTask<Domain.Mandate.Mandate> ReadMandateAsync(int farmId)
        {
            string json = await localStorage_.GetItemAsStringAsync(MandateDetailKey(farmId));
            return new MandateFactory().Parse(json);
        }

        public async ValueTask<Signature> ReadInspectorSignatureAsync(int farmId, int farmInspectionId)
        {
            return await ReadSignatureAsync(farmId, farmInspectionId, x => x?.InspectorSignature);
        }

        public async ValueTask SaveInspectorSignatureAsync(int farmId, int farmInspectionId, Signature signature)
        {
            var mandate = await ReadMandateAsync(farmId);
            var inspection = mandate.Inspections.FirstOrDefault(x => x.FarmInspectionId == farmInspectionId);
            if (inspection != null)
                inspection.InspectorSigns(signature);
            
            await localStorage_.SetItemAsync(MandateDetailKey(farmId), new MandateFactory().Serialize(mandate));
        }

        public async ValueTask<Signature> ReadFarmerSignatureAsync(int farmId, int farmInspectionId)
        {
            return await ReadSignatureAsync(farmId, farmInspectionId, x => x?.FarmerSignature);
        }

        public async ValueTask SaveFarmerSignatureAsync(int farmId, int farmInspectionId, Signature signature)
        {
            await SaveSignatureAsync(farmId, farmInspectionId, signature, (i, s) => i.FarmerSigns(s));
        }

        public async ValueTask<Signature> ReadInspector2SignatureAsync(int farmId, int farmInspectionId)
        {
            return await ReadSignatureAsync(farmId, farmInspectionId, x => x?.Inspector2Signature);
        }

        public async ValueTask SaveInspector2SignatureAsync(int farmId, int farmInspectionId, Signature signature)
        {
            await SaveSignatureAsync(farmId, farmInspectionId, signature, (i, s) => i.Inspector2Signs(s));
        }

        public async ValueTask<Farm> ReadFarmAsync(int farmId)
        {
            string json = await localStorage_.GetItemAsStringAsync(FarmKey(farmId));
            return new FarmFactory().Parse(json);
        }

        public async ValueTask SaveMandateAsync(Domain.Mandate.Mandate mandate, int id)
        {
            string key = MandateDetailKey(id); 
            await localStorage_.SetItemAsync(key, new MandateFactory().Serialize(mandate));
        }

        public async ValueTask SaveMandateJsonAsync(string json, int id)
        {
            string key = MandateDetailKey(id);
            await localStorage_.SetItemAsync(key, json);
        }

        public async ValueTask SaveFarmJsonAsync(string json, int id)
        {
            string key = FarmKey(id);
            await localStorage_.SetItemAsync(key, json);
        }

        public async ValueTask SaveChecklistJsonAsync(string json, int id)
        {
            string key = ChecklistKey(id);
            await localStorage_.SetItemAsync(key, json);
        }

        public async ValueTask<ChecklistSample> ReadChecklistSampleAsync()
        {
            return await localStorage_.GetItemAsync<ChecklistSample>(Checklist);
        }

        public async ValueTask SaveChecklistSampleAsync(ChecklistSample checklist)
        {
            await localStorage_.SetItemAsync(Checklist, checklist);
        }

        public async ValueTask SaveChecklistAsync(Checklist checklist)
        {
            string json = new ChecklistFactory().Serialize(checklist);
            await localStorage_.SetItemAsync(ChecklistKey(checklist), json);
        }

        public async Task<Checklist> ReadChecklistAsync(int farmInspectionId)
        {
            string json = await localStorage_.GetItemAsStringAsync(ChecklistKey(farmInspectionId));
            return new ChecklistFactory().Parse(json);
        }

        public async ValueTask<ActionsOrDocumentEditModel> ReadActionsOrDocumentsAsync()
        {
            return await localStorage_.GetItemAsync<ActionsOrDocumentEditModel>(ActionsOrDocuments);
        }

        public async ValueTask SaveActionsOrDocumentsAsync(ActionsOrDocumentEditModel model)
        {
            await localStorage_.SetItemAsync(ActionsOrDocuments, model);
        }

        private async ValueTask<Signature> ReadSignatureAsync(int farmId, int farmInspectionId, Func<Inspection, Signature> getSignature)
        {
            var mandate = await ReadMandateAsync(farmId);
            var inspection = mandate.Inspections.FirstOrDefault(x => x.FarmInspectionId == farmInspectionId);
            return getSignature(inspection) ?? Signature.None;
        }

        public async ValueTask SaveSignatureAsync(int farmId, int farmInspectionId, Signature signature, Action<Inspection, Signature> signAction)
        {
            var mandate = await ReadMandateAsync(farmId);
            var inspection = mandate.Inspections.FirstOrDefault(x => x.FarmInspectionId == farmInspectionId);
            if (inspection != null)
                signAction(inspection, signature);

            await localStorage_.SetItemAsync(MandateDetailKey(farmId), new MandateFactory().Serialize(mandate));
        }

        private static string MandateDetailKey(int farmId)
        {
            return $"mandate_FarmId{farmId}";
        }

        private static string FarmKey(int farmId)
        {
            return $"farm_Id{farmId}";
        }

        private static string ChecklistKey(Checklist checklist)
        {
            return ChecklistKey(checklist.FarmInspectionId);
        }

        private static string ChecklistKey(int farmInspectionId)
        {
            return $"{Checklist}_FarmInspectionId{farmInspectionId}";
        }
    }
}
