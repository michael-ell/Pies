using System;
using System.Collections.Generic;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientPercentageUpdatedEvent : SourcedEvent
    {
        public Guid Id { get; private set; }
        public int Percent { get; private set; }
        public IEnumerable<AffectedIngredients> Affected { get; private set; }

        public IngredientPercentageUpdatedEvent(Guid id, int percent) : this(id, percent, new List<AffectedIngredients>())
        {
        }

        public IngredientPercentageUpdatedEvent(Guid id, int percent, IEnumerable<AffectedIngredients> affected)
        {
            Id = id;
            Percent = percent;
            Affected = affected;
        }

        public class AffectedIngredients
        {
            public Guid Id { get; private set; }
            public int Percent { get; private set; }

            public AffectedIngredients(Guid id, int percent)
            {
                Id = id;
                Percent = percent;
            }
        }
    }
}