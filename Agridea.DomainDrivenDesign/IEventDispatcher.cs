using System.Collections.Generic;

namespace Agridea.DomainDrivenDesign
{
    public interface IEventDispatcher
    {
        void Dispatch(IEnumerable<IDomainEvent> events);
    }
}
