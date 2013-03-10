using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Ncqrs.Domain;

namespace Codell.Pies.Core.Domain
{
    public class Pie : AggregateRootMappedByConvention
    {
        public const int Max = 100;

        private List<Slice> _slices;

        public IEnumerable<Slice> Slices { get { return _slices; } }

        public Pie()
        {
            Init();
        }

        public Pie(Guid id, string name) : base(id)
        {
            Init();
            ApplyEvent(new PieCreatedEvent(name));
            AddSlice(Max, string.Empty);
        }

        private void Init()
        {
            _slices = new List<Slice>();
        }

        protected void OnPieStarted(PieCreatedEvent @event)
        {            
        }

        private int Total { get { return Slices.Sum(slice => slice.Percent);  } }

        private int Remaining { get { return Max - Total; } }

        public void UpdateSlicePercentage(Guid sliceId, int newPercent)
        {
            if (newPercent < 0 || newPercent > Max)
                throw new BusinessRuleException(Resources.InvalidPercentage);
            //if (percent > Remaining)
            //{
            //    throw new BusinessRuleException(Resources.PieAccountedFor);
            //}
            var slice = Slices.Single(s => s.Id == sliceId);
            if (slice.Percent == newPercent) return;
            ApplyEvent(new SlicePercentageUpdatedEvent(sliceId, newPercent));
            if (Remaining > 0)
            {
                AddSlice(Remaining, string.Empty);
            }
        }

        protected void OnSlicePercentageUpdated(SlicePercentageUpdatedEvent @event)
        {
            Slices.Single(s => s.Id == @event.SliceId).Percent = @event.Percent;
        }

        private void AddSlice(int percent, string description)
        {
            ApplyEvent(new SliceAddedEvent(percent, description, Guid.NewGuid()));
        }

        protected void OnSliceAdded(SliceAddedEvent @event)
        {
            _slices.Add(new Slice(@event.SliceId, @event.Percent, @event.Description));
        }
    }
}