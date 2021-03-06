﻿using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.Services;
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
        private string _nextColor;
        private IEnumerable<string> _tags;
        private bool _deleted;
        private IUser _owner;

        public Pie()
        {
            Init();
        }

        public Pie(Guid id, IUser owner) : base(id)
        {
            Init();
            ApplyEvent(new PieCreatedEvent(owner, string.Empty, _ingredients, new Ingredient(Guid.NewGuid(), "Filler", Max, _colors.Filler)));
        }

        private void Init()
        {
            _colors = new Colors();
            _ingredients = new List<Ingredient>{new Ingredient(Guid.NewGuid(), string.Empty, 0, _colors.GetNext())};
            _nextColor = _colors.GetNext();
            _tags = new List<string>();
        }

        protected void OnPieCreated(PieCreatedEvent @event)
        {
            _filler = @event.Filler;
            _ingredients = @event.Ingredients.ToList();
            _owner = @event.Owner;
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

        public void AddIngredient(string description, ICleaner cleaner, ISettings settings)
        {
            if (_ingredients.Exists(i => string.Equals(description, i.Description))) return;

            var max = settings.Get<int>(Keys.MaxIngredients);
            if (_ingredients.Count + 1 > max)
            {
                ApplyEvent(new MaxIngredientsReachedEvent(string.Format(Resources.MaxIngredientsReached, max)));
            }
            else
            {
                var result = cleaner.Clean(description);
                var toAdd = new Ingredient(Guid.NewGuid(), result.CleanValue, 0, _nextColor);
                ApplyEvent(new IngredientAddedEvent(toAdd, _ingredients, _filler, result.WasDirty ? Resources.DirtyWordDetected : ""));                
            }
        }

        protected void OnIngredientAdded(IngredientAddedEvent @event)
        {
            _filler.Percent = Max - Total;
            _ingredients.Add(@event.Added);
            _nextColor = _colors.GetNext();
        }

        protected void OnMaxIngredientsReached(MaxIngredientsReachedEvent @event)
        {            
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

        public void UpdateIngredientDescription(Guid id, string newDescription, ICleaner cleaner)
        {
            Ingredient ingredient;
            if (TryToGetIngredient(id, out ingredient))
            {
                if (ingredient.Description != newDescription)
                {
                    var result = cleaner.Clean(newDescription);
                    ApplyEvent(new IngredientDescriptionUpdatedEvent(id, result.CleanValue, _ingredients, _filler, result.WasDirty ? Resources.DirtyWordDetected : ""));
                }
            }
        }

        protected void OnIngredientDescriptionUpdated(IngredientDescriptionUpdatedEvent @event)
        {
            IngredientFor(@event.Id).Description = @event.NewDescription;
        }

        public void UpdateIngredientColor(Guid id, string newColor)
        {
            Ingredient ingredient;
            if (TryToGetIngredient(id, out ingredient))
            {
                if (ingredient.Color != newColor)
                {
                    ApplyEvent(new IngredientColorUpdatedEvent(id, newColor, _ingredients, _filler));
                }
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
            _filler = @event.Filler;
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

        public void UpdateTags(IEnumerable<string> newTags)
        {
            var tags = new Tags(newTags);
            if (_tags.Count() != tags.Count())
            {
                ApplyEvent(new PieTagsUpdatedEvent(tags));
            }
            else
            {
                var changed = tags.Except(_tags);
                if (changed.IsNotEmpty())
                {
                    ApplyEvent(new PieTagsUpdatedEvent(tags));
                }                
            }
        }

        protected void OnPieTagsUpdated(PieTagsUpdatedEvent @event)
        {
            _tags = @event.NewTags;
        }

        public void UpdateIsPrivate(bool isPrivate)
        {
            ApplyEvent(new IsPrivateUpdatedEvent(isPrivate));
        }

        protected void OnIsPiePrivateEvent(IsPrivateUpdatedEvent @event)
        {                
        }

        public void Delete()
        {
            if (!_deleted)
            {
                ApplyEvent(new PieDeletedEvent(_owner));
            }
        }

        protected void OnPieDeleted(PieDeletedEvent @event)
        {
            _deleted = true;
        }
    }
}