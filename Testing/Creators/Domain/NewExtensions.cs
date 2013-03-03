using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Domain
{
    public static class NewExtensions
    {
        public static NewDomain Domain(this New @new)
        {
            return new NewDomain(@new);
        }

        public class NewDomain
        {
            public New New { get; private set; }

            public NewDomain(New @new)
            {
                New = @new;
            }

            public PieCreator Pie()
            {
                return new PieCreator(New.Context);
            }
        }
    }
}