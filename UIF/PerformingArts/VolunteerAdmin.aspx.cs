using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UrbanImpactCommon;
using System.Data.SqlClient;

namespace UIF.PerformingArts
{
    public partial class VolunteerAdmin : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                //Department = Request.QueryString["Dept"];

                //Ryan C Manners...6/16/11.
                //UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                //MenuBest = BuildMenu.BuildMenuControl(MenuBest);
            }
        }
    }
}