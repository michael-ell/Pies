using System;
using Codell.Pies.Common;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;

namespace Codell.Pies.Data.Storage.InMemory
{
    public class DeleteEmptyPies : IDeleteEmptyPies
    {
        private readonly IRepository _repository;

        public DeleteEmptyPies(IRepository repository)
        {
            Verify.NotNull(repository, "repository");            
            _repository = repository;
        }

        public void Before(DateTime value)
        {
            var toDelete = _repository.Find<Pie>(pie => pie.IsEmpty && pie.CreatedOn < value);
            foreach (var pie in toDelete)
            {
                _repository.DeleteById<Guid, Pie>(pie.Id);
            }
        }
    }
}