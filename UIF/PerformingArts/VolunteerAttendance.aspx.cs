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
using System.Net.Mail;
using UrbanImpactCommon;

namespace UIF.PerformingArts
{
    public partial class VolunteerAttendance : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public event GridViewEditEventHandler RowEditing;
        public Boolean flag = false;
        public int irowNum = 0;
        public static string Department = "";

        public void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["security"] == "Good")
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
                        ddlProgram.Items.Add("Choose a Program");
                        ddlProgram.Items.Add("Childrens Choir");
                        ddlProgram.Items.Add("MSHS Choir");
                        ddlProgram.Items.Add("PerformingArtsAcademy");
                        ddlProgram.Items.Add("Shakes");
                        ddlProgram.Items.Add("Singers");
                        ddlProgram.Items.Add("Impact Urban Schools");
                        ddlProgram.Text = "Choose a Program";
                    }
                    else if (Department == "Athletics")
                    {
                        ddlProgram.Items.Add("Choose a Program");
                        ddlProgram.Items.Add("Outreach Basketball");
                        ddlProgram.Items.Add("3on3 Basketball");
                        ddlProgram.Items.Add("BasketballTEAMS");
                        ddlProgram.Items.Add("SoccerIntraMurals");
                        ddlProgram.Items.Add("SoccerTEAMS");
                        ddlProgram.Items.Add("Baseball");
                        ddlProgram.Items.Add("Bible Study");
                        ddlProgram.Items.Add("HS Basketball League");
                        ddlProgram.Items.Add("MS Basketball League");
                        ddlProgram.Items.Add("MondayNights");
                        ddlProgram.Items.Add("Special Events");
                        ddlProgram.Items.Add("Impact Urban Schools");
                        ddlProgram.Text = "Choose a Program";
                    }
                    else if (Department == "Education")
                    {
                        ddlProgram.Items.Add("Choose a Program");
                        ddlProgram.Items.Add("Academic Reading Support");
                        ddlProgram.Items.Add("Impact Urban Schools");
                        ddlProgram.Items.Add("SummerDay Camp");
                        ddlProgram.Items.Add("SAT Prep Class");
                        ddlProgram.Text = "Choose a Program";
                    }
                    else
                    {
                        Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                    }

                    //Populate the Meals dropdownlist..RCM./8/13/12.
                    ddlMealsYesNo.Items.Add("Meal(s)?");
                    ddlMealsYesNo.Items.Add("Yes");
                    ddlMealsYesNo.Items.Add("No");
                    ddlMealsYesNo.Text = "Meal(s)?";

                    //Populate the MealCount dropdownlist..RCM..8/13/12.
                    ddlMealCount.Items.Add("0");
                    ddlMealCount.Items.Add("1");
                    ddlMealCount.Items.Add("2");
                    ddlMealCount.Items.Add("3");
                    ddlMealCount.Items.Add("4");
                    ddlMealCount.Items.Add("5");
                    ddlMealCount.Text = "0";
                    
                    cmbCommittAttendance.Enabled = false;
                    cmbCommittAttendance.Visible = false;
                    cmbAddAnotherClass.Enabled = false;
                    cmbAddAnotherClass.Visible = false;
                }
            }
            else
            {
                //Ryan C Manners..1/5/11
                //Do NOT ALLOW ACCESS TO THE PAGE!
                Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
        }

        protected void cmbEnterAttendance_Click(object sender, EventArgs e)
        {
            ////Bring up an editable grid list of the volunteers in the applicable program...

            //SqlDataReader reader = null;

            //try
            //{
            //    con.Open();//Opens the db connection.
            //    string sql_LoadGrid = "";
            //    if (Department == "PerformingArts")
            //    {
            //        if (ddlProgram.Text == "MSHS Choir")
            //        {
            //            sql_LoadGrid = "select si.Lastname as 'volunteerLastName', si.Firstname as 'volunteerFirstName',  '' as 'Attended' "
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.mshschoir = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //            DataSet ds = new DataSet();
            //            da.Fill(ds, "VolunteerInformation");
            //            gvvolunteerList.DataSource = ds.Tables[0];
            //            gvStudentList.DataBind();
            //            con.Close();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            //lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "Childrens Choir")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.childrenschoir = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            //lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "Shakes")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.shakes = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            //lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "Singers")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.singers = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            //lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "PerformingArtsAcademy")
            //        {
            //            ddlProgram.Enabled = false;
            //            ddlProgram.Visible = false;
            //            Label2.Visible = false;

            //            string sql = "Select ClassName "
            //                       + "from PerformingArtsAcademyAvailableClasses "
            //                       + "Where MeetTime = '4:30-6:00 Class' "
            //                       + "and MeetDay = 'Thursday' "
            //                       + "group by ClassName "
            //                       + "order by ClassName ";
            //            SqlCommand cmd = new SqlCommand(sql, con);
            //            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //            SqlDataAdapter custDA = new SqlDataAdapter();
            //            custDA.SelectCommand = cmd;
            //            DataSet custDS = new DataSet();
            //            custDA.Fill(custDS, "PerformingArtsAcademyAvailableClasses");

            //            ddlClassSelection.Items.Add(" ");
            //            //Iterate over setup records and call method to do the work on each one...RCM..
            //            foreach (DataRow myDataRowPO in custDS.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
            //            {
            //                //Adding options to the drop downs for a new entry.
            //                ddlClassSelection.Items.Add(myDataRowPO[0].ToString());
            //            }
            //            custDS.Clear();


            //            string sql2 = "select ClassName "
            //                        + "from PerformingArtsAcademyAvailableClasses "
            //                        + "where MeetTime = '6:30-8:00 Class' "
            //                        + "and MeetDay = 'Thursday' "
            //                        + "group by ClassName "
            //                        + "order by ClassName ";
            //            SqlCommand cmd2 = new SqlCommand(sql2, con);
            //            cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //            SqlDataAdapter custDA2 = new SqlDataAdapter();
            //            custDA2.SelectCommand = cmd2;
            //            DataSet custDS2 = new DataSet();
            //            custDA2.Fill(custDS2, "PerformingArtsAcademyAvailableClasses");

            //            ddlClassSelection2.Items.Add(" ");
            //            //Iterate over setup records and call method to do the work on each one...RCM..
            //            foreach (DataRow myDataRowPO in custDS2.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
            //            {
            //                //Adding options to the drop downs for a new entry.
            //                ddlClassSelection2.Items.Add(myDataRowPO[0].ToString());
            //            }
            //            custDS2.Clear();

            //            //Configuring the controls correctly for viewing...RCM..11/3/10.
            //            ddlClassSelection.Enabled = true;
            //            ddlClassSelection.Visible = true;
            //            ddlClassSelection2.Enabled = true;
            //            ddlClassSelection2.Visible = true;
            //            lblInformation.Visible = false;
            //            lblClass2.Enabled = true;
            //            lblClass2.Visible = true;
            //            lblClass1.Enabled = true;
            //            lblClass1.Visible = true;
            //            lblPAATracking.Enabled = true;
            //            lblPAATracking.Visible = true;
            //        }
            //    }
            //    else if (Department == "Athletics")
            //    {
            //        //if (ddlProgram.Text == "Outreach Basketball")
            //        //{
            //        //    sql_LoadGrid = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
            //        //                 + "FROM VolunteerInformation si "
            //        //                 + "LEFT OUTER JOIN ProgramsList pl "
            //        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //        //                 + "WHERE pl.outreachbasketball = 1 "
            //        //                 + "GROUP BY si.Lastname, si.Firstname "
            //        //                 + "order by si.lastname, si.firstname ";

            //        //    //Perform database lookup based on the chosen child..RCM..
            //        //    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //        //    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //        //    DataSet ds = new DataSet();
            //        //    da.Fill(ds, "VolunteerInformation");
            //        //    gvStudentList.DataSource = ds.Tables[0];
            //        //    gvStudentList.DataBind();
            //        //    con.Close();

            //        //    cmbCommittAttendance.Visible = true;
            //        //    cmbCommittAttendance.Enabled = false;
            //        //    cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //        //    lblInformation.Visible = true;
            //        //    lblInformation.Enabled = true;
            //        //    lblSetAttendance.Visible = true;
            //        //    cmbExcelExport.Visible = true;
            //        //}
            //        if (ddlProgram.Text == "Outreach Basketball")
            //        {
            //            ddlProgram.Enabled = false;
            //            ddlProgram.Visible = false;
            //            Label2.Visible = false;

            //            string sql = "Select SectionName "
            //                       + "from UIF_PerformingArts.dbo.OutreachBasketballProgramSections "
            //                       //+ "Where MeetTime = '4:30-6:00 Class' "
            //                       //+ "and MeetDay = 'Thursday' "
            //                       + "group by SectionName "
            //                       + "order by SectionName ";
            //            SqlCommand cmd = new SqlCommand(sql, con);
            //            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //            SqlDataAdapter custDA = new SqlDataAdapter();
            //            custDA.SelectCommand = cmd;
            //            DataSet custDS = new DataSet();
            //            custDA.Fill(custDS, "UIF_PerformingArts.dbo.OutreachBasketballProgramSections");

            //            ddlOutreachBasketball.Items.Add("Choose a section");
            //            //ddlBasketballTEAMS.Items.Add(" ");
            //            //Iterate over setup records and call method to do the work on each one...RCM..
            //            foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.OutreachBasketballProgramSections"].Rows)
            //            {
            //                //Adding options to the drop downs for a new entry.
            //                ddlOutreachBasketball.Items.Add(myDataRowPO[0].ToString());
            //            }
            //            custDS.Clear();

            //            //Configuring the controls correctly for viewing...RCM..11/3/10.
            //            ddlOutreachBasketball.Enabled = true;
            //            ddlOutreachBasketball.Visible = true;
            //            //ddlClassSelection.Enabled = true;
            //            //ddlClassSelection.Visible = true;
            //            //ddlClassSelection2.Enabled = true;
            //            //ddlClassSelection2.Visible = true;
            //            lblInformation.Visible = false;
            //            //lblClass2.Enabled = true;
            //            //lblClass2.Visible = true;
            //            lblClass1.Enabled = true;
            //            lblClass1.Visible = true;
            //            lblClass1.Text = "Outreach Basketball Sections";
            //            lblPAATracking.Enabled = true;
            //            lblPAATracking.Visible = false;
            //        }
            //        else if (ddlProgram.Text == "3on3 Basketball")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.[3on3Basketball] = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "BasketballTEAMS")
            //        {
            //            ddlProgram.Enabled = false;
            //            ddlProgram.Visible = false;
            //            Label2.Visible = false;

            //            string sql = "Select SectionName "
            //                       + "from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
            //                       //+ "Where MeetTime = '4:30-6:00 Class' "
            //                       //+ "and MeetDay = 'Thursday' "
            //                       + "group by SectionName "
            //                       + "order by SectionName ";
            //            SqlCommand cmd = new SqlCommand(sql, con);
            //            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //            SqlDataAdapter custDA = new SqlDataAdapter();
            //            custDA.SelectCommand = cmd;
            //            DataSet custDS = new DataSet();
            //            custDA.Fill(custDS, "UIF_PerformingArts.dbo.BasketballTEAMSProgramSections");

            //            ddlBasketballTEAMS.Items.Add("Choose a section");
            //            //ddlBasketballTEAMS.Items.Add(" ");
            //            //Iterate over setup records and call method to do the work on each one...RCM..
            //            foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.BasketballTEAMSProgramSections"].Rows)
            //            {
            //                //Adding options to the drop downs for a new entry.
            //                ddlBasketballTEAMS.Items.Add(myDataRowPO[0].ToString());
            //            }
            //            custDS.Clear();


            //            //Configuring the controls correctly for viewing...RCM..11/3/10.
            //            ddlBasketballTEAMS.Enabled = true;
            //            ddlBasketballTEAMS.Visible = true;
            //            //ddlClassSelection.Enabled = true;
            //            //ddlClassSelection.Visible = true;
            //            //ddlClassSelection2.Enabled = true;
            //            //ddlClassSelection2.Visible = true;
            //            lblInformation.Visible = false;
            //            //lblClass2.Enabled = true;
            //            //lblClass2.Visible = true;
            //            lblClass1.Enabled = true;
            //            lblClass1.Visible = true;
            //            lblClass1.Text = "BasketballTEAMS Sections";
            //            lblPAATracking.Enabled = true;
            //            lblPAATracking.Visible = false;
            //        }
            //        else if (ddlProgram.Text == "SoccerIntraMurals")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.soccerintramurals = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "SoccerTEAMS")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.SoccerTEAMS = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "Baseball")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.Baseball = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "Bible Study")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.BibleStudy = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "HS Basketball League")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.hsbasketballlg = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "MS Basketball League")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.msbasketballlg = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        else if (ddlProgram.Text == "MondayNights")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.mondaynights = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            cmd.Connection = con;
            //            gvStudentList.DataSource = cmd.ExecuteReader();
            //            gvStudentList.DataBind();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //    }
            //    else if (Department == "Education")
            //    {
            //        if (ddlProgram.Text == "SummerDay Camp")
            //        {
            //            sql_LoadGrid = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
            //                         + "FROM VolunteerInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.summerdaycamp = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //            DataSet ds = new DataSet();
            //            da.Fill(ds, "VolunteerInformation");
            //            gvStudentList.DataSource = ds.Tables[0];
            //            gvStudentList.DataBind();
            //            con.Close();

            //            cmbCommittAttendance.Visible = true;
            //            cmbCommittAttendance.Enabled = false;
            //            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //            lblInformation.Visible = true;
            //            lblInformation.Enabled = true;
            //            //lblSetAttendance.Visible = true;
            //            cmbExcelExport.Visible = true;
            //        }
            //        //else if (ddlProgram.Text == "SAT Prep Class")
            //        //{
            //        //    sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //        //                 + "FROM VolunteerInformation si "
            //        //                 + "LEFT OUTER JOIN ProgramsList pl "
            //        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //        //                 + "WHERE pl.satprepclass = 1 "
            //        //                 + "GROUP BY si.Lastname, si.Firstname "
            //        //                 + "order by si.lastname, si.firstname ";

            //        //    //Perform database lookup based on the chosen child..RCM..
            //        //    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //        //    cmd.Connection = con;
            //        //    gvStudentList.DataSource = cmd.ExecuteReader();
            //        //    gvStudentList.DataBind();

            //        //    cmbCommittAttendance.Visible = true;
            //        //    cmbCommittAttendance.Enabled = false;
            //        //    cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //        //    lblInformation.Visible = true;
            //        //    lblInformation.Enabled = true;
            //        //    //lblSetAttendance.Visible = true;
            //        //    cmbExcelExport.Visible = true;
            //        //}

            //    }

            //    try
            //    {
            //        string PopulateStudentName_sql = "";

            //        PopulateStudentName_sql = "Select si.lastname, si.firstname "
            //                               + "From VolunteerInformation si "
            //                               + "LEFT OUTER JOIN ProgramsList pl "
            //                               + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
            //                               + "WHERE pl.mshschoir = 1 "
            //                               //+ "Where Program = '" + ddlProgram.Text.Trim() + "' "
            //                               //+ "And Class = '" +  ddlClassSelection.Text.Trim() + "' "
            //                               + "GROUP BY si.lastname, si.firstname "
            //                               + "ORDER BY si.lastname, si.firstname ";

            //        SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con);
            //        cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //        SqlDataAdapter custDA = new SqlDataAdapter();
            //        custDA.SelectCommand = cmd;
            //        DataSet custDS = new DataSet();
            //        custDA.Fill(custDS, "VolunteerInformation");

            //        //Iterate over setup records and call method to do the work on each one...RCM..
            //        //string currentstudent = "";
            //        //currentstudent = ddlIndividualStudentAttendance.Text;
            //        ddlIndividualStudentAttendance.Items.Clear();
            //        ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
            //        foreach (DataRow myDataRowPO in custDS.Tables["VolunteerInformation"].Rows)
            //        {
            //            //Adding options to the drop downs for a new entry.
            //            ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //        }
            //        ddlIndividualStudentAttendance.Text = "Choose a volunteer";
            //        ddlIndividualStudentAttendance.Visible = true;
            //        lblIndividualStudent.Visible = true;
            //        custDS.Clear();
            //    }
            //    catch (Exception lkjl)
            //    {

            //    }

            //    cmbEnterAttendance.Visible = false;
            //    cmbEnterAttendance.Enabled = false;
            //    lblPleaseChoose.Visible = true;
            //    lblPleaseChoose.Enabled = true;
            //    calCalender2.Enabled = true;
            //    calCalender2.Visible = true;
            //    calCalender2.ShowTitle = true;
            //    calCalender2.ShowNextPrevMonth = true;
            //    calCalender2.ShowTitle = true;
            //}
            //catch (Exception lkjl_)
            //{

            //    string lkjl = "";
            //}
        }


        private void BindData()
        {
        }

        protected void gvStudentList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            if (gvStudentList.SelectedRow.Cells[3].Text == "Yes")
            {
                gvStudentList.SelectedRow.Cells[3].Text = "No";
            }
        }

        protected void gvStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (gvStudentList.SelectedRow.Cells[2].Text == "Yes")
            {
                gvStudentList.SelectedRow.Cells[2].Text = "No";
            }
        }

        protected void gvStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow && row.RowIndex == irowNum)
            {
            }
        }

        protected void cmbCommittAttendance_Click(object sender, EventArgs e)
        {
            //Commit the data to the database..RCM..2/5/11.
            cmbCommittAttendance.Enabled = false;
            cmbCommittAttendance.Visible = false;
            lblSetAttendance.Visible = false;

            lblMealsServed.Visible = false;
            lblHowMany.Visible = false;
            ddlMealsYesNo.Visible = false;
            ddlMealCount.Visible = false;
            ddlIndividualStudentAttendance.Visible = false;
            lblIndividualStudent.Visible = false;
            string ProgramSeason = "";

            try
            {
                con.Open();

                int i = 0;

                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    lblErrorMessage.Visible = false;
                    string INSERT_ATTENDANCE_DATA = "";
                    GridViewRow row2 = (GridViewRow)gvStudentList.Rows[i];
                    DropDownList Attended = (DropDownList)row2.FindControl("dropdownlist1");
                    DropDownList Exempt = (DropDownList)row2.FindControl("dropdownlist2");
                    DropDownList Hours = (DropDownList)row2.FindControl("dropdownlist3");

                    if (Department == "PerformingArts")
                    {
                        //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                        if ((calCalender2.SelectedDate.ToString("MM") == "01") || (calCalender2.SelectedDate.ToString("MM") == "02") || (calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05"))
                        {
                            ProgramSeason = "WinterSpring" + calCalender2.SelectedDate.ToString("yyyy");
                        }
                        else if ((calCalender2.SelectedDate.ToString("MM") == "06") || (calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08"))
                        {
                            ProgramSeason = "Summer" + calCalender2.SelectedDate.ToString("yyyy");
                        }
                        else if ((calCalender2.SelectedDate.ToString("MM") == "09") || (calCalender2.SelectedDate.ToString("MM") == "10") || (calCalender2.SelectedDate.ToString("MM") == "11") || (calCalender2.SelectedDate.ToString("MM") == "12"))
                        {
                            ProgramSeason = "Fall" + calCalender2.SelectedDate.ToString("yyyy");
                        }

                        if (ddlProgram.Text == "MSHS Choir")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "MSHS Choir" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "Shakes")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "Shakes" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "Singers")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "Singers" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "PerformingArtsAcademy")
                        {
                            if (ddlClassSelection.Text == "Select a class")
                            {
                                INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                        + "values ("
                                                        + "'" + row2.Cells[0].Text + "',"
                                                        + "'" + row2.Cells[1].Text + "',"
                                                        + "'" + ddlProgram.Text + "',"
                                                        + "'" + ddlClassSelection2.Text + "',"
                                                        + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                        + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                        + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                        + "'" + "PerformingArtsAcademy" + "',"//Comment field.
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                        + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                        + "'" + ProgramSeason + "', "
                                                        + "'" + Hours.Text + "', "
                                                        + "'" + ddlTeamName.Text.Trim() + "') ";
                            }
                            else if (ddlClassSelection2.Text == "Select a class")
                            {
                                INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                        + "values ("
                                                        + "'" + row2.Cells[0].Text + "',"
                                                        + "'" + row2.Cells[1].Text + "',"
                                                        + "'" + ddlProgram.Text + "',"
                                                        + "'" + ddlClassSelection.Text + "',"
                                                        + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                        + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                        + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                        + "'" + "PerformingArtsAcademy" + "',"//Comment field.
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                        + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                        + "'" + ProgramSeason + "', "
                                                        + "'" + Hours.Text + "', "
                                                        + "'" + ddlTeamName.Text.Trim() + "') ";
                            }
                        }
                        else if (ddlProgram.Text == "Childrens Choir")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "Childrens Choir" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                    }
                    else if (Department == "Athletics")
                    {
                        if (ddlProgram.Text == "Outreach Basketball")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "01") || (calCalender2.SelectedDate.ToString("MM") == "02") || (calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05"))
                            {
                                ProgramSeason = "WinterSpring" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "06") || (calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08"))
                            {
                                ProgramSeason = "Summer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "09") || (calCalender2.SelectedDate.ToString("MM") == "10") || (calCalender2.SelectedDate.ToString("MM") == "11") || (calCalender2.SelectedDate.ToString("MM") == "12"))
                            {
                                ProgramSeason = "Fall" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "Outreach Basketball" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "3on3 Basketball")
                        {
                            ProgramSeason = "Spring" + DateTime.Now.ToString("yyyy");

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "3on3 Basketball" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "BasketballTEAMS")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "01") || (calCalender2.SelectedDate.ToString("MM") == "02") || (calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05"))
                            {
                                ProgramSeason = "WinterSpring" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "06") || (calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08"))
                            {
                                ProgramSeason = "Summer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "09") || (calCalender2.SelectedDate.ToString("MM") == "10") || (calCalender2.SelectedDate.ToString("MM") == "11") || (calCalender2.SelectedDate.ToString("MM") == "12"))
                            {
                                ProgramSeason = "Fall" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "BasketballTEAMS" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "SoccerIntraMurals")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "01") || (calCalender2.SelectedDate.ToString("MM") == "02") || (calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05") || (calCalender2.SelectedDate.ToString("MM") == "06"))
                            {
                                ProgramSeason = "SpringSummer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08") || (calCalender2.SelectedDate.ToString("MM") == "09") || (calCalender2.SelectedDate.ToString("MM") == "10") || (calCalender2.SelectedDate.ToString("MM") == "11") || (calCalender2.SelectedDate.ToString("MM") == "12"))
                            {
                                ProgramSeason = "SummerFall" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "SoccerIntraMurals" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "SoccerTEAMS")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "01") || (calCalender2.SelectedDate.ToString("MM") == "02") || (calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05") || (calCalender2.SelectedDate.ToString("MM") == "06"))
                            {
                                ProgramSeason = "SpringSummer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "SoccerTEAMS" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "Baseball")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05") || (calCalender2.SelectedDate.ToString("MM") == "06") || (calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08"))
                            {
                                ProgramSeason = "SpringSummer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "Baseball" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "Bible Study")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "08") || (calCalender2.SelectedDate.ToString("MM") == "09") || (calCalender2.SelectedDate.ToString("MM") == "10") || (calCalender2.SelectedDate.ToString("MM") == "11") || (calCalender2.SelectedDate.ToString("MM") == "12"))
                            {
                                ProgramSeason = "SummerFall" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "Bible Study" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "HS Basketball League")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "08") || (calCalender2.SelectedDate.ToString("MM") == "09") || (calCalender2.SelectedDate.ToString("MM") == "10") || (calCalender2.SelectedDate.ToString("MM") == "11") || (calCalender2.SelectedDate.ToString("MM") == "12"))
                            {
                                ProgramSeason = "SummerFall" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "HS Basketball League" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "MS Basketball League")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "01") || (calCalender2.SelectedDate.ToString("MM") == "02") || (calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05"))
                            {
                                ProgramSeason = "WinterSpring" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "MS Basketball League" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "MondayNights")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "05") || (calCalender2.SelectedDate.ToString("MM") == "06") || (calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08") || (calCalender2.SelectedDate.ToString("MM") == "09"))
                            {
                                ProgramSeason = "Summer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "MondayNights" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                    }
                    else if (Department == "Education")
                    {
                        if (ddlProgram.Text == "SummerDay Camp")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "05") || (calCalender2.SelectedDate.ToString("MM") == "06") || (calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08") || (calCalender2.SelectedDate.ToString("MM") == "09"))
                            {
                                ProgramSeason = "Summer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "SummerDay Camp" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "Academic Reading Support")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "01") || (calCalender2.SelectedDate.ToString("MM") == "02") || (calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05") || (calCalender2.SelectedDate.ToString("MM") == "06"))
                            {
                                ProgramSeason = "Spring" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08"))
                            {
                                ProgramSeason = "Summer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "09") || (calCalender2.SelectedDate.ToString("MM") == "10") || (calCalender2.SelectedDate.ToString("MM") == "11") || (calCalender2.SelectedDate.ToString("MM") == "12"))
                            {
                                ProgramSeason = "Fall" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "Academic Reading Support" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        else if (ddlProgram.Text == "Impact Urban Schools")
                        {
                            //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                            if ((calCalender2.SelectedDate.ToString("MM") == "01") || (calCalender2.SelectedDate.ToString("MM") == "02") || (calCalender2.SelectedDate.ToString("MM") == "03") || (calCalender2.SelectedDate.ToString("MM") == "04") || (calCalender2.SelectedDate.ToString("MM") == "05") || (calCalender2.SelectedDate.ToString("MM") == "06"))
                            {
                                ProgramSeason = "Spring" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "07") || (calCalender2.SelectedDate.ToString("MM") == "08"))
                            {
                                ProgramSeason = "Summer" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else if ((calCalender2.SelectedDate.ToString("MM") == "09") || (calCalender2.SelectedDate.ToString("MM") == "10") || (calCalender2.SelectedDate.ToString("MM") == "11") || (calCalender2.SelectedDate.ToString("MM") == "12"))
                            {
                                ProgramSeason = "Fall" + calCalender2.SelectedDate.ToString("yyyy");
                            }
                            else
                            {
                                ProgramSeason = "OffSeason" + calCalender2.SelectedDate.ToString("yyyy");
                            }

                            INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlOutreachBasketball.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                                                    + "'" + "Impact Urban Schools" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                                                    + "'" + ProgramSeason + "', "
                                                    + "'" + Hours.Text + "', "
                                                    + "'" + ddlTeamName.Text.Trim() + "') ";
                        }
                        //else if (ddlProgram.Text == "SAT Prep Class")
                        //{
                        //    INSERT_ATTENDANCE_DATA = "Insert into VolunteerProgramAttendance "
                        //                            + "values ("
                        //                            + "'" + row2.Cells[0].Text + "',"
                        //                            + "'" + row2.Cells[1].Text + "',"
                        //                            + "'" + ddlProgram.Text + "',"
                        //                            + "'" + ddlOutreachBasketball.Text + "',"
                        //                            + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                        //                            + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                        //                            + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", "
                        //                            + "'" + "Shakes" + "',"//Comment field.
                        //                            + "'" + System.DateTime.Now.ToString() + "',"
                        //                            + "'" + System.DateTime.Now.ToString() + "',"
                        //                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                        //                            + System.Convert.ToInt32(System.Convert.ToBoolean(Exempt.Text)) + ", "
                        //                            + "'" + ProgramSeason + "', "
                        //                            + "'" + Hours.Text + "', "
                        //                            + "'" + ddlTeamName.Text.Trim() + "') ";
                        //}
                    }

                    try
                    {
                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand = new SqlCommand(INSERT_ATTENDANCE_DATA, con);
                        if (sqlInsertCommand.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful                    
                        }
                        else
                        {
                            //Insert didn't work..
                        }
                    }
                    catch (Exception lkjlaabbc)
                    {
                        //Trap error at the item level and continue on with the rest 
                        //of the lines... ONLY prevents the duplicate row, not necessarily the 
                        //entire list unless they are all duplicates...RCM..10/18/12.
                        if (lkjlaabbc.Message.ToString().Contains("duplicate"))
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "";

                            if (gvStudentList.Rows.Count > 1)
                            {
                                lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to enter a DUPLICATE attendance record that already exists for record, but CONTINUING ON... "
                                                                            + "'" + row2.Cells[0].Text + "'," + "'" + row2.Cells[1].Text + "'," + "'" + ddlProgram.Text + "',"
                                                                            + "'" + ddlOutreachBasketball.Text + "'," + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                                            + " Please EXIT or RESET the page.  Thankyou  ";
                            }
                            else
                            {
                                lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to enter a DUPLICATE attendance record that already exists for record: "
                                                                            + "'" + row2.Cells[0].Text + "'," + "'" + row2.Cells[1].Text + "'," + "'" + ddlProgram.Text + "',"
                                                                            + "'" + ddlOutreachBasketball.Text + "'," + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                                            + " Please EXIT or RESET the page.  Thankyou  ";
                            }
                            //DisableEntireScreen();
                        }
                    }
                    i++;
                }
                gvStudentList.Visible = false;
                //gvStudentList.Enabled = false;
                cmbEnterAttendance.Visible = true;
                cmbEnterAttendance.Enabled = true;
                ddlClassSelection.Visible = false;
                ddlClassSelection2.Visible = false;
                ddlBasketballTEAMS.Enabled = false;
                lblClass1.Visible = false;
                lblClass2.Visible = false;
                ddlTeamName.Visible = false;
                lblTeamName.Visible = false;
                lblInformation.Visible = false;
                lblPAATracking.Visible = false;

                //calCalender2.SelectedDates.Clear();

                //Response.Redirect("StudentAttendance.aspx");//Reset button for the page.
                cmbEnterAttendance.Visible = false;
                lblConfirmation.Enabled = true;
                lblConfirmation.Visible = true;
                ddlProgram.Enabled = false;

                if ((ddlProgram.Text == "PerformingArtsAcademy") || (ddlProgram.Text == "BasketballTEAMS") || (ddlProgram.Text == "Outreach Basketball") || (ddlProgram.Text == "Baseball"))
                {
                    cmbAddAnotherClass.Enabled = true;
                    cmbAddAnotherClass.Visible = true;
                    if (ddlProgram.Text == "BasketballTEAMS")
                    {
                        cmbAddAnotherClass.Text = "Add Another Entry";
                    }
                    else if (ddlProgram.Text == "Outreach Basketball")
                    {
                        cmbAddAnotherClass.Text = "Add Another Entry";
                    }
                    else if (ddlProgram.Text == "Baseball")
                    {
                        cmbAddAnotherClass.Text = "Add Another Entry";
                    }
                }
            }
            catch (Exception lkjlllll)
            {
                //Send an email to myself..to notify of a problem..RCM..2/10/11
                //System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
                System.Net.Mail.MailMessage msg = new MailMessage();
                //MailMessage msg = new MailMessage();
                //msg.To = "rmanners@verizon.net; ryan.manners@urbanimpactpittsburgh.org";
                //msg.From = "uifmail@urbanimpactpittsburgh.org";
                msg.Subject = "Error Inserting Student Attenance! ";
                //MailMessage.Priority = System.Web.Mail.MailPriority.High;
                msg.Body = "There was an error inserting StudentAttendance data into the database.   Please address ASAP! ";


                SmtpClient smtp = new SmtpClient();
                //smtp.Credentials = new System.Net.NetworkCredential(“YourSMTPServerUserName”, “YourSMTPServerPassword”); 
                smtp.Send("", "ryan.manners@urbanimpactpittsburgh.org", "subject", "the body of the message");
                //smtp.Send(msg); //send email out
                //MailMessage.Dispose(); //get rid of the object

            }
            finally
            {
                con.Close();
                try
                {
                    InsertEventTrackerAttendance(CalculateNumberOfVolunteersAttended(), ProgramSeason); //This actually loads the EventTracker table...RCM..8/29/12.           
                    //InsertEventTrackerAttendance(gvStudentList.Rows.Count, ProgramSeason); //This actually loads the EventTracker table...RCM..8/29/12.           
                }
                catch (Exception lklaabb)
                {

                }
                calCalender2.SelectedDates.Clear();
                //ddlAudience.Text = "0";//Reset the audience dropdownlist..RCM..
            }
        }

        protected int CalculateNumberOfVolunteersAttended()
        {
            int numberAttended = 0;

            try
            {
                foreach (GridViewRow gvr in gvStudentList.Rows)
                {
                    DropDownList Attended = (DropDownList)gvr.FindControl("dropdownlist1");

                    if (Attended.Text == "True")//Only for students that were present.  
                    {
                        //Number of volunteers should reflect the number that
                        //were actually present... not just on the roster list..RCM..1/23/13.
                        numberAttended = numberAttended + 1;
                    }
                }
            }
            catch (Exception lkjlkjabbbac)
            {

            }
            return numberAttended;
        }

        public static string ConvertBoolToYesNo(bool b)
        {
            if (b) { return "Yes"; }

            return "No";
        }

        public static bool ConvertYesNoToBool(string YesNo)
        {
            if (YesNo == "Yes")
            {
                return true;
            }
            return false;
        }

        public static int ConvertYesNoToInt(string YesNo)
        {
            if (YesNo == "Yes")
            {
                return 1;
            }
            return 0;
        }

        protected void InsertEventTrackerAttendance(int NumberOfVolunteers, string ProgramSeason)
        {
            string INSERT_ATTENDANCE_DATA = "";
            int i = 0;

            try
            {
                int MiscellaneousMeals = 0;
                //if (ddlMiscellaneousMeals.Text != "Misc Meals")
                //{
                //    MiscellaneousMeals = System.Convert.ToInt32(ddlMiscellaneousMeals.Text);
                //}

                int MealsYesNo = 0;
                MealsYesNo = ConvertYesNoToInt(ddlMealsYesNo.Text);

                int GospelGiven = 0;
                //GospelGiven = ConvertYesNoToInt(ddlGospelGiven.Text);

                if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    if (ddlClassSelection.Text == "Select a class")
                    {
                        INSERT_ATTENDANCE_DATA = "Insert into VolunteerEventAttendanceTracker "
                                                + "values ("
                                                + "'" + ddlProgram.Text + "', "
                                                + "'" + ddlClassSelection2.Text + "', "//Contains the section...
                                                + MealsYesNo + ", "//Were meals served?
                                                + (System.Convert.ToInt32(ddlMealCount.Text) * NumberOfVolunteers) + ", "//Calculated # of meals.
                                                + MiscellaneousMeals + ", "//Extra Meals.
                                                + GospelGiven + ", "//Was the Gospel given?
                                                + "0, "//# that Responded to the Gosepel...Not applicable for Volunteer Attendance.
                                                + "0, "//# of bibles handed out....Not applicable for Volunteer Attendance.
                                                + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "', "
                                                + "'" + ddlProgram.Text + " Activity', "//Comment field.
                                                + "'" + System.DateTime.Now.ToString() + "', "
                                                + "'" + System.DateTime.Now.ToString() + "', "
                                                + "'" + Request.QueryString["lastname"] + ", " + Request.QueryString["firstname"] + "', "
                                                + "'" + ProgramSeason + "', "
                                                + "'3', "//Default Hours..
                                                + "'" + ddlTeamName.Text.Trim() + "', "//TeamName....optional, depends on Program.
                                                + "0) ";//AudienceSize (optional)....Not applicable for Volunteer Attendance.
                    }
                    else
                    {
                        INSERT_ATTENDANCE_DATA = "Insert into VolunteerEventAttendanceTracker "
                                                + "values ("
                                                + "'" + ddlProgram.Text + "', "
                                                + "'" + ddlClassSelection.Text + "', "//Contains the section...
                                                + MealsYesNo + ", "//Were meals served?
                                                + (System.Convert.ToInt32(ddlMealCount.Text) * NumberOfVolunteers) + ", "//Calculated # of meals.
                                                + MiscellaneousMeals + ", "//Extra Meals.
                                                + GospelGiven + ", "//Was the Gospel given?
                                                + "0, "//# that Responded to the Gosepel...Not applicable for Volunteer Attendance.
                                                + "0, "//# of bibles handed out....Not applicable for Volunteer Attendance.
                                                + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "', "
                                                + "'" + ddlProgram.Text + " Activity', "//Comment field.
                                                + "'" + System.DateTime.Now.ToString() + "', "
                                                + "'" + System.DateTime.Now.ToString() + "', "
                                                + "'" + Request.QueryString["lastname"] + ", " + Request.QueryString["firstname"] + "', "
                                                + "'" + ProgramSeason + "', "
                                                + "'3', "//Default Hours..
                                                + "'" + ddlTeamName.Text.Trim() + "', "//TeamName....optional, depends on Program.
                                                + "0) ";//AudienceSize (optional)....Not applicable for Volunteer Attendance.
                    }
                }
                else
                {
                    //All other programs use the ddlOutreachBasketball dropdown for Section...RCM..9/6/12.
                    INSERT_ATTENDANCE_DATA = "Insert into VolunteerEventAttendanceTracker "
                                            + "values ("
                                            + "'" + ddlProgram.Text + "', "
                                            + "'" + ddlOutreachBasketball.Text + "', "//Contains the section...
                                            + MealsYesNo + ", "//Were meals served?
                                            + (System.Convert.ToInt32(ddlMealCount.Text) * NumberOfVolunteers) + ", "//Calculated # of meals.
                                            + MiscellaneousMeals + ", "//Extra Meals.
                                            + GospelGiven + ", "//Was the Gospel given?
                                            + "0, "//# that Responded to the Gosepel...Not applicable for Volunteer Attendance.
                                            + "0, "//# of bibles handed out....Not applicable for Volunteer Attendance.
                                            + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "', "
                                            + "'" + ddlProgram.Text + " Activity', "//Comment field.
                                            + "'" + System.DateTime.Now.ToString() + "', "
                                            + "'" + System.DateTime.Now.ToString() + "', "
                                            + "'" + Request.QueryString["lastname"] + ", " + Request.QueryString["firstname"] + "', "
                                            + "'" + ProgramSeason + "', "
                                            + "'3', "//Default Hours..
                                            + "'" + ddlTeamName.Text.Trim() + "', "//TeamName....optional, depends on Program.
                                            + "0) ";//AudienceSize (optional)....Not applicable for Volunteer Attendance.
                }

                con.Open();
                //create a SQL command to update record
                SqlCommand sqlInsertCommand = new SqlCommand(INSERT_ATTENDANCE_DATA, con);
                if (sqlInsertCommand.ExecuteNonQuery() > 0)
                {
                    //maybe display a message confirming update has been successful                    
                }
                else
                {
                    //Insert didn't work..
                }
            }
            catch (Exception lkjlkaaabb)
            {

            }
            finally
            {
                con.Close();
            }
        }

        public void calCalender2_SelectionChanged(object sender, EventArgs e)
        {
            flag = true;
            cmbCommittAttendance.Enabled = true;
            cmbCommittAttendance.Text = "Committ the attendance data.";
            DateTime chosendatetime = calCalender2.SelectedDate;
        }

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendanceOptions.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbEnterAttendance.Visible = true;
            //cmbEnterAttendance.Enabled = true;

            //Bring up an editable grid list of the students in the applicable program...
            ddlIndividualStudentAttendance.Items.Clear();
            ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");

            ddlTeamName.Items.Clear();
            ddlTeamName.Items.Add("Choose team name");
            ddlTeamName.Visible = false;
            lblTeamName.Visible = false;

            int i = 0;

            //foreach (GridViewRow row in gvStudentList.Rows)
            //{


            //    GridViewRow row2 = (GridViewRow)gvStudentList.Rows[i];
            //    DropDownList Attended = (DropDownList)row2.FindControl("dropdownlist1");
            //    DropDownList Exempt = (DropDownList)row2.FindControl("dropdownlist2");
            //}

            SqlDataReader reader = null;

            try
            {
                con.Open();//Opens the db connection.
                string sql_LoadGrid = "";

                ddlBasketballTEAMS.Visible = false;
                ddlOutreachBasketball.Visible = false;
                ddlBaseballSections.Visible = false;

                lblInformation.Visible = false;
                lblInformation.Text = "Volunteer Names in Program";

                ddlMealsYesNo.Text = "Meal(s)?";
                ddlMealCount.Text = "0";
                ddlMealCount.Enabled = false;

                ddlClassSelection.Visible = false;
                ddlClassSelection2.Visible = false;
                lblClass1.Visible = false;
                lblClass2.Visible = false;

                ddlIndividualStudentAttendance.Visible = false;
                lblIndividualStudent.Visible = false;
                lblSetAttendance.Visible = false;

                if (ddlProgram.Text == "Choose a Program")
                {
                    //Do Nothing.. No program has been selected..RCM..
                }
                else
                {
                    if (Department == "PerformingArts")
                    {
                        //if (ddlProgram.Text == "MSHS Choir")
                        //{
                        //    sql_LoadGrid = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                        //                 + "FROM VolunteerInformation si "
                        //                 + "LEFT OUTER JOIN ProgramsList pl "
                        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                        //                 + "WHERE pl.mshschoir = 1 "
                        //                 + "AND pl.staffvolunteer = 1 "
                        //                 + "GROUP BY si.Lastname, si.Firstname "
                        //                 + "order by si.lastname, si.firstname ";

                        //    //Perform database lookup based on the chosen child..RCM..
                        //    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                        //    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                        //    DataSet ds = new DataSet();
                        //    da.Fill(ds, "VolunteerInformation");
                        //    gvStudentList.DataSource = ds.Tables[0];
                        //    gvStudentList.DataBind();
                        //    gvStudentList.Visible = true;
                        //    con.Close();

                        //    cmbCommittAttendance.Visible = true;
                        //    cmbCommittAttendance.Enabled = false;
                        //    cmbCommittAttendance.Text = "Please select a date before committing the data.";
                        //    lblInformation.Visible = true;
                        //    lblInformation.Enabled = true;
                        //    //lblSetAttendance.Visible = true;
                        //    cmbExcelExport.Visible = true;
                        //}
                        //else if (ddlProgram.Text == "Childrens Choir")
                        //{
                        //    sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                        //                 + "FROM VolunteerInformation si "
                        //                 + "LEFT OUTER JOIN ProgramsList pl "
                        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                        //                 + "WHERE pl.childrenschoir = 1 "
                        //                 + "AND pl.staffvolunteer = 1 "
                        //                 + "GROUP BY si.Lastname, si.Firstname "
                        //                 + "order by si.lastname, si.firstname ";

                        //    //Perform database lookup based on the chosen child..RCM..
                        //    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                        //    cmd.Connection = con;
                        //    gvStudentList.DataSource = cmd.ExecuteReader();
                        //    gvStudentList.DataBind();
                        //    gvStudentList.Visible = true;

                        //    cmbCommittAttendance.Visible = true;
                        //    cmbCommittAttendance.Enabled = false;
                        //    cmbCommittAttendance.Text = "Please select a date before committing the data.";
                        //    lblInformation.Visible = true;
                        //    lblInformation.Enabled = true;
                        //    //lblSetAttendance.Visible = true;
                        //    cmbExcelExport.Visible = true;
                        //}
                        //else if (ddlProgram.Text == "Shakes")
                        //{
                        //    sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                        //                 + "FROM VolunteerInformation si "
                        //                 + "LEFT OUTER JOIN ProgramsList pl "
                        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                        //                 + "WHERE pl.shakes = 1 "
                        //                 + "AND pl.staffvolunteer = 1 "
                        //                 + "GROUP BY si.Lastname, si.Firstname "
                        //                 + "order by si.lastname, si.firstname ";

                        //    //Perform database lookup based on the chosen child..RCM..
                        //    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                        //    cmd.Connection = con;
                        //    gvStudentList.DataSource = cmd.ExecuteReader();
                        //    gvStudentList.DataBind();
                        //    gvStudentList.Visible = true;

                        //    cmbCommittAttendance.Visible = true;
                        //    cmbCommittAttendance.Enabled = false;
                        //    cmbCommittAttendance.Text = "Please select a date before committing the data.";
                        //    lblInformation.Visible = true;
                        //    lblInformation.Enabled = true;
                        //    //lblSetAttendance.Visible = true;
                        //    cmbExcelExport.Visible = true;
                        //}
                        //else if (ddlProgram.Text == "Singers")
                        //{
                        //    sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                        //                 + "FROM VolunteerInformation si "
                        //                 + "LEFT OUTER JOIN ProgramsList pl "
                        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                        //                 + "WHERE pl.singers = 1 "
                        //                 + "AND pl.staffvolunteer = 1 "
                        //                 + "GROUP BY si.Lastname, si.Firstname "
                        //                 + "order by si.lastname, si.firstname ";

                        //    //Perform database lookup based on the chosen child..RCM..
                        //    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                        //    cmd.Connection = con;
                        //    gvStudentList.DataSource = cmd.ExecuteReader();
                        //    gvStudentList.DataBind();
                        //    gvStudentList.Visible = true;

                        //    cmbCommittAttendance.Visible = true;
                        //    cmbCommittAttendance.Enabled = false;
                        //    cmbCommittAttendance.Text = "Please select a date before committing the data.";
                        //    lblInformation.Visible = true;
                        //    lblInformation.Enabled = true;
                        //    //lblSetAttendance.Visible = true;
                        //    cmbExcelExport.Visible = true;
                        //}
                        if (ddlProgram.Text == "PerformingArtsAcademy")
                        {
                            ddlIndividualStudentAttendance.Visible = false;
                            lblIndividualStudent.Visible = false;
                            gvStudentList.Visible = false;

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

                            ddlClassSelection.Items.Clear();
                            //ddlClassSelection.Items.Add(" ");
                            ddlClassSelection.Items.Add("Select a class");
                            //Iterate over setup records and call method to do the work on each one...RCM..
                            foreach (DataRow myDataRowPO in custDS.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                            {
                                //Adding options to the drop downs for a new entry.
                                ddlClassSelection.Items.Add(myDataRowPO[0].ToString());
                            }
                            custDS.Clear();


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

                            ddlClassSelection2.Items.Clear();
                            //ddlClassSelection2.Items.Add(" ");
                            ddlClassSelection2.Items.Add("Select a class");
                            //Iterate over setup records and call method to do the work on each one...RCM..
                            foreach (DataRow myDataRowPO in custDS2.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                            {
                                //Adding options to the drop downs for a new entry.
                                ddlClassSelection2.Items.Add(myDataRowPO[0].ToString());
                            }
                            custDS2.Clear();

                            //Configuring the controls correctly for viewing...RCM..11/3/10.
                            ddlClassSelection.Visible = true;
                            ddlClassSelection2.Visible = true;
                            lblInformation.Visible = false;
                            lblClass2.Enabled = true;
                            lblClass2.Visible = true;
                            lblClass1.Enabled = true;
                            lblClass1.Visible = true;
                        }
                        else
                        {
                            try
                            {
                                gvStudentList.Visible = false;
                                string tablename = "";
                                if (ddlProgram.Text == "MSHS Choir")
                                {
                                    tablename = "MSHSChoir";
                                }
                                else if (ddlProgram.Text == "Childrens Choir")
                                {
                                    tablename = "ChildrensChoir";
                                }
                                else if (ddlProgram.Text == "Singers")
                                {
                                    tablename = "Singers";
                                }
                                else if (ddlProgram.Text == "Shakes")
                                {
                                    tablename = "Shakes";
                                }
                                //else if (ddlProgram.Text == "PerformingArtsAcademy")
                                //{
                                //    tablename = "PerformingArtsAcademy";
                                //}
                                else if (ddlProgram.Text == "Impact Urban Schools")
                                {
                                    tablename = "ImpactUrbanSchools";
                                }

                                string sql = "Select SectionName "
                                           + "from UIF_PerformingArts.dbo.[" + tablename + "ProgramSections] "
                                           + "group by SectionName "
                                           + "order by SectionName ";

                                SqlCommand cmd = new SqlCommand(sql, con);
                                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                                SqlDataAdapter custDA = new SqlDataAdapter();
                                custDA.SelectCommand = cmd;
                                DataSet custDS = new DataSet();
                                custDA.Fill(custDS, "UIF_PerformingArts.dbo.[" + tablename + "ProgramSections]");

                                //Using outreachbasketball sections dropdown to handle all...  generically...RCM..6/25/12.
                                ddlOutreachBasketball.Items.Clear();
                                ddlOutreachBasketball.Items.Add("Select a section");
                                //Iterate over setup records and call method to do the work on each one...RCM..
                                foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.[" + tablename + "ProgramSections]"].Rows)
                                {
                                    //Adding options to the drop downs for a new entry.
                                    ddlOutreachBasketball.Items.Add(myDataRowPO[0].ToString());
                                }
                                custDS.Clear();

                                //Configuring the controls correctly for viewing...RCM..11/3/10.
                                ddlOutreachBasketball.Enabled = true;
                                ddlOutreachBasketball.Visible = true;
                                lblInformation.Visible = false;
                                lblClass1.Enabled = true;
                                lblClass1.Visible = true;
                                lblClass1.Text = tablename + " Sections";
                                lblPAATracking.Enabled = true;
                                lblPAATracking.Visible = false;
                            }
                            catch (Exception lkjlkjaaavv)
                            {


                            }
                        }

                        ddlMealsYesNo.Visible = true;
                        ddlMealCount.Visible = true;
                        lblMealsServed.Visible = true;
                        lblHowMany.Visible = true;
                        ddlOutreachBasketball.Enabled = false;
                        gvStudentList.Enabled = false;
                    }
                    else if (Department == "Athletics")
                    {
                        try
                        {
                            gvStudentList.Visible = false;
                            string tablename = "";
                            if (ddlProgram.Text == "BasketballTEAMS")
                            {
                                tablename = "BasketballTEAMS";
                            }
                            else if (ddlProgram.Text == "Outreach Basketball")
                            {
                                tablename = "OutreachBasketball";
                            }
                            else if (ddlProgram.Text == "3on3 Basketball")
                            {
                                tablename = "3on3Basketball";
                            }
                            else if (ddlProgram.Text == "Baseball")
                            {
                                tablename = "Baseball";
                            }
                            else if (ddlProgram.Text == "MS Basketball League")
                            {
                                tablename = "MSBasketballLeague";
                            }
                            else if (ddlProgram.Text == "HS Basketball League")
                            {
                                tablename = "HSBasketballLeague";
                            }
                            else if (ddlProgram.Text == "SoccerTEAMS")
                            {
                                tablename = "SoccerTEAMS";
                            }
                            else if (ddlProgram.Text == "SoccerIntraMurals")
                            {
                                tablename = "SoccerIntraMurals";
                            }
                            else if (ddlProgram.Text == "MondayNights")
                            {
                                tablename = "MondayNights";
                            }
                            else if (ddlProgram.Text == "Bible Study")
                            {
                                tablename = "BibleStudy";
                            }
                            else if (ddlProgram.Text == "Impact Urban Schools")
                            {
                                tablename = "ImpactUrbanSchools";
                            }

                            string sql = "Select SectionName "
                                       + "from UIF_PerformingArts.dbo.[" + tablename + "ProgramSections] "
                                       + "group by SectionName "
                                       + "order by SectionName ";

                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                            SqlDataAdapter custDA = new SqlDataAdapter();
                            custDA.SelectCommand = cmd;
                            DataSet custDS = new DataSet();
                            custDA.Fill(custDS, "UIF_PerformingArts.dbo.[" + tablename + "ProgramSections]");

                            //Using outreachbasketball sections dropdown to handle all...  generically...RCM..6/25/12.
                            ddlOutreachBasketball.Items.Clear();
                            ddlOutreachBasketball.Items.Add("Select a section");
                            //Iterate over setup records and call method to do the work on each one...RCM..
                            foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.[" + tablename + "ProgramSections]"].Rows)
                            {
                                //Adding options to the drop downs for a new entry.
                                ddlOutreachBasketball.Items.Add(myDataRowPO[0].ToString());
                            }
                            custDS.Clear();

                            //Configuring the controls correctly for viewing...RCM..11/3/10.
                            ddlOutreachBasketball.Enabled = true;
                            ddlOutreachBasketball.Visible = true;
                            lblInformation.Visible = false;
                            lblClass1.Enabled = true;
                            lblClass1.Visible = true;
                            lblClass1.Text = tablename + " Sections";
                            lblPAATracking.Enabled = true;
                            lblPAATracking.Visible = false;
                        }
                        catch (Exception lkjlkjaaavv)
                        {


                        }

                        ddlMealsYesNo.Visible = true;
                        ddlMealCount.Visible = true;
                        lblMealsServed.Visible = true;
                        lblHowMany.Visible = true;
                        ddlOutreachBasketball.Enabled = false;
                    }
                    else if (Department == "Education")
                    {
                        try
                        {
                            gvStudentList.Visible = false;
                            string tablename = "";
                            if (ddlProgram.Text == "Impact Urban Schools")
                            {
                                tablename = "ImpactUrbanSchools";
                            }
                            else if (ddlProgram.Text == "SummerDay Camp")
                            {
                                tablename = "SummerDayCamp";
                            }
                            else if (ddlProgram.Text == "Academic Reading Support")
                            {
                                tablename = "AcademicReadingSupport";
                            }
                            //else if (ddlProgram.Text == "SAT Prep Class")
                            //{
                            //    tablename = "SATPrep";
                            //}

                            string sql = "Select SectionName "
                                       + "from UIF_PerformingArts.dbo.[" + tablename + "ProgramSections] "
                                       + "group by SectionName "
                                       + "order by SectionName ";

                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                            SqlDataAdapter custDA = new SqlDataAdapter();
                            custDA.SelectCommand = cmd;
                            DataSet custDS = new DataSet();
                            custDA.Fill(custDS, "UIF_PerformingArts.dbo.[" + tablename + "ProgramSections]");

                            //Using outreachbasketball sections dropdown to handle all...  generically...RCM..6/25/12.
                            ddlOutreachBasketball.Items.Clear();
                            ddlOutreachBasketball.Items.Add("Select a section");
                            //Iterate over setup records and call method to do the work on each one...RCM..
                            foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.[" + tablename + "ProgramSections]"].Rows)
                            {
                                //Adding options to the drop downs for a new entry.
                                ddlOutreachBasketball.Items.Add(myDataRowPO[0].ToString());
                            }
                            custDS.Clear();

                            //Configuring the controls correctly for viewing...RCM..11/3/10.
                            ddlOutreachBasketball.Enabled = true;
                            ddlOutreachBasketball.Visible = true;
                            lblInformation.Visible = false;
                            lblClass1.Enabled = true;
                            lblClass1.Visible = true;
                            lblClass1.Text = tablename + " Sections";
                            lblPAATracking.Enabled = true;
                            lblPAATracking.Visible = false;
                        }
                        catch (Exception lkjlkjaaavv)
                        {


                        }
                                                                     
                        //if (ddlProgram.Text == "SummerDay Camp")
                        //{
                        //    sql_LoadGrid = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                        //                 + "FROM VolunteerInformation si "
                        //                 + "LEFT OUTER JOIN ProgramsList pl "
                        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                        //                 + "WHERE pl.summerdaycamp = 1 "
                        //                 + "AND pl.staffvolunteer = 1 "
                        //                 + "GROUP BY si.Lastname, si.Firstname "
                        //                 + "order by si.lastname, si.firstname ";

                        //    //Perform database lookup based on the chosen child..RCM..
                        //    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                        //    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                        //    DataSet ds = new DataSet();
                        //    da.Fill(ds, "VolunteerInformation");
                        //    gvStudentList.DataSource = ds.Tables[0];
                        //    gvStudentList.DataBind();
                        //    gvStudentList.Visible = true;
                        //    con.Close();

                        //    cmbCommittAttendance.Visible = true;
                        //    cmbCommittAttendance.Enabled = false;
                        //    cmbCommittAttendance.Text = "Please select a date before committing the data.";
                        //    lblInformation.Visible = true;
                        //    lblInformation.Enabled = true;
                        //    //lblSetAttendance.Visible = true;
                        //    cmbExcelExport.Visible = true;
                        //}
                        //else if (ddlProgram.Text == "SAT Prep Class")
                        //{
                        //    sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                        //                 + "FROM VolunteerInformation si "
                        //                 + "LEFT OUTER JOIN ProgramsList pl "
                        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                        //                 + "WHERE pl.childrenschoir = 1 "
                        //                 + "AND pl.staffvolunteer = 1 "
                        //                 + "GROUP BY si.Lastname, si.Firstname "
                        //                 + "order by si.lastname, si.firstname ";

                        //    //Perform database lookup based on the chosen child..RCM..
                        //    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                        //    cmd.Connection = con;
                        //    gvStudentList.DataSource = cmd.ExecuteReader();
                        //    gvStudentList.DataBind();
                        //    gvStudentList.Visible = true;

                        //    cmbCommittAttendance.Visible = true;
                        //    cmbCommittAttendance.Enabled = false;
                        //    cmbCommittAttendance.Text = "Please select a date before committing the data.";
                        //    lblInformation.Visible = true;
                        //    lblInformation.Enabled = true;
                        //    //lblSetAttendance.Visible = true;
                        //    cmbExcelExport.Visible = true;
                        //}

                        ddlMealsYesNo.Visible = true;
                        ddlMealCount.Visible = true;
                        lblMealsServed.Visible = true;
                        lblHowMany.Visible = true;
                        ddlOutreachBasketball.Enabled = false;
                        gvStudentList.Enabled = false;
                    }

                    //try
                    //{
                    //    string PopulateStudentName_sql = "";
                    //    con2.Open();

                    //    ddlIndividualStudentAttendance.Visible = false;
                    //    lblIndividualStudent.Visible = false;

                    //    if (Department == "PerformingArts")
                    //    {
                    //        //if (ddlProgram.Text == "MSHS Choir")
                    //        //{
                    //        //    PopulateStudentName_sql = "Select si.lastname, si.firstname "
                    //        //                           + "From VolunteerInformation si "
                    //        //                           + "LEFT OUTER JOIN ProgramsList pl "
                    //        //                           + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                    //        //                           + "WHERE pl.mshschoir = 1 "
                    //        //                           + "AND pl.staffvolunteer = 1 "
                    //        //                           + "GROUP BY si.lastname, si.firstname "
                    //        //                           + "ORDER BY si.lastname, si.firstname ";
                    //        //}
                    //        //else if (ddlProgram.Text == "Childrens Choir")
                    //        //{
                    //        //    PopulateStudentName_sql = "Select si.lastname, si.firstname "
                    //        //                           + "From VolunteerInformation si "
                    //        //                           + "LEFT OUTER JOIN ProgramsList pl "
                    //        //                           + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                    //        //                           + "WHERE pl.childrenschoir = 1 "
                    //        //                           + "AND pl.staffvolunteer = 1 "
                    //        //                           + "GROUP BY si.lastname, si.firstname "
                    //        //                           + "ORDER BY si.lastname, si.firstname ";
                    //        //}
                    //        //else if (ddlProgram.Text == "Shakes")
                    //        //{
                    //        //    PopulateStudentName_sql = "Select si.lastname, si.firstname "
                    //        //                           + "From VolunteerInformation si "
                    //        //                           + "LEFT OUTER JOIN ProgramsList pl "
                    //        //                           + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                    //        //                           + "WHERE pl.shakes = 1 "
                    //        //                           + "AND pl.staffvolunteer = 1 "
                    //        //                           + "GROUP BY si.lastname, si.firstname "
                    //        //                           + "ORDER BY si.lastname, si.firstname ";
                    //        //}
                    //        //else if (ddlProgram.Text == "Singers")
                    //        //{
                    //        //    PopulateStudentName_sql = "Select si.lastname, si.firstname "
                    //        //                           + "From VolunteerInformation si "
                    //        //                           + "LEFT OUTER JOIN ProgramsList pl "
                    //        //                           + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                    //        //                           + "WHERE pl.singers = 1 "
                    //        //                           + "AND pl.staffvolunteer = 1 "
                    //        //                           + "GROUP BY si.lastname, si.firstname "
                    //        //                           + "ORDER BY si.lastname, si.firstname ";
                    //        //}
                    //        //else if (ddlProgram.Text == "PerformingArtsAcademy")
                    //        //{

                    //        //}
                    //    }
                    //    else if (Department == "Athletics")
                    //    {


                    //    }
                    //    else if (Department == "Education")
                    //    {
                    //        //if (ddlProgram.Text == "SummerDay Camp")
                    //        //{
                    //        //    PopulateStudentName_sql = "Select si.lastname, si.firstname "
                    //        //                           + "From VolunteerInformation si "
                    //        //                           + "LEFT OUTER JOIN ProgramsList pl "
                    //        //                           + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                    //        //                           + "WHERE pl.summerdaycamp = 1 "
                    //        //                           + "AND pl.staffvolunteer = 1 "
                    //        //                           + "GROUP BY si.lastname, si.firstname "
                    //        //                           + "ORDER BY si.lastname, si.firstname ";
                    //        //}
                    //        //else if (ddlProgram.Text == "SAT Prep Class")
                    //        //{
                    //        //    PopulateStudentName_sql = "Select si.lastname, si.firstname "
                    //        //                           + "From VolunteerInformation si "
                    //        //                           + "LEFT OUTER JOIN ProgramsList pl "
                    //        //                           + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                    //        //                           + "WHERE pl.childrenschoir = 1 "
                    //        //                           + "AND pl.staffvolunteer = 1 "
                    //        //                           + "GROUP BY si.lastname, si.firstname "
                    //        //                           + "ORDER BY si.lastname, si.firstname ";
                    //        //}
                    //    }

                    //    SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                    //    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    //    SqlDataAdapter custDA = new SqlDataAdapter();
                    //    custDA.SelectCommand = cmd;
                    //    DataSet custDS = new DataSet();
                    //    custDA.Fill(custDS, "VolunteerInformation");

                    //    //Iterate over setup records and call method to do the work on each one...RCM..
                    //    //string currentstudent = "";
                    //    //currentstudent = ddlIndividualStudentAttendance.Text;
                    //    //     ddlIndividualStudentAttendance.Items.Clear();
                    //    //     ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                    //    foreach (DataRow myDataRowPO in custDS.Tables["VolunteerInformation"].Rows)
                    //    {
                    //        //Adding options to the drop downs for a new entry.
                    //        ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                    //    }
                    //    ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                    //    ddlIndividualStudentAttendance.Visible = true;
                    //    lblIndividualStudent.Visible = true;
                    //    custDS.Clear();
                    //    custDA.Dispose();
                    //}
                    //catch (Exception lkjl)
                    //{

                    //}
                    //finally
                    //{
                    //    con2.Close();
                    //}

                    cmbEnterAttendance.Visible = false;
                    cmbEnterAttendance.Enabled = false;
                    lblPleaseChoose.Visible = true;
                    lblPleaseChoose.Enabled = true;
                    calCalender2.Enabled = true;
                    calCalender2.Visible = true;
                    calCalender2.ShowTitle = true;
                    calCalender2.ShowNextPrevMonth = true;
                    calCalender2.ShowTitle = true;
                }
            }
            catch (Exception lkjl_)
            {
                con.Close();
                string lkjl = "";
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlClassSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlClassSelection2.Text = " ";
            ddlClassSelection2.Text = "Select a class";

            bind();

            ddlIndividualStudentAttendance.Visible = true;
            lblIndividualStudent.Visible = true;

            try
            {
                string PopulateStudentName_sql = "";
                con2.Open();

                if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    PopulateStudentName_sql = "Select ce.studentlastname, ce.studentfirstname  "
                                    + "from PerformingArtsAcademyClassEnrollment ce "
                                    + "where ce.meettime = '4:30-6:00 Class' "
                                    + "and ce.classname = '" + ddlClassSelection.Text + "' "
                                    + "and ce.volunteer = 1 "
                        //+ "AND ce.studentfirstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                        //+ "AND ce.studentlastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                        //+ "and paidforclass = 1 "
                                    + "group by ce.studentlastname, ce.studentfirstname "
                                    + "order by ce.studentlastname, ce.studentfirstname ";
                }

                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "VolunteerInformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                foreach (DataRow myDataRowPO in custDS.Tables["VolunteerInformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                ddlIndividualStudentAttendance.Visible = true;
                lblIndividualStudent.Visible = true;
                custDS.Clear();
                custDA.Dispose();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                con2.Close();
            }

            gvStudentList.Visible = true;
            gvStudentList.Enabled = true;

            calCalender2.Enabled = true;
            calCalender2.Visible = true;
            calCalender2.ShowTitle = true;
            calCalender2.ShowNextPrevMonth = true;
            calCalender2.ShowTitle = true;

            lblInformation.Text = ddlClassSelection.Text + " volunteers, during the 4:30-6:00pm Class";

            cmbCommittAttendance.Enabled = false;
            cmbCommittAttendance.Visible = true;
            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            lblInformation.Visible = true;
            lblInformation.Enabled = true;
            lblSetAttendance.Visible = true;
            cmbExcelExport.Visible = true;
        }

        protected void ddlClassSelection2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlClassSelection.Text = " ";
            ddlClassSelection.Text = "Select a class";

            bind2();

            ddlIndividualStudentAttendance.Visible = true;
            lblIndividualStudent.Visible = true;

            try
            {
                string PopulateStudentName_sql = "";
                con2.Open();

                if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    PopulateStudentName_sql = "Select ce.studentlastname, ce.studentfirstname  "
                                    + "from PerformingArtsAcademyClassEnrollment ce "
                                    + "where ce.meettime = '6:30-8:00 Class' "
                                    + "and ce.classname = '" + ddlClassSelection2.Text + "' "
                                    + "and ce.volunteer = 1 "
                                    //+ "AND ce.studentfirstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                    //+ "AND ce.studentlastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                    //+ "and paidforclass = 1 "
                                    + "group by ce.studentlastname, ce.studentfirstname "
                                    + "order by ce.studentlastname, ce.studentfirstname ";
                }

                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "VolunteerInformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                foreach (DataRow myDataRowPO in custDS.Tables["VolunteerInformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                ddlIndividualStudentAttendance.Visible = true;
                lblIndividualStudent.Visible = true;
                custDS.Clear();
                custDA.Dispose();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                con2.Close();
            }
            gvStudentList.Visible = true;
            gvStudentList.Enabled = true;

            calCalender2.Enabled = true;
            calCalender2.Visible = true;
            calCalender2.ShowTitle = true;
            calCalender2.ShowNextPrevMonth = true;
            calCalender2.ShowTitle = true;

            lblInformation.Text = ddlClassSelection2.Text + " volunteers, during the 6:30-8:00pm Class";

            cmbCommittAttendance.Enabled = false;
            cmbCommittAttendance.Visible = true;
            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            lblInformation.Visible = true;
            lblInformation.Enabled = true;
            lblSetAttendance.Visible = true;
            cmbExcelExport.Visible = true;
        }

        public void bind()
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select studentlastname, studentfirstname, '' as 'Attended'  "
                         + "from PerformingArtsAcademyClassEnrollment "
                         + "where meettime = '4:30-6:00 Class' "
                         + "and classname = '" + ddlClassSelection.Text + "' "
                         + "and volunteer = 1 "
                         + "group by studentlastname, studentfirstname "
                         + "order by studentlastname, studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "PerformingArtsAcademyClassEnrollment");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }

        public void bind2()
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select studentlastname, studentfirstname,  '' as 'Attended' "
                         + "from PerformingArtsAcademyClassEnrollment "
                         + "where meettime = '6:30-8:00 Class' "
                         + "and classname = '" + ddlClassSelection2.Text + "' "
                         + "and volunteer = 1 "
                         + "group by studentlastname, studentfirstname "
                         + "order by studentlastname, studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "PerformingArtsAcademyClassEnrollment");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }

        public void bind3()//BasketballTEAMS.. bind..
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname,  '' as 'Attended' "
                         + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                         + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                         + "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                         + "AND pl.basketballTEAMS = 1 "
                         + "AND pl.staffvolunteer = 1 "
                         + "order by bte.studentlastname, bte.studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UIF_PerformingArts.dbo.BasketballTEAMSEnrollment");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }

        public void bindSoccerTEAMS()//SoccerTEAMS.. bind..
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname,  '' as 'Attended' "
                         + "from UIF_PerformingArts.dbo.SoccerTEAMSEnrollment bte "
                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                         + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                         + "where bte.sectionname = '" + ddlSoccerTEAMS.Text.Trim() + "' "
                         + "AND pl.SoccerTEAMS = 1 "
                         + "AND pl.staffvolunteer = 1 "
                         + "order by bte.studentlastname, bte.studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UIF_PerformingArts.dbo.SoccerTEAMSEnrollment");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }

        public void bindBaseball()//Baseball.. bind..
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname,  '' as 'Attended' "
                         + "from UIF_PerformingArts.dbo.BaseballEnrollment bte "
                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                         + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                         + "where bte.sectionname = '" + ddlBaseballSections.Text.Trim() + "' "
                         + "AND pl.Baseball = 1 "
                         + "AND pl.staffvolunteer = 1 "
                         + "order by bte.studentlastname, bte.studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UIF_PerformingArts.dbo.BaseballEnrollment");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }

        public void bind4()//OutreachBasketball.. bind..
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname,  '' as 'Attended' "
                         + "from UIF_PerformingArts.dbo.OutreachBasketball bte "
                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                         + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                         + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                         + "AND pl.outreachbasketball = 1 "
                         + "AND pl.staffvolunteer = 1 "
                         + "order by bte.studentlastname, bte.studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UIF_PerformingArts.dbo.OutreachBasketball");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }

        public void bindGeneric(string Program)//Generic.. bind..
        {
            con.Open();
            string tablename = "";
            string PL_column_name = "";

            if (Program == "BasketballTEAMS")
            {
                tablename = "BasketballTEAMS";
            }
            else if (Program == "Outreach Basketball")
            {
                tablename = "OutreachBasketball";
            }
            else if (Program == "3on3 Basketball")
            {
                tablename = "3on3Basketball";
            }
            else if (Program == "Baseball")
            {
                tablename = "Baseball";
            }
            else if (Program == "MS Basketball League")
            {
                tablename = "MSBasketballLeague";
            }
            else if (Program == "HS Basketball League")
            {
                tablename = "HSBasketballLeague";
            }
            else if (Program == "SoccerTEAMS")
            {
                tablename = "SoccerTEAMS";
            }
            else if (Program == "SoccerIntraMurals")
            {
                tablename = "SoccerIntraMurals";
            }
            else if (Program == "MondayNights")
            {
                tablename = "MondayNights";
            }
            else if (Program == "Bible Study")
            {
                tablename = "BibleStudy";
            }
            else if (Program == "Impact Urban Schools")
            {
                tablename = "ImpactUrbanSchools";
            }
            else if (Program == "MSHS Choir")
            {
                tablename = "MSHSChoir";
            }
            else if (Program == "Childrens Choir")
            {
                tablename = "ChildrensChoir";
            }
            else if (Program == "Singers")
            {
                tablename = "Singers";
            }
            else if (Program == "Shakes")
            {
                tablename = "Shakes";
            }
            else if (Program == "SummerDay Camp")
            {
                tablename = "SummerDayCamp";
            }
            else if (Program == "SAT Prep Class")
            {
                tablename = "SAT Prep Class";
            }
            else if (Program == "Academic Reading Support")
            {
                tablename = "AcademicReadingSupport";
            }


            if (Program == "BasketballTEAMS")
            {
                PL_column_name = "BasketballTEAMS";
            }
            else if (Program == "Outreach Basketball")
            {
                PL_column_name = "OutreachBasketball";
            }
            else if (Program == "3on3 Basketball")
            {
                PL_column_name = "3on3Basketball";
            }
            else if (Program == "Baseball")
            {
                PL_column_name = "Baseball";
            }
            else if (Program == "MS Basketball League")
            {
                PL_column_name = "MSBasketballLg";
            }
            else if (Program == "HS Basketball League")
            {
                PL_column_name = "HSBasketballLg";
            }
            else if (Program == "SoccerTEAMS")
            {
                PL_column_name = "SoccerTEAMS";
            }
            else if (Program == "SoccerIntraMurals")
            {
                PL_column_name = "SoccerIntraMurals";
            }
            else if (Program == "MondayNights")
            {
                PL_column_name = "MondayNights";
            }
            else if (Program == "Bible Study")
            {
                PL_column_name = "BibleStudy";
            }
            else if (Program == "Impact Urban Schools")
            {
                PL_column_name = "ImpactUrbanSchools";
            }
            else if (Program == "MSHS Choir")
            {
                PL_column_name = "MSHSChoir";
            }
            else if (Program == "Childrens Choir")
            {
                PL_column_name = "ChildrensChoir";
            }
            else if (Program == "Singers")
            {
                PL_column_name = "Singers";
            }
            else if (Program == "Shakes")
            {
                PL_column_name = "Shakes";
            }
            else if (Program == "SummerDay Camp")
            {
                PL_column_name = "SummerDayCamp";
            }
            else if (Program == "SAT Prep Class")
            {
                PL_column_name = "SATPrepClass";
            }
            else if (Program == "Academic Reading Support")
            {
                PL_column_name = "AcademicReadingSupport";
            }

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname,  '' as 'Attended' "
                         + "from UIF_PerformingArts.dbo.[" + tablename + "Enrollment] bte "
                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                         + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                         + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                         + "AND pl.[" + PL_column_name + "] = 1 "
                         + "AND pl.staffvolunteer = 1 "
                         + "group by bte.studentlastname, bte.studentfirstname "
                         + "order by bte.studentlastname, bte.studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UIF_PerformingArts.dbo.[" + tablename + "Enrollment]");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }


        public void bindGeneric2(string Program)//Generic.. bind..
        {
            con.Open();
            string tablename = "";
            string PL_column_name = "";

            if (Program == "BasketballTEAMS")
            {
                tablename = "BasketballTEAMS";
            }
            else if (Program == "Outreach Basketball")
            {
                tablename = "OutreachBasketball";
            }
            else if (Program == "3on3 Basketball")
            {
                tablename = "3on3Basketball";
            }
            else if (Program == "Baseball")
            {
                tablename = "Baseball";
            }
            else if (Program == "MS Basketball League")
            {
                tablename = "MSBasketballLeague";
            }
            else if (Program == "HS Basketball League")
            {
                tablename = "HSBasketballLeague";
            }
            else if (Program == "SoccerTEAMS")
            {
                tablename = "SoccerTEAMS";
            }
            else if (Program == "SoccerIntraMurals")
            {
                tablename = "SoccerIntraMurals";
            }
            else if (Program == "MondayNights")
            {
                tablename = "MondayNights";
            }
            else if (Program == "Bible Study")
            {
                tablename = "BibleStudy";
            }
            else if (Program == "Impact Urban Schools")
            {
                tablename = "ImpactUrbanSchools";
            }
            else if (Program == "MSHS Choir")
            {
                tablename = "MSHSChoir";
            }
            else if (Program == "Childrens Choir")
            {
                tablename = "ChildrensChoir";
            }
            else if (Program == "Singers")
            {
                tablename = "Singers";
            }
            else if (Program == "Shakes")
            {
                tablename = "Shakes";
            }
            else if (Program == "SummerDay Camp")
            {
                tablename = "SummerDayCamp";
            }
            else if (Program == "SAT Prep Class")
            {
                tablename = "SAT Prep Class";
            }

            if (Program == "BasketballTEAMS")
            {
                PL_column_name = "BasketballTEAMS";
            }
            else if (Program == "Outreach Basketball")
            {
                PL_column_name = "OutreachBasketball";
            }
            else if (Program == "3on3 Basketball")
            {
                PL_column_name = "3on3Basketball";
            }
            else if (Program == "Baseball")
            {
                PL_column_name = "Baseball";
            }
            else if (Program == "MS Basketball League")
            {
                PL_column_name = "MSBasketballLg";
            }
            else if (Program == "HS Basketball League")
            {
                PL_column_name = "HSBasketballLg";
            }
            else if (Program == "SoccerTEAMS")
            {
                PL_column_name = "SoccerTEAMS";
            }
            else if (Program == "SoccerIntraMurals")
            {
                PL_column_name = "SoccerIntraMurals";
            }
            else if (Program == "MondayNights")
            {
                PL_column_name = "MondayNights";
            }
            else if (Program == "Bible Study")
            {
                PL_column_name = "BibleStudy";
            }
            else if (Program == "Impact Urban Schools")
            {
                PL_column_name = "ImpactUrbanSchools";
            }
            else if (Program == "MSHS Choir")
            {
                PL_column_name = "MSHSChoir";
            }
            else if (Program == "Childrens Choir")
            {
                PL_column_name = "ChildrensChoir";
            }
            else if (Program == "Singers")
            {
                PL_column_name = "Singers";
            }
            else if (Program == "Shakes")
            {
                PL_column_name = "Shakes";
            }
            else if (Program == "SummerDay Camp")
            {
                PL_column_name = "SummerDay Camp";
            }
            else if (Program == "SAT Prep Class")
            {
                PL_column_name = "SAT Prep Class";
            }

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname,  '' as 'Attended' "
                         + "from UIF_PerformingArts.dbo.[" + tablename + "Enrollment] bte "
                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                         + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                         + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                         + "AND bte.teamcolor = '" + ddlTeamName.Text.Trim() + "' "
                         + "AND pl.[" + PL_column_name + "] = 1 "
                         + "AND pl.staffvolunteer = 1 "
                         + "group by bte.studentlastname, bte.studentfirstname "
                         + "order by bte.studentlastname, bte.studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UIF_PerformingArts.dbo.[" + tablename + "Enrollment]");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void gvPAAStudentClassList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void calCalender_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void cmbAddAnotherClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (Department == "PerformingArts")
                {
                    cmbAddAnotherClass.Enabled = false;
                    cmbAddAnotherClass.Visible = false;
                    lblPleaseChoose.Visible = false;

                    ddlProgram.Text = "PerformingArtsAcademy";

                    //gvStudentList.Visible = false;
                    //gvStudentList.Enabled = false;
                    cmbEnterAttendance.Visible = true;
                    cmbEnterAttendance.Enabled = true;

                    cmbEnterAttendance.Visible = false;
                    lblConfirmation.Enabled = true;
                    lblConfirmation.Visible = true;
                    ddlProgram.Enabled = true;

                    lblIndividualStudent.Visible = false;
                    ddlIndividualStudentAttendance.Visible = false;

                    cmbCommittAttendance.Enabled = false;
                    cmbCommittAttendance.Visible = false;
                    cmbAddAnotherClass.Enabled = false;
                    cmbAddAnotherClass.Visible = false;

                    //ddlProgram.Visible = false;
                    //ddlProgram.Enabled = false;
                    calCalender2.Visible = false;
                    lblInformation.Visible = false;
                    lblConfirmation.Visible = false;

                    //Configuring the controls correctly for viewing...RCM..11/3/10.
                    ddlClassSelection.Text = "Select a class";
                    ddlClassSelection2.Text = "Select a class";
                    ddlClassSelection.Enabled = true;
                    ddlClassSelection.Visible = true;
                    ddlClassSelection2.Enabled = true;
                    ddlClassSelection2.Visible = true;

                    lblClass2.Enabled = true;
                    lblClass2.Visible = true;
                    lblClass1.Enabled = true;
                    lblClass1.Visible = true;


                    //lblPAATracking.Enabled = true;
                    //lblPAATracking.Visible = true;
                }
                else if (Department == "Athletics")
                {
                    cmbAddAnotherClass.Enabled = false;
                    cmbAddAnotherClass.Visible = false;
                    lblPleaseChoose.Visible = false;

                    //ddlProgram.Text = "PerformingArtsAcademy";

                    //gvStudentList.Visible = false;
                    //gvStudentList.Enabled = false;
                    cmbEnterAttendance.Visible = true;
                    cmbEnterAttendance.Enabled = true;

                    cmbEnterAttendance.Visible = false;
                    lblConfirmation.Enabled = true;
                    lblConfirmation.Visible = true;
                    ddlProgram.Enabled = true;

                    lblIndividualStudent.Visible = false;
                    ddlIndividualStudentAttendance.Visible = false;

                    cmbCommittAttendance.Enabled = false;
                    cmbCommittAttendance.Visible = false;
                    cmbAddAnotherClass.Enabled = false;
                    cmbAddAnotherClass.Visible = false;

                    //ddlProgram.Visible = false;
                    //ddlProgram.Enabled = false;
                    calCalender2.Visible = false;
                    lblInformation.Visible = false;
                    lblConfirmation.Visible = false;

                    ddlBasketballTEAMS.Text = "Select a section";
                    ddlBasketballTEAMS.Enabled = true;

                    //Configuring the controls correctly for viewing...RCM..11/3/10.
                    //ddlClassSelection.Text = "Select a class";
                    //ddlClassSelection2.Text = "Select a class";
                    //ddlClassSelection.Enabled = true;
                    //ddlClassSelection.Visible = true;
                    //ddlClassSelection2.Enabled = true;
                    //ddlClassSelection2.Visible = true;

                    //lblClass2.Enabled = true;
                    //lblClass2.Visible = true;
                    //lblClass1.Enabled = true;
                    //lblClass1.Visible = true;


                    //lblPAATracking.Enabled = true;
                    //lblPAATracking.Visible = true;
                }
            }
            catch (Exception ljkljaa)
            {

            }
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            gvStudentList.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            ExcelExport.ExportGridView(gvStudentList, Response);
        }

        protected void cmbReset_Click1(object sender, EventArgs e)
        {
            Response.Redirect("VolunteerAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            Response.Redirect("menutest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void ddlBasketballTEAMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind3();

            ddlIndividualStudentAttendance.Visible = true;
            lblIndividualStudent.Visible = true;

            try
            {
                string PopulateStudentName_sql = "";
                con2.Open();

                if (Department == "Athletics")
                {
                    if (ddlProgram.Text == "BasketballTEAMS")
                    {
                        PopulateStudentName_sql = "Select bte.studentlastname, bte.studentfirstname "
                                     + "from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                     + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                     + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                                     + "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                     + "AND pl.basketballTEAMS = 1 "
                                     + "AND pl.staffvolunteer = 1 "
                                     + "GROUP BY bte.studentlastname, bte.studentfirstname "
                                     + "order by bte.studentlastname, bte.studentfirstname ";
                    }
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "VolunteerInformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                foreach (DataRow myDataRowPO in custDS.Tables["VolunteerInformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                ddlIndividualStudentAttendance.Visible = true;
                lblIndividualStudent.Visible = true;
                custDS.Clear();
                custDA.Dispose();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                con2.Close();
            }

            gvStudentList.Visible = true;
            gvStudentList.Enabled = true;

            calCalender2.Enabled = true;
            calCalender2.Visible = true;
            calCalender2.ShowTitle = true;
            calCalender2.ShowNextPrevMonth = true;
            calCalender2.ShowTitle = true;

            cmbCommittAttendance.Enabled = false;
            cmbCommittAttendance.Visible = true;
            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            lblInformation.Visible = true;
            lblInformation.Enabled = true;
            lblSetAttendance.Visible = true;
            cmbExcelExport.Visible = true;
        }

        protected void ddlIndividualStudentAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlIndividualStudentAttendance.Items.Remove("Choose a volunteer");
            con.Open();
            string sql_LoadGrid = "";
            string PopulateStudentName_sql = "";

            if (ddlProgram.Text == "PerformingArtsAcademy")
            {
                    if ((ddlClassSelection.Text == "Select a class") && (ddlClassSelection2.Text != "Select a class"))
                    {
                        PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                                //+ "from PerformingArtsAcademyClassEnrollment  "
                                                 + "FROM VolunteerInformation si "
                                                 + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pce "
                                                 + "ON (si.lastname = pce.studentlastname AND si.firstname = pce.studentfirstname) "
                                                 + "WHERE pce.meettime = '6:30-8:00 Class' "
                                                 + "AND pce.classname = '" + ddlClassSelection2.Text + "' "
                                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                                 + "AND pce.volunteer = 1 "
                                                 + "group by si.lastname, si.firstname "
                                                 + "order by si.lastname, si.firstname ";
                    }
            }
            else
            {
                string PL_column_name = "";
                string tablename = "";

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    tablename = "BasketballTEAMS";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    tablename = "OutreachBasketball";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    tablename = "3on3Basketball";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    tablename = "Baseball";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    tablename = "MSBasketballLeague";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    tablename = "HSBasketballLeague";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    tablename = "SoccerTEAMS";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    tablename = "SoccerIntraMurals";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    tablename = "MondayNights";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    tablename = "BibleStudy";
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    tablename = "MSHSChoir";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    tablename = "ChildrensChoir";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    tablename = "Singers";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    tablename = "Shakes";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    tablename = "ImpactUrbanSchools";
                }
                else if (ddlProgram.Text == "SummerDay Camp")
                {
                    tablename = "SummerDayCamp";
                }
                else if (ddlProgram.Text == "SAT Prep Class")
                {
                    tablename = "SATPrepClass";
                }
                else if (ddlProgram.Text == "Academic Reading Support")
                {
                    tablename = "AcademicReadingSupport";
                }

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    PL_column_name = "BasketballTEAMS";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    PL_column_name = "OutreachBasketball";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    PL_column_name = "3on3Basketball";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    PL_column_name = "Baseball";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    PL_column_name = "MSBasketballLeague";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    PL_column_name = "HSBasketballLeague";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    PL_column_name = "SoccerTEAMS";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    PL_column_name = "SoccerIntraMurals";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    PL_column_name = "MondayNights";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    PL_column_name = "BibleStudy";
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    PL_column_name = "MSHSChoir";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    PL_column_name = "ChildrensChoir";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    PL_column_name = "Singers";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    PL_column_name = "Shakes";
                }
                else if (ddlProgram.Text == "SummerDay Camp")
                {
                    PL_column_name = "SummerDayCamp";
                }
                else if (ddlProgram.Text == "SAT Prep Class")
                {
                    PL_column_name = "SATPrepClass";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    PL_column_name = "ImpactUrbanSchools";
                }
                else if (ddlProgram.Text == "Academic Reading Support")
                {
                    PL_column_name = "AcademicReadingSupport";
                }

                PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                             + "FROM VolunteerInformation si "
                             + "LEFT OUTER JOIN ProgramsList pl "
                             + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                             + "LEFT OUTER JOIN UIF_PerformingArts.dbo.[" + tablename + "Enrollment] bte "
                             + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                             + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                             + "and pl.[" + PL_column_name + "] = 1 "
                             + "AND pl.staffvolunteer = 1 "
                             + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                             + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                             + "GROUP BY si.Lastname, si.Firstname "
                             + "order by si.lastname, si.firstname ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql);

                SqlDataAdapter da = new SqlDataAdapter(PopulateStudentName_sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerInformation");
                gvStudentList.DataSource = ds.Tables[0];
                gvStudentList.DataBind();
                con.Close();

                cmbCommittAttendance.Visible = true;
                cmbCommittAttendance.Enabled = false;
                cmbCommittAttendance.Text = "Please select a date before committing the data.";
                //lblInformation.Visible = true;
                //lblInformation.Enabled = true;
                lblSetAttendance.Visible = true;
                cmbExcelExport.Visible = true;
            }
        }

        protected void ddlOutreachBasketball_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tablename = "";
            string PL_column_name = "";

            if (ddlProgram.Text == "PerformingArtsAcademy")
            {

            }
            else
            {
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    tablename = "BasketballTEAMS";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    tablename = "OutreachBasketball";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    tablename = "3on3Basketball";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    tablename = "Baseball";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    tablename = "MSBasketballLeague";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    tablename = "HSBasketballLeague";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    tablename = "SoccerTEAMS";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    tablename = "SoccerIntraMurals";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    tablename = "MondayNights";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    tablename = "BibleStudy";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    tablename = "ImpactUrbanSchools";
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    tablename = "MSHSChoir";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    tablename = "ChildrensChoir";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    tablename = "Singers";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    tablename = "Shakes";
                }
                else if (ddlProgram.Text == "SummerDay Camp")
                {
                    tablename = "SummerDayCamp";
                }
                else if (ddlProgram.Text == "SAT Prep Class")
                {
                    tablename = "SATPrepClass";
                }
                else if (ddlProgram.Text == "Academic Reading Support")
                {
                    tablename = "AcademicReadingSupport";
                }

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    PL_column_name = "BasketballTEAMS";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    PL_column_name = "OutreachBasketball";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    PL_column_name = "3on3Basketball";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    PL_column_name = "Baseball";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    PL_column_name = "MSBasketballLg";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    PL_column_name = "HSBasketballLg";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    PL_column_name = "SoccerTEAMS";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    PL_column_name = "SoccerIntraMurals";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    PL_column_name = "MondayNights";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    PL_column_name = "BibleStudy";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    PL_column_name = "ImpactUrbanSchools";
                }
                else if (ddlProgram.Text == "MSHS Choir")
                {
                    PL_column_name = "MSHSChoir";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    PL_column_name = "ChildrensChoir";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    PL_column_name = "Singers";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    PL_column_name = "Shakes";
                }
                else if (ddlProgram.Text == "SummerDay Camp")
                {
                    PL_column_name = "SummerDayCamp";
                }
                else if (ddlProgram.Text == "SAT Prep Class")
                {
                    PL_column_name = "SAT Prep Class";
                }
                else if (ddlProgram.Text == "Academic Reading Support")
                {
                    PL_column_name = "AcademicReadingSupport";
                }

                bindGeneric(ddlProgram.Text);
                //bind4();

                ddlIndividualStudentAttendance.Visible = true;
                lblIndividualStudent.Visible = true;

                try
                {
                    string PopulateStudentName_sql = "";
                    con2.Open();

                    PopulateStudentName_sql = "Select bte.studentlastname, bte.studentfirstname "
                                    + "from UIF_PerformingArts.dbo.[" + tablename + "Enrollment] bte "
                                    + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                    + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                                    + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                                    + "AND pl.[" + PL_column_name + "] = 1 "
                                    + "AND pl.staffvolunteer = 1 "
                                    + "GROUP BY bte.studentlastname, bte.studentfirstname "
                                    + "order by bte.studentlastname, bte.studentfirstname ";

                    SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "[" + tablename + "Enrollment]");

                    //Iterate over setup records and call method to do the work on each one...RCM..
                    //string currentstudent = "";
                    //currentstudent = ddlIndividualStudentAttendance.Text;
                    ddlIndividualStudentAttendance.Items.Clear();
                    ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                    foreach (DataRow myDataRowPO in custDS.Tables["[" + tablename + "Enrollment]"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                    }
                    ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                    ddlIndividualStudentAttendance.Visible = true;
                    lblIndividualStudent.Visible = true;
                    custDS.Clear();
                    custDA.Dispose();
                }
                catch (Exception lkjl)
                {

                }
                finally
                {
                    //con2.Close();
                }


                try
                {
                    //string PopulateStudentName_sql = "";
                    ////con2.Open();

                    //if (Department == "Athletics")
                    //{
                    //    //if (ddlProgram.Text == "Outreach Basketball")
                    //    //{
                    //    PopulateStudentName_sql = "Select bte.studentlastname, bte.studentfirstname "
                    //                 + "from UIF_PerformingArts.dbo.[" + tablename + "Enrollment] bte "
                    //                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                    //                 + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                    //                 + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                    //                 + "AND pl.[" + PL_column_name + "] = 1 "
                    //                 + "AND pl.staffvolunteer = 1 "
                    //                 + "GROUP BY bte.studentlastname, bte.studentfirstname "
                    //                 + "order by bte.studentlastname, bte.studentfirstname ";
                    //    //}
                    //}
                    //SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                    //cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    //SqlDataAdapter custDA = new SqlDataAdapter();
                    //custDA.SelectCommand = cmd;
                    //DataSet custDS = new DataSet();
                    //custDA.Fill(custDS, "[" + tablename + "Enrollment]");

                    ////Iterate over setup records and call method to do the work on each one...RCM..
                    ////string currentstudent = "";
                    ////currentstudent = ddlIndividualStudentAttendance.Text;
                    //ddlIndividualStudentAttendance.Items.Clear();
                    //ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                    //foreach (DataRow myDataRowPO in custDS.Tables["[" + tablename + "Enrollment]"].Rows)
                    //{
                    //    //Adding options to the drop downs for a new entry.
                    //    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                    //}
                    //ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                    //ddlIndividualStudentAttendance.Visible = true;
                    //lblIndividualStudent.Visible = true;
                    //custDS.Clear();
                    //custDA.Dispose();

                    string sql = "Select TeamName "
                               + "from UIF_PerformingArts.dbo.[" + tablename + "TeamNameSections] "
                               + "where sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                               + "group by TeamName "
                               + "order by TeamName ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "UIF_PerformingArts.dbo.[" + tablename + "TeamNameSections]");

                    ddlTeamName.Items.Clear();
                    ddlTeamName.Items.Add("Select team name");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.[" + tablename + "TeamNameSections]"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamName.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamName.Visible = true;
                    lblTeamName.Visible = true;
                }
                catch (Exception lkjl)
                {

                }
                finally
                {
                    //con2.Close();
                }

                gvStudentList.Visible = true;
                gvStudentList.Enabled = true;

                calCalender2.Enabled = true;
                calCalender2.Visible = true;
                calCalender2.ShowTitle = true;
                calCalender2.ShowNextPrevMonth = true;
                calCalender2.ShowTitle = true;

                cmbCommittAttendance.Enabled = false;
                cmbCommittAttendance.Visible = true;
                cmbCommittAttendance.Text = "Please select a date before committing the data.";
                lblInformation.Visible = true;
                lblInformation.Enabled = true;
                lblSetAttendance.Visible = true;
                cmbExcelExport.Visible = true;
            }
        }

        protected void ddlBaseballSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindBaseball();

            ddlIndividualStudentAttendance.Visible = true;
            lblIndividualStudent.Visible = true;

            try
            {
                string PopulateStudentName_sql = "";
                con2.Open();

                if (Department == "Athletics")
                {
                    if (ddlProgram.Text == "Baseball")
                    {
                        PopulateStudentName_sql = "Select bte.studentlastname, bte.studentfirstname "
                                     + "from UIF_PerformingArts.dbo.BaseballEnrollment bte "
                                     + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                     + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                                     + "where bte.sectionname = '" + ddlBaseballSections.Text.Trim() + "' "
                                     + "AND pl.Baseball = 1 "
                                     + "AND pl.staffvolunteer = 1 "
                                     + "GROUP BY bte.studentlastname, bte.studentfirstname "
                                     + "order by bte.studentlastname, bte.studentfirstname ";
                    }
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "VolunteerInformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                foreach (DataRow myDataRowPO in custDS.Tables["VolunteerInformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                ddlIndividualStudentAttendance.Visible = true;
                lblIndividualStudent.Visible = true;
                custDS.Clear();
                custDA.Dispose();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                con2.Close();
            }

            gvStudentList.Visible = true;
            gvStudentList.Enabled = true;

            calCalender2.Enabled = true;
            calCalender2.Visible = true;
            calCalender2.ShowTitle = true;
            calCalender2.ShowNextPrevMonth = true;
            calCalender2.ShowTitle = true;

            cmbCommittAttendance.Enabled = false;
            cmbCommittAttendance.Visible = true;
            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            lblInformation.Visible = true;
            lblInformation.Enabled = true;
            lblSetAttendance.Visible = true;
            cmbExcelExport.Visible = true;
        }

        protected void ddlSoccerTEAMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSoccerTEAMS();

            ddlIndividualStudentAttendance.Visible = true;
            lblIndividualStudent.Visible = true;

            try
            {
                string PopulateStudentName_sql = "";
                con2.Open();

                if (Department == "Athletics")
                {
                    if (ddlProgram.Text == "SoccerTEAMS")
                    {
                        PopulateStudentName_sql = "Select bte.studentlastname, bte.studentfirstname "
                                     + "from UIF_PerformingArts.dbo.SoccerTEAMSEnrollment bte "
                                     + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                     + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                                     + "where bte.sectionname = '" + ddlSoccerTEAMS.Text.Trim() + "' "
                                     + "AND pl.soccerTEAMS = 1 "
                                     + "AND pl.staffvolunteer = 1 "
                                     + "GROUP BY bte.studentlastname, bte.studentfirstname "
                                     + "order by bte.studentlastname, bte.studentfirstname ";
                    }
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "VolunteerInformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                foreach (DataRow myDataRowPO in custDS.Tables["VolunteerInformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                ddlIndividualStudentAttendance.Visible = true;
                lblIndividualStudent.Visible = true;
                custDS.Clear();
                custDA.Dispose();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                con2.Close();
            }

            gvStudentList.Visible = true;
            gvStudentList.Enabled = true;

            calCalender2.Enabled = true;
            calCalender2.Visible = true;
            calCalender2.ShowTitle = true;
            calCalender2.ShowNextPrevMonth = true;
            calCalender2.ShowTitle = true;

            cmbCommittAttendance.Enabled = false;
            cmbCommittAttendance.Visible = true;
            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            lblInformation.Visible = true;
            lblInformation.Enabled = true;
            lblSetAttendance.Visible = true;
            cmbExcelExport.Visible = true;
        }

        protected void ddlTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tablename = "";
            string PL_column_name = "";

            if (ddlProgram.Text == "BasketballTEAMS")
            {
                tablename = "BasketballTEAMS";
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                tablename = "OutreachBasketball";
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                tablename = "3on3Basketball";
            }
            else if (ddlProgram.Text == "Baseball")
            {
                tablename = "Baseball";
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                tablename = "MSBasketballLeague";
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                tablename = "HSBasketballLeague";
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                tablename = "SoccerTEAMS";
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                tablename = "SoccerIntraMurals";
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                tablename = "MondayNights";
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                tablename = "BibleStudy";
            }

            if (ddlProgram.Text == "BasketballTEAMS")
            {
                PL_column_name = "BasketballTEAMS";
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                PL_column_name = "OutreachBasketball";
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                PL_column_name = "3on3Basketball";
            }
            else if (ddlProgram.Text == "Baseball")
            {
                PL_column_name = "Baseball";
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                PL_column_name = "MSBasketballLg";
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                PL_column_name = "HSBasketballLg";
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                PL_column_name = "SoccerTEAMS";
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                PL_column_name = "SoccerIntraMurals";
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                PL_column_name = "MondayNights";
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                PL_column_name = "BibleStudy";
            }

            bindGeneric2(ddlProgram.Text);

            ddlIndividualStudentAttendance.Visible = true;
            lblIndividualStudent.Visible = true;

            try
            {
                string PopulateStudentName_sql = "";
                con2.Open();

                if (Department == "Athletics")
                {
                    //if (ddlProgram.Text == "Outreach Basketball")
                    //{
                    PopulateStudentName_sql = "Select bte.studentlastname, bte.studentfirstname "
                                 + "from UIF_PerformingArts.dbo.[" + tablename + "Enrollment] bte "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                                 + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                                 + "AND bte.teamcolor = '" + ddlTeamName.Text.Trim() + "' "
                                 + "AND pl.[" + PL_column_name + "] = 1 "
                                 + "AND pl.staffvolunteer = 1 "
                                 + "GROUP BY bte.studentlastname, bte.studentfirstname "
                                 + "order by bte.studentlastname, bte.studentfirstname ";
                    //}
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "[" + tablename + "Enrollment]");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a volunteer");
                foreach (DataRow myDataRowPO in custDS.Tables["[" + tablename + "Enrollment]"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a volunteer";
                ddlIndividualStudentAttendance.Visible = true;
                lblIndividualStudent.Visible = true;
                custDS.Clear();
                custDA.Dispose();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                //con2.Close();
            }

            gvStudentList.Visible = true;
            gvStudentList.Enabled = true;

            calCalender2.Enabled = true;
            calCalender2.Visible = true;
            calCalender2.ShowTitle = true;
            calCalender2.ShowNextPrevMonth = true;
            calCalender2.ShowTitle = true;

            cmbCommittAttendance.Enabled = false;
            cmbCommittAttendance.Visible = true;
            cmbCommittAttendance.Text = "Please select a date before committing the data.";
            lblInformation.Visible = true;
            lblInformation.Enabled = true;
            lblSetAttendance.Visible = true;
            cmbExcelExport.Visible = true;
        }

        protected void ddlMealsYesNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMealsYesNo.Text == "Yes")
            {
                ddlMealCount.Enabled = true;
            }
            else if (ddlMealsYesNo.Text == "No")
            {
                ddlMealCount.Enabled = false;

                if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    ddlClassSelection.Enabled = true;
                    ddlClassSelection2.Enabled = true;
                }
                else if ((ddlProgram.Text == "MSHS Choir") || (ddlProgram.Text == "Childrens Choir") || (ddlProgram.Text == "Singers") || (ddlProgram.Text == "Shakes"))
                {
                    ddlOutreachBasketball.Enabled = true;
                }
                else if ((ddlProgram.Text == "BasketballTEAMS") || (ddlProgram.Text == "Outreach Basketball") || (ddlProgram.Text == "Baseball") || (ddlProgram.Text == "MS Basketball League") || (ddlProgram.Text == "HS Basketball League") || (ddlProgram.Text == "3on3 Basketball") || (ddlProgram.Text == "SoccerIntraMurals") || (ddlProgram.Text == "SoccerTEAMS") || (ddlProgram.Text == "Bible Study") || (ddlProgram.Text == "MondayNights") || (ddlProgram.Text == "Special Events"))
                {
                    ddlOutreachBasketball.Enabled = true;
                }
                else if ((ddlProgram.Text == "Impact Urban Schools") || (ddlProgram.Text == "SummerDay Camp") || (ddlProgram.Text == "SAT Prep Class") || (ddlProgram.Text == "Academic Reading Support"))
                {
                    ddlOutreachBasketball.Enabled = true;
                }
                else
                {
                    gvStudentList.Enabled = true;
                }
            }
        }

        protected void ddlMealCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgram.Text == "PerformingArtsAcademy")
            {
                ddlClassSelection.Enabled = true;
                ddlClassSelection2.Enabled = true;
            }
            else if ((ddlProgram.Text == "BasketballTEAMS") || (ddlProgram.Text == "Outreach Basketball") || (ddlProgram.Text == "Baseball") || (ddlProgram.Text == "MS Basketball League") || (ddlProgram.Text == "HS Basketball League") || (ddlProgram.Text == "3on3 Basketball") || (ddlProgram.Text == "SoccerIntraMurals") || (ddlProgram.Text == "SoccerTEAMS") || (ddlProgram.Text == "Bible Study") || (ddlProgram.Text == "MondayNights") || (ddlProgram.Text == "Special Events"))
            {
                ddlOutreachBasketball.Enabled = true;
            }
            else if ((ddlProgram.Text == "MSHS Choir") || (ddlProgram.Text == "Childrens Choir") || (ddlProgram.Text == "Singers") || (ddlProgram.Text == "Shakes"))
            {
                ddlOutreachBasketball.Enabled = true;
            }
            else if ((ddlProgram.Text == "Impact Urban Schools") || (ddlProgram.Text == "SummerDay Camp") || (ddlProgram.Text == "SAT Prep Class") || (ddlProgram.Text == "Academic Reading Support"))
            {
                ddlOutreachBasketball.Enabled = true;
            }
            else
            {
                gvStudentList.Enabled = true;
            }
        }
    }
}