using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;

namespace Codell.Pies.Tests.Core.Domain.DeletingPieSpecs
{
    [Concern(typeof (Pie))]
    public class When_deleting_a_pie : PieSpecBase
    {
        protected override void When()
        {
            Sut.Delete();
        }

        [Observation]
        public void Then_should_announce_that_a_pie_was_deleted()
        {
            Verify<PieDeletedEvent>().WasPublished();
        }

        [Observation]
        public void Then_should_announce_the_owner_of_the_pie_that_was_deleted()
        {
            Verify<PieDeletedEvent>(e => e.Owner == Owner).WasPublished();
        }
    }

    [Concern(typeof(Pie))]
    public class When_deleting_a_pie_that_has_already_been_deleted : PieSpecBase
    {
        protected override void Given()
        {
            Sut.Delete();
        }

        protected override void When()
        {
            Sut.Delete();
        }

        [Observation]
        public void Then_should_not_announce_that_a_pie_was_deleted()
        {
            Verify<PieDeletedEvent>().WasNotPublished();
        }
    }
}