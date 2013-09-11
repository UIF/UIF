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
    public partial class RSVPGospelTracking : System.Web.UI.Page
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
                        ddlProgram.Items.Add("LittleLeague Baseball");
                        ddlProgram.Items.Add("Oliver Football");
                        ddlProgram.Items.Add("HS Basketball League");
                        ddlProgram.Items.Add("MS Basketball League");
                        ddlProgram.Items.Add("MondayNights");
                        ddlProgram.Text = "Choose a Program";
                    }
                    else if (Department == "Education")
                    {
                        ddlProgram.Items.Add("Choose a Program");
                        ddlProgram.Items.Add("SummerDay Camp");
                        ddlProgram.Text = "Choose a Program";
                    }
                    else
                    {
                        Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
                    }
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
            //Bring up an editable grid list of the students in the applicable program...
            
            //SqlDataReader reader = null;

            //try
            //{
            //    con.Open();//Opens the db connection.
            //    string sql_LoadGrid = "";
            //    if (Department == "PerformingArts")
            //    {
            //        if (ddlProgram.Text == "MSHS Choir")
            //        {
            //            sql_LoadGrid = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
            //                         + "FROM StudentInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.mshschoir = 1 "
            //                         + "GROUP BY si.Lastname, si.Firstname "
            //                         + "order by si.lastname, si.firstname ";

            //            //Perform database lookup based on the chosen child..RCM..
            //            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //            DataSet ds = new DataSet();
            //            da.Fill(ds, "StudentInformation");
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
            //        else if (ddlProgram.Text == "Childrens Choir")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM StudentInformation si "
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
            //                         + "FROM StudentInformation si "
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
            //                         + "FROM StudentInformation si "
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
            //        if (ddlProgram.Text == "Outreach Basketball")
            //        {
            //            ddlProgram.Enabled = false;
            //            ddlProgram.Visible = false;
            //            Label2.Visible = false;

            //            string sql = "Select SectionName "
            //                       + "from UIF_PerformingArts.dbo.OutreachBasketballProgramSections "
            //                        //+ "Where MeetTime = '4:30-6:00 Class' "
            //                        //+ "and MeetDay = 'Thursday' "
            //                       + "group by SectionName "
            //                       + "order by SectionName ";
            //            SqlCommand cmd = new SqlCommand(sql, con);
            //            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //            SqlDataAdapter custDA = new SqlDataAdapter();
            //            custDA.SelectCommand = cmd;
            //            DataSet custDS = new DataSet();
            //            custDA.Fill(custDS, "UIF_PerformingArts.dbo.OutreachBasketballProgramSections");

            //            ddlBasketballTEAMS.Items.Add("Choose a section");
            //            //ddlBasketballTEAMS.Items.Add(" ");
            //            //Iterate over setup records and call method to do the work on each one...RCM..
            //            foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.OutreachBasketballProgramSections"].Rows)
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
            //            lblClass1.Text = "Outreach Basketball Sections";
            //            lblPAATracking.Enabled = true;
            //            lblPAATracking.Visible = false;
            //        }
            //        else if (ddlProgram.Text == "3on3 Basketball")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM StudentInformation si "
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
            //                         + "FROM StudentInformation si "
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
            //                         + "FROM StudentInformation si "
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
            //        else if (ddlProgram.Text == "LittleLeague Baseball")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM StudentInformation si "
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
            //        else if (ddlProgram.Text == "Oliver Football")
            //        {
            //            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
            //                         + "FROM StudentInformation si "
            //                         + "LEFT OUTER JOIN ProgramsList pl "
            //                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
            //                         + "WHERE pl.oliverfootball = 1 "
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
            //                         + "FROM StudentInformation si "
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
            //                         + "FROM StudentInformation si "
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
            //                         + "FROM StudentInformation si "
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

            //    try
            //    {
            //        string PopulateStudentName_sql = "";

            //        PopulateStudentName_sql = "Select si.lastname, si.firstname "
            //                               + "From studentinformation si "
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
            //        custDA.Fill(custDS, "studentinformation");

            //        //Iterate over setup records and call method to do the work on each one...RCM..
            //        //string currentstudent = "";
            //        //currentstudent = ddlIndividualStudentAttendance.Text;
            //        ddlIndividualStudentAttendance.Items.Clear();
            //        ddlIndividualStudentAttendance.Items.Add("Choose a student");
            //        foreach (DataRow myDataRowPO in custDS.Tables["studentinformation"].Rows)
            //        {
            //            //Adding options to the drop downs for a new entry.
            //            ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //        }
            //        ddlIndividualStudentAttendance.Text = "Choose a student";
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

            ddlIndividualStudentAttendance.Visible = false;
            lblIndividualStudent.Visible = false;

            try
            {
                con.Open();

                int i = 0;
                                
                foreach (GridViewRow row in gvStudentList.Rows)
                {
                    string ProgramSeason = "";
                    string INSERT_ATTENDANCE_DATA = "";
                    GridViewRow row2 = (GridViewRow)gvStudentList.Rows[i];
                    DropDownList RSVPGospel = (DropDownList)row2.FindControl("dropdownlist1");
                    DropDownList RSVPBible = (DropDownList)row2.FindControl("dropdownlist2");
                    
                    //Evaluate dates to set the ProgramSeason field correctly in attendance..RCM..11/29/11.
                    if ((DateTime.Now.Month == 1) || (DateTime.Now.Month == 2) || (DateTime.Now.Month == 3) || (DateTime.Now.Month == 4) || (DateTime.Now.Month == 5))
                    {//Winter/Spring Season.
                        ProgramSeason = "WinterSpring" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.Month == 6) || (DateTime.Now.Month == 7) || (DateTime.Now.Month == 8))
                    {//Summer Season.
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.Month == 9) || (DateTime.Now.Month == 10) || (DateTime.Now.Month == 11) || (DateTime.Now.Month == 12))
                    {//Fall Season.
                        ProgramSeason = "Fall" + DateTime.Now.ToString("yyyy");
                    }
                                       
                    if (Department == "PerformingArts")
                    {
                        if (ddlProgram.Text == "MSHS Choir")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "MSHS Choir" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "Shakes")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "Shakes" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "Singers")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "Singers" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "PerformingArtsAcademy")
                        {
                            if (ddlClassSelection.Text == "Select a class")
                            {
                                INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                        + "values ("
                                                        + "'" + row2.Cells[0].Text + "',"
                                                        + "'" + row2.Cells[1].Text + "',"
                                                        + "'" + ddlProgram.Text + "',"
                                                        + "'" + ddlClassSelection2.Text + "',"
                                                        + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                        + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                        + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                        + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                        + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                        + "'" + "PerformingArtsAcademy" + "',"//Comment field.
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                        + "'" + ProgramSeason + "') ";
                            }
                            else if (ddlClassSelection2.Text == "Select a class")
                            {
                                INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                        + "values ("
                                                        + "'" + row2.Cells[0].Text + "',"
                                                        + "'" + row2.Cells[1].Text + "',"
                                                        + "'" + ddlProgram.Text + "',"
                                                        + "'" + ddlClassSelection.Text + "',"
                                                        + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                        + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                        + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                        + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                        + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                        + "'" + "PerformingArtsAcademy" + "',"//Comment field.
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + System.DateTime.Now.ToString() + "',"
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                        + "'" + ProgramSeason + "') ";
                            }
                        }
                        else if (ddlProgram.Text == "Childrens Choir")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "Childrens Choir" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                    }
                    else if (Department == "Athletics")
                    {
                        if (ddlProgram.Text == "Outreach Basketball")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "OutreachBasketball" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "3on3 Basketball")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "3on3 Basketball" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "BasketballTEAMS")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + ddlBasketballTEAMS.Text + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "BasketballTEAMS" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "SoccerIntraMurals")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "SoccerIntraMurals" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "SoccerTEAMS")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "SoccerTEAMS" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "LittleLeague Baseball")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "LittleLeague Baseball" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "Oliver Football")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "Oliver Football" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "HS Basketball League")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "HS Basketball League" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "MS Basketball League")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "MS Basketball League" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        else if (ddlProgram.Text == "MondayNights")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "MondayNights" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                    }
                    else if (Department == "Education")
                    {
                        if (ddlProgram.Text == "SummerDay Camp")
                        {
                            INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                                                    + "values ("
                                                    + "'" + row2.Cells[0].Text + "',"
                                                    + "'" + row2.Cells[1].Text + "',"
                                                    + "'" + ddlProgram.Text + "',"
                                                    + "'" + "N/A" + "',"
                                                    + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                                                    + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                                                    + "'" + "SummerDay Camp" + "',"//Comment field.
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + ProgramSeason + "') ";
                        }
                        //else if (ddlProgram.Text == "SAT Prep Class")
                        //{
                        //    INSERT_ATTENDANCE_DATA = "Insert into RSVPGospelTracking "
                        //                            + "values ("
                        //                            + "'" + row2.Cells[0].Text + "',"
                        //                            + "'" + row2.Cells[1].Text + "',"
                        //                            + "'" + ddlProgram.Text + "',"
                        //                            + "'" + "N/A" + "',"
                        //                            + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                        //                            + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPGospel.Text)) + ", "
                        //                            + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                        //                            + System.Convert.ToInt32(System.Convert.ToBoolean(RSVPBible.Text)) + ", "
                        //                            + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                        //                            + "'" + "SAT Prep Class" + "',"//Comment field.
                        //                            + "'" + System.DateTime.Now.ToString() + "',"
                        //                            + "'" + System.DateTime.Now.ToString() + "',"
                        //                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                        //                            + "'" + ProgramSeason + "') ";
                        //}
                    }

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
                lblInformation.Visible = false;
                lblPAATracking.Visible = false;

                calCalender2.SelectedDates.Clear();

                //Response.Redirect("StudentAttendance.aspx");//Reset button for the page.
                cmbEnterAttendance.Visible = false;
                lblConfirmation.Enabled = true;
                lblConfirmation.Visible = true;
                ddlProgram.Enabled = false;

                if ((ddlProgram.Text == "PerformingArtsAcademy") || (ddlProgram.Text == "BasketballTEAMS"))
                {
                    cmbAddAnotherClass.Enabled = true;
                    cmbAddAnotherClass.Visible = true;
                    if (ddlProgram.Text == "BasketballTEAMS")
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

            SqlDataReader reader = null;

            try
            {
                con.Open();//Opens the db connection.
                string sql_LoadGrid = "";

                ddlBasketballTEAMS.Visible = false;
                ddlOutreachBasketball.Visible = false;

                lblInformation.Visible = false;
                lblInformation.Text = "Student Names in Program";

                ddlClassSelection.Visible = false;
                ddlClassSelection2.Visible = false;
                lblClass1.Visible = false;
                lblClass2.Visible = false;

                //gvStudentList.Visible = false;
                ddlIndividualStudentAttendance.Visible = false;
                lblIndividualStudent.Visible = false;
                
                if (ddlProgram.Text == "Choose a Program")
                {
                    //Do Nothing.. No program has been selected..RCM..
                }
                else
                {
                    if (Department == "PerformingArts")
                    {
                        if (ddlProgram.Text == "MSHS Choir")
                        {
                            sql_LoadGrid = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.mshschoir = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "StudentInformation");
                            gvStudentList.DataSource = ds.Tables[0];
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;
                            con.Close();

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            //lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "Childrens Choir")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.childrenschoir = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            //lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "Shakes")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.shakes = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            //lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "Singers")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.singers = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            //lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "PerformingArtsAcademy")
                        {
                            ddlIndividualStudentAttendance.Visible = false;
                            lblIndividualStudent.Visible = false;
                            gvStudentList.Visible = false;

                            //ddlProgram.Enabled = false;
                            //ddlProgram.Visible = false;
                            //Label2.Visible = false;

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
                            ddlClassSelection.Enabled = true;
                            ddlClassSelection.Visible = true;
                            ddlClassSelection2.Enabled = true;
                            ddlClassSelection2.Visible = true;
                            lblInformation.Visible = false;
                            lblClass2.Enabled = true;
                            lblClass2.Visible = true;
                            lblClass1.Enabled = true;
                            lblClass1.Visible = true;
                            //lblPAATracking.Enabled = true;
                            //lblPAATracking.Visible = true;
                        }
                    }
                    else if (Department == "Athletics")
                    {
                        if (ddlProgram.Text == "Outreach Basketball")
                        {
                            gvStudentList.Visible = false;

                            string sql = "Select SectionName "
                                       + "from UIF_PerformingArts.dbo.OutreachBasketballProgramSections "
                                        //+ "Where MeetTime = '4:30-6:00 Class' "
                                        //+ "and MeetDay = 'Thursday' "
                                       + "group by SectionName "
                                       + "order by SectionName ";

                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                            SqlDataAdapter custDA = new SqlDataAdapter();
                            custDA.SelectCommand = cmd;
                            DataSet custDS = new DataSet();
                            custDA.Fill(custDS, "UIF_PerformingArts.dbo.OutreachBasketballProgramSections");

                            ddlOutreachBasketball.Items.Clear();
                            ddlOutreachBasketball.Items.Add("Select a section");
                            //Iterate over setup records and call method to do the work on each one...RCM..
                            foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.OutreachBasketballProgramSections"].Rows)
                            {
                                //Adding options to the drop downs for a new entry.
                                ddlOutreachBasketball.Items.Add(myDataRowPO[0].ToString());
                            }
                            custDS.Clear();

                            //Configuring the controls correctly for viewing...RCM..11/3/10.
                            ddlOutreachBasketball.Enabled = true;
                            ddlOutreachBasketball.Visible = true;
                            //ddlClassSelection.Enabled = true;
                            //ddlClassSelection.Visible = true;
                            //ddlClassSelection2.Enabled = true;
                            //ddlClassSelection2.Visible = true;
                            lblInformation.Visible = false;
                            //lblClass2.Enabled = true;
                            //lblClass2.Visible = true;
                            lblClass1.Enabled = true;
                            lblClass1.Visible = true;
                            lblClass1.Text = "Outreach Basketball Sections";
                            lblPAATracking.Enabled = true;
                            lblPAATracking.Visible = false;
                        }
                        else if (ddlProgram.Text == "3on3 Basketball")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.[3on3Basketball] = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "BasketballTEAMS")
                        {
                            gvStudentList.Visible = false;
                            
                            string sql = "Select SectionName "
                                       + "from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
                                       //+ "Where MeetTime = '4:30-6:00 Class' "
                                       //+ "and MeetDay = 'Thursday' "
                                       + "group by SectionName "
                                       + "order by SectionName ";

                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                            SqlDataAdapter custDA = new SqlDataAdapter();
                            custDA.SelectCommand = cmd;
                            DataSet custDS = new DataSet();
                            custDA.Fill(custDS, "UIF_PerformingArts.dbo.BasketballTEAMSProgramSections");

                            ddlBasketballTEAMS.Items.Clear();
                            //ddlBasketballTEAMS.Items.Add(" ");
                            ddlBasketballTEAMS.Items.Add("Select a section");
                            //Iterate over setup records and call method to do the work on each one...RCM..
                            foreach (DataRow myDataRowPO in custDS.Tables["UIF_PerformingArts.dbo.BasketballTEAMSProgramSections"].Rows)
                            {
                                //Adding options to the drop downs for a new entry.
                                ddlBasketballTEAMS.Items.Add(myDataRowPO[0].ToString());
                            }
                            custDS.Clear();

                            //Configuring the controls correctly for viewing...RCM..11/3/10.
                            ddlBasketballTEAMS.Enabled = true;
                            ddlBasketballTEAMS.Visible = true;
                            //ddlClassSelection.Enabled = true;
                            //ddlClassSelection.Visible = true;
                            //ddlClassSelection2.Enabled = true;
                            //ddlClassSelection2.Visible = true;
                            lblInformation.Visible = false;
                            //lblClass2.Enabled = true;
                            //lblClass2.Visible = true;
                            lblClass1.Enabled = true;
                            lblClass1.Visible = true;
                            lblClass1.Text = "BasketballTEAMS Sections";
                            lblPAATracking.Enabled = true;
                            lblPAATracking.Visible = false;
                        }
                        else if (ddlProgram.Text == "SoccerIntraMurals")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.soccerintramurals = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "SoccerTEAMS")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.SoccerTEAMS = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "LittleLeague Baseball")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.Baseball = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "Oliver Football")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.oliverfootball = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "HS Basketball League")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.hsbasketballlg = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "MS Basketball League")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.msbasketballlg = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        else if (ddlProgram.Text == "MondayNights")
                        {
                            sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.mondaynights = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            cmd.Connection = con;
                            gvStudentList.DataSource = cmd.ExecuteReader();
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                    }
                    else if (Department == "Education")
                    {
                        if (ddlProgram.Text == "SummerDay Camp")
                        {
                            sql_LoadGrid = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                         + "FROM StudentInformation si "
                                         + "LEFT OUTER JOIN ProgramsList pl "
                                         + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                         + "WHERE pl.summerdaycamp = 1 "
                                         + "AND pl.student = 1 "
                                         + "GROUP BY si.Lastname, si.Firstname "
                                         + "order by si.lastname, si.firstname ";

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "StudentInformation");
                            gvStudentList.DataSource = ds.Tables[0];
                            gvStudentList.DataBind();
                            gvStudentList.Visible = true;
                            con.Close();

                            cmbCommittAttendance.Visible = true;
                            cmbCommittAttendance.Enabled = false;
                            cmbCommittAttendance.Text = "Please select a date before committing the data.";
                            lblInformation.Visible = true;
                            lblInformation.Enabled = true;
                            //lblSetAttendance.Visible = true;
                            cmbExcelExport.Visible = true;
                        }
                        //else if (ddlProgram.Text == "SAT Prep Class")
                        //{
                        //    sql_LoadGrid = "Select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName', '' as 'Attended'"
                        //                 + "FROM StudentInformation si "
                        //                 + "LEFT OUTER JOIN ProgramsList pl "
                        //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                        //                 + "WHERE pl.SATPrepClass = 1 "
                        //                 + "AND pl.student = 1 "
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
                    }

                    try
                    {
                        string PopulateStudentName_sql = "";
                        con2.Open();

                        ddlIndividualStudentAttendance.Visible = false;
                        lblIndividualStudent.Visible = false;

                        if (Department == "PerformingArts")
                        {
                            if (ddlProgram.Text == "MSHS Choir")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.mshschoir = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "Childrens Choir")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.childrenschoir = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "Shakes")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.shakes = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "Singers")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.singers = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "PerformingArtsAcademy")
                            {

                            }
                        }
                        else if (Department == "Athletics")
                        {
                            if (ddlProgram.Text == "HS Basketball League")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.hsbasketballlg = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "MS Basketball League")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.msbasketballlg = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "LittleLeague Baseball")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.Baseball = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "Outreach Basketball")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.boysoutreachbasketball = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "3on3 Basketball")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.[3on3Basketball] = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "Oliver Football")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.oliverfootball = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "SoccerIntraMurals")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.soccerintramurals = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "SoccerTEAMS")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.SoccerTEAMS = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "MondayNights")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.mondaynights = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            else if (ddlProgram.Text == "BasketballTEAMS")//Left blank on purpose for sections..RCM..
                            {

                            }
                        }
                        else if (Department == "Education")
                        {
                            if (ddlProgram.Text == "SummerDay Camp")
                            {
                                PopulateStudentName_sql = "Select si.lastname, si.firstname "
                                                       + "From studentinformation si "
                                                       + "LEFT OUTER JOIN ProgramsList pl "
                                                       + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                                       + "WHERE pl.summerdaycamp = 1 "
                                                       + "AND pl.student = 1 "
                                                       + "GROUP BY si.lastname, si.firstname "
                                                       + "ORDER BY si.lastname, si.firstname ";
                            }
                            //else if (ddlProgram.Text == "SAT Prep Class")
                            //{
                            //    PopulateStudentName_sql = "Select si.lastname, si.firstname "
                            //                           + "From studentinformation si "
                            //                           + "LEFT OUTER JOIN ProgramsList pl "
                            //                           + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                            //                           + "WHERE pl.childrenschoir = 1 "
                            //                           + "AND pl.student = 1 "
                            //                           + "GROUP BY si.lastname, si.firstname "
                            //                           + "ORDER BY si.lastname, si.firstname ";
                            //}
                        }

                        SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                        cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                        SqlDataAdapter custDA = new SqlDataAdapter();
                        custDA.SelectCommand = cmd;
                        DataSet custDS = new DataSet();
                        custDA.Fill(custDS, "studentinformation");

                        //Iterate over setup records and call method to do the work on each one...RCM..
                        //string currentstudent = "";
                        //currentstudent = ddlIndividualStudentAttendance.Text;
                        ddlIndividualStudentAttendance.Items.Clear();
                        ddlIndividualStudentAttendance.Items.Add("Choose a student");
                        foreach (DataRow myDataRowPO in custDS.Tables["studentinformation"].Rows)
                        {
                            //Adding options to the drop downs for a new entry.
                            ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                        }
                        ddlIndividualStudentAttendance.Text = "Choose a student";
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
                                    + "and ce.student = 1 "
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
                custDA.Fill(custDS, "studentinformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a student");
                foreach (DataRow myDataRowPO in custDS.Tables["studentinformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a student";
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

            lblInformation.Text = ddlClassSelection.Text + " students, during the 4:30-6:00pm Class";

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
                                    + "and ce.student = 1 "
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
                custDA.Fill(custDS, "studentinformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a student");
                foreach (DataRow myDataRowPO in custDS.Tables["studentinformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a student";
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

            lblInformation.Text = ddlClassSelection2.Text + " students, during the 6:30-8:00pm Class";

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
                         + "and student = 1 "
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
                         + "and student = 1 "
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
                         + "AND pl.student = 1 "
                         + "order by bte.studentlastname, bte.studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UIF_PerformingArts.dbo.BasketballTEAMSEnrollment");
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
            ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            ExcelExport.ExportGridView(gvStudentList, Response);
        }

        protected void cmbReset_Click1(object sender, EventArgs e)
        {
            Response.Redirect("RSVPGospelTracking.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
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
                                     + "AND pl.student = 1 "
                                     + "GROUP BY bte.studentlastname, bte.studentfirstname "
                                     + "order by bte.studentlastname, bte.studentfirstname ";
                    }
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "studentinformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a student");
                foreach (DataRow myDataRowPO in custDS.Tables["studentinformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a student";
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

            ddlIndividualStudentAttendance.Items.Remove("Choose a student");
            con.Open();
            string sql_LoadGrid = "";
            string PopulateStudentName_sql = "";

            if (Department == "PerformingArts")
            {
                if (ddlProgram.Text == "MSHS Choir")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN ProgramsList pl "
                                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                 + "WHERE pl.mshschoir = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.Lastname, si.Firstname "
                                 + "order by si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN ProgramsList pl "
                                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                 + "WHERE pl.childrenschoir = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.Lastname, si.Firstname "
                                 + "order by si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "Shakes")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN ProgramsList pl "
                                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                 + "WHERE pl.shakes = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.Lastname, si.Firstname "
                                 + "order by si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "Singers")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN ProgramsList pl "
                                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                 + "WHERE pl.singers = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.Lastname, si.Firstname "
                                 + "order by si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    if ((ddlClassSelection.Text == "Select a class") && (ddlClassSelection2.Text != "Select a class"))
                    {
                        PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                            //+ "from PerformingArtsAcademyClassEnrollment  "
                                     + "FROM StudentInformation si "
                                     + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pce "
                                     + "ON (si.lastname = pce.studentlastname AND si.firstname = pce.studentfirstname) "
                                     + "WHERE pce.meettime = '6:30-8:00 Class' "
                                     + "AND pce.classname = '" + ddlClassSelection2.Text + "' "
                                     + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                     + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                     + "AND pce.student = 1 "
                                     + "group by si.lastname, si.firstname "
                                     + "order by si.lastname, si.firstname ";
                    }
                    else if ((ddlClassSelection2.Text == "Select a class") && (ddlClassSelection.Text != "Select a class"))
                    {
                        PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                            //+ "from PerformingArtsAcademyClassEnrollment  "
                                     + "FROM StudentInformation si "
                                     + "LEFT OUTER JOIN PerformingArtsAcademyClassEnrollment pce "
                                     + "ON (si.lastname = pce.studentlastname AND si.firstname = pce.studentfirstname) "
                                     + "WHERE pce.meettime = '4:30-6:00 Class' "
                                     + "AND pce.classname = '" + ddlClassSelection.Text + "' "
                                     + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                     + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                     + "AND pce.student = 1 "
                                     + "group by si.lastname, si.firstname "
                                     + "order by si.lastname, si.firstname ";
                    }
                }
                else if (ddlProgram.Text == "Choose a student")
                {
                    //This option not possible.  Handled up top by removing an item from the dropdown list..RCM..10/28/11.
                }
            }
            else if (Department == "Athletics")
            {
                if (ddlProgram.Text == "Oliver Football")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN ProgramsList pl "
                                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                 + "WHERE pl.oliverfootball = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.Lastname, si.Firstname "
                                 + "order by si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN ProgramsList pl "
                                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                 + "WHERE pl.mondaynights = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.Lastname, si.Firstname "
                                 + "order by si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "BasketballTEAMS")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                 + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                 + "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                 + "AND pl.basketballTEAMS = 1 "
                                 + "AND pl.student = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.lastname, si.firstname "
                                 + "ORDER BY si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                 + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                 //+ "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                 + "WHERE pl.[3on3Basketball] = 1 "
                                 + "AND pl.student = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.lastname, si.firstname "
                                 + "ORDER BY si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                 + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                 //+ "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                 + "WHERE pl.SoccerIntraMurals = 1 "
                                 + "AND pl.student = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.lastname, si.firstname "
                                 + "ORDER BY si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                 + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                 //+ "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                 + "WHERE pl.SoccerTEAMS = 1 "
                                 + "AND pl.student = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.lastname, si.firstname "
                                 + "ORDER BY si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "LittleLeague Baseball")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                 + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                 //+ "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                 + "WHERE pl.SoccerTEAMS = 1 "
                                 + "AND pl.student = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.lastname, si.firstname "
                                 + "ORDER BY si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                 + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                 //+ "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                 + "WHERE pl.hsbasketballlg = 1 "
                                 + "AND pl.student = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.lastname, si.firstname "
                                 + "ORDER BY si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                 + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                 //+ "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                 + "WHERE pl.msbasketballlg = 1 "
                                 + "AND pl.student = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.lastname, si.firstname "
                                 + "ORDER BY si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.BasketballTEAMSEnrollment bte "
                                 + "ON (si.lastname = bte.studentlastname AND si.firstname = bte.studentfirstname) "
                                 + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                 + "ON (si.lastname = pl.lastname AND si.firstname = pl.firstname) "
                                 //+ "where bte.sectionname = '" + ddlBasketballTEAMS.Text.Trim() + "' "
                                 + "WHERE pl.outreachbasketball = 1 "
                                 + "AND pl.student = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.lastname, si.firstname "
                                 + "ORDER BY si.lastname, si.firstname ";
                }
                else if (ddlProgram.Text == "Choose a student")
                {
                    //This option not possible.  Handled up top by removing an item from the dropdown list..RCM..10/28/11.
                }
            }
            else if (Department == "Education")
            {
                if (ddlProgram.Text == "SummerDay Camp")
                {
                    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM StudentInformation si "
                                 + "LEFT OUTER JOIN ProgramsList pl "
                                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                                 + "WHERE pl.summerdaycamp = 1 "
                                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                                 + "GROUP BY si.Lastname, si.Firstname "
                                 + "order by si.lastname, si.firstname ";
                }
                //else if (ddlProgram.Text == "SAT Prep Class")
                //{
                //    PopulateStudentName_sql = "select si.Lastname as 'StudentLastName', si.Firstname as 'StudentFirstName',  '' as 'Attended' "
                //                 + "FROM StudentInformation si "
                //                 + "LEFT OUTER JOIN ProgramsList pl "
                //                 + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName) "
                //                 + "WHERE pl.SATPrepClass = 1 "
                //                 + "AND si.firstname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1, ddlIndividualStudentAttendance.SelectedValue.Length - (ddlIndividualStudentAttendance.SelectedValue.IndexOf(",") + 1)) + "' "
                //                 + "AND si.lastname = '" + ddlIndividualStudentAttendance.SelectedValue.Substring(0, ddlIndividualStudentAttendance.SelectedValue.IndexOf(",")) + "' "
                //                 + "GROUP BY si.Lastname, si.Firstname "
                //                 + "order by si.lastname, si.firstname ";
                //}
            }

            //Perform database lookup based on the chosen child..RCM..
            SqlCommand cmd = new SqlCommand(PopulateStudentName_sql);

            SqlDataAdapter da = new SqlDataAdapter(PopulateStudentName_sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "StudentInformation");
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


        public void bind4()//OutreachBasketball.. bind..
        {
            con.Open();

            string sql_LoadGrid = "";
            sql_LoadGrid = "Select bte.studentlastname, bte.studentfirstname,  '' as 'Attended' "
                         + "from UIF_PerformingArts.dbo.OutreachBasketball bte "
                         + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                         + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                         + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                         + "AND pl.boysoutreachbasketball = 1 "
                         + "AND pl.student = 1 "
                         + "order by bte.studentlastname, bte.studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UIF_PerformingArts.dbo.OutreachBasketball");
            gvStudentList.DataSource = ds.Tables[0];
            gvStudentList.DataBind();
            con.Close();
        }  

        protected void ddlOutreachBasketball_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind4();

            ddlIndividualStudentAttendance.Visible = true;
            lblIndividualStudent.Visible = true;

            try
            {
                string PopulateStudentName_sql = "";
                con2.Open();

                if (Department == "Athletics")
                {
                    if (ddlProgram.Text == "Outreach Basketball")
                    {
                        PopulateStudentName_sql = "Select bte.studentlastname, bte.studentfirstname "
                                     + "from UIF_PerformingArts.dbo.OutreachBasketball bte "
                                     + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                                     + "ON (pl.Lastname = bte.studentlastname AND pl.Firstname = bte.studentfirstname) "
                                     + "where bte.sectionname = '" + ddlOutreachBasketball.Text.Trim() + "' "
                                     + "AND pl.boysoutreachbasketball = 1 "
                                     + "AND pl.student = 1 "
                                     + "GROUP BY bte.studentlastname, bte.studentfirstname "
                                     + "order by bte.studentlastname, bte.studentfirstname ";
                    }
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con2);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "studentinformation");

                //Iterate over setup records and call method to do the work on each one...RCM..
                //string currentstudent = "";
                //currentstudent = ddlIndividualStudentAttendance.Text;
                ddlIndividualStudentAttendance.Items.Clear();
                ddlIndividualStudentAttendance.Items.Add("Choose a student");
                foreach (DataRow myDataRowPO in custDS.Tables["studentinformation"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlIndividualStudentAttendance.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlIndividualStudentAttendance.Text = "Choose a student";
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
    }
}