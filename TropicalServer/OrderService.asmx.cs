using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TropicalServer
{
    /// <summary>
    /// Summary description for OrderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OrderService : System.Web.Services.WebService
    {
        //[WebMethod]
        //public DataTable GetCustomerID(string customerID)
        //{
        //    string CS = ConfigurationManager.ConnectionStrings["TropicalServer.Properties.Settings.TropicalServerConn"].ConnectionString;
        //    DataTable dt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        SqlCommand cmd = new SqlCommand("spGetOrders", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlParameter parameter = new SqlParameter("@CustomerID", customerID);
        //        cmd.Parameters.Add(parameter);
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        DataTable dtSchema = rdr.GetSchemaTable();
        //        List<DataColumn> listCols = new List<DataColumn>();
        //        if (dtSchema != null)
        //        {
        //            foreach (DataRow drow in dtSchema.Rows)
        //            {
        //                string columnName = System.Convert.ToString(drow["ColumnName"]);
        //                DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
        //                column.Unique = (bool)drow["IsUnique"];
        //                column.AllowDBNull = (bool)drow["AllowDBNull"];
        //                column.AutoIncrement = (bool)drow["IsAutoIncrement"];
        //                listCols.Add(column);
        //                dt.Columns.Add(column);
        //            }
        //        }
        //        while (rdr.Read())
        //        {
        //            //System.Diagnostics.Debug.WriteLine(rdr[0].ToString());                    
        //            DataRow newRow = dt.NewRow();
        //            for (int i = 0; i < listCols.Count; i++)
        //            {
        //                newRow[((DataColumn)listCols[i])] = rdr[i];
        //            }
        //            //newRow["OrderTrackingNumber"] = "Test data"/*rdr[0].ToString()*/;
        //            //newRow["Order Date"] = rdr["Order Date"].ToString();
        //            //newRow["Customer ID"] = rdr["Customer ID"].ToString();
        //            //newRow["Customer Name"] = rdr["Customer Name"].ToString();
        //            //newRow["Address"] = rdr["Address"].ToString();
        //            //newRow["Route #"] = rdr["Route #"].ToString();
        //            dt.Rows.Add(newRow);
        //        }
        //        dt.TableName = "TableBasedOnCustomerID";
        //    }
        //    return dt;
        //}
        [WebMethod]
        public List<string> GetCustomerID(string prefixText)
        {
            string CS = ConfigurationManager.ConnectionStrings["TropicalServer.Properties.Settings.TropicalServerConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select DISTINCT OrderCustomerNumber from tblOrder join tblCustomer on tblOrder.OrderCustomerNumber = tblCustomer.CustNumber where OrderCustomerNumber like @CustomerID+'%' order by OrderCustomerNumber";
                cmd.Parameters.AddWithValue("@CustomerID", prefixText);
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                List<string> customerID = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    customerID.Add(ds.Tables[0].Rows[i][0].ToString());
                }
                return customerID;
            }
        }
    }
}
