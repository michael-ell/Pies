using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Codell.Pies.Common
{
    public static class Is
    {
        public static bool IsBetween(this string value, int minimum, int maximum)
        {
            return ((minimum.CompareTo(value.Length) <= 0) && (maximum.CompareTo(value.Length) >= 0));

        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
        }

        public static bool IsNotEmpty(this string value)
        {
            return !IsEmpty(value);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            return value == null || !value.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> value)
        {
            return !IsEmpty(value);
        }

        public static bool IsEmpty(this IEnumerable value)
        {
            if (value == null) return true;
            var enumerator = value.GetEnumerator();
            return !enumerator.MoveNext();
        }

        public static bool IsNotEmpty(this IEnumerable value)
        {
            return !IsEmpty(value);
        }
    }
}
