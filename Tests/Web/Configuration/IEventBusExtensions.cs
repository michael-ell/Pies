using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Testing.Helpers;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Tests.Web.Configuration
{
    public static class IEventBusExtensions
    {
        public static bool IsRegistered(this IEventBus bus, Type eventType, int expectedImplcount)
        {
            if (bus is InProcessEventBus)
                return IsRegistered((InProcessEventBus)bus, eventType, expectedImplcount);
            throw new ArgumentException(string.Format("Unknown bus type {0}, could not verify if event was registered", bus.GetType().Name));
        }

        private static bool IsRegistered(InProcessEventBus bus, Type eventType, int expectedImplCount)
        {
            var value = bus.ReflectFieldValue<Dictionary<Type, List<Action<PublishedEvent>>>>("_handlerRegister");
            var keyType = eventType.GetGenericArguments()[0];
            return value.Count(v => v.Key == keyType && v.Value.Count == expectedImplCount) == 1;
        }
    }
}