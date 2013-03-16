using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Core.Events;
using Ncqrs.Domain;

namespace Codell.Pies.Core.Domain
{
    public class Pie : AggregateRootMappedByConvention
    {
        public const int Max = 100;

        private List<Slice> _slices;
        private List<Ingredient> _ingredients;

        public IEnumerable<Slice> Slices { get { return _slices; } }

        public Pie()
        {
            Init();
        }

        public Pie(Guid id, string name) : base(id)
        {
            Init();
            ApplyEvent(new PieCreatedEvent(name));
            KeepFull();
        }

        private void Init()
        {
            _slices = new List<Slice>();
            _ingredients = new List<Ingredient>();
        }

        protected void OnPieStarted(PieCreatedEvent @event)
        {            
        }

        private int Total { get { return Slices.Sum(slice => slice.Percent);  } }

        private int Remaining { get { return Max - Total; } }

        public void UpdateSlicePercentage(Guid sliceId, int proposedPercent)
        {
            if (proposedPercent < 0)
                proposedPercent = 0;

            var slice = Slices.Single(s => s.Id == sliceId);           
            if (slice.Percent == proposedPercent) return;

            if (proposedPercent > slice.Percent)
            {
                ApplyEvent(new SlicePercentageRejectedEvent(sliceId, proposedPercent, slice.Percent));
            }
            else
            {
                ApplyEvent(new SlicePercentageUpdatedEvent(sliceId, proposedPercent));
                KeepFull();
            }
        }

        protected void OnSlicePercentageUpdated(SlicePercentageUpdatedEvent @event)
        {
            Slices.Single(s => s.Id == @event.SliceId).Percent = @event.Percent;
        }

        protected void OnSlicePercentageRejected(SlicePercentageRejectedEvent @event)
        {
        }

        public void DeleteSlice(Guid sliceId)
        {
            if (_slices.Exists(slice => slice.Id == sliceId))
            {
                ApplyEvent(new SliceDeletedEvent(sliceId));
                //KeepFull();
            }
        }

        protected void OnSliceDeleted(SliceDeletedEvent @event)
        {
            _slices.Remove(_slices.Single(slice => slice.Id == @event.SliceId));
        }

        private void KeepFull()
        {
            if (Remaining > 0)
            {
                AddSlice(Remaining, string.Empty);
            }
        }

        private void AddSlice(int percent, string description)
        {
            ApplyEvent(new SliceAddedEvent(percent, description, Guid.NewGuid()));
        }

        protected void OnSliceAdded(SliceAddedEvent @event)
        {
            _slices.Add(new Slice(@event.SliceId, @event.Percent, @event.Description));
        }

        public void AddIngredient(string description)
        {
            ApplyEvent(new IngredientAddedEvent(description, _ingredients.Count == 0 ? 100 : 0, Guid.NewGuid()));
        }

        protected void OnIngredientAdded(IngredientAddedEvent @event)
        {
            _ingredients.Add(new Ingredient(@event.Id, @event.Description, @event.Percent)); 
        }

        public void UpdateIngredientPercentage(Guid id, int newPercent)
        {
            if (newPercent < 0)
            {
                newPercent = 0;
            }

            if (newPercent > 100)
            {
                newPercent = 100;
            }

            var change = newPercent - IngredientFor(id).Percent;
            var affected = _ingredients.Where(i => i.Id != id && i.Percent > 0).ToList();
            var split = change/affected.Count;
            affected.ForEach(i => ApplyEvent(new IngredientPercentageUpdatedEvent(i.Id, i.Percent + split)));
            ApplyEvent(new IngredientPercentageUpdatedEvent(id, newPercent));

        }

        protected void OnIngredientPercentageUpdated(IngredientPercentageUpdatedEvent @event)
        {
            IngredientFor(@event.Id).Percent = @event.Percent;
        }

        private Ingredient IngredientFor(Guid id)
        {
            return _ingredients.Single(i => i.Id == id);
        }
    }
}