using System;
using Xunit;

namespace Codell.Pies.Tests.Integration.Data.Storage.SqlServer.PersistFixtures
{
    public class Persister<T> : FixtureContextAware where T : class
    {
        private T _persister;

        public Persister(IFixtureContext context, T creation) : base(context)
        {
            Creation = creation;
        }

        protected T Creation
        {
            get { return _persister; }
            set
            {
                if (Current.Get<T>() != value)
                {
                    Current.Push(value);
                }
                _persister = value;
            }
        }

        /// <summary>
        /// Persists the changes to the database and clears the session
        /// </summary>
        /// <returns>Entity saved</returns>
        public T Save()
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(Creation);
                transaction.Commit();
            }
            Session.Flush();
            Session.Clear();             
            return Creation;
        }

        /// <summary>
        /// Get an entity from the database by an id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>The entity found</returns>
        public T GetById(object id)
        {
            using (var trans = Session.BeginTransaction())
            {
                Creation = Session.Get<T>(id);
                trans.Commit();
            }
            return this;
        }

        /// <summary>
        /// Performs an action against the current entity
        /// </summary>
        /// <param name="operation">Action to perform</param>
        /// <returns>Persister for chaining</returns>
        public Persister<T> Perform(Action<T> operation)
        {
            operation.Invoke(Creation);
            return this;
        }

        /// <summary>
        /// Performs a true assertion on a predicate using the current entity
        /// </summary>
        /// <param name="predicate">Predicate used for the true assertion</param>
        public void Verify(Predicate<T> predicate)
        {
            Assert.True(predicate.Invoke(Creation));
        }

        public static implicit operator T(Persister<T> creator)
        {
            if (creator._persister == null)
                throw new Exception(String.Format("Creation of {0} is null, it probably shouldn't be.", typeof(T)));
            creator.Current.Pop<T>();
            return creator._persister;
        }
    }
}