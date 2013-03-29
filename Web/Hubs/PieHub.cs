﻿using System;
using System.Linq;
using Codell.Pies.Common;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Web.EventHandlers
{
    [HubName("pie")]
    public class PieHub : Hub, IEventHandler<IngredientAddedEvent>, 
                               IEventHandler<PercentageUpdatedEvent>, 
                               IEventHandler<ProposedPercentageChangedEvent>,
                               IEventHandler<PercentageRejectedEvent>
    {
        private readonly IHubContext _hubContext;

        public PieHub(IHubContext hubContext)
        {
            Verify.NotNull(hubContext, "hubContext");       
            _hubContext = hubContext;
        }

        public void Handle(IPublishedEvent<IngredientAddedEvent> @event)
        {
            //var model = new IngredientModel { Id = @event.Payload.Id, Percent = @event.Payload.Percent, Description = @event.Payload.Description, PieId = @event.EventSourceId };
            //var view = _controller.Render("_EditableIngredient", model);
            //_hubContext.Clients.All.ingredientAdded(view);
            PublishIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        public void Handle(IPublishedEvent<PercentageUpdatedEvent> @event)
        {
            PublishIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        private void PublishIngredientsUpdated(IIngredientsUpdatedEvent @event, Guid pieId)
        {
            var ingredients = @event.AllIngredients.Select(ingredient => ToDto(ingredient, pieId));
            var filler = ToDto(@event.Filler, pieId);
            _hubContext.Clients.All.ingredientsUpdated(new { ingredients, filler });            
        }

        private dynamic ToDto(Ingredient ingredient, Guid pieId)
        {
            return new
            {
                id = ingredient.Id,
                percent = ingredient.Percent,
                description = ingredient.Description,
                pieId
            };
        }

        public void Handle(IPublishedEvent<ProposedPercentageChangedEvent> @event)
        {
            PublishPercentageChanged(@event.Payload.Id, @event.Payload.AcceptedPercent, string.Empty);
        }

        public void Handle(IPublishedEvent<PercentageRejectedEvent> @event)
        {
            PublishPercentageChanged(@event.Payload.Id, @event.Payload.CurrentPercent, Resources.PercentRejected);
        }

        private void PublishPercentageChanged(Guid ingredientId, int updatedPercentage, string message)
        {
            _hubContext.Clients.All.percentageChanged (new { id = ingredientId,
                                                             currentPercent = updatedPercentage, 
                                                             message } );            
        }
    }
}