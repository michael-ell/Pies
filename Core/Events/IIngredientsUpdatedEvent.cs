using System.Collections.Generic;
using Codell.Pies.Core.Domain;

namespace Codell.Pies.Core.Events
{
    public interface IIngredientsUpdatedEvent
    {
        IEnumerable<Ingredient> AllIngredients { get; }
        Ingredient Filler { get; }
    }
}