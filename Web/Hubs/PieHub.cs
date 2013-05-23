using System;
using System.Linq;
using System.Threading.Tasks;
using Codell.Pies.Common;
using Codell.Pies.Common.Security;
using Codell.Pies.Core.Domain;
using Codell.Pies.Core.Events;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Web.EventHandlers
{
    [HubName("pie")]
    public class PieHub : Hub, IEventHandler<PieCaptionUpdatedEvent>,
                               IEventHandler<IngredientAddedEvent>, 
                               IEventHandler<IngredientPercentageUpdatedEvent>, 
                               IEventHandler<IngredientColorUpdatedEvent>, 
                               IEventHandler<IngredientDescriptionUpdatedEvent>, 
                               IEventHandler<ProposedIngredientPercentageChangedEvent>,
                               IEventHandler<PercentageRejectedEvent>,
                               IEventHandler<IngredientDeletedEvent>,
                               IEventHandler<MaxIngredientsReachedEvent>,
                               IEventHandler<PieDeletedEvent>
    {
        public void Handle(IPublishedEvent<PieCaptionUpdatedEvent> @event)
        {  
            SendTo(@event.EventSourceId).captionUpdated(@event.Payload.NewCaption);
        }

        public void Handle(IPublishedEvent<IngredientAddedEvent> @event)
        {
            //var model = new IngredientModel { Id = @event.Payload.Id, Percent = @event.Payload.Percent, Description = @event.Payload.Description, PieId = @event.EventSourceId };
            //var view = _controller.Render("_EditableIngredient", model);
            PublishIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        public void Handle(IPublishedEvent<IngredientPercentageUpdatedEvent> @event)
        {
            PublishIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        public void Handle(IPublishedEvent<IngredientDeletedEvent> @event)
        {
            PublishIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        public void Handle(IPublishedEvent<IngredientColorUpdatedEvent> @event)
        {
            PublishIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        public void Handle(IPublishedEvent<IngredientDescriptionUpdatedEvent> @event)
        {
            PublishIngredientsUpdated(@event.Payload, @event.EventSourceId);
        }

        private void PublishIngredientsUpdated(IIngredientsUpdatedEvent @event, Guid pieId)
        {                       
            var ingredients = @event.Ingredients.Select(ingredient => ToDto(ingredient));
            var filler = ToDto(@event.Filler);
            SendTo(pieId).ingredientsUpdated(new { ingredients, filler });                    
        }

        private dynamic ToDto(Ingredient ingredient)
        {
            return new
            {
                id = ingredient.Id,
                percent = ingredient.Percent,
                description = ingredient.Description,
                color = ingredient.Color
            };
        }

        public void Handle(IPublishedEvent<ProposedIngredientPercentageChangedEvent> @event)
        {
            PublishPercentageChanged(@event.Payload.Id, @event.Payload.AcceptedPercent, string.Empty, @event.EventSourceId);
        }

        public void Handle(IPublishedEvent<PercentageRejectedEvent> @event)
        {
            PublishPercentageChanged(@event.Payload.Id, @event.Payload.CurrentPercent, Resources.PercentRejected, @event.EventSourceId);
        }

        private void PublishPercentageChanged(Guid ingredientId, int updatedPercentage, string message, Guid pieId)
        {
            SendTo(pieId).percentageChanged (new { id = ingredientId,
                                                   currentPercent = updatedPercentage, 
                                                   message } );            
        }

        public void Handle(IPublishedEvent<MaxIngredientsReachedEvent> @event)
        {
            SendTo(@event.EventSourceId).showMessage(@event.Payload.Message);
        }

        public void Handle(IPublishedEvent<PieDeletedEvent> @event)
        {
            SendTo(@event.Payload.Owner).pieDeleted(new { id = @event.EventSourceId });
        }

        private dynamic SendTo(Guid pieId)
        {
            return SendTo(pieId.ToString());
        }

        private dynamic SendTo(IUser user)
        {
            return SendTo(user.Id);
        }

        private dynamic SendTo(string name)
        {
            //Need to resolve context as this is called via event bus and not created by signalr
            return GlobalHost.ConnectionManager.GetHubContext<PieHub>().Clients.Group(name);
        }

        public Task Join(string pieId)
        {
            //No need to resolve context as this is created by signalr so context is already set
            return Groups.Add(Context.ConnectionId, pieId);
        }
    }
}