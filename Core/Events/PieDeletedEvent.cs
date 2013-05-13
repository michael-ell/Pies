using System;
using Codell.Pies.Common.Security;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Core.Events
{
    [Serializable]
    public class PieDeletedEvent : SourcedEvent
    {
        public IUser Owner { get; private set; }

        public PieDeletedEvent(IUser owner)
        {
            Owner = owner;
        }
    }
}