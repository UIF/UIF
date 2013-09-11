using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using UrbanImpactCommon;


namespace UIF.PerformingArts
{
    public partial class HistoricalReports : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public static string Department = "";
                
        protected void Page_Load(object sender, EventArgs e)
        {
            //calCalender.ShowNextPrevMonth = true;
            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];

                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);

                Startup();

                if (Request.QueryString["security"] == "Good")
                {
                    con.Open();

                    string selectcolumnnames = "select name "
                        + "from sys.columns "
                        + "where object_id = (SELECT OBJECT_ID('UIF_PerformingArts.dbo.StudentInformation')) "
                        + "UNION "
                        + "select name "
                        + "from sys.columns "
                        + "where object_id = (SELECT OBJECT_ID('UIF_PerformingArts.dbo.ProgramsList')) ";

                    SqlDataReader reader = null;
                    SqlCommand cmd = new SqlCommand(selectcolumnnames);

                    cmd.Connection = con;
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {	//Retrieve the first record only
                        ddlChooseField.Items.Add("Choose Field");
                        ddlChooseField2.Items.Add("Choose Field");
                        ddlChooseField3.Items.Add("Choose Field");
                        ddlChooseField4.Items.Add("Choose Field");
                        ddlChooseField5.Items.Add("Choose Field");

                        //Add one field to the first dropdown ONLY..  to pull all programs.
                        ddlChooseField.Items.Add("All Programs");
                        ddlChooseField.Items.Add("AthleticsDept");
                        ddlChooseField.Items.Add("PerformingArtsDept");
                        ddlChooseField.Items.Add("EducationDept");
                        do
                        {
                            ddlChooseField.Items.Add(reader.GetString(0));
                            ddlChooseField2.Items.Add(reader.GetString(0));
                            ddlChooseField3.Items.Add(reader.GetString(0));
                            ddlChooseField4.Items.Add(reader.GetString(0));
                            ddlChooseField5.Items.Add(reader.GetString(0));
                        } while (reader.Read());
                        ddlChooseField.Text = "Choose Field";
                        ddlChooseField2.Text = "Choose Field";
                        ddlChooseField3.Text = "Choose Field";
                        ddlChooseField4.Text = "Choose Field";
                        ddlChooseField5.Text = "Choose Field";
                    }

                    //To be used for Grade, Age, and all integer fields..RCM..
                    ddlChooseOperator.Items.Add("Choose Operator");
                    ddlChooseOperator.Items.Add("contains");
                    ddlChooseOperator.Items.Add("NOT contains");
                    ddlChooseOperator.Items.Add("equals");
                    ddlChooseOperator.Items.Add("NOT equals");
                    ddlChooseOperator.Items.Add(">");
                    ddlChooseOperator.Items.Add("<");
                    ddlChooseOperator.Items.Add(">=");
                    ddlChooseOperator.Items.Add("<=");
                    ddlChooseOperator.Text = "Choose Operator";
                    ddlChooseOperator2.Items.Add("Choose Operator");
                    ddlChooseOperator2.Items.Add("contains");
                    ddlChooseOperator2.Items.Add("NOT contains");
                    ddlChooseOperator2.Items.Add("equals");
                    ddlChooseOperator2.Items.Add("NOT equals");
                    ddlChooseOperator2.Items.Add(">");
                    ddlChooseOperator2.Items.Add("<");
                    ddlChooseOperator2.Items.Add(">=");
                    ddlChooseOperator2.Items.Add("<=");
                    ddlChooseOperator2.Text = "Choose Operator";
                    ddlChooseOperator3.Items.Add("Choose Operator");
                    ddlChooseOperator3.Items.Add("contains");
                    ddlChooseOperator3.Items.Add("NOT contains");
                    ddlChooseOperator3.Items.Add("equals");
                    ddlChooseOperator3.Items.Add("NOT equals");
                    ddlChooseOperator3.Items.Add(">");
                    ddlChooseOperator3.Items.Add("<");
                    ddlChooseOperator3.Items.Add(">=");
                    ddlChooseOperator3.Items.Add("<=");
                    ddlChooseOperator3.Text = "Choose Operator";
                    ddlChooseOperator4.Items.Add("Choose Operator");
                    ddlChooseOperator4.Items.Add("contains");
                    ddlChooseOperator4.Items.Add("NOT contains");
                    ddlChooseOperator4.Items.Add("equals");
                    ddlChooseOperator4.Items.Add("NOT equals");
                    ddlChooseOperator4.Items.Add(">");
                    ddlChooseOperator4.Items.Add("<");
                    ddlChooseOperator4.Items.Add(">=");
                    ddlChooseOperator4.Items.Add("<=");
                    ddlChooseOperator4.Text = "Choose Operator";
                    ddlChooseOperator5.Items.Add("Choose Operator");
                    ddlChooseOperator5.Items.Add("contains");
                    ddlChooseOperator5.Items.Add("NOT contains");
                    ddlChooseOperator5.Items.Add("equals");
                    ddlChooseOperator5.Items.Add("NOT equals");
                    ddlChooseOperator5.Items.Add(">");
                    ddlChooseOperator5.Items.Add("<");
                    ddlChooseOperator5.Items.Add(">=");
                    ddlChooseOperator5.Items.Add("<=");
                    ddlChooseOperator5.Text = "Choose Operator";

                    //To be used for Boolean fields..RCM.
                    ddlOperatorBoolean.Items.Add("Choose Operator");
                    ddlOperatorBoolean.Items.Add("equals");
                    ddlOperatorBoolean.Text = "Choose Operator";
                    ddlOperatorBoolean2.Items.Add("Choose Operator");
                    ddlOperatorBoolean2.Items.Add("equals");
                    ddlOperatorBoolean2.Text = "Choose Operator";
                    ddlOperatorBoolean3.Items.Add("Choose Operator");
                    ddlOperatorBoolean3.Items.Add("equals");
                    ddlOperatorBoolean3.Text = "Choose Operator";
                    ddlOperatorBoolean4.Items.Add("Choose Operator");
                    ddlOperatorBoolean4.Items.Add("equals");
                    ddlOperatorBoolean4.Text = "Choose Operator";
                    ddlOperatorBoolean5.Items.Add("Choose Operator");
                    ddlOperatorBoolean5.Items.Add("equals");
                    ddlOperatorBoolean5.Text = "Choose Operator";

                    //To be used for Character fields...RCM..
                    ddlOperatorCharacter.Items.Add("Choose Operator");
                    ddlOperatorCharacter.Items.Add("equals");
                    ddlOperatorCharacter.Items.Add("NOT equals");
                    ddlOperatorCharacter.Items.Add("NOT contains");
                    ddlOperatorCharacter.Items.Add("contains");
                    ddlOperatorCharacter.Text = "Choose Operator";
                    ddlOperatorCharacter2.Items.Add("Choose Operator");
                    ddlOperatorCharacter2.Items.Add("equals");
                    ddlOperatorCharacter2.Items.Add("contains");
                    ddlOperatorCharacter2.Items.Add("NOT equals");
                    ddlOperatorCharacter2.Items.Add("NOT contains");
                    ddlOperatorCharacter2.Text = "Choose Operator";
                    ddlOperatorCharacter3.Items.Add("Choose Operator");
                    ddlOperatorCharacter3.Items.Add("equals");
                    ddlOperatorCharacter3.Items.Add("contains");
                    ddlOperatorCharacter3.Items.Add("NOT equals");
                    ddlOperatorCharacter3.Items.Add("NOT contains");
                    ddlOperatorCharacter3.Text = "Choose Operator";
                    ddlOperatorCharacter4.Items.Add("Choose Operator");
                    ddlOperatorCharacter4.Items.Add("equals");
                    ddlOperatorCharacter4.Items.Add("contains");
                    ddlOperatorCharacter4.Items.Add("NOT equals");
                    ddlOperatorCharacter4.Items.Add("NOT contains");
                    ddlOperatorCharacter4.Text = "Choose Operator";
                    ddlOperatorCharacter5.Items.Add("Choose Operator");
                    ddlOperatorCharacter5.Items.Add("equals");
                    ddlOperatorCharacter5.Items.Add("contains");
                    ddlOperatorCharacter5.Items.Add("NOT equals");
                    ddlOperatorCharacter5.Items.Add("NOT contains");
                    ddlOperatorCharacter5.Text = "Choose Operator";

                    StartAdvancedSearch();
                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!
                    Response.Redirect("ErrorAccess.aspx");
                }
            }
        }

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendanceOptions.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void gvAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void Startup()
        {

                        
            //Starting out with the AdvancedSearch invisible..RCM.
            lblFindRecords.Visible = false;
            cmdStudent.Visible = false;
            ddlChooseField.Visible = false;
            ddlChooseField2.Visible = false;
            ddlChooseField3.Visible = false;
            ddlChooseField4.Visible = false;
            ddlChooseField5.Visible = false;
            ddlChooseOperator.Visible = false;
            ddlChooseOperator2.Visible = false;
            ddlChooseOperator3.Visible = false;
            ddlChooseOperator4.Visible = false;
            ddlChooseOperator5.Visible = false;
            txbSearchValue.Visible = false;
            txbSearchValue2.Visible = false;
            txbSearchValue3.Visible = false;
            txbSearchValue4.Visible = false;
            txbSearchValue5.Visible = false;
            rblNumber1.Visible = false;
            rblNumber2.Visible = false;
            rblNumber3.Visible = false;
            rblNumber4.Visible = false;

            ddlChooseField.Items.Add("CoreKid");
            ddlChooseField2.Items.Add("CoreKid");
            ddlChooseField3.Items.Add("CoreKid");
            ddlChooseField4.Items.Add("CoreKid");
            ddlChooseField5.Items.Add("CoreKid");
        }

        protected void StartAdvancedSearch()
        {
            cmbExcelExport.Enabled = false;

            //lbCreateCustomView.Visible = true;
            cmbCreateCustomView.Visible = true;
            //cmbCustomView.Visible = true;

            //Grades dropdown list...RCM..
            ddlGrades.Items.Add("Select a Grade");
            ddlGrades.Items.Add("PreK");
            ddlGrades.Items.Add("K");
            ddlGrades.Items.Add("GR");
            ddlGrades.Items.Add("SV");
            ddlGrades.Items.Add("1");
            ddlGrades.Items.Add("2");
            ddlGrades.Items.Add("3");
            ddlGrades.Items.Add("4");
            ddlGrades.Items.Add("5");
            ddlGrades.Items.Add("6");
            ddlGrades.Items.Add("7");
            ddlGrades.Items.Add("8");
            ddlGrades.Items.Add("9");
            ddlGrades.Items.Add("10");
            ddlGrades.Items.Add("11");
            ddlGrades.Items.Add("12");
            ddlGrades.Text = "Select a Grade";

            ddlGrades2.Items.Add("Select a Grade");
            ddlGrades2.Items.Add("PreK");
            ddlGrades2.Items.Add("K");
            ddlGrades2.Items.Add("GR");
            ddlGrades2.Items.Add("SV");
            ddlGrades2.Items.Add("1");
            ddlGrades2.Items.Add("2");
            ddlGrades2.Items.Add("3");
            ddlGrades2.Items.Add("4");
            ddlGrades2.Items.Add("5");
            ddlGrades2.Items.Add("6");
            ddlGrades2.Items.Add("7");
            ddlGrades2.Items.Add("8");
            ddlGrades2.Items.Add("9");
            ddlGrades2.Items.Add("10");
            ddlGrades2.Items.Add("11");
            ddlGrades2.Items.Add("12");
            ddlGrades2.Text = "Select a Grade";

            ddlGrades3.Items.Add("Select a Grade");
            ddlGrades3.Items.Add("PreK");
            ddlGrades3.Items.Add("K");
            ddlGrades3.Items.Add("GR");
            ddlGrades3.Items.Add("SV");
            ddlGrades3.Items.Add("1");
            ddlGrades3.Items.Add("2");
            ddlGrades3.Items.Add("3");
            ddlGrades3.Items.Add("4");
            ddlGrades3.Items.Add("5");
            ddlGrades3.Items.Add("6");
            ddlGrades3.Items.Add("7");
            ddlGrades3.Items.Add("8");
            ddlGrades3.Items.Add("9");
            ddlGrades3.Items.Add("10");
            ddlGrades3.Items.Add("11");
            ddlGrades3.Items.Add("12");
            ddlGrades3.Text = "Select a Grade";

            ddlGrades4.Items.Add("Select a Grade");
            ddlGrades4.Items.Add("PreK");
            ddlGrades4.Items.Add("K");
            ddlGrades4.Items.Add("GR");
            ddlGrades4.Items.Add("SV");
            ddlGrades4.Items.Add("1");
            ddlGrades4.Items.Add("2");
            ddlGrades4.Items.Add("3");
            ddlGrades4.Items.Add("4");
            ddlGrades4.Items.Add("5");
            ddlGrades4.Items.Add("6");
            ddlGrades4.Items.Add("7");
            ddlGrades4.Items.Add("8");
            ddlGrades4.Items.Add("9");
            ddlGrades4.Items.Add("10");
            ddlGrades4.Items.Add("11");
            ddlGrades4.Items.Add("12");
            ddlGrades4.Text = "Select a Grade";

            ddlGrades5.Items.Add("Select a Grade");
            ddlGrades5.Items.Add("PreK");
            ddlGrades5.Items.Add("K");
            ddlGrades5.Items.Add("GR");
            ddlGrades5.Items.Add("SV");
            ddlGrades5.Items.Add("1");
            ddlGrades5.Items.Add("2");
            ddlGrades5.Items.Add("3");
            ddlGrades5.Items.Add("4");
            ddlGrades5.Items.Add("5");
            ddlGrades5.Items.Add("6");
            ddlGrades5.Items.Add("7");
            ddlGrades5.Items.Add("8");
            ddlGrades5.Items.Add("9");
            ddlGrades5.Items.Add("10");
            ddlGrades5.Items.Add("11");
            ddlGrades5.Items.Add("12");
            ddlGrades5.Text = "Select a Grade";

            ddlSearchValueBool.Items.Add("Select a value");
            ddlSearchValueBool.Items.Add("1 (Active in Program)");
            ddlSearchValueBool.Items.Add("0 (Not Active)");
            ddlSearchValueBool.Text = "Select a value";

            ddlSearchValue2Bool.Items.Add("Select a value");
            ddlSearchValue2Bool.Items.Add("1 (Active in Program)");
            ddlSearchValue2Bool.Items.Add("0 (Not Active)");
            ddlSearchValue2Bool.Text = "Select a value";

            ddlSearchValue3Bool.Items.Add("Select a value");
            ddlSearchValue3Bool.Items.Add("1 (Active in Program)");
            ddlSearchValue3Bool.Items.Add("0 (Not Active)");
            ddlSearchValue3Bool.Text = "Select a value";

            ddlSearchValue4Bool.Items.Add("Select a value");
            ddlSearchValue4Bool.Items.Add("1 (Active in Program)");
            ddlSearchValue4Bool.Items.Add("0 (Not Active)");
            ddlSearchValue4Bool.Text = "Select a value";

            ddlSearchValue5Bool.Items.Add("Select a value");
            ddlSearchValue5Bool.Items.Add("1 (Active in Program)");
            ddlSearchValue5Bool.Items.Add("0 (Not Active)");
            ddlSearchValue5Bool.Text = "Select a value";

            lblViewChoices.Visible = true;
            ddlAdvancedSearchView.Items.Add("Select a View (Optional)");
            ddlAdvancedSearchView.Items.Add("Address Info");
            ddlAdvancedSearchView.Items.Add("Personal Info");
            //ddlAdvancedSearchView.Items.Add("Program Info");
            //ddlAdvancedSearchView.Items.Add("DiscipleshipMentor Info");
            ddlAdvancedSearchView.Items.Add("All Available Info");
            ddlAdvancedSearchView.Text = "Select a View (Optional)";
            ddlAdvancedSearchView.Enabled = true;
            ddlAdvancedSearchView.Visible = true;

            //Clear the gridview..RCM.
            gvAdvancedSearchResults.DataSource = null;
            gvAdvancedSearchResults.DataBind();

            //Make the general search disappear..RCM.
            cmbSearch.Visible = false;
            txbSearch.Visible = false;

            //Make the advanced search appear..RCM.
            lblFindRecords.Visible = true;
            cmdStudent.Visible = true;
            ddlChooseField.Visible = true;
            ddlChooseField2.Visible = true;
            ddlChooseField3.Visible = true;
            ddlChooseField4.Visible = true;
            ddlChooseField5.Visible = true;
            ddlChooseOperator.Visible = true;
            ddlChooseOperator2.Visible = true;
            ddlChooseOperator3.Visible = true;
            ddlChooseOperator4.Visible = true;
            ddlChooseOperator5.Visible = true;
            txbSearchValue.Visible = true;
            txbSearchValue2.Visible = true;
            txbSearchValue3.Visible = true;
            txbSearchValue4.Visible = true;
            txbSearchValue5.Visible = true;

            txbSearchValue.Enabled = true;
            txbSearchValue2.Enabled = true;
            txbSearchValue3.Enabled = true;
            txbSearchValue4.Enabled = true;
            txbSearchValue5.Enabled = true;
            
            
            rblNumber1.Visible = true;
            rblNumber2.Visible = true;
            rblNumber3.Visible = true;
            rblNumber4.Visible = true;
        }

        protected void cmdStudent_Click(object sender, EventArgs e)
        {
            con.Open();
            string group = "";
            lblErrorMessage.Visible = false;
            Boolean ChooseCustomView = false;
            int flag = 0;
            Boolean GradeFLAG = false;


            try
            {
                string sql = "";
                string sql_fields = "";

                string SearchBool = "Select a value";
                string SearchBool2 = "Select a value";
                string SearchBool3 = "Select a value";
                string SearchBool4 = "Select a value";
                string SearchBool5 = "Select a value";

                if (ddlSearchValueBool.Text == "1 (Active in Program)")
                {
                    SearchBool = "1";
                }
                else if (ddlSearchValueBool.Text == "0 (Not Active)")
                {
                    SearchBool = "0";
                }

                if (ddlSearchValue2Bool.Text == "1 (Active in Program)")
                {
                    SearchBool2 = "1";
                }
                else if (ddlSearchValue2Bool.Text == "0 (Not Active)")
                {
                    SearchBool2 = "0";
                }

                if (ddlSearchValue3Bool.Text == "1 (Active in Program)")
                {
                    SearchBool3 = "1";
                }
                else if (ddlSearchValue3Bool.Text == "0 (Not Active)")
                {
                    SearchBool3 = "0";
                }

                if (ddlSearchValue4Bool.Text == "1 (Active in Program)")
                {
                    SearchBool4 = "1";
                }
                else if (ddlSearchValue4Bool.Text == "0 (Not Active)")
                {
                    SearchBool4 = "0";
                }

                if (ddlSearchValue5Bool.Text == "1 (Active in Program)")
                {
                    SearchBool5 = "1";
                }
                else if (ddlSearchValue5Bool.Text == "0 (Not Active)")
                {
                    SearchBool5 = "0";
                }

                foreach (ListItem item in cblCreateCustomView.Items)
                {
                    if (item.Selected == true)
                    {
                        flag = 1;
                        ChooseCustomView = true;
                    }
                }

                if ((ddlChooseField.Text == "Grade") || (ddlChooseField2.Text == "Grade") || (ddlChooseField3.Text == "Grade") || (ddlChooseField4.Text == "Grade") || (ddlChooseField5.Text == "Grade"))
                {
                    GradeFLAG = true;
                }

                if (ChooseCustomView)
                {   //Use the custom view created by the user..RCM.. 10/26/11.
                    sql_fields = DetermineCustomViewQuery(GradeFLAG);
                    group = sql_fields;
                    if (GradeFLAG)
                    {
                        group = group.Replace(" as 'Grade'", "");
                        group = group.Replace(" as 'Age'", "");
                        group = group.Replace(" as 'Zip'", "");
                    }
                    group = group.Replace("select", "GROUP BY ");
                }
                else
                {   //Use one of the pre-canned views..RCM..10/26/11.

                    //Handle the different desired views on the data...RCM..
                    if (ddlAdvancedSearchView.Text == "Select a View (Optional)")
                    {
                        sql_fields = "select si.LastName + ',' + si.FirstName  + ' ('+ si.MiddleName + ')' as 'Name', si.address as 'Address', si.city as 'City', si.state as 'State', si.zip as 'Zip', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', si.school as 'School' ";
                    }
                    else if (ddlAdvancedSearchView.Text == "Address Info")
                    {
                        sql_fields = "select si.LastName + ',' + si.FirstName  + ' ('+ si.MiddleName + ')' as 'Name', si.address as 'Address', si.city as 'City', si.state as 'State', si.zip as 'Zip', si.homephone as 'HomePhone', si.studentcellphone as 'CellPhone', si.school as 'School' ";
                    }
                    else if (ddlAdvancedSearchView.Text == "Personal Info")
                    {
                        sql_fields = "select si.LastName + ',' + si.FirstName  + ' ('+ si.MiddleName + ')' as 'Name', si.studentemail as 'Email', si.dob, si.sex, si.church, si.healthconditions, si.notes ";
                    }
                    else if (ddlAdvancedSearchView.Text == "All Available Info")
                    {
                        sql_fields = "select si.LastName + ',' + si.FirstName  + ' ('+ si.MiddleName + ')' as 'Name', si.address as 'Address', si.city as 'City', si.state as 'State', si.zip as 'Zip', si.homephone as 'HomePhone', si.StudentCellPhone, si.StudentEmail, si.Grade, "
                                   + "si.dob as 'DateOfBirth', si.sex as 'Gender', si.Church, si.Voicepart, si.careergoal, si.healthconditions, si.notes, si.tshirtsize, si.BibleStudyparticipation, si.havereceivedchrist, si.whenreceivedchrist ";
                    }
                }

                if (ddlChooseField.Text == "MSHSChoir" || ddlChooseField.Text == "ChildrensChoir" || ddlChooseField.Text == "PerformingArts" || ddlChooseField.Text == "Shakes" || ddlChooseField.Text == "Singers" || ddlChooseField.Text == "BibleStudy" || ddlChooseField.Text == "SoccerIntraMurals" || ddlChooseField.Text == "SoccerTEAMS" || ddlChooseField.Text == "MondayNights" || ddlChooseField.Text == "3on3Basketball" || ddlChooseField.Text == "BasketballTEAMS" || ddlChooseField.Text == "OutreachBasketball" || ddlChooseField.Text == "HSBasketballLg" || ddlChooseField.Text == "MSBasketballLg" || ddlChooseField.Text == "SummerDayCamp" || ddlChooseField.Text == "Baseball" || ddlChooseField.Text == "SpecialEvents" || ddlChooseField.Text == "ImpactUrbanSchools" || ddlChooseField.Text == "AcademicReadingSupport")
                {
                    if (ddlChooseField.Text == "PerformingArts")
                    {
                        sql = sql_fields
                        + "FROM UIF_PerformingArts.dbo.studentprogramattendance  si "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation sin "
                        + "ON (si.LastName = sin.LastName AND si.FirstName = sin.FirstName AND si.MiddleName = sin.MiddleName) "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                        + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo.CoreKidsProgram ckp "
                        + "ON (si.LastName = ckp.LastName AND si.FirstName = ckp.FirstName) "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo.parentguardiancontactinformation pg "
                        + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName AND si.MiddleName = pg.StudentMiddleName) ";
                    }
                    else
                    {
                        sql = sql_fields
                        + "FROM UIF_PerformingArts.dbo.studentprogramattendance  si "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation sin "
                        + "ON (si.LastName = sin.LastName AND si.FirstName = sin.FirstName AND si.MiddleName = sin.MiddleName) "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                        + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo.CoreKidsProgram ckp "
                        + "ON (si.LastName = ckp.LastName AND si.FirstName = ckp.FirstName) "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo.parentguardiancontactinformation pg "
                        + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName AND si.MiddleName = pg.StudentMiddleName) "
                        + "LEFT OUTER JOIN UIF_PerformingArts.dbo." + ddlChooseField.Text + "Enrollment pe "
                        + "ON (si.LastName = pe.StudentLastName AND si.FirstName = pe.StudentFirstName AND si.MiddleName = pe.MiddleName) ";
                    }
                }
                else
                {
                    sql = sql_fields
                    + "FROM UIF_PerformingArts.dbo.studentprogramattendance  si "
                    + "LEFT OUTER JOIN UIF_PerformingArts.dbo.studentinformation sin "
                    + "ON (si.LastName = sin.LastName AND si.FirstName = sin.FirstName AND si.MiddleName = sin.MiddleName) "
                    + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                    + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                    + "LEFT OUTER JOIN UIF_PerformingArts.dbo.CoreKidsProgram ckp "
                    + "ON (si.LastName = ckp.LastName AND si.FirstName = ckp.FirstName) "
                    + "LEFT OUTER JOIN UIF_PerformingArts.dbo.parentguardiancontactinformation pg "
                    + "ON (si.LastName = pg.StudentLastName AND si.FirstName = pg.StudentFirstName AND si.MiddleName = pg.StudentMiddleName) ";
                }

                if (ddlChooseField.Text != "Choose Field")
                {
                    if ((ddlChooseOperator.Text != "Choose Operator") && (ddlOperatorCharacter.Text == "Choose Operator") && (ddlOperatorBoolean.Text == "Choose Operator"))
                    {
                        if (ddlChooseField.Text == "MSHSChoir" || ddlChooseField.Text == "ChildrensChoir" || ddlChooseField.Text == "PerformingArts" || ddlChooseField.Text == "Shakes" || ddlChooseField.Text == "Singers" || ddlChooseField.Text == "BibleStudy" || ddlChooseField.Text == "SoccerIntraMurals" || ddlChooseField.Text == "SoccerTEAMS" || ddlChooseField.Text == "MondayNights" || ddlChooseField.Text == "3on3Basketball" || ddlChooseField.Text == "BasketballTEAMS" || ddlChooseField.Text == "OutreachBasketball" || ddlChooseField.Text == "HSBasketballLg" || ddlChooseField.Text == "MSBasketballLg" || ddlChooseField.Text == "SummerDayCamp" || ddlChooseField.Text == "Baseball" || ddlChooseField.Text == "SpecialEvents" || ddlChooseField.Text == "ImpactUrbanSchools" || ddlChooseField.Text == "AcademicReadingSupport")
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
                            }
                        }
                        else if ((ddlChooseField.Text == "Grade") || (ddlChooseField.Text == "Age"))
                        {
                            if (ddlChooseField.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades.Text.Trim() == "K") || (ddlGrades.Text.Trim() == "k") || (ddlGrades.Text.Trim() == "SV") || (ddlGrades.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "sv") || (ddlGrades.Text.Contains("G")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlChooseOperator.Text == "equals")
                                    {
                                        sql = sql + "where si." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + "where si." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades.Text.Trim() == "1") || (ddlGrades.Text.Trim() == "2") || (ddlGrades.Text.Trim() == "3") || (ddlGrades.Text.Trim() == "4") || (ddlGrades.Text.Trim() == "5") || (ddlGrades.Text.Trim() == "6") || (ddlGrades.Text.Trim() == "7") || (ddlGrades.Text.Trim() == "8") || (ddlGrades.Text.Trim() == "9") || (ddlGrades.Text.Trim() == "10") || (ddlGrades.Text.Trim() == "11") || (ddlGrades.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR13' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR13' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <> " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR13' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR13' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR13' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR13' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'GR13' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'GR13' AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) NOT LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField.Text == "Age")
                            {
                                if (ddlChooseOperator.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) = " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) <> " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) > " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) >= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) < " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) <= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) like " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) NOT like " + txbSearchValue.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
                            }
                        }
                        else if (ddlChooseField.Text == "All Programs")
                        {
                            sql = sql + "where (pl.outreachbasketball = 1 or pl.basketballteams = 1 or pl.msbasketballlg = 1 or pl.hsbasketballlg = 1 or pl.baseball = 1 or pl.[3on3basketball] = 1 or pl.soccerintramurals = 1 or pl.soccerteams = 1 or pl.mondaynights = 1 or pl.specialevents = 1 or pl.biblestudy = 1 or pl.mshschoir = 1 or pl.childrenschoir = 1 or pl.performingarts = 1 or pl.shakes = 1 or pl.singers = 1 or pl.impacturbanschools = 1 or pl.academicreadingsupport = 1 or pl.summerdaycamp = 1) ";
                        }
                        else if (ddlChooseField.Text == "AthleticsDept")
                        {
                            sql = sql + "where (pl.outreachbasketball = 1 or pl.basketballteams = 1 or pl.msbasketballlg = 1 or pl.hsbasketballlg = 1 or pl.baseball = 1 or pl.[3on3basketball] = 1 or pl.soccerintramurals = 1 or pl.soccerteams = 1 or pl.mondaynights = 1 or pl.specialevents = 1 or pl.biblestudy = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "PerformingArtsDept")
                        {
                            sql = sql + "where (pl.mshschoir = 1 or pl.childrenschoir = 1 or pl.performingarts = 1 or pl.shakes = 1 or pl.singers = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "EducationDept")
                        {
                            sql = sql + "where (pl.academicreadingsupport = 1 or pl.summerdaycamp = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "CoreKid")
                        {
                           sql = sql + "where ckp." + ddlChooseField.Text + " = " + SearchBool + " ";


                           //if (ddlChooseOperator.Text == "equals")
                           //{
                           //    sql = sql + "where si." + ddlChooseField.Text + " = " + SearchBool + " ";
                           //}
                           //else if (ddlChooseOperator.Text == ">")
                           //{
                           //    sql = sql + "where si." + ddlChooseField.Text + " > " + SearchBool + " ";
                           //}
                           //else if (ddlChooseOperator.Text == "<")
                           //{
                           //    sql = sql + "where si." + ddlChooseField.Text + " < " + SearchBool + " ";
                           //}
                           //else if (ddlChooseOperator.Text == "contains")
                           //{
                           //    sql = sql + "where si." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                           //}
                           //else if (ddlChooseOperator.Text == "NOT equals")
                           //{
                           //    sql = sql + "where si." + ddlChooseField.Text + " <> " + SearchBool + " ";
                           //}
                           //else if (ddlChooseOperator.Text == "NOT contains")
                           //{
                           //    sql = sql + "where si." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
                           //}
                        }
                        else
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " = '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " > '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " < '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " like '%" + txbSearchValue.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator.Text == "Choose Operator") && (ddlOperatorCharacter.Text != "Choose Operator") && (ddlOperatorBoolean.Text == "Choose Operator"))
                    {
                        if (ddlChooseField.Text == "MSHSChoir" || ddlChooseField.Text == "ChildrensChoir" || ddlChooseField.Text == "PerformingArts" || ddlChooseField.Text == "Shakes" || ddlChooseField.Text == "Singers" || ddlChooseField.Text == "BibleStudy" || ddlChooseField.Text == "SoccerIntraMurals" || ddlChooseField.Text == "SoccerTEAMS" || ddlChooseField.Text == "MondayNights" || ddlChooseField.Text == "3on3Basketball" || ddlChooseField.Text == "BasketballTEAMS" || ddlChooseField.Text == "OutreachBasketball" || ddlChooseField.Text == "HSBasketballLg" || ddlChooseField.Text == "MSBasketballLg" || ddlChooseField.Text == "SummerDayCamp" || ddlChooseField.Text == "Baseball" || ddlChooseField.Text == "SpecialEvents" || ddlChooseField.Text == "ImpactUrbanSchools" || ddlChooseField.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorCharacter.Text == "equals")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == ">")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == "<")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == "contains")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                        }
                        else if ((ddlChooseField.Text == "Grade") || (ddlChooseField.Text == "Age"))
                        {
                            if (ddlChooseField.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades.Text.Trim() == "K") || (ddlGrades.Text.Trim() == "k") || (ddlGrades.Text.Trim() == "SV") || (ddlGrades.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "sv") || (ddlGrades.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorCharacter.Text == "equals")
                                    {
                                        sql = sql + "where si." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + "where si." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades.Text.Trim() == "1") || (ddlGrades.Text.Trim() == "2") || (ddlGrades.Text.Trim() == "3") || (ddlGrades.Text.Trim() == "4") || (ddlGrades.Text.Trim() == "5") || (ddlGrades.Text.Trim() == "6") || (ddlGrades.Text.Trim() == "7") || (ddlGrades.Text.Trim() == "8") || (ddlGrades.Text.Trim() == "9") || (ddlGrades.Text.Trim() == "10") || (ddlGrades.Text.Trim() == "11") || (ddlGrades.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'GR13' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'GR13' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <> " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'GR13' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'GR13' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'GR13' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'GR13' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'GR13' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'GR13' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) NOT LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField.Text == "Age")
                            {
                                if (ddlOperatorCharacter.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) = " + txbSearchValue.Text.Trim() + " ";
                                }
                                if (ddlOperatorCharacter.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) <> " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) > " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) >= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) < " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) <= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) like " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) NOT like " + txbSearchValue.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter.Text == "equals")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == ">")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == "<")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == "contains")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                        }
                        else if (ddlChooseField.Text == "All Programs")
                        {
                            sql = sql + "where (pl.outreachbasketball = 1 or pl.basketballteams = 1 or pl.msbasketballlg = 1 or pl.hsbasketballlg = 1 or pl.baseball = 1 or pl.[3on3basketball] = 1 or pl.soccerintramurals = 1 or pl.soccerteams = 1 or pl.mondaynights = 1 or pl.specialevents = 1 or pl.biblestudy = 1 or pl.mshschoir = 1 or pl.childrenschoir = 1 or pl.performingarts = 1 or pl.shakes = 1 or pl.singers = 1 or pl.impacturbanschools = 1 or pl.academicreadingsupport = 1 or pl.summerdaycamp = 1) ";
                        }
                        else if (ddlChooseField.Text == "AthleticsDept")
                        {
                            sql = sql + "where (pl.outreachbasketball = 1 or pl.basketballteams = 1 or pl.msbasketballlg = 1 or pl.hsbasketballlg = 1 or pl.baseball = 1 or pl.[3on3basketball] = 1 or pl.soccerintramurals = 1 or pl.soccerteams = 1 or pl.mondaynights = 1 or pl.specialevents = 1 or pl.biblestudy = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "PerformingArtsDept")
                        {
                            sql = sql + "where (pl.mshschoir = 1 or pl.childrenschoir = 1 or pl.performingarts = 1 or pl.shakes = 1 or pl.singers = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "EducationDept")
                        {
                            sql = sql + "where (pl.academicreadingsupport = 1 or pl.summerdaycamp = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "CoreKid")
                        {
                            sql = sql + "where ckp." + ddlChooseField.Text + " = " + SearchBool + " ";
                        }
                        else
                        {
                            if (ddlOperatorCharacter.Text == "equals")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " = '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter.Text == ">")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " > '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter.Text == "<")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " < '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter.Text == "contains")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " like '%" + txbSearchValue.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator.Text == "Choose Operator") && (ddlOperatorCharacter.Text == "Choose Operator") && (ddlOperatorBoolean.Text != "Choose Operator"))
                    {
                        if (ddlChooseField.Text == "MSHSChoir" || ddlChooseField.Text == "ChildrensChoir" || ddlChooseField.Text == "PerformingArts" || ddlChooseField.Text == "Shakes" || ddlChooseField.Text == "Singers" || ddlChooseField.Text == "BibleStudy" || ddlChooseField.Text == "SoccerIntraMurals" || ddlChooseField.Text == "SoccerTEAMS" || ddlChooseField.Text == "MondayNights" || ddlChooseField.Text == "3on3Basketball" || ddlChooseField.Text == "BasketballTEAMS" || ddlChooseField.Text == "OutreachBasketball" || ddlChooseField.Text == "HSBasketballLg" || ddlChooseField.Text == "MSBasketballLg" || ddlChooseField.Text == "SummerDayCamp" || ddlChooseField.Text == "Baseball" || ddlChooseField.Text == "SpecialEvents" || ddlChooseField.Text == "ImpactUrbanSchools" || ddlChooseField.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorBoolean.Text == "equals")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == ">")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == "<")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == "contains")
                            {
                                sql = sql + "where pl." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                        }
                        else if ((ddlChooseField.Text == "Grade") || (ddlChooseField.Text == "Age"))
                        {
                            if (ddlChooseField.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades.Text.Trim() == "K") || (ddlGrades.Text.Trim() == "k") || (ddlGrades.Text.Trim() == "SV") || (ddlGrades.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "sv") || (ddlGrades.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorBoolean.Text == "equals")
                                    {
                                        sql = sql + "where si." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + "where si." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades.Text.Trim() == "1") || (ddlGrades.Text.Trim() == "2") || (ddlGrades.Text.Trim() == "3") || (ddlGrades.Text.Trim() == "4") || (ddlGrades.Text.Trim() == "5") || (ddlGrades.Text.Trim() == "6") || (ddlGrades.Text.Trim() == "7") || (ddlGrades.Text.Trim() == "8") || (ddlGrades.Text.Trim() == "9") || (ddlGrades.Text.Trim() == "10") || (ddlGrades.Text.Trim() == "11") || (ddlGrades.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR13' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR13' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR13' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR13' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR13' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR13' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField.Text == "Age")
                            {
                                if (ddlOperatorBoolean.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) = " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) > " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) >= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) < " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) <= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,si.Age) like " + txbSearchValue.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean.Text == "equals")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == ">")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == "<")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == "contains")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                        }
                        else if (ddlChooseField.Text == "All Programs")
                        {
                            sql = sql + "where (pl.outreachbasketball = 1 or pl.basketballteams = 1 or pl.msbasketballlg = 1 or pl.hsbasketballlg = 1 or pl.baseball = 1 or pl.[3on3basketball] = 1 or pl.soccerintramurals = 1 or pl.soccerteams = 1 or pl.mondaynights = 1 or pl.specialevents = 1 or pl.biblestudy = 1 or pl.mshschoir = 1 or pl.childrenschoir = 1 or pl.performingarts = 1 or pl.shakes = 1 or pl.singers = 1 or pl.impacturbanschools = 1 or pl.academicreadingsupport = 1 or pl.summerdaycamp = 1) ";
                        }
                        else if (ddlChooseField.Text == "AthleticsDept")
                        {
                            sql = sql + "where (pl.outreachbasketball = 1 or pl.basketballteams = 1 or pl.msbasketballlg = 1 or pl.hsbasketballlg = 1 or pl.baseball = 1 or pl.[3on3basketball] = 1 or pl.soccerintramurals = 1 or pl.soccerteams = 1 or pl.mondaynights = 1 or pl.specialevents = 1 or pl.biblestudy = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "PerformingArtsDept")
                        {
                            sql = sql + "where (pl.mshschoir = 1 or pl.childrenschoir = 1 or pl.performingarts = 1 or pl.shakes = 1 or pl.singers = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "EducationDept")
                        {
                            sql = sql + "where (pl.academicreadingsupport = 1 or pl.summerdaycamp = 1 or pl.impacturbanschools = 1) ";
                        }
                        else if (ddlChooseField.Text == "CoreKid")
                        {
                            sql = sql + "where ckp." + ddlChooseField.Text + " = " + SearchBool + " ";
                        }
                        else
                        {
                            if (ddlOperatorBoolean.Text == "equals")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " = '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean.Text == ">")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " > '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean.Text == "<")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " < '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean.Text == "contains")
                            {
                                sql = sql + "where si." + ddlChooseField.Text + " like '%" + txbSearchValue.Text.Trim() + "%' ";
                            }
                        }
                    }
                }
                if (ddlChooseField2.Text != "Choose Field")
                {
                    if ((ddlChooseOperator2.Text != "Choose Operator") && (ddlOperatorCharacter2.Text == "Choose Operator") && (ddlOperatorBoolean2.Text == "Choose Operator"))
                    {
                        if (ddlChooseField2.Text == "MSHSChoir" || ddlChooseField2.Text == "ChildrensChoir" || ddlChooseField2.Text == "PerformingArts" || ddlChooseField2.Text == "Shakes" || ddlChooseField2.Text == "Singers" || ddlChooseField2.Text == "BibleStudy" || ddlChooseField2.Text == "SoccerIntraMurals" || ddlChooseField2.Text == "SoccerTEAMS" || ddlChooseField2.Text == "MondayNights" || ddlChooseField2.Text == "3on3Basketball" || ddlChooseField2.Text == "BasketballTEAMS" || ddlChooseField2.Text == "OutreachBasketball" || ddlChooseField2.Text == "HSBasketballLg" || ddlChooseField2.Text == "MSBasketballLg" || ddlChooseField2.Text == "SummerDayCamp" || ddlChooseField2.Text == "Baseball" || ddlChooseField2.Text == "SpecialEvents" || ddlChooseField2.Text == "ImpactUrbanSchools" || ddlChooseField2.Text == "AcademicReadingSupport")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " = '" + SearchBool2 + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " <> '" + SearchBool2 + "' ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " > '" + SearchBool2 + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " < '" + SearchBool2 + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " not like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if ((ddlChooseField2.Text == "Grade") || (ddlChooseField2.Text == "Age"))
                        {
                            if (ddlChooseField2.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades2.Text.Trim() == "K") || (ddlGrades2.Text.Trim() == "k") || (ddlGrades2.Text.Trim() == "SV") || (ddlGrades2.Text.Contains("GR")) || (ddlGrades2.Text.Trim() == "sv") || (ddlGrades2.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlChooseOperator2.Text == "equals")
                                    {
                                        sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades2.Text.Trim() == "1") || (ddlGrades2.Text.Trim() == "2") || (ddlGrades2.Text.Trim() == "3") || (ddlGrades2.Text.Trim() == "4") || (ddlGrades2.Text.Trim() == "5") || (ddlGrades2.Text.Trim() == "6") || (ddlGrades2.Text.Trim() == "7") || (ddlGrades2.Text.Trim() == "8") || (ddlGrades2.Text.Trim() == "9") || (ddlGrades2.Text.Trim() == "10") || (ddlGrades2.Text.Trim() == "11") || (ddlGrades2.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator2.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField2.Text == "Age")
                            {
                                if (ddlChooseOperator2.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue2.Text.Trim() + " ";
                                }
                                if (ddlChooseOperator2.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <> " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) NOT like " + txbSearchValue2.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField2.Text == "CampDropOff" || ddlChooseField2.Text == "CampPickUp" || ddlChooseField2.Text == "CurrentRegistrationForm" || ddlChooseField2.Text == "Dance" || ddlChooseField2.Text == "HaveReceivedChrist" || ddlChooseField2.Text == "MailingListInclude" || ddlChooseField2.Text == "ParentalConsentForm" || ddlChooseField2.Text == "PermissionToTransport" || ddlChooseField2.Text == "PromotionalRelease" || ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " NOT like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if (ddlChooseField2.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if (ddlChooseField2.Text == "Section")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " pe." + ddlChooseField2.Text + " = '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " pe." + ddlChooseField2.Text + " <> '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " pe." + ddlChooseField2.Text + " > '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " pe." + ddlChooseField2.Text + " < '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " pe." + ddlChooseField2.Text + " like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " pe." + ddlChooseField2.Text + " NOT like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " <> '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " > '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " < '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " NOT like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator2.Text == "Choose Operator") && (ddlOperatorCharacter2.Text != "Choose Operator") && (ddlOperatorBoolean2.Text == "Choose Operator"))
                    {
                        if (ddlChooseField2.Text == "MSHSChoir" || ddlChooseField2.Text == "ChildrensChoir" || ddlChooseField2.Text == "PerformingArts" || ddlChooseField2.Text == "Shakes" || ddlChooseField2.Text == "Singers" || ddlChooseField2.Text == "BibleStudy" || ddlChooseField2.Text == "SoccerIntraMurals" || ddlChooseField2.Text == "SoccerTEAMS" || ddlChooseField2.Text == "MondayNights" || ddlChooseField2.Text == "3on3Basketball" || ddlChooseField2.Text == "BasketballTEAMS" || ddlChooseField2.Text == "OutreachBasketball" || ddlChooseField2.Text == "HSBasketballLg" || ddlChooseField2.Text == "MSBasketballLg" || ddlChooseField2.Text == "SummerDayCamp" || ddlChooseField2.Text == "Baseball" || ddlChooseField2.Text == "SpecialEvents" || ddlChooseField2.Text == "ImpactUrbanSchools" || ddlChooseField2.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorCharacter2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " = '" + SearchBool2 + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " <> '" + SearchBool2 + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " > '" + SearchBool2 + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " < '" + SearchBool2 + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " NOT like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if ((ddlChooseField2.Text == "Grade") || (ddlChooseField2.Text == "Age"))
                        {
                            if (ddlChooseField2.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades2.Text.Trim() == "K") || (ddlGrades2.Text.Trim() == "k") || (ddlGrades2.Text.Trim() == "SV") || (ddlGrades2.Text.Contains("GR")) || (ddlGrades2.Text.Trim() == "sv") || (ddlGrades2.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorCharacter2.Text == "equals")
                                    {
                                        sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades2.Text.Trim() == "1") || (ddlGrades2.Text.Trim() == "2") || (ddlGrades2.Text.Trim() == "3") || (ddlGrades2.Text.Trim() == "4") || (ddlGrades2.Text.Trim() == "5") || (ddlGrades2.Text.Trim() == "6") || (ddlGrades2.Text.Trim() == "7") || (ddlGrades2.Text.Trim() == "8") || (ddlGrades2.Text.Trim() == "9") || (ddlGrades2.Text.Trim() == "10") || (ddlGrades2.Text.Trim() == "11") || (ddlGrades2.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter2.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <> " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) NOT LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField2.Text == "Age")
                            {
                                if (ddlOperatorCharacter2.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <> " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) NOT like " + txbSearchValue2.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField2.Text == "CampDropOff" || ddlChooseField2.Text == "CampPickUp" || ddlChooseField2.Text == "CurrentRegistrationForm" || ddlChooseField2.Text == "Dance" || ddlChooseField2.Text == "HaveReceivedChrist" || ddlChooseField2.Text == "MailingListInclude" || ddlChooseField2.Text == "ParentalConsentForm" || ddlChooseField2.Text == "PermissionToTransport" || ddlChooseField2.Text == "PromotionalRelease" || ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorCharacter2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorCharacter2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorCharacter2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " NOT like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if (ddlChooseField2.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " <> '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " > '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " < '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " NOT like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator2.Text == "Choose Operator") && (ddlOperatorCharacter2.Text == "Choose Operator") && (ddlOperatorBoolean2.Text != "Choose Operator"))
                    {
                        if (ddlChooseField2.Text == "MSHSChoir" || ddlChooseField2.Text == "ChildrensChoir" || ddlChooseField2.Text == "PerformingArts" || ddlChooseField2.Text == "Shakes" || ddlChooseField2.Text == "Singers" || ddlChooseField2.Text == "BibleStudy" || ddlChooseField2.Text == "SoccerIntraMurals" || ddlChooseField2.Text == "SoccerTEAMS" || ddlChooseField2.Text == "MondayNights" || ddlChooseField2.Text == "3on3Basketball" || ddlChooseField2.Text == "BasketballTEAMS" || ddlChooseField2.Text == "OutreachBasketball" || ddlChooseField2.Text == "HSBasketballLg" || ddlChooseField2.Text == "MSBasketballLg" || ddlChooseField2.Text == "SummerDayCamp" || ddlChooseField2.Text == "Baseball" || ddlChooseField2.Text == "SpecialEvents" || ddlChooseField2.Text == "ImpactUrbanSchools" || ddlChooseField2.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorBoolean2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " = '" + SearchBool2 + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " > '" + SearchBool2 + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " < '" + SearchBool2 + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " pl." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if ((ddlChooseField2.Text == "Grade") || (ddlChooseField2.Text == "Age"))
                        {
                            if (ddlChooseField2.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades2.Text.Trim() == "K") || (ddlGrades2.Text.Trim() == "k") || (ddlGrades2.Text.Trim() == "SV") || (ddlGrades2.Text.Contains("GR")) || (ddlGrades2.Text.Trim() == "sv") || (ddlGrades2.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorBoolean2.Text == "equals")
                                    {
                                        sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades2.Text.Trim() == "1") || (ddlGrades2.Text.Trim() == "2") || (ddlGrades2.Text.Trim() == "3") || (ddlGrades2.Text.Trim() == "4") || (ddlGrades2.Text.Trim() == "5") || (ddlGrades2.Text.Trim() == "6") || (ddlGrades2.Text.Trim() == "7") || (ddlGrades2.Text.Trim() == "8") || (ddlGrades2.Text.Trim() == "9") || (ddlGrades2.Text.Trim() == "10") || (ddlGrades2.Text.Trim() == "11") || (ddlGrades2.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean2.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField2.Text == "Age")
                            {
                                if (ddlOperatorBoolean2.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue2.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField2.Text == "CampDropOff" || ddlChooseField2.Text == "CampPickUp" || ddlChooseField2.Text == "CurrentRegistrationForm" || ddlChooseField2.Text == "Dance" || ddlChooseField2.Text == "HaveReceivedChrist" || ddlChooseField2.Text == "MailingListInclude" || ddlChooseField2.Text == "ParentalConsentForm" || ddlChooseField2.Text == "PermissionToTransport" || ddlChooseField2.Text == "PromotionalRelease" || ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if (ddlChooseField2.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " ckp." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " = '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " > '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " < '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " si." + ddlChooseField2.Text + " like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                        }
                    }
                }
                if (ddlChooseField3.Text != "Choose Field")
                {
                    if ((ddlChooseOperator3.Text != "Choose Operator") && (ddlOperatorCharacter3.Text == "Choose Operator") && (ddlOperatorBoolean3.Text == "Choose Operator"))
                    {
                        if (ddlChooseField3.Text == "MSHSChoir" || ddlChooseField3.Text == "ChildrensChoir" || ddlChooseField3.Text == "PerformingArts" || ddlChooseField3.Text == "Shakes" || ddlChooseField3.Text == "Singers" || ddlChooseField3.Text == "BibleStudy" || ddlChooseField3.Text == "SoccerIntraMurals" || ddlChooseField3.Text == "SoccerTEAMS" || ddlChooseField3.Text == "MondayNights" || ddlChooseField3.Text == "3on3Basketball" || ddlChooseField3.Text == "BasketballTEAMS" || ddlChooseField3.Text == "OutreachBasketball" || ddlChooseField3.Text == "HSBasketballLg" || ddlChooseField3.Text == "MSBasketballLg" || ddlChooseField3.Text == "SummerDayCamp" || ddlChooseField3.Text == "Baseball" || ddlChooseField3.Text == "SpecialEvents" || ddlChooseField3.Text == "ImpactUrbanSchools" || ddlChooseField3.Text == "AcademicReadingSupport")
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " = '" + SearchBool3 + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " <> '" + SearchBool3 + "' ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " > '" + SearchBool3 + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " < '" + SearchBool3 + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " not like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if ((ddlChooseField3.Text == "Grade") || (ddlChooseField3.Text == "Age"))
                        {
                            if (ddlChooseField3.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades3.Text.Trim() == "K") || (ddlGrades3.Text.Trim() == "k") || (ddlGrades3.Text.Trim() == "SV") || (ddlGrades3.Text.Contains("GR")) || (ddlGrades3.Text.Trim() == "sv") || (ddlGrades3.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlChooseOperator3.Text == "equals")
                                    {
                                        sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades3.Text.Trim() == "1") || (ddlGrades3.Text.Trim() == "2") || (ddlGrades3.Text.Trim() == "3") || (ddlGrades3.Text.Trim() == "4") || (ddlGrades3.Text.Trim() == "5") || (ddlGrades3.Text.Trim() == "6") || (ddlGrades3.Text.Trim() == "7") || (ddlGrades3.Text.Trim() == "8") || (ddlGrades3.Text.Trim() == "9") || (ddlGrades3.Text.Trim() == "10") || (ddlGrades3.Text.Trim() == "11") || (ddlGrades3.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator3.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <> " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) NOT LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField3.Text == "Age")
                            {
                                if (ddlChooseOperator3.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <> " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) NOT like " + txbSearchValue3.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField3.Text == "CampDropOff" || ddlChooseField3.Text == "CampPickUp" || ddlChooseField3.Text == "CurrentRegistrationForm" || ddlChooseField3.Text == "Dance" || ddlChooseField3.Text == "HaveReceivedChrist" || ddlChooseField3.Text == "MailingListInclude" || ddlChooseField3.Text == "ParentalConsentForm" || ddlChooseField3.Text == "PermissionToTransport" || ddlChooseField3.Text == "PromotionalRelease" || ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " NOT like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if (ddlChooseField3.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " <> '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " > '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " < '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " NOT like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator3.Text == "Choose Operator") && (ddlOperatorCharacter3.Text != "Choose Operator") && (ddlOperatorBoolean3.Text == "Choose Operator"))
                    {
                        if (ddlChooseField3.Text == "MSHSChoir" || ddlChooseField3.Text == "ChildrensChoir" || ddlChooseField3.Text == "PerformingArts" || ddlChooseField3.Text == "Shakes" || ddlChooseField3.Text == "Singers" || ddlChooseField3.Text == "BibleStudy" || ddlChooseField3.Text == "SoccerIntraMurals" || ddlChooseField3.Text == "SoccerTEAMS" || ddlChooseField3.Text == "MondayNights" || ddlChooseField3.Text == "3on3Basketball" || ddlChooseField3.Text == "BasketballTEAMS" || ddlChooseField3.Text == "OutreachBasketball" || ddlChooseField3.Text == "HSBasketballLg" || ddlChooseField3.Text == "MSBasketballLg" || ddlChooseField3.Text == "SummerDayCamp" || ddlChooseField3.Text == "Baseball" || ddlChooseField3.Text == "SpecialEvents" || ddlChooseField3.Text == "ImpactUrbanSchools" || ddlChooseField3.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorCharacter3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " = '" + SearchBool3 + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " <> '" + SearchBool3 + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " > '" + SearchBool3 + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " < '" + SearchBool3 + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " not like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if ((ddlChooseField3.Text == "Grade") || (ddlChooseField3.Text == "Age"))
                        {
                            if (ddlChooseField3.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades3.Text.Trim() == "K") || (ddlGrades3.Text.Trim() == "k") || (ddlGrades3.Text.Trim() == "SV") || (ddlGrades3.Text.Contains("GR")) || (ddlGrades3.Text.Trim() == "sv") || (ddlGrades3.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorCharacter3.Text == "equals")
                                    {
                                        sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades3.Text.Trim() == "1") || (ddlGrades3.Text.Trim() == "2") || (ddlGrades3.Text.Trim() == "3") || (ddlGrades3.Text.Trim() == "4") || (ddlGrades3.Text.Trim() == "5") || (ddlGrades3.Text.Trim() == "6") || (ddlGrades3.Text.Trim() == "7") || (ddlGrades3.Text.Trim() == "8") || (ddlGrades3.Text.Trim() == "9") || (ddlGrades3.Text.Trim() == "10") || (ddlGrades3.Text.Trim() == "11") || (ddlGrades3.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter3.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <> " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) NOT LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField3.Text == "Age")
                            {
                                if (ddlOperatorCharacter3.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <> " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) NOT like " + txbSearchValue3.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField3.Text == "CampDropOff" || ddlChooseField3.Text == "CampPickUp" || ddlChooseField3.Text == "CurrentRegistrationForm" || ddlChooseField3.Text == "Dance" || ddlChooseField3.Text == "HaveReceivedChrist" || ddlChooseField3.Text == "MailingListInclude" || ddlChooseField3.Text == "ParentalConsentForm" || ddlChooseField3.Text == "PermissionToTransport" || ddlChooseField3.Text == "PromotionalRelease" || ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorCharacter3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorCharacter3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorCharacter3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " NOT like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if (ddlChooseField3.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " <> '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " > '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " < '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " NOT like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator3.Text == "Choose Operator") && (ddlOperatorCharacter3.Text == "Choose Operator") && (ddlOperatorBoolean3.Text != "Choose Operator"))
                    {
                        if (ddlChooseField3.Text == "MSHSChoir" || ddlChooseField3.Text == "ChildrensChoir" || ddlChooseField3.Text == "PerformingArts" || ddlChooseField3.Text == "Shakes" || ddlChooseField3.Text == "Singers" || ddlChooseField3.Text == "BibleStudy" || ddlChooseField3.Text == "SoccerIntraMurals" || ddlChooseField3.Text == "SoccerTEAMS" || ddlChooseField3.Text == "MondayNights" || ddlChooseField3.Text == "3on3Basketball" || ddlChooseField3.Text == "BasketballTEAMS" || ddlChooseField3.Text == "OutreachBasketball" || ddlChooseField3.Text == "HSBasketballLg" || ddlChooseField3.Text == "MSBasketballLg" || ddlChooseField3.Text == "SummerDayCamp" || ddlChooseField3.Text == "Baseball" || ddlChooseField3.Text == "SpecialEvents" || ddlChooseField3.Text == "ImpactUrbanSchools" || ddlChooseField3.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorBoolean3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " = '" + SearchBool3 + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " > '" + SearchBool3 + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " < '" + SearchBool3 + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " pl." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if ((ddlChooseField3.Text == "Grade") || (ddlChooseField3.Text == "Age"))
                        {
                            if (ddlChooseField3.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades3.Text.Trim() == "K") || (ddlGrades3.Text.Trim() == "k") || (ddlGrades3.Text.Trim() == "SV") || (ddlGrades3.Text.Contains("GR")) || (ddlGrades3.Text.Trim() == "sv") || (ddlGrades3.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorBoolean3.Text == "equals")
                                    {
                                        sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades3.Text.Trim() == "1") || (ddlGrades3.Text.Trim() == "2") || (ddlGrades3.Text.Trim() == "3") || (ddlGrades3.Text.Trim() == "4") || (ddlGrades3.Text.Trim() == "5") || (ddlGrades3.Text.Trim() == "6") || (ddlGrades3.Text.Trim() == "7") || (ddlGrades3.Text.Trim() == "8") || (ddlGrades3.Text.Trim() == "9") || (ddlGrades3.Text.Trim() == "10") || (ddlGrades3.Text.Trim() == "11") || (ddlGrades3.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean3.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField3.Text == "Age")
                            {
                                if (ddlOperatorBoolean3.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue3.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField3.Text == "CampDropOff" || ddlChooseField3.Text == "CampPickUp" || ddlChooseField3.Text == "CurrentRegistrationForm" || ddlChooseField3.Text == "Dance" || ddlChooseField3.Text == "HaveReceivedChrist" || ddlChooseField3.Text == "MailingListInclude" || ddlChooseField3.Text == "ParentalConsentForm" || ddlChooseField3.Text == "PermissionToTransport" || ddlChooseField3.Text == "PromotionalRelease" || ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if (ddlChooseField3.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " ckp." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " = '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " > '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " < '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " si." + ddlChooseField3.Text + " like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                        }
                    }
                }
                if (ddlChooseField4.Text != "Choose Field")
                {
                    if ((ddlChooseOperator4.Text != "Choose Operator") && (ddlOperatorCharacter4.Text == "Choose Operator") && (ddlOperatorBoolean4.Text == "Choose Operator"))
                    {
                        if (ddlChooseField4.Text == "MSHSChoir" || ddlChooseField4.Text == "ChildrensChoir" || ddlChooseField4.Text == "PerformingArts" || ddlChooseField4.Text == "Shakes" || ddlChooseField4.Text == "Singers" || ddlChooseField4.Text == "BibleStudy" || ddlChooseField4.Text == "SoccerIntraMurals" || ddlChooseField4.Text == "SoccerTEAMS" || ddlChooseField4.Text == "MondayNights" || ddlChooseField4.Text == "3on3Basketball" || ddlChooseField4.Text == "BasketballTEAMS" || ddlChooseField4.Text == "OutreachBasketball" || ddlChooseField4.Text == "HSBasketballLg" || ddlChooseField4.Text == "MSBasketballLg" || ddlChooseField4.Text == "SummerDayCamp" || ddlChooseField4.Text == "Baseball" || ddlChooseField4.Text == "SpecialEvents" || ddlChooseField4.Text == "ImpactUrbanSchools" || ddlChooseField4.Text == "AcademicReadingSupport")
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " = '" + SearchBool4 + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " <> '" + SearchBool4 + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " not like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " > '" + SearchBool4 + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " < '" + SearchBool4 + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " not like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if ((ddlChooseField4.Text == "Grade") || (ddlChooseField4.Text == "Age"))
                        {
                            if (ddlChooseField4.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades4.Text.Trim() == "K") || (ddlGrades4.Text.Trim() == "k") || (ddlGrades4.Text.Trim() == "SV") || (ddlGrades4.Text.Contains("GR")) || (ddlGrades4.Text.Trim() == "sv") || (ddlGrades4.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlChooseOperator4.Text == "equals")
                                    {
                                        sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades4.Text.Trim() == "1") || (ddlGrades4.Text.Trim() == "2") || (ddlGrades4.Text.Trim() == "3") || (ddlGrades4.Text.Trim() == "4") || (ddlGrades4.Text.Trim() == "5") || (ddlGrades4.Text.Trim() == "6") || (ddlGrades4.Text.Trim() == "7") || (ddlGrades4.Text.Trim() == "8") || (ddlGrades4.Text.Trim() == "9") || (ddlGrades4.Text.Trim() == "10") || (ddlGrades4.Text.Trim() == "11") || (ddlGrades4.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator4.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <> " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) NOT LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField4.Text == "Age")
                            {
                                if (ddlChooseOperator4.Text == "equals")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == ">")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == ">=")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "<")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "<=")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "contains")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                            }
                        }
                        else if (ddlChooseField4.Text == "CampDropOff" || ddlChooseField4.Text == "CampPickUp" || ddlChooseField4.Text == "CurrentRegistrationForm" || ddlChooseField4.Text == "Dance" || ddlChooseField4.Text == "HaveReceivedChrist" || ddlChooseField4.Text == "MailingListInclude" || ddlChooseField4.Text == "ParentalConsentForm" || ddlChooseField4.Text == "PermissionToTransport" || ddlChooseField4.Text == "PromotionalRelease" || ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " NOT like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if (ddlChooseField4.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " > '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " < '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " NOT like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator4.Text == "Choose Operator") && (ddlOperatorCharacter4.Text != "Choose Operator") && (ddlOperatorBoolean4.Text == "Choose Operator"))
                    {
                        if (ddlChooseField4.Text == "MSHSChoir" || ddlChooseField4.Text == "ChildrensChoir" || ddlChooseField4.Text == "PerformingArts" || ddlChooseField4.Text == "Shakes" || ddlChooseField4.Text == "Singers" || ddlChooseField4.Text == "BibleStudy" || ddlChooseField4.Text == "SoccerIntraMurals" || ddlChooseField4.Text == "SoccerTEAMS" || ddlChooseField4.Text == "MondayNights" || ddlChooseField4.Text == "3on3Basketball" || ddlChooseField4.Text == "BasketballTEAMS" || ddlChooseField4.Text == "OutreachBasketball" || ddlChooseField4.Text == "HSBasketballLg" || ddlChooseField4.Text == "MSBasketballLg" || ddlChooseField4.Text == "SummerDayCamp" || ddlChooseField4.Text == "Baseball" || ddlChooseField4.Text == "SpecialEvents" || ddlChooseField4.Text == "ImpactUrbanSchools" || ddlChooseField4.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorCharacter4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " = '" + SearchBool4 + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " <> '" + SearchBool4 + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " > '" + SearchBool4 + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " < '" + SearchBool4 + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " not like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if ((ddlChooseField4.Text == "Grade") || (ddlChooseField4.Text == "Age"))
                        {
                            if (ddlChooseField4.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades4.Text.Trim() == "K") || (ddlGrades4.Text.Trim() == "k") || (ddlGrades4.Text.Trim() == "SV") || (ddlGrades4.Text.Contains("GR")) || (ddlGrades4.Text.Trim() == "sv") || (ddlGrades4.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorCharacter4.Text == "equals")
                                    {
                                        sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades4.Text.Trim() == "1") || (ddlGrades4.Text.Trim() == "2") || (ddlGrades4.Text.Trim() == "3") || (ddlGrades4.Text.Trim() == "4") || (ddlGrades4.Text.Trim() == "5") || (ddlGrades4.Text.Trim() == "6") || (ddlGrades4.Text.Trim() == "7") || (ddlGrades4.Text.Trim() == "8") || (ddlGrades4.Text.Trim() == "9") || (ddlGrades4.Text.Trim() == "10") || (ddlGrades4.Text.Trim() == "11") || (ddlGrades4.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter4.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <> " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) NOT LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField4.Text == "Age")
                            {
                                if (ddlOperatorCharacter4.Text == "equals")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == ">")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == ">=")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == "<")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == "<=")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == "contains")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                            }
                        }
                        else if (ddlChooseField4.Text == "CampDropOff" || ddlChooseField4.Text == "CampPickUp" || ddlChooseField4.Text == "CurrentRegistrationForm" || ddlChooseField4.Text == "Dance" || ddlChooseField4.Text == "HaveReceivedChrist" || ddlChooseField4.Text == "MailingListInclude" || ddlChooseField4.Text == "ParentalConsentForm" || ddlChooseField4.Text == "PermissionToTransport" || ddlChooseField4.Text == "PromotionalRelease" || ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorCharacter4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorCharacter4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorCharacter4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " NOT like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if (ddlChooseField4.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " <> '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " > '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " < '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " NOT like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator4.Text == "Choose Operator") && (ddlOperatorCharacter4.Text == "Choose Operator") && (ddlOperatorBoolean4.Text != "Choose Operator"))
                    {
                        if (ddlChooseField4.Text == "MSHSChoir" || ddlChooseField4.Text == "ChildrensChoir" || ddlChooseField4.Text == "PerformingArts" || ddlChooseField4.Text == "Shakes" || ddlChooseField4.Text == "Singers" || ddlChooseField4.Text == "BibleStudy" || ddlChooseField4.Text == "SoccerIntraMurals" || ddlChooseField4.Text == "SoccerTEAMS" || ddlChooseField4.Text == "MondayNights" || ddlChooseField4.Text == "3on3Basketball" || ddlChooseField4.Text == "BasketballTEAMS" || ddlChooseField4.Text == "OutreachBasketball" || ddlChooseField4.Text == "HSBasketballLg" || ddlChooseField4.Text == "MSBasketballLg" || ddlChooseField4.Text == "SummerDayCamp" || ddlChooseField4.Text == "Baseball" || ddlChooseField4.Text == "SpecialEvents" || ddlChooseField4.Text == "ImpactUrbanSchools" || ddlChooseField4.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorBoolean4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " = '" + SearchBool4 + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " > '" + SearchBool4 + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " < '" + SearchBool4 + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " pl." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if ((ddlChooseField4.Text == "Grade") || (ddlChooseField4.Text == "Age"))
                        {
                            if (ddlChooseField4.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades4.Text.Trim() == "K") || (ddlGrades4.Text.Trim() == "k") || (ddlGrades4.Text.Trim() == "SV") || (ddlGrades4.Text.Contains("GR")) || (ddlGrades4.Text.Trim() == "sv") || (ddlGrades4.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorBoolean4.Text == "equals")
                                    {
                                        sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades4.Text.Trim() == "1") || (ddlGrades4.Text.Trim() == "2") || (ddlGrades4.Text.Trim() == "3") || (ddlGrades4.Text.Trim() == "4") || (ddlGrades4.Text.Trim() == "5") || (ddlGrades4.Text.Trim() == "6") || (ddlGrades4.Text.Trim() == "7") || (ddlGrades4.Text.Trim() == "8") || (ddlGrades4.Text.Trim() == "9") || (ddlGrades4.Text.Trim() == "10") || (ddlGrades4.Text.Trim() == "11") || (ddlGrades4.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean4.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField4.Text == "Age")
                            {
                                if (ddlOperatorBoolean4.Text == "equals")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == ">")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == ">=")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == "<")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == "<=")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == "contains")
                                {
                                    //Perfect.
                                    if (sql.Contains("si.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,si.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                            }
                        }
                        else if (ddlChooseField4.Text == "CampDropOff" || ddlChooseField4.Text == "CampPickUp" || ddlChooseField4.Text == "CurrentRegistrationForm" || ddlChooseField4.Text == "Dance" || ddlChooseField4.Text == "HaveReceivedChrist" || ddlChooseField4.Text == "MailingListInclude" || ddlChooseField4.Text == "ParentalConsentForm" || ddlChooseField4.Text == "PermissionToTransport" || ddlChooseField4.Text == "PromotionalRelease" || ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if (ddlChooseField4.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " ckp." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " > '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " < '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " si." + ddlChooseField4.Text + " like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                        }
                    }
                }
                if (ddlChooseField5.Text != "Choose Field")
                {
                    if ((ddlChooseOperator5.Text != "Choose Operator") && (ddlOperatorCharacter5.Text == "Choose Operator") && (ddlOperatorBoolean5.Text == "Choose Operator"))
                    {
                        if (ddlChooseField5.Text == "MSHSChoir" || ddlChooseField5.Text == "ChildrensChoir" || ddlChooseField5.Text == "PerformingArts" || ddlChooseField5.Text == "Shakes" || ddlChooseField5.Text == "Singers" || ddlChooseField5.Text == "BibleStudy" || ddlChooseField5.Text == "SoccerIntraMurals" || ddlChooseField5.Text == "SoccerTEAMS" || ddlChooseField5.Text == "MondayNights" || ddlChooseField5.Text == "3on3Basketball" || ddlChooseField5.Text == "BasketballTEAMS" || ddlChooseField5.Text == "OutreachBasketball" || ddlChooseField5.Text == "HSBasketballLg" || ddlChooseField5.Text == "MSBasketballLg" || ddlChooseField5.Text == "SummerDayCamp" || ddlChooseField5.Text == "Baseball" || ddlChooseField5.Text == "SpecialEvents" || ddlChooseField5.Text == "ImpactUrbanSchools" || ddlChooseField5.Text == "AcademicReadingSupport")
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " = '" + SearchBool5 + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " <> '" + SearchBool5 + "' ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " > '" + SearchBool5 + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " < '" + SearchBool5 + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " not like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if ((ddlChooseField5.Text == "Grade") || (ddlChooseField5.Text == "Age"))
                        {
                            if (ddlChooseField5.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades5.Text.Trim() == "K") || (ddlGrades5.Text.Trim() == "k") || (ddlGrades5.Text.Trim() == "SV") || (ddlGrades5.Text.Contains("GR")) || (ddlGrades5.Text.Trim() == "sv") || (ddlGrades5.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlChooseOperator5.Text == "equals")
                                    {
                                        sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades5.Text.Trim() == "1") || (ddlGrades5.Text.Trim() == "2") || (ddlGrades5.Text.Trim() == "3") || (ddlGrades5.Text.Trim() == "4") || (ddlGrades5.Text.Trim() == "5") || (ddlGrades5.Text.Trim() == "6") || (ddlGrades5.Text.Trim() == "7") || (ddlGrades5.Text.Trim() == "8") || (ddlGrades5.Text.Trim() == "9") || (ddlGrades5.Text.Trim() == "10") || (ddlGrades5.Text.Trim() == "11") || (ddlGrades5.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator5.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <> " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) NOT LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField5.Text == "Age")
                            {
                                if (ddlChooseOperator5.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) = " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) > " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) >= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) < " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) <= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) like " + txbSearchValue5.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField5.Text == "CampDropOff" || ddlChooseField5.Text == "CampPickUp" || ddlChooseField5.Text == "CurrentRegistrationForm" || ddlChooseField5.Text == "Dance" || ddlChooseField5.Text == "HaveReceivedChrist" || ddlChooseField5.Text == "MailingListInclude" || ddlChooseField5.Text == "ParentalConsentForm" || ddlChooseField5.Text == "PermissionToTransport" || ddlChooseField5.Text == "PromotionalRelease" || ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " NOT like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if (ddlChooseField5.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " <> '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " > '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " < '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " NOT like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator5.Text == "Choose Operator") && (ddlOperatorCharacter5.Text != "Choose Operator") && (ddlOperatorBoolean5.Text == "Choose Operator"))
                    {
                        if (ddlChooseField5.Text == "MSHSChoir" || ddlChooseField5.Text == "ChildrensChoir" || ddlChooseField5.Text == "PerformingArts" || ddlChooseField5.Text == "Shakes" || ddlChooseField5.Text == "Singers" || ddlChooseField5.Text == "BibleStudy" || ddlChooseField5.Text == "SoccerIntraMurals" || ddlChooseField5.Text == "SoccerTEAMS" || ddlChooseField5.Text == "MondayNights" || ddlChooseField5.Text == "3on3Basketball" || ddlChooseField5.Text == "BasketballTEAMS" || ddlChooseField5.Text == "OutreachBasketball" || ddlChooseField5.Text == "HSBasketballLg" || ddlChooseField5.Text == "MSBasketballLg" || ddlChooseField5.Text == "SummerDayCamp" || ddlChooseField5.Text == "Baseball" || ddlChooseField5.Text == "SpecialEvents" || ddlChooseField5.Text == "ImpactUrbanSchools" || ddlChooseField5.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorCharacter5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " = '" + SearchBool5 + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " <> '" + SearchBool5 + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " > '" + SearchBool5 + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " < '" + SearchBool5 + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " not like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if ((ddlChooseField5.Text == "Grade") || (ddlChooseField5.Text == "Age"))
                        {
                            if (ddlChooseField5.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades5.Text.Trim() == "K") || (ddlGrades5.Text.Trim() == "k") || (ddlGrades5.Text.Trim() == "SV") || (ddlGrades5.Text.Contains("GR")) || (ddlGrades5.Text.Trim() == "sv") || (ddlGrades5.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorCharacter5.Text == "equals")
                                    {
                                        sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades5.Text.Trim() == "1") || (ddlGrades5.Text.Trim() == "2") || (ddlGrades5.Text.Trim() == "3") || (ddlGrades5.Text.Trim() == "4") || (ddlGrades5.Text.Trim() == "5") || (ddlGrades5.Text.Trim() == "6") || (ddlGrades5.Text.Trim() == "7") || (ddlGrades5.Text.Trim() == "8") || (ddlGrades5.Text.Trim() == "9") || (ddlGrades5.Text.Trim() == "10") || (ddlGrades5.Text.Trim() == "11") || (ddlGrades5.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter5.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade <> 'GR' AND si.Grade <> 'G'  AND si.Grade <> 'GR11' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR10' AND si.Grade <> 'G11'  AND si.Grade <> 'G12' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField5.Text == "Age")
                            {
                                if (ddlOperatorCharacter5.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) = " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) > " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) >= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) < " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) <= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) like " + txbSearchValue5.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField5.Text == "CampDropOff" || ddlChooseField5.Text == "CampPickUp" || ddlChooseField5.Text == "CurrentRegistrationForm" || ddlChooseField5.Text == "Dance" || ddlChooseField5.Text == "HaveReceivedChrist" || ddlChooseField5.Text == "MailingListInclude" || ddlChooseField5.Text == "ParentalConsentForm" || ddlChooseField5.Text == "PermissionToTransport" || ddlChooseField5.Text == "PromotionalRelease" || ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorCharacter5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorCharacter5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorCharacter5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " NOT like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if (ddlChooseField5.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " <> '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " > '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " < '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " NOT like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator5.Text == "Choose Operator") && (ddlOperatorCharacter5.Text == "Choose Operator") && (ddlOperatorBoolean5.Text != "Choose Operator"))
                    {
                        if (ddlChooseField5.Text == "MSHSChoir" || ddlChooseField5.Text == "ChildrensChoir" || ddlChooseField5.Text == "PerformingArts" || ddlChooseField5.Text == "Shakes" || ddlChooseField5.Text == "Singers" || ddlChooseField5.Text == "BibleStudy" || ddlChooseField5.Text == "SoccerIntraMurals" || ddlChooseField5.Text == "SoccerTEAMS" || ddlChooseField5.Text == "MondayNights" || ddlChooseField5.Text == "3on3Basketball" || ddlChooseField5.Text == "BasketballTEAMS" || ddlChooseField5.Text == "OutreachBasketball" || ddlChooseField5.Text == "HSBasketballLg" || ddlChooseField5.Text == "MSBasketballLg" || ddlChooseField5.Text == "SummerDayCamp" || ddlChooseField5.Text == "Baseball" || ddlChooseField5.Text == "SpecialEvents" || ddlChooseField5.Text == "ImpactUrbanSchools" || ddlChooseField5.Text == "AcademicReadingSupport")
                        {
                            if (ddlOperatorBoolean5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " = '" + SearchBool5 + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " > '" + SearchBool5 + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " < '" + SearchBool5 + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " pl." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if ((ddlChooseField5.Text == "Grade") || (ddlChooseField5.Text == "Age"))
                        {
                            if (ddlChooseField5.Text == "Grade")
                            {
                                GradeFLAG = true;
                                if ((ddlGrades5.Text.Trim() == "K") || (ddlGrades5.Text.Trim() == "k") || (ddlGrades5.Text.Trim() == "SV") || (ddlGrades5.Text.Contains("GR")) || (ddlGrades5.Text.Trim() == "sv") || (ddlGrades5.Text.Contains("GR")) || (ddlGrades.Text.Trim() == "PreK"))
                                {
                                    sql = sql.Replace("CONVERT(INT,si.Grade) as 'Grade'", "si.Grade");
                                    group = group.Replace("CONVERT(INT,si.Grade)", "si.Grade");
                                    if (ddlOperatorBoolean5.Text == "equals")
                                    {
                                        sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades5.Text.Trim() == "1") || (ddlGrades5.Text.Trim() == "2") || (ddlGrades5.Text.Trim() == "3") || (ddlGrades5.Text.Trim() == "4") || (ddlGrades5.Text.Trim() == "5") || (ddlGrades5.Text.Trim() == "6") || (ddlGrades5.Text.Trim() == "7") || (ddlGrades5.Text.Trim() == "8") || (ddlGrades5.Text.Trim() == "9") || (ddlGrades5.Text.Trim() == "10") || (ddlGrades5.Text.Trim() == "11") || (ddlGrades5.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean5.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'GR12' AND si.Grade <> 'GR11' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) = " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'GR12' AND si.Grade <> 'GR11' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) > " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'GR12' AND si.Grade <> 'GR11' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) >= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'GR12' AND si.Grade <> 'GR11' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) < " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'GR12' AND si.Grade <> 'GR11' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) <= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'GR12' AND si.Grade <> 'GR11' AND si.Grade <> 'PreK') AND CONVERT(INT,si.Grade) LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField5.Text == "Age")
                            {
                                if (ddlOperatorBoolean5.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) = " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) > " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) >= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) < " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) <= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,si.Age) like " + txbSearchValue5.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField5.Text == "CampDropOff" || ddlChooseField5.Text == "CampPickUp" || ddlChooseField5.Text == "CurrentRegistrationForm" || ddlChooseField5.Text == "Dance" || ddlChooseField5.Text == "HaveReceivedChrist" || ddlChooseField5.Text == "MailingListInclude" || ddlChooseField5.Text == "ParentalConsentForm" || ddlChooseField5.Text == "PermissionToTransport" || ddlChooseField5.Text == "PromotionalRelease" || ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if (ddlChooseField5.Text == "CoreKid")
                        {
                            if (ddlOperatorBoolean5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " ckp." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " = '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " > '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " < '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " si." + ddlChooseField5.Text + " like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                        }
                    }
                }

                //Check for the presence of the Grade field in the select list.  If it is present,
                //hasn't been handled as a filter field, then account for special values that will cause
                //an error...RCM..3/30/12.
                if ((sql.Contains("si.Grade")) && (GradeFLAG))
                {
                    sql = sql + "AND (si.Grade <> 'K' AND si.Grade <> 'SV' AND si.Grade NOT LIKE '%G%' AND si.Grade <> 'GR12' AND si.Grade <> 'GR09' AND si.Grade <> 'GR11' AND si.Grade <> 'GR10' AND si.Grade <> 'PreK' AND si.Grade <> 'GR') ";
                }

                //Handles the ProgramsList table..to sort out students from volunteers.
                if (sql.Contains("pl."))
                {
                    sql = sql + " AND pl.student = 1 ";
                }

                //Right here. determine which group by fields if the view is custom...RCM..10/26/11.
                if (group != "")
                {
                    //Use the customer GroupBy fields..RCM...
                    sql = sql + group;
                }
                else
                {
                    //Adds a GROUP BY to the query...RCM..9/16/11.
                    if (ddlAdvancedSearchView.Text == "Select a View (Optional)")
                    {
                        sql = sql + "GROUP BY si.LastName + ',' + si.FirstName, si.MiddleName, si.address, si.city, si.state, si.zip, si.homephone, si.StudentCellPhone, si.School ";
                    }
                    else if (ddlAdvancedSearchView.Text == "Address Info")
                    {
                        sql = sql + "GROUP BY si.LastName + ',' + si.FirstName, si.MiddleName, si.address, si.city, si.state, si.zip, si.homephone, si.StudentCellPhone, si.School ";
                    }
                    else if (ddlAdvancedSearchView.Text == "Personal Info")
                    {
                        sql = sql + "GROUP BY si.LastName + ',' + si.FirstName, si.MiddleName, si.studentemail, si.dob, si.sex, si.church, si.healthconditions, si.notes ";
                    }
                    else if (ddlAdvancedSearchView.Text == "All Available Info")
                    {
                        sql = sql + "GROUP BY si.LastName + ',' + si.FirstName, si.MiddleName, si.address, si.city, si.state, si.zip, si.homephone, si.StudentCellPhone, si.StudentEmail, si.Grade, "
                                   + "si.dob, si.sex, si.Church, si.Voicepart, si.careergoal, si.healthconditions, si.notes, si.tshirtsize, si.biblestudyparticipation, si.havereceivedchrist, si.whenreceivedchrist ";
                    }
                }

                //ADD an ORDER BY to the query..RCM..9/16/11.
                sql = sql + "order by si.lastname + ',' + si.firstname ";
                //sql = sql + "order by si.lastname, si.firstname ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "UIF_PerformingArts.dbo.StudentInformation");

                //Clear the gridview..RCM.
                gvAdvancedSearch.DataSource = null;
                gvAdvancedSearch.DataBind();

                //Reload the gridview..RCM.
                //gvAdvancedSearch.DataSource = ds.Tables[0];
                //gvAdvancedSearch.DataBind();

                //-------------
                //gvAdvancedSearchResults.DataSource = cmd.ExecuteReader();
                //gvAdvancedSearchResults.DataBind();

                //-----------
                //Clear the gridview..RCM.
                gvAddressView.DataSource = null;
                gvAddressView.DataBind();

                //Clear the gridview..RCM.
                //gvAddressView.DataSource = null;
                //gvAddressView.DataBind();

                //Clear the gridview..RCM.
                gvPersonalView.DataSource = null;
                gvPersonalView.DataBind();

                //Clear the gridview..RCM.
                //gvProgramView.DataSource = null;
                //gvProgramView.DataBind();

                //Clear the gridview..RCM.
                //gvDiscipleshipMentorView.DataSource = null;
                //gvDiscipleshipMentorView.DataBind();

                //Clear the gridview..RCM.
                gvViewAllInfo.DataSource = null;
                gvViewAllInfo.DataBind();

                //Clear the gridview..RCM.
                gvCustomView.DataSource = null;
                gvCustomView.DataBind();

                if (group != "")//Custom View was selected, so use the generic gridview...RCM..10/28/11.
                {
                    //Reload the gridview..RCM.
                    gvCustomView.DataSource = ds.Tables[0];
                    gvCustomView.DataBind();
                }
                else
                {
                    //Match the gridviews up with the different desired views...RCM..8/5/11.
                    if (ddlAdvancedSearchView.Text == "Select a View (Optional)")
                    {
                        //Reload the gridview..RCM.
                        gvAddressView.DataSource = ds.Tables[0];
                        gvAddressView.DataBind();
                    }
                    else if (ddlAdvancedSearchView.Text == "Address Info")
                    {
                        //Reload the gridview..RCM.
                        gvAddressView.DataSource = ds.Tables[0];
                        gvAddressView.DataBind();
                    }
                    else if (ddlAdvancedSearchView.Text == "Personal Info")
                    {
                        //Reload the gridview..RCM.
                        gvPersonalView.DataSource = ds.Tables[0];
                        gvPersonalView.DataBind();
                    }
                    else if (ddlAdvancedSearchView.Text == "Program Info")
                    {
                        //Reload the gridview..RCM.
                        //gvProgramView.DataSource = ds.Tables[0];
                        //gvProgramView.DataBind();
                    }
                    else if (ddlAdvancedSearchView.Text == "DiscipleshipMentor Info")
                    {
                        //Reload the gridview..RCM.
                        //gvDiscipleshipMentorView.DataSource = ds.Tables[0];
                        //gvDiscipleshipMentorView.DataBind();
                    }
                    else if (ddlAdvancedSearchView.Text == "All Available Info")
                    {
                        //Reload the gridview..RCM.
                        gvViewAllInfo.DataSource = ds.Tables[0];
                        gvViewAllInfo.DataBind();
                    }
                }
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = lblErrorMessage.Text + " " + lkjl.Message.ToString() + " ";
            }
        }

        protected string DetermineCustomViewQuery(bool GradeFlag)
        {
            //Ryan C Manners...10/26/11.
            //Builds the sql query dynamically..
            string sql = "";
            try
            {
                sql = "select ";
                CheckBoxList chkbx = (CheckBoxList)Form.FindControl("cblCreateCustomView");
                for (int i = 0; i < chkbx.Items.Count; i++)
                {
                    if (chkbx.Items[i].Selected)
                    {
                        if (i > 0)
                        {
                            if ((chkbx.Items[i].Value.StartsWith("MSHSChoir")) || (chkbx.Items[i].Value.StartsWith("ChildrensChoir")) || (chkbx.Items[i].Value.StartsWith("PerformingArts")) || (chkbx.Items[i].Value.StartsWith("Shakes")) || (chkbx.Items[i].Value.StartsWith("Singers")) || (chkbx.Items[i].Value.StartsWith("MondayNights")) || (chkbx.Items[i].Value.StartsWith("BasketballTEAMS")) || (chkbx.Items[i].Value.StartsWith("3on3Basketball")) || (chkbx.Items[i].Value.StartsWith("BibleStudy")) || (chkbx.Items[i].Value.StartsWith("OutreachBasketball")) || (chkbx.Items[i].Value.StartsWith("HSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("MSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("SoccerTEAMS")) || (chkbx.Items[i].Value.StartsWith("SoccerIntraMurals")) || (chkbx.Items[i].Value.StartsWith("SpecialEvents")) || (chkbx.Items[i].Value.StartsWith("SummerDayCamp")) || (chkbx.Items[i].Value.StartsWith("Baseball")))
                            {
                                if (chkbx.Items[i].Value.StartsWith("3on3Basketball"))
                                {
                                    sql = sql + "," + "pl.[" + chkbx.Items[i].Value + "] ";
                                }
                                else
                                {
                                    if (chkbx.Items[i].Value.StartsWith("PerformingArtsAcademy"))
                                    {
                                        sql = sql + "," + "pl.performingarts ";
                                    }
                                    else
                                    {
                                        sql = sql + "," + "pl." + chkbx.Items[i].Value + " ";
                                    }
                                }
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Parent")))
                            {
                                sql = sql + "," + "pg.[" + chkbx.Items[i].Value + "] ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Work")))
                            {
                                sql = sql + "," + "pg.[" + chkbx.Items[i].Value + "] ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Cell")))
                            {
                                sql = sql + "," + "pg.[" + chkbx.Items[i].Value + "] ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Text")))
                            {
                                sql = sql + "," + "pg.[" + chkbx.Items[i].Value + "] ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("CoreKid")))
                            {
                                sql = sql + "," + "ckp.[" + chkbx.Items[i].Value + "] ";
                            }
                            else
                            {
                                if (chkbx.Items[i].Value.StartsWith("3on3Basketball"))
                                {
                                    sql = sql + "," + "si.[" + chkbx.Items[i].Value + "] ";
                                }
                                else
                                {
                                    sql = sql + "," + "si." + chkbx.Items[i].Value + " ";
                                }
                            }
                        }
                        else if (i == 0)
                        {
                            if ((chkbx.Items[i].Value.StartsWith("MSHSChoir")) || (chkbx.Items[i].Value.StartsWith("ChildrensChoir")) || (chkbx.Items[i].Value.StartsWith("PerformingArts")) || (chkbx.Items[i].Value.StartsWith("Shakes")) || (chkbx.Items[i].Value.StartsWith("Singers")) || (chkbx.Items[i].Value.StartsWith("MondayNights")) || (chkbx.Items[i].Value.StartsWith("BasketballTEAMS")) || (chkbx.Items[i].Value.StartsWith("3on3Basketball")) || (chkbx.Items[i].Value.StartsWith("BibleStudy")) || (chkbx.Items[i].Value.StartsWith("OutreachBasketball")) || (chkbx.Items[i].Value.StartsWith("HSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("MSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("SoccerTEAMS")) || (chkbx.Items[i].Value.StartsWith("SoccerIntraMurals")) || (chkbx.Items[i].Value.StartsWith("SpecialEvents")) || (chkbx.Items[i].Value.StartsWith("SummerDayCamp")) || (chkbx.Items[i].Value.StartsWith("Baseball")))
                            {
                                if (chkbx.Items[i].Value.StartsWith("3on3Basketball"))
                                {
                                    sql = sql + "pl.[" + chkbx.Items[i].Value + "] ";
                                }
                                else
                                {
                                    sql = sql + "pl." + chkbx.Items[i].Value + " ";
                                }
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("CoreKid")))
                            {
                                sql = sql + "ckp.[" + chkbx.Items[i].Value + "] ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Parent")))
                            {
                                sql = sql + "," + "pg.[" + chkbx.Items[i].Value + "] ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Work")))
                            {
                                sql = sql + "," + "pg.[" + chkbx.Items[i].Value + "] ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Cell")))
                            {
                                sql = sql + "," + "pg.[" + chkbx.Items[i].Value + "] ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Text")))
                            {
                                sql = sql + "," + "pg.[" + chkbx.Items[i].Value + "] ";
                            }
                            else
                            {
                                sql = sql + "si." + chkbx.Items[i].Value + " ";
                            }
                        }
                    }
                }
            }
            catch (Exception lkjlkjl)
            {
                string lkjlssskj = "";
            }
            finally
            {

            }

            //Modify the sql if the Grade needs to be cast as an Integer.. Otherwise, leave alone.
            if (GradeFlag)
            {
                //Special case fields.. handling as INTEGERS...RCM...3/9/12.
                sql = sql.Replace("si.Grade", "CONVERT(INT,si.Grade) as 'Grade'");
                sql = sql.Replace("si.Zip", "CONVERT(INT,si.Zip) as 'Zip'");
                sql = sql.Replace("si.Age", "CONVERT(INT,si.Age) as 'Age'");
            }
            return sql;
        }

        protected void cmbAbsences_Click(object sender, EventArgs e)
        {

            con.Open();

            string selectSQL = "select a.Lastname, a.Firstname, COUNT(a.Attended) as '# of Absences', pas.HomePhone, pas.StudentCellPhone "
                            + "from StudentprogramAttendance a "
                            + "LEFT OUTER JOIN PerformingArtsAcademyStudents pas "
                            + "ON (a.LastName = pas.LastName AND a.FirstName = pas.FirstName) "
                            + "where a.Attended = 0 "
                            + "and pas.MSHSChoir = 1 "
                            + "and day > '2011-01-01' "
                            + "group by a.LastName, a.FirstName, pas.HomePhone, pas.StudentCellPhone "
                            + "order by COUNT(a.Attended) desc, a.LastName, a.FirstName ";

            //Perform database lookup based on the chosen child..RCM..
            SqlCommand cmd = new SqlCommand(selectSQL);

            cmd.Connection = con;
            gvAdvancedSearch.DataSource = cmd.ExecuteReader();
            gvAdvancedSearch.DataBind();
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

            con.Open();

            try
            {
                string selectSQL = "select Day " +
                                   "from dbo.studentprogramattendance " +
                                   "where lastname=@lastname " +
                                   "and firstname=@firstname " +
                                   "and program=@program " +
                                   "group by Day ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);
//                cmd.Parameters.Add(new SqlParameter("@lastname", ddlStudents.SelectedValue.Substring(0, ddlStudents.SelectedValue.IndexOf(","))));
//                cmd.Parameters.Add(new SqlParameter("@firstname", ddlStudents.SelectedValue.Substring(ddlStudents.SelectedValue.IndexOf(",") + 1, ddlStudents.SelectedValue.Length - (ddlStudents.SelectedValue.IndexOf(",") + 1))));
//                cmd.Parameters.Add(new SqlParameter("@program", ddlProgram.Text));
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    do
                    {
 //                       ddlDay.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                }
            }
            catch (Exception lkjlla)
            {


            }
        }

        protected void cmbSearch_Click(object sender, EventArgs e)
        {
            con.Open();

            //General, Quick Search..RCM..7/22/11.
            try
            {
                //string selectSQL = "SELECT LastName + ',' + FirstName as 'Name', Address as 'Address/ParentGuard', City as 'City/ParentRelationship', State as 'State/Email', Zip, HomePhone, studentcellphone as 'CellPhone', School "
                string selectSQL = "SELECT LastName + ',' + FirstName + ' (' + MiddleName + ')' as 'Name', Address, City, State, Zip, HomePhone, studentcellphone as 'CellPhone', School "
                                 + ",Studentemail as 'Email', grade, dob, sex "
                                 //",church, careergoal, healthconditions, notes, tshirtsize "
                                 + "FROM UIF_PerformingArts.dbo.StudentInformation "
                                 + "WHERE FREETEXT(*, '" + txbSearch.Text.Trim() + "') "
                                 + "UNION "
                                 + "select StudentLastName + ',' + StudentFirstName + ' (' + StudentMiddleName + ')' as 'Name', ParentGuardian1, ParentGuardianRelationship1, ParentGuardian1Email, WorkPhone1, CellPhone1, ParentGuardian2, ParentGuardian2Relationship, CellPhone2, EmergencyContact, EmergRelationship, EmergencyContactPhone "
                                 + "FROM UIF_PerformingArts.dbo.parentguardiancontactinformation "
                                 + "WHERE FREETEXT(*, '" + txbSearch.Text.Trim() + "') ";
                                 //+ "UNION "
                                 //+ "select LastName + ',' + FirstName as 'Name', ParentGuardian1, ParentGuardianRelationship1, ParentGuardian1Email, WorkPhone1, CellPhone1, ParentGuardian2, ParentGuardian2Relationship, CellPhone2, EmergencyContact, EmergRelationship, EmergencyContactPhone "
                                 //+ "FROM UIF_PerformingArts.dbo.programslist "
                                 //+ "WHERE FREETEXT(*, '" + txbSearch.Text.Trim() + "') ";
                                 //+ "GROUP BY lastname, firstname "
                                 //+ "ORDER BY lastname, firstname ";
                 
                SqlCommand cmd = new SqlCommand(selectSQL);
                cmd.Connection = con;
                gvAdvancedSearchResults.DataSource = cmd.ExecuteReader();
                gvAdvancedSearchResults.DataBind();

                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkkjkjl)
            {

            }
        }

        protected void LastName_Click(object sender, ImageClickEventArgs e)
        {
            string lkjlkj = "";
        }


        protected void gvAdvancedSearchResults_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int i = 0;

            string currentCommand = e.CommandName;
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            string StudentName = gvAdvancedSearchResults.DataKeys[currentRowIndex].Value.ToString();

            //string lkj = "Command: " + currentCommand;
            //string asssv = "Row Index: " + currentRowIndex.ToString;
            //string lkjaa = "Product ID: " + ProductID;

            Response.Clear();
            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + StudentName.Substring(0, StudentName.IndexOf(","))
                            + "&StudentFirstName=" + StudentName.Substring(StudentName.IndexOf(",") + 1, ((StudentName.IndexOf("(")) - (StudentName.IndexOf(",") + 2))) 
                            + "&StudentMiddleName=" + StudentName.Substring(StudentName.IndexOf("(") + 1, ((StudentName.IndexOf(")")) - (StudentName.IndexOf("("))) - 1) 
                            + "&Dept=" + Request.QueryString["Dept"]);


//            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(","))
//                            + "&StudentFirstName=" + ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ((ddlNames.SelectedValue.IndexOf("(")) - (ddlNames.SelectedValue.IndexOf(",") + 2))) 
//                            + "&StudentMiddleName=" + ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf("(") + 1, ((ddlNames.SelectedValue.IndexOf(")")) - (ddlNames.SelectedValue.IndexOf("("))) - 1) 
//                            + "&Dept=" + Request.QueryString["Dept"]);
        
        
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

        protected void cmb_Click(object sender, EventArgs e)
        {

        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            //Ryan C Manners...6/13/11.
            //Export the contents of the gridview to an Excel object for use...RCM..
            if (gvAddressView.Rows.Count != 0)
            {
                gvAddressView.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvAddressView, Response);
            }
            else if (gvAdvancedSearchResults.Rows.Count != 0)
            {
                gvAdvancedSearchResults.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvAdvancedSearchResults, Response);
            }
            else if (gvPersonalView.Rows.Count != 0)
            {
                gvPersonalView.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvPersonalView, Response);
            }
            else if (gvAdvancedSearch.Rows.Count != 0)
            {
                gvAdvancedSearch.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvAdvancedSearch, Response);
            }
            //else if (gvProgramView.Rows.Count != 0)
            //{
            //    ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            //    ExcelExport.ExportGridView(gvProgramView, Response);
            //}
            //else if (gvDiscipleshipMentorView.Rows.Count != 0)
            //{
            //    ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            //    ExcelExport.ExportGridView(gvDiscipleshipMentorView, Response);
            //}
            else if (gvViewAllInfo.Rows.Count != 0)
            {
                gvViewAllInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvViewAllInfo, Response);
            }
            else if (gvCustomView.Rows.Count != 0)
            {
                gvCustomView.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvCustomView, Response);
            }
        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("StudentAttendanceReporting.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void chbAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAdvancedSearch.Checked)
            {
                chbAdvancedSearch.Checked = true;
                Response.Clear();
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
                chbAdvancedSearch.Text = "Advanced Search";
            }
            else
            {
                StartAdvancedSearch();
                chbAdvancedSearch.Text = "Quick Search";
            }
        }

        protected void imbAdvancedSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (chbAdvancedSearch.Checked)
            {
                chbAdvancedSearch.Checked = false;
                StartAdvancedSearch();
                chbAdvancedSearch.Text = "Quick Search";
            }
            else
            {
                Response.Clear();
                Response.Redirect("StudentAttendanceReporting.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
                chbAdvancedSearch.Text = "Advanced Search";
            }
        }

        protected void gvAdvancedSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        protected void gvAdvancedSearchResults_RowCommand(object sender, EventArgs e)
        {
            string lkjlkj = "";
        }

        protected void txbSearchValue2_TextChanged(object sender, EventArgs e)
        {
            rblNumber2.Enabled = true;
        }

        protected void ddlAdvancedSearchView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvViewAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void gvViewAllInfo_RowCommand(object sender, EventArgs e)
        {
            string lkjlkj = "";
        }

        protected void gvViewAllInfo_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int i = 0;

            string currentCommand = e.CommandName;
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            string StudentName = gvViewAllInfo.DataKeys[currentRowIndex].Value.ToString();

            Response.Clear();
            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + StudentName.Substring(0, StudentName.IndexOf(","))
                            + "&StudentFirstName=" + StudentName.Substring(StudentName.IndexOf(",") + 1, StudentName.Length - (StudentName.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);

            //Response.Clear();
            //Response.Redirect("StudentAttendanceReporting.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
        }


        protected void gvAddressView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void gvAddressView_RowCommand(object sender, EventArgs e)
        {
            string lkjlkj = "";
        }

        protected void gvAddressView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int i = 0;

            string currentCommand = e.CommandName;
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            string VolunteerName = gvAddressView.DataKeys[currentRowIndex].Value.ToString();

//            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + VolunteerName.Substring(0, VolunteerName.IndexOf(","))
//                            + "&StudentFirstName=" + VolunteerName.Substring(VolunteerName.IndexOf(",") + 1, VolunteerName.Length - (VolunteerName.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);

            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + VolunteerName.Substring(0, VolunteerName.IndexOf(","))
                            + "&StudentFirstName=" +VolunteerName.Substring(VolunteerName.IndexOf(",") + 1, ((VolunteerName.IndexOf("(")) - (VolunteerName.IndexOf(",") + 2)))
                            + "&StudentMiddleName=" + VolunteerName.Substring(VolunteerName.IndexOf("(") + 1, ((VolunteerName.IndexOf(")")) - (VolunteerName.IndexOf("("))) - 1)
                            + "&Dept=" + Request.QueryString["Dept"]);
        
        }


        protected void gvPersonalView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void gvPersonalView_RowCommand(object sender, EventArgs e)
        {
            string lkjlkj = "";
        }

        protected void gvPersonalView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int i = 0;

            string currentCommand = e.CommandName;
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            string VolunteerName = gvPersonalView.DataKeys[currentRowIndex].Value.ToString();

//            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + VolunteerName.Substring(0, VolunteerName.IndexOf(","))
//                            + "&StudentFirstName=" + VolunteerName.Substring(VolunteerName.IndexOf(",") + 1, VolunteerName.Length - (VolunteerName.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);

            Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + VolunteerName.Substring(0, VolunteerName.IndexOf(","))
                            + "&StudentFirstName=" + VolunteerName.Substring(VolunteerName.IndexOf(",") + 1, ((VolunteerName.IndexOf("(")) - (VolunteerName.IndexOf(",") + 2)))
                            + "&StudentMiddleName=" + VolunteerName.Substring(VolunteerName.IndexOf("(") + 1, ((VolunteerName.IndexOf(")")) - (VolunteerName.IndexOf("("))) - 1)
                            + "&Dept=" + Request.QueryString["Dept"]);
        
        }

        protected void lbCreateCustomView_Click(object sender, EventArgs e)
        {
            //if (chbBasketballTEAMS.Checked)
            //{
            //cmbProgramManagement.Enabled = true;
            //cmbProgramManagement.Visible = true;
            //cmbProgramManagement.Style.Add("z-index", "99999");

            //cmbProgramManageCancel.Enabled = true;
            //cmbProgramManageCancel.Visible = true;
            //cmbProgramManageCancel.Style.Add("z-index", "99999");

            //lblProgramManagement.Style.Add("z-index", "99999");
            //lblProgramManagement.Visible = true;

            cblCreateCustomView.Style.Add("z-index", "99999");
            cblCreateCustomView.Visible = true;

            lblCustomView.Style.Add("z-index", "99999");
            lblCustomView.Visible = true;

            cmbConfirmCustomViewFields.Style.Add("z-index", "99999");
            cmbConfirmCustomViewFields.Visible = true;

            cmbClearViewFields.Style.Add("z-index", "99999");
            cmbClearViewFields.Visible = true;

            cmbCancelCustomViewFields.Style.Add("z-index", "99999");
            cmbCancelCustomViewFields.Visible = true;

            //rblProgramManagement.Style.Add("z-index", "99999");
            //rblProgramManagement.Visible = true;

            pnlCustomView.Style.Add("z-index", "9999");
            pnlCustomView.Visible = true;
            //}
        }

        protected void gvCustomView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void gvCustomView_RowCommand(object sender, EventArgs e)
        {
            string lkjlkj = "";
        }

        protected void gvCustomView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            //int i = 0;

            //string currentCommand = e.CommandName;
            //int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            //string VolunteerName = gvViewAllInfo.DataKeys[currentRowIndex].Value.ToString();

            //Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + VolunteerName.Substring(0, VolunteerName.IndexOf(","))
            //                + "&VolunteerFirstName=" + VolunteerName.Substring(VolunteerName.IndexOf(",") + 1, VolunteerName.Length - (VolunteerName.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cblCreateCustomView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string llkjaaa = "";

        }

        protected void cmbCancelCustomViewFields_Click1(object sender, EventArgs e)
        {
            cblCreateCustomView.ClearSelection();
            pnlCustomView.Visible = false;
            cblCreateCustomView.Visible = false;
            cmbCancelCustomViewFields.Visible = false;
            cmbConfirmCustomViewFields.Visible = false;
            cmbClearViewFields.Visible = false;
            lblCustomView.Visible = false;
        }

        protected void cmbConfirmCustomViewFields_Click1(object sender, EventArgs e)
        {
            //ChooseCustomView = true;
            pnlCustomView.Visible = false;
            cblCreateCustomView.Visible = false;
            cmbConfirmCustomViewFields.Visible = false;
            lblCustomView.Visible = false;
        }

        protected void cmbClearViewFields_Click1(object sender, EventArgs e)
        {
            cblCreateCustomView.ClearSelection();
        }

        protected void cblCreateCustomView_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string lkjlkj = "";
        }

        protected void cmbCreateCustomView_Click(object sender, EventArgs e)
        {
            //if (chbBasketballTEAMS.Checked)
            //{
            //cmbProgramManagement.Enabled = true;
            //cmbProgramManagement.Visible = true;
            //cmbProgramManagement.Style.Add("z-index", "99999");

            //cmbProgramManageCancel.Enabled = true;
            //cmbProgramManageCancel.Visible = true;
            //cmbProgramManageCancel.Style.Add("z-index", "99999");

            //lblProgramManagement.Style.Add("z-index", "99999");
            //lblProgramManagement.Visible = true;

            cblCreateCustomView.Style.Add("z-index", "99999");
            cblCreateCustomView.Visible = true;

            lblCustomView.Style.Add("z-index", "99999");
            lblCustomView.Visible = true;

            cmbConfirmCustomViewFields.Style.Add("z-index", "99999");
            cmbConfirmCustomViewFields.Visible = true;

            cmbClearViewFields.Style.Add("z-index", "99999");
            cmbClearViewFields.Visible = true;

            cmbCancelCustomViewFields.Style.Add("z-index", "99999");
            cmbCancelCustomViewFields.Visible = true;

            //rblProgramManagement.Style.Add("z-index", "99999");
            //rblProgramManagement.Visible = true;

            pnlCustomView.Style.Add("z-index", "9999");
            pnlCustomView.Visible = true;

            cmbConfirmCustomViewFields.TabIndex = 1;
            //}
        }

        protected void ddlChooseField_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset the Operators..RCM.4/2/12..
            ddlOperatorBoolean.Text = "Choose Operator";
            ddlOperatorCharacter.Text = "Choose Operator";
            ddlChooseOperator.Text = "Choose Operator";

            //Reset the SearchValues..RCM..4/2/12..
            ddlGrades.Text = "Select a Grade";
            ddlSearchValueBool.Text = "Select a value";
            txbSearchValue.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField.Text == "MSHSChoir") || (ddlChooseField.Text == "ChildrensChoir") || (ddlChooseField.Text == "PerformingArts") || (ddlChooseField.Text == "Shakes") || (ddlChooseField.Text == "Singers") || (ddlChooseField.Text == "OutreachBasketball") || (ddlChooseField.Text == "BasketballTEAMS") || (ddlChooseField.Text == "HSBasketballLg") || (ddlChooseField.Text == "MSBasketballLg") || (ddlChooseField.Text == "3on3Basketball") || (ddlChooseField.Text == "SoccerIntraMurals") || (ddlChooseField.Text == "SoccerTEAMS") || (ddlChooseField.Text == "Baseball") || (ddlChooseField.Text == "BibleStudy") || (ddlChooseField.Text == "MondayNights") || (ddlChooseField.Text == "SummerDayCamp") || (ddlChooseField.Text == "ImpactUrbanSchools") || (ddlChooseField.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = true;
                ddlOperatorBoolean.Enabled = true;
                ddlOperatorCharacter.Visible = false;
                ddlChooseOperator.Visible = false;

                //Adding once a Program has been selected...
                //So the user can drill down into a Program.
                ddlChooseField2.Items.Add("Section");
                ddlChooseField3.Items.Add("Section");
                ddlChooseField4.Items.Add("Section");
                ddlChooseField5.Items.Add("Section");
            }
            else if ((ddlChooseField.Text == "All Programs") || (ddlChooseField.Text == "AthleticsDept") || (ddlChooseField.Text == "PerformingArtsDept") || (ddlChooseField.Text == "EducationDept"))
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = true;
                ddlOperatorBoolean.Enabled = true;
                ddlOperatorCharacter.Visible = false;
                ddlChooseOperator.Visible = false;
            }
            else if ((ddlChooseField.Text == "Grade") || (ddlChooseField.Text == "Age"))
            {
                if (ddlChooseField.Text == "Age")
                {

                }

                //Manage the search field..
                ddlGrades.Visible = true;
                ddlGrades.Enabled = false;

                ddlSearchValueBool.Visible = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator.Visible = true;
                ddlChooseOperator.Enabled = true;
                ddlOperatorBoolean.Visible = false;
                ddlOperatorCharacter.Visible = false;
            }
            else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = true;
                ddlOperatorBoolean.Enabled = true;
                ddlOperatorCharacter.Visible = false;
                ddlChooseOperator.Visible = false;
            }
            else if (ddlChooseField.Text == "CoreKid")
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = true;
                ddlOperatorBoolean.Enabled = true;
                ddlOperatorCharacter.Visible = false;
                ddlChooseOperator.Visible = false;
            }
            else if (ddlChooseField.Text == "ScrubbedDate")
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlGrades.Enabled = false;
                ddlSearchValueBool.Visible = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator.Visible = true;
                ddlChooseOperator.Enabled = true;
                ddlOperatorBoolean.Visible = false;
                ddlOperatorCharacter.Visible = false;
            }
            else
            {
                //Manage the search field..
                ddlSearchValueBool.Visible = false;
                ddlGrades.Visible = false;
                txbSearchValue.Visible = true;
                txbSearchValue.Enabled = true;

                //Manage the operators...RCM..
                ddlOperatorCharacter.Visible = true;
                ddlOperatorCharacter.Enabled = true;
                ddlOperatorBoolean.Visible = false;
                ddlChooseOperator.Visible = false;
            }
        }

        protected void ddlChooseField2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset the Operators..RCM.4/2/12..
            ddlOperatorBoolean2.Text = "Choose Operator";
            ddlOperatorCharacter2.Text = "Choose Operator";
            ddlChooseOperator2.Text = "Choose Operator";

            //Reset the SearchValues..RCM..4/2/12..
            ddlGrades2.Text = "Select a Grade";
            ddlSearchValue2Bool.Text = "Select a value";
            txbSearchValue2.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField2.Text == "MSHSChoir") || (ddlChooseField2.Text == "ChildrensChoir") || (ddlChooseField2.Text == "PerformingArts") || (ddlChooseField2.Text == "Shakes") || (ddlChooseField2.Text == "Singers") || (ddlChooseField2.Text == "OutreachBasketball") || (ddlChooseField2.Text == "BasketballTEAMS") || (ddlChooseField2.Text == "HSBasketballLg") || (ddlChooseField2.Text == "MSBasketballLg") || (ddlChooseField2.Text == "3on3Basketball") || (ddlChooseField2.Text == "SoccerIntraMurals") || (ddlChooseField2.Text == "SoccerTEAMS") || (ddlChooseField2.Text == "Baseball") || (ddlChooseField2.Text == "BibleStudy") || (ddlChooseField2.Text == "MondayNights") || (ddlChooseField2.Text == "SummerDayCamp") || (ddlChooseField2.Text == "ImpactUrbanSchools") || (ddlChooseField2.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlGrades2.Visible = false;
                ddlSearchValue2Bool.Visible = true;
                ddlSearchValue2Bool.Enabled = false;
                txbSearchValue2.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean2.Visible = true;
                ddlOperatorBoolean2.Enabled = true;
                ddlOperatorCharacter2.Visible = false;
                ddlChooseOperator2.Visible = false;
            }
            else if (ddlChooseField2.Text == "Grade")
            {
                //Manage the search field..
                ddlGrades2.Visible = true;
                ddlGrades2.Enabled = false;
                ddlSearchValue2Bool.Visible = false;
                txbSearchValue2.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator2.Visible = true;
                ddlChooseOperator2.Enabled = true;
                ddlOperatorBoolean2.Visible = false;
                ddlOperatorCharacter2.Visible = false;
            }
            else if (ddlChooseField2.Text == "CampDropOff" || ddlChooseField2.Text == "CampPickUp" || ddlChooseField2.Text == "CurrentRegistrationForm" || ddlChooseField2.Text == "Dance" || ddlChooseField2.Text == "HaveReceivedChrist" || ddlChooseField2.Text == "MailingListInclude" || ddlChooseField2.Text == "ParentalConsentForm" || ddlChooseField2.Text == "PermissionToTransport" || ddlChooseField2.Text == "PromotionalRelease" || ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlGrades2.Visible = false;
                ddlSearchValue2Bool.Visible = true;
                ddlSearchValue2Bool.Enabled = false;
                txbSearchValue2.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean2.Visible = true;
                ddlOperatorBoolean2.Enabled = true;
                ddlOperatorCharacter2.Visible = false;
                ddlChooseOperator2.Visible = false;
            }
            else if (ddlChooseField2.Text == "CoreKid")
            {
                //Manage the search field..
                ddlGrades2.Visible = false;
                ddlSearchValue2Bool.Visible = true;
                ddlSearchValue2Bool.Enabled = false;
                txbSearchValue2.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean2.Visible = true;
                ddlOperatorBoolean2.Enabled = true;
                ddlOperatorCharacter2.Visible = false;
                ddlChooseOperator2.Visible = false;
            }
            else if (ddlChooseField2.Text == "ScrubbedDate")
            {
                //Manage the search field..
                ddlGrades2.Visible = false;
                ddlGrades2.Enabled = false;
                ddlSearchValue2Bool.Visible = false;
                txbSearchValue2.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator2.Visible = true;
                ddlChooseOperator2.Enabled = true;
                ddlOperatorBoolean2.Visible = false;
                ddlOperatorCharacter2.Visible = false;
            }
            else if (ddlChooseField2.Text == "Section")
            {
                //Manage the search field..
                
                //ddlGrades2.Visible = true;
                //ddlGrades2.Enabled = false;

                ddlProgramSection.Visible = true;
                ddlProgramSection.Enabled = false;
                
                ddlSearchValue2Bool.Visible = false;
                txbSearchValue2.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator2.Visible = true;
                ddlChooseOperator2.Enabled = true;
                ddlOperatorBoolean2.Visible = false;
                ddlOperatorCharacter2.Visible = false;
            }
            else
            {
                //Manage the search field..
                ddlSearchValue2Bool.Visible = false;
                ddlGrades2.Visible = false;
                txbSearchValue2.Visible = true;
                txbSearchValue2.Enabled = true;

                //Manage the operators...RCM..
                ddlOperatorCharacter2.Visible = true;
                ddlOperatorCharacter2.Enabled = true;
                ddlOperatorBoolean2.Visible = false;
                ddlChooseOperator2.Visible = false;
            }
        }

        protected void ddlSearchValue2Bool_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber2.Enabled = true;
            //ddlSearchValue2Bool.Items.Remove("Select a value");
        }

        protected void ddlSearchValue5Bool_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlSearchValue5Bool.Items.Remove("Select a value");
        }

        protected void ddlSearchValue4Bool_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber4.Enabled = true;
            //ddlSearchValue4Bool.Items.Remove("Select a value");
        }

        protected void ddlSearchValue3Bool_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber3.Enabled = true;
            //ddlSearchValue3Bool.Items.Remove("Select a value");
        }

        protected void ddlSearchValueBool_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber1.Enabled = true;
            //ddlSearchValueBool.Items.Remove("Select a value");
        }

        protected void ddlChooseField3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset the Operators..RCM.4/2/12..
            ddlOperatorBoolean3.Text = "Choose Operator";
            ddlOperatorCharacter3.Text = "Choose Operator";
            ddlChooseOperator3.Text = "Choose Operator";

            //Reset the SearchValues..RCM..4/2/12..
            ddlGrades3.Text = "Select a Grade";
            ddlSearchValue3Bool.Text = "Select a value";
            txbSearchValue3.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField3.Text == "MSHSChoir") || (ddlChooseField3.Text == "ChildrensChoir") || (ddlChooseField3.Text == "PerformingArts") || (ddlChooseField3.Text == "Shakes") || (ddlChooseField3.Text == "Singers") || (ddlChooseField3.Text == "OutreachBasketball") || (ddlChooseField3.Text == "BasketballTEAMS") || (ddlChooseField3.Text == "HSBasketballLg") || (ddlChooseField3.Text == "MSBasketballLg") || (ddlChooseField3.Text == "3on3Basketball") || (ddlChooseField3.Text == "SoccerIntraMurals") || (ddlChooseField3.Text == "SoccerTEAMS") || (ddlChooseField3.Text == "Baseball") || (ddlChooseField3.Text == "BibleStudy") || (ddlChooseField3.Text == "MondayNights") || (ddlChooseField3.Text == "SummerDayCamp") || (ddlChooseField3.Text == "ImpactUrbanSchools") || (ddlChooseField3.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlGrades3.Visible = false;
                ddlSearchValue3Bool.Visible = true;
                ddlSearchValue3Bool.Enabled = false;
                txbSearchValue3.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean3.Visible = true;
                ddlOperatorBoolean3.Enabled = true;
                ddlOperatorCharacter3.Visible = false;
                ddlChooseOperator3.Visible = false;
            }
            else if (ddlChooseField3.Text == "Grade")
            {
                //Manage the search field..
                ddlGrades3.Visible = true;
                ddlGrades3.Enabled = false;
                ddlSearchValue3Bool.Visible = false;
                txbSearchValue3.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator3.Visible = true;
                ddlChooseOperator3.Enabled = true;
                ddlOperatorBoolean3.Visible = false;
                ddlOperatorCharacter3.Visible = false;
            }
            else if (ddlChooseField3.Text == "CampDropOff" || ddlChooseField3.Text == "CampPickUp" || ddlChooseField3.Text == "CurrentRegistrationForm" || ddlChooseField3.Text == "Dance" || ddlChooseField3.Text == "HaveReceivedChrist" || ddlChooseField3.Text == "MailingListInclude" || ddlChooseField3.Text == "ParentalConsentForm" || ddlChooseField3.Text == "PermissionToTransport" || ddlChooseField3.Text == "PromotionalRelease" || ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlGrades3.Visible = false;
                ddlSearchValue3Bool.Visible = true;
                ddlSearchValue3Bool.Enabled = false;
                txbSearchValue3.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean3.Visible = true;
                ddlOperatorBoolean3.Enabled = true;
                ddlOperatorCharacter3.Visible = false;
                ddlChooseOperator3.Visible = false;
            }
            else if (ddlChooseField3.Text == "CoreKid")
            {
                //Manage the search field..
                ddlGrades3.Visible = false;
                ddlSearchValue3Bool.Visible = true;
                ddlSearchValue3Bool.Enabled = false;
                txbSearchValue3.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean3.Visible = true;
                ddlOperatorBoolean3.Enabled = true;
                ddlOperatorCharacter3.Visible = false;
                ddlChooseOperator3.Visible = false;
            }
            else if (ddlChooseField3.Text == "ScrubbedDate")
            {
                //Manage the search field..
                ddlGrades3.Visible = false;
                ddlGrades3.Enabled = false;
                ddlSearchValue3Bool.Visible = false;
                txbSearchValue3.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator3.Visible = true;
                ddlChooseOperator3.Enabled = true;
                ddlOperatorBoolean3.Visible = false;
                ddlOperatorCharacter3.Visible = false;
            }
            else
            {
                //Manage the search field..
                ddlSearchValue3Bool.Visible = false;
                ddlGrades3.Visible = false;
                txbSearchValue3.Visible = true;
                txbSearchValue3.Enabled = true;

                //Manage the operators...RCM..
                ddlOperatorCharacter3.Visible = true;
                ddlOperatorCharacter3.Enabled = true;
                ddlOperatorBoolean3.Visible = false;
                ddlChooseOperator3.Visible = false;
            }
        }
        
        protected void ddlChooseField4_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //Reset the Operators..RCM.4/2/12..
            ddlOperatorBoolean4.Text = "Choose Operator";
            ddlOperatorCharacter4.Text = "Choose Operator";
            ddlChooseOperator4.Text = "Choose Operator";

            //Reset the SearchValues..RCM..4/2/12..
            ddlGrades4.Text = "Select a Grade";
            ddlSearchValue4Bool.Text = "Select a value";
            txbSearchValue4.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField4.Text == "MSHSChoir") || (ddlChooseField4.Text == "ChildrensChoir") || (ddlChooseField4.Text == "PerformingArts") || (ddlChooseField4.Text == "Shakes") || (ddlChooseField4.Text == "Singers") || (ddlChooseField4.Text == "OutreachBasketball") || (ddlChooseField4.Text == "BasketballTEAMS") || (ddlChooseField4.Text == "HSBasketballLg") || (ddlChooseField4.Text == "MSBasketballLg") || (ddlChooseField4.Text == "3on3Basketball") || (ddlChooseField4.Text == "SoccerIntraMurals") || (ddlChooseField4.Text == "SoccerTEAMS") || (ddlChooseField4.Text == "Baseball") || (ddlChooseField4.Text == "BibleStudy") || (ddlChooseField4.Text == "MondayNights") || (ddlChooseField4.Text == "SummerDayCamp") || (ddlChooseField4.Text == "ImpactUrbanSchools") || (ddlChooseField4.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlGrades4.Visible = false;
                ddlSearchValue4Bool.Visible = true;
                ddlSearchValue4Bool.Enabled = false;
                txbSearchValue4.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean4.Visible = true;
                ddlOperatorBoolean4.Enabled = true;
                ddlOperatorCharacter4.Visible = false;
                ddlChooseOperator4.Visible = false;
            }
            else if (ddlChooseField4.Text == "Grade")
            {
                //Manage the search field..
                ddlGrades4.Visible = true;
                ddlGrades4.Enabled = false;
                ddlSearchValue4Bool.Visible = false;
                txbSearchValue4.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator4.Visible = true;
                ddlChooseOperator4.Enabled = true;
                ddlOperatorBoolean4.Visible = false;
                ddlOperatorCharacter4.Visible = false;
            }
            else if (ddlChooseField4.Text == "CampDropOff" || ddlChooseField4.Text == "CampPickUp" || ddlChooseField4.Text == "CurrentRegistrationForm" || ddlChooseField4.Text == "Dance" || ddlChooseField4.Text == "HaveReceivedChrist" || ddlChooseField4.Text == "MailingListInclude" || ddlChooseField4.Text == "ParentalConsentForm" || ddlChooseField4.Text == "PermissionToTransport" || ddlChooseField4.Text == "PromotionalRelease" || ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlGrades4.Visible = false;
                ddlSearchValue4Bool.Visible = true;
                ddlSearchValue4Bool.Enabled = false;
                txbSearchValue4.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean4.Visible = true;
                ddlOperatorBoolean4.Enabled = true;
                ddlOperatorCharacter4.Visible = false;
                ddlChooseOperator4.Visible = false;
            }
            else if (ddlChooseField4.Text == "CoreKid")
            {
                //Manage the search field..
                ddlGrades4.Visible = false;
                ddlSearchValue4Bool.Visible = true;
                ddlSearchValue4Bool.Enabled = false;
                txbSearchValue4.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean4.Visible = true;
                ddlOperatorBoolean4.Enabled = true;
                ddlOperatorCharacter4.Visible = false;
                ddlChooseOperator4.Visible = false;
            }
            else if (ddlChooseField4.Text == "ScrubbedDate")
            {
                //Manage the search field..
                ddlGrades4.Visible = false;
                ddlGrades4.Enabled = false;
                ddlSearchValue4Bool.Visible = false;
                txbSearchValue4.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator4.Visible = true;
                ddlChooseOperator4.Enabled = true;
                ddlOperatorBoolean4.Visible = false;
                ddlOperatorCharacter4.Visible = false;
            }
            else
            {
                //Manage the search field..
                ddlSearchValue4Bool.Visible = false;
                ddlGrades4.Visible = false;
                txbSearchValue4.Visible = true;
                txbSearchValue4.Enabled = true;

                //Manage the operators...RCM..
                ddlOperatorCharacter4.Visible = true;
                ddlOperatorCharacter4.Enabled = true;
                ddlOperatorBoolean4.Visible = false;
                ddlChooseOperator4.Visible = false;
            }
        }

        protected void ddlChooseField5_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //Reset the Operators..RCM.4/2/12..
            ddlOperatorBoolean5.Text = "Choose Operator";
            ddlOperatorCharacter5.Text = "Choose Operator";
            ddlChooseOperator5.Text = "Choose Operator";

            //Reset the SearchValues..RCM..4/2/12..
            ddlGrades5.Text = "Select a Grade";
            ddlSearchValue5Bool.Text = "Select a value";
            txbSearchValue5.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField5.Text == "MSHSChoir") || (ddlChooseField5.Text == "ChildrensChoir") || (ddlChooseField5.Text == "PerformingArts") || (ddlChooseField5.Text == "Shakes") || (ddlChooseField5.Text == "Singers") || (ddlChooseField5.Text == "OutreachBasketball") || (ddlChooseField5.Text == "BasketballTEAMS") || (ddlChooseField5.Text == "HSBasketballLg") || (ddlChooseField5.Text == "MSBasketballLg") || (ddlChooseField5.Text == "3on3Basketball") || (ddlChooseField5.Text == "SoccerIntraMurals") || (ddlChooseField5.Text == "SoccerTEAMS") || (ddlChooseField5.Text == "Baseball") || (ddlChooseField5.Text == "BibleStudy") || (ddlChooseField5.Text == "MondayNights") || (ddlChooseField5.Text == "SummerDayCamp") || (ddlChooseField5.Text == "ImpactUrbanSchools") || (ddlChooseField5.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlGrades5.Visible = false;
                ddlSearchValue5Bool.Visible = true;
                ddlSearchValue5Bool.Enabled = false;
                txbSearchValue5.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean5.Visible = true;
                ddlOperatorBoolean5.Enabled = true;
                ddlOperatorCharacter5.Visible = false;
                ddlChooseOperator5.Visible = false;
            }
            else if (ddlChooseField5.Text == "Grade")
            {
                //Manage the search field..
                ddlGrades5.Visible = true;
                ddlGrades5.Enabled = false;
                ddlSearchValue5Bool.Visible = false;
                txbSearchValue5.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator5.Visible = true;
                ddlChooseOperator5.Enabled = true;
                ddlOperatorBoolean5.Visible = false;
                ddlOperatorCharacter5.Visible = false;
            }
            else if (ddlChooseField5.Text == "CampDropOff" || ddlChooseField5.Text == "CampPickUp" || ddlChooseField5.Text == "CurrentRegistrationForm" || ddlChooseField5.Text == "Dance" || ddlChooseField5.Text == "HaveReceivedChrist" || ddlChooseField5.Text == "MailingListInclude" || ddlChooseField5.Text == "ParentalConsentForm" || ddlChooseField5.Text == "PermissionToTransport" || ddlChooseField5.Text == "PromotionalRelease" || ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlGrades5.Visible = false;
                ddlSearchValue5Bool.Visible = true;
                ddlSearchValue5Bool.Enabled = false;
                txbSearchValue5.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean5.Visible = true;
                ddlOperatorBoolean5.Enabled = true;
                ddlOperatorCharacter5.Visible = false;
                ddlChooseOperator5.Visible = false;
            }
            else if (ddlChooseField5.Text == "CoreKid")
            {
                //Manage the search field..
                ddlGrades5.Visible = false;
                ddlSearchValue5Bool.Visible = true;
                ddlSearchValue5Bool.Enabled = false;
                txbSearchValue5.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean5.Visible = true;
                ddlOperatorBoolean5.Enabled = true;
                ddlOperatorCharacter5.Visible = false;
                ddlChooseOperator5.Visible = false;
            }
            else if (ddlChooseField5.Text == "ScrubbedDate")
            {
                //Manage the search field..
                ddlGrades5.Visible = false;
                ddlGrades5.Enabled = false;
                ddlSearchValue5Bool.Visible = false;
                txbSearchValue5.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator5.Visible = true;
                ddlChooseOperator5.Enabled = true;
                ddlOperatorBoolean5.Visible = false;
                ddlOperatorCharacter5.Visible = false;
            }
            else
            {
                //Manage the search field..
                ddlSearchValue5Bool.Visible = false;
                ddlGrades5.Visible = false;
                txbSearchValue5.Visible = true;
                txbSearchValue5.Enabled = true;

                //Manage the operators...RCM..
                ddlOperatorCharacter5.Visible = true;
                ddlOperatorCharacter5.Enabled = true;
                ddlOperatorBoolean5.Visible = false;
                ddlChooseOperator5.Visible = false;
            }
        }
        
        protected void ddlChooseField_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //Reset the Operators..RCM.4/2/12..
            ddlOperatorBoolean.Text = "Choose Operator";
            ddlOperatorCharacter.Text = "Choose Operator";
            ddlChooseOperator.Text = "Choose Operator";

            //Reset the SearchValues..RCM..4/2/12..
            ddlGrades.Text = "Select a Grade";
            ddlSearchValueBool.Text = "Select a value";
            txbSearchValue.Text = "";
            
            //Help with the boolean fields..
            if ((ddlChooseField.Text == "MSHSChoir") || (ddlChooseField.Text == "ChildrensChoir") || (ddlChooseField.Text == "PerformingArts") || (ddlChooseField.Text == "Shakes") || (ddlChooseField.Text == "Singers") || (ddlChooseField.Text == "OutreachBasketball") || (ddlChooseField.Text == "BasketballTEAMS") || (ddlChooseField.Text == "HSBasketballLg") || (ddlChooseField.Text == "MSBasketballLg") || (ddlChooseField.Text == "3on3Basketball") || (ddlChooseField.Text == "SoccerIntraMurals") || (ddlChooseField.Text == "SoccerTEAMS") || (ddlChooseField.Text == "Baseball") || (ddlChooseField.Text == "BibleStudy") || (ddlChooseField.Text == "MondayNights") || (ddlChooseField.Text == "SummerDayCamp") || (ddlChooseField.Text == "ImpactUrbanSchools") || (ddlChooseField.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = true;
                ddlOperatorBoolean.Enabled = true;
                ddlOperatorCharacter.Visible = false;
                ddlChooseOperator.Visible = false;

                //Adding once a Program has been selected...
                //So the user can drill down into a Program.
                ddlChooseField2.Items.Add("Section");
                ddlChooseField3.Items.Add("Section");
                ddlChooseField4.Items.Add("Section");
                ddlChooseField5.Items.Add("Section");
            }
            else if ((ddlChooseField.Text == "All Programs") || (ddlChooseField.Text == "AthleticsDept") || (ddlChooseField.Text == "PerformingArtsDept") || (ddlChooseField.Text == "EducationDept"))
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = true;
                ddlOperatorBoolean.Enabled = true;
                ddlOperatorCharacter.Visible = false;
                ddlChooseOperator.Visible = false;
            }
            else if (ddlChooseField.Text == "Grade")
            {
                //Manage the search field..
                ddlGrades.Visible = true;
                ddlGrades.Enabled = false;
                ddlSearchValueBool.Visible = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator.Visible = true;
                ddlChooseOperator.Enabled = true;
                ddlOperatorBoolean.Visible = false;
                ddlOperatorCharacter.Visible = false;
            }
            else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = true;
                ddlOperatorBoolean.Enabled = true;
                ddlOperatorCharacter.Visible = false;
                ddlChooseOperator.Visible = false;
            }
            else if (ddlChooseField.Text == "CoreKid")
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = true;
                ddlOperatorBoolean.Enabled = true;
                ddlOperatorCharacter.Visible = false;
                ddlChooseOperator.Visible = false;
            }
            else if (ddlChooseField.Text == "ScrubbedDate")
            {
                //Manage the search field..
                ddlGrades.Visible = false;
                ddlGrades.Enabled = false;
                ddlSearchValueBool.Visible = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlChooseOperator.Visible = true;
                ddlChooseOperator.Enabled = true;
                ddlOperatorBoolean.Visible = false;
                ddlOperatorCharacter.Visible = false;
            }
            else
            {
                //Manage the search field..
                ddlSearchValueBool.Visible = false;
                ddlGrades.Visible = false;
                txbSearchValue.Visible = true;
                txbSearchValue.Enabled = true;

                //Manage the operators...RCM..
                ddlOperatorCharacter.Visible = true;
                ddlOperatorCharacter.Enabled = true;
                ddlOperatorBoolean.Visible = false;
                ddlChooseOperator.Visible = false;
            }
        }

        protected void ddlChooseOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseField.Text == "ScrubbedDate")
            {
                ddlSearchValueBool.Visible = false;
                ddlGrades.Visible = false;
                ddlGrades.Enabled = false;
                txbSearchValue.Visible = true;
            }
            else
            {
                ddlSearchValueBool.Visible = false;
                ddlGrades.Visible = true;
                ddlGrades.Enabled = true;
                txbSearchValue.Visible = false;
            }
        }

        protected void ddlChooseOperator2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseField2.Text == "ScrubbedDate")
            {
                ddlSearchValue2Bool.Visible = false;
                ddlGrades2.Visible = false;
                ddlGrades2.Enabled = false;
                txbSearchValue2.Visible = true;
            }
            else
            {
                ddlSearchValue2Bool.Visible = false;
                ddlGrades2.Visible = true;
                ddlGrades2.Enabled = true;
                txbSearchValue2.Visible = false;
            }
        }

        protected void ddlChooseOperator3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseField3.Text == "ScrubbedDate")
            {
                ddlSearchValue3Bool.Visible = false;
                ddlGrades3.Visible = false;
                ddlGrades3.Enabled = false;
                txbSearchValue3.Visible = true;
            }
            else
            {
                ddlSearchValue3Bool.Visible = false;
                ddlGrades3.Visible = true;
                ddlGrades3.Enabled = true;
                txbSearchValue3.Visible = false;
            }
        }

        protected void ddlChooseOperator4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseField4.Text == "ScrubbedDate")
            {
                ddlSearchValue4Bool.Visible = false;
                ddlGrades4.Visible = false;
                ddlGrades4.Enabled = false;
                txbSearchValue4.Visible = true;
            }
            else
            {
                ddlSearchValue4Bool.Visible = false;
                ddlGrades4.Visible = true;
                ddlGrades4.Enabled = true;
                txbSearchValue4.Visible = false;
            }
        }

        protected void ddlChooseOperator5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChooseField5.Text == "ScrubbedDate")
            {
                ddlSearchValue5Bool.Visible = false;
                ddlGrades5.Visible = false;
                ddlGrades5.Enabled = false;
                txbSearchValue5.Visible = true;
            }
            else
            {
                ddlSearchValue5Bool.Visible = false;
                ddlGrades5.Visible = true;
                ddlGrades5.Enabled = true;
                txbSearchValue5.Visible = false;
            }
        }

        protected void rblNumber1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChooseField2.Enabled = true;
            //ddlChooseOperator2.Enabled = true;
        }

        protected void rblNumber2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChooseField3.Enabled = true;
            //ddlChooseOperator3.Enabled = true;
        }

        protected void rblNumber3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChooseField4.Enabled = true;
            //ddlChooseOperator4.Enabled = true;
        }

        protected void rblNumber4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChooseField5.Enabled = true;
            //ddlChooseOperator5.Enabled = true;
        }

        protected void ddlOperatorBoolean_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValueBool.Enabled = true;
            ddlSearchValueBool.Visible = true;
            ddlGrades.Visible = false;
            txbSearchValue.Visible = false;
        }

        protected void ddlOperatorBoolean2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue2Bool.Enabled = true;
            ddlSearchValue2Bool.Visible = true;
            ddlGrades2.Visible = false;
            txbSearchValue2.Visible = false;
        }

        protected void ddlOperatorBoolean3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue3Bool.Enabled = true;
            ddlSearchValue3Bool.Visible = true;
            ddlGrades3.Visible = false;
            txbSearchValue3.Visible = false;
        }

        protected void ddlOperatorBoolean4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue4Bool.Enabled = true;
            ddlSearchValue4Bool.Visible = true;
            ddlGrades4.Visible = false;
            txbSearchValue4.Visible = false;
        }

        protected void ddlOperatorBoolean5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue5Bool.Enabled = true;
            ddlSearchValue5Bool.Visible = true;
            ddlGrades5.Visible = false;
            txbSearchValue5.Visible = false;
        }

        protected void ddlOperatorCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValueBool.Enabled = false;
            ddlSearchValueBool.Visible = false;
            ddlGrades.Visible = false;
            txbSearchValue.Visible = true;

            rblNumber1.Enabled = true;
        }

        protected void ddlOperatorCharacter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue2Bool.Enabled = false;
            ddlSearchValue2Bool.Visible = false;
            ddlGrades2.Visible = false;
            txbSearchValue2.Visible = true;

            rblNumber2.Enabled = true;
        }

        protected void ddlOperatorCharacter3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue3Bool.Enabled = false;
            ddlSearchValue3Bool.Visible = false;
            ddlGrades3.Visible = false;
            txbSearchValue3.Visible = true;

            rblNumber3.Enabled = true;
        }

        protected void ddlOperatorCharacter4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue4Bool.Enabled = false;
            ddlSearchValue4Bool.Visible = false;
            ddlGrades4.Visible = false;
            txbSearchValue4.Visible = true;

            rblNumber4.Enabled = true;
        }

        protected void ddlOperatorCharacter5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue5Bool.Enabled = false;
            ddlSearchValue5Bool.Visible = false;
            ddlGrades5.Visible = false;
            txbSearchValue5.Visible = true;
        }

        protected void txbSearchValue_TextChanged(object sender, EventArgs e)
        {
            rblNumber1.Enabled = true;
        }

        protected void txbSearchValue4_TextChanged(object sender, EventArgs e)
        {
            rblNumber4.Enabled = true;
        }

        protected void txbSearchValue5_TextChanged(object sender, EventArgs e)
        {
            //
        }

        protected void txbSearchValue3_TextChanged(object sender, EventArgs e)
        {
            rblNumber3.Enabled = true;
        }

        protected void ddlGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber1.Enabled = true;
        }

        protected void ddlGrades2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber2.Enabled = true;
        }

        protected void ddlGrades3_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber3.Enabled = true;
        }

        protected void ddlGrades4_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber4.Enabled = true;
        }

        protected void ddlGrades5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rblNumber1.Enabled = true;
        }

        protected void ddlProgramSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}