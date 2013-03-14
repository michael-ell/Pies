using Autofac;
using Codell.Pies.Common.Mapping;
using Codell.Pies.Web.EventHandlers;
using Microsoft.AspNet.SignalR;

namespace Codell.Pies.Web.Configuration
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => AutoMapperEngine.Configure(new AutoMapperConfiguration())).SingleInstance();
            builder.Register(c => GlobalHost.ConnectionManager.GetHubContext<PieHub>()).As<IHubContext>();
        }
    }
}