﻿using Codell.Pies.Common;
using Codell.Pies.Core.Events;
using Codell.Pies.Web.Controllers;
using Codell.Pies.Web.Extensions;
using Codell.Pies.Web.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Web.EventHandlers
{
    [HubName("pie")]
    public class PieHub : Hub, IEventHandler<IngredientAddedEvent>, IEventHandler<IngredientPercentageUpdatedEvent>,  IEventHandler<IngredientPercentageRejectedEvent>
    {
        private readonly IHubContext _hubContext;
        private readonly PieController _controller;

        public PieHub(IHubContext hubContext, PieController controller)
        {
            Verify.NotNull(hubContext, "hubContext");
            Verify.NotNull(controller, "controller");            
            _hubContext = hubContext;
            _controller = controller;
        }

        public void Handle(IPublishedEvent<IngredientAddedEvent> @event)
        {
            var model = new IngredientModel { Id = @event.Payload.Id, Percent = @event.Payload.Percent, Description = @event.Payload.Description, PieId = @event.EventSourceId };
            var view = _controller.Render("_EditableIngredient", model);
            _hubContext.Clients.All.ingredientAdded(view);
        }

        public void Handle(IPublishedEvent<IngredientPercentageUpdatedEvent> evnt)
        {
            _hubContext.Clients.All.ingredientPercentUpdated( new {id = evnt.Payload.Id, percent = evnt.Payload.Percent} );
        }

        public void Handle(IPublishedEvent<IngredientPercentageRejectedEvent> @event)
        {
            _hubContext.Clients.All.ingredientPercentageRejected( new {id = @event.Payload.Id, 
                                                                  currentPercent = @event.Payload.CurrentPercent,
                                                                  message = string.Format(Resources.RejectedPercentage, @event.Payload.RejectedPercent)} );
        }
    }
}