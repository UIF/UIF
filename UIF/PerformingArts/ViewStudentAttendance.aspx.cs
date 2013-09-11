using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using UrbanImpactCommon;

namespace UIF.PerformingArts
{
    public partial class ViewStudentAttendance : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
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

                    Populate_MonthList();
                    Populate_DayList();
                    Populate_YearList();
                    PopulateDateRanges();

                    ddlStudentName.Items.Add("Choose a student");
                    ddlStudentName.Text = "Choose a student";
                    ddlClassName.Items.Add("Choose a classname");
                    ddlClassName.Text = "Choose a classname";
                    ddlSectionName.Items.Add("Choose a section");
                    ddlSectionName.Text = "Choose a section";
                    ddlTeamName.Items.Add("Choose a teamname");
                    ddlTeamName.Text = "Choose a teamname";
                    ddlProgramSeason.Items.Add("Choose a ProgramSeason");
                    ddlProgramSeason.Text = "Choose a ProgramSeason";
                    ddlAttended.Items.Add("Attended?");
                    ddlAttended.Text = "Attended?";

                    PopulateProgramName();
                    PopulateStudentName();
                                        
                    ddlStudentName.Enabled = true;
                    PopulateClassName();
                }
            }
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("menutest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlProgram.Enabled = false;
            ddlStudentName.Enabled = true;
            
            PopulateStudentName();
            PopulateProgramSeason();
            ddlProgramSeason.Enabled = true;

            //If athletics, utilize the Section DropDownList...RCM..
            if ((ddlProgram.Text.Trim() == "Outreach Basketball") || (ddlProgram.Text.Trim() == "BasketballTEAMS") || (ddlProgram.Text.Trim() == "MS Basketball League") || (ddlProgram.Text.Trim() == "HS Basketball League") || (ddlProgram.Text.Trim() == "Baseball") || (ddlProgram.Text.Trim() == "SoccerIntraMurals") || (ddlProgram.Text.Trim() == "SoccerTEAMS") || (ddlProgram.Text.Trim() == "3on3 Basketball") || (ddlProgram.Text.Trim() == "Bible Study") || (ddlProgram.Text.Trim() == "Special Events") || (ddlProgram.Text.Trim() == "MondayNights"))
            {
                PopulateSectionName();
                ddlSectionName.Enabled = true;
            }
            else if (ddlProgram.Text.Trim() == "PerformingArtsAcademy")
            {
                PopulateClassName();
                ddlClassName.Enabled = true;
                PopulateSectionName();
                PopulateTeamName();
                PopulateProgramSeason();
            }
            else if (ddlProgram.Text.Trim() == "Childrens Choir")
            {

            }
            else
            {
                ddlClassName.Enabled = false;
            }

            ddlPickDateRangeMonth1.Text = "Month";
            ddlPickDateRangeDay1.Text = "Day";
            ddlPickDateRangeYear1.Text = "Year";
            ddlPickDateRangeMonth2.Text = "Month";
            ddlPickDateRangeDay2.Text = "Day";
            ddlPickDateRangeYear2.Text = "Year";
        }

        protected void ddlStudentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgram.Text == "PerformingArtsAcademy")
            {
                PopulateClassName();
            }
        }


        protected void PopulateProgramName()
        {
            //ddlProgram.Items.Add("Choose a program");
            //ddlProgram.Items.Add("Childrens Choir");
            //ddlProgram.Items.Add("MSHS Choir");
            //ddlProgram.Items.Add("PerformingArtsAcademy");
            //ddlProgram.Items.Add("Shakes");
            //ddlProgram.Items.Add("Singers");
            //ddlProgram.Items.Add("MondayNights");
            //ddlProgram.Text = "Choose a program";

            //Ryan C Manners..7/6/11. Handle dynamically...
            con.Open();

            try
            {
                //Pull the Program names dynamically..based on data that has been entered into the system... RCM..7/6/11.
                string Program_sql = "";
                Program_sql = "select Program "
                            + "from UIF_PerformingArts.dbo.studentprogramattendance "
                            + "group by Program "
                            + "order by Program ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(Program_sql);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    ddlProgram.Items.Add("Choose a program");
                    do
                    {
                        ddlProgram.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    ddlProgram.Text = "Choose a program";
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

        protected void PopulateStudentName()
        {
            SqlDataReader reader = null;
            con.Open();//Opens the db connection.
             
            try
            {
                string PopulateStudentName_sql = "";

                if (ddlProgram.Text == "Choose a program")
                {
                    if (ddlSectionName.Text == "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                    {
                        //Program
                        PopulateStudentName_sql = "Select lastname, firstname "
                                                + "From studentprogramattendance "
                                                //+ "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "GROUP BY lastname, firstname "
                                                + "ORDER BY lastname, firstname ";
                    }
                    else if (ddlSectionName.Text == "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                    {
                        //Program, day
                        PopulateStudentName_sql = "Select lastname, firstname "
                                               + "From studentprogramattendance "
                                               //+ "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               //+ "Where day = '" + "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                               + "Where day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "GROUP BY lastname, firstname "
                                               + "ORDER BY lastname, firstname ";
                    }
                    else if (ddlSectionName.Text == "Choose a section" && ddlClassName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                    {
                        //Program, class, day
                        PopulateStudentName_sql = "Select lastname, firstname "
                                               + "From studentprogramattendance "
                                               //+ "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "Where Section = '" + ddlClassName.Text.Trim() + "' "
                                               //+ "And day = '" + "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                               + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "GROUP BY lastname, firstname "
                                               + "ORDER BY lastname, firstname ";
                    }
                    else if (ddlSectionName.Text == "Choose a section" && ddlClassName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                    {
                        //Program, class
                        PopulateStudentName_sql = "Select lastname, firstname "
                                               + "From studentprogramattendance "
                                               //+ "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "Where Section = '" + ddlClassName.Text.Trim() + "' "
                                               + "GROUP BY lastname, firstname "
                                               + "ORDER BY lastname, firstname ";
                    }
                }
                else
                {
                    if (ddlProgram.Text == "PerformingArtsAcademy")
                    {
                        if (ddlSectionName.Text == "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlTeamName.Text == "Choose a teamname")
                        {
                            //Program
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                    + "From studentprogramattendance "
                                                    + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "GROUP BY lastname, firstname "
                                                    + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlTeamName.Text == "Choose a teamname")
                        {

                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                //+ "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlTeamName.Text != "Choose a teamname")
                        {
                            //Program, class, day
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlClassName.Text.Trim() + "' "
                                                   + "And TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                //+ "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                        {
                            //Program, class
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlClassName.Text.Trim() + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlTeamName.Text != "Choose a teamname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                        {
                            //Program, class
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                                   + "And TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if ((ddlSectionName.Text == "Choose a section") && (ddlProgramSeason.Text != "Choose a ProgramSeason") && (ddlTeamName.Text == "Choose a teamname") && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                        {
                            //Program, class
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And ProgramSeason = '" + ddlProgramSeason.Text.Trim() + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                    }
                    else
                    {
                        if (ddlSectionName.Text == "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlTeamName.Text == "Choose a teamname")
                        {
                            //Program
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                    + "From studentprogramattendance "
                                                    + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "GROUP BY lastname, firstname "
                                                    + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlTeamName.Text != "Choose a teamname")
                        {

                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                                   + "And TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                                   //+ "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlTeamName.Text == "Choose a teamname")
                        {

                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlTeamName.Text == "Choose a teamname")
                        {

                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                                   + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlTeamName.Text != "Choose a teamname")
                        {
                            //Program, class, day
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlClassName.Text.Trim() + "' "
                                                   + "And TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlTeamName.Text != "Choose a teamname")
                        {
                            //Program, class, day
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlClassName.Text.Trim() + "' "
                                                   + "And TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                                   + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        else if (ddlSectionName.Text != "Choose a section" && ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlTeamName.Text != "Choose a teamname")
                        {
                            //Program, class
                            PopulateStudentName_sql = "Select lastname, firstname "
                                                   + "From studentprogramattendance "
                                                   + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                   + "And Section = '" + ddlClassName.Text.Trim() + "' "
                                                   + "And TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                                   + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                   + "GROUP BY lastname, firstname "
                                                   + "ORDER BY lastname, firstname ";
                        }
                        //else if (ddlSectionName.Text != "Choose a section" && ddlTeamName.Text != "Choose a teamname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                        //{
                        //    //Program, class
                        //    PopulateStudentName_sql = "Select lastname, firstname "
                        //                           + "From studentprogramattendance "
                        //                           + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                        //                           + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                        //                           + "And TeamName = '" + ddlTeamName.Text.Trim() + "' "
                        //                           + "GROUP BY lastname, firstname "
                        //                           + "ORDER BY lastname, firstname ";
                        //}
                        //else if ((ddlSectionName.Text == "Choose a section") && (ddlProgramSeason.Text != "Choose a ProgramSeason") && (ddlTeamName.Text == "Choose a teamname") && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                        //{
                        //    //Program, class
                        //    PopulateStudentName_sql = "Select lastname, firstname "
                        //                           + "From studentprogramattendance "
                        //                           + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                        //                           + "And ProgramSeason = '" + ddlProgramSeason.Text.Trim() + "' "
                        //                           + "GROUP BY lastname, firstname "
                        //                           + "ORDER BY lastname, firstname ";
                        //}
                    }
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "studentprogramattendance");

                //Iterate over setup records and call method to do the work on each one...RCM..
                string currentstudent = "";
                currentstudent = ddlStudentName.Text;
                ddlStudentName.Items.Clear();
                ddlStudentName.Items.Add("Choose a student");
                foreach (DataRow myDataRowPO in custDS.Tables["studentprogramattendance"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlStudentName.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                }
                ddlStudentName.Text = currentstudent;
                custDS.Clear();
            }
            catch (Exception lkjl)
            {


            }
            finally
            {
                con.Close();
            }
        }



        protected void PopulateSectionName()
        {
            SqlDataReader reader = null;
            con.Open();//Opens the db connection.

            try
            {
                string PopulateStudentName_sql = "";

                if (ddlProgram.Text == "Choose a program")
                {
                    //if (ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                    //{
                    //    //Program
                    //    PopulateStudentName_sql = "Select Section "
                    //                            + "From studentprogramattendance "
                    //                            + "GROUP BY Section "
                    //                            + "ORDER BY Section ";
                    //}
                    //else if (ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                    //{
                    //    //Program, day
                    //    PopulateStudentName_sql = "Select Section "
                    //                           + "From studentprogramattendance "
                    //                           + "Where day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                    //                           + "GROUP BY Section "
                    //                           + "ORDER BY Section ";
                    //}
                    //else if (ddlClassName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                    //{
                    //    //Program, class, day
                    //    PopulateStudentName_sql = "Select Section "
                    //                           + "From studentprogramattendance "
                    //                           + "Where Section = '" + ddlClassName.Text.Trim() + "' "
                    //                           + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                    //                           + "GROUP BY Section "
                    //                           + "ORDER BY Section ";
                    //}
                    //else if (ddlClassName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                    //{
                    //    //Program, class
                    //    PopulateStudentName_sql = "Select Section "
                    //                           + "From studentprogramattendance "
                    //                           //+ "Where Section = '" + ddlClassName.Text.Trim() + "' "
                    //                           + "GROUP BY Section "
                    //                           + "ORDER BY Section ";
                    //}
                }
                else
                {
                    if ((ddlProgramSeason.Text == "Choose a ProgramSeason") && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                    {
                        //Program, no daterange.
                        PopulateStudentName_sql = "Select Section "
                                                + "From studentprogramattendance "
                                                + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "GROUP BY Section "
                                                + "ORDER BY Section ";
                    }
                    else if ((ddlProgramSeason.Text == "Choose a ProgramSeason") && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))
                    {
                        //Program, daterange.
                        PopulateStudentName_sql = "Select Section "
                                               + "From studentprogramattendance "
                                               + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "GROUP BY Section "
                                               + "ORDER BY Section ";
                    }
                    else if ((ddlProgramSeason.Text == "Choose a ProgramSeason") && (ddlStudentName.Text != "Choose a student") && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))
                    {
                        //Program, no daterange, student
                        PopulateStudentName_sql = "Select Section "
                                               + "From studentprogramattendance "
                                               + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                               + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "GROUP BY Section "
                                               + "ORDER BY Section ";
                    }
                    else if ((ddlProgramSeason.Text == "Choose a ProgramSeason") && (ddlStudentName.Text != "Choose a student") && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                    {
                        //Program, class
                        PopulateStudentName_sql = "Select Section "
                                               + "From studentprogramattendance "
                                               + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                               + "GROUP BY Section "
                                               + "ORDER BY Section ";
                    }
                    else if ((ddlProgramSeason.Text != "Choose a ProgramSeason") && (ddlStudentName.Text != "Choose a student") && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                    {
                        //Program, class
                        PopulateStudentName_sql = "Select Section "
                                               + "From studentprogramattendance "
                                               + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                               + "AND ProgramSeason = '" + ddlProgramSeason.Text.Trim() + "' "
                                               + "GROUP BY Section "
                                               + "ORDER BY Section ";
                    }
                    else if ((ddlProgramSeason.Text != "Choose a ProgramSeason") && (ddlStudentName.Text == "Choose a student") && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                    {
                        //Program, class
                        PopulateStudentName_sql = "Select Section "
                                               + "From studentprogramattendance "
                                               + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               //+ "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               //+ "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                               + "AND ProgramSeason = '" + ddlProgramSeason.Text.Trim() + "' "
                                               + "GROUP BY Section "
                                               + "ORDER BY Section ";
                    }
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "studentprogramattendance");

                //Iterate over setup records and call method to do the work on each one...RCM..
                string currentsection = "";
                currentsection = ddlSectionName.Text;
                ddlSectionName.Items.Clear();
                ddlSectionName.Items.Add("Choose a section");
                foreach (DataRow myDataRowPO in custDS.Tables["studentprogramattendance"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlSectionName.Items.Add(myDataRowPO[0].ToString());
                }
                ddlSectionName.Text = currentsection;
                custDS.Clear();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                con.Close();
            }
        }


        protected void PopulateProgramSeason()
        {
            SqlDataReader reader = null;
            con.Open();//Opens the db connection.

            try
            {
                string PopulateStudentName_sql = "";

                if (ddlProgram.Text == "Choose a program")
                {
                    //if (ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                    //{
                    //    //Program
                    //    PopulateStudentName_sql = "Select Section "
                    //                            + "From studentprogramattendance "
                    //                            + "GROUP BY Section "
                    //                            + "ORDER BY Section ";
                    //}
                    //else if (ddlClassName.Text == "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                    //{
                    //    //Program, day
                    //    PopulateStudentName_sql = "Select Section "
                    //                           + "From studentprogramattendance "
                    //                           + "Where day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                    //                           + "GROUP BY Section "
                    //                           + "ORDER BY Section ";
                    //}
                    //else if (ddlClassName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                    //{
                    //    //Program, class, day
                    //    PopulateStudentName_sql = "Select Section "
                    //                           + "From studentprogramattendance "
                    //                           + "Where Section = '" + ddlClassName.Text.Trim() + "' "
                    //                           + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                    //                           + "GROUP BY Section "
                    //                           + "ORDER BY Section ";
                    //}
                    //else if (ddlClassName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                    //{
                    //    //Program, class
                    //    PopulateStudentName_sql = "Select Section "
                    //                           + "From studentprogramattendance "
                    //        //+ "Where Section = '" + ddlClassName.Text.Trim() + "' "
                    //                           + "GROUP BY Section "
                    //                           + "ORDER BY Section ";
                    //}
                }
                else
                {
                    if ((ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                    {
                        //Program, no daterange.
                        PopulateStudentName_sql = "Select ProgramSeason "
                                                + "From studentprogramattendance "
                                                + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "GROUP BY ProgramSeason "
                                                + "ORDER BY ProgramSeason ";
                    }
                    else if ((ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))
                    {
                        //Program, daterange.
                        PopulateStudentName_sql = "Select ProgramSeason "
                                               + "From studentprogramattendance "
                                               + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "GROUP BY ProgramSeason "
                                               + "ORDER BY ProgramSeason ";
                    }
                    else if ((ddlStudentName.Text != "Choose a student") && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))
                    {
                        //Program, no daterange, student
                        PopulateStudentName_sql = "Select ProgramSeason "
                                               + "From studentprogramattendance "
                                               + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                               + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "GROUP BY ProgramSeason "
                                               + "ORDER BY ProgramSeason ";
                    }
                    else if (ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                    {
                        //Program, class
                        PopulateStudentName_sql = "Select ProgramSeason "
                                               + "From studentprogramattendance "
                                               + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                               + "GROUP BY ProgramSeason "
                                               + "ORDER BY ProgramSeason ";
                    }
                }
                SqlCommand cmd = new SqlCommand(PopulateStudentName_sql, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "studentprogramattendance");

                //Iterate over setup records and call method to do the work on each one...RCM..
                string currentprogramseason = "";
                currentprogramseason = ddlProgramSeason.Text;
                ddlProgramSeason.Items.Clear();
                ddlProgramSeason.Items.Add("Choose a ProgramSeason");
                foreach (DataRow myDataRowPO in custDS.Tables["studentprogramattendance"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlProgramSeason.Items.Add(myDataRowPO[0].ToString());
                }
                ddlProgramSeason.Text = currentprogramseason;
                custDS.Clear();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void PopulateClassName()
        {
            SqlDataReader reader = null;
            con.Open();//Opens the db connection.

            try
            {
                string PopulateClassName_sql = "";
                if (ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                {
                    //Program
                    PopulateClassName_sql = "Select Section "
                                            + "From studentprogramattendance "
                                            + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                            + "GROUP BY Section "
                                            + "ORDER BY Section ";
                }
                else if (ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                {
                    //Program, day
                    PopulateClassName_sql = "Select Section "
                                           + "From studentprogramattendance "
                                           + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                           //+ "And day = '" + "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                           + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                           + "GROUP BY Section "
                                           + "ORDER BY Section ";
                }
                else if (ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                {
                    //Program, day
                    PopulateClassName_sql = "Select Section "
                                           + "From studentprogramattendance "
                                           + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                           + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                           + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                           + "GROUP BY Section "
                                           + "ORDER BY Section ";
                }
                else if (ddlStudentName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                {
                    //Program, class, day
                    PopulateClassName_sql = "Select Section "
                                           + "From studentprogramattendance "
                                           + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                           + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                           + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                           //+ "And day = '" + "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                           + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                           + "GROUP BY Section "
                                           + "ORDER BY Section ";
                }

                SqlCommand cmd = new SqlCommand(PopulateClassName_sql, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "studentprogramattendance");

                //Iterate over setup records and call method to do the work on each one...RCM..
                string currentstudent = "";
                currentstudent = ddlClassName.Text;
                ddlClassName.Items.Clear();
                ddlClassName.Items.Add("Choose a classname");
                foreach (DataRow myDataRowPO in custDS.Tables["studentprogramattendance"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                   ddlClassName.Items.Add(myDataRowPO[0].ToString());
                }
                ddlClassName.Text = currentstudent;
                //ddlClassName.Text = "Choose a classname";
                custDS.Clear();
            }
            catch (Exception lkjl)
            {


            }
            finally
            {
                con.Close();
            }
        }


        protected void PopulateTeamName()
        {
            SqlDataReader reader = null;
            con.Open();//Opens the db connection.

            try
            {
                string PopulateClassName_sql = "";
                if (ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))
                {
                    PopulateClassName_sql = "Select TeamName "
                                            + "From studentprogramattendance "
                                            + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                            + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                            + "GROUP BY TeamName "
                                            + "ORDER BY TeamName ";
                }
                else if (ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                {
                    PopulateClassName_sql = "Select TeamName "
                                           + "From studentprogramattendance "
                                           + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                           + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                           + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                           + "GROUP BY TeamName "
                                           + "ORDER BY TeamName ";
                }
                else if (ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day"))//more to code..?
                {
                    PopulateClassName_sql = "Select TeamName "
                                           + "From studentprogramattendance "
                                           + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                           + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                           + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                           + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                           + "GROUP BY TeamName "
                                           + "ORDER BY TeamName ";
                }
                else if (ddlStudentName.Text != "Choose a classname" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day"))//more to code..?
                {
                    PopulateClassName_sql = "Select TeamName "
                                           + "From studentprogramattendance "
                                           + "Where Program = '" + ddlProgram.Text.Trim() + "' "
                                           + "And Section = '" + ddlSectionName.Text.Trim() + "' "
                                           + "AND lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                           + "AND firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                           + "And day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                           + "GROUP BY TeamName "
                                           + "ORDER BY TeamName ";
                }

                SqlCommand cmd = new SqlCommand(PopulateClassName_sql, con);
                cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                SqlDataAdapter custDA = new SqlDataAdapter();
                custDA.SelectCommand = cmd;
                DataSet custDS = new DataSet();
                custDA.Fill(custDS, "studentprogramattendance");

                //Iterate over setup records and call method to do the work on each one...RCM..
                string currentteam = "";
                currentteam = ddlTeamName.Text;
                ddlTeamName.Items.Clear();
                ddlTeamName.Items.Add("Choose a teamname");
                foreach (DataRow myDataRowPO in custDS.Tables["studentprogramattendance"].Rows)
                {
                    //Adding options to the drop downs for a new entry.
                    ddlTeamName.Items.Add(myDataRowPO[0].ToString());
                }
                ddlTeamName.Text = currentteam;
                //ddlClassName.Text = "Choose a classname";
                custDS.Clear();
            }
            catch (Exception lkjl)
            {


            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbGetResults_Click(object sender, EventArgs e)
        {
            string sql_GetResults = "";


            if (ddlProgram.Text == "Choose a program" && (ddlPickDateRangeMonth1.Text == "Month" || ddlPickDateRangeDay1.Text == "Day" || ddlPickDateRangeYear1.Text == "Year") && ddlStudentName.Text == "Choose a student" && ddlClassName.Text == "Choose a classname")
            {
                //No filters were chosen.  Would result in too much data..RCM..
                lblTooMuchData.Visible = true;
                //Exit.
            }
            else
            {

                if (ddlProgram.Text == "Choose a program")
                {
                    if (ddlPickDateRangeMonth2.Text == "Month" || ddlPickDateRangeDay2.Text == "Day" || ddlPickDateRangeYear2.Text == "Year")
                    {
                        //Just a single date parameter was specified.
                        //if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && calCalender1.SelectedDate.ToString("yyyy-MM-dd") == "0001-01-01" && ddlClassName.Text == "Choose a classname")
                        if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, StudentName
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "Where spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Section, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, StudentName, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "Where spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, StudentName, Class, 
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "Where spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "Where spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "Where spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class, StudentName, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "Where spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "Where spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        //else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        //{
                            //Program, Attended
                        //}
                        //else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        //{
                            //Program, Exempt
                        //}
                        //else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        //{
                            //Program, LastUpdateBy
                        //}
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                    }
                    else
                    {
                        //A date range was selected using a start and stop date..RCM.
                        if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, StudentName
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                               + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "Where spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                               + "order by spa.lastname, spa.firstname, spa.Section, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, StudentName, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                               + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "Where spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                               + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                               + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, StudentName, Class, 
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                               + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "Where spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                               + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                               + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                               + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                               + "Where spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                               + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                               + "FROM studentprogramattendance spa "
                                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "Where spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                               + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class, StudentName, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                               + "FROM studentprogramattendance spa "
                                                 //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                               + "Where spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                               + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                               + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                               + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                               + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                               + "FROM studentprogramattendance spa "
                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                               + "Where spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                               + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                               + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                               + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        //else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && calCalender1.SelectedDate.ToString("yyyy-MM-dd") != "0001-01-01" && ddlClassName.Text == "Choose a classname")
                        //{
                            //Program, Attended
                        //}
                        //else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && calCalender1.SelectedDate.ToString("yyyy-MM-dd") != "0001-01-01" && ddlClassName.Text == "Choose a classname")
                        //{
                            //Program, Exempt
                        //}
                        //else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && calCalender1.SelectedDate.ToString("yyyy-MM-dd") != "0001-01-01" && ddlClassName.Text == "Choose a classname")
                        //{
                            //Program, LastUpdateBy
                        //}
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                               + "FROM studentprogramattendance spa "
                                //+ "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                               + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                    }
                }
                else
                {
                    if (ddlProgram.Text == "PerformingArtsAcademy")
                    {
                        if (ddlSectionName.Text != "Choose a section" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeMonth2.Text == "Month" || ddlPickDateRangeDay2.Text == "Day" || ddlPickDateRangeYear2.Text == "Year"))
                        {
                            //Program, StudentName
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.Section = '" + ddlSectionName.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Section, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, StudentName
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Section, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, StudentName, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, StudentName, Class, 
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class, StudentName, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else
                        {
                            //No date range was picked...RCM..8/10/12.
                            //Program, Class, StudentName, Day
                            if (ddlProgram.Text == "PerformingArtsAcademy")
                            {
                                //Handle Section differently...  Section is used for classnames.

                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                    //+ "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                    //+ "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else
                            {
                                //No date range or Student picked.
                                if (ddlStudentName.Text == "Choose a student")
                                {
                                    sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                        + "FROM studentprogramattendance spa "
                                                        + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                        + "AND spa.Section = '" + ddlSectionName.Text.Trim() + "' "
                                                        + "AND spa.TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                        //+ "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                        //+ "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                        //+ "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                        //+ "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                        + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                        + "order by spa.lastname, spa.firstname, spa.Day ";
                                }
                                else
                                {
                                    //No date range picked..
                                    sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                        + "FROM studentprogramattendance spa "
                                                        + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                        + "AND spa.Section = '" + ddlSectionName.Text.Trim() + "' "
                                                        + "AND spa.TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                                        + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                        + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                        //+ "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                        //+ "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                        + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                        + "order by spa.lastname, spa.firstname, spa.Day ";
                                }



                                //No date range picked..  ProgramSeason, Section, TeamName, 
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.Section = '" + ddlSectionName.Text.Trim() + "' "
                                                    + "AND spa.ProgramSeason = '" + ddlProgramSeason.Text.Trim() + "' "
                                                    + "AND spa.TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                    //+ "AND spa.Attended = 1 "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                    //+ "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                    //+ "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";


                            }
                        }
                    }
                    else
                    {
                        if (ddlPickDateRangeMonth2.Text == "Month" || ddlPickDateRangeDay2.Text == "Day" || ddlPickDateRangeYear2.Text == "Year")
                        {
                            //Just a single date parameter was specified.
                            //if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && calCalender1.SelectedDate.ToString("yyyy-MM-dd") == "0001-01-01" && ddlClassName.Text == "Choose a classname")
                            if (ddlSectionName.Text != "Choose a teamname" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                            {
                                //Program, StudentName
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Section, spa.Day ";
                            }
                            else if (ddlSectionName.Text != "Choose a teamname" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                            {
                                //Program, StudentName, Day
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                   //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                    + "AND spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                    + "AND spa.ProgramSeason = '" + ddlProgramSeason.Text.Trim() + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else if (ddlSectionName.Text != "Choose a teamname" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname" && ddlProgramSeason.Text != "Choose a ProgramSeason")
                            {
                                //Program, StudentName, Day
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                    //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                    + "AND spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                    + "AND spa.ProgramSeason = '" + ddlProgramSeason.Text.Trim() + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else if (ddlSectionName.Text != "Choose a teamname" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                            {
                                //Program, StudentName, Class, 
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                    + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else if (ddlSectionName.Text != "Choose a teamname" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                            {
                                //Program, Day
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                    //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                    + "AND spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else if (ddlSectionName.Text != "Choose a teamname" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                            {
                                //Program, Class
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else if (ddlSectionName.Text != "Choose a teamname" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text != "Choose a classname")
                            {
                                //Program, Class, StudentName, Day
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                    //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                    + "AND spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                    + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else if (ddlSectionName.Text != "Choose a teamname" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text != "Choose a classname")
                            {
                                //Program, Class, Day
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                    //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                    + "AND spa.Day = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                    + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                            {
                                //Program, Attended
                            }
                            else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                            {
                                //Program, Exempt
                            }
                            else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                            {
                                //Program, LastUpdateBy
                            }
                            else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                            {
                                //Program
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, StudentName
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Section, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, StudentName, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, StudentName, Class, 
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text != "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class, StudentName, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text != "Year" && ddlPickDateRangeMonth1.Text != "Month" && ddlPickDateRangeDay1.Text != "Day") && ddlClassName.Text != "Choose a classname")
                        {
                            //Program, Class, Day
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                //+ "AND spa.Day = '" + calCalender1.SelectedDate.ToString("yyyy-MM-dd") + "' "
                                                + "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                                + "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else if (ddlProgram.Text != "" && ddlStudentName.Text == "Choose a student" && (ddlPickDateRangeYear1.Text == "Year" && ddlPickDateRangeMonth1.Text == "Month" && ddlPickDateRangeDay1.Text == "Day") && ddlClassName.Text == "Choose a classname")
                        {
                            //Program
                            sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                + "FROM studentprogramattendance spa "
                                                + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                + "order by spa.lastname, spa.firstname, spa.Day ";
                        }
                        else
                        {
                            //No date range was picked...RCM..8/10/12.
                            //Program, Class, StudentName, Day
                            if (ddlProgram.Text == "PerformingArtsAcademy")
                            {
                                //Handle Section differently...  Section is used for classnames.

                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.Section = '" + ddlClassName.Text.Trim() + "' "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                    //+ "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                    //+ "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";
                            }
                            else
                            {
                                //No date range or Student picked.
                                if (ddlStudentName.Text == "Choose a student")
                                {
                                    sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                        + "FROM studentprogramattendance spa "
                                                        + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                        + "AND spa.Section = '" + ddlSectionName.Text.Trim() + "' "
                                                        + "AND spa.TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                        //+ "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                        //+ "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                        //+ "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                        //+ "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                        + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                        + "order by spa.lastname, spa.firstname, spa.Day ";
                                }
                                else
                                {
                                    //No date range picked..
                                    sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                        + "FROM studentprogramattendance spa "
                                                        + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                        + "AND spa.Section = '" + ddlSectionName.Text.Trim() + "' "
                                                        + "AND spa.TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                                        + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                        + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                        //+ "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                        //+ "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                        + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                        + "order by spa.lastname, spa.firstname, spa.Day ";
                                }



                                //No date range picked..  ProgramSeason, Section, TeamName, 
                                sql_GetResults = "select spa.Lastname as 'StudentLastName', spa.Firstname as 'StudentFirstName', spa.Program, spa.Section, spa.TeamName, LEFT(spa.Day, 10) as 'Day', spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy "
                                                    + "FROM studentprogramattendance spa "
                                                    + "WHERE spa.Program = '" + ddlProgram.Text.Trim() + "' "
                                                    + "AND spa.Section = '" + ddlSectionName.Text.Trim() + "' "
                                                    + "AND spa.ProgramSeason = '" + ddlProgramSeason.Text.Trim() + "' "
                                                    + "AND spa.TeamName = '" + ddlTeamName.Text.Trim() + "' "
                                    //+ "AND spa.Attended = 1 "
                                                    + "AND spa.lastname = '" + ddlStudentName.SelectedValue.Substring(0, ddlStudentName.SelectedValue.IndexOf(",")) + "' "
                                                    + "AND spa.firstname = '" + ddlStudentName.SelectedValue.Substring(ddlStudentName.SelectedValue.IndexOf(",") + 1, ddlStudentName.SelectedValue.Length - (ddlStudentName.SelectedValue.IndexOf(",") + 1)) + "' "
                                    //+ "AND spa.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' "
                                    //+ "AND spa.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' "
                                                    + "GROUP BY spa.Lastname, spa.Firstname, spa.Program, spa.Section, spa.TeamName, spa.Day, spa.Attended, spa.Exempt, spa.Hours, spa.ProgramSeason, spa.sysupdate, spa.LastUpdatedBy  "
                                                    + "order by spa.lastname, spa.firstname, spa.Day ";


                            }
                        }
                    }
                }
            }

            if (ddlProgram.Text == "Choose a program" && (ddlPickDateRangeMonth1.Text == "Month" || ddlPickDateRangeDay1.Text == "Day" || ddlPickDateRangeYear1.Text == "Year") && ddlStudentName.Text == "Choose a student" && ddlClassName.Text == "Choose a classname")
            {
                //No filters were chosen.  Would result in too much data..RCM..
                lblTooMuchData.Visible = true;
                //Exit.
            }
            else
            {

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql_GetResults);

                SqlDataAdapter da = new SqlDataAdapter(sql_GetResults, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "studentprogramattendance");

                //Clear the gridview..RCM.
                gvAttendanceResults.DataSource = null;
                gvAttendanceResults.DataBind();

                //Reload the gridview..RCM.
                gvAttendanceResults.DataSource = ds.Tables[0];
                gvAttendanceResults.DataBind();


                //---------testing...
                //gvAttendanceResults.Columns[5]. = ds.Tables[0].Columns[5].ReadOnly = false;
                //gvAttendanceResults.DataSourceID = ds.Tables[0].Columns[6].ReadOnly = false;


                //int i = 0;
                //foreach (GridViewRow row in gvAttendanceResults.Rows)
                //{
                //    //string INSERT_ATTENDANCE_DATA = "";
                //    GridViewRow row2 = (GridViewRow)gvAttendanceResults.Rows[i];
                //    string lkjl = row2.Cells[1].Text;
                //    string ll = row2.Cells[3].Text;
                //    string aa = row2.Cells[4].Text;
                //    Boolean bb = System.Convert.ToBoolean(row2.Cells[5]);
                //    Boolean cc = System.Convert.ToBoolean(row2.Cells[6]);
                //    string dd = row2.Cells[7].Text;

                //    CheckBox Attended = (CheckBox)row2.FindControl("checkboxlist1");
                //    Attended.Checked = Convert.ToBoolean(row2.Cells[5].Text);

                    

                    
                    //DropDownList Exempt = (DropDownList)row2.FindControl("dropdownlist2");
                    //CheckBox Attended = (CheckBox)

                    
                    
                    //CheckBox Attended = (CheckBox)row2.FindControl("checkboxlist1");
                    //Attended.Checked = 
                    //row2.Cells[5].Text = Attended;                    

                    //(CheckBox)row2.FindControl("checkboxlist1").r
                    
                    
                    //Attended.
                    //Boolean Attended = false;
                    //Boolean Exempt = false;

                    //if (ddlProgram.Text == "MSHS Choir")
                    //{
                    //INSERT_ATTENDANCE_DATA = "Insert into StudentProgramAttendance "
                      //                      + "values ("
                                            //+ "'" + row2.Cells[0].Text + "',"
                                            //+ "'" + row2.Cells[1].Text + "',"
                                            //+ "'" + ddlProgram.Text + "',"
                                            //+ "'" + "N/A" + "',"
                        //                 + "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calCalender2.SelectedDate.Month) + "',"
                        //                 + "'" + calCalender2.SelectedDate.ToString("yyyy-MM-dd") + "',"
                        //                    + System.Convert.ToInt32(System.Convert.ToBoolean(Attended.Text)) + ", ";
                    //}
               // }


                //-----testing..
                
                con.Close();
                cmbExcelExport.Enabled = true;
            }
        }
        
        protected void PopulateDateRanges()
        {
            //ddlDateRange.Items.Add("Choose a daterange");
            //ddlDateRange.Items.Add("Summer 2010");
            //ddlDateRange.Items.Add("Fall Semester 2010");
            //ddlDateRange.Items.Add("Srping Semester 2011");
            //ddlDateRange.Items.Add("Summer 2011");
            //ddlDateRange.Items.Add("Fall Semester 2011");
            //ddlDateRange.Items.Add("Sping Semester 2012");
            //ddlDateRange.Text = "Choose a daterange";
        }

        protected void Populate_MonthList()
        {
            //Add each month to the list
            //ddlCalMonth.Items.Add("Month");
            //ddlCalMonth.Items.Add("January");
            //ddlCalMonth.Items.Add("February");
            //ddlCalMonth.Items.Add("March");
            //ddlCalMonth.Items.Add("April");
            //ddlCalMonth.Items.Add("May");
            //ddlCalMonth.Items.Add("June");
            //ddlCalMonth.Items.Add("July");
            //ddlCalMonth.Items.Add("August");
            //ddlCalMonth.Items.Add("September");
            //ddlCalMonth.Items.Add("October");
            //ddlCalMonth.Items.Add("November");
            //ddlCalMonth.Items.Add("December");
            //ddlCalMonth.Text = "Month";
            ////ddlCalMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;

            //ddlCalMonth.Text = System.DateTime.Now.Month.ToString();

            //Make the current month selected item in the list
            //ddlCalMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;

            //Add each month to the list
            ddlPickDateRangeMonth1.Items.Add("Month");
            ddlPickDateRangeMonth1.Items.Add("January");
            ddlPickDateRangeMonth1.Items.Add("February");
            ddlPickDateRangeMonth1.Items.Add("March");
            ddlPickDateRangeMonth1.Items.Add("April");
            ddlPickDateRangeMonth1.Items.Add("May");
            ddlPickDateRangeMonth1.Items.Add("June");
            ddlPickDateRangeMonth1.Items.Add("July");
            ddlPickDateRangeMonth1.Items.Add("August");
            ddlPickDateRangeMonth1.Items.Add("September");
            ddlPickDateRangeMonth1.Items.Add("October");
            ddlPickDateRangeMonth1.Items.Add("November");
            ddlPickDateRangeMonth1.Items.Add("December");
            ddlPickDateRangeMonth1.Text = "Month";

            ddlPickDateRangeMonth2.Items.Add("Month");
            ddlPickDateRangeMonth2.Items.Add("January");
            ddlPickDateRangeMonth2.Items.Add("February");
            ddlPickDateRangeMonth2.Items.Add("March");
            ddlPickDateRangeMonth2.Items.Add("April");
            ddlPickDateRangeMonth2.Items.Add("May");
            ddlPickDateRangeMonth2.Items.Add("June");
            ddlPickDateRangeMonth2.Items.Add("July");
            ddlPickDateRangeMonth2.Items.Add("August");
            ddlPickDateRangeMonth2.Items.Add("September");
            ddlPickDateRangeMonth2.Items.Add("October");
            ddlPickDateRangeMonth2.Items.Add("November");
            ddlPickDateRangeMonth2.Items.Add("December");
            ddlPickDateRangeMonth2.Text = "Month";
        }


        protected void Populate_DayList()
        {
            //ddlCalDay.Items.Add("Day");
            //ddlCalDay.Items.Add("01");
            //ddlCalDay.Items.Add("02");
            //ddlCalDay.Items.Add("03");
            //ddlCalDay.Items.Add("04");
            //ddlCalDay.Items.Add("05");
            //ddlCalDay.Items.Add("06");
            //ddlCalDay.Items.Add("07");
            //ddlCalDay.Items.Add("08");
            //ddlCalDay.Items.Add("09");
            //ddlCalDay.Items.Add("10");
            //ddlCalDay.Items.Add("11");
            //ddlCalDay.Items.Add("12");
            //ddlCalDay.Items.Add("13");
            //ddlCalDay.Items.Add("14");
            //ddlCalDay.Items.Add("15");
            //ddlCalDay.Items.Add("16");
            //ddlCalDay.Items.Add("17");
            //ddlCalDay.Items.Add("18");
            //ddlCalDay.Items.Add("19");
            //ddlCalDay.Items.Add("20");
            //ddlCalDay.Items.Add("21");
            //ddlCalDay.Items.Add("22");
            //ddlCalDay.Items.Add("23");
            //ddlCalDay.Items.Add("24");
            //ddlCalDay.Items.Add("25");
            //ddlCalDay.Items.Add("26");
            //ddlCalDay.Items.Add("27");
            //ddlCalDay.Items.Add("28");
            //ddlCalDay.Items.Add("29");
            //ddlCalDay.Items.Add("30");
            //ddlCalDay.Items.Add("31");
            //ddlCalDay.Text = "Day";

            //ddlCalMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;

//            ddlCalDay.Text = System.DateTime.Now.Day.ToString();

            //Make the current month selected item in the list
            //ddlCalMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;

            ddlPickDateRangeDay1.Items.Add("Day");
            ddlPickDateRangeDay1.Items.Add("01");
            ddlPickDateRangeDay1.Items.Add("02");
            ddlPickDateRangeDay1.Items.Add("03");
            ddlPickDateRangeDay1.Items.Add("04");
            ddlPickDateRangeDay1.Items.Add("05");
            ddlPickDateRangeDay1.Items.Add("06");
            ddlPickDateRangeDay1.Items.Add("07");
            ddlPickDateRangeDay1.Items.Add("08");
            ddlPickDateRangeDay1.Items.Add("09");
            ddlPickDateRangeDay1.Items.Add("10");
            ddlPickDateRangeDay1.Items.Add("11");
            ddlPickDateRangeDay1.Items.Add("12");
            ddlPickDateRangeDay1.Items.Add("13");
            ddlPickDateRangeDay1.Items.Add("14");
            ddlPickDateRangeDay1.Items.Add("15");
            ddlPickDateRangeDay1.Items.Add("16");
            ddlPickDateRangeDay1.Items.Add("17");
            ddlPickDateRangeDay1.Items.Add("18");
            ddlPickDateRangeDay1.Items.Add("19");
            ddlPickDateRangeDay1.Items.Add("20");
            ddlPickDateRangeDay1.Items.Add("21");
            ddlPickDateRangeDay1.Items.Add("22");
            ddlPickDateRangeDay1.Items.Add("23");
            ddlPickDateRangeDay1.Items.Add("24");
            ddlPickDateRangeDay1.Items.Add("25");
            ddlPickDateRangeDay1.Items.Add("26");
            ddlPickDateRangeDay1.Items.Add("27");
            ddlPickDateRangeDay1.Items.Add("28");
            ddlPickDateRangeDay1.Items.Add("29");
            ddlPickDateRangeDay1.Items.Add("30");
            ddlPickDateRangeDay1.Items.Add("31");
            ddlPickDateRangeDay1.Text = "Day";

            ddlPickDateRangeDay2.Items.Add("Day");
            ddlPickDateRangeDay2.Items.Add("01");
            ddlPickDateRangeDay2.Items.Add("02");
            ddlPickDateRangeDay2.Items.Add("03");
            ddlPickDateRangeDay2.Items.Add("04");
            ddlPickDateRangeDay2.Items.Add("05");
            ddlPickDateRangeDay2.Items.Add("06");
            ddlPickDateRangeDay2.Items.Add("07");
            ddlPickDateRangeDay2.Items.Add("08");
            ddlPickDateRangeDay2.Items.Add("09");
            ddlPickDateRangeDay2.Items.Add("10");
            ddlPickDateRangeDay2.Items.Add("11");
            ddlPickDateRangeDay2.Items.Add("12");
            ddlPickDateRangeDay2.Items.Add("13");
            ddlPickDateRangeDay2.Items.Add("14");
            ddlPickDateRangeDay2.Items.Add("15");
            ddlPickDateRangeDay2.Items.Add("16");
            ddlPickDateRangeDay2.Items.Add("17");
            ddlPickDateRangeDay2.Items.Add("18");
            ddlPickDateRangeDay2.Items.Add("19");
            ddlPickDateRangeDay2.Items.Add("20");
            ddlPickDateRangeDay2.Items.Add("21");
            ddlPickDateRangeDay2.Items.Add("22");
            ddlPickDateRangeDay2.Items.Add("23");
            ddlPickDateRangeDay2.Items.Add("24");
            ddlPickDateRangeDay2.Items.Add("25");
            ddlPickDateRangeDay2.Items.Add("26");
            ddlPickDateRangeDay2.Items.Add("27");
            ddlPickDateRangeDay2.Items.Add("28");
            ddlPickDateRangeDay2.Items.Add("29");
            ddlPickDateRangeDay2.Items.Add("30");
            ddlPickDateRangeDay2.Items.Add("31");
            ddlPickDateRangeDay2.Text = "Day";
        }


        protected void Populate_YearList()
        {
            //int  intYear = 0;

         
            //Year list can be changed by changing the lower and upper 
            //limits of the For statement    
            //ddlCalYear.Items.Add("Year");
            ddlPickDateRangeYear1.Items.Add("Year");
            ddlPickDateRangeYear2.Items.Add("Year");
            for (int intYear = (DateTime.Now.Year - 20); intYear <= (DateTime.Now.Year + 20); intYear++)
            {
                //ddlCalYear.Items.Add(intYear.ToString());
                ddlPickDateRangeYear1.Items.Add(intYear.ToString());
                ddlPickDateRangeYear2.Items.Add(intYear.ToString());
                //ddlCalYear.Items.Add(intYear);    
                //Next
            }
            //ddlCalYear.Text = "Year";
            ddlPickDateRangeYear1.Text = "Year";
            ddlPickDateRangeYear2.Text = "Year";

            //Make the current year selected item in the list
//            ddlCalYear.Items.FindByValue(DateTime.Now.Year).Selected = True;
        }

 




        protected void gvAttendanceResults_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("ViewStudentAttendance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&StudentLastName=&StudentFirstName=");
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            gvAttendanceResults.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            ExcelExport.ExportGridView(gvAttendanceResults, Response);
        }

        protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStudentName();
            //PopulateStudentNameFromAcademy();
        }

        protected void ddlCalMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCalDay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSectionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStudentName();
            
            PopulateProgramSeason();
            ddlProgramSeason.Enabled = true;

            PopulateTeamName();
            ddlTeamName.Enabled = true;

            ddlAttended.Enabled = true;
        }

        protected void ddlTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStudentName();
        }

        protected void ddlProgramSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStudentName();

            //If athletics, utilize the Section DropDownList...RCM..
            if ((ddlProgram.Text.Trim() == "Outreach Basketball") || (ddlProgram.Text.Trim() == "BasketballTEAMS") || (ddlProgram.Text.Trim() == "MS Basketball League") || (ddlProgram.Text.Trim() == "HS Basketball League") || (ddlProgram.Text.Trim() == "Baseball") || (ddlProgram.Text.Trim() == "SoccerIntraMurals") || (ddlProgram.Text.Trim() == "SoccerTEAMS") || (ddlProgram.Text.Trim() == "3on3 Basketball") || (ddlProgram.Text.Trim() == "Bible Study") || (ddlProgram.Text.Trim() == "Special Events") || (ddlProgram.Text.Trim() == "MondayNights"))
            {
                PopulateSectionName();
                ddlSectionName.Enabled = true;
            }
            else if (ddlProgram.Text.Trim() == "PerformingArtsAcademy")
            {
                PopulateClassName();
                ddlClassName.Enabled = true;
            }
            else if ((ddlProgram.Text.Trim() == "Childrens Choir") || (ddlProgram.Text == "MSHS Choir") || (ddlProgram.Text == "Shakes") || (ddlProgram.Text == "Singers"))
            {
                ddlSectionName.Enabled = false;
            }
            else
            {
                ddlClassName.Enabled = false;
            }
        }

        protected void ddlAttended_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeDay1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}