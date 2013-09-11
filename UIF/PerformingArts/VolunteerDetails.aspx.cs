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
    public partial class VolunteerDetails : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public Boolean flag = false;
        public int irowNum = 0;
        public static string Department = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader reader = null;
            
            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];

                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);

                if (Request.QueryString["security"] == "Good")
                {
                    if (((Request.QueryString["LastName"] == "Churchill") && (Request.QueryString["FirstName"] == "Andrew")) || ((Request.QueryString["LastName"] == "Sims-Reed") && (Request.QueryString["FirstName"] == "Donna")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Boll") && (Request.QueryString["FirstName"] == "Becky")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")) || ((Request.QueryString["LastName"] == "Speicher") && (Request.QueryString["FirstName"] == "Kelly")) || ((Request.QueryString["LastName"] == "Gilmore") && (Request.QueryString["FirstName"] == "Anna")))
                    {
                        if (((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")) || ((Request.QueryString["LastName"] == "Brazil") && (Request.QueryString["FirstName"] == "Nehemiah")) || ((Request.QueryString["LastName"] == "Braunersrither") && (Request.QueryString["FirstName"] == "Chad")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Nate")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Hannah")))
                        {
                            cmbUpdateInformation.Enabled = false;
                            //imbAdvancedSearch.Enabled = false;
                        }

                        PopulateDateOfBirth();

                        PopulateVolunteerList();
                        PopulateVehichleInsuranceCodesList();
                        PopulateNationalCheckCodesList();
                        PopulateBackgroundCheckCodesList();
                        PopulatePACriminalCheckCodesList();
                        PopulateDMVCheckCodesList();
                        PopulateBackgroundCheckPAIDCodesList();
                        PopulateCalenderDropDowns();
                        
                        txbNewVolunteerTrainingDate.Attributes.Add("onclick", "return false;");
                        txbVehichleInsurDate.Attributes.Add("onclick", "return false;");
                        txbPACriminalCheckDate.Attributes.Add("onclick", "return false;");
                        txbNationalCheckDate.Attributes.Add("onclick", "return false;");
                        txbDMVCheckDate.Attributes.Add("onclick", "return false;");
                        txbBackgroundCheckPAIDDate.Attributes.Add("onclick", "return false;");

                        chbDMVCheck.Attributes.Add("onclick", "return false;");
                        chbNationalCheck.Attributes.Add("onclick", "return false;");
                        chbPACriminalCheck.Attributes.Add("onclick", "return false;");
                        chbVehichleInsurance.Attributes.Add("onclick", "return false;");
                        chbBackgroundCheckPAID.Attributes.Add("onclick", "return false;");

                        imbDMVCheckDate.ImageUrl = "Calender.jpg";
                        imbNationalCheckDate.ImageUrl = "Calender.jpg";
                        imbVehichleInsurDate.ImageUrl = "Calender.jpg";
                        imbPACriminalCheckDate.ImageUrl = "Calender.jpg";
                        imgCalender.ImageUrl = "Calender.jpg";
                        imbNewVolunteerTrainingDate.ImageUrl = "Calender.jpg";
                        imbBackgroundCheckPAIDDate.ImageUrl = "Calender.jpg";

                        if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                        {
                            //Do Nothing, Default Entry..
                        }
                        else
                        {
                            RetreiveInformation(Request.QueryString["VolunteerLastName"], Request.QueryString["VolunteerFirstName"]);
                            DisplayHeaderFields();
                            ddlNames.Text = Request.QueryString["VolunteerLastName"] + "," + Request.QueryString["VolunteerFirstName"];
                        }
                    }
                    else
                    {
                        //Ryan C Manners..1/5/11
                        //Do NOT ALLOW ACCESS TO THE PAGE!
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



        protected void PopulateDateOfBirth()
        {
            ddlDateOfBirthMonth.Items.Add("01");
            ddlDateOfBirthMonth.Items.Add("02");
            ddlDateOfBirthMonth.Items.Add("03");
            ddlDateOfBirthMonth.Items.Add("04");
            ddlDateOfBirthMonth.Items.Add("05");
            ddlDateOfBirthMonth.Items.Add("06");
            ddlDateOfBirthMonth.Items.Add("07");
            ddlDateOfBirthMonth.Items.Add("08");
            ddlDateOfBirthMonth.Items.Add("09");
            ddlDateOfBirthMonth.Items.Add("10");
            ddlDateOfBirthMonth.Items.Add("11");
            ddlDateOfBirthMonth.Items.Add("12");

            ddlDateOfBirthDay.Items.Add("01");
            ddlDateOfBirthDay.Items.Add("02");
            ddlDateOfBirthDay.Items.Add("03");
            ddlDateOfBirthDay.Items.Add("04");
            ddlDateOfBirthDay.Items.Add("05");
            ddlDateOfBirthDay.Items.Add("06");
            ddlDateOfBirthDay.Items.Add("07");
            ddlDateOfBirthDay.Items.Add("08");
            ddlDateOfBirthDay.Items.Add("09");
            ddlDateOfBirthDay.Items.Add("10");
            ddlDateOfBirthDay.Items.Add("11");
            ddlDateOfBirthDay.Items.Add("12");
            ddlDateOfBirthDay.Items.Add("13");
            ddlDateOfBirthDay.Items.Add("14");
            ddlDateOfBirthDay.Items.Add("15");
            ddlDateOfBirthDay.Items.Add("16");
            ddlDateOfBirthDay.Items.Add("17");
            ddlDateOfBirthDay.Items.Add("18");
            ddlDateOfBirthDay.Items.Add("19");
            ddlDateOfBirthDay.Items.Add("20");
            ddlDateOfBirthDay.Items.Add("21");
            ddlDateOfBirthDay.Items.Add("22");
            ddlDateOfBirthDay.Items.Add("23");
            ddlDateOfBirthDay.Items.Add("24");
            ddlDateOfBirthDay.Items.Add("25");
            ddlDateOfBirthDay.Items.Add("26");
            ddlDateOfBirthDay.Items.Add("27");
            ddlDateOfBirthDay.Items.Add("28");
            ddlDateOfBirthDay.Items.Add("29");
            ddlDateOfBirthDay.Items.Add("30");
            ddlDateOfBirthDay.Items.Add("31");

            ddlDateOfBirthYear.Items.Add("1920");
            ddlDateOfBirthYear.Items.Add("1921");
            ddlDateOfBirthYear.Items.Add("1922");
            ddlDateOfBirthYear.Items.Add("1923");
            ddlDateOfBirthYear.Items.Add("1924");
            ddlDateOfBirthYear.Items.Add("1925");
            ddlDateOfBirthYear.Items.Add("1926");
            ddlDateOfBirthYear.Items.Add("1927");
            ddlDateOfBirthYear.Items.Add("1928");
            ddlDateOfBirthYear.Items.Add("1929");
            ddlDateOfBirthYear.Items.Add("1930");
            ddlDateOfBirthYear.Items.Add("1931");
            ddlDateOfBirthYear.Items.Add("1932");
            ddlDateOfBirthYear.Items.Add("1933");
            ddlDateOfBirthYear.Items.Add("1934");
            ddlDateOfBirthYear.Items.Add("1935");
            ddlDateOfBirthYear.Items.Add("1936");
            ddlDateOfBirthYear.Items.Add("1937");
            ddlDateOfBirthYear.Items.Add("1938");
            ddlDateOfBirthYear.Items.Add("1939");
            ddlDateOfBirthYear.Items.Add("1940");
            ddlDateOfBirthYear.Items.Add("1941");
            ddlDateOfBirthYear.Items.Add("1942");
            ddlDateOfBirthYear.Items.Add("1943");
            ddlDateOfBirthYear.Items.Add("1944");
            ddlDateOfBirthYear.Items.Add("1945");
            ddlDateOfBirthYear.Items.Add("1946");
            ddlDateOfBirthYear.Items.Add("1947");
            ddlDateOfBirthYear.Items.Add("1948");
            ddlDateOfBirthYear.Items.Add("1949");
            ddlDateOfBirthYear.Items.Add("1950");
            ddlDateOfBirthYear.Items.Add("1951");
            ddlDateOfBirthYear.Items.Add("1952");
            ddlDateOfBirthYear.Items.Add("1953");
            ddlDateOfBirthYear.Items.Add("1954");
            ddlDateOfBirthYear.Items.Add("1955");
            ddlDateOfBirthYear.Items.Add("1956");
            ddlDateOfBirthYear.Items.Add("1957");
            ddlDateOfBirthYear.Items.Add("1958");
            ddlDateOfBirthYear.Items.Add("1959");
            ddlDateOfBirthYear.Items.Add("1960");
            ddlDateOfBirthYear.Items.Add("1961");
            ddlDateOfBirthYear.Items.Add("1962");
            ddlDateOfBirthYear.Items.Add("1963");
            ddlDateOfBirthYear.Items.Add("1964");
            ddlDateOfBirthYear.Items.Add("1965");
            ddlDateOfBirthYear.Items.Add("1966");
            ddlDateOfBirthYear.Items.Add("1967");
            ddlDateOfBirthYear.Items.Add("1968");
            ddlDateOfBirthYear.Items.Add("1969");
            ddlDateOfBirthYear.Items.Add("1970");
            ddlDateOfBirthYear.Items.Add("1971");
            ddlDateOfBirthYear.Items.Add("1972");
            ddlDateOfBirthYear.Items.Add("1973");
            ddlDateOfBirthYear.Items.Add("1974");
            ddlDateOfBirthYear.Items.Add("1975");
            ddlDateOfBirthYear.Items.Add("1976");
            ddlDateOfBirthYear.Items.Add("1977");
            ddlDateOfBirthYear.Items.Add("1978");
            ddlDateOfBirthYear.Items.Add("1979");
            ddlDateOfBirthYear.Items.Add("1980");
            ddlDateOfBirthYear.Items.Add("1981");
            ddlDateOfBirthYear.Items.Add("1982");
            ddlDateOfBirthYear.Items.Add("1983");
            ddlDateOfBirthYear.Items.Add("1984");
            ddlDateOfBirthYear.Items.Add("1985");
            ddlDateOfBirthYear.Items.Add("1986");
            ddlDateOfBirthYear.Items.Add("1987");
            ddlDateOfBirthYear.Items.Add("1988");
            ddlDateOfBirthYear.Items.Add("1989");
            ddlDateOfBirthYear.Items.Add("1990");
            ddlDateOfBirthYear.Items.Add("1991");
            ddlDateOfBirthYear.Items.Add("1992");
            ddlDateOfBirthYear.Items.Add("1993");
            ddlDateOfBirthYear.Items.Add("1994");
            ddlDateOfBirthYear.Items.Add("1995");
            ddlDateOfBirthYear.Items.Add("1996");
            ddlDateOfBirthYear.Items.Add("1997");
            ddlDateOfBirthYear.Items.Add("1998");
            ddlDateOfBirthYear.Items.Add("1999");
            ddlDateOfBirthYear.Items.Add("2000");
            ddlDateOfBirthYear.Items.Add("2001");
            ddlDateOfBirthYear.Items.Add("2002");
            ddlDateOfBirthYear.Items.Add("2003");
            ddlDateOfBirthYear.Items.Add("2004");
            ddlDateOfBirthYear.Items.Add("2005");
            ddlDateOfBirthYear.Items.Add("2006");
            ddlDateOfBirthYear.Items.Add("2007");
            ddlDateOfBirthYear.Items.Add("2008");
            ddlDateOfBirthYear.Items.Add("2009");
            ddlDateOfBirthYear.Items.Add("2010");
            ddlDateOfBirthYear.Items.Add("2011");
            ddlDateOfBirthYear.Items.Add("2012");
            ddlDateOfBirthYear.Items.Add("2013");
            ddlDateOfBirthYear.Items.Add("2014");
            ddlDateOfBirthYear.Items.Add("2015");
            ddlDateOfBirthYear.Items.Add("2016");
            ddlDateOfBirthYear.Items.Add("2017");
            ddlDateOfBirthYear.Items.Add("2018");
            ddlDateOfBirthYear.Items.Add("2019");
            ddlDateOfBirthYear.Items.Add("2020");
        }

        protected void ClearPage()
        {

            imgImage.Visible = false;
            ddlDateOfBirthDay.Visible = false;
            ddlDateOfBirthMonth.Visible = false;
            ddlDateOfBirthYear.Visible = false;
            lblDateOfBirth.Visible = false;

            lblLastUpdatedBy.Visible = false;
            lbVolunteerProfilePage.Visible = false;
            
            txbComments2.Visible = false;
            lblComments2.Visible = false;
            txbDMVCheckDate.Visible = false;
            txbNationalCheckDate.Visible = false;
            txbNewVolunteerTrainingDate.Visible = false;
            txbVehichleInsurDate.Visible = false;
            txbPACriminalCheckDate.Visible = false;
            txbBackgroundCheckPAIDDate.Visible = false;

            ddlBackgroundCheckPaidCode.Visible = false;

            ddlCriminalCheckCodes.Visible = false;
            ddlDMVCheckCodes.Visible = false;
            ddlVehichleInsuranceCodes.Visible = false;
            ddlNationalCheckCodes.Visible = false;



            ddlPACriminalYear.Visible = false;
            ddlPACriminalMonth.Visible = false;
            ddlPACriminalDay.Visible = false;
            ddlNationalDay.Visible = false;
            ddlNationalMonth.Visible = false;
            ddlNationalYear.Visible = false;
            ddlDMVDay.Visible = false;
            ddlDMVMonth.Visible = false;
            ddlDMVYear.Visible = false;
            ddlVehichleDay.Visible = false;
            ddlVehichleMonth.Visible = false;
            ddlVehichleYear.Visible = false;

            chbDMVCheck.Visible = false;
            chbNationalCheck.Visible = false;
            chbReleaseWaiver.Visible = false;
            chbSpiritualJourney.Visible = false;
            chbVehichleInsurance.Visible = false;
            chbPACriminalCheck.Visible = false;
            chbBackgroundCheckPAID.Visible = false;
            chbNewVolunteerTraining.Visible = false;
            chbGeneralInformation.Visible = false;

            cmbUpdateInformation.Visible = false;

            imbBackgroundCheckPAIDDate.Visible = false;
            imbDMVCheckDate.Visible = false;
            imbNationalCheckDate.Visible = false;
            imbPACriminalCheckDate.Visible = false;
            imbVehichleInsurDate.Visible = false;
            imbNewVolunteerTrainingDate.Visible = false;
        }


        protected void DisplayHeaderFields()
        {
            lblLastUpdatedBy.Visible = true;

            lbVolunteerProfilePage.Visible = true;

            imgImage.Visible = true;

            txbComments2.Visible = true;
            lblComments2.Visible = true;
            //txbDMVCheckDate.Visible = true;
            //txbNationalCheckDate.Visible = true;
            //txbNewVolunteerTrainingDate.Visible = true;
            //txbVehichleInsurDate.Visible = true;
            //txbPACriminalCheckDate.Visible = true;
            //txbBackgroundCheckPAIDDate.Visible = true;

            ddlCriminalCheckCodes.Visible = true;
            ddlDMVCheckCodes.Visible = true;
            ddlVehichleInsuranceCodes.Visible = true;
            ddlNationalCheckCodes.Visible = true;
            ddlBackgroundCheckPaidCode.Visible = true;

            chbDMVCheck.Visible = true;
            chbNationalCheck.Visible = true;
            chbReleaseWaiver.Visible = true;
            chbSpiritualJourney.Visible = true;
            chbVehichleInsurance.Visible = true;
            chbPACriminalCheck.Visible = true;
            chbBackgroundCheckPAID.Visible = true;
            chbNewVolunteerTraining.Visible = true;
            chbGeneralInformation.Visible = true;

            cmbUpdateInformation.Visible = true;
            
            
            ////imbBackgroundCheckPAIDDate.Visible = true;
            //imbDMVCheckDate.Visible = true;
            //imbNationalCheckDate.Visible = true;
            //imbPACriminalCheckDate.Visible = true;
            //imbVehichleInsurDate.Visible = true;
            ////imbNewVolunteerTrainingDate.Visible = true;

            ddlPACriminalYear.Visible = true;
            ddlPACriminalMonth.Visible = true;
            ddlPACriminalDay.Visible = true;
            ddlNationalDay.Visible = true;
            ddlNationalMonth.Visible = true;
            ddlNationalYear.Visible = true;
            ddlDMVDay.Visible = true;
            ddlDMVMonth.Visible = true;
            ddlDMVYear.Visible = true;
            ddlVehichleDay.Visible = true;
            ddlVehichleMonth.Visible = true;
            ddlVehichleYear.Visible = true;


            //Make this readonly but not enabled..RCM..
            ddlDateOfBirthDay.Visible = true;
            ddlDateOfBirthDay.Enabled = false;
            ddlDateOfBirthMonth.Visible = true;
            ddlDateOfBirthMonth.Enabled = false;
            ddlDateOfBirthYear.Visible = true;
            ddlDateOfBirthYear.Enabled = false; 
            lblDateOfBirth.Visible = true;
        }

        protected void RetreiveInformation(string LastName, string FirstName)
        {
            SqlDataReader reader = null;

            try
            {

                con.Open();//Opens the db connection.
                string sql = "";

                sql = "select vd.LastName + ',' + vd.FirstName, "
                    + "vd.GeneralInformation, "
                    + "vd.SpiritualJourney, "
                    + "vd.ReleaseWaiver, "
                    + "vd.NewVolunteerTraining, "
                    + "vd.NewVolunteerTrainingDate, "
                    + "vd.VehichleInsurance, "
                    + "vd.VehichleInsuranceCodes, "
                    + "vd.VehichleInsuranceDate, "
                    + "vd.BackgroundCheck, "
                    + "vd.NationalCheck, "
                    + "vd.NationalCheckCodes, "
                    + "vd.NationalCheckDate, "
                    + "vd.DMVCheck, "
                    + "vd.DMVCheckCodes, "
                    + "vd.DMVCheckDate, "
                    + "vd.PACriminalCheck, "
                    + "vd.PACriminalCheckCodes, "
                    + "vd.PACriminalCheckDate, "
                    + "vd.BackgroundCheckPAID, "
                    + "vd.BackgroundCheckCodes, "
                    + "vd.BackgroundCheckPAIDDate, "
                    + "vd.Comments, "
                    + "vd.LastUpdatedBy, "
                    + "vd.ID, "
                    + "vi.pictureidentification, "
                    + "vd.sysupdate, "
                    + "vi.dob "
                    + "from volunteerdetails vd "
                    + "LEFT OUTER JOIN VolunteerInformation vi "
                    + "ON (vd.lastname = vi.lastname AND vd.firstname = vi.firstname) "
                    + "WHERE vd.lastname = '" + LastName + "' "
                    + "AND vd.firstname = '" + FirstName + "' ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);
                //cmd.Parameters.Add(new SqlParameter("@lastname", Request.QueryString["StudentLastName"].Trim()));
                //cmd.Parameters.Add(new SqlParameter("@firstname", Request.QueryString["StudentFirstName"].Trim()));

                //StartingSettings();

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    if (reader.IsDBNull(0))
                    {
                        //txtLastName.Text = "N/A";
                    }
                    else
                    {
                        //txtLastName.Text = reader.GetString(0);
                    }
                    if (reader.IsDBNull(1))
                    {
                        chbGeneralInformation.Checked = false;
                    }
                    else
                    {
                        chbGeneralInformation.Checked = reader.GetBoolean(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        chbSpiritualJourney.Checked = false;
                    }
                    else
                    {
                        chbSpiritualJourney.Checked = reader.GetBoolean(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        chbReleaseWaiver.Checked = false;
                    }
                    else
                    {
                        chbReleaseWaiver.Checked = reader.GetBoolean(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        chbNewVolunteerTraining.Checked = false;
                    }
                    else
                    {
                        chbNewVolunteerTraining.Checked = reader.GetBoolean(4);
                    }
                    if (reader.IsDBNull(5))
                    {
                        txbNewVolunteerTrainingDate.Text = "";
                    }
                    else
                    {
                        //if (
                        txbNewVolunteerTrainingDate.Text = reader.GetSqlValue(5).ToString().Substring(0, reader.GetSqlValue(5).ToString().IndexOf(" "));
                    }
                    if (reader.IsDBNull(6))
                    {
                        chbVehichleInsurance.Checked = false;
                    }
                    else
                    {
                        chbVehichleInsurance.Checked = reader.GetBoolean(6);
                    }
                    if (reader.IsDBNull(7))
                    {
                        ddlVehichleInsuranceCodes.Text = "";
                    }
                    else
                    {
                        string ll = "";
                        ll = reader.GetString(7);
                        ddlVehichleInsuranceCodes.Text = reader.GetString(7);
                    }
                    if (reader.IsDBNull(8))
                    {
                        txbVehichleInsurDate.Text = "";
                    }
                    else
                    {
                        ddlVehichleMonth.Text = reader.GetDateTime(8).Month.ToString();
                        ddlVehichleDay.Text = reader.GetDateTime(8).Day.ToString();
                        ddlVehichleYear.Text = reader.GetDateTime(8).Year.ToString();

                        txbVehichleInsurDate.Text = reader.GetSqlValue(8).ToString().Substring(0, reader.GetSqlValue(8).ToString().IndexOf(" "));
                    }
                    //if (reader.IsDBNull(9))
                    //{
                    //    chbBackgroundCheck.Checked = false;
                    //}
                    //else
                    //{
                    //    chbBackgroundCheck.Checked = reader.GetBoolean(9);
                    //}
                    if (reader.IsDBNull(10))
                    {
                        chbNationalCheck.Checked = false;
                    }
                    else
                    {
                        chbNationalCheck.Checked = reader.GetBoolean(10);
                    }
                    if (reader.IsDBNull(11))
                    {
                        ddlNationalCheckCodes.Text = "";
                    }
                    else
                    {
                        ddlNationalCheckCodes.Text = reader.GetString(11);
                    }
                    if (reader.IsDBNull(12))
                    {
                        txbNationalCheckDate.Text = "";
                    }
                    else
                    {
                        ddlNationalMonth.Text = reader.GetDateTime(12).Month.ToString();
                        ddlNationalDay.Text = reader.GetDateTime(12).Day.ToString();
                        ddlNationalYear.Text = reader.GetDateTime(12).Year.ToString();

                        txbNationalCheckDate.Text = reader.GetSqlValue(12).ToString().Substring(0, reader.GetSqlValue(12).ToString().IndexOf(" "));

                        //if (ddlNationalMonth.Text == "01")
                        //{
                        //    ddlNationalMonth.Text = "00";
                        //}
                        //if (ddlNationalDay.Text == "01")
                        //{
                        //    ddlNationalDay.Text = "00";
                        //}
                    }
                    if (reader.IsDBNull(13))
                    {
                        chbDMVCheck.Checked = false;
                    }
                    else
                    {
                        chbDMVCheck.Checked = reader.GetBoolean(13);
                    }
                    if (reader.IsDBNull(14))
                    {
                        ddlDMVCheckCodes.Text = "";
                    }
                    else
                    {
                        ddlDMVCheckCodes.Text = reader.GetString(14);
                    }
                    if (reader.IsDBNull(15))
                    {
                        txbDMVCheckDate.Text = "";
                    }
                    else
                    {
                        ddlDMVMonth.Text = reader.GetDateTime(15).Month.ToString();
                        ddlDMVDay.Text = reader.GetDateTime(15).Day.ToString();
                        ddlDMVYear.Text = reader.GetDateTime(15).Year.ToString();

                        txbDMVCheckDate.Text = reader.GetSqlValue(15).ToString().Substring(0, reader.GetSqlValue(15).ToString().IndexOf(" "));
                    }
                    if (reader.IsDBNull(16))
                    {
                        chbPACriminalCheck.Checked = false;
                    }
                    else
                    {
                        chbPACriminalCheck.Checked = reader.GetBoolean(16);
                    }
                    if (reader.IsDBNull(17))
                    {
                        ddlCriminalCheckCodes.Text = "";
                    }
                    else
                    {
                        ddlCriminalCheckCodes.Text = reader.GetString(17);
                    }
                    if (reader.IsDBNull(18))
                    {
                        txbPACriminalCheckDate.Text = "";
                    }
                    else
                    {
                        ddlPACriminalMonth.Text = reader.GetDateTime(18).Month.ToString();
                        ddlPACriminalDay.Text = reader.GetDateTime(18).Day.ToString();
                        ddlPACriminalYear.Text = reader.GetDateTime(18).Year.ToString();

                        txbPACriminalCheckDate.Text = reader.GetSqlValue(18).ToString().Substring(0, reader.GetSqlValue(18).ToString().IndexOf(" "));
                    }
                    if (reader.IsDBNull(19))
                    {
                        chbBackgroundCheckPAID.Checked = false;
                    }
                    else
                    {
                        chbBackgroundCheckPAID.Checked = reader.GetBoolean(19);
                    }
                    if (reader.IsDBNull(20))
                    {
                        ddlBackgroundCheckPaidCode.Text = "Checks Not Run";
                    }
                    else
                    {
                        ddlBackgroundCheckPaidCode.Text = reader.GetString(20);
                    }
                    //if (reader.IsDBNull(21))
                    //{
                    //    txbBackgroundCheckPAIDDate.Text = "";
                    //}
                    //else
                    //{
                    //    txbBackgroundCheckPAIDDate.Text = reader.GetSqlValue(21).ToString().Substring(0, reader.GetSqlValue(21).ToString().IndexOf(" "));
                    //}
                    if (reader.IsDBNull(22))
                    {
                        txbComments2.Text = "N/A";
                    }
                    else
                    {
                        txbComments2.Text = reader.GetString(22);
                    }
                    if (reader.IsDBNull(23))
                    {
                        lblLastUpdatedBy.Text = "N/A";
                    }
                    else
                    {
                        lblLastUpdatedBy.Text = "LastUpdatedBy: " + reader.GetString(23) + " On: " + reader.GetSqlValue(26).ToString();
                    }

                    if (reader.IsDBNull(25))
                    {
                        imgImage.ImageUrl = "N/A";
                    }
                    else
                    {
                        imgImage.ImageUrl = reader.GetString(25);
                    }
                    if (reader.IsDBNull(27))
                    {
                        ddlDateOfBirthMonth.Text = "00";
                        ddlDateOfBirthDay.Text = "00";
                        ddlDateOfBirthYear.Text = "0000";
                    }
                    else
                    {
                        ddlDateOfBirthMonth.Text = reader.GetSqlValue(27).ToString().Substring(0, 2);
                        ddlDateOfBirthDay.Text = reader.GetSqlValue(27).ToString().Substring(3, 2);
                        ddlDateOfBirthYear.Text = reader.GetSqlValue(27).ToString().Substring(6, 4);
                    }
                }
            }
            catch (Exception lklaavv)
            {
                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.UIFCommon ErrorHandler = new UrbanImpactCommon.UIFCommon();
                ErrorHandler.InsertIntoSystemErrors(this.Page.ToString(), "Error Retreiving VolunteerDetails: " + lklaavv.Message.ToString(), Request.QueryString["lastname"] + "," + Request.QueryString["firstname"]);
            }
            finally
            {
                con.Close();
            }
        }

        protected void PopulateBackgroundCheckPAIDCodesList()
        {
            try
            {

                //ddlBackgroundCheckPaidCode.Items.Add("Select a Code?");
                ddlBackgroundCheckPaidCode.Items.Add("Checks Not Run");
                ddlBackgroundCheckPaidCode.Items.Add("Paid In Full");
                ddlBackgroundCheckPaidCode.Items.Add("Grandfathered In");
                ddlBackgroundCheckPaidCode.Items.Add("Owe $37.00");
                ddlBackgroundCheckPaidCode.Items.Add("Owe $27.00");
                ddlBackgroundCheckPaidCode.Items.Add("Owe $7.00");
            }
            catch (Exception lkjlkaabbc)
            {

            }
            finally
            {
                //con.Close();
            }
        }
        
        protected void PopulateVehichleInsuranceCodesList()
        {
            try
            {
                con.Open();

                string selectSQL = "select vehichleinsurancecodes " +
                                    "from volunteerdetails " +
                                    "group by vehichleinsurancecodes " +
                                    "order by vehichleinsurancecodes ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    ddlVehichleInsuranceCodes.Items.Add("Check Not Run");
                    do
                    {
                        ddlVehichleInsuranceCodes.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    ddlVehichleInsuranceCodes.Text = "Check Not Run";
                }
            }
            catch (Exception lkjlkaabbc)
            {


            }
            finally
            {
                con.Close();
            }
        }


        protected void PopulateCalenderDropDowns()
        {
            try
            {
                //Populate the date Months..RCM..8/15/12.
                ddlVehichleMonth.Items.Add("1");
                ddlVehichleMonth.Items.Add("2");
                ddlVehichleMonth.Items.Add("3");
                ddlVehichleMonth.Items.Add("4");
                ddlVehichleMonth.Items.Add("5");
                ddlVehichleMonth.Items.Add("6");
                ddlVehichleMonth.Items.Add("7");
                ddlVehichleMonth.Items.Add("8");
                ddlVehichleMonth.Items.Add("9");
                ddlVehichleMonth.Items.Add("10");
                ddlVehichleMonth.Items.Add("11");
                ddlVehichleMonth.Items.Add("12");

                ddlDMVMonth.Items.Add("1");
                ddlDMVMonth.Items.Add("2");
                ddlDMVMonth.Items.Add("3");
                ddlDMVMonth.Items.Add("4");
                ddlDMVMonth.Items.Add("5");
                ddlDMVMonth.Items.Add("6");
                ddlDMVMonth.Items.Add("7");
                ddlDMVMonth.Items.Add("8");
                ddlDMVMonth.Items.Add("9");
                ddlDMVMonth.Items.Add("10");
                ddlDMVMonth.Items.Add("11");
                ddlDMVMonth.Items.Add("12");

                //ddlNationalMonth.Items.Add("00");
                ddlNationalMonth.Items.Add("1");
                ddlNationalMonth.Items.Add("2");
                ddlNationalMonth.Items.Add("3");
                ddlNationalMonth.Items.Add("4");
                ddlNationalMonth.Items.Add("5");
                ddlNationalMonth.Items.Add("6");
                ddlNationalMonth.Items.Add("7");
                ddlNationalMonth.Items.Add("8");
                ddlNationalMonth.Items.Add("9");
                ddlNationalMonth.Items.Add("10");
                ddlNationalMonth.Items.Add("11");
                ddlNationalMonth.Items.Add("12");

                ddlPACriminalMonth.Items.Add("1");
                ddlPACriminalMonth.Items.Add("2");
                ddlPACriminalMonth.Items.Add("3");
                ddlPACriminalMonth.Items.Add("4");
                ddlPACriminalMonth.Items.Add("5");
                ddlPACriminalMonth.Items.Add("6");
                ddlPACriminalMonth.Items.Add("7");
                ddlPACriminalMonth.Items.Add("8");
                ddlPACriminalMonth.Items.Add("9");
                ddlPACriminalMonth.Items.Add("10");
                ddlPACriminalMonth.Items.Add("11");
                ddlPACriminalMonth.Items.Add("12");
                //----------------------------------

                //Populate the date Day..RCM..8/15/12.
                ddlVehichleDay.Items.Add("1");
                ddlVehichleDay.Items.Add("2");
                ddlVehichleDay.Items.Add("3");
                ddlVehichleDay.Items.Add("4");
                ddlVehichleDay.Items.Add("5");
                ddlVehichleDay.Items.Add("6");
                ddlVehichleDay.Items.Add("7");
                ddlVehichleDay.Items.Add("8");
                ddlVehichleDay.Items.Add("9");
                ddlVehichleDay.Items.Add("10");
                ddlVehichleDay.Items.Add("11");
                ddlVehichleDay.Items.Add("12");
                ddlVehichleDay.Items.Add("13");
                ddlVehichleDay.Items.Add("14");
                ddlVehichleDay.Items.Add("15");
                ddlVehichleDay.Items.Add("16");
                ddlVehichleDay.Items.Add("17");
                ddlVehichleDay.Items.Add("18");
                ddlVehichleDay.Items.Add("19");
                ddlVehichleDay.Items.Add("20");
                ddlVehichleDay.Items.Add("21");
                ddlVehichleDay.Items.Add("22");
                ddlVehichleDay.Items.Add("23");
                ddlVehichleDay.Items.Add("24");
                ddlVehichleDay.Items.Add("25");
                ddlVehichleDay.Items.Add("26");
                ddlVehichleDay.Items.Add("27");
                ddlVehichleDay.Items.Add("28");
                ddlVehichleDay.Items.Add("29");
                ddlVehichleDay.Items.Add("30");
                ddlVehichleDay.Items.Add("31");


                ddlDMVDay.Items.Add("1");
                ddlDMVDay.Items.Add("2");
                ddlDMVDay.Items.Add("3");
                ddlDMVDay.Items.Add("4");
                ddlDMVDay.Items.Add("5");
                ddlDMVDay.Items.Add("6");
                ddlDMVDay.Items.Add("7");
                ddlDMVDay.Items.Add("8");
                ddlDMVDay.Items.Add("9");
                ddlDMVDay.Items.Add("10");
                ddlDMVDay.Items.Add("11");
                ddlDMVDay.Items.Add("12");
                ddlDMVDay.Items.Add("13");
                ddlDMVDay.Items.Add("14");
                ddlDMVDay.Items.Add("15");
                ddlDMVDay.Items.Add("16");
                ddlDMVDay.Items.Add("17");
                ddlDMVDay.Items.Add("18");
                ddlDMVDay.Items.Add("19");
                ddlDMVDay.Items.Add("20");
                ddlDMVDay.Items.Add("21");
                ddlDMVDay.Items.Add("22");
                ddlDMVDay.Items.Add("23");
                ddlDMVDay.Items.Add("24");
                ddlDMVDay.Items.Add("25");
                ddlDMVDay.Items.Add("26");
                ddlDMVDay.Items.Add("27");
                ddlDMVDay.Items.Add("28");
                ddlDMVDay.Items.Add("29");
                ddlDMVDay.Items.Add("30");
                ddlDMVDay.Items.Add("31");

                //ddlNationalDay.Items.Add("00");
                ddlNationalDay.Items.Add("1");
                ddlNationalDay.Items.Add("2");
                ddlNationalDay.Items.Add("3");
                ddlNationalDay.Items.Add("4");
                ddlNationalDay.Items.Add("5");
                ddlNationalDay.Items.Add("6");
                ddlNationalDay.Items.Add("7");
                ddlNationalDay.Items.Add("8");
                ddlNationalDay.Items.Add("9");
                ddlNationalDay.Items.Add("10");
                ddlNationalDay.Items.Add("11");
                ddlNationalDay.Items.Add("12");
                ddlNationalDay.Items.Add("13");
                ddlNationalDay.Items.Add("14");
                ddlNationalDay.Items.Add("15");
                ddlNationalDay.Items.Add("16");
                ddlNationalDay.Items.Add("17");
                ddlNationalDay.Items.Add("18");
                ddlNationalDay.Items.Add("19");
                ddlNationalDay.Items.Add("20");
                ddlNationalDay.Items.Add("21");
                ddlNationalDay.Items.Add("22");
                ddlNationalDay.Items.Add("23");
                ddlNationalDay.Items.Add("24");
                ddlNationalDay.Items.Add("25");
                ddlNationalDay.Items.Add("26");
                ddlNationalDay.Items.Add("27");
                ddlNationalDay.Items.Add("28");
                ddlNationalDay.Items.Add("29");
                ddlNationalDay.Items.Add("30");
                ddlNationalDay.Items.Add("31");

                ddlPACriminalDay.Items.Add("1");
                ddlPACriminalDay.Items.Add("2");
                ddlPACriminalDay.Items.Add("3");
                ddlPACriminalDay.Items.Add("4");
                ddlPACriminalDay.Items.Add("5");
                ddlPACriminalDay.Items.Add("6");
                ddlPACriminalDay.Items.Add("7");
                ddlPACriminalDay.Items.Add("8");
                ddlPACriminalDay.Items.Add("9");
                ddlPACriminalDay.Items.Add("10");
                ddlPACriminalDay.Items.Add("11");
                ddlPACriminalDay.Items.Add("12");
                ddlPACriminalDay.Items.Add("13");
                ddlPACriminalDay.Items.Add("14");
                ddlPACriminalDay.Items.Add("15");
                ddlPACriminalDay.Items.Add("16");
                ddlPACriminalDay.Items.Add("17");
                ddlPACriminalDay.Items.Add("18");
                ddlPACriminalDay.Items.Add("19");
                ddlPACriminalDay.Items.Add("20");
                ddlPACriminalDay.Items.Add("21");
                ddlPACriminalDay.Items.Add("22");
                ddlPACriminalDay.Items.Add("23");
                ddlPACriminalDay.Items.Add("24");
                ddlPACriminalDay.Items.Add("25");
                ddlPACriminalDay.Items.Add("26");
                ddlPACriminalDay.Items.Add("27");
                ddlPACriminalDay.Items.Add("28");
                ddlPACriminalDay.Items.Add("29");
                ddlPACriminalDay.Items.Add("30");
                ddlPACriminalDay.Items.Add("31");

                //----------------------------------
                ddlPACriminalYear.Items.Add("1900");
                ddlPACriminalYear.Items.Add("1989");
                ddlPACriminalYear.Items.Add("1990");
                ddlPACriminalYear.Items.Add("1991");
                ddlPACriminalYear.Items.Add("1992");
                ddlPACriminalYear.Items.Add("1993");
                ddlPACriminalYear.Items.Add("1994");
                ddlPACriminalYear.Items.Add("1995");
                ddlPACriminalYear.Items.Add("1996");
                ddlPACriminalYear.Items.Add("1997");
                ddlPACriminalYear.Items.Add("1998");
                ddlPACriminalYear.Items.Add("1999");
                ddlPACriminalYear.Items.Add("2000");
                ddlPACriminalYear.Items.Add("2001");
                ddlPACriminalYear.Items.Add("2002");
                ddlPACriminalYear.Items.Add("2003");
                ddlPACriminalYear.Items.Add("2004");
                ddlPACriminalYear.Items.Add("2005");
                ddlPACriminalYear.Items.Add("2006");
                ddlPACriminalYear.Items.Add("2007");
                ddlPACriminalYear.Items.Add("2008");
                ddlPACriminalYear.Items.Add("2009");
                ddlPACriminalYear.Items.Add("2010");
                ddlPACriminalYear.Items.Add("2011");
                ddlPACriminalYear.Items.Add("2012");
                ddlPACriminalYear.Items.Add("2013");
                ddlPACriminalYear.Items.Add("2014");
                ddlPACriminalYear.Items.Add("2015");
                ddlPACriminalYear.Items.Add("2016");
                ddlPACriminalYear.Items.Add("2017");
                ddlPACriminalYear.Items.Add("2018");
                ddlPACriminalYear.Items.Add("2019");
                ddlPACriminalYear.Items.Add("2020");
                ddlPACriminalYear.Items.Add("2021");



                ddlDMVYear.Items.Add("1900");
                ddlDMVYear.Items.Add("1989");
                ddlDMVYear.Items.Add("1990");
                ddlDMVYear.Items.Add("1991");
                ddlDMVYear.Items.Add("1992");
                ddlDMVYear.Items.Add("1993");
                ddlDMVYear.Items.Add("1994");
                ddlDMVYear.Items.Add("1995");
                ddlDMVYear.Items.Add("1996");
                ddlDMVYear.Items.Add("1997");
                ddlDMVYear.Items.Add("1998");
                ddlDMVYear.Items.Add("1999");
                ddlDMVYear.Items.Add("2000");
                ddlDMVYear.Items.Add("2001");
                ddlDMVYear.Items.Add("2002");
                ddlDMVYear.Items.Add("2003");
                ddlDMVYear.Items.Add("2004");
                ddlDMVYear.Items.Add("2005");
                ddlDMVYear.Items.Add("2006");
                ddlDMVYear.Items.Add("2007");
                ddlDMVYear.Items.Add("2008");
                ddlDMVYear.Items.Add("2009");
                ddlDMVYear.Items.Add("2010");
                ddlDMVYear.Items.Add("2011");
                ddlDMVYear.Items.Add("2012");
                ddlDMVYear.Items.Add("2013");
                ddlDMVYear.Items.Add("2014");
                ddlDMVYear.Items.Add("2015");
                ddlDMVYear.Items.Add("2016");
                ddlDMVYear.Items.Add("2017");

                //ddlNationalYear.Items.Add("0000");
                ddlNationalYear.Items.Add("1900");
                ddlNationalYear.Items.Add("1989");
                ddlNationalYear.Items.Add("1990");
                ddlNationalYear.Items.Add("1991");
                ddlNationalYear.Items.Add("1992");
                ddlNationalYear.Items.Add("1993");
                ddlNationalYear.Items.Add("1994");
                ddlNationalYear.Items.Add("1995");
                ddlNationalYear.Items.Add("1996");
                ddlNationalYear.Items.Add("1997");
                ddlNationalYear.Items.Add("1998");
                ddlNationalYear.Items.Add("1999");
                ddlNationalYear.Items.Add("2000");
                ddlNationalYear.Items.Add("2001");
                ddlNationalYear.Items.Add("2002");
                ddlNationalYear.Items.Add("2003");
                ddlNationalYear.Items.Add("2004");
                ddlNationalYear.Items.Add("2005");
                ddlNationalYear.Items.Add("2006");
                ddlNationalYear.Items.Add("2007");
                ddlNationalYear.Items.Add("2008");
                ddlNationalYear.Items.Add("2009");
                ddlNationalYear.Items.Add("2010");
                ddlNationalYear.Items.Add("2011");
                ddlNationalYear.Items.Add("2012");
                ddlNationalYear.Items.Add("2013");
                ddlNationalYear.Items.Add("2014");
                ddlNationalYear.Items.Add("2015");
                ddlNationalYear.Items.Add("2016");
                ddlNationalYear.Items.Add("2017");

                ddlVehichleYear.Items.Add("1900");
                ddlVehichleYear.Items.Add("1989");
                ddlVehichleYear.Items.Add("1990");
                ddlVehichleYear.Items.Add("1991");
                ddlVehichleYear.Items.Add("1992");
                ddlVehichleYear.Items.Add("1993");
                ddlVehichleYear.Items.Add("1994");
                ddlVehichleYear.Items.Add("1995");
                ddlVehichleYear.Items.Add("1996");
                ddlVehichleYear.Items.Add("1997");
                ddlVehichleYear.Items.Add("1998");
                ddlVehichleYear.Items.Add("1999");
                ddlVehichleYear.Items.Add("2000");
                ddlVehichleYear.Items.Add("2001");
                ddlVehichleYear.Items.Add("2002");
                ddlVehichleYear.Items.Add("2003");
                ddlVehichleYear.Items.Add("2004");
                ddlVehichleYear.Items.Add("2005");
                ddlVehichleYear.Items.Add("2006");
                ddlVehichleYear.Items.Add("2007");
                ddlVehichleYear.Items.Add("2008");
                ddlVehichleYear.Items.Add("2009");
                ddlVehichleYear.Items.Add("2010");
                ddlVehichleYear.Items.Add("2011");
                ddlVehichleYear.Items.Add("2012");
                ddlVehichleYear.Items.Add("2013");
                ddlVehichleYear.Items.Add("2014");
                ddlVehichleYear.Items.Add("2015");
                ddlVehichleYear.Items.Add("2016");
                ddlVehichleYear.Items.Add("2017");
                ddlVehichleYear.Items.Add("2018");
                ddlVehichleYear.Items.Add("2019");
                ddlVehichleYear.Items.Add("2020");
                ddlVehichleYear.Items.Add("2021");
                ddlVehichleYear.Items.Add("2022");
                ddlVehichleYear.Items.Add("2023");
                ddlVehichleYear.Items.Add("2024");
                ddlVehichleYear.Items.Add("2025");
                ddlVehichleYear.Items.Add("2026");
                ddlVehichleYear.Items.Add("2027");
                ddlVehichleYear.Items.Add("2028");
            }
            catch (Exception lkjlkaabbc)
            {


            }
            finally
            {
                con.Close();
            }
        }


        protected void PopulateNationalCheckCodesList()
        {
            try
            {
                con.Open();

                string selectSQL = "select nationalcheckcodes " +
                                    "from volunteerdetails " +
                                    "group by nationalcheckcodes " +
                                    "order by nationalcheckcodes ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    ddlNationalCheckCodes.Items.Add("Check Not Run");
                    ddlNationalCheckReport.Items.Add("Select a Code?");
                    ddlNationalCheckCodes.Items.Add("Cleared Without Restrictions");
                    ddlNationalCheckReport.Items.Add("Cleared Without Restrictions");

                    //--------------------------------------------                    
                    //ddlGrades.Items.Add("Select a value");
                    //ddlGrades.Items.Add("Check Not Run");
                    //ddlGrades.Items.Add("Cleared Without Restrictions");

                    //ddlGrades2.Items.Add("Select a value");
                    //ddlGrades2.Items.Add("Check Not Run");
                    //ddlGrades2.Items.Add("Cleared Without Restrictions");

                    //ddlGrades3.Items.Add("Select a value");
                    //ddlGrades3.Items.Add("Check Not Run");
                    //ddlGrades3.Items.Add("Cleared Without Restrictions");

                    //ddlGrades4.Items.Add("Select a value");
                    //ddlGrades4.Items.Add("Check Not Run");
                    //ddlGrades4.Items.Add("Cleared Without Restrictions");

                    //ddlGrades5.Items.Add("Select a value");
                    //ddlGrades5.Items.Add("Check Not Run");
                    //ddlGrades5.Items.Add("Cleared Without Restrictions");
                    //----------------------------------------------------------------

                    do
                    {
                        ddlNationalCheckCodes.Items.Add(reader.GetString(0));
                        ddlNationalCheckReport.Items.Add(reader.GetString(0));

                        //ddlSearchValueBool.Items.Add(reader.GetString(0));
                        //ddlSearchValue2Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue3Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue4Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue5Bool.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    ddlNationalCheckCodes.Text = "Check Not Run";
                    ddlNationalCheckReport.Text = "Select a Code?";
                    //ddlSearchValue2Bool.Text = "Check Not Run";
                    //ddlSearchValue3Bool.Text = "Check Not Run";
                    //ddlSearchValue4Bool.Text = "Check Not Run";
                    //ddlSearchValue5Bool.Text = "Check Not Run";
                }
            }
            catch (Exception lkjlkaabbc)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void PopulateSearchValueBoolean()
        {

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

        }


        protected void PopulateGradesWithCodes()
        {

            ddlGrades.Items.Add("Select a value");
            ddlGrades.Items.Add("Check Not Run");
            ddlGrades.Items.Add("Cleared Without Restrictions");
            ddlGrades.Items.Add("Cleared With Restrictions");
            ddlGrades.Items.Add("InActive, Run Clearance Upon Return");
            ddlGrades.Items.Add("No Record");
            ddlGrades.Items.Add("Permanently Denied");
            ddlGrades.Items.Add("STAFF");
            ddlGrades.Items.Add("Under 18");

            ddlGrades2.Items.Add("Select a value");
            ddlGrades2.Items.Add("Check Not Run");
            ddlGrades2.Items.Add("Cleared Without Restrictions");
            ddlGrades2.Items.Add("Cleared With Restrictions");
            ddlGrades2.Items.Add("InActive, Run Clearance Upon Return");
            ddlGrades2.Items.Add("No Record");
            ddlGrades2.Items.Add("Permanently Denied");
            ddlGrades2.Items.Add("STAFF");
            ddlGrades2.Items.Add("Under 18");

            ddlGrades3.Items.Add("Select a value");
            ddlGrades3.Items.Add("Check Not Run");
            ddlGrades3.Items.Add("Cleared Without Restrictions");
            ddlGrades3.Items.Add("Cleared With Restrictions");
            ddlGrades3.Items.Add("InActive, Run Clearance Upon Return");
            ddlGrades3.Items.Add("No Record");
            ddlGrades3.Items.Add("Permanently Denied");
            ddlGrades3.Items.Add("STAFF");
            ddlGrades3.Items.Add("Under 18");

            ddlGrades4.Items.Add("Select a value");
            ddlGrades4.Items.Add("Check Not Run");
            ddlGrades4.Items.Add("Cleared Without Restrictions");
            ddlGrades4.Items.Add("Cleared With Restrictions");
            ddlGrades4.Items.Add("InActive, Run Clearance Upon Return");
            ddlGrades4.Items.Add("No Record");
            ddlGrades4.Items.Add("Permanently Denied");
            ddlGrades4.Items.Add("STAFF");
            ddlGrades4.Items.Add("Under 18");

            ddlGrades5.Items.Add("Select a value");
            ddlGrades5.Items.Add("Check Not Run");
            ddlGrades5.Items.Add("Cleared Without Restrictions");
            ddlGrades5.Items.Add("Cleared With Restrictions");
            ddlGrades5.Items.Add("InActive, Run Clearance Upon Return");
            ddlGrades5.Items.Add("No Record");
            ddlGrades5.Items.Add("Permanently Denied");
            ddlGrades5.Items.Add("STAFF");
            ddlGrades5.Items.Add("Under 18");

        }


        protected void PopulateDMVCheckCodesList()
        {
            try
            {
                con.Open();

                string selectSQL = "select dmvcheckcodes " +
                                    "from volunteerdetails " +
                                    "group by dmvcheckcodes " +
                                    "order by dmvcheckcodes ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    ddlDMVCheckCodes.Items.Add("Check Not Run");
                    ddlDMVCheckReport.Items.Add("Select a Code?");
                    //ddlDMVCheckCodes.Items.Add("Cleared With Restrictions");
                    ddlDMVCheckCodes.Items.Add("Cleared Without Restrictions");
                    //ddlDMVCheckReport.Items.Add("Cleared With Restrictions");
                    ddlDMVCheckReport.Items.Add("Cleared Without Restrictions");

                    //--------------------------------------------                    
                    //ddlGrades.Items.Add("Select a value");
                    //ddlGrades.Items.Add("Check Not Run");
                    //ddlGrades.Items.Add("Cleared Without Restrictions");

                    //ddlGrades2.Items.Add("Select a value");
                    //ddlGrades2.Items.Add("Check Not Run");
                    //ddlGrades2.Items.Add("Cleared Without Restrictions");
                    //XhtmlMobileDocType
                    //ddlGrades3.Items.Add("Select a value");
                    //ddlGrades3.Items.Add("Check Not Run");
                    //ddlGrades3.Items.Add("Cleared Without Restrictions");

                    //ddlGrades4.Items.Add("Select a value");
                    //ddlGrades4.Items.Add("Check Not Run");
                    //ddlGrades4.Items.Add("Cleared Without Restrictions");

                    //ddlGrades5.Items.Add("Select a value");
                    //ddlGrades5.Items.Add("Check Not Run");
                    //ddlGrades5.Items.Add("Cleared Without Restrictions");
                    //----------------------------------------------------------------
                    
                    do
                    {
                        ddlDMVCheckCodes.Items.Add(reader.GetString(0));
                        ddlDMVCheckReport.Items.Add(reader.GetString(0));

                        //ddlGrades.Items.Add(reader.GetString(0));
                        //ddlGrades2.Items.Add(reader.GetString(0));
                        //ddlGrades3.Items.Add(reader.GetString(0));
                        //ddlGrades4.Items.Add(reader.GetString(0));
                        //ddlGrades5.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    ddlDMVCheckCodes.Text = "Check Not Run";
                    ddlDMVCheckReport.Text = "Select a Code?";
                    //ddlGrades.Text = "Check Not Run";
                    //ddlGrades2.Text = "Check Not Run";
                    //ddlGrades3.Text = "Check Not Run";
                    //ddlGrades4.Text = "Check Not Run";
                    //ddlGrades5.Text = "Check Not Run";
                }
            }
            catch (Exception lkjlkaabbc)
            {

            }
            finally
            {
                con.Close();
            }
        }
        
        protected void PopulateBackgroundCheckCodesList()
        {
            try
            {
                con.Open();

                string selectSQL = "select backgroundcheckcodes " +
                                    "from volunteerdetails " +
                                    "group by backgroundcheckcodes " +
                                    "order by backgroundcheckcodes ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    ddlBackgroundCheckCodes.Items.Add("Checks Not Run");

                    //ddlDMVCheckCodes.Items.Add("Cleared With Restrictions");
                    ddlDMVCheckCodes.Items.Add("Cleared Without Restrictions");
                    //ddlDMVCheckReport.Items.Add("Cleared With Restrictions");
                    ddlDMVCheckReport.Items.Add("Cleared Without Restrictions");

                    //--------------------------------------------                    
                    //ddlSearchValueBool.Items.Add("Select a value");
                    //ddlSearchValueBool.Items.Add("Check Not Run");
                    //ddlSearchValueBool.Items.Add("Cleared Without Restrictions");
                    //----------------------------------------------------------------

                    do
                    {
                        ddlBackgroundCheckCodes.Items.Add(reader.GetString(0));

                        //ddlSearchValueBool.Items.Add(reader.GetString(0));
                        //ddlSearchValue2Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue3Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue4Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue5Bool.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    ddlBackgroundCheckCodes.Text = "Checks Not Run";
                    ddlPACriminalCodes.Text = "Select a Code?";
                    //ddlSearchValue2Bool.Text = "Check Not Run";
                    //ddlSearchValue3Bool.Text = "Check Not Run";
                    //ddlSearchValue4Bool.Text = "Check Not Run";
                    //ddlSearchValue5Bool.Text = "Check Not Run";
                }
            }
            catch (Exception lkjlkaabbc)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void PopulatePACriminalCheckCodesList()
        {
            try
            {
                con.Open();

                string selectSQL = "select pacriminalcheckcodes " +
                                    "from volunteerdetails " +
                                    "group by pacriminalcheckcodes " +
                                    "order by pacriminalcheckcodes ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    ddlCriminalCheckCodes.Items.Add("Check Not Run");
                    ddlPACriminalCodes.Items.Add("Select a Code?");

                    //ddlDMVCheckCodes.Items.Add("Cleared With Restrictions");
                    ddlDMVCheckCodes.Items.Add("Cleared Without Restrictions");
                    //ddlDMVCheckReport.Items.Add("Cleared With Restrictions");
                    ddlDMVCheckReport.Items.Add("Cleared Without Restrictions");

                    //--------------------------------------------                    
                    //ddlSearchValueBool.Items.Add("Select a value");
                    //ddlSearchValueBool.Items.Add("Check Not Run");
                    //ddlSearchValueBool.Items.Add("Cleared Without Restrictions");
                    //----------------------------------------------------------------

                    do
                    {
                        ddlCriminalCheckCodes.Items.Add(reader.GetString(0));
                        ddlPACriminalCodes.Items.Add(reader.GetString(0));

                        //ddlSearchValueBool.Items.Add(reader.GetString(0));
                        //ddlSearchValue2Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue3Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue4Bool.Items.Add(reader.GetString(0));
                        //ddlSearchValue5Bool.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    ddlCriminalCheckCodes.Text = "Check Not Run";
                    ddlPACriminalCodes.Text = "Select a Code?";
                    //ddlSearchValue2Bool.Text = "Check Not Run";
                    //ddlSearchValue3Bool.Text = "Check Not Run";
                    //ddlSearchValue4Bool.Text = "Check Not Run";
                    //ddlSearchValue5Bool.Text = "Check Not Run";
                }
            }
            catch (Exception lkjlkaabbc)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void PopulateVolunteerList()
        {
            try
            {
                con.Open();

                string selectSQL = "select lastname, firstname " +
                                    "from volunteerinformation " +
                                    "group by lastname, firstname " +
                                    "order by lastname, firstname ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    ddlNames.Items.Add("Please select a volunteer");
                    do
                    {
                        ddlNames.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                    } while (reader.Read());
                    reader.Close();
                    ddlNames.Text = "Please select a volunteer";
                }
            }
            catch (Exception lkjlkaabbc)
            {


            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbProgram_Click(object sender, EventArgs e)
        {

            ////Retrieve the class lists
            //try
            //{
            //    con.Open();//Opens the db connection.
            //    string sql_LoadGrid = "";


            //    if (ddlProgram.Text == "MSHS Choir")
            //    {
            //        sql_LoadGrid = "select Lastname, Firstname, Grade "
            //                            + "FROM PerformingArtsAcademyStudents "
            //                            + "WHERE mshschoir = 1 "
            //                            + "order by lastname, firstname ";

            //        //Perform database lookup based on the chosen child..RCM..
            //        SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //        cmd.Connection = con;
            //        gvStudentList.DataSource = cmd.ExecuteReader();
            //        gvStudentList.DataBind();
            //        gvStudentList.Columns[1].HeaderText = "Test";
            //    }
            //    else if (ddlProgram.Text == "Childrens Choir")
            //    {
            //        sql_LoadGrid = "select Lastname, Firstname, Grade"
            //                            + "FROM PerformingArtsAcademyStudents "
            //                            + "WHERE childrenschoir = 1 "
            //                            + "order by lastname, firstname ";

            //        //Perform database lookup based on the chosen child..RCM..
            //        SqlCommand cmd = new SqlCommand(sql_LoadGrid);

            //        cmd.Connection = con;
            //        gvStudentList.DataSource = cmd.ExecuteReader();
            //        gvStudentList.DataBind();
            //    }
            //    else if (ddlProgram.Text == "PerformingArtsAcademy")
            //    {
            //        lblProgram.Visible = false;
            //        cmbProgram.Visible = false;
            //        ddlProgram.Enabled = false;
            //        ddlProgram.Visible = false;

            //        sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', sizelimit as 'SizeLimit', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM PerformingArtsAcademyAvailableClasses  order by classname";

            //        SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //        DataSet ds = new DataSet();
            //        da.Fill(ds, "PerformingArtsAcademyAvailableClasses");
            //        gvStudentList.DataSource = ds.Tables[0];
            //        gvStudentList.DataBind();
            //        con.Close(); 
            //    }        
            //}
            //catch (Exception lkjl_)
            //{

            //    string lkjl = "";
            //}
        }

        
        
        protected void gvStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow && row.RowIndex == irowNum)
            {        

                    DataRowView drv = (DataRowView)e.Row.DataItem;

                    string ClassName = drv.Row.ItemArray.GetValue(0).ToString();
                    string ClassTime = drv.Row.ItemArray.GetValue(1).ToString();
                    string ClassDay = drv.Row.ItemArray.GetValue(2).ToString();
                    string Location = drv.Row.ItemArray.GetValue(3).ToString();
                    string Comments = drv.Row.ItemArray.GetValue(4).ToString();
                    string Instructor = drv.Row.ItemArray.GetValue(5).ToString();
                    string devotional = drv.Row.ItemArray.GetValue(6).ToString();
                    //int sizelimit = drv.Row.ItemArray.GetValue(7);

           //         Response.Redirect("AcademyClassEnrollment.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&ClassName=" + ClassName + "&ClassTime=" + ClassTime + "&ClassDay=" + ClassDay);
                    
            }       
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

            ddlPickDateRangeMonth22.Items.Add("Month");
            ddlPickDateRangeMonth22.Items.Add("January");
            ddlPickDateRangeMonth22.Items.Add("February");
            ddlPickDateRangeMonth22.Items.Add("March");
            ddlPickDateRangeMonth22.Items.Add("April");
            ddlPickDateRangeMonth22.Items.Add("May");
            ddlPickDateRangeMonth22.Items.Add("June");
            ddlPickDateRangeMonth22.Items.Add("July");
            ddlPickDateRangeMonth22.Items.Add("August");
            ddlPickDateRangeMonth22.Items.Add("September");
            ddlPickDateRangeMonth22.Items.Add("October");
            ddlPickDateRangeMonth22.Items.Add("November");
            ddlPickDateRangeMonth22.Items.Add("December");
            ddlPickDateRangeMonth22.Text = "Month";

            ddlPickDateRangeMonth3.Items.Add("Month");
            ddlPickDateRangeMonth3.Items.Add("January");
            ddlPickDateRangeMonth3.Items.Add("February");
            ddlPickDateRangeMonth3.Items.Add("March");
            ddlPickDateRangeMonth3.Items.Add("April");
            ddlPickDateRangeMonth3.Items.Add("May");
            ddlPickDateRangeMonth3.Items.Add("June");
            ddlPickDateRangeMonth3.Items.Add("July");
            ddlPickDateRangeMonth3.Items.Add("August");
            ddlPickDateRangeMonth3.Items.Add("September");
            ddlPickDateRangeMonth3.Items.Add("October");
            ddlPickDateRangeMonth3.Items.Add("November");
            ddlPickDateRangeMonth3.Items.Add("December");
            ddlPickDateRangeMonth3.Text = "Month";

            ddlPickDateRangeMonth4.Items.Add("Month");
            ddlPickDateRangeMonth4.Items.Add("January");
            ddlPickDateRangeMonth4.Items.Add("February");
            ddlPickDateRangeMonth4.Items.Add("March");
            ddlPickDateRangeMonth4.Items.Add("April");
            ddlPickDateRangeMonth4.Items.Add("May");
            ddlPickDateRangeMonth4.Items.Add("June");
            ddlPickDateRangeMonth4.Items.Add("July");
            ddlPickDateRangeMonth4.Items.Add("August");
            ddlPickDateRangeMonth4.Items.Add("September");
            ddlPickDateRangeMonth4.Items.Add("October");
            ddlPickDateRangeMonth4.Items.Add("November");
            ddlPickDateRangeMonth4.Items.Add("December");
            ddlPickDateRangeMonth4.Text = "Month";

            ddlPickDateRangeMonth5.Items.Add("Month");
            ddlPickDateRangeMonth5.Items.Add("January");
            ddlPickDateRangeMonth5.Items.Add("February");
            ddlPickDateRangeMonth5.Items.Add("March");
            ddlPickDateRangeMonth5.Items.Add("April");
            ddlPickDateRangeMonth5.Items.Add("May");
            ddlPickDateRangeMonth5.Items.Add("June");
            ddlPickDateRangeMonth5.Items.Add("July");
            ddlPickDateRangeMonth5.Items.Add("August");
            ddlPickDateRangeMonth5.Items.Add("September");
            ddlPickDateRangeMonth5.Items.Add("October");
            ddlPickDateRangeMonth5.Items.Add("November");
            ddlPickDateRangeMonth5.Items.Add("December");
            ddlPickDateRangeMonth5.Text = "Month";

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

            ddlPickDateRangeDay22.Items.Add("Day");
            ddlPickDateRangeDay22.Items.Add("01");
            ddlPickDateRangeDay22.Items.Add("02");
            ddlPickDateRangeDay22.Items.Add("03");
            ddlPickDateRangeDay22.Items.Add("04");
            ddlPickDateRangeDay22.Items.Add("05");
            ddlPickDateRangeDay22.Items.Add("06");
            ddlPickDateRangeDay22.Items.Add("07");
            ddlPickDateRangeDay22.Items.Add("08");
            ddlPickDateRangeDay22.Items.Add("09");
            ddlPickDateRangeDay22.Items.Add("10");
            ddlPickDateRangeDay22.Items.Add("11");
            ddlPickDateRangeDay22.Items.Add("12");
            ddlPickDateRangeDay22.Items.Add("13");
            ddlPickDateRangeDay22.Items.Add("14");
            ddlPickDateRangeDay22.Items.Add("15");
            ddlPickDateRangeDay22.Items.Add("16");
            ddlPickDateRangeDay22.Items.Add("17");
            ddlPickDateRangeDay22.Items.Add("18");
            ddlPickDateRangeDay22.Items.Add("19");
            ddlPickDateRangeDay22.Items.Add("20");
            ddlPickDateRangeDay22.Items.Add("21");
            ddlPickDateRangeDay22.Items.Add("22");
            ddlPickDateRangeDay22.Items.Add("23");
            ddlPickDateRangeDay22.Items.Add("24");
            ddlPickDateRangeDay22.Items.Add("25");
            ddlPickDateRangeDay22.Items.Add("26");
            ddlPickDateRangeDay22.Items.Add("27");
            ddlPickDateRangeDay22.Items.Add("28");
            ddlPickDateRangeDay22.Items.Add("29");
            ddlPickDateRangeDay22.Items.Add("30");
            ddlPickDateRangeDay22.Items.Add("31");
            ddlPickDateRangeDay22.Text = "Day";


            ddlPickDateRangeDay3.Items.Add("Day");
            ddlPickDateRangeDay3.Items.Add("01");
            ddlPickDateRangeDay3.Items.Add("02");
            ddlPickDateRangeDay3.Items.Add("03");
            ddlPickDateRangeDay3.Items.Add("04");
            ddlPickDateRangeDay3.Items.Add("05");
            ddlPickDateRangeDay3.Items.Add("06");
            ddlPickDateRangeDay3.Items.Add("07");
            ddlPickDateRangeDay3.Items.Add("08");
            ddlPickDateRangeDay3.Items.Add("09");
            ddlPickDateRangeDay3.Items.Add("10");
            ddlPickDateRangeDay3.Items.Add("11");
            ddlPickDateRangeDay3.Items.Add("12");
            ddlPickDateRangeDay3.Items.Add("13");
            ddlPickDateRangeDay3.Items.Add("14");
            ddlPickDateRangeDay3.Items.Add("15");
            ddlPickDateRangeDay3.Items.Add("16");
            ddlPickDateRangeDay3.Items.Add("17");
            ddlPickDateRangeDay3.Items.Add("18");
            ddlPickDateRangeDay3.Items.Add("19");
            ddlPickDateRangeDay3.Items.Add("20");
            ddlPickDateRangeDay3.Items.Add("21");
            ddlPickDateRangeDay3.Items.Add("22");
            ddlPickDateRangeDay3.Items.Add("23");
            ddlPickDateRangeDay3.Items.Add("24");
            ddlPickDateRangeDay3.Items.Add("25");
            ddlPickDateRangeDay3.Items.Add("26");
            ddlPickDateRangeDay3.Items.Add("27");
            ddlPickDateRangeDay3.Items.Add("28");
            ddlPickDateRangeDay3.Items.Add("29");
            ddlPickDateRangeDay3.Items.Add("30");
            ddlPickDateRangeDay3.Items.Add("31");
            ddlPickDateRangeDay3.Text = "Day";


            ddlPickDateRangeDay4.Items.Add("Day");
            ddlPickDateRangeDay4.Items.Add("01");
            ddlPickDateRangeDay4.Items.Add("02");
            ddlPickDateRangeDay4.Items.Add("03");
            ddlPickDateRangeDay4.Items.Add("04");
            ddlPickDateRangeDay4.Items.Add("05");
            ddlPickDateRangeDay4.Items.Add("06");
            ddlPickDateRangeDay4.Items.Add("07");
            ddlPickDateRangeDay4.Items.Add("08");
            ddlPickDateRangeDay4.Items.Add("09");
            ddlPickDateRangeDay4.Items.Add("10");
            ddlPickDateRangeDay4.Items.Add("11");
            ddlPickDateRangeDay4.Items.Add("12");
            ddlPickDateRangeDay4.Items.Add("13");
            ddlPickDateRangeDay4.Items.Add("14");
            ddlPickDateRangeDay4.Items.Add("15");
            ddlPickDateRangeDay4.Items.Add("16");
            ddlPickDateRangeDay4.Items.Add("17");
            ddlPickDateRangeDay4.Items.Add("18");
            ddlPickDateRangeDay4.Items.Add("19");
            ddlPickDateRangeDay4.Items.Add("20");
            ddlPickDateRangeDay4.Items.Add("21");
            ddlPickDateRangeDay4.Items.Add("22");
            ddlPickDateRangeDay4.Items.Add("23");
            ddlPickDateRangeDay4.Items.Add("24");
            ddlPickDateRangeDay4.Items.Add("25");
            ddlPickDateRangeDay4.Items.Add("26");
            ddlPickDateRangeDay4.Items.Add("27");
            ddlPickDateRangeDay4.Items.Add("28");
            ddlPickDateRangeDay4.Items.Add("29");
            ddlPickDateRangeDay4.Items.Add("30");
            ddlPickDateRangeDay4.Items.Add("31");
            ddlPickDateRangeDay4.Text = "Day";


            ddlPickDateRangeDay5.Items.Add("Day");
            ddlPickDateRangeDay5.Items.Add("01");
            ddlPickDateRangeDay5.Items.Add("02");
            ddlPickDateRangeDay5.Items.Add("03");
            ddlPickDateRangeDay5.Items.Add("04");
            ddlPickDateRangeDay5.Items.Add("05");
            ddlPickDateRangeDay5.Items.Add("06");
            ddlPickDateRangeDay5.Items.Add("07");
            ddlPickDateRangeDay5.Items.Add("08");
            ddlPickDateRangeDay5.Items.Add("09");
            ddlPickDateRangeDay5.Items.Add("10");
            ddlPickDateRangeDay5.Items.Add("11");
            ddlPickDateRangeDay5.Items.Add("12");
            ddlPickDateRangeDay5.Items.Add("13");
            ddlPickDateRangeDay5.Items.Add("14");
            ddlPickDateRangeDay5.Items.Add("15");
            ddlPickDateRangeDay5.Items.Add("16");
            ddlPickDateRangeDay5.Items.Add("17");
            ddlPickDateRangeDay5.Items.Add("18");
            ddlPickDateRangeDay5.Items.Add("19");
            ddlPickDateRangeDay5.Items.Add("20");
            ddlPickDateRangeDay5.Items.Add("21");
            ddlPickDateRangeDay5.Items.Add("22");
            ddlPickDateRangeDay5.Items.Add("23");
            ddlPickDateRangeDay5.Items.Add("24");
            ddlPickDateRangeDay5.Items.Add("25");
            ddlPickDateRangeDay5.Items.Add("26");
            ddlPickDateRangeDay5.Items.Add("27");
            ddlPickDateRangeDay5.Items.Add("28");
            ddlPickDateRangeDay5.Items.Add("29");
            ddlPickDateRangeDay5.Items.Add("30");
            ddlPickDateRangeDay5.Items.Add("31");
            ddlPickDateRangeDay5.Text = "Day";


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
            ddlPickDateRangeYear22.Items.Add("Year");
            ddlPickDateRangeYear3.Items.Add("Year");
            ddlPickDateRangeYear4.Items.Add("Year");
            ddlPickDateRangeYear5.Items.Add("Year");
            for (int intYear = (DateTime.Now.Year - 20); intYear <= (DateTime.Now.Year + 20); intYear++)
            {
                //ddlCalYear.Items.Add(intYear.ToString());
                ddlPickDateRangeYear1.Items.Add(intYear.ToString());
                ddlPickDateRangeYear2.Items.Add(intYear.ToString());
                ddlPickDateRangeYear22.Items.Add(intYear.ToString());
                ddlPickDateRangeYear3.Items.Add(intYear.ToString());
                ddlPickDateRangeYear4.Items.Add(intYear.ToString());
                ddlPickDateRangeYear5.Items.Add(intYear.ToString());
                //ddlCalYear.Items.Add(intYear);    
                //Next
            }
            //ddlCalYear.Text = "Year";
            ddlPickDateRangeYear1.Text = "Year";
            ddlPickDateRangeYear2.Text = "Year";
            ddlPickDateRangeYear22.Text = "Year";
            ddlPickDateRangeYear3.Text = "Year";
            ddlPickDateRangeYear4.Text = "Year";
            ddlPickDateRangeYear5.Text = "Year";

            //Make the current year selected item in the list
            //            ddlCalYear.Items.FindByValue(DateTime.Now.Year).Selected = True;
        }


        protected void gvStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvStudentList.SelectedRow;
            irowNum = gvStudentList.SelectedIndex;
            bind();
        }

        protected void gvStudentList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvStudentList.Rows[e.NewSelectedIndex];
        }

        public void bind()  
        {  
             con.Open();

             string sql_LoadGrid = "";
                    sql_LoadGrid = "select vd.LastName + ',' + vd.FirstName as 'Name', "
                                 + "vd.GeneralInformation as 'GeneralInfo', "
                                 + "vd.SpiritualJourney as 'SpirJourney', "
                                 + "vd.ReleaseWaiver, "
                                 + "vd.NewVolunteerTraining as 'NewVolTrain', "
                                 + "vd.NewVolunteerTrainingDate as 'NewVolTrainDate', "
                                 + "vd.VehichleInsurance as 'VehInsur', "
                                 + "vd.VehichleInsuranceCodes as 'VehInsurCodes', "
                                 + "vd.VehichleInsuranceDate as 'VehInsurDate', "
                                 + "vd.BackgroundCheck as 'BckgrndCk', "
                                 + "vd.NationalCheck as 'NatCk', "
                                 + "vd.NationalCheckCodes as 'NatCkCodes', "
                                 + "vd.NationalCheckDate as 'NatCkDate', "
                                 + "vd.DMVCheck as 'DMVCk', "
                                 + "vd.DMVCheckCodes as 'DMVCkCodes', "
                                 + "vd.DMVCheckDate as 'DMVCkDate', "
                                 + "vd.PACriminalCheck as 'PACrimCk', "
                                 + "vd.PACriminalCheckCodes as 'PACrimCkCodes', "
                                 + "vd.PACriminalCheckDate as 'PACrimCkDate', "
                                 + "vd.BackgroundCheckPAID as 'BckgrndCkPd', "
                                 + "vd.BackgroundCheckCodes as 'BckgrndCkCodes', "
                                 + "vd.BackgroundCheckPAIDDate as 'BckgrndCkPdDate', "
                                 + "vd.Comments, "
                                 //+ "vd.SysCreate, "
                                 //+ "vd.sysupdate, "
                                 + "vd.LastUpdatedBy, "
                                 + "vd.ID "
                                 + "from volunteerdetails vd ";
            
             SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);  
             DataSet ds = new DataSet();  
             da.Fill(ds, "VolunteerDetails");  
             gvStudentList.DataSource = ds.Tables[0];  
             gvStudentList.DataBind();  
             con.Close();  
        }  

        protected void gvStudentList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStudentList.EditIndex = e.NewEditIndex;
            bind();
        }

        protected void gvStudentList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //gvStudentList.DeleteRow(e.RowIndex);
            gvStudentList.DataBind();
        }

        protected void gvStudentList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
             gvStudentList.EditIndex = -1;  
             bind();  
        }

        protected void gvStudentList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvStudentList.Rows[e.RowIndex];
            //Label lbl = (Label)row.FindControl("lblid");
            TextBox classname = (TextBox)row.FindControl("textbox1");
            TextBox classmeettime = (TextBox)row.FindControl("textbox2");
            TextBox classmeetday = (TextBox)row.FindControl("textbox3");
            TextBox classlocation = (TextBox)row.FindControl("textbox4");
            TextBox comments = (TextBox)row.FindControl("textbox5");
            TextBox instructor = (TextBox)row.FindControl("textbox6");
            TextBox devotionalleader = (TextBox)row.FindControl("textbox7");
            TextBox ID = (TextBox)row.FindControl("textbox8");
            TextBox sizelimit = (TextBox)row.FindControl("textbox20");

            classname.Text = classname.Text.Replace("'", "");
            classmeettime.Text = classmeettime.Text.Replace("'", "");
            classmeetday.Text = classmeetday.Text.Replace("'", "");
            classlocation.Text = classlocation.Text.Replace("'", "");
            comments.Text = comments.Text.Replace("'", "");
            instructor.Text = instructor.Text.Replace("'", "");
            devotionalleader.Text = devotionalleader.Text.Replace("'", "");
            sizelimit.Text = sizelimit.Text.Replace("'", "");

            gvStudentList.EditIndex = -1;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            if (ddlProgram.Text == "BasketballTEAMS")
            {
                cmd = new SqlCommand("Update BasketballTEAMSProgramSections set "
                                    + "  instructor='" + instructor.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , classname='" + classname.Text
                                    + "' , meettime='" + classmeettime.Text
                                    + "' , meetday='" + classmeetday.Text
                                    + "' , location='" + classlocation.Text
                                    + "' , sizelimit=" + sizelimit.Text
                                    + "  , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "  
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Baseball")
            {
                cmd = new SqlCommand("Update BaseballProgramSections set "
                                    + "  instructor='" + instructor.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , classname='" + classname.Text
                                    + "' , meettime='" + classmeettime.Text
                                    + "' , meetday='" + classmeetday.Text
                                    + "' , location='" + classlocation.Text
                                    + "' , sizelimit=" + sizelimit.Text
                                    + "  , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "  
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                cmd = new SqlCommand("Update OutreachBasketballProgramSections set "
                                    + "  instructor='" + instructor.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , classname='" + classname.Text
                                    + "' , meettime='" + classmeettime.Text
                                    + "' , meetday='" + classmeetday.Text
                                    + "' , location='" + classlocation.Text
                                    + "' , sizelimit=" + sizelimit.Text
                                    + "  , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "  
                                    + " where ID = '" + ID.Text + "' ");
            }
            
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            bind();
        }

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            if (((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")) || ((Request.QueryString["LastName"] == "Gilmore") && (Request.QueryString["FirstName"] == "Anna")) || ((Request.QueryString["LastName"] == "Brazil") && (Request.QueryString["FirstName"] == "Nehemiah")) || ((Request.QueryString["LastName"] == "Braunersrither") && (Request.QueryString["FirstName"] == "Chad")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Nate")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Hannah")))
            {

            }
            else
            {
                if (ddlNames.Text == "Please select a volunteer")
                {

                }
                else
                {
                    UpdateVolunteerDetails(ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(",")), ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
                }
            }
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (ddlNames.Text != "Please select a volunteer")
            {
                if (((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")) || ((Request.QueryString["LastName"] == "Gilmore") && (Request.QueryString["FirstName"] == "Anna")) || ((Request.QueryString["LastName"] == "Brazil") && (Request.QueryString["FirstName"] == "Nehemiah")) || ((Request.QueryString["LastName"] == "Braunersrither") && (Request.QueryString["FirstName"] == "Chad")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Nate")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Hannah")))
                {

                }
                else
                {
                    UpdateVolunteerDetails(ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(",")), ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
                }
            }
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            gvCustomView.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            ExcelExport.ExportGridView(gvCustomView, Response);
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql_LoadGrid = "";

            gvStudentList.Visible = false;
            cmbAddRecord.Visible = false;
            txbClassLocation.Visible = false;
            txbClassName.Visible = false;
            txbComments.Visible = false;
            txbDay.Visible = false;
            txbInstructor.Visible = false;
            txbSizeLimit.Visible = false;
            txbDevotionalLeader.Visible = false;
            txbClassName.Visible = false;
            ddlTime.Visible = false;

            lblClassInstructor.Visible = false;
            lblClassLocation.Visible = false;
            lblComments.Visible = false;
            lblDay.Visible = false;
            lblTime.Visible = false;
            lblSizeLimit.Visible = false;
            lblClassName.Visible = false;
            lblDevotionalLeader.Visible = false;



            lbAddNewEntry.Visible = true;
            lbAddNewEntry.Enabled = true;
            try
            {


                //ddlProgram.Items.Add("Please select maintenance details");
                //ddlProgram.Items.Add("Volunteer Profile Information");
                //ddlProgram.Items.Add("Volunteer Background Check Details");
                //ddlProgram.Items.Add("Volunteer Vehichle Insurance Details");
                //ddlProgram.Text = "Please select maintenance details";
                
                
                if (ddlProgram.Text == "Volunteer Profile Information")
                {
                    sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', sizelimit as 'SizeLimit', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BasketballTEAMSProgramSections  order by classname";
                }
                else if (ddlProgram.Text == "Volunteer Background Details")
                {
                    sql_LoadGrid = "select vd.LastName + ',' + vd.FirstName as 'Name', "
                                 + "vd.GeneralInformation as 'GeneralInfo', "
                                 + "vd.SpiritualJourney as 'SpirJourney', "
                                 + "vd.ReleaseWaiver, "
                                 + "vd.NewVolunteerTraining as 'NewVolTrain', "
                                 + "vd.NewVolunteerTrainingDate as 'NewVolTrainDate', "
                                 + "vd.VehichleInsurance as 'VehInsur', "
                                 + "vd.VehichleInsuranceCodes as 'VehInsurCodes', "
                                 + "vd.VehichleInsuranceDate as 'VehInsurDate', "
                                 + "vd.BackgroundCheck as 'BckgrndCk', "
                                 + "vd.NationalCheck as 'NatCk', "
                                 + "vd.NationalCheckCodes as 'NatCkCodes', "
                                 + "vd.NationalCheckDate as 'NatCkDate', "
                                 + "vd.DMVCheck as 'DMVCk', "
                                 + "vd.DMVCheckCodes as 'DMVCkCodes', "
                                 + "vd.DMVCheckDate as 'DMVCkDate', "
                                 + "vd.PACriminalCheck as 'PACrimCk', "
                                 + "vd.PACriminalCheckCodes as 'PACrimCkCodes', "
                                 + "vd.PACriminalCheckDate as 'PACrimCkDate', "
                                 + "vd.BackgroundCheckPAID as 'BckgrndCkPd', "
                                 + "vd.BackgroundCheckCodes as 'BckgrndCkCodes', "
                                 + "vd.BackgroundCheckPAIDDate as 'BckgrndCkPdDate', "
                                 + "vd.Comments, "
                                //+ "vd.SysCreate, "
                                //+ "vd.sysupdate, "
                                 + "vd.LastUpdatedBy, "
                                 + "vd.ID "
                                 + "from volunteerdetails vd ";
                }
                else if (ddlProgram.Text == "Volunteer Vehichle Insurance Details")
                {
                    sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', sizelimit as 'SizeLimit', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM OutreachBasketballProgramSections  order by classname";
                }

                con.Open();//Opens the db connection.

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();

                if (ddlProgram.Text == "Volunteer Profile Information")
                {
                    da.Fill(ds, "BasketballTEAMSProgramSections");
                }
                else if (ddlProgram.Text == "Volunteer Background Details")
                {
                    da.Fill(ds, "VolunteerDetails");
                }
                else if (ddlProgram.Text == "Volunteer Vehichle Insurance Details")
                {
                    da.Fill(ds, "OutreachBasketballProgramSections");
                }
                gvStudentList.DataSource = ds.Tables[0];
                gvStudentList.DataBind();
                gvStudentList.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {

            }
        }

        protected void CleanFields()
        {
            txbClassName.Text = txbClassName.Text.Replace("'", "");
            txbComments.Text = txbComments.Text.Replace("'", "");
            txbDay.Text = txbDay.Text.Replace("'", "");
            txbDevotionalLeader.Text = txbDevotionalLeader.Text.Replace("'", "");
            txbInstructor.Text = txbInstructor.Text.Replace("'", "");
            txbSizeLimit.Text = txbSizeLimit.Text.Replace("'", "");
            txbClassLocation.Text = txbClassLocation.Text.Replace("'", "");
        }

        protected void cmbAddRecord_Click(object sender, EventArgs e)
        {
            //Insert a new class record.
            string sqlInsertStatement = "";
            if (txbClassName.Text == "")
            {

            }
            else
            {
                txbClassName.Text = txbClassName.Text.Replace("'", "");
                txbComments.Text = txbComments.Text.Replace("'", "");
                txbDay.Text = txbDay.Text.Replace("'", "");
                txbDevotionalLeader.Text = txbDevotionalLeader.Text.Replace("'", "");
                txbInstructor.Text = txbInstructor.Text.Replace("'", "");
                txbSizeLimit.Text = txbSizeLimit.Text.Replace("'", "");
                txbClassLocation.Text = txbClassLocation.Text.Replace("'", "");
                
                try
                {

                    if (ddlProgram.Text == "Volunteer Profile Information")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.BasketballTEAMSProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'77', "
                            + System.Convert.ToInt32(txbSizeLimit.Text.Trim()) + ") ";

                    }
                    else if (ddlProgram.Text == "Volunteer Background Check Details")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.BaseballProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'77', "
                            + System.Convert.ToInt32(txbSizeLimit.Text.Trim()) + ") ";

                    }
                    else if (ddlProgram.Text == "Volunteer Vehichle Insurance Details")
                    {
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.OutreachBasketballProgramSections " +
                            "values ( "
                            + "'" + txbClassName.Text.Trim() + "',"
                            + "'" + ddlTime.Text.Trim() + "',"
                            + "'" + txbDay.Text.Trim() + "',"
                            + "'" + txbClassLocation.Text.Trim() + "', "
                            + "'" + txbComments.Text.Trim() + "', "
                            + "'" + txbInstructor.Text.Trim() + "', "
                            + "'" + txbDevotionalLeader.Text.Trim() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'77', "
                            + System.Convert.ToInt32(txbSizeLimit.Text.Trim()) + ") ";

                    }
                    
                    con.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                    if (sqlInsertCommand.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        lbAddNewEntry.Visible = true;
                        ResetNewPerson();
                        gvStudentList.DataBind();
                        DisplayTheGrid();
                    }
                    else
                    {
                        //display message that record was NOT updated
                        //	btnContinue.Visible = false;
                        //	lblAlert.Visible = true;
                        //	lblAlert.Text = "No update. Error has occurred.";
                    }
                }
                catch (Exception lkjlaa)
                {

                }
                finally
                {
                    //con.Close();
                }
            }
        }

        protected void DisplayTheGrid()
        {
            bind();
        }

        protected void ResetNewPerson()
        {
            txbComments.Visible = false;
            txbComments.Text = "";
            txbDay.Visible = false;
            txbDay.Text = "";
            txbClassName.Visible = false;
            txbClassName.Text = "";
            txbClassLocation.Visible = false;
            txbClassLocation.Text = "";
            txbDevotionalLeader.Visible = false;
            txbDevotionalLeader.Text = "";
            txbSizeLimit.Visible = false;
            txbSizeLimit.Text = "";
            txbInstructor.Visible = false;
            txbInstructor.Text = "";

            ddlTime.Visible = false;
            lblTime.Visible = false;
            cmbAddRecord.Visible = false;

            lblClassLocation.Visible = false;
            lblClassName.Visible = false;
            lblDay.Visible = false;
            lblComments.Visible = false;
            lblClassInstructor.Visible = false;
            lblDevotionalLeader.Visible = false;
            lblSizeLimit.Visible = false;
        }

        protected void lbAddNewEntry_Click(object sender, EventArgs e)
        {
            lbAddNewEntry.Visible = false;
            cmbAddRecord.Visible = true;
            ddlTime.Visible = true;
            txbClassLocation.Visible = true;
            txbClassName.Visible = true;
            txbComments.Visible = true;
            txbDevotionalLeader.Visible = true;
            txbInstructor.Visible = true;
            txbSizeLimit.Visible = true;
            txbDay.Visible = true;

            lblClassInstructor.Visible = true;
            lblClassLocation.Visible = true;
            lblClassName.Visible = true;
            lblComments.Visible = true;
            lblDay.Visible = true;
            lblDevotionalLeader.Visible = true;
            lblSizeLimit.Visible = true;
            lblTime.Visible = true;
        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("VolunteerDetails.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + "&VolunteerFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void calDate_SelectionChanged(object sender, EventArgs e)
        {
            txbBackgroundCheckDate.Text = calDate.SelectedDate.ToString("yyyy-MM-dd");
            calDate.Visible = false;
        }



        protected void PopupCalender1()
        {

        }

        protected void imgCalender_Click(object sender, ImageClickEventArgs e)
        {
            calDate.Visible = true;
            calDate.Enabled = true;
        }


        protected void ClearFields()
        {
            txbBackgroundCheckDate.Text = "";
            chbBackgroundCheck.Checked = false;
            chbSpiritualJourney.Checked = false;
            chbVehichleInsurance.Checked = false;
            chbGeneralInformation.Checked = false;
            chbDMVCheck.Checked = false;
            chbBackgroundCheckPAID.Checked = false;
            chbReleaseWaiver.Checked = false;
            chbNationalCheck.Checked = false;
            chbNewVolunteerTraining.Checked = false;
            chbPACriminalCheck.Checked = false;

            txbDMVCheckDate.Text = "Select a date.";
            txbNationalCheckDate.Text = "Select a date.";
            txbPACriminalCheckDate.Text = "Select a date.";
            txbVehichleInsurDate.Text = "Select a date.";
            txbBackgroundCheckPAIDDate.Text = "Select a date.";
            txbNewVolunteerTrainingDate.Text = "Select a date.";
            
            txbComments2.Text = "";
            imgImage.ImageUrl = "";
        }

        protected void ddlNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearFields();
            RetreiveInformation(ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(",")), ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
            DisplayHeaderFields();
        }

        protected void cmbUpdateInformation_Click(object sender, EventArgs e)
        {
            if (((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")) || ((Request.QueryString["LastName"] == "Gilmore") && (Request.QueryString["FirstName"] == "Anna")) || ((Request.QueryString["LastName"] == "Brazil") && (Request.QueryString["FirstName"] == "Nehemiah")) || ((Request.QueryString["LastName"] == "Braunersrither") && (Request.QueryString["FirstName"] == "Chad")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Nate")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Hannah")))
            {

            }
            else
            {
                UpdateVolunteerDetails(ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(",")), ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
            }
        }

        protected void UpdateVolunteerDetails(string LastName, string FirstName)
        {
            //lblInformation.Enabled = false;
            //lblInformation.Text = "";
            try
            {
                string sqlUpdateStatement = "";
                try
                {
                    sqlUpdateStatement = " UPDATE VolunteerDetails "
                    + "SET "
                    + "NewVolunteerTrainingDate = '" + txbNewVolunteerTrainingDate.Text.Trim() + "', "
                    + "VehichleInsuranceCodes = '" + ddlVehichleInsuranceCodes.Text.Trim() + "', "
                    + "NationalCheckCodes = '" + ddlNationalCheckCodes.Text.Trim() + "', "
                    //---------------------------------------------------------------------------------------------
                    //+ "VehichleInsuranceDate = '" + txbVehichleInsurDate.Text.Trim() + "', "
                    + "VehichleInsuranceDate = '" + ddlVehichleMonth.Text + "/" + ddlVehichleDay.Text + "/" + ddlVehichleYear.Text + "', "
                    //---------------------------------------------------------------------------------------------
                    //+ "PACriminalCheckDate = '" + txbPACriminalCheckDate.Text.Trim() + "', "
                    + "PACriminalCheckDate = '" + ddlPACriminalMonth.Text + "/" + ddlPACriminalDay.Text + "/" + ddlPACriminalYear.Text + "', "
                    //-----------------------------------------------------------------------------
                    //+ "NationalCheckDate = '" + txbNationalCheckDate.Text.Trim() + "', "
                    + "NationalCheckDate = '" + ddlNationalMonth.Text + "/" + ddlNationalDay.Text + "/" + ddlNationalYear.Text + "', "
                    //-----------------------------------------------------------------------------
                    //+ "DMVCheckDate = '" + txbDMVCheckDate.Text + "', "
                    + "DMVCheckDate = '" + ddlDMVMonth.Text + "/" + ddlDMVDay.Text + "/" + ddlDMVYear.Text + "', "
                    //---------------------------------------------------------------------------------------------------
                    + "NationalCheck = " + Convert.ToInt32(chbNationalCheck.Checked) + ", "
                    + "DMVCheckCodes = '" + ddlDMVCheckCodes.Text.Trim() + "', "
                    + "PACriminalCheckCodes = '" + ddlCriminalCheckCodes.Text.Trim() + "', "
                    //----------------------------------------------------------------------------
                    //------------------------------------------------------------------------------
                    + "BackgroundCheckCodes = '" + ddlBackgroundCheckPaidCode.Text.Trim() + "', "
                    + "BackgroundCheckPAID = " + Convert.ToInt32(chbBackgroundCheckPAID.Checked) + ", "
                    //+ "BackgroundCheckPAIDDate = '" + txbBackgroundCheckPAIDDate.Text.Trim() + "', "
                    + "comments = '" + txbComments2.Text.Trim() + "', "
                    + "lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                    + "backgroundcheck = " + Convert.ToInt32(chbBackgroundCheck.Checked) + ", "
                    + "spiritualjourney = " + Convert.ToInt32(chbSpiritualJourney.Checked) + ", "
                    + "vehichleinsurance = " + Convert.ToInt32(chbVehichleInsurance.Checked) + ", "
                    + "releasewaiver = " + Convert.ToInt32(chbReleaseWaiver.Checked) + ", "
                    + "generalinformation = " + Convert.ToInt32(chbGeneralInformation.Checked) + ", "
                    + "newvolunteertraining = " + Convert.ToInt32(chbNewVolunteerTraining.Checked) + ", "
                    + "dmvcheck = " + Convert.ToInt32(chbDMVCheck.Checked) + ", "
                    + "pacriminalcheck = " + Convert.ToInt32(chbPACriminalCheck.Checked) + ", "
                    + "sysupdate = '" + System.DateTime.Now.ToString() + "' "
                    + "  "
                    + " WHERE lastname = '" + LastName + "' "
                    + " AND firstname = '" + FirstName + "' ";

                    con.Open();
                    //create a SQL command to update record
                    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                    {
                    }
                    else
                    {
                        //Didn't find a record to update..RCM..
                    }
                }
                catch (Exception lkjaaa)
                {
                    //lblInformation.Enabled = true;
                    //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                //add code to send error to admin via email
                Session["Exception"] = ex.Message.ToString();
                Response.Redirect("Error.aspx");
            }
            finally
            {
                //Close connection
                con.Close();
                con.Dispose();
            }
            //return true;
        }

        protected void calNationalCheckDate_SelectionChanged(object sender, EventArgs e)
        {
            txbNationalCheckDate.Text = calNationalCheckDate.SelectedDate.ToString("yyyy-MM-dd");
            calNationalCheckDate.Visible = false;
        }

        protected void imbVehichleInsurDate_Click(object sender, ImageClickEventArgs e)
        {
            calVehichleInsuranceDate.Visible = true;
            calVehichleInsuranceDate.Enabled = true;
        }

        protected void calVehichleInsuranceDate_SelectionChanged(object sender, EventArgs e)
        {
            txbVehichleInsurDate.Text = calVehichleInsuranceDate.SelectedDate.ToString("yyyy-MM-dd");
            calVehichleInsuranceDate.Visible = false;
        }

        protected void imbNationalCheckDate_Click(object sender, ImageClickEventArgs e)
        {
            calNationalCheckDate.Visible = true;
            calNationalCheckDate.Enabled = true;
        }

        protected void imbDMVCheckDate_Click(object sender, ImageClickEventArgs e)
        {
            calDMVCheckDate.Visible = true;
            calDMVCheckDate.Enabled = true;
        }

        protected void imbPACriminalCheckDate_Click(object sender, ImageClickEventArgs e)
        {
            calPACriminalDate.Visible = true;
            calPACriminalDate.Enabled = true;
        }

        protected void calDMVCheckDate_SelectionChanged(object sender, EventArgs e)
        {
            txbDMVCheckDate.Text = calDMVCheckDate.SelectedDate.ToString("yyyy-MM-dd");
            calDMVCheckDate.Visible = false;
        }

        protected void calPACriminalDate_SelectionChanged(object sender, EventArgs e)
        {
            txbPACriminalCheckDate.Text = calPACriminalDate.SelectedDate.ToString("yyyy-MM-dd");
            calPACriminalDate.Visible = false;
        }

        protected void imbNewVolunteerTrainingDate_Click(object sender, ImageClickEventArgs e)
        {
            calNewVolunteerTrainingDate.Visible = true;
            calNewVolunteerTrainingDate.Enabled = true;
        }

        protected void calNewVolunteerTrainingDate_SelectionChanged(object sender, EventArgs e)
        {
            txbNewVolunteerTrainingDate.Text = calNewVolunteerTrainingDate.SelectedDate.ToString("yyyy-MM-dd");
            calNewVolunteerTrainingDate.Visible = false;
        }

        protected void imbBackgroundCheckPAIDDate_Click(object sender, ImageClickEventArgs e)
        {
            calBackgroundCheckPAIDDate.Visible = true;
            calBackgroundCheckPAIDDate.Enabled = true;
        }

        protected void calBackgroundCheckPAIDDate_SelectionChanged(object sender, EventArgs e)
        {
            txbBackgroundCheckPAIDDate.Text = calBackgroundCheckPAIDDate.SelectedDate.ToString("yyyy-MM-dd");
            calBackgroundCheckPAIDDate.Visible = false;
        }

        protected void lbVolunteerProfilePage_Click(object sender, EventArgs e)
        {
            if (ddlNames.Text != "Please select a volunteer")
            {
                if (((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Seth")) || ((Request.QueryString["LastName"] == "Gilmore") && (Request.QueryString["FirstName"] == "Anna")) || ((Request.QueryString["LastName"] == "Brazil") && (Request.QueryString["FirstName"] == "Nehemiah")) || ((Request.QueryString["LastName"] == "Braunersrither") && (Request.QueryString["FirstName"] == "Chad")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Nate")) || ((Request.QueryString["LastName"] == "Reichart") && (Request.QueryString["FirstName"] == "Hannah")))
                {

                }
                else
                {
                    UpdateVolunteerDetails(ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(",")), ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
                }

                Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(","))
                                + "&VolunteerFirstName=" + ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);
            }
        }

        protected void cmbNationalCheckReport_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPage();
                con.Open();

                string sql = "";
                sql = "Select vi.LastName, vi.FirstName, vi.Comments, vi.NationalCheckCodes, vi.NationalCheckDate as 'Last NationalCheckDate' "
                           + "from VolunteerDetails vi "
                           + "LEFT OUTER JOIN volunteerprogramattendance vpa "
                           + "ON (vi.lastname = vpa.lastname AND vi.firstname = vpa.firstname) "
                           + "WHERE vi.nationalcheckdate < '" + System.DateTime.Now.AddMonths(-24).ToString() + "' "
                           + "AND vpa.lastname is not null "
                           //+ "OR (vi.nationcheckcodes = 'Suspended' vi.nationalcheckcodes = 'Pending Submitted Results') "
                           + "GROUP BY vi.LastName, vi.FirstName, vi.Comments, vi.Nationalcheckdate, vi.Nationalcheckcodes "
                           + "ORDER BY vi.LastName, vi.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerDetails");
                gvReports.DataSource = ds.Tables[0];
                gvReports.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
                gvReports.Visible = true;
                ddlNames.Visible = false;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void cmbDMVCheckReport_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPage();
                con.Open();

                string sql = "";
                sql = "Select vi.LastName, vi.FirstName, vi.Comments, vi.dmvCheckCodes, vi.dmvCheckDate as 'Last DMVCheckDate' "
                           + "from VolunteerDetails vi "
                           + "LEFT OUTER JOIN volunteerprogramattendance vpa "
                           + "ON (vi.lastname = vpa.lastname AND vi.firstname = vpa.firstname) "
                           + "WHERE vi.dmvcheckdate < '" + System.DateTime.Now.AddMonths(-24).ToString() + "' "
                           + "AND vpa.lastname is not null "
                            //+ "OR (vi.nationcheckcodes = 'Suspended' vi.nationalcheckcodes = 'Pending Submitted Results') "
                           + "GROUP BY vi.LastName, vi.FirstName, vi.Comments, vi.dmvcheckdate, vi.DMVcheckcodes "
                           + "ORDER BY vi.LastName, vi.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerDetails");
                gvReports.DataSource = ds.Tables[0];
                gvReports.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
                gvReports.Visible = true;
                ddlNames.Visible = false;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void imbAdvancedSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (lblReporting.Visible)
            {
                Response.Redirect("VolunteerDetails.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + "&VolunteerFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
            }
            else
            {
                ClearPage();
                ddlNames.Visible = false;
                ShowReporting();
                
                imbAdvancedSearch.ToolTip = "Go back to select an individual volunteer.";
            }
        }

        protected void ShowReporting()
        {
            cmbCreateCustomView.Visible = true;
            cmbRetrieveResults.Visible = true;

            //lblTo.Visible = true;
            //lblOptionalAnother.Visible = true;

            ddlAdvancedSearchView.Visible = true;
            lblViewChoices.Visible = true;
            lblFindRecords.Visible = true;
            ddlAdvancedSearchView.Items.Add("Select a View (Optional)");
            ddlAdvancedSearchView.Items.Add("Background Check Info");
            ddlAdvancedSearchView.Items.Add("Application Info");
            //ddlAdvancedSearchView.Items.Add("Program Info");
            //ddlAdvancedSearchView.Items.Add("DiscipleshipMentor Info");
            //ddlAdvancedSearchView.Items.Add("All Available Info");
            ddlAdvancedSearchView.Text = "Select a View (Optional)";
            ddlAdvancedSearchView.Enabled = true;
            ddlAdvancedSearchView.Visible = true;

            //Clear the gridview..RCM.
            gvAddressView.DataSource = null;
            gvAddressView.DataBind();

            lblReporting.Visible = true;
            //ddlPACriminalCodes.Visible = true;
            //ddlNationalCheckReport.Visible = true;
            //ddlDMVCheckReport.Visible = true;

            //lblPACriminalCheckReport.Visible = true;
            //lblDMVCheckReport.Visible = true;
            //lblNationalCheckReport.Visible = true;

            //ddlPickDateRangeDay1.Visible = true;
            //ddlPickDateRangeDay2.Visible = true;
            //ddlPickDateRangeMonth1.Visible = true;
            //ddlPickDateRangeMonth2.Visible = true;
            //ddlPickDateRangeYear1.Visible = true;
            //ddlPickDateRangeYear2.Visible = true;
            ////////lblYear.Visible = true;


            Populate_DayList();
            Populate_MonthList();
            Populate_YearList();

            PopulateGradesWithCodes();
            PopulateSearchValueBoolean();

            //PopulateDMVCheckCodesList();
            //PopulateNationalCheckCodesList();
            //PopulatePACriminalCheckCodesList();
            
            cmbExcelExport.Visible = true;

            chbActiveNonActive.Visible = true;

            //Populating the operator to be used with dates..RCM..8/28/13.
            ddlChooseDate.Items.Add("Choose Operator");
            ddlChooseDate.Items.Add("equals");
            ddlChooseDate.Items.Add("AFTER to present");
            ddlChooseDate.Items.Add("On or AFTER to present");
            ddlChooseDate.Items.Add("BEFORE");
            ddlChooseDate.Items.Add("On or BEFORE");
            ddlChooseDate.Text = "Choose Operator";

            ddlChooseDate2.Items.Add("Choose Operator");
            ddlChooseDate2.Items.Add("equals");
            ddlChooseDate2.Items.Add("AFTER to present");
            ddlChooseDate2.Items.Add("On or AFTER to present");
            ddlChooseDate2.Items.Add("BEFORE");
            ddlChooseDate2.Items.Add("On or BEFORE");
            ddlChooseDate2.Text = "Choose Operator";

            ddlChooseDate3.Items.Add("Choose Operator");
            ddlChooseDate3.Items.Add("equals");
            ddlChooseDate3.Items.Add("AFTER to present");
            ddlChooseDate3.Items.Add("On or AFTER to present");
            ddlChooseDate3.Items.Add("BEFORE");
            ddlChooseDate3.Items.Add("On or BEFORE");
            ddlChooseDate3.Text = "Choose Operator";

            ddlChooseDate4.Items.Add("Choose Operator");
            ddlChooseDate4.Items.Add("equals");
            ddlChooseDate4.Items.Add("AFTER to present");
            ddlChooseDate4.Items.Add("On or AFTER to present");
            ddlChooseDate4.Items.Add("BEFORE");
            ddlChooseDate4.Items.Add("On or BEFORE");
            ddlChooseDate4.Text = "Choose Operator";

            ddlChooseDate5.Items.Add("Choose Operator");
            ddlChooseDate5.Items.Add("equals");
            ddlChooseDate5.Items.Add("AFTER to present");
            ddlChooseDate5.Items.Add("On or AFTER to present");
            ddlChooseDate5.Items.Add("BEFORE");
            ddlChooseDate5.Items.Add("On or BEFORE");
            ddlChooseDate5.Text = "Choose Operator";

            //To be used for Grade, Age, and all integer fields..RCM..
            ddlChooseOperator.Items.Add("Choose Operator");
            //ddlChooseOperator.Items.Add("contains");
            //ddlChooseOperator.Items.Add("NOT contains");
            ddlChooseOperator.Items.Add("equals");
            //ddlChooseOperator.Items.Add("NOT equals");
            //ddlChooseOperator.Items.Add(">");
            //ddlChooseOperator.Items.Add("<");
            //ddlChooseOperator.Items.Add(">=");
            //ddlChooseOperator.Items.Add("<=");
            ddlChooseOperator.Text = "Choose Operator";
            ddlChooseOperator2.Items.Add("Choose Operator");
            //ddlChooseOperator2.Items.Add("contains");
            //ddlChooseOperator2.Items.Add("NOT contains");
            ddlChooseOperator2.Items.Add("equals");
            //ddlChooseOperator2.Items.Add("NOT equals");
            //ddlChooseOperator2.Items.Add(">");
            //ddlChooseOperator2.Items.Add("<");
            //ddlChooseOperator2.Items.Add(">=");
            //ddlChooseOperator2.Items.Add("<=");
            ddlChooseOperator2.Text = "Choose Operator";
            ddlChooseOperator3.Items.Add("Choose Operator");
            //ddlChooseOperator3.Items.Add("contains");
            //ddlChooseOperator3.Items.Add("NOT contains");
            ddlChooseOperator3.Items.Add("equals");
            //ddlChooseOperator3.Items.Add("NOT equals");
            //ddlChooseOperator3.Items.Add(">");
            //ddlChooseOperator3.Items.Add("<");
            //ddlChooseOperator3.Items.Add(">=");
            //ddlChooseOperator3.Items.Add("<=");
            ddlChooseOperator3.Text = "Choose Operator";
            ddlChooseOperator4.Items.Add("Choose Operator");
            //ddlChooseOperator4.Items.Add("contains");
            //ddlChooseOperator4.Items.Add("NOT contains");
            ddlChooseOperator4.Items.Add("equals");
            //ddlChooseOperator4.Items.Add("NOT equals");
            //ddlChooseOperator4.Items.Add(">");
            //ddlChooseOperator4.Items.Add("<");
            //ddlChooseOperator4.Items.Add(">=");
            //ddlChooseOperator4.Items.Add("<=");
            ddlChooseOperator4.Text = "Choose Operator";
            ddlChooseOperator5.Items.Add("Choose Operator");
            //ddlChooseOperator5.Items.Add("contains");
            //ddlChooseOperator5.Items.Add("NOT contains");
            ddlChooseOperator5.Items.Add("equals");
            //ddlChooseOperator5.Items.Add("NOT equals");
            //ddlChooseOperator5.Items.Add(">");
            //ddlChooseOperator5.Items.Add("<");
            //ddlChooseOperator5.Items.Add(">=");
            //ddlChooseOperator5.Items.Add("<=");
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


            //Make the advanced search appear..RCM.
            //lblFindRecords.Visible = true;
            //cmdStudent.Visible = true;
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


            try
            {
                con.Open();

                string selectcolumnnames = "select name "
                    + "from sys.columns "
                    + "where object_id = (SELECT OBJECT_ID('UIF_PerformingArts.dbo.VolunteerDetails')) "
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

                    ddlChooseField.Items.Add("Address");
                    ddlChooseField.Items.Add("City");
                    ddlChooseField.Items.Add("State");
                    ddlChooseField.Items.Add("Zip");
                    ddlChooseField.Items.Add("HomePhone");
                    ddlChooseField.Items.Add("CellPhone");
                    ddlChooseField.Items.Add("Church");
                    ddlChooseField.Items.Add("DOB");

                    ddlChooseField2.Items.Add("Address");
                    ddlChooseField2.Items.Add("City");
                    ddlChooseField2.Items.Add("State");
                    ddlChooseField2.Items.Add("Zip");
                    ddlChooseField2.Items.Add("HomePhone");
                    ddlChooseField2.Items.Add("CellPhone");
                    ddlChooseField2.Items.Add("Church");
                    ddlChooseField2.Items.Add("DOB");

                    ddlChooseField3.Items.Add("Address");
                    ddlChooseField3.Items.Add("City");
                    ddlChooseField3.Items.Add("State");
                    ddlChooseField3.Items.Add("Zip");
                    ddlChooseField3.Items.Add("HomePhone");
                    ddlChooseField3.Items.Add("CellPhone");
                    ddlChooseField3.Items.Add("Church");
                    ddlChooseField3.Items.Add("DOB");

                    ddlChooseField4.Items.Add("Address");
                    ddlChooseField4.Items.Add("City");
                    ddlChooseField4.Items.Add("State");
                    ddlChooseField4.Items.Add("Zip");
                    ddlChooseField4.Items.Add("HomePhone");
                    ddlChooseField4.Items.Add("CellPhone");
                    ddlChooseField4.Items.Add("Church");
                    ddlChooseField4.Items.Add("DOB");

                    ddlChooseField5.Items.Add("Address");
                    ddlChooseField5.Items.Add("City");
                    ddlChooseField5.Items.Add("State");
                    ddlChooseField5.Items.Add("Zip");
                    ddlChooseField5.Items.Add("HomePhone");
                    ddlChooseField5.Items.Add("CellPhone");
                    ddlChooseField5.Items.Add("Church");
                    ddlChooseField5.Items.Add("DOB");

                    ddlChooseField.Items.Remove("Comments");
                    ddlChooseField.Items.Remove("SysCreate");
                    ddlChooseField.Items.Remove("sysupdate");
                    ddlChooseField.Items.Remove("LastUpdatedBy");
                    ddlChooseField.Items.Remove("ID");
                    ddlChooseField.Items.Remove("Student");
                    ddlChooseField.Items.Remove("StaffVolunteer");
                    ddlChooseField.Items.Remove("MiddleName");

                    ddlChooseField2.Items.Remove("Comments");
                    ddlChooseField2.Items.Remove("SysCreate");
                    ddlChooseField2.Items.Remove("sysupdate");
                    ddlChooseField2.Items.Remove("LastUpdatedBy");
                    ddlChooseField2.Items.Remove("ID");
                    ddlChooseField2.Items.Remove("Student");
                    ddlChooseField2.Items.Remove("StaffVolunteer");
                    ddlChooseField2.Items.Remove("MiddleName");

                    ddlChooseField3.Items.Remove("Comments");
                    ddlChooseField3.Items.Remove("SysCreate");
                    ddlChooseField3.Items.Remove("sysupdate");
                    ddlChooseField3.Items.Remove("LastUpdatedBy");
                    ddlChooseField3.Items.Remove("ID");
                    ddlChooseField3.Items.Remove("Student");
                    ddlChooseField3.Items.Remove("StaffVolunteer");
                    ddlChooseField3.Items.Remove("MiddleName");

                    ddlChooseField4.Items.Remove("Comments");
                    ddlChooseField4.Items.Remove("SysCreate");
                    ddlChooseField4.Items.Remove("sysupdate");
                    ddlChooseField4.Items.Remove("LastUpdatedBy");
                    ddlChooseField4.Items.Remove("ID");
                    ddlChooseField4.Items.Remove("Student");
                    ddlChooseField4.Items.Remove("StaffVolunteer");
                    ddlChooseField4.Items.Remove("MiddleName");

                    ddlChooseField5.Items.Remove("Comments");
                    ddlChooseField5.Items.Remove("SysCreate");
                    ddlChooseField5.Items.Remove("sysupdate");
                    ddlChooseField5.Items.Remove("LastUpdatedBy");
                    ddlChooseField5.Items.Remove("ID");
                    ddlChooseField5.Items.Remove("Student");
                    ddlChooseField5.Items.Remove("StaffVolunteer");
                    ddlChooseField5.Items.Remove("MiddleName");

                }
            }
            catch (Exception llllaabb)
            {

            }

        }

        protected void ddlNationalCheckCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNationalCheckCodes.Text != "Check Not Run")
            {
                if (ddlNationalCheckCodes.Text == "No Record")
                {
                    chbNationalCheck.Checked = true;
                }
                else if (ddlNationalCheckCodes.Text == "Cleared Without Restrictions")
                {
                    chbNationalCheck.Checked = true;
                }
                else if (ddlNationalCheckCodes.Text == "STAFF")
                {
                    chbNationalCheck.Checked = true;
                }
                else
                {
                    chbNationalCheck.Checked = false;
                }
            }
            UpdateVolunteerDetails(ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(",")), ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
        }

        protected void ddlDMVCheckCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDMVCheckCodes.Text != "Check Not Run")
            {
                if (ddlDMVCheckCodes.Text == "No Record")
                {
                    chbDMVCheck.Checked = true;
                }
                else if (ddlDMVCheckCodes.Text == "Cleared Without Restrictions")
                {
                    chbDMVCheck.Checked = true;
                }
                else if (ddlDMVCheckCodes.Text == "STAFF")
                {
                    chbDMVCheck.Checked = true;
                }
                else
                {
                    chbDMVCheck.Checked = false;
                }
            }
            UpdateVolunteerDetails(ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(",")), ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
        }

        protected void ddlCriminalCheckCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCriminalCheckCodes.Text != "Check Not Run")
            {
                if (ddlCriminalCheckCodes.Text == "No Record")
                {
                    chbPACriminalCheck.Checked = true;
                }
                else if (ddlCriminalCheckCodes.Text == "Cleared Without Restrictions")
                {
                    chbPACriminalCheck.Checked = true;
                }
                else if (ddlCriminalCheckCodes.Text == "STAFF")
                {
                    chbPACriminalCheck.Checked = true;
                }
                else
                {
                    chbPACriminalCheck.Checked = false;
                }
            }
            UpdateVolunteerDetails(ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(",")), ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)));
        }

        protected void ddlVehichleInsuranceCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ddlVehichleInsuranceCodes.Text != "Check Not Run") && (ddlVehichleInsuranceCodes.Text != "N/A"))
            {
                if (ddlVehichleInsuranceCodes.Text == "Approved")
                {
                    chbVehichleInsurance.Checked = true;
                }
                else
                {
                    chbVehichleInsurance.Checked = false;
                }
            }
        }

        protected void ddlPACriminalCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlNationalCheckReport.Text = "Select a Code?";
            ddlDMVCheckReport.Text = "Select a Code?";
            try
            {
                con.Open();

                string sql = "";

                if (chbActiveNonActive.Checked)
                {
                    //Active in at least 1 program..
                    sql = "Select vi.LastName, vi.FirstName, vi.Pacriminalcheckcodes, vi.Pacriminalcheckdate "
                               + "from VolunteerDetails vi "
                               + "LEFT OUTER JOIN volunteerprogramattendance vpa "
                               + "ON (vi.lastname = vpa.lastname AND vi.firstname = vpa.firstname) "
                               + "LEFT OUTER JOIN programslist pl "
                               + "ON (vi.lastname = pl.lastname AND vi.firstname = pl.firstname AND pl.staffvolunteer = 1) "
                               + "WHERE vi.pacriminalcheckcodes = '" + ddlPACriminalCodes.Text + "' "
                               + "AND (pl.MSHSChoir = 1 or pl.ChildrensChoir = 1 or pl.PerformingArts = 1 or pl.Shakes = 1 or pl.Singers = 1 or pl.ImpactUrbanSchools = 1 or pl.OutreachBasketball = 1 or pl.BasketballTEAMS = 1 or pl.MSBasketballLg = 1 or pl.HSBasketballLg = 1 or pl.Baseball = 1 or pl.SoccerIntraMurals = 1 or pl.SoccerTEAMS = 1 or pl.[3on3Basketball] = 1 or pl.BibleStudy = 1 or pl.MondayNights = 1 or pl.SpecialEvents = 1 or pl.SummerDayCamp = 1 or pl.AcademicReadingSupport = 1) "
                               //+ "WHERE vi.pacriminalcheckdate < '" + System.DateTime.Now.AddMonths(-24).ToString() + "' "
                               + "GROUP BY vi.LastName, vi.FirstName, vi.Pacriminalcheckcodes, vi.Pacriminalcheckdate "
                               + "ORDER BY vi.LastName, vi.FirstName ";
                }
                else
                {
                    //Non-Active in any programs...
                    sql = "Select vi.LastName, vi.FirstName, vi.Pacriminalcheckcodes, vi.Pacriminalcheckdate "
                               + "from VolunteerDetails vi "
                               + "LEFT OUTER JOIN volunteerprogramattendance vpa "
                               + "ON (vi.lastname = vpa.lastname AND vi.firstname = vpa.firstname) "
                               + "LEFT OUTER JOIN programslist pl "
                               + "ON (vi.lastname = pl.lastname AND vi.firstname = pl.firstname AND pl.staffvolunteer = 1) "
                               + "WHERE vi.pacriminalcheckcodes = '" + ddlPACriminalCodes.Text + "' "
                               + "AND (pl.MSHSChoir = 0 AND pl.ChildrensChoir = 0 AND pl.PerfANDmingArts = 0 AND pl.Shakes = 0 AND pl.Singers = 0 AND pl.ImpactUrbanSchools = 0 AND pl.OutreachBasketball = 0 AND pl.BasketballTEAMS = 0 AND pl.MSBasketballLg = 0 AND pl.HSBasketballLg = 0 AND pl.Baseball = 0 AND pl.SoccerIntraMurals = 0 AND pl.SoccerTEAMS = 0 AND pl.[3on3Basketball] = 0 AND pl.BibleStudy = 0 AND pl.MondayNights = 0 AND pl.SpecialEvents = 0 AND pl.SummerDayCamp = 0 AND pl.AcademicReadingSuppANDt = 0) "                                //+ "WHERE vi.pacriminalcheckdate < '" + System.DateTime.Now.AddMonths(-24).ToString() + "' "
                               + "GROUP BY vi.LastName, vi.FirstName, vi.Pacriminalcheckcodes, vi.Pacriminalcheckdate "
                               + "ORDER BY vi.LastName, vi.FirstName ";
                }
                
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerDetails");
                gvReports.DataSource = ds.Tables[0];
                gvReports.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
                gvReports.Visible = true;
                ddlNames.Visible = false;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void ddlDMVCheckReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlNationalCheckReport.Text = "Select a Code?";
            ddlPACriminalCodes.Text = "Select a Code?";
            try
            {
                con.Open();

                string sql = "";
                sql = "Select vi.LastName, vi.FirstName, vi.dmvcheckcodes, vi.dmvcheckdate "
                           + "from VolunteerDetails vi "
                           + "LEFT OUTER JOIN volunteerprogramattendance vpa "
                           + "ON (vi.lastname = vpa.lastname AND vi.firstname = vpa.firstname) "
                           + "WHERE vi.dmvcheckcodes = '" + ddlDMVCheckReport.Text + "' "
                           //+ "AND vi.dmvcheckdate < '" + System.DateTime.Now.AddMonths(-24).ToString() + "' "
                           + "GROUP BY vi.LastName, vi.FirstName, vi.dmvcheckcodes, vi.dmvcheckdate "
                           + "ORDER BY vi.LastName, vi.FirstName ";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "VolunteerDetails");
                gvReports.DataSource = ds.Tables[0];
                gvReports.DataBind();
                con.Close();
                cmbExcelExport.Enabled = true;
                gvReports.Visible = true;
                ddlNames.Visible = false;
            }
            catch (Exception lkjl)
            {
            }
        }

        protected void ddlNationalCheckReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDMVCheckReport.Text = "Select a Code?";
            ddlPACriminalCodes.Text = "Select a Code?";
            try
            {
                //con.Open();

                //string sql = "";
                //sql = "Select vi.LastName, vi.FirstName, vi.Nationalcheckcodes, vi.Nationalcheckdate "
                //           + "from VolunteerDetails vi "
                //           + "LEFT OUTER JOIN volunteerprogramattendance vpa "
                //           + "ON (vi.lastname = vpa.lastname AND vi.firstname = vpa.firstname) "
                //           + "WHERE vi.nationalcheckcodes = '" + ddlNationalCheckReport.Text + "' "
                //           //+ "AND vi.nationalcheckdate < '" + System.DateTime.Now.AddMonths(-24).ToString() + "' "
                //           + "GROUP BY vi.LastName, vi.FirstName, vi.Nationalcheckcodes, vi.Nationalcheckdate "
                //           + "ORDER BY vi.LastName, vi.FirstName ";

                //SqlDataAdapter da = new SqlDataAdapter(sql, con);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "VolunteerDetails");
                //gvReports.DataSource = ds.Tables[0];
                //gvReports.DataBind();
                //con.Close();
                cmbExcelExport.Enabled = true;
                gvReports.Visible = true;
                ddlNames.Visible = false;
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void ddlBackgroundCheckPaidCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBackgroundCheckPaidCode.Text != "Checks Not Run")
            {
                if ((ddlBackgroundCheckPaidCode.Text == "Paid In Full") || (ddlBackgroundCheckPaidCode.Text == "Grandfathered In"))
                {
                    chbBackgroundCheckPAID.Checked = true;
                }
                else
                {
                    chbBackgroundCheckPAID.Checked = false;
                }
            }
        }

        protected void txbVehichleInsurDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmbCreateCustomView_Click(object sender, EventArgs e)
        {
            chbCustomList.Style.Add("z-index", "999999");
            chbCustomList.Visible = true;

            //lblCustomView.Style.Add("z-index", "999999");
            //lblCustomView.Visible = true;

            cmbConfirmCustomViewFields.Style.Add("z-index", "999999");
            cmbConfirmCustomViewFields.Visible = true;

            cmbClearViewFields.Style.Add("z-index", "999999");
            cmbClearViewFields.Visible = true;

            cmbCancelCustomViewFields.Style.Add("z-index", "999999");
            cmbCancelCustomViewFields.Visible = true;

            //rblProgramManagement.Style.Add("z-index", "99999");
            //rblProgramManagement.Visible = true;

            pnlCustomList.Style.Add("z-index", "99999");
            pnlCustomList.Visible = true;

            cmbConfirmCustomViewFields.TabIndex = 1;
        }

        protected void cblCreateCustomView_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void cmbCancelCustomViewFields_Click1(object sender, EventArgs e)
        {
            chbCustomList.ClearSelection();
            chbCustomList.Visible = false;
            chbCustomList.Visible = false;
            cmbCancelCustomViewFields.Visible = false;
            cmbConfirmCustomViewFields.Visible = false;
            cmbClearViewFields.Visible = false;
            //lblCustomView.Visible = false;
        }

        protected void cmbConfirmCustomViewFields_Click1(object sender, EventArgs e)
        {
            //ChooseCustomView = true;
            pnlCustomList.Visible = false;
            chbCustomList.Visible = false;
            cmbConfirmCustomViewFields.Visible = false;
            //lblCustomView.Visible = false;
        }

        protected void cmbClearViewFields_Click1(object sender, EventArgs e)
        {
            chbCustomList.ClearSelection();
        }

        protected void gvPersonalView_RowCommand(object sender, EventArgs e)
        {

        }

        protected void ddlPACriminalYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOperatorCharacter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue2Bool.Enabled = false;
            ddlSearchValue2Bool.Visible = false;
            txbSearchValue2.Visible = true;

            rblNumber2.Enabled = true;
        }

        protected void ddlSearchValueBool_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber1.Enabled = true;
        }

        protected void ddlSearchValue3Bool_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber3.Enabled = true;
        }

        protected void ddlSearchValue4Bool_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber4.Enabled = true;
        }

        protected void ddlSearchValue5Bool_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOperatorCharacter3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue3Bool.Enabled = false;
            ddlSearchValue3Bool.Visible = false;
            txbSearchValue3.Visible = true;

            rblNumber3.Enabled = true;
        }

        protected void cmbCreateCustomView_Click1(object sender, EventArgs e)
        {
            chbCustomList.Style.Add("z-index", "99999");
            chbCustomList.Visible = true;

            lblCustomView.Style.Add("z-index", "99999");
            lblCustomView.Visible = true;

            cmbConfirmCustomViewFields.Style.Add("z-index", "99999");
            cmbConfirmCustomViewFields.Visible = true;

            cmbClearViewFields.Style.Add("z-index", "99999");
            cmbClearViewFields.Visible = true;

            cmbCancelCustomViewFields.Style.Add("z-index", "99999");
            cmbCancelCustomViewFields.Visible = true;

            pnlCustomList.Style.Add("z-index", "9999");
            pnlCustomList.Visible = true;

            cmbConfirmCustomViewFields.TabIndex = 1;
        }

        protected void cmbCancelCustomViewFields_Click(object sender, EventArgs e)
        {
            chbCustomList.ClearSelection();
            pnlCustomList.Visible = false;
            chbCustomList.Visible = false;
            cmbCancelCustomViewFields.Visible = false;
            cmbConfirmCustomViewFields.Visible = false;
            cmbClearViewFields.Visible = false;
            lblCustomView.Visible = false;
        }

        protected void cmbConfirmCustomViewFields_Click(object sender, EventArgs e)
        {
            //ChooseCustomView = true;
            pnlCustomList.Visible = false;
            chbCustomList.Visible = false;
            cmbConfirmCustomViewFields.Visible = false;
            lblCustomView.Visible = false;
        }

        protected void cmbClearViewFields_Click(object sender, EventArgs e)
        {
            chbCustomList.ClearSelection();
        }

        protected string DetermineCustomViewQuery()
        {
            //Ryan C Manners...10/26/11.
            //Builds the sql query dynamically..
            string sql = "";
            try
            {
                sql = "select ";
                CheckBoxList chkbx = (CheckBoxList)Form.FindControl("chbCustomList");
                for (int i = 0; i < chkbx.Items.Count; i++)
                {
                    if (chkbx.Items[i].Selected)
                    {
                        if (i > 0)
                        {
                            if ((chkbx.Items[i].Value.StartsWith("MSHSChoir")) || (chkbx.Items[i].Value.StartsWith("ChildrensChoir")) || (chkbx.Items[i].Value.StartsWith("PerformingArts")) || (chkbx.Items[i].Value.StartsWith("Shakes")) || (chkbx.Items[i].Value.StartsWith("Singers")) || (chkbx.Items[i].Value.StartsWith("MondayNights")) || (chkbx.Items[i].Value.StartsWith("BasketballTEAMS")) || (chkbx.Items[i].Value.StartsWith("3on3Basketball")) || (chkbx.Items[i].Value.StartsWith("BibleStudy")) || (chkbx.Items[i].Value.StartsWith("OutreachBasketball")) || (chkbx.Items[i].Value.StartsWith("HSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("MSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("SoccerTEAMS")) || (chkbx.Items[i].Value.StartsWith("SoccerIntraMurals")) || (chkbx.Items[i].Value.StartsWith("Baseball")) || (chkbx.Items[i].Value.StartsWith("ImpactUrbanSchools")) || (chkbx.Items[i].Value.StartsWith("SpecialEvents")) || (chkbx.Items[i].Value.StartsWith("SummerDayCamp")) || (chkbx.Items[i].Value.StartsWith("AcademicReadingSupport")))
                            {
                                if (chkbx.Items[i].Value.StartsWith("3on3Basketball"))
                                {
                                    sql = sql + "," + "pl.[" + chkbx.Items[i].Value + "] ";
                                }
                                else
                                {
                                    sql = sql + "," + "pl." + chkbx.Items[i].Value + " ";
                                }
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("VehichleInsurance")) || (chkbx.Items[i].Value.StartsWith("VehichleInsuranceCodes")) || (chkbx.Items[i].Value.StartsWith("VehichleInsuranceDate")) || (chkbx.Items[i].Value.StartsWith("NationalCheck")) || (chkbx.Items[i].Value.StartsWith("NationalCheckCodes")) || (chkbx.Items[i].Value.StartsWith("NationalCheckDate")) || (chkbx.Items[i].Value.StartsWith("DMVCheck")) || (chkbx.Items[i].Value.StartsWith("DMVCheckCodes")) || (chkbx.Items[i].Value.StartsWith("DMVCheckDate")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheck")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheckCodes")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheckDate")) || (chkbx.Items[i].Value.StartsWith("BackgroundCheckPAID")) || (chkbx.Items[i].Value.StartsWith("BackgroundCheckPAIDDate")) || (chkbx.Items[i].Value.StartsWith("GeneralInformation")) || (chkbx.Items[i].Value.StartsWith("SpiritualJourney")) || (chkbx.Items[i].Value.StartsWith("ReleaseWaiver")) || (chkbx.Items[i].Value.StartsWith("NewVolunteerTraining")) || (chkbx.Items[i].Value.StartsWith("NewVolunteerTrainingDate")))
                            {
                                sql = sql + "," + "vd." + chkbx.Items[i].Value + " ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Church")) || (chkbx.Items[i].Value.StartsWith("Address")) || (chkbx.Items[i].Value.StartsWith("City")) || (chkbx.Items[i].Value.StartsWith("State")) || (chkbx.Items[i].Value.StartsWith("Zip")) || (chkbx.Items[i].Value.StartsWith("Address")) || (chkbx.Items[i].Value.StartsWith("HomePhone")) || (chkbx.Items[i].Value.StartsWith("CellPhone")))
                            //|| (chkbx.Items[i].Value.StartsWith("VehichleInsuranceCodes")) || (chkbx.Items[i].Value.StartsWith("VehichleInsuranceDate")) || (chkbx.Items[i].Value.StartsWith("NationalCheck")) || (chkbx.Items[i].Value.StartsWith("NationalCheckCodes")) || (chkbx.Items[i].Value.StartsWith("NationalCheckDate")) || (chkbx.Items[i].Value.StartsWith("DMVCheck")) || (chkbx.Items[i].Value.StartsWith("DMVCheckCodes")) || (chkbx.Items[i].Value.StartsWith("DMVCheckDate")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheck")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheckCodes")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheckDate")) || (chkbx.Items[i].Value.StartsWith("BackgroundCheckPAID")) || (chkbx.Items[i].Value.StartsWith("BackgroundCheckPAIDDate")) || (chkbx.Items[i].Value.StartsWith("GeneralInformation")) || (chkbx.Items[i].Value.StartsWith("SpiritualJourney")) || (chkbx.Items[i].Value.StartsWith("ReleaseWaiver")) || (chkbx.Items[i].Value.StartsWith("NewVolunteerTraining")) || (chkbx.Items[i].Value.StartsWith("NewVolunteerTrainingDate")))
                            {
                                sql = sql + "," + "vi." + chkbx.Items[i].Value + " ";
                            }
                            else
                            {
                                if (chkbx.Items[i].Value.StartsWith("3on3Basketball"))
                                {
                                    sql = sql + "," + "pl.[" + chkbx.Items[i].Value + "] ";
                                }
                                else
                                {
                                    sql = sql + "," + "vd." + chkbx.Items[i].Value + " ";
                                }
                            }
                        }
                        else if (i == 0)
                        {
                            //if ((chkbx.Items[i].Value.StartsWith("MSHSChoir")) || (chkbx.Items[i].Value.StartsWith("ChildrensChoir")) || (chkbx.Items[i].Value.StartsWith("PerformingArts")) || (chkbx.Items[i].Value.StartsWith("Shakes")) || (chkbx.Items[i].Value.StartsWith("Singers")) || (chkbx.Items[i].Value.StartsWith("MondayNights")) || (chkbx.Items[i].Value.StartsWith("BasketballTEAMS")) || (chkbx.Items[i].Value.StartsWith("3on3Basketball")) || (chkbx.Items[i].Value.StartsWith("BibleStudy")) || (chkbx.Items[i].Value.StartsWith("OutreachBasketball")) || (chkbx.Items[i].Value.StartsWith("HSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("MSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("SoccerTEAMS")) || (chkbx.Items[i].Value.StartsWith("SoccerIntraMurals")))
                            if ((chkbx.Items[i].Value.StartsWith("MSHSChoir")) || (chkbx.Items[i].Value.StartsWith("ChildrensChoir")) || (chkbx.Items[i].Value.StartsWith("PerformingArts")) || (chkbx.Items[i].Value.StartsWith("Shakes")) || (chkbx.Items[i].Value.StartsWith("Singers")) || (chkbx.Items[i].Value.StartsWith("MondayNights")) || (chkbx.Items[i].Value.StartsWith("BasketballTEAMS")) || (chkbx.Items[i].Value.StartsWith("3on3Basketball")) || (chkbx.Items[i].Value.StartsWith("BibleStudy")) || (chkbx.Items[i].Value.StartsWith("OutreachBasketball")) || (chkbx.Items[i].Value.StartsWith("HSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("MSBasketballLg")) || (chkbx.Items[i].Value.StartsWith("SoccerTEAMS")) || (chkbx.Items[i].Value.StartsWith("SoccerIntraMurals")) || (chkbx.Items[i].Value.StartsWith("Baseball")) || (chkbx.Items[i].Value.StartsWith("ImpactUrbanSchools")) || (chkbx.Items[i].Value.StartsWith("SpecialEvents")) || (chkbx.Items[i].Value.StartsWith("SummerDayCamp")) || (chkbx.Items[i].Value.StartsWith("AcademicReadingSupport")))
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
                            else if ((chkbx.Items[i].Value.StartsWith("VehichleInsurance")) || (chkbx.Items[i].Value.StartsWith("VehichleInsuranceCodes")) || (chkbx.Items[i].Value.StartsWith("VehichleInsuranceDate")) || (chkbx.Items[i].Value.StartsWith("NationalCheck")) || (chkbx.Items[i].Value.StartsWith("NationalCheckCodes")) || (chkbx.Items[i].Value.StartsWith("NationalCheckDate")) || (chkbx.Items[i].Value.StartsWith("DMVCheck")) || (chkbx.Items[i].Value.StartsWith("DMVCheckCodes")) || (chkbx.Items[i].Value.StartsWith("DMVCheckDate")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheck")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheckCodes")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheckDate")) || (chkbx.Items[i].Value.StartsWith("BackgroundCheckPAID")) || (chkbx.Items[i].Value.StartsWith("BackgroundCheckPAIDDate")) || (chkbx.Items[i].Value.StartsWith("GeneralInformation")) || (chkbx.Items[i].Value.StartsWith("SpiritualJourney")) || (chkbx.Items[i].Value.StartsWith("ReleaseWaiver")) || (chkbx.Items[i].Value.StartsWith("NewVolunteerTraining")) || (chkbx.Items[i].Value.StartsWith("NewVolunteerTrainingDate")))
                            {
                                sql = sql + "," + "vd." + chkbx.Items[i].Value + " ";
                            }
                            else if ((chkbx.Items[i].Value.StartsWith("Church")) || (chkbx.Items[i].Value.StartsWith("Address")) || (chkbx.Items[i].Value.StartsWith("City")) || (chkbx.Items[i].Value.StartsWith("State")) || (chkbx.Items[i].Value.StartsWith("Zip")) || (chkbx.Items[i].Value.StartsWith("Address")) || (chkbx.Items[i].Value.StartsWith("HomePhone")) || (chkbx.Items[i].Value.StartsWith("CellPhone")))
                            //|| (chkbx.Items[i].Value.StartsWith("VehichleInsuranceCodes")) || (chkbx.Items[i].Value.StartsWith("VehichleInsuranceDate")) || (chkbx.Items[i].Value.StartsWith("NationalCheck")) || (chkbx.Items[i].Value.StartsWith("NationalCheckCodes")) || (chkbx.Items[i].Value.StartsWith("NationalCheckDate")) || (chkbx.Items[i].Value.StartsWith("DMVCheck")) || (chkbx.Items[i].Value.StartsWith("DMVCheckCodes")) || (chkbx.Items[i].Value.StartsWith("DMVCheckDate")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheck")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheckCodes")) || (chkbx.Items[i].Value.StartsWith("PACriminalCheckDate")) || (chkbx.Items[i].Value.StartsWith("BackgroundCheckPAID")) || (chkbx.Items[i].Value.StartsWith("BackgroundCheckPAIDDate")) || (chkbx.Items[i].Value.StartsWith("GeneralInformation")) || (chkbx.Items[i].Value.StartsWith("SpiritualJourney")) || (chkbx.Items[i].Value.StartsWith("ReleaseWaiver")) || (chkbx.Items[i].Value.StartsWith("NewVolunteerTraining")) || (chkbx.Items[i].Value.StartsWith("NewVolunteerTrainingDate")))
                            {
                                sql = sql + "," + "vi." + chkbx.Items[i].Value + " ";
                            }
                            else
                            {
                                sql = sql + "vd." + chkbx.Items[i].Value + " ";
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
            //Special case fields.. handling as INTEGERS...RCM...3/9/12.
            //      sql = sql.Replace("spa.Grade", "CONVERT(INT,spa.Grade) as 'Grade'");
            //sql = sql.Replace("si.Zip", "CONVERT(INT,si.Zip) as 'Zip'");
            sql = sql.Replace("si.Age", "CONVERT(INT,si.Age) as 'Age'");
            return sql;
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

        protected void cmbRetrieveResults_Click(object sender, EventArgs e)
        {
            con.Open();
            string group = "";
            //lblErrorMessage.Visible = false;
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

                foreach (ListItem item in chbCustomList.Items)
                {
                    if (item.Selected == true)
                    {
                        flag = 1;
                        ChooseCustomView = true;
                    }
                }

                if (ChooseCustomView)
                {   //Use the custom view created by the user..RCM.. 10/26/11.
                    sql_fields = DetermineCustomViewQuery();
                    //sql_fields = DetermineCustomViewQuery(GradeFLAG);
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
                {   //The pre-defined default view...RCM..8/26/13.
                    //sql_fields = "select vd.LastName + ',' + vd.FirstName as 'Name', vd.VehichleInsurance, vd.NationalCheck, vd.DMVCheck, vd.PACriminalCheck, vd.BackgroundCheckPAID ";
                    //sql_fields = "select vd.LastName, vd.FirstName, vd.VehichleInsurance, vd.NationalCheck, vd.DMVCheck, vd.PACriminalCheck, vd.BackgroundCheckPAID ";
                    //group = sql_fields;
                    //group = group.Replace("select", "GROUP BY ");

                    //Handle the different desired views on the data...RCM..
                    if (ddlAdvancedSearchView.Text == "Select a View (Optional)")
                    {
                        sql_fields = "select vd.LastName, vd.FirstName, vd.NationalCheckCodes, vd.NationalCheckDate, vd.DMVCheckCodes, vd.DMVCheckDate, vd.PACriminalCheckCodes, vd.PACriminalCheckDate ";
                        //sql_fields = "select vd.LastName, vd.FirstName, vd.VehichleInsurance, vd.NationalCheck, vd.DMVCheck, vd.PACriminalCheck, vd.BackgroundCheckPAID ";
                        //sql_fields = "select vd.LastName + ',' + vd.FirstName as 'Name', si.address as 'Address', si.city as 'City', si.state as 'State', si.zip as 'Zip', si.homephone as 'HomePhone', si.CellPhone, si.Email ";
                    }
                    else if (ddlAdvancedSearchView.Text == "Background Check Info")
                    {
                        sql_fields = "select vd.LastName, vd.FirstName, vd.NationalCheckCodes, vd.NationalCheckDate, vd.DMVCheckCodes, vd.DMVCheckDate, vd.PACriminalCheckCodes, vd.PACriminalCheckDate ";
                        //sql_fields = "select vd.LastName + ',' + vd.FirstName as 'Name', si.address as 'Address', si.city as 'City', si.state as 'State', si.zip as 'Zip', si.homephone as 'HomePhone', si.CellPhone, si.Email ";
                    }
                    else if (ddlAdvancedSearchView.Text == "Application Info")
                    {
                        sql_fields = "select vd.LastName, vd.FirstName, vd.GeneralInformation, vd.ReleaseWaiver, vd.SpiritualJourney, vd.NewVolunteerTraining, vd.VehichleInsurance, vd.VehichleInsuranceCodes, vd.BackgroundCheckPAID, vd.BackgroundCheckCodes ";
                        //sql_fields = "select vd.LastName + ',' + vd.FirstName as 'Name', si.email, si.dob, si.sex, si.church, si.healthconditions, si.notes ";
                    }
                    //else if (ddlAdvancedSearchView.Text == "Program Info")
                    //{
                    //    sql_fields = "select vd.LastName + ',' + vd.FirstName as 'Name', si.backgroundcheck, si.spiritualjourney, si.vehichleinsurance, si.releasewaiver, si.generalinformation, si.newvolunteertraining  ";
                    //}
                    //else if (ddlAdvancedSearchView.Text == "DiscipleshipMentor Info")
                    //{
                    //    sql_fields = "select vd.LastName + ',' + vd.FirstName as 'Name', si.discipleshipmentorparticipation, si.discipleshipmentortraining, si.discipleshipmentortraineddate, si.discipleshipmentorstartdate, si.discipleshipmentornotes, si.discipleshipmentorpotentials, si.discipleshipmentorwaitinglist ";
                    //}
                    //else if (ddlAdvancedSearchView.Text == "All Available Info")
                    //{
                    //    sql_fields = "select vd.LastName + ',' + vd.FirstName as 'Name', si.address as 'Address', si.city as 'City', si.state as 'State', si.zip as 'Zip', si.homephone as 'HomePhone', si.CellPhone, si.Email, "
                    //               + "si.dob, si.sex, si.church, si.careergoal, si.healthconditions, si.notes, si.tshirtsize, si.biblestudyparticipation, si.havereceivedchrist, si.whenreceivedchrist, "
                    //               + "vd.backgroundcheck, vd.spiritualjourney, vd.vehichleinsurance, vd.releasewaiver, vd.generalinformation, si.newvolunteertraining, si.discipleshipmentorparticipation, si.discipleshipmentortraining, "
                    //               + "si.discipleshipmentortraineddate, si.discipleshipmentorstartdate, si.discipleshipmentornotes, si.discipleshipmentorpotentials as 'DM WaitingList' ";
                    //}
                    group = sql_fields;
                    group = group.Replace("select", "GROUP BY ");
                }

                sql = sql_fields
                + "FROM UIF_PerformingArts.dbo.volunteerdetails  vd "
                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.ProgramsList pl "
                + "ON (vd.LastName = pl.LastName AND vd.FirstName = pl.FirstName AND pl.staffvolunteer = 1) "
                + "LEFT OUTER JOIN UIF_PerformingArts.dbo.VolunteerInformation vi "
                + "ON (vd.LastName = vi.LastName AND vd.FirstName = vi.FirstName) ";

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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator.Text == "equals")
                                    {
                                        sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades.Text.Trim() == "1") || (ddlGrades.Text.Trim() == "2") || (ddlGrades.Text.Trim() == "3") || (ddlGrades.Text.Trim() == "4") || (ddlGrades.Text.Trim() == "5") || (ddlGrades.Text.Trim() == "6") || (ddlGrades.Text.Trim() == "7") || (ddlGrades.Text.Trim() == "8") || (ddlGrades.Text.Trim() == "9") || (ddlGrades.Text.Trim() == "10") || (ddlGrades.Text.Trim() == "11") || (ddlGrades.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'GR13' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'GR13' AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField.Text == "Age")
                            {
                                if (ddlChooseOperator.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) = " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) <> " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) > " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) >= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) < " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) <= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) like " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) NOT like " + txbSearchValue.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
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
                        else if (ddlChooseField.Text == "GeneralInformation" || ddlChooseField.Text == "SpiritualJourney" || ddlChooseField.Text == "ReleaseWaiver" || ddlChooseField.Text == "NewVolunteerTraining" || ddlChooseField.Text == "BackgroundCheckPAID" || ddlChooseField.Text == "VehichleInsurance" || ddlChooseField.Text == "DMVCheck" || ddlChooseField.Text == "PACriminalCheck" || ddlChooseField.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
                            }
                        }
                        else if (ddlChooseField.Text == "DMVCheckCodes" || ddlChooseField.Text == "NationalCheckCodes" || ddlChooseField.Text == "PACriminalCheckCodes" || ddlChooseField.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text + "' ";
                        }
                        else if (ddlChooseField.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "BEFORE")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "On or AFTER to present")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "On or BEFORE")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + txbSearchValue.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorCharacter.Text == "equals")
                                    {
                                        sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades.Text.Trim() == "1") || (ddlGrades.Text.Trim() == "2") || (ddlGrades.Text.Trim() == "3") || (ddlGrades.Text.Trim() == "4") || (ddlGrades.Text.Trim() == "5") || (ddlGrades.Text.Trim() == "6") || (ddlGrades.Text.Trim() == "7") || (ddlGrades.Text.Trim() == "8") || (ddlGrades.Text.Trim() == "9") || (ddlGrades.Text.Trim() == "10") || (ddlGrades.Text.Trim() == "11") || (ddlGrades.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField.Text == "Age")
                            {
                                if (ddlOperatorCharacter.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) = " + txbSearchValue.Text.Trim() + " ";
                                }
                                if (ddlOperatorCharacter.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) <> " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) > " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) >= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) < " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) <= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) like " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) NOT like " + txbSearchValue.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlOperatorCharacter.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
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
                        else if (ddlChooseField.Text == "GeneralInformation" || ddlChooseField.Text == "SpiritualJourney" || ddlChooseField.Text == "ReleaseWaiver" || ddlChooseField.Text == "NewVolunteerTraining" || ddlChooseField.Text == "BackgroundCheckPAID" || ddlChooseField.Text == "VehichleInsurance" || ddlChooseField.Text == "DMVCheck" || ddlChooseField.Text == "PACriminalCheck" || ddlChooseField.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
                            }
                        }
                        else if (ddlChooseField.Text == "DMVCheckCodes" || ddlChooseField.Text == "NationalCheckCodes" || ddlChooseField.Text == "PACriminalCheckCodes" || ddlChooseField.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text + "' ";
                        }
                        else if (ddlChooseField.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "BEFORE")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "On or AFTER to present")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "On or BEFORE")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + txbSearchValue.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorBoolean.Text == "equals")
                                    {
                                        sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades.Text.Trim() == "1") || (ddlGrades.Text.Trim() == "2") || (ddlGrades.Text.Trim() == "3") || (ddlGrades.Text.Trim() == "4") || (ddlGrades.Text.Trim() == "5") || (ddlGrades.Text.Trim() == "6") || (ddlGrades.Text.Trim() == "7") || (ddlGrades.Text.Trim() == "8") || (ddlGrades.Text.Trim() == "9") || (ddlGrades.Text.Trim() == "10") || (ddlGrades.Text.Trim() == "11") || (ddlGrades.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField.Text == "Age")
                            {
                                if (ddlOperatorBoolean.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) = " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) > " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) >= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) < " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) <= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) like " + txbSearchValue.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlOperatorBoolean.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
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
                        else if (ddlChooseField.Text == "GeneralInformation" || ddlChooseField.Text == "SpiritualJourney" || ddlChooseField.Text == "ReleaseWaiver" || ddlChooseField.Text == "NewVolunteerTraining" || ddlChooseField.Text == "BackgroundCheckPAID" || ddlChooseField.Text == "VehichleInsurance" || ddlChooseField.Text == "DMVCheck" || ddlChooseField.Text == "PACriminalCheck" || ddlChooseField.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
                            }
                        }
                        else if (ddlChooseField.Text == "DMVCheckCodes" || ddlChooseField.Text == "NationalCheckCodes" || ddlChooseField.Text == "PACriminalCheckCodes" || ddlChooseField.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text + "' ";
                        }
                        else if (ddlChooseField.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "BEFORE")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "On or AFTER to present")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "On or BEFORE")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + txbSearchValue.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator.Text == "Choose Operator") && (ddlOperatorCharacter.Text == "Choose Operator") && (ddlOperatorBoolean.Text == "Choose Operator") && (ddlChooseDate.Text != "Choose Operator"))
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator.Text == "equals")
                                    {
                                        sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades.Text.Trim() == "1") || (ddlGrades.Text.Trim() == "2") || (ddlGrades.Text.Trim() == "3") || (ddlGrades.Text.Trim() == "4") || (ddlGrades.Text.Trim() == "5") || (ddlGrades.Text.Trim() == "6") || (ddlGrades.Text.Trim() == "7") || (ddlGrades.Text.Trim() == "8") || (ddlGrades.Text.Trim() == "9") || (ddlGrades.Text.Trim() == "10") || (ddlGrades.Text.Trim() == "11") || (ddlGrades.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR13' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'GR13' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + "where (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'GR13' AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField.Text == "Age")
                            {
                                if (ddlChooseOperator.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) = " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) <> " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) > " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) >= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) < " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) <= " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) like " + txbSearchValue.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + "where CONVERT(INT,vd.Age) NOT like " + txbSearchValue.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField.Text == "CampDropOff" || ddlChooseField.Text == "CampPickUp" || ddlChooseField.Text == "CurrentRegistrationForm" || ddlChooseField.Text == "Dance" || ddlChooseField.Text == "HaveReceivedChrist" || ddlChooseField.Text == "MailingListInclude" || ddlChooseField.Text == "ParentalConsentForm" || ddlChooseField.Text == "PermissionToTransport" || ddlChooseField.Text == "PromotionalRelease" || ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
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
                        else if (ddlChooseField.Text == "GeneralInformation" || ddlChooseField.Text == "SpiritualJourney" || ddlChooseField.Text == "ReleaseWaiver" || ddlChooseField.Text == "NewVolunteerTraining" || ddlChooseField.Text == "BackgroundCheckPAID" || ddlChooseField.Text == "VehichleInsurance" || ddlChooseField.Text == "DMVCheck" || ddlChooseField.Text == "PACriminalCheck" || ddlChooseField.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + SearchBool + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
                            }
                        }
                        else if (ddlChooseField.Text == "DMVCheckCodes" || ddlChooseField.Text == "NationalCheckCodes" || ddlChooseField.Text == "PACriminalCheckCodes" || ddlChooseField.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlGrades.Text + "' ";
                        }
                        else if (ddlChooseField.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "BEFORE")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "On or AFTER to present")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "On or BEFORE")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " = '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " > '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " < '" + txbSearchValue.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " like '%" + txbSearchValue.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " <> " + SearchBool + " ";
                            }
                            else if (ddlChooseOperator.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField.Text + " not like '%" + SearchBool + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator2.Text == "equals")
                                    {
                                        sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades2.Text.Trim() == "1") || (ddlGrades2.Text.Trim() == "2") || (ddlGrades2.Text.Trim() == "3") || (ddlGrades2.Text.Trim() == "4") || (ddlGrades2.Text.Trim() == "5") || (ddlGrades2.Text.Trim() == "6") || (ddlGrades2.Text.Trim() == "7") || (ddlGrades2.Text.Trim() == "8") || (ddlGrades2.Text.Trim() == "9") || (ddlGrades2.Text.Trim() == "10") || (ddlGrades2.Text.Trim() == "11") || (ddlGrades2.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator2.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField2.Text == "Age")
                            {
                                if (ddlChooseOperator2.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue2.Text.Trim() + " ";
                                }
                                if (ddlChooseOperator2.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <> " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) NOT like " + txbSearchValue2.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField2.Text == "CampDropOff" || ddlChooseField2.Text == "CampPickUp" || ddlChooseField2.Text == "CurrentRegistrationForm" || ddlChooseField2.Text == "Dance" || ddlChooseField2.Text == "HaveReceivedChrist" || ddlChooseField2.Text == "MailingListInclude" || ddlChooseField2.Text == "ParentalConsentForm" || ddlChooseField2.Text == "PermissionToTransport" || ddlChooseField2.Text == "PromotionalRelease" || ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " NOT like '%" + SearchBool2 + "%' ";
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
                        else if (ddlChooseField2.Text == "GeneralInformation" || ddlChooseField2.Text == "SpiritualJourney" || ddlChooseField2.Text == "ReleaseWaiver" || ddlChooseField2.Text == "NewVolunteerTraining" || ddlChooseField2.Text == "BackgroundCheckPAID" || ddlChooseField2.Text == "VehichleInsurance" || ddlChooseField2.Text == "DMVCheck" || ddlChooseField2.Text == "PACriminalCheck" || ddlChooseField2.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " not like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if (ddlChooseField2.Text == "DMVCheckCodes" || ddlChooseField2.Text == "NationalCheckCodes" || ddlChooseField2.Text == "PACriminalCheckCodes" || ddlChooseField2.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text + "' ";
                        }
                        else if (ddlChooseField2.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " >= '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <= '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <> '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " NOT like '%" + txbSearchValue2.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorCharacter2.Text == "equals")
                                    {
                                        sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades2.Text.Trim() == "1") || (ddlGrades2.Text.Trim() == "2") || (ddlGrades2.Text.Trim() == "3") || (ddlGrades2.Text.Trim() == "4") || (ddlGrades2.Text.Trim() == "5") || (ddlGrades2.Text.Trim() == "6") || (ddlGrades2.Text.Trim() == "7") || (ddlGrades2.Text.Trim() == "8") || (ddlGrades2.Text.Trim() == "9") || (ddlGrades2.Text.Trim() == "10") || (ddlGrades2.Text.Trim() == "11") || (ddlGrades2.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter2.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter2.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField2.Text == "Age")
                            {
                                if (ddlOperatorCharacter2.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <> " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter2.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) NOT like " + txbSearchValue2.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField2.Text == "CampDropOff" || ddlChooseField2.Text == "CampPickUp" || ddlChooseField2.Text == "CurrentRegistrationForm" || ddlChooseField2.Text == "Dance" || ddlChooseField2.Text == "HaveReceivedChrist" || ddlChooseField2.Text == "MailingListInclude" || ddlChooseField2.Text == "ParentalConsentForm" || ddlChooseField2.Text == "PermissionToTransport" || ddlChooseField2.Text == "PromotionalRelease" || ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorCharacter2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorCharacter2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorCharacter2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " NOT like '%" + SearchBool2 + "%' ";
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
                        else if (ddlChooseField2.Text == "GeneralInformation" || ddlChooseField2.Text == "SpiritualJourney" || ddlChooseField2.Text == "ReleaseWaiver" || ddlChooseField2.Text == "NewVolunteerTraining" || ddlChooseField2.Text == "BackgroundCheckPAID" || ddlChooseField2.Text == "VehichleInsurance" || ddlChooseField2.Text == "DMVCheck" || ddlChooseField2.Text == "PACriminalCheck" || ddlChooseField2.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " not like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if (ddlChooseField2.Text == "DMVCheckCodes" || ddlChooseField2.Text == "NationalCheckCodes" || ddlChooseField2.Text == "PACriminalCheckCodes" || ddlChooseField2.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text + "' ";
                        }
                        else if (ddlChooseField2.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " >= '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <= '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <> '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                            else if (ddlOperatorCharacter2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " NOT like '%" + txbSearchValue2.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorBoolean2.Text == "equals")
                                    {
                                        sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades2.Text.Trim() == "1") || (ddlGrades2.Text.Trim() == "2") || (ddlGrades2.Text.Trim() == "3") || (ddlGrades2.Text.Trim() == "4") || (ddlGrades2.Text.Trim() == "5") || (ddlGrades2.Text.Trim() == "6") || (ddlGrades2.Text.Trim() == "7") || (ddlGrades2.Text.Trim() == "8") || (ddlGrades2.Text.Trim() == "9") || (ddlGrades2.Text.Trim() == "10") || (ddlGrades2.Text.Trim() == "11") || (ddlGrades2.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean2.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean2.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField2.Text == "Age")
                            {
                                if (ddlOperatorBoolean2.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean2.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue2.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField2.Text == "CampDropOff" || ddlChooseField2.Text == "CampPickUp" || ddlChooseField2.Text == "CurrentRegistrationForm" || ddlChooseField2.Text == "Dance" || ddlChooseField2.Text == "HaveReceivedChrist" || ddlChooseField2.Text == "MailingListInclude" || ddlChooseField2.Text == "ParentalConsentForm" || ddlChooseField2.Text == "PermissionToTransport" || ddlChooseField2.Text == "PromotionalRelease" || ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlOperatorBoolean2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
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
                        else if (ddlChooseField2.Text == "GeneralInformation" || ddlChooseField2.Text == "SpiritualJourney" || ddlChooseField2.Text == "ReleaseWaiver" || ddlChooseField2.Text == "NewVolunteerTraining" || ddlChooseField2.Text == "BackgroundCheckPAID" || ddlChooseField2.Text == "VehichleInsurance" || ddlChooseField2.Text == "DMVCheck" || ddlChooseField2.Text == "PACriminalCheck" || ddlChooseField2.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " not like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if (ddlChooseField2.Text == "DMVCheckCodes" || ddlChooseField2.Text == "NationalCheckCodes" || ddlChooseField2.Text == "PACriminalCheckCodes" || ddlChooseField2.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text + "' ";
                        }
                        else if (ddlChooseField2.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " >= '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <= '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator2.Text == "Choose Operator") && (ddlOperatorCharacter2.Text == "Choose Operator") && (ddlOperatorBoolean2.Text == "Choose Operator") && (ddlChooseDate2.Text != "Choose Operator"))
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator2.Text == "equals")
                                    {
                                        sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades2.Text.Trim() == "1") || (ddlGrades2.Text.Trim() == "2") || (ddlGrades2.Text.Trim() == "3") || (ddlGrades2.Text.Trim() == "4") || (ddlGrades2.Text.Trim() == "5") || (ddlGrades2.Text.Trim() == "6") || (ddlGrades2.Text.Trim() == "7") || (ddlGrades2.Text.Trim() == "8") || (ddlGrades2.Text.Trim() == "9") || (ddlGrades2.Text.Trim() == "10") || (ddlGrades2.Text.Trim() == "11") || (ddlGrades2.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator2.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades2.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator2.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades2.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField2.Text == "Age")
                            {
                                if (ddlChooseOperator2.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue2.Text.Trim() + " ";
                                }
                                if (ddlChooseOperator2.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <> " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue2.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator2.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber1.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) NOT like " + txbSearchValue2.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField2.Text == "CampDropOff" || ddlChooseField2.Text == "CampPickUp" || ddlChooseField2.Text == "CurrentRegistrationForm" || ddlChooseField2.Text == "Dance" || ddlChooseField2.Text == "HaveReceivedChrist" || ddlChooseField2.Text == "MailingListInclude" || ddlChooseField2.Text == "ParentalConsentForm" || ddlChooseField2.Text == "PermissionToTransport" || ddlChooseField2.Text == "PromotionalRelease" || ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " NOT like '%" + SearchBool2 + "%' ";
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
                        else if (ddlChooseField2.Text == "GeneralInformation" || ddlChooseField2.Text == "SpiritualJourney" || ddlChooseField2.Text == "ReleaseWaiver" || ddlChooseField2.Text == "NewVolunteerTraining" || ddlChooseField2.Text == "BackgroundCheckPAID" || ddlChooseField2.Text == "VehichleInsurance" || ddlChooseField2.Text == "DMVCheck" || ddlChooseField2.Text == "PACriminalCheck" || ddlChooseField2.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " = " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " > " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " < " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " like '%" + SearchBool2 + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " <> " + SearchBool2 + " ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField2.Text + " not like '%" + SearchBool2 + "%' ";
                            }
                        }
                        else if (ddlChooseField2.Text == "DMVCheckCodes" || ddlChooseField2.Text == "NationalCheckCodes" || ddlChooseField2.Text == "PACriminalCheckCodes" || ddlChooseField2.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField2.Text + " = '" + ddlGrades2.Text + "' ";
                        }
                        else if (ddlChooseField2.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " >= '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <= '" + ddlPickDateRangeYear22.Text + "-" + ddlPickDateRangeMonth22.Text + "-" + ddlPickDateRangeDay22.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator2.Text == "equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " = '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            if (ddlChooseOperator2.Text == "NOT equals")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " <> '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == ">")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " > '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "<")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " < '" + txbSearchValue2.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator2.Text == "contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " like '%" + txbSearchValue2.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator2.Text == "NOT contains")
                            {
                                sql = sql + rblNumber1.Text + " vd." + ddlChooseField2.Text + " NOT like '%" + txbSearchValue2.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator3.Text == "equals")
                                    {
                                        sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades3.Text.Trim() == "1") || (ddlGrades3.Text.Trim() == "2") || (ddlGrades3.Text.Trim() == "3") || (ddlGrades3.Text.Trim() == "4") || (ddlGrades3.Text.Trim() == "5") || (ddlGrades3.Text.Trim() == "6") || (ddlGrades3.Text.Trim() == "7") || (ddlGrades3.Text.Trim() == "8") || (ddlGrades3.Text.Trim() == "9") || (ddlGrades3.Text.Trim() == "10") || (ddlGrades3.Text.Trim() == "11") || (ddlGrades3.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator3.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField3.Text == "Age")
                            {
                                if (ddlChooseOperator3.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <> " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) NOT like " + txbSearchValue3.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField3.Text == "CampDropOff" || ddlChooseField3.Text == "CampPickUp" || ddlChooseField3.Text == "CurrentRegistrationForm" || ddlChooseField3.Text == "Dance" || ddlChooseField3.Text == "HaveReceivedChrist" || ddlChooseField3.Text == "MailingListInclude" || ddlChooseField3.Text == "ParentalConsentForm" || ddlChooseField3.Text == "PermissionToTransport" || ddlChooseField3.Text == "PromotionalRelease" || ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " NOT like '%" + SearchBool3 + "%' ";
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
                        else if (ddlChooseField3.Text == "GeneralInformation" || ddlChooseField3.Text == "SpiritualJourney" || ddlChooseField3.Text == "ReleaseWaiver" || ddlChooseField3.Text == "NewVolunteerTraining" || ddlChooseField3.Text == "BackgroundCheckPAID" || ddlChooseField3.Text == "VehichleInsurance" || ddlChooseField3.Text == "DMVCheck" || ddlChooseField3.Text == "PACriminalCheck" || ddlChooseField3.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " not like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if (ddlChooseField3.Text == "DMVCheckCodes" || ddlChooseField3.Text == "NationalCheckCodes" || ddlChooseField3.Text == "PACriminalCheckCodes" || ddlChooseField3.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text + "' ";
                        }
                        else if (ddlChooseField3.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " >= '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <= '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <> '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " NOT like '%" + txbSearchValue3.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorCharacter3.Text == "equals")
                                    {
                                        sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades3.Text.Trim() == "1") || (ddlGrades3.Text.Trim() == "2") || (ddlGrades3.Text.Trim() == "3") || (ddlGrades3.Text.Trim() == "4") || (ddlGrades3.Text.Trim() == "5") || (ddlGrades3.Text.Trim() == "6") || (ddlGrades3.Text.Trim() == "7") || (ddlGrades3.Text.Trim() == "8") || (ddlGrades3.Text.Trim() == "9") || (ddlGrades3.Text.Trim() == "10") || (ddlGrades3.Text.Trim() == "11") || (ddlGrades3.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter3.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter3.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField3.Text == "Age")
                            {
                                if (ddlOperatorCharacter3.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <> " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter3.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) NOT like " + txbSearchValue3.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField3.Text == "CampDropOff" || ddlChooseField3.Text == "CampPickUp" || ddlChooseField3.Text == "CurrentRegistrationForm" || ddlChooseField3.Text == "Dance" || ddlChooseField3.Text == "HaveReceivedChrist" || ddlChooseField3.Text == "MailingListInclude" || ddlChooseField3.Text == "ParentalConsentForm" || ddlChooseField3.Text == "PermissionToTransport" || ddlChooseField3.Text == "PromotionalRelease" || ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorCharacter3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorCharacter3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorCharacter3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " NOT like '%" + SearchBool3 + "%' ";
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
                        else if (ddlChooseField3.Text == "GeneralInformation" || ddlChooseField3.Text == "SpiritualJourney" || ddlChooseField3.Text == "ReleaseWaiver" || ddlChooseField3.Text == "NewVolunteerTraining" || ddlChooseField3.Text == "BackgroundCheckPAID" || ddlChooseField3.Text == "VehichleInsurance" || ddlChooseField3.Text == "DMVCheck" || ddlChooseField3.Text == "PACriminalCheck" || ddlChooseField3.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " not like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if (ddlChooseField3.Text == "DMVCheckCodes" || ddlChooseField3.Text == "NationalCheckCodes" || ddlChooseField3.Text == "PACriminalCheckCodes" || ddlChooseField3.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text + "' ";
                        }
                        else if (ddlChooseField3.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " >= '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <= '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <> '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                            else if (ddlOperatorCharacter3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " NOT like '%" + txbSearchValue3.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorBoolean3.Text == "equals")
                                    {
                                        sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades3.Text.Trim() == "1") || (ddlGrades3.Text.Trim() == "2") || (ddlGrades3.Text.Trim() == "3") || (ddlGrades3.Text.Trim() == "4") || (ddlGrades3.Text.Trim() == "5") || (ddlGrades3.Text.Trim() == "6") || (ddlGrades3.Text.Trim() == "7") || (ddlGrades3.Text.Trim() == "8") || (ddlGrades3.Text.Trim() == "9") || (ddlGrades3.Text.Trim() == "10") || (ddlGrades3.Text.Trim() == "11") || (ddlGrades3.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean3.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean3.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField3.Text == "Age")
                            {
                                if (ddlOperatorBoolean3.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean3.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue3.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField3.Text == "CampDropOff" || ddlChooseField3.Text == "CampPickUp" || ddlChooseField3.Text == "CurrentRegistrationForm" || ddlChooseField3.Text == "Dance" || ddlChooseField3.Text == "HaveReceivedChrist" || ddlChooseField3.Text == "MailingListInclude" || ddlChooseField3.Text == "ParentalConsentForm" || ddlChooseField3.Text == "PermissionToTransport" || ddlChooseField3.Text == "PromotionalRelease" || ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlOperatorBoolean3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
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
                        else if (ddlChooseField3.Text == "GeneralInformation" || ddlChooseField3.Text == "SpiritualJourney" || ddlChooseField3.Text == "ReleaseWaiver" || ddlChooseField3.Text == "NewVolunteerTraining" || ddlChooseField3.Text == "BackgroundCheckPAID" || ddlChooseField3.Text == "VehichleInsurance" || ddlChooseField3.Text == "DMVCheck" || ddlChooseField3.Text == "PACriminalCheck" || ddlChooseField3.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " not like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if (ddlChooseField3.Text == "DMVCheckCodes" || ddlChooseField3.Text == "NationalCheckCodes" || ddlChooseField3.Text == "PACriminalCheckCodes" || ddlChooseField3.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text + "' ";
                        }
                        else if (ddlChooseField3.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " >= '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <= '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator3.Text == "Choose Operator") && (ddlOperatorCharacter3.Text == "Choose Operator") && (ddlOperatorBoolean3.Text == "Choose Operator") && (ddlChooseDate3.Text != "Choose Operator"))
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator3.Text == "equals")
                                    {
                                        sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades3.Text.Trim() == "1") || (ddlGrades3.Text.Trim() == "2") || (ddlGrades3.Text.Trim() == "3") || (ddlGrades3.Text.Trim() == "4") || (ddlGrades3.Text.Trim() == "5") || (ddlGrades3.Text.Trim() == "6") || (ddlGrades3.Text.Trim() == "7") || (ddlGrades3.Text.Trim() == "8") || (ddlGrades3.Text.Trim() == "9") || (ddlGrades3.Text.Trim() == "10") || (ddlGrades3.Text.Trim() == "11") || (ddlGrades3.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator3.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator3.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades3.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField3.Text == "Age")
                            {
                                if (ddlChooseOperator3.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "NOT equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <> " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue3.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator3.Text == "NOT contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber2.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) NOT like " + txbSearchValue3.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField3.Text == "CampDropOff" || ddlChooseField3.Text == "CampPickUp" || ddlChooseField3.Text == "CurrentRegistrationForm" || ddlChooseField3.Text == "Dance" || ddlChooseField3.Text == "HaveReceivedChrist" || ddlChooseField3.Text == "MailingListInclude" || ddlChooseField3.Text == "ParentalConsentForm" || ddlChooseField3.Text == "PermissionToTransport" || ddlChooseField3.Text == "PromotionalRelease" || ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " NOT like '%" + SearchBool3 + "%' ";
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
                        else if (ddlChooseField3.Text == "GeneralInformation" || ddlChooseField3.Text == "SpiritualJourney" || ddlChooseField3.Text == "ReleaseWaiver" || ddlChooseField3.Text == "NewVolunteerTraining" || ddlChooseField3.Text == "BackgroundCheckPAID" || ddlChooseField3.Text == "VehichleInsurance" || ddlChooseField3.Text == "DMVCheck" || ddlChooseField3.Text == "PACriminalCheck" || ddlChooseField3.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " = " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " > " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " < " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " like '%" + SearchBool3 + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " <> " + SearchBool3 + " ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField3.Text + " not like '%" + SearchBool3 + "%' ";
                            }
                        }
                        else if (ddlChooseField3.Text == "DMVCheckCodes" || ddlChooseField3.Text == "NationalCheckCodes" || ddlChooseField3.Text == "PACriminalCheckCodes" || ddlChooseField3.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField3.Text + " = '" + ddlGrades3.Text + "' ";
                        }
                        else if (ddlChooseField3.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " >= '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <= '" + ddlPickDateRangeYear3.Text + "-" + ddlPickDateRangeMonth3.Text + "-" + ddlPickDateRangeDay3.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator3.Text == "equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " = '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT equals")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " <> '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == ">")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " > '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "<")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " < '" + txbSearchValue3.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator3.Text == "contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " like '%" + txbSearchValue3.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator3.Text == "NOT contains")
                            {
                                sql = sql + rblNumber2.Text + " vd." + ddlChooseField3.Text + " NOT like '%" + txbSearchValue3.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator4.Text == "equals")
                                    {
                                        sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades4.Text.Trim() == "1") || (ddlGrades4.Text.Trim() == "2") || (ddlGrades4.Text.Trim() == "3") || (ddlGrades4.Text.Trim() == "4") || (ddlGrades4.Text.Trim() == "5") || (ddlGrades4.Text.Trim() == "6") || (ddlGrades4.Text.Trim() == "7") || (ddlGrades4.Text.Trim() == "8") || (ddlGrades4.Text.Trim() == "9") || (ddlGrades4.Text.Trim() == "10") || (ddlGrades4.Text.Trim() == "11") || (ddlGrades4.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator4.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField4.Text == "Age")
                            {
                                if (ddlChooseOperator4.Text == "equals")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == ">")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == ">=")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "<")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "<=")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "contains")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                            }
                        }
                        else if (ddlChooseField4.Text == "CampDropOff" || ddlChooseField4.Text == "CampPickUp" || ddlChooseField4.Text == "CurrentRegistrationForm" || ddlChooseField4.Text == "Dance" || ddlChooseField4.Text == "HaveReceivedChrist" || ddlChooseField4.Text == "MailingListInclude" || ddlChooseField4.Text == "ParentalConsentForm" || ddlChooseField4.Text == "PermissionToTransport" || ddlChooseField4.Text == "PromotionalRelease" || ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " NOT like '%" + SearchBool4 + "%' ";
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
                        else if (ddlChooseField4.Text == "GeneralInformation" || ddlChooseField4.Text == "SpiritualJourney" || ddlChooseField4.Text == "ReleaseWaiver" || ddlChooseField4.Text == "NewVolunteerTraining" || ddlChooseField4.Text == "BackgroundCheckPAID" || ddlChooseField4.Text == "VehichleInsurance" || ddlChooseField4.Text == "DMVCheck" || ddlChooseField4.Text == "PACriminalCheck" || ddlChooseField4.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " not like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if (ddlChooseField4.Text == "DMVCheckCodes" || ddlChooseField4.Text == "NationalCheckCodes" || ddlChooseField4.Text == "PACriminalCheckCodes" || ddlChooseField4.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text + "' ";
                        }
                        else if (ddlChooseField4.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " >= '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " <= '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " NOT like '%" + txbSearchValue4.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorCharacter4.Text == "equals")
                                    {
                                        sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades4.Text.Trim() == "1") || (ddlGrades4.Text.Trim() == "2") || (ddlGrades4.Text.Trim() == "3") || (ddlGrades4.Text.Trim() == "4") || (ddlGrades4.Text.Trim() == "5") || (ddlGrades4.Text.Trim() == "6") || (ddlGrades4.Text.Trim() == "7") || (ddlGrades4.Text.Trim() == "8") || (ddlGrades4.Text.Trim() == "9") || (ddlGrades4.Text.Trim() == "10") || (ddlGrades4.Text.Trim() == "11") || (ddlGrades4.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter4.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter4.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField4.Text == "Age")
                            {
                                if (ddlOperatorCharacter4.Text == "equals")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == ">")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == ">=")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == "<")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == "<=")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorCharacter4.Text == "contains")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                            }
                        }
                        else if (ddlChooseField4.Text == "CampDropOff" || ddlChooseField4.Text == "CampPickUp" || ddlChooseField4.Text == "CurrentRegistrationForm" || ddlChooseField4.Text == "Dance" || ddlChooseField4.Text == "HaveReceivedChrist" || ddlChooseField4.Text == "MailingListInclude" || ddlChooseField4.Text == "ParentalConsentForm" || ddlChooseField4.Text == "PermissionToTransport" || ddlChooseField4.Text == "PromotionalRelease" || ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorCharacter4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorCharacter4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorCharacter4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " NOT like '%" + SearchBool4 + "%' ";
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
                        else if (ddlChooseField4.Text == "GeneralInformation" || ddlChooseField4.Text == "SpiritualJourney" || ddlChooseField4.Text == "ReleaseWaiver" || ddlChooseField4.Text == "NewVolunteerTraining" || ddlChooseField4.Text == "BackgroundCheckPAID" || ddlChooseField4.Text == "VehichleInsurance" || ddlChooseField4.Text == "DMVCheck" || ddlChooseField4.Text == "PACriminalCheck" || ddlChooseField4.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " not like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if (ddlChooseField4.Text == "DMVCheckCodes" || ddlChooseField4.Text == "NationalCheckCodes" || ddlChooseField4.Text == "PACriminalCheckCodes" || ddlChooseField4.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text + "' ";
                        }
                        else if (ddlChooseField4.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " >= '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " <= '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " <> '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                            else if (ddlOperatorCharacter4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " NOT like '%" + txbSearchValue4.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorBoolean4.Text == "equals")
                                    {
                                        sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades4.Text.Trim() == "1") || (ddlGrades4.Text.Trim() == "2") || (ddlGrades4.Text.Trim() == "3") || (ddlGrades4.Text.Trim() == "4") || (ddlGrades4.Text.Trim() == "5") || (ddlGrades4.Text.Trim() == "6") || (ddlGrades4.Text.Trim() == "7") || (ddlGrades4.Text.Trim() == "8") || (ddlGrades4.Text.Trim() == "9") || (ddlGrades4.Text.Trim() == "10") || (ddlGrades4.Text.Trim() == "11") || (ddlGrades4.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean4.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean4.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField4.Text == "Age")
                            {
                                if (ddlOperatorBoolean4.Text == "equals")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == ">")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == ">=")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == "<")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == "<=")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlOperatorBoolean4.Text == "contains")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                            }
                        }
                        else if (ddlChooseField4.Text == "CampDropOff" || ddlChooseField4.Text == "CampPickUp" || ddlChooseField4.Text == "CurrentRegistrationForm" || ddlChooseField4.Text == "Dance" || ddlChooseField4.Text == "HaveReceivedChrist" || ddlChooseField4.Text == "MailingListInclude" || ddlChooseField4.Text == "ParentalConsentForm" || ddlChooseField4.Text == "PermissionToTransport" || ddlChooseField4.Text == "PromotionalRelease" || ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlOperatorBoolean4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
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
                        else if (ddlChooseField4.Text == "GeneralInformation" || ddlChooseField4.Text == "SpiritualJourney" || ddlChooseField4.Text == "ReleaseWaiver" || ddlChooseField4.Text == "NewVolunteerTraining" || ddlChooseField4.Text == "BackgroundCheckPAID" || ddlChooseField4.Text == "VehichleInsurance" || ddlChooseField4.Text == "DMVCheck" || ddlChooseField4.Text == "PACriminalCheck" || ddlChooseField4.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " not like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if (ddlChooseField4.Text == "DMVCheckCodes" || ddlChooseField4.Text == "NationalCheckCodes" || ddlChooseField4.Text == "PACriminalCheckCodes" || ddlChooseField4.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text + "' ";
                        }
                        else if (ddlChooseField4.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " >= '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " <= '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator4.Text == "Choose Operator") && (ddlOperatorCharacter4.Text == "Choose Operator") && (ddlOperatorBoolean4.Text == "Choose Operator") && (ddlChooseDate4.Text != "Choose Operator"))
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator4.Text == "equals")
                                    {
                                        sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades4.Text.Trim() == "1") || (ddlGrades4.Text.Trim() == "2") || (ddlGrades4.Text.Trim() == "3") || (ddlGrades4.Text.Trim() == "4") || (ddlGrades4.Text.Trim() == "5") || (ddlGrades4.Text.Trim() == "6") || (ddlGrades4.Text.Trim() == "7") || (ddlGrades4.Text.Trim() == "8") || (ddlGrades4.Text.Trim() == "9") || (ddlGrades4.Text.Trim() == "10") || (ddlGrades4.Text.Trim() == "11") || (ddlGrades4.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator4.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator4.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades4.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField4.Text == "Age")
                            {
                                if (ddlChooseOperator4.Text == "equals")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) = " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == ">")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) > " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == ">=")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) >= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "<")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) < " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "<=")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) <= " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                                else if (ddlChooseOperator4.Text == "contains")
                                {
                                    //Perfect.
                                    if (sql.Contains("vd.Grade"))
                                    {
                                        sql = sql + rblNumber3.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber3.Text + " CONVERT(INT,vd.Age) like " + txbSearchValue4.Text.Trim() + " ";
                                    }
                                }
                            }
                        }
                        else if (ddlChooseField4.Text == "CampDropOff" || ddlChooseField4.Text == "CampPickUp" || ddlChooseField4.Text == "CurrentRegistrationForm" || ddlChooseField4.Text == "Dance" || ddlChooseField4.Text == "HaveReceivedChrist" || ddlChooseField4.Text == "MailingListInclude" || ddlChooseField4.Text == "ParentalConsentForm" || ddlChooseField4.Text == "PermissionToTransport" || ddlChooseField4.Text == "PromotionalRelease" || ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " NOT like '%" + SearchBool4 + "%' ";
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
                        else if (ddlChooseField4.Text == "GeneralInformation" || ddlChooseField4.Text == "SpiritualJourney" || ddlChooseField4.Text == "ReleaseWaiver" || ddlChooseField4.Text == "NewVolunteerTraining" || ddlChooseField4.Text == "BackgroundCheckPAID" || ddlChooseField4.Text == "VehichleInsurance" || ddlChooseField4.Text == "DMVCheck" || ddlChooseField4.Text == "PACriminalCheck" || ddlChooseField4.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " = " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " > " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " < " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " like '%" + SearchBool4 + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " <> " + SearchBool4 + " ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField4.Text + " not like '%" + SearchBool4 + "%' ";
                            }
                        }
                        else if (ddlChooseField4.Text == "DMVCheckCodes" || ddlChooseField4.Text == "NationalCheckCodes" || ddlChooseField4.Text == "PACriminalCheckCodes" || ddlChooseField4.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField4.Text + " = '" + ddlGrades4.Text + "' ";
                        }
                        else if (ddlChooseField4.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " >= '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " <= '" + ddlPickDateRangeYear4.Text + "-" + ddlPickDateRangeMonth4.Text + "-" + ddlPickDateRangeDay4.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator4.Text == "equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT equals")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " = '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == ">")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " > '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "<")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " < '" + txbSearchValue4.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator4.Text == "contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " like '%" + txbSearchValue4.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator4.Text == "NOT contains")
                            {
                                sql = sql + rblNumber3.Text + " vd." + ddlChooseField4.Text + " NOT like '%" + txbSearchValue4.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator5.Text == "equals")
                                    {
                                        sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades5.Text.Trim() == "1") || (ddlGrades5.Text.Trim() == "2") || (ddlGrades5.Text.Trim() == "3") || (ddlGrades5.Text.Trim() == "4") || (ddlGrades5.Text.Trim() == "5") || (ddlGrades5.Text.Trim() == "6") || (ddlGrades5.Text.Trim() == "7") || (ddlGrades5.Text.Trim() == "8") || (ddlGrades5.Text.Trim() == "9") || (ddlGrades5.Text.Trim() == "10") || (ddlGrades5.Text.Trim() == "11") || (ddlGrades5.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator5.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField5.Text == "Age")
                            {
                                if (ddlChooseOperator5.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) = " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) > " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) >= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) < " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) <= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) like " + txbSearchValue5.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField5.Text == "CampDropOff" || ddlChooseField5.Text == "CampPickUp" || ddlChooseField5.Text == "CurrentRegistrationForm" || ddlChooseField5.Text == "Dance" || ddlChooseField5.Text == "HaveReceivedChrist" || ddlChooseField5.Text == "MailingListInclude" || ddlChooseField5.Text == "ParentalConsentForm" || ddlChooseField5.Text == "PermissionToTransport" || ddlChooseField5.Text == "PromotionalRelease" || ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " NOT like '%" + SearchBool5 + "%' ";
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
                        else if (ddlChooseField5.Text == "GeneralInformation" || ddlChooseField5.Text == "SpiritualJourney" || ddlChooseField5.Text == "ReleaseWaiver" || ddlChooseField5.Text == "NewVolunteerTraining" || ddlChooseField5.Text == "BackgroundCheckPAID" || ddlChooseField5.Text == "VehichleInsurance" || ddlChooseField5.Text == "DMVCheck" || ddlChooseField5.Text == "PACriminalCheck" || ddlChooseField5.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " not like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if (ddlChooseField5.Text == "DMVCheckCodes" || ddlChooseField5.Text == "NationalCheckCodes" || ddlChooseField5.Text == "PACriminalCheckCodes" || ddlChooseField5.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text + "' ";
                        }
                        else if (ddlChooseField5.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " >= '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <= '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <> '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " NOT like '%" + txbSearchValue5.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorCharacter5.Text == "equals")
                                    {
                                        sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades5.Text.Trim() == "1") || (ddlGrades5.Text.Trim() == "2") || (ddlGrades5.Text.Trim() == "3") || (ddlGrades5.Text.Trim() == "4") || (ddlGrades5.Text.Trim() == "5") || (ddlGrades5.Text.Trim() == "6") || (ddlGrades5.Text.Trim() == "7") || (ddlGrades5.Text.Trim() == "8") || (ddlGrades5.Text.Trim() == "9") || (ddlGrades5.Text.Trim() == "10") || (ddlGrades5.Text.Trim() == "11") || (ddlGrades5.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorCharacter5.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorCharacter5.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField5.Text == "Age")
                            {
                                if (ddlOperatorCharacter5.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) = " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) > " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) >= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) < " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) <= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorCharacter5.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) like " + txbSearchValue5.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField5.Text == "CampDropOff" || ddlChooseField5.Text == "CampPickUp" || ddlChooseField5.Text == "CurrentRegistrationForm" || ddlChooseField5.Text == "Dance" || ddlChooseField5.Text == "HaveReceivedChrist" || ddlChooseField5.Text == "MailingListInclude" || ddlChooseField5.Text == "ParentalConsentForm" || ddlChooseField5.Text == "PermissionToTransport" || ddlChooseField5.Text == "PromotionalRelease" || ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorCharacter5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorCharacter5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorCharacter5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorCharacter5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " NOT like '%" + SearchBool5 + "%' ";
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
                        else if (ddlChooseField5.Text == "GeneralInformation" || ddlChooseField5.Text == "SpiritualJourney" || ddlChooseField5.Text == "ReleaseWaiver" || ddlChooseField5.Text == "NewVolunteerTraining" || ddlChooseField5.Text == "BackgroundCheckPAID" || ddlChooseField5.Text == "VehichleInsurance" || ddlChooseField5.Text == "DMVCheck" || ddlChooseField5.Text == "PACriminalCheck" || ddlChooseField5.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " not like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if (ddlChooseField5.Text == "DMVCheckCodes" || ddlChooseField5.Text == "NationalCheckCodes" || ddlChooseField5.Text == "PACriminalCheckCodes" || ddlChooseField5.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text + "' ";
                        }
                        else if (ddlChooseField5.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " >= '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <= '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorCharacter5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <> '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                            else if (ddlOperatorCharacter5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " NOT like '%" + txbSearchValue5.Text.Trim() + "%' ";
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlOperatorBoolean5.Text == "equals")
                                    {
                                        sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades5.Text.Trim() == "1") || (ddlGrades5.Text.Trim() == "2") || (ddlGrades5.Text.Trim() == "3") || (ddlGrades5.Text.Trim() == "4") || (ddlGrades5.Text.Trim() == "5") || (ddlGrades5.Text.Trim() == "6") || (ddlGrades5.Text.Trim() == "7") || (ddlGrades5.Text.Trim() == "8") || (ddlGrades5.Text.Trim() == "9") || (ddlGrades5.Text.Trim() == "10") || (ddlGrades5.Text.Trim() == "11") || (ddlGrades5.Text.Trim() == "12"))
                                {
                                    if (ddlOperatorBoolean5.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR11' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR11' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR11' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR11' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR11' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlOperatorBoolean5.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade NOT LIKE '%G%' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR11' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField5.Text == "Age")
                            {
                                if (ddlOperatorBoolean5.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) = " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) > " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) >= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) < " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) <= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlOperatorBoolean5.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) like " + txbSearchValue5.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField5.Text == "CampDropOff" || ddlChooseField5.Text == "CampPickUp" || ddlChooseField5.Text == "CurrentRegistrationForm" || ddlChooseField5.Text == "Dance" || ddlChooseField5.Text == "HaveReceivedChrist" || ddlChooseField5.Text == "MailingListInclude" || ddlChooseField5.Text == "ParentalConsentForm" || ddlChooseField5.Text == "PermissionToTransport" || ddlChooseField5.Text == "PromotionalRelease" || ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
                        {
                            if (ddlOperatorBoolean5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlOperatorBoolean5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
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
                        else if (ddlChooseField5.Text == "GeneralInformation" || ddlChooseField5.Text == "SpiritualJourney" || ddlChooseField5.Text == "ReleaseWaiver" || ddlChooseField5.Text == "NewVolunteerTraining" || ddlChooseField5.Text == "BackgroundCheckPAID" || ddlChooseField5.Text == "VehichleInsurance" || ddlChooseField5.Text == "DMVCheck" || ddlChooseField5.Text == "PACriminalCheck" || ddlChooseField5.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " not like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if (ddlChooseField5.Text == "DMVCheckCodes" || ddlChooseField5.Text == "NationalCheckCodes" || ddlChooseField5.Text == "PACriminalCheckCodes" || ddlChooseField5.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text + "' ";
                        }
                        else if (ddlChooseField5.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " >= '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <= '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlOperatorBoolean5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlOperatorBoolean5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                        }
                    }
                    else if ((ddlChooseOperator5.Text == "Choose Operator") && (ddlOperatorCharacter5.Text == "Choose Operator") && (ddlOperatorBoolean5.Text == "Choose Operator") && (ddlChooseDate5.Text != "Choose Operator"))
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
                                    sql = sql.Replace("CONVERT(INT,vd.Grade) as 'Grade'", "vd.Grade");
                                    group = group.Replace("CONVERT(INT,vd.Grade)", "vd.Grade");
                                    if (ddlChooseOperator5.Text == "equals")
                                    {
                                        sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text.Trim() + "' ";
                                    }
                                }
                                else if ((ddlGrades5.Text.Trim() == "1") || (ddlGrades5.Text.Trim() == "2") || (ddlGrades5.Text.Trim() == "3") || (ddlGrades5.Text.Trim() == "4") || (ddlGrades5.Text.Trim() == "5") || (ddlGrades5.Text.Trim() == "6") || (ddlGrades5.Text.Trim() == "7") || (ddlGrades5.Text.Trim() == "8") || (ddlGrades5.Text.Trim() == "9") || (ddlGrades5.Text.Trim() == "10") || (ddlGrades5.Text.Trim() == "11") || (ddlGrades5.Text.Trim() == "12"))
                                {
                                    if (ddlChooseOperator5.Text == "equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) = " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "NOT equals")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <> " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == ">")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) > " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == ">=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) >= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "<")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) < " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "<=")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) <= " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                    else if (ddlChooseOperator5.Text == "NOT contains")
                                    {
                                        //Perfect.
                                        sql = sql + rblNumber4.Text + " (vd.Grade <> 'K' AND vd.Grade <> 'SV' AND vd.Grade <> 'GR' AND vd.Grade <> 'G'  AND vd.Grade <> 'GR11' AND vd.Grade <> 'GR12' AND vd.Grade <> 'GR09' AND vd.Grade <> 'GR10' AND vd.Grade <> 'G11'  AND vd.Grade <> 'G12' AND vd.Grade <> 'PreK') AND CONVERT(INT,vd.Grade) NOT LIKE " + ddlGrades5.Text.Trim() + " ";
                                    }
                                }
                            }
                            else if (ddlChooseField5.Text == "Age")
                            {
                                if (ddlChooseOperator5.Text == "equals")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) = " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == ">")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) > " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == ">=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) >= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "<")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) < " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "<=")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) <= " + txbSearchValue5.Text.Trim() + " ";
                                }
                                else if (ddlChooseOperator5.Text == "contains")
                                {
                                    //Perfect.
                                    sql = sql + rblNumber4.Text + " CONVERT(INT,vd.Age) like " + txbSearchValue5.Text.Trim() + " ";
                                }
                            }
                        }
                        else if (ddlChooseField5.Text == "CampDropOff" || ddlChooseField5.Text == "CampPickUp" || ddlChooseField5.Text == "CurrentRegistrationForm" || ddlChooseField5.Text == "Dance" || ddlChooseField5.Text == "HaveReceivedChrist" || ddlChooseField5.Text == "MailingListInclude" || ddlChooseField5.Text == "ParentalConsentForm" || ddlChooseField5.Text == "PermissionToTransport" || ddlChooseField5.Text == "PromotionalRelease" || ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " NOT like '%" + SearchBool5 + "%' ";
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
                        else if (ddlChooseField5.Text == "GeneralInformation" || ddlChooseField5.Text == "SpiritualJourney" || ddlChooseField5.Text == "ReleaseWaiver" || ddlChooseField5.Text == "NewVolunteerTraining" || ddlChooseField5.Text == "BackgroundCheckPAID" || ddlChooseField5.Text == "VehichleInsurance" || ddlChooseField5.Text == "DMVCheck" || ddlChooseField5.Text == "PACriminalCheck" || ddlChooseField5.Text == "NationalCheck")
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " = " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " > " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " < " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " like '%" + SearchBool5 + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " <> " + SearchBool5 + " ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + "where vd." + ddlChooseField5.Text + " not like '%" + SearchBool5 + "%' ";
                            }
                        }
                        else if (ddlChooseField5.Text == "DMVCheckCodes" || ddlChooseField5.Text == "NationalCheckCodes" || ddlChooseField5.Text == "PACriminalCheckCodes" || ddlChooseField5.Text == "BackgroundCheckPAIDCode")
                        {
                            sql = sql + "where vd." + ddlChooseField5.Text + " = '" + ddlGrades5.Text + "' ";
                        }
                        else if (ddlChooseField5.Text.EndsWith("Date"))
                        {
                            if (ddlChooseDate5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate.Text == "AFTER to present")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "BEFORE")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or AFTER to present")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " >= '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                            else if (ddlChooseDate2.Text == "On or BEFORE")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <= '" + ddlPickDateRangeYear5.Text + "-" + ddlPickDateRangeMonth5.Text + "-" + ddlPickDateRangeDay5.Text + "' ";
                            }
                        }
                        else
                        {
                            if (ddlChooseOperator5.Text == "equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " = '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT equals")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " <> '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == ">")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " > '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "<")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " < '" + txbSearchValue5.Text.Trim() + "' ";
                            }
                            else if (ddlChooseOperator5.Text == "contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                            else if (ddlChooseOperator5.Text == "NOT contains")
                            {
                                sql = sql + rblNumber4.Text + " vd." + ddlChooseField5.Text + " NOT like '%" + txbSearchValue5.Text.Trim() + "%' ";
                            }
                        }
                    }
                }

                //DateRange..
                //if ((ddlPickDateRangeMonth1.Text != "Month") && (ddlPickDateRangeMonth2.Text != "Month"))
                //{
                //    sql = sql + "AND vd.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                //    sql = sql + "AND vd.Day <= '" + ddlPickDateRangeYear2.Text + "-" + ddlPickDateRangeMonth2.Text + "-" + ddlPickDateRangeDay2.Text + "' ";
                //}
                //else if (ddlPickDateRangeMonth1.Text != "Month")
                //{
                //    sql = sql + "AND vd.Day >= '" + ddlPickDateRangeYear1.Text + "-" + ddlPickDateRangeMonth1.Text + "-" + ddlPickDateRangeDay1.Text + "' ";
                //}

                //Right here. determine which group by fields if the view is custom...RCM..10/26/11.
                if (group != "")
                {
                    //Use the customer GroupBy fields..RCM...
                    sql = sql + group;
                }
                else
                {

                }

                //ADD an ORDER BY to the query..RCM..9/16/11.
                sql = sql + "order by vd.lastname + ',' + vd.firstname ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "UIF_PerformingArts.dbo.VolunteerDetails");

                //Clear the gridview..RCM.
