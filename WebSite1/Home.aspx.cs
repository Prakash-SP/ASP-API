using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
public partial class Home : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            name.Text = Session["code"].ToString();
            if (name.Text == "")
            {
                showErrMsg.Text = "Erro";
            }
            showData();
            this.dataReaderExample();
            dataTableExample();
        }
        
       
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("Default.aspx");
    }

    protected void showData()
    {
        try
        {
            //con.Open();
            string selectQuery = "Select top 10 barcode, name, compname, pack, Clqty from item where code > 0";
            SqlCommand cmd = new SqlCommand(selectQuery, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                showErrMsg.Text = "There is no record in the Table";
            }
        }
        catch (Exception ex)
        {
            showErrMsg.Text = ex.Message;
        }
        finally
        {
            //con.Close();
        }
    }
    protected void editRow(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        showData();


        GridView1.Rows[e.NewEditIndex].FindControl("tb_qty").Focus();
    }
    protected void updateRow(object sender, GridViewUpdateEventArgs e)
    {
        Label lb_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lb_code");

        TextBox tb_qty = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_qty");

        GridView1.EditIndex = -1;

        try
        {
            con.Open();
            string updateQuery = "Update item set Clqty = '" + tb_qty.Text + "' where barcode = '" + lb_code.Text + "' ";
            SqlCommand cmd = new SqlCommand(updateQuery, con);
            cmd.ExecuteNonQuery();
            showErrMsg.Text = "Update Successfully";
            con.Close();
            showData();
        }
        catch (Exception ex)
        {
            showErrMsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }

    }
    protected void deleteRow(object sender, GridViewDeleteEventArgs e)
    {
        Label lb_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lb_code");
        try
        {
            con.Open();
            string deleteQuery = "delete FROM item where barcode='" + lb_code.Text + "'";
            SqlCommand cmd = new SqlCommand(deleteQuery, con);
            cmd.ExecuteNonQuery();
            showData();
        }
        catch (Exception ex)
        {
            showErrMsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }

    protected void dataReaderExample()
    {
        try
        {
            con.Open();
            string query = "Select top 5 barcode, name from item where code > 0";
            SqlCommand cmd = new SqlCommand(query, con);
            using(SqlDataReader dr = cmd.ExecuteReader())
            {
                //DataTable dt = new DataTable();
                //dt.Columns.Add("Code");
                //dt.Columns.Add("Name");
                //while (dr.Read())
                //{
                //    DataRow dataRow = dt.NewRow();
                //    dataRow["Code"] = dr["barcode"];
                //    dataRow["Name"] = dr["name"];
                //    dt.Rows.Add(dataRow);
                //}
                GridView2.DataSource = dr;
                GridView2.DataBind();
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
    }

    protected void dataTableExample()
    {
        try
        {
            con.Open();
            string query = "Select top 5 compname from item where code > 0";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Company Name");
            while (dr.Read())
            {
                DataRow dataRow = dt.NewRow();
                dataRow["Company Name"] = dr["compname"];
                dt.Rows.Add(dataRow);
            }
            GridView3.DataSource = dt;
            GridView3.DataBind(); 
            dr.Close();
         }
        catch (Exception ex)
        {
            Literal2.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
}