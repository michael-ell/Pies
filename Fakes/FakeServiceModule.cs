using Autofac;
using Ncqrs.Commanding.ServiceModel;

namespace Codell.Pies.Fakes
{
    public class FakeServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<FakeCommandService>().As<ICommandService>();
        }       
    }
}