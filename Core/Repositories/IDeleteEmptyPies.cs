using System;

namespace Codell.Pies.Core.Repositories
{
    public interface IDeleteEmptyPies
    {
        void Before(DateTime datetime);
    }
}