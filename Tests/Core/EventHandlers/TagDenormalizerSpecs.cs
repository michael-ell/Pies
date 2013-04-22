using System;
using System.Collections.Generic;
using Codell.Pies.Core.EventHandlers;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;
using Codell.Pies.Testing.Creators.Events;
using Moq;

namespace Codell.Pies.Tests.Core.EventHandlers.TagDenormalizerSpecs
{
    [Concern(typeof (TagDenormalizer))]
    public class When_a_tag_is_added_to_a_pie_that_does_not_exist_in_the_applications_tag_list : EventHandlerSpecBase<TagDenormalizer>
    {
        private PieTagsUpdatedEvent _event;
        private string _expected;

        protected override void Given()
        {
            _expected = "some tag";
            _event = New.Events().PieTagsUpdatedEvent().With(_expected);
            MockFor<IRepository>().Setup(repo => repo.Project(It.IsAny<Func<Tag, Pies.Core.Domain.Tag>>())).Returns(new List<Pies.Core.Domain.Tag>());
        }

        protected override void When()
        {
            Sut.Handle(PublishedEvent.For(_event));
        }

        [Observation]
        public void Then_should_add_the_tag_to_the_applications_tag_list()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Tag>(tag => tag.Value == _expected)));
        }
    }

    public class When_a_tag_is_added_to_a_pie_that_already_exists_in_the_applications_tag_list : EventHandlerSpecBase<TagDenormalizer>
    {
        private PieTagsUpdatedEvent _event;
        private string _alreadyExists;

        protected override void Given()
        {
            _alreadyExists = "some tag";
            _event = New.Events().PieTagsUpdatedEvent().With(_alreadyExists);
            MockFor<IRepository>().Setup(repo => repo.Project(It.IsAny<Func<Tag, Pies.Core.Domain.Tag>>())).Returns(new List<Pies.Core.Domain.Tag>{_alreadyExists});
        }

        protected override void When()
        {
            Sut.Handle(PublishedEvent.For(_event));
        }

        [Observation]
        public void Then_should_not_add_the_tag_to_the_applications_tag_list()
        {
            MockFor<IRepository>().Verify(repo => repo.Save(It.Is<Tag>(tag => tag.Value == _alreadyExists)), Times.Never());
        }
    }
}