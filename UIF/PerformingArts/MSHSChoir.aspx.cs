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
    public partial class MSHSChoir : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public Boolean flag = false;
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

                if (Department == "PerformingArts")
                {


                }
                else
                {
                    Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                }
            }
        }

        protected void cmbAttendanceSheets_Click(object sender, EventArgs e)
        {
            gvViewAttendance.DataSource = null;
            gvViewAttendance.DataBind();
            gvViewAttendance.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select '' as 'Comments','' as 'Attendance',si.LastName, si.FirstName, si.MiddleName, ckp.CoreKid, si.currentregistrationform as 'RegForm', si.MeetCCGF, si.Address as 'Address    ', si.HomePhone, si.StudentCellPhone as 'StudentCell', si.TShirtSize, si.retreatconsentform as 'RetreatForm', si.parentalconsentform as 'ParentConsent', si.studentchoirquestionareform as 'StudentApp'"
                            + "from StudentInformation si "
                            + "LEFT OUTER JOIN ProgramsList pl "
                            + "ON (pl.LastName = si.Lastname AND pl.firstname = si.firstname AND pl.MiddleName = si.MiddleName AND pl.student = 1) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.CoreKidsProgram ckp "
                            + "ON (si.lastname = ckp.lastname AND si.firstname = ckp.firstname) "
                            + "WHERE pl.mshschoir = 1 "
                            + "AND pl.student = 1 "
                            + "AND (si.grade <> 'GR' AND si.grade <> 'G12' AND si.grade <> 'G11' AND si.grade <> 'GR12' AND si.grade <> 'GR11') "
                            //+ "OR (si.grade = 'SV') "
                            + "GROUP BY si.LastName, si.FirstName, si.MiddleName, ckp.CoreKid, si.currentregistrationform,  si.MeetCCGF, si.Address, si.HomePhone, si.StudentCellPhone, si.TShirtSize, si.retreatconsentform, si.parentalconsentform, si.studentchoirquestionareform "
                            + "ORDER BY si.LastName, si.FirstName ";


                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvGeneralDisplay.DataSource = ds.Tables[0];
                gvGeneralDisplay.DataBind();
                gvGeneralDisplay.Visible = true;
                con.Close();
                lblStudentAttendance.Text = "";
                lblStudentAttendance.Text = "Student Attendance List" + " for: " + System.DateTime.Now.ToString("MM-dd-yyyy");
                lblStudentAttendance.Visible = true;
                lblAttendance.Visible = true;
            }
            catch (Exception lkjl)
            {

            }
        }

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvViewAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvGeneralDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }        


        protected void cmbWeeklyAttendance_Click(object sender, EventArgs e)
        {
            gvGeneralDisplay.Visible = false;
            try
            {
                con.Open();

                string sql_LoadGrid = "";
                sql_LoadGrid = "select LastName, FirstName, MONTH, Day, Attended "
                            +  "from StudentProgramAttendance "
                            +  "where Program = 'MSHS Choir' "
                            +  "order by LastName, FirstName, day, month, attended ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");
                gvViewAttendance.DataSource = ds.Tables[0];
                gvViewAttendance.DataBind();
                gvViewAttendance.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {



            }
        }

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UIFAdmin2.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbResetPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("MSHSChoir.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            //ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            //ExcelExport.ExportGridView(gvGeneralDisplay, Response);
            //Email capability..?


            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            if ((gvGeneralDisplay.Rows.Count != 0) && (gvGeneralDisplay.Visible = true))
            {
                gvGeneralDisplay.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvGeneralDisplay, Response);
            }
            else if ((gvViewAttendance.Rows.Count != 0) && (gvViewAttendance.Visible = true))
            {
                gvViewAttendance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvViewAttendance, Response);
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

        protected void cmbProgramVolunteers_Click(object sender, EventArgs e)
        {
            gvGeneralDisplay.DataSource = null;
            gvGeneralDisplay.DataBind();
            gvGeneralDisplay.Visible = false;
            try
            {
                con.Open();

                string sql_LoadGrid = "";
                sql_LoadGrid = "select '' as 'Comments','' as 'Attendance',vi.LastName, vi.FirstName, vi.Address, vi.City, vi.State, vi.Zip, vi.HomePhone, vi.CellPhone, vi.Email, vi.sex as 'Gender', vd.GeneralInformation as 'GenInfo', "
                            + "vd.spiritualjourney as 'SpirtJourn', vd.vehichleinsurance as 'Insured', vd.releasewaiver as 'Waiver', vd.newvolunteertraining as 'NewVolTrain', vi.discipleshipmentortraining as 'DiscipleMentor' "
                            + "from UIF_PerformingArts.dbo.volunteerinformation vi "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                            + "ON (vi.lastname = pl.lastname AND vi.firstname = pl.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerDetails vd "
                            + "ON (vi.lastname = vd.lastname AND vi.firstname = vd.firstname) "
                            + "where pl.staffvolunteer = 1 "
                            + "AND pl.mshschoir = 1 "
                            + "GROUP BY vi.LastName, vi.FirstName, vi.address, vi.city, vi.state, vi.zip, vi.homephone, vi.cellphone, vi.email, vi.sex "
                            + ",vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, vd.newvolunteertraining, vi.discipleshipmentortraining "
                            + "order by LastName, FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "UIF_PerformingArts.dbo.volunteerinformation");
                gvViewAttendance.DataSource = ds.Tables[0];
                gvViewAttendance.DataBind();
                gvViewAttendance.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void gvGeneralDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbEmergencyContact_Click(object sender, EventArgs e)
        {
            gvGeneralDisplay.DataSource = null;
            gvGeneralDisplay.DataBind();
            gvGeneralDisplay.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName AND pl.student = 1) "
                             + "WHERE pl.MSHSChoir = 1 "
                             + "GROUP BY si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "ORDER BY si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvViewAttendance.DataSource = ds.Tables[0];
                gvViewAttendance.DataBind();
                gvViewAttendance.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void cmbHealthConditions_Click(object sender, EventArgs e)
        {
            gvGeneralDisplay.DataSource = null;
            gvGeneralDisplay.DataBind();
            gvGeneralDisplay.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select si.LastName,	si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName AND pl.student = 1) "
                             + "WHERE pl.MSHSChoir = 1 "
                             + "GROUP BY si.LastName, si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "ORDER BY si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvViewAttendance.DataSource = ds.Tables[0];
                gvViewAttendance.DataBind();
                gvViewAttendance.Visible = true;
                con.Close();
                //lblStudentAttendance.Text = "";
                //lblStudentAttendance.Text = "Student Attendance List" + " for: " + System.DateTime.Now.ToString("MM-dd-yyyy");
                //lblStudentAttendance.Visible = true;
                //lblAttendance.Visible = true;
            }
            catch (Exception lkjl)
            {

            }
        }
    }
}