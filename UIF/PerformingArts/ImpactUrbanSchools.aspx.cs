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
    public partial class ImpactUrbanSchools : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
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

                //Check for a login... If not, re-direct them to the mainmenu page..RCM..
                if (Request.QueryString["security"] == "Good")
                {
                    if (((Request.QueryString["LastName"] == "VanKirk") && (Request.QueryString["FirstName"] == "Sara")) || ((Request.QueryString["LastName"] == "Wagner") && (Request.QueryString["FirstName"] == "Amanda")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Tammy")) || ((Request.QueryString["LastName"] == "Kreider") && (Request.QueryString["FirstName"] == "Tom")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")) || ((Request.QueryString["LastName"] == "Krance") && (Request.QueryString["FirstName"] == "Tom")) || ((Request.QueryString["LastName"] == "Lapalme") && (Request.QueryString["FirstName"] == "Brittany")))
                    {
                        if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
                        {
                            PopulateDropDown();
                            ddlOptions.Items.Add("Please select a student");
                            ddlOptions.Text = "Please select a student";
                        }
                        else
                        {
                            DisplayHeaderFields();
                            RetrieveInformation();
                            PopulateDropDown();
                            DisplayTheGrid();
                            ddlOptions.Items.Add(Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"]);
                            ddlOptions.Text = Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"];
                            lblNotes.Visible = true;
                            lbAddNewEntry.Visible = true;
                            cmbUpdate.Enabled = true;
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


        protected void PopulateDropDown()
        {
            try
            {
                con.Open();

                string selectSQL = "select studentlastname, studentfirstname " +
                                   "from ImpactUrbanSchools " +
                                   "group by studentlastname, studentfirstname ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    do
                    {
                        ddlOptions.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
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

        protected void DisplayHeaderFields()
        {
            lblComments.Visible = true;
            lblDiscipleMentor.Visible = true;
            lblProgramEnrollment.Visible = true;
            lblStaffCoordinator.Visible = true;
            chbHasGraduated.Visible = true;
            txbDiscipleshipMentor.Visible = true;
            txbStaffCoordinator.Visible = true;
            txbProgramEnrollment.Visible = true;
            txbComments.Visible = true;
            cmbUpdate.Visible = true;
            txbPostGraduatePlans.Visible = true;
            chbFAFSA.Visible = true;
            chbSAT.Visible = true;
            chbACT.Visible = true;
            chbCollegeFair.Visible = true;
            chbPromise.Visible = true;
            lblPostGraduatePlans.Visible = true;
            //cmbStudentsPage.Enabled = true;
            cmbStudentPage2.Enabled = true;
        }

        protected void DisplayTheGrid()
        {
            con.Open();
            string sql_LoadGrid = "";
            sql_LoadGrid = "select syscreate as 'TimeOfEntry', activitydescription as 'Activity Description', lastupdatedby as 'LastEditedBy'"
                         + "from ImpactUrbanSchoolsDescription "
                         + "where studentlastname ='" + Request.QueryString["StudentLastName"] + "' "
                         + "and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                         + "order by syscreate desc ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ImpactUrbanSchoolsDescription");
            gvStudentHistory.DataSource = ds.Tables[0];
            gvStudentHistory.DataBind();
            con.Close();
        }

        protected void DisplayTheGridFromButton()
        {
            con.Open();
            string sql_LoadGrid = "";
            sql_LoadGrid = "select syscreate as 'TimeOfEntry', activitydescription as 'Activity Description', lastupdatedby as 'LastEditedBy'"
                         + "from ImpactUrbanSchoolsDescription "
                         + "where studentlastname ='" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim() + "' "
                         + "and studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim() + "' "
                         + "order by syscreate desc ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ImpactUrbanSchoolsDescription");
            gvStudentHistory.DataSource = ds.Tables[0];
            gvStudentHistory.DataBind();
            con.Close();
        }

        protected void gvStudentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }

            //GridViewRow row = e.Row;
            //if (e.Row.RowType == DataControlRowType.DataRow && row.RowIndex == irowNum)
            //{

            //    DataRowView drv = (DataRowView)e.Row.DataItem;

            //    string ClassName = drv.Row.ItemArray.GetValue(0).ToString();
            //    string ClassTime = drv.Row.ItemArray.GetValue(1).ToString();
            //    string ClassDay = drv.Row.ItemArray.GetValue(2).ToString();
            //    string Location = drv.Row.ItemArray.GetValue(3).ToString();
            //    string Comments = drv.Row.ItemArray.GetValue(4).ToString();
            //    string Instructor = drv.Row.ItemArray.GetValue(5).ToString();
            //    string devotional = drv.Row.ItemArray.GetValue(6).ToString();
            //}
        }

        public void bind()
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "select syscreate, activitydescription, lastupdatedby "
                         + "from ImpactUrbanSchoolsDescription "
                         + "where studentlastname ='" + Request.QueryString["StudentLastName"] + "' "
                         + "and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                         + "order by syscreate desc ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ImpactUrbanSchoolsDescription");
            gvStudentHistory.DataSource = ds.Tables[0];
            gvStudentHistory.DataBind();
            con.Close();
        }

        protected void RetrieveInformation()
        {
            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select dmp.studentlastname, dmp.studentfirstname, dmp.discipleshipmentor, dmp.optionsstaffcoordinator, dmp.programenrollment, "
                           + "dmp.hasgraduated, dmp.comments, dmp.lastupdatedby, pas.pictureidentification, dmp.sysupdate, "
                           + "dmp.postgraduateplans, dmp.collegefair, dmp.FAFSA, dmp.SAT, dmp.ACT, dmp.satexamdate, dmp.actexamdate, dmp.promise   "
                           + "FROM ImpactUrbanSchools dmp "
                           + "LEFT OUTER JOIN PerformingArtsAcademyStudents pas "
                           + "ON (dmp.studentlastname = pas.lastname AND dmp.studentfirstname = pas.firstname) "
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
                        txbDiscipleshipMentor.Text = "N/A";
                    }
                    else
                    {
                        txbDiscipleshipMentor.Text = reader.GetString(2);
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
                    Image1.ImageUrl = reader.GetString(8);
                    Image1.Visible = true;
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
                        chbPromise.Checked = false;
                    }
                    else
                    {
                        chbPromise.Checked = reader.GetBoolean(17);
                    }
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

        protected void RetrieveInformationFromButton()
        {
            //Retreive information on the student chosen, pertaining to the Options Program.
            SqlDataReader reader = null;
            cmbOptions.Enabled = false;

            try
            {
                con.Open();
                string sql = "Select dmp.studentlastname, dmp.studentfirstname, dmp.optionsstaffcoordinator, dmp.programenrollment, "
                           + "dmp.hasgraduated, dmp.lastupdatedby, si.pictureidentification, dmp.sysupdate, dmp.comments, dmp.discipleshipmentor, "
                           + "dmp.postgraduateplans, dmp.collegefair, dmp.FAFSA, dmp.SAT, dmp.ACT, dmp.satexamdate, dmp.actexamdate, dmp.promise  "
                           + "FROM ImpactUrbanSchools dmp "
                           + "LEFT OUTER JOIN StudentInformation si "
                           + "ON (dmp.studentlastname = si.lastname AND dmp.studentfirstname = si.firstname) "
                           + "WHERE dmp.studentlastname=@studentlastname "
                           + "AND dmp.studentfirstname=@studentfirstname "
                           + "GROUP BY dmp.studentlastname, dmp.studentfirstname, dmp.optionsstaffcoordinator, dmp.programenrollment, "
                           + "dmp.hasgraduated, dmp.lastupdatedby, si.pictureidentification, dmp.sysupdate, dmp.comments, dmp.discipleshipmentor, "
                           + "dmp.postgraduateplans, dmp.collegefair, dmp.FAFSA, dmp.SAT, dmp.ACT, dmp.satexamdate, dmp.actexamdate, dmp.promise ";

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
                        txbStaffCoordinator.Text = "N/A";
                    }
                    else
                    {
                        txbStaffCoordinator.Text = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        txbProgramEnrollment.Text = "N/A";
                    }
                    else
                    {
                        txbProgramEnrollment.Text = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        chbHasGraduated.Checked = false;
                    }
                    else
                    {
                        chbHasGraduated.Checked = reader.GetBoolean(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        lblLastUpdatedBy.Text = "LastUpdatedBy:  N/A";
                    }
                    else
                    {
                        lblLastUpdatedBy.Text = "LastUpdatedBy:  " + reader.GetString(5) + " On: " + reader.GetSqlValue(7).ToString();
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
                        txbDiscipleshipMentor.Text = "N/A";
                    }
                    else
                    {
                        txbDiscipleshipMentor.Text = reader.GetString(9);
                    }
                    Image1.ImageUrl = reader.GetString(6);
                    Image1.Visible = true;
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
                        chbPromise.Checked = false;
                    }
                    else
                    {
                        chbPromise.Checked = reader.GetBoolean(17);
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
                        //txbDiscipleshipMentor.Enabled = true;
                        //ddlDiscipleShipMentorAvailable.Enabled = true;
                        //ddlProgramEnrollment.Enabled = true;
                        //txbCovenantReceivedDate.Enabled = true;
                        //txbCovenantSentDate.Enabled = true;
                        txbComments.Enabled = true;
                        chbHasGraduated.Enabled = true;
                        //chbWaitingListInactive.Enabled = true;
                        //chbVolunteerWaitingList.Enabled = true;
                        //lblWaitingListStudents.Enabled = true;
                    
                    }
                    else
                    {
                        //Insert the new entry into the database table.
                        string sql_InsertNewEntry = "";
                        if ((Request.QueryString["StudentLastName"] == "") && (Request.QueryString["StudentFirstName"] == ""))
                        {
                            sql_InsertNewEntry = "INSERT into ImpactUrbanSchoolsDescription "
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
                            sql_InsertNewEntry = "INSERT into ImpactUrbanSchoolsDescription "
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

                        sqlUpdateStatement = " UPDATE ImpactUrbanSchools "
                        + "SET "
                            //+ " studentlastname = '" + ddlOptions.SelectedValue.Substring(0, ddlOptions.SelectedValue.IndexOf(",")).Trim() + "' , "
                            //+ " studentfirstname = '" + ddlOptions.SelectedValue.Substring(ddlOptions.SelectedValue.IndexOf(",") + 1).Trim() + "' , "
                        + " postgraduateplans = '" + txbPostGraduatePlans.Text.Trim() + "' , "
                        + " FAFSA = " + Convert.ToInt32(chbFAFSA.Checked) + ", "
                        + " SAT = " + Convert.ToInt32(chbSAT.Checked) + ", "
                            //+ " SATExamDate = '" + System.DateTime.Now.ToString() + "',"
                        + " ACT = " + Convert.ToInt32(chbACT.Checked) + ", "
                            //+ " ACTExamDate = '" + System.DateTime.Now.ToString() + "',"
                        + " optionsstaffcoordinator = '" + txbStaffCoordinator.Text.Trim() + "' , "
                        + " programenrollment = '" + txbProgramEnrollment.Text.Trim() + "' , "
                        + " hasgraduated = " + Convert.ToInt32(chbHasGraduated.Checked) + ", "
                        + " comments = '" + txbComments.Text.Trim() + "' , "
                        + " discipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "' , "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                        + " collegefair = " + Convert.ToInt32(chbCollegeFair.Checked) + ", "
                        + " promise = " + Convert.ToInt32(chbPromise.Checked) + ", "
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
        }
        
        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        //protected void gvStudentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    for (int i = 0; i < e.Row.Cells.Count; i++)
        //    {
        //        e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
        //    }
        //}

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvOptionsComprehensive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
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

            RetrieveInformationFromButton();
            DisplayTheGridFromButton();
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
                                    + "from ImpactUrbanSchools op "
                                    + "LEFT OUTER JOIN PerformingArtsAcademyStudents pas "
                                    + "ON (op.studentlastname = pas.lastname AND op.studentfirstname = pas.firstname) "
                                    + "ORDER BY pas.grade, op.studentlastname, op.studentfirstname "; 

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "ImpactUrbanSchools");
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

            RetrieveInformationFromButton();
            DisplayTheGridFromButton();
            cmbUpdate.Enabled = true;
            lblNotes.Visible = true;
            lbAddNewEntry.Visible = true;
            DisplayHeaderFields();
        }

        protected void cmbResetPage_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("ImpactUrbanSchools.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
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
            ExcelExport.ExportGridView(gvOptionsComprehensive, Response);
        }

        protected void cmbStudentDescription_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPage();
                con.Open();

                string sql_LoadGrid = "select op.studentlastname as 'LastName', op.studentfirstname as 'FirstName', op.activitydescription, op.syscreate, op.lastupdatedby "
                                    + "from ImpactUrbanSchoolsDescription op "
                                    + "GROUP BY op.studentlastname, op.studentfirstname, op.activitydescription, op.syscreate, op.lastupdatedby "
                                    + "ORDER BY op.studentlastname, op.studentfirstname ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "ImpactUrbanSchools");
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
    }
}