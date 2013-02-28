using Autofac;
using Autofac.Core;
using Codell.Pies.Common;

namespace Codell.Pies.Testing.Helpers
{
    public static class AutofacContainer
    {
         public static IContainer Setup(params IModule[] modules)
         {
             var builder = new ContainerBuilder();
             if (modules.IsNotEmpty())
             {
                 foreach (var module in modules)
                 {
                     builder.RegisterModule(module);
                 }
             }
             return builder.Build();
         }
    }
}