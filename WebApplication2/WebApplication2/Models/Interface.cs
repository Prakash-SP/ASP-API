using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    interface Ilib
    {
        string CheckAuthorization(string TokenStr = "");

        void LogError(Exception ex);

        void WriteLog(string message, string lFileName = "ErrorLog.txt");

        SqlDataReader GetDataReader(string str);

        DataTable GetDataTable(string str);

        int SqlExecute(string str);

        string ExecuteScalar(string str);

        int ExecuteNonQuery(string str);

        int ExecuteNonQuery(string str, SqlConnection con, SqlTransaction SqlTrans);
    }
}