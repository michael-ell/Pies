using System;
using System.Collections.Generic;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieCreatedEvent : SourcedEvent, IIngredientsUpdatedEvent
    {
        public IUser User { get; set; }

        public string Caption { get; private set; }

        public IEnumerable<Ingredient> Ingredients { get; private set; }

        public Ingredient Filler { get; private set; }

        public PieCreatedEvent(IUser user, string caption, IEnumerable<Ingredient> ingredients, Ingredient filler)
        {
            User = user;
            Caption = caption;
            Ingredients = ingredients;
            Filler = filler;
        }
    }
}