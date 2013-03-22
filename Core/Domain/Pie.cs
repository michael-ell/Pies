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
        private List<Ingredient> _ingredients;
        private string _caption;

        public Pie()
        {
            Init();
        }

        public Pie(Guid id) : base(id)
        {
            Init();
            ApplyEvent(new PieCreatedEvent());
        }

        private void Init()
        {
            _ingredients = new List<Ingredient>();
        }

        protected void OnPieCreated(PieCreatedEvent @event)
        {            
        }

        public void UpdateCaption(string caption)
        {
            if (_caption != caption)
            {
                ApplyEvent(new PieCaptionUpdatedEvent(caption));
            }
        }

        protected void OnPieCaptionUpdated(PieCaptionUpdatedEvent @event)
        {
            _caption = @event.Caption;
        }

        public void AddIngredient(string description)
        {
            if (_ingredients.Exists(i => string.Equals(description, i.Description))) return;

            var toAdd = new Ingredient(Guid.NewGuid(), description, _ingredients.Count == 0 ? 100 : 0);
            ApplyEvent(new IngredientAddedEvent(toAdd, _ingredients));
        }

        protected void OnIngredientAdded(IngredientAddedEvent @event)
        {
            _ingredients.Add(@event.IngredientAdded); 
        }

        public void UpdateIngredientPercentage(Guid id, int proposedPercent)
        {
            if (proposedPercent < 0)
            {
                proposedPercent = 0;
            }

            if (IngredientFor(id).Percent == proposedPercent) return;

            var ingredient = IngredientFor(id);
            if (_ingredients.Sum(i => i.Percent) + (proposedPercent - ingredient.Percent) > Max)
            {
                ApplyEvent(new IngredientPercentageRejectedEvent(id, proposedPercent, ingredient.Percent));
            }
            else
            {
                ApplyEvent(new IngredientPercentageUpdatedEvent(id, proposedPercent));
            }
        }

        protected void OnIngredientPercentageUpdated(IngredientPercentageUpdatedEvent @event)
        {
            IngredientFor(@event.Id).Percent = @event.Percent;
        }

        protected void OnIngredientPercentageRejected(IngredientPercentageRejectedEvent @event)
        {            
        }

        public void DeleteIngredient(Guid id)
        {
            if (_ingredients.Exists(i => i.Id == id))
            {
                ApplyEvent(new IngredientDeletedEvent(id));
            }
        }

        protected void OnIngredientDeleted(IngredientDeletedEvent @event)
        {
            _ingredients.Remove(IngredientFor(@event.Id));
        }

        private Ingredient IngredientFor(Guid id)
        {
            return _ingredients.Single(i => i.Id == id);
        }
    }
}