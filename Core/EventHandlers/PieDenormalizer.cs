using System;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Core.EventHandlers
{
    public class PieDenormalizer : IEventHandler<PieCreatedEvent>, IEventHandler<PieCaptionUpdatedEvent>, IEventHandler<IngredientAddedEvent>
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mapper;

        public PieDenormalizer(IRepository repository, IMappingEngine mapper)
        {
            Verify.NotNull(repository, "repository");
            Verify.NotNull(mapper, "mapper");
                        
            _repository = repository;
            _mapper = mapper;
        }

        public void Handle(IPublishedEvent<PieCreatedEvent> evnt)
        {
            _repository.Save(new Pie { Id = evnt.EventSourceId, CreatedOn = DateTime.Now } );
        }

        public void Handle(IPublishedEvent<PieCaptionUpdatedEvent> evnt)
        {
            var pie = GetPieFor(evnt);
            pie.Caption = evnt.Payload.Caption;
            _repository.Save(pie);
        }

        public void Handle(IPublishedEvent<IngredientAddedEvent> evnt)
        {
            var pie = GetPieFor(evnt);
            pie.Ingredients.Add(_mapper.Map<Domain.Ingredient, Ingredient>(evnt.Payload.Added));
            _repository.Save(pie);
        }

        private Pie GetPieFor(IPublishableEvent evnt)
        {
            return _repository.FindById<Guid, Pie>(evnt.EventSourceId);
        }
    }
}