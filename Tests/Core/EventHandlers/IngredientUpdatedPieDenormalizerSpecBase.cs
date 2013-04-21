using System;
using System.Collections.Generic;
using AutoMapper;
using Codell.Pies.Core.EventHandlers;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Testing.Creators.ReadModels;
using Codell.Pies.Testing.Ncqrs;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Tests.Core.EventHandlers.PieDenormalizerSpecs
{
    public abstract class IngredientUpdatedPieDenormalizerSpecBase<TEvent> : EventHandlerSpecBase<PieDenormalizer> where TEvent :  SourcedEvent, IIngredientsUpdatedEvent
    {
        protected abstract TEvent GetEvent();

        protected PublishedEvent<TEvent> PublishedIngredientUpdatedEvent { get; private set; }

        protected Pie Pie { private set; get; }

        protected List<Ingredient> ExpectedIngredients { get; private set; }

        protected Ingredient ExpectedFiller { get; private set; }

        protected override void Given()
        {
            var @event = GetEvent();
            PublishedIngredientUpdatedEvent = PublishedEvent.For(@event);
            Pie = New.ReadModels().Pie();
            ExpectedIngredients = new List<Ingredient> { New.ReadModels().Ingredient() };
            ExpectedFiller = New.ReadModels().Ingredient();

            MockFor<IRepository>().Setup(repo => repo.FindById<Guid, Pie>(PublishedIngredientUpdatedEvent.EventSourceId)).Returns(Pie);
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<IEnumerable<Pies.Core.Domain.Ingredient>, IEnumerable<Ingredient>>(@event.AllIngredients))
                                     .Returns(ExpectedIngredients);
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<Pies.Core.Domain.Ingredient, Ingredient>(@event.Filler))
                                     .Returns(ExpectedFiller);
        }
    }
}