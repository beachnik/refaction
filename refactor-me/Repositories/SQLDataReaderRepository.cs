using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using refactor_me.Models;

namespace refactor_me.Repositories
{
    public abstract class SQLDataReaderRepository
    {
        protected SqlDataReader ExecuteReaderQuery(string cmd)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var sqlcmd = new SqlCommand(cmd, conn);
            return sqlcmd.ExecuteReader();
        }

        protected void ExecuteNonReaderQuery(string cmd)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var sqlcmd = new SqlCommand(cmd, conn);
            sqlcmd.ExecuteNonQuery();
        }
    }
}