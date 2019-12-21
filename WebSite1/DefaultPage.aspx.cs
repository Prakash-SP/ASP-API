using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        getProductData();
    }
    protected void regBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("register.aspx");
    }
    protected void loginbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
    public void getProductData()
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}