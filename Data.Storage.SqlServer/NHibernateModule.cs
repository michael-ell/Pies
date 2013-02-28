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
                var connectionString = ResolveConnectionStringFrom(context);
                return SessionFactory.Build(connectionString);
            }

            private static string ResolveConnectionStringFrom(IComponentContext context)
            {
                var appConfigProvider = context.Resolve<IApplicationStorageConfigurationProvider>();
                var auditConfigProvider = context.Resolve<IAuditStorageConfigurationProvider>();
                if (UsesSqlServer(appConfigProvider) && 
                    appConfigProvider.Configuration.ConnectionStringName != auditConfigProvider.Configuration.ConnectionStringName)
                {
                    //The NHibnerate session factory (and the app) currently cannot support connections to 2 different databases...
                    throw new NotSupportedException("Both application and audit storage must be in the same sql server database.");
                }                
                return auditConfigProvider.Configuration.GetConnectionString();
            }

            private static bool UsesSqlServer(IApplicationStorageConfigurationProvider provider)
            {
                return provider.ModuleConfiguration.TypeName.Contains(typeof(StorageModule).FullName);
            }
        }
    }
}