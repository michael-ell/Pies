using System.Reflection;
using Codell.Pies.Common;
using Common.Logging;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Codell.Pies.Data.Storage.SqlServer
{
    public static class SessionFactory
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
      
        public static ISessionFactory Build(string connectionString)
        {
            return Build(connectionString, Assembly.GetExecutingAssembly());
        }

        public static ISessionFactory Build(string connectionString, Assembly mappingAssembly)
        {
            Verify.NotWhitespace(connectionString, "connectionString");
            
            Log.Warn("Building NHibernate SessionFactory.");
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            var configuration = MsSqlConfiguration.MsSql2008.ConnectionString(connectionString);
            if (LogManager.GetLogger("NHibernate.SQL").IsDebugEnabled)
                configuration = configuration.ShowSql().FormatSql();

            return Fluently.Configure()
                           .Database(configuration)                           
                           .Mappings(m => m.FluentMappings.AddFromAssembly(mappingAssembly))
                           .BuildSessionFactory();            
        }
    }
}
