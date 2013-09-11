using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Globalization;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using UrbanImpactCommon;

namespace UIF.PerformingArts
{
    public partial class AcademyClassEnrollment : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public Boolean flag = false;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["security"] == "Good")
            {
                try
                {
                    con.Open();
                    gvClassEnrollment.Enabled = true;
                    gvClassEnrollment.Visible = true;

                    string sql_LoadGrid = "";
                    sql_LoadGrid = "select LastName, FirstName "
                                + "from PerformingArtsAcademyClassEnrollment "
                                + "where Program = "
                                + "order by LastName, FirstName, day, month, attended ";

                    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "PerformingArtsAcademyClassEnrollment");
                    gvClassEnrollment.DataSource = ds.Tables[0];
                    gvClassEnrollment.DataBind();
                    con.Close();
                }
                catch (Exception lkjl)
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

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }
    }
}