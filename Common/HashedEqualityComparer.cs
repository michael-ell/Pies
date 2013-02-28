using System.Collections;
using System.Runtime.Serialization;
using Codell.Pies.Common.Extensions;

namespace Codell.Pies.Common
{
    public class HashedEqualityComparer : IEqualityComparer
    {
        public bool Equals(object x, object y)
        {
            return Equals(x, y, null);
        }

        public bool Equals(object x, object y, ISurrogateSelector selector)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.Hash(selector) == y.Hash(selector);
        }

        public int GetHashCode(object obj)
        {
            return obj == null ? 0 : obj.GetHashCode();
        }
    }
}