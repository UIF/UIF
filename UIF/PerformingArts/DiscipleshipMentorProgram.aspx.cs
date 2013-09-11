using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UrbanImpactCommon;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Collections;
using System.Globalization;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.IO;


namespace UIF.PerformingArts
{
    public partial class DiscipleshipMentorProgram : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public SqlConnection con3 = new SqlConnection(connectionString);
        public int irowNum = 0;
        public static string Department = "";

        //public int WaitingList = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];

                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);
                MenuBest.Style.Add("z-index", "9999999999");

                //Check for a login... If not, re-direct them to the mainmenu page..RCM..
                if (Request.QueryString["security"] == "Good")
                {
                    if (((Request.QueryString["LastName"] == "Mason") && (Request.QueryString["FirstName"] == "Matt")) || ((Request.QueryString["LastName"] == "Sims-Reed") && (Request.QueryString["FirstName"] == "Donna")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Tammy")) || ((Request.QueryString["LastName"] == "Boll") && (Request.QueryString["FirstName"] == "Becky")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")) || ((Request.QueryString["LastName"] == "Churchill") && (Request.QueryString["FirstName"] == "Andrew")))
                    {

                        if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
                        {
                            PopulateDropDown();
                            PopulateDropDownWaitList();
                            PopulateDiscipleshipMentorAvailable();
                            ddlDiscipleshipMentor.Items.Add("Please select a student");
                            ddlDiscipleshipMentor.Text = "Please select a student";
                            ddlWaitingListInactiveStudents.Items.Add("Please select a student");
                            ddlWaitingListInactiveStudents.Text = "Please select a student";

                            PopulateVolunteerWaitingList();
                            PopulateVolunteerActiveList();
                            ddlVolunteerWaitingList.Items.Add("Please select a mentor");
                            ddlVolunteerWaitingList.Text = "Please select a mentor";
                            ddlVolunteerActiveMentees.Items.Add("Please select a mentor");
                            ddlVolunteerActiveMentees.Text = "Please select a mentor";
                            
                            chbCovenantLetter.Enabled = true;

                            ddlProgramEnrollment.Items.Add("Select a department");
                            ddlProgramEnrollment.Items.Add("Athletics");
                            ddlProgramEnrollment.Items.Add("PerformingArts");
                            ddlProgramEnrollment.Items.Add("N/A");
                            ddlProgramEnrollment.Text = "Select a department";
                        }
                        else
                        {
                            DisplayHeaderFields();
                            PopulateDiscipleshipMentorAvailable();
                            RetrieveInformation();
                            PopulateDropDown();
                            PopulateDropDownWaitList();
                            ddlDiscipleshipMentor.Items.Add("Please select a student");
                            ddlDiscipleshipMentor.Text = "Please select a student";
                            ddlWaitingListInactiveStudents.Items.Add("Please select a student");
                            ddlWaitingListInactiveStudents.Text = "Please select a student";

                            PopulateVolunteerWaitingList();
                            PopulateVolunteerActiveList();
                            ddlVolunteerWaitingList.Items.Add("Please select a mentor");
                            ddlVolunteerWaitingList.Text = "Please select a mentor";
                            ddlVolunteerActiveMentees.Items.Add("Please select a mentor");
                            ddlVolunteerActiveMentees.Text = "Please select a mentor";

                            ddlProgramEnrollment.Items.Add("Select a department");
                            ddlProgramEnrollment.Items.Add("Athletics");
                            ddlProgramEnrollment.Items.Add("PerformingArts");
                            ddlProgramEnrollment.Items.Add("N/A");
                            ddlProgramEnrollment.Text = "Select a department";
                            
                            DisplayTheGrid();

                            if (Request.QueryString["WaitList"] == "True")
                            {
                                ddlWaitingListInactiveStudents.Text = Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"];
                                lbStudentInfo2.Style.Add("z-index", "99999");
                                lbStudentInfo2.Enabled = true;
                                lbStudentInfo2.Visible = true;
                            }
                            else if (Request.QueryString["WaitList"] == "False")
                            {
                                ddlDiscipleshipMentor.Text = Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"];
                                lbStudentInfo.Style.Add("z-index", "99999");
                                lbStudentInfo.Enabled = true;
                                lbStudentInfo.Visible = true;
                            }
                            lblNotes.Visible = true;
                            lbAddNewEntry.Visible = true;
                            cmbUpdate.Enabled = true;
                            cmbBackToStudentPage.Enabled = true;
                            chbCovenantLetter.Enabled = true;
                        }
                    }
                    else
                    {
                        //Ryan C Manners..1/5/11
                        //Do NOT ALLOW ACCESS TO THE PAGE!
                        //Response.Redirect("ErrorAccess.aspx");
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

        protected void PopulateVolunteerActiveList()
        {
            try
            {
                con.Open();

                string selectSQL = "select lastname, firstname " +
                                   "from UIF_PerformingArts.dbo.VolunteerInformation " +
                                   "where discipleshipmentorparticipation = 1 " +
                                   "and discipleshipmentorpotentials = 0 " +
                                   "group by lastname, firstname " +
                                   "Order by lastname, firstname ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    do
                    {
                        //ddlDiscipleShipMentorAvailable.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                        ddlVolunteerActiveMentees.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                        //ddlVolunteerWaitingList.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                    } while (reader.Read());
                    //ddlVolunteerWaitingList.Items.Add("N/A");
                    ddlVolunteerActiveMentees.Items.Add("N/A");
                    reader.Close();
                }
            }
            catch (Exception lkj)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void PopulateVolunteerWaitingList()
        {
            try
            {
                con.Open();

                string selectSQL = "select lastname, firstname, discipleshipmentornotes " +
                                   "from UIF_PerformingArts.dbo.VolunteerInformation " +
                                   "where discipleshipmentorparticipation = 1 " +
                                   "and discipleshipmentorpotentials = 1 " +
                                   "group by lastname, firstname, discipleshipmentornotes " +
                                   "Order by lastname, firstname ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    do
                    {
                        //////ddlDiscipleShipMentorAvailable.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                        //////ddlVolunteerActiveMentees.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                        //ddlVolunteerWaitingList.Items.Add(reader.GetString(0) + "," + reader.GetString(1) + "(" + reader.GetString(2).Trim().ToString() + ")");
                        ddlVolunteerWaitingList.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                    } while (reader.Read());
                    ddlVolunteerWaitingList.Items.Add("N/A");
                    //ddlVolunteerActiveMentees.Items.Add("N/A");
                    reader.Close();
                }
            }
            catch (Exception lkj)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void PopulateDropDown()
        {
            try
            {
                con.Open();

                string selectSQL = "select studentlastname, studentfirstname " +
                                   "from UIF_PerformingArts.dbo.DiscipleshipmentorProgram " +
                                   "where waitinglistinactive = 0 " +
                                   "group by studentlastname, studentfirstname ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    do
                    {
                        ddlDiscipleshipMentor.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                    } while (reader.Read());
                    reader.Close();
                }
            }
            catch (Exception lkj)
            {


            }
            finally
            {
                con.Close();
            }
        }


        protected void PopulateDropDownWaitList()
        {
            try
            {
                con.Open();

                string selectSQL = "select studentlastname, studentfirstname " +
                                   "from UIF_PerformingArts.dbo.DiscipleshipmentorProgram " +
                                   "where waitinglistinactive = 1 " +
                                   "group by studentlastname, studentfirstname ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    do
                    {
                        ddlWaitingListInactiveStudents.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                    } while (reader.Read());
                    reader.Close();
                }
            }
            catch (Exception lkj)
            {


            }
            finally
            {
                con.Close();
            }
        }


        protected void PopulateDiscipleshipMentorAvailable()
        {
            try
            {
                //Ryan C Manners..1/10/12.
                //Added in to have a default value for this dropdownlist..
                ddlAssignedMentors.Items.Add("No mentor assigned.");
                ddlAssignedMentors.Text = "No mentor assigned.";
                                
                con.Open();

                string selectSQL = "select lastname, firstname " +
                                   "from UIF_PerformingArts.dbo.VolunteerInformation " +
                                   "where discipleshipmentorparticipation = 1 " +
                                   "and discipleshipmentorpotentials = 0 " +
                                   "group by lastname, firstname " +
                                   "Order by lastname, firstname ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                ddlDiscipleShipMentorAvailable.Items.Clear();
                ddlDiscipleshipMentorOptions2.Items.Clear();
                if (reader.Read())
                {	//Retrieve the first record only
                    ddlDiscipleShipMentorAvailable.Items.Add("Please select a mentor.");
                    ddlDiscipleshipMentorOptions2.Items.Add("Please select a mentor.");
                    do
                    {   
                        ddlDiscipleShipMentorAvailable.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                        ddlDiscipleshipMentorOptions2.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                    } while (reader.Read());
                    //ddlVolunteerActiveMentees.Items.Add("N/A");
                    reader.Close();
                }

                string selectSQL2 = "select lastname, firstname, discipleshipmentornotes " +
                   "from UIF_PerformingArts.dbo.VolunteerInformation " +
                   "where discipleshipmentorparticipation = 1 " +
                   "and discipleshipmentorpotentials = 1 " +
                   "group by lastname, firstname, discipleshipmentornotes " +
                   "Order by lastname, firstname ";

                SqlDataReader reader2 = null;
                SqlCommand cmd2 = new SqlCommand(selectSQL2);

                cmd2.Connection = con;
                reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {	//Retrieve the first record only
                    ddlDiscipleShipMentorAvailable.Items.Add("Please select a mentor.");
                    ddlDiscipleshipMentorOptions2.Items.Add("Please select a mentor.");
                    do
                    {
                        ddlDiscipleShipMentorAvailable.Items.Add(reader2.GetString(0) + "," + reader2.GetString(1) + " (OnWaitingList: " + reader2.GetString(2).Trim().ToString() + ")" );
                        ddlDiscipleshipMentorOptions2.Items.Add(reader2.GetString(0) + "," + reader2.GetString(1) + " (OnWaitingList: " + reader2.GetString(2).Trim().ToString() + ")");
                    } while (reader2.Read());
                    reader2.Close();
                }
                ddlDiscipleShipMentorAvailable.Text = "Please select a mentor.";
                ddlDiscipleshipMentorOptions2.Text = "Please select a mentor.";
            }
            catch (Exception lkj)
            {

            }
            finally
            {
                con.Close();
            }
        }
        
               
        protected void cmbDiscipleshipMentor_Click(object sender, EventArgs e)
        {
            //Special to retrieve information differently.

            ClearValues();
            DisplayHeaderFields();
            //PopulateDiscipleshipMentorAvailable();
            RetrieveInformationFromButton();
            DisplayTheGridFromButton();
            cmbUpdate.Enabled = true;
            lblNotes.Visible = true;
            lbAddNewEntry.Visible = true;
        }

        protected void DisplayHeaderFields()
        {
            //Mentee Panel handling..
            Panel5.Style.Add("z-index", "9999");
            Panel5.Visible = true;

            ddlDiscipleshipMentor.Style.Add("z-index","99999");
            //ddlDiscipleshipMentor.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFA500");
            //ddlDiscipleshipMentor.BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
            ddlDiscipleshipMentor.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBB3E");

            
            ddlWaitingListInactiveStudents.Style.Add("z-index", "99999");
            ddlWaitingListInactiveStudents.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBB3E");
            lblMenteeMaintenance.Style.Add("z-index", "99999");
            lblEnrolledStudents.Style.Add("z-index", "99999");

            //lblMenteeMaintenance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            txbComments.Style.Add("z-index", "99999");
            txbComments.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBB3E");
            lblComments.Style.Add("z-index", "99999");
            //lblComments.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            lblWaitingListStudents.Style.Add("z-index", "99999");
            //lblWaitingListStudents.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            lblDiscipleMentor.Style.Add("z-index", "99999");
            //lblDiscipleMentor.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            imgImage.Style.Add("z-index", "99999");
            chbWaitingListInactive.Style.Add("z-index", "99999");

            chbBackGroundCheck.Style.Add("z-index", "99999");
            //chbBackGroundCheck.Visible = true;
            lbVolunteerDetails.Visible = true;
            //chbBackGroundCheck.Enabled = true;

            chbTrained.Style.Add("z-index", "99999");
            chbTrained.Visible = true;

            chbSpiritualJourney.Style.Add("z-index", "99999");
            chbSpiritualJourney.Visible = true;
            //chbSpiritualJourney.Enabled = true;

            chbVehichleInsurance.Style.Add("z-index", "99999");
            chbVehichleInsurance.Visible = true;
            //chbVehichleInsurance.Enabled = true;

            chbReleaseWaiver.Style.Add("z-index", "99999");
            chbReleaseWaiver.Visible = true;
            //chbReleaseWaiver.Enabled = true;

            chbGeneralInformation.Style.Add("z-index", "99999");
            chbGeneralInformation.Visible = true;
            //chbGeneralInformation.Enabled = true;

            ddlVolunteerWaitingList.Style.Add("z-index", "99999");
            ddlVolunteerWaitingList.Visible = true;
            
            ddlVolunteerActiveMentees.Style.Add("z-index", "99999");
            ddlVolunteerActiveMentees.Visible = true;

            lblVariousPaperwork.Visible = true;
            lblVariousPaperwork.Style.Add("z-index", "99999");
            //--------------------------------------------------------------------------------------------------

            //Mentor Panel handling...RCM..
            pnlPanel.Style.Add("z-index", "9999");
            pnlPanel.Visible = true;

            lblMentorMaintenance.Style.Add("z-index", "99999");
            lbVolunteerInformation.Style.Add("z-index", "99999");
            lbAddMentor.Style.Add("z-index", "99999");

            ddlProgramEnrollment.Style.Add("z-index", "99999");
            ddlProgramEnrollment.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBB3E");
            lblProgramEnrollment.Style.Add("z-index", "99999");

            ddlAssignedMentors.Style.Add("z-index", "99999");
            ddlAssignedMentors.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBB3E");
            lblDiscipleshipmentor.Style.Add("z-index", "99999");

            imgMentor.Style.Add("z-index", "99999");
            chbVolunteerWaitingList.Style.Add("z-index", "99999");

            txbDiscipleshipMentorNotes.Visible = true;
            txbDiscipleshipMentorNotes.Style.Add("z-index", "99999");
            txbDiscipleshipMentorNotes.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBB3E");

            ddlVolunteerActiveMentees.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBB3E");
            ddlVolunteerWaitingList.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFBB3E");

            lbAddMentor.Enabled = true;

            lblCovenantReceivedDate.Style.Add("z-index", "99999");
            lblCovenantReceivedDate.Visible = true;

            lblCovenantSentDate.Style.Add("z-index", "99999");
            lblCovenantSentDate.Visible = true;

                       
            lblVolunteerWaiting.Style.Add("z-index", "99999");

            //ddlCovRecDay.Enabled = true;
            //ddlCovRecDay.Visible = true;
            //ddlCovRecDay.Style.Add("z-index", "99999");

            //ddlCovRecMonth.Enabled = true;
            //ddlCovRecMonth.Visible = true;
            //ddlCovRecMonth.Style.Add("z-index", "99999");

            //ddlCovRecYear.Enabled = true;
            //ddlCovRecYear.Visible = true;
            //ddlCovRecYear.Style.Add("z-index", "99999");

            //ddlCovSentDay.Enabled = true;
            //ddlCovSentDay.Visible = true;
            //ddlCovSentDay.Style.Add("z-index", "99999");

            //ddlCovSentMonth.Enabled = true;
            //ddlCovSentMonth.Visible = true;
            //ddlCovSentMonth.Style.Add("z-index", "99999");

            //ddlCovSentYear.Enabled = true;
            //ddlCovSentYear.Visible = true;
            //ddlCovSentYear.Style.Add("z-index", "99999");

            txbCovenantReceivedDate.Enabled = true;
            txbCovenantReceivedDate.Visible = true;
            txbCovenantReceivedDate.Style.Add("z-index", "99999");

            txbCovenantSentDate.Enabled = true;
            txbCovenantSentDate.Visible = true;
            txbCovenantSentDate.Style.Add("z-index", "99999");

            imbCalCovenantReceiveDate.Enabled = true;
            imbCalCovenantReceiveDate.Visible = true;
            imbCalCovenantReceiveDate.Style.Add("z-index", "99999");

            imbCalCovenantSentDate.Enabled = true;
            imbCalCovenantSentDate.Visible = true;
            imbCalCovenantSentDate.Style.Add("z-index", "99999");

            imbCalCovenantSentDate.ImageUrl = "Calender.jpg";
            imbCalCovenantReceiveDate.ImageUrl = "Calender.jpg";

            //ddlCovRecMonth.Items.Add("Month");
            ddlCovRecMonth.Items.Add("01");
            ddlCovRecMonth.Items.Add("02");
            ddlCovRecMonth.Items.Add("03");
            ddlCovRecMonth.Items.Add("04");
            ddlCovRecMonth.Items.Add("05");
            ddlCovRecMonth.Items.Add("06");
            ddlCovRecMonth.Items.Add("07");
            ddlCovRecMonth.Items.Add("08");
            ddlCovRecMonth.Items.Add("09");
            ddlCovRecMonth.Items.Add("10");
            ddlCovRecMonth.Items.Add("11");
            ddlCovRecMonth.Items.Add("12");
            //ddlCovRecMonth.Text = "Month";

            //ddlCovRecDay.Items.Add("Day");
            ddlCovRecDay.Items.Add("01");
            ddlCovRecDay.Items.Add("02");
            ddlCovRecDay.Items.Add("03");
            ddlCovRecDay.Items.Add("04");
            ddlCovRecDay.Items.Add("05");
            ddlCovRecDay.Items.Add("06");
            ddlCovRecDay.Items.Add("07");
            ddlCovRecDay.Items.Add("08");
            ddlCovRecDay.Items.Add("09");
            ddlCovRecDay.Items.Add("10");
            ddlCovRecDay.Items.Add("11");
            ddlCovRecDay.Items.Add("12");
            ddlCovRecDay.Items.Add("13");
            ddlCovRecDay.Items.Add("14");
            ddlCovRecDay.Items.Add("15");
            ddlCovRecDay.Items.Add("16");
            ddlCovRecDay.Items.Add("17");
            ddlCovRecDay.Items.Add("18");
            ddlCovRecDay.Items.Add("19");
            ddlCovRecDay.Items.Add("20");
            ddlCovRecDay.Items.Add("21");
            ddlCovRecDay.Items.Add("22");
            ddlCovRecDay.Items.Add("23");
            ddlCovRecDay.Items.Add("24");
            ddlCovRecDay.Items.Add("25");
            ddlCovRecDay.Items.Add("26");
            ddlCovRecDay.Items.Add("27");
            ddlCovRecDay.Items.Add("28");
            ddlCovRecDay.Items.Add("29");
            ddlCovRecDay.Items.Add("30");
            ddlCovRecDay.Items.Add("31");
            //ddlCovRecDay.Text = "Day";

            //ddlCovRecYear.Items.Add("Year");
            ddlCovRecYear.Items.Add("1989");
            ddlCovRecYear.Items.Add("1990");
            ddlCovRecYear.Items.Add("1991");
            ddlCovRecYear.Items.Add("1992");
            ddlCovRecYear.Items.Add("1993");
            ddlCovRecYear.Items.Add("1994");
            ddlCovRecYear.Items.Add("1995");
            ddlCovRecYear.Items.Add("1996");
            ddlCovRecYear.Items.Add("1997");
            ddlCovRecYear.Items.Add("1998");
            ddlCovRecYear.Items.Add("1999");
            ddlCovRecYear.Items.Add("2000");
            ddlCovRecYear.Items.Add("2001");
            ddlCovRecYear.Items.Add("2002");
            ddlCovRecYear.Items.Add("2003");
            ddlCovRecYear.Items.Add("2004");
            ddlCovRecYear.Items.Add("2005");
            ddlCovRecYear.Items.Add("2006");
            ddlCovRecYear.Items.Add("2007");
            ddlCovRecYear.Items.Add("2008");
            ddlCovRecYear.Items.Add("2009");
            ddlCovRecYear.Items.Add("2010");
            ddlCovRecYear.Items.Add("2011");
            ddlCovRecYear.Items.Add("2012");
            ddlCovRecYear.Items.Add("2013");
            ddlCovRecYear.Items.Add("2014");
            ddlCovRecYear.Items.Add("2015");
            ddlCovRecYear.Items.Add("2016");
            ddlCovRecYear.Items.Add("2017");
            ddlCovRecYear.Items.Add("2018");
            ddlCovRecYear.Items.Add("2019");
            ddlCovRecYear.Items.Add("2020");
            //ddlCovRecYear.Text = "Year";

            //ddlCovSentMonth.Items.Add("Month");
            ddlCovSentMonth.Items.Add("01");
            ddlCovSentMonth.Items.Add("02");
            ddlCovSentMonth.Items.Add("03");
            ddlCovSentMonth.Items.Add("04");
            ddlCovSentMonth.Items.Add("05");
            ddlCovSentMonth.Items.Add("06");
            ddlCovSentMonth.Items.Add("07");
            ddlCovSentMonth.Items.Add("08");
            ddlCovSentMonth.Items.Add("09");
            ddlCovSentMonth.Items.Add("10");
            ddlCovSentMonth.Items.Add("11");
            ddlCovSentMonth.Items.Add("12");
            //ddlCovSentMonth.Text = "Month";

            //ddlCovSentDay.Items.Add("Day");
            ddlCovSentDay.Items.Add("01");
            ddlCovSentDay.Items.Add("02");
            ddlCovSentDay.Items.Add("03");
            ddlCovSentDay.Items.Add("04");
            ddlCovSentDay.Items.Add("05");
            ddlCovSentDay.Items.Add("06");
            ddlCovSentDay.Items.Add("07");
            ddlCovSentDay.Items.Add("08");
            ddlCovSentDay.Items.Add("09");
            ddlCovSentDay.Items.Add("10");
            ddlCovSentDay.Items.Add("11");
            ddlCovSentDay.Items.Add("12");
            ddlCovSentDay.Items.Add("13");
            ddlCovSentDay.Items.Add("14");
            ddlCovSentDay.Items.Add("15");
            ddlCovSentDay.Items.Add("16");
            ddlCovSentDay.Items.Add("17");
            ddlCovSentDay.Items.Add("18");
            ddlCovSentDay.Items.Add("19");
            ddlCovSentDay.Items.Add("20");
            ddlCovSentDay.Items.Add("21");
            ddlCovSentDay.Items.Add("22");
            ddlCovSentDay.Items.Add("23");
            ddlCovSentDay.Items.Add("24");
            ddlCovSentDay.Items.Add("25");
            ddlCovSentDay.Items.Add("26");
            ddlCovSentDay.Items.Add("27");
            ddlCovSentDay.Items.Add("28");
            ddlCovSentDay.Items.Add("29");
            ddlCovSentDay.Items.Add("30");
            ddlCovSentDay.Items.Add("31");
            //ddlCovSentDay.Text = "Day";

            //ddlCovSentYear.Items.Add("Year");
            ddlCovSentYear.Items.Add("1989");
            ddlCovSentYear.Items.Add("1990");
            ddlCovSentYear.Items.Add("1991");
            ddlCovSentYear.Items.Add("1992");
            ddlCovSentYear.Items.Add("1993");
            ddlCovSentYear.Items.Add("1994");
            ddlCovSentYear.Items.Add("1995");
            ddlCovSentYear.Items.Add("1996");
            ddlCovSentYear.Items.Add("1997");
            ddlCovSentYear.Items.Add("1998");
            ddlCovSentYear.Items.Add("1999");
            ddlCovSentYear.Items.Add("2000");
            ddlCovSentYear.Items.Add("2001");
            ddlCovSentYear.Items.Add("2002");
            ddlCovSentYear.Items.Add("2003");
            ddlCovSentYear.Items.Add("2004");
            ddlCovSentYear.Items.Add("2005");
            ddlCovSentYear.Items.Add("2006");
            ddlCovSentYear.Items.Add("2007");
            ddlCovSentYear.Items.Add("2008");
            ddlCovSentYear.Items.Add("2009");
            ddlCovSentYear.Items.Add("2010");
            ddlCovSentYear.Items.Add("2011");
            ddlCovSentYear.Items.Add("2012");
            ddlCovSentYear.Items.Add("2013");
            ddlCovSentYear.Items.Add("2014");
            ddlCovSentYear.Items.Add("2015");
            ddlCovSentYear.Items.Add("2016");
            ddlCovSentYear.Items.Add("2017");
            ddlCovSentYear.Items.Add("2018");
            ddlCovSentYear.Items.Add("2019");
            ddlCovSentYear.Items.Add("2020");
            ddlCovSentYear.Items.Add("2021");
            ddlCovSentYear.Items.Add("2022");
            ddlCovSentYear.Items.Add("2023");
            ddlCovSentYear.Items.Add("2024");
            ddlCovSentYear.Items.Add("2025");
            ddlCovSentYear.Items.Add("2026");
            ddlCovSentYear.Items.Add("2027");
            ddlCovSentYear.Items.Add("2028");
            ddlCovSentYear.Items.Add("2029");
            ddlCovSentYear.Items.Add("2030");
            //ddlCovSentYear.Text = "Year";
            //----------------------------------------------------------------------------------------
            
            lblComments.Visible = true;
            lblDiscipleMentor.Visible = true;
            lbVolunteerInformation.Visible = true;
            lblProgramEnrollment.Visible = true;
            //lblStaffCoordinator.Visible = true;
            lblMentorMaintenance.Visible = true;

            lblActiveMentors.Visible = true;
            lblVolunteerWaiting.Visible = true;
            ddlVolunteerActiveMentees.Visible = true;
            ddlVolunteerWaitingList.Visible = true;
            
            //chbHasGraduated.Visible = true;
            //txbDiscipleshipMentor.Visible = true;
            ///ddlDiscipleShipMentorAvailable.Visible = true;  taking out..RCM..1/10/12.
            //txbStaffCoordinator.Visible = true;
            ddlProgramEnrollment.Visible = true;
            //txbCovenantSentDate.Visible = true;
            //txbCovenantReceivedDate.Visible = true;
            lblCovenantReceivedDate.Visible = true;
            lblCovenantSentDate.Visible = true;
            txbComments.Visible = true;
            cmbUpdate.Visible = true;
            chbCovenantLetter.Visible = true;
            txbStaffCoordinator.Enabled = true;
            //txbDiscipleshipMentor.Enabled = true;
            ddlDiscipleShipMentorAvailable.Enabled = true;

            ddlAssignedMentors.Visible = true;
            ddlAssignedMentors.Enabled = true;
            lbAddMentor.Visible = true;

            ddlProgramEnrollment.Enabled = true;
            //txbCovenantReceivedDate.Enabled = true;
            //txbCovenantSentDate.Enabled = true;
            txbComments.Enabled = true;
            
            //chbHasGraduated.Enabled = true;
            chbCovenantLetter.Enabled = true;

            chbWaitingListInactive.Enabled = true;
            chbWaitingListInactive.Visible = true;
            
            lblWaitingListStudents.Enabled = true;
            lblWaitingListStudents.Visible = true;

            chbVolunteerWaitingList.Enabled = true;
            chbVolunteerWaitingList.Visible = true;
            lblVolunteerWaiting.Visible = true;
            lblActiveMentors.Visible = true;
        }

        protected void DisplayTheGrid()
        {
            con.Open();
            string sql_LoadGrid = "";
            sql_LoadGrid = "select syscreate as 'TimeOfEntry', activitydescription as 'Activity Description', lastupdatedby as 'LastEditedBy'"
                         + "from discipleshipmentordescription "
                         + "where studentlastname ='" + Request.QueryString["StudentLastName"] + "' "
                         + "and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                         + "order by syscreate desc ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "DiscipleshipmentorDescription");
            gvStudentHistory.DataSource = ds.Tables[0];
            gvStudentHistory.DataBind();
            con.Close(); 
        }

        protected void DisplayTheGridFromButton()
        {
            con.Open();
            string sql_LoadGrid = "";
            if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
            {
                sql_LoadGrid = "select syscreate as 'TimeOfEntry', activitydescription as 'Activity Description', lastupdatedby as 'LastEditedBy' "
                             + "from discipleshipmentordescription "
                             + "where studentlastname ='" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "' "
                             + "and studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim() + "' "
                             + "order by syscreate desc ";
            }
            else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
            {
                sql_LoadGrid = "select syscreate as 'TimeOfEntry', activitydescription as 'Activity Description', lastupdatedby as 'LastEditedBy' "
                             + "from discipleshipmentordescription "
                             + "where studentlastname ='" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "' "
                             + "and studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim() + "' "
                             + "order by syscreate desc ";
            }
            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "DiscipleshipmentorDescription");
            gvStudentHistory.DataSource = ds.Tables[0];
            gvStudentHistory.DataBind();
            gvStudentHistory.Visible = true;
            con.Close();
        }

        protected void gvStudentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
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

        public void bind()
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "select syscreate, activitydescription, lastupdatedby "
                         + "from discipleshipmentordescription "
                         + "where studentlastname ='" + Request.QueryString["StudentLastName"] + "' "
                         + "and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                         + "order by syscreate desc ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Discipleshipmentordescription");
            gvStudentHistory.DataSource = ds.Tables[0];
            gvStudentHistory.DataBind();
            con.Close();
        }  

        protected void RetrieveInformation()
        {
            //Retreive information on the student chosen, pertaining to the DiscipleshipMentor Program.
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            cmbDiscipleshipMentor.Enabled = false;

            try
            {
                con.Open();
                con2.Open();
                string sql = "Select dmp.studentlastname, dmp.studentfirstname, dmam.discipleshipmentor, dmp.optionsstaffcoordinator, dmam.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, si.pictureidentification, dmp.sysupdate, "
                           + "dmp.covenantreceived, dmam.covenantsentdate, dmam.covenantreceiveddate, dmp.waitinglistinactive  "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN DiscipleshipMentorAssignedMentors dmam "
                           + "ON (dmp.studentlastname = dmam.studentlastname AND dmp.studentfirstname = dmam.studentfirstname) "
                           //+ "LEFT OUTER JOIN VolunteerInformation vi "
                           //+ "ON (dmp.studentlastname = dmam.studentlastname AND dmp.studentfirstname = dmam.studentfirstname AND dmam.discipleshipmentor) "
                           + "WHERE dmp.studentlastname=@studentlastname "
                           + "AND dmp.studentfirstname=@studentfirstname "
                           + "GROUP BY dmp.studentlastname, dmp.studentfirstname, dmam.discipleshipmentor, dmp.optionsstaffcoordinator, dmam.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, si.pictureidentification, dmp.sysupdate, "
                           + "dmp.covenantreceived, dmam.covenantsentdate, dmam.covenantreceiveddate, dmp.waitinglistinactive  "
                           + "ORDER BY dmp.studentlastname, dmp.studentfirstname, dmam.discipleshipmentor ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                SqlCommand cmd2 = new SqlCommand(sql);

                if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                {
                    cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim()));
                    cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@studentlastname", ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@studentfirstname", ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim()));
                    lbStudentInfo2.Visible = true;
                }
                else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                {
                    cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim()));
                    cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@studentlastname", ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@studentfirstname", ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim()));
                    lbStudentInfo.Visible = true;
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@studentlastname", Request.QueryString["StudentLastName"]));
                    cmd.Parameters.Add(new SqlParameter("@studentfirstname", Request.QueryString["StudentFirstName"]));
                    cmd2.Parameters.Add(new SqlParameter("@studentlastname", Request.QueryString["StudentLastName"]));
                    cmd2.Parameters.Add(new SqlParameter("@studentfirstname", Request.QueryString["StudentFirstName"]));
                }
                cmd.Connection = con;
                cmd2.Connection = con2;
                reader = cmd.ExecuteReader();
                reader2 = cmd2.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    if (reader.IsDBNull(0))
                    {
                        //txbNotes
                    }
                    else
                    {
                        txbNotes.Text = reader.GetString(0);
                    }
                    if (reader.IsDBNull(1))
                    {
                        //txbNotes
                    }
                    else
                    {
                        txbNotes.Text = reader.GetString(1);
                    }
                    if (reader2.Read())
                    {
                        if (reader2.IsDBNull(2))
                        {
                            ddlAssignedMentors.Items.Clear();
                            ddlAssignedMentors.Items.Add("No mentor assigned.");
                            ddlAssignedMentors.Text = "No mentor assigned.";
                            lbVolunteerInformation.Enabled = false;
                        }
                        else
                        {
                            ddlAssignedMentors.Items.Clear();
                            do
                            {
                                ddlAssignedMentors.Items.Add(reader2.GetString(2));
                                ddlAssignedMentors.Text = reader2.GetString(2);
                                lbVolunteerInformation.Enabled = true;

                                if (ddlAssignedMentors.Text == "No mentor assigned.")
                                {
                                    lbVolunteerInformation.Enabled = false;
                                }

                                if (ddlAssignedMentors.Text == "N/A")
                                {
                                    ddlAssignedMentors.Text = "No mentor assigned.";
                                    lbVolunteerInformation.Enabled = false;
                                }
                            } while (reader2.Read());

                        }
                    }
                    //if (reader.IsDBNull(3))
                    //{
                    //    txbStaffCoordinator.Text = "N/A";
                    //}
                    //else
                    //{
                    //    txbStaffCoordinator.Text = reader.GetString(3);
                    //}
                    if (reader.IsDBNull(4))
                    {
                        ddlProgramEnrollment.Text = "N/A";
                    }
                    else
                    {
                        ddlProgramEnrollment.Text = reader.GetString(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        chbHasGraduated.Checked = false;
                    }
                    else
                    {
                        chbHasGraduated.Checked = reader.GetBoolean(5);
                    }
                    if (reader.IsDBNull(6))
                    {
                        txbComments.Text = "N/A";
                    }
                    else
                    {
                        txbComments.Text = reader.GetString(6);
                    }
                    if (reader.IsDBNull(7))
                    {
                        lblLastUpdatedBy.Text = "LastUpdatedBy:  N/A";
                    }
                    else
                    {
                        lblLastUpdatedBy.Text = "LastUpdatedBy:  " + reader.GetString(7) + " On: " + reader.GetSqlValue(9).ToString();
                    }
                    imgImage.ImageUrl = reader.GetString(8);
                    imgImage.Visible = true;
                    if (reader.IsDBNull(10))
                    {
                        chbCovenantLetter.Checked = false;
                    }
                    else
                    {
                        chbCovenantLetter.Checked = reader.GetBoolean(10);
                        if (chbCovenantLetter.Checked)
                        {
                            chbCovenantLetter.Enabled = false;
                        }
                    }
                    if (reader.IsDBNull(11))
                    {
                        txbCovenantSentDate.Text = "1989-01-01";

                        //ddlCovSentMonth.Text = "00";
                        //ddlCovSentDay.Text = "00";
                        //ddlCovSentYear.Text = "0000";
                    }
                    else
                    {
                        txbCovenantSentDate.Text = reader.GetSqlValue(11).ToString();

                        //ddlCovSentMonth.Text = reader.GetSqlValue(11).ToString().Substring(0, 2);
                        //ddlCovSentDay.Text = reader.GetSqlValue(11).ToString().Substring(3, 2);
                        //ddlCovSentYear.Text = reader.GetSqlValue(11).ToString().Substring(6, 4);
                    }
                    if (reader.IsDBNull(12))
                    {
                        txbCovenantReceivedDate.Text = "1989-01-01";
                        //ddlCovRecMonth.Text = "00";
                        //ddlCovRecDay.Text = "00";
                        //ddlCovRecYear.Text = "0000";
                    }
                    else
                    {
                        txbCovenantReceivedDate.Text = reader.GetSqlValue(12).ToString();

                        //ddlCovRecMonth.Text = reader.GetSqlValue(12).ToString().Substring(0, 2);
                        //ddlCovRecDay.Text = reader.GetSqlValue(12).ToString().Substring(3, 2);
                        //ddlCovRecYear.Text = reader.GetSqlValue(12).ToString().Substring(6, 4);
                    }
                    if (reader.IsDBNull(13))
                    {
                        chbWaitingListInactive.Checked = false;
                    }
                    else
                    {
                        chbWaitingListInactive.Checked = reader.GetBoolean(13);
                    }
                    reader2.Close();
                    RetrieveMentorInformation(reader.GetString(0), reader.GetString(1));
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



        protected void RetrieveInformationFromButton()
        {
            //Retreive information on the student chosen, pertaining to the DiscipleshipMentor Program.
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            cmbDiscipleshipMentor.Enabled = false;

            try
            {
                con.Open();
                con2.Open();
                string sql = "Select dmp.studentlastname, dmp.studentfirstname, dmam.discipleshipmentor, dmp.optionsstaffcoordinator, dmam.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, si.pictureidentification, dmp.sysupdate, "
                           + "dmp.covenantreceived, dmam.covenantsentdate, dmam.covenantreceiveddate, dmp.waitinglistinactive  "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN DiscipleshipMentorAssignedMentors dmam "
                           + "ON (dmp.studentlastname = dmam.studentlastname AND dmp.studentfirstname = dmam.studentfirstname) "
                           //+ "LEFT OUTER JOIN VolunteerInformation vi "
                           //+ "ON (dmp.studentlastname = dmam.studentlastname AND dmp.studentfirstname = dmam.studentfirstname AND dmam.discipleshipmentor) "
                           + "WHERE dmp.studentlastname=@studentlastname "
                           + "AND dmp.studentfirstname=@studentfirstname "
                           + "GROUP BY dmp.studentlastname, dmp.studentfirstname, dmam.discipleshipmentor, dmp.optionsstaffcoordinator, dmam.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, si.pictureidentification, dmp.sysupdate, "
                           + "dmp.covenantreceived, dmam.covenantsentdate, dmam.covenantreceiveddate, dmp.waitinglistinactive  "
                           + "ORDER BY dmp.studentlastname, dmp.studentfirstname, dmam.discipleshipmentor ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                SqlCommand cmd2 = new SqlCommand(sql);
                
                if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                {
                    cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim()));
                    cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@studentlastname", ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@studentfirstname", ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim()));
                }
                else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                {
                    cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim()));
                    cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@studentlastname", ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim()));
                    cmd2.Parameters.Add(new SqlParameter("@studentfirstname", ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim()));
                }
                cmd.Connection = con;
                cmd2.Connection = con2;
                reader = cmd.ExecuteReader();
                reader2 = cmd2.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    if (reader.IsDBNull(0))
                    {
                        //              txbNotes
                    }
                    else
                    {
                        txbNotes.Text = reader.GetString(0);
                    }
                    if (reader.IsDBNull(1))
                    {
                        //              txbNotes
                    }
                    else
                    {
                        txbNotes.Text = reader.GetString(1);
                    }
                    if (reader2.Read())
                    {
                        if (reader2.IsDBNull(2))
                        {
                            ddlAssignedMentors.Items.Clear();
                            ddlAssignedMentors.Items.Add("No mentor assigned.");
                            ddlAssignedMentors.Text = "No mentor assigned.";
                            lbVolunteerInformation.Enabled = false;
                        }
                        else
                        {
                            ddlAssignedMentors.Items.Clear();
                            do
                            {
                                ddlAssignedMentors.Items.Add(reader2.GetString(2));
                                ddlAssignedMentors.Text = reader2.GetString(2);
                                lbVolunteerInformation.Enabled = true;

                                if (ddlAssignedMentors.Text == "No mentor assigned.")
                                {
                                    lbVolunteerInformation.Enabled = false;
                                }

                                if (ddlAssignedMentors.Text == "N/A")
                                {
                                    ddlAssignedMentors.Text = "No mentor assigned.";
                                    lbVolunteerInformation.Enabled = false;
                                }
                            } while (reader2.Read());

                        }
                    }
                    //if (reader.IsDBNull(3))
                    //{
                    //    txbStaffCoordinator.Text = "N/A";
                    //}
                    //else
                    //{
                    //    txbStaffCoordinator.Text = reader.GetString(3);
                    //}
                    if (reader.IsDBNull(4))
                    {
                        ddlProgramEnrollment.Text = "N/A";
                    }
                    else
                    {
                        ddlProgramEnrollment.Text = reader.GetString(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        chbHasGraduated.Checked = false;
                    }
                    else
                    {
                        chbHasGraduated.Checked = reader.GetBoolean(5);
                    }
                    if (reader.IsDBNull(6))
                    {
                        txbComments.Text = "N/A";
                    }
                    else
                    {
                        txbComments.Text = reader.GetString(6);
                    }
                    if (reader.IsDBNull(7))
                    {
                        lblLastUpdatedBy.Text = "LastUpdatedBy:  N/A";
                    }
                    else
                    {
                        lblLastUpdatedBy.Text = "LastUpdatedBy:  " + reader.GetString(7) + " On: " + reader.GetSqlValue(9).ToString();
                    }
                    imgImage.ImageUrl = reader.GetString(8);
                    imgImage.Visible = true;
                    if (reader.IsDBNull(10))
                    {
                        chbCovenantLetter.Checked = false;
                    }
                    else
                    {
                        chbCovenantLetter.Checked = reader.GetBoolean(10);
                        if (chbCovenantLetter.Checked)
                        {
                            chbCovenantLetter.Enabled = false;
                        }
                    }
                    if (reader.IsDBNull(11))
                    {
                        txbCovenantSentDate.Text = "1989-01-01";
                        //ddlCovSentMonth.Text = "01";
                        //ddlCovSentDay.Text = "01";
                        //ddlCovSentYear.Text = "1989";
                    }
                    else
                    {
                        txbCovenantSentDate.Text = reader.GetSqlValue(11).ToString();
                        
                        //ddlCovSentMonth.Text = reader.GetSqlValue(11).ToString().Substring(0, 2);
                        //ddlCovSentDay.Text = reader.GetSqlValue(11).ToString().Substring(3, 2);
                        //ddlCovSentYear.Text = reader.GetSqlValue(11).ToString().Substring(6, 4);
                    }
                    if (reader.IsDBNull(12))
                    {
                        txbCovenantReceivedDate.Text = "1989-01-01";
                        //ddlCovRecMonth.Text = "01";
                        //ddlCovRecDay.Text = "01";
                        //ddlCovRecYear.Text = "1989";
                    }
                    else
                    {
                        txbCovenantReceivedDate.Text = reader.GetSqlValue(12).ToString();
                        
                        //ddlCovRecMonth.Text = reader.GetSqlValue(12).ToString().Substring(0, 2);
                        //ddlCovRecDay.Text = reader.GetSqlValue(12).ToString().Substring(3, 2);
                        //ddlCovRecYear.Text = reader.GetSqlValue(12).ToString().Substring(6, 4);
                    }
                    if (reader.IsDBNull(13))
                    {
                        chbWaitingListInactive.Checked = false;
                    }
                    else
                    {
                        chbWaitingListInactive.Checked = reader.GetBoolean(13);
                    }
                    reader2.Close();
                    RetrieveMentorInformation(reader.GetString(0), reader.GetString(1));
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

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CleanCharacters()
        {
            //Strips out all apostrophes from all fields...RCM..3/21/12.

            txbComments.Text = txbComments.Text.Replace("'","");
            txbNotes.Text = txbNotes.Text.Replace("'", "");
            txbDiscipleshipMentorNotes.Text = txbDiscipleshipMentorNotes.Text.Replace("'", "");
        }

        protected void cmbUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CleanCharacters();
                
                if (cmbUpdate.Text == "Insert New Entry")
                {
                    txbNotes.Text = txbNotes.Text.Replace("'", "");


                    ddlAssignedMentors.Enabled = true;
                    lbVolunteerInformation.Enabled = true;
                    lbViewMentorProfile.Enabled = true;
                    lbAddMentor.Enabled = true;
                    ddlProgramEnrollment.Enabled = true;
                    txbCovenantReceivedDate.Enabled = true;
                    txbCovenantSentDate.Enabled = true;

                    if (txbNotes.Text == "(Type new activity entry here!)")
                    {
                        //Prompt to tell them that they haven't entered anything..
                        lbAddNewEntry.Enabled = true;
                        txbNotes.Visible = false;
                        lblNewEntry.Visible = false;

                        cmbUpdate.Text = "Update Student Information";
                        cmbUpdate.Enabled = true;

                        //UnLocking the other header columns..RCM..
                        txbStaffCoordinator.Enabled = true;
                        //txbDiscipleshipMentor.Enabled = true;
                        ddlDiscipleShipMentorAvailable.Enabled = true;
                        ddlProgramEnrollment.Enabled = true;
                        //txbCovenantReceivedDate.Enabled = true;
                        //txbCovenantSentDate.Enabled = true;
                        txbComments.Enabled = true;
                        chbHasGraduated.Enabled = true;
                        chbWaitingListInactive.Enabled = true;
                        chbVolunteerWaitingList.Enabled = true;
                        lblWaitingListStudents.Enabled = true;
                        if (chbCovenantLetter.Checked)
                        {

                        }
                        else
                        {
                            chbCovenantLetter.Enabled = true;
                        }

                        if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                        {
                            lbStudentInfo2.Enabled = true;
                        }
                        else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                        {
                            lbStudentInfo.Enabled = true;
                        }
                    }
                    else
                    {
                        //Insert the new entry into the database table.
                        string sql_InsertNewEntry = "";
                        if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
                        {
                            if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                            {
                                sql_InsertNewEntry = "INSERT into DiscipleshipMentorDescription "
                                                            + "values ("
                                                            + "'" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "' , "
                                                            + "'" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim() + "' , "
                                                            + "'" + txbNotes.Text.Trim() + "' , "
                                                            + "'" + System.DateTime.Now.ToString() + "',"
                                                            + "'" + System.DateTime.Now.ToString() + "',"
                                                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";

                                lbStudentInfo2.Enabled = true;
                            }
                            else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                            {
                                sql_InsertNewEntry = "INSERT into DiscipleshipMentorDescription "
                                                            + "values ("
                                                            + "'" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "' , "
                                                            + "'" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim() + "' , "
                                                            + "'" + txbNotes.Text.Trim() + "' , "
                                                            + "'" + System.DateTime.Now.ToString() + "',"
                                                            + "'" + System.DateTime.Now.ToString() + "',"
                                                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                                lbStudentInfo.Enabled = true;
                            }
                        }
                        else
                        {
                            sql_InsertNewEntry = "INSERT into DiscipleshipMentorDescription "
                                                        + "values ("
                                                        + "'" + Request.QueryString["StudentLastName"].Trim() + "' , "
                                                        + "'" + Request.QueryString["StudentFirstName"].Trim() + "' , "
                                                        + "'" + txbNotes.Text.Trim() + "' , "
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                        }
                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand2 = new SqlCommand(sql_InsertNewEntry, con);
                        if (sqlInsertCommand2.ExecuteNonQuery() > 0)
                        {
                            con.Close();
                            gvStudentHistory.DataBind();
                            txbNotes.Visible = false;
                            lblNewEntry.Visible = false;
                            lbAddNewEntry.Enabled = true;
                            if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
                            {
                                DisplayTheGridFromButton();
                            }
                            else
                            {
                                DisplayTheGrid();
                            }
                            cmbUpdate.Text = "Update Student Information";
                            cmbUpdate.Enabled = true;

                            //UnLocking the other header columns..RCM..
                            txbStaffCoordinator.Enabled = true;
                            //txbDiscipleshipMentor.Enabled = true;
                            ddlDiscipleShipMentorAvailable.Enabled = true;
                            ddlProgramEnrollment.Enabled = true;
                            //txbCovenantReceivedDate.Enabled = true;
                            //txbCovenantSentDate.Enabled = true;
                            txbComments.Enabled = true;
                            chbHasGraduated.Enabled = true;
                            chbWaitingListInactive.Enabled = true;
                            chbVolunteerWaitingList.Enabled = true;
                            lblWaitingListStudents.Enabled = true;
                            if (chbCovenantLetter.Checked)
                            {

                            }
                            else
                            {
                                chbCovenantLetter.Enabled = true;
                            }
                        }
                        else
                        {
                            //display message that record was NOT updated
                            //	btnContinue.Visible = false;
                            //	lblAlert.Visible = true;
                            //	lblAlert.Text = "No update. Error has occurred.";
                        }
                    }
                }
                else if (cmbUpdate.Text == "Update Mentor Maintenance")
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        string secondUpdate = "";
                        string thirdupdate = "";
                        //ddlDiscipleShipMentorAvailable.Text = ddlDiscipleShipMentorAvailable.Text.Replace("'", "");
                        //txbDiscipleshipMentor.Text = txbDiscipleshipMentor.Text.Replace("'", "");
                        txbStaffCoordinator.Text = txbStaffCoordinator.Text.Replace("'", "");
                        //txbProgramEnrollment.Text = txbProgramEnrollment.Text.Replace("'", "");
                        txbComments.Text = txbComments.Text.Replace("'", "");

                        if ((ddlVolunteerActiveMentees.Text == "Please select a mentor") && (ddlVolunteerWaitingList.Text != "Please select a mentor"))
                        {
                            sqlUpdateStatement = " UPDATE VolunteerInformation "
                            + "SET "
                            + " discipleshipmentortraining = " + Convert.ToInt32(chbTrained.Checked) + ", "
                            + " discipleshipmentornotes = '" + txbDiscipleshipMentorNotes.Text.Trim() + "' , "
                            + " discipleshipmentorpotentials = " + Convert.ToInt32(chbVolunteerWaitingList.Checked) + ", "
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                            + "  "
                            + " WHERE lastname = '" + ddlVolunteerWaitingList.SelectedValue.Substring(0, ddlVolunteerWaitingList.SelectedValue.IndexOf(",")) + "' "
                            + " AND firstname = '" + ddlVolunteerWaitingList.SelectedValue.Substring(ddlVolunteerWaitingList.SelectedValue.IndexOf(",") + 1) + "' ";

                            secondUpdate = "UPDATE DiscipleshipmentorAssignedMentors "
                                         + "SET programenrollment = '" + ddlProgramEnrollment.Text.Trim() + "' "
                                         + "WHERE Discipleshipmentor = '" + ddlVolunteerWaitingList.Text.Trim().Substring(ddlVolunteerWaitingList.Text.Trim().IndexOf(",") + 1, ddlVolunteerWaitingList.Text.Length - (ddlVolunteerWaitingList.Text.Trim().IndexOf(",") + 1)) + " " + ddlVolunteerWaitingList.Text.Trim().Substring(0, ddlVolunteerWaitingList.Text.IndexOf(",")) + "' ";

                            thirdupdate = " UPDATE VolunteerDetails "
                                        + " SET backgroundcheck = " + Convert.ToInt32(chbBackGroundCheck.Checked) + ", "
                                        + " spiritualjourney = " + Convert.ToInt32(chbSpiritualJourney.Checked) + ", "
                                        + " vehichleinsurance = " + Convert.ToInt32(chbVehichleInsurance.Checked) + ", "
                                        + " releasewaiver = " + Convert.ToInt32(chbReleaseWaiver.Checked) + ", "
                                        + " generalinformation = " + Convert.ToInt32(chbGeneralInformation.Checked) + " "
                                        + "  "
                                        + " WHERE lastname = '" + ddlVolunteerWaitingList.SelectedValue.Substring(0, ddlVolunteerWaitingList.SelectedValue.IndexOf(",")) + "' "
                                        + " AND firstname = '" + ddlVolunteerWaitingList.SelectedValue.Substring(ddlVolunteerWaitingList.SelectedValue.IndexOf(",") + 1) + "' ";
                        }
                        else if ((ddlVolunteerWaitingList.Text == "Please select a mentor") && (ddlVolunteerActiveMentees.Text != "Please select a mentor"))
                        {
                            sqlUpdateStatement = " UPDATE VolunteerInformation "
                            + "SET "
                            + " discipleshipmentortraining = " + Convert.ToInt32(chbTrained.Checked) + ", "
                            + " discipleshipmentornotes = '" + txbDiscipleshipMentorNotes.Text.Trim() + "' , "
                            + " discipleshipmentorpotentials = " + Convert.ToInt32(chbVolunteerWaitingList.Checked) + ", "
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                            + "  "
                            + " WHERE lastname = '" + ddlVolunteerActiveMentees.SelectedValue.Substring(0, ddlVolunteerActiveMentees.SelectedValue.IndexOf(",")) + "' "
                            + " AND firstname = '" + ddlVolunteerActiveMentees.SelectedValue.Substring(ddlVolunteerActiveMentees.SelectedValue.IndexOf(",") + 1) + "' ";

                            secondUpdate = "UPDATE DiscipleshipmentorAssignedMentors "
                                         + "SET programenrollment = '" + ddlProgramEnrollment.Text.Trim() + "' "
                                         + "WHERE Discipleshipmentor = '" + ddlVolunteerActiveMentees.Text.Trim().Substring(ddlVolunteerActiveMentees.Text.Trim().IndexOf(",") + 1, ddlVolunteerActiveMentees.Text.Length - (ddlVolunteerActiveMentees.Text.Trim().IndexOf(",") + 1)) + " " + ddlVolunteerActiveMentees.Text.Trim().Substring(0, ddlVolunteerActiveMentees.Text.IndexOf(",")) + "' ";

                            thirdupdate = " UPDATE VolunteerDetails "
                                        + " SET backgroundcheck = " + Convert.ToInt32(chbBackGroundCheck.Checked) + ", "
                                        + " spiritualjourney = " + Convert.ToInt32(chbSpiritualJourney.Checked) + ", "
                                        + " vehichleinsurance = " + Convert.ToInt32(chbVehichleInsurance.Checked) + ", "
                                        + " releasewaiver = " + Convert.ToInt32(chbReleaseWaiver.Checked) + ", "
                                        + " generalinformation = " + Convert.ToInt32(chbGeneralInformation.Checked) + " "
                                        + "  "
                                        + " WHERE lastname = '" + ddlVolunteerActiveMentees.SelectedValue.Substring(0, ddlVolunteerActiveMentees.SelectedValue.IndexOf(",")) + "' "
                                        + " AND firstname = '" + ddlVolunteerActiveMentees.SelectedValue.Substring(ddlVolunteerActiveMentees.SelectedValue.IndexOf(",") + 1) + "' ";
                        }

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

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand2 = new SqlCommand(secondUpdate, con);
                        if (sqlUpdateCommand2.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand3 = new SqlCommand(thirdupdate, con);
                        if (sqlUpdateCommand3.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjlka)
                    {

                    }
                }
                else
                {
                    try
                    {
                        string sqlUpdateStatement = "";
                        string secondUpdate = "";
                        //ddlDiscipleShipMentorAvailable.Text = ddlDiscipleShipMentorAvailable.Text.Replace("'", "");
                        //txbDiscipleshipMentor.Text = txbDiscipleshipMentor.Text.Replace("'", "");
                        txbStaffCoordinator.Text = txbStaffCoordinator.Text.Replace("'", "");
                        //txbProgramEnrollment.Text = txbProgramEnrollment.Text.Replace("'", "");
                        txbComments.Text = txbComments.Text.Replace("'", "");

                        if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                        {
                            sqlUpdateStatement = " UPDATE DiscipleshipMentorProgram "
                            + "SET "
                            + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                            + " covenantreceived = " + Convert.ToInt32(chbCovenantLetter.Checked) + ", "
                            + " waitinglistinactive = " + Convert.ToInt32(chbWaitingListInactive.Checked) + ", "
                            + " comment = '" + txbComments.Text.Trim() + "' , "
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                            + "  "
                            + " WHERE studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")) + "' "
                            + " AND studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1) + "' ";

                            secondUpdate = "UPDATE DiscipleshipmentorAssignedMentors "
                                         + "SET programenrollment = '" + ddlProgramEnrollment.Text.Trim() + "', "
                                         + " covenantreceiveddate = '" + txbCovenantReceivedDate.Text + "', "
                                         + " covenantsentdate = '" + txbCovenantSentDate.Text + "' "
                                         + " WHERE studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")) + "' "
                                         + " AND studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1) + "' "
                                         + " AND discipleshipmentor = '" + ddlAssignedMentors.Text.ToString() + "' ";
                        }
                        else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                        {
                            sqlUpdateStatement = " UPDATE DiscipleshipMentorProgram "
                            + "SET "
                            + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                            + " covenantreceived = " + Convert.ToInt32(chbCovenantLetter.Checked) + ", "
                            + " waitinglistinactive = " + Convert.ToInt32(chbWaitingListInactive.Checked) + ", "
                            + " comment = '" + txbComments.Text.Trim() + "' , "
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                            + "  "
                            + " WHERE studentlastname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")) + "' "
                            + " AND studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1) + "' ";

                            secondUpdate = "UPDATE DiscipleshipmentorAssignedMentors "
                                         + "SET programenrollment = '" + ddlProgramEnrollment.Text.Trim() + "', "
                                         + " covenantreceiveddate = '" + txbCovenantReceivedDate.Text + "', "
                                         + " covenantsentdate = '" + txbCovenantSentDate.Text + "' "
                                         + " WHERE studentlastname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")) + "' "
                                         + " AND studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1) + "' "
                                         + " AND discipleshipmentor = '" + ddlAssignedMentors.Text.ToString() + "' ";
                        }

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        {
                            ddlAssignedMentors.Enabled = true;
                            lbAddMentor.Enabled = true;
                            lbVolunteerInformation.Enabled = true;
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }

                        //create a SQL command to update record
                        SqlCommand sqlUpdateCommand2 = new SqlCommand(secondUpdate, con);
                        if (sqlUpdateCommand2.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                            //Didn't find a record to update..RCM..
                        }
                    }
                    catch (Exception lkjlka)
                    {


                    }
                }
                //ddlAssignedMentors.Enabled = true;
                //lbAddMentor.Enabled = true;
                //lbVolunteerInformation.Enabled = true;

                ddlDiscipleshipMentor.Enabled = true;
                ddlWaitingListInactiveStudents.Enabled = true;
                lblEnrolledStudents.Enabled = true;
                lblWaitingListStudents.Enabled = true;
            }
            catch (Exception lkjlkaabb)
            {

            }
        }

        
        protected void UpdateAllInformation()
        {
            try
            {
                CleanCharacters();

                string sqlUpdateStatement = "";
                string secondUpdate = "";
                txbStaffCoordinator.Text = txbStaffCoordinator.Text.Replace("'", "");
                txbComments.Text = txbComments.Text.Replace("'", "");

                if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                {
                    sqlUpdateStatement = " UPDATE DiscipleshipMentorProgram "
                    + "SET "
                    + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                    + " covenantreceived = " + Convert.ToInt32(chbCovenantLetter.Checked) + ", "
                    + " waitinglistinactive = " + Convert.ToInt32(chbWaitingListInactive.Checked) + ", "
                    + " comment = '" + txbComments.Text.Trim() + "' , "
                    + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                    + "  "
                    + " WHERE studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")) + "' "
                    + " AND studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1) + "' ";

                    secondUpdate = "UPDATE DiscipleshipmentorAssignedMentors "
                                 + "SET programenrollment = '" + ddlProgramEnrollment.Text.Trim() + "', "
                                 + " covenantreceiveddate = '" + txbCovenantReceivedDate.Text + "', "
                                 + " covenantsentdate = '" + txbCovenantSentDate.Text + "', "
                                 //+ " covenantreceiveddate = '" + ddlCovRecMonth.Text.Trim() + "-" + ddlCovRecDay.Text.Trim() + "-" + ddlCovRecYear.Text.Trim() + "' , "
                                 //+ " covenantsentdate = '" + ddlCovSentMonth.Text.Trim() + "-" + ddlCovSentDay.Text.Trim() + "-" + ddlCovSentYear.Text.Trim() + "'  "
                                 + " WHERE studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")) + "' "
                                 + " AND studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1) + "' ";
                }
                else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                {
                    sqlUpdateStatement = " UPDATE DiscipleshipMentorProgram "
                    + "SET "
                    + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                    + " covenantreceived = " + Convert.ToInt32(chbCovenantLetter.Checked) + ", "
                    + " waitinglistinactive = " + Convert.ToInt32(chbWaitingListInactive.Checked) + ", "
                    + " comment = '" + txbComments.Text.Trim() + "' , "
                    + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                    + "  "
                    + " WHERE studentlastname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")) + "' "
                    + " AND studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1) + "' ";

                    secondUpdate = "UPDATE DiscipleshipmentorAssignedMentors "
                                 + "SET programenrollment = '" + ddlProgramEnrollment.Text.Trim() + "', "
                                 + " covenantreceiveddate = '" + txbCovenantReceivedDate.Text + "', "
                                 + " covenantsentdate = '" + txbCovenantSentDate.Text + "', "
                                 //+ " covenantreceiveddate = '" + ddlCovRecMonth.Text.Trim() + "-" + ddlCovRecDay.Text.Trim() + "-" + ddlCovRecYear.Text.Trim() + "' , "
                                 //+ " covenantsentdate = '" + ddlCovSentMonth.Text.Trim() + "-" + ddlCovSentDay.Text.Trim() + "-" + ddlCovSentYear.Text.Trim() + "'  "
                                 + " WHERE studentlastname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")) + "' "
                                 + " AND studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1) + "' ";
                }

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

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand2 = new SqlCommand(secondUpdate, con);
                if (sqlUpdateCommand2.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                    //Didn't find a record to update..RCM..
                }
            }
            catch (Exception lkjlka)
            {
                //Make the Error visible to the user so that they know there was a problem...RCM..

            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void chbHasGraduated_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void txbProgramEnrollment_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txbComments_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txbDiscipleshipMentor_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDiscipleshipMentor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateAllInformation();

            ddlVolunteerWaitingList.Text = "Please select a mentor";
            ddlVolunteerActiveMentees.Text = "Please select a mentor";
            
            //WaitingList = 0;
            cmbDiscipleshipMentor.Enabled = true;
            cmbUpdate.Enabled = false;
            cmbBackToStudentPage.Enabled = true;

            //Disable all fields until they retreive the information so they aren't confused with who they are working on..RCM..
            //Locking the other header columns..RCM..
            txbStaffCoordinator.Enabled = false;
            //txbDiscipleshipMentor.Enabled = false;
            ddlDiscipleShipMentorAvailable.Enabled = false;
            //txbProgramEnrollment.Enabled = false;
            //txbCovenantReceivedDate.Enabled = false;
            //txbCovenantSentDate.Enabled = false;
            txbComments.Enabled = false;
            chbHasGraduated.Enabled = false;
            chbCovenantLetter.Enabled = false;
            chbWaitingListInactive.Enabled = false;
            ddlWaitingListInactiveStudents.Text = "Please select a student";

            ClearValues();
            lbStudentInfo.Style.Add("z-index", "99999");
            lbStudentInfo.Enabled = true;
            lbStudentInfo.Visible = true;
            lbStudentInfo2.Style.Add("z-index", "99999");
            lbStudentInfo2.Enabled = false;
            lbStudentInfo2.Visible = true;

            DisplayHeaderFields();
            //PopulateDiscipleshipMentorAvailable();
            RetrieveInformationFromButton();
            DisplayTheGridFromButton();
            cmbUpdate.Enabled = true;
            lblNotes.Visible = true;
            lbAddNewEntry.Visible = true;

            ddlCovSentYear.Enabled = true;
            ddlCovSentMonth.Enabled = true;
            ddlCovSentDay.Enabled = true;
            ddlCovRecYear.Enabled = true;
            ddlCovRecMonth.Enabled = true;
            ddlCovRecDay.Enabled = true;

            txbCovenantSentDate.Enabled = true;
            txbCovenantReceivedDate.Enabled = true;
            imbCalCovenantReceiveDate.Enabled = true;
            imbCalCovenantSentDate.Enabled = true;


            lbVolunteerDetails.Enabled = false;
            chbBackGroundCheck.Enabled = false;
            chbGeneralInformation.Enabled = false;
            chbSpiritualJourney.Enabled = false;
            chbVehichleInsurance.Enabled = false;
            chbReleaseWaiver.Enabled = false;
            txbDiscipleshipMentorNotes.Enabled = false;
            chbTrained.Enabled = false;
            cmbUpdate.Text = "Update Student Information";
            ddlProgramEnrollment.Visible = true;
            ddlProgramEnrollment.Enabled = true;
        }

        protected void gvStudentHistory_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void lbAddNewEntry_Click(object sender, EventArgs e)
        {
            lbAddNewEntry.Enabled = false;//RCM..11/14/11.

            txbNotes.Visible = true;
            txbNotes.Text = "(Type new activity entry here!)";
            txbNotes.Style.Add("z-index", "999999");
            
            cmbUpdate.Text = "Insert New Entry";
            cmbUpdate.Enabled = true;
            lblNewEntry.Visible = true;

            lbStudentInfo.Enabled = false;
            lbStudentInfo2.Enabled = false;

            ddlDiscipleshipMentor.Enabled = false;
            ddlWaitingListInactiveStudents.Enabled = false;
            
            //Locking the other header columns..RCM..
            txbStaffCoordinator.Enabled = false;
            //txbDiscipleshipMentor.Enabled = false;

            ddlAssignedMentors.Enabled = false;
            lbAddMentor.Enabled = false;
            lbVolunteerInformation.Enabled = false;

            ddlDiscipleShipMentorAvailable.Enabled = false;
            ddlProgramEnrollment.Enabled = false;
            //txbCovenantReceivedDate.Enabled = false;
            //txbCovenantSentDate.Enabled = false;
            txbComments.Enabled = false;
            chbHasGraduated.Enabled = false;
            chbCovenantLetter.Enabled = false;
            chbWaitingListInactive.Enabled = false;
            chbVolunteerWaitingList.Enabled = false;
            lblEnrolledStudents.Enabled = false;
            lblWaitingListStudents.Enabled = false;

            ddlCovRecDay.Enabled = false;
            ddlCovRecMonth.Enabled = false;
            ddlCovRecYear.Enabled = false;
            ddlCovSentDay.Enabled = false;
            ddlCovSentMonth.Enabled = false;
            ddlCovSentYear.Enabled = false;

            txbCovenantReceivedDate.Enabled = false;
            txbCovenantSentDate.Enabled = false;
        }

        protected void txbNotes_TextChanged(object sender, EventArgs e)
        {
            //txbNotes.Text = "";
            //cmbUpdate.Enabled = true;
        }

        protected void cmbBackToStudentPage_Click(object sender, EventArgs e)
        {
            Response.Clear();
            if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim());
            }
            else
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim());
            }
        }

        protected void cmbViewAll_Click(object sender, EventArgs e)
        {
            try
            {
                //UpdateAllInformation();
                ClearPage();
                lblReportTitle.Text = "View All Information Report";
                lblReportTitle.Visible = true;
                cmbWaitListReport.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbExpandedMentorReport.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                cmbVolunteerReport.Enabled = false;
                cmbRecentUpdates.Enabled = false;
                con.Open();

                string sql = "Select dmp.studentlastname as 'LastName', dmp.studentfirstname as 'FirstName', si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1 as 'ParentGuardian', pg.workphone1 as 'WorkPhone', pg.cellphone1 as 'CellPhone', "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived as 'Covenant', dmam.discipleshipmentor as 'DiscipleshipMentor',dmp.optionsstaffcoordinator as 'StaffCoordinator', dmam.programenrollment as 'ProgramEnrollment', "
                           + "dmp.hasgraduated as 'Graduated', dmp.comment as 'Comments', dmp.lastupdatedby as 'LastUpdatedBy', dmam.covenantreceiveddate as 'Cov ReceiveDate', dmam.covenantsentdate as 'Cov SentDate', dmp.waitinglistinactive as 'WaitingList' "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                           + "ON (dmp.studentlastname = pg.studentlastname AND dmp.studentfirstname = pg.studentfirstname) "
                           + "LEFT OUTER JOIN DiscipleshipmentorAssignedMentors dmam "
                           + "ON (dmp.studentlastname = dmam.Studentlastname AND dmp.studentfirstname = dmam.studentfirstname) "
                           + "GROUP BY dmp.studentlastname, dmp.studentfirstname, si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1, pg.workphone1, pg.cellphone1, "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived, dmam.discipleshipmentor,dmp.optionsstaffcoordinator, dmam.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, dmam.covenantreceiveddate, dmam.covenantsentdate, dmp.waitinglistinactive "
                           + "ORDER BY dmp.studentlastname, dmp.studentfirstname ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorProgram");
                gvViewAll.DataSource = ds.Tables[0];
                gvViewAll.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }


        protected void ClearValues()
        {

            ddlCovSentYear.ClearSelection();
            ddlCovSentMonth.ClearSelection();
            ddlCovSentDay.ClearSelection();
            ddlCovRecYear.ClearSelection();
            ddlCovRecMonth.ClearSelection();
            ddlCovRecDay.ClearSelection();

            txbCovenantSentDate.Text = "1989-01-01";
            txbCovenantReceivedDate.Text = "1989-01-01";

            txbDiscipleshipMentorNotes.Text = "N/A";
            
            chbBackGroundCheck.Checked = false;
            chbSpiritualJourney.Checked = false;
            chbVehichleInsurance.Checked = false;
            chbReleaseWaiver.Checked = false;
            chbGeneralInformation.Checked = false;
            chbTrained.Checked = false;

            txbComments.Text = "N/A";
            txbNotes.Text = "N/A";
            ddlProgramEnrollment.Text = "N/A";
            imgImage.ImageUrl = "";
            imgMentor.ImageUrl = "";
            chbCovenantLetter.Checked = false;
            //chbWaitingListInactive.Checked = false;
        }


        protected void ClearPage()
        {
            txbComments.Visible = false;
            //txbDiscipleshipMentor.Visible = false;
            ddlDiscipleShipMentorAvailable.Visible = false;

            ddlAssignedMentors.Visible = false;
            lbAddMentor.Visible = false;


            //New.. as of 2/27/12..
            Panel5.Visible = false;
            lbStudentInfo.Visible = false;
            lbStudentInfo2.Visible = false;
            lblMenteeMaintenance.Visible = false;
            lblVariousPaperwork.Visible = false;
            lblMentorMaintenance.Visible = false;

            txbComments.Visible = false;
            txbNotes.Visible = false;
            //txbCovenantSentDate.Visible = false;
            //txbCovenantReceivedDate.Visible = false;
            txbStaffCoordinator.Visible = false;
            ddlProgramEnrollment.Visible = false;
            gvStudentHistory.Visible = false;
            chbHasGraduated.Visible = false;
            lbAddNewEntry.Visible = false;
            lblComments.Visible = false;
            lblDiscipleMentor.Visible = false;
            lbVolunteerInformation.Visible = false;
            lblNewEntry.Visible = false;
            lblProgramEnrollment.Visible = false;
            lblStaffCoordinator.Visible = false;
            ddlProgramEnrollment.Visible = false;
            cmbUpdate.Visible = false;
            lblEnrolledStudents.Visible = false;
            ddlDiscipleshipMentor.Visible = false;
            ddlWaitingListInactiveStudents.Visible = false;
            ddlVolunteerActiveMentees.Visible = false;
            ddlVolunteerWaitingList.Visible = false;
            //lblDiscipleshipmentor.Visible = false;

            lblActiveMentors.Visible = false;
            lblVolunteerWaiting.Visible = false;
            chbVolunteerWaitingList.Visible = false;

            lbVolunteerInformation.Visible = false;
            cmbDiscipleshipMentor.Visible = false;
            imgImage.Visible = false;
            lblNotes.Visible = false;
            chbCovenantLetter.Enabled = true;
            chbCovenantLetter.Visible = false;
            chbWaitingListInactive.Visible = false;
            lblWaitingListStudents.Visible = false;
            lblCovenantReceivedDate.Visible = false;
            lblCovenantSentDate.Visible = false;

            txbCovenantReceivedDate.Visible = false;
            txbCovenantSentDate.Visible = false;

            ddlCovRecDay.Visible = false;
            ddlCovRecMonth.Visible = false;
            ddlCovRecYear.Visible = false;
            ddlCovSentDay.Visible = false;
            ddlCovSentMonth.Visible = false;
            ddlCovSentYear.Visible = false;
            ddlDiscipleShipMentorAvailable.Visible = false;
            pnlPanel.Visible = false;
        }


        protected void cmbResetPage_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void chbCovenantLetter_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCovenantLetter.Checked)
            {
                //Message("This will enroll this student into the DiscipleshipMentor Program.  Do you wish to proceed?  ", this);
                //lbMSHSChoir.Enabled = true;
                //Insert into the Discipleship Mentor Program table...RCM..3/22/11.
                try
                {
                    chbCovenantLetter.Enabled = false;
                    //txbCovenantReceivedDate.Enabled = true;
                    //txbCovenantReceivedDate.Text = "";
                }
                catch (Exception alskdjaa)
                {

                }
                finally
                {
                    //con.Close();
                }
            }
            else
            {
                //lbMSHSChoir.Enabled = false;
            }
        }

        protected void txbDateEstablished_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbVolunteerInformation_Click(object sender, EventArgs e)
        {
            if ((ddlVolunteerActiveMentees.Text == "Please select a mentor") && (ddlVolunteerWaitingList.Text == "Please select a mentor"))
            {
                if ((lbVolunteerInformation.Text == "(Mentor Profile)") && (ddlAssignedMentors.Text != "No mentor assigned."))
                {
                    Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&VolunteerFirstName=" + ddlAssignedMentors.Text.Substring(0, ddlAssignedMentors.Text.IndexOf(" "))
                                + "&VolunteerLastName=" + ddlAssignedMentors.Text.Substring(ddlAssignedMentors.Text.IndexOf(" ") + 1, (ddlAssignedMentors.Text.Length - ddlAssignedMentors.Text.IndexOf(" ") - 1)));
                }
            }
            else if ((ddlVolunteerActiveMentees.Text != "Please select a mentor") && (ddlVolunteerWaitingList.Text == "Please select a mentor"))
            {
                Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&VolunteerLastName=" + ddlVolunteerActiveMentees.Text.Substring(0, ddlVolunteerActiveMentees.Text.IndexOf(","))
                                + "&VolunteerFirstName=" + ddlVolunteerActiveMentees.Text.Substring(ddlVolunteerActiveMentees.Text.IndexOf(",") + 1, (ddlVolunteerActiveMentees.Text.Length - ddlVolunteerActiveMentees.Text.IndexOf(",") - 1)));
            }
            else if ((ddlVolunteerActiveMentees.Text == "Please select a mentor") && (ddlVolunteerWaitingList.Text != "Please select a mentor"))
            {
                Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&VolunteerLastName=" + ddlVolunteerWaitingList.Text.Substring(0, ddlVolunteerWaitingList.Text.IndexOf(","))
                                + "&VolunteerFirstName=" + ddlVolunteerWaitingList.Text.Substring(ddlVolunteerWaitingList.Text.IndexOf(",") + 1, (ddlVolunteerWaitingList.Text.Length - ddlVolunteerWaitingList.Text.IndexOf(",") - 1)));
            }
        }

        protected void gvViewAll_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlWaitingListInactiveStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateAllInformation();

            ddlVolunteerWaitingList.Text = "Please select a mentor";
            ddlVolunteerActiveMentees.Text = "Please select a mentor";
                        
            //WaitingList = 2;
            cmbDiscipleshipMentor.Enabled = true;
            cmbUpdate.Enabled = false;
            cmbBackToStudentPage.Enabled = true;

            //Disable all fields until they retreive the information so they aren't confused with who they are working on..RCM..
            //Locking the other header columns..RCM..
            txbStaffCoordinator.Enabled = false;
            //txbDiscipleshipMentor.Enabled = false;
            ddlDiscipleShipMentorAvailable.Enabled = false;
            //txbProgramEnrollment.Enabled = false;
            //txbCovenantReceivedDate.Enabled = false;
            //txbCovenantSentDate.Enabled = false;
            txbComments.Enabled = false;
            chbHasGraduated.Enabled = false;
            chbCovenantLetter.Enabled = false;
            chbWaitingListInactive.Enabled = false;
            //lblWaitingListStudents.Enabled = false;
            ddlDiscipleshipMentor.Text = "Please select a student";

            //testing putting in...
            ClearValues();

            lbStudentInfo.Style.Add("z-index", "99999");
            lbStudentInfo.Enabled = false;
            lbStudentInfo.Visible = true;

            lbStudentInfo2.Style.Add("z-index", "99999");
            lbStudentInfo2.Enabled = true;
            lbStudentInfo2.Visible = true;
                        
            DisplayHeaderFields();
            //PopulateDiscipleshipMentorAvailable();
            RetrieveInformationFromButton();
            DisplayTheGridFromButton();
            cmbUpdate.Enabled = true;
            lblNotes.Visible = true;
            lbAddNewEntry.Visible = true;

            ddlCovSentYear.Enabled = true;
            ddlCovSentMonth.Enabled = true;
            ddlCovSentDay.Enabled = true;
            ddlCovRecYear.Enabled = true;
            ddlCovRecMonth.Enabled = true;
            ddlCovRecDay.Enabled = true;

            txbCovenantSentDate.Enabled = true;
            txbCovenantReceivedDate.Enabled = true;
            imbCalCovenantReceiveDate.Enabled = true;
            imbCalCovenantSentDate.Enabled = true;

            lbVolunteerDetails.Enabled = false;
            chbBackGroundCheck.Enabled = false;
            chbGeneralInformation.Enabled = false;
            chbSpiritualJourney.Enabled = false;
            chbVehichleInsurance.Enabled = false;
            chbReleaseWaiver.Enabled = false;
            txbDiscipleshipMentorNotes.Enabled = false;
            chbTrained.Enabled = false;
            cmbUpdate.Text = "Update Student Information";
            ddlProgramEnrollment.Visible = true;
            ddlProgramEnrollment.Enabled = true;
        }

        protected void cmbViewWaitingList_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPage();
                cmbWaitListReport.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                cmbVolunteerReport.Enabled = false;
                con.Open();

                string sql = "Select dmp.studentlastname as 'LastName', dmp.studentfirstname as 'FirstName', si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1 as 'ParentGuardian', pg.workphone1 as 'WorkPhone', pg.cellphone1 as 'CellPhone', "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived as 'Covenant', dmp.discipleshipmentor as 'DiscipleshipMentor',dmp.optionsstaffcoordinator as 'StaffCoordinator', dmam.programenrollment as 'ProgramEnrollment', "
                           + "dmp.hasgraduated as 'Graduated', dmp.comment as 'Comments', dmp.lastupdatedby as 'LastUpdatedBy', dmam.covenantreceiveddate as 'Cov ReceiveDate', dmam.covenantsentdate as 'Cov SentDate', dmp.waitinglistinactive as 'WaitingList' "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                           + "ON (dmp.studentlastname = pg.studentlastname AND dmp.studentfirstname = pg.studentfirstname) "
                           + "WHERE dmp.waitinglistinactive = 1 "
                           + "GROUP BY dmp.studentlastname, dmp.studentfirstname, si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1, pg.workphone1, pg.cellphone1, "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived, dmp.discipleshipmentor,dmp.optionsstaffcoordinator, dmam.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, dmam.covenantreceiveddate, dmam.covenantsentdate, dmp.waitinglistinactive "
                           + "ORDER BY dmp.studentlastname, dmp.studentfirstname ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorProgram");
                gvViewAll.DataSource = ds.Tables[0];
                gvViewAll.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }

        protected void cmbViewEnrolledStudents_Click(object sender, EventArgs e)
        {
            try
            {
                //UpdateAllInformation();
                ClearPage();
                lblReportTitle.Text = "Active Mentee Report";
                lblReportTitle.Visible = true;
                cmbWaitListReport.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbExpandedMentorReport.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                cmbVolunteerReport.Enabled = false;
                cmbRecentUpdates.Enabled = false;
                con.Open();

                string sql = "Select dmp.studentlastname as 'LastName', dmp.studentfirstname as 'FirstName', si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1 as 'ParentGuardian', pg.workphone1 as 'WorkPhone', pg.cellphone1 as 'CellPhone', "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived as 'Covenant', dmam.discipleshipmentor as 'DiscipleshipMentor',dmp.optionsstaffcoordinator as 'StaffCoordinator', dmam.programenrollment as 'ProgramEnrollment', "
                           + "dmp.hasgraduated as 'Graduated', dmp.comment as 'Comments', dmp.lastupdatedby as 'LastUpdatedBy', dmam.covenantreceiveddate as 'Cov ReceiveDate', dmam.covenantsentdate as 'Cov SentDate', dmp.waitinglistinactive as 'WaitingList' "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                           + "ON (dmp.studentlastname = pg.studentlastname AND dmp.studentfirstname = pg.studentfirstname) "
                           + "LEFT OUTER JOIN DiscipleshipmentorAssignedMentors dmam "
                           + "ON (dmp.studentlastname = dmam.Studentlastname AND dmp.studentfirstname = dmam.studentfirstname) "
                           + "WHERE dmp.waitinglistinactive = 0 "
                           + "GROUP BY dmp.studentlastname, dmp.studentfirstname, si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1, pg.workphone1, pg.cellphone1, "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived, dmam.discipleshipmentor,dmp.optionsstaffcoordinator, dmam.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, dmam.covenantreceiveddate, dmam.covenantsentdate, dmp.waitinglistinactive "
                           + "ORDER BY dmp.studentlastname, dmp.studentfirstname ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorProgram");
                gvViewAll.DataSource = ds.Tables[0];
                gvViewAll.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            //UpdateAllInformation();//Ensure we don't loose any information...RCM.1/12/12.
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            //UpdateAllInformation();//Ensure we don't loose any information...RCM.1/12/12.
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            //UpdateAllInformation();
            gvViewAll.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            ExcelExport.ExportGridView(gvViewAll, Response);
        }

        protected void ddlDiscipleShipMentorAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDiscipleShipMentorAvailable.Text == "N/A")
            {
                lbVolunteerInformation.Enabled = false;
            }
            else
            {
                cmbAssignMentor.Enabled = true;
                
                if (ddlDiscipleShipMentorAvailable.Text.Contains(" ("))
                {
                    int whereat = ddlDiscipleShipMentorAvailable.Text.IndexOf(" (");
                    ddlDiscipleShipMentorAvailable.Items.Add(ddlDiscipleShipMentorAvailable.Text.Substring(0, whereat));
                    ddlDiscipleShipMentorAvailable.Text = ddlDiscipleShipMentorAvailable.Text.Substring(0, whereat);
                }
                else
                {
                    lbVolunteerInformation.Enabled = true;
                }
            }
        }

        protected void cmbVolunteerReport_Click(object sender, EventArgs e)
        {
            try
            {
                //UpdateAllInformation();
                ClearPage();
                lblReportTitle.Text = "Mentor Report";
                lblReportTitle.Visible = true;
                cmbWaitListReport.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                cmbExpandedMentorReport.Enabled = false;
                cmbVolunteerReport.Enabled = false;
                cmbRecentUpdates.Enabled = false;
                con.Open();

                string sql = "";
                sql = "Select vi.LastName, vi.FirstName, dmam.ProgramEnrollment as 'PrgrmAffiliation', (SELECT TOP 1 t2.sysupdate FROM Discipleshipmentordescription t2 where dmd.StudentLastName = t2.StudentLastName and dmd.StudentFirstName = t2.StudentFirstName ORDER BY t2.sysupdate DESC) as 'LastUpdate', dmam.StudentLastName + ',' + dmam.StudentFirstName as 'StudentsName', vi.DiscipleshipmentorStartDate as 'DateEstablished', vi.DiscipleshipmentorTraining as 'Trained', vi.DiscipleshipmentorNotes as 'MentorNotes' "
                           + "from VolunteerInformation vi "
                           + "LEFT OUTER JOIN DiscipleshipMentorAssignedMentors dmam "
                           + "ON (vi.FirstName + ' ' + vi.LastName = dmam.discipleshipmentor) "
                           + "LEFT OUTER JOIN DiscipleshipMentorDescription dmd "
                           + "ON (dmam.studentlastname = dmd.studentlastname AND dmam.studentfirstname = dmd.studentfirstname) "
                           + "where vi.DiscipleshipmentorParticipation = 1 "
                           + "GROUP BY vi.LastName, vi.FirstName, dmam.programenrollment, dmam.StudentLastName + ',' + dmam.StudentFirstName, vi.DiscipleshipmentorStartDate, vi.DiscipleshipmentorTraining, vi.DiscipleshipmentorNotes, dmd.StudentFirstName, dmd.StudentLastName "
                           + "ORDER BY vi.LastName, vi.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorProgram");
                gvViewAll.DataSource = ds.Tables[0];
                gvViewAll.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void cmbCancelAssignMentor_Click(object sender, EventArgs e)
        {
            //ddlDiscipleShipMentorAvailable.Text = "Please select a mentor.";

            ////cmbUpdateMentor.Visible = true;
            ////cmbUpdateMentor.Enabled = false;
            //cmbUpdateMentor.Style.Add("z-index", "1");

            ////cmbRemoveMentor.Visible = true;
            ////cmbRemoveMentor.Enabled = false;
            //cmbRemoveMentor.Style.Add("z-index", "1");

            ////cmbAssignMentor.Visible = true;
            ////cmbAssignMentor.Enabled = false;
            //cmbAssignMentor.Style.Add("z-index", "1");

            ////cmbClearMentors.Visible = true;
            ////cmbClearMentors.Enabled = false;
            //cmbRemoveMentor.Style.Add("z-index", "1");


            ////cmbRemoveMentor.Enabled = true;
            ////cmbClearMentors.Enabled = true;


            ////cmbCancelAssignMentor.Enabled = true;
            ////cmbCancelAssignMentor.Visible = true;
            //cmbCancelAssignMentor.Style.Add("z-index", "1");

            ////ddlDiscipleShipMentorAvailable.Visible = true;
            //ddlDiscipleShipMentorAvailable.Style.Add("z-index", "1");

            ////ddlDiscipleshipMentorOptions2.Visible = true;
            //ddlDiscipleshipMentorOptions2.Style.Add("z-index", "1");

            ////ddlDiscipleshipmentorAvailable3.Visible = true;
            ////ddlDiscipleshipmentorAvailable3.Style.Add("z-index", "99999");

            //lblAssignMenetor.Style.Add("z-index", "1");
            ////lblAssignMenetor.Visible = true;

            //pnlAssignMentor.Style.Add("z-index", "1");
            ////pnlAssignMentor.Visible = true;

            System.Threading.Thread.Sleep(1000);//Wait 1 seconds before disappearing..RCM.
            cmbAssignMentor.Visible = false;
            cmbCancelAssignMentor.Visible = false;
            cmbClearMentors.Visible = false;
            cmbRemoveMentor.Visible = false;
            cmbUpdateMentor.Visible = false;
            ddlDiscipleShipMentorAvailable.Visible = false;
            lblAssignMenetor.Visible = false;
            pnlAssignMentor.Visible = false;
        }

        protected void cmbAssignMentor_Click(object sender, EventArgs e)
        {
            lbVolunteerInformation.Text = "(Mentor Profile)";
            InsertNewlyAssignedMentors();
        }

        protected void RemoveCurrentMentor()
        {
            try
            {
                string sql_DeleteMentor = "";
                if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                {
                        sql_DeleteMentor = "Delete from UIF_PerformingArts.dbo.DiscipleshipMentorAssignedMentors "
                        //sql_DeleteMentor = "Delete from UIF_PerformingArts.dbo.DiscipleshipMentorProgram "
                                        + " where studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "' "
                                        + " and studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim() + "' "
                                        + " and discipleshipmentor = '" + ddlAssignedMentors.Text.Trim() + "' ";
                }
                else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                {
                        sql_DeleteMentor = "Delete from UIF_PerformingArts.dbo.DiscipleshipMentorAssignedMentors "
                        //sql_DeleteMentor = "Delete from UIF_PerformingArts.dbo.DiscipleshipMentorProgram "
                                        + " where studentlastname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "' "
                                        + " and studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim() + "' "
                                        + " and discipleshipmentor = '" + ddlAssignedMentors.Text.Trim() + "' ";
                }

                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteMentor = new SqlCommand(sql_DeleteMentor, con);
                if (sqlDeleteMentor.ExecuteNonQuery() > 0)
                {
                    ddlAssignedMentors.Items.Remove(ddlAssignedMentors.Text);//Remove the mentor from the dropdown list..RCM..1/12/12..
                    if (ddlAssignedMentors.Items.Count == 0)
                    {
                        ddlAssignedMentors.Items.Add("No mentor assigned.");
                        ddlAssignedMentors.Text = "No mentor assigned.";
                    }
                }
                else
                {

                }
                System.Threading.Thread.Sleep(1000);//Wait 1 seconds before disappearing..RCM.
                cmbAssignMentor.Visible = false;
                cmbCancelAssignMentor.Visible = false;
                cmbClearMentors.Visible = false;
                cmbRemoveMentor.Visible = false;
                cmbUpdateMentor.Visible = false;
                ddlDiscipleShipMentorAvailable.Visible = false;
                lblAssignMenetor.Visible = false;
                pnlAssignMentor.Visible = false;
                //UpdateAllInformation();
            }
            catch (Exception lkjlkj)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void RemoveAllMentors()
        {
            try
            {
                string sql_DeleteMentor = "";
                if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                {
                    sql_DeleteMentor = "Delete from UIF_PerformingArts.dbo.DiscipleshipMentorAssignedMentors "
                    //sql_DeleteMentor = "Delete from UIF_PerformingArts.dbo.DiscipleshipMentorProgram "
                                    + " where studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "' "
                                    + " and studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim() + "' ";
                }
                else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                {
                    sql_DeleteMentor = "Delete from UIF_PerformingArts.dbo.DiscipleshipMentorAssignedMentors "
                    //sql_DeleteMentor = "Delete from UIF_PerformingArts.dbo.DiscipleshipMentorProgram "
                                    + " where studentlastname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "' "
                                    + " and studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim() + "' ";
                }
                
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteMentors = new SqlCommand(sql_DeleteMentor, con);
                if (sqlDeleteMentors.ExecuteNonQuery() > 0)
                {
                    ddlAssignedMentors.Items.Clear();
                    ddlAssignedMentors.Items.Add("No mentor assigned.");
                    ddlAssignedMentors.Text = "No mentor assigned.";
                }
                else
                {

                }
                System.Threading.Thread.Sleep(1000);//Wait 1 seconds before disappearing..RCM.
                cmbAssignMentor.Visible = false;
                cmbCancelAssignMentor.Visible = false;
                cmbClearMentors.Visible = false;
                cmbRemoveMentor.Visible = false;
                cmbUpdateMentor.Visible = false;
                ddlDiscipleShipMentorAvailable.Visible = false;
                lblAssignMenetor.Visible = false;
                pnlAssignMentor.Visible = false;
                //UpdateAllInformation();
            }
            catch (Exception lkjlkj)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void InsertNewlyAssignedMentors()
        {
            string lastname = "";
            string firstname = "";
            string sqlInsertStatement = "";
            if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
            {
                lastname = ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim();
                firstname = ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim();
            }
            else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
            {
                lastname = ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim();
                firstname = ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim();
            }
            
            try
            {
                sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.DiscipleshipMentorAssignedMentors " +
                    "values ( "
                    + "'" + lastname + "',"
                    + "'" + firstname + "',"
                    + "'" + ddlDiscipleShipMentorAvailable.Text + "',"
                    + "'" + txbComments.Text.Trim() + "', "
                    + "'" + System.DateTime.Now.ToString() + "', "
                    + "'" + System.DateTime.Now.ToString() + "', "
                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                    + Convert.ToInt32(chbWaitingListInactive.Checked) + ", "
                    + "'" + ddlProgramEnrollment.Text.Trim() + "', "
                    + "'N/A', "
                    + "'N/A') ";
                    //+ "'" + ddlCovRecMonth.Text.Trim() + "-" + ddlCovRecDay.Text.Trim() + "-" + ddlCovRecYear.Text.Trim() + "' , "
                    //+ "'" + ddlCovSentMonth.Text.Trim() + "-" + ddlCovSentDay.Text.Trim() + "-" + ddlCovSentYear.Text.Trim() + "') ";
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                if (sqlInsertCommand.ExecuteNonQuery() > 0)
                {
                    ddlAssignedMentors.Items.Add(ddlDiscipleShipMentorAvailable.Text);
                    ddlAssignedMentors.Items.Remove("No mentor assigned.");
                    
                }
                else
                {
                    //display message that record was NOT updated
                    //	btnContinue.Visible = false;
                    //	lblAlert.Visible = true;
                    //	lblAlert.Text = "No update. Error has occurred.";
                }
                System.Threading.Thread.Sleep(1000);//Wait 1 seconds before disappearing..RCM.
                cmbAssignMentor.Visible = false;
                cmbCancelAssignMentor.Visible = false;
                cmbClearMentors.Visible = false;
                cmbRemoveMentor.Visible = false;
                cmbUpdateMentor.Visible = false;
                ddlDiscipleShipMentorAvailable.Visible = false;
                ddlDiscipleshipMentorOptions2.Visible = false;
                lblAssignMenetor.Visible = false;
                pnlAssignMentor.Visible = false;
                //UpdateAllInformation();
            }
            catch (Exception lkjlaa)
            {
                //lblInformation.Enabled = true;
                //lblInformation.Text = "There was an exception INSERTING NEW data into the StudentInformation table..  Please fix and try again MSG: " + lkjlaa.Message.ToString();
                //throw new Exception(lblInformation.Text);
            }
        }

        protected void lbAddMentor_Click(object sender, EventArgs e)
        {
            cmbUpdateMentor.Enabled = false;
            ddlDiscipleshipMentorOptions2.Enabled = false;

            if ((lbAddMentor.Text == "(Add/Update/Remove a mentor)") && (ddlAssignedMentors.Text == "No mentor assigned."))
            {
                //Assigning a Mentor..
                //Give them the option to pick from a drop down list of available mentors...RCM..1/10/12.

                ddlDiscipleShipMentorAvailable.Text = "Please select a mentor.";

                cmbUpdateMentor.Visible = true;
                cmbUpdateMentor.Enabled = false;
                cmbUpdateMentor.Style.Add("z-index", "9999999");

                cmbRemoveMentor.Visible = true;
                cmbRemoveMentor.Enabled = false;
                cmbRemoveMentor.Style.Add("z-index", "9999999");

                cmbAssignMentor.Visible = true;
                cmbAssignMentor.Enabled = false;
                cmbAssignMentor.Style.Add("z-index", "9999999");

                cmbClearMentors.Visible = true;
                cmbClearMentors.Enabled = false;
                cmbRemoveMentor.Style.Add("z-index", "9999999");


                cmbRemoveMentor.Enabled = true;
                cmbClearMentors.Enabled = true;


                cmbCancelAssignMentor.Enabled = true;
                cmbCancelAssignMentor.Visible = true;
                cmbCancelAssignMentor.Style.Add("z-index", "9999999");

                ddlDiscipleShipMentorAvailable.Visible = true;
                ddlDiscipleShipMentorAvailable.Style.Add("z-index", "9999999");

                ddlDiscipleshipMentorOptions2.Visible = true;
                ddlDiscipleshipMentorOptions2.Style.Add("z-index", "9999999");

                //ddlDiscipleshipmentorAvailable3.Visible = true;
                //ddlDiscipleshipmentorAvailable3.Style.Add("z-index", "99999");

                lbViewMentorProfile.Style.Add("z-index", "9999999");
                lbViewMentorProfile.Visible = true;

                lblAssignMenetor.Style.Add("z-index", "9999999");
                lblAssignMenetor.Visible = true;

                pnlAssignMentor.Style.Add("z-index", "999999");
                pnlAssignMentor.Visible = true;
            }
            else if ((lbAddMentor.Text == "(Add/Update/Remove a mentor)") && (ddlAssignedMentors.Text != "No mentor assigned."))
            {
                //Either Adding or Updating a mentor..RCM..1/10/12.

                //Hmm...update or insert.. how do I know which....

                ddlDiscipleShipMentorAvailable.Text = "Please select a mentor.";

                cmbUpdateMentor.Visible = true;
                cmbUpdateMentor.Enabled = false;
                cmbUpdateMentor.Style.Add("z-index", "9999999");

                cmbRemoveMentor.Visible = true;
                cmbRemoveMentor.Enabled = false;
                cmbRemoveMentor.Style.Add("z-index", "9999999");

                cmbClearMentors.Visible = true;
                cmbClearMentors.Enabled = false;
                cmbRemoveMentor.Style.Add("z-index", "9999999");

                cmbRemoveMentor.Enabled = true;
                cmbClearMentors.Enabled = true;

                cmbAssignMentor.Visible = true;
                cmbAssignMentor.Enabled = false;
                cmbAssignMentor.Style.Add("z-index", "9999999");

                cmbCancelAssignMentor.Enabled = true;
                cmbCancelAssignMentor.Visible = true;
                cmbCancelAssignMentor.Style.Add("z-index", "9999999");

                ddlDiscipleShipMentorAvailable.Visible = true;
                ddlDiscipleShipMentorAvailable.Style.Add("z-index", "9999999");

                ddlDiscipleshipMentorOptions2.Visible = true;
                ddlDiscipleshipMentorOptions2.Style.Add("z-index", "9999999");

                //ddlDiscipleshipmentorAvailable3.Visible = true;
                //ddlDiscipleshipmentorAvailable3.Style.Add("z-index", "99999");

                lbViewMentorProfile.Style.Add("z-index", "9999999");
                lbViewMentorProfile.Visible = true;

                lblAssignMenetor.Style.Add("z-index", "9999999");
                lblAssignMenetor.Visible = true;

                pnlAssignMentor.Style.Add("z-index", "999999");
                pnlAssignMentor.Visible = true;

            }
        }

        protected void cmbUpdateMentor_Click(object sender, EventArgs e)
        {
            ddlAssignedMentors.Text = ddlDiscipleshipMentorOptions2.Text;
            try
            {
                string sqlUpdateStatement = "";
                //ddlDiscipleShipMentorAvailable.Text = ddlDiscipleShipMentorAvailable.Text.Replace("'", "");
                //txbDiscipleshipMentor.Text = txbDiscipleshipMentor.Text.Replace("'", "");
                txbStaffCoordinator.Text = txbStaffCoordinator.Text.Replace("'", "");
                //txbProgramEnrollment.Text = txbProgramEnrollment.Text.Replace("'", "");
                txbComments.Text = txbComments.Text.Replace("'", "");

                if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                {
                    sqlUpdateStatement = " UPDATE DiscipleshipMentorProgram "
                    + "SET "
                    + " studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "' , "
                    + " studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim() + "' , "
                        //+ " discipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "' , "
                    //+ " discipleshipmentor = '" + ddlAssignedMentors.Text.Trim() + "', "
                    + " optionsstaffcoordinator = '" + txbStaffCoordinator.Text.Trim() + "' , "
                    + " programenrollment = '" + ddlProgramEnrollment.Text.Trim() + "' , "
                    + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                    + " covenantreceived = " + Convert.ToInt32(chbCovenantLetter.Checked) + ", "
                        //+ " covenantreceiveddate = '" + Convert.ToDateTime(txbCovenantReceivedDate.Text.Trim()) + "', "
                        //+ " covenantsentdate = '" + Convert.ToDateTime(txbCovenantSentDate.Text.Trim()) + "', "
                    + " covenanatreceiveddate = '" + txbCovenantReceivedDate.Text.Trim() + "', "
                    + " covenantsentdate = '" + txbCovenantSentDate.Text.Trim() + "', "
                    + " waitinglistinactive = " + Convert.ToInt32(chbWaitingListInactive.Checked) + ", "
                    + " comment = '" + txbComments.Text.Trim() + "' , "
                    + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                    + "  "
                    + " WHERE studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")) + "' "
                    + " AND studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1) + "' ";
                }
                else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                {
                    sqlUpdateStatement = " UPDATE DiscipleshipMentorProgram "
                    + "SET "
                    + " studentlastname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "' , "
                    + " studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim() + "' , "
                        //+ " discipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "' , "
                    //+ " discipleshipmentor = '" + ddlAssignedMentors.Text.Trim() + "', "
                    + " optionsstaffcoordinator = '" + txbStaffCoordinator.Text.Trim() + "' , "
                    + " programenrollment = '" + ddlProgramEnrollment.Text.Trim() + "' , "
                    + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                    + " covenantreceived = " + Convert.ToInt32(chbCovenantLetter.Checked) + ", "
                    //+ " covenantreceiveddate = '" + ddlCovRecMonth.Text.Trim() + "-" + ddlCovRecDay.Text.Trim() + "-" + ddlCovRecYear.Text.Trim() + "' , "
                    //+ " covenantsentdate = '" + ddlCovSentMonth.Text.Trim() + "-" + ddlCovSentDay.Text.Trim() + "-" + ddlCovSentYear.Text.Trim() + "' , "
                    + " covenanatreceiveddate = '" + txbCovenantReceivedDate.Text.Trim() + "', "
                    + " covenantsentdate = '" + txbCovenantSentDate.Text.Trim() + "', "
                    + " waitinglistinactive = " + Convert.ToInt32(chbWaitingListInactive.Checked) + ", "
                    + " comment = '" + txbComments.Text.Trim() + "' , "
                    + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                    + "  "
                    + " WHERE studentlastname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")) + "' "
                    + " AND studentfirstname = '" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1) + "' ";
                }

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
            catch (Exception lkjlka)
            {


            }
            System.Threading.Thread.Sleep(1000);//Wait 1 seconds before disappearing..RCM.
            cmbAssignMentor.Visible = false;
            cmbCancelAssignMentor.Visible = false;
            cmbClearMentors.Visible = false;
            cmbRemoveMentor.Visible = false;
            cmbUpdateMentor.Visible = false;
            ddlDiscipleShipMentorAvailable.Visible = false;
            ddlDiscipleshipMentorOptions2.Visible = false;
            lblAssignMenetor.Visible = false;
            pnlAssignMentor.Visible = false;
        }

        protected void cmbRemoveMentor_Click(object sender, EventArgs e)
        {
            RemoveCurrentMentor();
        }

        protected void ddlDiscipleshipMentorOptions2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUpdateMentor.Enabled = true;
        }

        protected void ddlDiscipleshipmentorAvailable3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRemoveMentor.Enabled = true;
        }

        protected void cmbClearMentors_Click(object sender, EventArgs e)
        {
            RemoveAllMentors();
        }

        protected void ddlVolunteerWaitingList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbWaitListReport_Click(object sender, EventArgs e)
        {
            try
            {
                //UpdateAllInformation();
                ClearPage();
                lblReportTitle.Text = "Mentee Waiting List Report";
                lblReportTitle.Visible = true;
                cmbWaitListReport.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbExpandedMentorReport.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                cmbVolunteerReport.Enabled = false;
                cmbRecentUpdates.Enabled = false;
                con.Open();

                string sql = "Select dmp.studentlastname as 'LastName', dmp.studentfirstname as 'FirstName', si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1 as 'ParentGuardian', pg.workphone1 as 'WorkPhone', pg.cellphone1 as 'CellPhone', "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived as 'Covenant', dmam.discipleshipmentor as 'DiscipleshipMentor',dmp.optionsstaffcoordinator as 'StaffCoordinator', dmam.programenrollment as 'ProgramEnrollment', "
                           + "dmp.hasgraduated as 'Graduated', dmp.comment as 'Comments', dmp.lastupdatedby as 'LastUpdatedBy', dmam.covenantreceiveddate as 'Cov ReceiveDate', dmam.covenantsentdate as 'Cov SentDate', dmp.waitinglistinactive as 'WaitingList' "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                           + "ON (dmp.studentlastname = pg.studentlastname AND dmp.studentfirstname = pg.studentfirstname) "
                           + "LEFT OUTER JOIN DiscipleshipmentorAssignedMentors dmam "
                           + "ON (dmp.studentlastname = dmam.Studentlastname AND dmp.studentfirstname = dmam.studentfirstname) "
                           + "WHERE dmp.waitinglistinactive = 1 "
                           + "GROUP BY dmp.studentlastname, dmp.studentfirstname, si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1, pg.workphone1, pg.cellphone1, "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived, dmam.discipleshipmentor,dmp.optionsstaffcoordinator, dmam.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, dmam.covenantreceiveddate, dmam.covenantsentdate, dmp.waitinglistinactive "
                           + "ORDER BY dmp.studentlastname, dmp.studentfirstname ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorProgram");
                gvViewAll.DataSource = ds.Tables[0];
                gvViewAll.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }

        protected void cmbRecentUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                //UpdateAllInformation();
                ClearPage();
                lblReportTitle.Text = "Most Recent Updates Report";
                lblReportTitle.Visible = true;
                cmbWaitListReport.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbExpandedMentorReport.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                cmbVolunteerReport.Enabled = false;
                //cmbVolunteerReport.Enabled = false;
                con.Open();

                string sql = "";
                sql = "select t1.StudentLastName, t1.StudentFirstName,dmp.DiscipleshipMentor,(SELECT TOP 1 t2.sysupdate FROM Discipleshipmentordescription t2 where t1.StudentLastName = t2.StudentLastName and t1.StudentFirstName = t2.StudentFirstName ORDER BY t2.sysupdate DESC) as 'LastUpdate' "
                    + "from DiscipleshipMentorDescription t1 "
                    + "LEFT OUTER JOIN DiscipleshipMentorAssignedMentors dmp "
                    + "ON (dmp.StudentLastName = t1.StudentLastName AND dmp.StudentFirstName = t1.StudentFirstName) "
                    + "LEFT OUTER JOIN DiscipleshipMentorProgram dm "
                    + "ON (t1.StudentLastName = dm.StudentLastName AND t1.StudentFirstName = dm.StudentFirstName) "
                    + "WHERE t1.StudentLastName <> '' "
                    + "AND dm.waitinglistinactive = 0 "
                    + "GROUP BY t1.StudentLastName, t1.StudentFirstName, dmp.DiscipleshipMentor ";

                //sql = "select t1.StudentLastName, t1.StudentFirstName,dmp.DiscipleshipMentor,(SELECT TOP 1 t2.syscreate FROM Discipleshipmentordescription t2 where t1.StudentLastName = t2.StudentLastName and t1.StudentFirstName = t2.StudentFirstName) as 'LastUpdate' "
                //    + "from DiscipleshipMentorDescription t1 "
                //    + "LEFT OUTER JOIN DiscipleshipMentorAssignedMentors dmp "
                //    + "ON (dmp.StudentLastName = t1.StudentLastName AND dmp.StudentFirstName = t1.StudentFirstName) "
                //    + "LEFT OUTER JOIN DiscipleshipMentorProgram dm "
                //    + "ON (t1.StudentLastName = dm.StudentLastName AND t1.StudentFirstName = dm.StudentFirstName) "
                //    + "WHERE t1.StudentLastName <> '' "
                //    + "AND dm.waitinglistinactive = 0 "
                //    + "GROUP BY t1.StudentLastName, t1.StudentFirstName, dmp.DiscipleshipMentor "
                //    + "ORDER BY t1.StudentLastName, t1.StudentFirstName, t2.syscreate desc ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorDescription");
                gvViewAll.DataSource = ds.Tables[0];
                gvViewAll.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjlaab)
            {
                string lkjlaaa = "";
            }
        }

        protected void lbViewMentorProfile_Click(object sender, EventArgs e)
        {
            if (ddlDiscipleShipMentorAvailable.Text != "Please select a mentor.")
            {
                Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&VolunteerLastName=" + ddlDiscipleShipMentorAvailable.Text.Substring(ddlDiscipleShipMentorAvailable.Text.IndexOf(" ") + 1, (ddlDiscipleShipMentorAvailable.Text.Length - ddlDiscipleShipMentorAvailable.Text.IndexOf(" ") - 1))
                                    + "&VolunteerFirstName=" + ddlDiscipleShipMentorAvailable.Text.Substring(0, ddlDiscipleShipMentorAvailable.Text.IndexOf(" ")));
            }
        }

        protected void RetrieveMentorInformation(string StudentLastName, string StudentFirstName)
        {
            SqlDataReader reader3 = null;

            try
            {
                con3.Open();
                string sql = "";                
                sql = "select vi.LastName, vi.FirstName, vi.pictureidentification, vi.Discipleshipmentorparticipation, "
                    + "vi.Discipleshipmentortraineddate, vi.discipleshipmentorstartdate, vi.discipleshipmentornotes, "
                    + "vi.discipleshipmentorpotentials, vi.backgroundcheckdate, dmam.programenrollment, vd.Backgroundcheck, vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, vi.discipleshipmentortraining, dmam.covenantsentdate, dmam.covenantreceiveddate "
                    + "from VolunteerInformation vi "
                    + "LEFT OUTER JOIN Discipleshipmentorassignedmentors dmam "
                    + "ON (vi.firstname + ' ' + vi.lastname = dmam.discipleshipmentor ) "
                    + "LEFT OUTER JOIN VolunteerDetails vd "
                    + "ON (vi.lastname = vd.lastname AND vi.firstname = vd.firstname) "
                    + "WHERE vi.lastname = '" + ddlAssignedMentors.Text.Substring(ddlAssignedMentors.Text.IndexOf(" ") + 1, ddlAssignedMentors.Text.Length - (ddlAssignedMentors.Text.IndexOf(" ") + 1)) + "' "
                    + "AND vi.firstname = '" + ddlAssignedMentors.Text.Substring(0, ddlAssignedMentors.Text.IndexOf(" ") + 1) + "' "
                    + "AND dmam.studentlastname = '" + StudentLastName.Trim() + "' "
                    + "AND dmam.studentfirstname = '" + StudentFirstName.Trim() + "' "
                    + "GROUP BY vi.LastName, vi.FirstName, vi.pictureidentification, vi.Discipleshipmentorparticipation, "
                    + "vi.Discipleshipmentortraineddate, vi.discipleshipmentorstartdate, vi.discipleshipmentornotes, "
                    + "vi.discipleshipmentorpotentials, vi.backgroundcheckdate, dmam.programenrollment, vd.Backgroundcheck, vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, vi.discipleshipmentortraining, dmam.covenantsentdate, dmam.covenantreceiveddate  ";

                SqlCommand cmd3 = new SqlCommand(sql);
                if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                {
                    cmd3.Parameters.Add(new SqlParameter("@studentlastname", ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim()));
                    cmd3.Parameters.Add(new SqlParameter("@studentfirstname", ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim()));
                }
                else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                {
                    cmd3.Parameters.Add(new SqlParameter("@studentlastname", ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim()));
                    cmd3.Parameters.Add(new SqlParameter("@studentfirstname", ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim()));
                }
                cmd3.Connection = con3;
                reader3 = cmd3.ExecuteReader();
                if (reader3.Read())
                {	//Retrieve the first record only
                    if (reader3.IsDBNull(0))
                    {

                    }
                    if (reader3.IsDBNull(1))
                    {
                        imgMentor.ImageUrl = "N/A";
                    }
                    else
                    {
                        //imgMentor.ImageUrl = reader.GetString(1);
                    }
                    if (reader3.IsDBNull(2))
                    {
                        imgMentor.ImageUrl = "N/A";
                    }
                    else
                    {
                        imgMentor.ImageUrl = reader3.GetString(2);
                        imgMentor.Visible = true;
                    }
                    if (reader3.IsDBNull(3))
                    {
                        //imgMentor.ImageUrl = "N/A";
                    }
                    else
                    {
                        //imgMentor.ImageUrl = reader.GetString(3);
                    }
                    if (reader3.IsDBNull(4))
                    {
                        //TrainedDate "N/A";
                    }
                    else
                    {
                        //TrainedDate = reader.GetString(4);
                    }
                    if (reader3.IsDBNull(5))
                    {
                        //imgMentor.ImageUrl = "N/A";
                    }
                    else
                    {
                        //imgMentor.ImageUrl = reader.GetString(5);
                    }
                    if (reader3.IsDBNull(6))
                    {
                        txbDiscipleshipMentorNotes.Text = "N/A";
                    }
                    else
                    {
                        txbDiscipleshipMentorNotes.Text = reader3.GetString(6);
                    }
                    if (reader3.IsDBNull(7))
                    {
                        chbVolunteerWaitingList.Checked = false;
                    }
                    else
                    {
                        chbVolunteerWaitingList.Checked = reader3.GetBoolean(7);
                    }
                    if (reader3.IsDBNull(8))
                    {
                        //chbVolunteerWaitingList.Checked = false;
                    }
                    else
                    {
                        //chbVolunteerWaitingList.Checked = reader3.GetBoolean(8);
                    }
                    if (reader3.IsDBNull(9))
                    {
                        ddlProgramEnrollment.Text = "N/A";
                    }
                    else
                    {
                        ddlProgramEnrollment.Text = reader3.GetString(9);
                    }
                    if (reader3.IsDBNull(10))
                    {
                        chbBackGroundCheck.Checked = false;
                    }
                    else
                    {
                        chbBackGroundCheck.Checked = reader3.GetBoolean(10);
                    }
                    if (reader3.IsDBNull(11))
                    {
                        chbSpiritualJourney.Checked = false;
                    }
                    else
                    {
                        chbSpiritualJourney.Checked = reader3.GetBoolean(11);
                    }
                    if (reader3.IsDBNull(12))
                    {
                        chbVehichleInsurance.Checked = false;
                    }
                    else
                    {
                        chbVehichleInsurance.Checked = reader3.GetBoolean(12);
                    }
                    if (reader3.IsDBNull(13))
                    {
                        chbReleaseWaiver.Checked = false;
                    }
                    else
                    {
                        chbReleaseWaiver.Checked = reader3.GetBoolean(13);
                    }
                    if (reader3.IsDBNull(14))
                    {
                        chbGeneralInformation.Checked = false;
                    }
                    else
                    {
                        chbGeneralInformation.Checked = reader3.GetBoolean(14);
                    }
                    if (reader3.IsDBNull(15))
                    {
                        chbTrained.Checked = false;
                    }
                    else
                    {
                        chbTrained.Checked = reader3.GetBoolean(15);
                    }
                    if (reader3.IsDBNull(16))
                    {
                        txbCovenantSentDate.Text = "N/A";
                    }
                    else
                    {
                        txbCovenantSentDate.Text = reader3.GetSqlValue(16).ToString();
                    }
                    if (reader3.IsDBNull(17))
                    {
                        txbCovenantReceivedDate.Text = "N/A";
                    }
                    else
                    {
                        txbCovenantReceivedDate.Text = reader3.GetSqlValue(17).ToString();
                    }
                }
                //SqlDataAdapter da = new SqlDataAdapter(sql, con);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "DiscipleshipmentorDescription");
                ////gvViewAll.DataSource = ds.Tables[0];
                ////gvViewAll.DataBind();
                //con.Close();
                //cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjlaab)
            {
                string lkjlaaa = "";
            }
            finally
            {
                con3.Close();
            }
        }

        protected void RetrieveMentorInformationForMaintenance()
        {
            SqlDataReader reader3 = null;

            try
            {
                con3.Open();
                string sql = "";
                sql = "select vi.LastName, vi.FirstName, vi.pictureidentification, vi.Discipleshipmentorparticipation, "
                    + "vi.Discipleshipmentortraineddate, vi.discipleshipmentorstartdate, vi.discipleshipmentornotes, "
                    + "vi.discipleshipmentorpotentials, vi.backgroundcheckdate, dmam.programenrollment, vd.Backgroundcheck, vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, vi.discipleshipmentortraining  "
                    + "from VolunteerInformation vi "
                    + "LEFT OUTER JOIN Discipleshipmentorassignedmentors dmam "
                    + "ON (vi.firstname + ' ' + vi.lastname = dmam.discipleshipmentor ) "
                    + "LEFT OUTER JOIN VolunteerDetails vd "
                    + "ON (vi.firstname = vd.firstname AND vi.lastname = vd.lastname ) "
                    + "WHERE vi.lastname=@volunteerlastname "
                    + "AND vi.firstname=@volunteerfirstname "
                    + "GROUP BY vi.LastName, vi.FirstName, vi.pictureidentification, vi.Discipleshipmentorparticipation, "
                    + "vi.Discipleshipmentortraineddate, vi.discipleshipmentorstartdate, vi.discipleshipmentornotes, "
                    + "vi.discipleshipmentorpotentials, vi.backgroundcheckdate, dmam.programenrollment, vd.Backgroundcheck, vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, vi.discipleshipmentortraining  ";

                SqlCommand cmd3 = new SqlCommand(sql);
                if ((ddlVolunteerActiveMentees.Text == "Please select a mentor") && (ddlVolunteerWaitingList.Text != "Please select a mentor"))
                {
                    cmd3.Parameters.Add(new SqlParameter("@volunteerlastname", ddlVolunteerWaitingList.SelectedValue.Substring(0, ddlVolunteerWaitingList.SelectedValue.IndexOf(",")).Trim()));
                    cmd3.Parameters.Add(new SqlParameter("@volunteerfirstname", ddlVolunteerWaitingList.SelectedValue.Substring(ddlVolunteerWaitingList.SelectedValue.IndexOf(",") + 1).Trim()));
                    ddlAssignedMentors.Items.Add(ddlVolunteerWaitingList.Text);
                    ddlAssignedMentors.Text = ddlVolunteerWaitingList.Text;//Set the dropdown list to the name being maintenanced...RCM..
                }
                else if ((ddlVolunteerWaitingList.Text == "Please select a mentor") && (ddlVolunteerActiveMentees.Text != "Please select a mentor"))
                {
                    cmd3.Parameters.Add(new SqlParameter("@volunteerlastname", ddlVolunteerActiveMentees.SelectedValue.Substring(0, ddlVolunteerActiveMentees.SelectedValue.IndexOf(",")).Trim()));
                    cmd3.Parameters.Add(new SqlParameter("@volunteerfirstname", ddlVolunteerActiveMentees.SelectedValue.Substring(ddlVolunteerActiveMentees.SelectedValue.IndexOf(",") + 1).Trim()));
                    ddlAssignedMentors.Items.Add(ddlVolunteerActiveMentees.Text);
                    ddlAssignedMentors.Text = ddlVolunteerActiveMentees.Text;//Set the dropdown list to the name being maintenanced...RCM..
                }
                cmd3.Connection = con3;
                reader3 = cmd3.ExecuteReader();
                if (reader3.Read())
                {	//Retrieve the first record only
                    if (reader3.IsDBNull(0))
                    {

                    }
                    if (reader3.IsDBNull(1))
                    {
                        imgMentor.ImageUrl = "N/A";
                    }
                    else
                    {
                        //imgMentor.ImageUrl = reader.GetString(1);
                    }
                    if (reader3.IsDBNull(2))
                    {
                        imgMentor.ImageUrl = "N/A";
                    }
                    else
                    {
                        imgMentor.ImageUrl = reader3.GetString(2);
                        imgMentor.Visible = true;
                    }
                    if (reader3.IsDBNull(3))
                    {
                        //imgMentor.ImageUrl = "N/A";
                    }
                    else
                    {
                        //imgMentor.ImageUrl = reader.GetString(3);
                    }
                    if (reader3.IsDBNull(4))
                    {
                        //TrainedDate "N/A";
                    }
                    else
                    {
                        //TrainedDate = reader.GetString(4);
                    }
                    if (reader3.IsDBNull(5))
                    {
                        //imgMentor.ImageUrl = "N/A";
                    }
                    else
                    {
                        //imgMentor.ImageUrl = reader.GetString(5);
                    }
                    if (reader3.IsDBNull(6))
                    {
                        txbDiscipleshipMentorNotes.Text = "N/A";
                    }
                    else
                    {
                        txbDiscipleshipMentorNotes.Text = reader3.GetString(6);
                    }
                    if (reader3.IsDBNull(7))
                    {
                        chbVolunteerWaitingList.Checked = false;
                    }
                    else
                    {
                        chbVolunteerWaitingList.Checked = reader3.GetBoolean(7);
                    }
                    if (reader3.IsDBNull(8))
                    {
                        //chbVolunteerWaitingList.Checked = false;
                    }
                    else
                    {
                        //chbVolunteerWaitingList.Checked = reader3.GetBoolean(8);
                    }
                    if (reader3.IsDBNull(9))
                    {
                        ddlProgramEnrollment.Text = "N/A";
                    }
                    else
                    {
                        ddlProgramEnrollment.Text = reader3.GetString(9);
                    }
                    if (reader3.IsDBNull(10))
                    {
                        chbBackGroundCheck.Checked = false;
                    }
                    else
                    {
                        chbBackGroundCheck.Checked = reader3.GetBoolean(10);
                    }
                    if (reader3.IsDBNull(11))
                    {
                        chbSpiritualJourney.Checked = false;
                    }
                    else
                    {
                        chbSpiritualJourney.Checked = reader3.GetBoolean(11);
                    }
                    if (reader3.IsDBNull(12))
                    {
                        chbVehichleInsurance.Checked = false;
                    }
                    else
                    {
                        chbVehichleInsurance.Checked = reader3.GetBoolean(12);
                    }
                    if (reader3.IsDBNull(13))
                    {
                        chbReleaseWaiver.Checked = false;
                    }
                    else
                    {
                        chbReleaseWaiver.Checked = reader3.GetBoolean(13);
                    }
                    if (reader3.IsDBNull(14))
                    {
                        chbGeneralInformation.Checked = false;
                    }
                    else
                    {
                        chbGeneralInformation.Checked = reader3.GetBoolean(14);
                    }
                    if (reader3.IsDBNull(15))
                    {
                        chbTrained.Checked = false;
                    }
                    else
                    {
                        chbTrained.Checked = reader3.GetBoolean(15);
                    }
                }
            }
            catch (Exception lkjlaab)
            {
                string lkjlaaa = "";
            }
            finally
            {
                con3.Close();
            }
        }


        protected void ddlAssignedMentors_SelectedIndexChanged(object sender, EventArgs e)
        {
            //imgMentor.ImageUrl = "~/VolunteerPictures/" + ddlAssignedMentors.Text.Replace(" ","").ToString() + ".jpg";
            //imgMentor.Visible = true;

            if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
            {
                RetrieveMentorInformation(ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim(),ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim());
            }
            else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
            {
                RetrieveMentorInformation(ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim(),ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim());
            }
        }

        protected void cmbEditVolunteerAll_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPage();
                lblReportTitle.Text = "Edit All Mentors Report";
                lblReportTitle.Visible = true;
                cmbWaitListReport.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                cmbVolunteerReport.Enabled = false;
                cmbRecentUpdates.Enabled = false;
                con.Open();

                con.Open();//Opens the db connection.
                string sql_LoadGrid = "";

                sql_LoadGrid = "Select vi.LastName, vi.FirstName "
                //dmam.StudentLastName "
                    //+ ',' + dmam.StudentFirstName as 'StudentsName', vi.DiscipleshipmentorStartDate as 'DateEstablished', vi.DiscipleshipmentorTraining as 'Trained', vi.DiscipleshipmentorNotes as 'MentorNotes' "
                           + "from VolunteerInformation vi "
                    //+ "LEFT OUTER JOIN DiscipleshipMentorProgram dmp "
                    //+ "ON (vi.FirstName + ' ' + vi.LastName = dmp.discipleshipmentor) "
                    //+ "LEFT OUTER JOIN DiscipleshipMentorAssignedMentors dmam "
                    //+ "ON (vi.FirstName + ' ' + vi.LastName = dmam.discipleshipmentor) "
                           + "where vi.DiscipleshipmentorParticipation = 1 ";
                           //+ "GROUP BY vi.LastName, vi.FirstName, dmam.StudentLastName + ',' + dmam.StudentFirstName, vi.DiscipleshipmentorStartDate, vi.DiscipleshipmentorTraining, vi.DiscipleshipmentorNotes  "
                           //+ "ORDER BY vi.LastName, vi.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerInformation");
                gvEditVolunteersAll.DataSource = ds.Tables[0];
                gvEditVolunteersAll.DataBind();
                con.Close();
            }
            catch (Exception lkjlkaaa)
            {
                
            }
        }


        protected void gvEditVolunteersAll_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvEditVolunteersAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvEditVolunteersAll.SelectedRow;
            irowNum = gvEditVolunteersAll.SelectedIndex;
            Bind3();
        }


        protected void gvEditVolunteersAll_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvEditVolunteersAll.Rows[e.NewSelectedIndex];
        }

        public void Bind3()
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', sizelimit as 'SizeLimit', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                         + "FROM PerformingArtsAcademyAvailableClasses  order by classname";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "VolunteerInformation");
            gvEditVolunteersAll.DataSource = ds.Tables[0];
            gvEditVolunteersAll.DataBind();
            con.Close();
        }

        protected void gvEditVolunteersAll_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEditVolunteersAll.EditIndex = e.NewEditIndex;
            bind();
        }

        protected void gvEditVolunteersAll_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //gvStudentList.DeleteRow(e.RowIndex);
            gvEditVolunteersAll.DataBind();
        }

        protected void gvEditVolunteersAll_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEditVolunteersAll.EditIndex = -1;
            bind();
        }

        protected void gvEditVolunteersAll_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvEditVolunteersAll.Rows[e.RowIndex];
            TextBox classname = (TextBox)row.FindControl("textbox1");
            TextBox classmeettime = (TextBox)row.FindControl("textbox2");
            TextBox classmeetday = (TextBox)row.FindControl("textbox3");
            TextBox classlocation = (TextBox)row.FindControl("textbox4");
            TextBox comments = (TextBox)row.FindControl("textbox5");
            TextBox instructor = (TextBox)row.FindControl("textbox6");
            TextBox devotionalleader = (TextBox)row.FindControl("textbox7");
            TextBox ID = (TextBox)row.FindControl("textbox8");
            TextBox sizelimit = (TextBox)row.FindControl("textbox20");

            classname.Text = classname.Text.Replace("'", "");
            classmeettime.Text = classmeettime.Text.Replace("'", "");
            classmeetday.Text = classmeetday.Text.Replace("'", "");
            classlocation.Text = classlocation.Text.Replace("'", "");
            comments.Text = comments.Text.Replace("'", "");
            instructor.Text = instructor.Text.Replace("'", "");
            devotionalleader.Text = devotionalleader.Text.Replace("'", "");
            sizelimit.Text = sizelimit.Text.Replace("'", "");

            gvEditVolunteersAll.EditIndex = -1;
            con.Open();
            SqlCommand cmd = new SqlCommand("Update VolunteerInformation set "
                                          + "  instructor='" + instructor.Text
                                          + "' , devotionalleader='" + devotionalleader.Text
                                          + "' , classname='" + classname.Text
                                          + "' , meettime='" + classmeettime.Text
                                          + "' , meetday='" + classmeetday.Text
                                          + "' , location='" + classlocation.Text
                                          + "' , sizelimit=" + sizelimit.Text
                                          + "  , comments='" + comments.Text
                                          + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                          + " where ID = '" + ID.Text + "' ");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            bind();
        }

        protected void lbStudentInfo2_Click(object sender, EventArgs e)
        {
            //UpdateAllInformation();
            if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim());
            }
            else
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim());
            }
        }

        protected void lbStudentInfo_Click(object sender, EventArgs e)
        {
            //UpdateAllInformation();
            if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim());
            }
            else
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim());
            }
        }

        protected void cmbExpandedMentorReport_Click(object sender, EventArgs e)
        {
            try
            {
                //UpdateAllInformation();
                ClearPage();
                lblReportTitle.Text = "Expanded Mentor Report";
                lblReportTitle.Visible = true;
                cmbWaitListReport.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                cmbVolunteerReport.Enabled = false;
                cmbRecentUpdates.Enabled = false;
                con.Open();

                string sql = "";
                sql = "Select vi.LastName, vi.FirstName, dmam.ProgramEnrollment as 'PrgrmAffiliation', (SELECT TOP 1 t2.sysupdate FROM Discipleshipmentordescription t2 where dmd.StudentLastName = t2.StudentLastName and dmd.StudentFirstName = t2.StudentFirstName ORDER BY t2.sysupdate DESC) as 'LastUpdate', dmam.StudentLastName + ',' + dmam.StudentFirstName as 'StudentsName', vi.DiscipleshipmentorStartDate as 'DateEstablished', vi.DiscipleshipmentorTraining as 'Trained', "
                           + "vi.discipleshipmentorpotentials as 'WaitList', "
                           + "vd.GeneralInformation as 'GenInfo', vd.SpiritualJourney as 'Testimony', vd.ReleaseWaiver as 'Waiver', vd.VehichleInsurance as 'Transport', CONVERT(VARCHAR(10),vd.NationalCheckDate,111) as 'National', CONVERT(VARCHAR(10),vd.dmvcheckdate,111) as 'DMV', CONVERT(VARCHAR(10),vd.pacriminalcheckdate,111) as 'PACrim', "
                           //+ "vd.Backgroundcheck, vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, "
                           + "vi.discipleshipmentortraineddate, "
                           + "dmam.covenantsentdate, dmam.covenantreceiveddate "
                           + "from VolunteerInformation vi "
                           //+ "LEFT OUTER JOIN DiscipleshipMentorProgram dmp "
                           //+ "ON (vi.FirstName + ' ' + vi.LastName = dmp.discipleshipmentor) "
                           + "LEFT OUTER JOIN DiscipleshipMentorAssignedMentors dmam "
                           + "ON (vi.FirstName + ' ' + vi.LastName = dmam.discipleshipmentor) "
                           + "LEFT OUTER JOIN DiscipleshipMentorDescription dmd "
                           + "ON (dmam.studentlastname = dmd.studentlastname AND dmam.studentfirstname = dmd.studentfirstname) "
                           + "LEFT OUTER JOIN VolunteerDetails vd "
                           + "ON (vi.lastname = vd.lastname AND vi.firstname = vd.firstname) "
                           + "WHERE vi.DiscipleshipmentorParticipation = 1 "
                           + "GROUP BY vi.LastName, vi.FirstName, dmam.ProgramEnrollment, dmam.StudentLastName + ',' + dmam.StudentFirstName, vi.DiscipleshipmentorStartDate, vi.DiscipleshipmentorTraining, "
                           + "vi.discipleshipmentorpotentials, "
                           + "vd.GeneralInformation, vd.SpiritualJourney, vd.ReleaseWaiver, vd.VehichleInsurance, vd.NationalCheckDate, vd.DMVCheckDate, vd.PACriminalCheckDate, "                           
                           //+ "vd.Backgroundcheck, vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, 
                           + "vi.discipleshipmentortraineddate, dmam.covenantsentdate, dmam.covenantreceiveddate, dmd.StudentFirstName, dmd.StudentLastName "
                           + "ORDER BY vi.LastName, vi.FirstName ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorProgram");
                gvViewAll.DataSource = ds.Tables[0];
                gvViewAll.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void ddlVolunteerWaitingList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //UpdateAllInformation();
            ddlVolunteerActiveMentees.Text = "Please select a mentor";

            lblCovenantReceivedDate.Visible = true;
            lblCovenantSentDate.Visible = true;
            cmbUpdate.Text = "Update Mentor Maintenance";
            
            ClearValues();
            RetrieveMentorInformationForMaintenance();
            DisplayHeaderFields();

            txbComments.Enabled = false;
            chbWaitingListInactive.Enabled = false;
            //lbAddNewEntry.Enabled = false;
            lbStudentInfo.Enabled = false;
            lbStudentInfo2.Enabled = false;
            gvStudentHistory.Visible = false;
            imgImage.ImageUrl = "";
            Panel5.Enabled = false;
            txbComments.Visible = false;
            chbWaitingListInactive.Visible = false;
            lbStudentInfo.Visible = false;
            lbStudentInfo2.Visible = false;
            imgImage.Visible = false;
            Panel5.Visible = false;
            lblComments.Visible = false;
            lbAddNewEntry.Visible = false;
            lblNotes.Visible = false;
            ddlDiscipleshipMentor.Text = "Please select a student";
            ddlWaitingListInactiveStudents.Text = "Please select a student";
            lbAddMentor.Enabled = false;
            ddlAssignedMentors.Enabled = false;
            ddlDiscipleShipMentorAvailable.Enabled = false;
                        
            ddlCovSentYear.Enabled = false;
            ddlCovSentMonth.Enabled = false;
            ddlCovSentDay.Enabled = false;
            ddlCovRecYear.Enabled = false;
            ddlCovRecMonth.Enabled = false;
            ddlCovRecDay.Enabled = false;

            txbCovenantReceivedDate.Enabled = false;
            txbCovenantSentDate.Enabled = false;
            imbCalCovenantSentDate.Enabled = false;
            imbCalCovenantReceiveDate.Enabled = false;
            txbCovenantSentDate.Text = "Not Applicable";
            txbCovenantReceivedDate.Text = "Not Applicable";

            lbVolunteerDetails.Enabled = true;
            chbBackGroundCheck.Enabled = true;
            chbGeneralInformation.Enabled = true;
            chbSpiritualJourney.Enabled = true;
            chbVehichleInsurance.Enabled = true;
            chbReleaseWaiver.Enabled = true;
            txbDiscipleshipMentorNotes.Enabled = true;
            chbTrained.Enabled = true;
            cmbUpdate.Enabled = true;
            lbAddMentor.Enabled = false;
        }

        protected void ddlVolunteerActiveMentees_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateAllInformation();
            
            ddlVolunteerWaitingList.Text = "Please select a mentor";
            
            ddlDiscipleShipMentorAvailable.Enabled = false;
            cmbUpdate.Text = "Update Mentor Maintenance";
            
            ClearValues();
            RetrieveMentorInformationForMaintenance();
            DisplayHeaderFields();

            txbComments.Enabled = false;
            chbWaitingListInactive.Enabled = false;
            //lbAddNewEntry.Enabled = false;
            lbStudentInfo.Enabled = false;
            lbStudentInfo2.Enabled = false;
            gvStudentHistory.Visible = false;
            imgImage.ImageUrl = "";
            Panel5.Enabled = false;
            lblCovenantReceivedDate.Visible = true;
            lblCovenantSentDate.Visible = true;
            txbComments.Visible = false;
            chbWaitingListInactive.Visible = false;
            lbStudentInfo.Visible = false;
            lbStudentInfo2.Visible = false;
            imgImage.Visible = false;
            Panel5.Visible = false;
            lblComments.Visible = false;
            lbAddNewEntry.Visible = false;
            lblNotes.Visible = false;
            ddlDiscipleshipMentor.Text = "Please select a student";
            ddlWaitingListInactiveStudents.Text = "Please select a student";
            lbAddMentor.Enabled = false;
            ddlAssignedMentors.Enabled = false;            
            
            ddlCovSentYear.Enabled = false;
            ddlCovSentMonth.Enabled = false;
            ddlCovSentDay.Enabled = false;
            ddlCovRecYear.Enabled = false;
            ddlCovRecMonth.Enabled = false;
            ddlCovRecDay.Enabled = false;

            txbCovenantReceivedDate.Enabled = false;
            txbCovenantSentDate.Enabled = false;
            imbCalCovenantSentDate.Enabled = false;
            imbCalCovenantReceiveDate.Enabled = false;
            txbCovenantSentDate.Text = "Not Applicable";
            txbCovenantReceivedDate.Text = "Not Applicable";

            lbVolunteerDetails.Enabled = true;
            chbBackGroundCheck.Enabled = true;
            chbGeneralInformation.Enabled = true;
            chbSpiritualJourney.Enabled = true;
            chbVehichleInsurance.Enabled = true;
            chbReleaseWaiver.Enabled = true;
            txbDiscipleshipMentorNotes.Enabled = true;
            chbTrained.Enabled = true;
            cmbUpdate.Enabled = true;
            lbAddMentor.Enabled = false;
        }

        protected void chbWaitingListInactive_CheckedChanged(object sender, EventArgs e)
        {
            //UpdateAllInformation();
        }

        protected void imbCalCovenantSentDate_Click(object sender, ImageClickEventArgs e)
        {
            calCovenantSentDate.Visible = true;
            calCovenantSentDate.Enabled = true;
        }

        protected void imbCalCovenantReceiveDate_Click(object sender, ImageClickEventArgs e)
        {
            calCovenantReceiveDate.Visible = true;
            calCovenantReceiveDate.Enabled = true;
        }

        protected void calCovenantReceiveDate_SelectionChanged(object sender, EventArgs e)
        {
            txbCovenantReceivedDate.Text = calCovenantReceiveDate.SelectedDate.ToString("yyyy-MM-dd");
            calCovenantReceiveDate.Visible = false;
        }

        protected void calCovenantSentDate_SelectionChanged(object sender, EventArgs e)
        {
            txbCovenantSentDate.Text = calCovenantSentDate.SelectedDate.ToString("yyyy-MM-dd");
            calCovenantSentDate.Visible = false;
        }

        protected void imbCalCovenantSentDate_Click1(object sender, ImageClickEventArgs e)
        {
            calCovenantSentDate.Visible = true;
            calCovenantSentDate.Style.Add("z-index", "999999999");
            calCovenantSentDate.Enabled = true;
        }

        protected void imbCalCovenantReceiveDate_Click1(object sender, ImageClickEventArgs e)
        {
            calCovenantReceiveDate.Visible = true;
            calCovenantReceiveDate.Style.Add("z-index", "999999999");
            calCovenantReceiveDate.Enabled = true;
        }

        protected void calCovenantSentDate_SelectionChanged1(object sender, EventArgs e)
        {
            txbCovenantSentDate.Text = calCovenantSentDate.SelectedDate.ToString("yyyy-MM-dd");
            calCovenantSentDate.Visible = false;
        }

        protected void calCovenantReceiveDate_SelectionChanged1(object sender, EventArgs e)
        {
            txbCovenantReceivedDate.Text = calCovenantReceiveDate.SelectedDate.ToString("yyyy-MM-dd");
            calCovenantReceiveDate.Visible = false;
        }

        protected void lbVolunteerDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("VolunteerDetails.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&VolunteerLastName=" + ddlAssignedMentors.Text.Substring(0, ddlAssignedMentors.Text.IndexOf(",")) + "&VolunteerFirstName=" + ddlAssignedMentors.Text.Substring(ddlAssignedMentors.Text.IndexOf(",") + 1, (ddlAssignedMentors.Text.Length - ddlAssignedMentors.Text.IndexOf(",") - 1)));
        }

        protected void txbCovenantReceivedDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}