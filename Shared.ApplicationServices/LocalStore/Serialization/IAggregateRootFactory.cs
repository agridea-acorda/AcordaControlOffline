using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization
{
    public interface IAggregateRootFactory<T> where T: AggregateRoot
    {
        T Parse(string json);
        string Serialize(T aggregateRoot);
    }
}