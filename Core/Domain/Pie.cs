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
        private Ingredient _filler;

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
            _filler = new Ingredient(Guid.NewGuid(), "Other", 100);
        }

        protected void OnPieCreated(PieCreatedEvent @event)
        {
        }

        private int Total
        {
            get { return _ingredients.Sum(i => i.Percent); }
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

            //var toAdd = new Ingredient(Guid.NewGuid(), description, _ingredients.Count == 0 ? 100 : 0);
            var toAdd = new Ingredient(Guid.NewGuid(), description, 0);
            ApplyEvent(new IngredientAddedEvent(toAdd, _ingredients, _filler));
        }

        protected void OnIngredientAdded(IngredientAddedEvent @event)
        {
            _filler.Percent = Max - Total;
            _ingredients.Add(@event.Added);
        }

        public void UpdateIngredientPercentage(Guid id, int proposedPercent)
        {
            if (proposedPercent < 0)
            {
                proposedPercent = 0;
            }

            var ingredient = IngredientFor(id);
            if (ingredient.Percent != proposedPercent)
            {
                if (proposedPercent < ingredient.Percent)
                {
                    _filler.Percent = _filler.Percent - (proposedPercent - ingredient.Percent);
                    ApplyEvent(new IngredientPercentageUpdatedEvent(id, proposedPercent, _ingredients, _filler));
                }
                else if (_filler.Percent > 0)
                {
                    var cap = _filler.Percent + ingredient.Percent;
                    var newPercent = proposedPercent < cap ? proposedPercent : cap;
                    _filler.Percent = _filler.Percent - (newPercent - ingredient.Percent);
                    ApplyEvent(new IngredientPercentageUpdatedEvent(id, newPercent, _ingredients, _filler));
                }
                else
                {
                    ApplyEvent(new IngredientPercentageRejectedEvent(id, proposedPercent, ingredient.Percent));
                }
            }
        }

        protected void OnIngredientPercentageUpdated(IngredientPercentageUpdatedEvent @event)
        {
            IngredientFor(@event.Id).Percent = @event.NewPercent;
            _filler = @event.Filler;
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