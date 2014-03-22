using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    public class IsPrivateUpdatedEvent : SourcedEvent
    {
        public bool IsPrivate { get; private set; }
        
        public IsPrivateUpdatedEvent(bool isPrivate)
        {
            IsPrivate = isPrivate;
        }
    }
}