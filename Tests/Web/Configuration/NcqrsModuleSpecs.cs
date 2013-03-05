using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Codell.Pies.Common.Extensions;
using Codell.Pies.Core.Commands;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Tests.Web.Configuration;
using FluentAssertions;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Module = Autofac.Module;

namespace Codell.Pies.Web.Configuration.NcqrsModuleSpecs
{
    [Concern(typeof (NcqrsModule))]
    public class When_setting_up_the_ncqrs_environment : ContextBase
    {
        protected override void When()
        {
            Configure.With<TestableNcqrsModule>();
        }

        [Observation]
        public void Then_all_commands_should_have_registered_command_executors()
        {
            var commandService = NcqrsEnvironment.Get<ICommandService>();
            foreach (var cmdType in typeof(CreatePieCommand).Assembly.GetTypes().Where(t => typeof(ICommand).IsAssignableFrom(t)))
            {
                commandService.IsRegistered(cmdType).Should().BeTrue(string.Format("all command should have executors (missing executor for {0})", cmdType.Name));
            }
        }

        [Observation]
        public void Then_all_event_handlers_in_the_core_should_be_registered_within_the_event_bus()
        {
            var bus = NcqrsEnvironment.Get<IEventBus>();
            var handlerAssemblies = new List<Assembly> {typeof (PieStartedEvent).Assembly};
            var handlerTypes = handlerAssemblies.SelectMany(a => a.GetTypes().SelectMany(type => type.GetImplementationsOf(typeof(IEventHandler<>)))).Distinct();

            foreach (var eventType in handlerTypes)
            {
                var expectedImplcount = handlerAssemblies.Sum(a => a.GetTypes().Count(t => eventType.IsAssignableFrom(t)));
                bus.IsRegistered(eventType, expectedImplcount)
                   .Should()
                   .BeTrue(string.Format("all event handlers should be registered within the bus (expected {0} to be registered {1} time(s))", eventType.UnderlyingSystemType, expectedImplcount));
            }            
        }

        private class TestableNcqrsModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterModule<NcqrsModule>();
                base.Load(builder);
            }
        }
    }
}