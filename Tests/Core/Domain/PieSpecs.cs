using System;
using System.Linq;
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
        private string _expectedName;

        protected override Pie CreateSut()
        {
            _expectedName = "xxxx";
            return new Pie(Guid.NewGuid(), _expectedName);
        }

        protected override void When()
        {
            // na, done in create sut
        }

        [Observation]
        public void Then_should_announce_that_a_pie_was_created()
        {
            Verify<PieCreatedEvent>().WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_name_of_the_pie_that_was_created()
        {
            Verify<PieCreatedEvent>(e => e.Name == _expectedName).WasPublished();
        }

        [Observation]
        public void Then_should_fill_the_pie()
        {
            Verify<SliceAddedEvent>(e => e.Percent == Pie.Max && e.Description.IsEmpty()).WasPublished();
        }        
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_slice_and_the_percent_is_negative : AggregateRootSpecBase<Pie>
    {
        private Action _act;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void When()
        {
            _act = () => Sut.UpdateSlicePercentage(Sut.Slices.First().Id, -10);
        }

        [Observation]
        public void Then_should_not_allow_the_slice_to_continue()
        {
            _act.ShouldThrow<BusinessRuleException>().WithMessage(Resources.InvalidPercentage);
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_slice_and_the_percent_is_greater_than_100 : AggregateRootSpecBase<Pie>
    {
        private Action _act;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void When()
        {
            _act = () => Sut.UpdateSlicePercentage(Sut.Slices.First().Id, 101);
        }

        [Observation]
        public void Then_should_not_allow_the_slice_to_continue()
        {
            _act.ShouldThrow<BusinessRuleException>().WithMessage(Resources.InvalidPercentage);
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_slice : AggregateRootSpecBase<Pie>
    {
        private int _percent;
        private Slice _slice;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void Given()
        {
            _percent = 20;
        }

        protected override void When()
        {
            _slice = Sut.Slices.First();
            Sut.UpdateSlicePercentage(_slice.Id, _percent);
        }

        [Observation]
        public void Then_should_announce_slice_percent_was_updated()
        {
            Verify<SlicePercentageUpdatedEvent>(e => e.Percent == _percent && e.SliceId == _slice.Id).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_slice_and_the_pie_is_no_longer_full : AggregateRootSpecBase<Pie>
    {
        private int _percent;
        private int _expectedPercent;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void Given()
        {
            _percent = 20;
            _expectedPercent = 80;
        }

        protected override void When()
        {
            Sut.UpdateSlicePercentage(Sut.Slices.First().Id, _percent);
        }

        [Observation]
        public void Then_should_add_another_slice_to_fill_the_pie()
        {
            Verify<SliceAddedEvent>(e => e.Percent == _expectedPercent && e.Description == string.Empty).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_slice_but_the_percentage_has_not_changed : AggregateRootSpecBase<Pie>
    {
        private Slice _slice;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void Given()
        {
            _slice = Sut.Slices.First();
        }

        protected override void When()
        { 
            Sut.UpdateSlicePercentage(_slice.Id, _slice.Percent);
        }

        [Observation]
        public void Then_should_not_announce_slice_percent_was_updated()
        {
            Verify<SlicePercentageUpdatedEvent>().WasNotPublished();
        }
    }
}