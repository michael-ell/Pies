using System;
using System.Linq;
using Autofac;
using Codell.Pies.Common.Extensions;
using Codell.Pies.Core.CommandExecutors;
using Codell.Pies.Core.Cqrs;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Core.Configuration
{
    public class NcqrsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterEventHandlers(builder);
            builder.Register(EventBusProvider.CreateInstance).As<IEventBus>();
            builder.Register(CommandServiceProvider.CreateInstance).As<ICommandService>();
        }

        private void RegisterEventHandlers(ContainerBuilder builder)
        {
            var implementationTypes = AppDomain.CurrentDomain.GetProjectTypesImplementing(typeof(IEventHandler<>));
            foreach (var implementationType in implementationTypes)
            {
                foreach (var serviceType in implementationType.GetImplementationsOf(typeof(IEventHandler<>)))
                {
                    builder.RegisterType(implementationType).Named(implementationType.Name, serviceType);
                }
            }
        }

        private static class EventBusProvider
        {
            public static InMemoryEventBus CreateInstance(IComponentContext context)
            {
                var bus = new InMemoryEventBus();
                //var implementationTypes = typeof(BookingStartedEvent).Assembly.GetTypesImplementing(typeof(IEventHandler<>));
                var implementationTypes = AppDomain.CurrentDomain.GetProjectTypesImplementing(typeof(IEventHandler<>));

                foreach (var implementationType in implementationTypes)
                {
                    foreach (var serviceType in implementationType.GetImplementationsOf(typeof(IEventHandler<>)))
                    {
                        var eventDataType = serviceType.GetGenericArguments().Single();
                        bus.RegisterIocResolvedHandler(eventDataType, serviceType, implementationType.Name);
                    }
                }
                return bus;
            }
        }

        private static class CommandServiceProvider
        {
            public static ICommandService CreateInstance(IComponentContext context)
            {
                var service = new CommandService();
                RegisterCommandExecutors(context, service);
                RegisterMappedCommandsByConstructor(service);
                RegisterMappedCommandsByMethod(service);
                return service;
            }

            private static void RegisterCommandExecutors(IComponentContext context, CommandService service)
            {
                var exclude = typeof(AggregateRootExistsUoWMappedCommandExecutor);
                var executorTypes = typeof(AggregateRootExistsUoWMappedCommandExecutor).Assembly.GetTypes().Where(type => type.Implements(typeof(ICommandExecutor<>)) && type != exclude);
                var registerMethodDefinition = service.GetType().GetGenericMethod("RegisterExecutor", new[] { typeof(ICommandExecutor<>) });
                foreach (var executorType in executorTypes)
                {
                    var executor = context.CreateInstance(executorType);
                    var genericTypeParameter = executorType.GetGenericArgumentsFor(typeof(ICommandExecutor<>)).Single();
                    var genericRegisterMethod = registerMethodDefinition.MakeGenericMethod(genericTypeParameter);

                    genericRegisterMethod.Invoke(service, new[] { executor });
                }
            }

            private static void RegisterMappedCommandsByConstructor(CommandService service)
            {
                var mappedCommands =
                    typeof(AggregateRootExistsUoWMappedCommandExecutor).Assembly.GetTypes().Where(
                        type => Attribute.IsDefined(type, typeof(MapsToAggregateRootConstructorAttribute)));
                var mapper = new AttributeBasedCommandMapper();
                foreach (var commandType in mappedCommands)
                {
                    service.RegisterExecutor(commandType, new AggregateRootExistsUoWMappedCommandExecutor(mapper));
                }
            }

            private static void RegisterMappedCommandsByMethod(CommandService service)
            {
                var mappedCommands =
                    typeof(AggregateRootExistsUoWMappedCommandExecutor).Assembly.GetTypes().Where(
                        type => Attribute.IsDefined(type, typeof(MapsToAggregateRootMethodAttribute)));
                var mapper = new AttributeBasedCommandMapper();
                foreach (var commandType in mappedCommands)
                {
                    service.RegisterExecutor(commandType, new AggregateRootExistsUoWMappedCommandExecutor(mapper));
                }
            }
        }
    }

    internal static class AutofacContextExtensions
    {
        public static Object CreateInstance(this IComponentContext context, Type type)
        {
            var constructor = type.GetConstructors().OrderBy(ctor => ctor.GetParameters().Count()).First();
            var parameterTypes = constructor.GetParameters().Select(parameter => parameter.ParameterType);

            return Activator.CreateInstance(type, parameterTypes.Select(parameterType => context.Resolve(parameterType)).ToArray());
        }
    }
}