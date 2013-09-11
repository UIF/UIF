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
    public partial class AcademicsProgramSectionMaintenance : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public Boolean flag = false;
        public int irowNum = 0;
        public static string Department = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];

                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);

                if (Request.QueryString["security"] == "Good")
                {
                    if (((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")))
                    {
                        if (Department == "Education")
                        {
                            ddlProgram.Items.Add("Please select a program");
                            ddlProgram.Items.Add("Impact Urban Schools");
                            ddlProgram.Items.Add("SATPrep");
                            ddlProgram.Items.Add("SummerDayCamp");
                            ddlProgram.Text = "Please select a program";
                        }
                        else
                        {
                            Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                        }
                    }
                    else
                    {
                        Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                    }
                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!
                    Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                }
            }
        }

        protected void cmbProgram_Click(object sender, EventArgs e)
        {
            //Retrieve the class lists
            try
            {
                con.Open();//Opens the db connection.
                string sql_LoadGrid = "";

                if (ddlProgram.Text == "MSHS Choir")
                {
                    sql_LoadGrid = "select Lastname, Firstname, Grade "
                                        + "FROM PerformingArtsAcademyStudents "
                                        + "WHERE mshschoir = 1 "
                                        + "order by lastname, firstname ";

                    //Perform database lookup based on the chosen child..RCM..
                    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                    cmd.Connection = con;
                    gvStudentList.DataSource = cmd.ExecuteReader();
                    gvStudentList.DataBind();
                    gvStudentList.Columns[1].HeaderText = "Test";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    sql_LoadGrid = "select Lastname, Firstname, Grade"
                                        + "FROM PerformingArtsAcademyStudents "
                                        + "WHERE childrenschoir = 1 "
                                        + "order by lastname, firstname ";

                    //Perform database lookup based on the chosen child..RCM..
                    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                    cmd.Connection = con;
                    gvStudentList.DataSource = cmd.ExecuteReader();
                    gvStudentList.DataBind();
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    lblProgram.Visible = false;
                    cmbProgram.Visible = false;
                    ddlProgram.Enabled = false;
                    ddlProgram.Visible = false;

                    sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', sizelimit as 'SizeLimit', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM PerformingArtsAcademyAvailableClasses  order by classname";

                    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "PerformingArtsAcademyAvailableClasses");
                    gvStudentList.DataSource = ds.Tables[0];
                    gvStudentList.DataBind();
                    con.Close(); 
                }        
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }
               
        protected void gvStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow && row.RowIndex == irowNum)
            {        

                    DataRowView drv = (DataRowView)e.Row.DataItem;

                    string ClassName = drv.Row.ItemArray.GetValue(0).ToString();
                    string ClassTime = drv.Row.ItemArray.GetValue(1).ToString();
                    string ClassDay = drv.Row.ItemArray.GetValue(2).ToString();
                    string Location = drv.Row.ItemArray.GetValue(3).ToString();
                    string Comments = drv.Row.ItemArray.GetValue(4).ToString();
                    string Instructor = drv.Row.ItemArray.GetValue(5).ToString();
                    string devotional = drv.Row.ItemArray.GetValue(6).ToString();
            }       
        }        
        
        protected void gvStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvStudentList.SelectedRow;
            irowNum = gvStudentList.SelectedIndex;
            bind();
        }

        protected void gvStudentList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvStudentList.Rows[e.NewSelectedIndex];
        }

        public void bind()  
        {
            try
            {
                string sql_LoadGrid = "";
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BasketballTEAMSProgramSections  order by sectionName";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BaseballProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM OutreachBasketballProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MSBasketballLeagueProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM HSBasketballLeagueProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM [3on3BasketballProgramSections]  order by sectionname";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SoccerIntraMuralsProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SoccerTEAMSProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MondayNightsProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BibleStudyProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SpecialEventsProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MSHSChoirProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ChildrensChoirProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM PerformingArtsAcademyProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SingersProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ShakesProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ImpactUrbanSchoolsProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "SummerDayCamp")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SummerDayCampProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "SATPrep")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SATPrepProgramSections  order by sectionname";
                }

                con.Open();//Opens the db connection.

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    da.Fill(ds, "BasketballTEAMSProgramSections");
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    da.Fill(ds, "BaseballProgramSections");
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    da.Fill(ds, "OutreachBasketballProgramSections");
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    da.Fill(ds, "HSBasketballLeagueProgramSections");
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    da.Fill(ds, "MSBasketballLeagueProgramSections");
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    da.Fill(ds, "SoccerIntraMuralsProgramSections");
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    da.Fill(ds, "SoccerTEAMSProgramSections");
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    da.Fill(ds, "[3on3BasketballProgramSections]");
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    da.Fill(ds, "MondayNightsProgramSections");
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    da.Fill(ds, "BibleStudyProgramSections");
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    da.Fill(ds, "SpecialEventsProgramSections");
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    da.Fill(ds, "MSHSChoirProgramSections");
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    da.Fill(ds, "[ChildrensChoirProgramSections]");
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    da.Fill(ds, "PerformingArtsAcademyProgramSections");
                }
                else if (ddlProgram.Text == "Singers")
                {
                    da.Fill(ds, "SingersProgramSections");
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    da.Fill(ds, "ShakesProgramSections");
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    da.Fill(ds, "ImpactUrbanSchoolsProgramSections");
                }
                else if (ddlProgram.Text == "SummerDayCamp")
                {
                    da.Fill(ds, "SummerDayCampProgramSections");
                }
                else if (ddlProgram.Text == "SATPrep")
                {
                    da.Fill(ds, "SATPrepProgramSections");
                }
                
                gvStudentList.DataSource = ds.Tables[0];
                gvStudentList.DataBind();
                gvStudentList.Visible = true;
                con.Close();
            }
            catch (Exception lkjlasaaa)
            {

            }
            finally
            {

            }
        }

        protected void gvTeamSections_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow && row.RowIndex == irowNum)
            {

                DataRowView drv = (DataRowView)e.Row.DataItem;

                string ClassName = drv.Row.ItemArray.GetValue(0).ToString();
                string ClassTime = drv.Row.ItemArray.GetValue(1).ToString();
                string ClassDay = drv.Row.ItemArray.GetValue(2).ToString();
                string Location = drv.Row.ItemArray.GetValue(3).ToString();
                string Comments = drv.Row.ItemArray.GetValue(4).ToString();
                string Instructor = drv.Row.ItemArray.GetValue(5).ToString();
                string devotional = drv.Row.ItemArray.GetValue(6).ToString();
            }
        }

        protected void gvTeamSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvTeamSections.SelectedRow;
            irowNum = gvTeamSections.SelectedIndex;
            bind2();
        }

        protected void gvTeamSections_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvTeamSections.Rows[e.NewSelectedIndex];
        }

        public void bind2()
        {
            string sql_LoadGrid = "";
            try
            {
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BasketballTEAMSTeamNameSections  "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BaseballTeamNameSections  "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM OutreachBasketballTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MSBasketballLeagueTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM HSBasketballLeagueTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM [3on3BasketballTeamNameSections]  "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SoccerIntraMuralsTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SoccerTEAMSTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BibleStudyTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MondayNightsTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SpecialEventsTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MSHSChoirTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ChildrensChoirTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM PerformingArtsAcademyTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SingersTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ShakesTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ImpactUrbanSchoolsTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "SummerDayCamp")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SummerDayCampTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "SATPrep")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SATPrepTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }

                con.Open();//Opens the db connection.

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    da.Fill(ds, "BasketballTEAMSTeamNameSections");
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    da.Fill(ds, "BaseballTeamNameSections");
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    da.Fill(ds, "OutreachBasketballTeamNameSections");
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    da.Fill(ds, "HSBasketballLeagueTeamNameSections");
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    da.Fill(ds, "MSBasketballLeagueTeamNameSections");
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    da.Fill(ds, "SoccerIntraMuralsTeamNameSections");
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    da.Fill(ds, "SoccerTEAMSTeamNameSections");
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    da.Fill(ds, "[3on3BasketballTeamNameSections]");
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    da.Fill(ds, "[BibleStudyTeamNameSections]");
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    da.Fill(ds, "[MondayNightsTeamNameSections]");
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    da.Fill(ds, "[SpecialEventsTeamNameSections]");
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    da.Fill(ds, "MSHSChoirTeamNameSections");
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    da.Fill(ds, "[ChildrensChoirTeamNameSections]");
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    da.Fill(ds, "[PerformingArtsAcademyTeamNameSections]");
                }
                else if (ddlProgram.Text == "Singers")
                {
                    da.Fill(ds, "[SingersTeamNameSections]");
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    da.Fill(ds, "[ShakesTeamNameSections]");
                }
                else if (ddlProgram.Text == "ImpactUrbanSchools")
                {
                    da.Fill(ds, "[ImpactUrbanSchoolsTeamNameSections]");
                }
                else if (ddlProgram.Text == "SummerDayCamp")
                {
                    da.Fill(ds, "[SummerDayCampTeamNameSections]");
                }
                else if (ddlProgram.Text == "SATPrep")
                {
                    da.Fill(ds, "[SATPrepTeamNameSections]");
                }
                
                gvTeamSections.DataSource = ds.Tables[0];
                gvTeamSections.DataBind();
                gvTeamSections.Visible = true;
                gvStudentList.Visible = false;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
            }
        }  

        protected void gvStudentList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStudentList.EditIndex = e.NewEditIndex;
            bind();
        }

        protected void gvStudentList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gvStudentList.DataKeys[e.RowIndex].Values[0].ToString();
            RemoveFromTable(sender, e, ID);
        }

        protected void RemoveFromTable(object sender, GridViewDeleteEventArgs e, string ID)
        {
            string sql_DeleteFromClass1 = "";
            try
            {
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BaseballProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.OutreachBasketballProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.MSBasketballLeagueProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.HSBasketballLeagueProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.[3on3BasketballProgramSections] "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SoccerIntraMuralsProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SoccerTEAMSProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.MondayNightsProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BibleStudyProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SpecialEventsProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.MSHSChoirProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.ChildrensChoirProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SingersProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.ShakesProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.ImpactUrbanSchoolsProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SummerDayCamp")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SummerDayCampProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SATPrep")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SATPrepProgramSections "
                                         + "WHERE id = '" + ID + "' ";
                }

                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromClass1 = new SqlCommand(sql_DeleteFromClass1, con);
                if (sqlDeleteFromClass1.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                }
            }
            catch (Exception lkjlkj)
            {

            }
            finally
            {
                con.Close();
                bind();
            }
        }

        protected void gvTeamSections_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTeamSections.EditIndex = e.NewEditIndex;
            bind2();
        }

        protected void gvTeamSections_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gvTeamSections.DataKeys[e.RowIndex].Values[0].ToString();
            RemoveFromTeamSectionsTable(sender, e, ID);
        }

        protected void RemoveFromTeamSectionsTable(object sender, GridViewDeleteEventArgs e, string ID)
        {
            string sql_DeleteFromClass1 = "";
            try
            {
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BasketballTEAMSTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BaseballTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.OutreachBasketballTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.MSBasketballLeagueTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.HSBasketballLeagueTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.[3on3BasketballTeamNameSections] "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SoccerIntraMuralsTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SoccerTEAMSTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.MondayNightsTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BibleStudyTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SpecialEventsTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.MSHSChoirTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.ChildrensChoirTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SingersTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.ShakesTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.ImpactUrbanSchoolsTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SummerDayCamp")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SummerDayCampTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SATPrep")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SATPrepTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }

                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromClass1 = new SqlCommand(sql_DeleteFromClass1, con);
                if (sqlDeleteFromClass1.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                }
            }
            catch (Exception lkjlkj)
            {

            }
            finally
            {
                con.Close();
                bind2();
            }
        }

        protected void gvStudentList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
             gvStudentList.EditIndex = -1;  
             bind();  
        }

        protected void gvStudentList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvStudentList.Rows[e.RowIndex];
            //Label lbl = (Label)row.FindControl("lblid");
            TextBox sectionname = (TextBox)row.FindControl("textbox1");
            TextBox meettime = (TextBox)row.FindControl("textbox2");
            TextBox meetday = (TextBox)row.FindControl("textbox3");
            TextBox location = (TextBox)row.FindControl("textbox4");
            TextBox comments = (TextBox)row.FindControl("textbox5");
            TextBox staffleader = (TextBox)row.FindControl("textbox6");
            TextBox devotionalleader = (TextBox)row.FindControl("textbox7");
            TextBox ID = (TextBox)row.FindControl("textbox8");

            sectionname.Text = sectionname.Text.Replace("'", "");
            meettime.Text = meettime.Text.Replace("'", "");
            meetday.Text = meetday.Text.Replace("'", "");
            location.Text = location.Text.Replace("'", "");
            comments.Text = comments.Text.Replace("'", "");
            staffleader.Text = staffleader.Text.Replace("'", "");
            devotionalleader.Text = devotionalleader.Text.Replace("'", "");

            gvStudentList.EditIndex = -1;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            if (ddlProgram.Text == "BasketballTEAMS")
            {
                cmd = new SqlCommand("Update BasketballTEAMSProgramSections set "
                                    + "  staffleader='" + staffleader.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , sectionname='" + sectionname.Text
                                    + "' , meettime='" + meettime.Text
                                    + "' , meetday='" + meetday.Text
                                    + "' , location='" + location.Text
                                    + "' , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");

                KeepTablesInSynch("BasketballTEAMS", sectionname.Text, "hmmm");
            }
            else if (ddlProgram.Text == "Baseball")
            {
                cmd = new SqlCommand("Update BaseballProgramSections set "
                                    + "  staffleader='" + staffleader.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , sectionname='" + sectionname.Text
                                    + "' , meettime='" + meettime.Text
                                    + "' , meetday='" + meetday.Text
                                    + "' , location='" + location.Text
                                    + "' , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                cmd = new SqlCommand("Update OutreachBasketballProgramSections set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                cmd = new SqlCommand("Update SoccerIntraMuralsProgramSections set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                cmd = new SqlCommand("Update SoccerTEAMSProgramSections set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                cmd = new SqlCommand("Update MSBasketballLeagueProgramSections set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                cmd = new SqlCommand("Update HSBasketballLeagueProgramSections set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                cmd = new SqlCommand("Update [3on3BasketballProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                cmd = new SqlCommand("Update [BibleStudyProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                cmd = new SqlCommand("Update [MondayNightsProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Special Events")
            {
                cmd = new SqlCommand("Update [SpecialEventsProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "MSHS Choir")
            {
                cmd = new SqlCommand("Update MSHSChoirProgramSections set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Childrens Choir")
            {
                cmd = new SqlCommand("Update [ChildrensChoirProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "PerformingArtsAcademy")
            {
                cmd = new SqlCommand("Update [PerformingArtsAcademyProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Shakes")
            {
                cmd = new SqlCommand("Update [ShakesProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Singers")
            {
                cmd = new SqlCommand("Update [SingersProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Impact Urban Schools")
            {
                cmd = new SqlCommand("Update [ImpactUrbanSchoolsProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SummerDayCamp")
            {
                cmd = new SqlCommand("Update [SummerDayCampProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SATPrep")
            {
                cmd = new SqlCommand("Update [SATPrepProgramSections] set "
                                     + "  staffleader='" + staffleader.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            bind();

            KeepTablesInSynch(ddlProgram.Text + "ProgramSections", sectionname.Text, "");
            KeepEnrollmentTablesInSynch();
        }

        protected void KeepEnrollmentTablesInSynch()
        {

        }

        protected void KeepTablesInSynch(string tablename, string newsectionname, string oldsectionname)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd = new SqlCommand("Update " + tablename + "TeamNameSections  "
                            + "set sectionname='" + newsectionname + "' "
                            + "where sectionname = '" + oldsectionname + "' ");
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception lkjlkaa)
            {

            }
            finally
            {

            }
        }

        protected void gvTeamSections_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTeamSections.EditIndex = -1;
            bind2();
        }

        protected void gvTeamSections_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvTeamSections.Rows[e.RowIndex];
            TextBox sectionname = (TextBox)row.FindControl("textbox1");
            TextBox teamname = (TextBox)row.FindControl("textbox2");
            TextBox meettime = (TextBox)row.FindControl("textbox3");
            TextBox meetday = (TextBox)row.FindControl("textbox4");
            TextBox location = (TextBox)row.FindControl("textbox5");
            TextBox comments = (TextBox)row.FindControl("textbox6");
            TextBox supervisor = (TextBox)row.FindControl("textbox7");
            TextBox devotionalleader = (TextBox)row.FindControl("textbox8");
            TextBox ID = (TextBox)row.FindControl("textbox9");

            sectionname.Text = sectionname.Text.Replace("'","");
            teamname.Text = teamname.Text.Replace("'", "");
            meettime.Text = meettime.Text.Replace("'", "");
            meetday.Text = meetday.Text.Replace("'", "");
            location.Text = location.Text.Replace("'", "");
            comments.Text = comments.Text.Replace("'", "");
            supervisor.Text = supervisor.Text.Replace("'", "");
            devotionalleader.Text = devotionalleader.Text.Replace("'", "");

            gvStudentList.EditIndex = -1;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            if (ddlProgram.Text == "BasketballTEAMS")
            {
                cmd = new SqlCommand("Update BasketballTEAMSTeamNameSections set "
                                    + "  supervisor='" + supervisor.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , teamname='" + teamname.Text
                                    + "' , meettime='" + meettime.Text
                                    + "' , meetday='" + meetday.Text
                                    + "' , location='" + location.Text
                                    + "' , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Baseball")
            {
                cmd = new SqlCommand("Update BaseballTeamNameSections set "
                                    + "  supervisor='" + supervisor.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , teamname='" + teamname.Text
                                    + "' , meettime='" + meettime.Text
                                    + "' , meetday='" + meetday.Text
                                    + "' , location='" + location.Text
                                    + "' , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                cmd = new SqlCommand("Update OutreachBasketballTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                cmd = new SqlCommand("Update SoccerIntraMuralsTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                cmd = new SqlCommand("Update SoccerTEAMSTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                cmd = new SqlCommand("Update MSBasketballLeagueTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                cmd = new SqlCommand("Update HSBasketballLeagueTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                cmd = new SqlCommand("Update [3on3BasketballTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                cmd = new SqlCommand("Update [MondayNightsTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                cmd = new SqlCommand("Update [BibleStudyTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Special Events")
            {
                cmd = new SqlCommand("Update [SpecialEventsTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }

            else if (ddlProgram.Text == "MSHS Choir")
            {
                cmd = new SqlCommand("Update MSHSChoirProgramSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Childrens Choir")
            {
                cmd = new SqlCommand("Update [ChildrensChoirProgramSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "PerformingArtsAcademy")
            {
                cmd = new SqlCommand("Update [PerformingArtsAcademyProgramSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Shakes")
            {
                cmd = new SqlCommand("Update [ShakesProgramSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Singers")
            {
                cmd = new SqlCommand("Update [SingersProgramSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Impact Urban Schools")
            {
                cmd = new SqlCommand("Update [SingersProgramSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }

            else if (ddlProgram.Text == "SummerDayCamp")
            {
                cmd = new SqlCommand("Update [SummerDayCampProgramSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SATPrep")
            {
                cmd = new SqlCommand("Update [SATPrepProgramSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , sectionname='" + sectionname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            bind2();
        }
        
        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            if ((gvTeamSections.Rows.Count != 0))
            {
                gvTeamSections.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvTeamSections, Response);
            }
            else if (gvStudentList.Rows.Count != 0)
            {
                gvStudentList.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvStudentList, Response);
            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql_LoadGrid = "";

            gvStudentList.Visible = false;
            gvTeamSections.Visible = false;
            cmbAddRecord.Visible = false;
            txbClassLocation.Visible = false;
            txbClassName.Visible = false;
            txbComments.Visible = false;
            txbDay.Visible = false;
            txbInstructor.Visible = false;
            txbSizeLimit.Visible = false;
            txbDevotionalLeader.Visible = false;
            txbClassName.Visible = false;
            ddlTime.Visible = false;

            lblClassInstructor.Visible = false;
            lblClassLocation.Visible = false;
            lblComments.Visible = false;
            lblDay.Visible = false;
            lblTime.Visible = false;
            lblSizeLimit.Visible = false;
            lblClassName.Visible = false;
            lblDevotionalLeader.Visible = false;

            lbAddNewEntry.Visible = true;
            lbAddNewEntry.Enabled = true;
            try
            {
                if (ddlProgram.Text == "MSHS Choir")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MSHSChoirProgramSections  order by sectionName";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ChildrensChoirProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM PerformingArtsAcademyProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SingersProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ShakesProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM [ImpactUrbanSchoolsProgramSections]  order by sectionname";
                }

                con.Open();//Opens the db connection.

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();

                if (ddlProgram.Text == "MSHS Choir")
                {
                    da.Fill(ds, "MSHSChoirProgramSections");
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    da.Fill(ds, "ChildrensChoirProgramSections");
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    da.Fill(ds, "PerformingArtsAcademyProgramSections");
                }
                else if (ddlProgram.Text == "Singers")
                {
                    da.Fill(ds, "SingersProgramSections");
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    da.Fill(ds, "ShakesProgramSections");
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    da.Fill(ds, "ImpactUrbanSchoolsProgramSections");
                }
                gvStudentList.DataSource = ds.Tables[0];
                gvStudentList.DataBind();
                gvStudentList.Visible = true;
                lbAddNewEntry.Text = "Add a New Section";
                lblHeading.Visible = true;
                lblHeading.Text = "Program Sections";

                PopulateTeamSectionDropDown();
                ddlTeamNames.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
            }
        }

        protected void PopulateTeamSectionDropDown()
        {
            if (ddlProgram.Text != "Please select a program")
            {
                if (ddlProgram.Text == "MSHS Choir")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.MSHSChoirProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MSHSChoirProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MSHSChoirProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.ChildrensChoirProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "ChildrensChoirProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["ChildrensChoirProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.PerformingArtsAcademyProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "PerformingArtsAcademyProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["PerformingArtsAcademyProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Singers")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SingersProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SingersProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SingersProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.ShakesProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "ShakesProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["ShakesProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.ImpactUrbanSchoolsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "ImpactUrbanSchoolsProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["ImpactUrbanSchoolsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
            }
        }

        protected void CleanFields()
        {
            txbClassName.Text = txbClassName.Text.Replace("'", "");
            txbComments.Text = txbComments.Text.Replace("'", "");
            txbDay.Text = txbDay.Text.Replace("'", "");
            txbDevotionalLeader.Text = txbDevotionalLeader.Text.Replace("'", "");
            txbInstructor.Text = txbInstructor.Text.Replace("'", "");
            txbSizeLimit.Text = txbSizeLimit.Text.Replace("'", "");
            txbClassLocation.Text = txbClassLocation.Text.Replace("'", "");
        }

        protected void cmbAddRecord_Click(object sender, EventArgs e)
        {
            //Insert a new class record.
            string sqlInsertStatement = "";
            if ((ddlTeamNames.Text != "Please select a Section") && (ddlTeamNames.Visible))
            {
                txbClassName.Text = txbClassName.Text.Replace("'", "");
                txbComments.Text = txbComments.Text.Replace("'", "");
                txbDay.Text = txbDay.Text.Replace("'", "");
                txbDevotionalLeader.Text = txbDevotionalLeader.Text.Replace("'", "");
                txbInstructor.Text = txbInstructor.Text.Replace("'", "");
                txbSizeLimit.Text = txbSizeLimit.Text.Replace("'", "");
                txbClassLocation.Text = txbClassLocation.Text.Replace("'", "");
                Random randomNumber = new Random();
                string NewNumber = System.Guid.NewGuid().ToString();

                try
                {

                    if (ddlProgram.Text == "MSHS Choir")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.MSHSChoirTeamNameSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "', "
                            + "'" + txbTeamName.Text.Trim() + "', "
                            + "'" + ddlTime.Text.Trim() + "', "
                            + "'" + txbDay.Text.Trim() + "', "
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "Childrens Choir")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.ChildrensChoirTeamNameSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + txbTeamName.Text.Trim() + "', "
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "PerformingArtsAcademy")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.PerformingArtsAcademyTeamNameSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + txbTeamName.Text.Trim() + "', "
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "Singers")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.SingersTeamNameSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + txbTeamName.Text.Trim() + "', "
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "Shakes")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.ShakesTeamNameSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + txbTeamName.Text.Trim() + "', "
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "Impact Urban Schools")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.ImpactUrbanSchoolsTeamNameSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + txbTeamName.Text.Trim() + "', "
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }

                    con.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                    if (sqlInsertCommand.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        lbAddNewEntry.Visible = true;
                        ResetNewPerson();
                        gvTeamSections.DataBind();
                        DisplayTheGrid2();
                    }
                    else
                    {
                        //display message that record was NOT updated
                        //	btnContinue.Visible = false;
                        //	lblAlert.Visible = true;
                        //	lblAlert.Text = "No update. Error has occurred.";
                    }
                }
                catch (Exception lkjlaa)
                {

                }
                finally
                {
                    //con.Close();
                }
            }
            else
            {
                txbClassName.Text = txbClassName.Text.Replace("'", "");
                txbComments.Text = txbComments.Text.Replace("'", "");
                txbDay.Text = txbDay.Text.Replace("'", "");
                txbDevotionalLeader.Text = txbDevotionalLeader.Text.Replace("'", "");
                txbInstructor.Text = txbInstructor.Text.Replace("'", "");
                txbSizeLimit.Text = txbSizeLimit.Text.Replace("'", "");
                txbClassLocation.Text = txbClassLocation.Text.Replace("'", "");
                Random randomNumber = new Random();
                string NewNumber = System.Guid.NewGuid().ToString();
                
                try
                {

                    if (ddlProgram.Text == "MSHS Choir")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.MSHSChoirProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "', "
                            + "'" + ddlTime.Text.Trim() + "', "
                            + "'" + txbDay.Text.Trim() + "', "
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "Childrens Choir")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.ChildrensChoirProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "PerformingArtsAcademy")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.PerformingArtsAcademeyProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "Singers")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.SingersProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "Shakes")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.ShakesProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";
                    }
                    else if (ddlProgram.Text == "Impact Urban Schools")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.ImpactUrbanSchoolsProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + NewNumber.Substring(0, 10) + "') ";

                    }
                    
                    con.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                    if (sqlInsertCommand.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        lbAddNewEntry.Visible = true;
                        ResetNewPerson();
                        gvStudentList.DataBind();
                        DisplayTheGrid();
                    }
                    else
                    {
                        //display message that record was NOT updated
                        //	btnContinue.Visible = false;
                        //	lblAlert.Visible = true;
                        //	lblAlert.Text = "No update. Error has occurred.";
                    }
                }
                catch (Exception lkjlaa)
                {

                }
                finally
                {
                    //con.Close();
                }
            }
        }

        protected void DisplayTheGrid()
        {
            bind();
        }

        protected void DisplayTheGrid2()
        {
            bind2();
        }

        protected void ResetNewPerson()
        {
            txbComments.Visible = false;
            txbComments.Text = "";
            txbDay.Visible = false;
            txbDay.Text = "";
            txbClassName.Visible = false;
            txbClassName.Text = "";
            txbClassLocation.Visible = false;
            txbClassLocation.Text = "";
            txbTeamName.Text = "";
            txbTeamName.Visible = false;
            //lblTeamName.Visible = false;            
            txbDevotionalLeader.Visible = false;
            txbDevotionalLeader.Text = "";
            txbSizeLimit.Visible = false;
            txbSizeLimit.Text = "";
            txbInstructor.Visible = false;
            txbInstructor.Text = "";

            ddlTime.Visible = false;
            lblTime.Visible = false;
            cmbAddRecord.Visible = false;

            lblClassLocation.Visible = false;
            lblClassName.Visible = false;
            lblDay.Visible = false;
            lblComments.Visible = false;
            lblClassInstructor.Visible = false;
            lblDevotionalLeader.Visible = false;
            lblSizeLimit.Visible = false;
        }

        protected void lbAddNewEntry_Click(object sender, EventArgs e)
        {
            if ((ddlTeamNames.Text != "Please select a Section") && (ddlTeamNames.Visible))
            {
                txbClassName.Text = ddlTeamNames.Text.Trim();
                txbClassName.ReadOnly = true;
                txbTeamName.Visible = true;
                lblTeamName.Visible = true;
            }
            lbAddNewEntry.Visible = false;
            cmbAddRecord.Visible = true;
            ddlTime.Visible = true;
            txbClassLocation.Visible = true;
            txbClassName.Visible = true;
            txbComments.Visible = true;
            txbDevotionalLeader.Visible = true;
            txbInstructor.Visible = true;
            txbDay.Visible = true;

            lblClassInstructor.Visible = true;
            lblClassLocation.Visible = true;
            lblClassName.Visible = true;
            lblComments.Visible = true;
            lblDay.Visible = true;
            lblDevotionalLeader.Visible = true;
            lblTime.Visible = true;

            lblHeading.Visible = false;
        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArtsProgramSectionMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void ddlTeamNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql_LoadGrid = "";

            gvStudentList.Visible = false;
            cmbAddRecord.Visible = false;
            txbClassLocation.Visible = false;
            txbClassName.Visible = false;
            txbComments.Visible = false;
            txbDay.Visible = false;
            txbInstructor.Visible = false;
            txbSizeLimit.Visible = false;
            txbDevotionalLeader.Visible = false;
            txbClassName.Visible = false;
            ddlTime.Visible = false;

            lblClassInstructor.Visible = false;
            lblClassLocation.Visible = false;
            lblComments.Visible = false;
            lblDay.Visible = false;
            lblTime.Visible = false;
            lblSizeLimit.Visible = false;
            lblClassName.Visible = false;
            lblDevotionalLeader.Visible = false;

            lbAddNewEntry.Visible = true;
            lbAddNewEntry.Enabled = true;
            lbAddNewEntry.Text = "Add a New SubSection";

            lblHeading.Visible = true;
            lblHeading.Text = "SubSection Names";

            bind2();
        } 
    }
}