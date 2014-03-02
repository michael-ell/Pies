using System;
using Codell.Pies.Common;
using Codell.Pies.Core.ReadModels;
using Codell.Pies.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Codell.Pies.Data.Storage.Mongo
{
    public class DeleteEmptyPies : IDeleteEmptyPies
    {
        private readonly ICollectionFactory _factory;

        public DeleteEmptyPies(ICollectionFactory factory)
        {
            Verify.NotNull(factory, "factory");            
            _factory = factory;
        }

        public void Before(DateTime value)
        {
            var query = Query.And(Query.EQ("IsEmpty", true), Query.LT("CreatedOn", new BsonDateTime(value.ToUniversalTime())));
            var result = _factory.GetCollection<Pie>().Remove(query);
            if (result != null && !result.Ok)
                throw new ApplicationException(result.LastErrorMessage);
        }
    }
}