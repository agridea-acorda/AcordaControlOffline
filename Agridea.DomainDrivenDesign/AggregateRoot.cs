using System.Collections.Generic;

namespace Agridea.DomainDrivenDesign
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> domainEvents_ = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => domainEvents_;

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents_.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            domainEvents_.Clear();
        }
    }
}