using System;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Signature;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore {
    public interface IRepository
    {
        ValueTask SaveMandatesAsync(Mandate[] mandates);
        ValueTask<Mandate[]> ReadAllMandatesAsync();
        ValueTask<bool> HasMandateAsync(int farmId);
        ValueTask<ViewModel.MandateDetail.Mandate> ReadMandateAsync(int farmId);
        ValueTask SaveMandateAsync(ViewModel.MandateDetail.Mandate mandate, int id = 0);
        ValueTask SaveMandateJsonAsync(string json, int id);
        ValueTask SaveFarmJsonAsync(string json, int id);
        ValueTask<ChecklistSample> ReadChecklistSampleAsync();
        ValueTask SaveChecklistSampleAsync(ChecklistSample checklist);
        ValueTask<ActionsOrDocumentEditModel> ReadActionsOrDocumentsAsync();
        ValueTask SaveActionsOrDocumentsAsync(ActionsOrDocumentEditModel model);
        ValueTask SaveChecklistAsync(Checklist checklist, Func<Checklist, string> serialize);
        ValueTask<Checklist> ReadChecklistAsync(int farmInspectionId, Func<string, Checklist> deserialize);
    }
}