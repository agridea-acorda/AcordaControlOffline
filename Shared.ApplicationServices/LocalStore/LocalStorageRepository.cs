﻿using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Farm;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Mandate;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Signature;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Town;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Farm;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Microsoft.JSInterop;
using Inspection = Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection.Inspection;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore
{
    /// <summary>
    /// todo new version with localforage instead of localstorage (to remove local storage size limitation)
    /// todo (move code in new LocalForageRepository)
    /// </summary>
    /// <remarks>
    /// System.Text.Json.JsonSerializer is used where no explicit serialization/deserialization is provided.
    /// todo: use either System.Text.Json or NewtonSoft.Json everywhere.
    /// </remarks>
    public class LocalStorageRepository : IRepository
    {
        public const string Mandates = "mandates";
        public const string Towns = "towns";
        public const string InspectorName = "inspectorName";
        public const string Checklist = "checklist";
        public const string ActionsOrDocuments = "actionsOrDocuments";
        private readonly IJSRuntime jsRuntime_;

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

        public LocalStorageRepository(IJSRuntime jsRuntime)
        {
            jsRuntime_ = jsRuntime;
        }

        public async ValueTask SaveInspectorNameAsync(string inspectorName)
        {
            //await jsRuntime_.InvokeVoidAsync("console.time", "Mandate[] Serialize");
            // force use of System.Text.Json when Json.NET is not used explicitly (temporary fix that mimics Blazored.LocalStorage, it 'just works').
            string json = JsonSerializer.Serialize(inspectorName, JsonSerializerOptions);
            //await jsRuntime_.InvokeVoidAsync("console.timeEnd", "Mandate[] Serialize");

            await SetStringItemAsync(InspectorName, json);
        }

        public async ValueTask SaveMandatesAsync(Mandate[] mandates)
        {
            //await jsRuntime_.InvokeVoidAsync("console.time", "Mandate[] Serialize");
            // force use of System.Text.Json when Json.NET is not used explicitly (temporary fix that mimics Blazored.LocalStorage, it 'just works').
            string json = JsonSerializer.Serialize(mandates, JsonSerializerOptions); 
            //await jsRuntime_.InvokeVoidAsync("console.timeEnd", "Mandate[] Serialize");

            await SetStringItemAsync(Mandates, json);
        }

        public async ValueTask<Mandate[]> ReadAllMandatesAsync()
        {
            string json = await GetItemAsStringAsync(Mandates);
            return json == null ? null : JsonSerializer.Deserialize<Mandate[]>(json, JsonSerializerOptions);
        }

        public async ValueTask SaveTownsAsync(Town[] towns)
        {
            //await jsRuntime_.InvokeVoidAsync("console.time", "Mandate[] Serialize");
            // force use of System.Text.Json when Json.NET is not used explicitly (temporary fix that mimics Blazored.LocalStorage, it 'just works').
            string json = JsonSerializer.Serialize(towns, JsonSerializerOptions);
            //await jsRuntime_.InvokeVoidAsync("console.timeEnd", "Mandate[] Serialize");

            await SetStringItemAsync(Towns, json);
        }

        public async ValueTask<Town[]> ReadAllTownsAsync()
        {
            string json = await GetItemAsStringAsync(Towns);
            return json == null ? null : JsonSerializer.Deserialize<Town[]>(json, JsonSerializerOptions);
        }

        public async ValueTask ClearMandatesListAsync()
        {
            await RemoveItemAsync(Mandates);
        }

        public async ValueTask<bool> HasMandateAsync(int farmId)
        {
            string key = MandateDetailKey(farmId);
            return await ContainsKeyAsync(key);
        }

        public async ValueTask<string> ReadMandateJsonAsync(int farmId)
        {
            return await GetItemAsStringAsync(MandateDetailKey(farmId));
        }

        public async ValueTask<Domain.Mandate.Mandate> ReadMandateAsync(int farmId)
        {
            string json = await ReadMandateJsonAsync(farmId);
            return new MandateFactory().Parse(json);
        }

        public async ValueTask DeleteMandateAsync(int farmId)
        {
            await RemoveItemAsync(MandateDetailKey(farmId));
        }

        public async ValueTask<Signature> ReadInspectorSignatureAsync(int farmId, int farmInspectionId)
        {
            return await ReadSignatureAsync(farmId, farmInspectionId, x => x?.InspectorSignature);
        }

        public async ValueTask<string> ReadInspectorNameAsync()
        {
            return await GetItemAsStringAsync(InspectorName);
        }

        public async ValueTask SaveInspectorSignatureAsync(int farmId, int farmInspectionId, Signature signature)
        {
            var mandate = await ReadMandateAsync(farmId);
            var inspection = mandate.Inspections.FirstOrDefault(x => x.FarmInspectionId == farmInspectionId);
            if (inspection != null)
                inspection.InspectorSigns(signature);
            
            await SetStringItemAsync(MandateDetailKey(farmId), new MandateFactory().Serialize(mandate));
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
            string json = await GetItemAsStringAsync(FarmKey(farmId));
            return new FarmFactory().Parse(json);
        }

        public async ValueTask DeleteFarmAsync(int farmId)
        {
            await RemoveItemAsync(FarmKey(farmId));
        }

        public async ValueTask SaveMandateAsync(Domain.Mandate.Mandate mandate, int id)
        {
            string key = MandateDetailKey(id);
            string json = new MandateFactory().Serialize(mandate);
            await SetStringItemAsync(key, json);
        }

        public async ValueTask SaveMandateJsonAsync(string json, int id)
        {
            string key = MandateDetailKey(id);
            await SetStringItemAsync(key, json);
        }

        public async ValueTask SaveFarmJsonAsync(string json, int id)
        {
            string key = FarmKey(id);
            await SetStringItemAsync(key, json);
        }

        public async ValueTask SaveChecklistJsonAsync(string json, int id)
        {
            string key = ChecklistKey(id);
            await SetStringItemAsync(key, json);
        }

        public async ValueTask<ChecklistSample> ReadChecklistSampleAsync()
        {
            string json = await GetItemAsStringAsync(Checklist);
            return json == null ? null : JsonSerializer.Deserialize<ChecklistSample>(json, JsonSerializerOptions);
        }

        public async ValueTask SaveChecklistSampleAsync(ChecklistSample checklist)
        {
            string json = JsonSerializer.Serialize(checklist, JsonSerializerOptions);
            await SetStringItemAsync(Checklist, json);
        }

        public async ValueTask SaveChecklistAsync(Checklist checklist)
        {
            string key = ChecklistKey(checklist);

            //await jsRuntime_.InvokeVoidAsync("console.time", "ChecklistFactory.Serialize");
            string json = new ChecklistFactory().Serialize(checklist);
            //await jsRuntime_.InvokeVoidAsync("console.timeEnd", "ChecklistFactory.Serialize");

            await SetStringItemAsync(key, json);
        }

        public async Task<string> ReadChecklistJsonAsync(int farmInspectionId)
        {
            return await GetItemAsStringAsync(ChecklistKey(farmInspectionId));
        }

        public async Task<Checklist> ReadChecklistAsync(int farmInspectionId)
        {
            string json = await ReadChecklistJsonAsync(farmInspectionId);
            return new ChecklistFactory().Parse(json);
        }

        public async ValueTask DeleteChecklistAsync(int farmInspectionId)
        {
            await RemoveItemAsync(ChecklistKey(farmInspectionId));
        }

        public async ValueTask<ActionsOrDocumentEditModel> ReadActionsOrDocumentsAsync()
        {
            string json = await GetItemAsStringAsync(ActionsOrDocuments);
            return json == null ? null : JsonSerializer.Deserialize<ActionsOrDocumentEditModel>(json, JsonSerializerOptions);
        }

        public async ValueTask SaveActionsOrDocumentsAsync(ActionsOrDocumentEditModel model)
        {
            string json = JsonSerializer.Serialize(model, JsonSerializerOptions);
            await SetStringItemAsync(ActionsOrDocuments, json);
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

            await SetStringItemAsync(MandateDetailKey(farmId), new MandateFactory().Serialize(mandate));
        }

        public async ValueTask SetAppUpdateAvailable()
        {
            await SetStringItemAsync(AppUpdateAvailableKey(), JsonSerializer.Serialize(true, JsonSerializerOptions));
        }

        public async ValueTask<bool> GetAppUpdateAvailableFlag()
        {
            string json = await GetItemAsStringAsync(AppUpdateAvailableKey());
            return json != null && JsonSerializer.Deserialize<bool>(json, JsonSerializerOptions);
        }

        public async ValueTask ResetAppUpdateAvailable()
        {
            await RemoveItemAsync(AppUpdateAvailableKey());
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

        private async ValueTask SetStringItemAsync(string key, string json)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            //await jsRuntime_.InvokeVoidAsync("console.time", "SetStringItemAsync");
            switch (jsRuntime_)
            {
                case IJSUnmarshalledRuntime jsUnmarshalledRuntime:
                    jsUnmarshalledRuntime.InvokeUnmarshalled<string, string, bool>("setItemUnmarshalled", key, json);
                    break;
                case IJSInProcessRuntime jsInProcessRuntime:
                    jsInProcessRuntime.InvokeVoid("localStorage.setItem", key, json);
                    break;

                default:
                    await jsRuntime_.InvokeVoidAsync("localStorage.setItem", key, json);
                    break;
            }
            //await jsRuntime_.InvokeVoidAsync("console.timeEnd", "SetStringItemAsync");
        }

        private async ValueTask<string> GetItemAsStringAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            //await jsRuntime_.InvokeVoidAsync("console.time", "GetItemAsStringAsync");
            string returnedValue = jsRuntime_ switch
            {
                IJSUnmarshalledRuntime jsUnmarshalledRuntime => jsUnmarshalledRuntime.InvokeUnmarshalled<string, string>("getItemUnmarshalled", key),
                IJSInProcessRuntime jsInProcessRuntime => jsInProcessRuntime.Invoke<string>("localStorage.getItem", key),
                _ => await jsRuntime_.InvokeAsync<string>("localStorage.getItem", key)
            };
            //await jsRuntime_.InvokeVoidAsync("console.timeEnd", "GetItemAsStringAsync");
            return returnedValue;
        }

        public async ValueTask RemoveItemAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            //await jsRuntime_.InvokeVoidAsync("console.time", "RemoveItemAsync");
            switch (jsRuntime_)
            {
                case IJSUnmarshalledRuntime jsUnmarshalledRuntime:
                    jsUnmarshalledRuntime.InvokeUnmarshalled<string, bool>("removeItemUnmarshalled", key);
                    break;
                case IJSInProcessRuntime jsInProcessRuntime:
                    jsInProcessRuntime.InvokeVoid("localStorage.removeItem", key);
                    break;

                default:
                    await jsRuntime_.InvokeVoidAsync("localStorage.removeItem", key);
                    break;
            }
            //await jsRuntime_.InvokeVoidAsync("console.timeEnd", "RemoveItemAsync");
        }

        public async ValueTask<bool> ContainsKeyAsync(string key)
        {
            //await jsRuntime_.InvokeVoidAsync("console.time", "ContainsKeyAsync");
            bool returnedValue = jsRuntime_ switch
            {
                //IJSUnmarshalledRuntime jsUnmarshalledRuntime => jsUnmarshalledRuntime.InvokeUnmarshalled<string, bool>("containsKeyUnmarshalled", key),
                IJSInProcessRuntime jsInProcessRuntime => jsInProcessRuntime.Invoke<bool>("localStorage.hasOwnProperty", key),
                _ => await jsRuntime_.InvokeAsync<bool>("localStorage.hasOwnProperty", key)
            };
            //await jsRuntime_.InvokeVoidAsync("console.timeEnd", "ContainsKeyAsync");
            return returnedValue;
        }
    }
}
