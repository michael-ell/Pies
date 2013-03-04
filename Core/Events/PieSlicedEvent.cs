using System;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieSlicedEvent : SourcedEvent
    {
        public int Percent { get; private set; }

        public string Description { get; private set; }

        public int RemainingPercent { get; private set; }

        public PieSlicedEvent(int percent, string description, int remainingPercent)
        {
            Percent = percent;
            Description = description;
            RemainingPercent = remainingPercent;
        }
    }
}