﻿using System;
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
        private Colors _colors;

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
            _filler = new Ingredient(Guid.NewGuid(), "Filler", Max);
            _colors = new Colors();
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
            _caption = @event.NewCaption;
        }

        public void AddIngredient(string description)
        {
            if (_ingredients.Exists(i => string.Equals(description, i.Description))) return;

            var toAdd = new Ingredient(Guid.NewGuid(), description, 0);
            ApplyEvent(new IngredientAddedEvent(toAdd, _ingredients, _filler));
        }

        protected void OnIngredientAdded(IngredientAddedEvent @event)
        {
            _filler.Percent = Max - Total;
            @event.Added.Color = _colors.Next();
            _ingredients.Add(@event.Added);
        }

        public void UpdateIngredientPercentage(Guid id, int proposedPercent)
        {
            if (proposedPercent < 0)
            {
                proposedPercent = 0;
            }

            Ingredient ingredient;
            if (!TryToGetIngredient(id, out ingredient)) return;

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
                    if (newPercent != proposedPercent)
                    {
                        ApplyEvent(new ProposedIngredientPercentageChangedEvent(id,  proposedPercent, newPercent, _ingredients, _filler));
                    }      
                    ApplyEvent(new IngredientPercentageUpdatedEvent(id, newPercent, _ingredients, _filler));
                }
                else
                {
                    ApplyEvent(new PercentageRejectedEvent(id, proposedPercent, ingredient.Percent));
                }
            }
        }

        protected void OnIngredientPercentageUpdated(IngredientPercentageUpdatedEvent @event)
        {
            IngredientFor(@event.Id).Percent = @event.NewPercent;
            _filler = @event.Filler;
        }

        protected void OnProposedPercentageChanged(ProposedIngredientPercentageChangedEvent @event)
        {
            IngredientFor(@event.Id).Percent = @event.AcceptedPercent;
            _filler = @event.Filler;
        }

        protected void OnIngredientPercentageRejected(PercentageRejectedEvent @event)
        {
        }

        public void UpdateIngredientColor(Guid id, string newColor)
        {
            Ingredient ingredient;
            if (TryToGetIngredient(id, out ingredient))
            {
                ApplyEvent(new IngredientColorUpdatedEvent(id, newColor, _ingredients, _filler));
            }
        }

        protected void OnIngredientColorUpdated(IngredientColorUpdatedEvent @event)
        {
            IngredientFor(@event.Id).Color = @event.NewColor;
        }

        public void DeleteIngredient(Guid id)
        {
            Ingredient toDelete;
            if (!TryToGetIngredient(id, out toDelete)) return;

            _filler.Percent += toDelete.Percent ;
            ApplyEvent(new IngredientDeletedEvent(toDelete, _ingredients, _filler));
        }

        protected void OnIngredientDeleted(IngredientDeletedEvent @event)
        {
            _ingredients.Remove(IngredientFor(@event.Deleted.Id));
        }

        private bool TryToGetIngredient(Guid id, out Ingredient ingredient)
        {
            ingredient = _ingredients.SingleOrDefault(i => i.Id == id);
            return ingredient != null;
        }

        private Ingredient IngredientFor(Guid id)
        {
            return _ingredients.Single(i => i.Id == id);
        }
    }
}