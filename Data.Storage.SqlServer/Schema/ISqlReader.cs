using System.Collections.Generic;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public interface ISqlReader
    {
        IEnumerable<string> ReadSqlScripts();
    }
}