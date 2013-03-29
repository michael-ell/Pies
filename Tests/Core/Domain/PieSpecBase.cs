using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.Creators.Domain;
using Codell.Pies.Testing.Ncqrs;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Tests.Core.Domain
{
    public abstract class PieSpecBase : AggregateRootSpecBase<Pie>
    {
        private readonly List<Ingredient> _ingredients;
        protected IReadOnlyList<Ingredient> Ingredients { get { return _ingredients; } }
        protected Ingredient Filler { get; private set; }

        protected PieSpecBase()
        {
            PublishedEventWatcher = OnEventPublished;
            _ingredients = new List<Ingredient>();
        }

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        private void OnEventPublished(IPublishableEvent @event)
        {
            var added = @event.Payload as IngredientAddedEvent;
            if (added != null)
            {
                _ingredients.Add(added.Added);
                Filler = added.Filler;
            }
            else
            {
                var updated = @event.Payload as PercentageUpdatedEvent;
                if (updated != null)
                {
                    Filler = updated.Filler;
                }
            }
        }

        public Ingredient IngredientFor(string description)
        {
            return _ingredients.Single(i => string.Equals(description, i.Description, StringComparison.OrdinalIgnoreCase));
        }
    }
}