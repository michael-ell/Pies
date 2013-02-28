using System;

namespace Codell.Pies.Common
{
    public interface IServiceLocator
    {
        bool TryFind<T>(out T service);
        T Find<T>();
        object Find(Type type, string name);
    }
}