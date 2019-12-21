using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0)
        {
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string saveLoc = Server.MapPath("upload") + "\\" + filename;
            try
            {
                FileUpload1.PostedFile.SaveAs(saveLoc);
                fileMsg.Text = "File Uploaded Successfully";
            }
            catch (Exception ex)
            {
                fileMsg.Text = ex.Message;
            }
        }
        else
        {
            fileMsg.Text = "Please upload a file.";
        }
    }
}