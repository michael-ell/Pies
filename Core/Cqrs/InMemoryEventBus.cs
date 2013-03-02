using System;
using Codell.Pies.Common;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Core.Cqrs
{
    public class InMemoryEventBus : InProcessEventBus, IRegisterEventHandlers
    {
         public void RegisterIocResolvedHandler(Type eventDataType, Type handlerServiceType, string handlerImplementationName)
         {
             RegisterHandler(eventDataType, @event =>
                                            {
                                                var handler = ServiceLocator.Instance.Find(handlerServiceType, handlerImplementationName);
                                                var info = handlerServiceType.GetMethod("Handle");
                                                info.Invoke(handler, new[] { @event });                                                  
                                            });
         }
    }
}