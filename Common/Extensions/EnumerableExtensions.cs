using System;
using System.Collections.Generic;
using System.Linq;

namespace Codell.Pies.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static T FirstOrEmpty<T>(this IEnumerable<T> enumerable) where T : class, new()
        {
            var source = enumerable.ToList();
            return source.IsEmpty() ? new T() : source.FirstOrDefault() ?? new T();
        }

        public static T FirstOrEmpty<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) where T : class, new()
        {
            var source = enumerable.ToList();
            return source.IsEmpty() ? new T() : source.FirstOrDefault(predicate) ?? new T();
        }

        public static IEnumerable<T> Safe<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? new List<T>();
        }

        public static int SafeCount<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.IsEmpty() ? 0 : enumerable.Count();
        }

        public static void AddIfNotNull<T>(this List<T> list, T item) where T : class
        {
            if (item != null)
            {
                list.Add(item);
            }
        }
    }
}