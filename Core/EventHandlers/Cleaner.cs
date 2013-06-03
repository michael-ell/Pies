using System;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.Repositories;
using Common.Logging;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Core.EventHandlers
{
    public class Cleaner : IEventHandler<PieCreatedEvent>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IDeleteEmptyPies _deleteEmptyPies;
        private readonly ISettings _settings;

        public Cleaner(IDeleteEmptyPies deleteEmptyPies, ISettings settings)
        {
            Verify.NotNull(deleteEmptyPies, "deleteEmptyPies");
            Verify.NotNull(settings, "settings");
            
            _deleteEmptyPies = deleteEmptyPies;
            _settings = settings;
        }

        public void Handle(IPublishedEvent<PieCreatedEvent> evnt)
        {
            try
            {
                var lifetime = _settings.Get<int>(Keys.EmptyPieLifetimeMinutes);
                if (lifetime > 0)
                {
                    _deleteEmptyPies.Before(DateTime.Now.AddMinutes(lifetime * -1));
                }
            }
            catch (Exception e)
            {
                Log.Error(Resources.FailedToPurgeEmptyPies, e);
            }  
        }
    }
}