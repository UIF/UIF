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
using System.Web.Script.Services;
using System.Resources;
using System.Net;
using System.Data.Sql;
//using Microsoft.Office.Interop.Excel;


namespace UIF.PerformingArts
{
    public partial class PerformingArtsAcademy : System.Web.UI.Page
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
                    if (Department == "PerformingArts")
                    {
                        ddlFunctions.Items.Add("Please select a function");
                        ddlFunctions.Items.Add("Maintain Academy Class List");
                        ddlFunctions.Items.Add("Retrieve Class Lists");
                        //ddlFunctions.Items.Add("View Academy Enrollment");
                        //ddlFunctions.Items.Add("View Academy Volunteers");
                        //ddlFunctions.Items.Add("View Academy Class Availability");
                        //ddlFunctions.Items.Add("View Weeks Entered Attendance");
                        ddlFunctions.Items.Add("Emergency Information (UP)");
                        ddlFunctions.Items.Add("Health Conditions (UP)");
                        ddlFunctions.Items.Add("Emergency Information (4AC)");
                        ddlFunctions.Items.Add("Health Conditions (4AC)");
                        ddlFunctions.Items.Add("Emergency Information (WS)");
                        ddlFunctions.Items.Add("Health Conditions (WS)");
                        ddlFunctions.Text = "Please select a function";
                    }
                    else
                    {
                        Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                    }
                    
                    if ((Request.QueryString["LastName"] == "Anderson") || (Request.QueryString["FirstName"] == "Eric"))
                    {
                        cmbClearStudentEnrollment.Enabled = true;
                        cmbClearStudentEnrollment.Visible = true;
                    }

