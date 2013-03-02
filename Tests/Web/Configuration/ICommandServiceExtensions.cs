using System;
using System.Collections.Generic;
using Codell.Pies.Testing.Helpers;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;

namespace Codell.Pies.Tests.Web.Configuration
{
    public static class ICommandServiceExtensions
    {
        public static bool IsRegistered(this ICommandService service, Type commandType)
        {
            if (service is CommandService)
                return IsRegistered((CommandService)service, commandType);
            throw new ArgumentException(string.Format("Unknown bus type {0}, could not verify if event was registered", service.GetType().Name));
        }

        private static bool IsRegistered(CommandService service, Type commandType)
        {
            var value = service.ReflectFieldValue<Dictionary<Type, Action<ICommand>>>("_executors");
            return value.ContainsKey(commandType);
        }            
    }
}