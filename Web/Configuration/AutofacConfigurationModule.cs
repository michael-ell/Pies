using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Codell.Pies.Data.Storage.Configuration;
using Module = Autofac.Module;

namespace Codell.Pies.Web.Configuration
{
    public class AutofacConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<NcqrsModule>();
            builder.RegisterModule<StorageModule>();
            builder.RegisterModule(new ConfigurationSettingsReader());
        }
    }
}