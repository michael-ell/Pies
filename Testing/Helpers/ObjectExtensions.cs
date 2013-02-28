using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Codell.Pies.Testing.Helpers
{
    public static class ObjectExtensions
    {
        public static T ReflectFieldValue<T>(this object instance, string fieldName)
        {
            var type = instance.GetType();
            var fields = GetAllFieldsFor(type);
            var fi = fields.SingleOrDefault(field => field.Name == fieldName);
            if (fi == null) throw new ApplicationException(string.Format("Could not find {0} in {1}", fieldName, type.Name));
            return (T)fi.GetValue(instance);
        }

        private static IEnumerable<FieldInfo> GetAllFieldsFor(Type type)
        {
            if (type == null) return Enumerable.Empty<FieldInfo>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            return type.GetFields(flags).Union(GetAllFieldsFor(type.BaseType));
        }

        public static T ReflectPropertyValue<T>(this object instance, string propertyName)
        {
            var type = instance.GetType();
            var properties = GetAllPropertiesFor(type);
            var pi = properties.SingleOrDefault(property => property.Name == propertyName);
            if (pi == null) throw new ApplicationException(string.Format("Could not find {0} in {1}", propertyName, type.Name));
            return (T)pi.GetValue(instance, null);
        }

        private static IEnumerable<PropertyInfo> GetAllPropertiesFor(Type type)
        {
            if (type == null) return Enumerable.Empty<PropertyInfo>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            return type.GetProperties(flags).Union(GetAllPropertiesFor(type.BaseType));
        }
    }
}