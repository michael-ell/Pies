﻿using System;
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
                                   IEventHandler<IngredientAddedEvent>,
                                   IEventHandler<PercentageUpdatedEvent>,
                                   IEventHandler<ProposedPercentageChangedEvent>
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
            _repository.Save(new Pie { Id = evnt.EventSourceId, CreatedOn = DateTime.Now, IsEmpty = true} );
        }

        public void Handle(IPublishedEvent<PieCaptionUpdatedEvent> evnt)
        {
            var pie = GetPieFor(evnt);
            pie.Caption = evnt.Payload.NewCaption;
            _repository.Save(pie);
        }

        public void Handle(IPublishedEvent<IngredientAddedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        public void Handle(IPublishedEvent<PercentageUpdatedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        public void Handle(IPublishedEvent<ProposedPercentageChangedEvent> evnt)
        {
            UpdateIngredients(GetPieFor(evnt), evnt.Payload);
        }

        private void UpdateIngredients(Pie pie, IIngredientsUpdatedEvent evnt)
        {
            pie.IsEmpty = false;
            pie.Ingredients.Clear();
            pie.Ingredients.AddRange(_mapper.Map<IEnumerable<Domain.Ingredient>, IEnumerable<Ingredient>>(evnt.AllIngredients));
            if (evnt.Filler.Percent > 0)
            {
                pie.Ingredients.Add(_mapper.Map<Domain.Ingredient, Ingredient>(evnt.Filler));
            }
            _repository.Save(pie);            
        }

        private Pie GetPieFor(IPublishableEvent evnt)
        {
            return _repository.FindById<Guid, Pie>(evnt.EventSourceId);
        }
    }
}