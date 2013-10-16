using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
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
using System.Net.Mail;
using UrbanImpactCommon;


namespace UrbanImpactCommon
{
    public class UIFCommon
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public int Counter = 0;


        public Boolean SetAFlag(Boolean waitinglist)
        {
            Boolean FromTheWaitingList = false;
            FromTheWaitingList = waitinglist;
            return FromTheWaitingList;
        }


        public void BuildHTMLMenuControlBehavior(System.Web.UI.WebControls.MenuEventArgs e, System.Web.HttpRequest Request, System.Web.HttpResponse Response)
        {
            string lkjl = "";            

        }



        public void InsertIntoSystemErrors(string PageName, string Description, string LastUpdatedBy)
        {

            Description = Description.Replace("'", "");
            PageName = PageName.Replace("'", "");

            try
            {
                string SystemErrors = "";
                SystemErrors = "Insert into UIF_PerformingArts.dbo.SystemErrors "
                            + "values ("
                            + "'" + PageName + "',"
                            + "'" + Description + "',"
                            + "'" + System.DateTime.Now.ToString() + "',"
                            + "'" + System.DateTime.Now.ToString() + "',"
                            + "'" + LastUpdatedBy + "')";

                con.Open();

                //create a SQL command to update record
                SqlCommand sqlInsertCommand = new SqlCommand(SystemErrors, con);
                if (sqlInsertCommand.ExecuteNonQuery() > 0)
                {
                    //maybe display a message confirming update has been successful                    
                }
                else
                {
                    //Insert didn't work..
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
                
        //public void MenuControlBehavior(System.Web.UI.WebControls.MenuEventArgs e, System.Web.HttpRequest Request,System.Web.HttpResponse Response, string lastname, string firstname)
        public void MenuControlBehavior(System.Web.UI.WebControls.MenuEventArgs e, System.Web.HttpRequest Request,System.Web.HttpResponse Response)
        {
            var newSiteBaseUrl = ConfigurationManager.AppSettings["NewSiteBaseUrl"];
            Boolean LimitAccess = false;
            Boolean fullaccess = false;
            Boolean partialaccess = false;
            string AccessLevel = "1";

            GlobalSecuity RestrictAccess = new GlobalSecuity();

            //Ryan C Manners.6/10/11.
            if (e.Item.Text == "New Coming Features")
            {
                //Response.Redirect("Volunteers.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                //e.Item.NavigateUrl = "";
            }
            else if (e.Item.Text == "Main Menu")
            {
                //if (LimitAccess)
                //{
                //    Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
                //}
                //else
                //{
                Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                //}
            }
            else if (e.Item.Text == "Volunteer Directory")
            {
                //LimitAccess = RestrictAccess.RestrictAccess(ref AccessLevel, ref partialaccess, ref fullaccess, lastname, firstname,"Volunteers"); 
                //if (LimitAccess)
                //{
                //    Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
                //}
                //else
                //{
                Response.Redirect("Volunteers.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                //}
            }
            else if (e.Item.Text == "Student Directory")
            {
                //LimitAccess = RestrictAccess.RestrictAccess(ref AccessLevel, ref partialaccess, ref fullaccess, lastname, firstname,"uifadmin2");
                //if (LimitAccess)
                //{
                //    Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                //}
                //else
                //{
                Response.Redirect("uifadmin2.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                //}
            }
            else if (e.Item.Text == "RSVP Gospel Tracking")
            {
                Response.Redirect("RSVPGospelTracking.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Impact Urban Schools")
            {
                Response.Clear();
                Response.Redirect("ImpactUrbanSchools.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Enter Student Attendance")
            {
                Response.Redirect("StudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "View Student Attendance")
            {
                Response.Redirect("ViewStudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Edit Student Attendance")
            {
                //Response.Redirect("StudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Enter Volunteer Attendance")
            {
                Response.Redirect("VolunteerAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "View Volunteer Attendance")
            {
                Response.Redirect("ViewVolunteerAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Edit Volunteer Attendance")
            {
                //Response.Redirect("StudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Athletics")
            {
                Response.Redirect("AthleticsProgramMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "PerformingArtsAcademy")
            {
                Response.Redirect("PerformingArtsAcademy.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "MSHS Choir")
            {
                Response.Redirect("MSHSChoir.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Childrens Choir")
            {
                Response.Redirect("ChildrensChoir.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Singers")
            {
                Response.Redirect("Singers.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Shakes")
            {
                Response.Redirect("Shakes.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "CoreKids")
            {
                Response.Redirect("CoreKidsProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Options")
            {
                Response.Redirect(newSiteBaseUrl + "Options");
            }
            else if (e.Item.Text == "KRA Reports")
            {
                Response.Redirect("KRA.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Systems Billboard")
            {
                Response.Redirect("SystemEnhancementsBulletin.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "DiscipleshipMentor")
            {
                Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + "&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "MSHS Choir")
            {
                Response.Redirect("MSHSChoir.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "SummerDay Camp")
            {
                Response.Redirect("SummerDayCamp.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Childrens Choir")
            {
                Response.Redirect("ChildrensChoir.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Student Search/Queries")
            {
                Response.Redirect("StudentAttendanceReporting.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Volunteer Search/Queries")
            {
                Response.Redirect("VolunteerSearchQueries.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Volunteer Background Details")
            {
                Response.Redirect("VolunteerDetails.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + "&VolunteerFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Mailing Lists")
            {
                Response.Redirect("MailingLists.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Attendance History Information")
            {
                Response.Redirect("AttendanceHistoryInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "StudentProgram Maintenance")
            {
                Response.Redirect("AthleticsProgramMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "VolunteerProgram Maintenance")
            {
                Response.Redirect("AthleticsVolunteerMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Student Attendance History")
            {
                Response.Redirect("AthleticsAttendanceHistory.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Volunteer Attendance History")
            {
                Response.Redirect("VolunteerAttendanceHistory.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "AthleticsProgramSection Maintenance")
            {
                Response.Redirect("AthleticsProgramSectionMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Education AdminMaintenance")
            {
                Response.Redirect("EducationProgramSectionMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Admin Functions")
            {
                Response.Redirect("AdminFunctions.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "Program Admin Maintenance")
            {
                Response.Redirect("PerformingArtsProgramSectionMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            else if (e.Item.Text == "LogOut")
            {
                Response.Redirect(newSiteBaseUrl + "Account/Logout");
            }
        }

        private void DatabaseRoutine(string abc)
        {

            string sqlquery = "";
            try
            {




            }
            catch (Exception lkjl)
            {





            }
        }


        //public static void Message(String message, Control cntrl)
        //{
        //    ScriptManager.RegisterStartupScript(cntrl, cntrl.GetType(), "alert", "alert('" + message + "');", true);
        //}
    }
}
