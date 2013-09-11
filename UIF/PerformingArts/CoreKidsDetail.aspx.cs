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
    public partial class CoreKidsDetail : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
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

                //Check for a login... If not, re-direct them to the mainmenu page..RCM..
                if (Request.QueryString["security"] == "Good")
                {
                    //if (((Request.QueryString["LastName"] == "Kreider") && (Request.QueryString["FirstName"] == "Tom")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Tammy")) || ((Request.QueryString["LastName"] == "Braunersrither") && (Request.QueryString["FirstName"] == "Chad")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")))
                    //{
                        if ((Request.QueryString["StudentLastName"] != "") && (Request.QueryString["StudentFirstName"] != ""))
                        {
                            //string sql = "select spa.LastName, spa.FirstName, spa.Program as 'ProgramName', AVG(CAST(spa.Attended as FLOAT)) * 100 as 'Attend %', COUNT(spa.Day) as '# of entries', spa.ProgramSeason "
                            string sql = "select spa.Program as 'ProgramName', LEFT(AVG(CAST(spa.Attended as FLOAT)) * 100, 5) as 'Attend %', COUNT(spa.Day) as '# of entries', spa.ProgramSeason "
                                //, si.HaveReceivedChrist as 'RSVPGospel', si.Discipleshipmentorprogram as 'DiscMentorActive', '0' as 'StudentStaffAssistant' "
                                       + "from StudentProgramAttendance spa "
                                       + "LEFT OUTER JOIN StudentInformation si "
                                       + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName) "
                                       //+ "LEFT OUTER JOIN DiscipleshipMentorProgram dmp "
                                       //+ "ON (spa.LastName = dmp.StudentLastName AND spa.FirstName = dmp.StudentFirstName) "
                                       + "where spa.LastName = '" + Request.QueryString["StudentLastName"] + "' "
                                       + "and spa.FirstName = '" + Request.QueryString["StudentFirstName"] + "' "
                                       + "group by spa.Program, spa.ProgramSeason "
                                       //, si.HaveReceivedChrist, si.Discipleshipmentorprogram "
                                       + "order by spa.ProgramSeason, LEFT(AVG(CAST(spa.Attended as FLOAT)) * 100,5) desc,COUNT(day) desc, spa.Program ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql);

                            SqlDataAdapter da = new SqlDataAdapter(sql, con);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "UIF_PerformingArts.dbo.StudentProgramAttendance");

                            //Clear the gridview..RCM.
                            gvCoreKidsDetail.DataSource = null;
                            gvCoreKidsDetail.DataBind();

                            gvCoreKidsDetail.DataSource = ds.Tables[0];
                            gvCoreKidsDetail.DataBind();

                            lblReportCard.Text = lblReportCard.Text + Request.QueryString["StudentFirstName"] + " " + Request.QueryString["StudentLastName"];
                            lblReportCard.Visible = true;

                            //Handles the checkboxes for the other 3 criteria...RCM..9/18/12.
                            CheckRSVPGospelTracking();
                            CheckDiscipleshipMentorProgram();
                            CheckStudentVolunteerTracking();

                            try
                            {
                                string selectpicture = "Select si.pictureidentification "
                                                     + "from StudentInformation si "
                                                     + "LEFT OUTER JOIN DiscipleshipMentorProgram dmp "
                                                     + "ON (si.lastname = dmp.studentlastname AND si.firstname = dmp.studentfirstname) "
                                                     + "WHERE si.lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                                     + "AND si.firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                                     + "GROUP BY si.pictureidentification ";

                                SqlCommand cmd4 = new SqlCommand(selectpicture, con);
                                cmd4.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                                SqlDataAdapter custDA4 = new SqlDataAdapter();
                                custDA4.SelectCommand = cmd4;
                                DataSet custDS4 = new DataSet();
                                custDA4.Fill(custDS4, "StudentInformation");

                                foreach (DataRow myDataRowPO2 in custDS4.Tables["StudentInformation"].Rows)
                                {
                                    if (myDataRowPO2.IsNull(0))
                                    {
                                        imgImage.ImageUrl = "N/A";
                                    }
                                    else
                                    {
                                        imgImage.Visible = true;
                                        imgImage.ImageUrl = myDataRowPO2[0].ToString();
                                    }
                                }
                            }
                            catch (Exception lkjlk)
                            {

                            }
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
                            
                            
                            DisplayTheGrid();

                            //hmm.. this might be an issue.. need to know if they are on waitinglist or not.
                            ddlDiscipleshipMentor.Items.Add(Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"]);
                            ddlDiscipleshipMentor.Text = Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"];
                            lblNotes.Visible = true;
                            lbAddNewEntry.Visible = true;
                            cmbUpdate.Enabled = true;
                            cmbBackToStudentPage.Enabled = true;
                            chbCovenantLetter.Enabled = true;
                        }
                    //}
                    //else
                    //{
                        //Ryan C Manners..1/5/11
                        //Do NOT ALLOW ACCESS TO THE PAGE!
                        //Response.Redirect("ErrorAccess.aspx");
                    //    Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                    //}
                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!
                    Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                }
            }
        }


        protected void CheckRSVPGospelTracking()
        {
            try
            {
                con.Open();

                string selectSQL = "select RSVPGospel "
                                 + "from UIF_PerformingArts.dbo.CoreKidsDetail "
                                 + "WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                 + "AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                 + "AND RSVPGospel = 1 ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    chbRSVPGospel.Checked = reader.GetBoolean(0);
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

        protected void CheckDiscipleshipMentorProgram()
        {
            try
            {
                con.Open();

                string selectSQL = "select studentlastname, studentfirstname "
                                 + "from UIF_PerformingArts.dbo.DiscipleshipmentorProgram "
                                 + "WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                                 + "AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    chbDiscipleShipMentor.Checked = true;
                    lbDiscipleshipMentor.Enabled = true;
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

        protected void CheckStudentVolunteerTracking()
        {
            try
            {
                con.Open();

                string selectSQL = "select StudentVolunteer "
                                 + "from UIF_PerformingArts.dbo.StudentInformation "
                                 + "WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                 + "AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                 + "AND StudentVolunteer = 1 ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    chbStudentVolunteer.Checked = reader.GetBoolean(0);
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

        protected void PopulateVolunteerActiveList()
        {
            try
            {
                con.Open();

                string selectSQL = "select lastname, firstname " +
                                   "from UIF_PerformingArts.dbo.VolunteerInformation " +
                                   "where discipleshipmentorparticipation = 1 " +
                                   "and discipleshipmentorWaitingList = 0 " +
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
                                   "and discipleshipmentorWaitingList = 1 " +
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
                        //ddlDiscipleShipMentorAvailable.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                        //ddlVolunteerActiveMentees.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                        ddlVolunteerWaitingList.Items.Add(reader.GetString(0) + "," + reader.GetString(1) + "(" + reader.GetString(2).Trim().ToString() + ")" );
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
                con.Open();

                string selectSQL = "select lastname, firstname " +
                                   "from VolunteerInformation " +
                                   "where discipleshipmentorparticipation = 1 " +
                                   "and discipleshipmentorWaitingList = 0 " +
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
                        ddlDiscipleShipMentorAvailable.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                        //ddlVolunteerActiveMentees.Items.Add(reader.GetString(1) + " " + reader.GetString(0));
                        //ddlWaitingListInactiveStudents.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                    } while (reader.Read());
                    ddlDiscipleShipMentorAvailable.Items.Add("N/A");
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
            lblComments.Visible = true;
            lblDiscipleMentor.Visible = true;
            lbVolunteerInformation.Visible = true;
            lblProgramEnrollment.Visible = true;
            lblStaffCoordinator.Visible = true;
            chbHasGraduated.Visible = true;
            //txbDiscipleshipMentor.Visible = true;
            ddlDiscipleShipMentorAvailable.Visible = true;
            txbStaffCoordinator.Visible = true;
            txbProgramEnrollment.Visible = true;
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
            txbProgramEnrollment.Enabled = true;
            //txbCovenantReceivedDate.Enabled = true;
            //txbCovenantSentDate.Enabled = true;
            txbComments.Enabled = true;
            chbHasGraduated.Enabled = true;
            chbCovenantLetter.Enabled = true;

            chbWaitingListInactive.Enabled = true;
            chbWaitingListInactive.Visible = true;
            
            lblWaitingListStudents.Enabled = true;
            lblWaitingListStudents.Visible = true;

            chbVolunteerWaitingList.Enabled = true;
            chbVolunteerWaitingList.Visible = true;

            //New fields..
            pnlPanel.Enabled = true;
            pnlPanel.Visible = true;
            ddlCovRecDay.Enabled = true;
            ddlCovRecDay.Visible = true;
            ddlCovRecMonth.Enabled = true;
            ddlCovRecMonth.Visible = true;
            ddlCovRecYear.Enabled = true;
            ddlCovRecYear.Visible = true;
            ddlCovSentDay.Enabled = true;
            ddlCovSentDay.Visible = true;
            ddlCovSentMonth.Enabled = true;
            ddlCovSentMonth.Visible = true;
            ddlCovSentYear.Enabled = true;
            ddlCovSentYear.Visible = true;

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
            //ddlCovSentYear.Text = "Year";
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
            cmbDiscipleshipMentor.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select dmp.studentlastname, dmp.studentfirstname, dmp.discipleshipmentor, dmp.optionsstaffcoordinator, dmp.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, si.pictureidentification, dmp.sysupdate, "
                           + "dmp.covenantreceived, dmp.covenantsentdate, dmp.covenantreceiveddate, dmp.waitinglistinactive "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "WHERE dmp.studentlastname=@studentlastname "
                           + "AND dmp.studentfirstname=@studentfirstname";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", Request.QueryString["StudentLastName"]));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", Request.QueryString["StudentFirstName"]));

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
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
                    if (reader.IsDBNull(2))
                    {
                        ddlDiscipleShipMentorAvailable.Text = "N/A";
                    }
                    else
                    {
                        ddlDiscipleShipMentorAvailable.Items.Add(reader.GetString(2));
                        ddlDiscipleShipMentorAvailable.Text = reader.GetString(2);                        
                    }
                    if (reader.IsDBNull(3))
                    {
                        txbStaffCoordinator.Text = "N/A";
                    }
                    else
                    {
                        txbStaffCoordinator.Text = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        txbProgramEnrollment.Text = "N/A";
                    }
                    else
                    {
                        txbProgramEnrollment.Text = reader.GetString(4);
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
                        ddlCovSentMonth.Text = "00";
                        ddlCovSentDay.Text = "00";
                        ddlCovSentYear.Text = "0000";
                    }
                    else
                    {
                        ddlCovSentMonth.Text = reader.GetSqlValue(11).ToString().Substring(0, 2);
                        ddlCovSentDay.Text = reader.GetSqlValue(11).ToString().Substring(3, 2);
                        ddlCovSentYear.Text = reader.GetSqlValue(11).ToString().Substring(6, 4);
                    }
                    if (reader.IsDBNull(12))
                    {
                        ddlCovRecMonth.Text = "00";
                        ddlCovRecDay.Text = "00";
                        ddlCovRecYear.Text = "0000";
                    }
                    else
                    {
                        ddlCovRecMonth.Text = reader.GetSqlValue(12).ToString().Substring(0, 2);
                        ddlCovRecDay.Text = reader.GetSqlValue(12).ToString().Substring(3, 2);
                        ddlCovRecYear.Text = reader.GetSqlValue(12).ToString().Substring(6, 4);
                    }
                    if (reader.IsDBNull(13))
                    {
                        chbWaitingListInactive.Checked = false;
                    }
                    else
                    {
                        chbWaitingListInactive.Checked = reader.GetBoolean(13);
                    }
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
            cmbDiscipleshipMentor.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select dmp.studentlastname, dmp.studentfirstname, dmp.discipleshipmentor, dmp.optionsstaffcoordinator, dmp.programenrollment, "
                           + "dmp.hasgraduated, dmp.comment, dmp.lastupdatedby, si.pictureidentification, dmp.sysupdate, "
                           + "dmp.covenantreceived, dmp.covenantsentdate, dmp.covenantreceiveddate, dmp.waitinglistinactive  "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "WHERE dmp.studentlastname=@studentlastname "
                           + "AND dmp.studentfirstname=@studentfirstname";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                
                if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                {
                    cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim()));
                    cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim()));
                }
                else if ((ddlWaitingListInactiveStudents.Text == "Please select a student") && (ddlDiscipleshipMentor.Text != "Please select a student"))
                {
                    cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim()));
                    cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlDiscipleshipMentor.SelectedValue.Substring(ddlDiscipleshipMentor.SelectedValue.IndexOf(",") + 1).Trim()));
                }
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
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
                    if (reader.IsDBNull(2))
                    {
                        ddlDiscipleShipMentorAvailable.Text = "N/A";
                    }
                    else
                    {
                        ddlDiscipleShipMentorAvailable.Items.Add(reader.GetString(2));
                        ddlDiscipleShipMentorAvailable.Text = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        txbStaffCoordinator.Text = "N/A";
                    }
                    else
                    {
                        txbStaffCoordinator.Text = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        txbProgramEnrollment.Text = "N/A";
                    }
                    else
                    {
                        txbProgramEnrollment.Text = reader.GetString(4);
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
                        ddlCovSentMonth.Text = "00";
                        ddlCovSentDay.Text = "00";
                        ddlCovSentYear.Text = "0000";
                    }
                    else
                    {
                        ddlCovSentMonth.Text = reader.GetSqlValue(11).ToString().Substring(0, 2);
                        ddlCovSentDay.Text = reader.GetSqlValue(11).ToString().Substring(3, 2);
                        ddlCovSentYear.Text = reader.GetSqlValue(11).ToString().Substring(6, 4);
                    }
                    if (reader.IsDBNull(12))
                    {
                        ddlCovRecMonth.Text = "00";
                        ddlCovRecDay.Text = "00";
                        ddlCovRecYear.Text = "0000";
                    }
                    else
                    {
                        ddlCovRecMonth.Text = reader.GetSqlValue(12).ToString().Substring(0, 2);
                        ddlCovRecDay.Text = reader.GetSqlValue(12).ToString().Substring(3, 2);
                        ddlCovRecYear.Text = reader.GetSqlValue(12).ToString().Substring(6, 4);
                    }
                    if (reader.IsDBNull(13))
                    {
                        chbWaitingListInactive.Checked = false;
                    }
                    else
                    {
                        chbWaitingListInactive.Checked = reader.GetBoolean(13);
                    }
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

        protected void cmbUpdate_Click(object sender, EventArgs e)
        {
            if (cmbUpdate.Text == "Insert New Entry")
            {
                txbNotes.Text = txbNotes.Text.Replace("'", "");
                if (txbNotes.Text == "(Type new activity entry here!)")
                {
                    //Prompt to tell them that they haven't entered anything..
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
                        txbProgramEnrollment.Enabled = true;
                        //txbCovenantReceivedDate.Enabled = true;
                        //txbCovenantSentDate.Enabled = true;
                        txbComments.Enabled = true;
                        chbHasGraduated.Enabled = true;
                        chbWaitingListInactive.Enabled = true;
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
            else
            {
                try
                {
                    string sqlUpdateStatement = "";
                    //ddlDiscipleShipMentorAvailable.Text = ddlDiscipleShipMentorAvailable.Text.Replace("'", "");
                    //txbDiscipleshipMentor.Text = txbDiscipleshipMentor.Text.Replace("'", "");
                    txbStaffCoordinator.Text = txbStaffCoordinator.Text.Replace("'", "");
                    txbProgramEnrollment.Text = txbProgramEnrollment.Text.Replace("'", "");
                    txbComments.Text = txbComments.Text.Replace("'", "");

                    if ((ddlDiscipleshipMentor.Text == "Please select a student") && (ddlWaitingListInactiveStudents.Text != "Please select a student"))
                    {
                        sqlUpdateStatement = " UPDATE DiscipleshipMentorProgram "
                        + "SET "
                        + " studentlastname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "' , "
                        + " studentfirstname = '" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim() + "' , "
                        //+ " discipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "' , "
                        + " discipleshipmentor = '" + ddlDiscipleShipMentorAvailable.Text.Trim() + "', "
                        + " optionsstaffcoordinator = '" + txbStaffCoordinator.Text.Trim() + "' , "
                        + " programenrollment = '" + txbProgramEnrollment.Text.Trim() + "' , "
                        + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                        + " covenantreceived = " + Convert.ToInt32(chbCovenantLetter.Checked) + ", "
                            //+ " covenantreceiveddate = '" + Convert.ToDateTime(txbCovenantReceivedDate.Text.Trim()) + "', "
                            //+ " covenantsentdate = '" + Convert.ToDateTime(txbCovenantSentDate.Text.Trim()) + "', "
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
                        + " discipleshipmentor = '" + ddlDiscipleShipMentorAvailable.Text.Trim() + "', "
                        + " optionsstaffcoordinator = '" + txbStaffCoordinator.Text.Trim() + "' , "
                        + " programenrollment = '" + txbProgramEnrollment.Text.Trim() + "' , "
                        + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                        + " covenantreceived = " + Convert.ToInt32(chbCovenantLetter.Checked) + ", "
                        + " covenantreceiveddate = '" + ddlCovRecMonth.Text.Trim() + "-" + ddlCovRecDay.Text.Trim() + "-" + ddlCovRecYear.Text.Trim() + "' , "
                        + " covenantsentdate = '" + ddlCovSentMonth.Text.Trim() + "-" + ddlCovSentDay.Text.Trim() + "-" + ddlCovSentYear.Text.Trim() + "' , "
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
            //WaitingList = 0;
            cmbDiscipleshipMentor.Enabled = true;
            cmbUpdate.Enabled = false;
            cmbBackToStudentPage.Enabled = true;

            //Disable all fields until they retreive the information so they aren't confused with who they are working on..RCM..
            //Locking the other header columns..RCM..
            txbStaffCoordinator.Enabled = false;
            //txbDiscipleshipMentor.Enabled = false;
            ddlDiscipleShipMentorAvailable.Enabled = false;
            txbProgramEnrollment.Enabled = false;
            //txbCovenantReceivedDate.Enabled = false;
            //txbCovenantSentDate.Enabled = false;
            txbComments.Enabled = false;
            chbHasGraduated.Enabled = false;
            chbCovenantLetter.Enabled = false;
            chbWaitingListInactive.Enabled = false;
            ddlWaitingListInactiveStudents.Text = "Please select a student";
        }

        protected void gvStudentHistory_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void lbAddNewEntry_Click(object sender, EventArgs e)
        {
            txbNotes.Visible = true;
            txbNotes.Text = "(Type new activity entry here!)";
            cmbUpdate.Text = "Insert New Entry";
            cmbUpdate.Enabled = true;
            lblNewEntry.Visible = true;

            //Locking the other header columns..RCM..
            txbStaffCoordinator.Enabled = false;
            //txbDiscipleshipMentor.Enabled = false;
            ddlDiscipleShipMentorAvailable.Enabled = false;
            txbProgramEnrollment.Enabled = false;
            //txbCovenantReceivedDate.Enabled = false;
            //txbCovenantSentDate.Enabled = false;
            txbComments.Enabled = false;
            chbHasGraduated.Enabled = false;
            chbCovenantLetter.Enabled = false;
            chbWaitingListInactive.Enabled = false;
            lblWaitingListStudents.Enabled = false;
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
                ClearPage();
                cmbViewWaitingList.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                con.Open();


                string sql = "Select dmp.studentlastname as 'LastName', dmp.studentfirstname as 'FirstName', si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1 as 'ParentGuardian', pg.workphone1 as 'WorkPhone', pg.cellphone1 as 'CellPhone', "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived as 'Covenant', dmp.discipleshipmentor as 'DiscipleshipMentor',dmp.optionsstaffcoordinator as 'StaffCoordinator', dmp.programenrollment as 'ProgramEnrollment', "
                           + "dmp.hasgraduated as 'Graduated', dmp.comment as 'Comments', dmp.lastupdatedby as 'LastUpdatedBy', dmp.covenantreceiveddate as 'Cov ReceiveDate', dmp.covenantsentdate as 'Cov SentDate', dmp.waitinglistinactive as 'WaitingList' "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                           + "ON (dmp.studentlastname = pg.studentlastname AND dmp.studentfirstname = pg.studentfirstname) "
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

            
            txbComments.Text = "N/A";
            //txbDiscipleshipMentor.Text = "N/A";
            //ddlDiscipleShipMentorAvailable.Items.Add("N/A");
            //ddlDiscipleShipMentorAvailable.Text = "N/A";
            txbNotes.Text = "N/A";
            //txbCovenantSentDate.Text = "N/A";
            //txbCovenantReceivedDate.Text = "N/A";
            txbStaffCoordinator.Text = "N/A";
            txbProgramEnrollment.Text = "N/A";
            //gvStudentHistory.Visible = false;
            //chbHasGraduated.Visible = false;
            //lbAddNewEntry.Visible = false;
            //lblComments.Visible = false;
            //lblDiscipleMentor.Visible = false;
            //lbVolunteerInformation.Visible = false;
            //lblNewEntry.Visible = false;
            //lblProgramEnrollment.Visible = false;
            //lblStaffCoordinator.Visible = false;
            txbProgramEnrollment.Text = "N/A";
            //cmbUpdate.Visible = false;
            //lblEnrolledStudents.Visible = false;
            //ddlDiscipleshipMentor.Visible = false;
            //ddlWaitingListInactiveStudents.Visible = false;
            //lblDiscipleshipmentor.Visible = false;
            //lbVolunteerInformation.Visible = false;
            //cmbDiscipleshipMentor.Visible = false;
            imgImage.ImageUrl = "";
            //lblNotes.Visible = false;
            chbCovenantLetter.Checked = false;
            chbCovenantLetter.Checked = false;
            chbWaitingListInactive.Checked = false;
            //lblWaitingListStudents.Visible = false;
            //lblCovenantReceivedDate.Visible = false;
            //lblCovenantSentDate.Visible = false;
        }


        protected void ClearPage()
        {
            txbComments.Visible = false;
            //txbDiscipleshipMentor.Visible = false;
            ddlDiscipleShipMentorAvailable.Visible = false;
            txbComments.Visible = false;
            txbNotes.Visible = false;
            //txbCovenantSentDate.Visible = false;
            //txbCovenantReceivedDate.Visible = false;
            txbStaffCoordinator.Visible = false;
            txbProgramEnrollment.Visible = false;
            gvStudentHistory.Visible = false;
            chbHasGraduated.Visible = false;
            lbAddNewEntry.Visible = false;
            lblComments.Visible = false;
            lblDiscipleMentor.Visible = false;
            lbVolunteerInformation.Visible = false;
            lblNewEntry.Visible = false;
            lblProgramEnrollment.Visible = false;
            lblStaffCoordinator.Visible = false;
            txbProgramEnrollment.Visible = false;
            cmbUpdate.Visible = false;
            lblEnrolledStudents.Visible = false;
            ddlDiscipleshipMentor.Visible = false;
            ddlWaitingListInactiveStudents.Visible = false;
            lblDiscipleshipmentor.Visible = false;
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
            Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&VolunteerLastName=" + ddlDiscipleShipMentorAvailable.Text.Substring(ddlDiscipleShipMentorAvailable.Text.IndexOf(" ") + 1, (ddlDiscipleShipMentorAvailable.Text.Length - ddlDiscipleShipMentorAvailable.Text.IndexOf(" ") - 1))
                 + "&VolunteerFirstName=" + ddlDiscipleShipMentorAvailable.Text.Substring(0, ddlDiscipleShipMentorAvailable.Text.IndexOf(" ")));
        }

        protected void gvViewAll_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlWaitingListInactiveStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //WaitingList = 2;
            cmbDiscipleshipMentor.Enabled = true;
            cmbUpdate.Enabled = false;
            cmbBackToStudentPage.Enabled = true;

            //Disable all fields until they retreive the information so they aren't confused with who they are working on..RCM..
            //Locking the other header columns..RCM..
            txbStaffCoordinator.Enabled = false;
            //txbDiscipleshipMentor.Enabled = false;
            ddlDiscipleShipMentorAvailable.Enabled = false;
            txbProgramEnrollment.Enabled = false;
            //txbCovenantReceivedDate.Enabled = false;
            //txbCovenantSentDate.Enabled = false;
            txbComments.Enabled = false;
            chbHasGraduated.Enabled = false;
            chbCovenantLetter.Enabled = false;
            chbWaitingListInactive.Enabled = false;
            //lblWaitingListStudents.Enabled = false;
            ddlDiscipleshipMentor.Text = "Please select a student";
        }

        protected void cmbViewWaitingList_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPage();
                cmbViewWaitingList.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                con.Open();

                string sql = "Select dmp.studentlastname as 'LastName', dmp.studentfirstname as 'FirstName', si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1 as 'ParentGuardian', pg.workphone1 as 'WorkPhone', pg.cellphone1 as 'CellPhone', "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived as 'Covenant', dmp.discipleshipmentor as 'DiscipleshipMentor',dmp.optionsstaffcoordinator as 'StaffCoordinator', dmp.programenrollment as 'ProgramEnrollment', "
                           + "dmp.hasgraduated as 'Graduated', dmp.comment as 'Comments', dmp.lastupdatedby as 'LastUpdatedBy', dmp.covenantreceiveddate as 'Cov ReceiveDate', dmp.covenantsentdate as 'Cov SentDate', dmp.waitinglistinactive as 'WaitingList' "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                           + "ON (dmp.studentlastname = pg.studentlastname AND dmp.studentfirstname = pg.studentfirstname) "
                           + "WHERE dmp.waitinglistinactive = 1 "
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
                ClearPage();
                cmbViewWaitingList.Enabled = false;
                cmbViewAll.Enabled = false;
                cmbViewEnrolledStudents.Enabled = false;
                cmbBackToStudentPage.Enabled = false;
                con.Open();

                string sql = "Select dmp.studentlastname as 'LastName', dmp.studentfirstname as 'FirstName', si.Address, si.City, si.State, si.Zip, si.HomePhone, si.StudentCellPhone, pg. ParentGuardian1 as 'ParentGuardian', pg.workphone1 as 'WorkPhone', pg.cellphone1 as 'CellPhone', "
                           + "si.Sex, si.Grade, si.DOB, dmp.covenantreceived as 'Covenant', dmp.discipleshipmentor as 'DiscipleshipMentor',dmp.optionsstaffcoordinator as 'StaffCoordinator', dmp.programenrollment as 'ProgramEnrollment', "
                           + "dmp.hasgraduated as 'Graduated', dmp.comment as 'Comments', dmp.lastupdatedby as 'LastUpdatedBy', dmp.covenantreceiveddate as 'Cov ReceiveDate', dmp.covenantsentdate as 'Cov SentDate', dmp.waitinglistinactive as 'WaitingList' "
                           + "FROM discipleshipmentorprogram dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                           + "ON (dmp.studentlastname = pg.studentlastname AND dmp.studentfirstname = pg.studentfirstname) "
                           + "WHERE dmp.waitinglistinactive = 0 "
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
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
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
            ExcelExport.ExportGridView(gvViewAll, Response);
        }

        protected void gvCoreKidsDetail_RowCommand(object sender, EventArgs e)
        {

        }



        protected void gvGeneralReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lkj = "";

        }
        protected void gvGeneralReport_RowCommand(object sender, EventArgs e)
        {
            //Link out to a new page to show detailed ReportCard...
            //Response.Redirect("CoreKidsDetail.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + "&Dept=" + Request.QueryString["Dept"]);

            //int i = 0;

        }

        protected void gvGeneralReport_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int i = 0;

            string currentCommand = e.CommandName;
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            string StudentName = gvCoreKidsDetail.DataKeys[currentRowIndex].Value.ToString();

            Response.Redirect("CoreKidsDetail.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + StudentName.Substring(0, StudentName.IndexOf(","))
                            + "&StudentFirstName=" + StudentName.Substring(StudentName.IndexOf(",") + 1, StudentName.Length - (StudentName.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void lbDiscipleshipMentor_Click(object sender, EventArgs e)
        {
            //Determine which list the student is on before Calling the DiscipleshipMentor page...RCM..2/3/12.
            if (OnWaitingList())
            {
                Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&WaitList=True" + "&StudentLastName=" + Request.QueryString["StudentLastName"] 
                                + "&StudentFirstName=" + Request.QueryString["StudentFirstName"]);
            }
            else if (ActiveMentee())
            {
                Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&WaitList=False" + "&StudentLastName=" + Request.QueryString["StudentLastName"]
                                + "&StudentFirstName=" + Request.QueryString["StudentFirstName"]);
            }
            else
            {
                Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + "&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
            }
        }

        protected Boolean OnWaitingList()
        {
            Boolean WaitingList = false;

            try
            {
                con.Open();//Opens the db connection.

                string sql1 = "select studentlastname "
                            + "from DiscipleshipMentorProgram "
                            + "where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                            + "and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                            + "and waitinglistinactive = 1 ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql1);
                SqlDataReader reader = null;
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    WaitingList = true;
                }
            }
            catch (Exception lkjaaffccss)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return WaitingList;
        }

        protected Boolean ActiveMentee()
        {
            Boolean ActiveMenteeOrNot = false;
            try
            {
                con.Open();//Opens the db connection.

                string sql1 = "select studentlastname "
                            + "from DiscipleshipMentorProgram "
                            + "where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                            + "and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                            + "and waitinglistinactive = 0 ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql1);
                SqlDataReader reader = null;
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    ActiveMenteeOrNot = true;
                }
            }
            catch (Exception lkjaaffccgg)
            {
                con.Close();
            }
            finally
            {
                con.Close();
            }
            return ActiveMenteeOrNot;
        }


    }
}