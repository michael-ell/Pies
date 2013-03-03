using System;
using System.Collections;
using System.Collections.Generic;

namespace Codell.Pies.Tests.Integration.Data.Storage.SqlServer.PersistFixtures
{
    public class Current
    {
        private readonly Dictionary<Type, Stack> _currentMap = new Dictionary<Type, Stack>();

        public void Push<T>(T current)
        {
            GetStack<T>().Push(current);
        }

        public T Pop<T>()
        {
            return (T) GetStack<T>().Pop();
        }

        public T Get<T>() where T : class
        {
            if (GetStack<T>().Count == 0)
            {
                return null;
            }

            return (T) GetStack<T>().Peek();
        }

        private Stack GetStack<T>()
        {
            if (!_currentMap.ContainsKey(typeof (T)))
            {
                _currentMap[typeof (T)] = new Stack();
            }

            return _currentMap[typeof (T)];
        }
    }
}