using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Ncqrs.Domain;

namespace Codell.Pies.Core.Domain
{
    public class Pie : AggregateRootMappedByConvention
    {
        private int _totalPercent;

        public Pie()
        {
        }

        public Pie(Guid id) : base(id)
        {
            ApplyEvent(new PieStartedEvent());
        }

        protected void OnPieStarted(PieStartedEvent @event)
        {}

        public void Slice(int percent, string description)
        {
            const int maxPercentage = 100;
            if (percent < 0 || percent > maxPercentage)
                throw new BusinessRuleException(Resources.InvalidPercentage);
            var proposed = _totalPercent + percent;
            if (proposed > maxPercentage)
            {
                throw new BusinessRuleException(Resources.PieAccountedFor);
            }
            ApplyEvent(new PieSlicedEvent(percent, description, maxPercentage - proposed));
        }

        protected void OnPieSliced(PieSlicedEvent @event)
        {
            _totalPercent += @event.Percent;
        }
    }
}