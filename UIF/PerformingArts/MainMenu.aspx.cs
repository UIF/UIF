using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using UrbanImpactCommon;
using Microsoft.Web.Administration;

namespace UIF.PerformingArts
{
	/// <summary>
	/// Summary description for MainMenu.
	/// </summary>
	public partial class MainMenu : System.Web.UI.Page
	{
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);        

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                //string ServerConnectionString = Request.ServerVariables["SERVER_NAME"];
                // Put user code to initialize the page here
                lgnUIFLogin.Enabled = true;
                lgnUIFLogin.Visible = true;

//                string CurrentName = "";

                //using (ServerManager serverManager = new ServerManager())
                //{
                //    Configuration config = serverManager.GetApplicationHostConfiguration();

                //    ConfigurationSection serverRuntimeSection = config.GetSection("system.webServer/serverRuntime", "Default Web Site");
                //    serverRuntimeSection["enabled"] = true;
                //    //serverRuntimeSection["frequentHitThreshold"] = 5;
                //    //serverRuntimeSection["frequentHitTimePeriod"] = TimeSpan.Parse("00:00:20");
                //    serverRuntimeSection["maxRequestEntityAllowed"] = "4294967295";
                //    serverRuntimeSection["uploadReadAheadSize"] = "2000000";
                //    serverManager.CommitChanges();
                //}               
            }
        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion


        public string BT
        {
            get
            {
                //if page is first time requested, then set 
                //the default resolution setting as 1280x1024
                if (!Page.IsPostBack)
                {
                    //Set the Html Hidden Input Control Value 
                    //For Reffering on Client Through JavaScript
                    //Request.Form["ClientResolution"] = "1280Res";
                    //ServerResolution.Value = "1280Res";
                    //Returning CSS class name according to client's resolution 
                    return "button1024";
                }
                else if (Request.Form["ClientResolution"] == "1280Res")
                {
                    //Set the Html Hidden Input Control Value 
                    //For Reffering on Client Through JavaScript
                    Request.Form["ClientResolution"] = "1280Res";
                    //ServerResolution.Value = "1280Res";
                    //Returning CSS class name according to client's resolution 
                    return "button1280";
                }
                else if (Request.Form["ClientResolution"] == "1024Res")
                {
                    //Set the Html Hidden Input Control Value 
                    //For Reffering on Client Through JavaScript
                    Request.Form["ClientResolution"] = "1024Res";
                    //ServerResolution.Value = "1024Res";
                    //Returning CSS class name according to client's resolution
                    return "button1024";
                }
                else if (Request.Form["ClientResolution"] == "800Res")
                {
                    //Set the Html Hidden Input Control Value 
                    //For Reffering on Client Through JavaScript
                    Request.Form["ClientResolution"] = "800Res";
                    //ServerResolution.Value = "800Res";
                    //Returning CSS class name according to client's resolution
                    return "button800";
                }
                else
                {
                    //if client resolution is not detected yet, 
                    //then set default 1280x1024 resolution
                    return "button1280";
                }
            }
            //return "lkjlk";
        }
        
        protected void imbAthletics_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("uifadmin2.aspx");
		
		}

		protected void imbPerformingArts_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("performingarts.aspx");

		}

        protected void lgnUIFLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {

		    SqlDataReader reader = null;
            
            con.Open();//Opens the db connection.

            string sql = "select lastname, firstname, department, role, activestaffmember, comments "
                        + "FROM dbo.StaffMembers "
                        + "WHERE username=@username "
                        + "AND password=@password "
                        + "AND activestaffmember = 1";

            //Perform database lookup based on the chosen child..RCM..
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@username", lgnUIFLogin.UserName));
            cmd.Parameters.Add(new SqlParameter("@password", lgnUIFLogin.Password));

            cmd.Connection = con;
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader.IsDBNull(0))
                {
                    //Problem logging in..RCM.. Blank lastname..
                    Response.Redirect("ErrorAccess.aspx");
                }
                else
                {
                    string lastname = "";
                    string firstname = "";
                    string department = "";
                    string role = "";
                    Boolean active = false;
                    string comments = "";

                    lastname = reader.GetString(0);
                    firstname = reader.GetString(1);
                    department = reader.GetString(2);
                    role = reader.GetString(3);
                    active = reader.GetBoolean(4);
                    comments = reader.GetString(5);

                    //Boolean fullaccess = false;
                    //Boolean partialaccess = false;
                    //string accesslevel = "1";
                    //Boolean LimitedAccess = false;

                    
                    //Check GlobalSecurity class to determine action..RCM..6/20/11.
                    //GlobalSecuity CheckSecurity = new GlobalSecuity();
                    //LimitedAccess = CheckSecurity.CheckCredentials(ref accesslevel,ref partialaccess,ref fullaccess, lastname, firstname, department, role, System.Web.VirtualPathUtility.GetFileName(Request.Path));

                    //if (LimitedAccess)
                    //{
                    //    Response.Redirect("ErrorAccess.aspx");
                    //}
                    //else
                    //{
                        //Response.Redirect("menutest.aspx?Security=Good&lastname=" + lastname + "&firstname=" + firstname);
                        Response.Redirect("menutest.aspx?Security=Good&lastname=" + lastname + "&firstname=" + firstname + "&Dept=" + department);
                    //}

                    //Response.Redirect("performingarts.aspx");
                    //Response.Redirect("PerformingArtsClasses.aspx?LastName=" + txtLastName.Text + "&FirstName=" + txtFirstName.Text + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
                    //Response.Redirect("performingarts.aspx?SecurityLevel=" + ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(","))
                    // "&FirstName=" + ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
                    //if ((role == "Program Assistant") | (role == "Ministry Associate") | (role == "Dance Choir Coordinator"))
                }
            }
            else
            {
                //Invalid Login...OR InActive.. kick them out.... NO ACCESS!!!!
                //Response.Redirect("MainMenu.aspx");
            }
        }
	}
}
