using System.Collections.Generic;
using MediatR;

namespace Agridea.DomainDrivenDesign
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IMediator _mediator;

        public EventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void Dispatch(IEnumerable<IDomainEvent> events)
        {
            foreach (var ev in events) await _mediator.Publish(ev);
        }
    }
}
