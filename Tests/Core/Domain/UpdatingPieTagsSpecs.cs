using System.Collections.Generic;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;

namespace Codell.Pies.Tests.Core.Domain.UpdatingPieTagsSpecs
{
    [Concern(typeof (Pie))]
    public class When_updating_tags_used_to_find_a_pie_and_the_tags_have_changed : AggregateRootSpecBase<Pie>
    {
        private IEnumerable<Tag> _expected;

        protected override void Given()
        {
            _expected = new List<Tag> { "a", "b", "c" };
        }

        protected override void When()
        {
            Sut.UpdateTags(_expected);
        }

        [Observation]
        public void Then_should_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>(e => Equals(e.NewTags, _expected)).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_removing_existing_tags_used_to_find_a_pie : AggregateRootSpecBase<Pie>
    {
        private List<Tag> _expected;

        protected override void Given()
        {
            Sut.UpdateTags(new List<Tag> { "a", "b", "c" });
            _expected = new List<Tag> { "a" };
        }

        protected override void When()
        {
            Sut.UpdateTags(_expected);
        }

        [Observation]
        public void Then_should_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>(e => Equals(_expected, e.NewTags)).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_clearing_existing_tags_used_to_find_a_pie : AggregateRootSpecBase<Pie>
    {
        protected override void Given()
        {
            Sut.UpdateTags(new List<Tag> { "a", "b", "c" });
        }

        protected override void When()
        {
            Sut.UpdateTags(new List<Tag>());
        }

        [Observation]
        public void Then_should_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>(e => e.NewTags.IsEmpty()).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_tags_used_to_find_a_pie_and_the_tags_have_NOT_changed : AggregateRootSpecBase<Pie>
    {
        private IEnumerable<Tag> _sameTags;

        protected override void Given()
        {
            _sameTags = new List<Tag> { "a", "b", "c" };
            Sut.UpdateTags(_sameTags);
        }

        protected override void When()
        {
            Sut.UpdateTags(_sameTags);
        }

        [Observation]
        public void Then_should_not_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>().WasNotPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_tags_used_to_find_a_pie_and_the_tags_have_the_same_values_but_in_different_orders: AggregateRootSpecBase<Pie>
    {
        protected override void Given()
        {
            Sut.UpdateTags(new List<Tag> { "a", "b", "c" });
        }

        protected override void When()
        {
            Sut.UpdateTags(new List<Tag> { "b", "c", "a" });
        }

        [Observation]
        public void Then_should_not_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>().WasNotPublished();
        }
    }
}