using System.Collections.Generic;
using Codell.Pies.Core.Domain;

namespace Codell.Pies.Tests.Core.Domain
{
    public class IngredientEqualityComparer : IEqualityComparer<Ingredient>
    {
        public bool Equals(Ingredient x, Ingredient y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return Equals(x.Id, y.Id) && Equals(x.Description, y.Description) && Equals(x.Percent, y.Percent);
        }

        public int GetHashCode(Ingredient obj)
        {
            if (obj == null) return 0;
            unchecked
            {
                var result = obj.Id.GetHashCode();
                result = (result * 397) ^ (obj.Description != null ? obj.Description.GetHashCode() : 0);
                result = (result * 397) ^ obj.Percent;
                return result;
            }
        }
    }
}