using System;
using System.Linq;
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
using Codell.Pies.Testing.Creators.Common;

namespace Codell.Pies.Tests.Core.EventHandlers.PieDenormalizerSpecs
{
    [Concern(typeof (PieDenormalizer))]
    public class When_a_pie_is_created : EventHandlerSpecBase<PieDenormalizer>
    {
        private PublishedEvent<PieCreatedEvent> _event;
        private Ingredient _expectedFiller;

        protected override void Given()
        {
            _event = PublishedEvent.For(New.Events().PieCreatedEvent().Creation);
            _expectedFiller = New.ReadModels().Ingredient();
            MockFor<IMappingEngine>().Setup(mapper => mapper.Map<Pies.Core.Domain.Ingredient, Ingredient>(_event.Payload.Filler)).Returns(_expectedFiller);
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
        public void Then_should_save_the_pie_with_the_owners_id()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.OwnerId == _event.Payload.Owner.Id)));
        }

        [Observation]
        public void Then_should_save_the_pie_with_the_default_caption()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.Caption == _event.Payload.Caption)));
        }

        [Observation]
        public void Then_should_save_the_date_the_pie_was_created()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.CreatedOn.IgnoreSeconds() == DateTime.Now.IgnoreSeconds())));
        }

        [Observation]
        public void Then_should_save_the_pie_with_no_ingredients()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.EditableIngredients != null && !pie.EditableIngredients.Any())));
        }

        [Observation]
        public void Then_should_save_the_pie_with_the_filler()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.Filler == _expectedFiller)));
        }

        [Observation]
        public void Then_should_save_the_pie_with_empty_tags()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.Tags != null && !pie.Tags.Any())));
        }

        [Observation]
        public void Then_the_pie_should_be_empty()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Pie>(pie => pie.IsEmpty)));
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
    public class When_an_ingredient_is_added : IngredientUpdatedPieDenormalizerSpecBase<IngredientAddedEvent>
    {
        protected override IngredientAddedEvent GetEvent()
        {
            return New.Events().IngredientAddedEvent();
        }

        protected override void When()
        {
            Sut.Handle(PublishedIngredientUpdatedEvent);
        }

        [Observation]
        public void Then_should_update_the_ingredients_to_reflect_the_change()
        {
            ExpectedIngredients.ForEach(ingredient => Pie.EditableIngredients.Should().Contain(ingredient));
        }

        [Observation]
        public void Then_should_update_the_filler()
        {
            Pie.Filler.Should().Be(ExpectedFiller);
        }

        [Observation]
        public void Then_the_pie_should_not_be_empty()
        {
            Pie.IsEmpty.Should().BeFalse();
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(Pie));
        }   
    }

    [Concern(typeof(PieDenormalizer))]
    public class When_an_ingredients_percentage_is_updated : IngredientUpdatedPieDenormalizerSpecBase<IngredientPercentageUpdatedEvent>
    {
        protected override IngredientPercentageUpdatedEvent GetEvent()
        {
            return New.Events().IngredientPercentageUpdatedEvent();
        }

        protected override void When()
        {
            Sut.Handle(PublishedIngredientUpdatedEvent);
        }

        [Observation]
        public void Then_should_update_the_ingredients_to_reflect_the_change()
        {
            ExpectedIngredients.ForEach(ingredient => Pie.EditableIngredients.Should().Contain(ingredient));
        }

        [Observation]
        public void Then_should_update_the_filler()
        {
            Pie.Filler.Should().Be(ExpectedFiller);
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(Pie));
        }
    }

    [Concern(typeof(PieDenormalizer))]
    public class When_an_ingredients_proposed_precentage_is_changed : IngredientUpdatedPieDenormalizerSpecBase<ProposedIngredientPercentageChangedEvent>
    {
        protected override ProposedIngredientPercentageChangedEvent GetEvent()
        {
            return New.Events().ProposedIngredientPercentageChangedEvent();
        }

        protected override void When()
        {
            Sut.Handle(PublishedIngredientUpdatedEvent);
        }

        [Observation]
        public void Then_should_update_the_ingredients_to_reflect_the_change()
        {
            ExpectedIngredients.ForEach(ingredient => Pie.EditableIngredients.Should().Contain(ingredient));
        }

        [Observation]
        public void Then_should_update_the_filler()
        {
            Pie.Filler.Should().Be(ExpectedFiller);
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(Pie));
        }
    }

    [Concern(typeof(PieDenormalizer))]
    public class When_an_ingredient_is_deleted : IngredientUpdatedPieDenormalizerSpecBase<IngredientDeletedEvent>
    {
        protected override IngredientDeletedEvent GetEvent()
        {
            return New.Events().IngredientDeletedEvent();
        }

        protected override void When()
        {
            Sut.Handle(PublishedIngredientUpdatedEvent);
        }

        [Observation]
        public void Then_should_update_the_ingredients_to_reflect_the_change()
        {
            ExpectedIngredients.ForEach(ingredient => Pie.EditableIngredients.Should().Contain(ingredient));
        }

        [Observation]
        public void Then_the_pie_should_not_be_empty()
        {
            Pie.IsEmpty.Should().BeFalse();
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(Pie));
        }
    }

    [Concern(typeof(PieDenormalizer))]
    public class When_an_ingredient_color_is_updated : IngredientUpdatedPieDenormalizerSpecBase<IngredientColorUpdatedEvent>
    {    
        protected override IngredientColorUpdatedEvent GetEvent()
        {
            return New.Events().IngredientColorUpdatedEvent();
        }

        protected override void When()
        {
            Sut.Handle(PublishedIngredientUpdatedEvent);
        }

        [Observation]
        public void Then_should_update_the_ingredients_to_reflect_the_change()
        {
            ExpectedIngredients.ForEach(ingredient => Pie.EditableIngredients.Should().Contain(ingredient));
        }

        [Observation]
        public void Then_the_pie_should_not_be_empty()
        {
            Pie.IsEmpty.Should().BeFalse();
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(Pie));
        }
    }

    [Concern(typeof(PieDenormalizer))]
    public class When_an_ingredient_description_is_updated : IngredientUpdatedPieDenormalizerSpecBase<IngredientDescriptionUpdatedEvent>
    {
        protected override IngredientDescriptionUpdatedEvent GetEvent()
        {
            return New.Events().IngredientDescriptionUpdatedEvent();
        }

        protected override void When()
        {
            Sut.Handle(PublishedIngredientUpdatedEvent);
        }

        [Observation]
        public void Then_should_update_the_ingredients_to_reflect_the_change()
        {
            ExpectedIngredients.ForEach(ingredient => Pie.EditableIngredients.Should().Contain(ingredient));
        }

        [Observation]
        public void Then_the_pie_should_not_be_empty()
        {
            Pie.IsEmpty.Should().BeFalse();
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(Pie));
        }
    }

    [Concern(typeof (PieDenormalizer))]
    public class When_tags_for_a_pie_are_updated : EventHandlerSpecBase<PieDenormalizer>
    {
        private PieTagsUpdatedEvent _event;
        private Pie _pie;

        protected override void Given()
        {
            _event = New.Events().PieTagsUpdatedEvent();
            _pie = New.ReadModels().Pie();
            MockFor<IRepository>().Setup(repo => repo.FindById<Guid, Pie>(_event.EventSourceId)).Returns(_pie);
        }

        protected override void When()
        {
            Sut.Handle(PublishedEvent.For(_event));
        }


        [Observation]
        public void Then_should_update_the_tags_for_the_pie()
        {
            _pie.Tags.Should().Equal(_event.NewTags);
        }

        [Observation]
        public void Then_should_save_the_pie()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(_pie));
        }         
    }

    [Concern(typeof(PieDenormalizer))]
    public class When_a_pie_is_deleted : EventHandlerSpecBase<PieDenormalizer>
    {
        private PublishedEvent<PieDeletedEvent> _event;

        protected override void Given()
        {
            _event = PublishedEvent.For(new PieDeletedEvent(New.Common().User()));
        }

        protected override void When()
        {
            Sut.Handle(_event);
        }

        [Observation]
        public void Then_should_remove_the_pie_read_model()
        {
             MockFor<IRepository>().Verify(repo => repo.DeleteById<Guid, Pie>(_event.EventSourceId));
        }
    }
}