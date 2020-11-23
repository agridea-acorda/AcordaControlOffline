using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization
{
    public class ResultFactory
    {
        private static readonly List<PropertyInfo> SourceProperties;
        private static readonly Dictionary<string, PropertyInfo> TargetProperties;
        
        static ResultFactory()
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

        public static Result Parse(ChecklistDeserializationDto.Result dto, Result parent = null, int depth = 0)
        {
            var targetType = depth == 0 ? typeof(RubricResult) :
                             dto.Children.Any() ? typeof(GroupResult) :
                             typeof(PointResult);
            var targetInstance = FormatterServices.GetUninitializedObject(targetType);
            foreach (var sourceProp in SourceProperties)
            {
                Console.WriteLine($"Trying to map source property {sourceProp.Name}...");
                if (TargetProperties.TryGetValue(sourceProp.Name, out var targetProp))
                {
                    var value = sourceProp.GetValue(dto);
                    Console.WriteLine($"... to target property {targetProp.Name} with value {value}.");
                    var field = typeof(Result).GetField($"<{targetProp.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                    field.SetValue(targetInstance, value);
                }
            }

            var target = (Result)targetInstance;
            if (dto.Children.Any() && target.Children == null)
            {
                var childrenField = typeof(Result).GetField($"<{nameof(Result.Children)}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                childrenField.SetValue(target, new SortedList<string, ITreeNode<Result>>());
            }
            foreach (var child in dto.Children)
            {
                target.Children.TryAdd(child.Key, Parse(child.Value, (Result)targetInstance, ++depth));
            }

            var parentField = typeof(Result).GetField($"<{nameof(Result.Parent)}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            parentField.SetValue(targetInstance, parent);
            return (Result) targetInstance;
        }
    }
}
