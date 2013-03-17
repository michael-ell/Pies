using System;
using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Core.EventHandlers
{
    public class PieDenormalizer : IEventHandler<PieCreatedEvent>, IEventHandler<PieCaptionUpdatedEvent>
    {
        private readonly IRepository _repository;

        public PieDenormalizer(IRepository repository)
        {
            Verify.NotNull(repository, "repository");
            
            _repository = repository;
        }

        public void Handle(IPublishedEvent<PieCreatedEvent> evnt)
        {
            _repository.Save(new Pie { Id = evnt.EventSourceId } );
        }

        public void Handle(IPublishedEvent<PieCaptionUpdatedEvent> evnt)
        {
            var pie = _repository.FindById<Guid, Pie>(evnt.EventSourceId);
            pie.Caption = evnt.Payload.Caption;
            _repository.Save(pie);
        }
    }
}