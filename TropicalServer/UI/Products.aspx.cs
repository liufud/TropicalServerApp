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
    public partial class Products : System.Web.UI.Page
    {
        string btnCategory = "";       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsSideBar = GetSideBarData();
                rptrProductCategories.DataSource = dsSideBar;
                rptrProductCategories.DataBind();
                //if(btnCategory != "" || Cache["btnCategory"] != null)
                //{
                //    DataTable dsGridView = GetGridViewData();
                //    gvChart.DataSource = dsGridView;
                //    gvChart.DataBind();
                //}                
                //System.Diagnostics.Debug.WriteLine("Button is: " + btnCategory + "===========================");
            }
            if (Session["username"] == null)
            {
                Response.Redirect("login.aspx");
            }         
        }

        private DataSet GetSideBarData()
        {
            string CS = ConfigurationManager.ConnectionStrings["TropicalServer.Properties.Settings.TropicalServerConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select ItemTypeDescription from tblItemType", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        private DataTable GetGridViewData()
        {     
            string CS = ConfigurationManager.ConnectionStrings["TropicalServer.Properties.Settings.TropicalServerConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                if(Cache["btnCategory"] == null)
                {
                    string cmdStr = "select ItemNumber as [Item #],ItemDescription as [Item Description],PrePrice as [Pre-Price], tblItemType.ItemTypeDescription " +
                    "from tblItem " +
                    "Left Join tblItemType on tblItem.ItemType = tblItemType.ItemTypeID " +
                    "Where tblItemType.ItemTypeDescription = @itemDescription";
                    SqlCommand cmd = new SqlCommand(cmdStr, con);
                    cmd.Parameters.AddWithValue("@itemDescription", btnCategory);
                    List<string> btnCategories = new List<string>();                    
                    btnCategories.Add(btnCategory);
                    Cache["btnCategory"] = btnCategory;
                    Cache["btnCategories"] = btnCategories;
                    System.Diagnostics.Debug.WriteLine("Button is: " + btnCategory + "===========================");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    ds.Tables[0].TableName = btnCategory;
                    System.Diagnostics.Debug.WriteLine("from database");
                    Cache["gvDataSet"] = ds;
                    //gvChart.DataSource = ds;
                    //gvChart.DataBind();
                    return ds.Tables[btnCategory];
                }
                else if (((List<string>) Cache["btnCategories"]).Contains(btnCategory))
                {
                    System.Diagnostics.Debug.WriteLine("from cache");
                    return ((DataSet)Cache["gvDataSet"]).Tables[btnCategory];
                }
                else if (!((List<string>)Cache["btnCategories"]).Contains(btnCategory) && btnCategory != "")
                {
                    string cmdStr = "select ItemNumber as [Item #],ItemDescription as [Item Description],PrePrice as [Pre-Price], tblItemType.ItemTypeDescription " +
                    "from tblItem " +
                    "Left Join tblItemType on tblItem.ItemType = tblItemType.ItemTypeID " +
                    "Where tblItemType.ItemTypeDescription = @itemDescription";
                    SqlCommand cmd = new SqlCommand(cmdStr, con);
                    cmd.Parameters.AddWithValue("@itemDescription", btnCategory);
                    List<string> btnCategories = (List<string>) Cache["btnCategories"];
                    btnCategories.Add(btnCategory);
                    Cache.Remove("btnCategory");
                    Cache["btnCategory"] = btnCategory;
                    Cache.Remove("btnCategories");
                    Cache["btnCategories"] = btnCategories;
                    System.Diagnostics.Debug.WriteLine("Button is: " + btnCategory + "===========================");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = (DataSet) Cache["gvDataSet"];
                    DataTable dt = ds.Tables.Add(btnCategory);
                    da.Fill(ds.Tables[btnCategory]);
                    System.Diagnostics.Debug.WriteLine("uncached new button from database");
                    Cache.Remove("gvDataSet");
                    Cache["gvDataSet"] = ds;
                    //gvChart.DataSource = ds;
                    //gvChart.DataBind();
                    return ds.Tables[btnCategory];
                }
                else
                {
                    string cmdStr = "select ItemNumber as [Item #],ItemDescription as [Item Description],PrePrice as [Pre-Price], tblItemType.ItemTypeDescription " +
                    "from tblItem " +
                    "Left Join tblItemType on tblItem.ItemType = tblItemType.ItemTypeID " +
                    "Where tblItemType.ItemTypeDescription = @itemDescription";
                    SqlCommand cmd = new SqlCommand(cmdStr, con);
                    cmd.Parameters.AddWithValue("@itemDescription", (string)Cache["btnCategory"]);
                    btnCategory = (string)Cache["btnCategory"];                  
                    Cache.Remove("btnCategory");
                    Cache["btnCategory"] = btnCategory;
                    System.Diagnostics.Debug.WriteLine("Button is: " + btnCategory + "===========================");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = (DataSet)Cache["gvDataSet"];
                    DataTable dt = ds.Tables[btnCategory];
                    da.Fill(ds.Tables[btnCategory]);
                    System.Diagnostics.Debug.WriteLine("cached data for paging");
                    Cache.Remove("gvDataSet");
                    Cache["gvDataSet"] = ds;
                    //gvChart.DataSource = ds;
                    //gvChart.DataBind();
                    return ds.Tables[btnCategory];
                }
            }            
        }

        protected void itemType_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            btnCategory = btn.CommandArgument;

            DataTable dsGridView = GetGridViewData();
            gvChart.DataSource = dsGridView;
            gvChart.DataBind();
        }

        protected void gvChart_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvChart.PageIndex = e.NewPageIndex;

            DataTable dsGridView = GetGridViewData();
            gvChart.DataSource = dsGridView;
            gvChart.DataBind();
        }
    }
}