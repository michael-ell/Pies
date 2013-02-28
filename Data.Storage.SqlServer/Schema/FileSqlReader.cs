using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codell.Pies.Common;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public class FileSqlReader : ISqlReader
    {
        private readonly string _path;

        public FileSqlReader(string path)
        {
            Verify.NotNull(path, "path");
            _path = path;
        }

        public IEnumerable<string> ReadSqlScripts()
        {
            foreach (var fileName in FileNames)
            {
                if (!File.Exists(fileName))
                    throw new FileNotFoundException(string.Format("Could not read sql from {0} as it does not exist.", fileName));
                using (var reader = new StreamReader(fileName))
                {
                    yield return reader.ReadToEnd();
                }
            }   
        }

        private IEnumerable<string> FileNames
        {
            get { return Directory.GetFiles(_path, "*.sql").OrderBy(sqlFile => sqlFile).ToList(); }
        }
    }
}