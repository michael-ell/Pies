using System.Collections.Generic;
using Ncqrs.Eventing;

namespace Codell.Pies.Testing.Ncqrs
{
    public interface IPublishedEventsViewer
    {
        IEnumerable<UncommittedEvent> PublishedEvents { get; }
    }
}