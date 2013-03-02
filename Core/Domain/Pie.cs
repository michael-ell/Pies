using System;
using Codell.Pies.Core.Events;
using Ncqrs.Domain;

namespace Codell.Pies.Core.Domain
{
    public class Pie : AggregateRootMappedByConvention
    {
        public Pie()
        {
        }

        public Pie(Guid id) : base(id)
        {
            ApplyEvent(new PieStartedEvent());
        }

        protected void OnPieStarted(PieStartedEvent @event)
        {}
    }
}