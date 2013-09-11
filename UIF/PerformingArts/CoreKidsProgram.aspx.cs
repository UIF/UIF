using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UrbanImpactCommon;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
//using System.Data.SqlClient;
using System.IO;
//using UrbanImpactCommon;
using System.Resources;
using System.Net;
//using System.Data.Sql;
using System.Web.Script.Services;
using System.Data.SqlTypes;

namespace UIF.PerformingArts
{
    public partial class CoreKidsProgram : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
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

                    RetreiveDataForDisplay();
                    if (((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")))
                    {
                        cmbRetrieveReport.Visible = true;
                    }

                    ddlReports.Items.Add("Please Select a Report");
                    ddlReports.Items.Add("CoreKids By School");
                    ddlReports.Items.Add("CoreKids By ZipCode");
                    ddlReports.Items.Add("CoreKids By Church");
                    ddlReports.Text = "Please Select a Report";

                    ddlReportProgram.Items.Add("Please Select a Program");
                    if (Department == "Athletics")
                    {
                        ddlReportProgram.Items.Add("Baseball");
                        ddlReportProgram.Items.Add("BasketballTEAMS");
                        ddlReportProgram.Items.Add("Outreach Basketball");
                        ddlReportProgram.Items.Add("MS Basketball League");
                        ddlReportProgram.Items.Add("HS Basketball League");
                        ddlReportProgram.Items.Add("SoccerIntraMurals");
                        ddlReportProgram.Items.Add("SoccerTEAMS");
                        ddlReportProgram.Items.Add("Bible Study");
                        ddlReportProgram.Items.Add("MondayNights");
                        ddlReportProgram.Items.Add("Special Events");
                        ddlReportProgram.Items.Add("3on3Basketball");
                    }
                    else if (Department == "PerformingArts")
                    {
                        ddlReportProgram.Items.Add("MSHS Choir");
                        ddlReportProgram.Items.Add("Childrens Choir");
                        ddlReportProgram.Items.Add("PerformingArtsAcademy");
                        ddlReportProgram.Items.Add("Singers");
                        ddlReportProgram.Items.Add("Shakes");
                    }
                    else if (Department == "Education")
                    {
                        ddlReportProgram.Items.Add("SummerDayCamp");
                        ddlReportProgram.Items.Add("SummerDayCamp");
                    }
                    else
                    {
                        ddlReportProgram.Items.Add("Baseball");
                        ddlReportProgram.Items.Add("BasketballTEAMS");
                        ddlReportProgram.Items.Add("Outreach Basketball");
                        ddlReportProgram.Items.Add("MS Basketball League");
                        ddlReportProgram.Items.Add("HS Basketball League");
                        ddlReportProgram.Items.Add("SoccerIntraMurals");
                        ddlReportProgram.Items.Add("SoccerTEAMS");
                        ddlReportProgram.Items.Add("Bible Study");
                        ddlReportProgram.Items.Add("MondayNights");
                        ddlReportProgram.Items.Add("Special Events");
                        ddlReportProgram.Items.Add("3on3Basketball");
                        ddlReportProgram.Items.Add("MSHS Choir");
                        ddlReportProgram.Items.Add("Childrens Choir");
                        ddlReportProgram.Items.Add("PerformingArtsAcademy");
                        ddlReportProgram.Items.Add("Singers");
                        ddlReportProgram.Items.Add("Shakes");
                    }
                    ddlReportProgram.Text = "Please Select a Program";
                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!
                    Response.Redirect("ErrorAccess.aspx");
                }
            }
        }


