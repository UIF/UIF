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
    public partial class AthleticsAttendanceHistory : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public SqlConnection con3 = new SqlConnection(connectionString);
        public static string Department = "";
        SqlDataReader reader = null;
                
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
                    if (Department == "Athletics")
                    {
                        //ddlProgram.Items.Add("Childrens Choir");
                        //ddlProgram.Items.Add("MSHS Choir");
                        //ddlProgram.Items.Add("PerformingArtsAcademy");
                        //ddlProgram.Items.Add("Shakes");
                        //ddlProgram.Items.Add("Singers");

                        ddlProgram.Items.Add("Please select a program");
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
                        ddlProgram.Text = "Please select a program";

                        ddlProgramSeasonHistory.Items.Add("Program Season");
                        ddlProgramSeasonHistory.Text = "Program Season";

                        //ddlTeamSections.Items.Add("Select a team?");
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

        protected void lbAttendanceHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("AthleticsAttendanceHistory.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            //UpdateInformation();
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

            //ddlTeamSectionUpdate.Visible = false;
            //lblTeamColor.Visible = false;
            //lblComments.Visible = false;
            //lblSPNotes.Visible = false;
            //txbSPComments.Visible = false;
            //lbStudentProfileLink.Visible = false;
            ddlTeamSections.Visible = false;
            lblTeamColors.Visible = false;
            gvAttendanceList.Visible = false;
            cmbCallingListReport.Enabled = true;
            gvReport.Visible = false;

            if (ddlProgram.Text != "Please select a program")
            {
                cmbRetreiveProgram.Enabled = true;
                ddlProgramSection.Visible = false;
//                ddlStudents.Visible = false;
                lblProgramSections.Visible = false;
                ddlProgramSection.Items.Clear();
//                ddlStudents.Items.Clear();


                //chbParentalConsentForm.Visible = false;
                //chbRegistrationForm.Visible = false;
                //chbPaid.Visible = false;
                //chbGotPicture.Visible = false;
                //chbContract.Visible = false;
                //txbCoachTeam.Visible = false;
                //lblName.Visible = false;
                //lblName2.Visible = false;
                //lblStudentNames.Visible = false;
                //lblCoachTeam.Visible = false;
                imgStudent.Visible = false;
                //cmbUpdate.Visible = false;
                //txbComments.Visible = false;

                //cmbReport.Enabled = true;

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.BasketballTEAMSProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BasketballTEAMSProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.BaseballProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BaseballProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BaseballProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.MSBasketballLeagueProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MSBasketballLeagueProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MSBasketballLeagueProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.HSBasketballLeagueProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "HSBasketballLeagueProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["HSBasketballLeagueProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.OutReachBasketballProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "OutReachBasketballProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["OutReachBasketballProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.[3on3BasketballProgramSections] "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "[3on3BasketballProgramSections]");

                    ddlProgramSection.Items.Add("Please select a section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["[3on3BasketballProgramSections]"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SoccerIntraMuralsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SoccerIntraMuralsProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SoccerIntraMuralsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SoccerTEAMSProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SoccerTEAMSProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SoccerTEAMSProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.BibleStudyProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BibleStudyProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BibleStudyProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.MondayNightsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MondayNightsProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MondayNightsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SpecialEventsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SpecialEventsProgramSections");

                    ddlProgramSection.Items.Add("Please select a section");
                    //ddlProgramSection.Items.Add(" ");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SpecialEventsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlProgramSection.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlProgramSection.Text = "Please select a section";
                    lblProgramSections.Visible = true;
                    ddlProgramSection.Visible = true;
                    ddlProgramSection.Enabled = true;
                    cmbRetreiveProgram.Enabled = false;
                }
            }

            ddlProgramSeasonHistory.Visible = true;
            //cmbReport.Enabled = true;
            PopulateProgramSeason();
        }

        protected void PopulateProgramSeason()
        {
            string sql = "";
            if (ddlProgram.Text == "Please select a program")
            {
                //Do Nothing...
            }
            else
            {
                if (ddlProgramSection.Text == "Please select a section")
                {
                    sql = "Select spa.ProgramSeason "
                        + "from StudentProgramAttendance spa "
                        + "where spa.Program = '" + ddlProgram.Text + "' "
                        + "GROUP BY spa.ProgramSeason "
                        + "ORDER BY spa.ProgramSeason ";
                }
                else
                {
                    sql = "Select spa.ProgramSeason "
                        + "from StudentProgramAttendance spa "
                        + "where spa.Program = '" + ddlProgram.Text + "' "
                        + "AND spa.Section = '" + ddlProgramSection.Text + "' "
                        + "GROUP BY spa.ProgramSeason "
                        + "ORDER BY spa.ProgramSeason ";
                }
            }

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA = new SqlDataAdapter();
            custDA.SelectCommand = cmd;
            DataSet custDS = new DataSet();

            custDA.Fill(custDS, "StudentProgramAttendance");

            ddlProgramSeasonHistory.Items.Clear();
            ddlProgramSeasonHistory.Items.Add("Program Season");

            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS.Tables["StudentProgramAttendance"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
                ddlProgramSeasonHistory.Items.Add(myDataRowPO[0].ToString());

                //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            }

            //else if (ddlProgram.Text == "Baseball")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.BaseballEnrollment bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}
            //else if (ddlProgram.Text == "SoccerTEAMS")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.SoccerTEAMSEnrollment bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}
            //else if (ddlProgram.Text == "SoccerIntraMurals")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.SoccerIntraMuralsEnrollment bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}
            //else if (ddlProgram.Text == "MS Basketball League")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.MSBasketballLeagueEnrollment bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}
            //else if (ddlProgram.Text == "HS Basketball League")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.HSBasketballLeagueEnrollment bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}
            //else if (ddlProgram.Text == "3on3 Basketball")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.[3on3BasketballEnrollment] bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}
            //else if (ddlProgram.Text == "MondayNights")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.MondayNightsEnrollment bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}
            //else if (ddlProgram.Text == "Bible Study")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.BibleStudyEnrollment bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}
            //else if (ddlProgram.Text == "Special Events")
            //{
            //    //Load the dropdown list for the sections.
            //    sql = "Select bte.Studentlastname, bte.studentfirstname, bte.middlename  "
            //        + "from UIF_PerformingArts.dbo.SpecialEventsEnrollment bte "
            //        + "where bte.student = 1 "
            //        + "and bte.sectionname = '" + ddlProgramSection.Text.Trim() + "' "
            //        + "group by bte.studentlastname, bte.studentfirstname, bte.middlename "
            //        + "order by bte.studentlastname ";
            //}

            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //SqlDataAdapter custDA = new SqlDataAdapter();
            //custDA.SelectCommand = cmd;
            //DataSet custDS = new DataSet();

            //if (ddlProgram.Text == "BasketballTEAMS")
            //{
            //    custDA.Fill(custDS, "BasketballTEAMSEnrollment");
            //}
            //else if (ddlProgram.Text == "Outreach Basketball")
            //{
            //    custDA.Fill(custDS, "OutreachBasketballEnrollment");
            //}
            //else if (ddlProgram.Text == "Baseball")
            //{
            //    custDA.Fill(custDS, "BaseballEnrollment");
            //}
            //else if (ddlProgram.Text == "SoccerTEAMS")
            //{
            //    custDA.Fill(custDS, "SoccerTEAMSEnrollment");
            //}
            //else if (ddlProgram.Text == "SoccerIntraMurals")
            //{
            //    custDA.Fill(custDS, "SoccerIntraMuralsEnrollment");
            //}
            //else if (ddlProgram.Text == "MS Basketball League")
            //{
            //    custDA.Fill(custDS, "MSBasketballLeagueEnrollment");
            //}
            //else if (ddlProgram.Text == "HS Basketball League")
            //{
            //    custDA.Fill(custDS, "HSBasketballLeagueEnrollment");
            //}
            //else if (ddlProgram.Text == "3on3 Basketball")
            //{
            //    custDA.Fill(custDS, "[3on3BasketballEnrollment]");
            //}
            //else if (ddlProgram.Text == "MondayNights")
            //{
            //    custDA.Fill(custDS, "[MondayNightsEnrollment]");
            //}
            //else if (ddlProgram.Text == "Bible Study")
            //{
            //    custDA.Fill(custDS, "[BibleStudyEnrollment]");
            //}
            //else if (ddlProgram.Text == "Special Events")
            //{
            //    custDA.Fill(custDS, "[SpecialEventsEnrollment]");
            //}

            ////Clear the Students dropdown list..RCM..
            //chbParentalConsentForm.Visible = false;
            //chbRegistrationForm.Visible = false;
            //chbPaid.Visible = false;
            //chbGotPicture.Visible = false;
            //chbContract.Visible = false;
            //txbCoachTeam.Visible = false;
            //lblName.Visible = false;
            //lblName2.Visible = false;
            //lblStudentNames.Visible = false;
            //imgStudent.Visible = false;
            //cmbUpdate.Visible = false;
            //txbComments.Visible = false;
            //lblCoachTeam.Visible = false;

            ////Reset dropdown list.
            //ddlStudents.Items.Clear();
            //ddlStudents.Items.Add("Please select a student");
            ////ddlTeamSections.Items.Clear();
            ////ddlTeamSections.Items.Add("Select a team?");
            ////ddlProgramSection.Items.Add(" ");
            ////Iterate over setup records and call method to do the work on each one...RCM..
            //if (ddlProgram.Text == "BasketballTEAMS")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //    }
            //}
            //else if (ddlProgram.Text == "Outreach Basketball")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["OutreachBasketballEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "SoccerTEAMS")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["SoccerTEAMSEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "Baseball")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["BaseballEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "SoccerIntraMurals")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["SoccerIntraMuralsEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "MS Basketball League")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["MSBasketballLeagueEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "HS Basketball League")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["HSBasketballLeagueEnrollment"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "3on3 Basketball")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["[3on3BasketballEnrollment]"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "Bible Study")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["[BibleStudyEnrollment]"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "MondayNights")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["[MondayNightsEnrollment]"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
            //else if (ddlProgram.Text == "Special Events")
            //{
            //    foreach (DataRow myDataRowPO in custDS.Tables["[SpecialEventsEnrollment]"].Rows)
            //    {
            //        //Adding options to the drop downs for a new entry.
            //        //ddlTeamSections.Items.Add(myDataRowPO[0].ToString());
            //        ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString() + " (" + myDataRowPO[2].ToString() + ") ");
            //        //ddlStudents.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
            //    }
            //}
        }

        protected void ddlProgramSection_SelectedIndexChanged(object sender, EventArgs e)
        {


            PopulateProgramSeason();

        }

        protected void ddlTeamSections_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlProgramSeasonHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbReport.Enabled = true;
        }

        protected void cmbRetreiveProgram_Click(object sender, EventArgs e)
        {

        }

        protected string DetermineCurrentProgramSeason(string Program)
        {
            string ProgramSeason = "";

            try
            {
                if (ddlProgram.Text == "Outreach Basketball")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05"))
                    {
                        ProgramSeason = "WinterSpring" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08"))
                    {
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "Fall" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    ProgramSeason = "Spring" + DateTime.Now.ToString("yyyy");
                }
                else if (ddlProgram.Text == "BasketballTEAMS")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05"))
                    {
                        ProgramSeason = "WinterSpring" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08"))
                    {
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "Fall" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06"))
                    {
                        ProgramSeason = "SpringSummer" + DateTime.Now.ToString("yyyy");
                    }
                    else if ((DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "SummerFall" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06"))
                    {
                        ProgramSeason = "SpringSummer" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08"))
                    {
                        ProgramSeason = "SpringSummer" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "SummerFall" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09") || (DateTime.Now.ToString("MM") == "10") || (DateTime.Now.ToString("MM") == "11") || (DateTime.Now.ToString("MM") == "12"))
                    {
                        ProgramSeason = "SummerFall" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "01") || (DateTime.Now.ToString("MM") == "02") || (DateTime.Now.ToString("MM") == "03") || (DateTime.Now.ToString("MM") == "04") || (DateTime.Now.ToString("MM") == "05"))
                    {
                        ProgramSeason = "WinterSpring" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09"))
                    {
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    //Define the ProgramSeason for the inserted record..RCM...4/5/12.
                    if ((DateTime.Now.ToString("MM") == "05") || (DateTime.Now.ToString("MM") == "06") || (DateTime.Now.ToString("MM") == "07") || (DateTime.Now.ToString("MM") == "08") || (DateTime.Now.ToString("MM") == "09"))
                    {
                        ProgramSeason = "Summer" + DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        ProgramSeason = "OffSeason" + DateTime.Now.ToString("yyyy");
                    }
                }
            }
            catch (Exception lkjlkj)
            {


            }
            finally
            {
            }
            return ProgramSeason;
        }
        
        protected void cmbReport_Click(object sender, EventArgs e)
        {
            gvAttendanceList.Visible = false;
            lblTeamColors.Visible = false;
            //lblTeamColor.Visible = false;
            //ddlTeamSectionUpdate.Visible = false;
            gvReport.Visible = true;
                       
            if (ddlProgram.Text == "MS Basketball League")
            {
                try
                {
                    //chbParentalConsentForm.Visible = false;
                    //chbRegistrationForm.Visible = false;
                    //chbPaid.Visible = false;
                    //chbGotPicture.Visible = false;
                    //chbContract.Visible = false;
                    //txbCoachTeam.Visible = false;
                    //lblName.Visible = false;
                    //lblName2.Visible = false;
                    //lblStudentNames.Visible = false;
                    //lblCoachTeam.Visible = false;
                    //imgStudent.Visible = false;
                    //cmbUpdate.Visible = false;
                    //txbComments.Visible = false;

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("MSBasketballLeague");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("MSBasketballLeagueEnrollment", "MSBasketballLeague", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("MSBasketballLeagueEnrollment", "MSBasketballLeague", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionmsbasketballleague", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    //cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                try
                {

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("HSBasketballLeague");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("HSBasketballLeagueEnrollment", "HSBasketballLeague", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("HSBasketballLeagueEnrollment", "HSBasketballLeague", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionhsbasketballleague", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                    //cmbStudentPage.Enabled = false;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "BasketballTEAMS")
            {
                try
                {

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("BasketballTEAMS");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("BasketballTEAMSEnrollment", "BasketballTEAMS", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("BasketballTEAMSEnrollment", "BasketballTEAMS", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionbasketballTEAMS", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Baseball")
            {
                try
                {
                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;
                    
                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("Baseball");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }
                    
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("BaseballEnrollment", "Baseball", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("BaseballEnrollment", "Baseball", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionbaseball", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                try
                {
                    imgStudent.Visible = false;

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("Outreach Basketball");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    
                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("OutreachBasketballEnrollment", "Outreach Basketball", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("OutreachBasketballEnrollment", "Outreach Basketball", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionoutreachbasketball", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                try
                {

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("SoccerIntraMurals");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("SoccerIntraMuralsEnrollment", "SoccerIntraMurals", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("SoccerIntraMuralsEnrollment", "SoccerIntraMurals", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionsoccerintramurals", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjllaaabb)
                {



                }
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                try
                {

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("SoccerTEAMS");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("SoccerTEAMSEnrollment", "SoccerTEAMS", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("SoccerTEAMSEnrollment", "SoccerTEAMS", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionsoccerteams", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjl_)
                {

                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                try
                {

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("MondayNights");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("MondayNightsEnrollment", "MondayNights", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("MondayNightsEnrollment", "MondayNights", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionmondaynights", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjl_)
                {
                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Special Events")
            {
                try
                {

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("Special Events");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("SpecialEventsEnrollment", "Special Events", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("SpecialEventsEnrollment", "Special Events", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionspecialevents", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjl_)
                {
                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                try
                {

                    ddlTeamSections.Visible = false;

                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("Bible Study");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("BibleStudyEnrollment", "Bible Study", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("BibleStudyEnrollment", "Bible Study", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolutionbiblestudy", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjl_)
                {
                    string lkjl = "";
                }
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                try
                {

                    ddlTeamSections.Visible = false;
                    ddlProgramSection.Visible = false;
                    lblProgramSections.Visible = false;

                    con3.Open();

                    ClearPivotDataTable();

                    //Determine Correct ProgramSeason...RCM..
                    string CurrentProgramSeason = "";
                    if (ddlProgramSeasonHistory.Text == "Program Season")
                    {
                        CurrentProgramSeason = DetermineCurrentProgramSeason("3on3 Basketball");
                    }
                    else
                    {
                        CurrentProgramSeason = ddlProgramSeasonHistory.Text;
                    }

                    if (ddlProgramSection.Text == "Please select a section")
                    {
                        ReloadPivotDataTable("[3on3BasketballEnrollment]", "3on3 Basketball", CurrentProgramSeason);
                    }
                    else
                    {
                        ReloadPivotDataTable("[3on3BasketballEnrollment]", "3on3 Basketball", CurrentProgramSeason, ddlProgramSection.Text);
                    }

                    SqlCommand objCmd = new System.Data.SqlClient.SqlCommand("pivotsolution3on3basketball", con3);
                    objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    gvReport.DataSource = objCmd.ExecuteReader();
                    gvReport.DataBind();

                    con3.Close();
                    cmbExcelExport.Enabled = true;
                    cmbExcelExport.Visible = true;
                }
                catch (Exception lkjl_)
                {
                    string lkjl = "";
                }
            }
        }

        protected void ClearPivotDataTable()
        {
            //Remove student from PivotData table..
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.PivotData ";
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
            finally
            {
                con.Close();
            }
        }

        protected void ReloadPivotDataTable(string ProgramTable, string Program, string ProgramSeason, string SectionName)
        {
            string reload_sql = "";
            
            try
            {

                if (SectionName == "")
                {
                    //By Program 
                    reload_sql = "insert into pivotdata "
                                + "select spa.LastName,spa.FirstName, spa.Section, spa.DAY, spa.Attended, si.CurrentRegistrationForm, si.Grade, si.ParentalConsentForm, 0, 0, spa.TeamName, 0,spa.Comment, si.HomePhone "
                                + "from StudentProgramAttendance spa "
                                + "LEFT OUTER JOIN StudentInformation si "
                                + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName AND spa.MiddleName = si.MiddleName) "
                                + "WHERE spa.Program = '" + Program + "' "
                                + "AND spa.ProgramSeason = '" + ProgramSeason + "' "
                                + "GROUP BY spa.LastName,spa.FirstName, spa.Section, spa.DAY, spa.Attended, si.CurrentRegistrationForm, si.Grade, si.ParentalConsentForm, spa.TeamName, spa.Comment, si.HomePhone "
                                + "ORDER BY spa.LastName, spa.FirstName ";
                }
                else
                {
                    //By ProgramSeason, SectionName
                    reload_sql = "insert into pivotdata "
                                + "select spa.LastName,spa.FirstName, spa.Section, spa.DAY, spa.Attended, si.CurrentRegistrationForm, si.Grade, si.ParentalConsentForm, 0, 0, spa.TeamName, 0,spa.Comment, si.HomePhone "
                                + "from StudentProgramAttendance spa "
                                + "LEFT OUTER JOIN StudentInformation si "
                                + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName AND spa.MiddleName = si.MiddleName) "
                                + "WHERE spa.Program = '" + Program + "' "
                                + "AND spa.ProgramSeason = '" + ProgramSeason + "' "
                                + "AND spa.Section = '" + SectionName + "' "
                                + "GROUP BY spa.LastName,spa.FirstName, spa.Section, spa.DAY, spa.Attended, si.CurrentRegistrationForm, si.Grade, si.ParentalConsentForm, spa.TeamName, spa.Comment, si.HomePhone "
                                + "ORDER BY spa.LastName, spa.FirstName ";
                }
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand = new SqlCommand(reload_sql, con);
                if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                    //Didn't find a record to update..RCM..
                }
            }
            catch (Exception lkjlkaaabbb)
            {


            }
        }

        protected void ReloadPivotDataTable(string ProgramTable, string Program, string ProgramSeason)
        {
            string reload_sql = "";

            try
            {
                //if (ProgramSeason == "")
                //{
                    //By Program 
                    reload_sql = "insert into pivotdata "
                                + "select spa.LastName,spa.FirstName, spa.Section, spa.DAY, spa.Attended, si.CurrentRegistrationForm, si.Grade, si.ParentalConsentForm, 0, 0, spa.TeamName, 0,spa.Comment, si.HomePhone "
                                + "from StudentProgramAttendance spa "
                                + "LEFT OUTER JOIN StudentInformation si "
                                + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName AND spa.MiddleName = si.MiddleName) "
                                + "WHERE spa.Program = '" + Program + "' "
                                + "AND spa.ProgramSeason = '" + ProgramSeason + "' "
                                + "GROUP BY spa.LastName,spa.FirstName, spa.Section, spa.DAY, spa.Attended, si.CurrentRegistrationForm, si.Grade, si.ParentalConsentForm, spa.TeamName, spa.Comment, si.HomePhone "
                                + "ORDER BY spa.LastName, spa.FirstName ";
                //}
                //else
                //{
                //    //By ProgramSeason, SectionName
                //    reload_sql = "insert into pivotdata "
                //                + "select spa.LastName,spa.FirstName, spa.Section, spa.DAY, spa.Attended, si.CurrentRegistrationForm, si.Grade, si.ParentalConsentForm, 0, 0, spa.TeamName, 0,spa.Comment, si.HomePhone "
                //                + "from StudentProgramAttendance spa "
                //                + "LEFT OUTER JOIN StudentInformation si "
                //                + "ON (spa.LastName = si.LastName AND spa.FirstName = si.FirstName AND spa.MiddleName = si.MiddleName) "
                //                + "WHERE spa.Program = '" + Program + "' " 
                //                + "AND spa.ProgramSeason = '" + ProgramSeason + "' "
                //                + "GROUP BY spa.LastName,spa.FirstName, spa.Section, spa.DAY, spa.Attended, si.CurrentRegistrationForm, si.Grade, si.ParentalConsentForm, spa.TeamName, spa.Comment, si.HomePhone "
                //                + "ORDER BY spa.LastName, spa.FirstName ";
                //}
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand = new SqlCommand(reload_sql, con);
                if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                {
                }
                else
                {
                    //Didn't find a record to update..RCM..
                }
            }
            catch (Exception lkjlkaaabbb)
            {

            }
        }

        protected void cmbCallingListReport_Click(object sender, EventArgs e)
        {

        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            if ((gvReport.Rows.Count != 0))
            {
                gvReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvReport, Response);
            }
            else if (gvAttendanceList.Rows.Count != 0)
            {
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvAttendanceList, Response);
            }
        }

        protected void cmbSectionMaintenance_Click(object sender, EventArgs e)
        {

        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("AthleticsAttendanceHistory.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
        }
    }
}