//                gvAdvancedSearch.DataSource = null;
//                gvAdvancedSearch.DataBind();

                //Reload the gridview..RCM.
                //gvAdvancedSearch.DataSource = ds.Tables[0];
                //gvAdvancedSearch.DataBind();

                //-------------
                //gvAdvancedSearchResults.DataSource = cmd.ExecuteReader();
                //gvAdvancedSearchResults.DataBind();

                //-----------
                //Clear the gridview..RCM.
//                gvAddressView.DataSource = null;
//                gvAddressView.DataBind();

                //Clear the gridview..RCM.
                //gvAddressView.DataSource = null;
                //gvAddressView.DataBind();

                //Clear the gridview..RCM.
//                gvPersonalView.DataSource = null;
//                gvPersonalView.DataBind();

                //Clear the gridview..RCM.
                //gvProgramView.DataSource = null;
                //gvProgramView.DataBind();

                //Clear the gridview..RCM.
                //gvDiscipleshipMentorView.DataSource = null;
                //gvDiscipleshipMentorView.DataBind();

                //Clear the gridview..RCM.
//                gvViewAllInfo.DataSource = null;
//                gvViewAllInfo.DataBind();

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
                    //if (ddlAdvancedSearchView.Text == "Select a View (Optional)")
                    //{
                    //    //Reload the gridview..RCM.
                    //    gvAddressView.DataSource = ds.Tables[0];
                    //    gvAddressView.DataBind();
                    //}
                    //else if (ddlAdvancedSearchView.Text == "Address Info")
                    //{
                    //    //Reload the gridview..RCM.
                    //    gvAddressView.DataSource = ds.Tables[0];
                    //    gvAddressView.DataBind();
                    //}
                    //else if (ddlAdvancedSearchView.Text == "Personal Info")
                    //{
                    //    //Reload the gridview..RCM.
                    //    gvPersonalView.DataSource = ds.Tables[0];
                    //    gvPersonalView.DataBind();
                    //}
                    //else if (ddlAdvancedSearchView.Text == "Program Info")
                    //{
                    //    //Reload the gridview..RCM.
                    //    //gvProgramView.DataSource = ds.Tables[0];
                    //    //gvProgramView.DataBind();
                    //}
                    //else if (ddlAdvancedSearchView.Text == "DiscipleshipMentor Info")
                    //{
                    //    //Reload the gridview..RCM.
                    //    //gvDiscipleshipMentorView.DataSource = ds.Tables[0];
                    //    //gvDiscipleshipMentorView.DataBind();
                    //}
                    //else if (ddlAdvancedSearchView.Text == "All Available Info")
                    //{
                    //    //Reload the gridview..RCM.
                    //    gvViewAllInfo.DataSource = ds.Tables[0];
                    //    gvViewAllInfo.DataBind();
                    //}
                }
                cmbExcelExport.Enabled = true;
            }
            catch (Exception lkjl)
            {
                //lblErrorMessage.Visible = true;
                //lblErrorMessage.Text = lblErrorMessage.Text + " " + lkjl.Message.ToString() + " ";
            }
        }

        protected void ddlPickDateRangeMonth1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeDay1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeYear1_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber1.Enabled = true;
        }

        protected void ddlPickDateRangeMonth2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeDay2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber2.Enabled = true;
        }

        protected void ddlChooseField_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //Reset the Operators..RCM.4/2/12..
            ddlOperatorBoolean.Text = "Choose Operator";
            ddlOperatorCharacter.Text = "Choose Operator";
            ddlChooseOperator.Text = "Choose Operator";

            //Reset the SearchValues..RCM..4/2/12..
            ddlChooseDate.Text = "Choose Operator";
            ddlPickDateRangeYear1.Text = "Year";
            ddlPickDateRangeMonth1.Text = "Month";
            ddlPickDateRangeDay1.Text = "Day";
            ddlGrades.Text = "Select a value";
            ddlSearchValueBool.Text = "Select a value";
            txbSearchValue.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField.Text == "MSHSChoir") || (ddlChooseField.Text == "ChildrensChoir") || (ddlChooseField.Text == "PerformingArts") || (ddlChooseField.Text == "Shakes") || (ddlChooseField.Text == "Singers") || (ddlChooseField.Text == "OutreachBasketball") || (ddlChooseField.Text == "BasketballTEAMS") || (ddlChooseField.Text == "HSBasketballLg") || (ddlChooseField.Text == "MSBasketballLg") || (ddlChooseField.Text == "3on3Basketball") || (ddlChooseField.Text == "SoccerIntraMurals") || (ddlChooseField.Text == "SoccerTEAMS") || (ddlChooseField.Text == "Baseball") || (ddlChooseField.Text == "BibleStudy") || (ddlChooseField.Text == "MondayNights") || (ddlChooseField.Text == "SummerDayCamp") || (ddlChooseField.Text == "ImpactUrbanSchools") || (ddlChooseField.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlChooseDate.Visible = false;
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
                ddlChooseDate.Visible = false;
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
            else if (ddlChooseField.Text == "PACriminalCheckCodes" || ddlChooseField.Text == "NationalCheckCodes" || ddlChooseField.Text == "DMVCheckCodes" || ddlChooseField.Text == "BackgroundCheckCodes" || ddlChooseField.Text == "VehichleInsuranceCodes")
            {
                //Manage the search field..
                ddlChooseDate.Visible = false;
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
            else if (ddlChooseField.Text == "GeneralInformation" || ddlChooseField.Text == "SpiritualJourney" || ddlChooseField.Text == "ReleaseWaiver" || ddlChooseField.Text == "NewVolunteerTraining" || ddlChooseField.Text == "VehichleInsurance" || ddlChooseField.Text == "NationalCheck" || ddlChooseField.Text == "DMVCheck" || ddlChooseField.Text == "PACriminalCheck" || ddlChooseField.Text == "BackgroundCheckPAID")
            //Volunteer Queries..
            //|| ddlChooseField.Text == "RetreatConsentForm" || ddlChooseField.Text == "Soloist" || ddlChooseField.Text == "StaffVolunteer" || ddlChooseField.Text == "Student" || ddlChooseField.Text == "StudentChoirQuestionareForm" || ddlChooseField.Text == "DiscipleshipMentorProgram" || ddlChooseField.Text == "TextPhone" || ddlChooseField.Text == "BibleStudyParticipation" || ddlChooseField.Text == "MeetCCGF" || ddlChooseField.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlChooseDate.Visible = false;
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
            else if (ddlChooseField.Text.EndsWith("Date"))
            //else if (ddlChooseField.Text == "PACriminalCheckDate" || ddlChooseField.Text == "NationalCheckCodes" || ddlChooseField.Text == "DMVCheckCodes" || ddlChooseField.Text == "BackgroundCheckCodes" || ddlChooseField.Text == "VehichleInsuranceCodes")
            {
                ddlPickDateRangeDay1.Visible = true;
                ddlPickDateRangeYear1.Visible = true;
                ddlPickDateRangeMonth1.Visible = true;
                ddlPickDateRangeDay1.Enabled = false;
                ddlPickDateRangeYear1.Enabled = false;
                ddlPickDateRangeMonth1.Enabled = false;

                ddlChooseDate.Visible = true;
                ddlChooseDate.Enabled = true;

                ddlGrades.Visible = false;
                ddlSearchValueBool.Visible = false;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean.Visible = false;
                ddlOperatorBoolean.Enabled = false;
                ddlOperatorCharacter.Visible = false;
                ddlOperatorCharacter.Enabled = false;
                ddlChooseOperator.Visible = false;
                ddlChooseOperator.Enabled = false;
            }
            else if (ddlChooseField.Text == "CoreKid")
            {
                //Manage the search field..
                ddlChooseDate.Visible = false;
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
                ddlChooseDate.Visible = false;
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
                ddlChooseDate.Visible = false;
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
            ddlChooseDate2.Text = "Choose Operator";
            ddlPickDateRangeYear22.Text = "Year";
            ddlPickDateRangeMonth22.Text = "Month";
            ddlPickDateRangeDay22.Text = "Day";
            ddlGrades2.Text = "Select a value";
            ddlSearchValue2Bool.Text = "Select a value";
            txbSearchValue2.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField2.Text == "MSHSChoir") || (ddlChooseField2.Text == "ChildrensChoir") || (ddlChooseField2.Text == "PerformingArts") || (ddlChooseField2.Text == "Shakes") || (ddlChooseField2.Text == "Singers") || (ddlChooseField2.Text == "OutreachBasketball") || (ddlChooseField2.Text == "BasketballTEAMS") || (ddlChooseField2.Text == "HSBasketballLg") || (ddlChooseField2.Text == "MSBasketballLg") || (ddlChooseField2.Text == "3on3Basketball") || (ddlChooseField2.Text == "SoccerIntraMurals") || (ddlChooseField2.Text == "SoccerTEAMS") || (ddlChooseField2.Text == "Baseball") || (ddlChooseField2.Text == "BibleStudy") || (ddlChooseField2.Text == "MondayNights") || (ddlChooseField2.Text == "SummerDayCamp") || (ddlChooseField2.Text == "ImpactUrbanSchools") || (ddlChooseField2.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlChooseDate2.Visible = false;
                ddlGrades2.Visible = false;
                ddlSearchValue2Bool.Visible = true;
                ddlSearchValueBool.Enabled = false;
                txbSearchValue2.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean2.Visible = true;
                ddlOperatorBoolean2.Enabled = true;
                ddlOperatorCharacter2.Visible = false;
                ddlChooseOperator2.Visible = false;

                //Adding once a Program has been selected...
                //So the user can drill down into a Program.
                ddlChooseField2.Items.Add("Section");
                ddlChooseField3.Items.Add("Section");
                ddlChooseField4.Items.Add("Section");
                ddlChooseField5.Items.Add("Section");
            }
            else if ((ddlChooseField2.Text == "All Programs") || (ddlChooseField2.Text == "AthleticsDept") || (ddlChooseField2.Text == "PerformingArtsDept") || (ddlChooseField2.Text == "EducationDept"))
            {
                //Manage the search field..
                ddlChooseDate2.Visible = false;
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
            else if (ddlChooseField2.Text == "PACriminalCheckCodes" || ddlChooseField2.Text == "NationalCheckCodes" || ddlChooseField2.Text == "DMVCheckCodes" || ddlChooseField2.Text == "BackgroundCheckCodes" || ddlChooseField2.Text == "VehichleInsuranceCodes")
            {
                //Manage the search field..
                ddlChooseDate2.Visible = false;
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
            else if (ddlChooseField2.Text == "GeneralInformation" || ddlChooseField2.Text == "SpiritualJourney" || ddlChooseField2.Text == "ReleaseWaiver" || ddlChooseField2.Text == "NewVolunteerTraining" || ddlChooseField2.Text == "VehichleInsurance" || ddlChooseField2.Text == "NationalCheck" || ddlChooseField2.Text == "DMVCheck" || ddlChooseField2.Text == "PACriminalCheck" || ddlChooseField2.Text == "BackgroundCheckPAID")
            //Volunteer Queries..
            //|| ddlChooseField2.Text == "RetreatConsentForm" || ddlChooseField2.Text == "Soloist" || ddlChooseField2.Text == "StaffVolunteer" || ddlChooseField2.Text == "Student" || ddlChooseField2.Text == "StudentChoirQuestionareForm" || ddlChooseField2.Text == "DiscipleshipMentorProgram" || ddlChooseField2.Text == "TextPhone" || ddlChooseField2.Text == "BibleStudyParticipation" || ddlChooseField2.Text == "MeetCCGF" || ddlChooseField2.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlChooseDate2.Visible = false;
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
                ddlChooseDate2.Visible = false;
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
                ddlChooseDate2.Visible = false;
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
            else if (ddlChooseField2.Text.EndsWith("Date"))
            {
                ddlPickDateRangeDay22.Visible = true;
                ddlPickDateRangeYear22.Visible = true;
                ddlPickDateRangeMonth22.Visible = true;

                ddlChooseDate2.Visible = true;
                ddlChooseDate2.Enabled = true;

                ddlGrades2.Visible = false;
                ddlSearchValue2Bool.Visible = false;
                ddlSearchValue2Bool.Enabled = false;
                txbSearchValue2.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean2.Visible = false;
                ddlOperatorBoolean2.Enabled = false;
                ddlOperatorCharacter2.Visible = false;
                ddlOperatorCharacter2.Enabled = false;
                ddlChooseOperator2.Visible = false;
                ddlChooseOperator2.Enabled = false;
            }
            else
            {
                //Manage the search field..
                ddlChooseDate2.Visible = false;
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

        protected void ddlChooseField3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset the Operators..RCM.4/2/12..
            ddlOperatorBoolean3.Text = "Choose Operator";
            ddlOperatorCharacter3.Text = "Choose Operator";
            ddlChooseOperator3.Text = "Choose Operator";

            //Reset the SearchValues..RCM..4/2/12..
            ddlChooseDate3.Text = "Choose Operator";
            ddlPickDateRangeYear3.Text = "Year";
            ddlPickDateRangeMonth3.Text = "Month";
            ddlPickDateRangeDay3.Text = "Day";
            ddlGrades3.Text = "Select a value";
            ddlSearchValue3Bool.Text = "Select a value";
            txbSearchValue3.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField3.Text == "MSHSChoir") || (ddlChooseField3.Text == "ChildrensChoir") || (ddlChooseField3.Text == "PerformingArts") || (ddlChooseField3.Text == "Shakes") || (ddlChooseField3.Text == "Singers") || (ddlChooseField3.Text == "OutreachBasketball") || (ddlChooseField3.Text == "BasketballTEAMS") || (ddlChooseField3.Text == "HSBasketballLg") || (ddlChooseField3.Text == "MSBasketballLg") || (ddlChooseField3.Text == "3on3Basketball") || (ddlChooseField3.Text == "SoccerIntraMurals") || (ddlChooseField3.Text == "SoccerTEAMS") || (ddlChooseField3.Text == "Baseball") || (ddlChooseField3.Text == "BibleStudy") || (ddlChooseField3.Text == "MondayNights") || (ddlChooseField3.Text == "SummerDayCamp") || (ddlChooseField3.Text == "ImpactUrbanSchools") || (ddlChooseField3.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlChooseDate3.Visible = false;
                ddlGrades3.Visible = false;
                ddlSearchValue3Bool.Visible = true;
                ddlSearchValue3Bool.Enabled = false;
                txbSearchValue3.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean3.Visible = true;
                ddlOperatorBoolean3.Enabled = true;
                ddlOperatorCharacter3.Visible = false;
                ddlChooseOperator3.Visible = false;

                //Adding once a Program has been selected...
                //So the user can drill down into a Program.
                ddlChooseField3.Items.Add("Section");
                ddlChooseField3.Items.Add("Section");
                ddlChooseField4.Items.Add("Section");
                ddlChooseField5.Items.Add("Section");
            }
            else if ((ddlChooseField3.Text == "All Programs") || (ddlChooseField3.Text == "AthleticsDept") || (ddlChooseField3.Text == "PerformingArtsDept") || (ddlChooseField3.Text == "EducationDept"))
            {
                //Manage the search field..
                ddlChooseDate3.Visible = false;
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
            else if (ddlChooseField3.Text == "PACriminalCheckCodes" || ddlChooseField3.Text == "NationalCheckCodes" || ddlChooseField3.Text == "DMVCheckCodes" || ddlChooseField3.Text == "BackgroundCheckCodes" || ddlChooseField3.Text == "VehichleInsuranceCodes")
            {
                //Manage the search field..
                ddlChooseDate3.Visible = false;
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
            else if (ddlChooseField3.Text == "GeneralInformation" || ddlChooseField3.Text == "SpiritualJourney" || ddlChooseField3.Text == "ReleaseWaiver" || ddlChooseField3.Text == "NewVolunteerTraining" || ddlChooseField3.Text == "VehichleInsurance" || ddlChooseField3.Text == "NationalCheck" || ddlChooseField3.Text == "DMVCheck" || ddlChooseField3.Text == "PACriminalCheck" || ddlChooseField3.Text == "BackgroundCheckPAID")
            //Volunteer Queries..
            //|| ddlChooseField3.Text == "RetreatConsentForm" || ddlChooseField3.Text == "Soloist" || ddlChooseField3.Text == "StaffVolunteer" || ddlChooseField3.Text == "Student" || ddlChooseField3.Text == "StudentChoirQuestionareForm" || ddlChooseField3.Text == "DiscipleshipMentorProgram" || ddlChooseField3.Text == "TextPhone" || ddlChooseField3.Text == "BibleStudyParticipation" || ddlChooseField3.Text == "MeetCCGF" || ddlChooseField3.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlChooseDate3.Visible = false;
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
                ddlChooseDate3.Visible = false;
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
                ddlChooseDate3.Visible = false;
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
            else if (ddlChooseField3.Text.EndsWith("Date"))
            {
                ddlPickDateRangeDay3.Visible = true;
                ddlPickDateRangeYear3.Visible = true;
                ddlPickDateRangeMonth3.Visible = true;

                ddlChooseDate3.Visible = true;
                ddlChooseDate3.Enabled = true;

                ddlGrades3.Visible = false;
                ddlSearchValue3Bool.Visible = false;
                ddlSearchValue3Bool.Enabled = false;
                txbSearchValue3.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean3.Visible = false;
                ddlOperatorBoolean3.Enabled = false;
                ddlOperatorCharacter3.Visible = false;
                ddlOperatorCharacter3.Enabled = false;
                ddlChooseOperator3.Visible = false;
                ddlChooseOperator3.Enabled = false;
            }
            else
            {
                //Manage the search field..
                ddlChooseDate3.Visible = false;
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
            ddlChooseDate4.Text = "Choose Operator";
            ddlPickDateRangeYear4.Text = "Year";
            ddlPickDateRangeMonth4.Text = "Month";
            ddlPickDateRangeDay4.Text = "Day";
            ddlGrades4.Text = "Select a value";
            ddlSearchValue4Bool.Text = "Select a value";
            txbSearchValue4.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField4.Text == "MSHSChoir") || (ddlChooseField4.Text == "ChildrensChoir") || (ddlChooseField4.Text == "PerformingArts") || (ddlChooseField4.Text == "Shakes") || (ddlChooseField4.Text == "Singers") || (ddlChooseField4.Text == "OutreachBasketball") || (ddlChooseField4.Text == "BasketballTEAMS") || (ddlChooseField4.Text == "HSBasketballLg") || (ddlChooseField4.Text == "MSBasketballLg") || (ddlChooseField4.Text == "4on4Basketball") || (ddlChooseField4.Text == "SoccerIntraMurals") || (ddlChooseField4.Text == "SoccerTEAMS") || (ddlChooseField4.Text == "Baseball") || (ddlChooseField4.Text == "BibleStudy") || (ddlChooseField4.Text == "MondayNights") || (ddlChooseField4.Text == "SummerDayCamp") || (ddlChooseField4.Text == "ImpactUrbanSchools") || (ddlChooseField4.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlChooseDate4.Visible = false;
                ddlGrades4.Visible = false;
                ddlSearchValue4Bool.Visible = true;
                ddlSearchValue4Bool.Enabled = false;
                txbSearchValue4.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean4.Visible = true;
                ddlOperatorBoolean4.Enabled = true;
                ddlOperatorCharacter4.Visible = false;
                ddlChooseOperator4.Visible = false;

                //Adding once a Program has been selected...
                //So the user can drill down into a Program.
                ddlChooseField4.Items.Add("Section");
                ddlChooseField4.Items.Add("Section");
                ddlChooseField4.Items.Add("Section");
                ddlChooseField5.Items.Add("Section");
            }
            else if ((ddlChooseField4.Text == "All Programs") || (ddlChooseField4.Text == "AthleticsDept") || (ddlChooseField4.Text == "PerformingArtsDept") || (ddlChooseField4.Text == "EducationDept"))
            {
                //Manage the search field..
                ddlChooseDate4.Visible = false;
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
            else if (ddlChooseField4.Text == "PACriminalCheckCodes" || ddlChooseField4.Text == "NationalCheckCodes" || ddlChooseField4.Text == "DMVCheckCodes" || ddlChooseField4.Text == "BackgroundCheckCodes" || ddlChooseField4.Text == "VehichleInsuranceCodes")
            {
                //Manage the search field..
                ddlChooseDate4.Visible = false;
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
            else if (ddlChooseField4.Text == "GeneralInformation" || ddlChooseField4.Text == "SpiritualJourney" || ddlChooseField4.Text == "ReleaseWaiver" || ddlChooseField4.Text == "NewVolunteerTraining" || ddlChooseField4.Text == "VehichleInsurance" || ddlChooseField4.Text == "NationalCheck" || ddlChooseField4.Text == "DMVCheck" || ddlChooseField4.Text == "PACriminalCheck" || ddlChooseField4.Text == "BackgroundCheckPAID")
            //Volunteer Queries..
            //|| ddlChooseField4.Text == "RetreatConsentForm" || ddlChooseField4.Text == "Soloist" || ddlChooseField4.Text == "StaffVolunteer" || ddlChooseField4.Text == "Student" || ddlChooseField4.Text == "StudentChoirQuestionareForm" || ddlChooseField4.Text == "DiscipleshipMentorProgram" || ddlChooseField4.Text == "TextPhone" || ddlChooseField4.Text == "BibleStudyParticipation" || ddlChooseField4.Text == "MeetCCGF" || ddlChooseField4.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlChooseDate4.Visible = false;
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
                ddlChooseDate4.Visible = false;
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
                ddlChooseDate4.Visible = false;
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
            else if (ddlChooseField4.Text.EndsWith("Date"))
            {
                ddlPickDateRangeDay4.Visible = true;
                ddlPickDateRangeYear4.Visible = true;
                ddlPickDateRangeMonth4.Visible = true;

                ddlChooseDate4.Visible = true;
                ddlChooseDate4.Enabled = true;

                ddlGrades4.Visible = false;
                ddlSearchValue4Bool.Visible = false;
                ddlSearchValue4Bool.Enabled = false;
                txbSearchValue4.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean4.Visible = false;
                ddlOperatorBoolean4.Enabled = false;
                ddlOperatorCharacter4.Visible = false;
                ddlOperatorCharacter4.Enabled = false;
                ddlChooseOperator4.Visible = false;
                ddlChooseOperator4.Enabled = false;
            }
            else
            {
                //Manage the search field..
                ddlChooseDate4.Visible = false;
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
            //Reset the Operators..RCM.5/2/12..
            ddlOperatorBoolean5.Text = "Choose Operator";
            ddlOperatorCharacter5.Text = "Choose Operator";
            ddlChooseOperator5.Text = "Choose Operator";

            //Reset the SearchValues..RCM..5/2/12..
            ddlChooseDate5.Text = "Choose Operator";
            ddlPickDateRangeYear5.Text = "Year";
            ddlPickDateRangeMonth5.Text = "Month";
            ddlPickDateRangeDay5.Text = "Day";
            ddlGrades5.Text = "Select a value";
            ddlSearchValue5Bool.Text = "Select a value";
            txbSearchValue5.Text = "";

            //Help with the boolean fields..
            if ((ddlChooseField5.Text == "MSHSChoir") || (ddlChooseField5.Text == "ChildrensChoir") || (ddlChooseField5.Text == "PerformingArts") || (ddlChooseField5.Text == "Shakes") || (ddlChooseField5.Text == "Singers") || (ddlChooseField5.Text == "OutreachBasketball") || (ddlChooseField5.Text == "BasketballTEAMS") || (ddlChooseField5.Text == "HSBasketballLg") || (ddlChooseField5.Text == "MSBasketballLg") || (ddlChooseField5.Text == "5on5Basketball") || (ddlChooseField5.Text == "SoccerIntraMurals") || (ddlChooseField5.Text == "SoccerTEAMS") || (ddlChooseField5.Text == "Baseball") || (ddlChooseField5.Text == "BibleStudy") || (ddlChooseField5.Text == "MondayNights") || (ddlChooseField5.Text == "SummerDayCamp") || (ddlChooseField5.Text == "ImpactUrbanSchools") || (ddlChooseField5.Text == "AcademicReadingSupport"))
            {
                //Manage the search field..
                ddlChooseDate5.Visible = false;
                ddlGrades5.Visible = false;
                ddlSearchValue5Bool.Visible = true;
                ddlSearchValue5Bool.Enabled = false;
                txbSearchValue5.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean5.Visible = true;
                ddlOperatorBoolean5.Enabled = true;
                ddlOperatorCharacter5.Visible = false;
                ddlChooseOperator5.Visible = false;

                //Adding once a Program has been selected...
                //So the user can drill down into a Program.
                ddlChooseField5.Items.Add("Section");
                ddlChooseField5.Items.Add("Section");
                ddlChooseField5.Items.Add("Section");
                ddlChooseField5.Items.Add("Section");
            }
            else if ((ddlChooseField5.Text == "All Programs") || (ddlChooseField5.Text == "AthleticsDept") || (ddlChooseField5.Text == "PerformingArtsDept") || (ddlChooseField5.Text == "EducationDept"))
            {
                //Manage the search field..
                ddlChooseDate5.Visible = false;
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
            else if (ddlChooseField5.Text == "PACriminalCheckCodes" || ddlChooseField5.Text == "NationalCheckCodes" || ddlChooseField5.Text == "DMVCheckCodes" || ddlChooseField5.Text == "BackgroundCheckCodes" || ddlChooseField5.Text == "VehichleInsuranceCodes")
            {
                //Manage the search field..
                ddlChooseDate5.Visible = false;
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
            else if (ddlChooseField5.Text == "GeneralInformation" || ddlChooseField5.Text == "SpiritualJourney" || ddlChooseField5.Text == "ReleaseWaiver" || ddlChooseField5.Text == "NewVolunteerTraining" || ddlChooseField5.Text == "VehichleInsurance" || ddlChooseField5.Text == "NationalCheck" || ddlChooseField5.Text == "DMVCheck" || ddlChooseField5.Text == "PACriminalCheck" || ddlChooseField5.Text == "BackgroundCheckPAID")
            //Volunteer Queries..
            //|| ddlChooseField5.Text == "RetreatConsentForm" || ddlChooseField5.Text == "Soloist" || ddlChooseField5.Text == "StaffVolunteer" || ddlChooseField5.Text == "Student" || ddlChooseField5.Text == "StudentChoirQuestionareForm" || ddlChooseField5.Text == "DiscipleshipMentorProgram" || ddlChooseField5.Text == "TextPhone" || ddlChooseField5.Text == "BibleStudyParticipation" || ddlChooseField5.Text == "MeetCCGF" || ddlChooseField5.Text == "StudentVolunteer")
            {
                //Manage the search field..
                ddlChooseDate5.Visible = false;
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
                ddlChooseDate5.Visible = false;
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
                ddlChooseDate5.Visible = false;
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
            else if (ddlChooseField5.Text.EndsWith("Date"))
            {
                ddlPickDateRangeDay5.Visible = true;
                ddlPickDateRangeYear5.Visible = true;
                ddlPickDateRangeMonth5.Visible = true;

                ddlChooseDate5.Visible = true;
                ddlChooseDate5.Enabled = true;

                ddlGrades5.Visible = false;
                ddlSearchValue5Bool.Visible = false;
                ddlSearchValue5Bool.Enabled = false;
                txbSearchValue5.Visible = false;

                //Manage the operators...RCM..
                ddlOperatorBoolean5.Visible = false;
                ddlOperatorBoolean5.Enabled = false;
                ddlOperatorCharacter5.Visible = false;
                ddlOperatorCharacter5.Enabled = false;
                ddlChooseOperator5.Visible = false;
                ddlChooseOperator5.Enabled = false;
            }
            else
            {
                //Manage the search field..
                ddlChooseDate5.Visible = false;
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

        protected void ddlGrades3_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber3.Enabled = true;
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
            //else if (ddlChooseField.Text.EndsWith("Date"))
            //{
            //    ddlPickDateRangeYear1.Enabled = true;
            //    ddlPickDateRangeMonth1.Enabled = true;
            //    ddlPickDateRangeDay1.Enabled = true;
            //}
            else
            {
                ddlSearchValueBool.Visible = false;
                ddlGrades.Visible = true;
                ddlGrades.Enabled = true;
                txbSearchValue.Visible = false;
            }
        }

        protected void txbSearchValue_TextChanged(object sender, EventArgs e)
        {

        }

        protected void rblNumber1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChooseField2.Enabled = true;
            //ddlChooseOperator2.Enabled = true;
        }

        protected void ddlGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber1.Enabled = true;
        }

        protected void ddlGrades4_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber4.Enabled = true;
        }

        protected void ddlGrades5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOperatorBoolean_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (ddlChooseField.Text.EndsWith("Date"))
            //{
            //    ddlPickDateRangeYear1.Enabled = true;
            //    ddlPickDateRangeMonth1.Enabled = true;
            //    ddlPickDateRangeDay1.Enabled = true;
            //}
            //else
            //{
                ddlSearchValueBool.Enabled = true;
                ddlSearchValueBool.Visible = true;
                txbSearchValue.Visible = false;
            //}
        }

        protected void ddlOperatorBoolean2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue2Bool.Enabled = true;
            ddlSearchValue2Bool.Visible = true;
            txbSearchValue2.Visible = false;
        }

        protected void ddlOperatorBoolean3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue3Bool.Enabled = true;
            ddlSearchValue3Bool.Visible = true;
            txbSearchValue3.Visible = false;
        }

        protected void ddlOperatorBoolean4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue4Bool.Enabled = true;
            ddlSearchValue4Bool.Visible = true;
            txbSearchValue4.Visible = false;
        }

        protected void ddlOperatorBoolean5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue5Bool.Enabled = true;
            ddlSearchValue5Bool.Visible = true;
            txbSearchValue5.Visible = false;
        }

        protected void ddlOperatorCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlChooseField.Text.EndsWith("Date"))
            //{
            //    ddlPickDateRangeYear1.Enabled = true;
            //    ddlPickDateRangeMonth1.Enabled = true;
            //    ddlPickDateRangeDay1.Enabled = true;
            //}
            //else
            //{
                ddlSearchValueBool.Enabled = false;
                ddlSearchValueBool.Visible = false;
                txbSearchValue.Visible = true;

                rblNumber1.Enabled = true;
//            }
        }

        protected void ddlOperatorCharacter4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue4Bool.Enabled = false;
            ddlSearchValue4Bool.Visible = false;
            txbSearchValue4.Visible = true;

            rblNumber4.Enabled = true;
        }

        protected void ddlOperatorCharacter5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSearchValue5Bool.Enabled = false;
            ddlSearchValue5Bool.Visible = false;
            txbSearchValue5.Visible = true;
        }

        protected void ddlGrades2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber2.Enabled = true;
        }

        protected void ddlSearchValue2Bool_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber2.Enabled = true;
        }

        protected void txbSearchValue2_TextChanged(object sender, EventArgs e)
        {
            rblNumber2.Enabled = true;
        }

        protected void txbSearchValue3_TextChanged(object sender, EventArgs e)
        {
            rblNumber3.Enabled = true;
        }

        protected void txbSearchValue4_TextChanged(object sender, EventArgs e)
        {
            rblNumber4.Enabled = true;
        }

        protected void txbSearchValue5_TextChanged(object sender, EventArgs e)
        {

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

        protected void gvCustomView_RowCommand(object sender, EventArgs e)
        {

        }

        protected void gvAddressView_RowCommand(object sender, EventArgs e)
        {

        }

        protected void ddlAdvancedSearchView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlChooseDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPickDateRangeDay1.Enabled = true;
            ddlPickDateRangeMonth1.Enabled = true;
            ddlPickDateRangeYear1.Enabled = true;
        }

        protected void ddlChooseDate3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPickDateRangeDay3.Enabled = true;
            ddlPickDateRangeMonth3.Enabled = true;
            ddlPickDateRangeYear3.Enabled = true;

        }

        protected void ddlChooseDate2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPickDateRangeDay22.Enabled = true;
            ddlPickDateRangeMonth22.Enabled = true;
            ddlPickDateRangeYear22.Enabled = true;

        }

        protected void ddlChooseDate4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPickDateRangeDay4.Enabled = true;
            ddlPickDateRangeMonth4.Enabled = true;
            ddlPickDateRangeYear4.Enabled = true;

        }

        protected void ddlChooseDate5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPickDateRangeDay5.Enabled = true;
            ddlPickDateRangeMonth5.Enabled = true;
            ddlPickDateRangeYear5.Enabled = true;
        }

        protected void ddlPickDateRangeMonth22_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeDay22_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeYear22_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber2.Enabled = true;
        }

        protected void ddlPickDateRangeMonth3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeDay3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeYear3_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber3.Enabled = true;

        }

        protected void ddlPickDateRangeMonth4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeDay4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeYear4_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblNumber4.Enabled = true;
        }

        protected void ddlPickDateRangeMonth5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeDay5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPickDateRangeYear5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}