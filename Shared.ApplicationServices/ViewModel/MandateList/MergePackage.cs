using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Mandate;
using EnsureThat;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList
{
    public class MergePackage
    {
        public string Mandate { get; set; }
        public string[] Checklists { get; set; }

        public static MergePackage FromDomain(Domain.Mandate.Mandate mandate, Domain.Checklist.Checklist[] checklists)
        {
            Ensure.That(mandate, nameof(mandate)).IsNotNull();
            Ensure.That(checklists, nameof(checklists)).IsNotNull();
            Ensure.That(checklists, nameof(checklists)).HasItems();
            foreach (var checklist in checklists)
                Ensure.That(checklist, nameof(checklist)).IsNotNull();
            
            var mergePackage = new MergePackage();
            var mandateFactory = new MandateFactory();
            var checklistFactory = new ChecklistFactory();
            mergePackage.Mandate = mandateFactory.Serialize(mandate);
            mergePackage.Checklists = checklists.Select(checklistFactory.Serialize).ToArray();
            return mergePackage;
        }

        public static MergePackage FromJson(string mandateJson, string[] checklistsJson)
        {
            Ensure.That(mandateJson, nameof(mandateJson)).IsNotEmptyOrWhiteSpace();
            Ensure.That(checklistsJson, nameof(checklistsJson)).IsNotNull();
            Ensure.That(checklistsJson, nameof(checklistsJson)).HasItems();
            foreach (string checklist in checklistsJson)
                Ensure.That(checklist, nameof(checklist)).IsNotEmptyOrWhiteSpace();
            
            var mergePackage = new MergePackage
            {
                Mandate = mandateJson,
                Checklists = checklistsJson
            };
            return mergePackage;
        }
    }
}
