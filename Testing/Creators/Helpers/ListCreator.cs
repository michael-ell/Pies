using System;
using System.Collections.Generic;
using Codell.Pies.Testing.FluentFixtures;

namespace Codell.Pies.Testing.Creators.Helpers
{
    public class ListCreator<T> : Creator<List<T>> where T : new()
    {
        private readonly Func<T> _creator;

        public ListCreator(IFixtureContext context) : base(context, new List<T>())
        {
        }

        public ListCreator(IFixtureContext context, Func<T> creator) : this(context)
        {
            _creator = creator;
        }

        public List<T> Fill(int numberToCreate)
        {
            for (int i = 0; i < numberToCreate; i++)
                Creation.Add(NewEntity);
            return this;
        }

        private T NewEntity
        {
            get
            {
                if (_creator != null) return _creator.Invoke();
                return new T();
            }
        }
    }
}