using System;
using System.Collections.Generic;
using AutoMapper;
using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Core.EventHandlers
{
    public class PieDenormalizer : IEventHandler<PieCreatedEvent>, 
                                   IEventHandler<PieCaptionUpdatedEvent>, 
                                   IEventHandler<PieTagsUpdatedEvent>,
                                   IEventHandler<IngredientAddedEvent>,
                                   IEventHandler<IngredientDeletedEvent>,
                                   IEventHandler<IngredientPercentageUpdatedEvent>,
                                   IEventHandler<IngredientDescriptionUpdatedEvent>,
                                   IEventHandler<IngredientColorUpdatedEvent>,
                                   IEventHandler<ProposedIngredientPercentageChangedEvent>,
                                   IEventHandler<PieDeletedEvent>
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
            var filler = _mapper.Map<Domain.Ingredient, Ingredient>(evnt.Payload.Filler);
            _repository.Save(new Pie
                                 {
                                     Id = evnt.EventSourceId, 
                                     UserEmail = evnt.Payload.User.Email,
                                     Caption = evnt.Payload.Caption,
                                     EditableIngredients = new List<Ingredient>(),
                                     Filler = filler,
                                     CreatedOn = DateTime.Now, 
                                     IsEmpty = true
                                 } );
        }

        public void Handle(IPublishedEvent<PieCaptionUpdatedEvent> evnt)
        {
            var pie = GetPieFor(evnt);
            pie.Caption = evnt.Payload.NewCaption;
            _repository.Save(pie);
        }


        public void Handle(IPublishedEvent<PieTagsUpdatedEvent> evnt)
        {
            var pie = GetPieFor(evnt);
            pie.Tags = evnt.Payload.NewTags;
            _repository.Save(pie);
        }

        public void Handle(IPublishedEvent<IngredientAddedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        public void Handle(IPublishedEvent<IngredientPercentageUpdatedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        public void Handle(IPublishedEvent<ProposedIngredientPercentageChangedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        public void Handle(IPublishedEvent<IngredientColorUpdatedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        public void Handle(IPublishedEvent<IngredientDescriptionUpdatedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        public void Handle(IPublishedEvent<IngredientDeletedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        private void UpdateIngredients(Pie pie, IIngredientsUpdatedEvent evnt)
        {
            pie.IsEmpty = false;
            pie.EditableIngredients = new List<Ingredient>(_mapper.Map<IEnumerable<Domain.Ingredient>, IEnumerable<Ingredient>>(evnt.Ingredients));
            pie.Filler = _mapper.Map<Domain.Ingredient, Ingredient>(evnt.Filler);
            _repository.Save(pie);            
        }

        private Pie GetPieFor(IPublishableEvent evnt)
        {
            return _repository.FindById<Guid, Pie>(evnt.EventSourceId);
        }

        public void Handle(IPublishedEvent<PieDeletedEvent> evnt)
        {
            _repository.DeleteById<Guid, Pie>(evnt.EventSourceId);
        }
    }
}