using Autofac;
using Codell.Pies.Web.Models.Providers;

namespace Codell.Pies.Web.Configuration
{
    public class ProvidersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<PiesProvider>().As<IPiesProvider>();
            builder.RegisterType<PageProvider>().As<IPageProvider>();
        }
    }
}