using System;
using System.Linq;
using System.Reflection;
using Codell.Pies.Common.Extensions;
using Codell.Pies.Core.Commands;
using Codell.Pies.Testing.BDD;
using FluentAssertions;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Codell.Pies.Tests.Core.Commands.MappedCommandsSpecs
{
    [Concern(typeof (CommandBase))]
    public class When_using_a_mapped_based_approach_for_executing_commands : ContextBase
    {
        protected override void Given()
        {
            AppDomain.CurrentDomain.Load(typeof (StartPieCommand).Assembly.FullName);
        }

        protected override void When()
        {
            //not applicable
        }

        [Observation]
        public void Then_all_methods_to_be_executed_on_the_aggregate_root_should_exist()
        {
            var methodMap = typeof (MapsToAggregateRootMethodAttribute);
            foreach (var cmdType in AppDomain.CurrentDomain.GetProjectTypesHavingCustomAttribute(methodMap))
            {                
                //Ensure method exists on aggregate root
                var map = cmdType.GetCustomAttributes(methodMap, false).First().As<MapsToAggregateRootMethodAttribute>();
                var method = map.Type.GetMethod(map.MethodName);    
                method.As<object>().Should()
                      .NotBeNull(string.Format("Command '{0}' is mapped to aggregate root method '{1}' that does not exist on '{2}'.",
                                                cmdType.Name, map.MethodName, map.Type.Name));

                //Ensure only one Aggregate Root Id is identified...
                var properties = cmdType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                properties.SingleOrDefault(property => Attribute.IsDefined(property, typeof (AggregateRootIdAttribute)))
                          .As<object>().Should().NotBeNull(string.Format("'{0}' is missing '{1}' on at least one property", cmdType.FullName, typeof(AggregateRootIdAttribute).Name));

                //Ensure all method params can be matched to command properties...
                var parameters = method.GetParameters();
                var nonAggregateRootProperties = properties.Where(property => !Attribute.IsDefined(property, typeof(AggregateRootIdAttribute))).ToList();
                foreach (var parameter in parameters)
                {
                    nonAggregateRootProperties.SingleOrDefault(property => string.Equals(property.Name, parameter.Name, StringComparison.OrdinalIgnoreCase) && 
                                                               property.PropertyType == parameter.ParameterType)
                                              .As<object>().Should()
                                              .NotBeNull((string.Format("could not find a matching property named '{0}' on '{1}' (method '{2}' is expecting a parameter named '{0}')", 
                                                          parameter.Name, cmdType.FullName, method.Name)));
                }
            }
        }

        [Observation]
        public void Then_all_commands_that_create_new_aggregates_should_have_a_matching_constructor_that_can_be_executed()
        {
            var constructorMap = typeof(MapsToAggregateRootConstructorAttribute);
            foreach (var cmdType in AppDomain.CurrentDomain.GetProjectTypesHavingCustomAttribute(constructorMap))
            {
                var map = cmdType.GetCustomAttributes(constructorMap, false).First().As<MapsToAggregateRootConstructorAttribute>();
                var properties = cmdType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                        .Select(pi => pi.PropertyType).ToArray();
                map.Type.GetConstructor(properties)
                   .Should()
                   .NotBeNull(string.Format("Command '{0}' is mapped to aggregate root that does not have a constructor with the types '{1}'",
                                            cmdType.Name, String.Join(", ", properties.Select(pi => pi.Name))));
            }
        }
    }
}