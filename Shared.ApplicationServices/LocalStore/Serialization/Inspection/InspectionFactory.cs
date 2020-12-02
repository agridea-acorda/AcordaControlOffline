using System;
using System.Runtime.Serialization;
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
                                               Formatting.Indented,
                                               new JsonSerializerSettings
                                               {
                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                   ContractResolver = new AggregateRootContractResolver()
                                               });
        }

        private Domain.Inspection.Inspection Parse(InspectionDeserializationDto.Root dto)
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
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.FinishStatus), targetInstance, Parse(dto.FinishStatus));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.CloseStatus), targetInstance, Parse(dto.CloseStatus));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Inspection), nameof(Domain.Inspection.Inspection.ReopenStatus), targetInstance, Parse(dto.ReopenStatus));
            return targetInstance;
        }

        private Domain.Inspection.Domain Parse(InspectionDeserializationDto.Domain dtp)
        {
            if (dtp == null) return null;
            var targetInstance = (Domain.Inspection.Domain)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.Domain));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Domain), nameof(Domain.Inspection.Domain.Id), targetInstance, dtp.Id);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Domain), nameof(Domain.Inspection.Domain.ShortName), targetInstance, dtp.ShortName);
            return targetInstance;
        }

        private Domain.Inspection.Campaign Parse(InspectionDeserializationDto.Campaign dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.Campaign)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.Campaign));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Campaign), nameof(Domain.Inspection.Campaign.Id), targetInstance, dto.Id);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Campaign), nameof(Domain.Inspection.Campaign.Name), targetInstance, dto.Name);
            return targetInstance;
        }

        private Domain.Inspection.InspectionReason Parse(InspectionDeserializationDto.Reason dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.InspectionReason)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.InspectionReason));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.InspectionReason), nameof(Domain.Inspection.InspectionReason.Code), targetInstance, dto.Code);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.InspectionReason), nameof(Domain.Inspection.InspectionReason.Name), targetInstance, dto.Name);
            return targetInstance;
        }

        private Domain.Inspection.Appointment Parse(InspectionDeserializationDto.Appointment dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.Appointment)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.Appointment));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Appointment), nameof(Domain.Inspection.Appointment.Date), targetInstance, dto.Date);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Appointment), nameof(Domain.Inspection.Appointment.FirstContactDate), targetInstance, dto.FirstContactDate);
            return targetInstance;
        }

        private Domain.Inspection.InspectionStatus Parse(InspectionDeserializationDto.Status dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.InspectionStatus)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.InspectionStatus));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.InspectionStatus), nameof(Domain.Inspection.InspectionStatus.Code), targetInstance, dto.Code);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.InspectionStatus), nameof(Domain.Inspection.InspectionStatus.Name), targetInstance, dto.Name);
            return targetInstance;
        }

        private Domain.Inspection.InspectionOutcome Parse(InspectionDeserializationDto.OutcomeComputed dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.InspectionOutcome)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.InspectionOutcome));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.InspectionOutcome), nameof(Domain.Inspection.InspectionOutcome.Code), targetInstance, dto.Code);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.InspectionOutcome), nameof(Domain.Inspection.InspectionOutcome.Value), targetInstance, dto.Value);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.InspectionOutcome), nameof(Domain.Inspection.InspectionOutcome.Text), targetInstance, dto.Text);
            return targetInstance;
        }

        private Domain.Inspection.Signature Parse(InspectionDeserializationDto.Signature dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.Signature)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.Signature));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Signature), nameof(Domain.Inspection.Signature.Signatory), targetInstance, dto.Signatory);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Signature), nameof(Domain.Inspection.Signature.Proxy), targetInstance, dto.Proxy);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Signature), nameof(Domain.Inspection.Signature.Data), targetInstance, dto.Data);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Signature), nameof(Domain.Inspection.Signature.DataUrl), targetInstance, dto.DataUrl);
            return targetInstance;
        }

        private Domain.Inspection.Compliance Parse(InspectionDeserializationDto.Compliance dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.Compliance)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.Compliance));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Compliance), nameof(Domain.Inspection.Compliance.ActionsOrDocuments), targetInstance, dto.ActionsOrDocuments);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Compliance), nameof(Domain.Inspection.Compliance.DueDate), targetInstance, dto.DueDate);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Compliance), nameof(Domain.Inspection.Compliance.DueDateRespected), targetInstance, dto.DueDateRespected);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Compliance), nameof(Domain.Inspection.Compliance.DueDateNotRespected), targetInstance, dto.DueDateNotRespected);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Compliance), nameof(Domain.Inspection.Compliance.IncompleteOrNonCompliant), targetInstance, dto.IncompleteOrNonCompliant);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.Compliance), nameof(Domain.Inspection.Compliance.FurtherInvestigationNeeded), targetInstance, dto.FurtherInvestigationNeeded);
            return targetInstance;
        }

        private Domain.Inspection.FinishStatus Parse(InspectionDeserializationDto.FinishStatus dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.FinishStatus)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.FinishStatus));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.FinishStatus), nameof(Domain.Inspection.FinishStatus.DoneOn), targetInstance, dto.DoneOn);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.FinishStatus), nameof(Domain.Inspection.FinishStatus.DoneByInspector), targetInstance, dto.DoneByInspector);
            
            return targetInstance;
        }

        private Domain.Inspection.CloseStatus Parse(InspectionDeserializationDto.CloseStatus dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.CloseStatus)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.CloseStatus));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.CloseStatus), nameof(Domain.Inspection.CloseStatus.CloseDate), targetInstance, dto.CloseDate);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.CloseStatus), nameof(Domain.Inspection.CloseStatus.ClosedBy), targetInstance, dto.ClosedBy);

            return targetInstance;
        }

        private Domain.Inspection.ReopenStatus Parse(InspectionDeserializationDto.ReopenStatus dto)
        {
            if (dto == null) return null;
            var targetInstance = (Domain.Inspection.ReopenStatus)FormatterServices.GetUninitializedObject(typeof(Domain.Inspection.ReopenStatus));
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.ReopenStatus), nameof(Domain.Inspection.ReopenStatus.ReopenDate), targetInstance, dto.ReopenDate);
            SetPropertyValueViaBackingField(typeof(Domain.Inspection.ReopenStatus), nameof(Domain.Inspection.ReopenStatus.ReopenedBy), targetInstance, dto.ReopenedBy);

            return targetInstance;
        }
    }
}
