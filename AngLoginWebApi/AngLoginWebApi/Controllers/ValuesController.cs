using AngLoginWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngLoginWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        Ilib lib;
        public ValuesController()
        {
            lib = new Lib();
        }
        string token = null;
        SqlDataReader dr;

        [HttpGet]
        [Route("test")]
        public string test(string Dbname, string userId)
        {
            string query = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + Dbname + "_Acm'";
            try
            {
                string res = lib.ExecuteScalar(query);
                if (Convert.ToInt32(res) == 1)
                {
                    string userqry = "SELECT count(*) FROM WebLogins WHERE DbName = '" + userId + "'";
                    try
                    {
                        string ures = lib.ExecuteScalar(userqry);
                        if (Convert.ToInt32(ures) == 1)
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
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                        (HttpStatusCode.InternalServerError, ex));
            }
            catch (Exception ex)
            {
                var errorMessagError = new System.Web.Http.HttpError(ex.Message) { { "ErrorCode", HttpStatusCode.InternalServerError } };

                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, errorMessagError));
            }
        }

        [HttpPost]
        [Route("RegEmp")]
        public string RegEmp([FromBody] string Id, string Name, string Dob, string Gender, string Email, string Post, long MobileNo, string Image, string ImageName)
        {
            {
                string query = "insert into spEmpData values ('" + Id + "','" + Name + "','" + Dob + "','" + Gender + "','" + Email + "','" + Post + "','" + MobileNo + "','" + Image + "','" + ImageName + "')";
                try
                {
                    int res = lib.ExecuteNonQuery(query);
                    return res.ToString();
                }
                catch (HttpResponseException ex)
                {
                    throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                            (HttpStatusCode.InternalServerError, ex));
                }
                catch (Exception ex)
                {
                    var errorMessagError = new System.Web.Http.HttpError(ex.Message) { { "ErrorCode", HttpStatusCode.InternalServerError } };

                    throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                        (HttpStatusCode.InternalServerError, errorMessagError));
                }
            }
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public List<EmpData> GetAllEmployees()
        {
            List<EmpData> dataList = new List<EmpData>();
            try
            {
                string tblqry = "select * from spEmpData";
                dr = lib.GetDataReader(tblqry);
                if (dr.HasRows)
                {
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
                        });
                    }
                    return dataList;
                }
                else
                {
                    return dataList;
                }
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                        (HttpStatusCode.InternalServerError, ex));
            }
            catch (Exception ex)
            {
                var errorMessagError = new System.Web.Http.HttpError(ex.Message) { { "ErrorCode", HttpStatusCode.InternalServerError } };

                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, errorMessagError));
            }
        }

        [HttpPut]
        [Route("UpdateEmpData")]
        public string UpdateEmpData(string Id, string Name, string Dob, string Gender, string Email, string Post, long MobileNo, string Image, string ImageName)
        {
            string updateQuery = "Update spEmpData set Name='" + Name + "', Dob = '" + Dob + "', Gender='" + Gender + "', Email = '" + Email + "', Post='" + Post + "', MobileNo='" + MobileNo + "', Image='" + Image + "',ImageName='" + ImageName + "' where EmpId='" + Id + "'";
            try
            {
                int res = lib.ExecuteNonQuery(updateQuery);
                return res.ToString();
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                        (HttpStatusCode.InternalServerError, ex));
            }
            catch (Exception ex)
            {
                var errorMessagError = new System.Web.Http.HttpError(ex.Message) { { "ErrorCode", HttpStatusCode.InternalServerError } };

                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, errorMessagError));
            }
        }

        [HttpDelete]
        [Route("DeleteEmpData")]
        public string DeleteEmpData(string Id)
        {
            string query = "delete from spEmpData where EmpId = '" + Id + "'";
            try
            {
                int res = lib.ExecuteNonQuery(query);
                return res.ToString();
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                        (HttpStatusCode.InternalServerError, ex));
            }
            catch (Exception ex)
            {
                var errorMessagError = new System.Web.Http.HttpError(ex.Message) { { "ErrorCode", HttpStatusCode.InternalServerError } };

                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, errorMessagError));
            }
        }

        [HttpGet]
        [Route("GetEmpById")]
        public List<EmpData> GetEmpById(string Id)
        {
            List<EmpData> dataList = new List<EmpData>();
            try
            {
                //var eId = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["Id"];
                string tblqry = "select * from spEmpData where EmpId = '" + Id + "'";
                dr = lib.GetDataReader(tblqry);
                if (dr.HasRows)
                {
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
                else
                {
                    return dataList;
                }

            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                        (HttpStatusCode.InternalServerError, ex));
            }
            catch (Exception ex)
            {
                var errorMessagError = new System.Web.Http.HttpError(ex.Message) { { "ErrorCode", HttpStatusCode.InternalServerError } };

                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, errorMessagError));
            }
        }

        [HttpDelete]
        [Route("DeleteEmpDataBQ")]
        public string DeleteEmpDataBQ(string Id)
        {
            //var id = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["Id"];
            string query = "delete from spEmpData where EmpId = '" + Id + "'";
            try
            {
                int res = lib.ExecuteNonQuery(query);
                return res.ToString();
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                        (HttpStatusCode.InternalServerError, ex));
            }
            catch (Exception ex)
            {
                var errorMessagError = new System.Web.Http.HttpError(ex.Message) { { "ErrorCode", HttpStatusCode.InternalServerError } };

                throw new HttpResponseException(ControllerContext.Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, errorMessagError));
            }
        }
    }
}