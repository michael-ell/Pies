using System;
using System.Collections.Generic;

namespace Codell.Pies.Data.Storage.Mongo
{
    public class CollectionNameMap : ICollectionNameMap
    {
        private readonly Dictionary<Type, string> _store;

        public CollectionNameMap()
        {
            _store = new Dictionary<Type, string>();
        }

        public ICollectionNameMap Register<T>(string collectionName)
        {
            _store[typeof (T)] = collectionName;
            return this;
        }

        public string GetNameFor<T>()
        {
            string name;
            var type = typeof (T);
            return _store.TryGetValue(type, out name) ? name : type.Name;
        }
    }
}