using System.Linq;
using Codell.Pies.Common;
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
            _hubContext.Clients.All.pieIngredientsUpdated(@event.Payload.AllIngredients.Select(ingredient => new { id = ingredient.Id, 
                                                                                                                   percent = ingredient.Percent, 
                                                                                                                   description = ingredient.Description, 
                                                                                                                   pieId = @event.EventSourceId }).ToList());
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