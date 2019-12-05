using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.ServiceModel.Web;

namespace LoginWCF
{
    public class Login : ILogin
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
        string token = null;
        public string test(string Dbname,string userId)
        {
            string query = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + Dbname + "_Acm'";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                Int32 res = (Int32)cmd.ExecuteScalar();
                con.Close();
                if (res == 1)
                {
                    string userqry = "SELECT count(*) FROM WebLogins WHERE DbName = '" + userId + "'";
                    try
                    {
                        con.Open();
                        SqlCommand ucmd = new SqlCommand(userqry, con);
                        Int32 ures = (Int32)ucmd.ExecuteScalar();
                        con.Close();
                        if (ures == 1)
                        {
                            token = Guid.NewGuid().ToString(); 
                            //string inserttoken = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + Dbname + "_Acm'";
                            //con.Open();
                            //SqlCommand insertcmd = new SqlCommand(inserttoken, con);
                            //int insertres = inserttoken.ExecuteNonQuery();
                            return token;
                        }
                        else
                        {
                            token = "No User Data Exist";
                            return token;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    token = "No User Data Exist";
                    return token;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public string RegEmp(string Id, string Name, string Dob, string Gender, string Email, string Post, long MobileNo, string Image, string ImageName)
        {
            {
                string query = "insert into spEmpData values (@id,@name,@dob,@gender,@email,@post,@mobileNo,@image,@imageName)";
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.Parameters.AddWithValue("@name", Name);
                    cmd.Parameters.AddWithValue("@dob", Dob);
                    cmd.Parameters.AddWithValue("@gender", Gender);
                    cmd.Parameters.AddWithValue("@email", Email);
                    cmd.Parameters.AddWithValue("@post", Post);
                    cmd.Parameters.AddWithValue("@mobileNo", MobileNo);
                    cmd.Parameters.AddWithValue("@image", Image);
                    cmd.Parameters.AddWithValue("@imageName", ImageName);
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                    return res.ToString();
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
        public List<EmpData> GetAllEmployees()
        {
            List<EmpData> dataList = new List<EmpData>();
            try
            {
                con.Open();
                string tblqry = "select * from spEmpData";
                SqlCommand cmd = new SqlCommand(tblqry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataList.Add(new EmpData
                    {
                        Id = dr["EmpId"].ToString(),
                        Name = dr["Name"].ToString(),
                        Dob =Convert.ToDateTime( dr["Dob"]).ToString("yyyy-MM-dd"),
                        Gender = dr["Gender"].ToString(),
                        Email = dr["Email"].ToString(),
                        Post = dr["Post"].ToString(),
                    });
                }
                return dataList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return null;
        }
        public string UpdateEmpData(string Id, string Name, string Dob, string Gender, string Email, string Post, long MobileNo, string Image, string ImageName)
        {
            string updateQuery = "Update spEmpData set Name=@name, Dob = @dob, Gender=@gender, Email = @email, Post=@post, MobileNo=@mobileNo, Image=@image,ImageName=@imageName where EmpId=@id";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@dob", Dob);
                cmd.Parameters.AddWithValue("@gender", Gender);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@post", Post);
                cmd.Parameters.AddWithValue("@mobileNo", MobileNo);
                cmd.Parameters.AddWithValue("@image", Image);
                cmd.Parameters.AddWithValue("@imageName", ImageName);
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res.ToString();
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
        public string DeleteEmpData(string Id)
        {
            string query = "delete from spEmpData where EmpId = @id";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Id);
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res.ToString();
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
        public List<EmpData> GetEmpById(string Id)
        {
            List<EmpData> dataList = new List<EmpData>();
            try
            {
                //var eId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["Id"];
                con.Open();
                string tblqry = "select * from spEmpData where EmpId = @id";
                SqlCommand cmd = new SqlCommand(tblqry, con);
                cmd.Parameters.AddWithValue("@id", Id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataList.Add(new EmpData
                    {
                        Id = dr["EmpId"].ToString(),
                        Name = dr["Name"].ToString(),
                        Dob = Convert.ToDateTime(dr["Dob"]).ToString("yyyy-MM-dd"),
                        Gender = dr["Gender"].ToString(),
                        Email = dr["Email"].ToString(),
                        Post = dr["Post"].ToString(),
                        MobileNo = (long)dr["MobileNo"],
                        Image = dr["Image"].ToString(),
                        ImageName = dr["ImageName"].ToString(),
                    });
                }
                return dataList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return null;
        }
        public string DeleteEmpDataBQ(string Id)
        {
            //var id = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["Id"];
            string query = "delete from spEmpData where EmpId = @id";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Id);
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res.ToString();
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
