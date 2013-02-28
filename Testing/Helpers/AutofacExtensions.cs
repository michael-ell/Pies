using System;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace Codell.Pies.Testing.Helpers
{
    public static class AutofacExtensions
    {
         public static void Verify(this IContainer container, params Type[] excluding)
         {
             excluding = excluding ?? new Type[0];
             foreach (var registration in container.ComponentRegistry.Registrations)
             {
                 foreach (var service in registration.Services.OfType<TypedService>())
                 {
                     try
                     {
                         if (!excluding.Contains(service.ServiceType))
                         {
                             container.Resolve(service.ServiceType);
                         }
                     }
                     catch (Exception e)
                     {
                         throw new Exception(string.Format("Failed to create service {0} from {1}\n\t {2}", service.Description, registration.Activator, e.Message));
                     }
                 }
             }             
         }

    }
}