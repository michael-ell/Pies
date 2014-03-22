using Codell.Pies.Core.Events;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Events
{
    public class IsPrivateUpdatedEventCreator : Creator<IsPrivateUpdatedEvent>
    {
        public IsPrivateUpdatedEventCreator(IFixtureContext context): base(context, new IsPrivateUpdatedEvent(true))
        {
        }
    }
}