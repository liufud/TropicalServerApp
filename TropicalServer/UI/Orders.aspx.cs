using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TropicalServer.UI
{
    public partial class Orders : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["TropicalServer.Properties.Settings.TropicalServerConn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                System.Diagnostics.Debug.WriteLine("page load not postback");
                ddlOrderDate.Items.Add(new ListItem("", ""));
                ddlOrderDate.Items.Add(new ListItem("Today", "Today"));
                ddlOrderDate.Items.Add(new ListItem("Last 7 Days", "Last 7 Days"));
                ddlOrderDate.Items.Add(new ListItem("Last 1 Month", "Last 1 Month"));
                ddlOrderDate.Items.Add(new ListItem("Last 6 Months", "Last 6 Months"));
                GetGridViewData();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("page load postback");
                //GetGridViewData();
                //
            }
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");
            }
        }

        private void GetGridViewData()
        {
            System.Diagnostics.Debug.WriteLine("GetGridViewData");
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetOrders";
                if (ddlOrderDate.Text != "")
                {
                    //System.Diagnostics.Debug.WriteLine("order date added");
                    cmd.Parameters.Add("@OrderDate", SqlDbType.VarChar).Value = ddlOrderDate.Text;
                }
                if (ddlSalesManager.Text != "")
                {
                    //System.Diagnostics.Debug.WriteLine(ddlSalesManager.Text + " Selected ------------");
                    cmd.Parameters.Add("@SalesManager", SqlDbType.VarChar).Value = ddlSalesManager.Text;
                }
                if (tbxCustomerID.Text != "")
                {
                    //System.Diagnostics.Debug.WriteLine("Customer ID" + tbxCustomerID.Text + " Selected ------------");
                    cmd.Parameters.Add("@CustomerID", SqlDbType.VarChar).Value = tbxCustomerID.Text;
                }

                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //System.Diagnostics.Debug.WriteLine(da.SelectCommand.CommandText);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvOrderResults.DataSource = ds.Tables[0];
                    gvOrderResults.DataBind();
                }                
                if (gvOrderResults.Columns.Count > 0)
                {
                    gvOrderResults.Columns[0].Visible = false;
                }
                else
                {
                    gvOrderResults.HeaderRow.Cells[0].Visible = false;
                    foreach (GridViewRow gvr in gvOrderResults.Rows)
                    {
                        gvr.Cells[0].Visible = false;
                    }
                }
            }
        }

        //void gvOrders_RowDataBound(Object sender, GridViewRowEventArgs e)
        //{

        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        e.Row.Cells[7].ColumnSpan = 2;
        //        e.Row.Cells[8].Visible = false;
        //        e.Row.Cells[7].Text = "Available Action";
        //    }

        //}

        protected void gvOrders_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Update");
            //Finding the controls from Gridview for the row which is going to update  
            Label id = gvOrderResults.Rows[e.RowIndex].FindControl("lbl_OrderID") as Label;
            TextBox trackingNum = gvOrderResults.Rows[e.RowIndex].FindControl("txt_Tracking") as TextBox;
            TextBox orderDate = gvOrderResults.Rows[e.RowIndex].FindControl("txt_OrderDate") as TextBox;
            Label customerID = gvOrderResults.Rows[e.RowIndex].FindControl("lblCustID") as Label;
            TextBox customerName = gvOrderResults.Rows[e.RowIndex].FindControl("txt_CustomerName") as TextBox;
            TextBox address = gvOrderResults.Rows[e.RowIndex].FindControl("txt_Address") as TextBox;
            TextBox routeNum = gvOrderResults.Rows[e.RowIndex].FindControl("txt_RouteNumber") as TextBox;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //updating the record  
                SqlCommand cmd1 = new SqlCommand("Update tblOrder set OrderTrackingNumber='" + trackingNum.Text + "',OrderDate='" + orderDate.Text + "',OrderRouteNumber='" + routeNum.Text +
                                                "' where OrderID=" + Convert.ToInt32(id.Text) + ";" + "Update tblCustomer set CustName='" + customerName.Text + "',CustOfficeAddress1='" + address.Text +
                                                "' where CustNumber=" + Convert.ToInt32(customerID.Text) + ";", con);
                cmd1.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine(cmd1.CommandText);
                //SqlCommand cmd2 = new SqlCommand("Update tblCustomer set CustName='" + customerName.Text + "',CustOfficeAddress1='" + address.Text +
                //                                "' where CustNumber=" + Convert.ToInt32(customerID.Text), con);            
                //cmd2.ExecuteNonQuery();

                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                gvOrderResults.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                GetGridViewData();
            }

        }

        protected void gvOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Edit");
            gvOrderResults.EditIndex = e.NewEditIndex;
            GetGridViewData();
        }

        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Deleting");
            GridViewRow row = (GridViewRow)gvOrderResults.Rows[e.RowIndex];
            //Label lbldeleteid = (Label)row.FindControl("lblID");
            //string CS = ConfigurationManager.ConnectionStrings["TropicalServer.Properties.Settings.TropicalServerConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete FROM tblOrder where OrderID='" + Convert.ToInt32(gvOrderResults.DataKeys[e.RowIndex].Value.ToString()) + "'", con);
                cmd.ExecuteNonQuery();
                GetGridViewData();
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvOrderResults.EditIndex)
            {
                (e.Row.Cells[7].Controls[1].FindControl("LkB2") as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
        }

        protected void gvOrders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrderResults.EditIndex = -1;
            GetGridViewData();
        }

        protected void ddlSalesManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGridViewData();
        }

        protected void gvOrderResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(checkNull(gvOrderResults.SelectedRow.Cells[1].Text) + "printing view value");
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            lblTrackingNumber.Text = (clickedRow.FindControl("lbl_Tracking") as Label).Text;
            lblOrderDate.Text = (clickedRow.FindControl("lbl_OrderDate") as Label).Text;
            lblCustID.Text = (clickedRow.FindControl("lblCustID") as Label).Text;
            lblCustName.Text = (clickedRow.FindControl("lbl_CustomerName") as Label).Text;
            lblAddress.Text = (clickedRow.FindControl("lbl_Address") as Label).Text;
            lblRouteNumber.Text = (clickedRow.FindControl("lbl_RouteNumber") as Label).Text;
            mpeView.Show();
        }

        private string checkNull(string text)
        {
            if (text == null)
            {
                return "N/A";
            }
            else
            {
                return text;
            }
        }
    }
}