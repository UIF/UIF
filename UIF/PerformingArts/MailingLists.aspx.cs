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
    public partial class MailingLists : System.Web.UI.Page
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

            }            
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

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            ExcelExport.ExportGridView(gvMailingList, Response);

            //Email capability..?
        }

        protected void cmbRetrieveData_Click(object sender, EventArgs e)
        {
            string sql_LoadGrid = "";

            try
            {
                con.Open();
                gvMailingList.Enabled = true;
                gvMailingList.Visible = true;

                if (chb3on3Basketball.Checked)
                {
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.3on3Basketball = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbBasketballTEAMS.Checked)
                {
                    if ((chb3on3Basketball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.basketballTEAMS = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbBoysOutreachBball.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.boysoutreachbasketball = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbGirlsOutreachBball.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbBoysOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.girlsoutreachbasketball = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbOliverFootballBible.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.oliverfootball = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbLittleLeagueBaseball.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbOliverFootballBible.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.Baseball = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbMondayNights.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbOliverFootballBible.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.mondaynights = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbSoccerInterMurals.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbOliverFootballBible.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.soccerintramurals = 1 "
                                + "AND pl.3on3Basketball = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbSoccerLgTravel.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbOliverFootballBible.Checked) || (chbSoccerInterMurals.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.SoccerTEAMS = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbHSBasketLeague.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbOliverFootballBible.Checked) || (chbSoccerInterMurals.Checked) || (chbSoccerLgTravel.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.hsbasketballlg = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbMSBasketLeague.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbHSBasketLeague.Checked) || (chbOliverFootballBible.Checked) || (chbSoccerInterMurals.Checked) || (chbSoccerLgTravel.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.msbasketballlg = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbShakes.Checked)
                {
                    if ((chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbMSBasketLeague.Checked) || (chbHSBasketLeague.Checked) || (chbOliverFootballBible.Checked) || (chbSoccerInterMurals.Checked) || (chbSoccerLgTravel.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.shakes = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbMSHSChoir.Checked)
                {
                    if ((chbShakes.Checked) || (chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbMSBasketLeague.Checked) || (chbHSBasketLeague.Checked) || (chbOliverFootballBible.Checked) || (chbSoccerInterMurals.Checked) || (chbSoccerLgTravel.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.mshschoir = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbChildrensChoir.Checked)
                {
                    if ((chbMSHSChoir.Checked) || (chbShakes.Checked) || (chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbMSBasketLeague.Checked) || (chbHSBasketLeague.Checked) || (chbOliverFootballBible.Checked) || (chbSoccerInterMurals.Checked) || (chbSoccerLgTravel.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.childrenschoir = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbPerformingArts.Checked)
                {
                    if ((chbMSHSChoir.Checked) || (chbShakes.Checked) || (chbChildrensChoir.Checked) || (chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbMSBasketLeague.Checked) || (chbHSBasketLeague.Checked) || (chbOliverFootballBible.Checked) || (chbSoccerInterMurals.Checked) || (chbSoccerLgTravel.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }
                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.performingarts = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }
                if (chbSingers.Checked)
                {
                    if ((chbMSHSChoir.Checked) || (chbShakes.Checked) || (chbChildrensChoir.Checked) || (chbPerformingArts.Checked) || (chb3on3Basketball.Checked) || (chbBasketballTEAMS.Checked) || (chbLittleLeagueBaseball.Checked) || (chbMondayNights.Checked) || (chbMSBasketLeague.Checked) || (chbHSBasketLeague.Checked) || (chbOliverFootballBible.Checked) || (chbSoccerInterMurals.Checked) || (chbSoccerLgTravel.Checked) || (chbBoysOutreachBball.Checked) || (chbGirlsOutreachBball.Checked))
                    {
                        sql_LoadGrid = sql_LoadGrid + " UNION ";
                    }

                    sql_LoadGrid = sql_LoadGrid + "select si.LastName, si.FirstName, si.Address, si.city, si.state, si.zip "
                                + "from UIF_PerformingArts.dbo.studentinformation si "
                                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                + "WHERE si.mailinglistinclude = 1 "
                                + "AND pl.singers = 1 "
                                + "AND si.grade <> 'GR' "
                                + "AND si.LastName not like 'test%' "
                                + "GROUP BY si.lastname, si.firstname, si.Address, si.city, si.state, si.zip  ";
                }

                sql_LoadGrid = sql_LoadGrid + "ORDER by si.lastname, si.firstname ";


                //Clear the gridview..RCM.
                gvMailingList.DataSource = null;
                gvMailingList.DataBind();

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "UIF_PerformingArts.dbo.studentinformation");
                gvMailingList.DataSource = ds.Tables[0];
                gvMailingList.DataBind();
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                //con.Close();
            }
        }

        protected void chbMSBasketLeague_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbLittleLeagueBaseball_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbShakes_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbSingers_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbPerformingArts_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbChildrensChoir_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbMSHSChoir_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbMondayNights_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbOliverFootballBible_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chb3on3Basketball_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbSoccerInterMurals_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbSoccerLgTravel_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbBasketballTEAMS_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbHSBasketLeague_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbBoysOutreachBball_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbGirlsOutreachBball_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}