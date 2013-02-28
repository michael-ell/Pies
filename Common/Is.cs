using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Codell.Pies.Common
{
    /// <summary>
    /// A fluent interface for helper methods on strings and lists of objects
    /// </summary>
    public static class Is
    {
        /// <summary>
        /// Determines if the specified value is empty
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the value is empty</returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
        }

        /// <summary>
        /// Determines if the specified value is not empty
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the value is not empty</returns>
        public static bool IsNotEmpty(this string value)
        {
            return !IsEmpty(value);
        }

        /// <summary>
        /// Determines if the specified value is not empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            return value == null || !value.Any();
        }

        /// <summary>
        /// Determines if the the Enumerable is not empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>True if the enumerable is empty</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> value)
        {
            return !IsEmpty(value);
        }

        /// <summary>
        /// Determines if the Enumerable is empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the enumerable is empty</returns>
        public static bool IsEmpty(this IEnumerable value)
        {
            if (value == null) return true;
            IEnumerator enumerator = value.GetEnumerator();
            return !enumerator.MoveNext();
        }

        /// <summary>
        /// Determines if the enumerable is not empty
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the enumerable is not empty</returns>
        public static bool IsNotEmpty(this IEnumerable value)
        {
            return !IsEmpty(value);
        }
    }
}