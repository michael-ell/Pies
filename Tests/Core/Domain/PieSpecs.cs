using System;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;

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
            // na, done in create
        }

        [Observation]
        public void Then_should_announce_that_a_pie_was_started()
        {
            Verify<PieStartedEvent>().WasPublished();
        }
    }
}