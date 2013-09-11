using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
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
using System.IO;


namespace UIF.PerformingArts
{
    public partial class AthleticsVolunteerMaintenance : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public SqlConnection con3 = new SqlConnection(connectionString);
        public static string Department = "";
        SqlDataReader reader = null;
                
        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean FoundRecord1 = false;
            Boolean FoundRecord2 = false;
            
            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];
                
                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);
                
                if (Request.QueryString["security"] == "Good")
                {
                    if (Department == "Athletics")
                    {
                        //ddlProgram.Items.Add("Childrens Choir");
                        //ddlProgram.Items.Add("MSHS Choir");
                        //ddlProgram.Items.Add("PerformingArtsAcademy");
                        //ddlProgram.Items.Add("Shakes");
                        //ddlProgram.Items.Add("Singers");

                        ddlProgram.Items.Add("Please select a program");
                        ddlProgram.Items.Add("Outreach Basketball");
                        ddlProgram.Items.Add("3on3 Basketball");
                        ddlProgram.Items.Add("BasketballTEAMS");
                        ddlProgram.Items.Add("SoccerIntraMurals");
                        ddlProgram.Items.Add("SoccerTEAMS");
                        ddlProgram.Items.Add("Baseball");
                        ddlProgram.Items.Add("Bible Study");
                        ddlProgram.Items.Add("HS Basketball League");
                        ddlProgram.Items.Add("MS Basketball League");
                        ddlProgram.Items.Add("MondayNights");
                        ddlProgram.Items.Add("Special Events");
                        ddlProgram.Text = "Please select a program";

                        //ddlTeamSections.Items.Add("Select a team?");
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

        protected void txbClass1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txbClass2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmdDropDown_Click(object sender, EventArgs e)
        {
            //Response.Redirect("uifadmin2.aspx");
            Response.Redirect("uifadmin2.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            string sql_UPDATE = "";
            string sql_UPDATE2 = "";

            try
            {
                con.Open();

            //    //Perform an update to the database.. for class1...RCM..1/31/11..
            //    sql_UPDATE = "Update PerformingArtsAcademyClassEnrollment  "
            //            + "Set ClassName = '" + ddlClass1.Text + "', "
            ////            + " MeetTime = '" + Label1.Text + "', "
            //            //+ " MeetDay = '" + ddlClass2.Text + "', "
            //            //+ " Location = '" + ddlClass2.Text + "', "
            //            + " PaidForClass = " + Convert.ToInt32(chbPaidClass1.Checked) + ", "
            //            + " Active = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
            //            + " Comments = '" + txbComments.Text + "', "
            //            //+ " Instructor = '" + ddlClass2.Text + "', "
            //            //+ " DevotionalLeader = '" + ddlClass2.Text + "', "
            //            + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
            //            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
            //            + " where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
            //            + " and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
            //            + " and meettime = '4:30-6:00 Class' ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql_UPDATE, con);
                cmd.Connection = con;
                //reader = cmd.ExecuteReader();
                if (cmd.ExecuteNonQuery() > 0)
                {	//Retrieve the first record only

                }

                //Perform an update to the database..for class2...RCM..1/31/11.
              //  sql_UPDATE2 = "Update PerformingArtsAcademyClassEnrollment  "
              //          + "Set ClassName = '" + ddlClass2.Text + "', "
              ////          + " MeetTime = '" + Label2.Text + "', "
              //          //+ " MeetDay = '" + ddlClass2.Text + "', "
              //          //+ " Location = '" + ddlClass2.Text + "', "
              //          + " PaidForClass = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
              //          + " Active = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
              //          + " Comments = '" + txbComments.Text + "', "
              //          //+ " Instructor = '" + ddlClass2.Text + "', "
              //          //+ " DevotionalLeader = '" + ddlClass2.Text + "', "
              //          + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
              //          + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
              //          + " where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
              //          + " and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
              //          + " and meettime = '6:30-8:00 Class' ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd2 = new SqlCommand(sql_UPDATE2, con);
                cmd2.Connection = con;
                //reader = cmd.ExecuteReader();
                if (cmd2.ExecuteNonQuery() > 0)
                {	//Retrieve the first record only

                }
            }
            catch (Exception lkjlkj)
            {
                string lkjl = "";
            }
            finally
            {
                con.Close();
            }
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + Request.QueryString["StudentLastName"] + "&StudentFirstName=" + Request.QueryString["StudentFirstName"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void chbClass1_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbClass1.Checked = true)
            //{
            //    chbClass1.Checked = false;
            //    ddlClass1.Enabled = true;
            //    cmdUpdate.Enabled = true;
            //    chbPaidClass1.Enabled = true;
            //}
            //else if (chbClass1.Checked = false)
            //{
            //    chbClass1.Checked = true;
            //    ddlClass1.Enabled = false;
            //}
        }

        protected void chbClass2_CheckedChanged(object sender, EventArgs e)
        { 
            //if (chbClass2.Checked = true)
            //{
            //    chbClass2.Checked = false;
            //    ddlClass2.Enabled = true;
            //    cmdUpdate.Enabled = true;
            //    chbPaidClass2.Enabled = true;
            //}
            //else if (chbClass2.Checked = false)
            //{
            //    chbClass2.Checked = true;
            //    ddlClass2.Enabled = false;
            //}
        }

        protected void lbClass1Attendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendanceOptions.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void lbClass2Attendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendanceOptions.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmbNewEnrollment_Click(object sender, EventArgs e)
        {
            ////Perform an INSERT operation to create a new record in the database..
            //string sql_INSERT = "";
            //string sql_INSERT2 = "";
            //cmbNewEnrollment.Enabled = false;
            try
            {
            //    con.Open();
            //    if (ddlClass1.Text != " ")
            //    {
            //        sql_INSERT = "INSERT INTO UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
            //                    + "values ( "
            //                    + "'" + Request.QueryString["StudentLastName"] + "', "
            //                    + "'" + Request.QueryString["StudentFirstName"] + "', "
            //                    + "'" + ddlClass1.Text + "', "
            //    //                + "'" + Label1.Text + "', "
            //                    //+ "'" + lblClass1MeetDay.Text + "', "
            //                    + "'Thursday', "
            //                    + "'N/A', "
            //                    //+ "'" + lblClass1MeetLocation.Text + "', "
            //                    + Convert.ToInt32(chbPaidClass1.Checked) + ", "
            //                    + "1, "
            //                    + "'" + txbComments.Text + "', "
            //                    + "'Instructor', "
            //                    + "'DevotionalLeader', "
            //                    + "'" + System.DateTime.Now.ToString() + "', "
            //                    + "'" + System.DateTime.Now.ToString() + "', "
            //                    + "333, "
            //                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "')";

            //        //create a SQL command to update record
            //        SqlCommand sql_InsertCommand = new SqlCommand(sql_INSERT, con);
            //        if (sql_InsertCommand.ExecuteNonQuery() > 0)
            //        {
            //            //maybe display a message confirming update has been successful
            //        }
            //        else
            //        {

            //        }
            //    }

                ////Check for the presence of a second class INSERT....RCM..1/29/11.
                //if (ddlClass2.Text != " ")
                //{
                //    sql_INSERT2 = "INSERT INTO PerformingArtsAcademyClassEnrollment "
                //                + "values ( "
                //                + "'" + Request.QueryString["StudentLastName"] + "', "
                //                + "'" + Request.QueryString["StudentFirstName"] + "', "
                //                + "'" + ddlClass2.Text + "', "
                //                + "'" + Label2.Text + "', "
                //                //+ "'" + lblClass2MeetDay.Text + "', "
                //                + "'Thursday', "
                //                + "'N/A', "
                //                //+ "'" + lblClass2MeetLocation.Text + "', "
                //                + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                //                + "1, "
                //                + "'" + txbComments.Text + "', "
                //                + "'Instructor', "
                //                + "'DevotionalLeader', "
                //                + "'" + System.DateTime.Now.ToString() + "', "
                //                + "'" + System.DateTime.Now.ToString() + "', "
                //                + "333, "
                //                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "')";

                //    //create a SQL command to update record
                //    SqlCommand sql_InsertCommand2 = new SqlCommand(sql_INSERT2, con);
                //    if (sql_InsertCommand2.ExecuteNonQuery() > 0)
                //    {
                //        //maybe display a message confirming update has been successful
                //    }
                //    else
                //    {

                //    }
                //}
            }
            catch (Exception lkjlk)
            {
                string lkjl = "";
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void cmbNewEnrollment2_Click(object sender, EventArgs e)
        {
            //Perform an INSERT operation to create a new record in the database..
            string sql_INSERT = "";
            string sql_INSERT2 = "";
//            cmbNewEnrollment2.Enabled = false;
            try
            {
                //con.Open();
                ////Check for the presence of a second class INSERT....RCM..1/29/11.
                //if (ddlClass2.Text != " ")
                //{
                //    sql_INSERT2 = "INSERT INTO PerformingArtsAcademyClassEnrollment "
                //                + "values ( "
                //                + "'" + Request.QueryString["StudentLastName"] + "', "
                //                + "'" + Request.QueryString["StudentFirstName"] + "', "
                //                + "'" + ddlClass2.Text + "', "
                //  //              + "'" + Label2.Text + "', "
                //                //+ "'" + lblClass2MeetDay.Text + "', "
                //                + "'Thursday', "
                //                + "'N/A', "
                //                //+ "'" + lblClass2MeetLocation.Text + "', "
                //                + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                //                + "1, "
                //                + "'" + txbComments.Text + "', "
                //                + "'Instructor', "
                //                + "'DevotionalLeader', "
                //                + "'" + System.DateTime.Now.ToString() + "', "
                //                + "'" + System.DateTime.Now.ToString() + "', "
                //                + "333, "
                //                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "')";

                //    //create a SQL command to update record
                //    SqlCommand sql_InsertCommand2 = new SqlCommand(sql_INSERT2, con);
                //    if (sql_InsertCommand2.ExecuteNonQuery() > 0)
                //    {
                //        //maybe display a message confirming update has been successful
                //    }
                //    else
                //    {

                //    }
                //}
            }
            catch (Exception lkjlk)
            {
                string lkjl = "";
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            UpdateInformation();
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UpdateInformation();
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void ddlClass2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbReport_Click(object sender, EventArgs e)
        {
            gvAttendanceList.Visible = false;
            lblTeamColors.Visible = false;
            lblTeamColor.Visible = false;
            ddlTeamSectionUpdate.Visible = false;
            gvReport.Visible = true;
                       
            if (ddlProgram.Text == "MS Basketball League")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("MSBasketballLeague");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("MSBasketballLeagueEnrollment", "MSBasketballLeague", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("MSBasketballLeagueEnrollment", "MSBasketballLeague", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionmsbasketballleague", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("HSBasketballLeague");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("HSBasketballLeagueEnrollment", "HSBasketballLeague", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("HSBasketballLeagueEnrollment", "HSBasketballLeague", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionhsbasketballleague", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "BasketballTEAMS")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("BasketballTEAMS");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("BasketballTEAMSEnrollment", "BasketballTEAMS", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("BasketballTEAMSEnrollment", "BasketballTEAMS", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionbasketballTEAMS", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Baseball")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    
                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("Baseball");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("BaseballEnrollment", "Baseball", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("BaseballEnrollment", "Baseball", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionbaseball", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("OutreachBasketball");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("OutreachBasketballEnrollment", "Outreach Basketball", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("OutreachBasketballEnrollment", "Outreach Basketball", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionoutreachbasketball", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("BibleStudy");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("BibleStudyEnrollment", "Bible Study", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("BibleStudyEnrollment", "Bible Study", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionbiblestudy", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("SoccerIntraMurals");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("SoccerIntraMuralsEnrollment", "SoccerIntraMurals", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("SoccerIntraMuralsEnrollment", "SoccerIntraMurals", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionsoccerintramurals", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjllaaabb)
                {



                }
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("SoccerTEAMS");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("SoccerTEAMSEnrollment", "SoccerTEAMS", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("SoccerTEAMSEnrollment", "SoccerTEAMS", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionsoccerteams", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("MondayNights");
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("MondayNightsEnrollment", "MondayNights", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("MondayNightsEnrollment", "MondayNights", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionmondaynights", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {
                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Special Events")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;
                    lblComments.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Current ProgramSeason...RCM..
                    string CurrentProgramSeason = DetermineCurrentProgramSeason("Special Events");

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("SpecialEventsEnrollment", "Special Events", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("SpecialEventsEnrollment", "Special Events", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("volunteerpivotsolutionspecialevents", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {
                    string lkjl = "";
                }
            }
        }

        protected string DetermineCurrentProgramSeason(string Program)
        {
            string ProgramSeason = "";

            try
            {
                if (ddlProgram.Text == "Outreach Basketball")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05"))
                    {
                        ProgramSeason = "WinterSpring" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08"))
                    {
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "Fall" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    ProgramSeason = "Spring" + DateTime.Now.ToString("yyyy");
                }
                else if (ddlProgram.Text == "BasketballTEAMS")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05"))
                    {
                        ProgramSeason = "WinterSpring" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08"))
                    {
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "Fall" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06"))
                    {
                        ProgramSeason = "SpringSummer" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "SummerFall" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06"))
                    {
                        ProgramSeason = "SpringSummer" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08"))
                    {
                        ProgramSeason = "SpringSummer" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "SummerFall" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "SummerFall" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05"))
                    {
                        ProgramSeason = "WinterSpring" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09"))
                    {
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09"))
                    {
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
            }
            catch (Exception lkjlkj)
            {


            }
            finally
            {
            }
            return ProgramSeason;
        }


        protected void ClearPage()
        {
            chbParentalConsentForm.Visible = false;
            chbRegistrationForm.Visible = false;
            chbPaid.Visible = false;
            chbGotPicture.Visible = false;
            chbContract.Visible = false;
            txbCoachTeam.Visible = false;
            lblName.Visible = false;
            lblName2.Visible = false;
            lblStudentNames.Visible = false;
            lblCoachTeam.Visible = false;
            imgStudent.Visible = false;
            cmbUpdate.Visible = false;
            txbComments.Visible = false;

            ddlTeamSections.Visible = false;
            lbStudentProfileLink.Visible = false;
            txbSPComments.Visible = false;
            lblSPNotes.Visible = false;
            lblComments.Visible = false;

            ddlProgramSection.Visible = false;
            lblProgramSections.Visible = false;
            ddlStudents.Visible = false;
            lblStudentNames.Visible = false;

        }

        protected void ClearPivotDataTable()
        {
            //Remove student from PivotData table..
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.VolunteerPivotData ";
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromPerformingArts = new SqlCommand(sql_DeleteFromPerformingArts, con);
                if (sqlDeleteFromPerformingArts.ExecuteNonQuery() > 0)
                {
                    //maybe display a message confirming update has been successful
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
            }
        }


        protected void ReloadPivotDataTable(string ProgramTable, string Program, string ProgramSeason, string SectionName)
        {
            string reload_sql = "";
            
            try
            {
                if (ProgramSeason == "")
                {
                    //By Program 
                    reload_sql = "insert into volunteerpivotdata "
                                + "select sie.StudentLastName,sie.StudentFirstName, sie.SectionName, si.CellPhone, si.HomePhone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions, spa.DAY, spa.Attended  "
                                + "from " + ProgramTable + " sie "
                                + "LEFT OUTER JOIN VolunteerProgramAttendance spa "
                                + "ON (sie.studentlastname = spa.lastname AND sie.studentfirstname = spa.firstname AND Program = '" + Program + "') "
                                + "LEFT OUTER JOIN VolunteerInformation si "
                                + "ON (sie.StudentLastName = si.LastName AND sie.StudentFirstName = si.FirstName) "
                                + "LEFT OUTER JOIN VolunteerDetails vd "
                                + "ON (sie.StudentLastName = vd.LastName AND sie.StudentFirstName = vd.FirstName) "
                                + "WHERE sie.volunteer = 1 "
                                + "GROUP BY sie.StudentLastName,sie.StudentFirstName, sie.SectionName, si.CellPhone, si.HomePhone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions, spa.DAY, spa.Attended  "
                                + "ORDER BY sie.StudentLastName, sie.StudentFirstName ";
                }
                else
                {
                    //By ProgramSeason, SectionName
                    reload_sql = "insert into volunteerpivotdata "
                                + "select sie.StudentLastName,sie.StudentFirstName, sie.SectionName, si.CellPhone, si.HomePhone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions, spa.DAY, spa.Attended  "
                                + "from " + ProgramTable + " sie "
                                + "LEFT OUTER JOIN VolunteerProgramAttendance spa "
                                + "ON (sie.studentlastname = spa.lastname AND sie.studentfirstname = spa.firstname AND Program = '" + Program + "') "
                                + "LEFT OUTER JOIN VolunteerInformation si "
                                + "ON (sie.StudentLastName = si.LastName AND sie.StudentFirstName = si.FirstName) "
                                + "LEFT OUTER JOIN VolunteerDetails vd "
                                + "ON (sie.StudentLastName = vd.LastName AND sie.StudentFirstName = vd.FirstName) "
                                + "WHERE sie.volunteer = 1 "
                                + "AND spa.ProgramSeason = '" + ProgramSeason + "' "
                                + "AND spa.Section = '" + SectionName + "' "
                                + "GROUP BY sie.StudentLastName,sie.StudentFirstName, sie.SectionName, si.CellPhone, si.HomePhone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions, spa.DAY, spa.Attended  "
                                + "ORDER BY sie.StudentLastName, sie.StudentFirstName ";
                }
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand = new SqlCommand(reload_sql, con);
                if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                    //Didn't find a record to update..RCM..
                }
            }
            catch (Exception lkjlkaaabbb)
            {


            }
        }


        protected void ReloadPivotDataTable(string ProgramTable, string Program, string ProgramSeason)
        {
            string reload_sql = "";

            try
            {
                if (ProgramSeason == "")
                {
                    //By Program 
                    reload_sql = "insert into volunteerpivotdata "
                                + "select sie.StudentLastName,sie.StudentFirstName, sie.SectionName, si.CellPhone, si.HomePhone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions, spa.DAY, spa.Attended  "
                                + "from " + ProgramTable + " sie "
                                + "LEFT OUTER JOIN VolunteerProgramAttendance spa "
                                + "ON (sie.studentlastname = spa.lastname AND sie.studentfirstname = spa.firstname AND Program = '" + Program + "') "
                                + "LEFT OUTER JOIN VolunteerInformation si "
                                + "ON (sie.StudentLastName = si.LastName AND sie.StudentFirstName = si.FirstName) "
                                + "LEFT OUTER JOIN VolunteerDetails vd "
                                + "ON (sie.StudentLastName = vd.LastName AND sie.StudentFirstName = vd.FirstName) "
                                + "WHERE sie.volunteer = 1 "
                                + "GROUP BY sie.StudentLastName,sie.StudentFirstName, sie.SectionName, si.CellPhone, si.HomePhone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions, spa.DAY, spa.Attended  "
                                + "ORDER BY sie.StudentLastName, sie.StudentFirstName ";
                }
                else
                {
                    //By ProgramSeason.
                    reload_sql = "insert into volunteerpivotdata "
                                + "select sie.StudentLastName,sie.StudentFirstName, sie.SectionName, si.CellPhone, si.HomePhone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions, spa.DAY, spa.Attended  "
                                + "from " + ProgramTable + " sie "
                                + "LEFT OUTER JOIN VolunteerProgramAttendance spa "
                                + "ON (sie.studentlastname = spa.lastname AND sie.studentfirstname = spa.firstname AND Program = '" + Program + "') "
                                + "LEFT OUTER JOIN VolunteerInformation si "
                                + "ON (sie.StudentLastName = si.LastName AND sie.StudentFirstName = si.FirstName) "
                                + "LEFT OUTER JOIN VolunteerDetails vd "
                                + "ON (sie.StudentLastName = vd.LastName AND sie.StudentFirstName = vd.FirstName) "
                                + "WHERE sie.volunteer = 1 "
                                + "AND spa.ProgramSeason = '" + ProgramSeason + "' "
                                + "GROUP BY sie.StudentLastName,sie.StudentFirstName, sie.SectionName, si.CellPhone, si.HomePhone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions, spa.DAY, spa.Attended  "
                                + "ORDER BY sie.StudentLastName, sie.StudentFirstName ";
                }
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand = new SqlCommand(reload_sql, con);
                if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                    //Didn't find a record to update..RCM..
                }
            }
            catch (Exception lkjlkaaabbb)
            {

            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlTeamSectionUpdate.Visible = false;
            lblTeamColor.Visible = false;
            lblComments.Visible = false;
            lblSPNotes.Visible = false;
            txbSPComments.Visible = false;
            lbStudentProfileLink.Visible = false;
            ddlTeamSections.Visible = false;
            lblTeamColors.Visible = false;
            gvAttendanceList.Visible = false;
            cmbCallingListReport.Enabled = true;
            gvReport.Visible = false;

            if (ddlProgram.Text != "Please select a program")
            {
                cmbRetreiveProgram.Enabled = true;
                ddlProgramSection.Visible = false;
                ddlStudents.Visible = false;
                lblProgramSections.Visible = false;
                ddlProgramSection.Items.Clear();
                ddlStudents.Items.Clear();


                chbParentalConsentForm.Visible = false;
                chbRegistrationForm.Visible = false;
                chbPaid.Visible = false;
                chbGotPicture.Visible = false;
                chbContract.Visible = false;
                txbCoachTeam.Visible = false;
                lblName.Visible = false;
                lblName2.Visible = false;
                lblStudentNames.Visible = false;
                lblCoachTeam.Visible = false;
                imgStudent.Visible = false;
                cmbUpdate.Visible = false;
                txbComments.Visible = false;

                cmbReport.Enabled = true;

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BasketballTEAMSProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.BaseballProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BaseballProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BaseballProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.MSBasketballLeagueProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MSBasketballLeagueProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MSBasketballLeagueProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.HSBasketballLeagueProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "HSBasketballLeagueProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["HSBasketballLeagueProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.OutReachBasketballProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "OutReachBasketballProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["OutReachBasketballProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.[3on3BasketballProgramSections] "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "[3on3BasketballProgramSections]");

                    ddlProgramSection.Items.Add("Please select a section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["[3on3BasketballProgramSections]"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SoccerIntraMuralsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SoccerIntraMuralsProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SoccerIntraMuralsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SoccerTEAMSProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SoccerTEAMSProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SoccerTEAMSProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.BibleStudyProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BibleStudyProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BibleStudyProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.MondayNightsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MondayNightsProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MondayNightsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SpecialEventsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SpecialEventsProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SpecialEventsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
            }
            //UpdateInformation();
        }

        protected void cmbRetreiveProgram_Click(object sender, EventArgs e)
        {
            //if (ddlProgram.Text == "BasketballTEAMS")
            //{
            //    //Load the dropdown list for the sections.
            //    string sql = "Select sectionname "
            //               + "from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
            //               + "group by sectionname "
            //               + "order by sectionname ";

            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //    SqlDataAdapter custDA = new SqlDataAdapter();
            //    custDA.SelectCommand = cmd;
            //    DataSet custDS = new DataSet();
            //    custDA.Fill(custDS, "BasketballTEAMSProgramSections");

            //    ddlProgramSection.Items.Add("Please select a section");
            //    //ddlProgramSection.Items.Add(" ");
            //    //Iterate over setup records and call method to do the work on each one...RCM..
            //    foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSProgramSections"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
            //    }
            //    custDS.Clear();
            //    ddlProgramSection.Text = "Please select a section";
            //    lblProgramSections.Visible = true;
            //    ddlProgramSection.Visible = true;
            //    ddlProgramSection.Enabled = true;
            //    cmbRetreiveProgram.Enabled = false;
            //}
            //else if (ddlProgram.Text == "Baseball")
            //{
            //    //string sql = "Select sectionname "
            //    //           + "from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
            //    //           + "group by sectionname "
            //    //           + "order by sectionname ";

            //    //SqlCommand cmd = new SqlCommand(sql, con);
            //    //cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //    //SqlDataAdapter custDA = new SqlDataAdapter();
            //    //custDA.SelectCommand = cmd;
            //    //DataSet custDS = new DataSet();
            //    //custDA.Fill(custDS, "BasketballTEAMSProgramSections");

            //    //ddlProgramSection.Items.Add("Please select a section");
            //    ////ddlProgramSection.Items.Add(" ");
            //    ////Iterate over setup records and call method to do the work on each one...RCM..
            //    //foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSProgramSections"].Rows)
            //    //{
            //    //    //Adding options to the drop downs for a new entry.
            //    //    ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
            //    //}
            //    //custDS.Clear();
            //    //ddlProgramSection.Text = "Please select a section";
            //    //lblProgramSections.Visible = true;
            //    //ddlProgramSection.Visible = true;
            //    //ddlProgramSection.Enabled = true;
            //}
            //else if (ddlProgram.Text == "MS Basketball League")
            //{
            //    //Load the dropdown list for the sections.
            //    string sql = "Select bte.studentlastname, bte.studentfirstname  "
            //                + "from UIF_PerformingArts.dbo.msbasketballleagueenrollment bte "
            //                + "where bte.volunteer = 1 "
            //                + "group by bte.studentlastname, bte.studentfirstname "
            //                + "order by bte.studentlastname, bte.studentfirstname ";

            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //    SqlDataAdapter custDA = new SqlDataAdapter();
            //    custDA.SelectCommand = cmd;
            //    DataSet custDS = new DataSet();
            //    custDA.Fill(custDS, "MSBasketballLeagueEnrollment");

            //    //Clear the Students dropdown list..RCM..
            //    chbParentalConsentForm.Visible = false;
            //    chbRegistrationForm.Visible = false;
            //    chbPaid.Visible = false;
            //    chbGotPicture.Visible = false;
            //    chbContract.Visible = false;
            //    txbCoachTeam.Visible = false;
            //    lblName.Visible = false;
            //    lblName2.Visible = false;
            //    lblStudentNames.Visible = false;
            //    imgStudent.Visible = false;
            //    cmbUpdate.Visible = false;
            //    txbComments.Visible = false;
            //    lblCoachTeam.Visible = false;

            //    //Reset dropdown list.
            //    ddlStudents.Items.Clear();
            //    ddlStudents.Items.Add("Please select a volunteer");
            //    //ddlProgramSection.Items.Add(" ");
            //    //Iterate over setup records and call method to do the work on each one...RCM..
            //    foreach (DataRow myDataRowPO in custDS.Tables["MSBasketballLeagueEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //    custDS.Clear();
            //    ddlStudents.Text = "Please select a volunteer";
            //    //lblProgramSections.Visible = true;
            //    //ddlProgramSection.Visible = true;
            //    //ddlProgramSection.Enabled = true;
            //    lblStudentNames.Visible = true;
            //    ddlStudents.Visible = true;
            //}
            //else if (ddlProgram.Text == "HS Basketball League")
            //{
            //    //Load the dropdown list for the sections.
            //    string sql = "Select bte.studentlastname, bte.studentfirstname  "
            //                + "from UIF_PerformingArts.dbo.hsbasketballleagueenrollment bte "
            //                + "where bte.volunteer = 1 "
            //                + "group by bte.studentlastname, bte.studentfirstname "
            //                + "order by bte.studentlastname, bte.studentfirstname ";

            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //    SqlDataAdapter custDA = new SqlDataAdapter();
            //    custDA.SelectCommand = cmd;
            //    DataSet custDS = new DataSet();
            //    custDA.Fill(custDS, "HSBasketballLeagueEnrollment");

            //    //Clear the Students dropdown list..RCM..
            //    chbParentalConsentForm.Visible = false;
            //    chbRegistrationForm.Visible = false;
            //    chbPaid.Visible = false;
            //    chbGotPicture.Visible = false;
            //    chbContract.Visible = false;
            //    txbCoachTeam.Visible = false;
            //    lblName.Visible = false;
            //    lblName2.Visible = false;
            //    lblStudentNames.Visible = false;
            //    imgStudent.Visible = false;
            //    cmbUpdate.Visible = false;
            //    txbComments.Visible = false;
            //    lblCoachTeam.Visible = false;

            //    //Reset dropdown list.
            //    ddlStudents.Items.Clear();
            //    ddlStudents.Items.Add("Please select a volunteer");
            //    //ddlProgramSection.Items.Add(" ");
            //    //Iterate over setup records and call method to do the work on each one...RCM..
            //    foreach (DataRow myDataRowPO in custDS.Tables["HSBasketballLeagueEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //    custDS.Clear();
            //    ddlStudents.Text = "Please select a volunteer";
            //    //lblProgramSections.Visible = true;
            //    //ddlProgramSection.Visible = true;
            //    //ddlProgramSection.Enabled = true;
            //    lblStudentNames.Visible = true;
            //    ddlStudents.Visible = true;
            //}
            //else if (ddlProgram.Text == "BoysOutreach Basketball")
            //{
            //    //string sql = "Select sectionname "
            //    //           + "from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
            //    //           + "group by sectionname "
            //    //           + "order by sectionname ";

            //    //SqlCommand cmd = new SqlCommand(sql, con);
            //    //cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //    //SqlDataAdapter custDA = new SqlDataAdapter();
            //    //custDA.SelectCommand = cmd;
            //    //DataSet custDS = new DataSet();
            //    //custDA.Fill(custDS, "BasketballTEAMSProgramSections");

            //    //ddlProgramSection.Items.Add("Please select a section");
            //    ////ddlProgramSection.Items.Add(" ");
            //    ////Iterate over setup records and call method to do the work on each one...RCM..
            //    //foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSProgramSections"].Rows)
            //    //{
            //    //    //Adding options to the drop downs for a new entry.
            //    //    ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
            //    //}
            //    //custDS.Clear();
            //    //ddlProgramSection.Text = "Please select a section";
            //    //lblProgramSections.Visible = true;
            //    //ddlProgramSection.Visible = true;
            //    //ddlProgramSection.Enabled = true;
            //}
            //else if (ddlProgram.Text == "GirlsOutreach Basketball")
            //{
            //    //string sql = "Select sectionname "
            //    //           + "from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
            //    //           + "group by sectionname "
            //    //           + "order by sectionname ";

            //    //SqlCommand cmd = new SqlCommand(sql, con);
            //    //cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //    //SqlDataAdapter custDA = new SqlDataAdapter();
            //    //custDA.SelectCommand = cmd;
            //    //DataSet custDS = new DataSet();
            //    //custDA.Fill(custDS, "BasketballTEAMSProgramSections");

            //    //ddlProgramSection.Items.Add("Please select a section");
            //    ////ddlProgramSection.Items.Add(" ");
            //    ////Iterate over setup records and call method to do the work on each one...RCM..
            //    //foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSProgramSections"].Rows)
            //    //{
            //    //    //Adding options to the drop downs for a new entry.
            //    //    ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
            //    //}
            //    //custDS.Clear();
            //    //ddlProgramSection.Text = "Please select a section";
            //    //lblProgramSections.Visible = true;
            //    //ddlProgramSection.Visible = true;
            //    //ddlProgramSection.Enabled = true;
            //}
            //else
            //{
            //    if (ddlProgram.Text == "3on3 Basketball")
            //    {
            //        //con.Open();

            //        //string sql_LoadGrid = "";
            //        //sql_LoadGrid = "Select studentlastname, studentfirstname, '' as 'Attended'  "
            //        //             + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
            //        //             + "where sectionname = '" + ddlProgramSection.Text + "' "
            //        //    //+ "and classname = '" + ddlClassSelection.Text + "' "
            //        //    //+ "and paidforclass = 1 ".tex
            //        //             + "GROUP BY studentlastname, studentfirstname "
            //        //             + "order by studentlastname, studentfirstname ";

            //        //SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //        //DataSet ds = new DataSet();
            //        //da.Fill(ds, "BasketballTEAMSEnrollment");
            //        //gvReport.DataSource = ds.Tables[0];
            //        //gvReport.DataBind();
            //        //con.Close();
            //    }
            //    else if (ddlProgram.Text == "SoccerIntraMurals")
            //    {
            //        //con.Open();

            //        //string sql_LoadGrid = "";
            //        //sql_LoadGrid = "Select studentlastname, studentfirstname, '' as 'Attended'  "
            //        //             + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
            //        //             + "where sectionname = '" + ddlProgramSection.Text + "' "
            //        //    //+ "and classname = '" + ddlClassSelection.Text + "' "
            //        //    //+ "and paidforclass = 1 ".tex
            //        //             + "GROUP BY studentlastname, studentfirstname "
            //        //             + "order by studentlastname, studentfirstname ";

            //        //SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //        //DataSet ds = new DataSet();
            //        //da.Fill(ds, "BasketballTEAMSEnrollment");
            //        //gvReport.DataSource = ds.Tables[0];
            //        //gvReport.DataBind();
            //        //con.Close();
            //    }
            //    else if (ddlProgram.Text == "SoccerTEAMS")
            //    {
            //        //con.Open();

            //        //string sql_LoadGrid = "";
            //        //sql_LoadGrid = "Select studentlastname, studentfirstname, '' as 'Attended'  "
            //        //             + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
            //        //             + "where sectionname = '" + ddlProgramSection.Text + "' "
            //        //    //+ "and classname = '" + ddlClassSelection.Text + "' "
            //        //    //+ "and paidforclass = 1 ".tex
            //        //             + "GROUP BY studentlastname, studentfirstname "
            //        //             + "order by studentlastname, studentfirstname ";

            //        //SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //        //DataSet ds = new DataSet();
            //        //da.Fill(ds, "BasketballTEAMSEnrollment");
            //        //gvReport.DataSource = ds.Tables[0];
            //        //gvReport.DataBind();
            //        //con.Close();
            //    }
            //    else if (ddlProgram.Text == "Bible Study")
            //    {
            //        //con.Open();

            //        //string sql_LoadGrid = "";
            //        //sql_LoadGrid = "Select studentlastname, studentfirstname, '' as 'Attended'  "
            //        //             + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
            //        //             + "where sectionname = '" + ddlProgramSection.Text + "' "
            //        //    //+ "and classname = '" + ddlClassSelection.Text + "' "
            //        //    //+ "and paidforclass = 1 ".tex
            //        //             + "GROUP BY studentlastname, studentfirstname "
            //        //             + "order by studentlastname, studentfirstname ";

            //        //SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //        //DataSet ds = new DataSet();
            //        //da.Fill(ds, "BasketballTEAMSEnrollment");
            //        //gvReport.DataSource = ds.Tables[0];
            //        //gvReport.DataBind();
            //        //con.Close();
            //    }
            //    else if (ddlProgram.Text == "HS Basketball League")
            //    {
            //        //con.Open();

            //        //string sql_LoadGrid = "";
            //        //sql_LoadGrid = "Select studentlastname, studentfirstname, '' as 'Attended'  "
            //        //             + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
            //        //             + "where sectionname = '" + ddlProgramSection.Text + "' "
            //        //    //+ "and classname = '" + ddlClassSelection.Text + "' "
            //        //    //+ "and paidforclass = 1 ".tex
            //        //             + "GROUP BY studentlastname, studentfirstname "
            //        //             + "order by studentlastname, studentfirstname ";

            //        //SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //        //DataSet ds = new DataSet();
            //        //da.Fill(ds, "BasketballTEAMSEnrollment");
            //        //gvReport.DataSource = ds.Tables[0];
            //        //gvReport.DataBind();
            //        //con.Close();
            //    }
            //    else if (ddlProgram.Text == "MondayNights")
            //    {
            //        //con.Open();

            //        //string sql_LoadGrid = "";
            //        //sql_LoadGrid = "Select studentlastname, studentfirstname, '' as 'Attended'  "
            //        //             + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
            //        //             + "where sectionname = '" + ddlProgramSection.Text + "' "
            //        //    //+ "and classname = '" + ddlClassSelection.Text + "' "
            //        //    //+ "and paidforclass = 1 ".tex
            //        //             + "GROUP BY studentlastname, studentfirstname "
            //        //             + "order by studentlastname, studentfirstname ";

            //        //SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //        //DataSet ds = new DataSet();
            //        //da.Fill(ds, "BasketballTEAMSEnrollment");
            //        //gvReport.DataSource = ds.Tables[0];
            //        //gvReport.DataBind();
            //        //con.Close();
            //    }
            //}
        }

        protected void ddlProgramSection_SelectedIndexChanged(object sender, EventArgs e)
        {

            PopulateStudentsDropDown();
            lbStudentProfileLink.Visible = false;

            string sql = "";
            if (ddlProgram.Text == "BasketballTEAMS")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.BasketballTEAMSTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.OutreachBasketballTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "Baseball")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.BaseballTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.SoccerTEAMSTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.SoccerIntraMuralsTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.MSBasketballLeagueTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.HSBasketballLeagueTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.[3on3BasketballTeamNameSections] bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.MondayNightsTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.BibleStudyTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }
            else if (ddlProgram.Text == "Special Events")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.TeamName  "
                    + "from UIF_PerformingArts.dbo.SpecialEventsTeamNameSections bte "
                    + "where bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.TeamName "
                    + "order by bte.TeamName";
            }

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA = new SqlDataAdapter();
            custDA.SelectCommand = cmd;
            DataSet custDS = new DataSet();

            if (ddlProgram.Text == "BasketballTEAMS")
            {
                custDA.Fill(custDS, "BasketballTEAMSTeamNameSections");
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                custDA.Fill(custDS, "OutreachBasketballTeamNameSections");
            }
            else if (ddlProgram.Text == "Baseball")
            {
                custDA.Fill(custDS, "BaseballTeamNameSections");
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                custDA.Fill(custDS, "SoccerTEAMSTeamNameSections");
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                custDA.Fill(custDS, "SoccerIntraMuralsTeamNameSections");
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                custDA.Fill(custDS, "MSBasketballLeagueTeamNameSections");
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                custDA.Fill(custDS, "HSBasketballLeagueTeamNameSections");
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                custDA.Fill(custDS, "[3on3BasketballTeamNameSections]");
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                custDA.Fill(custDS, "[MondayNightsTeamNameSections]");
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                custDA.Fill(custDS, "[BibleStudyTeamNameSections]");
            }
            else if (ddlProgram.Text == "Special Events")
            {
                custDA.Fill(custDS, "[SpecialEventsTeamNameSections]");
            }

            //Clear the Students dropdown list..RCM..
            chbParentalConsentForm.Visible = false;
            chbRegistrationForm.Visible = false;
            chbPaid.Visible = false;
            chbGotPicture.Visible = false;
            chbContract.Visible = false;
            txbCoachTeam.Visible = false;
            lblName.Visible = false;
            lblName2.Visible = false;
            lblStudentNames.Visible = false;
            imgStudent.Visible = false;
            cmbUpdate.Visible = false;
            txbComments.Visible = false;
            lblCoachTeam.Visible = false;
            gvAttendanceList.Visible = false;

            //Reset dropdown list.
            //ddlStudents.Items.Clear();
            //ddlStudents.Items.Add("Please select a volunteer");
            ddlTeamSections.Items.Clear();
            ddlTeamSections.Items.Add("Select a team?");
            ddlTeamSectionUpdate.Items.Clear();
            ddlTeamSectionUpdate.Items.Add("Select a team?");
            //ddlProgramSection.Items.Add(" ");
            //Iterate over setup records and call method to do the work on each one...RCM..
            if (ddlProgram.Text == "BasketballTEAMS")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSTeamNameSections"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["OutreachBasketballTeamNameSections"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["SoccerTEAMSTeamNameSections"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "Baseball")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["BaseballTeamNameSections"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["SoccerIntraMuralsTeamNameSections"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["MSBasketballLeagueTeamNameSections"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["HSBasketballLeagueTeamNameSections"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["[3on3BasketballTeamNameSections]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["[BibleStudyTeamNameSections]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["[MondayNightsTeamNameSections]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
            else if (ddlProgram.Text == "SpecialEvents")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["[SpecialEventsTeamNameSections]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlTeamSectionUpdate.Items.Add(myDataRowPO[0].ToString());
                    //ddlStudents.Items.Add(myDataRowPO[0].ToString());
                }
            }
                        
            custDS.Clear();
            ddlStudents.Text = "Please select a volunteer";
            lblProgramSections.Visible = true;
            lblStudentNames.Visible = true;
            ddlProgramSection.Visible = true;
            ddlProgramSection.Enabled = true;
            ddlTeamSections.Visible = true;
            lblTeamColors.Visible = true;
            lblStudentNames.Visible = true;
            ddlStudents.Visible = true;
            ddlTeamSectionUpdate.Visible = false;
            lblTeamColor.Visible = false;
            txbSPComments.Visible = false;
            lblSPNotes.Visible = false;
            lblComments.Visible = false;
            UpdateInformation();
        }

        protected void PopulateStudentsDropDown()
        {

            string sql = "";
            if (ddlProgram.Text == "BasketballTEAMS")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.OutreachBasketballEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "Baseball")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.BaseballEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.SoccerTEAMSEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.SoccerIntraMuralsEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.MSBasketballLeagueEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.HSBasketballLeagueEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.[3on3BasketballEnrollment] bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.MondayNightsEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.BibleStudyEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }
            else if (ddlProgram.Text == "Special Events")
            {
                //Load the dropdown list for the sections.
                sql = "Select bte.Studentlastname, bte.studentfirstname  "
                    + "from UIF_PerformingArts.dbo.SpecialEventsEnrollment bte "
                    + "where bte.volunteer = 1 "
                    + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                    + "group by bte.studentlastname, bte.studentfirstname "
                    + "order by bte.studentlastname ";
            }

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA = new SqlDataAdapter();
            custDA.SelectCommand = cmd;
            DataSet custDS = new DataSet();

            if (ddlProgram.Text == "BasketballTEAMS")
            {
                custDA.Fill(custDS, "BasketballTEAMSEnrollment");
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                custDA.Fill(custDS, "OutreachBasketballEnrollment");
            }
            else if (ddlProgram.Text == "Baseball")
            {
                custDA.Fill(custDS, "BaseballEnrollment");
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                custDA.Fill(custDS, "SoccerTEAMSEnrollment");
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                custDA.Fill(custDS, "SoccerIntraMuralsEnrollment");
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                custDA.Fill(custDS, "MSBasketballLeagueEnrollment");
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                custDA.Fill(custDS, "HSBasketballLeagueEnrollment");
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                custDA.Fill(custDS, "[3on3BasketballEnrollment]");
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                custDA.Fill(custDS, "[MondayNightsEnrollment]");
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                custDA.Fill(custDS, "[BibleStudyEnrollment]");
            }
            else if (ddlProgram.Text == "Special Events")
            {
                custDA.Fill(custDS, "[SpecialEventsEnrollment]");
            }

            //Clear the Students dropdown list..RCM..
            chbParentalConsentForm.Visible = false;
            chbRegistrationForm.Visible = false;
            chbPaid.Visible = false;
            chbGotPicture.Visible = false;
            chbContract.Visible = false;
            txbCoachTeam.Visible = false;
            lblName.Visible = false;
            lblName2.Visible = false;
            lblStudentNames.Visible = false;
            imgStudent.Visible = false;
            cmbUpdate.Visible = false;
            txbComments.Visible = false;
            lblCoachTeam.Visible = false;

            //Reset dropdown list.
            ddlStudents.Items.Clear();
            ddlStudents.Items.Add("Please select a volunteer");
            //ddlTeamSections.Items.Clear();
            //ddlTeamSections.Items.Add("Select a team?");
            //ddlProgramSection.Items.Add(" ");
            //Iterate over setup records and call method to do the work on each one...RCM..
            if (ddlProgram.Text == "BasketballTEAMS")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSEnrollment"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["OutreachBasketballEnrollment"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["SoccerTEAMSEnrollment"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "Baseball")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["BaseballEnrollment"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["SoccerIntraMuralsEnrollment"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["MSBasketballLeagueEnrollment"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["HSBasketballLeagueEnrollment"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["[3on3BasketballEnrollment]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["[BibleStudyEnrollment]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["[MondayNightsEnrollment]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
            else if (ddlProgram.Text == "Special Events")
            {
                foreach (DataRow myDataRowPO in custDS.Tables["[SpecialEventsEnrollment]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                    ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
            }
        }
        
        public void bind()
        {
            con.Open();
            
            string sql_LoadGrid = "";
            sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, si.currentregistrationform, si.parentalconsentform, bte.contract, bte.paid, bte.coachteam, bte.gotpicture   "
                         + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                         + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                         + "WHERE bte.sectionname = '" + ddlProgramSection.Text + "' "
                         + "AND bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                         + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                         + "GROUP BY bte.studentlastname, bte.studentfirstname, si.currentregistrationform, si.parentalconsentform, bte.contract, bte.paid, bte.coachteam, bte.gotpicture "
                         + "order by bte.studentlastname, bte.studentfirstname ";
            
            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "BasketballTEAMSEnrollment");
            gvReport.DataSource = ds.Tables[0];
            gvReport.DataBind();
            gvReport.Visible = true;
            gvReport.Enabled = true;
            con.Close();
        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("AthleticsVolunteerMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void ddlStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

            gvAttendanceList.Visible = false;
            
            con.Open();//Opens the db connection.
            string sql = "";
            string sql_LoadGrid = "";
            if (ddlProgram.Text == "MS Basketball League")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor  "
                             + "from UIF_PerformingArts.dbo.MSBasketballLeagueEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            if (ddlProgram.Text == "HS Basketball League")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.HSBasketballLeagueEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "BasketballTEAMS")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.sectionname = '" + ddlProgramSection.Text + "' "
                             + "AND bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "Baseball")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.BaseballEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.sectionname = '" + ddlProgramSection.Text + "' "
                             + "AND bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.OutreachBasketballEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.sectionname = '" + ddlProgramSection.Text + "' "
                             + "AND bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.SoccerIntraMuralsEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.SoccerTEAMSEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.MondayNightsEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.BibleStudyEnrollment bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.[3on3BasketballEnrollment] bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }
            else if (ddlProgram.Text == "Special Events")
            {
                sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor   "
                             + "from UIF_PerformingArts.dbo.[SpecialEventsEnrollment] bte "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                             + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                             + "WHERE bte.studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                             + "AND bte.studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND bte.volunteer = 1 "
                             + "AND bte.student = 0 "
                             + "GROUP BY bte.studentlastname, bte.studentfirstname, bte.coachteam, bte.gotpicture, si.pictureidentification, bte.comments, bte.middlename, bte.lastupdatedby, bte.sysupdate, si.notes, bte.teamcolor "
                             + "order by bte.studentlastname, bte.studentfirstname ";
            }

            imgStudent.Visible = false;
            imgStudent.ImageUrl = "N/A";

            //Perform database lookup based on the chosen child..RCM..
            SqlCommand cmd = new SqlCommand(sql_LoadGrid);            
            cmd.Connection = con;
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {	//Retrieve the first record only
                if (reader.IsDBNull(0) || reader.IsDBNull(1))
                {
                    lblName.Text = "N/A";
                }
                else
                {
                    lblName.Text = reader.GetString(0) + "," + reader.GetString(1);
                }
                //if (reader.IsDBNull(2))
                //{
                //    chbRegistrationForm.Checked = false;
                //}
                //else
                //{
                //    chbRegistrationForm.Checked = reader.GetBoolean(2);
                //}
                //if (reader.IsDBNull(3))
                //{
                //    chbParentalConsentForm.Checked = false;
                //}
                //else
                //{
                //    chbParentalConsentForm.Checked = reader.GetBoolean(3);
                //}
                //if (reader.IsDBNull(4))
                //{
                //    chbContract.Checked = false;
                //}
                //else
                //{
                //    chbContract.Checked = reader.GetBoolean(4);
                //}
                //if (reader.IsDBNull(5))
                //{
                //    chbPaid.Checked = false;
                //}
                //else
                //{
                //    chbPaid.Checked = reader.GetBoolean(5);
                //}
                if (reader.IsDBNull(2))
                {
                    txbCoachTeam.Text = "N/A";
                    ddlTeamSectionUpdate.Text = "N/A";
                }
                else
                {
                    txbCoachTeam.Text = reader.GetString(2);
                }
                if (reader.IsDBNull(3))
                {
                    chbGotPicture.Checked = false;
                }
                else
                {
                    chbGotPicture.Checked = reader.GetBoolean(3);
                }
                if (reader.IsDBNull(4))
                {
                    imgStudent.ImageUrl = "No picture Available";
                }
                else
                {
                    imgStudent.ImageUrl = reader.GetString(4);
                }
                if (reader.IsDBNull(5))
                {
                    txbComments.Text = "N/A";
                }
                else
                {
                    txbComments.Text = reader.GetString(5);
                }
                if (reader.IsDBNull(7))
                {
                    lblLastUpdatedBy.Text = "N/A";
                }
                else
                {
                    lblLastUpdatedBy.Text = "LastUpdatedBy: " + reader.GetString(7) + " On: " + reader.GetSqlValue(8).ToString();
                }
                if (reader.IsDBNull(9))
                {
                    txbSPComments.Text = "N/A";
                }
                else
                {
                    txbSPComments.Text = reader.GetString(9);
                }
                if (reader.IsDBNull(10))
                {
                    ddlTeamSections.Text = "Select a team?";
                    ddlTeamSectionUpdate.Text = "Select a team?";
                }
                else
                {
                    ddlTeamSections.Text = reader.GetString(10);
                    ddlTeamSectionUpdate.Text = reader.GetString(10);
                }

                lblComments.Visible = true;
                txbSPComments.Visible = true;
                txbSPComments.Enabled = false;
                lblSPNotes.Visible = true;
                lblTeamColor.Visible = true;

                if (ddlProgram.Text == "Outreach Basketball")
                {
                    //chbParentalConsentForm.Visible = true;
                    ////chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "BasketballTEAMS")
                {
                    //chbParentalConsentForm.Visible = true;
                    ////chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //chbPaid.Visible = true;

                    ddlTeamSections.Visible = true;
                    ddlTeamSections.Enabled = true;

                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    //chbParentalConsentForm.Visible = true;
                    ////chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //chbPaid.Visible = true;

                    ddlTeamSections.Visible = true;
                    ddlTeamSections.Enabled = true;

                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    //chbParentalConsentForm.Visible = true;
                    ////chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //chbPaid.Visible = true;

                    ddlTeamSections.Visible = true;
                    ddlTeamSections.Enabled = true;

                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    //chbParentalConsentForm.Visible = true;
                    //chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    ////chbPaid.Visible = true;

                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    //chbParentalConsentForm.Visible = true;
                    //chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    ////chbPaid.Visible = true;

                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    //chbParentalConsentForm.Visible = true;
                    //chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //chbPaid.Visible = true;

                    //txbCoachTeam.Visible = true;
                    ////lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    //chbParentalConsentForm.Visible = true;
                    //chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //chbPaid.Visible = true;

                    ////txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    //chbParentalConsentForm.Visible = true;
                    //chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //chbPaid.Visible = true;

                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    ////lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    //chbParentalConsentForm.Visible = true;
                    //chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //chbPaid.Visible = true;

                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    //chbParentalConsentForm.Visible = true;
                    //chbRegistrationForm.visible = true;
                    chbGotPicture.Visible = true;
                    //chbPaid.Visible = true;

                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
                else
                {
                    chbParentalConsentForm.Visible = true;
                    //chbRegistrationForm.visible = true;
                    //chbPaid.Visible = true;
                    chbGotPicture.Visible = true;
                    chbContract.Visible = true;
                    //txbCoachTeam.Visible = true;
                    //lblName.Visible = true;
                    lblName2.Visible = true;
                    lblName.Text = ddlStudents.Text.Trim();
                    lblName2.Text = ddlStudents.Text.Trim();
                    imgStudent.Visible = true;
                    cmbUpdate.Visible = true;
                    cmbExcelExport.Visible = true;
                    cmbExcelExport.Enabled = false;
                    txbComments.Visible = true;
                    //lblCoachTeam.Visible = true;

                    cmbStudentPage.Enabled = true;
                }
            }
            lbStudentProfileLink.Visible = true;
            lbStudentProfileLink.Enabled = true;
            ddlTeamSectionUpdate.Visible = true;
            //UpdateInformation();
        }

        protected void cmbUpdate_Click(object sender, EventArgs e)
        {
            UpdateInformation();
        }

        protected void CleanCharacters()
        {
            //Strips out all apostrophes from data..RCM..3/21/12.
            txbCoachTeam.Text = txbCoachTeam.Text.Replace("'", "");
            txbComments.Text = txbComments.Text.Replace("'", "");
        }
        
        protected void UpdateInformation()
        {
            try
            {
                CleanCharacters();
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.OutreachBasketballEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.MSBasketballLeagueEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.HSBasketballLeagueEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.SoccerIntraMuralsEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.SoccerTEAMSEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.BaseballEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.[3on3BasketballEnrollment] "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.MondayNightsEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.BibleStudyEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                        //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.SpecialEventsEnrollment "
                        + "SET "
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " contract = " + Convert.ToInt32(chbContract.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        //+ " coachteam = '" + txbCoachTeam.Text.Trim() + "', "
                        + " teamcolor = '" + ddlTeamSectionUpdate.Text.Trim() + "', "
                            //+ " sectionname = '" + ddlProgramSection.Text.Trim() + "', "
                        + " gotpicture = " + Convert.ToInt32(chbGotPicture.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE studentlastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND studentfirstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' "
                        + "AND sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                        + "AND volunteer = 1 ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjaaa)
                    {
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                
                try
                {
                    string sqlUpdateStatement = "";
                    //txtFirstName.Text.Replace("'", "''");

                    if (chbRegistrationForm.Checked)
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.VolunteerInformation "
                        + "SET "
                        + " currentregistrationform = " + Convert.ToInt32(chbRegistrationForm.Checked) + ", "
                        + " promotionalrelease = 1, "//Automatically sets these to 1.
                        + " permissiontotransport = 1, "//Automatically sets these to 1.
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE lastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND firstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' ";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.VolunteerInformation "
                        + "SET "
                        + " currentregistrationform = " + Convert.ToInt32(chbRegistrationForm.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + "WHERE lastname = '" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")) + "' "
                        + "AND firstname = '" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1)) + "' ";
                    }

                    con2.Open();

                    //create a SQL command to update record
                    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con2);
                    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                    {
                    }
                    else
                    {
                        //Didn't find a record to update..RCM..
                    }
                }
                catch (Exception lkjaaa)
                {
                    //lblInformation.Enabled = true;
                    //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                }
                finally
                {
                    con2.Close();
                }
            }
            catch (Exception lkjaaabbc)
            {

            }
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            if ((gvReport.Rows.Count != 0))
            {
                gvReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvReport, Response);
            }
            else if (gvAttendanceList.Rows.Count != 0)
            {
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvAttendanceList, Response);
            }
            //else if (gvAddressView.Rows.Count != 0)
            //{
            //    ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            //    ExcelExport.ExportGridView(gvAddressView, Response);
            //}
        }

        protected void cmbStudentPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1).Trim());
        }

        protected void cmbCallingListReport_Click(object sender, EventArgs e)
        {
            gvAttendanceList.Visible = false;
            lblTeamColors.Visible = false;
            lblTeamColor.Visible = false;
            ddlTeamSectionUpdate.Visible = false;
            gvReport.Visible = true;

            if (ddlProgram.Text == "MS Basketball League")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[MSBasketballLeagueEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[MSBasketballLeagueEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "MSBasketballLeagueEnrollment");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[HSBasketballLeagueEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[HSBasketballLeagueEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "HSBasketballLeagueEnrollment");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "BasketballTEAMS")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[BasketballTEAMSEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[BasketballTEAMSEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "BasketballTEAMSEnrollment");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[SoccerTEAMSEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[SoccerTEAMSEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SoccerTEAMSEnrollment");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Baseball")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[BaseballEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[BaseballEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "BaseballEnrollment");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[OutreachBasketballEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[OutreachBasketballEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "OutreachBasketball");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[SoccerIntraMuralsEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[SoccerIntraMuralsEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SoccerIntraMuralsEnrollment");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }

            }
            else if (ddlProgram.Text == "MondayNights")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[MondayNightsEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[MondayNightsEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "MondayNightsEnrollment");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[BibleStudyEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[BibleStudyEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "BibleStudyEnrollment");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                try
                {
                    chbParentalConsentForm.Visible = false;
                    chbRegistrationForm.Visible = false;
                    chbPaid.Visible = false;
                    chbGotPicture.Visible = false;
                    chbContract.Visible = false;
                    txbCoachTeam.Visible = false;
                    lblName.Visible = false;
                    lblName2.Visible = false;
                    lblStudentNames.Visible = false;
                    lblCoachTeam.Visible = false;
                    imgStudent.Visible = false;
                    cmbUpdate.Visible = false;
                    txbComments.Visible = false;
                    lblComments.Visible = false;
                    txbSPComments.Visible = false;
                    lblSPNotes.Visible = false;

                    ddlTeamSections.Visible = false;
                    lbStudentProfileLink.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    ddlStudents.Visible = false;
                    lblStudentNames.Visible = false;
                    //ddlProgram.Text = "Please select a program";

                    con3.Open();

                    string sql = "";
                    if ((ddlProgramSection.Text == "Please select a section") || (ddlProgramSection.Text == ""))
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[3on3BasketballEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }
                    else
                    {
                        sql = "Select bte.SectionName, bte.Studentlastname as 'LastName', bte.Studentfirstname as 'FirstName', vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', vd.BackgroundCheckPaid as 'Paid', si.CellPhone as 'CellPhoneNum', si.Email "
                            + "FROM UIF_PerformingArts.dbo.[3on3BasketballEnrollment] bte "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation si "
                            + "ON (bte.studentlastname = si.lastname AND bte.studentfirstname = si.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ParentGuardianContactInformation pgi "
                            + "ON (bte.studentlastname = pgi.studentlastname AND bte.studentfirstname = pgi.studentfirstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (bte.studentlastname = vd.lastname AND bte.studentfirstname = vd.firstname) "
                            + "WHERE bte.volunteer = 1 "
                            + "AND bte.Sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                            + "GROUP BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname, vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, vd.BackgroundCheckPaid, si.CellPhone, si.Email "
                            + "ORDER BY bte.SectionName, bte.Studentlastname, bte.Studentfirstname ";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con3);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "[3on3BasketballEnrollment]");
                    gvReport.DataSource = ds.Tables[0];
                    gvReport.DataBind();
                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
        }


        //public void bind()
        //{
            //con.Open();
            //string Tablename = "";

            //try
            //{
            //    string sql_LoadGrid = "";
            //    string sql_Volunteer = "";

            //    if (ddlProgram.Text == "Outreach Basketball")
            //    {
            //        Tablename = "OutreachBasketball";
            //    }
            //    else if (ddlProgram.Text == "BasketballTEAMS")
            //    {
            //        Tablename = "BasketballTEAMSEnrollment";
            //    }
            //    else if (ddlProgram.Text == "SoccerTEAMS")
            //    {
            //        Tablename = "SoccerTEAMSEnrollment";
            //    }
            //    else if (ddlProgram.Text == "SoccerIntraMurals")
            //    {
            //        Tablename = "SoccerIntraMuralsEnrollment";
            //    }
            //    else if (ddlProgram.Text == "Baseball")
            //    {
            //        Tablename = "BaseballEnrollment";
            //    }
            //    else if (ddlProgram.Text == "3on3 Basketball")
            //    {
            //        Tablename = "[3on3BasketballEnrollment]";
            //    }
            //    else if (ddlProgram.Text == "MondayNights")
            //    {
            //        Tablename = "MondayNightsEnrollment";
            //    }
            //    else if (ddlProgram.Text == "Bible Study")
            //    {
            //        Tablename = "BibleStudyEnrollment";
            //    }
            //    else if (ddlProgram.Text == "MS Basketball League")
            //    {
            //        Tablename = "MSBasketballLeagueEnrollment";
            //    }
            //    else if (ddlProgram.Text == "HS Basketball League")
            //    {
            //        Tablename = "HSBasketballLeagueEnrollment";
            //    }

            //    if (ddlProgramSection.Text == "Please select a section")
            //    {
            //        if (ddlTeamSections.Text == "Select a team color")
            //        {
            //            sql_LoadGrid = "Select tt.studentlastname, tt.studentfirstname, tt.teamcolor, si.homephone, pg.parentguardian1 "
            //                         + "FROM " + Tablename + " tt "
            //                         + "LEFT OUTER JOIN VolunteerInformation si "
            //                         + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
            //                         + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.teamcolor, si.homephone, pg.parentguardian1 "
            //                         + "ORDER BY tt.studentlastname, tt.studentfirstname ";
            //        }
            //        else
            //        {
            //            sql_LoadGrid = "Select tt.studentlastname, tt.studentfirstname, tt.teamcolor, si.homephone, pg.parentguardian1 "
            //                         + "FROM " + Tablename + " tt "
            //                         + "LEFT OUTER JOIN VolunteerInformation si "
            //                         + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
            //                         + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.teamcolor, si.homephone, pg.parentguardian1 "
            //                         + "ORDER BY tt.studentlastname, tt.studentfirstname ";
            //        }
            //    }
            //    else
            //    {
            //        if (ddlTeamSections.Text == "")
            //        {
            //            sql_LoadGrid = "Select tt.studentlastname, tt.studentfirstname, tt.teamcolor, si.homephone, pg.parentguardian1 "
            //                         + "FROM " + Tablename + " tt "
            //                         + "LEFT OUTER JOIN VolunteerInformation si "
            //                         + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
            //                         + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.teamcolor, si.homephone, pg.parentguardian1 "
            //                         + "ORDER BY tt.studentlastname, tt.studentfirstname ";
            //        }
            //        else
            //        {
            //            sql_LoadGrid = "Select tt.studentlastname, tt.studentfirstname, tt.teamcolor, si.homephone, pg.parentguardian1 "
            //                         + "FROM " + Tablename + " tt "
            //                         + "LEFT OUTER JOIN VolunteerInformation si "
            //                         + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
            //                         + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.teamcolor, si.homephone, pg.parentguardian1 "
            //                         + "ORDER BY tt.studentlastname, tt.studentfirstname ";
            //        }
            //    }

            //    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //    DataSet ds = new DataSet();
            //    DataSet custDS = new DataSet();
            //    da.Fill(ds, Tablename);
            //    da.Fill(custDS, Tablename);
            //    gvAttendanceList.DataSource = ds.Tables[0];
            //    gvAttendanceList.DataBind();
            //}
            //catch (Exception lkjlkj)
            //{


            //}
            //finally
            //{
            //    con.Close();
            //}
        //}


        protected void chbRegistrationForm_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRegistrationForm.Checked)
            {
                UpdateInformation();
            }
            else
            {

            }
        }

        protected void lbStudentProfileLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&VolunteerFirstName=" + ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1).Trim());
        }

        protected void cmbSectionMaintenance_Click(object sender, EventArgs e)
        {
            Response.Redirect("AthleticsProgramSectionMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbRetreiveClassList_Click(object sender, EventArgs e)
        {
            con.Open();
            string Tablename = "";
            ClearPage();

            try
            {
                string sql_LoadGrid = "";
                string sql_Volunteer = "";

                if (ddlProgram.Text == "Outreach Basketball")
                {
                    Tablename = "OutreachBasketball";
                }
                else if (ddlProgram.Text == "BasketballTEAMS")
                {
                    Tablename = "BasketballTEAMSEnrollment";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    Tablename = "SoccerTEAMSEnrollment";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    Tablename = "SoccerIntraMuralsEnrollment";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    Tablename = "BaseballEnrollment";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    Tablename = "[3on3BasketballEnrollment]";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    Tablename = "MondayNightsEnrollment";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    Tablename = "BibleStudyEnrollment";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    Tablename = "MSBasketballLeagueEnrollment";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    Tablename = "HSBasketballLeagueEnrollment";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    Tablename = "SpecialEventsEnrollment";
                }

                if (ddlProgramSection.Text == "Please select a section")
                {
                    if (ddlTeamSections.Text == "Select team color")
                    {
                        sql_LoadGrid = "Select tt.studentlastname as 'LastName', tt.studentfirstname as 'FirstName', tt.Teamcolor, si.Homephone, pgc.Parentguardian1, pgc.parentguardianrelationship1 as 'PGRelationship1', pgc.parentguardian2, pgc.parentguardian2relationship as 'PGRelationship2' "
                                     + "FROM " + Tablename + " tt "
                                     + "LEFT OUTER JOIN VolunteerInformation si "
                                     + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
                                     + "LEFT OUTER JOIN ParentGuardianContactInformation pgc "
                                     + "ON (tt.studentlastname = pgc.studentlastname AND tt.studentfirstname = pgc.studentfirstname) "
                                     + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.Teamcolor, si.Homephone, pgc.Parentguardian1, pgc.parentguardianrelationship1, pgc.parentguardian2, pgc.parentguardian2relationship "
                                     + "ORDER BY tt.studentlastname, tt.studentfirstname ";
                    }
                    else
                    {
                        sql_LoadGrid = "Select tt.studentlastname as 'LastName', tt.studentfirstname as 'FirstName', tt.Teamcolor, si.Homephone, pgc.Parentguardian1, pgc.parentguardianrelationship1 as 'PGRelationship1', pgc.parentguardian2, pgc.parentguardian2relationship as 'PGRelationship2' "
                                     + "FROM " + Tablename + " tt "
                                     + "LEFT OUTER JOIN VolunteerInformation si "
                                     + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
                                     + "LEFT OUTER JOIN ParentGuardianContactInformation pgc "
                                     + "ON (tt.studentlastname = pgc.studentlastname AND tt.studentfirstname = pgc.studentfirstname) "
                                     + "WHERE tt.teamcolor = '" + ddlTeamSections.Text.Trim() + "' "
                                     + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.Teamcolor, si.Homephone, pgc.Parentguardian1, pgc.parentguardianrelationship1, pgc.parentguardian2, pgc.parentguardian2relationship "
                                     + "ORDER BY tt.studentlastname, tt.studentfirstname ";
                    }
                }
                else
                {
                    if (ddlTeamSections.Text == "Select team color")
                    {
                        sql_LoadGrid = "Select tt.studentlastname as 'LastName', tt.studentfirstname as 'FirstName', tt.Teamcolor, si.Homephone, pgc.Parentguardian1, pgc.parentguardianrelationship1 as 'PGRelationship1', pgc.parentguardian2, pgc.parentguardian2relationship as 'PGRelationship2' "
                                     + "FROM " + Tablename + " tt "
                                     + "LEFT OUTER JOIN VolunteerInformation si "
                                     + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
                                     + "LEFT OUTER JOIN ParentGuardianContactInformation pgc "
                                     + "ON (tt.studentlastname = pgc.studentlastname AND tt.studentfirstname = pgc.studentfirstname) "
                                     + "WHERE tt.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                                     + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.Teamcolor, si.Homephone, pgc.Parentguardian1, pgc.parentguardianrelationship1, pgc.parentguardian2, pgc.parentguardian2relationship "
                                     + "ORDER BY tt.studentlastname, tt.studentfirstname ";
                    }
                    else
                    {
                        sql_LoadGrid = "Select tt.studentlastname as 'LastName', tt.studentfirstname as 'FirstName', tt.Teamcolor, si.Homephone, pgc.Parentguardian1, pgc.parentguardianrelationship1 as 'PGRelationship1', pgc.parentguardian2, pgc.parentguardian2relationship as 'PGRelationship2' "
                                     + "FROM " + Tablename + " tt "
                                     + "LEFT OUTER JOIN VolunteerInformation si "
                                     + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
                                     + "LEFT OUTER JOIN ParentGuardianContactInformation pgc "
                                     + "ON (tt.studentlastname = pgc.studentlastname AND tt.studentfirstname = pgc.studentfirstname) "
                                     + "WHERE tt.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                                     + "AND tt.teamcolor = '" + ddlTeamSections.Text.Trim() + "' "
                                     + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.Teamcolor, si.Homephone, pgc.Parentguardian1, pgc.parentguardianrelationship1, pgc.parentguardian2, pgc.parentguardian2relationship "
                                     + "ORDER BY tt.studentlastname, tt.studentfirstname ";
                    }
                }

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                DataSet custDS = new DataSet();
                da.Fill(ds, Tablename);
                da.Fill(custDS, Tablename);
                gvAttendanceList.DataSource = ds.Tables[0];
                gvAttendanceList.DataBind();
            }
            catch (Exception lkjlkj)
            {


            }
            finally
            {
                con.Close();
            }
        }

        protected void ClearPartialPage()
        {
            chbParentalConsentForm.Visible = false;
            chbRegistrationForm.Visible = false;
            chbPaid.Visible = false;
            chbGotPicture.Visible = false;
            chbContract.Visible = false;
            txbCoachTeam.Visible = false;
            lblName.Visible = false;
            lblName2.Visible = false;
            lblStudentNames.Visible = false;
            lblCoachTeam.Visible = false;
            lblTeamColor.Visible = false;
            imgStudent.Visible = false;
            cmbUpdate.Visible = false;
            txbComments.Visible = false;

            //ddlTeamSections.Visible = false;
            lbStudentProfileLink.Visible = false;
            txbSPComments.Visible = false;
            lblSPNotes.Visible = false;
            lblComments.Visible = false;

            ddlTeamSectionUpdate.Visible = false;

            //ddlProgramSection.Visible = false;
            //lblProgramSections.Visible = false;
            //ddlStudents.Visible = false;
            //lblStudentNames.Visible = false;
        }

        protected void ddlTeamSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string Tablename = "";
            ddlStudents.Text = "Please select a volunteer";
            ClearPartialPage();

            try
            {
                string sql_LoadGrid = "";
                string sql_Volunteer = "";

                if (ddlProgram.Text == "Outreach Basketball")
                {
                    Tablename = "OutreachBasketball";
                }
                else if (ddlProgram.Text == "BasketballTEAMS")
                {
                    Tablename = "BasketballTEAMSEnrollment";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    Tablename = "SoccerTEAMSEnrollment";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    Tablename = "SoccerIntraMuralsEnrollment";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    Tablename = "BaseballEnrollment";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    Tablename = "[3on3BasketballEnrollment]";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    Tablename = "MondayNightsEnrollment";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    Tablename = "BibleStudyEnrollment";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    Tablename = "MSBasketballLeagueEnrollment";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    Tablename = "HSBasketballLeagueEnrollment";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    Tablename = "SpecialEventsEnrollment";
                }

                sql_LoadGrid = "Select tt.studentlastname as 'LastName', tt.studentfirstname as 'FirstName', tt.SectionName, tt.TeamColor, si.Homephone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions   "
                                + "FROM " + Tablename + " tt "
                                + "LEFT OUTER JOIN VolunteerInformation si "
                                + "ON (tt.studentlastname = si.lastname AND tt.studentfirstname = si.firstname) "
                                + "LEFT OUTER JOIN ParentGuardianContactInformation pgc "
                                + "ON (tt.studentlastname = pgc.studentlastname AND tt.studentfirstname = pgc.studentfirstname) "
                                + "WHERE tt.teamcolor = '" + ddlTeamSections.Text.Trim() + "' "
                                + "AND tt.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
                                + "AND tt.volunteer = 1 "
                                + "GROUP BY tt.studentlastname, tt.studentfirstname, tt.SectionName, tt.Teamcolor, si.Homephone, si.EmergencyContact, si.EmergencyRelationship, si.EmergencyContactPhone, si.HealthConditions  "
                                + "ORDER BY tt.studentlastname, tt.studentfirstname, tt.SectionName, tt.Teamcolor ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                DataSet custDS = new DataSet();
                da.Fill(ds, Tablename);
                da.Fill(custDS, Tablename);
                gvAttendanceList.DataSource = ds.Tables[0];
                gvAttendanceList.DataBind();
                gvAttendanceList.Visible = true;
                cmbExcelExport.Enabled = true;
                lblStudentNames.Visible = true;
                lbStudentProfileLink.Visible = true;
                lbStudentProfileLink.Enabled = false;
            }
            catch (Exception lkjlkj)
            {


            }
            finally
            {
                con.Close();
            }
        }

        protected void lbSectionMaintenance_Click(object sender, EventArgs e)
        {
            Response.Redirect("AthleticsProgramSectionMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void ddlTeamSectionUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbTESTRYAN_Click(object sender, EventArgs e)
        {
            try
            {
                chbParentalConsentForm.Visible = false;
                chbRegistrationForm.Visible = false;
                chbPaid.Visible = false;
                chbGotPicture.Visible = false;
                chbContract.Visible = false;
                txbCoachTeam.Visible = false;
                lblName.Visible = false;
                lblName2.Visible = false;
                lblStudentNames.Visible = false;
                lblCoachTeam.Visible = false;
                imgStudent.Visible = false;
                cmbUpdate.Visible = false;
                txbComments.Visible = false;

                ddlTeamSections.Visible = false;
                lbStudentProfileLink.Visible = false;
                txbSPComments.Visible = false;
                lblSPNotes.Visible = false;
                lblComments.Visible = false;

                ddlProgramSection.Visible = false;
                lblProgramSections.Visible = false;
                ddlStudents.Visible = false;
                lblStudentNames.Visible = false;
                //ddlProgram.Text = "Please select a program";

                con.Open();

                SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionryanryan", con);
                objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                gvReport.DataSource = objCmd.ExecuteReader();
                gvReport.DataBind();
                gvReport.Visible = true;

                con.Close();
                cmbExcelExport.Enabled = true;
                cmbExcelExport.Visible = true;
                cmbStudentPage.Enabled = false;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void lbAttendanceHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("VolunteerAttendanceHistory.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }
    }
}