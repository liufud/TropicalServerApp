using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace TropicalServer.UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerror.Visible = false;
            if (!IsPostBack)
            {
                if(Request.Cookies["username"] != null)
                {
                    usernametextbox.Text = Request.Cookies["username"].Value;
                }
                if (Request.Cookies["password"] != null)
                {
                    passwordtextbox.Attributes.Add("value", Request.Cookies["password"].Value);
                }
                if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
                {
                    cbRememberID.Checked = true;
                }
            }
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            string sConn = "TropicalServer.Properties.Settings.TropicalServerConn";
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[sConn];
            //Data Source=JACKY\SQLEXPRESS;Initial Catalog=TropicalServer;Integrated Security=True
            using (SqlConnection sqlCon = new SqlConnection(settings.ConnectionString))
            {
                sqlCon.Open();
                string query = "Select COUNT(1) from tblUserLogin where UserID=@username and Password=@password";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.Parameters.AddWithValue("@username", usernametextbox.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@password", passwordtextbox.Text.Trim());
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if (count == 1)
                {
                    Session["username"] = usernametextbox.Text.Trim();                    
                    if (cbRememberID.Checked)
                    {
                        Response.Cookies["username"].Value = usernametextbox.Text;
                        Response.Cookies["password"].Value = passwordtextbox.Text;
                        Response.Cookies["username"].Expires = DateTime.Now.AddDays(15);
                        Response.Cookies["password"].Expires = DateTime.Now.AddDays(15);
                    } 
                    else
                    {
                        Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
                    }
                    Response.Redirect("Products.aspx");
                }
                else
                {
                    lblerror.Visible = true;                    
                }
            }
        }
    }
}