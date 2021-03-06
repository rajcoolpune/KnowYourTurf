using System;
using System.Data;
using FluentNHibernate;
using KnowYourTurf.Core.Config;
using NHibernate;

namespace Generator
{
    public static class SqlServerHelper
    {
        public static void KillAllFKs(ISessionFactory source)
        {
            using (ISession session = source.OpenSession(new SaveUpdateInterceptorWithCompanyFilter()))
            {
                try
                {
                    var sql =
                        @"declare @tablename sysname, @constraint sysname
declare FK_KILLER CURSOR FOR 
SELECT fk.table_name, c.constraint_name 
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C 
JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
OPEN FK_KILLER
FETCH NEXT FROM FK_KILLER INTO @tablename, @constraint
WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT 'alter table ' + @tablename + ' drop constraint ' + @constraint
	EXECUTE ('alter table ' + @tablename + ' drop constraint ' + @constraint + ';')
	FETCH NEXT FROM FK_KILLER INTO @tablename, @constraint
END
CLOSE FK_KILLER
DEALLOCATE FK_KILLER
";

                    IDbConnection conn = session.Connection;
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}