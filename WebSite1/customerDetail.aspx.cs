using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Configuration;



public partial class customerDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showCustomer();
        }
    }
    protected void showCustomer()
    {
        try
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateCertificate;
            
            string ApiURL = "https://192.168.1.202/EasySolAPI/EsDataService.svc/customers?ldate=01-Jan-2000";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(ApiURL));
            httpRequest.Headers.Add("Authorization", "782a3a8f-7291-45ed-8c8f-6486d6379175");
            httpRequest.Headers.Add("Key", "PHR");
            httpRequest.Accept = "Application/json";
            httpRequest.ContentType = "Application/json";
            httpRequest.Method = "GET";


            using (HttpWebResponse httpresponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpresponse.GetResponseStream())
                {
                    // Label1.Text = (new StreamReader(stream).ReadToEnd());
                    string json = (new StreamReader(stream).ReadToEnd());
                    GridView1.DataSource = table(json);
                    GridView1.DataBind();
                }

            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public DataTable table(string jsonString)
    {
        var jsonLinq = JObject.Parse(jsonString);
        // Find the first array using Linq
        var linqArray = jsonLinq.Descendants().Where(x => x is JArray).First();
        var jsonArray = new JArray();
        foreach (JObject row in linqArray.Children<JObject>())
        {
            var createRow = new JObject();
            foreach (JProperty column in row.Properties())
            {
                // Only include JValue types
                if (column.Value is JValue)
                {
                    createRow.Add(column.Name, column.Value);
                }

            }
            jsonArray.Add(createRow);
        }

        var _data = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
        
        foreach (DataRow dr in _data.Rows)
        {
            //int rowCount = dr.Table.Rows.Count;
            
            if (dr.Table.Rows.Count > 0)
            {
                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myConn"].ConnectionString))
                {
                    // int selectResult;
                    cn.Open();
                    string sql = "INSERT INTO djCustomer (code, name, city, state, address, address1, address2,pincode, mobile, email, Dlno, gstno, branchcode) VALUES (@code, @name, @city, @state, @address, @address1, @address2, @pincode, @mobile, @email, @Dlno, @gstno, @branchcode)";
                    //string checkData = "Select Count(*) as 'Row Count' from djCustomer";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    //SqlCommand cmd1 = new SqlCommand(checkData, cn);
                    //SqlDataReader dataReader = cmd1.ExecuteReader();
                    //while (dataReader.Read())
                    //{
                    //    selectResult = Int32.Parse(dataReader["Row Count"].ToString());
                    //}

                    //if (rowCount == selectResult)
                    //{
                    //    System.Windows.Forms.MessageBox.Show("Data Already exist in the database");                        
                    //}
                    //else
                    //{
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@code", dr["code"]);
                        cmd.Parameters.AddWithValue("@name", dr["name"]);
                        cmd.Parameters.AddWithValue("@city", dr["city"]);
                        cmd.Parameters.AddWithValue("@state", dr["state"]);
                        cmd.Parameters.AddWithValue("@address", dr["address"]);
                        cmd.Parameters.AddWithValue("@address1", dr["address1"]);
                        cmd.Parameters.AddWithValue("@address2", dr["address2"]);
                        cmd.Parameters.AddWithValue("@pincode", dr["pincode"]);
                        cmd.Parameters.AddWithValue("@mobile", dr["mobile"]);
                        cmd.Parameters.AddWithValue("@email", dr["email"]);
                        cmd.Parameters.AddWithValue("@Dlno", dr["Dlno"]);
                        cmd.Parameters.AddWithValue("@gstno", dr["gstno"]);
                        cmd.Parameters.AddWithValue("@branchcode", dr["branchcode"]);
                        cmd.ExecuteNonQuery();
                    //}                    
                    cn.Close();
                }
            }
            
        }

        return _data;

        //return JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
    }

    public static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
}