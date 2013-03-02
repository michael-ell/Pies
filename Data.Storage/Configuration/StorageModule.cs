using System;
using Autofac;
using Autofac.Core;
using Codell.Pies.Common;

namespace Codell.Pies.Data.Storage.Configuration
{
    public class StorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterApplicationStorage(builder);
        }

        private void RegisterApplicationStorage(ContainerBuilder builder)
        {
            RegisterStorage<IApplicationStorageConfigurationProvider>(builder, new ApplicationStorageConfigurationProvider());
        }

        private void RegisterStorage<TService>(ContainerBuilder builder, StorageConfigurationProviderBase provider)
        {
            builder.RegisterInstance(provider).As<TService>();

            if (provider.ModuleConfiguration.TypeName.IsEmpty()) return;

            var storageType = Type.GetType(provider.ModuleConfiguration.TypeName);
            if (storageType == null) return;

            var storageModule = (IModule)Activator.CreateInstance(storageType);
            builder.RegisterModule(storageModule);
        }         
    }
}