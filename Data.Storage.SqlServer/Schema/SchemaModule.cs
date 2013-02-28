using Autofac;
using Codell.Pies.Common;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public class SchemaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Bootstrapper>().As<IBootstrapper>();
        }
    }
}