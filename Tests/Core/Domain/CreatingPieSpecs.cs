using System;
using Codell.Pies.Common;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;
using Codell.Pies.Testing.Creators.Common;

namespace Codell.Pies.Tests.Core.Domain.CreatingPieSpecs
{
    [Concern(typeof (Pie))]
    public class When_creating_a_pie : AggregateRootSpecBase<Pie>
    {
        private Guid _expectedId;
        private IUser _expectedUser;

        protected override Pie CreateSut()
        {
            _expectedId = Guid.NewGuid();
            _expectedUser = New.Common().User();
            return new Pie(_expectedId, _expectedUser);
        }

        protected override void When()
        {
            //done in create sut...
        }

        [Observation]
        public void Then_should_announce_that_a_pie_was_created()
        {
            Verify<PieCreatedEvent>(e => e.EventSourceId == _expectedId);
        }

        [Observation]
        public void Then_should_announce_the_user_associated_who_owns_the_pie()
        {
            Verify<PieCreatedEvent>(e => e.User == _expectedUser);
        }

        [Observation]
        public void Then_should_announce_the_default_caption_for_the_pie()
        {
            Verify<PieCreatedEvent>(e => e.Caption == string.Empty);
        }

        [Observation]
        public void Then_should_announce_that_the_pie_is_100_percent_filler()
        {
            Verify<PieCreatedEvent>(e => e.Filler.Percent == Pie.Max).WasPublished();
        }

        [Observation]
        public void Then_should_announce_a_color_for_the_filler()
        {
            Verify<PieCreatedEvent>(e => e.Filler.Color.IsNotEmpty()).WasPublished();
        }

    }
}