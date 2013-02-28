using Autofac;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Data.Storage.Configuration;
using EventStore;
using Ncqrs.Domain;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;

namespace Codell.Pies.Data.Storage.SqlServer
{
    public class StorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<NHibernateModule>();
            builder.Register(StoreEventsProvider.CreateInstance).As<IStoreEvents>().SingleInstance();
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>();
            builder.RegisterType<EventStore>().As<IEventStore>();
            builder.RegisterType<Repository>().As<IRepository>();
        }    
           
        private static class StoreEventsProvider
        {
            public static IStoreEvents CreateInstance(IComponentContext context)
            {
                var provider = context.Resolve<IApplicationStorageConfigurationProvider>();              
                var store = Wireup.Init()     
                                  .LogToOutputWindow()
                                  .UsingSqlPersistence(provider.Configuration.ConnectionStringName) 
                                  .InitializeStorageEngine()         
                                  .UsingBinarySerialization()
                                  .UsingSynchronousDispatchScheduler(new EventDispatcher(context.Resolve<IEventBus>()))
                                  .Build();
                return store;
            }
        }
    }
}