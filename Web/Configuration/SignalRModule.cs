using Autofac;
using Codell.Pies.Web.EventHandlers;
using Microsoft.AspNet.SignalR;

namespace Codell.Pies.Web.Configuration
{
    public class SignalRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => GlobalHost.ConnectionManager.GetHubContext<PieHub>()).As<IHubContext>();
        }
    }
}