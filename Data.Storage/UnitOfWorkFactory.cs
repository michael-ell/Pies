using System;
using Codell.Pies.Common;
using Ncqrs;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.Sourcing.Snapshotting;
using Ncqrs.Eventing.Storage;

namespace Codell.Pies.Data.Storage
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWorkContext CreateUnitOfWork(Guid commandId)
        {
            if (UnitOfWorkContext.Current != null)
            {
                throw new InvalidOperationException(Resources.ExistingUnitOfWork);
            }

            var store = NcqrsEnvironment.Get<IEventStore>();
            //Allow event store to dispatch events when commit is successful vs the unit of work
            var bus = new NonPublishingEventBus();
            var snapshotStore = NcqrsEnvironment.Get<ISnapshotStore>();
            var snapshottingPolicy = NcqrsEnvironment.Get<ISnapshottingPolicy>();
            var creationStrategy = NcqrsEnvironment.Get<IAggregateRootCreationStrategy>();
            var snapshotter = NcqrsEnvironment.Get<IAggregateSnapshotter>();

            var repository = new DomainRepository(creationStrategy, snapshotter);
            var unitOfWork = new UnitOfWork(commandId, repository, store, snapshotStore, bus, snapshottingPolicy);
            UnitOfWorkContext.Bind(unitOfWork);
            return unitOfWork;
        }
    }
}