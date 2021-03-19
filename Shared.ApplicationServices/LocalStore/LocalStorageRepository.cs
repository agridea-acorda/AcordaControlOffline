using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization;
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
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Inspection = Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection.Inspection;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore
{
    public class LocalStorageRepository : IRepository
    {
        public const string Mandates = "mandates";
        public const string Checklist = "checklist";
        public const string ActionsOrDocuments = "actionsOrDocuments";
        
        private readonly ILocalStorageService localStorage_;
        private readonly IJSRuntime jsRuntime_; // for profiling, to be removed

        private JsonSerializerOptions JsonSerializerOptions { get; } = new JsonSerializerOptions
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true,
            IgnoreReadOnlyProperties = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            WriteIndented = false
        };

        public LocalStorageRepository(ILocalStorageService localStorage, IJSRuntime jsRuntime)
        {
            localStorage_ = localStorage;
            jsRuntime_ = jsRuntime;
        }

        public async ValueTask SaveMandatesAsync(Mandate[] mandates)
        {
            await jsRuntime_.InvokeVoidAsync("console.time", "Mandate[] Serialize");
            // force use of System.Text.Json when Json.NET is not used explicitly (temporary fix that mimics Blazored.LocalStorage, it 'just works').
            string json = JsonSerializer.Serialize(mandates, JsonSerializerOptions); 
            await jsRuntime_.InvokeVoidAsync("console.timeEnd", "Mandate[] Serialize");

            await SetItemWithOptimization(Mandates, json);
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

        public async ValueTask<string> ReadMandateJsonAsync(int farmId)
        {
            return await localStorage_.GetItemAsStringAsync(MandateDetailKey(farmId));
        }

        public async ValueTask<Domain.Mandate.Mandate> ReadMandateAsync(int farmId)
        {
            string json = await ReadMandateJsonAsync(farmId);
            return new MandateFactory().Parse(json);
        }

        public async ValueTask DeleteMandateAsync(int farmId)
        {
            await localStorage_.RemoveItemAsync(MandateDetailKey(farmId));
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

        public async ValueTask DeleteFarmAsync(int farmId)
        {
            await localStorage_.RemoveItemAsync(FarmKey(farmId));
        }

        public async ValueTask SaveMandateAsync(Domain.Mandate.Mandate mandate, int id)
        {
            string key = MandateDetailKey(id);
            await jsRuntime_.InvokeVoidAsync("console.time", "MandateFactory.Serialize");
            string json = new MandateFactory().Serialize(mandate);
            await jsRuntime_.InvokeVoidAsync("console.timeEnd", "MandateFactory.Serialize");

            await SetItemWithOptimization(key, json);
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
            string key = ChecklistKey(checklist);

            await jsRuntime_.InvokeVoidAsync("console.time", "ChecklistFactory.Serialize");
            string json = new ChecklistFactory().Serialize(checklist);
            await jsRuntime_.InvokeVoidAsync("console.timeEnd", "ChecklistFactory.Serialize");

            await SetItemWithOptimization(key, json);
        }

        public async Task<string> ReadChecklistJsonAsync(int farmInspectionId)
        {
            return await localStorage_.GetItemAsStringAsync(ChecklistKey(farmInspectionId));
        }

        public async Task<Checklist> ReadChecklistAsync(int farmInspectionId)
        {
            string json = await ReadChecklistJsonAsync(farmInspectionId);
            return new ChecklistFactory().Parse(json);
        }

        public async ValueTask DeleteChecklistAsync(int farmInspectionId)
        {
            await localStorage_.RemoveItemAsync(ChecklistKey(farmInspectionId));
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

        public async ValueTask SetAppUpdateAvailable()
        {
            await localStorage_.SetItemAsync(AppUpdateAvailableKey(), true);
        }

        public async ValueTask<bool> GetAppUpdateAvailableFlag()
        {
            return await localStorage_.GetItemAsync<bool>(AppUpdateAvailableKey());
        }

        public async ValueTask ResetAppUpdateAvailable()
        {
            await localStorage_.RemoveItemAsync(AppUpdateAvailableKey());
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

        private static string AppUpdateAvailableKey()
        {
            return "appupdateavailable";
        }

        private async ValueTask SetItemWithOptimization(string key, string json)
        {
            await jsRuntime_.InvokeVoidAsync("console.time", "SetItemWithOptimization");
            if (jsRuntime_ is IJSUnmarshalledRuntime jsUnmarshalledRuntime)
            {
                jsUnmarshalledRuntime.InvokeUnmarshalled<string, string, bool>("setItemInLocalStorageUnmarshalled", key, json);
            }
            else if (jsRuntime_ is IJSInProcessRuntime jsInProcessRuntime)
            {
                jsInProcessRuntime.InvokeVoid("localStorage.setItem", key, json);
            }
            else // not webassembly, async call necessary to accomodate server-side scenario
            {
                await jsRuntime_.InvokeVoidAsync("localStorage.setItem", key, json);
            }
            await jsRuntime_.InvokeVoidAsync("console.timeEnd", "SetItemWithOptimization");
        }
    }
}
