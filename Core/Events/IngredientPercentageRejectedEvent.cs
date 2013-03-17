using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class IngredientPercentageRejectedEvent : SourcedEvent
    {
        public Guid Id { get; private set; }

        public int RejectedPercent { get; private set; }

        public int CurrentPercent { get; private set; }

        public IngredientPercentageRejectedEvent(Guid id, int rejectedPercent, int currentPercent)
        {
            Id = id;
            RejectedPercent = rejectedPercent;
            CurrentPercent = currentPercent;
        }
    }
}