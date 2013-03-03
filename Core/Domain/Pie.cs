using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Ncqrs.Domain;

namespace Codell.Pies.Core.Domain
{
    public class Pie : AggregateRootMappedByConvention
    {
        private int _total;

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
            if (percent < 0 || percent > 100)
                throw new BusinessRuleException(Resources.InvalidPercentage);
            if (_total + percent > 100)
            {
                throw new BusinessRuleException(Resources.PieAccountedFor);
            }
            ApplyEvent(new PieSlicedEvent(percent, description));
        }

        protected void OnPieSliced(PieSlicedEvent @event)
        {
            _total += @event.Percent;
        }
    }
}