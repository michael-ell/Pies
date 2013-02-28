using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Codell.Pies.Common.Extensions
{
    public static class TypeExtensions
    {
        private const string ProjectNamespace = "codell.pies";

        public static IEnumerable<Type> GetGenericArgumentsFor(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                       .Single(item => item.IsGenericType && item.GetGenericTypeDefinition() == interfaceType)
                       .GetGenericArguments();
        }

        public static IEnumerable<Type> GetImplementationsOf(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                       .Where(item => item.IsGenericType && item.GetGenericTypeDefinition() == interfaceType);
        }

        public static IEnumerable<Type> GetInterfaces(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                       .Where(item => item.IsGenericType && item.GetGenericTypeDefinition() == interfaceType);
        }

        public static Boolean Implements(this Type type, Type toFind)
        {
            return !type.IsAbstract &&
                   type.IsClass &&
                   (toFind.IsAssignableFrom(type) ||
                   type.GetInterfaces().Any(item => toFind.IsGenericTypeDefinition ? item.IsGenericType && item.GetGenericTypeDefinition() == toFind : item == toFind));
        }

        public static MethodInfo GetGenericMethod(this Type type, String name, Type[] types)
        {
            var methods = type.GetMethods().Where(method => method.Name == name && method.IsGenericMethod).ToList();

            foreach (var method in methods)
            {
                var parameters = method.GetParameters().Select(item => item.ParameterType).ToArray();
                if (parameters.Length != types.Length)
                    continue;

                var match = false;
                for (var i = 0; i < types.Length; i++)
                {
                    var actual = parameters[i];
                    var expected = types[i];

                    if (expected != actual && (!expected.IsGenericType || expected != actual.GetGenericTypeDefinition()))
                        break;

                    match = true;
                }

                if (match)
                    return method;
            }

            return null;
        }

        public static IEnumerable<Type> GetProjectTypesImplementing(this AppDomain domain, Type toFind, Assembly assemblyToExclude)
        {
            return domain.GetAssemblies().Where(a => a.FullName.IndexOf(ProjectNamespace, StringComparison.OrdinalIgnoreCase) != -1 && !Equals(a, assemblyToExclude))
                                         .SelectMany(a => a.ReflectTypes())
                                         .Where(type => type.Implements(toFind));
        }

        public static IEnumerable<Type> GetProjectTypesImplementing(this AppDomain domain, Type toFind)
        {
            return domain.GetAllWestJetTypes().Where(type => type.Implements(toFind));
        }

        public static IEnumerable<Type> GetWestJetTypesHavingCustomAttribute(this AppDomain domain, Type toFind)
        {
            var allWestJetTypes = domain.GetAllWestJetTypes();
            return allWestJetTypes.Where(type => Attribute.IsDefined(type, toFind));
        }

        public static IEnumerable<Type> GetWestJetClassTypesWithinNamespace(this AppDomain domain, string ns)
        {
            return domain.GetAllWestJetTypes().Where(type => string.Equals(type.Namespace, ns, StringComparison.OrdinalIgnoreCase) && type.IsClass);
        }

        public static IEnumerable<Type> GetAllWestJetTypes(this AppDomain domain)
        {
            return domain.GetAssemblies()
                         .Where(a => a.FullName.IndexOf(ProjectNamespace, StringComparison.OrdinalIgnoreCase) != -1)
                         .SelectMany(a => a.ReflectTypes());
        }

        public static IEnumerable<Type> GetTypesImplementing(this Assembly assembly, Type toFind)
        {
            return assembly.ReflectTypes().Where(type => type.Implements(toFind));
        }

        public static IEnumerable<Type> ReflectTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                var msg = string.Empty;
                msg = e.LoaderExceptions.Aggregate(msg, (current, loaderException) => current + loaderException.Message + " | ");
                throw new Exception(string.Format(Resources.ReflectionTypeLoaderError, msg));
            }         
        }
    }
}
