using System;
using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class ProposedPercentageChangedEvent : SourcedEvent, IIngredientsUpdatedEvent
    {
        public Guid Id { get; private set; }

        public int ProposedPercent { get; private set; }

        public int AcceptedPercent { get; private set; }

        public IEnumerable<Ingredient> AllIngredients { get; private set; }

        public Ingredient Filler { get; private set; }

        public ProposedPercentageChangedEvent(Guid id, int proposedPercent, int acceptedPercent, IEnumerable<Ingredient> allIngredients, Ingredient filler)
        {
            Id = id;
            ProposedPercent = proposedPercent;
            AcceptedPercent = acceptedPercent;
            AllIngredients = allIngredients;
            Filler = filler;
        }         
    }
}