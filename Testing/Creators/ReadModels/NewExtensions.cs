using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.ReadModels
{
    public static class NewExtensions
    {
        public static NewReadModels ReadModels(this New @new)
        {
            return new NewReadModels(@new);
        }

        public class NewReadModels
        {
            public New New { get; private set; }

            public NewReadModels(New @new)
            {
                New = @new;
            }

            public PieCreator Pie()
            {
                return new PieCreator(New.Context);
            }

            public IngredientCreator Ingredient()
            {
                return new IngredientCreator(New.Context);
            }
        }
    }
}