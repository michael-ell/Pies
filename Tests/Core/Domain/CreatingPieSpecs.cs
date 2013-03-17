using System;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;

namespace Codell.Pies.Tests.Core.Domain.CreatingPieSpecs
{
    [Concern(typeof (Pie))]
    public class When_creating_a_pie : AggregateRootSpecBase<Pie>
    {
        private Guid _expected;

        protected override Pie CreateSut()
        {
            _expected = Guid.NewGuid();
            return new Pie(_expected);
        }

        protected override void When()
        {
            //done in create sut...
        }

        [Observation]
        public void Then_should_announce_that_a_pie_was_created()
        {
            Verify<PieCreatedEvent>(e => e.EventSourceId == _expected);
        }
    }
}