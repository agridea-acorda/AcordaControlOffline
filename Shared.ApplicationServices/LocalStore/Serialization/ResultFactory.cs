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
        private static readonly PropertyInfo ParentProperty;
        
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

            ParentProperty = typeof(Result)
                             .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                             .FirstOrDefault(prop => prop.Name != nameof(Result.Parent));
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
                    //targetProp.SetValue(targetInstance, sourceProp.GetValue(dto));
                    var field = typeof(Result).GetField($"<{targetProp.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                    field.SetValue(targetInstance, value);
                }
            }

            foreach (var child in dto.Children)
            {
                ((Result)targetInstance).Children.TryAdd(child.Key, Parse(child.Value, (Result)targetInstance, ++depth));
            }

            //ParentProperty.SetValue(targetInstance, parent);
            var parentField = typeof(Result).GetField($"<{ParentProperty.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            parentField.SetValue(targetInstance, parent);
            return (Result) targetInstance;
        }
    }
}
