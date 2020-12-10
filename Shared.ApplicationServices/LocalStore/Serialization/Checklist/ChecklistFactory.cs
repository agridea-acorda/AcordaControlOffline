using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist
{
    public class ChecklistFactory : AggregateRootFactoryBase<Domain.Checklist.Checklist>
    {
        private static readonly List<PropertyInfo> SourceProperties;
        private static readonly Dictionary<string, PropertyInfo> TargetProperties;

        static ChecklistFactory()
        {
            SourceProperties = typeof(ChecklistDeserializationDto.Result)
                               .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                               .Where(prop => prop.Name != nameof(ChecklistDeserializationDto.Result.Children))
                               .ToList();

            TargetProperties = typeof(Result)
                               .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                               .Where(prop => prop.Name != nameof(Result.Children))
                               .ToDictionary(x => x.Name, x => x);
        }

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
            //foreach (var sourceProp in SourceProperties)
            //{
            //    string indentation = "".PadLeft(depth * 2);
            //    Console.WriteLine($"{indentation}Trying to map source property {sourceProp.Name}...");
            //    if (!TargetProperties.TryGetValue(sourceProp.Name, out var targetProp))
            //        continue;

            //    var value = sourceProp.GetValue(dto);
            //    Console.WriteLine($"{indentation}...to target property {targetProp.Name} with value {value}.");
            //    SetPropertyValueViaBackingField(typeof(Result), targetProp.Name, targetInstance, value); // does not seem to work with inherited properties
            //}
            SetPropertyValueViaBackingField(targetType, nameof(Result.ConjunctElementCode), targetInstance, dto.ConjunctElementCode);
            SetPropertyValueViaBackingField(targetType, nameof(Result.ElementCode), targetInstance, dto.ElementCode);
            SetPropertyValueViaBackingField(targetType, nameof(Result.Name), targetInstance, dto.Name);
            SetPropertyValueViaBackingField(targetType, nameof(Result.ShortName), targetInstance, dto.ShortName);
            SetPropertyValueViaBackingField(targetType, nameof(Result.InspectorComment), targetInstance, dto.InspectorComment);
            SetPropertyValueViaBackingField(targetType, nameof(Result.FarmerComment), targetInstance, dto.FarmerComment);
            SetPropertyValueViaBackingField(targetType, nameof(Result.Percent), targetInstance, dto.Percent);
            SetPropertyValueViaBackingField(targetType, nameof(Result.Outcome), targetInstance, Parse(dto.Outcome));
            // todo Defect
            // todo DefectSeriousness

            if (dto.Children != null && dto.Children.Any() && targetInstance.Children == null)
            {
                SetPropertyValueViaBackingField(nameof(Result.Children), targetInstance, new SortedList<string, ITreeNode<Result>>());
                foreach (var child in dto.Children)
                    targetInstance.Children.TryAdd(child.Key, ParseResult(child.Value, (Result)targetInstance, ++depth));
            }


            SetPropertyValueViaBackingField(nameof(Result.Parent), targetInstance, parent);
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

    }
}
