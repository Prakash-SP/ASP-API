using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.Serialization.Json;


public partial class apiIntegration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getProductData();
        }
    }

    public void getProductData()
    {
        try
        {
            string ApiURL = "https://192.168.1.202/EasySolAPI/EsDataService.svc/products?stock=Y";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(ApiURL));
            httpRequest.Headers.Add("Authorization", "782a3a8f-7291-45ed-8c8f-6486d6379175");
            httpRequest.Headers.Add("Key", "PHR");
            httpRequest.Accept = "Application/json";
            httpRequest.ContentType = "Application/json";
            httpRequest.Method = "GET";

            ServicePointManager.ServerCertificateValidationCallback = ValidateCertificate;

            using (HttpWebResponse httpresponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpresponse.GetResponseStream())
                {
                    string json = (new StreamReader(stream).ReadToEnd());
                    GridView1.DataSource = table(json);
                    GridView1.DataBind();



                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DataTable table(string jsonString)
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
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myConn"].ConnectionString))
            {
                cn.Open();
                string sql = "INSERT INTO djProduct (code, name, qty, pack, scheme, mrp, Srate, company, category, Cgstper, Sgstper, Igstper, HsnCode) VALUES (@code, @name, @qty, @pack, @scheme, @mrp, @Srate, @company, @category, @Cgstper, @Sgstper, @Igstper, @HsnCode)";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    //Loop through the and get of parameter values
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", dr["code"].ToString());
                    cmd.Parameters.AddWithValue("@name", dr["name"].ToString());
                    cmd.Parameters.AddWithValue("@qty", dr["qty"].ToString());
                    cmd.Parameters.AddWithValue("@pack", dr["pack"].ToString());
                    cmd.Parameters.AddWithValue("@scheme", dr["scheme"].ToString());
                    cmd.Parameters.AddWithValue("@mrp", dr["mrp"].ToString());
                    cmd.Parameters.AddWithValue("@Srate", dr["Srate"].ToString());
                    cmd.Parameters.AddWithValue("@company", dr["company"].ToString());
                    cmd.Parameters.AddWithValue("@category", dr["category"].ToString());
                    cmd.Parameters.AddWithValue("@Cgstper", dr["Cgstper"].ToString());
                    cmd.Parameters.AddWithValue("@Sgstper", dr["Sgstper"].ToString());
                    cmd.Parameters.AddWithValue("@Igstper", dr["Igstper"].ToString());
                    cmd.Parameters.AddWithValue("@HsnCode", dr["hsnCode"].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        return _data;

        //return JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
    }

    //public void insertIntoDb()
    //{
    //    using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myConn"].ConnectionString))
    //    {
    //        cn.Open();
    //        string sql = "INSERT INTO djProduct (code, name, qty, pack, scheme, mrp, Srate, company, category, Cgstper, Sgstper, Igster, HsnCode) VALUES (@code, @name, @qty, @pack, @scheme, @mrp, @Srate, @company, @category, @Cgstper, @Sgstper, @Igstper, @HsnCode)";
    //        using (SqlCommand cmd = new SqlCommand(sql, cn))
    //        {
    //            //Loop through the and get of parameter values
    //            cmd.CommandType = System.Data.CommandType.Text;
    //            cmd.Parameters.AddWithValue("@code", code);
    //            cmd.Parameters.AddWithValue("@name", p.name);
    //            cmd.Parameters.AddWithValue("@qty", p.qty);
    //            cmd.Parameters.AddWithValue("@pack", p.pack);
    //            cmd.Parameters.AddWithValue("@scheme", p.scheme);
    //            cmd.Parameters.AddWithValue("@mrp", p.mrp);
    //            cmd.Parameters.AddWithValue("@Srate", p.Srate);
    //            cmd.Parameters.AddWithValue("@company", p.company);
    //            cmd.Parameters.AddWithValue("@category", p.category);
    //            cmd.Parameters.AddWithValue("@Cgstper", p.Cgstper);
    //            cmd.Parameters.AddWithValue("@Sgstper", p.Sgstper);
    //            cmd.Parameters.AddWithValue("@Igstper", p.Igstper);
    //            cmd.Parameters.AddWithValue("@HsnCode", p.hsnCode);
    //            cmd.ExecuteNonQuery();
    //        }
    //    }
    //}

    public static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
}