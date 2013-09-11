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
    public partial class Options : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public int irowNum = 0;
        public static string Department = "";
        public static string CalenderVariable1 = "";
        public static string CalenderVariable2 = "";
        public static string CalenderVariable3 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];
                CalenderVariable1 = "";
                CalenderVariable2 = "";
                CalenderVariable3 = "";

                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);

                //Check for a login... If not, re-direct them to the mainmenu page..RCM..
                if (Request.QueryString["security"] == "Good")
                {
                    if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
                    {
                        PopulateBusesDropDown();
                        PopulateReportsDropDown();
                        PopulateDropDown();//StudentNames.
                        ddlOptions.Items.Add("Please select a student");
                        ddlOptions.Text = "Please select a student";
                    }
                    else
                    {
                        PopulateBusesDropDown();
                        PopulateReportsDropDown();
                        DisplayHeaderFields();
                        
                        //RetrieveInformation();
                        
                        PopulateDropDown();//StudentNames.
                        DisplayTheGrid(Request.QueryString["StudentLastName"], Request.QueryString["StudentFirstName"], Request.QueryString["StudentMiddleName"]);

                        ddlOptions.Items.Add(Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"] + "(" + Request.QueryString["StudentMiddleName"] + ")");
                        ddlOptions.Text = Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"] + "(" + Request.QueryString["StudentMiddleName"] + ")";

                        lblNotes.Visible = true;
                        lbAddNewEntry.Visible = true;
                        cmbUpdate.Enabled = true;
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


        protected void PopulateReportsDropDown()
        {
            ddlReports.Items.Add("Please select a report");
            ddlReports.Items.Add("Student General Info");
            ddlReports.Items.Add("General College Report");
            ddlReports.Items.Add("General Trade Report");
            ddlReports.Items.Add("General Career Report");
            ddlReports.Items.Add("General Military Report");
            ddlReports.Items.Add("General Ministry Report");
            ddlReports.Items.Add("Student College Bus");
            ddlReports.Items.Add("Student Trade Bus");
            ddlReports.Items.Add("Student Career Bus");
            ddlReports.Items.Add("Student Military Bus");
            ddlReports.Items.Add("Student Ministry Bus");
            ddlReports.Text = "Please select a report";
        }

        protected void PopulateBusesDropDown()
        {
            try
            {
                ddlBuses.Items.Add("Select a Bus");
                ddlBuses.Items.Add("College");
                ddlBuses.Items.Add("Trade");
                ddlBuses.Items.Add("Military");
                ddlBuses.Items.Add("Career");
                ddlBuses.Items.Add("Ministry");
                ddlBuses.Text = "Select a Bus";
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

                //string selectSQL = "select si.lastname, si.firstname, si.middlename " +
                //                   "from StudentInformation si " +
                //                   "left outer join OptionsProgramNEW op " +
                //                   "on (op.studentlastname = si.lastname AND op.studentfirstname = si.firstname AND op.studentmiddlename = si.middlename) " +
                //                   "where op.studentlastname is not null " +
                //                   "group by si.lastname, si.firstname, si.middlename ";

                string selectSQL = "select op.Studentlastname, op.Studentfirstname, op.Studentmiddlename " +
                                   "from OptionsProgramNEW op " +
                                   //"left outer join StudentInformation si " +
                                   //"on (op.studentlastname = si.lastname AND op.studentfirstname = si.firstname AND op.studentmiddlename = si.middlename) " +
                                   //"where op.studentlastname is not null " +
                                   "group by op.studentlastname, op.studentfirstname, op.studentmiddlename ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    //ddlOptions.Items.Add("Please select a student");
                    do
                    {
                        ddlOptions.Items.Add(reader.GetString(0) + "," + reader.GetString(1) + " (" + reader.GetString(2) + ") ");
                    } while (reader.Read());
                    reader.Close();
                    //ddlOptions.Text = "Please select a student";
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

        protected void DisplayPartialHeaderFields()
        {
            pnlGeneralItems.Visible = false;
            lblComments.Visible = true;
            //lblDiscipleMentor.Visible = true;
            //lblProgramEnrollment.Visible = true;
            //lblStaffCoordinator.Visible = true;

            //txbDiscipleshipMentor.Visible = true;
            //txbStaffCoordinator.Visible = true;
            //txbProgramEnrollment.Visible = true;
            txbComments.Visible = true;
            cmbUpdate.Visible = true;
            //txbPostGraduatePlans.Visible = true;
            //Image1.ImageUrl

            lblGeneralInformation.Visible = true;

            lblBusOption.Visible = true;
            chbDriversLicense.Visible = true;
            //chbPersonalSSI.Visible = true;
            chbBirthCertificate.Visible = true;
            chbHSTranscript.Visible = true;
            chbHasGraduated.Visible = true;
            //chbHealthInsurance.Visible = true;
            chbBankAccount.Visible = true;
            //chbFAFSA.Visible = true;
            //chbPghPromiseEligible.Visible = true;
            chbSocSecurityCard.Visible = true;
            chbInterviewed.Visible = true;
            chbPaid.Visible = true;

            //chbSAT.Visible = true;
            //chbACT.Visible = true;
            //chbCollegeFair.Visible = true;
            //chbPromise.Visible = true;
            //lblPostGraduatePlans.Visible = true;
            //cmbStudentsPage.Enabled = true;
            //cmbStudentPage2.Enabled = true;

            //pnlGeneralItems.Visible = true;
        }


        protected void DisplayHeaderFields()
        {
            pnlGeneralItems.Visible = false;
            lblComments.Visible = true;
            //lblDiscipleMentor.Visible = true;
            //lblProgramEnrollment.Visible = true;
            //lblStaffCoordinator.Visible = true;
            
            //txbDiscipleshipMentor.Visible = true;
            //txbStaffCoordinator.Visible = true;
            //txbProgramEnrollment.Visible = true;
            txbComments.Visible = true;
            cmbUpdate.Visible = true;
            //txbPostGraduatePlans.Visible = true;
            //Image1.ImageUrl

            lblGeneralInformation.Visible = true;

            lblBusOption.Visible = true;
            chbDriversLicense.Visible = true;
            //chbPersonalSSI.Visible = true;
            chbBirthCertificate.Visible = true;
            chbHSTranscript.Visible = true;
            chbHasGraduated.Visible = true;
            //chbHealthInsurance.Visible = true;
            chbBankAccount.Visible = true;
            //chbFAFSA.Visible = true;
            //chbPghPromiseEligible.Visible = true;
            chbSocSecurityCard.Visible = true;
            chbInterviewed.Visible = true;
            //chbPaid.Visible = true;
            
            //chbSAT.Visible = true;
            //chbACT.Visible = true;
            //chbCollegeFair.Visible = true;
            //chbPromise.Visible = true;
            //lblPostGraduatePlans.Visible = true;
            //cmbStudentsPage.Enabled = true;
            //cmbStudentPage2.Enabled = true;

            pnlGeneralItems.Visible = true;
        }

        protected void DisplayTheGrid(string LastName, string FirstName, string MiddleName)
        {
            con.Open();
            string sql_LoadGrid = "";
            sql_LoadGrid = "select syscreate as 'TimeOfEntry', activitydescription as 'Activity Description', lastupdatedby as 'LastEditedBy'"
                         + "from Optionsdescription "
                         + "where studentlastname ='" + LastName + "' "
                         + "and studentfirstname = '" + FirstName + "' "
                         + "and studentmiddlename = '" + MiddleName + "' "
                         + "order by syscreate desc ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "OptionsDescription");
            gvStudentHistory.DataSource = ds.Tables[0];
            gvStudentHistory.DataBind();
            con.Close();
        }

        protected void DisplayTheGridFromButton(string LastName, string FirstName, string MiddleName)
        {
            con.Open();
            string sql_LoadGrid = "";
            sql_LoadGrid = "select syscreate as 'TimeOfEntry', activitydescription as 'Activity Description', lastupdatedby as 'LastEditedBy'"
                         + "from Optionsdescription "
                         + "where studentlastname ='" + LastName + "' "
                         + "and studentfirstname = '" + FirstName + "' "
                         + "and studentmiddlename = '" + MiddleName + "' "
                         + "order by syscreate desc ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "OptionsDescription");
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
                         + "from Optionsdescription "
                         + "where studentlastname ='" + Request.QueryString["StudentLastName"] + "' "
                         + "and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                         + "order by syscreate desc ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Optionsdescription");
            gvStudentHistory.DataSource = ds.Tables[0];
            gvStudentHistory.DataBind();
            con.Close();
        }

        protected void RetrieveInformation(string BusOption)
        {
            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;
            string sql = "";

            try
            {
                con.Open();
                
                if (BusOption == "College")
                {
                    sql = "";
                           sql = "Select op.busoption, op.gpa, op.gpadate, op.hstranscript, op.hstranscriptdate, op.interviewed, "
                               + "op.hsgraduation, op.hsgraduationdate, op.driverslicense, op.birthcertificate, op.socialsecuritycard, "
                               + "op.personalSSI, op.paid, op.healthinsurance, op.bankaccount, op.hasgraduated, op.comments, op.syscreate, op.sysupdate, op.lastupdatedby, "
                               + "si.pictureidentification "
                               + "FROM OptionsprogramNEW op "
                               + "LEFT OUTER JOIN StudentInformation si "
                               + "ON (op.StudentLastName = si.lastname AND op.StudentFirstName = si.firstname AND op.StudentMiddleName = si.MiddleName) "
                               + "WHERE dmp.studentlastname=@studentlastname "
                               + "AND dmp.studentfirstname=@studentfirstname "
                               + "AND dmp.studentmiddlename=@studentmiddlename "
                               + "GROUP BY op.studentlastname, op.studentfirstname, op.studentmiddlename ";
                }
                else if (BusOption == "Career")
                {

                }
                

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", Request.QueryString["StudentLastName"]));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", Request.QueryString["StudentFirstName"]));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", Request.QueryString["StudentMiddleName"]));
                
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    if (reader.IsDBNull(0))
                    {
                        //chbGPA.Checked = false;
                    }
                    else
                    {
                        //chbGPA.Checked = reader.GetBoolean(0);
                    }
                    if (reader.IsDBNull(1))
                    {
                        //DATE
                    }
                    else
                    {
                        //dATES = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        //chbGPA.Checked = false;
                    }
                    else
                    {
                        //chbGPA.Checked = reader.GetBoolean(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        //DATE
                    }
                    else
                    {
                        //DATE
                    }
                    if (reader.IsDBNull(4))
                    {
                        //chbGPA.Checked = false;
                    }
                    else
                    {
                        //chbGPA.Checked = reader.GetBoolean(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        //chbHasGraduated.Checked = false;
                    }
                    else
                    {
                        //chbHasGraduated.Checked = reader.GetBoolean(5);
                    }
                    if (reader.IsDBNull(6))
                    {
                        //txbComments.Text = "N/A";
                    }
                    else
                    {
                        //txbComments.Text = reader.GetString(6);
                    }
                    if (reader.IsDBNull(7))
                    {
                        //lblLastUpdatedBy.Text = "LastUpdatedBy:  N/A";
                    }
                    else
                    {
                        //lblLastUpdatedBy.Text = "LastUpdatedBy:  " + reader.GetString(7) + " On: " + reader.GetSqlValue(9).ToString();
                    }
                    if (reader.IsDBNull(10))
                    {
                        //txbPostGraduatePlans.Text = "N/A";
                    }
                    else
                    {
                        //txbPostGraduatePlans.Text = reader.GetString(10);
                    }
                    if (reader.IsDBNull(11))
                    {
                        //chbCollegeFair.Checked = false;
                    }
                    else
                    {
                        //chbCollegeFair.Checked = reader.GetBoolean(11);
                    }
                    if (reader.IsDBNull(12))
                    {
                        //chbFAFSA.Checked = false;
                    }
                    else
                    {
                        //chbFAFSA.Checked = reader.GetBoolean(12);
                    }
                    if (reader.IsDBNull(13))
                    {
                        //chbSAT.Checked = false;
                    }
                    else
                    {
                        //chbSAT.Checked = reader.GetBoolean(13);
                    }
                    if (reader.IsDBNull(14))
                    {
                        //chbACT.Checked = false;
                    }
                    else
                    {
                        //chbACT.Checked = reader.GetBoolean(14);
                    }
                    if (reader.IsDBNull(15))
                    {
                        //txbPostGraduatePlans.Text = "N/A";
                    }
                    else
                    {
                        //txbPostGraduatePlans.Text = reader.GetString(15);
                    }
                    if (reader.IsDBNull(16))
                    {
                        //txbPostGraduatePlans.Text = "N/A";
                    }
                    else
                    {
                        //txbPostGraduatePlans.Text = reader.GetString(16);
                    }
                    if (reader.IsDBNull(17))
                    {
                        //chbPromise.Checked = false;
                    }
                    else
                    {
                        //chbPromise.Checked = reader.GetBoolean(17);
                    }
                    Image1.ImageUrl = reader.GetString(22);
                    Image1.Visible = true;
                    //+ "dmp.postgraduateplans, dmp.collegefair, FAFSA, SAT, ACT, satexamdate, actexamdate   "
                    //imgImage.ImageUrl = reader.GetString(8);
                    //imgImage.Visible = true;
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

        protected void RetrieveInformationFromButton(string LastName, string FirstName, string MiddleName)
        {
            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select op.busoption, op.gpa, op.gpadate, op.hstranscript, op.hstranscriptdate, op.interviewed, "
                           + "op.hsgraduation, op.hsgraduationdate, op.driverslicense, op.birthcertificate, op.socialsecuritycard, "
                           + "op.paid, op.bankaccount, op.comments, op.syscreate, op.sysupdate, op.lastupdatedby, "
                           + "si.pictureidentification "
                           + "FROM OptionsprogramNEW op "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (op.StudentLastName = si.lastname AND op.StudentFirstName = si.firstname AND op.StudentMiddleName = si.MiddleName) "
                           + "WHERE op.studentlastname=@studentlastname "
                           + "AND op.studentfirstname=@studentfirstname "
                           + "AND op.studentmiddlename=@studentmiddlename "
                           + "GROUP BY op.busoption, op.gpa, op.gpadate, op.hstranscript, op.hstranscriptdate, op.interviewed, "
                           + "op.hsgraduation, op.hsgraduationdate, op.driverslicense, op.birthcertificate, op.socialsecuritycard, "
                           + "op.paid, op.bankaccount, op.comments, op.syscreate, op.sysupdate, op.lastupdatedby, "
                           + "si.pictureidentification ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", LastName));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", FirstName));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", MiddleName));

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    if (reader.IsDBNull(0))
                    {
                        ddlBuses.Text = "Select a Bus";
                    }
                    else
                    {
                        ddlBuses.Text = reader.GetString(0);
                    }
                    if (reader.IsDBNull(1))
                    {
                        //HSTranscriptDate.Text = "N/A";
                    }
                    else
                    {
                        //HSTranscriptDate.Text = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        //HSTranscriptDate.Text = "N/A";
                    }
                    else
                    {
                        //HSTranscriptDate.Text = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        chbHSTranscript.Checked = false;
                    }
                    else
                    {
                        chbHSTranscript.Checked = reader.GetBoolean(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        //HSTranscriptDate.Text = "N/A";
                    }
                    else
                    {
                        //HSTranscriptDate.Text = reader.GetString(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        chbInterviewed.Checked = false;
                    }
                    else
                    {
                        chbInterviewed.Checked = reader.GetBoolean(5);
                    }
                    if (reader.IsDBNull(6))
                    {
                        chbHasGraduated.Checked = false;
                    }
                    else
                    {
                        chbHasGraduated.Checked = reader.GetBoolean(6);
                    }
                    if (reader.IsDBNull(7))
                    {
                        //txbGPA.Text = "N/A";
                    }
                    else
                    {
                        //txbGPA.Text = reader.GetString(7);
                    }
                    if (reader.IsDBNull(8))
                    {
                        chbDriversLicense.Checked = false;
                    }
                    else
                    {
                        chbDriversLicense.Checked = reader.GetBoolean(8);
                    }
                    if (reader.IsDBNull(9))
                    {
                        chbBirthCertificate.Checked = false;
                    }
                    else
                    {
                        chbBirthCertificate.Checked = reader.GetBoolean(9);
                    }
                    if (reader.IsDBNull(10))
                    {
                        chbSocSecurityCard.Checked = false;
                    }
                    else
                    {
                        chbSocSecurityCard.Checked = reader.GetBoolean(10);
                    }
                    if (reader.IsDBNull(11))
                    {
                        chbPaid.Checked = false;
                    }
                    else
                    {
                        chbPaid.Checked = reader.GetBoolean(11);
                    }
                    if (reader.IsDBNull(12))
                    {
                        chbBankAccount.Checked = false;
                    }
                    else
                    {
                        chbBankAccount.Checked = reader.GetBoolean(12);
                    }
                    if (reader.IsDBNull(13))
                    {
                        txbComments.Text = "";
                    }
                    else
                    {
                        txbComments.Text = reader.GetString(13);
                    }
                    Image1.ImageUrl = reader.GetString(17);
                    Image1.Visible = true;
                }
            }
            catch (Exception lkjlkj)
            {


            }
            finally
            {
                con.Close();

                if (ddlBuses.Text == "Trade")
                {
                    DisplayTradeTemplate();                   
                }
                else if (ddlBuses.Text == "Career")
                {
                    DisplayCareerTemplate();                   
                }
                else if (ddlBuses.Text == "College")
                {
                    DisplayCollegeTemplate();                   
                }
                else if (ddlBuses.Text == "Military")
                {
                    DisplayMilitaryTemplate();                   
                }
                else if (ddlBuses.Text == "Ministry")
                {
                    DisplayMinistryTemplate();
                }
                else
                {
                    //Do nothing.. No bus has been set for this student!
                    //pnlGeneralItems.Visible = false;
                    UnDisplayAllTemplates();
                }
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CleanCharacters()
        {
            //Strips out all apostrophes from all fields...RCM..3/21/12.

            txbComments.Text = txbComments.Text.Replace("'", "");
            txbNotes.Text = txbNotes.Text.Replace("'", "");
            txbDiscipleshipMentor.Text = txbDiscipleshipMentor.Text.Replace("'", "");
            txbProgramEnrollment.Text = txbProgramEnrollment.Text.Replace("'", "");
        }

        protected void cmbUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CleanCharacters();

                if (cmbUpdate.Text == "Insert New Entry")
                {
                    txbNotes.Text = txbNotes.Text.Replace("'", "");
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
                        txbComments.Enabled = true;
                        chbHasGraduated.Enabled = true;
                    }
                    else
                    {
                        //Insert the new entry into the database table.
                        string sql_InsertNewEntry = "";
                        if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
                        {
                            sql_InsertNewEntry = "INSERT into OptionsDescription "
                                                        + "values ("
                                                        + "'" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim() + "' , "
                                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' , "
                                                        + "'" + txbNotes.Text.Trim() + "' , "
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "') ";
                        }
                        else
                        {
                            sql_InsertNewEntry = "INSERT into OptionsDescription "
                                                        + "values ("
                                                        + "'" + Request.QueryString["StudentLastName"].Trim() + "' , "
                                                        + "'" + Request.QueryString["StudentFirstName"].Trim() + "' , "
                                                        + "'" + txbNotes.Text.Trim() + "' , "
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                        + "'" + Request.QueryString["StudentMiddleName"].Trim() + "') ";
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
                                DisplayTheGridFromButton(ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1));
                            }
                            else
                            {
                                DisplayTheGrid(Request.QueryString["StudentLastName"].Trim(), Request.QueryString["StudentFirstName"].Trim(), Request.QueryString["StudentMiddleName"].Trim());
                            }
                            cmbUpdate.Text = "Update Student Information";
                            cmbUpdate.Enabled = true;

                            //UnLocking the other header columns..RCM..
                            txbStaffCoordinator.Enabled = true;
                            txbDiscipleshipMentor.Enabled = true;
                            txbProgramEnrollment.Enabled = true;
                            txbComments.Enabled = true;
                            chbHasGraduated.Enabled = true;
                            chbFAFSA.Enabled = true;
                            chbACT.Enabled = true;
                            chbSAT.Enabled = true;
                            txbPostGraduatePlans.Enabled = true;
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
                        UpdateGeneralInformation();
                                               
                        //string sqlUpdateStatement = "";

                        //sqlUpdateStatement = " UPDATE OptionsProgram "
                        //+ "SET "
                        //    //+ " studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim() + "' , "
                        //    //+ " studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim() + "' , "
                        //+ " postgraduateplans = '" + txbPostGraduatePlans.Text.Trim() + "' , "
                        //+ " FAFSA = " + Convert.ToInt32(chbFAFSA.Checked) + ", "
                        //+ " SAT = " + Convert.ToInt32(chbSAT.Checked) + ", "
                        //    //+ " SATExamDate = '" + System.DateTime.Now.ToString() + "',"
                        //+ " ACT = " + Convert.ToInt32(chbACT.Checked) + ", "
                        //    //+ " ACTExamDate = '" + System.DateTime.Now.ToString() + "',"
                        //+ " optionsstaffcoordinator = '" + txbStaffCoordinator.Text.Trim() + "' , "
                        //+ " programenrollment = '" + txbProgramEnrollment.Text.Trim() + "' , "
                        //+ " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                        //+ " comments = '" + txbComments.Text.Trim() + "' , "
                        //+ " discipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "' , "
                        //+ " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                        //+ " collegefair = " + Convert.ToInt32(chbCollegeFair.Checked) + ", "
                        //+ " promise = " + Convert.ToInt32(chbPromise.Checked) + ", "
                        //+ " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        //+ "  "
                        //+ " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
                        //+ " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1) + "' ";

                        //con.Open();

                        ////create a SQL command to update record
                        //SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                        //if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                        //{
                        //}
                        //else
                        //{
                        //    //Didn't find a record to update..RCM..
                        //}
                    }
                    catch (Exception lkjlka)
                    {


                    }
                }
            }
            catch (Exception lkjaaabbee)
            {

            }
        }


        protected void UpdateGeneralInformation()
        {
            try
            {
                string sqlUpdateStatement = "";

                sqlUpdateStatement = " UPDATE OptionsProgramNEW "
                + "SET "
                + " busoption = '" + ddlBuses.Text + "', "
                + " interviewed = " + Convert.ToInt32(chbInterviewed.Checked) + ", "
                + " driverslicense = " + Convert.ToInt32(chbDriversLicense.Checked) + ", "
                + " birthcertificate = " + Convert.ToInt32(chbBirthCertificate.Checked) + ", "
                + " socialsecuritycard = " + Convert.ToInt32(chbSocSecurityCard.Checked) + ", "
                + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                + " bankaccount = " + Convert.ToInt32(chbBankAccount.Checked) + ", "
                + " hstranscript = " + Convert.ToInt32(chbHSTranscript.Checked) + ", "
                //+ " hsgraduation = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                //+ " hsgraduation = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                + " "
                //+ " GPADate = '" + System.DateTime.Now.ToString() + "', "
                //+ " GPA = '" + System.DateTime.Now.ToString() + "', "
                //+ " HSTranscriptDate = '" + System.DateTime.Now.ToString() + "', "
                //+ " HSGraduationDate = '" + System.DateTime.Now.ToString() + "', "
                + " "
                + " comments = '" + txbComments.Text.Trim() + "', "
                + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                + "  "
                + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
                + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";

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
            finally
            {
                con.Close();
            }

            try
            {
                string sqlUpdateStatement = "";

                UpdateBusTemplates(ddlBuses.Text);

                //{
                //    UpdateTradeTemplate();
                //}
                //else if (ddlBuses.Text == "Career")
                //{
                //    UpdateCareerTemplate();
                //}
                //else if (ddlBuses.Text == "College")
                //{
                //    UpdateCollegeTemplate();
                //}
                //else if (ddlBuses.Text == "Military")
                //{
                //    UpdateMilitaryTemplate();
                //}
                //else if (ddlBuses.Text == "Ministry")
                //{
                //    //UpdateMinistryTemplate();
                //}
            }
            catch (Exception lkjlkkaabb)
            {

            }
        }


        //protected void UpdateTradeTemplate()
        //{
        //    try
        //    {
        //        string sqlUpdateStatement = "";

        //        sqlUpdateStatement = " UPDATE OptionsTradeTemplate "
        //        + "SET "
        //        + " WatchTradeVideo = " + Convert.ToInt32(chbWatchTradeVideo.Checked) + ", "
        //        + " DrugTest = " + Convert.ToInt32(chbTradeDrugTest.Checked) + ", "
        //        + " CollegeTour = " + Convert.ToInt32(chbTradeCollegeTour.Checked) + ", "
        //        + " PittsburghPromiseEligible = " + Convert.ToInt32(chbPghPromiseEligible.Checked) + ", "
        //        + " FAFSACompleted = " + Convert.ToInt32(chbFAFSACompleted.Checked) + ", "
        //        + " HealthInsurance = " + Convert.ToInt32(chbHealthInsurance.Checked) + ", "
        //        + " "
        //        + " WatchTradeVideoDate = '" + txbWatchVideoDate.Text + "', "
        //        + " DrugTestDate = '" + txbDrugTestDate.Text + "', "
        //        + " CollegeApplicationDate = '" + txbCollegeAppDate.Text + "', "
        //        + " CollegeDeadlineDate = '" + txbCollgDeadlineDate.Text + "', "
        //        + " CollegeVisitationDate = '" + txbCollegeVisitationDate.Text + "', "
        //        + " TradeApplicationDate = '" + txbTradeApplicationDate.Text + "', "
        //        + " ScholarshipGrantLoanDate = '" + txbSchlrshipLoanDate.Text + "', "
        //        + " PittsburghPromiseDate = '" + txbPittsbghPromiseDate.Text + "', "
        //        + " FAFSADate = '" + txbFAFSADate.Text + "', "
        //        + " "
        //        + " CollegeName = '" + txbCollegeVisitationDate.Text + "', "
        //        + " CollegeAcceptanceStatus = '" + ddlCollegeAcceptStatus.Text + "', "
        //        + " TradeAccepted = '" + txbTradeAccepted.Text + "', "
        //        + " ScholarshipGrantLoanAmount = '" + txbFAFSADate.Text + "', "
        //        + " Comments = '" + txbComments.Text + "', "
        //        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
        //        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
        //        + "  "
        //        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
        //        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
        //        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";

        //        con.Open();

        //        //create a SQL command to update record
        //        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
        //        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
        //        {
        //        }
        //        else
        //        {
        //            //Didn't find a record to update..RCM..
        //        }
        //    }
        //    catch (Exception lkjlka)
        //    {

        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //protected void UpdateCollegeTemplate()
        //{
        //    try
        //    {
        //        string sqlUpdateStatement = "";

        //        sqlUpdateStatement = " UPDATE OptionsCollegeTemplate "
        //        + "SET "
        //        //+ " WatchTradeVideo = " + Convert.ToInt32(chbWatchTradeVideo.Checked) + ", "
        //        //+ " DrugTest = " + Convert.ToInt32(chbTradeDrugTest.Checked) + ", "
        //        //+ " CollegeTour = " + Convert.ToInt32(chbTradeCollegeTour.Checked) + ", "
        //        //+ " PittsburghPromiseEligible = " + Convert.ToInt32(chbPghPromiseEligible.Checked) + ", "
        //        //+ " FAFSACompleted = " + Convert.ToInt32(chbFAFSACompleted.Checked) + ", "
        //        //+ " HealthInsurance = " + Convert.ToInt32(chbHealthInsurance.Checked) + ", "
        //        //+ " "
        //        //+ " WatchTradeVideoDate = '" + txbWatchVideoDate.Text + "', "
        //        //+ " DrugTestDate = '" + txbDrugTestDate.Text + "', "
        //        //+ " CollegeApplicationDate = '" + txbCollegeAppDate.Text + "', "
        //        //+ " CollegeDeadlineDate = '" + txbCollgDeadlineDate.Text + "', "
        //        //+ " CollegeVisitationDate = '" + txbCollegeVisitationDate.Text + "', "
        //        //+ " TradeApplicationDate = '" + txbTradeApplicationDate.Text + "', "
        //        //+ " ScholarshipGrantLoanDate = '" + txbSchlrshipLoanDate.Text + "', "
        //        //+ " PittsburghPromiseDate = '" + txbPittsbghPromiseDate.Text + "', "
        //        //+ " FAFSADate = '" + txbFAFSADate.Text + "', "
        //        //+ " "
        //        //+ " CollegeName = '" + txbCollegeVisitationDate.Text + "', "
        //        //+ " CollegeAcceptanceStatus = '" + ddlCollegeAcceptStatus.Text + "', "
        //        //+ " TradeAccepted = '" + txbTradeAccepted.Text + "', "
        //        //+ " ScholarshipGrantLoanAmount = '" + txbFAFSADate.Text + "', "
        //        //+ " Comments = '" + txbComments.Text + "', "
        //        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
        //        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
        //        + "  "
        //        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
        //        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
        //        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";

        //        con.Open();

        //        //create a SQL command to update record
        //        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
        //        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
        //        {
        //        }
        //        else
        //        {
        //            //Didn't find a record to update..RCM..
        //        }
        //    }
        //    catch (Exception lkjlka)
        //    {

        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //protected void UpdateMilitaryTemplate()
        //{
        //    try
        //    {
        //        string sqlUpdateStatement = "";

        //        sqlUpdateStatement = " UPDATE OptionsMilitaryTemplate "
        //        + "SET "
        //        + " Citizenship = " + Convert.ToInt32(chbCitizenship.Checked) + ", "
        //        + " CitizenshipDate = '" + calDrugTestDate.SelectedDate

                
        //        //+ " WatchTradeVideo = " + Convert.ToInt32(chbWatchTradeVideo.Checked) + ", "
        //            //+ " DrugTest = " + Convert.ToInt32(chbTradeDrugTest.Checked) + ", "
        //            //+ " CollegeTour = " + Convert.ToInt32(chbTradeCollegeTour.Checked) + ", "
        //            //+ " PittsburghPromiseEligible = " + Convert.ToInt32(chbPghPromiseEligible.Checked) + ", "
        //            //+ " FAFSACompleted = " + Convert.ToInt32(chbFAFSACompleted.Checked) + ", "
        //            //+ " HealthInsurance = " + Convert.ToInt32(chbHealthInsurance.Checked) + ", "
        //            //+ " "
        //            //+ " WatchTradeVideoDate = '" + txbWatchVideoDate.Text + "', "
        //            //+ " DrugTestDate = '" + txbDrugTestDate.Text + "', "
        //            //+ " CollegeApplicationDate = '" + txbCollegeAppDate.Text + "', "
        //            //+ " CollegeDeadlineDate = '" + txbCollgDeadlineDate.Text + "', "
        //            //+ " CollegeVisitationDate = '" + txbCollegeVisitationDate.Text + "', "
        //            //+ " TradeApplicationDate = '" + txbTradeApplicationDate.Text + "', "
        //            //+ " ScholarshipGrantLoanDate = '" + txbSchlrshipLoanDate.Text + "', "
        //            //+ " PittsburghPromiseDate = '" + txbPittsbghPromiseDate.Text + "', "
        //            //+ " FAFSADate = '" + txbFAFSADate.Text + "', "
        //            //+ " "
        //            //+ " CollegeName = '" + txbCollegeVisitationDate.Text + "', "
        //            //+ " CollegeAcceptanceStatus = '" + ddlCollegeAcceptStatus.Text + "', "
        //            //+ " TradeAccepted = '" + txbTradeAccepted.Text + "', "
        //            //+ " ScholarshipGrantLoanAmount = '" + txbFAFSADate.Text + "', "
        //            //+ " Comments = '" + txbComments.Text + "', "
                
                
        //        + "Citizenship = 0, "//Citizenship
        //        + "Null, "//CitizenshipDate
        //        + "0, "//ASVABPreparation
        //        + "Null, "//ASVABPreparationDate
        //        + "0, "//MedicalHistoryReview
        //        + "Null, "//MedicalHistoryReviewDate
        //        + "0, "//LegalHistoryReview
        //        + "Null, "//LegalHistoryReviewDate
        //        + "'N/A', "//RecruiterAppointmentBranch
        //        + "Null, "//RecruiterAppointmentDate
        //        + "'N/A', "//CareerOption1
        //        + "'N/A', "//CareerOption1Notes
        //        + "'N/A', "//CareerOption2
        //        + "'N/A', "//CareerOption2Notes
        //        + "'N/A', "//CareerOption3
        //        + "'N/A', "//CareerOption3Notes
        //        + "0, "//MEPSPreparation
        //        + "Null, "//MEPSPreparationDate
        //        + "0, "//MEPSAppointment
        //        + "'N/A', "//MEPSAppointmentBranch
        //        + "Null, "//MEPSAppointmentAcceptance
        //        + "'N/A', "//CareerAssigned
        //        + "'N/A', "//EnlistmentService
        //        + "Null, "//EnlistmentDate
        //        + "Null, "//BasicTrainingDeparture
        //        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
        //        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
        //        + "  "
        //        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
        //        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
        //        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";

        //        con.Open();

        //        //create a SQL command to update record
        //        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
        //        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
        //        {
        //        }
        //        else
        //        {
        //            //Didn't find a record to update..RCM..
        //        }
        //    }
        //    catch (Exception lkjlka)
        //    {

        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //protected void UpdateMinistryTemplate()
        //{
        //    try
        //    {
        //        string sqlUpdateStatement = "";

        //        sqlUpdateStatement = " UPDATE OptionsMinistryTemplate "
        //        + "SET "
        //        //+ " WatchTradeVideo = " + Convert.ToInt32(chbWatchTradeVideo.Checked) + ", "
        //        //+ " DrugTest = " + Convert.ToInt32(chbTradeDrugTest.Checked) + ", "
        //        //+ " CollegeTour = " + Convert.ToInt32(chbTradeCollegeTour.Checked) + ", "
        //        //+ " PittsburghPromiseEligible = " + Convert.ToInt32(chbPghPromiseEligible.Checked) + ", "
        //        //+ " FAFSACompleted = " + Convert.ToInt32(chbFAFSACompleted.Checked) + ", "
        //        //+ " HealthInsurance = " + Convert.ToInt32(chbHealthInsurance.Checked) + ", "
        //        //+ " "
        //        //+ " WatchTradeVideoDate = '" + txbWatchVideoDate.Text + "', "
        //        //+ " DrugTestDate = '" + txbDrugTestDate.Text + "', "
        //        //+ " CollegeApplicationDate = '" + txbCollegeAppDate.Text + "', "
        //        //+ " CollegeDeadlineDate = '" + txbCollgDeadlineDate.Text + "', "
        //        //+ " CollegeVisitationDate = '" + txbCollegeVisitationDate.Text + "', "
        //        //+ " TradeApplicationDate = '" + txbTradeApplicationDate.Text + "', "
        //        //+ " ScholarshipGrantLoanDate = '" + txbSchlrshipLoanDate.Text + "', "
        //        //+ " PittsburghPromiseDate = '" + txbPittsbghPromiseDate.Text + "', "
        //        //+ " FAFSADate = '" + txbFAFSADate.Text + "', "
        //        //+ " "
        //        //+ " CollegeName = '" + txbCollegeVisitationDate.Text + "', "
        //        //+ " CollegeAcceptanceStatus = '" + ddlCollegeAcceptStatus.Text + "', "
        //        //+ " TradeAccepted = '" + txbTradeAccepted.Text + "', "
        //        //+ " ScholarshipGrantLoanAmount = '" + txbFAFSADate.Text + "', "
        //        //+ " Comments = '" + txbComments.Text + "', "
        //        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
        //        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
        //        + "  "
        //        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
        //        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
        //        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";

        //        con.Open();

        //        //create a SQL command to update record
        //        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
        //        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
        //        {
        //        }
        //        else
        //        {
        //            //Didn't find a record to update..RCM..
        //        }
        //    }
        //    catch (Exception lkjlka)
        //    {

        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //protected void UpdateCareerTemplate()
        //{
        //    try
        //    {
        //        string sqlUpdateStatement = "";

        //        sqlUpdateStatement = " UPDATE OptionsCareerTemplate "
        //        + "SET "
        //        //+ " WatchTradeVideo = " + Convert.ToInt32(chbWatchTradeVideo.Checked) + ", "
        //        //+ " DrugTest = " + Convert.ToInt32(chbTradeDrugTest.Checked) + ", "
        //        //+ " CollegeTour = " + Convert.ToInt32(chbTradeCollegeTour.Checked) + ", "
        //        //+ " PittsburghPromiseEligible = " + Convert.ToInt32(chbPghPromiseEligible.Checked) + ", "
        //        //+ " FAFSACompleted = " + Convert.ToInt32(chbFAFSACompleted.Checked) + ", "
        //        //+ " HealthInsurance = " + Convert.ToInt32(chbHealthInsurance.Checked) + ", "
        //        //+ " "
        //        //+ " WatchTradeVideoDate = '" + txbWatchVideoDate.Text + "', "
        //        //+ " DrugTestDate = '" + txbDrugTestDate.Text + "', "
        //        //+ " CollegeApplicationDate = '" + txbCollegeAppDate.Text + "', "
        //        //+ " CollegeDeadlineDate = '" + txbCollgDeadlineDate.Text + "', "
        //        //+ " CollegeVisitationDate = '" + txbCollegeVisitationDate.Text + "', "
        //        //+ " TradeApplicationDate = '" + txbTradeApplicationDate.Text + "', "
        //        //+ " ScholarshipGrantLoanDate = '" + txbSchlrshipLoanDate.Text + "', "
        //        //+ " PittsburghPromiseDate = '" + txbPittsbghPromiseDate.Text + "', "
        //        //+ " FAFSADate = '" + txbFAFSADate.Text + "', "
        //        //+ " "
        //        //+ " CollegeName = '" + txbCollegeVisitationDate.Text + "', "
        //        //+ " CollegeAcceptanceStatus = '" + ddlCollegeAcceptStatus.Text + "', "
        //        //+ " TradeAccepted = '" + txbTradeAccepted.Text + "', "
        //        //+ " ScholarshipGrantLoanAmount = '" + txbFAFSADate.Text + "', "
        //        //+ " Comments = '" + txbComments.Text + "', "
        //        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
        //        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
        //        + "  "
        //        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
        //        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
        //        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";

        //        con.Open();

        //        //create a SQL command to update record
        //        SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
        //        if (sqlUpdateCommand.ExecuteNonQuery() > 0)
        //        {
        //        }
        //        else
        //        {
        //            //Didn't find a record to update..RCM..
        //        }
        //    }
        //    catch (Exception lkjlka)
        //    {

        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}



        protected void UpdateBusTemplates(string Bus)
        {
            try
            {
                string sqlUpdateStatement = "";

                if (Bus == "Career")
                {
                    sqlUpdateStatement = " UPDATE OptionsCareerTemplate "
                        + "SET "
                        + " BibleStudy = " + Convert.ToInt32(chbCareerBibleStudy.Checked) + ", "
                        + " BibleStudyDate = '" + txbCareerBibleStudyDate.Text + "', "
                        + " HSHonorsList = " + Convert.ToInt32(chbCareerHonorsList.Checked) + ", "
                        + " HSHonorsListDate = '" + txbCareerHSHonorsListDate.Text + "', "
                        + " HSLeadershipList = " + Convert.ToInt32(chbCareerHSLeadershipList.Checked) + ", "
                        + " HSLeadershipListDate = '" + txbCareerHSLeadershipListDate.Text + "', "
                        + " CommunityActivitiesList = " + Convert.ToInt32(chbCareerCommunityActivitiesList.Checked) + ", "
                        + " CommunityActivitiesDate = '" + txbCareerCommunityActivitiesListDate.Text + "', "
                        + " ResumeCompleted = " + Convert.ToInt32(chbCareerResumeCompleted.Checked) + ", "
                        + " ResumeCompletedDate = '" + txbCareerResumeCompletedDate.Text + "', "
                        + " InterviewPreparation = " + Convert.ToInt32(chbCareerInterviewPreparation.Checked) + ", "
                        + " InterviewPreparationDate = '" + txbCareerInterviewPreparationDate.Text + "', "
                        + " InterviewScheduledDate = '" + txbCareerInterviewScheduledDate.Text + "', "
                        + " InterviewScheduledAccepted = " + Convert.ToInt32(chbCareerInterviewScheduleAccepted.Checked) + ", "
                        + " ApplicationSubmittedDate = '" + txbCareerApplicationSubmittedDate.Text + "', "
                        //+ " ApplicationSubmittedCompany = '" + txb//txbCareerApplicationSubmittedCompany.Text + "', "
                        //+ " ChurchAffiliation = '" + //txbCareerChurchAffiliation.Text + "', "
                        //+ " ChurchActivityDescription = '" + txbCareerChurchActivityDescription.Text + "', "
                        //+ " ChurchActivityDate = '" + txbCareerChurchActivityDate.Text + "', "
                        //+ " MentorName = '" + txbCareerMentorName.Text + "', "
                        //+ " MentorDate = '" + txbCareerMentorDate.Text + "', "
                        //+ " MinistryVolunteerOrganization = '" + txbCareerMinistryVolunteerOrganization.Text + "', "
                        //+ " MinistryVolunteerOrganizationDate = '" + txbCareerMinistryVolunteerOrganizationDate.Text + "', "
                        //+ " InternshipOrganization = '" + txbCareerInternshipOrganization.Text + "', "
                        //+ " InternshipOrganizationDate = '" + txbCareerInternshipOrganizationDate.Text + "', "
                        //+ " MissionTripLocation = '" + txbCareerMissionTripLocation.Text + "', "
                        //+ " MissionTripLocationDate = '" + txbCareerMissionTripLocationDate.Text + "', "
                        + " Participation = " + Convert.ToInt32(chbCareerParticipation.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
                        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                }
                else if (Bus == "Trade")
                {
                        sqlUpdateStatement = " UPDATE OptionsTradeTemplate "
                        + "SET "
                        + " WatchTradeVideo = " + Convert.ToInt32(chbWatchTradeVideo.Checked) + ", "
                        + " DrugTest = " + Convert.ToInt32(chbTradeDrugTest.Checked) + ", "
                        + " CollegeTour = " + Convert.ToInt32(chbTradeCollegeTour.Checked) + ", "
                        + " PittsburghPromiseEligible = " + Convert.ToInt32(chbPghPromiseEligible.Checked) + ", "
                        + " FAFSACompleted = " + Convert.ToInt32(chbFAFSACompleted.Checked) + ", "
                        + " HealthInsurance = " + Convert.ToInt32(chbHealthInsurance.Checked) + ", "
                        + " "
                        + " WatchTradeVideoDate = '" + txbWatchVideoDate.Text + "', "
                        + " DrugTestDate = '" + txbDrugTestDate.Text + "', "
                        + " CollegeApplicationDate = '" + txbCollegeAppDate.Text + "', "
                        + " CollegeDeadlineDate = '" + txbCollgDeadlineDate.Text + "', "
                        + " CollegeVisitationDate = '" + txbCollegeVisitationDate.Text + "', "
                        + " TradeApplicationDate = '" + txbTradeApplicationDate.Text + "', "
                        + " ScholarshipGrantLoanDate = '" + txbSchlrshipLoanDate.Text + "', "
                        + " PittsburghPromiseDate = '" + txbPittsbghPromiseDate.Text + "', "
                        //+ " FAFSADate = '" + txbFAFSADate.Text + "', "
                        + " "
                        + " CollegeName = '" + txbCollegeVisitationDate.Text + "', "
                        + " CollegeAcceptanceStatus = '" + ddlCollegeAcceptStatus.Text + "', "
                        + " TradeAccepted = '" + txbTradeAccepted.Text + "', "
                        + " ScholarshipGrantLoanAmount = '" + txbCollegeScholarshipGrantLoanDate.Text + "', "
                        + " Comments = '" + txbComments.Text + "', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
                        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                }
                else if (Bus == "College")
                {
                    sqlUpdateStatement = " UPDATE OptionsCollegeTemplate "
                    + "SET "
                        + " HealthInsurance = " + Convert.ToInt32(chbCollegeHealthInsurance.Checked) + ", "
                        + " SAT = " + Convert.ToInt32(chbCollegeSAT.Checked) + ", "
                        + " SATReadingScore = '" + txbCollegeSATReadingScore.Text + "', "
                        + " SATMathScore = '" + txbCollegeSATMathScore.Text + "', "
                        + " SATWritingScore = '" + txbCollegeSATWritingScore.Text + "', "
                        + " SATTotalScore = '" + txbCollegeSATTotalScore.Text + "', "
                        + " SATExamDate = '" + txbCollegeSATExamDate.Text + "', "
                        + " ACT = " + Convert.ToInt32(chbCollegeACT.Checked) + ", "
                        + " ACTEnglishScore = '" + txbCollegeACTEnglishScore.Text + "', "
                        + " ACTReadingScore = '" + txbCollegeACTReadingScore.Text + "', "
                        + " ACTMathScore = '" + txbCollegeACTMathScore.Text + "', "
                        + " ACTCompositeScore = '" + txbCollegeACTCompositeScore.Text + "', "
                        + " ACTScienceScore = '" + txbCollegeACTScienceScore.Text + "', "
                        + " ACTTotalScore = '" + txbCollegeACTTotalScore.Text + "', "
                        + " ACTExamDate = '" + txbCollegeACTExamDate.Text + "', "
                        + " FAFSACompleted = " + Convert.ToInt32(chbCollegeFAFSACompleted.Checked) + ", "
                        + " FAFSADate = '" + txbCollegeFAFSADate.Text + "', "
                        + " CollegeApplicationAccepted = " + Convert.ToInt32(chbCollegeApplicationAccepted.Checked) + ", "
                        //+ " CollegeApplicationDate = " + Convert.ToInt32(chbPghPromiseEligible.Checked) + ", "
                        //+ " CollegeTourSchool = " + Convert.ToInt32(chbco//chbCollegeTourSchool.Checked) + ", "
                        + " CollegeDate = '" + txbCollegeVisitationDate.Text + "', "
                        + " PittsburghPromiseEligible = " + Convert.ToInt32(chbCollegePittsburghPromiseEligible.Checked) + ", "
                        + " PittsburghPromiseDate = '" + txbCollegePittsburghPromiseDate.Text + "', "
                        + " ScholarshipGrantLoan = '" + txbCollegeFAFSADate.Text + "', "
                        + " ScholarshipGrantLoanDate = '" + txbCollegeScholarshipGrantLoanDate.Text + "', "
                        //+ " NCAAEligibilityCenter = " + Convert.ToInt32(//chbCollegeNCAAEligibilityCenter.Checked) + ", "
                        + " NCAAElgibilityDate = '" + txbCollegeNCAAEligiblityDate.Text + "', "
                        + " GameFilm = " + Convert.ToInt32(chbCollegeGameFilm.Checked) + ", "
                        + " GameFilmDate = '" + txbCollegeGameFilmDate.Text + "', "
                        + " AuditionPortfolio = " + Convert.ToInt32(chbCollegeAuditionPortfolio.Checked) + ", "
                        + " AuditionPortfolioDate = '" + txbCollegeAuditionPortfolioDate.Text + "', "
                        //+ " Comments = '" + txbComments.Text + "', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
                        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                }
                else if (Bus == "Ministry")
                {
                    sqlUpdateStatement = " UPDATE OptionsMinistryTemplate "
                    + "SET "
                        + " ChurchAffiliation = '" + txbMinistryChurchAffiliation.Text + "', "
                        + " ChurchActivityDescription = '" + txbMinistryChurchActivityDescription.Text + "', "
                        + " ChurchActivityDate = '" + txbMinistryChurchActivityDate.Text + "', "
                        + " BibleStudy = " + Convert.ToInt32(chbMinistryBibleStudy.Checked) + ", "
                        + " BibleStudyDate = '" + txbMinistryBibleStudyDate.Text + "', "
                        + " MentorName = '" + txbMinistryMentorName.Text + "', "
                        + " MentorDate = '" + txbMinistryMentorDate.Text + "', "
                        + " MinistryVolunteerOrganization = '" + txbMinistryVolunteerOrganization.Text + "', "
                        + " MinistryVolunteerOrganizationDate = '" + txbMinistryVolunteerOrganizationDate.Text + "', "
                        + " InternshipOrganization = '" + txbMinistryInternshipOrganization.Text + "', "
                        + " InternshipOrganizationDate = '" + txbMinistryInternshipOrganizationDate.Text + "', "
                        + " MissionTripLocation = '" + txbMinistryMissionTripLocation.Text + "', "
                        + " MissionTripLocationDate = '" + txbMinistryMissionTripLocationDate.Text + "', "
                        + " Participation = " + Convert.ToInt32(chbMinistryParticipation.Checked) + ", "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
                        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                        + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                }
                else if (Bus == "Military")
                {
                    sqlUpdateStatement = " UPDATE OptionsMilitaryTemplate "
                    + "SET "
                    + " Citizenship = " + Convert.ToInt32(chbMilitaryCitizenship.Checked) + ", "
                    + " CitizenshipDate = '" + txbMilitaryCitizenshipDate.Text + "', "
                    + " ASVABPreparation = " + Convert.ToInt32(chbMilitaryASVABPreparation.Checked) + ", "
                    + " ASVABPreparationDate = '" + txbMilitaryASVABPreparationDate.Text + "', "
                    + " MedicalHistoryReview = " + Convert.ToInt32(chbMilitaryMedicalHistoryReview.Checked) + ", "
                    + " MedicalHistoryReviewDate = '" + txbMilitaryMedicalHistoryReviewDate.Text + "', "
                    + " LegalHistoryReview = " + Convert.ToInt32(chbMilitaryLegalHistoryReview.Checked) + ", "
                    + " LegalHistoryReviewDate = '" + txbMilitaryLegalHistoryReviewDate.Text + "', "
                    + " RecruiterAppointmentBranch = '', " 
                    + " RecruiterAppointmentDate = '" + txbMilitaryRecruiterAppointmentDate.Text + "', "
                    + " CareerOption1 = '', " //txbWatchVideoDate.Text + "', "
                    + " CareerOption1Notes = '" + txbWatchVideoDate.Text + "', "
                    + " CareerOption2 = '" + txbWatchVideoDate.Text + "', "
                    + " CareerOption2Notes = '" + txbWatchVideoDate.Text + "', "
                    + " CareerOption3 = '" + txbWatchVideoDate.Text + "', "
                    + " CareerOption3Notes = '" + txbWatchVideoDate.Text + "', "
                    + " MEPSPreparation = " + Convert.ToInt32(chbMilitaryMEPSPreparation.Checked) + ", "
                    + " MEPSPreparationDate = '" + txbMilitaryMEPSPreparationDate.Text + "', "
                    + " MEPSAppointment = '', " //Convert.ToInt32(chbMilitaryMEPSAppointment.Checked) + ", "
                    + " MEPSAppointmentBranch = '" + txbMilitaryMEPSAppointmentBranch.Text + "', "
                    + " MEPSAppointmentAcceptance = '" + txbMilitaryMEPSAppointmentAcceptance.Text + "', "
                    + " CareerAssigned = '" + txbMilitaryCareerAssigned.Text + "', "
                    + " EnlistmentService = '" + txbMilitaryEnlistmentService.Text + "', "
                    + " EnlistmentDate = '" + txbMilitaryEnlistmentDate.Text + "', "
                    + " BasicTrainingDeparture = '" + txbMilitaryBasicTrainingDeparture.Text + "', "
                    + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                    + "  "
                    + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
                    + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                    + " AND studentmiddlename = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
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
            finally
            {
                con.Close();
            }
        }


        protected void UpdateStudentInformation()
        {
            try
            {
                CleanCharacters();

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
                            sql_InsertNewEntry = "INSERT into OptionsDescription "
                                                        + "values ("
                                                        + "'" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim() + "' , "
                                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim() + "' , "
                                                        + "'" + txbNotes.Text.Trim() + "' , "
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                        }
                        else
                        {
                            sql_InsertNewEntry = "INSERT into OptionsDescription "
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
                                //DisplayTheGridFromButton();
                            }
                            else
                            {
                                //DisplayTheGrid();
                            }
                            cmbUpdate.Text = "Update Student Information";
                            cmbUpdate.Enabled = true;

                            //UnLocking the other header columns..RCM..
                            txbStaffCoordinator.Enabled = true;
                            txbDiscipleshipMentor.Enabled = true;
                            txbProgramEnrollment.Enabled = true;
                            txbComments.Enabled = true;
                            chbHasGraduated.Enabled = true;
                            chbFAFSA.Enabled = true;
                            chbACT.Enabled = true;
                            chbSAT.Enabled = true;
                            txbPostGraduatePlans.Enabled = true;
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

                        sqlUpdateStatement = " UPDATE OptionsProgramNEW "
                        + "SET "
                        + " busoption = '" + ddlBuses.Text + "', "
                        + " interviewed = " + Convert.ToInt32(chbInterviewed.Checked) + ", "
                        + " driverslicense = " + Convert.ToInt32(chbDriversLicense.Checked) + ", "
                        + " birthcertificate = " + Convert.ToInt32(chbBirthCertificate.Checked) + ", "
                        + " socialsecuritycard = " + Convert.ToInt32(chbSocSecurityCard.Checked) + ", "
                        + " paid = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        + " bankaccount = " + Convert.ToInt32(chbPaid.Checked) + ", "
                        + " hstranscript = " + Convert.ToInt32(chbHSTranscript.Checked) + ", "
                        + " hsgraduation = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "

                        
                        
                        + " comments = '" + txbComments.Text.Trim() + "', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + "  "
                        + " WHERE studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "' "
                        + " AND studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1) + "' ";

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
            catch (Exception lkjaaabbee)
            {

            }
            finally
            {

            }
        }

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
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

        protected void txbDiscipleshipmentor_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbOptions.Enabled = true;
            //cmbUpdate.Enabled = false;
        }

        protected void gvStudentHistory_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void lbAddNewEntry_Click(object sender, EventArgs e)
        {
            txbNotes.Visible = true;
            txbNotes.Style.Add("z-index", "99999");
            txbNotes.Text = "(Type new activity entry here!)";
            cmbUpdate.Text = "Insert New Entry";
            cmbUpdate.Enabled = true;
            lblNewEntry.Visible = true;

            //Locking the other header columns..RCM..
            txbStaffCoordinator.Enabled = false;
            txbDiscipleshipMentor.Enabled = false;
            txbProgramEnrollment.Enabled = false;
            txbComments.Enabled = false;
            chbHasGraduated.Enabled = false;
            txbPostGraduatePlans.Enabled = false;
            chbACT.Enabled = false;
            chbSAT.Enabled = false;
            chbFAFSA.Enabled = false;
        }

        protected void txbNotes_TextChanged(object sender, EventArgs e)
        {
            //txbNotes.Text = "";
            //cmbUpdate.Enabled = true;
        }

        protected void cmbOptions_Click1(object sender, EventArgs e)
        {
            //Special to retrieve information differently.

            RetrieveInformationFromButton(ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1));

            DisplayTheGridFromButton(ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1));
            
            cmbUpdate.Enabled = true;
            lblNotes.Visible = true;
            lbAddNewEntry.Visible = true;
            DisplayHeaderFields();
        }

        protected void txbDiscipleshipMentor_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmbStudentsPage_Click(object sender, EventArgs e)
        {
            Response.Clear();
            if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim() + "&StudentFirstName=" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim());
            }
            else
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + Request.QueryString["StudentLastName"] + "&StudentFirstName=" + Request.QueryString["StudentFirstName"]);
            }
        }

        protected void cmbComprehensiveReport_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPage();
                con.Open();

                string sql_LoadGrid = "select op.studentlastname as 'LastName', op.studentfirstname as 'FirstName', op.HasGraduated as 'Graduated', pas.grade as 'Grade', "
                                    + "pas.homephone as 'HomePhone', pas.school as 'School', op.promise as 'Promise',op.SAT as 'SAT', op.FAFSA as 'FAFSA', op.ACT as 'ACT', op.Postgraduateplans, op.comments, op.CollegeFair as 'CollegeFair',  "
                                    + "op.OptionsStaffCoordinator, op.Discipleshipmentor as 'DiscipleShipMentor' "
                                    + "from OptionsProgram op "
                                    + "LEFT OUTER JOIN PerformingArtsAcademyStudents pas "
                                    + "ON (op.studentlastname = pas.lastname AND op.studentfirstname = pas.firstname) "
                                    + "ORDER BY pas.grade, op.studentlastname, op.studentfirstname "; 

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "OptionsProgram");
                gvOptionsComprehensive.DataSource = ds.Tables[0];
                gvOptionsComprehensive.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }

        protected void ClearPage()
        {
            txbComments.Visible = false;
            txbDiscipleshipMentor.Visible = false;
            txbPostGraduatePlans.Visible = false;
            txbComments.Visible = false;
            txbNotes.Visible = false;
            txbStaffCoordinator.Visible = false;
            gvStudentHistory.Visible = false;
            chbACT.Visible = false;
            chbSAT.Visible = false;
            chbFAFSA.Visible = false;
            chbHasGraduated.Visible = false;
            lbAddNewEntry.Visible = false;
            lblComments.Visible = false;
            lblDiscipleMentor.Visible = false;
            lblNewEntry.Visible = false;
            lblProgramEnrollment.Visible = false;
            lblStaffCoordinator.Visible = false;
            lblPostGraduatePlans.Visible = false;
            txbProgramEnrollment.Visible = false;
            cmbUpdate.Visible = false;
            lblEnrolledStudents.Visible = false;
            ddlOptions.Visible = false;
            cmbOptions.Visible = false;
            Image1.Visible = false;
            lblNotes.Visible = false;
            chbPromise.Visible = false;
            chbCollegeFair.Visible = false;
        }      

        protected void ddlOptions_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cmbOptions.Enabled = true;
            cmbUpdate.Enabled = false;
            //cmbStudentsPage.Enabled = true;
            cmbStudentPage2.Enabled = true;

            RetrieveInformationFromButton(ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1,  ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1));

            if (ddlBuses.Text != "Select a Bus")
            {
                DisplayTheGridFromButton(ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1));
                cmbUpdate.Enabled = true;
                lblNotes.Visible = true;
                lbAddNewEntry.Visible = true;
                ddlBuses.Visible = true;
                DisplayHeaderFields();
                ddlReports.Visible = true;
                lblReports.Visible = true;
            }
            else
            {
                DisplayTheGridFromButton(ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))), ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1));
                cmbUpdate.Enabled = true;
                lblNotes.Visible = true;
                lbAddNewEntry.Visible = true;
                ddlBuses.Visible = true;
                pnlGeneralItems.Visible = false;
                DisplayPartialHeaderFields();
                //DisplayHeaderFields();
                ddlReports.Visible = true;
                lblReports.Visible = true;
            }
        }

        protected void RetrieveOptionsInformation()
        {
            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select op.studentlastname, op.studentfirstname, op.studentmiddlename, op.gpa, op.hstranscript, op.hstranscriptdate,  "
                           + "op.hsgraduation, op.hsgraduationdate, op.fafsa, op.fafsadate, op.pghpromiseeligible, op.driverslicense, op.birthcertificate, "
                           + "op.personalSSI, op.paid, op.healthinsurance, op.bankaccount, op.hasgraduated, op.comments, op.syscreate, op.sysupdate, op.lastupdatedby "
                           + "FROM OptionsprogramNEW op "
                           + "WHERE dmp.studentlastname=@studentlastname "
                           + "AND dmp.studentfirstname=@studentfirstname "
                           + "AND dmp.studentmiddlename=@studentmiddlename "
                           + "GROUP BY dmp.Bus ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim()));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim()));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim()));

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

                    }
                }
                else
                {
                    //Not been assigned to a bus..  So force the user to select a bus before proceeding...RCM..
                }
            }
            catch (Exception lkjlk)
            {

            }
        }


        protected void RetrieveBusInformation(string Bus)
        {
            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select  "
                           + "FROM Options" + Bus + "Template ot "
                           + "WHERE dmp.studentlastname=@studentlastname "
                           + "AND dmp.studentfirstname=@studentfirstname "
                           + "GROUP BY dmp.Bus ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim()));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim()));

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
                        ddlBuses.Text = reader.GetString(0);
                        if (ddlBuses.Text == "Military")
                        {
                            DisplayMilitaryTemplate();
                        }
                        else if (ddlBuses.Text == "Ministry")
                        {
                            DisplayMinistryTemplate();
                        }
                        else if (ddlBuses.Text == "Trade")
                        {
                            DisplayTradeTemplate();
                        }
                        else if (ddlBuses.Text == "Career")
                        {
                            DisplayCareerTemplate();
                        }
                        else if (ddlBuses.Text == "College")
                        {
                            DisplayCollegeTemplate();
                        }
                        else
                        {
                            //Do Nothing..force them to choose a bus option..RCM..
                        }
                    }
                }
                else
                {
                    //Not been assigned to a bus..  So force the user to select a bus before proceeding...RCM..
                }
            }
            catch (Exception lkjlk)
            {

            }
        }

        protected void DisplayMinistryTemplate()
        {
            lblBusInformation.Text = "Ministry Bus Information";
            lblBusInformation.Visible = true;

            chbMinistryBibleStudy.Visible = true;
            chbMinistryParticipation.Visible = true;
            chbMinistryChurchActivityDate.Visible = true;
            chbMinistryInternshipOrganizationDate.Visible = true;
            chbMinistryMentorDate.Visible = true;
            chbMinistryMissionTripLocationDate.Visible = true;
            chbMinistryVolunteerOrganizationDate.Visible = true;

            txbMinistryBibleStudyDate.Visible = true;
            txbMinistryChurchActivityDate.Visible = true;
            txbMinistryChurchAffiliation.Visible = true;
            txbMinistryInternshipOrganization.Visible = true;
            txbMinistryInternshipOrganizationDate.Visible = true;
            txbMinistryMentorDate.Visible = true;
            txbMinistryMentorName.Visible = true;
            txbMinistryMissionTripLocation.Visible = true;
            txbMinistryMissionTripLocationDate.Visible = true;
            txbMinistryParticipation.Visible = true;
            txbMinistryVolunteerOrganization.Visible = true;
            txbMinistryVolunteerOrganizationDate.Visible = true;
            txbMinistryParticipation.Visible = true;

            imbMinistryBibleStudyDate.Visible = true;
            imbMinistryChurchActivityDate.Visible = true;
            imbMinistryInternshipOrganizationDate.Visible = true;
            imbMinistryMentorDate.Visible = true;
            imbMinistryMissionTripLocationDate.Visible = true;
            imbMinistryVolunteerOrganizationDate.Visible = true;

            imbMinistryBibleStudyDate.ImageUrl = "Calender.jpg";
            imbMinistryChurchActivityDate.ImageUrl = "Calender.jpg";
            imbMinistryInternshipOrganizationDate.ImageUrl = "Calender.jpg";
            imbMinistryMentorDate.ImageUrl = "Calender.jpg";
            imbMinistryMissionTripLocationDate.ImageUrl = "Calender.jpg";
            imbMinistryVolunteerOrganizationDate.ImageUrl = "Calender.jpg";

            chbMinistryBibleStudy.Attributes.Add("onclick", "return false;");
            chbMinistryParticipation.Attributes.Add("onclick", "return false;");
            chbMinistryMentorDate.Attributes.Add("onclick", "return false;");
            chbMinistryMissionTripLocationDate.Attributes.Add("onclick", "return false;");
            chbMinistryInternshipOrganizationDate.Attributes.Add("onclick", "return false;");
            chbMinistryVolunteerOrganizationDate.Attributes.Add("onclick", "return false;");
            chbMinistryChurchActivityDate.Attributes.Add("onclick", "return false;");

            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select oct.studentlastname, oct.studentfirstname, oct.studentmiddlename, oct.churchaffiliation, oct.churchactivitydescription, oct.churchactivitydate, oct.biblestudy, "
                           + "oct.biblestudydate, oct.mentorname, oct.mentordate, oct.ministryvolunteerorganization, oct.ministryvolunteerorganizationdate, oct.internshiporganization, oct.internshiporganizationdate, oct.missiontriplocation, "
                           + "oct.missiontriplocationdate, oct.participation, oct.syscreate, "
                           + "oct.sysupdate, oct.lastupdatedby "
                           + "FROM OptionsMinistryTemplate oct "
                           + "WHERE oct.studentlastname=@studentlastname "
                           + "AND oct.studentfirstname=@studentfirstname "
                           + "AND oct.studentmiddlename=@studentmiddlename "
                           + "GROUP BY oct.studentlastname, oct.studentfirstname, oct.studentmiddlename, oct.churchaffiliation, oct.churchactivitydescription, oct.churchactivitydate, oct.biblestudy, "
                           + "oct.biblestudydate, oct.mentorname, oct.mentordate, oct.ministryvolunteerorganization, oct.ministryvolunteerorganizationdate, oct.internshiporganization, oct.internshiporganizationdate, oct.missiontriplocation, "
                           + "oct.missiontriplocationdate, oct.participation, oct.syscreate, "
                           + "oct.sysupdate, oct.lastupdatedby ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2)))));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1)));

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
                        //ddlBuses.Text = reader.GetString(0);
                    }

                    if (reader.IsDBNull(1))
                    {
                        //              txbNotes
                    }
                    else
                    {
                        //txbNotes.Text = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        //txbStaffCoordinator.Text = "N/A";
                    }
                    else
                    {
                        //txbStaffCoordinator.Text = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        txbMinistryChurchAffiliation.Text = "";
                    }
                    else
                    {
                        txbMinistryChurchAffiliation.Text = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        txbMinistryChurchActivityDescription.Text = "";
                    }
                    else
                    {
                        txbMinistryChurchActivityDescription.Text = reader.GetString(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        txbMinistryChurchActivityDate.Text = "";
                    }
                    else
                    {
                        txbMinistryChurchActivityDate.Text = reader.GetDateTime(5).ToString("yyyy-MM-dd");
                        chbMinistryChurchActivityDate.Checked = true;
                    }
                    if (reader.IsDBNull(6))
                    {
                        chbMinistryBibleStudy.Checked = false;
                    }
                    else
                    {
                        chbMinistryBibleStudy.Checked = reader.GetBoolean(6);
                    }
                    if (reader.IsDBNull(7))
                    {
                        txbMinistryBibleStudyDate.Text = "";
                    }
                    else
                    {
                        txbMinistryBibleStudyDate.Text = reader.GetDateTime(7).ToString("yyyy-MM-dd");
                        chbMinistryBibleStudy.Checked = true;
                    }
                    if (reader.IsDBNull(8))
                    {
                        txbMinistryMentorName.Text = "";
                    }
                    else
                    {
                        txbMinistryMentorName.Text = reader.GetString(8);
                    }
                    if (reader.IsDBNull(9))
                    {
                        txbMinistryMentorDate.Text = "N/A";
                    }
                    else
                    {
                        txbMinistryMentorDate.Text = reader.GetDateTime(9).ToString("yyyy-MM-dd");
                        chbMinistryMentorDate.Checked = true;
                    }
                    if (reader.IsDBNull(10))
                    {
                        txbMinistryVolunteerOrganization.Text = "";
                    }
                    else
                    {
                        txbMinistryVolunteerOrganization.Text = reader.GetString(10);
                    }
                    if (reader.IsDBNull(11))
                    {
                        txbMinistryVolunteerOrganizationDate.Text = "";
                    }
                    else
                    {
                        txbMinistryVolunteerOrganizationDate.Text = reader.GetDateTime(11).ToString("yyyy-MM-dd");
                        chbMinistryVolunteerOrganizationDate.Checked = true;
                    }
                    if (reader.IsDBNull(12))
                    {
                        txbMinistryInternshipOrganization.Text = "";
                    }
                    else
                    {
                        txbMinistryInternshipOrganization.Text = reader.GetString(12);
                    }
                    if (reader.IsDBNull(13))
                    {
                        txbMinistryInternshipOrganizationDate.Text = "";
                    }
                    else
                    {
                        txbMinistryInternshipOrganizationDate.Text = reader.GetDateTime(13).ToString("yyyy-MM-dd");
                        chbMinistryInternshipOrganizationDate.Checked = true;
                    }
                    if (reader.IsDBNull(14))
                    {
                        txbMinistryMissionTripLocation.Text = "";
                    }
                    else
                    {
                        txbMinistryMissionTripLocation.Text = reader.GetString(14);
                    }
                    if (reader.IsDBNull(15))
                    {
                        txbMinistryMissionTripLocationDate.Text = "";
                    }
                    else
                    {
                        txbMinistryMissionTripLocationDate.Text = reader.GetDateTime(15).ToString("yyyy-MM-dd");
                        chbMinistryMissionTripLocationDate.Checked = true;
                    }
                    if (reader.IsDBNull(16))
                    {
                        chbMinistryParticipation.Checked = false;
                    }
                    else
                    {
                        chbMinistryParticipation.Checked = reader.GetBoolean(16);
                    }
                }
                else
                {
                    //Not been assigned to a bus..  So force the user to select a bus before proceeding...RCM..
                }
            }
            catch (Exception lkjlk)
            {

            }
            finally
            {

                con.Close();
            }
        }

        protected void DisplayCollegeTemplate()
        {
            lblBusInformation.Text = "College Bus Information";
            lblBusInformation.Visible = true;

            ddlScholarshipType1.Visible = true;

            chbNCAA.Visible = true;
            chbCollegeGameFilm.Visible = true;
            chbCollegeAuditionPortfolio.Visible = true;
            chbCollegeACT.Visible = true;
            chbCollegeApplicationAccepted.Visible = true;
            chbCollegeAuditionPortfolio.Visible = true;
            chbCollegeFAFSACompleted.Visible = true;
            chbCollegeFair.Visible = true;
            chbCollegeGameFilm.Visible = true;
            chbCollegeHealthInsurance.Visible = true;
            chbCollegePittsburghPromiseEligible.Visible = true;
            chbCollegeSAT.Visible = true;
            chbCollegeScholarshipGrantLoanDate.Visible = true;
            chbCollegeVisitDate.Visible = true;
                        
            cmbAnotherCollegeApplication.Visible = true;
            cmbAnotherCollegeTour.Visible = true;
            //txbFAFSADate.Visible = true;
            txbCollegeAppDate.Visible = true;
            txbCollegeVisitationDate.Visible = true;
            //txbPittsbghPromiseDate.Visible = true;

            chbACT.Attributes.Add("onclick", "return false;");
            chbSAT.Attributes.Add("onclick", "return false;");
            chbNCAA.Attributes.Add("onclick", "return false;");
            chbGameFilm.Attributes.Add("onclick", "return false;");
            chbAuditionPortfolio.Attributes.Add("onclick", "return false;");
            chbCollegeACT.Attributes.Add("onclick", "return false;");
            chbCollegeApplicationAccepted.Attributes.Add("onclick", "return false;");
            chbCollegeAuditionPortfolio.Attributes.Add("onclick", "return false;");
            chbCollegeFAFSACompleted.Attributes.Add("onclick", "return false;");
            chbCollegeFair.Attributes.Add("onclick", "return false;");
            chbCollegeGameFilm.Attributes.Add("onclick", "return false;");
            chbCollegeHealthInsurance.Attributes.Add("onclick", "return false;");
            chbCollegePittsburghPromiseEligible.Attributes.Add("onclick", "return false;");
            chbCollegeSAT.Attributes.Add("onclick", "return false;");
            chbCollegeScholarshipGrantLoanDate.Attributes.Add("onclick", "return false;");
            chbCollegeVisitDate.Attributes.Add("onclick", "return false;");

            chbCollegeSATDate.Visible = true;
            chbCollegeACTDate.Visible = true;

            txbCollegeApplicationDate.Visible = true;
            txbCollegeHealthInsuranceDate.Visible = true;
            txbCollegeACTCompositeScore.Visible = true;
            
            txbCollegePittsburghPromiseDate.Visible = true;
            txbCollegeDate.Visible = true;
            txbCollegeScholarshipGrantLoanDate.Visible = true;
            txbCollegeVisitationDate.Visible = true; 

            txbCollegeACTEnglishScore.Visible = true;
            txbCollegeACTExamDate.Visible = true;
            txbCollegeACTMathScore.Visible = true;
            txbCollegeACTReadingScore.Visible = true;
            txbCollegeACTTotalScore.Visible = true;
            txbCollegeAppDate.Visible = true;
            //txbCollegeAuditionPortfolio.Visible = true;
            txbCollegeAuditionPortfolioDate.Visible = true;
            txbCollegeFAFSADate.Visible = true;
            txbCollegeGameFilmDate.Visible = true;
            txbCollegeNCAAEligiblityCenter.Visible = true;
            txbCollegeNCAAEligiblityDate.Visible = true;
            txbCollegeSATExamDate.Visible = true;
            txbCollegeSATMathScore.Visible = true;
            txbCollegeSATTotalScore.Visible = true;
            txbCollegeSATWritingScore.Visible = true;
            txbCollegeSATReadingScore.Visible = true;

            imbCollegeACTExamDate.Visible = true;
            imbCollegeApplicationDate.Visible = true;
            imbCollegeAudtionPortfolioDate.Visible = true;
            imbCollegeDate.Visible = true;
            imbCollegeFAFSADate.Visible = true;
            imbCollegeGameFilmDate.Visible = true;
            imbCollegeNCAAEligibilityDate.Visible = true;
            imbCollegePittsburghPromiseDate.Visible = true;
            imbCollegeSATExamDate.Visible = true;
            imbCollegeScholarshipGrantLoanDate.Visible = true;
            imbCollegeVisitDate.Visible = true;
            imbCollegeHealthInsurance.Visible = true;

            imbCollegeACTExamDate.ImageUrl = "Calender.jpg";
            imbCollegeApplicationDate.ImageUrl = "Calender.jpg";
            imbCollegeAudtionPortfolioDate.ImageUrl = "Calender.jpg";
            imbCollegeDate.ImageUrl = "Calender.jpg";
            imbCollegeFAFSADate.ImageUrl = "Calender.jpg";
            imbCollegeGameFilmDate.ImageUrl = "Calender.jpg";
            imbCollegeNCAAEligibilityDate.ImageUrl = "Calender.jpg";
            imbCollegePittsburghPromiseDate.ImageUrl = "Calender.jpg";
            imbCollegeSATExamDate.ImageUrl = "Calender.jpg";
            imbCollegeScholarshipGrantLoanDate.ImageUrl = "Calender.jpg";
            imbCollegeVisitDate.ImageUrl = "Calender.jpg";
            imbCollegeHealthInsurance.ImageUrl = "Calender.jpg";

            imbCollegeVisitDate.ImageUrl = "Calender.jpg";

            txbCollegeVisitationDate.Visible = true;
            

            //txbPittsbghPromiseDate.Visible = true;
            //chbPghPromiseEligible.Visible = true;

            //imbScholrGrntLoanDate.Visible = true;
            //imbScholrGrntLoanDate.ImageUrl = "Calender.jpg";

            ddlScholarshipType1.Visible = true;
            //txbSchlrshipLoanDate.Visible = true;
            
            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select oct.studentlastname, oct.studentfirstname, oct.studentmiddlename, oct.SAT, oct.satreadingscore, oct.satmathscore, oct.satwritingscore, "
                           + "oct.sattotalscore, oct.satexamdate, oct.act, oct.actenglishscore, oct.actreadingscore, oct.actcompositescore, oct.actmathscore, oct.actsciencescore, "
                           + "oct.acttotalscore, oct.actexamdate, oct.ncaaelgibilitycenter, oct.ncaaelgibilitydate, oct.gamefilm, oct.gamefilmdate, oct.comments, oct.syscreate, "
                           + "oct.sysupdate, oct.lastupdatedby "
                           + "FROM OptionsCollegeTemplate oct "
                           + "WHERE oct.studentlastname=@studentlastname "
                           + "AND oct.studentfirstname=@studentfirstname "
                           + "AND oct.studentmiddlename=@studentmiddlename "
                           + "GROUP BY oct.studentlastname, oct.studentfirstname, oct.studentmiddlename, oct.SAT, oct.satreadingscore, oct.satmathscore, oct.satwritingscore, "
                           + "oct.sattotalscore, oct.satexamdate, oct.act, oct.actenglishscore, oct.actreadingscore, oct.actcompositescore, oct.actmathscore, oct.actsciencescore, "
                           + "oct.acttotalscore, oct.actexamdate, oct.ncaaelgibilitycenter, oct.ncaaelgibilitydate, oct.gamefilm, oct.gamefilmdate, oct.comments, oct.syscreate, "
                           + "oct.sysupdate, oct.lastupdatedby ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim()));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' "));

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
        
                        //ddlBuses.Text = reader.GetString(0);
                    }

                    if (reader.IsDBNull(1))
                    {
                        //              txbNotes
                    }
                    else
                    {
                        //txbNotes.Text = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        //txbStaffCoordinator.Text = "N/A";
                    }
                    else
                    {
                        //txbStaffCoordinator.Text = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        chbCollegeSAT.Checked = false;
                    }
                    else
                    {
                        chbCollegeSAT.Checked = reader.GetBoolean(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        txbCollegeSATReadingScore.Text = "";
                    }
                    else
                    {
                        txbCollegeSATReadingScore.Text = reader.GetString(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        //lblLastUpdatedBy.Text = "LastUpdatedBy:  N/A";
                    }
                    else
                    {
                        //lblLastUpdatedBy.Text = "LastUpdatedBy:  " + reader.GetString(5) + " On: " + reader.GetSqlValue(7).ToString();
                    }
                    if (reader.IsDBNull(8))
                    {
                        txbComments.Text = "N/A";
                    }
                    else
                    {
                        txbComments.Text = reader.GetString(8);
                    }
                    if (reader.IsDBNull(9))
                    {
                        chbCollegeACT.Checked = false;
                    }
                    else
                    {
                        chbCollegeACT.Checked = reader.GetBoolean(9);
                    }
                    if (reader.IsDBNull(10))
                    {
                        txbPostGraduatePlans.Text = "N/A";
                    }
                    else
                    {
                        txbPostGraduatePlans.Text = reader.GetString(10);
                    }
                    if (reader.IsDBNull(11))
                    {
                        chbCollegeFair.Checked = false;
                    }
                    else
                    {
                        chbCollegeFair.Checked = reader.GetBoolean(11);
                    }
                    if (reader.IsDBNull(12))
                    {
                        chbFAFSA.Checked = false;
                    }
                    else
                    {
                        chbFAFSA.Checked = reader.GetBoolean(12);
                    }
                    if (reader.IsDBNull(13))
                    {
                        chbSAT.Checked = false;
                    }
                    else
                    {
                        chbSAT.Checked = reader.GetBoolean(13);
                    }
                    if (reader.IsDBNull(14))
                    {
                        chbACT.Checked = false;
                    }
                    else
                    {
                        chbACT.Checked = reader.GetBoolean(14);
                    }
                    if (reader.IsDBNull(19))
                    {
                        chbCollegeGameFilm.Checked = false;
                    }
                    else
                    {
                        chbCollegeGameFilm.Checked = reader.GetBoolean(19);
                    }
                    if (reader.IsDBNull(20))
                    {
                        txbCollegeGameFilmDate.Text = "";
                    }
                    else
                    {
                        txbCollegeGameFilmDate.Text = reader.GetString(20);
                    }
                }
                else
                {
                    //Not been assigned to a bus..  So force the user to select a bus before proceeding...RCM..
                }
            }
            catch (Exception lkjlk)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void DisplayCareerTemplate()
        {
            try
            {
                //Retreive information on the student chosen, pertaining to the Options Program.
                SqlDataReader reader = null;
                cmbOptions.Enabled = false;

                chbCareerHealthInsurance.Visible = true;
                chbCareerApplicationSubmittedDate.Visible = true;
                chbCareerHSActivitiesList.Visible = true;
                chbCareerBibleStudy.Visible = true;
                chbCareerCommunityActivitiesList.Visible = true;
                chbCareerHonorsList.Visible = true;
                chbCareerHSLeadershipList.Visible = true;
                chbCareerInterviewPreparation.Visible = true;
                chbCareerInterviewScheduleAccepted.Visible = true;
                chbCareerParticipation.Visible = true;
                chbCareerResumeCompleted.Visible = true;

                chbCareerHSActivitiesList.Attributes.Add("onclick", "return false;");
                chbCareerApplicationSubmittedDate.Attributes.Add("onclick", "return false;");
                //chbCareerBibleStudy.Attributes.Add("onclick", "return false;");
                chbCareerCommunityActivitiesList.Attributes.Add("onclick", "return false;");
                chbCareerHonorsList.Attributes.Add("onclick", "return false;");
                chbCareerHSLeadershipList.Attributes.Add("onclick", "return false;");
                chbCareerInterviewPreparation.Attributes.Add("onclick", "return false;");
                chbCareerInterviewScheduleAccepted.Attributes.Add("onclick", "return false;");
                //chbCareerParticipation.Attributes.Add("onclick", "return false;");
                chbCareerResumeCompleted.Attributes.Add("onclick", "return false;");

                //imbCareerBibleStudyDate.Visible = true;
                imbCareerHSActivitiesListDate.Visible = true;
                imbCareerHSHonorsListDate.Visible = true;
                imbCareerHSLeadershipListDate.Visible = true;
                imbCareerCommunityActivitiesListDate.Visible = true;
                imbCareerResumeCompletedDate.Visible = true;
                imbCareerInterviewPreparationDate.Visible = true;
                imbCareerApplicationSubmittedDate.Visible = true;
                imbCareerInterviewScheduledDate.Visible = true;
                //imbCareerChurchActivityDate.Visible = true;
                //imbCareerMentorDate.Visible = true;

                //imbCareerBibleStudyDate.ImageUrl = "Calender.jpg";
                imbCareerHSActivitiesListDate.ImageUrl = "Calender.jpg";
                imbCareerHSHonorsListDate.ImageUrl = "Calender.jpg";
                imbCareerHSLeadershipListDate.ImageUrl = "Calender.jpg";
                imbCareerCommunityActivitiesListDate.ImageUrl = "Calender.jpg";
                imbCareerResumeCompletedDate.ImageUrl = "Calender.jpg";
                imbCareerInterviewPreparationDate.ImageUrl = "Calender.jpg";
                imbCareerApplicationSubmittedDate.ImageUrl = "Calender.jpg";
                imbCareerInterviewScheduledDate.ImageUrl = "Calender.jpg";
                //imbCareerChurchActivityDate.ImageUrl = "Calender.jpg";
                //imbCareerMentorDate.ImageUrl = "Calender.jpg";

                txbCareerApplicationSubmittedDate.Visible = true;
                txbCareerHSActivitiesListDate.Visible = true;
                    //txbCareerBibleStudy.Visible = true;
                //txbCareerBibleStudyDate.Visible = true;
                //txbCareerChurchActivityDate.Visible = true;
                //txbCareerChurchActivityDescription.Visible = true;
                txbCareerCommunityActivitiesListDate.Visible = true;
                //txbCareerHSHonorsList.Visible = true;
                txbCareerHSHonorsListDate.Visible = true;
                txbCareerHSLeadershipListDate.Visible = true;
                //txbCareerInternshipOrganization.Visible = true;
                //txbCareerInternshipOrganizationDate.Visible = true;
                txbCareerInterviewScheduledDate.Visible = true;
                txbCareerInterviewPreparationDate.Visible = true;
                txbCareerResumeCompletedDate.Visible = true;
                //txbCareerMentorDate.Visible = true;
                //txbCareerMentorName.Visible = true;
                //txbCareerMinistryVolunteerOrganization.Visible = true;
                //txbCareerMinistryVolunteerOrganizationDate.Visible = true;
                
                lblBusInformation.Text = "Career Bus Information";
                lblBusInformation.Visible = true;

                con.Open();
                string sql = "Select StudentLastName,StudentFirstName,StudentMiddleName,HealthInsurance,HSActivitiesList,HSActivitiesListDate,HSHonorsList,HSHonorsListDate,HSLeadershipList, "
	                       + "HSLeadershipListDate,CommunityActivitiesList,CommunityActivitiesDate,ResumeCompleted,ResumeCompletedDate,InterviewPreparation,InterviewPreparationDate,InterviewScheduledDate , "
	                       + "InterviewScheduledAccepted,ApplicationSubmittedDate,ApplicationSubmittedCompany,ChurchAffiliation,ChurchActivityDescription,ChurchActivityDate,BibleStudy,BibleStudyDate,MentorName, "
                           + "MentorDate,MinistryVolunteerOrganization,MinistryVolunteerOrganizationDate,InternshipOrganization,InternshipOrganizationDate,MissionTripLocation,MissionTripLocationDate,Participation "
                           + "FROM UIF_PerformingArts.dbo.OptionsCareerTemplate  "
                           + "WHERE studentlastname=@studentlastname "
                           + "AND studentfirstname=@studentfirstname "
                           + "AND studentmiddlename=@studentmiddlename "
                           + "GROUP BY StudentLastName,StudentFirstName,StudentMiddleName,HealthInsurance,HSActivitiesList,HSActivitiesListDate,HSHonorsList,HSHonorsListDate,HSLeadershipList, "
                           + "HSLeadershipListDate,CommunityActivitiesList,CommunityActivitiesDate,ResumeCompleted,ResumeCompletedDate,InterviewPreparation,InterviewPreparationDate,InterviewScheduledDate , "
                           + "InterviewScheduledAccepted,ApplicationSubmittedDate,ApplicationSubmittedCompany,ChurchAffiliation,ChurchActivityDescription,ChurchActivityDate,BibleStudy,BibleStudyDate,MentorName, "
                           + "MentorDate,MinistryVolunteerOrganization,MinistryVolunteerOrganizationDate,InternshipOrganization,InternshipOrganizationDate,MissionTripLocation,MissionTripLocationDate,Participation ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2)))));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1)));

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
                        //ddlBuses.Text = reader.GetString(0);
                    }

                    if (reader.IsDBNull(1))
                    {
                        //              txbNotes
                    }
                    else
                    {
                        //txbNotes.Text = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        //txbStaffCoordinator.Text = "N/A";
                    }
                    else
                    {
                        //txbStaffCoordinator.Text = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {

                        chbCareerHealthInsurance.Checked = false;
                    }
                    else
                    {
                        chbCareerHealthInsurance.Checked = reader.GetBoolean(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        chbCareerHSActivitiesList.Checked = false;
                    }
                    else
                    {
                        chbCareerHSActivitiesList.Checked = reader.GetBoolean(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        txbCareerHSActivitiesListDate.Text = "";
                    }
                    else
                    {
                        txbCareerHSActivitiesListDate.Text = reader.GetDateTime(5).ToString("yyyy-MM-dd");
                        chbCareerHSActivitiesList.Checked = true;
                    }
                    if (reader.IsDBNull(6))
                    {
                        chbCareerHonorsList.Checked = false;
                    }
                    else
                    {
                        chbCareerHonorsList.Checked = reader.GetBoolean(6);
                    }
                    if (reader.IsDBNull(7))
                    {
                        txbCareerHSHonorsListDate.Text = "";
                    }
                    else
                    {
                        txbCareerHSHonorsListDate.Text = reader.GetDateTime(7).ToString("yyyy-MM-dd");
                        chbCareerHonorsList.Checked = true;
                    }
                    if (reader.IsDBNull(8))
                    {
                        chbCareerHSLeadershipList.Checked = false;
                    }
                    else
                    {
                        chbCareerHSLeadershipList.Checked = reader.GetBoolean(8);
                    }
                    if (reader.IsDBNull(9))
                    {
                        txbCareerHSLeadershipListDate.Text = "";
                    }
                    else
                    {
                        txbCareerHSLeadershipListDate.Text = reader.GetDateTime(9).ToString("yyyy-MM-dd");
                        chbCareerHSLeadershipList.Checked = true;
                    }
                    if (reader.IsDBNull(10))
                    {
                        chbCareerCommunityActivitiesList.Checked = false;
                    }
                    else
                    {
                        chbCareerCommunityActivitiesList.Checked = reader.GetBoolean(10);
                    }
                    if (reader.IsDBNull(11))
                    {
                        txbCareerCommunityActivitiesListDate.Text = "";
                    }
                    else
                    {
                        txbCareerCommunityActivitiesListDate.Text = reader.GetDateTime(11).ToString("yyyy-MM-dd");
                        chbCareerCommunityActivitiesList.Checked = true;
                    }
                    if (reader.IsDBNull(12))
                    {
                        chbCareerResumeCompleted.Checked = false;
                    }
                    else
                    {
                        chbCareerResumeCompleted.Checked = reader.GetBoolean(12);
                    }
                    if (reader.IsDBNull(13))
                    {
                        txbCareerResumeCompletedDate.Text = "";
                    }
                    else
                    {
                        txbCareerResumeCompletedDate.Text = reader.GetDateTime(13).ToString("yyyy-MM-dd");
                        chbCareerResumeCompleted.Checked = true;
                    }
                    if (reader.IsDBNull(14))
                    {
                        chbCareerInterviewPreparation.Checked = false;
                    }
                    else
                    {
                        chbCareerInterviewPreparation.Checked = reader.GetBoolean(14);
                    }
                    if (reader.IsDBNull(15))
                    {
                        txbCareerInterviewPreparationDate.Text = "";
                    }
                    else
                    {
                        txbCareerInterviewPreparationDate.Text = reader.GetDateTime(15).ToString("yyyy-MM-dd");
                        chbCareerInterviewPreparation.Checked = true;
                    }
                    if (reader.IsDBNull(16))
                    {
                        txbCareerInterviewScheduledDate.Text = "";
                    }
                    else
                    {
                        txbCareerInterviewScheduledDate.Text = reader.GetDateTime(16).ToString("yyyy-MM-dd");
                        chbCareerInterviewScheduleAccepted.Checked = true;
                    }
                    if (reader.IsDBNull(17))
                    {
                        chbCareerInterviewScheduleAccepted.Checked = false;
                    }
                    else
                    {
                        chbCareerInterviewScheduleAccepted.Checked = reader.GetBoolean(17);
                    }

                    
                    if (reader.IsDBNull(18))
                    {
                        txbCareerApplicationSubmittedDate.Text = "";
                    }
                    else
                    {
                        txbCareerApplicationSubmittedDate.Text = reader.GetDateTime(18).ToString("yyyy-MM-dd");
                        chbCareerApplicationSubmittedDate.Checked = true;
                    }
                    if (reader.IsDBNull(19))
                    {
                        chbCareerApplicationSubmittedDate.Checked = false;
                    }
                    else
                    {
                        chbCareerApplicationSubmittedDate.Checked = reader.GetBoolean(19);
                    }
                    if (reader.IsDBNull(20))
                    {
                        //
                    }
                    else
                    {
                        //txbCareerApplicationSubmittedDate.Text = reader.GetSqlValue(20).ToString();
                    }
                    if (reader.IsDBNull(21))
                    {
                        //
                    }
                    else
                    {
                        //txbCareerApplicationSubmittedDate.Text = reader.GetSqlValue(21).ToString();
                    }
                    if (reader.IsDBNull(23))
                    {
                        chbCareerBibleStudy.Checked = false;
                    }
                    else
                    {
                        chbCareerBibleStudy.Checked = reader.GetBoolean(23);
                    }
                    if (reader.IsDBNull(24))
                    {
                        //BibleStudy
                    }
                    else
                    {
                        //BibleStudyDate
                    }

                    if (reader.IsDBNull(25))
                    {
                        chbCareerInterviewPreparation.Checked = false;
                    }
                    else
                    {
                        chbCareerInterviewPreparation.Checked = reader.GetBoolean(14);
                    }
                    //if (reader.IsDBNull(15))
                    //{
                    //    txbCareerInterviewPreparationDate.Text = "";
                    //}
                    //else
                    //{
                    //    txbCareerInterviewPreparationDate.Text = reader.GetDateTime(15).ToString("yyyy-MM-dd");
                    //}
                }
                else
                {
                    //Not been assigned to a bus..  So force the user to select a bus before proceeding...RCM..
                }
            }
            catch (Exception lkjlk)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void DisplayTradeTemplate()
        {
            lblBusInformation.Text = "Trade Bus Information";
            lblBusInformation.Visible = true;


            imbTradeApplicationDate.Visible = true;
            imbTradeCollegeApplicationDate.Visible = true;
            imbTradeCollegeDeadlineDate.Visible = true;
            imbTradeCollegeVisitationDate.Visible = true;
            imbTradeDrugTestDate.Visible = true;
            imbTradeFAFSACompletedDate.Visible = true;
            imbTradePittsburghPromiseDate.Visible = true;
            imbTradeWatchTradeVideo.Visible = true;

            imbTradeApplicationDate.ImageUrl = "Calender.jpg";
            imbTradeCollegeApplicationDate.ImageUrl = "Calender.jpg";
            imbTradeCollegeDeadlineDate.ImageUrl = "Calender.jpg";
            imbTradeCollegeVisitationDate.ImageUrl = "Calender.jpg";
            imbTradeDrugTestDate.ImageUrl = "Calender.jpg";
            imbTradeFAFSACompletedDate.ImageUrl = "Calender.jpg";
            imbTradeWatchTradeVideo.ImageUrl = "Calender.jpg";

            imbCalCollegeAppDate.Visible = true;
            imbCalCollegeAppDate.ImageUrl = "Calender.jpg";
            imbCalDrugTestDate.Visible = true;
            imbCalDrugTestDate.ImageUrl = "Calender.jpg";
            imbCalTradeVideoDate.Visible = true;
            imbCalTradeVideoDate.ImageUrl = "Calender.jpg";

            //chbDrugTest.Visible = true;
            chbWtchTrdVideo.Visible = true;
            chbHealthInsurance.Visible = true;
            chbTradeDrugTest.Visible = true;
            chbTradeCollegeTour.Visible = true;
            chbTradePghProm.Visible = true;
            chbFAFSACompleted.Visible = true;
            chbTradeHealthInsur.Visible = true;
            

            chbHealthInsurance.Attributes.Add("onclick", "return false;");
            chbTradeDrugTest.Attributes.Add("onclick", "return false;");
            chbTradeCollegeTour.Attributes.Add("onclick", "return false;");
            chbTradePghProm.Attributes.Add("onclick", "return false;");
            chbTradeHealthInsur.Attributes.Add("onclick", "return false;");
            chbFAFSACompleted.Attributes.Add("onclick", "return false;");
            chbWtchTrdVideo.Attributes.Add("onclick", "return false;");
            
            txbWatchVideoDate.Visible = true;
            txbWatchVideoDate.Enabled = false;
            txbDrugTestDate.Visible = true;
            //txbDrugTestDate.Enabled = false;

            txbTradeApplicationDate.Visible = true;

            //txbCollegeAppDate.Visible = true;
            //txbCollegeAppDate.Enabled = false;
            //txbCollgDeadlineDate.Visible = true;
            //txbCollgDeadlineDate.Enabled = false;
            //txbCollegeVisitationDate.Visible = true;
            //txbCollegeVisitationDate.Enabled = false;
            //ddlCollegeAcceptStatus.Visible = true;
            //ddlCollegeAcceptStatus.Enabled = false;
            txbTradeApplicationDate.Visible = true;
            txbTradeApplicationDate.Enabled = false;
            txbSchlrshipLoanAmount.Visible = true;
            txbSchlrshipLoanAmount.Enabled = false;
            //txbPittsbghPromiseDate.Visible = true;
            txbPittsbghPromiseDate.Enabled = false;
            //txbFAFSADate.Visible = true;
            //txbFAFSADate.Enabled = false;

            //imbFAFSADate.Visible = true;
            //imbFAFSADate.ImageUrl = "Calender.jpg";
            //imbPghPromiseDate.Visible = true;
            //imbPghPromiseDate.ImageUrl = "Calender.jpg";
            //imbScholrGrntLoanDate.Visible = true;
            //imbScholrGrntLoanDate.ImageUrl = "Calender.jpg";
            imbCalCollegeAppDate.Visible = true;
            imbCalCollegeAppDate.ImageUrl = "Calender.jpg";
            imbCalDrugTestDate.Visible = true;
            imbCalDrugTestDate.ImageUrl = "Calender.jpg";
            imbCalTradeVideoDate.Visible = true;
            imbCalTradeVideoDate.ImageUrl = "Calender.jpg";
            

            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select ott.StudentLastName,ott.StudentFirstName,ott.StudentMiddleName,ott.WatchTradeVideo,ott.WatchTradeVideoDate,ott.DrugTest,ott.DrugTestDate,ott.CollegeName, "
                           + "ott.CollegeApplicationDate, ott.CollegeDeadlineDate, ott.CollegeTour, ott.CollegeVisitationDate, ott.CollegeAcceptanceStatus, ott.TradeApplicationDate, ott.TradeAccepted, ott.ScholarshipGrantLoanAmount, ott.ScholarshipGrantLoanDate, "
                           + "ott.PittsburghPromiseEligible, ott.PittsburghPromiseDate, ott.FAFSACompleted, ott.FAFSADate, ott.HealthInsurance, ott.Comments, ott.SysCreate, ott.sysupdate, ott.LastUpdatedBy "
                           + "from OptionsTradeTemplate ott "
                           + "WHERE ott.studentlastname=@studentlastname "
                           + "AND ott.studentfirstname=@studentfirstname "
                           + "AND ott.studentmiddlename=@studentmiddlename "
                           + "GROUP BY ott.StudentLastName,ott.StudentFirstName,ott.StudentMiddleName,ott.WatchTradeVideo,ott.WatchTradeVideoDate,ott.DrugTest,ott.DrugTestDate,ott.CollegeName, "
                           + "ott.CollegeApplicationDate, ott.CollegeDeadlineDate, ott.CollegeTour, ott.CollegeVisitationDate, ott.CollegeAcceptanceStatus, ott.TradeApplicationDate, ott.TradeAccepted, ott.ScholarshipGrantLoanAmount, ott.ScholarshipGrantLoanDate, "
                           + "ott.PittsburghPromiseEligible, ott.PittsburghPromiseDate, ott.FAFSACompleted, ott.FAFSADate, ott.HealthInsurance, ott.Comments, ott.SysCreate, ott.sysupdate, ott.LastUpdatedBy ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim()));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' "));

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
                        //ddlBuses.Text = reader.GetString(0);
                    }

                    if (reader.IsDBNull(1))
                    {
                        //              txbNotes
                    }
                    else
                    {
                        //txbNotes.Text = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        //txbStaffCoordinator.Text = "N/A";
                    }
                    else
                    {
                        //txbStaffCoordinator.Text = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        chbWatchTradeVideo.Checked = false;
                    }
                    else
                    {
                        chbWatchTradeVideo.Checked = reader.GetBoolean(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        txbWatchVideoDate.Text = "";
                    }
                    else
                    {
                        txbWatchVideoDate.Text = reader.GetDateTime(4).ToString("yyyy-MM-dd");
                        imbCalTradeVideoDate.ImageUrl = "Calender.jpg";
                    }
                    if (reader.IsDBNull(5))
                    {
                        chbTradeDrugTest.Checked = false;
                    }
                    else
                    {
                        chbTradeDrugTest.Checked = reader.GetBoolean(5);
                    }
                    if (reader.IsDBNull(6))
                    {
                        txbDrugTestDate.Text = "";
                    }
                    else
                    {
                        //ddlVehichleMonth.Text = reader.GetDateTime(4).Month.ToString();
                        //ddlVehichleDay.Text = reader.GetDateTime(4).Day.ToString();
                        //ddlVehichleYear.Text = reader.GetDateTime(4).Year.ToString();
                        txbDrugTestDate.Text = reader.GetDateTime(6).ToString("yyyy-MM-dd");
                        //txbDrugTestDate.Text = reader.GetSqlValue(6).ToString().Substring(0, reader.GetSqlValue(6).ToString().IndexOf(" "));
                        imbCalDrugTestDate.ImageUrl = "Calender.jpg";
                    }
                    if (reader.IsDBNull(7))
                    {
                        //txbCollegeName.Text = false;
                    }
                    else
                    {
                        //txbCollegeName.Text = reader.GetBoolean(7);
                    }
                    if (reader.IsDBNull(8))
                    {
                        txbCollegeAppDate.Text = "";
                    }
                    else
                    {
                        txbCollegeAppDate.Text = reader.GetDateTime(8).ToString("yyyy-MM-dd");
                        imbCalCollegeAppDate.ImageUrl = "Calender.jpg";
                    }
                    if (reader.IsDBNull(9))
                    {
                        txbCollgDeadlineDate.Text = "";
                    }
                    else
                    {
                        txbCollgDeadlineDate.Text = reader.GetDateTime(9).ToString("yyyy-MM-dd");
                        //imbCalCollegeAppDate.ImageUrl = "Calender.jpg";
                    }
                    if (reader.IsDBNull(10))
                    {
                        chbTradeCollegeTour.Checked = false;
                    }
                    else
                    {
                        chbTradeCollegeTour.Checked = reader.GetBoolean(10);
                    }
                    if (reader.IsDBNull(11))
                    {
                        txbCollegeVisitationDate.Text = "";
                    }
                    else
                    {
                        //ddlVehichleMonth.Text = reader.GetDateTime(4).Month.ToString();
                        //ddlVehichleDay.Text = reader.GetDateTime(4).Day.ToString();
                        //ddlVehichleYear.Text = reader.GetDateTime(4).Year.ToString();
                        txbCollegeVisitationDate.Text = reader.GetDateTime(11).ToString("yyyy-MM-dd");
                        imbCollegeVisitDate.ImageUrl = "Calender.jpg";
                    }
                    if (reader.IsDBNull(12))
                    {
                        ddlCollegeAcceptStatus.Text = "No";
                    }
                    else
                    {
                        ddlCollegeAcceptStatus.Text = reader.GetString(12);
                    }
                    if (reader.IsDBNull(13))
                    {
                        txbTradeApplicationDate.Text = "";
                    }
                    else
                    {
                        txbTradeApplicationDate.Text = reader.GetDateTime(13).ToString("yyyy-MM-dd");
                        imbCalCollegeAppDate.ImageUrl = "Calender.jpg";
                    }
                    if (reader.IsDBNull(14))
                    {
                        txbTradeAccepted.Text = "N/A";
                    }
                    else
                    {
                        txbTradeAccepted.Text = reader.GetString(14);
                    }
                    if (reader.IsDBNull(15))
                    {
                        txbSchlrshipLoanAmount.Text = "N/A";
                    }
                    else
                    {
                        txbSchlrshipLoanAmount.Text = reader.GetString(15);
                    }
                    if (reader.IsDBNull(16))
                    {
                        txbSchlrshipLoanDate.Text = "";
                    }
                    else
                    {
                        txbSchlrshipLoanDate.Text = reader.GetDateTime(16).ToString("yyyy-MM-dd");
                    }
                    if (reader.IsDBNull(17))
                    {
                        chbPghPromiseEligible.Checked = false;
                    }
                    else
                    {
                        chbPghPromiseEligible.Checked = reader.GetBoolean(17);
                    }
                    if (reader.IsDBNull(18))
                    {
                        txbPittsbghPromiseDate.Text = "";
                    }
                    else
                    {
                        txbPittsbghPromiseDate.Text = reader.GetDateTime(18).ToString("yyyy-MM-dd");
                        //imbPghPromiseDate.ImageUrl = "Calender.jpg";
                    }
                    if (reader.IsDBNull(19))
                    {
                        chbFAFSACompleted.Checked = false;
                    }
                    else
                    {
                        chbFAFSACompleted.Checked = reader.GetBoolean(19);
                    }
                    if (reader.IsDBNull(20))
                    {
                        //txbFAFSADate.Text = "";
                    }
                    else
                    {
                        //ddlVehichleMonth.Text = reader.GetDateTime(4).Month.ToString();
                        //ddlVehichleDay.Text = reader.GetDateTime(4).Day.ToString();
                        //ddlVehichleYear.Text = reader.GetDateTime(4).Year.ToString();
                        //txbFAFSADate.Text = reader.GetSqlValue(20).ToString().Substring(0, reader.GetSqlValue(20).ToString().IndexOf(" "));
                        //imbFAFSADate.ImageUrl = "Calender.jpg";
                    }
                    if (reader.IsDBNull(21))
                    {
                        chbTradeHealthInsur.Checked = false;
                    }
                    else
                    {
                        chbTradeHealthInsur.Checked = reader.GetBoolean(21);
                    }
                }
                else
                {
                    //Not been assigned to a bus..  So force the user to select a bus before proceeding...RCM..
                }
            }
            catch (Exception lkjlk)
            {

            }
            finally
            {
                con.Close();
            }
        }
               
        protected void DisplayMilitaryTemplate()
        {
            lblBusInformation.Text = "Military Bus Information";
            lblBusInformation.Visible = true;

            chbMilitaryASVABPreparation.Visible = true;
            chbMilitaryCitizenship.Visible = true;
            chbMilitaryLegalHistoryReview.Visible = true;
            chbMilitaryMedicalHistoryReview.Visible = true;
            chbMilitaryMEPSAppointment.Visible = true;
            chbMilitaryMEPSPreparation.Visible = true;
            chbMilitaryRecruiterAppointmentDate.Visible = true;
            chbMilitaryEnlistmentServiceDate.Visible = true;
            chbMilitaryEnlistmentDate.Visible = true;
            chbMilitaryBasicTrainingDepartureDate.Visible = true;
            chbMilitaryCareerAssignedDate.Visible = true;

            txbMilitaryASVABPreparationDate.Visible = true;
            lblMilitaryMEPSAppointmentBranch.Visible = true;
            txbMilitaryBasicTrainingDeparture.Visible = true;
            txbMilitaryCareerAssigned.Visible = true;
            txbMilitaryCitizenshipDate.Visible = true;
            txbMilitaryEnlistmentDate.Visible = true;
            txbMilitaryEnlistmentService.Visible = true;
            txbMilitaryLegalHistoryReviewDate.Visible = true;
            txbMilitaryMedicalHistoryReviewDate.Visible = true;
            txbMilitaryMEPSAppointmentAcceptance.Visible = true;
            txbMilitaryMEPSAppointmentBranch.Visible = true;
            txbMilitaryMEPSPreparationDate.Visible = true;
            txbMilitaryRecruiterAppointmentDate.Visible = true;

            chbMilitaryCareerAssignedDate.Attributes.Add("onclick", "return false;");
            chbMilitaryRecruiterAppointmentDate.Attributes.Add("onclick", "return false;");
            chbMilitaryEnlistmentServiceDate.Attributes.Add("onclick", "return false;");
            chbMilitaryEnlistmentDate.Attributes.Add("onclick", "return false;");
            chbMilitaryBasicTrainingDepartureDate.Attributes.Add("onclick", "return false;");
            chbMilitaryASVABPreparation.Attributes.Add("onclick", "return false;");
            chbMilitaryCitizenship.Attributes.Add("onclick", "return false;");
            chbMilitaryLegalHistoryReview.Attributes.Add("onclick", "return false;");
            chbMilitaryMedicalHistoryReview.Attributes.Add("onclick", "return false;");
            chbMilitaryMEPSAppointment.Attributes.Add("onclick", "return false;");
            chbMilitaryMEPSPreparation.Attributes.Add("onclick", "return false;");

            imbMilitaryASVABPreparationDate.Visible = true;
            imbMilitaryBasicTrainingDeparture.Visible = true;
            imbMilitaryCareerAssigned.Visible = true;
            imbMilitaryCitizenshipDate.Visible = true;
            imbMilitaryEnlistmentDate.Visible = true;
            imbMilitaryEnlistmentService.Visible = true;
            imbMilitaryLegalHistoryReviewDate.Visible = true;
            imbMilitaryMedicalHistoryReviewDate.Visible = true;
            imbMilitaryMEPSAppointmentAcceptance.Visible = true;
            imbMilitaryMEPSPreparationDate.Visible = true;
            imbMilitaryRecruiterAppointmentDate.Visible = true;

            imbMilitaryASVABPreparationDate.ImageUrl = "Calender.jpg";
            imbMilitaryBasicTrainingDeparture.ImageUrl = "Calender.jpg";
            imbMilitaryCareerAssigned.ImageUrl = "Calender.jpg";
            imbMilitaryCitizenshipDate.ImageUrl = "Calender.jpg";
            imbMilitaryEnlistmentDate.ImageUrl = "Calender.jpg";
            imbMilitaryEnlistmentService.ImageUrl = "Calender.jpg";
            imbMilitaryLegalHistoryReviewDate.ImageUrl = "Calender.jpg";
            imbMilitaryMedicalHistoryReviewDate.ImageUrl = "Calender.jpg";
            imbMilitaryMEPSAppointmentAcceptance.ImageUrl = "Calender.jpg";
            imbMilitaryMEPSPreparationDate.ImageUrl = "Calender.jpg";
            imbMilitaryRecruiterAppointmentDate.ImageUrl = "Calender.jpg";

            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql =  "select oct.StudentLastName, oct.StudentFirstName, oct.StudentMiddleName, "
                            + "oct.Citizenship, oct.CitizenshipDate, oct.ASVABPreparation, oct.ASVABPreparationDate, "
                            + "oct.MedicalHistoryReview, oct.MedicalHistoryReviewDate, oct.LegalHistoryReview, oct.LegalHistoryReviewDate, "
                            + "oct.RecruiterAppointmentBranch, oct.RecruiterAppointmentDate, oct.CareerOption1,oct.CareerOption1Notes, "
                            + "oct.CareerOption2, oct.CareerOption2Notes, oct.CareerOption3, oct.CareerOption3Notes, "
                            + "oct.MEPSPreparation, oct.MEPSPreparationDate, oct.MEPSAppointment, oct.MEPSAppointmentBranch, oct.MEPSAppointmentAcceptance, "
                            + "oct.CareerAssigned, oct.EnlistmentDate, oct.BasicTrainingDeparture "
                            + "from OptionsMilitaryTemplate oct "
                            + "WHERE oct.studentlastname=@studentlastname "
                            + "AND oct.studentfirstname=@studentfirstname "
                            + "AND oct.studentmiddlename=@studentmiddlename "
                            + "GROUP BY oct.StudentLastName, oct.StudentFirstName, oct.StudentMiddleName, "
                            + "oct.Citizenship, oct.CitizenshipDate, oct.ASVABPreparation, oct.ASVABPreparationDate, "
                            + "oct.MedicalHistoryReview, oct.MedicalHistoryReviewDate, oct.LegalHistoryReview, oct.LegalHistoryReviewDate, "
                            + "oct.RecruiterAppointmentBranch, oct.RecruiterAppointmentDate, oct.CareerOption1,oct.CareerOption1Notes, "
                            + "oct.CareerOption2, oct.CareerOption2Notes, oct.CareerOption3, oct.CareerOption3Notes, "
                            + "oct.MEPSPreparation, oct.MEPSPreparationDate, oct.MEPSAppointment, oct.MEPSAppointmentBranch, oct.MEPSAppointmentAcceptance, "
                            + "oct.CareerAssigned, oct.EnlistmentDate, oct.BasicTrainingDeparture ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2)))));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1)));

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
                        //ddlBuses.Text = reader.GetString(0);
                    }

                    if (reader.IsDBNull(1))
                    {
                        //              txbNotes
                    }
                    else
                    {
                        //txbNotes.Text = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        //txbStaffCoordinator.Text = "N/A";
                    }
                    else
                    {
                        //txbStaffCoordinator.Text = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        chbMilitaryCitizenship.Checked = false;
                    }
                    else
                    {
                        chbMilitaryCitizenship.Checked = reader.GetBoolean(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        txbMilitaryCitizenshipDate.Text = "";
                    }
                    else
                    {
                        txbMilitaryCitizenshipDate.Text = reader.GetDateTime(4).ToString("yyyy-MM-dd");
                        chbMilitaryCitizenship.Checked = true;
                    }
                    if (reader.IsDBNull(5))
                    {
                        chbMilitaryASVABPreparation.Checked = false;
                    }
                    else
                    {
                        chbMilitaryASVABPreparation.Checked = reader.GetBoolean(5);
                    }
                    if (reader.IsDBNull(6))
                    {
                        txbMilitaryASVABPreparationDate.Text = "";
                    }
                    else
                    {
                        txbMilitaryASVABPreparationDate.Text = reader.GetDateTime(6).ToString("yyyy-MM-dd");
                        chbMilitaryASVABPreparation.Checked = true;
                    }
                    if (reader.IsDBNull(7))
                    {
                        chbMilitaryMedicalHistoryReview.Checked = false;
                    }
                    else
                    {
                        chbMilitaryMedicalHistoryReview.Checked = reader.GetBoolean(7);
                    }
                    if (reader.IsDBNull(8))
                    {
                        txbMilitaryMedicalHistoryReviewDate.Text = "";
                    }
                    else
                    {
                        txbMilitaryMedicalHistoryReviewDate.Text = reader.GetDateTime(8).ToString("yyyy-MM-dd");
                        chbMilitaryMedicalHistoryReview.Checked = true;
                    }
                    if (reader.IsDBNull(9))
                    {
                        chbMilitaryLegalHistoryReview.Checked = false;
                    }
                    else
                    {
                        chbMilitaryLegalHistoryReview.Checked = reader.GetBoolean(9);
                    }
                    if (reader.IsDBNull(10))
                    {
                        txbMilitaryLegalHistoryReviewDate.Text = "";
                    }
                    else
                    {
                        txbMilitaryLegalHistoryReviewDate.Text = reader.GetDateTime(10).ToString("yyyy-MM-dd");
                        chbMilitaryLegalHistoryReview.Checked = true;
                    }
                    //if (reader.IsDBNull(11))
                    //{
                    //    chbMilitaryRecruiterAppointmentDate.Checked = false;
                    //}
                    //else
                    //{
                    //    chbMilitaryRecruiterAppointmentDate.Checked = reader.GetBoolean(11);
                    //}
                    if (reader.IsDBNull(12))
                    {
                        txbMilitaryRecruiterAppointmentDate.Text = "";
                    }
                    else
                    {
                        txbMilitaryRecruiterAppointmentDate.Text = reader.GetDateTime(12).ToString("yyyy-MM-dd");
                        chbMilitaryRecruiterAppointmentDate.Checked = true;
                    }
                    //
                    //
                    if (reader.IsDBNull(19))
                    {
                        chbMilitaryMEPSPreparation.Checked = false;
                    }
                    else
                    {
                        chbMilitaryMEPSPreparation.Checked = reader.GetBoolean(19);
                    }
                    if (reader.IsDBNull(20))
                    {
                        txbMilitaryMEPSPreparationDate.Text = "";
                    }
                    else
                    {
                        txbMilitaryMEPSPreparationDate.Text = reader.GetDateTime(20).ToString("yyyy-MM-dd");
                        chbMilitaryMEPSPreparation.Checked = true;
                    }
                    if (reader.IsDBNull(21))
                    {
                        chbMilitaryMEPSAppointment.Checked = false;
                    }
                    else
                    {
                        chbMilitaryMEPSAppointment.Checked = reader.GetBoolean(21);
                    }
                    if (reader.IsDBNull(22))
                    {
                        txbMilitaryMEPSAppointmentBranch.Text = "";
                    }
                    else
                    {
                        txbMilitaryMEPSAppointmentBranch.Text = reader.GetBoolean(22).ToString();
                    }

                    if (reader.IsDBNull(10))
                    {
                        txbPostGraduatePlans.Text = "N/A";
                    }
                    else
                    {
                        txbPostGraduatePlans.Text = reader.GetString(10);
                    }
                    if (reader.IsDBNull(11))
                    {
                        
                        txbMilitaryRecruiterAppointmentDate.Text = "";
                    }
                    else
                    {
                        txbMilitaryRecruiterAppointmentDate.Text = reader.GetDateTime(11).ToString("yyyy-MM-dd");

                    }
                    if (reader.IsDBNull(12))
                    {
                        chbFAFSA.Checked = false;
                    }
                    else
                    {
                        chbFAFSA.Checked = reader.GetBoolean(12);
                    }
                    if (reader.IsDBNull(13))
                    {
                        chbSAT.Checked = false;
                    }
                    else
                    {
                        chbSAT.Checked = reader.GetBoolean(13);
                    }
                    if (reader.IsDBNull(14))
                    {
                        chbACT.Checked = false;
                    }
                    else
                    {
                        chbACT.Checked = reader.GetBoolean(14);
                    }








                }
                else
                {
                    //Not been assigned to a bus..  So force the user to select a bus before proceeding...RCM..
                }
            }
            catch (Exception lkjlk)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbResetPage_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("Options.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbStudentPage2_Click(object sender, EventArgs e)
        {
            Response.Clear();
            if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim());
            }
            else
            {
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim());
                //Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + Request.QueryString["StudentLastName"] + "&StudentFirstName=" + Request.QueryString["StudentFirstName"]);
            }

            //Response.Clear();
            //if ((ddlOptions.Text == "Please select a student"))
            //{
            //    Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlWaitingListInactiveStudents.SelectedValue.Substring(0, ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlWaitingListInactiveStudents.SelectedValue.Substring(ddlWaitingListInactiveStudents.SelectedValue.IndexOf(",") + 1).Trim());
            //}
            //else
            //{
            //    Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlDiscipleshipMentor.SelectedValue.Substring(0, ddlDiscipleshipMentor.SelectedValue.IndexOf(",")).Trim() + "&Dept=" + Request.QueryString["Dept"] + "&StudentFirstName=" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim());
            //}
        
        
        
        
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UpdateStudentInformation();
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            UpdateStudentInformation();
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            UpdateStudentInformation();
            ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            ExcelExport.ExportGridView(gvOptionsComprehensive, Response);
        }

        protected void cmbStudentDescription_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPage();
                con.Open();

                string sql_LoadGrid = "select op.studentlastname as 'LastName', op.studentfirstname as 'FirstName', op.activitydescription, op.syscreate, op.lastupdatedby "
                                    + "from OptionsDescription op "
                                    + "GROUP BY op.studentlastname, op.studentfirstname, op.activitydescription, op.syscreate, op.lastupdatedby "
                                    + "ORDER BY op.studentlastname, op.studentfirstname ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "OptionsProgram");
                gvOptionsComprehensive.DataSource = ds.Tables[0];
                gvOptionsComprehensive.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }


        protected void UnDisplayAllTemplates()
        {
            chbDrugTest.Visible = false;
            chbWatchTradeVideo.Visible = false;
            chbWtchTrdVideo.Visible = false;
            ddlScholarshipType1.Visible = false;
            chbACT.Visible = false;
            chbSAT.Visible = false;
            chbNCAA.Visible = false;
            chbGameFilm.Visible = false;
            chbAuditionPortfolio.Visible = false;

            chbASVABPreparation.Visible = false;
            chbCitizenship.Visible = false;
            chbCollegeFair.Visible = false;
            chbTradeDrugTest.Visible = false;
            chbFAFSA.Visible = false;
            chbFAFSACompleted.Visible = false;
            chbHealthInsurance.Visible = false;
            chbLegalHistoryReview.Visible = false;
            chbMedicalHistoryReview.Visible = false;
            chbMEPSAppointment.Visible = false;
            chbMEPSPreparation.Visible = false;
            chbMinistryBibleStudy.Visible = false;
            chbTradeHealthInsur.Visible = false;
            chbTradePghProm.Visible = false;

            chbTradeCollegeTour.Visible = false;

            imbCalCollegeAppDate.Visible = false;
            imbCalDrugTestDate.Visible = false;
            imbCalTradeVideoDate.Visible = false;
            imbCollegeVisitDate.Visible = false;
            //imbFAFSADate.Visible = false;
            //imbPghPromiseDate.Visible = false;
            //imbScholrGrntLoanDate.Visible = false;
            imbCollegeHealthInsurance.Visible = false;
            
            cmbAnotherCollegeApplication.Visible = false;
            cmbAnotherCollegeTour.Visible = false;
            lblBusInformation.Visible = false;

            txbCollegeAppDate.Visible = false;
            txbCollegeVisitationDate.Visible = false;
            txbCollgDeadlineDate.Visible = false;
            txbDrugTestDate.Visible = false;
            //txbFAFSADate.Visible = false;
            txbPittsbghPromiseDate.Visible = false;
            txbPostGraduatePlans.Visible = false;
            txbProgramEnrollment.Visible = false;
            txbSchlrshipLoanAmount.Visible = false;
            txbSchlrshipLoanDate.Visible = false;
            txbTradeAccepted.Visible = false;
            txbTradeApplicationDate.Visible = false;
            txbWatchVideoDate.Visible = false;



            imbTradeApplicationDate.Visible = false;
            imbTradeCollegeApplicationDate.Visible = false;
            imbTradeCollegeDeadlineDate.Visible = false;
            imbTradeCollegeVisitationDate.Visible = false;
            imbTradeDrugTestDate.Visible = false;
            imbTradeFAFSACompletedDate.Visible = false;
            imbTradePittsburghPromiseDate.Visible = false;
            imbTradeWatchTradeVideo.Visible = false;


            chbCareerBibleStudy.Visible = false;
            chbCareerCommunityActivitiesList.Visible = false;
            chbCareerHonorsList.Visible = false;
            chbCareerHSLeadershipList.Visible = false;
            chbCareerInterviewPreparation.Visible = false;
            chbCareerInterviewScheduleAccepted.Visible = false;
            chbCareerParticipation.Visible = false;
            chbCareerResumeCompleted.Visible = false;
            chbCareerHealthInsurance.Visible = false;
            chbCareerHSActivitiesList.Visible = false;
            chbCareerApplicationSubmittedDate.Visible = false;
            chbCollegeScholarshipGrantLoanDate.Visible = false;
            chbCollegeVisitDate.Visible = false;
            txbCareerApplicationSubmittedDate.Visible = false;
            txbCareerInterviewPreparationDate.Visible = false;
            //txbCareerBibleStudy.Visible = false;
            txbCareerBibleStudyDate.Visible = false;
            //txbCareerChurchActivityDate.Visible = false;
            //txbCareerChurchActivityDescription.Visible = false;
            txbCareerCommunityActivitiesListDate.Visible = false;
            //txbCareerHSHonorsList.Visible = false;
            txbCareerHSHonorsListDate.Visible = false;
            txbCareerHSLeadershipListDate.Visible = false;
            //txbCareerInternshipOrganization.Visible = false;
            //txbCareerInternshipOrganizationDate.Visible = false;
            txbCareerInterviewScheduledDate.Visible = false;
            txbCareerInterviewPreparationDate.Visible = false;
            txbCareerHSActivitiesListDate.Visible = false;
            txbCareerResumeCompletedDate.Visible = false;
            //txbCareerMentorDate.Visible = false;
            //txbCareerMentorName.Visible = false;
            //txbCareerMinistryVolunteerOrganization.Visible = false;
            //txbCareerMinistryVolunteerOrganizationDate.Visible = false;
            //imbCareerBibleStudyDate.Visible = false;
            imbCareerHSHonorsListDate.Visible = false;
            imbCareerHSLeadershipListDate.Visible = false;
            imbCareerCommunityActivitiesListDate.Visible = false;
            imbCareerResumeCompletedDate.Visible = false;
            imbCareerInterviewPreparationDate.Visible = false;
            imbCareerApplicationSubmittedDate.Visible = false;
            imbCareerInterviewScheduledDate.Visible = false;
            imbCareerHSActivitiesListDate.Visible = false;
            //imbCareerChurchActivityDate.Visible = false;
            //imbCareerMentorDate.Visible = false;
            
            //College
            chbCollegeACT.Visible = false;
            chbCollegeApplicationAccepted.Visible = false;
            chbCollegeAuditionPortfolio.Visible = false;
            chbCollegeFAFSACompleted.Visible = false;
            chbCollegeFair.Visible = false;
            chbCollegeGameFilm.Visible = false;
            chbCollegeHealthInsurance.Visible = false;
            chbCollegePittsburghPromiseEligible.Visible = false;
            chbCollegeSAT.Visible = false;
            chbPghPromiseEligible.Visible = false;

          
            
            //txbCollegePittsburghPromiseDate.Visible = true;
            //txbCollegeDate.Visible = true;
            //txbCollegeScholarshipGrantLoanDate.Visible = true;
            //txbCollegeVisitationDate.Visible = true; 


            chbCollegeSATDate.Visible = false;
            chbCollegeACTDate.Visible = false;
            txbCollegeApplicationDate.Visible = false;
            txbCollegeHealthInsuranceDate.Visible = false;
            txbCollegeACTCompositeScore.Visible = false;
            txbCollegeACTEnglishScore.Visible = false;
            txbCollegeACTExamDate.Visible = false;
            txbCollegeACTMathScore.Visible = false;
            txbCollegeACTReadingScore.Visible = false;
            txbCollegeACTTotalScore.Visible = false;
            txbCollegeAppDate.Visible = false;
            //txbCollegeAuditionPortfolio.Visible = false;
            txbCollegeAuditionPortfolioDate.Visible = false;
            txbCollegeFAFSADate.Visible = false;
            txbCollegeGameFilmDate.Visible = false;
            txbCollegeNCAAEligiblityCenter.Visible = false;
            txbCollegeNCAAEligiblityDate.Visible = false;
            txbCollegeSATExamDate.Visible = false;
            txbCollegeSATMathScore.Visible = false;
            txbCollegeSATTotalScore.Visible = false;
            txbCollegeSATWritingScore.Visible = false;
            txbCollegeSATReadingScore.Visible = false;
            txbCollegePittsburghPromiseDate.Visible = false;
            txbCollegeDate.Visible = false;
            txbCollegeScholarshipGrantLoanDate.Visible = false;
            txbCollegeVisitationDate.Visible = false; 

            imbCollegeACTExamDate.Visible = false;
            imbCollegeApplicationDate.Visible = false;
            imbCollegeAudtionPortfolioDate.Visible = false;
            imbCollegeDate.Visible = false;
            imbCollegeFAFSADate.Visible = false;
            imbCollegeGameFilmDate.Visible = false;
            imbCollegeNCAAEligibilityDate.Visible = false;
            imbCollegePittsburghPromiseDate.Visible = false;
            imbCollegeSATExamDate.Visible = false;
            imbCollegeScholarshipGrantLoanDate.Visible = false;
            imbCollegeVisitDate.Visible = false;
            //imbFAFSADate.Visible = false;

            //Military
            chbMilitaryASVABPreparation.Visible = false;
            chbMilitaryCitizenship.Visible = false;
            chbMilitaryLegalHistoryReview.Visible = false;
            chbMilitaryMedicalHistoryReview.Visible = false;
            chbMilitaryMEPSAppointment.Visible = false;
            chbMilitaryMEPSPreparation.Visible = false;
            chbMilitaryRecruiterAppointmentDate.Visible = false;
            chbMilitaryEnlistmentServiceDate.Visible = false;
            chbMilitaryEnlistmentDate.Visible = false;
            chbMilitaryBasicTrainingDepartureDate.Visible = false;
            chbMilitaryCareerAssignedDate.Visible = false;
            chbMilitaryLegalHistoryReview.Visible = false;

            lblMilitaryMEPSAppointmentBranch.Visible = false;

            txbMilitaryASVABPreparationDate.Visible = false;
            txbMilitaryBasicTrainingDeparture.Visible = false;
            txbMilitaryCareerAssigned.Visible = false;
            txbMilitaryCitizenshipDate.Visible = false;
            txbMilitaryEnlistmentDate.Visible = false;
            txbMilitaryEnlistmentService.Visible = false;
            txbMilitaryLegalHistoryReviewDate.Visible = false;
            txbMilitaryMedicalHistoryReviewDate.Visible = false;
            txbMilitaryMEPSAppointmentAcceptance.Visible = false;
            txbMilitaryMEPSAppointmentBranch.Visible = false;
            txbMilitaryMEPSPreparationDate.Visible = false;
            txbMilitaryRecruiterAppointmentDate.Visible = false;
            imbMilitaryASVABPreparationDate.Visible = false;
            imbMilitaryBasicTrainingDeparture.Visible = false;
            imbMilitaryCareerAssigned.Visible = false;
            imbMilitaryCitizenshipDate.Visible = false;
            imbMilitaryEnlistmentDate.Visible = false;
            imbMilitaryEnlistmentService.Visible = false;
            imbMilitaryLegalHistoryReviewDate.Visible = false;
            imbMilitaryMedicalHistoryReviewDate.Visible = false;
            imbMilitaryMEPSAppointmentAcceptance.Visible = false;
            imbMilitaryMEPSPreparationDate.Visible = false;
            imbMilitaryRecruiterAppointmentDate.Visible = false;

            //Ministry.
            chbMinistryBibleStudy.Visible = false;
            chbMinistryParticipation.Visible = false;
            chbMinistryChurchActivityDate.Visible = false;
            chbMinistryInternshipOrganizationDate.Visible = false;
            chbMinistryMentorDate.Visible = false;
            chbMinistryMissionTripLocationDate.Visible = false;
            chbMinistryVolunteerOrganizationDate.Visible = false;
            txbMinistryBibleStudyDate.Visible = false;
            txbMinistryChurchActivityDate.Visible = false;
            txbMinistryChurchAffiliation.Visible = false;
            txbMinistryInternshipOrganization.Visible = false;
            txbMinistryInternshipOrganizationDate.Visible = false;
            txbMinistryMentorDate.Visible = false;
            txbMinistryMentorName.Visible = false;
            txbMinistryMissionTripLocation.Visible = false;
            txbMinistryMissionTripLocationDate.Visible = false;
            txbMinistryParticipation.Visible = false;
            txbMinistryVolunteerOrganization.Visible = false;
            txbMinistryVolunteerOrganizationDate.Visible = false;
            txbMinistryParticipation.Visible = false;
            txbMinistryInternshipOrganizationDate.Visible = false;

            imbMinistryBibleStudyDate.Visible = false;
            imbMinistryChurchActivityDate.Visible = false;
            imbMinistryInternshipOrganizationDate.Visible = false;
            imbMinistryMentorDate.Visible = false;
            imbMinistryMissionTripLocationDate.Visible = false;
            imbMinistryVolunteerOrganizationDate.Visible = false;

            pnlGeneralItems.Visible = false;
        }


        protected Boolean DetermineBusRecord(string Bus)
        {
            Boolean Found = false;
            SqlDataReader reader = null;

            try
            {
                con.Open();
                string sql = "select oct.StudentLastName, oct.StudentFirstName, oct.StudentMiddleName "
                            + "from Options" + Bus + "Template oct "
                            + "WHERE oct.studentlastname=@studentlastname "
                            + "AND oct.studentfirstname=@studentfirstname "
                            + "AND oct.studentmiddlename=@studentmiddlename "
                            + "GROUP BY oct.studentlastname, oct.studentfirstname, oct.studentmiddlename ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.Add(new SqlParameter("@studentlastname", ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim()));
                cmd.Parameters.Add(new SqlParameter("@studentfirstname", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2)))));
                cmd.Parameters.Add(new SqlParameter("@studentmiddlename", ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1)));

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    if (!reader.IsDBNull(0))
                    {
                        Found = true;
                    }
                }
                else
                {
                    //Not been assigned to a bus..  So force the user to select a bus before proceeding...RCM..
                }
            }
            catch (Exception lkjlk)
            {

            }
            finally
            {
                con.Close();
            }
            return Found;
        }

        protected void ddlBuses_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pop up a panel to ask, do you really want to change the Bus?  

            UpdateGeneralInformation();

            UnDisplayAllTemplates();
            pnlBuses.Visible = false;

            //First determine if there is already a record for the bus selected.  
            //If there is no record, an insert needs to take place.  Otherwise, retrieve the current records information..
            if (DetermineBusRecord(ddlBuses.Text))
            {
                if (ddlBuses.Text == "College")
                {
                    DisplayCollegeTemplate();
                }
                else if (ddlBuses.Text == "Trade")
                {
                    DisplayTradeTemplate();
                }
                else if (ddlBuses.Text == "Career")
                {
                    DisplayCareerTemplate();
                }
                else if (ddlBuses.Text == "Military")
                {
                    DisplayMilitaryTemplate();
                }
                else if (ddlBuses.Text == "Ministry")
                {
                    DisplayMinistryTemplate();
                }

                pnlBuses.Visible = true;
                pnlGeneralItems.Visible = true;
            }
            else
            {
                InsertBusRecord(ddlBuses.Text);

                if (ddlBuses.Text == "College")
                {
                    DisplayCollegeTemplate();
                }
                else if (ddlBuses.Text == "Trade")
                {
                    DisplayTradeTemplate();
                }
                else if (ddlBuses.Text == "Career")
                {
                    DisplayCareerTemplate();
                }
                else if (ddlBuses.Text == "Military")
                {
                    DisplayMilitaryTemplate();
                }
                else if (ddlBuses.Text == "Ministry")
                {
                    DisplayMinistryTemplate();
                }
                pnlBuses.Visible = true;
                pnlGeneralItems.Visible = true;
            }
        }


        protected void InsertBusRecord(string Bus)
        {
            try
            {
                //Insert the new entry into the database table.
                string sql_InsertNewEntry = "";
                if (Bus == "Trade")
                {//DONE!!
                    sql_InsertNewEntry = "INSERT into Options" + Bus + "Template "
                                        + "values ("
                                        + "'" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "', "
                                        + "0, "//WatchTradeVideo
                                        + "Null, "//WatchTradeVideoDate
                                        + "0, "//DrugTest
                                        + "Null, "//DrugTestDate
                                        + "'" + "CollegeName" + "', "//CollegeName
                                        + "Null, "//CollegeApplicationDate
                                        + "Null, "//CollegeDeadlineDate
                                        + "0, "//CollegeTour
                                        + "Null, "//CollegeVisitationDate
                                        + "'', "//CollegeAcceptanceStatus
                                        + "Null, "//TradeApplicationDate
                                        + "'', "//TradeAccepted
                                        + "'', "//ScolarshipGrantLoanAmount
                                        + "Null, "//ScholarshipGrantLoanDate
                                        + "0, "//PittsburghPromiseEligible
                                        + "Null, "//PittsburghPromiseDate
                                        + "0, "//FAFSACompleted
                                        + "Null, "//FAFSADate
                                        + "0, "//HealthInsurance
                                        + "'" + txbComments.Text + "', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                }
                else if (Bus == "College")
                {//DONE!
                    sql_InsertNewEntry = "INSERT into Options" + Bus + "Template "
                                        + "values ("
                                        + "'" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "', "
                                        + "0, "//HealthInsurance
                                        + "0, "//SAT
                                        + "'', "//SATReadingScore
                                        + "'', "//SATMathScore
                                        + "'', "//SATWritingScore
                                        + "'', "//SATTotalScore
                                        + "0, "//SATExamDate
                                        + "'', "//ACT
                                        + "'', "//ACTEnglishScore
                                        + "'', "//ACTReadingScore
                                        + "'', "//ACTCompositeScore
                                        + "'', "//ACTMathScore
                                        + "'', "//ACTScienceScore
                                        + "'', "//ACTTotalScore
                                        + "Null, "//ACTExamDate
                                        + "0, "//FAFSACompleted
                                        + "Null, "//FAFSADate
                                        + "0, "//CollegeApplicationAccepted
                                        + "Null, "//CollegeApplicateDate
                                        + "'', "//CollegeTourSchool
                                        + "Null, "//CollegeDate
                                        + "0, "//PittsburghPromiseEligible
                                        + "Null, "//PittsburghPromiseDate
                                        + "Null, "//ScholarshipGrantLoan
                                        + "Null, "//ScholarshipGrantLoanDate
                                        + "0, "//NCAAElgibilityCenter
                                        + "Null, "//NCAAElgibilityDate
                                        + "0, "//GameFilm
                                        + "Null, "//GameFilmDate
                                        + "0, "//AudtionPortfolio
                                        + "Null, "//AuditionPortfolioDate
                                        + "'', "//Comments
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                }
                else if (Bus == "Career")
                {//DONE!!
                    sql_InsertNewEntry = "INSERT into Options" + Bus + "Template "
                                        + "values ("
                                        + "'" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "', "
                                        + "0, "//HealthInsurance
                                        + "0, "//HSActivitiesList
                                        + "Null, "//HSActivitiesListDate
                                        + "0, "//HSHonorsList
                                        + "Null, "//HSHonorsListDate
                                        + "0, "//HSLeadershipList
                                        + "Null, "//HSLeadershipListDate
                                        + "0, "//CommunityActivitiesList
                                        + "Null, "//CommunityActivitiesDate
                                        + "0, "//ResumeCompleted
                                        + "Null, "//ResumeCompletedDate
                                        + "0, "//InterviewPreparation
                                        + "Null, "//InterviewPreparationDate
                                        + "Null, "//InterviewScheduledDate
                                        + "'N/A', "//InterviewScheduleAccepted
                                        + "Null, "//ApplicationSubmittedDate
                                        + "'N/A', "//ApplicationSubmittedCompany
                                        + "'N/A', "//ChurchAffiliation
                                        + "'N/A', "//ChurchActivityDescription
                                        + "Null, "//ChurchActivityDate
                                        + "0, "//BibleStudy
                                        + "Null, "//BibleStudyDate
                                        + "'N/A', "//MentorName
                                        + "Null, "//MentorDate
                                        + "'N/A', "//MinistryVolunteerOrganization
                                        + "Null, "//MinistryVolunteerOrganizationDate
                                        + "'N/A', "//InternshipOrganization
                                        + "Null, "//InternshipOrganizationDate
                                        + "'N/A', "//MissionTripLocation
                                        + "Null, "//MissionTripLocationDate
                                        + "0, "//Participation
                                        //+ "'" + txbComments.Text + "', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                }
                else if (Bus == "Military")
                {//DONE!!
                    sql_InsertNewEntry = "INSERT into Options" + Bus + "Template "
                                        + "values ("
                                        + "'" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "', "
                                        + "0, "//Citizenship
                                        + "Null, "//CitizenshipDate
                                        + "0, "//ASVABPreparation
                                        + "Null, "//ASVABPreparationDate
                                        + "0, "//MedicalHistoryReview
                                        + "Null, "//MedicalHistoryReviewDate
                                        + "0, "//LegalHistoryReview
                                        + "Null, "//LegalHistoryReviewDate
                                        + "'N/A', "//RecruiterAppointmentBranch
                                        + "Null, "//RecruiterAppointmentDate
                                        + "'N/A', "//CareerOption1
                                        + "'N/A', "//CareerOption1Notes
                                        + "'N/A', "//CareerOption2
                                        + "'N/A', "//CareerOption2Notes
                                        + "'N/A', "//CareerOption3
                                        + "'N/A', "//CareerOption3Notes
                                        + "0, "//MEPSPreparation
                                        + "Null, "//MEPSPreparationDate
                                        + "0, "//MEPSAppointment
                                        + "'N/A', "//MEPSAppointmentBranch
                                        + "Null, "//MEPSAppointmentAcceptance
                                        + "'N/A', "//CareerAssigned
                                        + "'N/A', "//EnlistmentService
                                        + "Null, "//EnlistmentDate
                                        + "Null, "//BasicTrainingDeparture
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                }
                else if (Bus == "Ministry")
                {//DONE!!
                    sql_InsertNewEntry = "INSERT into Options" + Bus + "Template "
                                        + "values ("
                                        + "'" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "', "
                                        + "'" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "', "
                                        + "'N/A', "//ChurchAffiliation
                                        + "'N/A', "//ChurchActivityDescription
                                        + "Null, "//ChurchActivityDate
                                        + "0, "//BibleStudy
                                        + "Null, "//BibleStudyDate
                                        + "'N/A', "//MentorName
                                        + "Null, "//MentorDate
                                        + "'N/A', "//MinistryVolunteerOrganization
                                        + "Null, "//MinistryVolunteerOrganizationDate
                                        + "'N/A', "//InternshipOrganization
                                        + "Null, "//InternshipOrganizationDate
                                        + "'N/A', "//MissionTripLocation
                                        + "Null, "//MissionTripLocationDate
                                        + "0, "//Participation
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                }                
                
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlInsertCommand2 = new SqlCommand(sql_InsertNewEntry, con);
                if (sqlInsertCommand2.ExecuteNonQuery() > 0)
                {
                    con.Close();
                }
                else
                {

                }
            }
            catch (Exception lkjlkann)
            {

            }
        }

        protected void cmbAnotherCollegeTour_Click(object sender, EventArgs e)
        {

        }

        protected void cmbAnotherCollegeApplication_Click(object sender, EventArgs e)
        {

        }

        protected void cmbBaseReport_Click(object sender, EventArgs e)
        {


            string sql =  "select spa.LastName, spa.FirstName, ckp.CoreKid "
                        + "from StudentProgramAttendance spa "
                        + "LEFT OUTER JOIN StudentInformation si "
                        + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName) "
                        //--ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName AND spa.MiddleName = si.MiddleName)
                        + "LEFT OUTER JOIN CoreKidsProgram ckp "
                        + "ON (spa.LastName = ckp.LastName AND spa.FirstName = ckp.FirstName) "
                        + "LEFT OUTER JOIN ProgramsList pl "
                        + "ON (spa.LastName = pl.LastName AND spa.FirstName = pl.FirstName) "
                        //--ON (spa.LastName = pl.LastName AND spa.FirstName = pl.FirstName AND spa.MiddleName = pl.MiddleName)
                        //--WHERE EXISTS (select * from ProgramsList where (OutreachBasketball = 1 or BasketballTEAMS = 1)) 
                        + "WHERE (si.Grade = '12' AND spa.Attended = 1 AND spa.Day > '2010-09-01') "
                        //--------AND EXISTS (select * from ProgramsList where (OutreachBasketball = 1 or BasketballTEAMS = 1))
                        + "GROUP BY spa.LastName, spa.FirstName, ckp.CoreKid "
                        + "ORDER BY spa.LastName, spa.FirstName ";

        }

        protected void imbCalCollegeAppDate_Click(object sender, ImageClickEventArgs e)
        {
            //calDrugTestDate.Visible = true;
            //calDrugTestDate.Style.Add("z-index", "999999999");
            //calDrugTestDate.Enabled = true;
        }

        protected void imbCalDrugTestDate_Click(object sender, ImageClickEventArgs e)
        {
            calDrugTestDate.Visible = true;
            calDrugTestDate.Style.Add("z-index", "999999999");
            calDrugTestDate.Enabled = true;
        }

        protected void calDrugTestDate_SelectionChanged(object sender, EventArgs e)
        {
            txbDrugTestDate.Text = calDrugTestDate.SelectedDate.ToString("yyyy-MM-dd");
            calDrugTestDate.Visible = false;
            chbTradeDrugTest.Checked = true;
            chbTradeDrugTest.Enabled = true;
            txbDrugTestDate.Enabled = true;
            txbDrugTestDate.Attributes.Add("onclick", "return false;");

            //chbHealthInsurance.Attributes.Add("onclick", "return false;");
            chbTradeDrugTest.Attributes.Add("onclick", "return false;");
            //chbTradeCollegeTour.Attributes.Add("onclick", "return false;");
            //chbTradePghProm.Attributes.Add("onclick", "return false;");
            
            //chbTradeDrugTest.Checked = true;
        }

        //protected void calCovenantSentDate_SelectionChanged1(object sender, EventArgs e)
        //{
        //    txbCovenantSentDate.Text = calCovenantSentDate.SelectedDate.ToString("yyyy-MM-dd");
        //    calCovenantSentDate.Visible = false;
        //}

        protected void imbCalTradeVideoDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("LegalHistoryReviewDate", "LegalHistoryReview");
        }

        protected void imbFAFSADate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("LegalHistoryReviewDate", "LegalHistoryReview");
        }

        protected void imbScholrGrntLoanDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("LegalHistoryReviewDate", "LegalHistoryReview");
        }

        protected void imbPghPromiseDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("LegalHistoryReviewDate", "LegalHistoryReview");
        }

        protected void imbMilitaryCitizenshipDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("CitizenshipDate", "Citizenship");
        }

        protected void imbMilitaryMEPSAppointmentAcceptance_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("MEPSAppointmentAcceptance", "", "MEPSAppointmentBranch", "");
        }

        protected void imbMilitaryMEPSPreparationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("MEPSPreparationDate", "MEPSPreparation");
        }

        protected void imbMilitaryASVABPreparationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("ASVABPreparationDate", "ASVABPreparation");
        }

        protected void imbMilitaryBasicTrainingDeparture_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("BasicTrainingDeparture", "", "", "");
        }

        protected void imbMilitaryEnlistmentDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("EnlistmentDate", "", "", "");
        }

        protected void imbMilitaryRecruiterAppointmentDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("RecruiterAppointmentDate", "");
        }

        protected void imbMilitaryMedicalHistoryReviewDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("MedicalHistoryReviewDate", "MedicalHistoryReview");
        }

        protected void imbMilitaryCareerAssigned_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("", "", "CareerAssigned", "");
        }

        protected void imbMilitaryLegalHistoryReviewDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("LegalHistoryReviewDate", "LegalHistoryReview");
        }

        protected void imbMilitaryEnlistmentServiceDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("EnlistmentDate", "", "EnlistmentService", "");
        }

        protected void imbCareerApplicationSubmittedDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("ApplicationSubmittedDate", "", "ApplicationSubmittedCompany", "");
        }

        protected void imbCareerMentorDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("MentorDate", "", "MentorName", "");
        }

        protected void imbCareerHSHonorsListDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("HSHonorsListDate", "HSHonorsList", "", "");

            //if (txbCareerHSHonorsListDate.Text != "")
            //{
            //    calGeneralCalender.SelectedDate = Convert.ToDateTime(txbCareerHSHonorsListDate.Text);
            //}
            //else
            //{
            //    HandleCalender("HSHonorsListDate", "HSHonorsList");
            //}
            //calGeneralCalender.Visible = true;
        }

        protected void imbCareerResumeCompletedDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("ResumeCompletedDate", "ResumeCompleted");
        }

        protected void imbCareerBibleStudyDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("BibleStudyDate", "BibleStudy");
        }

        protected void imbCareerChurchActivityDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("ChurchActivityDate", "", "ChurchActivityDescription", "");
        }

        protected void imbCareerInterviewPreparationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("InterviewPreparationDate", "InterviewPreparation");
        }

        protected void imbCareerHSLeadershipListDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("HSLeadershipListDate", "HSLeadershipList");
        }

        protected void imbCareerMissionTripLocationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("MissionTripLocationDate", "", "MissionTripLocation", "");
        }

        //add a field just above...

        protected void imbCareerCommunityActivitiesListDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("CommunityActivitiesDate", "CommunityActivitiesList");
            
            //if (txbCareerCommunityActivitiesListDate.Text != "")
            //{
            //    calGeneralCalender.SelectMonthText = txbCareerCommunityActivitiesListDate.Text;
                
            //    //txbIdentifier.Text = "MinistryInternshipOrganizationDate";
            //}
            //else
            //{
            //    HandleCalender("CommunityActivitiesDate", "CommunityActivitiesList");
            //}
            //calGeneralCalender.Visible = true;
        }

        protected void calGeneralCalender_SelectionChanged(object sender, EventArgs e)
        {
            HandleCalender(CalenderVariable1, CalenderVariable2, "", "");

            //HandleCalender(CalenderVariable1, CalenderVariable2);
        }        
        
        protected void imbMinistryInternshipOrganizationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("InternshipOrganizationDate", "InternshipOrganization");
            
            //if (txbMinistryInternshipOrganizationDate.Text != "")
            //{
            //    calGeneralCalender.SelectMonthText = txbMinistryInternshipOrganizationDate.Text;
            //    txbIdentifier.Text = "MinistryInternshipOrganizationDate";
            //}
            //else
            //{
            //    HandleCalender("Ministry", "CommunityActivitiesList");
            //}
            //calGeneralCalender.Visible = true;
        }

        protected void imbMinistryChurchActivityDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("ChurchActivityDate", "");

            //if (txbMinistryChurchActivityDate.Text != "")
           // {
           //     calGeneralCalender.SelectMonthText = txbMinistryChurchActivityDate.Text;
           //     txbIdentifier.Text = "MinistryChurchActivityDate";
           // }
           // calGeneralCalender.Visible = true;
        }

        protected void imbMinistryVolunteerOrganizationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("InternshipOrganizationDate", "InternshipOrganization");
            
            //if (txbMinistryVolunteerOrganizationDate.Text != "")
            //{
            //    calGeneralCalender.SelectMonthText = txbMinistryVolunteerOrganizationDate.Text;
            //    txbIdentifier.Text = "MinistryVolunteerOrganizationDate";
            //}
            //calGeneralCalender.Visible = true;
        }

        protected void imbMinistryBibleStudyDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("BibleStudyDate", "BibleStudy");
        }

        protected void imbMinistryMissionTripLocationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("MissionTripLocationDate", "MissionTripLocation");
        }

        protected void imbMinistryMentorDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("MentorDate", "MentorName");
        }

        protected void imbCollegeApplicationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("CollegeApplicationDate", "CollegeApplicationAccepted");
        }

        protected void imbCollegeNCAAEligibilityDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("NCAAElgibilityDate", "NCAAElgibilityCenter");
        }

        protected void imbCollegeACTExamDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("ACTExamDate", "ACT");
        }

        protected void imbCollegeScholarshipGrantLoanDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("ScholarshipGrantLoanDate", "ScholarshipGrantLoan");
        }

        protected void imbCollegeDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("CollegeDate", "", "", "");
        }

        protected void imbCollegeGameFilmDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("GameFilmDate", "GameFilm");
        }

        protected void imbCollegeFAFSADate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("FAFSADate", "FAFSACompleted");
        }

        protected void imbCollegeSATExamDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("SATExamDate", "SAT");
        }

        protected void SaveCalenderValue(string DateField, string CheckboxField)
        {
            string sqlupdate = "";

            try
            {
                //if (Textfield == "")
                //{
                    sqlupdate = "Update Options" + ddlBuses.Text.Trim() + "Template "
                                + "Set " + DateField + " = '" + calGeneralCalender.SelectedDate.ToString("yyyy-MM-dd") + "', "
                                + CheckboxField + " = 1 "
                                + "Where StudentLastName = '" + (ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))) + "' "
                                + "And StudentFirstName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                                + "And StudentMiddleName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                //}
                //else if (CheckboxField == "")
                //{
                //    sqlupdate = "Update Options" + ddlBuses.Text.Trim() + "Template "
                //                + "Set " + DateField + " = '" + calGeneralCalender.SelectedDate.ToString("yyyy-MM-dd") + "', "
                //                + Textfield + " = '" + TextfieldValue + "' "
                //                + "Where StudentLastName = '" + (ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))) + "' "
                //                + "And StudentFirstName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                //                + "And StudentMiddleName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                //}
                //else if (DateField == "")
                //{
                //    sqlupdate = "Update Options" + ddlBuses.Text.Trim() + "Template "
                //                + "Set " + CheckboxField + " = 1, "
                //                + Textfield + " = '" + TextfieldValue + "' "
                //                + "Where StudentLastName = '" + (ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))) + "' "
                //                + "And StudentFirstName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                //                + "And StudentMiddleName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                //}

                con.Open();

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand = new SqlCommand(sqlupdate, con);
                if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                    //Didn't find a record to update..RCM..
                }
            }
            catch (Exception lkjlkaabb)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void SaveCalenderValue(string DateField, string CheckboxField, string Textfield, string TextfieldValue)
        {
            string sqlupdate = "";

            try
            {
                if ((Textfield == "") && (CheckboxField != "") && (DateField != ""))
                {
                    sqlupdate = "Update Options" + ddlBuses.Text.Trim() + "Template "
                                + "Set " + DateField + " = '" + calGeneralCalender.SelectedDate.ToString("yyyy-MM-dd") + "', "
                                + CheckboxField + " = 1 "
                                + "Where StudentLastName = '" + (ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))) + "' "
                                + "And StudentFirstName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                                + "And StudentMiddleName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                }
                else if (CheckboxField == "")
                {
                    sqlupdate = "Update Options" + ddlBuses.Text.Trim() + "Template "
                                + "Set " + DateField + " = '" + calGeneralCalender.SelectedDate.ToString("yyyy-MM-dd") + "', "
                                + Textfield + " = '" + TextfieldValue + "' "
                                + "Where StudentLastName = '" + (ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))) + "' "
                                + "And StudentFirstName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                                + "And StudentMiddleName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                }
                else if (DateField == "")
                {
                    sqlupdate = "Update Options" + ddlBuses.Text.Trim() + "Template "
                                + "Set " + CheckboxField + " = 1, "
                                + Textfield + " = '" + TextfieldValue + "' "
                                + "Where StudentLastName = '" + (ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(","))) + "' "
                                + "And StudentFirstName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1, ((ddlOptions.SelectedValue.IndexOf("(")) - (ddlOptions.SelectedValue.IndexOf(",") + 2))) + "' "
                                + "And StudentMiddleName = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf("(") + 1, ((ddlOptions.SelectedValue.IndexOf(")")) - (ddlOptions.SelectedValue.IndexOf("("))) - 1) + "' ";
                }

                con.Open();

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand = new SqlCommand(sqlupdate, con);
                if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                    //Didn't find a record to update..RCM..
                }
            }
            catch (Exception lkjlkaabb)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void HandleCalender(string DateField, string CheckboxField, string Textfield, string TextfieldValue)
        {

            if (CalenderVariable1 == DateField)
            {
                //Calender value has already been set.  Save and make disappear...RCM..
                SaveCalenderValue(DateField, CheckboxField, Textfield, TextfieldValue);
                calGeneralCalender.Visible = false;
                //CalenderVariable3 = calGeneralCalender.SelectedDate.ToString();
                CalenderVariable1 = "";
                CalenderVariable2 = "";
                //CalenderVariable3 = "";
                ReLoadBus();
            }
            else
            {
                calGeneralCalender.SelectedDates.Clear();

                calGeneralCalender.Visible = true;
                calGeneralCalender.Style.Add("z-index", "99999");
                CalenderVariable1 = DateField;
                CalenderVariable2 = CheckboxField;
            }
        }

        protected void HandleCalender(string FieldName1, string FieldName2)
        {
            
            if (CalenderVariable1 == FieldName1)
            {
                //Calender value has already been set.  Save and make disappear...RCM..
                SaveCalenderValue(FieldName1, FieldName2);
                                                
                calGeneralCalender.Visible = false;                
                //CalenderVariable3 = calGeneralCalender.SelectedDate.ToString();
                CalenderVariable1 = "";
                CalenderVariable2 = "";
                //CalenderVariable3 = "";
                ReLoadBus();
            }
            else
            {
                calGeneralCalender.SelectedDates.Clear();

                calGeneralCalender.Visible = true;
                calGeneralCalender.Style.Add("z-index", "99999");
                CalenderVariable1 = FieldName1;
                CalenderVariable2 = FieldName2;
            }
        }

        protected void ReLoadBus()
        {
            if (ddlBuses.Text == "College")
            {
                DisplayCollegeTemplate();
            }
            else if (ddlBuses.Text == "Trade")
            {
                DisplayTradeTemplate();
            }
            else if (ddlBuses.Text == "Career")
            {
                DisplayCareerTemplate();
            }
            else if (ddlBuses.Text == "Military")
            {
                DisplayMilitaryTemplate();
            }
            else if (ddlBuses.Text == "Ministry")
            {
                DisplayMinistryTemplate();
            }
        }

        protected void imbCollegeAudtionPortfolioDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("CollegeAuditionPortfolioDate", "CollegeAuditionPortfolio");
        }

        protected void imbCollegePittsburghPromiseDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("PittsburghPromiseDate", "PittsburghPromiseEligible");
        }

        protected void imbCareerHSActivitiesListDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("HSActivitiesListDate", "HSActivitiesList");
        }

        protected void imbCareerInterviewScheduledDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("InterviewScheduledDate", "InterviewScheduledAccepted");
        }

        protected void imbCollegeHealthInsurance_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("", "HealthInsurance", "", "");
        }

        protected void imbTradeWatchTradeVideo_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("WatchTradeVideoDate", "WatchTradeVideo");
        }

        protected void imbTradeDrugTestDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("DrugTestDate", "DrugTest");
        }

        protected void imbTradeCollegeApplicationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("DrugTestDate", "DrugTest");
        }

        protected void imbTradeCollegeDeadlineDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("CollegeDeadlineDate", "", "", "");
        }

        protected void imbTradeCollegeVisitationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("CollegeVisitationDate", "CollegeTour");
        }

        protected void imbTradeApplicationDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("ApplicationDate", "", "", "");
        }

        protected void imbTradePittsburghPromiseDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("PittsburghPromiseDate", "PittsburghPromiseEligible");
        }

        protected void imbTradeFAFSACompletedDate_Click(object sender, ImageClickEventArgs e)
        {
            HandleCalender("FAFSADate", "FAFSACompleted");   
        }
    }
}