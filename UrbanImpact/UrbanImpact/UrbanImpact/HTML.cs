using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web;

namespace UrbanImpactCommon
{
    public class HTML
    {
        //public void BuildMenuControl(System.Web.UI.WebControls.MenuEventArgs e, System.Web.HttpRequest Request, System.Web.HttpResponse Response)
        public Menu BuildMenuControl(Menu MenuBest)
        {
            // Create a new Menu control.    
            //Menu MenuBest = new Menu();              

            try
            {
                // Set the properties of the Menu control.   
                //MenuBest.ID = "MenuBest";

                //MenuBest.Height = 37;
                //MenuBest.Width = 947;
                //MenuBest.MaximumDynamicDisplayLevels = 8;
                //MenuBest.StaticEnableDefaultPopOutImage = false;
                //MenuBest.Font.Bold = true;
                //MenuBest.Font.Size = 15;
                ////MenuBest.RenderingMode = MenuRenderingMode.Default
                //MenuBest.BackColor = System.Drawing.Color.White;
                //MenuBest.BorderColor = System.Drawing.Color.Black;
                //MenuBest.ForeColor = System.Drawing.Color.Black;
                //MenuBest.Orientation = Orientation.Horizontal;

                //MenuBest.DynamicHoverStyle.BackColor = System.Drawing.Color.White;
                //MenuBest.DynamicHoverStyle.Font.Bold = false;
                //MenuBest.DynamicHoverStyle.Font.Strikeout = false;
                ////MenuBest.DynamicHoverStyle.Height = 20;
                //MenuBest.DynamicHoverStyle.Font.Italic = false;
                ////MenuBest.DynamicHoverStyle.Font.Size = 15;

                //MenuBest.DynamicMenuItemStyle.ForeColor = System.Drawing.Color.Black;
                //MenuBest.DynamicMenuItemStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
                ////MenuBest.DynamicMenuItemStyle.ItemSpacing = 4.0;
                ////MenuBest.DynamicMenuItemStyle.VerticalPadding = 4.0;

                //MenuBest.DynamicMenuItemStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
                //MenuBest.DynamicSelectedStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
                ////MenuBest.DynamicSelectedStyle.VerticalPadding = 4.0;
                ////MenuBest.DynamicSelectedStyle.Width = 40;
                //MenuBest.DynamicMenuStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");

                //MenuBest.StaticHoverStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
                //MenuBest.StaticHoverStyle.Font.Bold = true;
                //MenuBest.StaticHoverStyle.Font.Italic = true;
                ////MenuBest.StaticHoverStyle.Font.Size = 15;
                ////MenuBest.StaticHoverStyle.Height = 20;

                //MenuBest.StaticMenuItemStyle.BackColor = System.Drawing.Color.White;
                //MenuBest.StaticMenuStyle.BackColor = System.Drawing.Color.White;
                //MenuBest.StaticSelectedStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
                //MenuBest.StaticSelectedStyle.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");


                //MainMenu Menu. 
                MenuItem MainMenu = new MenuItem("Main Menu");
                MainMenu.Selectable = true;
                MenuItem NewComingFeatures = new MenuItem("New Coming Features");
                NewComingFeatures.ToolTip = "Allows you to see the up and coming new features.";
                MenuItem SystemsBillboard = new MenuItem("Systems Billboard");
                SystemsBillboard.ToolTip = "Gives you access to news within Urban Impact.";
                MenuItem AdminFunctions = new MenuItem("Admin Functions");
                //AdminFunctions.ToolTip = "Administrative Access ONLY to perform Admin duties.";
                MainMenu.ChildItems.Add(NewComingFeatures);
                MainMenu.ChildItems.Add(SystemsBillboard);
                MainMenu.ChildItems.Add(AdminFunctions);
                MenuBest.Items.Add(MainMenu);

                //Student Menu.
                MenuItem Students = new MenuItem("Students");
                MenuItem StudentDirectory = new MenuItem("Student Directory");
                StudentDirectory.ToolTip = "Allows you to view/edit STUDENT information";
                MenuItem StudentQueries = new MenuItem("Student Search/Queries");
                StudentQueries.ToolTip = "Allows you to query STUDENT information.";
                //MenuItem StudentMisc = new MenuItem("Student Misc.");
                Students.ChildItems.Add(StudentDirectory);
                Students.ChildItems.Add(StudentQueries);
                //Students.ChildItems.Add(StudentMisc);
                MenuBest.Items.Add(Students);

                //Volunteer Menu.
                MenuItem Volunteers = new MenuItem("Volunteers");
                MenuItem VolunteerDirectory = new MenuItem("Volunteer Directory");
                VolunteerDirectory.ToolTip = "Allows you to view/edit VOLUNTEER information.";
                MenuItem VolunteerQueries = new MenuItem("Volunteer Search/Queries");
                VolunteerQueries.ToolTip = "Allows you to query VOLUNTEER information.";
                MenuItem VolunteerDetails = new MenuItem("Volunteer Background Details");
                VolunteerQueries.ToolTip = "Allows you to view/edi VOLUNTEER Background Detail information.";
                Volunteers.ChildItems.Add(VolunteerDirectory);
                Volunteers.ChildItems.Add(VolunteerQueries);
                Volunteers.ChildItems.Add(VolunteerDetails);
                MenuBest.Items.Add(Volunteers);

                //Departments Menu. 
                MenuItem Departments = new MenuItem("Departments");
                Departments.Text = "Departments";
                Departments.Value = "Departments";
                //Departments.Selectable = true;
                Departments.ToolTip = "Allows you to view/edit volunteer information";
                MenuItem Athletics = new MenuItem("Athletics");
                MenuItem PerformingArts = new MenuItem("Performing Arts");
                MenuItem Education = new MenuItem("Education");
                MenuItem StudentProgramMaintenance = new MenuItem("StudentProgram Maintenance");
                MenuItem VolunteerProgramMaintenance = new MenuItem("VolunteerProgram Maintenance");
                MenuItem StudentAttendanceHistory = new MenuItem("Student Attendance History");
                MenuItem VolunteerAttendanceHistory = new MenuItem("Volunteer Attendance History");
                MenuItem AthleticsProgramSectionMaintenance = new MenuItem("AthleticsProgramSection Maintenance");
                Athletics.ChildItems.Add(StudentProgramMaintenance);
                Athletics.ChildItems.Add(VolunteerProgramMaintenance);
                Athletics.ChildItems.Add(StudentAttendanceHistory);
                Athletics.ChildItems.Add(VolunteerAttendanceHistory);
                Athletics.ChildItems.Add(AthleticsProgramSectionMaintenance);
                Departments.ChildItems.Add(Athletics);
                MenuItem MSHSChoir = new MenuItem("MSHS Choir");
                MenuItem ChildrensChoir = new MenuItem("Childrens Choir");
                MenuItem PerformingArtsAcademy = new MenuItem("PerformingArtsAcademy");
                MenuItem Shakes = new MenuItem("Shakes");
                MenuItem Singers = new MenuItem("Singers");
                MenuItem ProgramAdminMaintenance = new MenuItem("Program Admin Maintenance");
                PerformingArts.ChildItems.Add(MSHSChoir);
                PerformingArts.ChildItems.Add(ChildrensChoir);
                PerformingArts.ChildItems.Add(PerformingArtsAcademy);
                PerformingArts.ChildItems.Add(Shakes);
                PerformingArts.ChildItems.Add(Singers);
                PerformingArts.ChildItems.Add(ProgramAdminMaintenance);
                Departments.ChildItems.Add(PerformingArts);
                MenuItem AcademicReadingSupport = new MenuItem("Academic Reading Support");
                MenuItem ImpactUrbanSchools = new MenuItem("Impact Urban Schools");
                MenuItem SummerDayCamp = new MenuItem("SummerDay Camp");
                MenuItem SATPrepClass = new MenuItem("SAT Prep Class");
                MenuItem EducationProgramSectionMaintenance = new MenuItem("Education AdminMaintenance");
                Education.ChildItems.Add(AcademicReadingSupport);
                Education.ChildItems.Add(ImpactUrbanSchools);
                Education.ChildItems.Add(SummerDayCamp);
                Education.ChildItems.Add(SATPrepClass);
                Education.ChildItems.Add(EducationProgramSectionMaintenance);
                Departments.ChildItems.Add(Education);
                MenuBest.Items.Add(Departments);

                //Attendance Menu. 
                MenuItem Attendance = new MenuItem("Attendance");
                //Attendance.Text = "Attendance";
                //Attendance.Value = "Attenance";
                //Attendance.Selectable = true;
                Attendance.ToolTip = "Screens that allow you to handle Attendance Information";
                MenuItem StudentAttendance = new MenuItem("Student Attendance");
                MenuItem VolunteerAttendance = new MenuItem("Volunteer Attendance");
                MenuItem EnterStudentAttendance = new MenuItem("Enter Student Attendance");
                EnterStudentAttendance.ToolTip = "Allows you to enter STUDENT attendance.";
                MenuItem ViewStudentAttendace = new MenuItem("View Student Attendance");
                ViewStudentAttendace.ToolTip = "Allows you view STUDENT attendance.";
                //MenuItem EditStudentAttendance = new MenuItem("Edit Student Attendance");
                //EditStudentAttendance.ToolTip = "Allows you to edit STUDENT attendance.";
                MenuItem EnterVolunteerAttendance = new MenuItem("Enter Volunteer Attendance");
                EnterVolunteerAttendance.ToolTip = "Alows you to enter VOLUNTEER attendance.";
                MenuItem ViewVolunteerAttendance = new MenuItem("View Volunteer Attendance");
                ViewVolunteerAttendance.ToolTip = "Allows you to view VOLUNTEER attendance";
                //MenuItem EditVolunteerAttendance = new MenuItem("Edit Volunteer Attendance");
                //EditVolunteerAttendance.ToolTip = "Allows you to edit VOLUNTEER attendance.";

                Attendance.ChildItems.Add(StudentAttendance);
                StudentAttendance.ChildItems.Add(EnterStudentAttendance);
                StudentAttendance.ChildItems.Add(ViewStudentAttendace);
                //StudentAttendance.ChildItems.Add(EditStudentAttendance);
                Attendance.ChildItems.Add(VolunteerAttendance);
                VolunteerAttendance.ChildItems.Add(EnterVolunteerAttendance);
                VolunteerAttendance.ChildItems.Add(ViewVolunteerAttendance);
                //VolunteerAttendance.ChildItems.Add(EditVolunteerAttendance);
                //Attendance.ChildItems.Add(EnterStudentAttendance);
                //Attendance.ChildItems.Add(EnterVolunteerAttendance);
                //Attendance.ChildItems.Add(ViewRetreiveAttendace);
                //Attendance.ChildItems.Add(EditAttendanceData);
                MenuBest.Items.Add(Attendance);

                //Systems Menu.
                MenuItem Systems = new MenuItem("Systems");
                Systems.Text = "Systems";
                Systems.Value = "Systems";
                //Systems.Selectable = true;
                Systems.ToolTip = "Screens that allow you to access various different systems.";
                MenuItem Discipleshipmentor = new MenuItem("DiscipleshipMentor");
                MenuItem CoreKids = new MenuItem("CoreKids");
                MenuItem Options = new MenuItem("Options");
                MenuItem KRAReports = new MenuItem("KRA Reports");
                //MenuItem ImpactUrbanSchools = new MenuItem("Impact Urban Schools");
                MenuItem RSVPGospelTracking = new MenuItem("RSVP Gospel Tracking");
                Systems.ChildItems.Add(Discipleshipmentor);
                Systems.ChildItems.Add(CoreKids);
                Systems.ChildItems.Add(Options);
                Systems.ChildItems.Add(KRAReports);
                //Systems.ChildItems.Add(ImpactUrbanSchools);
                Systems.ChildItems.Add(RSVPGospelTracking);
                MenuBest.Items.Add(Systems);
                
                //GeneralReporting Menu.
                MenuItem GeneralReporting = new MenuItem("Attendance History");
                //GeneralReporting.Selectable = true;
                GeneralReporting.ToolTip = "Screen that allows you to handle History Reporting or Build Mailing Lists from History";
                //MenuItem MailingLists = new MenuItem("Mailing Lists");
                MenuItem MailingHistoryInformation = new MenuItem("Attendance History Information");
                //MenuItem EditAttendanceData = new MenuItem("Edit Attendance Data");
                //GeneralReporting.ChildItems.Add(MailingLists);
                GeneralReporting.ChildItems.Add(MailingHistoryInformation);
                //GeneralReporting.ChildItems.Add(EditAttendanceData);
                MenuBest.Items.Add(GeneralReporting);

                //LogOut Menu.
                MenuItem LogOut = new MenuItem("LogOut");
                LogOut.Text = "LogOut";
                LogOut.Value = "LogOut";
                LogOut.Selectable = true;
                LogOut.ToolTip = "Log Off the System";
                MenuBest.Items.Add(LogOut);
            }
            catch
            {

            }
            finally
            {

            }
            return MenuBest;
        }
    }
}
