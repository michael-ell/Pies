using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Codell.Pies.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string SafeToString(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        public static T Safe<T>(this T obj) where T : class, new()
        {
            return obj ?? new T();
        }

        public static string Hash(this object obj)
        {
            return obj.Hash(ServiceLocator.Instance.Find<ISurrogateSelector>());
        }

        public static string Hash(this object obj, ISurrogateSelector selector)
        {
            if (obj == null) return string.Empty;
            if (obj.GetType().GetCustomAttributes(typeof(SerializableAttribute), true).Length == 0)
            {
                throw new ArgumentException(Resources.CannotHashObject, "obj");
            }

            var md5 = MD5.Create();
            byte[] hash;
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter { SurrogateSelector = selector };
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                hash = md5.ComputeHash(ms);
            }
            var sb = new StringBuilder();
            foreach (var hashByte in hash)
            {
                sb.Append(hashByte.ToString("X2").ToLower());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Determines if one object equals another object by a Hash alogorithm.
        /// </summary>
        /// <param name="obj">Object starting the comparison.</param>
        /// <param name="other">Object to compare against.</param>
        /// <returns>True if hashes equal; otherwise false.</returns>
        /// <remarks>This comparison does NOT use the object Equals method.</remarks>
        public static bool DoesNotEqual(this object obj, object other)
        {
            if (obj == null && other == null) return true;
            if (obj == null || other == null) return false;
            return obj.Hash() != other.Hash();
        }

        public static String ToDescription(this object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var name = obj.ToString();
            var memberInfo = obj.GetType().GetMember(name);

            if (memberInfo.Length == 0)
                return name;

            var description = memberInfo.Single().GetCustomAttributes(typeof(DescriptionAttribute), false);

            return description.Length == 0 ? name : ((DescriptionAttribute)description.Single()).Description;
        }

        public static T ValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException(string.Format(Resources.UnknownDescription, description));
        }

        public static bool DynamicHasProperty(this object obj, string propertyName)
        {
            var dynamic = obj as dynamic;
            if (dynamic == null || propertyName.IsEmpty()) return false;
            return dynamic.GetDynamicMemberNames().Contains(propertyName);
        }
    }
}