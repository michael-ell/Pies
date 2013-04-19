using Autofac;
using Codell.Pies.Common.Mapping;

namespace Codell.Pies.Web.Configuration
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => AutoMapperEngine.Configure(new AutoMapperConfiguration())).SingleInstance();
        }
    }
}