                    if ((Request.QueryString["LastName"] == "Manners") || (Request.QueryString["FirstName"] == "Ryan"))
                    {
                        cmbAdminPageLink.Enabled = true;
                        cmbAdminPageLink.Visible = true;
                    }
                }
            }
        }

        protected void cmbStudentList_Click(object sender, EventArgs e)
        {
            ddlClass1.Visible = false;
            ddlClass2.Visible = false;
            gvClassList.Visible = false;
            lblClassLists.Visible = false;

            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;

            gvEmergency.DataSource = null;
            gvEmergency.DataBind();
            gvEmergency.Visible = false;

            gvAcademyAvailability.DataSource = null;
            gvAcademyAvailability.DataBind();
            gvAcademyAvailability.Visible = false;

            gvClassList.DataSource = null;
            gvClassList.DataBind();
            gvClassList.Visible = false;

            gvAttendanceReport.DataSource = null;
            gvAttendanceReport.DataBind();
            gvAttendanceReport.Visible = false;
            
            try
            {
                //cmbStudentList.Enabled = false;
                con.Open();
                gvGeneralUse.Visible = true;

                string sql_LoadGrid = "";
                sql_LoadGrid = "select ce.studentlastname, ce.studentfirstname, ce.studentmiddlename, ce.classname, ce.meettime, ce.meetday, ce.location, ce.paidforclass, ce.comments, ps.currentregistrationform as 'regform', ps.homephone, ps.grade "
                             + "from PerformingArtsAcademyClassEnrollment ce "
                             + "LEFT OUTER JOIN StudentInformation ps "
                             + "ON (ce.studentlastname = ps.lastname AND ce.studentfirstname = ps.firstname) "
                             + "WHERE ce.student = 1 "
                             + "GROUP BY ce.studentlastname, ce.studentfirstname, ce.studentmiddlename, ce.classname, ce.meettime, ce.meetday, ce.location, ce.paidforclass, ce.active, ce.comments, ps.currentregistrationform, ps.homephone, ps.grade  "
                             + "order by ce.classname, ce.studentLastName, ce.studentFirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "PerformingArtsAcademyClassEnrollment");
                gvGeneralUse.DataSource = ds.Tables[0];
                gvGeneralUse.DataBind();
                con.Close();
                lblClassLists.Text = "Student Class Lists";
                lblClassLists.Visible = true;
            }
            catch (Exception lkjl)
            {

                string lkjlkj = "";

            }
        }

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvClassList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UIFAdmin2.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void gvGeneralUse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvEmergency_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }


        protected void cmbWeeklyAttendance_Click(object sender, EventArgs e)
        {

            string sql = "select sa.LastName, sa.FirstName, sa.Program, sa.Class, sa.Month, sa.Day, sa.Attended, sa.Comment, ps.HomePhone  ";
            //            + "from UIF_PerformingArts.dbo.StudentProgramAttendance sa "
            //            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.StudentInformation ps "
            //            + "ON (sa.LastName = ps.LastName AND sa.FirstName = ps.FirstName) "
            //            + "where sa.SysCreate > '" + System.DateTime.Now.Subtract( //2011-02-16' "
            //            + "GROUP BY sa.LastName, sa.FirstName, sa.Program, sa.Class, sa.Month, sa.Day, sa.Attended, sa.Comment, ps.HomePhone  "
            //            + "order by sa.Attended desc";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "StudentProgramAttendance");
            gvGeneralUse.DataSource = ds.Tables[0];
            gvGeneralUse.DataBind();
            con.Close();
        }

        public static void Message(String message, Control cntrl)
        {
            ScriptManager.RegisterStartupScript(cntrl, cntrl.GetType(), "alert", "alert('" + message + "');", true);
        }


        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        //protected void gvVolunteerAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    for (int i = 0; i < e.Row.Cells.Count; i++)
        //    {
        //        e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
        //    }
        //}

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvGeneralUse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }        


        protected void cmbClearStudentEnrollment_Click(object sender, EventArgs e)
        {

            Message("Testing message here", this);
                   
            //This will clear the PerformingArtsAcademyClassEnrollment table...to start fresh for a 
            //new semester.   Would be good to archive it to a backup table...RCM..2/18/11.
            try
            {
                //con.Open();

                //string sql = "Delete from PerformingArtsAcademyClassEnrollment ";

                //create a SQL command to update record
                //SqlCommand sqlUpdateCommand = new SqlCommand(sql, con);
                //if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //{
                //}
                //else
                //{
                    //Didn't find a record to update..RCM..
                //}
            }
            catch (Exception lkjaa)
            {




            }
        }

        protected void cmbInventory_Click(object sender, EventArgs e)
        {

            try
            {

                string sql_LoadGrid = "select * from academyinventory ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "academyinventory");
                grvInventory.DataSource = ds.Tables[0];
                grvInventory.DataBind();
                con.Close(); 
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }



        protected void grvInventory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow && row.RowIndex == irowNum)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                string gender = drv.Row.ItemArray.GetValue(0).ToString();
                string color = drv.Row.ItemArray.GetValue(1).ToString();
                string type = drv.Row.ItemArray.GetValue(2).ToString();
                string style = drv.Row.ItemArray.GetValue(3).ToString();
                string size = drv.Row.ItemArray.GetValue(4).ToString();
                string description = drv.Row.ItemArray.GetValue(5).ToString();
            }
        }

        protected void grvInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvInventory.SelectedRow;
            irowNum = grvInventory.SelectedIndex;
            bind();
        }






        protected void grvInventory_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewRow row = grvInventory.SelectedRow;
            
            irowNum = grvInventory.SelectedIndex;
            bind();
        }



        protected void grvInventory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grvInventory.Rows[e.NewSelectedIndex];
        }

        //public void bind()
        //{
        //    con.Open();

        //    string sql_LoadGrid = "select * from academyinventory ";

        //    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "academyinventory");
        //    grvInventory.DataSource = ds.Tables[0];
        //    grvInventory.DataBind();
        //    con.Close();
        //}

        protected void gvVolunteerList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvInventory.EditIndex = e.NewEditIndex;
            bind();
        }

        protected void gvVolunteerList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //gvVolunteerList.DeleteRow(e.RowIndex);
            grvInventory.DataBind();
        }

        protected void grvInventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvInventory.EditIndex = -1;
            bind();
        }

        protected void grvInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)grvInventory.Rows[e.RowIndex];
            //Label lbl = (Label)row.FindControl("lblid");
            TextBox gender = (TextBox)row.FindControl("textbox1");
            TextBox color = (TextBox)row.FindControl("textbox2");
            TextBox type = (TextBox)row.FindControl("textbox3");
            TextBox style = (TextBox)row.FindControl("textbox4");
            TextBox size = (TextBox)row.FindControl("textbox5");
            TextBox description = (TextBox)row.FindControl("textbox6");

            grvInventory.EditIndex = -1;
            con.Open();
            SqlCommand cmd = new SqlCommand("Update academyinventory set "
                                          + "  gender='" + gender.Text
                                          + "' , color='" + color.Text
                                          + "' , type='" + type.Text
                                          + "' , style='" + style.Text
                                          + "' , size='" + size.Text
                                          + "' , description='" + description.Text
                                          + " where ID = '1' ");

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            bind();
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
            if ((gvGeneralUse.Rows.Count != 0))
            {
                gvGeneralUse.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvGeneralUse, Response);
            }
            else if (gvClassList.Rows.Count != 0)
            {
                if ((ddlClass1.Text == "Please select a class list.") && (ddlClass2.Text != "Please select a class list."))
                {
                    int i = 0;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.Text = String.Format(ddlClass2.Text, i);
                    row.Cells.Add(cell);
                    gvClassList.Controls[0].Controls.AddAt(i, row); 

                    gvClassList.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                    ExcelExport.ExportGridView(gvClassList, Response, ddlClass2.Text);
                }
                else if ((ddlClass2.Text == "Please select a class list.") && (ddlClass1.Text != "Please select a class list."))
                {
                    int i = 0;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.Text = String.Format(ddlClass1.Text, i);
                    row.Cells.Add(cell);
                    gvClassList.Controls[0].Controls.AddAt(i, row);

                    gvClassList.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                    ExcelExport.ExportGridView(gvClassList, Response, ddlClass1.Text);
                }
                else
                {
                    int i = 0;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.Text = String.Format(ddlClass1.Text, i);
                    row.Cells.Add(cell);
                    gvClassList.Controls[0].Controls.AddAt(i, row);

                    gvClassList.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                    ExcelExport.ExportGridView(gvClassList, Response);
                }
            }
            else if (gvAcademyAvailability.Rows.Count != 0)
            {
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvAcademyAvailability, Response);
            }
            else if (gvEmergency.Rows.Count != 0)
            {
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvEmergency, Response);
            }
        }

        protected void cmbAdministerAcademyClasses_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProgramClassLists.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArtsAcademy.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbVolunteerAttendance_Click(object sender, EventArgs e)
        {
            ddlClass1.Visible = false;
            ddlClass2.Visible = false;
            gvClassList.Visible = false;
            lblClassLists.Visible = false;

            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;

            gvEmergency.DataSource = null;
            gvEmergency.DataBind();
            gvEmergency.Visible = false;

            gvAcademyAvailability.DataSource = null;
            gvAcademyAvailability.DataBind();
            gvAcademyAvailability.Visible = false;

            gvClassList.DataSource = null;
            gvClassList.DataBind();
            gvClassList.Visible = false;

            gvAttendanceReport.DataSource = null;
            gvAttendanceReport.DataBind();
            gvAttendanceReport.Visible = false;
            
            try
            {
                //cmbVolunteerAttendance.Enabled = false;
                con.Open();
                gvGeneralUse.Enabled = true;
                gvGeneralUse.Visible = true;

                string sql_LoadGrid = "";
                sql_LoadGrid = "select vi.LastName, vi.FirstName, vi.address, vi.city, vi.state, vi.zip, vi.homephone, vi.email, vi.sex as 'Gender', vd.backgroundcheck as 'BackgrndChk', "
                            + "vd.spiritualjourney as 'SpirtJourn', vd.vehichleinsurance as 'Insured', vd.releasewaiver as 'Waiver', vd.generalinformation as 'GeneralInfo', vd.newvolunteertraining as 'NewVolTrain', vi.discipleshipmentortraining as 'DiscipleMentor' "
                            + "from UIF_PerformingArts.dbo.volunteerinformation vi "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                            + "ON (vi.lastname = pl.lastname AND vi.firstname = pl.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (vi.lastname = vd.lastname AND vi.firstname = vd.firstname) "
                            + "where pl.staffvolunteer = 1 "
                            + "AND pl.performingarts = 1 "
                            + "GROUP BY vi.LastName, vi.FirstName, vi.address, vi.city, vi.state, vi.zip, vi.homephone, vi.email, vi.sex, vd.backgroundcheck, "
                            + "vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, vd.newvolunteertraining, vi.discipleshipmentortraining "
                            + "order by LastName, FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "UIF_PerformingArts.dbo.volunteerinformation");
                gvGeneralUse.DataSource = ds.Tables[0];
                gvGeneralUse.DataBind();
                lblClassLists.Text = "Program Volunteers";
                lblClassLists.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void RetrieveClassLists()
        {
            try
            {
                cmbRetrieveClassLists.Enabled = false;
                ddlClass1.Visible = true;
                ddlClass2.Visible = true;
                lblClassLists.Text = "Student Class Lists";

                string sql = "Select ClassName "
                            + "from PerformingArtsAcademyAvailableClasses "
                            + "Where MeetTime = '4:30-6:00 Class' "
                            + "and MeetDay = 'Thursday' "
                            + "group by ClassName "
                            + "order by ClassName ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "PerformingArtsAcademyAvailableClasses");

                //ddlClass1.Items.Add(" ");
                ddlClass1.Items.Clear();
                ddlClass1.Items.Add("Please select a class list.");
                //Iterate over setup records and call method to do the work on each one...RCM..
                foreach (DataRow myDataRowPO in custDS.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlClass1.Items.Add(myDataRowPO[0].ToString());
                }
                custDS.Clear();
                ddlClass1.Text = "Please select a class list.";

                string sql2 = "select ClassName "
                            + "from PerformingArtsAcademyAvailableClasses "
                            + "where MeetTime = '6:30-8:00 Class' "
                            + "and MeetDay = 'Thursday' "
                            + "group by ClassName "
                            + "order by ClassName ";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA2 = new SqlDataAdapter();
                custDA2.SelectCommand = cmd2;
                DataSet custDS2 = new DataSet();
                custDA2.Fill(custDS2, "PerformingArtsAcademyAvailableClasses");

                //ddlClass2.Items.Add(" ");
                ddlClass2.Items.Clear();
                ddlClass2.Items.Add("Please select a class list.");
                //Iterate over setup records and call method to do the work on each one...RCM..
                foreach (DataRow myDataRowPO in custDS2.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlClass2.Items.Add(myDataRowPO[0].ToString());
                }
                custDS2.Clear();
                ddlClass2.Text = "Please select a class list.";

                //Configuring the controls correctly for viewing...RCM..11/3/10.
                //ddlClassSelection.Enabled = true;
                //ddlClassSelection.Visible = true;
                //ddlClassSelection2.Enabled = true;
                //ddlClassSelection2.Visible = true;
                //lblInformation.Visible = false;
                //lblClass2.Enabled = true;
                //lblClass2.Visible = true;
                //lblClass1.Enabled = true;
                //lblClass1.Visible = true;
                //lblPAATracking.Enabled = true;
                //lblPAATracking.Visible = true;
            }
            catch (Exception lkjla)
            {


            }
            finally
            {

            }
        }

        protected void cmbRetrieveClassLists_Click(object sender, EventArgs e)
        {
            try
            {
                //ddlProgram.Enabled = false;
                //ddlProgram.Visible = false;
                //Label2.Visible = false;

                cmbRetrieveClassLists.Enabled = false;
                ddlClass1.Visible = true;
                ddlClass2.Visible = true;

                string sql = "Select ClassName "
                            + "from PerformingArtsAcademyAvailableClasses "
                            + "Where MeetTime = '4:30-6:00 Class' "
                            + "and MeetDay = 'Thursday' "
                            + "group by ClassName "
                            + "order by ClassName ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "PerformingArtsAcademyAvailableClasses");

                //ddlClass1.Items.Add(" ");
                ddlClass1.Items.Add("Please select a class list.");
                //Iterate over setup records and call method to do the work on each one...RCM..
                foreach (DataRow myDataRowPO in custDS.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlClass1.Items.Add(myDataRowPO[0].ToString());
                }
                custDS.Clear();
                ddlClass1.Text = "Please select a class list.";


                string sql2 = "select ClassName "
                            + "from PerformingArtsAcademyAvailableClasses "
                            + "where MeetTime = '6:30-8:00 Class' "
                            + "and MeetDay = 'Thursday' "
                            + "group by ClassName "
                            + "order by ClassName ";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA2 = new SqlDataAdapter();
                custDA2.SelectCommand = cmd2;
                DataSet custDS2 = new DataSet();
                custDA2.Fill(custDS2, "PerformingArtsAcademyAvailableClasses");

                //ddlClass2.Items.Add(" ");
                ddlClass2.Items.Add("Please select a class list.");
                //Iterate over setup records and call method to do the work on each one...RCM..
                foreach (DataRow myDataRowPO in custDS2.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlClass2.Items.Add(myDataRowPO[0].ToString());
                }
                custDS2.Clear();
                ddlClass2.Text = "Please select a class list.";

                //Configuring the controls correctly for viewing...RCM..11/3/10.
                //ddlClassSelection.Enabled = true;
                //ddlClassSelection.Visible = true;
                //ddlClassSelection2.Enabled = true;
                //ddlClassSelection2.Visible = true;
                //lblInformation.Visible = false;
                //lblClass2.Enabled = true;
                //lblClass2.Visible = true;
                //lblClass1.Enabled = true;
                //lblClass1.Visible = true;
                //lblPAATracking.Enabled = true;
                //lblPAATracking.Visible = true;
            }
            catch (Exception lkjla)
            {


            }
            finally
            {

            }
        }

        protected void ddlClass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlClass1.Text = " ";
            ddlClass2.Text = "Please select a class list.";
            bind();
            lblClassLists.Visible = true;

            //gvStudentList.Visible = true;
            //gvStudentList.Enabled = true;

            //calCalender2.Enabled = true;
            //calCalender2.Visible = true;
            //calCalender2.ShowTitle = true;
            //calCalender2.ShowNextPrevMonth = true;
            //calCalender2.ShowTitle = true;

            //lblInformation.Text = ddlClassSelection.Text + " students, during the 4:30-6:00pm Class";

            //cmbCommittAttendance.Enabled = false;
            //cmbCommittAttendance.Visible = true;
            //cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //lblInformation.Visible = true;
            //lblInformation.Enabled = true;
            //lblSetAttendance.Visible = true;
            cmbExcelExport.Visible = true;
        }

        protected void ddlClass2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlClass2.Text = " ";

            ddlClass1.Text = "Please select a class list.";
            bind2();
            lblClassLists.Visible = true;

            //gvStudentList.Visible = true;
            //gvStudentList.Enabled = true;

            //calCalender2.Enabled = true;
            //calCalender2.Visible = true;
            //calCalender2.ShowTitle = true;
            //calCalender2.ShowNextPrevMonth = true;
            //calCalender2.ShowTitle = true;

            //lblInformation.Text = ddlClassSelection.Text + " students, during the 4:30-6:00pm Class";

            //cmbCommittAttendance.Enabled = false;
            //cmbCommittAttendance.Visible = true;
            //cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //lblInformation.Visible = true;
            //lblInformation.Enabled = true;
            //lblSetAttendance.Visible = true;
            cmbExcelExport.Visible = true;
        }

        public void bind()
        {
            con.Open();

            try
            {
                string sql_LoadGrid = "";
                string sql_Volunteer = "";

                if (ddlClass1.Text.StartsWith("El"))
                {
                    sql_LoadGrid = "Select pac.studentfirstname as 'FirstName', pac.studentlastname as 'LastName', pac.studentmiddlename as 'MiddleName', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', '' as 'Attended', '' as 'Call If Absent', pac.paidforclass as 'Paid', pl.childrenschoir as 'Childrens Choir', ckp.CoreKid "
                                 + "from PerformingArtsAcademyClassEnrollment pac "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation si "
                                 + "ON (pac.studentfirstname = si.firstname AND pac.studentlastname = si.lastname AND pac.studentmiddlename = si.middlename) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                 + "ON (pac.studentfirstname = pl.firstname AND pac.studentlastname = pl.lastname AND pac.studentmiddlename = si.middlename) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.corekidsprogram ckp "
                                 + "ON (pac.studentfirstname = ckp.firstname AND pac.studentlastname = ckp.lastname) "
                                 + "where pac.meettime = '4:30-6:00 Class' "
                                 + "and pac.classname = '" + ddlClass1.Text + "' "
                                 //+ "and pac.student = 1 "
                                 + "group by pac.studentfirstname, pac.studentlastname, pac.studentmiddlename, si.homephone, si.studentcellphone, pac.paidforclass, pl.childrenschoir, pac.volunteer, pac.student, ckp.CoreKid "
                                 + "order by pac.volunteer, pac.student, pac.studentlastname, pac.studentfirstname ";
                }
                else if (ddlClass1.Text.StartsWith("MS/HS"))
                {
                    sql_LoadGrid = "Select pac.studentfirstname as 'FirstName', pac.studentlastname as 'LastName', pac.studentmiddlename as 'MiddleName', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', '' as 'Attended', '' as 'Call If Absent', pac.paidforclass as 'Paid', ckp.CoreKid "
                                 + "from PerformingArtsAcademyClassEnrollment pac "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation si "
                                 + "ON (pac.studentfirstname = si.firstname AND pac.studentlastname = si.lastname AND pac.studentmiddlename = si.middlename) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                 + "ON (pac.studentfirstname = pl.firstname AND pac.studentlastname = pl.lastname AND pac.studentmiddlename = si.middlename) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.corekidsprogram ckp "
                                 + "ON (pac.studentfirstname = ckp.firstname AND pac.studentlastname = ckp.lastname) "
                                 + "where pac.meettime = '4:30-6:00 Class' "
                                 + "and pac.classname = '" + ddlClass1.Text + "' "
                                 //+ "and pac.student = 1 "
                                 + "group by pac.studentfirstname, pac.studentlastname, pac.studentmiddlename, si.homephone, si.studentcellphone, pac.paidforclass, pac.volunteer, pac.student, ckp.CoreKid "
                                 + "order by pac.volunteer, pac.student, pac.studentlastname, pac.studentfirstname ";
                }
                
                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                DataSet custDS = new DataSet();
                da.Fill(ds, "PerformingArtsAcademyClassEnrollment");
                da.Fill(custDS, "PerformingArtsAcademyClassEnrollment");

                gvClassList.DataSource = null;
                gvClassList.DataBind();
                //gvClassList.Visible = false;
                
                gvClassList.DataSource = ds.Tables[0];
                gvClassList.DataBind();
                gvClassList.Visible = true;
            }
            catch (Exception lkjlkj)
            {


            }
            finally
            {
                con.Close();
            }
        }

        public void bind2()
        {
            con.Open();

            string sql_LoadGrid = "";
            string sql_Volunteer = "";

            if (ddlClass2.Text.StartsWith("El"))
            {
                sql_LoadGrid = "Select pac.studentfirstname as 'FirstName', pac.studentlastname as 'LastName', pac.studentmiddlename as 'MiddleName', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', '' as 'Attended', '' as 'Call If Absent', pac.paidforclass as 'Paid', pl.childrenschoir as 'Childrens Choir', ckp.CoreKid "
                          + "from PerformingArtsAcademyClassEnrollment pac "
                          + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation si "
                          + "ON (pac.studentfirstname = si.firstname AND pac.studentlastname = si.lastname AND pac.studentmiddlename = si.middlename) "
                          + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                          + "ON (pac.studentfirstname = pl.firstname AND pac.studentlastname = pl.lastname AND pac.studentmiddlename = pl.middlename) "
                          + "LEFT OUTER JOIN UIF_PerformingArts.dbo.corekidsprogram ckp "
                          + "ON (pac.studentfirstname = ckp.firstname AND pac.studentlastname = ckp.lastname) " 
                          + "where pac.meettime = '6:30-8:00 Class' "
                          + "and pac.classname = '" + ddlClass2.Text + "' "
                          //+ "and pac.student = 1 "
                          + "group by pac.studentfirstname, pac.studentlastname, pac.studentmiddlename, si.homephone, si.studentcellphone, pac.paidforclass, pl.childrenschoir, pac.volunteer, pac.student, ckp.CoreKid  "
                          + "order by pac.volunteer, pac.student, pac.studentlastname, pac.studentfirstname ";
                          //+ "order by pac.studentlastname, pac.studentfirstname ";
            }
            else if (ddlClass2.Text.StartsWith("MS/HS"))
            {
                sql_LoadGrid = "Select pac.studentfirstname as 'FirstName', pac.studentlastname as 'LastName', pac.studentmiddlename as 'MiddleName', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', '' as 'Attended', '' as 'Call If Absent', pac.paidforclass as 'Paid', ckp.CoreKid "
                          + "from PerformingArtsAcademyClassEnrollment pac "
                          + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation si "
                          + "ON (pac.studentfirstname = si.firstname AND pac.studentlastname = si.lastname AND pac.studentmiddlename = si.middlename) "
                          + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                          + "ON (pac.studentfirstname = pl.firstname AND pac.studentlastname = pl.lastname AND pac.studentmiddlename = pl.middlename) "
                          + "LEFT OUTER JOIN UIF_PerformingArts.dbo.corekidsprogram ckp "
                          + "ON (pac.studentfirstname = ckp.firstname AND pac.studentlastname = ckp.lastname) "
                          + "where pac.meettime = '6:30-8:00 Class' "
                          + "and pac.classname = '" + ddlClass2.Text + "' "
                          //+ "and pac.student = 1 "
                          + "group by pac.studentfirstname, pac.studentlastname, pac.studentmiddlename, si.homephone, si.studentcellphone, pac.paidforclass, pac.volunteer, pac.student, ckp.CoreKid  "
                          + "order by pac.volunteer, pac.student, pac.studentlastname, pac.studentfirstname ";
                          //+ "order by pac.studentlastname, pac.studentfirstname ";
            }

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "PerformingArtsAcademyClassEnrollment");
            
            gvClassList.DataSource = null;
            gvClassList.DataBind();
            //gvClassList.Visible = false;

            gvClassList.DataSource = ds.Tables[0];
            gvClassList.DataBind();
            gvClassList.Visible = true;
            con.Close();
        }


        protected void CollectTheRoster(string ClassName, string Time, ref ExcelUtility.ExcelUtility ExcelExport)
        {
            try
            {
                string sql_LoadGrid = "";

                if (ClassName.StartsWith("El"))
                {
                    sql_LoadGrid = "Select pac.studentfirstname as 'FirstName', pac.studentlastname as 'LastName', pac.studentmiddlename as 'MiddleName', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', '' as 'Attended', '' as 'Call If Absent', pac.paidforclass as 'Paid', pl.childrenschoir as 'Childrens Choir', ckp.CoreKid "
                                 + "from PerformingArtsAcademyClassEnrollment pac "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation si "
                                 + "ON (pac.studentfirstname = si.firstname AND pac.studentlastname = si.lastname AND pac.studentmiddlename = si.middlename) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                 + "ON (pac.studentfirstname = pl.firstname AND pac.studentlastname = pl.lastname AND pac.studentmiddlename = si.middlename) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.corekidsprogram ckp "
                                 + "ON (pac.studentfirstname = ckp.firstname AND pac.studentlastname = ckp.lastname) "
                                 + "where pac.meettime like '" + Time + "%' "
                                 + "and pac.classname = '" + ClassName + "' "
                                 //+ "and pac.student = 1 "
                                 + "group by pac.studentfirstname, pac.studentlastname, pac.studentmiddlename, si.homephone, si.studentcellphone, pac.paidforclass, pl.childrenschoir, pac.volunteer, pac.student, ckp.CoreKid "
                                 + "order by pac.volunteer, pac.student, pac.studentlastname, pac.studentfirstname ";
                }
                else if (ClassName.StartsWith("MS/HS"))
                {
                    sql_LoadGrid = "Select pac.studentfirstname as 'FirstName', pac.studentlastname as 'LastName', pac.studentmiddlename as 'MiddleName', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', '' as 'Attended', '' as 'Call If Absent', pac.paidforclass as 'Paid', ckp.CoreKid "
                                 + "from PerformingArtsAcademyClassEnrollment pac "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation si "
                                 + "ON (pac.studentfirstname = si.firstname AND pac.studentlastname = si.lastname AND pac.studentmiddlename = si.middlename) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                 + "ON (pac.studentfirstname = pl.firstname AND pac.studentlastname = pl.lastname AND pac.studentmiddlename = si.middlename) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.corekidsprogram ckp "
                                 + "ON (pac.studentfirstname = ckp.firstname AND pac.studentlastname = ckp.lastname) "
                                 + "where pac.meettime like '" + Time + "%' "
                                 + "and pac.classname = '" + ClassName + "' "
                                 //+ "and pac.student = 1 "
                                 + "group by pac.studentfirstname, pac.studentlastname, pac.studentmiddlename, si.homephone, si.studentcellphone, pac.paidforclass, pac.volunteer, pac.student, ckp.CoreKid "
                                 + "order by pac.volunteer, pac.student, pac.studentlastname, pac.studentfirstname ";
                }

                con.Open();
                
                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "PerformingArtsAcademyClassEnrollment");

                gvClassList.DataSource = null;//Clears out the grid and re-uses on each iteration.
                gvClassList.DataBind();
                gvClassList.DataSource = ds.Tables[0];
                gvClassList.DataBind();
                gvClassList.Visible = true;

                //gvRosterDump.DataSource = null;//Clears out the grid and re-uses on each iteration.
                //gvRosterDump.DataBind();
                //gvRosterDump.DataSource = ds.Tables[0];
                //gvRosterDump.DataBind();

                if (gvClassList.Rows.Count != 0)
                {
                    try
                    {
                        int i = 0;
                        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                        TableCell cell = new TableCell();
                        cell.Text = String.Format(ClassName, i);
                        row.Cells.Add(cell);
                        gvClassList.Controls[0].Controls.AddAt(i, row);

                        //gvRosterDump.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        //ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                        ExcelExport = new ExcelUtility.ExcelUtility();
                        ExcelExport.ExportGridView(gvClassList, Response, ClassName);
                                               
                        //int i = 0;
                        //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                        //TableCell cell = new TableCell();
                        //cell.Text = String.Format(ClassName, i);
                        //row.Cells.Add(cell);
                        //gvRosterDump.Controls[0].Controls.AddAt(i, row);

                        ////gvRosterDump.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        //ExcelExport = new ExcelUtility.ExcelUtility();
                        //ExcelExport.ExportGridView(gvRosterDump, Response, ClassName);

                    //    ExcelExport.AddWorksheetToExcelWorkbook("PAAClasses", ClassName, gvRosterDump);
                        
                        //ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                        //ExcelExport.ExportGridView(gvRosterDump, Response, ClassName);
                    }
                    catch (System.Threading.ThreadAbortException lException)
                    {
                        // do nothing
                    }
                    catch (Exception lkjlbbfddd)
                    {

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

        protected void gvClassList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbAcademyAvailability_Click(object sender, EventArgs e)
        {

            //ddlClass1.Visible = false;
            //ddlClass2.Visible = false;
            //gvClassList.Visible = false;
            //lblClassLists.Visible = false;

            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;

            gvEmergency.DataSource = null;
            gvEmergency.DataBind();
            gvEmergency.Visible = false;

            gvAcademyAvailability.DataSource = null;
            gvAcademyAvailability.DataBind();
            gvAcademyAvailability.Visible = false;

            //gvClassList.DataSource = null;
            //gvClassList.DataBind();
            //gvClassList.Visible = false;

            gvAttendanceReport.DataSource = null;
            gvAttendanceReport.DataBind();
            gvAttendanceReport.Visible = false;

            try
            {
                //ClearPage();
                //cmbWaitListReport.Enabled = false;
                //cmbViewAll.Enabled = false;
                //cmbViewEnrolledStudents.Enabled = false;
                //cmbBackToStudentPage.Enabled = false;
                //cmbVolunteerReport.Enabled = false;
                con.Open();

                string sql = "";

                //sql = "select pca.ClassName, pca.SizeLimit as 'MaxCapacity', COUNT(pce.ClassName) as 'FilledSpots' "
                //    + "from PerformingArtsAcademyAvailableClasses pca "
                //    + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pce "
                //    + "ON (pca.ClassName = pce.ClassName) "
                //    + "WHERE pca.ClassName NOT LIKE '-%' "
                //    + "AND pce.student = 1 "
                //    + "group by pca.ClassName, pca.SizeLimit "
                //    + "order by pca.ClassName, pca.SizeLimit ";

                sql = "select pca.ClassName, pca.SizeLimit as 'MaxCapacity', COUNT(pce.ClassName) as 'FilledSpots'  "
                        + "from PerformingArtsAcademyAvailableClasses pca "
                        +  "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pce "
                        +  "ON (pca.ClassName = pce.ClassName AND pce.Student = 1) "
                        +  "WHERE pca.ClassName <> 'Miscellaneous Volunteer' "
                        //pca.ClassName NOT LIKE '-%' "
                        //+  "--AND pce.student = 1 "
                        //+  "--AND pce.Volunteer = 0 "
                        //+  "AND pce.student = 1 "
                        +  "group by pca.ClassName, pca.SizeLimit, pce.ClassName "
                        +  "order by pca.ClassName, pca.SizeLimit ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DiscipleshipmentorProgram");
                gvAcademyAvailability.DataSource = ds.Tables[0];
                gvAcademyAvailability.DataBind();
                con.Close();
                gvAcademyAvailability.Visible = true;
                lblClassAvailability.Visible = true;
                //cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void cmbAttendanceReport_Click(object sender, EventArgs e)
        {
            con.Open();

            ddlClass1.Visible = false;
            ddlClass2.Visible = false;
            gvClassList.Visible = false;
            lblClassLists.Visible = false;

            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;

            gvEmergency.DataSource = null;
            gvEmergency.DataBind();
            gvEmergency.Visible = false;

            gvAcademyAvailability.DataSource = null;
            gvAcademyAvailability.DataBind();
            gvAcademyAvailability.Visible = false;

            gvClassList.DataSource = null;
            gvClassList.DataBind();
            gvClassList.Visible = false;

            gvAttendanceReport.DataSource = null;
            gvAttendanceReport.DataBind();
            gvAttendanceReport.Visible = false;

            try
            {
                string sql = "";
                sql = "select Section, LEFT(Day, 10) as 'Attendance Date' "
                    + "from StudentProgramAttendance "
                    + "where Program = 'PerformingArtsAcademy' "
                    + "and DAY > '" + DateTime.Now.Date.AddDays(-7).ToString("yyyy-MM-dd") + "' "
                    + "GROUP BY Section, Day "
                    + "ORDER BY Section, Day ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");
                gvAttendanceReport.DataSource = ds.Tables[0];
                gvAttendanceReport.DataBind();
                gvAttendanceReport.Visible = true;
                con.Close();
            }
            catch (Exception lkjlkjaa)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbEmergencyContact_Click(object sender, EventArgs e)
        {
            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select si.Lastname, si.Firstname, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like 'UP%' "
                             + "GROUP BY si.Lastname, si.Firstname, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "ORDER BY si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvClassList.DataSource = ds.Tables[0];
                gvClassList.DataBind();
                gvClassList.Visible = true;
                lblClassLists.Text = "Emergency Contact Information UP";
                lblClassLists.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void cmbHealthConditions_Click(object sender, EventArgs e)
        {
            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select si.LastName,	si.FirstName, si.HealthConditions, si.Notes "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like 'UP%' "
                             + "GROUP BY si.LastName, si.FirstName, si.HealthConditions, si.Notes "
                             + "ORDER BY si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvClassList.DataSource = ds.Tables[0];
                gvClassList.DataBind();
                gvClassList.Visible = true;
                lblClassLists.Text = "Student Health Conditions UP";
                lblClassLists.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void cmbEmergencyContact2_Click(object sender, EventArgs e)
        {
            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select si.Lastname, si.Firstname, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like '4AC%' "
                             + "GROUP BY si.Lastname, si.Firstname, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "ORDER BY si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvClassList.DataSource = ds.Tables[0];
                gvClassList.DataBind();
                gvClassList.Visible = true;
                lblClassLists.Text = "Emergency Contact Information 4AC";
                lblClassLists.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void cmbHealthConditions2_Click(object sender, EventArgs e)
        {
            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select si.LastName,	si.FirstName, si.HealthConditions, si.Notes "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like '4AC%' "
                             + "GROUP BY si.LastName, si.FirstName, si.HealthConditions, si.Notes "
                             + "ORDER BY si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvClassList.DataSource = ds.Tables[0];
                gvClassList.DataBind();
                gvClassList.Visible = true;
                lblClassLists.Text = "Student Health Conditions 4AC";
                lblClassLists.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void ddlFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql_LoadGrid = "";

            ddlClass1.Visible = false;
            ddlClass2.Visible = false;
            gvClassList.Visible = false;
            lblClassLists.Visible = false;
            lblClassAvailability.Visible = false;

            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;

            gvEmergency.DataSource = null;
            gvEmergency.DataBind();
            gvEmergency.Visible = false;

            gvAcademyAvailability.DataSource = null;
            gvAcademyAvailability.DataBind();
            gvAcademyAvailability.Visible = false;

            gvClassList.DataSource = null;
            gvClassList.DataBind();
            gvClassList.Visible = false;

            gvAttendanceReport.DataSource = null;
            gvAttendanceReport.DataBind();
            gvAttendanceReport.Visible = false;

            if (ddlFunctions.Text == "Maintain Academy Class List")
            {
                Response.Redirect("ProgramClassLists.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (ddlFunctions.Text == "Retrieve Class Lists")
            {
                RetrieveClassLists();
            }
            else if (ddlFunctions.Text == "View Academy Enrollment")
            {

            }
            else if (ddlFunctions.Text == "View Academy Volunteers")
            {

            }
            else if (ddlFunctions.Text == "View Academy Class Availability")
            {

            }
            else if (ddlFunctions.Text == "View Weeks Entered Attendance")
            {

            }
            else if (ddlFunctions.Text == "Emergency Information (UP)")
            {
                lblClassLists.Text = "Emergency Contact Information (UP)";
                sql_LoadGrid = "select si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName AND si.MiddleName = pav.StudentMiddleName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like 'UP%' "
                             + "GROUP BY si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "ORDER BY si.LastName, si.FirstName ";
            }
            else if (ddlFunctions.Text == "Health Conditions (UP)")
            {
                lblClassLists.Text = "Health Conditions Information (UP)";
                sql_LoadGrid = "select si.LastName,	si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName AND si.MiddleName = pav.StudentMiddleName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like 'UP%' "
                             + "GROUP BY si.LastName, si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "ORDER BY si.LastName, si.FirstName ";
            }
            else if (ddlFunctions.Text == "Emergency Information (4AC)")
            {
                lblClassLists.Text = "Emergency Contact Information (4AC)";
                sql_LoadGrid = "select si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName AND si.MiddleName = pav.StudentMiddleName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like '4AC%' "
                             + "GROUP BY si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "ORDER BY si.LastName, si.FirstName ";
            }
            else if (ddlFunctions.Text == "Health Conditions (4AC)")
            {
                lblClassLists.Text = "Health Conditions Information (4AC)";
                sql_LoadGrid = "select si.LastName,	si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName AND si.MiddleName = pav.StudentMiddleName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like '4AC%' "
                             + "GROUP BY si.LastName, si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "ORDER BY si.LastName, si.FirstName ";
            }
            else if (ddlFunctions.Text == "Emergency Information (WS)")
            {
                lblClassLists.Text = "Emergency Contact Information (WS)";
                sql_LoadGrid = "select si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName AND si.MiddleName = pav.StudentMiddleName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like 'WS%' "
                             + "GROUP BY si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "ORDER BY si.LastName, si.FirstName ";
            }
            else if (ddlFunctions.Text == "Health Conditions (WS)")
            {
                lblClassLists.Text = "Health Conditions Information (WS)";
                sql_LoadGrid = "select si.LastName,	si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                             + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pav "
                             + "ON (si.LastName = pav.StudentLastName AND si.FirstName = pav.StudentFirstName AND si.MiddleName = pav.StudentMiddleName) "
                             + "WHERE pl.PerformingArts = 1 "
                             + "AND pav.Student = 1 "
                             + "AND pav.Location like 'WS%' "
                             + "GROUP BY si.LastName, si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "ORDER BY si.LastName, si.FirstName ";
            }

            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");

                gvEmergency.DataSource = null;
                gvEmergency.DataBind();
                //gvEmergency.Visible = false;
                                
                gvEmergency.DataSource = ds.Tables[0];
                gvEmergency.DataBind();
                gvEmergency.Visible = true;

                //lblClassLists.Text = "Emergency Contact Information UP";
                lblClassLists.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void cmbAdminPageLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArtsProgramSectionMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbAutomateRosters_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=\"" + "PAA_Rosters" + ".xml\"";
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            sw.WriteLine("<?xml version=\"1.0\"?>");
            sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
            sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sw.WriteLine("xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            sw.WriteLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            sw.WriteLine("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sw.WriteLine("xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            sw.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            sw.WriteLine("<LastAuthor>Try Not Catch</LastAuthor>");
            sw.WriteLine("<Created>2013-01-09T19:14:19Z</Created>");
            sw.WriteLine("<Version>11.9999</Version>");
            sw.WriteLine("</DocumentProperties>");
            sw.WriteLine("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            sw.WriteLine("<WindowHeight>9210</WindowHeight>");
            sw.WriteLine("<WindowWidth>19035</WindowWidth>");
            sw.WriteLine("<WindowTopX>0</WindowTopX>");
            sw.WriteLine("<WindowTopY>90</WindowTopY>");
            sw.WriteLine("<ProtectStructure>False</ProtectStructure>");
            sw.WriteLine("<ProtectWindows>False</ProtectWindows>");
            sw.WriteLine("</ExcelWorkbook>");
            sw.WriteLine("<Styles>");
            sw.WriteLine("<Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\"/>");
            sw.WriteLine("<Borders/>");
            sw.WriteLine("<Font/>");
            sw.WriteLine("<Interior/>");
            sw.WriteLine("<NumberFormat/>");
            sw.WriteLine("<Protection/>");
            sw.WriteLine("</Style>");
            sw.WriteLine("<Style ss:ID=\"s22\">");
            sw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>");
            sw.WriteLine("<Borders>");
            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("</Borders>");
            sw.WriteLine("<Font ss:Bold=\"1\"/>");
            sw.WriteLine("</Style>");
            sw.WriteLine("<Style ss:ID=\"s23\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
            sw.WriteLine("<Borders>");
            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("</Borders>");
            sw.WriteLine("</Style>");
            sw.WriteLine("<Style ss:ID=\"s24\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
            sw.WriteLine("<Borders>");
            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("</Borders>");
            sw.WriteLine("<Font ss:Color=\"#FFFFFF\"/>");
            sw.WriteLine("<Interior ss:Color=\"#FF6A6A\" ss:Pattern=\"Solid\"/>");
            //set header colour here
            sw.WriteLine("</Style>");
            sw.WriteLine("</Styles>");

            DataTable datatable = new DataTable();

            try
            {
                string sql = "Select ClassName, MeetTime "
                            + "from PerformingArtsAcademyAvailableClasses "
                            + "Where ((MeetTime = '4:30-6:00 Class') OR (MeetTime = '6:30-8:00 Class')) "
                            + "and MeetDay = 'Thursday' "
                            + "group by ClassName, MeetTime "
                            + "order by ClassName ";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "PerformingArtsAcademyAvailableClasses");

                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                
                //Iterate over setup records and call method to do the work on each one...RCM..
                foreach (DataRow myDataRowPO in custDS.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                {

                    try
                    {
                        string sql_LoadGrid = "";

                        if (myDataRowPO[0].ToString().StartsWith("El"))
                        {
                            sql_LoadGrid = "Select pac.studentfirstname as 'FirstName', pac.studentlastname as 'LastName', pac.studentmiddlename as 'MiddleName', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', '' as 'Attended', '' as 'Call If Absent', pac.paidforclass as 'Paid', pl.childrenschoir as 'Childrens Choir', ckp.CoreKid "
                                         + "from PerformingArtsAcademyClassEnrollment pac "
                                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation si "
                                         + "ON (pac.studentfirstname = si.firstname AND pac.studentlastname = si.lastname AND pac.studentmiddlename = si.middlename) "
                                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                         + "ON (pac.studentfirstname = pl.firstname AND pac.studentlastname = pl.lastname AND pac.studentmiddlename = si.middlename) "
                                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.corekidsprogram ckp "
                                         + "ON (pac.studentfirstname = ckp.firstname AND pac.studentlastname = ckp.lastname) "
                                         //+ "where pac.meettime like '" + Time + "%' "
                                         + "and pac.classname = '" + myDataRowPO[0].ToString() + "' "
                                //+ "and pac.student = 1 "
                                         + "group by pac.studentfirstname, pac.studentlastname, pac.studentmiddlename, si.homephone, si.studentcellphone, pac.paidforclass, pl.childrenschoir, pac.volunteer, pac.student, ckp.CoreKid "
                                         + "order by pac.volunteer, pac.student, pac.studentlastname, pac.studentfirstname ";
                        }
                        else if (myDataRowPO[0].ToString().StartsWith("MS/HS"))
                        {
                            sql_LoadGrid = "Select pac.studentfirstname as 'FirstName', pac.studentlastname as 'LastName', pac.studentmiddlename as 'MiddleName', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', '' as 'Attended', '' as 'Call If Absent', pac.paidforclass as 'Paid', ckp.CoreKid "
                                         + "from PerformingArtsAcademyClassEnrollment pac "
                                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation si "
                                         + "ON (pac.studentfirstname = si.firstname AND pac.studentlastname = si.lastname AND pac.studentmiddlename = si.middlename) "
                                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                         + "ON (pac.studentfirstname = pl.firstname AND pac.studentlastname = pl.lastname AND pac.studentmiddlename = si.middlename) "
                                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.corekidsprogram ckp "
                                         + "ON (pac.studentfirstname = ckp.firstname AND pac.studentlastname = ckp.lastname) "
                                         //+ "where pac.meettime like '" + Time + "%' "
                                         + "and pac.classname = '" + myDataRowPO[0].ToString() + "' "
                                //+ "and pac.student = 1 "
                                         + "group by pac.studentfirstname, pac.studentlastname, pac.studentmiddlename, si.homephone, si.studentcellphone, pac.paidforclass, pac.volunteer, pac.student, ckp.CoreKid "
                                         + "order by pac.volunteer, pac.student, pac.studentlastname, pac.studentfirstname ";
                        }

                        con.Open();

                        SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "PerformingArtsAcademyClassEnrollment");

                        gvClassList.DataSource = null;//Clears out the grid and re-uses on each iteration.
                        gvClassList.DataBind();
                        gvClassList.DataSource = ds.Tables[0];
                        gvClassList.DataBind();
                        gvClassList.Visible = true;

                   //     datatable = gvClassList.DataBind();

                    }
                    catch (Exception lkjlkaabbaaa)
                    {

                    }

                    //Call for each class to generate the class list...RCM..2/28/13.
                    CreateWorkSheet(myDataRowPO[0].ToString(), sw, datatable,80);
                    //CollectTheRoster(myDataRowPO[0].ToString(), myDataRowPO[1].ToString(), ref ExcelExport);
                    //CollectTheRoster(myDataRowPO[0].ToString(), "6:30-8:00");
                }
                //ExcelExport.GenerateExcelFile

                custDS.Clear();
            }
            catch (Exception lkjla)
            {

            }
            finally
            {

            }
        }

        //Code from internet------------------------------------------------
        //protected void Page_Load(object sender, System.EventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    //Patient Data Binding
        //    dt = GetPatientData();
        //    gvPatient.DataSource = dt;
        //    gvPatient.DataBind();
        //    PatientData = dt;
        //    //Student Data Binding
        //    dt = GetStudentData();
        //    gvStudent.DataSource = dt;
        //    gvStudent.DataBind();
        //    StudentData = dt;
        //}

        /// <summary>
        /// Get Patient Data - Sample Data creation
        /// </summary>
        /// <returns></returns>
        private DataTable GetPatientData()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable("Patients");
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            return table;
        }

        /// <summary>
        /// Get Student Data - Sample Data creation
        /// </summary>
        /// <returns></returns>
        private DataTable GetStudentData()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable("Students");
            table.Columns.Add("SrNo", typeof(int));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("Age", typeof(int));

            // Here we add five DataRows.
            table.Rows.Add(1, "Sandeep", "Ramani", 29);
            table.Rows.Add(2, "Kapil", "Bhaai", 28);
            table.Rows.Add(3, "Vinit", "Shah", 28);
            table.Rows.Add(4, "Samir", "Bhaai", 30);
            table.Rows.Add(5, "Umang", "Samani", 29);
            return table;
        } 

        protected void btnExportBoth_Click(object sender, EventArgs e)
        {
            object[] myGridViews = new object[2];
//            myGridViews[0] = PatientData;
//            myGridViews[1] = StudentData;
            CreateWorkBook(myGridViews, "ExportToExcel", 80);
        }

        /// <summary>
        /// Method to create workbook
        /// </summary>
        /// <param name="cList"></param>
        /// <param name="wbName"></param>
        /// <param name="CellWidth"></param>
        private void CreateWorkBook(object[] cList, string wbName, int CellWidth)
        {
            string attachment = "attachment; filename=\"" + wbName + ".xml\"";
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            sw.WriteLine("<?xml version=\"1.0\"?>");
            sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
            sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sw.WriteLine("xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            sw.WriteLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            sw.WriteLine("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sw.WriteLine("xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            sw.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            sw.WriteLine("<LastAuthor>Try Not Catch</LastAuthor>");
            sw.WriteLine("<Created>2013-01-09T19:14:19Z</Created>");
            sw.WriteLine("<Version>11.9999</Version>");
            sw.WriteLine("</DocumentProperties>");
            sw.WriteLine("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            sw.WriteLine("<WindowHeight>9210</WindowHeight>");
            sw.WriteLine("<WindowWidth>19035</WindowWidth>");
            sw.WriteLine("<WindowTopX>0</WindowTopX>");
            sw.WriteLine("<WindowTopY>90</WindowTopY>");
            sw.WriteLine("<ProtectStructure>False</ProtectStructure>");
            sw.WriteLine("<ProtectWindows>False</ProtectWindows>");
            sw.WriteLine("</ExcelWorkbook>");
            sw.WriteLine("<Styles>");
            sw.WriteLine("<Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\"/>");
            sw.WriteLine("<Borders/>");
            sw.WriteLine("<Font/>");
            sw.WriteLine("<Interior/>");
            sw.WriteLine("<NumberFormat/>");
            sw.WriteLine("<Protection/>");
            sw.WriteLine("</Style>");
            sw.WriteLine("<Style ss:ID=\"s22\">");
            sw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>");
            sw.WriteLine("<Borders>");
            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("</Borders>");
            sw.WriteLine("<Font ss:Bold=\"1\"/>");
            sw.WriteLine("</Style>");
            sw.WriteLine("<Style ss:ID=\"s23\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
            sw.WriteLine("<Borders>");
            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("</Borders>");
            sw.WriteLine("</Style>");
            sw.WriteLine("<Style ss:ID=\"s24\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
            sw.WriteLine("<Borders>");
            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("</Borders>");
            sw.WriteLine("<Font ss:Color=\"#FFFFFF\"/>");
            sw.WriteLine("<Interior ss:Color=\"#FF6A6A\" ss:Pattern=\"Solid\"/>");
            //set header colour here
            sw.WriteLine("</Style>");
            sw.WriteLine("</Styles>");
            foreach (DataTable myTable in cList)
            {
                CreateWorkSheet(myTable.TableName, sw, myTable, CellWidth);
            }
            sw.WriteLine("</Workbook>");
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Method to create worksheet
        /// </summary>
        /// <param name="wsName"></param>
        /// <param name="sw"></param>
        /// <param name="dt"></param>
        /// <param name="cellwidth"></param>
        private void CreateWorkSheet(string wsName, System.IO.StringWriter sw, DataTable dt, int cellwidth)
        {
            if (dt.Columns.Count > 0)
            {
                sw.WriteLine("<Worksheet ss:Name=\"" + wsName + "\">");
                int cCount = dt.Columns.Count;
                long rCount = dt.Rows.Count + 1;
                sw.WriteLine("<Table ss:ExpandedColumnCount=\"" + cCount + 
                    "\" ss:ExpandedRowCount=\"" + rCount + "\"x:FullColumns=\"1\"");
                sw.WriteLine("x:FullRows=\"1\">");
                for (int i = (cCount - cCount); i <= (cCount - 1); i++)
                {
                    sw.WriteLine("<Column ss:AutoFitWidth=\"1\" ss:Width=\"" + cellwidth + "\"/>");
                }
                DataTableRowIteration(dt, sw);
                sw.WriteLine("</Table>");
                sw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                sw.WriteLine("<Selected/>");
                sw.WriteLine("<DoNotDisplayGridlines/>");
                sw.WriteLine("<ProtectObjects>False</ProtectObjects>");
                sw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
                sw.WriteLine("</WorksheetOptions>");
                sw.WriteLine("</Worksheet>");
            }
        }

        /// <summary>
        /// Method to create rows by iterating thru datatable rows
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sw"></param>
        private void DataTableRowIteration(DataTable dt, System.IO.StringWriter sw)
        {
                sw.WriteLine("");
                foreach (DataColumn dc in dt.Columns)
                {
                string tcText = dc.ColumnName;
                sw.WriteLine("<data>" + tcText + "</data>");
                }
                sw.WriteLine("");
                foreach (DataRow dr in dt.Rows)
                {
                sw.WriteLine("");
                foreach (DataColumn tc in dt.Columns)
                {
                    string gcText = dr[tc].ToString();
                    sw.WriteLine("<data>" + gcText + "</data>");
                }
                sw.WriteLine("");
                }
        }

        //------------------------------------------------------------

        protected void gvRosterDump_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvRosterDump_RowDataBound(object sender, EventArgs e)
        {

        }
    
    }
}