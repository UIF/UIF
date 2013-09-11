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
    public partial class StudentAttendanceOptions : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public static string Department = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["security"] == "Good")
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];


            }
            else
            {
                //Ryan C Manners..1/5/11
                //Do NOT ALLOW ACCESS TO THE PAGE!
                Response.Redirect("ErrorAccess.aspx");
            }
        }

        protected void cmdEnterEventAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmdEnterSingleStudentAttend_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmbViewAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendanceReporting.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }
    }
}