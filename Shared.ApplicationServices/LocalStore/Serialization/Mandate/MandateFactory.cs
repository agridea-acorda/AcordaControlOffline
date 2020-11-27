using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Mandate
{
    public class MandateFactory : IAggregateRootFactory<Domain.Mandate.Mandate>
    {
        public Domain.Mandate.Mandate Parse(string json)
        {
            throw new NotImplementedException();
        }

        public string Serialize(Domain.Mandate.Mandate aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }

    public interface IAggregateRootFactory<T> where T: AggregateRoot
    {
        T Parse(string json);
        string Serialize(T aggregateRoot);
    }

    public abstract class AggregateRootFactoryBase<T> : IAggregateRootFactory<T> where T: AggregateRoot
    {
        public abstract T Parse(string json);

        public abstract string Serialize(T aggregateRoot);

        protected void SetPropertyValueViaBackingField<T, TProp>(string propertyName, T instance, TProp propertyValue)
        {
            SetPropertyValueViaBackingField(typeof(T), propertyName, instance, propertyValue);
        }

        protected void SetPropertyValueViaBackingField(Type targetType, string propertyName, object instance, object propertyValue)
        {
            var backingField = targetType.GetField(BackingField(propertyName), BindingFlags.Instance | BindingFlags.NonPublic);
            if (backingField == null)
                throw new InvalidOperationException($"Failed to extract backing field for property {propertyName} of type {targetType.Name}. Cannot proceed to set value.");

            backingField.SetValue(instance, propertyValue);
        }

        protected string BackingField(string propertyName)
        {
            return $"<{propertyName}>k__BackingField";
        }
    }
}
