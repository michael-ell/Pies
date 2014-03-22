using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Events
{
    public static class NewExtensions
    {
        public static NewEvents Events(this New @new)
        {
            return new NewEvents(@new);
        }

        public class NewEvents
        {
            public New New { get; private set; }

            public NewEvents(New @new)
            {
                New = @new;
            }

            public PieCreatedEventCreator PieCreatedEvent()
            {
                return new PieCreatedEventCreator(New.Context);
            }

            public IngredientAddedEventCreator IngredientAddedEvent()
            {
                return new IngredientAddedEventCreator(New.Context);
            }

            public IngredientDeletedEventCreator IngredientDeletedEvent()
            {
                return new IngredientDeletedEventCreator(New.Context);
            }

            public IngredientPercentageUpdatedEventCreator IngredientPercentageUpdatedEvent()
            {
                return new IngredientPercentageUpdatedEventCreator(New.Context);
            }

            public IngredientColorUpdatedEventCreator IngredientColorUpdatedEvent()
            {
                return new IngredientColorUpdatedEventCreator(New.Context);
            }

            public IngredientDescriptionUpdatedEventCreator IngredientDescriptionUpdatedEvent()
            {
                return new IngredientDescriptionUpdatedEventCreator(New.Context);
            }

            public ProposedIngredientPercentageChangedEventCreator ProposedIngredientPercentageChangedEvent()
            {
                return new ProposedIngredientPercentageChangedEventCreator(New.Context);
            }

            public PieTagsUpdatedEventCreator PieTagsUpdatedEvent()
            {
                return new PieTagsUpdatedEventCreator(New.Context);
            }

            public IsPrivateUpdatedEventCreator IsPrivateUpdatedEvent()
            {
                return new IsPrivateUpdatedEventCreator(New.Context);
            }
        }
    }
}