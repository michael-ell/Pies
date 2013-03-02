using System;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Core.Cqrs
{
    public interface IRegisterEventHandlers
    {
        void RegisterHandler<TEvent>(IEventHandler<TEvent> handler);
        void RegisterIocResolvedHandler(Type eventDataType, Type handlerServiceType, string handlerImplementationName);
    }
}