using Autofac;
using Codell.Pies.Core.Repositories;
using EventStore;
using Ncqrs.Domain;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;

namespace Codell.Pies.Data.Storage.InMemory
{
    public class StorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(StoreEventsProvider.CreateInstance).As<IStoreEvents>().SingleInstance();
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>();
            builder.RegisterType<EventStore>().As<IEventStore>();
            builder.RegisterType<Repository>().As<IRepository>().SingleInstance();
            builder.RegisterType<DeleteEmptyPies>().As<IDeleteEmptyPies>();
        }

        private static class StoreEventsProvider
        {
            public static IStoreEvents CreateInstance(IComponentContext context)
            {
                var store = Wireup.Init()
                                  .LogToOutputWindow()
                                  .UsingInMemoryPersistence()
                                  .InitializeStorageEngine()
                                  .UsingBinarySerialization()
                                  .UsingSynchronousDispatchScheduler(new EventDispatcher(context.Resolve<IEventBus>()))
                                  .Build();
                return store;
            }
        }
    }
}