using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Abstraction;
using EventBus.Events;
using EventBus.Extensions;
using Microsoft.Extensions.Logging;

namespace EventBus.InMemory
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IEventBusSubscriptionsManager _subscriptionsManager;
        private readonly ILogger<InMemoryEventBus> _logger;

        public InMemoryEventBus(IEventBusSubscriptionsManager subscriptionsManager, ILogger<InMemoryEventBus> logger)
        {
            _subscriptionsManager = subscriptionsManager;
            _logger = logger;
        }
        public void Publish(IntegrationEvent @event)
        {
            IEnumerable<InMemoryEventBusSubscriptionsManager.SubscriptionInfo> handlers = new List<InMemoryEventBusSubscriptionsManager.SubscriptionInfo>();
            try
            {
                handlers = _subscriptionsManager.GetHandlersForEvent(@event.GetType().Name);
            }
            catch (Exception ex)
            {
                // ignored
            }

            foreach (var handler in handlers)
            {
                if (!handler.IsDynamic)
                {
                    var initiatedHandler = (IIntegrationEventHandler<IntegrationEvent>)Activator.CreateInstance(handler.HandlerType);
                    initiatedHandler.Handle(@event);
                }
                else
                    throw new NotImplementedException("Dynamic handling not implemented");
            }
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subscriptionsManager.GetEventKey<T>();
            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());
            _subscriptionsManager.AddSubscription<T, TH>();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());
            _subscriptionsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            _subscriptionsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subscriptionsManager.GetEventKey<T>();
            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);
            _subscriptionsManager.RemoveSubscription<T, TH>();
        }
    }
}
