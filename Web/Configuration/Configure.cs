using Autofac;
using Autofac.Core;
using Codell.Pies.Common;
using Ncqrs;
using Ncqrs.Config;

namespace Codell.Pies.Web.Configuration
{
    public class Configure : IEnvironmentConfiguration
    {
        public static IContainer With<TModule>() where TModule : IModule, new()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TModule>();
            var container = builder.Build();
            ServiceLocator.RegisterInstance(new AutofacServiceLocator(container));
            if (!NcqrsEnvironment.IsConfigured)
                NcqrsEnvironment.Configure(new Configure(container));
            return container;
        }

        private readonly IContainer _container;

        public Configure(IContainer container)
        {
            Verify.NotNull(container, "container");
            _container = container;
        }

        public bool TryGet<T>(out T result) where T : class
        {
            result = _container.ResolveOptional<T>();
            return result != null;
        }         
    }
}