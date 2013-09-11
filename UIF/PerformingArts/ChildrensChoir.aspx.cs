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
    public partial class ChildrensChoir : System.Web.UI.Page
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

        protected void cmbWeeklyAttendanceSheets_Click(object sender, EventArgs e)
        {
            //Clear the gridview..RCM.
            gvVolunteerAttendance.DataSource = null;
            gvVolunteerAttendance.DataBind();
            gvVolunteerAttendance.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select '' as 'Attended' ,si.LastName, si.FirstName, si.MiddleName, si.currentregistrationform as 'RegForm', si.Address, si.HomePhone, si.Grade, si.TShirtSize, si.HealthConditions, pgc.ParentGuardian1, pgc.Parentguardianrelationship1 as 'PG1Relationship', pgc.CellPhone1 as 'PG1CellPhone' "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname AND si.MiddleName = pl.MiddleName and pl.Student = 1) "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pgc "
                             + "ON (si.lastname = pgc.studentlastname AND si.firstname = pgc.studentfirstname) "
                             + "WHERE pl.ChildrensChoir = 1 "
                             + "AND pl.student = 1 "
                             + "group by si.LastName, si.Firstname, si.MiddleName, si.currentregistrationform, si.address, si.homephone, si.grade, si.tshirtsize, si.healthconditions, pgc.ParentGuardian1, pgc.parentguardianrelationship1, pgc.CellPhone1 "
                             + "order by si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvGeneralUse.DataSource = ds.Tables[0];
                gvGeneralUse.DataBind();
                gvGeneralUse.Visible = true;
                con.Close();
                lblStudentAttendance.Text = "";
                lblStudentAttendance.Text = "Student Attendance List" + " for: " + System.DateTime.Now.ToString("MM-dd-yyyy");
                lblStudentAttendance.Visible = true;
            }
            catch (Exception lkjl)
            {

            }
        }

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvVolunteerAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvGeneralUse_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            //ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            //ExcelExport.ExportGridView(gvGeneralUse, Response);

            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            if ((gvGeneralUse.Rows.Count != 0))
            {
                gvGeneralUse.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvGeneralUse, Response);
            }
            else if (gvVolunteerAttendance.Rows.Count != 0)
            {
                gvVolunteerAttendance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvVolunteerAttendance, Response);
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

        protected void cmbVolunteerAttendance_Click(object sender, EventArgs e)
        {
            gvGeneralUse.DataSource = null;
            gvGeneralUse.DataBind();
            gvGeneralUse.Visible = false;
            try
            {
                con.Open();
                string sql_LoadGrid = "";
                sql_LoadGrid = "select '' as 'Comments','' as 'Attendance',vi.LastName, vi.FirstName, vi.address, vi.city, vi.state, vi.zip, vi.homephone, vi.cellphone, vi.email, vi.sex as 'Gender', vd.DMVCheck, vd.NationalCheck, vd.PaCriminalCheck, "
                            + "vd.spiritualjourney as 'SpirtJourn', vd.vehichleinsurance as 'Insured', vd.releasewaiver as 'Waiver', vd.generalinformation as 'GeneralInfo', vd.newvolunteertraining as 'NewVolTrain', vi.discipleshipmentortraining as 'DiscipleMentor' "
                            + "from UIF_PerformingArts.dbo.volunteerinformation vi "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                            + "ON (vi.lastname = pl.lastname AND vi.firstname = pl.firstname) "
                            + "LEFT OUTER JOIN UIF_PerformingArts.dbo.volunteerdetails vd "
                            + "ON (vi.lastname = vd.lastname AND vi.firstname = vd.firstname) "
                            + "where pl.staffvolunteer = 1 "
                            + "AND pl.childrenschoir = 1 "
                            + "GROUP BY vi.lastname, vi.firstname, vi.address, vi.city, vi.state, vi.zip, vi.homephone, vi.cellphone, vi.email, vi.sex,  vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, vd.newvolunteertraining, vi.discipleshipmentortraining, vd.DMVCheck, vd.NationalCheck, vd.PACriminalCheck "
                            + "ORDER BY LastName, FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "UIF_PerformingArts.dbo.volunteerinformation");
                gvVolunteerAttendance.DataSource = ds.Tables[0];
                gvVolunteerAttendance.DataBind();
                gvVolunteerAttendance.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChildrensChoir.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
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
                sql_LoadGrid = "select si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                             + "WHERE pl.ChildrensChoir = 1 "
                             + "GROUP BY si.Lastname, si.Firstname, si.MiddleName, si.Homephone, pg.ParentGuardian1,pg.CellPhone1,pg.WorkPhone1, pg.ParentGuardian2, pg.CellPhone2, pg.WorkPhone2, pg.EmergencyContact, pg.EmergencyContactPhone "
                             + "ORDER BY si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvVolunteerAttendance.DataSource = ds.Tables[0];
                gvVolunteerAttendance.DataBind();
                gvVolunteerAttendance.Visible = true;
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
                sql_LoadGrid = "select si.LastName,	si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "from StudentInformation si "
                             + "LEFT OUTER JOIN ParentGuardianContactInformation pg "
                             + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName) "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName AND pl.student = 1) "
                             + "WHERE pl.ChildrensChoir = 1 "
                             + "GROUP BY si.LastName, si.FirstName, si.MiddleName, si.HealthConditions, si.Notes "
                             + "ORDER BY si.LastName, si.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentInformation");
                gvVolunteerAttendance.DataSource = ds.Tables[0];
                gvVolunteerAttendance.DataBind();
                gvVolunteerAttendance.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
        }
    }
}