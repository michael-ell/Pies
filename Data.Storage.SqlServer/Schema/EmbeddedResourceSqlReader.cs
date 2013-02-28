using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Codell.Pies.Common;

namespace Codell.Pies.Data.Storage.SqlServer.Schema
{
    public class EmbeddedResourceSqlReader : ISqlReader
    {
        private readonly Assembly _assembly;
        private readonly Predicate<string> _filter;

        public EmbeddedResourceSqlReader(Assembly assembly) : this(assembly, s => true)
        {
        }

        public EmbeddedResourceSqlReader(Assembly assembly, Predicate<string> filter)
        {
            Verify.NotNull(assembly, "assembly");
            Verify.NotNull(filter, "filter");
            _assembly = assembly;
            _filter = filter;
        }

        public IEnumerable<string> ReadSqlScripts()
        {
            foreach (var resourceName in ResourceNames)
            {
                var resourceStream = _assembly.GetManifestResourceStream(resourceName);
                if (resourceStream == null)
                    throw new MissingManifestResourceException("Could not find embedded sql resource: " + resourceName);
                using (var reader = new StreamReader(resourceStream))
                {
                    yield return reader.ReadToEnd();
                }                    
            }           
        }

        private IEnumerable<string> ResourceNames
        {
            get
            {
                return _assembly.GetManifestResourceNames().Where(_filter.Invoke).OrderBy(name => name).ToList();                
            }
        }

        public string ReadFrom(string resourceName)
         {
             var assembly = Assembly.GetCallingAssembly();
             var resourceStream = assembly.GetManifestResourceStream(resourceName);
             if (resourceStream == null)
                 throw new MissingManifestResourceException("Could not find embedded sql resource: " + resourceName);
             string sql;
             using (var reader = new StreamReader(resourceStream))
             {
                 sql = reader.ReadToEnd();
             }
             return sql;
         }
    }
}