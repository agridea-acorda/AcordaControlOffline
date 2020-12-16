using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist
{
    public class ChecklistFactory : AggregateRootFactoryBase<Domain.Checklist.Checklist>
    {
        public override Domain.Checklist.Checklist Parse(string json)
        {
            var dto = JsonConvert.DeserializeObject<ChecklistDeserializationDto>(json);
            return Parse(dto);
        }

        public Domain.Checklist.Checklist Parse(ChecklistDeserializationDto dto)
        {
            if (dto == null) return null;
            
            var checklist = new Domain.Checklist.Checklist(dto.FarmInspectionId);
            foreach (var dtoRubric in dto.Rubrics)
            {
                if (ParseResult(dtoRubric.Value) is RubricResult rubricResult)
                    checklist.AddRubric(dtoRubric.Key, rubricResult);
            }

            return checklist;
        }

        public override string Serialize(Domain.Checklist.Checklist checklist)
        {
            return JsonConvert.SerializeObject(checklist,
                                               Formatting.Indented,
                                               new JsonSerializerSettings
                                               {
                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                   ContractResolver = new ChecklistContractResolver()
                                               });
        }

        private Result ParseResult(ChecklistDeserializationDto.Result dto, Result parent = null, int depth = 0)
        {
            var targetType = depth == 0 ? typeof(RubricResult) :
                             dto.Children?.Any() ?? false ? typeof(GroupResult) :
                             typeof(PointResult);
            var targetInstance = (Result)FormatterServices.GetUninitializedObject(targetType);
            SetPropertyValueViaBackingField(targetType, nameof(Result.ConjunctElementCode), targetInstance, dto.ConjunctElementCode);
            SetPropertyValueViaBackingField(targetType, nameof(Result.ElementCode), targetInstance, dto.ElementCode);
            SetPropertyValueViaBackingField(targetType, nameof(Result.Name), targetInstance, dto.Name);
            SetPropertyValueViaBackingField(targetType, nameof(Result.ShortName), targetInstance, dto.ShortName);
            SetPropertyValueViaBackingField(targetType, nameof(Result.InspectorComment), targetInstance, dto.InspectorComment);
            SetPropertyValueViaBackingField(targetType, nameof(Result.FarmerComment), targetInstance, dto.FarmerComment);
            SetPropertyValueViaBackingField(targetType, nameof(Result.Outcome), targetInstance, Parse(dto.Outcome));
            SetPropertyValueViaBackingField(targetType, nameof(Result.Defect), targetInstance, Parse(dto.Defect));
            SetPropertyValueViaBackingField(targetType, nameof(Result.Seriousness), targetInstance, Parse(dto.Seriousness));
            // todo PredefinedDefect

            SetPropertyValueViaBackingField(targetType, nameof(Result.Children), targetInstance, new SortedList<string, ITreeNode<Result>>());
            if (dto.Children?.Any() == true)
            {
                foreach (var child in dto.Children)
                    targetInstance.Children.TryAdd(child.Key, ParseResult(child.Value, targetInstance, ++depth));
            }

            SetPropertyValueViaBackingField(targetType, nameof(Result.Parent), targetInstance, parent);
            return targetInstance;
        }

        private InspectionOutcome Parse(ChecklistDeserializationDto.InspectionOutcome dto)
        {
            if (dto == null) return InspectionOutcome.NotInspected;
            var targetInstance = (InspectionOutcome)FormatterServices.GetUninitializedObject(typeof(InspectionOutcome));
            SetPropertyValueViaBackingField(typeof(InspectionOutcome), nameof(InspectionOutcome.Code), targetInstance, dto.Code);
            SetPropertyValueViaBackingField(typeof(InspectionOutcome), nameof(InspectionOutcome.Value), targetInstance, dto.Value);
            SetPropertyValueViaBackingField(typeof(InspectionOutcome), nameof(InspectionOutcome.Text), targetInstance, dto.Text);
            return targetInstance;
        }
        private Defect Parse(ChecklistDeserializationDto.Defect dto)
        {
            if (dto == null) return Defect.None;
            var targetInstance = (Defect)FormatterServices.GetUninitializedObject(typeof(Defect));
            SetPropertyValueViaBackingField(typeof(Defect), nameof(Defect.Description), targetInstance, dto.Description);
            SetPropertyValueViaBackingField(typeof(Defect), nameof(Defect.Size), targetInstance, Parse(dto.Size)); 
            return targetInstance;
        }

        private Defect.Measurement Parse(ChecklistDeserializationDto.Defect.Measurement dto)
        {
            if (dto == null) return Defect.Measurement.Unspecified;
            var targetInstance = (Defect.Measurement)FormatterServices.GetUninitializedObject(typeof(Defect.Measurement));
            SetPropertyValueViaBackingField(typeof(Defect.Measurement), nameof(Defect.Measurement.Size), targetInstance, dto.Size);
            SetPropertyValueViaBackingField(typeof(Defect.Measurement), nameof(Defect.Measurement.Unit), targetInstance, dto.Unit);
            return targetInstance;
        }

        private DefectSeriousness Parse(ChecklistDeserializationDto.DefectSeriousness dto)
        {
            if (dto == null) return DefectSeriousness.Empty;
            var targetInstance = (DefectSeriousness)FormatterServices.GetUninitializedObject(typeof(DefectSeriousness));
            SetPropertyValueViaBackingField(typeof(DefectSeriousness), nameof(DefectSeriousness.Code), targetInstance, dto.Code);
            SetPropertyValueViaBackingField(typeof(DefectSeriousness), nameof(DefectSeriousness.Name), targetInstance, dto.Name);
            return targetInstance;
        }
    }
}
