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
            UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
            MenuBest = BuildMenu.BuildMenuControl(MenuBest);
            
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
        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
            //menucontrol.MenuControlBehavior(e, Request, Response, Request.QueryString["lastname"], Request.QueryString["firstname"]);
        }

	}
}
