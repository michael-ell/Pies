using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.Repositories;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Core.EventHandlers
{
    public class Cleaner : IEventHandler<PieCreatedEvent>
    {
        private readonly IRepository _repository;

        public Cleaner(IRepository repository)
        {
            Verify.NotNull(repository, "repository");            
            _repository = repository;
        }

        public void Handle(IPublishedEvent<PieCreatedEvent> evnt)
        {
            throw new System.NotImplementedException();
        }
    }
}