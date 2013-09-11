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
    public partial class KRA : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public SqlConnection con3 = new SqlConnection(connectionString);
        public static string Department = "";
        SqlDataReader reader = null;
         
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
                    ddlReports.Items.Add("Please select a report");
                    ddlReports.Items.Add("Student's Per Program");
                    ddlReports.Items.Add("Student's Per Program/ProgramSeason");
                    ddlReports.Items.Add("Student's Per Program/Section");
                    ddlReports.Items.Add("Student's Per Program/Section/TeamName");
                    ddlReports.Items.Add("Volunteer's Per Program");
                    ddlReports.Items.Add("Volunteer's Per Program/ProgramSeason");
                    ddlReports.Items.Add("Volunteer's Per Program/Section");
                    ddlReports.Items.Add("Volunteer's Per Program/Section/TeamName");
                    ddlReports.Items.Add("Volunteers Meals/Hours Per Program/Section");
                    ddlReports.Items.Add("Volunteers Meals/Hours Per Program");
                    ddlReports.Items.Add("Student/Gospel Meal Program/Section");
                    ddlReports.Items.Add("Student/Gospel Meal Program");
                    ddlReports.Items.Add("Student/Gospel Meal ProgramSeason");
                    ddlReports.Items.Add("Student/Gospel Meal Count");
                    ddlReports.Items.Add("Shakes View Student/Gospel Meal Program");
                    ddlReports.Items.Add("Distinct Student Total");
                    ddlReports.Text = "Please select a report";

                    ddlProgramSeason.Items.Add("Select a Program Year/Season");
                    ddlProgramSeason.Items.Add("Year-2012-2013");
                    //ddlProgramSeason.Items.Add("Year-2011-2012");
                    //ddlProgramSeason.Items.Add("Year-2010-2011");
                    //RetrieveProgramSeasonsAndAddToList();
                    ddlReports.Text = "Select a Program Year/Season";


                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!
                    Response.Redirect("ErrorAccess.aspx");
                }
            }
        }

        protected void RetrieveProgramSeasonsAndAddToList()
        {
            con.Open();

            try
            {
                string sql = "select spa.programseason "
                           + "from studentprogramattendance spa "
                           + "where ";

                SqlCommand cmd = new SqlCommand(sql, con);
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
            catch (Exception lkjlkjaabbeecc)
            {

            }
            finally
            {
                con.Close();
            }
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

        protected void cmbGospelMealsReport_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "select (SUM(numberofmeals)+ SUM(MiscellaneousMeals)) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#StudentsRSVPGospel', SUM(bibles) as 'Bibles' "
                    + "from EventAttendanceTracker "
                    + " "
                    + "select (SUM(numberofmeals)+ SUM(MiscellaneousMeals)) as 'IndVolunteerMeals' "
                    + "from VolunteerEventAttendanceTracker "
                    + " "
                    + "select SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#StudentsRSVPGospel', SUM(bibles) as 'Bibles' "
                    + "from EventAttendanceTracker ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "EventAttendanceTracker");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
                con.Close();
                //cmbExcelExport.Enabled = true;
                //cmbExcelExport.Visible = true;
                //cmbStudentPage.Enabled = false;
            }
            catch (Exception lkjlkjaa)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbVolunteerMealCount_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "select (SUM(numberofmeals)+ SUM(MiscellaneousMeals)) as 'IndVolunteerMeals' "
                    + "from VolunteerEventAttendanceTracker ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerEventAttendanceTracker");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
                con.Close();
                //cmbExcelExport.Enabled = true;
                //cmbExcelExport.Visible = true;
                //cmbStudentPage.Enabled = false;
            }
            catch (Exception lkjlkjaa)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbStudentCount_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "Select spa.Program, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Students' "
                    + "FROM StudentProgramAttendance spa "
                    + "WHERE spa.Day > '2012-08-01' "
                    + "AND spa.Attended = 1 "
                    + "GROUP BY spa.Program, spa.ProgramSeason "
                    + "ORDER BY spa.Program, spa.ProgramSeason ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
                con.Close();
                //cmbExcelExport.Enabled = true;
                //cmbExcelExport.Visible = true;
                //cmbStudentPage.Enabled = false;
            }
            catch (Exception lkjlkjaa)
            {

            }
            finally
            {
                con.Close();
            }
        }


        protected void ExecuteQuery(string Report)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";


                if (Report == "Student/Gospel Meal Program/Section")
                {
                    sql = "Select spa.Program, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Students' "
                        + "FROM StudentProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.ProgramSeason ";
                }
                else if (Report == "Student/Gospel Meal Program")
                {

                }
                else if (Report == "Student/Gospel Meal Count")
                {

                }
                else if (Report == "Volunteer Count Program")
                {

                }
                else if (Report == "Volunteer Count Program/Section")
                {

                }
                else if (Report == "Student Count Program")
                {

                }
                else if (Report == "Student Count Program/Section")
                {

                }
                else if (Report == "Distinct Student Total")
                {

                }
                else if (Report == "")
                {

                }
                
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
                con.Close();
                //cmbExcelExport.Enabled = true;
                //cmbExcelExport.Visible = true;
                //cmbStudentPage.Enabled = false;
            }
            catch (Exception lkjlkjaa)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbDistinctStudents_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "Select COUNT(DISTINCT spa.LastName + spa.FirstName) as 'Tot # Distinct Students' "
                    + "FROM StudentProgramAttendance spa "
                    + "WHERE spa.Day > '2012-08-01' "
                    + "AND spa.Attended = 1 ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
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

        protected void chtGeneral_Load(object sender, EventArgs e)
        {
            //SET UP THE DATA TO PLOT   
            double[] yVal = { 80, 20 };
            //double[] yVal2 = ds.Tables.AddRange();
            string[] xName = { "Athletics", "PAA" };

            //double[] yVal2 = { 80, 20 };
            //string[] xName2 = { "Athletics", "PAA" };

            //CREATE THE CHART 
            // Don't need to create the chart because it's a control! 

            //BIND THE DATA TO THE CHART 
            //chtGeneral.Series.Add(new Series());
            chtGeneral.Series[0].Points.DataBindXY(xName, yVal);
            //chtGeneral.Series[1].Points.DataBindXY(xName2, yVal2);

            //SET THE CHART TYPE TO BE PIE 
            chtGeneral.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
          //  chtGeneral.Series[0]["PieLabelStyle"] = "Outside";
          //  chtGeneral.Series[0]["PieStartAngle"] = "-90";

          //  chtGeneral.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Bar;

            //chtGeneral.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Candlestick;

         //   chtGeneral.Series[0]["BarLabelStyle"] = "Outside";
         //   chtGeneral.Series[0]["BarStartAngle"] = "-90";

            //SET THE COLOR PALETTE FOR THE CHART TO BE A PRESET OF NONE  
            //DEFINE OUR OWN COLOR PALETTE FOR THE CHART  
            chtGeneral.Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.None;
            //chtGeneral.PaletteCustomColors = new Color[] { Color.Blue, Color.Red };
            chtGeneral.PaletteCustomColors = new Color[] { Color.Orange, Color.Yellow };
            //255, 221, 32

            //SET THE IMAGE OUTPUT TYPE TO BE JPEG 
            chtGeneral.ImageType = System.Web.UI.DataVisualization.Charting.ChartImageType.Jpeg;

            //ADD A PLACE HOLDER CHART AREA TO THE CHART 
            //SET THE CHART AREA TO BE 3D 
            //chtGeneral.ChartAreas.Add(new );
            chtGeneral.ChartAreas[0].Area3DStyle.Enable3D = true;

            //ADD A PLACE HOLDER LEGEND TO THE CHART 
            //DISABLE THE LEGEND 
            //chtGeneral.Legends.Add(new Legend());
            //chtGeneral.Legends[0].Enabled = false; 
        }

        protected void cmbStudentProgramSection_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "Select spa.Program, spa.Section, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Students' "
                    + "FROM StudentProgramAttendance spa " 
                    + "WHERE spa.Day > '2012-08-01' "
                    + "AND spa.Attended = 1 "
                    + "GROUP BY spa.Program, spa.Section, spa.ProgramSeason "
                    + "ORDER BY spa.Program, spa.Section, spa.ProgramSeason ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
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

        protected void cmbChart_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "Select COUNT(DISTINCT spa.LastName + spa.FirstName) as 'Tot # Distinct Students' "
                    + "FROM StudentProgramAttendance spa "
                    + "WHERE spa.Day > '2012-08-01' ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");

                //SET UP THE DATA TO PLOT   
                double[] yVal = { 80, 20 };
                //double[] yVal2 = ds.Tables.AddRange();
                string[] xName = { "Athletics", "PAA" };

                //double[] yVal2 = { 80, 20 };
                //string[] xName2 = { "Athletics", "PAA" };

                //CREATE THE CHART 
                // Don't need to create the chart because it's a control! 

                //BIND THE DATA TO THE CHART 
                //chtGeneral.Series.Add(new Series());
                chtGeneral.Series[0].Points.DataBindXY(xName, yVal);
                //chtGeneral.Series[1].Points.DataBindXY(xName2, yVal2);

                //SET THE CHART TYPE TO BE PIE 
                chtGeneral.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
                //  chtGeneral.Series[0]["PieLabelStyle"] = "Outside";
                //  chtGeneral.Series[0]["PieStartAngle"] = "-90";

                //  chtGeneral.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Bar;

                //chtGeneral.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Candlestick;

                //   chtGeneral.Series[0]["BarLabelStyle"] = "Outside";
                //   chtGeneral.Series[0]["BarStartAngle"] = "-90";

                //SET THE COLOR PALETTE FOR THE CHART TO BE A PRESET OF NONE  
                //DEFINE OUR OWN COLOR PALETTE FOR THE CHART  
                chtGeneral.Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.None;
                //chtGeneral.PaletteCustomColors = new Color[] { Color.Blue, Color.Red };
                chtGeneral.PaletteCustomColors = new Color[] { Color.Orange, Color.Yellow };
                //255, 221, 32

                //SET THE IMAGE OUTPUT TYPE TO BE JPEG 
                chtGeneral.ImageType = System.Web.UI.DataVisualization.Charting.ChartImageType.Jpeg;

                //ADD A PLACE HOLDER CHART AREA TO THE CHART 
                //SET THE CHART AREA TO BE 3D 
                //chtGeneral.ChartAreas.Add(new );
                chtGeneral.ChartAreas[0].Area3DStyle.Enable3D = true;

                //ADD A PLACE HOLDER LEGEND TO THE CHART 
                //DISABLE THE LEGEND 
                //chtGeneral.Legends.Add(new Legend());
                //chtGeneral.Legends[0].Enabled = false; 

                
                chtGeneral.DataSource = ds.Tables[0];
                chtGeneral.DataBind();
                chtGeneral.Visible = true;
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

        protected void cmbVolunteerBreakdown_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "Select spa.Program, spa.Section, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Volunteers' "
                    + "FROM VolunteerProgramAttendance spa "
                    + "WHERE spa.Day > '2012-08-01' "
                    + "AND spa.Attended = 1 "
                    + "GROUP BY spa.Program, spa.Section, spa.ProgramSeason "
                    + "ORDER BY spa.Program, spa.Section, spa.ProgramSeason ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerProgramAttendance");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
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

        protected void cmbVolunteerBasics_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "Select spa.Program, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Volunteers' "
                    + "FROM VolunteerProgramAttendance spa "
                    + "WHERE spa.Day > '2012-08-01' "
                    + "AND spa.Attended = 1 "
                    + "GROUP BY spa.Program, spa.ProgramSeason "
                    + "ORDER BY spa.Program, spa.ProgramSeason ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerProgramAttendance");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
                con.Close();
                //cmbExcelExport.Enabled = true;
                //cmbExcelExport.Visible = true;
                //cmbStudentPage.Enabled = false;
            }
            catch (Exception lkjlkjaa)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbStudentGospelProgramSection_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "select spa.Program, spa.Section, spa.ProgramSeason, SUM(numberofmeals + MiscellaneousMeals) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#RSVPGospel', SUM(bibles) as 'Bibles', SUM(hours) as 'Hours' "
                    + "FROM EventAttendanceTracker spa "
                    + "WHERE spa.Day > '2012-08-01' "
                    + "GROUP BY spa.Program, spa.Section, spa.ProgramSeason "
                    + "ORDER BY spa.Program, spa.Section, spa.ProgramSeason ";

                //sql = "select spa.Program, spa.Section, COUNT(distinct spaa.LastName + spaa.FirstName) as '# of Students', SUM(numberofmeals + MiscellaneousMeals) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#RSVPGospel', SUM(bibles) as 'Bibles', SUM(hours) as 'Hours' "
                //    + "FROM EventAttendanceTracker spa "
                //    + "LEFT OUTER JOIN StudentProgramAttendance spaa "
                //    + "ON (spa.Program = spaa.Program) "
                //    + "WHERE spa.Day > '2012-08-01' "
                //    + "GROUP BY spa.Program, spa.Section "
                //    + "ORDER BY spa.Program, spa.Section ";                

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
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

        protected void cmbStudentGospelMealProgram_Click(object sender, EventArgs e)
        {
            con.Open();

            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;

            try
            {
                string sql = "";
                sql = "select spa.Program, SUM(numberofmeals + MiscellaneousMeals) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#StudentsRSVPGospel', SUM(bibles) as 'Bibles', SUM(hours) as 'Hours'  "
                    + "FROM EventAttendanceTracker spa "
                    + "WHERE spa.Day > '2012-08-01' "
                    + "GROUP BY spa.Program "
                    + "ORDER BY spa.Program ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "StudentProgramAttendance");
                gvGeneralReports.DataSource = ds.Tables[0];
                gvGeneralReports.DataBind();
                gvGeneralReports.Visible = true;
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

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Export the contents of the gridview to an Excel object for use...RCM..
            if ((gvGeneralReports.Rows.Count != 0))
            {
                int i = 0;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.Text = String.Format(ddlReports.Text + " " + ddlProgramSeason.Text, i);
                row.Cells.Add(cell);
                gvGeneralReports.Controls[0].Controls.AddAt(i, row);
                                
                gvGeneralReports.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvGeneralReports, Response, ddlReports.Text + " " + ddlProgramSeason.Text);
            }
            //else if (gvAttendanceList.Rows.Count != 0)
            //{
            //    ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            //    ExcelExport.ExportGridView(gvAttendanceList, Response);
            //}
        }

        //Ryan C Manners...10/31/12..
        //This is key in preventing gridviews to wrap data..
        protected void gvGeneralReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }


        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvGeneralReports.DataSource = null;
            gvGeneralReports.DataBind();
            gvGeneralReports.Visible = false;
            lblReportLabel.Visible = false;
            lblReportLabel.Text = "";

            string sql = "";
            if (ddlProgramSeason.Text == "Select a Program Year/Season")
            {
                //No Program Year has been selected.  Show message to alert the user..
            
            }
            else
            {
                if (ddlReports.Text == "Student/Gospel Meal Program/Section")
                {
                    sql = "select spa.Program, spa.Section, spa.ProgramSeason, SUM(numberofmeals + MiscellaneousMeals) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#RSVPGospel', SUM(bibles) as 'Bibles', SUM(hours) as 'Hours' "
                        + "FROM EventAttendanceTracker spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        //+ "WHERE spa.ProgramSeason > '" + ddlProgramSeason.Text + "' "
                        + "GROUP BY spa.Program, spa.Section, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.Section, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Student/Gospel Meal Program")
                {
                    sql = "select spa.Program, SUM(numberofmeals + MiscellaneousMeals) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#StudentsRSVPGospel', SUM(bibles) as 'Bibles', SUM(hours) as 'Hours'  "
                        + "FROM EventAttendanceTracker spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        //+ "WHERE spa.ProgramSeason > '" + ddlProgramSeason.Text + "' "
                        + "GROUP BY spa.Program "
                        + "ORDER BY spa.Program ";
                }
                else if (ddlReports.Text == "Shakes View Student/Gospel Meal Program")
                {
                    sql = "select spa.Program, SUM(numberofmeals + MiscellaneousMeals) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#StudentsRSVPGospel', SUM(bibles) as 'Bibles', SUM(hours) as 'Hours'  "
                        + "FROM EventAttendanceTracker spa "
                        + "WHERE spa.Day > '2012-05-01' "
                        + "AND spa.Program = 'Shakes' "
                        + "GROUP BY spa.Program "
                        + "ORDER BY spa.Program ";
                }
                else if (ddlReports.Text == "Student/Gospel Meal ProgramSeason")
                {
                    sql = "select spa.Program, spa.ProgramSeason, SUM(numberofmeals + MiscellaneousMeals) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#StudentsRSVPGospel', SUM(bibles) as 'Bibles', SUM(hours) as 'Hours'  "
                        + "FROM EventAttendanceTracker spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        //+ "WHERE spa.ProgramSeason > '" + ddlProgramSeason.Text + "' "
                        + "GROUP BY spa.Program, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Student/Gospel Meal Count")
                {
                    sql = "select (SUM(numberofmeals)+ SUM(MiscellaneousMeals)) as 'IndStudentMeals',SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#StudentsRSVPGospel', SUM(bibles) as 'Bibles' "
                        + "from EventAttendanceTracker "
                        + " "
                        + "select (SUM(numberofmeals)+ SUM(MiscellaneousMeals)) as 'IndVolunteerMeals' "
                        + "from VolunteerEventAttendanceTracker "
                        + " "
                        + "select SUM(CONVERT(int,GospelPresented)) as 'GospelPresented', SUM(RSVPGospelCount) as '#StudentsRSVPGospel', SUM(bibles) as 'Bibles' "
                        + "from EventAttendanceTracker ";
                }
                else if (ddlReports.Text == "Volunteer's Per Program")
                {
                    sql = "Select spa.Program, COUNT(distinct spa.LastName + spa.FirstName) as '# of Volunteers' "
                        + "FROM VolunteerProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program "
                        + "ORDER BY spa.Program ";
                }
                else if (ddlReports.Text == "Volunteer's Per Program/ProgramSeason")
                {
                    sql = "Select spa.Program, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Volunteers' "
                        + "FROM VolunteerProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Volunteer's Per Program/Section")
                {
                    sql = "Select spa.Program, spa.Section, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Volunteers' "
                        + "FROM VolunteerProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program, spa.Section, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.Section, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Volunteers Meals/Hours Per Program/Section")
                {
                    sql = "select spa.Program, spa.Section, spa.ProgramSeason, SUM(numberofmeals + MiscellaneousMeals) as 'IndVolunteerMeals',SUM(hours) as 'Hours' "
                        + "FROM VolunteerEventAttendanceTracker spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "GROUP BY spa.Program, spa.Section, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.Section, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Volunteer's Per Program/Section/TeamName")
                {
                    sql = "Select spa.Program, spa.Section, spa.TeamName, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Volunteers' "
                        + "FROM VolunteerProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program, spa.Section, spa.TeamName, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.Section, spa.TeamName, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Volunteers Meals/Hours Per Program")
                {
                    sql = "select spa.Program, spa.ProgramSeason, SUM(numberofmeals + MiscellaneousMeals) as 'IndVolunteerMeals',SUM(hours) as 'Hours' "
                        + "FROM VolunteerEventAttendanceTracker spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "GROUP BY spa.Program, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Student's Per Program")
                {
                    sql = "Select spa.Program, COUNT(distinct spa.LastName + spa.FirstName) as '# of Students' "
                        + "FROM StudentProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program "
                        + "ORDER BY spa.Program ";
                }
                else if (ddlReports.Text == "Student's Per Program/ProgramSeason")
                {
                    sql = "Select spa.Program, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Students' "
                        + "FROM StudentProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Student's Per Program/Section")
                {
                    sql = "Select spa.Program, spa.Section, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Students' "
                        + "FROM StudentProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program, spa.Section, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.Section, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Student's Per Program/Section/TeamName")
                {
                    sql = "Select spa.Program, spa.Section, spa.TeamName, spa.ProgramSeason, COUNT(distinct spa.LastName + spa.FirstName) as '# of Students' "
                        + "FROM StudentProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 "
                        + "GROUP BY spa.Program, spa.Section, spa.TeamName, spa.ProgramSeason "
                        + "ORDER BY spa.Program, spa.Section, spa.TeamName, spa.ProgramSeason ";
                }
                else if (ddlReports.Text == "Distinct Student Total")
                {
                    sql = "Select COUNT(DISTINCT spa.LastName + spa.FirstName) as 'Tot # Distinct Students' "
                        + "FROM StudentProgramAttendance spa "
                        + "WHERE spa.Day > '2012-08-01' "
                        + "AND spa.Attended = 1 ";
                }

                try
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataSet ds = new DataSet();
                        
                    if ((ddlReports.Text.Contains("Volunteer")) && (ddlReports.Text.Contains("Program")))
                    {
                        da.Fill(ds, "VolunteerProgramAttendance");
                    }
                    else if ((ddlReports.Text.Contains("Volunteer")) && (ddlReports.Text.Contains("Meals")))
                    {
                        da.Fill(ds, "VolunteerEventAttendanceTracker");
                    }
                    else
                    {
                        da.Fill(ds, "StudentProgramAttendance");
                    }

                    lblReportLabel.Text = ddlReports.Text + " " + ddlProgramSeason.Text;
                    lblReportLabel.Visible = true;

                    gvGeneralReports.DataSource = ds.Tables[0];
                    gvGeneralReports.DataBind();
                    gvGeneralReports.Visible = true;
                }
                catch (Exception lkjaabb)
                {

                }
                finally
                {
                    con.Close();
                }
            }
        }

        protected void ddlProgramSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlReports.Enabled = true;
        }

        //protected void openDoc(ByVal sender As Object, ByVal e As CommandEventArgs) 
        //{
            //WordApp As New Word.ApplicationClass() 
            //missing As Object = System.Reflection.Missing.Value 
            //fileName As Object = "C:\\template.dot" 
            //newTemplate As Object = False 
            //docType As Object = 0 
            //isVisible As Object = True 
            //aDoc As Word.Document = WordApp.Documents.Add(fileName, newTemplate, docType, isVisible) 
 
            //WordApp.Visible = True 
            //aDoc.Activate() 
        //}
    }
}