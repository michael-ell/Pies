using System;

namespace Codell.Pies.Common
{
    public class NullServiceLocator : IServiceLocator
    {
        public bool TryFind<T>(out T service)
        {
            service = default(T);
            return false;
        }

        public T Find<T>()
        {
            return default(T);
        }

        public object Find(Type type, string name)
        {
            return null;
        }
    }
}