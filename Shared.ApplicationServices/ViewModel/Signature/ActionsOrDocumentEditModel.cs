using System;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using EnsureThat;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Signature
{
    public class ActionsOrDocumentEditModel
    {
        public string CommentForFarmer { get; set; }
        public string ActionsOrDocuments { get; set; }
        public DateTime? DueDate { get; set; }

        public static ActionsOrDocumentEditModel FromDomain(Inspection inspection)
        {
            Ensure.That(inspection, nameof(inspection)).IsNotNull();
            var model = new ActionsOrDocumentEditModel();
            model.CommentForFarmer = inspection.CommentForFarmer;
            model.ActionsOrDocuments = inspection.Compliance.ActionsOrDocuments;
            model.DueDate = inspection.Compliance.DueDate;
            return model;
        }
    }
}
