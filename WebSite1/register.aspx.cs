using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
public partial class register : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["reg"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showUserList();
        }

    }
    protected void bt_register_Click(object sender, EventArgs e)
    {
        string firstName = fname.Text,
            middleName = mName.Text,
            lastName = lname.Text,
            DOB = dob.Text,
            emailID = email.Text,
            contactNumber = contactNo.Text,
            genderVal = gender.SelectedValue,
           _address = address.Text,
           _pass = password.Text,
           _repass = rePassword.Text;
        if(FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0)
        {
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string ext = Path.GetExtension(FileUpload1.FileName);
            string saveLoc = Server.MapPath("upload") + "\\" + filename;
            if (ext == ".jpg" || ext == ".png")
            {
                try
                {
                    FileUpload1.PostedFile.SaveAs(saveLoc);
                    con.Open();
                    string query = "Insert into dheeraj ( fName, mName, lName, dob, email, contactNo, gender, address, password, rePassword) values ('" + firstName + "','" + middleName + "','" + lastName + "', '" + DOB + "', '" + emailID + "', '" + contactNumber + "', '" + genderVal + "', '" + _address + "', '" + _pass + "', '" + _repass + "' )";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    Literal1.Text = "User Registered Successfully";
                    Response.Redirect("register.aspx");
                    showUserList();

                    //Literal1.Text = "File Uploaded Successfully";


                    //    if (firstName != null && lastName != null && DOB != null && emailID != null && contactNumber != null && genderVal != null && _pass != null && _repass != null)
                    //    {
                    //        lb_fName.Text = firstName;
                    //        lb_mName.Text = middleName;
                    //        lb_lName.Text = lastName;
                    //        lb_dob.Text = DOB;
                    //        lb_email.Text = emailID;
                    //        lb_contactNo.Text = contactNumber;
                    //        lb_gender.Text = genderVal;
                    //        lb_address.Text = _address;
                    //        lb_password.Text = _pass;
                    //        lb_repassword.Text = _repass;

                    //        Image1.ImageUrl = "~/upload/" + Path.GetFileName(FileUpload1.FileName);
                    //    }
                }
                catch (Exception ex)
                {
                    Literal1.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                Literal1.Text = "Only accept .jpg file";
            }
        }        
    }

    protected void showUserList()
    {
        try
        {
            string query = "Select id, fName, mName, lName, dob, contactNo, email, gender, address from dheeraj";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Literal1.Text = ex.Message;
        }
    }

    protected void editRow(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        showUserList();
        GridView1.Rows[e.NewEditIndex].FindControl("tb_firstName").Focus();
    }
    protected void updateRow(object sender, GridViewUpdateEventArgs e)
    {
        System.Web.UI.WebControls.Label lb_userID = (System.Web.UI.WebControls.Label)GridView1.Rows[e.RowIndex].FindControl("lb_userID");
        System.Web.UI.WebControls.TextBox tb_fName = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_FirstName");
        System.Web.UI.WebControls.TextBox tb_mName = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_MiddleName");
        System.Web.UI.WebControls.TextBox tb_lName = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_LastName");
        System.Web.UI.WebControls.TextBox tb_email = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Email");
        System.Web.UI.WebControls.TextBox tb_contact = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_ContactNumber");
        System.Web.UI.WebControls.TextBox tb_gender = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Gender");
        System.Web.UI.WebControls.TextBox tb_address = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Address");


        try
        {
            con.Open();
            string updateQuery = "Update dheeraj set fN = '" + tb_fName.Text + "', mName = '" + tb_mName.Text + "', lName = '" + tb_lName.Text + "', email = '" + tb_email.Text + "', contactNo = '" + tb_contact.Text + "', gender = '" + tb_gender.Text + "', address = '" + tb_address.Text + "' where id = '" + lb_userID.Text + "' ";
            SqlCommand cmd = new SqlCommand(updateQuery, con);
            cmd.ExecuteNonQuery();
            // regTableMsg.Text = "Update Successfully";
            MessageBox.Show("Update Successfully");
            con.Close();
            Response.Redirect("register.aspx");
            showUserList();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"Error");
        }
        finally
        {
            con.Close();
        }

    }
    protected void deleteRow(object sender, GridViewDeleteEventArgs e)
    {
        System.Web.UI.WebControls.Label lb_userID = (System.Web.UI.WebControls.Label)GridView1.Rows[e.RowIndex].FindControl("lb_userID");
              
        if (MessageBox.Show("Are you sure ?","Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
        {
            try
            {   
                con.Open();
                string deleteQuery = "delete FROM dheeraj where id ='" + lb_userID.Text + "'";
                SqlCommand cmd = new SqlCommand(deleteQuery, con);
                cmd.ExecuteNonQuery();
                showUserList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            MessageBox.Show("User is not removed");
        }
    }

    protected void canceledit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        showUserList();        
    }
}