using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Primitives;

namespace Codell.Pies.Testing.Helpers
{
    public static class FluentAssertionExtensions
    {
         public static void Match<T>(this GenericCollectionAssertions<T> assertion, IEnumerable<T> expectedValue, IEqualityComparer<T> comparer)
         {
             if (expectedValue == null)
             {
                 assertion.Subject.Should().BeNull();
             }
             else
             {
                 assertion.Subject.Should().NotBeNull().And.HaveCount(expectedValue.Count());
                 foreach (var actualItem in assertion.Subject)
                 {
                     expectedValue.Contains(actualItem, comparer).Should().BeTrue();
                 }
             }
         }

         public static void Match<T>(this ObjectAssertions assertion, T expectedValue, IEqualityComparer<T> comparer) where T : class
         {
             if (expectedValue == null)
             {
                 assertion.Subject.Should().BeNull();
             }
             else
             {
                 comparer.Equals(expectedValue, (T) assertion.Subject).Should().BeTrue();
             }
         }
    }
}