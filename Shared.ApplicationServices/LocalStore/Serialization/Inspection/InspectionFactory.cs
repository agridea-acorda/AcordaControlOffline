using System;
using System.Runtime.Serialization;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection
{
    public class InspectionFactory : AggregateRootFactoryBase<Domain.Inspection.Inspection>
    {
        public override Domain.Inspection.Inspection Parse(string json)
        {
            var dto = JsonConvert.DeserializeObject<InspectionDeserializationDto.Root>(json);
            return Parse(dto);
        }

        public override string Serialize(Domain.Inspection.Inspection aggregateRoot)
        {
            return JsonConvert.SerializeObject(aggregateRoot,
                                               Formatting.None,
                                               new JsonSerializerSettings
                                               {
                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                   ContractResolver = new AggregateRootContractResolver()
                                               });
        }

        internal Domain.Inspection.Inspection Parse(InspectionDeserializationDto.Root dto)
        {
            if (dto == null) return null;

            var targetInstance = (Domain.Inspection.Inspection) FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.Inspection));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Id), targetInstance, dto.Id);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.FarmInspectionId), targetInstance, dto.FarmInspectionId);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.InspectionId), targetInstance, Guid.Parse(dto.InspectionId));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.ChecklistId), targetInstance, dto.ChecklistId);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.FarmId), targetInstance, dto.FarmId);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Comment), targetInstance, dto.Comment);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.CommentForFarmer), targetInstance, dto.CommentForFarmer);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.CommentForOffice), targetInstance, dto.CommentForOffice);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.PercentComputed), targetInstance, dto.PercentComputed);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.DateComputed), targetInstance, dto.DateComputed);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Domain), targetInstance, Parse(dto.Domain));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Campaign), targetInstance, Parse(dto.Campaign));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Reason), targetInstance, Parse(dto.Reason));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Appointment), targetInstance, Parse(dto.Appointment));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Status), targetInstance, Parse(dto.Status));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.OutcomeComputed), targetInstance, Parse(dto.OutcomeComputed));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.InspectorSignature), targetInstance, Parse(dto.InspectorSignature));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Inspector2Signature), targetInstance, Parse(dto.Inspector2Signature));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.FarmerSignature), targetInstance, Parse(dto.FarmerSignature));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.Compliance), targetInstance, Parse(dto.Compliance));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.PdfReport), targetInstance, Parse(dto.PdfReport));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.FinishStatus), targetInstance, Parse(dto.FinishStatus));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.CloseStatus), targetInstance, Parse(dto.CloseStatus));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.ReopenStatus), targetInstance, Parse(dto.ReopenStatus));
            return targetInstance;
        }

        private Domain.Inspection.Domain Parse(InspectionDeserializationDto.Domain dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.Domain)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.Domain));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Domain), nameof(Domain.Inspection.Domain.Id), targetInstance, dto.Id);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Domain), nameof(Domain.Inspection.Domain.ShortName), targetInstance, dto.ShortName);
            return targetInstance;
        }

        private Campaign Parse(InspectionDeserializationDto.Campaign dto)
        {
            if (dto == null) return null;
            var targetInstance = (Campaign)FormatterServices.GetUninitializedObject(typeof(Campaign));
            SetPropertyValueViaBackingField(typeof(Campaign), nameof(Campaign.Id), targetInstance, dto.Id);
            SetPropertyValueViaBackingField(typeof(Campaign), nameof(Campaign.Name), targetInstance, dto.Name);
            SetPropertyValueViaBackingField(typeof(Campaign), nameof(Campaign.Year), targetInstance, dto.Year);
            return targetInstance;
        }

        private InspectionReason Parse(InspectionDeserializationDto.Reason dto)
        {
            if (dto == null) return InspectionReason.Unknown;
            var targetInstance = (InspectionReason)FormatterServices.GetUninitializedObject(typeof(InspectionReason));
            SetPropertyValueViaBackingField(typeof(InspectionReason), nameof(InspectionReason.Code), targetInstance, dto.Code);
            SetPropertyValueViaBackingField(typeof(InspectionReason), nameof(InspectionReason.Name), targetInstance, dto.Name);
            return targetInstance;
        }

        private Appointment Parse(InspectionDeserializationDto.Appointment dto)
        {
            if (dto == null) return Appointment.None;
            var targetInstance = (Appointment)FormatterServices.GetUninitializedObject(typeof(Appointment));
            SetPropertyValueViaBackingField(typeof(Appointment), nameof(Appointment.Date), targetInstance, dto.Date);
            SetPropertyValueViaBackingField(typeof(Appointment), nameof(Appointment.FirstContactDate), targetInstance, dto.FirstContactDate);
            return targetInstance;
        }

        private InspectionStatus Parse(InspectionDeserializationDto.Status dto)
        {
            if (dto == null) return InspectionStatus.Planned;
            var targetInstance = (InspectionStatus)FormatterServices.GetUninitializedObject(typeof(InspectionStatus));
            SetPropertyValueViaBackingField(typeof(InspectionStatus), nameof(InspectionStatus.Code), targetInstance, dto.Code);
            SetPropertyValueViaBackingField(typeof(InspectionStatus), nameof(InspectionStatus.Name), targetInstance, dto.Name);
            return targetInstance;
        }

        private InspectionOutcome Parse(InspectionDeserializationDto.OutcomeComputed dto)
        {
            if (dto == null) return InspectionOutcome.NotInspected;
            var targetInstance = (InspectionOutcome)FormatterServices.GetUninitializedObject(typeof(InspectionOutcome));
            SetPropertyValueViaBackingField(typeof(InspectionOutcome), nameof(InspectionOutcome.Code), targetInstance, dto.Code);
            SetPropertyValueViaBackingField(typeof(InspectionOutcome), nameof(InspectionOutcome.Value), targetInstance, dto.Value);
            SetPropertyValueViaBackingField(typeof(InspectionOutcome), nameof(InspectionOutcome.Text), targetInstance, dto.Text);
            return targetInstance;
        }

        private Signature Parse(InspectionDeserializationDto.Signature dto)
        {
            if (dto == null) return Signature.None;
            var targetInstance = (Signature)FormatterServices.GetUninitializedObject(typeof(Signature));
            SetPropertyValueViaBackingField(typeof(Signature), nameof(Signature.Signatory), targetInstance, dto.Signatory);
            SetPropertyValueViaBackingField(typeof(Signature), nameof(Signature.Proxy), targetInstance, dto.Proxy);
            SetPropertyValueViaBackingField(typeof(Signature), nameof(Signature.Data), targetInstance, dto.Data);
            SetPropertyValueViaBackingField(typeof(Signature), nameof(Signature.DataUrl), targetInstance, dto.DataUrl);
            return targetInstance;
        }

        private Compliance Parse(InspectionDeserializationDto.Compliance dto)
        {
            if (dto == null) return Compliance.Empty;
            var targetInstance = (Compliance)FormatterServices.GetUninitializedObject(typeof(Compliance));
            SetPropertyValueViaBackingField(typeof(Compliance), nameof(Compliance.ActionsOrDocuments), targetInstance, dto.ActionsOrDocuments);
            SetPropertyValueViaBackingField(typeof(Compliance), nameof(Compliance.DueDate), targetInstance, dto.DueDate);
            SetPropertyValueViaBackingField(typeof(Compliance), nameof(Compliance.DueDateRespected), targetInstance, dto.DueDateRespected);
            SetPropertyValueViaBackingField(typeof(Compliance), nameof(Compliance.DueDateNotRespected), targetInstance, dto.DueDateNotRespected);
            SetPropertyValueViaBackingField(typeof(Compliance), nameof(Compliance.IncompleteOrNonCompliant), targetInstance, dto.IncompleteOrNonCompliant);
            SetPropertyValueViaBackingField(typeof(Compliance), nameof(Compliance.FurtherInvestigationNeeded), targetInstance, dto.FurtherInvestigationNeeded);
            return targetInstance;
        }

        private PdfReport Parse(InspectionDeserializationDto.PdfReport dto)
        {
            if (dto == null) return PdfReport.None;
            var targetInstance = (PdfReport)FormatterServices.GetUninitializedObject(typeof(PdfReport));
            SetPropertyValueViaBackingField(typeof(PdfReport), nameof(PdfReport.Bytes), targetInstance, dto.Bytes);
            return targetInstance;
        }

        private FinishStatus Parse(InspectionDeserializationDto.FinishStatus dto)
        {
            if (dto == null) return FinishStatus.NotFinished;
            var targetInstance = (FinishStatus)FormatterServices.GetUninitializedObject(typeof(FinishStatus));
            SetPropertyValueViaBackingField(typeof(FinishStatus), nameof(FinishStatus.DoneOn), targetInstance, dto.DoneOn);
            SetPropertyValueViaBackingField(typeof(FinishStatus), nameof(FinishStatus.DoneByInspector), targetInstance, dto.DoneByInspector);
            
            return targetInstance;
        }

        private CloseStatus Parse(InspectionDeserializationDto.CloseStatus dto)
        {
            if (dto == null) return CloseStatus.NotClosed;
            var targetInstance = (CloseStatus)FormatterServices.GetUninitializedObject(typeof(CloseStatus));
            SetPropertyValueViaBackingField(typeof(CloseStatus), nameof(CloseStatus.CloseDate), targetInstance, dto.CloseDate);
            SetPropertyValueViaBackingField(typeof(CloseStatus), nameof(CloseStatus.ClosedBy), targetInstance, dto.ClosedBy);

            return targetInstance;
        }

        private ReopenStatus Parse(InspectionDeserializationDto.ReopenStatus dto)
        {
            if (dto == null) return ReopenStatus.NotReopened;
            var targetInstance = (ReopenStatus)FormatterServices.GetUninitializedObject(typeof(ReopenStatus));
            SetPropertyValueViaBackingField(typeof(ReopenStatus), nameof(ReopenStatus.ReopenDate), targetInstance, dto.ReopenDate);
            SetPropertyValueViaBackingField(typeof(ReopenStatus), nameof(ReopenStatus.ReopenedBy), targetInstance, dto.ReopenedBy);

            return targetInstance;
        }
    }
}
