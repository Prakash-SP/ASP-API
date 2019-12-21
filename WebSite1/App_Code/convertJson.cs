using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;



public class convertJson
{
	public convertJson()
	{
        
	}

    public DataTable JsonData()
    {
        string json = "[{\"OrderNo\":\"ESPL10001\",\"VendorCode\":\"1013\",\"OrderDetails\":[{\"Item\":\"DICARIS TAB  (ADULT)\",\"Itemcode\":\"117\",\"Qty\":\"11\"},{\"Item\":\"abc\",\"Itemcode\":\"9589\",\"Qty\":\"10\"}]}]";
        DataTable dt = new DataTable();
        JToken jt = JToken.Parse(json);

        return dt;
    }
}