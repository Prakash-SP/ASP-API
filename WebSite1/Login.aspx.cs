using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        username.Attributes.Add("autocomplete", "off");
    }
    protected void login_Click(object sender, EventArgs e)
    {
        String UserName = username.Text;
        String Password = password.Text;
        try
        {
            con.Open();
            string query = "Select * from djCustomer where code = '" + UserName + "' AND code = '" + Password + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                String code = (string)sdr["code"];
                Session["code"] = code;
                Response.Redirect("Home.aspx");
            }
            else
            {
                Literal1.Text = "Invalid user name and password";
            }
        }
        catch (Exception ex)
        {
            Literal1.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }


        //if ((UserName == "admin") && (Password == "admin"))
        //{
        //    Response.Redirect("Home.aspx");
        //}
        //else if ((UserName == "") && (Password == ""))
        //{
        //    Literal1.Text = "Please enter user name and password";
        //}
        //else if(UserName != "admin"){
        //    Literal1.Text = "User Does Not Exist";
        //}
        //else if (Password != "admin"){
        //    Literal1.Text = "Incorrect Password";
        //}
        //else
        //{
        //    Literal1.Text = "User Does Not Exist";
        //}
    }
}