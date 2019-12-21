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
using System.Text.RegularExpressions;
public partial class createOrder : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
    string itemJson = string.Empty;
    string mJson = string.Empty;
    string ordNo = string.Empty;
    string itemCode = string.Empty;
    string _responseCode = string.Empty;
    string _failure = string.Empty;
    string _venderCode = string.Empty;
    string _itemCode = string.Empty;
    string _message = string.Empty;
    string statusMsg = string.Empty;
    string newClientOrder = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showData();
            ordNo = GenCustOrdNo(newClientOrder);
            tb_orderNo.Text = ordNo;
        }
    }

    public string GenCustOrdNo(string newClientOrder)
    {
        string currentOrdNo = "0";
        connection.Open();
        string query = "select max(orderNo) from djOrder";
        SqlCommand command = new SqlCommand(query, connection);
        currentOrdNo = command.ExecuteScalar().ToString();
        connection.Close();
        if (currentOrdNo == "" || currentOrdNo == "0")
        {
            ordNo = "ESPL10004";
        }
        else
        {
            if (newClientOrder == "Y")
            {
                Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                Match result = re.Match(currentOrdNo);
                string strpart = result.Groups[1].Value;
                int intpart = Convert.ToInt32(result.Groups[2].Value);
                intpart++;
                ordNo = string.Concat(strpart, (intpart.ToString()));

                tb_orderNo.Text = ordNo;
            }
            else
            {
                ordNo = "ESPL10001";
                
            }
            
        }
        return ordNo;
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        List<string> multipleOrder = new List<string>();
        try
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateCertificate;
            string _multipleOrders = string.Empty;
            
            connection.Open();
            SqlCommand getOrderNo = new SqlCommand("Select distinct(orderNo) from djOrder where custID = '"+ Session["code"].ToString() + "'",connection);
            
            SqlDataReader dr = getOrderNo.ExecuteReader();
            while(dr.Read()){
                string orderNos = dr["orderNo"].ToString();
                string fetchOrder = "Select itemName as Item, itemCode as Itemcode, quantity as Qty from djOrder where orderNo = '" + orderNos + "' AND itemStatus = 'AVL'";
                SqlCommand cmdFetchData = new SqlCommand(fetchOrder, connection);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmdFetchData.Connection = connection;
                    sda.SelectCommand = cmdFetchData;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        itemJson = JsonConvert.SerializeObject(new
                        {
                            OrderNo = orderNos,
                            VendorCode = Session["code"].ToString(),
                            OrderDetails = dt
                        });

                        multipleOrder.Add(itemJson);
                    }
                }
            }

            for (int i = 0; i < multipleOrder.Count; i++)
            {
                _multipleOrders = _multipleOrders + "," + multipleOrder[i];
            }
            connection.Close();
            
            
            
            
            //using (SqlCommand cmd = new SqlCommand(fetchOrder))
            //{
            //    using (SqlDataAdapter sda = new SqlDataAdapter())
            //    {
            //        cmd.Connection = connection;
            //        sda.SelectCommand = cmd;
            //        using (DataTable dt = new DataTable())
            //        {
            //            sda.Fill(dt);
            //            itemJson = JsonConvert.SerializeObject(new
            //            {
            //                //OrderNo = tb_orderNo.Text,
            //                OrderNo = dt.Rows,
            //                VendorCode = Session["code"].ToString(),
            //                OrderDetails = dt
            //            });
            //        }
            //    }
            //}
            //mJson = "[" + itemJson + "]";
            _multipleOrders = _multipleOrders.Remove(0, 1);
            mJson = "[" + _multipleOrders + "]"; 
            byte[] bytes = Encoding.UTF8.GetBytes(mJson);
            string ApiURL = "https://192.168.1.202/EasySolAPI/EsOrderService.svc/createorder";

            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(new Uri(ApiURL));
            hwr.Headers.Add("Authorization", "782a3a8f-7291-45ed-8c8f-6486d6379175");
            hwr.Headers.Add("Key", "PHR");
            hwr.Accept = "application/json";
            hwr.ContentType = "application/json";
            hwr.Method = "POST";

            using (Stream stream = hwr.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }

            using (HttpWebResponse httpresponse = (HttpWebResponse)hwr.GetResponse())
            {
                using (Stream stream = httpresponse.GetResponseStream())
                {
                    string orderResponse = (new StreamReader(stream).ReadToEnd());
                    table(orderResponse);
                    //Label1.Text = _message;
                    reset();
                    truncateTable();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable table(string orderResponse)
    {
        string permanentOrder = "Insert into permanentOrder (orderNo,custID,itemCode,quantity,mrp,rate) Select orderNo, custID, itemCode, quantity, mrp, rate from djOrder";
        string orderStatusQuery = "insert into orderStatus (orderNo, status, message) values (@ordNo,@status,@message)";
        DataTable dt = new DataTable();
        JObject ljobj = new JObject();
        JArray ljarr = new JArray();
        JToken lToken;
        int lStatus = -1;
        String Ordno = "", VendorCode = "", ItemCode = "", Message = "";
        try
        {
            ljobj = JObject.Parse(orderResponse);
            lToken = JToken.Parse(ljobj.ToString());
            ljarr = JArray.Parse(ljobj["response"]["OrderResult"].ToString());
            for (int i = 0; i < ljarr.Count; i++)
            {
                ljobj = JObject.Parse(ljarr[i].ToString());
                if (ljobj.ContainsKey("Failure"))
                {
                    ljobj = JObject.Parse(ljarr[i]["Failure"].ToString());
                    if (ljobj.ContainsKey("VendorCode"))
                    {
                        Ordno = ljarr[i]["Failure"]["OrdNo"].ToString();
                        VendorCode = ljarr[i]["Failure"]["VendorCode"].ToString();
                        Message = ljarr[i]["Failure"]["Message"].ToString();
                        if (Message.ToLower().Contains("customer"))
                        {
                            lStatus = 0; //customer does not exist....
                            try
                            {
                                connection.Open();
                                SqlCommand cmd = new SqlCommand(permanentOrder, connection);
                                SqlCommand cmdOrderStatus = new SqlCommand(orderStatusQuery, connection);
                                cmdOrderStatus.Parameters.AddWithValue("@ordNo", Ordno);
                                cmdOrderStatus.Parameters.AddWithValue("@status", lStatus);
                                cmdOrderStatus.Parameters.AddWithValue("@message", Message);
                                cmd.ExecuteNonQuery();
                                cmdOrderStatus.ExecuteNonQuery();
                                Label1.Text = "Vendor Code" + VendorCode + "does not exist";
                                connection.Close();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else
                        {
                            lStatus = 1; //Duplicate order
                            try
                            {
                                connection.Open();
                                SqlCommand cmd = new SqlCommand(permanentOrder, connection);
                                SqlCommand cmdOrderStatus = new SqlCommand(orderStatusQuery, connection);
                                cmdOrderStatus.Parameters.AddWithValue("@ordNo", Ordno);
                                cmdOrderStatus.Parameters.AddWithValue("@status", lStatus);
                                cmdOrderStatus.Parameters.AddWithValue("@message", Message);
                                cmd.ExecuteNonQuery();
                                cmdOrderStatus.ExecuteNonQuery();
                                Label1.Text = "Dupliacte Order" + tb_orderNo.Text;
                                connection.Close();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    else
                    {
                        Ordno = ljarr[i]["Failure"]["OrdNo"].ToString();
                        ItemCode = ljarr[i]["Failure"]["ItemCode"].ToString();
                        Message = ljarr[i]["Failure"]["Message"].ToString();
                        lStatus = 3; //item not found
                        try
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand(permanentOrder, connection);
                            SqlCommand cmdOrderStatus = new SqlCommand(orderStatusQuery, connection);
                            cmdOrderStatus.Parameters.AddWithValue("@ordNo", Ordno);
                            cmdOrderStatus.Parameters.AddWithValue("@status", lStatus);
                            cmdOrderStatus.Parameters.AddWithValue("@message", Message);
                            cmd.ExecuteNonQuery();
                            cmdOrderStatus.ExecuteNonQuery();
                            Label1.Text = "Item" + ItemCode + "not found";
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                else
                {
                    Ordno = ljarr[i]["Confirm"]["OrdNo"].ToString();
                    VendorCode = ljarr[i]["Confirm"]["VendorCode"].ToString();
                    Message = ljarr[i]["Confirm"]["Message"].ToString();
                    lStatus = -1; //item inserted successfully
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(permanentOrder, connection);
                        SqlCommand cmdOrderStatus = new SqlCommand(orderStatusQuery, connection);
                        cmdOrderStatus.Parameters.AddWithValue("@ordNo", Ordno);
                        cmdOrderStatus.Parameters.AddWithValue("@status", lStatus);
                        cmdOrderStatus.Parameters.AddWithValue("@message", Message);
                        cmd.ExecuteNonQuery();
                        cmdOrderStatus.ExecuteNonQuery();
                        Label1.Text = "Order" + tb_orderNo.Text + "placed Successfully";
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


        //string[] jsonStringArray = Regex.Split(orderResponse.Replace("[", "").Replace("]", ""), "},{");
        //List<string> ColumnsName = new List<string>();
        //foreach (string jSA in jsonStringArray)
        //{
        //    string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
        //    foreach (string ColumnsNameData in jsonStringData)
        //    {
        //        string colNameData = ColumnsNameData.Replace("\"OrderResult\":", "");
        //        try
        //        {
        //            int idx = colNameData.IndexOf(":");
        //            string ColumnsNameString = colNameData.Substring(0, idx - 1).Replace("\"", "");
        //            if (!ColumnsName.Contains(ColumnsNameString))
        //            {
        //                ColumnsName.Add(ColumnsNameString);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
        //            throw ex;
        //        }
        //    }
        //    break;
        //}
        //foreach (string AddColumnName in ColumnsName)
        //{
        //    dt.Columns.Add(AddColumnName);
        //}
        //foreach (string jSA in jsonStringArray)
        //{
        //    string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
        //    DataRow nr = dt.NewRow();
        //    foreach (string rowData in RowData)
        //    {
        //        string _rowData = rowData.Replace("\"OrderResult\":", "");
        //        try
        //        {
        //            int idx = _rowData.IndexOf(":");
        //            string RowColumns = _rowData.Substring(0, idx - 1).Replace("\"", "");
        //            string RowDataString = _rowData.Substring(idx + 1).Replace("\"", "");
        //            nr[RowColumns] = RowDataString;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    dt.Rows.Add(nr);

        //    var response = dt.Rows[0]["response"].ToString();
        //    var failure = dt.Rows[0]["Failure"].ToString();
        //    var venderCode = dt.Rows[0]["vendorCode"].ToString();
        //    //var itemCode = dt.Rows[0]["ItemCode"].ToString();
        //    var _Message = dt.Rows[0]["Message"].ToString();
        //    _responseCode = response.ToString();
        //    _failure = failure.ToString();
        //    _venderCode = venderCode.ToString();
        //    //_itemCode = itemCode.ToString();
        //    _message = _Message.ToString();
        //}
        //try
        //{
        ////    if (_message == "Item not found")
        ////    {
        ////        statusMsg = "Invalid Item";
        ////        string selectInvalidItem = "Select * from errorLog where message = '"+_message+"'";
        ////    }
        ////    if(_message == "Duplicate Order"){
        ////        statusMsg = "Duplicate";

        ////    }
        ////    if(_message == "Order Inserted Successfully.")
        ////    {
        ////        statusMsg = "Confirm";
        ////        System.Windows.Forms.MessageBox.Show("Error!" + _message);
        ////    }
        
        ////string errorLogQuery = "insert into errorLog (failure,venderCode, itemCode, message) values (@failure,@_resCode,@venderCode,@itemCode,@_message)";
        //connection.Open();
        //SqlCommand cmd = new SqlCommand(permanentOrder, connection);
        ////SqlCommand cmdUpdate = new SqlCommand(updatePermanentTable, connection);
        //SqlCommand cmdOrderStatus = new SqlCommand(orderStatusQuery, connection);
        ////SqlCommand cmdErrorLog = new SqlCommand(errorLogQuery, connection);
        //cmdOrderStatus.Parameters.AddWithValue("@ordNo", tb_orderNo.Text.Trim());
        //cmdOrderStatus.Parameters.AddWithValue("@status", statusMsg);
        //cmdOrderStatus.Parameters.AddWithValue("@message", _message);

        ////    //error file
        ////    cmdErrorLog.Parameters.AddWithValue("@failure",_failure);
        ////    cmdErrorLog.Parameters.AddWithValue("@_resCode", _responseCode);
        ////    cmdErrorLog.Parameters.AddWithValue("@venderCode", _venderCode);
        ////    cmdErrorLog.Parameters.AddWithValue("@itemCode", _itemCode);
        ////    cmdErrorLog.Parameters.AddWithValue("@_message", _message);
        //    cmd.ExecuteNonQuery();
        //    cmdUpdate.ExecuteNonQuery();
        //    cmdOrderStatus.ExecuteNonQuery();


        //    connection.Close();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        return dt;

        //return JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
    }

    public static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    public void reset()
    {
        tb_item.Text = "";
        tb_qty.Text = "";
        tb_itemstatus.Text = "";
        tb_pack.Text = "";
        tb_mrp.Text = "";
        tb_rate.Text = "";
        tb_scheme.Text = "";
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        reset();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static List<string> getProductData(string prefixText, int count)
    {
        string code = string.Empty;
        List<string> itemName = new List<string>();
        try
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ToString();
                con.Open();
                string query = "select * from djProduct where name like '" + prefixText + "%' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["name"].ToString(), sdr["code"].ToString());
                    itemName.Add(item);
                }
                sdr.Close();
                return itemName;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        int _qty = Convert.ToInt32(tb_qty.Text == "" ? "0" : tb_qty.Text);
        double _mrp = Convert.ToDouble(tb_mrp.Text == "" ? "0" : tb_mrp.Text);
        double _totalPrice = 0;
        if (_qty != 0 )
        {
            _totalPrice = _qty * _mrp;
        

            string addOrders = "insert into djOrder (orderNo, custID, itemName,itemCode, quantity, itemStatus, scheme, pack, mrp, rate, totalPrice) values (@orderNo, @custCode, @itemName, @_itemCode, @quantity, @itemStatus, @scheme, @pack, @mrp, @rate, @totalPrice)";
            string checkItemCode = "Select * from djOrder where itemCode = '"+ tb_code.Text.Trim() +"'";
            try
            {
                connection.Open();
                SqlCommand cmdAddOrder = new SqlCommand(addOrders, connection);
                SqlCommand cmdCheckItem = new SqlCommand(checkItemCode, connection);
                SqlDataReader sdr = cmdCheckItem.ExecuteReader();
                cmdAddOrder.Parameters.AddWithValue("@orderNo", tb_orderNo.Text);
                cmdAddOrder.Parameters.AddWithValue("@custCode", Session["code"].ToString());
                cmdAddOrder.Parameters.AddWithValue("@itemName",tb_item.Text.Trim());
                cmdAddOrder.Parameters.AddWithValue("@_itemCode", tb_code.Text.Trim());
                cmdAddOrder.Parameters.AddWithValue("@quantity",tb_qty.Text.Trim());
                cmdAddOrder.Parameters.AddWithValue("@itemStatus",tb_itemstatus.Text.Trim());
                cmdAddOrder.Parameters.AddWithValue("@scheme",tb_scheme.Text.Trim());
                cmdAddOrder.Parameters.AddWithValue("@pack", tb_pack.Text.Trim());
                cmdAddOrder.Parameters.AddWithValue("@mrp",tb_mrp.Text.Trim());
                cmdAddOrder.Parameters.AddWithValue("@rate",tb_rate.Text.Trim());
                cmdAddOrder.Parameters.AddWithValue("@totalPrice",_totalPrice);
            
                if (sdr.HasRows)
                {
                    System.Windows.Forms.MessageBox.Show("Error! Only one item you can add at one time. If you want to buy more than increase the quantity");
                }
                else
                {
                    cmdAddOrder.ExecuteNonQuery();
                }
                sdr.Close();
                connection.Close();
                showData();
                reset();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        else
        {
            System.Windows.Forms.MessageBox.Show("Quantity cannot be blank and 0");
        }
    }
    public void showData()
    {
        double sum=0;
        string showOrders = "select * from djOrder where custID = '"+ Session["code"].ToString()+"'";
        try
        {
            connection.Open();
            SqlCommand cmdShowOrders = new SqlCommand(showOrders, connection);
            SqlDataReader sdr = cmdShowOrders.ExecuteReader();
            GridView1.DataSource = sdr;
            GridView1.DataBind();
            foreach (GridViewRow row in GridView1.Rows)
            {
                var tAmt = row.FindControl("lb_tAmt") as Label;
                string _tAmt = tAmt.Text;
                sum += Convert.ToDouble(_tAmt);
            }
            Literal1.Text = sum.ToString();
            sdr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
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
        Label lb_orderNo = (Label)GridView1.Rows[e.RowIndex].FindControl("lb_itemCode");

        TextBox tb_qty = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_qty");

        GridView1.EditIndex = -1;

        try
        {
            connection.Open();
            string updateQuery = "Update djOrder set quantity = '" + tb_qty.Text + "' where orderNo = '" + lb_orderNo.Text + "' ";
            SqlCommand cmd = new SqlCommand(updateQuery, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            showData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }
    protected void deleteRow(object sender, GridViewDeleteEventArgs e)
    {
        Label lb_code = (Label)GridView1.Rows[e.RowIndex].FindControl("lb_itemCode");
        try
        {
            connection.Open();
            string deleteQuery = "delete FROM djOrder where itemCode='" + lb_code.Text + "'";
            SqlCommand cmd = new SqlCommand(deleteQuery, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            showData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }
    //protected void tb_item_TextChanged(object sender, EventArgs e)
    //{
    //    string msg = string.Empty;
    //    string code = tb_code.Text.Trim();
    //    try
    //    {
    //        using (SqlConnection con = new SqlConnection())
    //        {
    //            con.ConnectionString = ConfigurationManager.ConnectionStrings["myConn"].ToString();
    //            con.Open();
    //            string query = "select * from djProduct where code = @Code order by name";
    //            SqlCommand cmd = new SqlCommand(query, con);
    //            cmd.Parameters.AddWithValue("@Code", code);
    //            SqlDataReader sdr = cmd.ExecuteReader();

    //            while (sdr.Read())
    //            {
    //                int qty = Convert.ToInt32(sdr["qty"].ToString());
                    
    //                if (qty > 0)
    //                {
    //                    msg = "AVL";
    //                }
    //                tb_pack.Text = sdr["pack"].ToString();
    //                tb_rate.Text = sdr["Srate"].ToString();
    //                tb_scheme.Text = sdr["scheme"].ToString();
    //                tb_mrp.Text = sdr["mrp"].ToString();
    //                tb_itemstatus.Text = msg.ToString();
    //            }
    //            sdr.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    public void truncateTable()
    {
        try
        {
            connection.Open();
            string truncateTable = "truncate table djOrder ";
            SqlCommand cmd = new SqlCommand(truncateTable, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            showData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btn_newCustomerOrder_Click(object sender, EventArgs e)
    {
        newClientOrder = "Y";
        GenCustOrdNo(newClientOrder);
    }
}