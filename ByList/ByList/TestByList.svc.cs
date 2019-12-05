using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.ServiceModel.Web;

namespace ByList
{
      public class TestByList : ITestByList
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

        public List<DataMember> GetItemsAsList()
        {
            List<DataMember> dataList = new List<DataMember>();
            var email = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["code"];

            try
            {
                con.Open();
                string tblqry = "select Name,FatherName,MotherName,MobNo,Dob,Email from shaktireg where Email=@email";
                SqlCommand cmd = new SqlCommand(tblqry, con);
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataList.Add(new DataMember{
                        Name = dr["Name"].ToString(),
                        FatherName = dr["FatherName"].ToString(),
                        Email = dr["Email"].ToString(),
                        MobNo = dr["MobNo"].ToString(),
                        Dob = Convert.ToDateTime(dr["Dob"]).ToString("dd-MMM-yyyy")
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
        public List<Item> GetItem(string code)
        {
            List<Item> ItemList = new List<Item>();
            try {
                con.Open();
                string tblqry = "select Code,Name,Pack,Clqty from Item where code=@ICode";
                SqlCommand cmd = new SqlCommand(tblqry, con);
                cmd.Parameters.AddWithValue("@ICode", code);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ItemList.Add(new Item
                    {
                        Code=dr["Code"].ToString(),
                        Name = dr["Name"].ToString(),
                        Pack = dr["Pack"].ToString(),
                        Qty = dr["Clqty"].ToString(),
                        Status = false
                    });
                }
                return ItemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public List<AcmData> GetItemByCase(string slcd)
        {
            List<AcmData> ItemList = new List<AcmData>();
            try
            {
                con.Open();
                string tblqry = "select code,altercode,name,address + ' ' + address1 + ' ' + address2 as address,CASE WHEN status = '*' OR  flag = '#HIDE' THEN 'LOCK' WHEN name LIKE '~%' THEN 'Manual Lock' ELSE 'Active' END AS LStatus from  acm where slcd=@slcd";
                SqlCommand cmd = new SqlCommand(tblqry, con);
                cmd.Parameters.AddWithValue("@slcd", slcd);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ItemList.Add(new AcmData
                    {
                        Code = dr["Code"].ToString(),
                        AlterCode = dr["altercode"].ToString(),
                        Name = dr["name"].ToString(),
                        Address = dr["address"].ToString(),
                        LStatus = dr["LStatus"].ToString(),
                    });
                }
                return ItemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string InsertItem(string course,int year,int earning)
        {
            string query = "insert into CourseSales values (@crs,@year,@earn)";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@crs", course);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@earn", earning);
                int res=cmd.ExecuteNonQuery();
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
        public List<CourseData> GetCourseData()
        {
            List<CourseData> dataList = new List<CourseData>();
            try
            {
                con.Open();
                string tblqry = "select * from CourseSales";
                SqlCommand cmd = new SqlCommand(tblqry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataList.Add(new CourseData
                    {
                        Id = dr["Id"].ToString(),
                        Course = dr["Course"].ToString(),
                        Year = dr["Year"].ToString(),
                        Earning = dr["Earning"].ToString(),
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
        public string DeleteCourseData(string Id)
        {
            string query = "delete from CourseSales where Id = @Id";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
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
        public string UpdateCourseData(string Id, string Course, string Year, string Earning)
        {
            string updateQuery = "Update CourseSales set Course=@Course, Year = @Year, Earning = @Earning where Id=@Id";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Course", Course);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Earning", Earning);
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
