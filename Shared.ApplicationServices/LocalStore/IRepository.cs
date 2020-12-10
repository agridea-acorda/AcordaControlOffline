﻿using System;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Signature;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Farm;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore {
    public interface IRepository
    {
        ValueTask SaveMandatesAsync(Mandate[] mandates);
        ValueTask<Mandate[]> ReadAllMandatesAsync();
        ValueTask<bool> HasMandateAsync(int farmId);
        ValueTask<Domain.Mandate.Mandate> ReadMandateAsync(int farmId);
        ValueTask SaveMandateAsync(Domain.Mandate.Mandate mandate, int id);
        ValueTask SaveMandateJsonAsync(string json, int id);
        ValueTask SaveFarmJsonAsync(string json, int id);
        ValueTask SaveChecklistJsonAsync(string json, int id);
        ValueTask<Farm> ReadFarmAsync(int farmId);
        ValueTask<ChecklistSample> ReadChecklistSampleAsync();
        ValueTask SaveChecklistSampleAsync(ChecklistSample checklist);
        ValueTask<ActionsOrDocumentEditModel> ReadActionsOrDocumentsAsync();
        ValueTask SaveActionsOrDocumentsAsync(ActionsOrDocumentEditModel model);
        ValueTask SaveChecklistAsync(Checklist checklist);
        ValueTask<Checklist> ReadChecklistAsync(int farmInspectionId);
    }
}