namespace Codell.Pies.Testing.Helpers
{
    using System;
    using System.Collections.Generic;

    public class EntityFactory<TEntity>
    {
        private readonly List<TEntity> _entities;
        private readonly int _num;

        public EntityFactory(int numberOfInstances)
        {
            _num = numberOfInstances < 0 ? 0 : numberOfInstances;
            _entities = new List<TEntity>(numberOfInstances);
        }

        public List<TEntity> Using(Func<int, TEntity> factoryFunc)
        {
            for (int i = 0; i < _num; i++)
                _entities.Add(factoryFunc(i));
            return _entities;
        }
    }

    public static class FactoryExtensions
    {
        public static EntityFactory<TEntity> Of<TEntity>(this int numberOfInstances)
        {
            return new EntityFactory<TEntity>(numberOfInstances);
        }
    }
}