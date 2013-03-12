using System;
using System.Linq;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;
using Codell.Pies.Testing.Creators.Domain;

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
            Sut.UpdateSlicePercentage(_slice.Id, -10);
        }

        [Observation]
        public void Then_should_announce_slice_percent_was_updated_to_zero()
        {
            Verify<SlicePercentageUpdatedEvent>(e => e.Percent == 0 && e.SliceId == _slice.Id).WasPublished();
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
            _slice = Sut.Slices.First();
            _percent = 20;
        }

        protected override void When()
        {
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

    [Concern(typeof(Pie))]
    public class When_updating_the_percentage_of_a_slice_that_would_account_for_more_than_100_percent_of_the_pie : AggregateRootSpecBase<Pie>
    {
        private Slice _slice;
        private int _proposed;

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void Given()
        {
            _slice = Sut.Slices.First();
            _proposed = 110;
        }

        protected override void When()
        {
            Sut.UpdateSlicePercentage(_slice.Id, _proposed);
        }

        [Observation]
        public void Then_should_announce_that_the_slice_percent_was_rejected()
        {
            Verify<SlicePercentageRejectedEvent>(e => e.SliceId == _slice.Id && e.RejectedPercent == _proposed).WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_current_percent_of_the_slice_that_was_not_updated()
        {
            Verify<SlicePercentageRejectedEvent>(e => e.CurrentPercent == _slice.Percent).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_deleting_a_slice_that_exists : AggregateRootSpecBase<Pie>
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
            Sut.DeleteSlice(_slice.Id);
        }

        [Observation]
        public void Then_should_announce_that_the_slice_was_deleted()
        {
            Verify<SliceDeletedEvent>(e => e.SliceId == _slice.Id).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_deleting_a_slice_that_doeS_not_exist_in_the_pie : AggregateRootSpecBase<Pie>
    {

        protected override Pie CreateSut()
        {
            return New.Domain().Pie();
        }

        protected override void When()
        {
            Sut.DeleteSlice(Guid.NewGuid());
        }

        [Observation]
        public void Then_should_not_announce_that_a_slice_was_deleted()
        {
            Verify<SliceDeletedEvent>().WasNotPublished();
        }
    }
}