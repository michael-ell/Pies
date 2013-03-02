using System;
using Autofac;
using Codell.Pies.Common;

namespace Codell.Pies.Web.Configuration
{
    public class AutofacServiceLocator : IServiceLocator
    {
        private readonly IContainer _container;

        public AutofacServiceLocator(IContainer container)
        {
            Verify.NotNull(container, "container");
            _container = container;
        }

        public bool TryFind<T>(out T service)
        {
            return _container.TryResolve(out service);
        }

        public T Find<T>()
        {
            return _container.Resolve<T>();
        }

        public object Find(Type type, string name)
        {
            return _container.ResolveNamed(name, type);
        }
    }
}