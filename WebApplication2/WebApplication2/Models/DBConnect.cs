using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public abstract class Sqlf
    {
        private static string ConString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            }
        }

        public SqlDataReader GetDataReader(string str)
        {
            SqlDataReader rdr;
            using (SqlCommand cmd = new SqlCommand(str, new SqlConnection(ConString)))
            {
                cmd.Connection.Open();
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            return rdr;
        }

        public DataTable GetDataTable(string str)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(str, new SqlConnection(ConString)))
            {
                da.Fill(dt);
            }
            return dt;
        }

        public int SqlExecute(string str)
        {
            int Count;
            using (SqlCommand cmd = new SqlCommand(str, new SqlConnection(ConString)))
            {
                if (cmd.Connection.State == ConnectionState.Closed) { cmd.Connection.Open(); }
                Count = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            return Count;
        }

        public string ExecuteScalar(string str)
        {
            string Result = "";
            using (SqlCommand cmd = new SqlCommand(str, new SqlConnection(ConString)))
            {
                cmd.Connection.Open();
                cmd.CommandTimeout = 200;
                Result = Convert.ToString(cmd.ExecuteScalar());
                cmd.Connection.Close();
            }
            return Result ?? "";
        }

        public int ExecuteNonQuery(string str)
        {
            int Count;
            using (SqlCommand cmd = new SqlCommand(str, new SqlConnection(ConString)))
            {
                if (cmd.Connection.State == ConnectionState.Closed) { cmd.Connection.Open(); }
                Count = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            return Count;
        }

        public int ExecuteNonQuery(string str, SqlConnection con, SqlTransaction SqlTrans)
        {
            int Count;
            using (SqlCommand cmd = new SqlCommand(str, con, SqlTrans))
            {
                Count = cmd.ExecuteNonQuery();
            }
            return Count;
        }

        public abstract string CheckAuthorization(string TokenStr = "");

        public abstract void LogError(Exception ex);

        public abstract void WriteLog(string message, string lFileName = "ErrorLog.txt");
    }
}