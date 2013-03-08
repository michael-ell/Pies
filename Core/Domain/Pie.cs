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
        private List<Slice> _slices;
        public const int Max = 100;

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

        private int Total { get { return _slices.Sum(slice => slice.Percent);  } }

        private int Remaining { get { return Max - Total; } }

        public void UpdateSlicePercentage(Guid sliceId, int percent)
        {
            if (percent < 0 || percent > Max)
                throw new BusinessRuleException(Resources.InvalidPercentage);
            //if (percent > Remaining)
            //{
            //    throw new BusinessRuleException(Resources.PieAccountedFor);
            //}
            _slices.Single(slice => slice.Id == sliceId).Percent = percent;
            ApplyEvent(new SlicePercentageUpdatedEvent(sliceId, percent));
            if (Remaining > 0)
            {
                AddSlice(Remaining, string.Empty);
            }
        }

        protected void OnSlicePercentageUpdated(SlicePercentageUpdatedEvent @event)
        {            
        }

        private void AddSlice(int percent, string description)
        {
            ApplyEvent(new PieSliceAddedEvent(percent, description, CreateSliceId()));
        }

        protected virtual Guid CreateSliceId()
        {
            return Guid.NewGuid();
        }

        protected void OnPieSliceAdded(PieSliceAddedEvent @event)
        {
            _slices.Add(new Slice(@event.SliceId, @event.Percent, @event.Description));
        }

        private class Slice
        {
            public Slice(Guid id, int percent, string description)
            {
                Id = id;
                Percent = percent;
                Description = description;
            }

            public Guid Id { get; set; }
            public int Percent { get; set; }
            public string Description { get; set; }
        }
    }
}