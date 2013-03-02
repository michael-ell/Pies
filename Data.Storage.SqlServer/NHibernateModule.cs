using System;
using Autofac;
using Codell.Pies.Data.Storage.Configuration;
using NHibernate;
using Module = Autofac.Module;

namespace Codell.Pies.Data.Storage.SqlServer
{
    public class NHibernateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(SessionFactoryProvider.CreateInstance).As<ISessionFactory>().SingleInstance();
            builder.RegisterType<SessionPerOperationContextStore>().As<ISessionStore>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionStore>().GetActiveSession()).As<ISession>();
        }

        private static class SessionFactoryProvider
        {
            public static ISessionFactory CreateInstance(IComponentContext context)
            {
                var appConfigProvider = context.Resolve<IApplicationStorageConfigurationProvider>();
                return SessionFactory.Build(appConfigProvider.Configuration.GetConnectionString());
            }
        }
    }
}