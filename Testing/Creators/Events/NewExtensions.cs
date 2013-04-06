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

            public IngredientAddedEventCreator IngredientAddedEvent()
            {
                return new IngredientAddedEventCreator(New.Context);
            }
        }
    }
}