        protected void RetreiveDataForDisplay()
        {
            //Retreive the list of CoreKids from the table and display in the screen..RCM..
            try
            {
                //con.Open();

                try
                {
                    string sql_LoadGrid = "select LastName + ',' + FirstName as 'Name' "
                                        + "from UIF_PerformingArts.dbo.CoreKidsProgram "
                                        + "group by LastName, FirstName "
                                        + "order by LastName, FirstName ";

                    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "corekidsprogram");
                    gvGeneralReport.DataSource = ds.Tables[0];
                    gvGeneralReport.DataBind();
                    con.Close();
                    lblReportCard.Visible = true;
                    lblCoreKidsList.Visible = true;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
                finally
                {
                  //  con.Close();
                }
            }
            catch (Exception lkjlk)
            {
                

            }
        }

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }        
        
        protected void PopulateCoreKidsDetailData()
        {
           try
           {
                string sqlInsertStatement = "";

                sqlInsertStatement = "INSERT INTO CoreKidsDetail "
                                    + "select spa.LastName, spa.FirstName, spa.Program, AVG(CAST(spa.Attended as FLOAT)) * 100 as 'Attend %', COUNT(spa.Day) as '# of records', spa.ProgramSeason, si.HaveReceivedChrist, dmp.WaitinglistInactive as 'DiscipleshipMentorWant0', 0 as 'Student/Staff/Assistant', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP "
                                    + "from StudentProgramAttendance spa "
                                    + "LEFT OUTER JOIN StudentInformation si "
                                    + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName) "
                                    + "LEFT OUTER JOIN DiscipleshipMentorProgram dmp "
                                    + "ON (spa.LastName = dmp.StudentLastName AND spa.FirstName = dmp.StudentFirstName) "
                                    + "group by spa.LastName, spa.FirstName, spa.Program, spa.ProgramSeason, si.HaveReceivedChrist, dmp.WaitinglistInactive "
                                    + "order by spa.LastName, spa.FirstName, AVG(CAST(spa.Attended as FLOAT)) * 100 desc,COUNT(day) desc, spa.Program ";

                //create a SQL command to update record
                SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                if (sqlInsertCommand.ExecuteNonQuery() > 0)
                {
                    //maybe display a message confirming update has been successful
                    //con.Close();
                }
                else
                {
                    //display message that record was NOT updated
                    //	btnContinue.Visible = false;
                    //	lblAlert.Visible = true;
                    //	lblAlert.Text = "No update. Error has occurred.";
                }
           }
           catch (Exception lkjlaaa)
           {

           }
           finally
           {

           }
        }
                
        protected void CleanOutCoreKidsList()
        {
            //Clean Out CoreKidsProgram table each time the process runs..  Wash Method...RCM..
            try
            {
                string sqlDeleteStatement = "";
                //con.Open();

                try
                {
                    sqlDeleteStatement = "DELETE from UIF_PerformingArts.dbo.CoreKidsProgram ";

                    //create a SQL command to update record
                    SqlCommand sqlDeleteCommand = new SqlCommand(sqlDeleteStatement, con);
                    if (sqlDeleteCommand.ExecuteNonQuery() > 0)
                    {
                        //maybe display a message confirming update has been successful
                        //con.Close();
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
                    //lblInformation.Enabled = true;
                    //lblInformation.Text = "There was an exception INSERTING NEW data into the CoreKids table..  Please fix and try again MSG: " + lkjlaa.Message.ToString();
                    //throw new Exception(lblInformation.Text);
                }
            }
            catch (Exception lkjlaaa)
            {

            }
            finally
            {

            }
        }

        protected void CleanOutCoreKidsDetailData()
        {
            //Clean Out CoreKidsProgram table each time the process runs..  Wash Method...RCM..
            try
            {
                string sqlDeleteStatement = "";
                //con.Open();

                try
                {
                    sqlDeleteStatement = "DELETE from UIF_PerformingArts.dbo.CoreKidsDetail ";

                    //create a SQL command to update record
                    SqlCommand sqlDeleteCommand = new SqlCommand(sqlDeleteStatement, con);
                    if (sqlDeleteCommand.ExecuteNonQuery() > 0)
                    {
                        //maybe display a message confirming update has been successful
                        //con.Close();
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
                    //lblInformation.Enabled = true;
                    //lblInformation.Text = "There was an exception INSERTING NEW data into the CoreKids table..  Please fix and try again MSG: " + lkjlaa.Message.ToString();
                    //throw new Exception(lblInformation.Text);
                }
            }
            catch (Exception lkjlaaa)
            {

            }
            finally
            {

            }
        }

        protected void InsertIntoCoreKids(string StudentLastName, string StudentFirstName)
        {
            try
            {
                //CleanCharacters();

                //btnNewPerson1.Enabled = false;
                if (StudentLastName == "")
                {
                    //Student LastName is blank..  Abort..

                    //Throw an exception...RCM..
                    string except = "EXCEPTION";
                }
                else
                {
                    string sqlInsertStatement = "";
                    //con.Open();

                    try
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.CoreKidsProgram " +
                            "values ( "
                            + "'" + StudentLastName.Trim() + "',"
                            + "'" + StudentFirstName.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "  //SysUpdate
                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            + "1) ";//Yes, corekid... for other programs.
                       
                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                        if (sqlInsertCommand.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful
                            //con.Close();
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
                        //lblInformation.Enabled = true;
                        //lblInformation.Text = "There was an exception INSERTING NEW data into the CoreKids table..  Please fix and try again MSG: " + lkjlaa.Message.ToString();
                        //throw new Exception(lblInformation.Text);
                    }
                }
            }
            catch (Exception lkjlaaa)
            {

            }
            finally
            {

            }
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
            //Ryan C Manners...6/13/11.
             //Export the contents of the gridview to an Excel object for use...RCM..
            //ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            //ExcelExport.ExportGridView(gvGeneralDisplay, Response);
            
            //Email capability..?
        }

        protected void cmbRetrieveReport_Click(object sender, EventArgs e)
        {
            //Retrieve the class lists
            int NumberOfPoints = 0;
            con.Open();

            try
            {
                string StudentLastName = "";
                string StudentFirstName = "";
                Boolean AlreadySetPoints = false;
                Boolean RSVPGospelFlag = false;
                Boolean WaitingListInactiveFlag = false;
                Boolean AttendanceFlag = false;
                Boolean LongevityFlag = false;
                Boolean FlagYear1 = false;
                Boolean FlagYear2 = false;
                Boolean MultipleProgramFlag1 = false;
                Boolean MultipleProgramFlag2 = false;
                //Date time variable...RCM.
                DateTime RightNow = System.DateTime.Now;

                CleanOutCoreKidsList();//Refreshes the data.

                CleanOutCoreKidsDetailData();//Refreshes the data.

                PopulateCoreKidsDetailData();//Refreshes the data.

                string sql_LoadGrid = "select LastName + ',' + FirstName as 'Name' "
                                    + "from CoreKidsDetail "
                                    + "group by LastName, FirstName "
                                    + "order by LastName, FirstName ";

                SqlCommand cmd = new SqlCommand(sql_LoadGrid, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "CoreKidsDetail");

                //Iterate over setup records and call method to do the work on each one...RCM..
                foreach (DataRow myDataRowPO in custDS.Tables["CoreKidsDetail"].Rows)
                {
                    AlreadySetPoints = false;//Flag to determine multiple programs..RCM.
                    RSVPGospelFlag = false;//Flag to ensure GOSPEL response only counts once..RCM.
                    WaitingListInactiveFlag = false;//Flag to ensure Discipleshipmentor only counts once..RCM..
                    AttendanceFlag = false;//Flag to ensure that Consistent Attendance only counts once per student..RCM..
                    LongevityFlag = false;//Flag to ensure that Longevity only counts once per student..RCM..
                    NumberOfPoints = 0;//Reset the point counter..RCM.
                    FlagYear1 = false;//Longevity flag1..RCM.
                    FlagYear2 = false;//Longevity flag2..RCM.
                    MultipleProgramFlag1 = false;
                    MultipleProgramFlag2 = false;
                    StudentLastName = myDataRowPO[0].ToString().Substring(0, myDataRowPO[0].ToString().IndexOf(","));
                    StudentFirstName = myDataRowPO[0].ToString().Substring(myDataRowPO[0].ToString().IndexOf(",") + 1, myDataRowPO[0].ToString().Length - (myDataRowPO[0].ToString().IndexOf(",") + 1));
                    string TheProgramName1 = "";

                    string sql = "select spa.LastName, spa.FirstName, spa.Program as 'ProgramName', AVG(CAST(spa.Attended as FLOAT)) * 100 as 'Attend %', COUNT(spa.Day) as '# of entries', spa.ProgramSeason, si.HaveReceivedChrist, si.Discipleshipmentorprogram as 'DiscMentorActive', '0' as 'StudentStaffAssistant' "
                               + "from StudentProgramAttendance spa "
                               + "LEFT OUTER JOIN StudentInformation si "
                               + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName) "
                               + "LEFT OUTER JOIN RSVPGospelTracking rvg "
                               + "ON (spa.lastname = rvg.lastname AND spa.firstname = rvg.firstname) "
                               //+ "LEFT OUTER JOIN DiscipleshipMentorProgram dmp "
                               //+ "ON (spa.LastName = dmp.StudentLastName AND spa.FirstName = dmp.StudentFirstName) "
                               + "where spa.LastName = '" + StudentLastName + "' "
                               + "and spa.FirstName = '" + StudentFirstName + "' "
                               + "group by spa.LastName, spa.FirstName, spa.Program, spa.ProgramSeason, si.HaveReceivedChrist, si.Discipleshipmentorprogram "
                               + "order by spa.LastName, spa.FirstName, AVG(CAST(spa.Attended as FLOAT)) * 100 desc,COUNT(day) desc, spa.Program ";

                    SqlCommand cmd2 = new SqlCommand(sql, con);
                    cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA2 = new SqlDataAdapter();
                    custDA2.SelectCommand = cmd2;
                    DataSet custDS2 = new DataSet();
                    custDA2.Fill(custDS2, "StudentProgramAttendance");

                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO2 in custDS2.Tables["StudentProgramAttendance"].Rows)
                    {
                        StudentLastName = myDataRowPO2[0].ToString();
                        StudentFirstName = myDataRowPO2[1].ToString();
                        string ProgramName = myDataRowPO2[2].ToString();
                        decimal AttendPercent = System.Convert.ToDecimal(myDataRowPO2[3]);
                        string NumberOfRecords = myDataRowPO2[4].ToString();
                        string ProgramSeason = myDataRowPO2[5].ToString();
                        Boolean RSVPGospel = false;
                        Boolean WaitingListInactive;
                        Boolean StudentStaffVolunteer;

                        //Ryan C Manners...12/13/11.
                        //Use this query to determine Multiple Programs.  They must actually be different program
                        //names, not just different semesters of the same program..RCM.
                        string sql3 = "select spa.LastName, spa.FirstName, spa.Program as 'ProgramName' "
                                   + "from StudentProgramAttendance spa "
                                   + "LEFT OUTER JOIN StudentInformation si "
                                   + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName) "
                                   + "LEFT OUTER JOIN DiscipleshipMentorProgram dmp "
                                   + "ON (spa.LastName = dmp.StudentLastName AND spa.FirstName = dmp.StudentFirstName) "
                                   + "where spa.LastName = '" + StudentLastName + "' "
                                   + "and spa.FirstName = '" + StudentFirstName + "' "
                                   + "and spa.ProgramSeason like '%" + RightNow.AddYears(-1).Year.ToString() + "%' "
                                   + "group by spa.LastName, spa.FirstName, spa.Program "
                                   + "order by spa.LastName, spa.FirstName, spa.Program ";

                        SqlCommand cmd3 = new SqlCommand(sql3, con);
                        cmd3.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                        SqlDataAdapter custDA3 = new SqlDataAdapter();
                        custDA3.SelectCommand = cmd3;
                        DataSet custDS3 = new DataSet();
                        custDA3.Fill(custDS3, "StudentProgramAttendance");

                        //Ryan C Manners...12/13/11.
                        //Use this query to determine Multiple Programs.  They must actually be different program
                        //names, not just different semesters of the same program..RCM.
                        string sql4 = "select spa.LastName, spa.FirstName, spa.Program as 'ProgramName', spa.ProgramSeason, AVG(CAST(spa.Attended as FLOAT)) * 100 as 'Attend %' "
                                   + "from StudentProgramAttendance spa "
                                   + "LEFT OUTER JOIN StudentInformation si "
                                   + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName) "
                                   + "LEFT OUTER JOIN DiscipleshipMentorProgram dmp "
                                   + "ON (spa.LastName = dmp.StudentLastName AND spa.FirstName = dmp.StudentFirstName) "
                                   + "where spa.LastName = '" + StudentLastName + "' "
                                   + "and spa.FirstName = '" + StudentFirstName + "' "
                                   + "and (spa.ProgramSeason like '%" + RightNow.AddYears(-1).Year.ToString() + "%' OR spa.ProgramSeason like '%" + RightNow.Year.ToString() + "%') "
                                   + "group by spa.LastName, spa.FirstName, spa.Program, spa.ProgramSeason "
                                   + "order by spa.LastName, spa.FirstName, spa.Program ";

                        SqlCommand cmd4 = new SqlCommand(sql4, con);
                        cmd4.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                        SqlDataAdapter custDA4 = new SqlDataAdapter();
                        custDA4.SelectCommand = cmd4;
                        DataSet custDS4 = new DataSet();
                        custDA4.Fill(custDS4, "StudentProgramAttendance");
                                              
                        //TESTING...
                        //if ((StudentLastName == "Baurle") && (StudentFirstName == "Devin"))
                        if ((StudentLastName == "Brown") && (StudentFirstName == "Cameron"))
                        {
                            string lkjlk = "";
                        }

                        //Retreiving RSVP Gospel
                        try
                        {
                            //Ryan C Manners...
                            //Use this query to determine RSVP Gospel...RCM..9/22/12.
                            //string sql5 = "select spa.LastName, spa.FirstName, spa.RSVPGospel "
                            //           + "from RSVPGospelTracking spa "
                            //           + "where spa.LastName = '" + StudentLastName + "' "
                            //           + "and spa.FirstName = '" + StudentFirstName + "' "
                            //           + "and spa.RSVGPGospel = 1 "
                            //           + "GROUP BY spa.LastName, spa.FirstName, spa.RSVGPGospel ";

                            string sql5 = "select spa.LastName, spa.FirstName, spa.RSVPGospel "
                                       + "from CoreKidsDetail spa "
                                       + "where spa.LastName = '" + StudentLastName + "' "
                                       + "and spa.FirstName = '" + StudentFirstName + "' "
                                       + "and spa.RSVGPGospel = 1 "
                                       + "GROUP BY spa.LastName, spa.FirstName, spa.RSVGPGospel ";

                            SqlCommand cmd5 = new SqlCommand(sql5, con);
                            cmd5.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                            SqlDataAdapter custDA5 = new SqlDataAdapter();
                            custDA5.SelectCommand = cmd5;
                            DataSet custDS5 = new DataSet();
                            custDA5.Fill(custDS5, "CoreKidsDetail");
                            foreach (DataRow myDataRow88899 in custDS5.Tables["CoreKidsDetail"].Rows)
                            {
                                if (myDataRow88899.IsNull(2))
                                {
                                    RSVPGospel = false;
                                }
                                else
                                {
                                    RSVPGospel = System.Convert.ToBoolean(myDataRow88899[2]);
                                }
                            }
                        }
                        catch (Exception lkjlkaaddcc)
                        {

                        }

                        //Retreiving DiscipleshipMentor field...RCM.
                        if (myDataRowPO2.IsNull(7))
                        {
                            WaitingListInactive = false;
                        }
                        else
                        {
                            WaitingListInactive = System.Convert.ToBoolean(myDataRowPO2[7]);
                        }

                        //Retreiving Student/Staff/Volunteer information..RCM..
                        if (myDataRowPO2.IsNull(8))//In the current year.
                        {
                            StudentStaffVolunteer = false;
                        }
                        else
                        {
                            StudentStaffVolunteer = System.Convert.ToBoolean(System.Convert.ToInt32(myDataRowPO2[8]));
                        }

                        //-----------------------------------------------//--------------------------------------------------------------------------------------------                       
                        //Evaluation Formula Rules Beginning Now.........//
                        //-----------------------------------------------//

                        //Evaluate Consistent Attendance of at least 1 program...RCM
                        if ((AttendPercent >= 70.0m) && (!AttendanceFlag))    //Consistent Attendance in at least 1 Program..RCM..
                        {
                            NumberOfPoints = NumberOfPoints + 1;
                            AttendanceFlag = true;
                        }

                        //Determine multiple programs.  Evaluate once per student..RCM..12/9/11.   
                        //Using the 3rd dataset.. to determine different programs as Actually distinct different program names, not just
                        //different semesters of the same program...
                        if ((custDS3.Tables["StudentProgramAttendance"].Rows.Count > 1) && (!AlreadySetPoints))//Multiple Programs..RCM
                        {
                            foreach (DataRow myDataRowPO4 in custDS4.Tables["StudentProgramAttendance"].Rows)
                            {
                                //decimal MPAttendPercent = System.Convert.ToDecimal(myDataRowPO4[3]);
                                if ((!MultipleProgramFlag1) && (System.Convert.ToDecimal(myDataRowPO4[4]) >= 70.0m) )
                                {
                                    TheProgramName1 = myDataRowPO4[2].ToString();
                                    MultipleProgramFlag1 = true;
                                }

                                if ((MultipleProgramFlag1) && (!MultipleProgramFlag2) && (System.Convert.ToDecimal(myDataRowPO4[4]) >= 70.0m) && ((myDataRowPO4[2].ToString() != TheProgramName1)))
                                {
                                    MultipleProgramFlag2 = true;
                                }
                            }
                            
                            //Must have attendance percentage of 70% or higher in distinctly different multiple programs within the most recent same
                            //fiscal year in order to qualify for the "Multiple Programs".
                            if ((MultipleProgramFlag1) && (MultipleProgramFlag2) && (!AlreadySetPoints))
                            {
                                NumberOfPoints = NumberOfPoints + 1;
                                AlreadySetPoints = true;
                            }
                        }

                        //Determine if they've responded to the Gospel.  Only 1 point available..RCM.
                        if ((RSVPGospel) && (!RSVPGospelFlag))
                        {
                            NumberOfPoints = NumberOfPoints + 1;
                            RSVPGospelFlag = true;
                        }

                        //Determine if the Discipleshipmentor Point has been awarded once..RCM..
                        if ((WaitingListInactive) && (!WaitingListInactiveFlag))
                        {
                            NumberOfPoints = NumberOfPoints + 1;
                            WaitingListInactiveFlag = true;
                        }

                        //Leave alone.  Could get a point for every program in which they are Student/Staff/Volunteer..RCM..
                        //To be evaluated for future change..
                        if (StudentStaffVolunteer)
                        {
                            NumberOfPoints = NumberOfPoints + 1;
                        }
                         
                        //Longevity...Longevity is defined as:  To receive the point for this category, the student must maintain an attendance
                        //program percentage of at least 70% over the course of the 2 most recent years in any one program.  
                        if ((ProgramSeason.EndsWith(RightNow.AddYears(-2).Year.ToString())) && (AttendPercent >= 70.0m) && (!FlagYear1))
                        {
                            FlagYear1 = true;
                        }

                        if ((ProgramSeason.EndsWith(RightNow.AddYears(-1).Year.ToString())) && (AttendPercent >= 70.0m) && (!FlagYear2))
                        {
                            FlagYear2 = true;
                        }

                        //Determine Longevity through a series of flags..RCM..
                        if (FlagYear1 && FlagYear2 && !LongevityFlag)
                        {
                            NumberOfPoints = NumberOfPoints + 1;
                            LongevityFlag = true;
                        }
                    }
                    //Final Evaluation.  If the student has 3 or more points they are in..RCM..
                    if (NumberOfPoints >= 3)
                    {
                        //The student meets the criteria, so add them to CoreKids table..RCM..12/1/11.
                        InsertIntoCoreKids(StudentLastName, StudentFirstName);
                    }
                }
                RetreiveDataForDisplay();//Display the new current CoreKids list on the screen..RCM..12/8/11.
                custDS.Clear();

                //SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "corekidsdetail");
                //gvGeneralReport.DataSource = ds.Tables[0];
                //gvGeneralReport.DataBind();
                //con.Close();
            }
            catch (Exception lkjl)
            {
                string rr = "";
            }
            finally
            {
                con.Close();
            }
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
            string StudentName = gvGeneralReport.DataKeys[currentRowIndex].Value.ToString();

            Response.Redirect("CoreKidsDetail.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + StudentName.Substring(0, StudentName.IndexOf(","))
                            + "&StudentFirstName=" + StudentName.Substring(StudentName.IndexOf(",") + 1, StudentName.Length - (StudentName.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);
        }



        protected void RetrieveReport(string ReportName)
        {
            //ddlNationalCheckReport.Text = "Select a Code?";
            //ddlDMVCheckReport.Text = "Select a Code?";
            try
            {
                con.Open();

                string sql = "";
                sql = "Select ck.LastName, ck.FirstName, vi.Pacriminalcheckcodes, vi.Pacriminalcheckdate "
                           + "from CoreKidsProgram ck "
                           + "LEFT OUTER JOIN volunteerprogramattendance vpa "
                           + "ON (vi.lastname = vpa.lastname AND vi.firstname = vpa.firstname) ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerDetails");
                gvReports.DataSource = ds.Tables[0];
                gvReports.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
                gvReports.Visible = true;
                //ddlNames.Visible = false;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlReportProgram.Text = "Please Select a Program";

            string columnname = "";
            if (ddlReports.Text == "CoreKids By School")
            {
                columnname = "School";
            }
            else if (ddlReports.Text == "CoreKids By ZipCode")
            {
                columnname = "Zip";
            }
            else if (ddlReports.Text == "CoreKids By Church")
            {
                columnname = "Church";
            }
            else
            {
                //Do Nothing..RCM...No Report Selected...
            }

            try
            {
                con.Open();

                string sql = "";
                //if (columnname == "School")
                //{
                    sql = "Select ck.LastName, ck.FirstName, pl." + columnname + "  "
                                + "from CoreKidsProgram ck "
                                + "LEFT OUTER JOIN StudentInformation pl "
                                + "ON (ck.lastname = pl.lastname AND ck.firstname = pl.firstname) "
                                + "GROUP BY ck.LastName, ck.FirstName, pl." + columnname + " "
                                + "ORDER BY pl." + columnname + " ,ck.LastName, ck.FirstName " ;
                //}
                //else if (columnname == "Zip")
                //{
                //    sql = "Select ck.LastName, ck.FirstName, pl." + columnname + "  "
                //                + "from CoreKidsProgram ck "
                //                + "LEFT OUTER JOIN StudentInformation pl "
                //                + "ON (ck.lastname = pl.lastname AND ck.firstname = pl.firstname) "
                //        //+ "WHERE pl." + columnname + " = 1 "
                //                + "GROUP BY ck.LastName, ck.FirstName, pl." + columnname + " "
                //                + "ORDER BY pl." + columnname + " ,ck.LastName, ck.FirstName ";

                //}
                //else if (columnname == "Church")
                //{
                //    sql = "Select ck.LastName, ck.FirstName, pl." + columnname + "  "
                //                + "from CoreKidsProgram ck "
                //                + "LEFT OUTER JOIN StudentInformation pl "
                //                + "ON (ck.lastname = pl.lastname AND ck.firstname = pl.firstname) "
                //        //+ "WHERE pl." + columnname + " = 1 "
                //                + "GROUP BY ck.LastName, ck.FirstName, pl." + columnname + " "
                //                + "ORDER BY pl." + columnname + " ,ck.LastName, ck.FirstName ";

                //}


                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "CoreKidsProgram");
                gvReports.DataSource = ds.Tables[0];
                gvReports.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
                gvReports.Visible = true;
                //ddlNames.Visible = false;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void ddlReportProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlReports.Text = "Please Select a Report";

            string columnname = "";

            if (ddlReportProgram.Text == "BasketballTEAMS")
            {
                columnname = "BasketballTEAMS";
            }
            else if (ddlReportProgram.Text == "Outreach Basketball")
            {
                columnname = "OutreachBasketball";
            }
            else if (ddlReportProgram.Text == "SoccerIntraMurals")
            {
                columnname = "SoccerIntraMurals";
            }
            else if (ddlReportProgram.Text == "SoccerTEAMS")
            {
                columnname = "SoccerTEAMS";
            }
            else if (ddlReportProgram.Text == "Bible Study")
            {
                columnname = "BibleStudy";
            }
            else if (ddlReportProgram.Text == "Special Events")
            {
                columnname = "SpecialEvents";
            }
            else if (ddlReportProgram.Text == "MondayNights")
            {
                columnname = "MondayNights";
            }
            else if (ddlReportProgram.Text == "Baseball")
            {
                columnname = "Baseball";
            }
            else if (ddlReportProgram.Text == "MS Basketball League")
            {
                columnname = "MSBasketballLg";
            }
            else if (ddlReportProgram.Text == "HS Basketball League")
            {
                columnname = "HSBasketballLg";
            }
            else if (ddlReportProgram.Text == "3on3Basketball")
            {
                columnname = "[3on3Basketball]";
            }
            else if (ddlReportProgram.Text == "MSHS Choir")
            {
                columnname = "MSHSChoir";
            }
            else if (ddlReportProgram.Text == "Childrens Choir")
            {
                columnname = "ChildrensChoir";
            }
            else if (ddlReportProgram.Text == "PerformingArtsAcademy")
            {
                columnname = "PerformingArts";
            }
            else if (ddlReportProgram.Text == "Singers")
            {
                columnname = "Singers";
            }
            else if (ddlReportProgram.Text == "Shakes")
            {
                columnname = "Shakes";
            }
            else if (ddlReportProgram.Text == "SummerDayCamp")
            {
                columnname = "SummerDayCamp";
            }

            try
            {
                con.Open();

                string sql = "";
                if (columnname == "PerformingArts")
                {
                    sql = "Select ck.LastName, ck.FirstName, pl." + columnname + " as 'PerformingArtsAcademy' "
                               + "from CoreKidsProgram ck "
                               + "LEFT OUTER JOIN ProgramsList pl "
                               + "ON (ck.lastname = pl.lastname AND ck.firstname = pl.firstname) "
                               + "WHERE pl." + columnname + " = 1 "
                               + "GROUP BY ck.LastName, ck.FirstName, pl." + columnname + " "
                               + "ORDER BY ck.LastName, ck.FirstName ";
                }
                else
                {
                    sql = "Select ck.LastName, ck.FirstName, pl." + columnname + "  "
                               + "from CoreKidsProgram ck "
                               + "LEFT OUTER JOIN ProgramsList pl "
                               + "ON (ck.lastname = pl.lastname AND ck.firstname = pl.firstname) "
                               + "WHERE pl." + columnname + " = 1 "
                               + "GROUP BY ck.LastName, ck.FirstName, pl." + columnname + " "
                               + "ORDER BY ck.LastName, ck.FirstName ";
                }

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "CoreKidsProgram");
                gvReports.DataSource = ds.Tables[0];
                gvReports.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
                gvReports.Visible = true;
                //ddlNames.Visible = false;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }
    }
}