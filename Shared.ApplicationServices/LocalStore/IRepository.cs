using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Signature;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Farm;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore {
    public interface IRepository
    {
        ValueTask SaveMandatesAsync(Mandate[] mandates);
        ValueTask<Mandate[]> ReadAllMandatesAsync();
        ValueTask ClearMandatesListAsync();
        ValueTask<bool> HasMandateAsync(int farmId);
        ValueTask<string> ReadMandateJsonAsync(int farmId);
        ValueTask<Domain.Mandate.Mandate> ReadMandateAsync(int farmId);
        ValueTask DeleteMandateAsync(int farmId);
        ValueTask SaveMandateAsync(Domain.Mandate.Mandate mandate, int id);
        ValueTask SaveMandateJsonAsync(string json, int id);
        ValueTask SaveFarmJsonAsync(string json, int id);
        ValueTask SaveChecklistJsonAsync(string json, int id);
        ValueTask<Farm> ReadFarmAsync(int farmId);
        ValueTask DeleteFarmAsync(int farmId);
        ValueTask<ChecklistSample> ReadChecklistSampleAsync();
        ValueTask SaveChecklistSampleAsync(ChecklistSample checklist);
        ValueTask<ActionsOrDocumentEditModel> ReadActionsOrDocumentsAsync();
        ValueTask SaveActionsOrDocumentsAsync(ActionsOrDocumentEditModel model);
        ValueTask SaveChecklistAsync(Checklist checklist);
        Task<string> ReadChecklistJsonAsync(int farmInspectionId);
        Task<Checklist> ReadChecklistAsync(int farmInspectionId);
        ValueTask DeleteChecklistAsync(int farmInspectionId);
        ValueTask<Signature> ReadInspectorSignatureAsync(int farmId, int farmInspectionId);
        ValueTask SaveInspectorSignatureAsync(int farmId, int farmInspectionId, Signature signature);
        ValueTask<Signature> ReadFarmerSignatureAsync(int farmId, int farmInspectionId);
        ValueTask SaveFarmerSignatureAsync(int farmId, int farmInspectionId, Signature signature);
        ValueTask<Signature> ReadInspector2SignatureAsync(int farmId, int farmInspectionId);
        ValueTask SaveInspector2SignatureAsync(int farmId, int farmInspectionId, Signature signature);
        ValueTask SetAppUpdateAvailable();
        ValueTask<bool> GetAppUpdateAvailableFlag();
        ValueTask ResetAppUpdateAvailable();
    }
}