using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

public partial class SqlTranscationDemo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["reg"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        SqlCommand insertStudentCmd = new SqlCommand("Insert into student (name, address) values ('"+sName.Text+"','"+ sAddress.Text +"')", con);
        SqlCommand searchStudentCmd = new SqlCommand("Select sid from student");
        String SID = searchStudentCmd.CommandText;
        SqlCommand insertCourseCmd = new SqlCommand("Insert into course (cid,courseName,duration,sid) values ('"+ cID.Text +"','"+ cName.Text +"','"+ cDuration.Text +"','"+SID+"')", con);
        try
        {

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}