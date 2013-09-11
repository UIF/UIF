using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;

namespace UIF.PerformingArts
{
    public partial class SingleStudentAttendance : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public event GridViewEditEventHandler RowEditing;
        public Boolean flag = false;
        public int irowNum = 0;
        public static string Department = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["security"] == "Good")
            {
                if (!Page.IsPostBack)
                {
                    //Populate the Department Query string...RCM..6/28/11
                    Department = Request.QueryString["Dept"];

                    ddlProgram.Items.Add("Childrens Choir");
                    ddlProgram.Items.Add("MSHS Choir");
                    ddlProgram.Items.Add("PerformingArtsAcademy");
                    //cmbProgram.Enabled = false;
                    //cmbProgram.Visible = false;
                }
            }
            else
            {
                //Ryan C Manners..1/5/11
                //Do NOT ALLOW ACCESS TO THE PAGE!
                Response.Redirect("ErrorAccess.aspx");
            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlClassSelection.Text = " ";

            //bind2();

            //lblInformation.Text = ddlClassSelection2.Text + " students, during the 6:30-8:00pm Class";

            //cmbCommittAttendance.Enabled = false;
            //cmbCommittAttendance.Visible = true;
            //cmbCommittAttendance.Text = "Please select a date before committing the data.";
            //lblInformation.Visible = true;
            //lblInformation.Enabled = true;
            //lblSetAttendance.Visible = true;

            //--------------------------------

            SqlDataReader reader = null;

            try
            {
                con.Open();//Opens the db connection.
                string sql_LoadGrid = "";

                if (ddlProgram.Text == "MSHS Choir")
                {
                    sql_LoadGrid = "select Lastname as 'StudentLastName', Firstname as 'StudentFirstName',  '' as 'Attended' "
                                 + "FROM PerformingArtsAcademyStudents "
                                 + "WHERE mshschoir = 1 "
                                 + "order by studentlastname, studentfirstname ";

                    //Perform database lookup based on the chosen child..RCM..
                    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds, "PerformingArtsAcademyStudents");
                    //gvStudentList.DataSource = ds.Tables[0];
                    //gvStudentList.DataBind();
                    //con.Close();

                    //cmbCommittAttendance.Visible = true;
                    //cmbCommittAttendance.Enabled = false;
                    //cmbCommittAttendance.Text = "Please select a date before committing the data.";
                    //lblInformation.Visible = true;
                    //lblInformation.Enabled = true;
                    //lblSetAttendance.Visible = true;
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    sql_LoadGrid = "select Lastname as 'StudentLastName', Firstname as 'StudentFirstName', '' as 'Attended'"
                                        + "FROM PerformingArtsAcademyStudents "
                                        + "WHERE childrenschoir = 1 "
                                        + "order by studentlastname, studentfirstname ";

                    //Perform database lookup based on the chosen child..RCM..
                    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                    //cmd.Connection = con;
                    //gvStudentList.DataSource = cmd.ExecuteReader();
                    //gvStudentList.DataBind();

                    //cmbCommittAttendance.Visible = true;
                    //cmbCommittAttendance.Enabled = false;
                    //cmbCommittAttendance.Text = "Please select a date before committing the data.";
                    //lblInformation.Visible = true;
                    //lblInformation.Enabled = true;
                    //lblSetAttendance.Visible = true;
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    //ddlProgram.Enabled = false;
                    //ddlProgram.Visible = false;
                    //Label2.Visible = false;

                    //string sql = "select ClassName "
                    //            + "from PerformingArtsAcademyAvailableClasses "
                    //            + "where MeetTime = '4:30-6:00 Class' "
                    //            + "and MeetDay = 'Thursday' "
                    //            + "group by ClassName "
                    //            + "order by ClassName ";
                    //SqlCommand cmd = new SqlCommand(sql, con);
                    //cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    //SqlDataAdapter custDA = new SqlDataAdapter();
                    //custDA.SelectCommand = cmd;
                    //DataSet custDS = new DataSet();
                    //custDA.Fill(custDS, "PerformingArtsAcademyAvailableClasses");

                    //ddlClassSelection.Items.Add(" ");
                    ////Iterate over setup records and call method to do the work on each one...RCM..
                    //foreach (DataRow myDataRowPO in custDS.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                    //{
                    //    //Adding options to the drop downs for a new entry.
                    //    ddlClassSelection.Items.Add(myDataRowPO[0].ToString());
                    //}
                    //custDS.Clear();


                    //string sql2 = "select ClassName "
                    //            + "from PerformingArtsAcademyAvailableClasses "
                    //            + "where MeetTime = '6:30-8:00 Class' "
                    //            + "and MeetDay = 'Thursday' "
                    //            + "group by ClassName "
                    //            + "order by ClassName ";
                    //SqlCommand cmd2 = new SqlCommand(sql2, con);
                    //cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    //SqlDataAdapter custDA2 = new SqlDataAdapter();
                    //custDA2.SelectCommand = cmd2;
                    //DataSet custDS2 = new DataSet();
                    //custDA2.Fill(custDS2, "PerformingArtsAcademyAvailableClasses");

                    //ddlClassSelection2.Items.Add(" ");
                    ////Iterate over setup records and call method to do the work on each one...RCM..
                    //foreach (DataRow myDataRowPO in custDS2.Tables["PerformingArtsAcademyAvailableClasses"].Rows)
                    //{
                    //    //Adding options to the drop downs for a new entry.
                    //    ddlClassSelection2.Items.Add(myDataRowPO[0].ToString());
                    //}
                    //custDS2.Clear();

                    ////Configuring the controls correctly for viewing...RCM..11/3/10.
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

                //cmbEnterAttendance.Visible = false;
                //cmbEnterAttendance.Enabled = false;
                //calCalender2.Enabled = true;
                //calCalender2.Visible = true;
                //calCalender2.ShowTitle = true;
                ////lblInformation.Visible = true;
                ////lblInformation.Enabled = true;
                //lblPleaseChoose.Visible = true;
                //lblPleaseChoose.Enabled = true;
                ////calCalender2.VisibleDate = DateTime.Now.AddMonths(2);
                //calCalender2.ShowNextPrevMonth = true;
                //calCalender2.ShowTitle = true;
                //calCalender2.
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }

        public void bind()
        {
            con.Open();

            string sql_LoadGrid = "";
            //sql_LoadGrid = "Select studentlastname, studentfirstname, '' as 'Attended'  "
            //             + "from PerformingArtsAcademy "
            //             + "where meettime = '4:30-6:00 Class' "
            //             + "and classname = '" + ddlClassSelection.Text + "' "
            //             + "order by studentlastname, studentfirstname ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //DataSet ds = new DataSet();
            //da.Fill(ds, "PerformingArtsAcademyClassEnrollment");
            //gvStudentList.DataSource = ds.Tables[0];
            //gvStudentList.DataBind();
            //con.Close();
        }  
    }
}