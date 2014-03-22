using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Ncqrs;

namespace Codell.Pies.Tests.Core.Domain.UpdatingIsPiePrivateSpecs
{
    [Concern(typeof (IsPrivateUpdatedEvent))]
    public class When_updating_whether_a_pie_is_private_or_not : AggregateRootSpecBase<Pie>
    {
        private bool _expected;

        protected override void Given()
        {
            _expected = true;
        }

        protected override void When()
        {
            Sut.UpdateIsPrivate(_expected);
        }

        [Observation]
        public void Then_should_announce_whether_the_pie_is_private_or_not()
        {
            Verify<IsPrivateUpdatedEvent>(e => e.IsPrivate == _expected).WasPublished();
        }
    }
}