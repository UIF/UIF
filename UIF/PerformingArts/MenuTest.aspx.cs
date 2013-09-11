using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UrbanImpactCommon;
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



namespace UIF.PerformingArts
{
    public partial class MenuTest : System.Web.UI.Page
    {
        public static string jj = "";
        public static string Department = "";
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public Boolean flag = false;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];

                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);

                try
                {
                    con.Open();
                    gvBillboardInformation.Enabled = true;
                    gvBillboardInformation.Visible = true;

                    string sql_LoadGrid = "";
                    sql_LoadGrid = "select si.LastName, si.FirstName, si.dob "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE ((si.dob like '" + System.DateTime.Now.ToString("MM") + "/%') OR (si.dob like '" + System.DateTime.Now.ToString("MM") + "-%'))"
                                + "AND (pl.mshschoir = 1 or pl.childrenschoir = 1 or pl.performingarts = 1 or pl.shakes = 1 or pl.singers = 1) "
                                //+ "AND si.currentregistrationform = 1 "
                                + "AND si.dob > '" + System.DateTime.Now.ToString("MM-dd-YYYY") + "' "
                                + "GROUP BY si.lastname, si.firstname, si.dob "                                
                                + "order by si.dob ";

                    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "UIF_PerformingArts.dbo.studentinformation");
                    gvBillboardInformation.DataSource = ds.Tables[0];
                    gvBillboardInformation.DataBind();
                    con.Close();
                }
                catch (Exception lkjl)
                {



                }
            
            
            
            
            
            }            
        }

        MenuItem CreateMenuItem(String text, String url, String toolTip)
        {
            // Create a new MenuItem object.    
            MenuItem menuItem = new MenuItem();

            // Set the properties of the MenuItem object using the specified parameters.    
            menuItem.Text = text;
            menuItem.NavigateUrl = url;
            menuItem.ToolTip = toolTip;

            return menuItem;
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
            //menucontrol.MenuControlBehavior(e, Request, Response, Request.QueryString["lastname"], Request.QueryString["firstname"]);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }
    }
}