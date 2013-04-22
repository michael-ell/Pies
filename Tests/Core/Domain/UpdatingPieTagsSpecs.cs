using System.Collections.Generic;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Helpers;
using Codell.Pies.Testing.Ncqrs;

namespace Codell.Pies.Tests.Core.Domain.UpdatingPieTagsSpecs
{
    [Concern(typeof (Pie))]
    public class When_updating_tags_used_to_find_a_pie_and_the_tags_have_changed : AggregateRootSpecBase<Pie>
    {
        private IEnumerable<string> _expected;

        protected override void Given()
        {
            _expected = new List<string> { "abc", "def", "ghi" };
        }

        protected override void When()
        {
            Sut.UpdateTags(_expected);
        }

        [Observation]
        public void Then_should_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>(e => e.NewTags.Matches(_expected)).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_removing_existing_tags_used_to_find_a_pie : AggregateRootSpecBase<Pie>
    {
        private List<string> _expected;

        protected override void Given()
        {
            Sut.UpdateTags(new List<string> { "abc", "def", "ghi" });
            _expected = new List<string> { "abc" };
        }

        protected override void When()
        {
            Sut.UpdateTags(_expected);
        }

        [Observation]
        public void Then_should_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>(e => e.NewTags.Matches(_expected)).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_clearing_existing_tags_used_to_find_a_pie : AggregateRootSpecBase<Pie>
    {
        private List<string> _expected;

        protected override void Given()
        {
            Sut.UpdateTags(new List<string> { "abc", "def", "ghi" });
            _expected = new List<string>();
        }

        protected override void When()
        {
            Sut.UpdateTags(_expected);
        }

        [Observation]
        public void Then_should_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>(e => e.NewTags.Matches(_expected)).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_tags_used_to_find_a_pie_and_the_tags_have_NOT_changed : AggregateRootSpecBase<Pie>
    {
        private IEnumerable<string> _sameTags;

        protected override void Given()
        {
            _sameTags = new List<string> { "a", "b", "c" };
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
            Sut.UpdateTags(new List<string> { "a", "b", "c" });
        }

        protected override void When()
        {
            Sut.UpdateTags(new List<string> { "b", "c", "a" });
        }

        [Observation]
        public void Then_should_not_announce_that_the_tags_have_been_changed()
        {
            Verify<PieTagsUpdatedEvent>().WasNotPublished();
        }
    }
}