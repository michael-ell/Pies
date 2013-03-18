using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;

namespace Codell.Pies.Fakes
{
    public class FakeCommandService : ICommandService
    {
        public void Execute(ICommand command)
        {
            //Do nothing
        }
    }
}