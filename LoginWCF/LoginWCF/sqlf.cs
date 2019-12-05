using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LoginWCF
{
    public class sqlf
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
        public bool ChkIfExist(string name,string condition)
        {
            if (condition == "tbl")
            {
                string query = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N''" + name + "'_Acm'";
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    if(res==1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                string query = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N''" + name + "'_Acm'";
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    if (res == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}