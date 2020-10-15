using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Agridea.DomainDrivenDesign
{
    public sealed class EventDispatcher
    {
        private static List<Type> _handlers;

        public EventDispatcher(Assembly handlerContainer)
        {
            Guard.Against.Null(handlerContainer, nameof(handlerContainer));

            _handlers = handlerContainer
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>)))
                .ToList();
        }

        public void Dispatch(IEnumerable<IDomainEvent> events)
        {
            foreach (IDomainEvent ev in events)
            {
                Dispatch(ev);
            }
        }

        private void Dispatch(IDomainEvent domainEvent)
        {
            foreach (Type handlerType in _handlers)
            {
                bool canHandleEvent = handlerType.GetInterfaces()
                    .Any(x => x.IsGenericType
                              && x.GetGenericTypeDefinition() == typeof(IHandler<>)
                              && x.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandleEvent)
                {
                    dynamic handler = Activator.CreateInstance(handlerType);
                    handler.Handle((dynamic)domainEvent);
                }
            }
        }
    }
}
