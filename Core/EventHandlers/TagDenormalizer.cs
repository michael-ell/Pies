using System;
using System.Linq;
using Codell.Pies.Core.Events;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using Ncqrs.Eventing.ServiceModel.Bus;
using Codell.Pies.Common;

namespace Codell.Pies.Core.EventHandlers
{
    public class TagDenormalizer : IEventHandler<PieTagsUpdatedEvent>
    {
        private readonly IRepository _repository;

        public TagDenormalizer(IRepository repository)
        {
            Verify.NotNull(repository, "repository");
            
            _repository = repository;
        }

        public void Handle(IPublishedEvent<PieTagsUpdatedEvent> evnt)
        {
            var newTags = evnt.Payload.NewTags;
            if (newTags.IsEmpty()) return;

            var tags = _repository.Project<Tag, Domain.Tag>(tag => new Domain.Tag(tag.Value));
            var toAdd = newTags.Except(tags);
            foreach (var tag in toAdd)
            {
                //ToDo: Bulk insert?
                _repository.Save(new Tag{Id = Guid.NewGuid(), Value = tag});
            }
        }
    }
}