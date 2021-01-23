using System;
using System.Reflection;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization
{
    public abstract class AggregateRootFactoryBase<T> : IAggregateRootFactory<T> where T: AggregateRoot
    {
        public abstract T Parse(string json);

        public abstract string Serialize(T aggregateRoot);

        //protected void SetPropertyValueViaBackingField<TInstance, TProp>(string propertyName, TInstance instance, TProp propertyValue)
        //{
        //    SetPropertyValueViaBackingField(typeof(TInstance), propertyName, instance, propertyValue);
        //}

        protected void SetPropertyValueViaBackingField(Type targetType, string propertyName, object instance, object propertyValue)
        {
            var backingField = FindBackingFieldInType(targetType, propertyName);
            if (backingField == null)
                throw new InvalidOperationException($"Failed to extract backing field for property {propertyName} of type {targetType.Name}. Cannot proceed to set value.");

            backingField.SetValue(instance, propertyValue);
        }

        protected string BackingField(string propertyName)
        {
            return $"<{propertyName}>k__BackingField";
        }

        protected FieldInfo FindBackingFieldInType(Type type, string propertyName)
        {
            var backingField = type.GetField(BackingField(propertyName), BindingFlags.Instance | BindingFlags.NonPublic);
            if (backingField != null)
                return backingField;
            
            return type.BaseType != null ? FindBackingFieldInType(type.BaseType, propertyName) : null;
        }
    }
}