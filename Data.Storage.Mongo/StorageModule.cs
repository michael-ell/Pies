using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Codell.Pies.Common;
using Codell.Pies.Common.Extensions;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Data.Storage.Configuration;
using Codell.Pies.Data.Storage.Mongo.Schema;
using EventStore;
using EventStore.Serialization;
using MongoDB.Bson.Serialization;
using Ncqrs.Domain;
using Ncqrs.Eventing;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace Codell.Pies.Data.Storage.Mongo
{
    public class StorageModule : Module
    {
        private static bool _isLoaded;

        protected override void Load(ContainerBuilder builder)
        {
            if (_isLoaded) return;

            base.Load(builder);
            builder.Register(StoreEventsProvider.CreateInstance).As<IStoreEvents>().SingleInstance();
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>();
            builder.RegisterType<EventStore>().As<IEventStore>();
            builder.RegisterType<CollectionFactory>().As<ICollectionFactory>();
            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<Migrator>().As<IMigrator>();
            builder.RegisterType<Bootstrapper>().As<IBootstrapper>();
            RegisterCollectionNameMaps(builder);
            ConfigureSerialization();
            _isLoaded = true;
        }

        private static class StoreEventsProvider
        {
            public static IStoreEvents CreateInstance(IComponentContext context)
            {
                var provider = context.Resolve<IApplicationStorageConfigurationProvider>();
                var store = Wireup.Init()
                                  .LogToOutputWindow()
                                  .UsingMongoPersistence(provider.Configuration.ConnectionStringName, new DocumentObjectSerializer())
                                  .InitializeStorageEngine()
                                  .UsingSynchronousDispatchScheduler(new EventDispatcher(context.Resolve<IEventBus>()))
                                  .Build();
                return store;
            }
        }

        private void RegisterCollectionNameMaps(ContainerBuilder builder)
        {
            var map = new CollectionNameMap().Register<Pie>("Pies")
                                             .Register<Tag>("Tags");
            builder.RegisterInstance(map).As<ICollectionNameMap>();
        }

        private void ConfigureSerialization()
        {
            RegisterClassMaps();
        }

        private void RegisterClassMaps()
        {
            RegisterRootClassMaps();

            //Register all potential types that are serialized to avoid potential deserialization issues when new features are added
            //and to avoid 'Unknown discriminator...' exception when deserializing any polymorphic classes....
            RegisterClassMapsFor(AppDomain.CurrentDomain.GetProjectTypesImplementing(typeof(SourcedEvent)));
        }

        private void RegisterRootClassMaps()
        {
           
            RegisterClassMap<Pie>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(model => model.Id));
                cm.SetIgnoreExtraElements(true);
            });

            RegisterClassMap<Event>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });

            RegisterClassMap<SourcedEvent>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });

            RegisterClassMap<Migrator.VersionInfo>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(info => info.Version));
                cm.SetIgnoreExtraElements(true);
            });
        }

        private void RegisterClassMap<T>(Action<BsonClassMap<T>> classMapInitializer)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof (T)))
            {
                BsonClassMap.RegisterClassMap(classMapInitializer);
            }
        }

        private void RegisterClassMapsFor(IEnumerable<Type> types)
        {
            foreach (var type in types.Where(type => !BsonClassMap.IsClassMapRegistered(type)))
            {               
                BsonClassMap.RegisterClassMap(new ClassMap(type));
            }
        }

        private class ClassMap : BsonClassMap
        {
            public ClassMap(Type classType) : base(classType)
            {
                AutoMap();

                //Ignore extra / unknown properties when deserializing the entity
                SetIgnoreExtraElements(true);
            }
        }
    }
}