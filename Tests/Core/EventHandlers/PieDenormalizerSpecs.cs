using System;
using System.Collections.Generic;
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

    [Concern(typeof(PieDenormalizer))]
    public class When_an_ingredients_percentage_is_updated : EventHandlerSpecBase<PieDenormalizer>
    {
        private PercentageUpdatedEvent _event;
        private PublishedEvent<PercentageUpdatedEvent> _publishedEvent;
        private Pie _pie;
        private List<Ingredient> _expectedIngredients;
        private Ingredient _expectedFiller;

        protected override void Given()
        {
            _event = New.Events().PercentageUpdatedEvent();
            _publishedEvent = PublishedEvent.For(_event);
            _pie = New.ReadModels().Pie();
            _expectedIngredients = new List<Ingredient>{New.ReadModels().Ingredient()};
            _expectedFiller = New.ReadModels().Ingredient();

            MockFor<IRepository>().Setup(repo => repo.FindById<Guid, Pie>(_publishedEvent.EventSourceId)).Returns(_pie);
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<IEnumerable<Pies.Core.Domain.Ingredient>, IEnumerable<Ingredient>>(_event.AllIngredients))
                                     .Returns(_expectedIngredients);
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<Pies.Core.Domain.Ingredient, Ingredient>(_event.Filler))
                                     .Returns(_expectedFiller);
        }

        protected override void When()
        {
            Sut.Handle(_publishedEvent);
        }

        [Observation]
        public void Then_should_update_the_ingredients_to_reflect_the_change()
        {
            _expectedIngredients.ForEach(ingredient => _pie.Ingredients.Should().Contain(ingredient));
        }

        [Observation]
        public void Then_should_include_the_filler_as_part_of_the_ingredients()
        {
            _pie.Ingredients.Should().Contain(_expectedFiller);
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(_pie));
        }
    }

    [Concern(typeof(PieDenormalizer))]
    public class When_an_ingredients_proposed_precentage_is_changed: EventHandlerSpecBase<PieDenormalizer>
    {
        private ProposedPercentageChangedEvent _event;
        private PublishedEvent<ProposedPercentageChangedEvent> _publishedEvent;
        private Pie _pie;
        private List<Ingredient> _expectedIngredients;
        private Ingredient _expectedFiller;

        protected override void Given()
        {
            _event = New.Events().ProposedPercentageChangedEvent();
            _publishedEvent = PublishedEvent.For(_event);
            _pie = New.ReadModels().Pie();
            _expectedIngredients = new List<Ingredient> { New.ReadModels().Ingredient() };
            _expectedFiller = New.ReadModels().Ingredient();

            MockFor<IRepository>().Setup(repo => repo.FindById<Guid, Pie>(_publishedEvent.EventSourceId)).Returns(_pie);
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<IEnumerable<Pies.Core.Domain.Ingredient>, IEnumerable<Ingredient>>(_event.AllIngredients))
                                     .Returns(_expectedIngredients);
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<Pies.Core.Domain.Ingredient, Ingredient>(_event.Filler))
                                     .Returns(_expectedFiller);
        }

        protected override void When()
        {
            Sut.Handle(_publishedEvent);
        }

        [Observation]
        public void Then_should_update_the_ingredients_to_reflect_the_change()
        {
            _expectedIngredients.ForEach(ingredient => _pie.Ingredients.Should().Contain(ingredient));
        }

        [Observation]
        public void Then_should_include_the_filler_as_part_of_the_ingredients()
        {
            _pie.Ingredients.Should().Contain(_expectedFiller);
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(_pie));
        }
    }

    [Concern(typeof(PieDenormalizer))]
    public class When_an_ingredients_percentage_is_updated_that_eliminated_filler : EventHandlerSpecBase<PieDenormalizer>
    {
        private PercentageUpdatedEvent _event;
        private PublishedEvent<PercentageUpdatedEvent> _publishedEvent;
        private Pie _pie;
        private List<Ingredient> _expectedIngredients;

        protected override void Given()
        {
            _event = New.Events().PercentageUpdatedEvent().WithNoFiller();
            _publishedEvent = PublishedEvent.For(_event);
            _pie = New.ReadModels().Pie();
            _expectedIngredients = new List<Ingredient> { New.ReadModels().Ingredient() };


            MockFor<IRepository>().Setup(repo => repo.FindById<Guid, Pie>(_publishedEvent.EventSourceId)).Returns(_pie);
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<IEnumerable<Pies.Core.Domain.Ingredient>, IEnumerable<Ingredient>>(_event.AllIngredients))
                                     .Returns(_expectedIngredients);

        }

        protected override void When()
        {
            Sut.Handle(_publishedEvent);
        }

        [Observation]
        public void Then_should_only_contain_the_ingredients()
        {
            _expectedIngredients.ForEach(ingredient => _pie.Ingredients.Should().Contain(ingredient));
            _pie.Ingredients.Count.Should().Be(_expectedIngredients.Count);
        }

        [Observation]
        public void Then_should_not_include_the_filler_as_part_of_the_ingredients()
        {
            MockFor<IMappingEngine>().Verify(mapper => mapper.Map<Pies.Core.Domain.Ingredient, Ingredient>(_event.Filler), Times.Never());
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(_pie));
        }
    }
}