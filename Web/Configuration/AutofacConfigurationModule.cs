using System;
using System.Linq;
using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using Codell.Pies.Common.Extensions;

namespace Codell.Pies.Web.Configuration
{
    public class AutofacConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var moduleTypes = AppDomain.CurrentDomain.GetProjectTypesImplementing(typeof(IModule));
            foreach (var moduleType in moduleTypes.Where(moduleType => moduleType != typeof (AutofacConfigurationModule)))
            {
                builder.RegisterModule(Activator.CreateInstance(moduleType) as IModule);
            }
            builder.RegisterModule(new ConfigurationSettingsReader());
        }
    }
}