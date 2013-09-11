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
using UrbanImpactCommon;
using System.Data.SqlClient;


namespace UIF.PerformingArts
{
	/// <summary>
	/// Summary description for PerformingArts.
	/// </summary>
	public partial class PerformingArts : System.Web.UI.Page
	{
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);        

        protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["security"] == "Good")
                {
                    if (Request.QueryString["lastname"] == "Anderson" && Request.QueryString["firstname"] == "Eric")
                    {


                    }
                    else
                    {


                    }
                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!

                    Response.Redirect("ErrorAccess.aspx");
                }
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

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
             //+ "&Dept=" + Request.QueryString["Dept"]
        }


	}
}
