using System;
using AutoMapper;
using Codell.Pies.Core.EventHandlers;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;
using Moq;
using Ncqrs.Eventing.ServiceModel.Bus;
using Codell.Pies.Testing.Helpers;
using FluentAssertions;
using Codell.Pies.Testing.Creators.Events;
using Codell.Pies.Testing.Creators.ReadModels;

namespace Codell.Pies.Tests.Core.EventHandlers.PieDenormalizerSpecs
{
    [Concern(typeof (PieDenormalizer))]
    public class When_a_pie_is_created : EventHandlerSpecBase<PieDenormalizer>
    {
        private PublishedEvent<PieCreatedEvent> _event;

        protected override void Given()
        {
            _event = PublishedEvent.For(new PieCreatedEvent());
        }

        protected override void When()
        {
            Sut.Handle(_event);
        }

        [Observation]
        public void Then_should_save_the_pie_with_the_proper_reference()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.Id == _event.EventSourceId)));
        }

        [Observation]
        public void Then_should_save_the_date_the_pie_was_created()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.CreatedOn.IgnoreSeconds() == DateTime.Now.IgnoreSeconds())));
        }
    }

    [Concern(typeof (PieDenormalizer))]
    public class When_a_pies_caption_is_updated : EventHandlerSpecBase<PieDenormalizer>
    {
        private PublishedEvent<PieCaptionUpdatedEvent> _publishedEvent;
        private string _expectedCaption;
        private Pie _pie;

        protected override void Given()
        {
            _expectedCaption = "xxx";
            _publishedEvent = PublishedEvent.For(new PieCaptionUpdatedEvent(_expectedCaption));
            _pie = New.ReadModels().Pie();
            MockFor<IRepository>().Setup(repo => repo.FindById<Guid, Pie>(_publishedEvent.EventSourceId)).Returns(_pie);
        }

        protected override void When()
        {
            Sut.Handle(_publishedEvent);
        }

        [Observation]
        public void Then_should_update_the_caption_for_the_pie()
        {
            _pie.Caption.Should().Be(_expectedCaption);
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(_pie));
        }         
    }

    [Concern(typeof (PieDenormalizer))]
    public class When_an_ingredient_is_added : EventHandlerSpecBase<PieDenormalizer>
    {
        private IngredientAddedEvent _event;
        private PublishedEvent<IngredientAddedEvent> _publishedEvent;
        private Pie _pie;
        private Ingredient _expectedIngredient;

        protected override void Given()
        {
            _event = New.Events().IngredientAddedEvent();
            _publishedEvent = PublishedEvent.For(_event);
            _pie = New.ReadModels().Pie();
            _expectedIngredient = New.ReadModels().Ingredient();

            MockFor<IRepository>().Setup(repo => repo.FindById<Guid, Pie>(_publishedEvent.EventSourceId)).Returns(_pie);
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<Pies.Core.Domain.Ingredient, Ingredient>(_event.Added)).Returns(_expectedIngredient);
        }

        protected override void When()
        {
            Sut.Handle(_publishedEvent);
        }

        [Observation]
        public void Then_should_add_the_ingredient_to_the_pie()
        {
            _pie.Ingredients.Should().Contain(_expectedIngredient);
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(_pie));
        }        
    }
}