using System;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Core.EventHandlers;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.Repositories;
using Codell.Pies.Testing.BDD;
using Codell.Pies.Testing.Creators.Events;
using Codell.Pies.Testing.Ncqrs;
using Moq;
using Codell.Pies.Testing.Helpers;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace Codell.Pies.Tests.Core.EventHandlers.CleanerSpecs
{
    [Concern(typeof (Cleaner))]
    public class When_a_pie_is_created : EventHandlerSpecBase<Cleaner>
    {
        private PublishedEvent<PieCreatedEvent> _event;
        private int _expectedLifetime;

        protected override void Given()
        {
            _event = PublishedEvent.For(New.Events().PieCreatedEvent().Creation);
            _expectedLifetime = 20;
            MockFor<ISettings>().Setup(settings => settings.Get<int>(Keys.EmptyPieLifetimeMinutes)).Returns(_expectedLifetime);
        }

        protected override void When()
        {
            Sut.Handle(_event);
        }

        [Observation]
        public void Then_should_clean_up_empty_pies_whose_lifetime_has_expired()
        {
            MockFor<IDeleteEmptyPies>().Verify(deleteEmptyPies =>
                deleteEmptyPies.Before(It.Is<DateTime>(dateTime => dateTime.IgnoreSeconds() == DateTime.Now.AddMinutes(_expectedLifetime * -1).IgnoreSeconds())));
        }
    }
}