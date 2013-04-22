using System.Collections.Generic;
using System.Linq;

namespace Codell.Pies.Testing.Helpers
{
    public static class EnumerableExtensions
    {
         public static bool Matches<T>(this IEnumerable<T> toAssert, IEnumerable<T> expected)
         {
             if (toAssert == null && expected == null) return true;
             if (toAssert == null || expected == null) return false;
             if (toAssert.Count() != expected.Count()) return false;
             return expected.All(item => toAssert.Contains(item));
         }

         public static bool Matches<T>(this IEnumerable<T> toAssert, IEnumerable<T> expected, IEqualityComparer<T> comparer)
         {
             if (toAssert == null && expected == null) return true;
             if (toAssert == null || expected == null) return false;
             if (toAssert.Count() != expected.Count()) return false;
             return expected.All(item => toAssert.Contains(item, comparer));
         }
    }
}