using System.Runtime.Serialization;
using Autofac;
using Codell.Pies.Core.Repositories;

namespace Codell.Pies.Tests
{
    public class MockServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(new Moq.Mock<IRepository>().Object);
            builder.RegisterInstance(new Moq.Mock<IUnitOfWork>().Object);
            builder.RegisterInstance(new Moq.Mock<ISurrogateSelector>().Object);
        }
    }
}