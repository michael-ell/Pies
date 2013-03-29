using System;
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
    public class PieHub : Hub, IEventHandler<IngredientAddedEvent>, IEventHandler<IngredientPercentageUpdatedEvent>,  IEventHandler<IngredientPercentageRejectedEvent>
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
            PublishPieIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        public void Handle(IPublishedEvent<IngredientPercentageUpdatedEvent> @event)
        {
            PublishPieIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        private void PublishPieIngredientsUpdated(IIngredientsUpdatedEvent @event, Guid pieId)
        {
            var ingredients = @event.AllIngredients.Select(ingredient => ToDto(ingredient, pieId));
            var filler = ToDto(@event.Filler, pieId);
            _hubContext.Clients.All.pieIngredientsUpdated(new { ingredients, filler });            
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

        public void Handle(IPublishedEvent<IngredientPercentageRejectedEvent> @event)
        {
            _hubContext.Clients.All.ingredientPercentageRejected( new {id = @event.Payload.Id, 
                                                                  currentPercent = @event.Payload.CurrentPercent,
                                                                  message = Resources.PercentRejected} );
        }
    }
}