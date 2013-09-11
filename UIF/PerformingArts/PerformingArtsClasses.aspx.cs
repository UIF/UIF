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
    public partial class PerformingArtsClasses : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public static string Department = "";
                
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
                    try
                    {
                        con.Open();//Opens the db connection.

                        cmbNewEnrollment.Enabled = false;
                        cmbNewEnrollment2.Enabled = false;
                        cmdUpdate.Enabled = false;

                        try
                        {
                            string sql7 = "select pictureidentification FROM studentinformation WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' and firstname = '" + Request.QueryString["StudentFirstName"] + "' " + " and middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
                            SqlCommand cmd7 = new SqlCommand(sql7, con);
                            cmd7.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                            SqlDataAdapter custDA7 = new SqlDataAdapter();
                            custDA7.SelectCommand = cmd7;
                            DataSet custDS7 = new DataSet();
                            custDA7.Fill(custDS7, "StudentInformation");

                            //Iterate over setup records and call method to do the work on each one...RCM..
                            foreach (DataRow myDataRowPO7 in custDS7.Tables["StudentInformation"].Rows)
                            {
                                imgStudent.ImageUrl = myDataRowPO7[0].ToString();
                            }
                        }
                        catch (Exception lkjlaaffb)
                        {

                        }

                        //Handle dropdown list1..RCM..1/28/11.
                        string sql = "select pac.StudentLastName, pac.StudentFirstName, pac.ClassName, pac.MeetTime, pac.MeetDay, pac.Location, pac.PaidForClass, "
                                    + "pac.Active, pac.Comments, pac.Instructor, pac.DevotionalLeader, pac.SysCreate, pac.sysupdate, pac.ID, pac.lastupdatedby, si.pictureidentification "
                                    + "from PerformingArtsAcademyClassEnrollment pac "
                                    + "LEFT OUTER JOIN StudentInformation si "
                                    + "ON (pac.studentlastname = si.lastname AND pac.studentfirstname = si.firstname) "
                                    + "where pac.StudentLastName = '" + Request.QueryString["StudentLastName"] + "' "
                                    + "and pac.StudentFirstName = '" + Request.QueryString["StudentFirstName"] + "' "
                                    + "and pac.StudentMiddleName = '" + Request.QueryString["StudentMiddleName"] + "' "
                                    + "and pac.MeetTime = '4:30-6:00 Class' "
                                    + "and pac.student = 1 "
                                    + "ORDER BY pac.MeetTime ";

                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                        SqlDataAdapter custDA = new SqlDataAdapter();
                        custDA.SelectCommand = cmd;
                        DataSet custDS = new DataSet();
                        custDA.Fill(custDS, "PerformingArtsAcademyClassEnrollment");

                        //ddlClass1.Items.Add(" ");
                        //Iterate over setup records and call method to do the work on each one...RCM..
                        foreach (DataRow myDataRowPO in custDS.Tables["PerformingArtsAcademyClassEnrollment"].Rows)
                        {
                            //Adding options to the drop downs for a new entry.
                            //ddlClassSelection.Items.Add(myDataRowPO[0].ToString());
                            ddlClass1.Items.Add(myDataRowPO[2].ToString()); //readerList1.GetString(2));
                            Label1.Text = myDataRowPO[3].ToString();
                            lblClass1MeetDay.Text = myDataRowPO[4].ToString();
                            lblClass1MeetLocation.Text = myDataRowPO[5].ToString();
                            chbPaidClass1.Checked = System.Convert.ToBoolean(myDataRowPO[6]);
                            //chbActive.Checked = readerList1.GetBoolean(7);
                            txbComments.Text = myDataRowPO[8].ToString();
                            //Instructor  readerList1.GetString(9);
                            //DevotionalLeader = readerList1.GetString(10);
                            if (myDataRowPO[14].ToString() == "")
                            {
                                lblLastUpdatedBy.Text = "N/A";
                            }
                            else
                            {
                                lblLastUpdatedBy.Text = "LastUpdatedBy: " + myDataRowPO[14].ToString() + " On: " + myDataRowPO[12].ToString();
                            }
                            //cmdUpdate.Enabled = false;
                            chbClass1.Checked = true;
                            ddlClass1.Enabled = false;
                            cmdUpdate.Enabled = true;
                            chbPaidClass1.Enabled = false;
                            FoundRecord1 = true;
                            cmbNewEnrollment.Enabled = true;
                            cmbNewEnrollment.Text = "Remove from Class1";
                        }
                        custDS.Clear();

                        string sql2 = "select ClassName, MeetTime, MeetDay, Location, Comments, Instructor, DevotionalLeader "
                                    + "from PerformingArtsAcademyAvailableClasses " 
                                    + "where MeetTime = '4:30-6:00 Class' "
                                    + "group by ClassName, MeetTime, MeetDay, Location, Comments, Instructor, DevotionalLeader "
                                    + "order by ClassName";

                        SqlCommand cmd2 = new SqlCommand(sql2, con);
                        cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                        SqlDataAdapter custDA2 = new SqlDataAdapter();
                        custDA2.SelectCommand = cmd2;
                        DataSet custDS2 = new DataSet();
                        custDA2.Fill(custDS2, "PerformingArtsAcademyAvailableClasses");

                        if (FoundRecord1 == false)
                        {
                            ddlClass1.Items.Add("Select a class.");
                            ddlClass1.Text = "Select a class.";
                        }
                        //Iterate over setup records and call method to do the work on each one...RCM..
                        foreach (DataRow myDataRowPO in custDS2.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                        {
                            //Adding options to the drop downs for a new entry.
                            //if Empty then create blank.
                            ddlClass1.Items.Add(myDataRowPO[0].ToString());
                        }
                        //ddlClass1.Items.Add(" ");//RCM..2/9/11.
                        custDS2.Clear();
                        
                        //Handle dropdown list2..RCM..1/28/11.
                        string sql3 = "select StudentLastName, StudentFirstName, ClassName, MeetTime, MeetDay, Location, PaidForClass, "
                                    + "Active, Comments, Instructor, DevotionalLeader, SysCreate, sysupdate, ID, lastupdatedby "
                                    + "from PerformingArtsAcademyClassEnrollment "
                                    + "where StudentLastName = '" + Request.QueryString["StudentLastName"] + "' "
                                    + "and StudentFirstName = '" + Request.QueryString["StudentFirstName"] + "' "
                                    + "and StudentMiddleName = '" + Request.QueryString["StudentMiddleName"] + "' "
                                    + "and MeetTime = '6:30-8:00 Class' "
                                    + "and student = 1 "
                                    + "ORDER BY MeetTime ";

                        SqlCommand cmd3 = new SqlCommand(sql3, con);
                        cmd3.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                        SqlDataAdapter custDA3 = new SqlDataAdapter();
                        custDA3.SelectCommand = cmd3;
                        DataSet custDS3 = new DataSet();
                        custDA3.Fill(custDS3, "PerformingArtsAcademyClassEnrollment");

                        //ddlClass2.Items.Add(" ");
                        //Iterate over setup records and call method to do the work on each one...RCM..
                        foreach (DataRow myDataRowPO in custDS3.Tables["PerformingArtsAcademyClassEnrollment"].Rows)
                        {
                            //Adding options to the drop downs for a new entry.
                            //ddlClassSelection.Items.Add(myDataRowPO[0].ToString());
                            ddlClass2.Items.Add(myDataRowPO[2].ToString()); //readerList1.GetString(2));
                            Label2.Text = myDataRowPO[3].ToString();
                            lblClass2MeetDay.Text = myDataRowPO[4].ToString();
                            lblClass2MeetLocation.Text = myDataRowPO[5].ToString();
                            chbPaidClass2.Checked = System.Convert.ToBoolean(myDataRowPO[6]);
                            //chbActive.Checked = readerList1.GetBoolean(7);
                            txbComments.Text = myDataRowPO[8].ToString();
                            //Instructor  readerList1.GetString(9);
                            //DevotionalLeader = readerList1.GetString(10);
                            if (myDataRowPO[14].ToString() == "")
                            {
                                lblLastUpdatedBy.Text = "N/A";
                            }
                            else
                            {
                                lblLastUpdatedBy.Text = "LastUpdatedBy: " + myDataRowPO[14].ToString() + " On: " + myDataRowPO[12].ToString();
                            }
                            //cmdUpdate.Enabled = false;
                            FoundRecord2 = true;
                            chbPaidClass2.Enabled = false;
                            ddlClass2.Enabled = false;
                            chbClass2.Checked = true;
                            cmdUpdate.Enabled = true;
                            cmbNewEnrollment2.Enabled = true;
                            cmbNewEnrollment2.Text = "Remove from Class2";
                        }
                        custDS3.Clear();

                        string sql4 = "select ClassName, MeetTime, MeetDay, Location, Comments, Instructor, DevotionalLeader "
                                    + "from PerformingArtsAcademyAvailableClasses "
                                    + "where MeetTime = '6:30-8:00 Class' "
                                    + "group by ClassName, MeetTime, MeetDay, Location, Comments, Instructor, DevotionalLeader "
                                    + "order by ClassName";

                        SqlCommand cmd4 = new SqlCommand(sql4, con);
                        cmd4.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                        SqlDataAdapter custDA4 = new SqlDataAdapter();
                        custDA4.SelectCommand = cmd4;
                        DataSet custDS4 = new DataSet();
                        custDA4.Fill(custDS4, "PerformingArtsAcademyAvailableClasses");

                        if (FoundRecord2 == false)
                        {
                            ddlClass2.Items.Add("Select a class.");
                            ddlClass2.Text = "Select a class.";
                        }
                        //Iterate over setup records and call method to do the work on each one...RCM..
                        foreach (DataRow myDataRowPO in custDS4.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                        {
                            //Adding options to the drop downs for a new entry.
                            ddlClass2.Items.Add(myDataRowPO[0].ToString());
                        }
                        ddlClass2.Items.Add(" ");//RCM..2/9/11.
                        custDS4.Clear();

                        lblName.Text = Request.QueryString["StudentLastName"] + "," + Request.QueryString["StudentFirstName"] + "(" + Request.QueryString["StudentMiddleName"] + ")";
                        lblName.Font.Size = 16;
                        //ddlClass1.Enabled = false;
                        //ddlClass2.Enabled = false;
                        //imgStudent.ImageUrl = PictureIdent;
                        //imgStudent.ImageUrl = "~/StudentPictures/" + Request.QueryString["StudentFirstName"] + Request.QueryString["StudentLastName"] + ".jpg";
                    }
                    catch (Exception lkjl)
                    {
                        string lkaaa = "";
                    }
                    finally
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

                //Perform an update to the database.. for class1...RCM..1/31/11..
                sql_UPDATE = "Update PerformingArtsAcademyClassEnrollment  "
                        + "Set ClassName = '" + ddlClass1.Text + "', "
                        + " MeetTime = '" + Label1.Text + "', "
                        //+ " MeetDay = '" + ddlClass2.Text + "', "
                        //+ " Location = '" + ddlClass2.Text + "', "
                        + " PaidForClass = " + Convert.ToInt32(chbPaidClass1.Checked) + ", "
                        + " Active = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                        + " Comments = '" + txbComments.Text + "', "
                        //+ " Instructor = '" + ddlClass2.Text + "', "
                        //+ " DevotionalLeader = '" + ddlClass2.Text + "', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + " where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                        + " and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                        + " and StudentMiddleName = '" + Request.QueryString["StudentMiddleName"] + "' "
                        + " and meettime = '4:30-6:00 Class' "
                        + " and student = 1 ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql_UPDATE, con);
                cmd.Connection = con;
                //reader = cmd.ExecuteReader();
                if (cmd.ExecuteNonQuery() > 0)
                {	//Retrieve the first record only

                }

                //Perform an update to the database..for class2...RCM..1/31/11.
                sql_UPDATE2 = "Update PerformingArtsAcademyClassEnrollment  "
                        + "Set ClassName = '" + ddlClass2.Text + "', "
                        + " MeetTime = '" + Label2.Text + "', "
                        //+ " MeetDay = '" + ddlClass2.Text + "', "
                        //+ " Location = '" + ddlClass2.Text + "', "
                        + " PaidForClass = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                        + " Active = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                        + " Comments = '" + txbComments.Text + "', "
                        //+ " Instructor = '" + ddlClass2.Text + "', "
                        //+ " DevotionalLeader = '" + ddlClass2.Text + "', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + " where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                        + " and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                        + " and StudentMiddleName = '" + Request.QueryString["StudentMiddleName"] + "' "
                        + " and meettime = '6:30-8:00 Class' "
                        + " and student = 1 ";

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

        protected void UpdateAllFields()
        {
            string sql_UPDATE = "";
            string sql_UPDATE2 = "";

            try
            {
                con.Open();

                //Perform an update to the database.. for class1...RCM..1/31/11..
                sql_UPDATE = "Update PerformingArtsAcademyClassEnrollment  "
                        + "Set ClassName = '" + ddlClass1.Text + "', "
                        + " MeetTime = '" + Label1.Text + "', "
                        //+ " MeetDay = '" + ddlClass2.Text + "', "
                        //+ " Location = '" + ddlClass2.Text + "', "
                        + " PaidForClass = " + Convert.ToInt32(chbPaidClass1.Checked) + ", "
                        + " Active = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                        + " Comments = '" + txbComments.Text + "', "
                        //+ " Instructor = '" + ddlClass2.Text + "', "
                        //+ " DevotionalLeader = '" + ddlClass2.Text + "', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + " where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                        + " and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                        + " and StudentMiddleName = '" + Request.QueryString["StudentMiddleName"] + "' "
                        + " and meettime = '4:30-6:00 Class' "
                        + " and student = 1 ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql_UPDATE, con);
                cmd.Connection = con;
                //reader = cmd.ExecuteReader();
                if (cmd.ExecuteNonQuery() > 0)
                {	//Retrieve the first record only

                }

                //Perform an update to the database..for class2...RCM..1/31/11.
                sql_UPDATE2 = "Update PerformingArtsAcademyClassEnrollment  "
                        + "Set ClassName = '" + ddlClass2.Text + "', "
                        + " MeetTime = '" + Label2.Text + "', "
                        //+ " MeetDay = '" + ddlClass2.Text + "', "
                        //+ " Location = '" + ddlClass2.Text + "', "
                        + " PaidForClass = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                        + " Active = " + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                        + " Comments = '" + txbComments.Text + "', "
                        //+ " Instructor = '" + ddlClass2.Text + "', "
                        //+ " DevotionalLeader = '" + ddlClass2.Text + "', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                        + " where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                        + " and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                        + " and StudentMiddleName = '" + Request.QueryString["StudentMiddleName"] + "' "
                        + " and meettime = '6:30-8:00 Class' "
                        + " and student = 1 ";

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
            //Can't allow the student to remain in the program if they aren't enrolled in a class..RCM..12/14/11.
            if ((ddlClass1.Text == "Select a class.") && (ddlClass2.Text == "Select a class."))
            {
                RemoveFromProgram();
            }

            UpdateAllFields();
            Response.Clear();
            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + Request.QueryString["StudentLastName"] + "&StudentFirstName=" + Request.QueryString["StudentFirstName"] + "&StudentMiddleName=" + Request.QueryString["StudentMiddleName"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void RemoveFromProgram()
        {

            //Remove student from classes..  
            try
            {

                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyClassEnrollment "
                                                     + "WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                                                     + "AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                                     + "AND StudentMiddleName = '" + Request.QueryString["StudentMiddleName"] + "' "
                                                     + "AND student = 1 ";
                //+ "AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
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

            try
            {
                //Update the ParentGuardian information...RCM..10/7/10.
                string sqlUpdateStatement_ProgramsList = "";

                sqlUpdateStatement_ProgramsList = " UPDATE UIF_PerformingArts.dbo.ProgramsList " +
                    "SET "
                    + " performingarts = 0, "
                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                    + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                    + " "
                    + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                    + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                    + " AND MiddleName = '" + Request.QueryString["StudentMiddleName"] + "' "
                    + " AND student = 1 ";

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand_ProgramsList = new SqlCommand(sqlUpdateStatement_ProgramsList, con);
                if (sqlUpdateCommand_ProgramsList.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                    //Didn't find a record to update..RCM.
                }
            }
            catch (Exception lkjlk)
            {
                //lblInformation.Enabled = true;
                //lblInformation.Text = "The update to ProgramsList table failed.  Please fix and try again MSG: " + lkjlk.Message.ToString();
            }
        }

        protected void chbClass1_CheckedChanged(object sender, EventArgs e)
        {
            if (chbClass1.Checked = true)
            {
                chbClass1.Checked = false;
                ddlClass1.Enabled = true;
                cmdUpdate.Enabled = true;
                chbPaidClass1.Enabled = true;
            }
            else if (chbClass1.Checked = false)
            {
                chbClass1.Checked = true;
                ddlClass1.Enabled = false;
            }
        }

        protected void chbClass2_CheckedChanged(object sender, EventArgs e)
        { 
            if (chbClass2.Checked = true)
            {
                chbClass2.Checked = false;
                ddlClass2.Enabled = true;
                cmdUpdate.Enabled = true;
                chbPaidClass2.Enabled = true;
            }
            else if (chbClass2.Checked = false)
            {
                chbClass2.Checked = true;
                ddlClass2.Enabled = false;
            }
        }

        protected void lbClass1Attendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewStudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + Request.QueryString["StudentLastName"] + "&StudentFirstName=" + Request.QueryString["StudentFirstName"] + "&StudentMiddleName=" + Request.QueryString["StudentMiddleName"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void lbClass2Attendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewStudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + Request.QueryString["StudentLastName"] + "&StudentFirstName=" + Request.QueryString["StudentFirstName"] + "&StudentMiddleName=" + Request.QueryString["StudentMiddleName"] + "&Dept=" + Request.QueryString["Dept"]);
        }


        protected string GetClassLocation(string ClassName)
        {
            string ClassLocation = "";
            SqlDataReader reader = null;

            try
            {

                string sql_LookupLocation = "Select location from PerformingArtsAcademyAvailableClasses "
                                          + "Where ClassName = '" + ClassName + "' ";
                con.Open();

                SqlCommand cmd = new SqlCommand(sql_LookupLocation);
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    if (reader.IsDBNull(0))
                    {
                        ClassLocation = "ClassLocation";
                    }
                    else
                    {
                        ClassLocation = reader.GetSqlValue(0).ToString();
                    }
                }
            }
            catch (Exception lkjlaabbeede)
            {

            }
            finally
            {
                con.Close();
            }
            return ClassLocation;
        }

        protected void cmbNewEnrollment_Click(object sender, EventArgs e)
        {
            //Perform an INSERT operation to create a new record in the database..
            string sql_INSERT = "";
            string sql_INSERT2 = "";
            string ClassLocation = "ClassLocation";

            if (cmbNewEnrollment.Text == "Class1 New Enrollment")
            {
                try
                {
                    ClassLocation = GetClassLocation(ddlClass1.Text);
                }
                catch (Exception lkjlkaabb)
                {

                }

                try
                {
                    if (ddlClass1.Text != " ")
                    {
                        if (ClassSpaceAvailability(ddlClass1.Text))
                        {
                            con.Open();
                            sql_INSERT = "INSERT INTO PerformingArtsAcademyClassEnrollment "
                                        + "values ( "
                                        + "'" + Request.QueryString["StudentLastName"] + "', "
                                        + "'" + Request.QueryString["StudentFirstName"] + "', "
                                        + "'" + ddlClass1.Text + "', "
                                        + "'" + Label1.Text + "', "
                                        //+ "'" + lblClass1MeetDay.Text + "', "
                                        + "'Thursday', "
                                        + "'" + ClassLocation + "', "//Classlocation.
                                        + Convert.ToInt32(chbPaidClass1.Checked) + ", "
                                        + "1, "
                                        + "'" + txbComments.Text + "', "
                                        + "'Instructor', "
                                        + "'DevotionalLeader', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "'" + System.DateTime.Now.ToString() + "', "
                                        + "333, "
                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                        + "1, "
                                        + "0, "
                                        + "'" + Request.QueryString["StudentMiddleName"] + "') ";

                            //create a SQL command to update record
                            SqlCommand sql_InsertCommand = new SqlCommand(sql_INSERT, con);
                            if (sql_InsertCommand.ExecuteNonQuery() > 0)
                            {
                                cmbNewEnrollment.Text = "Remove from Class1";
                                cmdUpdate.Enabled = true;
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            //The class is full.  Display to the user!...RCM..1/17/12.
                            lblErrorInsert.Visible = true;
                            ddlClass1.Items.Add("Select a class.");
                            ddlClass1.Text = "Select a class.";
                            cmbNewEnrollment.Enabled = false;
                        }
                    }
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
            else if (cmbNewEnrollment.Text == "Remove from Class1")
            {
                RemoveFromClass1();
            }
        }


        protected Boolean ClassSpaceAvailability(string ClassName)
        {
            Boolean ClassSpaceAvailable = false;

            con2.Open();//Opens the db connection.

            string sql1 = "select SizeLimit "
                        + "from PerformingArtsAcademyAvailableClasses "
                        + "where classname = '" + ClassName + "' ";

            //Perform database lookup based on the chosen child..RCM..
            SqlCommand cmd = new SqlCommand(sql1);
            SqlDataReader reader = null;
                
            cmd.Connection = con2;
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {	//Retrieve the first record only
                if (reader.IsDBNull(0))
                {
                    //Do Nothing..just leave it as false..
                }
                else
                {
                    int LimitSizeField = reader.GetInt32(0);

                    try
                    {
                        reader.Close();
                        string sql2 = "Select count(*) "
                                    + "from PerformingArtsAcademyClassEnrollment "
                                    + "WHERE ClassName = '" + ClassName + "' "
                                    + "AND Student = 1 ";

                        //Perform database lookup based on the chosen child..RCM..
                        SqlCommand cmd2 = new SqlCommand(sql2);
                        SqlDataReader reader2 = null;

                        cmd2.Connection = con2;
                        reader2 = cmd2.ExecuteReader();
                        if (reader2.Read())
                        {	//Retrieve the first record only
                            if (reader2.IsDBNull(0))
                            {
                                //Do Nothing..just leave it as false..
                            }
                            else
                            {
                                int NumberOfRecords = reader2.GetInt32(0);

                                if (NumberOfRecords < LimitSizeField)
                                {
                                    ClassSpaceAvailable = true;
                                }
                            }
                        }
                    }
                    catch (Exception lkjlk)
                    {
                        //Do nothing..
                    }
                }
            }
            return ClassSpaceAvailable;
        }

        protected void RemoveFromClass1()
        {
            try
            {

                string sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyClassEnrollment "
                                            + " where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                                            + " and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                            + " and studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                                            + " and classname = '" + ddlClass1.Text.Trim() + "' "
                                            + " and meettime = '4:30-6:00 Class' "
                                            + " and student = 1 ";
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromClass1 = new SqlCommand(sql_DeleteFromClass1, con);
                if (sqlDeleteFromClass1.ExecuteNonQuery() > 0)
                {
                    cmbNewEnrollment.Text = "Class1 New Enrollment";
                    ddlClass1.Items.Add("Select a class.");
                    ddlClass1.Text = "Select a class.";
                    cmbNewEnrollment.Enabled = false;
                    ddlClass1.Enabled = true;
                    chbClass1.Checked = false;
                    chbPaidClass1.Enabled = true;

                    //Send an email to everyone so that someone follows up with a phonecall..RCM..10/24/11.
                    SendEmail();
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

        protected void SendEmail()
        {



        }

        protected void RemoveFromClass2()
        {
            try
            {
                string sql_DeleteFromClass2 = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyClassEnrollment "
                                            + " where studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                                            + " and studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                            + " and studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                                            + " and classname = '" + ddlClass2.Text.Trim() + "' "
                                            + " and meettime = '6:30-8:00 Class' "
                                            + " and student = 1 ";
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromClass2 = new SqlCommand(sql_DeleteFromClass2, con);
                if (sqlDeleteFromClass2.ExecuteNonQuery() > 0)
                {
                    cmbNewEnrollment2.Text = "Class2 New Enrollment";
                    ddlClass2.Items.Add("Select a class.");
                    ddlClass2.Text = "Select a class.";
                    cmbNewEnrollment2.Enabled = false;
                    ddlClass2.Enabled = true;
                    chbClass2.Checked = false;
                    chbPaidClass2.Enabled = true;

                    //Send an email to everyone so that someone follows up with a phonecall..RCM..10/24/11.
                    SendEmail();
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
        
        
        protected void cmbNewEnrollment2_Click(object sender, EventArgs e)
        {
            //Perform an INSERT operation to create a new record in the database..
            string sql_INSERT = "";
            string sql_INSERT2 = "";
            string ClassLocation = "ClassLocation";

            if (cmbNewEnrollment2.Text == "Class2 New Enrollment")
            {
                try
                {
                    try
                    {
                        ClassLocation = GetClassLocation(ddlClass2.Text);
                    }
                    catch (Exception lklaabbeecc)
                    {

                    }

                    //Check for the presence of a second class INSERT....RCM..1/29/11.
                    if (ddlClass2.Text != " ")
                    {
                        if (ClassSpaceAvailability(ddlClass2.Text))
                        {
                            con.Open();
                            sql_INSERT2 = "INSERT INTO PerformingArtsAcademyClassEnrollment "
                                    + "values ( "
                                    + "'" + Request.QueryString["StudentLastName"] + "', "
                                    + "'" + Request.QueryString["StudentFirstName"] + "', "
                                    + "'" + ddlClass2.Text + "', "
                                    + "'" + Label2.Text + "', "
                                    //+ "'" + lblClass2MeetDay.Text + "', "
                                    + "'Thursday', "
                                    + "'" + ClassLocation + "', "
                                    + Convert.ToInt32(chbPaidClass2.Checked) + ", "
                                    + "1, "
                                    + "'" + txbComments.Text + "', "
                                    + "'Instructor', "
                                    + "'DevotionalLeader', "
                                    + "'" + System.DateTime.Now.ToString() + "', "
                                    + "'" + System.DateTime.Now.ToString() + "', "
                                    + "333, "
                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                    + "1, "
                                    + "0, "
                                    + "'" + Request.QueryString["StudentMiddleName"] + "') ";

                            //create a SQL command to update record
                            SqlCommand sql_InsertCommand2 = new SqlCommand(sql_INSERT2, con);
                            if (sql_InsertCommand2.ExecuteNonQuery() > 0)
                            {
                                cmbNewEnrollment2.Text = "Remove from Class2";
                                cmdUpdate.Enabled = true;
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            //The class is full.  Display to the user!...RCM..1/17/12.
                            lblErrorInsert.Visible = true;
                            ddlClass2.Items.Add("Select a class.");
                            ddlClass2.Text = "Select a class.";
                            cmbNewEnrollment2.Enabled = false;
                        }
                    }
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
            else if (cmbNewEnrollment2.Text == "Remove from Class2")
            {
                RemoveFromClass2();
            }
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            //Can't allow the student to remain in the program if they aren't enrolled in a class..RCM..12/14/11.
            if ((ddlClass1.Text == "Select a class.") && (ddlClass2.Text == "Select a class."))
            {
                RemoveFromProgram();
            }

            UpdateAllFields();
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            //Can't allow the student to remain in the program if they aren't enrolled in a class..RCM..12/14/11.
            if ((ddlClass1.Text == "Select a class.") && (ddlClass2.Text == "Select a class."))
            {
                RemoveFromProgram();
            }

            UpdateAllFields();
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void ddlClass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClass1.Items.Remove("Select a class.");
            cmbNewEnrollment.Enabled = true;
            lblErrorInsert.Visible = false;
        }

        protected void ddlClass2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClass2.Items.Remove("Select a class.");
            cmbNewEnrollment2.Enabled = true;
            lblErrorInsert.Visible = false;
        }

        protected void cmbViewAvailability_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string sql = "";
                //sql = "select pca.ClassName, pca.SizeLimit as 'MaxCapacity', COUNT(pce.ClassName) as 'FilledSpots'  "
                //        + "from PerformingArtsAcademyAvailableClasses pca "
                //        + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pce "
                //        + "ON (pca.ClassName = pce.ClassName) "
                //        + "WHERE pca.ClassName NOT LIKE '-%' "
                //        //+  "--AND pce.student = 1 "
                //        //+  "--AND pce.Volunteer = 0 "
                //        + "AND pca.ClassName <> 'Miscellaneous Volunteer' "
                //        + "AND pce.student = 1 "
                //        + "group by pca.ClassName, pca.SizeLimit, pce.ClassName "
                //        + "order by pca.ClassName, pca.SizeLimit ";

                sql = "select pca.ClassName, pca.SizeLimit as 'MaxCapacity', COUNT(pce.ClassName) as 'FilledSpots'  "
                        + "from PerformingArtsAcademyAvailableClasses pca "
                        + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pce "
                        + "ON (pca.ClassName = pce.ClassName AND pce.Student = 1) "
                        + "WHERE pca.ClassName <> 'Miscellaneous Volunteer' "
                    //pca.ClassName NOT LIKE '-%' "
                    //+  "--AND pce.student = 1 "
                    //+  "--AND pce.Volunteer = 0 "
                    //+  "AND pce.student = 1 "
                        + "group by pca.ClassName, pca.SizeLimit, pce.ClassName "
                        + "order by pca.ClassName, pca.SizeLimit ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorProgram");
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                con.Close();
                GridView1.Visible = true;
                //cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }
    }
}