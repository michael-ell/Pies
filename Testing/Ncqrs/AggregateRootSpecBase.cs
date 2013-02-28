using System;
using System.Collections.Generic;
using System.Linq;
using Codell.Pies.Testing.BDD;
using FluentAssertions;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Sourcing;

namespace Codell.Pies.Testing.Ncqrs
{
    public abstract class AggregateRootSpecBase<TRoot> : ContextBase<TRoot>, IPublishedEventsViewer where TRoot : AggregateRoot
    {
        protected virtual IAggregateRootCreationStrategy CreationStrategy
        {
            get { return new SimpleAggregateRootCreationStrategy(); }
        }

        private readonly List<UncommittedEvent> _publishedEvents;

        protected bool TrackPublishedEvents { get; set; }

        public IEnumerable<UncommittedEvent> PublishedEvents
        {
            get { return _publishedEvents; }
        }

        protected AggregateRootSpecBase()
        {
            _publishedEvents = new List<UncommittedEvent>();
            TrackPublishedEvents = true;
        }

//        protected virtual CommittedEventStream History()
//        {
//            return new CommittedEventStream(new Guid());
//        }

        protected override void BeforeCreateSut()
        {
            AggregateRoot.RegisterThreadStaticEventAppliedCallback(OnEventApplied);
        }

        protected override TRoot CreateSut()
        {
            return CreationStrategy.CreateAggregateRoot<TRoot>();
        }

        private void OnEventApplied(AggregateRoot ar, UncommittedEvent e)
        {
            if (TrackPublishedEvents)
                _publishedEvents.Add(e);
        }

        protected override void AfterCreateSut()
        {
            //Sut.EventApplied += (s, e) => _publishedEvents.Add(e.Event);
            //Sut.InitializeFromHistory(History());
        }

        protected override void BeforeGiven()
        {
            TrackPublishedEvents = false;
        }

        protected override void AfterGiven()
        {
            TrackPublishedEvents = true;
        }

        protected PublishedVerifyer<TEvent> Verify<TEvent>(Predicate<TEvent> wasPublished) where TEvent : SourcedEvent
        {
            return new PublishedVerifyer<TEvent>(wasPublished, this);
        }

        protected PublishedVerifyer<TEvent> Verify<TEvent>() where TEvent : SourcedEvent
        {
            return new PublishedVerifyer<TEvent>(this);
        }

        protected class PublishedVerifyer<TEvent> where TEvent : SourcedEvent
        {
            private readonly Predicate<TEvent> _predicate;
            private readonly IPublishedEventsViewer _viewer;

            public PublishedVerifyer(Predicate<TEvent> predicate, IPublishedEventsViewer viewer)
            {
                if (predicate == null) throw new ArgumentNullException("predicate");
                if (viewer == null) throw new ArgumentNullException("viewer");
                _predicate = predicate;
                _viewer = viewer;
            }

            public PublishedVerifyer(IPublishedEventsViewer viewer)
            {
                if (viewer == null) throw new ArgumentNullException("viewer");
                _predicate = e => true;
                _viewer = viewer;
            }

            private IEnumerable<TEvent> EventsOfInterest
            {
                get
                {
                    return _viewer.PublishedEvents
                                  .Where(e => e.Payload.GetType() == typeof(TEvent))
                                  .Select(e => e.Payload)
                                  .Cast<TEvent>();
                }
            }

            public void WasPublished()
            {
                var events = EventsOfInterest;
                events.Where(e => _predicate.Invoke(e)).Should().HaveCount(1);
            }

            public void WasNotPublished()
            {
                var events = EventsOfInterest;
                events.Where(e => _predicate.Invoke(e)).Should().HaveCount(0);
            }
        }
    }
}