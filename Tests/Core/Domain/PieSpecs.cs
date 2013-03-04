using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;
using Codell.Pies.Testing.Creators.Domain;
using FluentAssertions;

namespace Codell.Pies.Tests.Core.Domain.PieSpecs
{
    [Concern(typeof (Pie))]
    public class When_creating_a_pie : AggregateRootSpecBase<Pie>
    {
        protected override Pie CreateSut()
        {
            return new Pie(Guid.NewGuid());
        }

        protected override void When()
        {
            // na, done in create sut
        }

        [Observation]
        public void Then_should_announce_that_a_pie_was_started()
        {
            Verify<PieStartedEvent>().WasPublished();
        }
    }

    [Concern(typeof (Pie))]
    public class When_slicing_a_pie : AggregateRootSpecBase<Pie>
    {
        private string _description;
        private int _percent;
        private int _expectedRemaining;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void Given()
        {
            _percent = 20;
            _description = "some description";
            _expectedRemaining = 80;
        }

        protected override void When()
        {
            Sut.Slice(_percent, _description);
        }

        [Observation]
        public void Then_should_announce_the_percent_of_the_slice()
        {
            Verify<PieSlicedEvent>(e => e.Percent == _percent).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_the_description_for_the_slice()
        {
            Verify<PieSlicedEvent>(e => e.Description == _description).WasPublished();
        }

        [Observation]
        public void Then_should_announce_that_the_remaining_precentage_for_the_pie()
        {
            Verify<PieSlicedEvent>(e => e.RemainingPercent == _expectedRemaining).WasPublished();
        } 
    }

    [Concern(typeof(Pie))]
    public class When_slicing_a_pie_with_a_negative_percentage: AggregateRootSpecBase<Pie>
    {
        private Action _act;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void When()
        {
            _act = () => Sut.Slice(-1, "xxx");
        }

        [Observation]
        public void Then_should_not_allow_the_slice_to_continue()
        {
            _act.ShouldThrow<BusinessRuleException>().WithMessage(Resources.InvalidPercentage);
        }
    }

    [Concern(typeof(Pie))]
    public class When_slicing_a_pie_with_a_percentage_greate_than_100 : AggregateRootSpecBase<Pie>
    {
        private Action _act;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void When()
        {
            _act = () => Sut.Slice(101, "xxx");
        }

        [Observation]
        public void Then_should_not_allow_the_slice_to_continue()
        {
            _act.ShouldThrow<BusinessRuleException>().WithMessage(Resources.InvalidPercentage);
        }
    }

    [Concern(typeof(Pie))]
    public class When_slicing_a_pie_that_is_already_has_100_percent_of_the_pie_accounted_for : AggregateRootSpecBase<Pie>
    {
        private Action _act;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void Given()
        {
            Sut.Slice(100, "all accounted for");
        }

        protected override void When()
        {
            _act = () => Sut.Slice(20, "my pie runneth over");
        }

        [Observation]
        public void Then_should_not_allow_the_slice_to_continue()
        {
            _act.ShouldThrow<BusinessRuleException>().WithMessage(Resources.PieAccountedFor);
        }
    }
}