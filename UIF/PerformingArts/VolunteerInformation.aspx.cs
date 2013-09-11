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
using System.Resources;
using System.Net;
using System.Data.Sql;
using System.Web.Script.Services;

namespace UIF.PerformingArts
{
	/// <summary>
	/// Summary description for VolunteerInformation.
    /// Ryan C Manners 4/5/11.
    /// Building this page to update information in the database for students..
	/// </summary>
	public partial class VolunteerInformation : System.Web.UI.Page
	{
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public static string Department = "";
		
        protected void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
		    SqlDataReader reader = null;
            SqlDataReader reader2 = null;

            if (!Page.IsPostBack)
            {
                //Populate the Department Query string...RCM..6/28/11
                Department = Request.QueryString["Dept"];

                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);
                
                //chbNewStudentFlag.Checked = false;
                if (Request.QueryString["security"] == "Good")
                {
                    if (Request.QueryString["newvolunteer"] == "newvolunteer")
                    {
                        chbNewVolunteerFlag.Checked = true;
                        StartingSettings();
                        NewStudent();
                    }
                    else
                    {
                        try
                        {
                            string sql = "select vi.LastName, vi.FirstName, vi.address, vi.HomePhone, vi.cellphone, vi.TextPhone, vi.Email, vi.age, "
                                       + "vi.dob, vi.sex, vi.church, vi.careergoal, vi.healthconditions, vi.notes, vi.tshirtsize, "
                                       + "vi.BibleStudyParticipation,vi.PictureIdentification, vi.HaveReceivedChrist,vi.WhenReceivedChrist, vi.CurrentPaperwork, "
                                       + "vi.SysCreate, vi.sysupdate, vi.LastUpdatedBy, vi.city, vi.state, vi.zip, vd.BackgroundCheck, vd.SpiritualJourney, vd.VehichleInsurance, vd.ReleaseWaiver, vd.GeneralInformation, vd.NewVolunteerTraining, "
                                       + "vi.discipleshipmentorparticipation, vi.discipleshipmentortraining, vi.discipleshipmentortraineddate, vi.discipleshipmentorstartdate, "
                                       + "pl.mshschoir, pl.childrenschoir, pl.performingarts, pl.shakes, pl.singers, pl.outreachbasketball, pl.outreachbasketball, pl.outreachbasketball, pl.mondaynights "
                                       + ", pl.[3on3basketball], pl.basketballTEAMS, pl.soccerintramurals, pl.SoccerTEAMS, pl.Baseball, pl.biblestudy, pl.hsbasketballlg, pl.msbasketballlg, pl.outreachbasketball, pl.outreachbasketball "
                                       + ", vi.discipleshipmentornotes, vi.discipleshipmentorwaitinglist, vi.backgroundcheckdate, vi.mailinglistinclude, vi.mailinglistcodes, vi.discipleshipmentorpotentials, vi.officevolunteer, pl.summerdaycamp "
                                       + ", vi.EmergencyContact, vi.Emergencyrelationship, vi.emergencycontactphone, pl.SpecialEvents, vi.staff, vi.MostRecentSeason, vi.MostRecentSeasonYear, pl.impacturbanschools, pl.academicreadingsupport "
                                       + "FROM UIF_PerformingArts.dbo.VolunteerInformation vi "
                                       + "LEFT OUTER JOIN UIF_PerformingArts.dbo.programslist pl "
                                       + "ON (vi.LastName = pl.LastName AND vi.FirstName = pl.FirstName) "
                                       + "LEFT OUTER JOIN UIF_PerformingArts.dbo.volunteerdetails vd "
                                       + "ON (vi.LastName = vd.LastName AND vi.FirstName = vd.FirstName) "
                                       + "WHERE vi.lastname='" + Request.QueryString["VolunteerLastName"] + "' "
                                       + "AND vi.firstname='" + Request.QueryString["VolunteerFirstName"] + "' "
                                       + "AND pl.staffvolunteer = 1 ";
                                       

                            //Perform database lookup based on the chosen child..RCM..
                            SqlCommand cmd = new SqlCommand(sql);
                            //cmd.Parameters.Add(new SqlParameter("@lastname", Request.QueryString["VolunteerLastName"]));
                            //cmd.Parameters.Add(new SqlParameter("@firstname", Request.QueryString["VolunteerFirstName"]));

                            StartingSettings();

                            con.Open();//Opens the db connection.
                            cmd.Connection = con;
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {	//Retrieve the first record only
                                if (reader.IsDBNull(0))
                                {
                                    txtLastName.Text = "N/A";
                                }
                                else
                                {
                                    txtLastName.Text = reader.GetString(0);
                                }
                                if (reader.IsDBNull(1))
                                {
                                    txtFirstName.Text = "N/A";
                                }
                                else
                                {
                                    txtFirstName.Text = reader.GetString(1);
                                }
                                if (reader.IsDBNull(2))
                                {
                                    txtAddress1.Text = "N/A";
                                }
                                else
                                {
                                    txtAddress1.Text = reader.GetString(2);
                                }
                                if (reader.IsDBNull(3))
                                {
                                    txtHomePhone.Text = "N/A";
                                }
                                else
                                {
                                    txtHomePhone.Text = reader.GetString(3);
                                }
                                if (reader.IsDBNull(4))
                                {
                                    txtStudentCellPhone.Text = "NULL";
                                }
                                else
                                {
                                    txtStudentCellPhone.Text = reader.GetSqlValue(4).ToString();
                                }
                                //5 textphone
                                if (reader.IsDBNull(6))
                                {
                                    txtStudentEmail.Text = "N/A";
                                }
                                else
                                {
                                    txtStudentEmail.Text = reader.GetSqlValue(6).ToString();
                                }
                                if (reader.IsDBNull(7))
                                {
                                }
                                else
                                {
                                    DateTime d1 = DateTime.Now;
                                    DateTime d2 = new DateTime(System.Convert.ToInt32(reader.GetSqlValue(8).ToString().Substring(6, 4)), System.Convert.ToInt32(reader.GetSqlValue(8).ToString().Substring(0, 2)), System.Convert.ToInt32(reader.GetSqlValue(8).ToString().Substring(3, 2)));
                                    TimeSpan difference = d1.Subtract(d2);

                                    ddlAge.Text = (difference.Days / 365).ToString();
                                    ddlAge.Enabled = false;
                                }
                                if (reader.IsDBNull(8))
                                {
                                    ddlMonthBirth.Text = "00";
                                    ddlDayBirth.Text = "00";
                                    ddlYearBirth.Text = "0000";
                                }
                                else
                                {
                                    ddlMonthBirth.Text = reader.GetSqlValue(8).ToString().Substring(0, 2);
                                    ddlDayBirth.Text = reader.GetSqlValue(8).ToString().Substring(3, 2);
                                    ddlYearBirth.Text = reader.GetSqlValue(8).ToString().Substring(6, 4);
                                }
                                if (reader.IsDBNull(9))
                                {
                                }
                                else
                                {
                                    ddlGender.Text = reader.GetString(9);
                                }
                                if (reader.IsDBNull(10))
                                {
                                    //txtChurch.Text = "N/A";
                                    ddlChurch.Text = "Attend Church?";
                                }
                                else
                                {
                                    ddlChurch.Text = reader.GetString(10);
                                    //txtChurch.Text = reader.GetString(10);
                                }
                                if (reader.IsDBNull(11))
                                {
                                    //txtCareerGoal.Text = "N/A";
                                }
                                else
                                {
                                    //txtCareerGoal.Text = reader.GetString(11);
                                }
                                if (reader.IsDBNull(12))
                                {
                                    txbHealthConditions.Text = "N/A";
                                }
                                else
                                {
                                    txbHealthConditions.Text = reader.GetString(12);
                                }
                                if (reader.IsDBNull(13))
                                {
                                    txbNotes.Text = "N/A";
                                }
                                else
                                {
                                    txbNotes.Text = reader.GetString(13);
                                }
                                if (reader.IsDBNull(14))
                                {
                                }
                                else
                                {
                                    ddlTShirtSize.Text = reader.GetString(14);
                                }
                                //if (reader.IsDBNull(15))
                                //{
                                //    txbDiscipleshipMentor.Text = "N/A";
                                //}
                                //else
                                //{
                                //    txbDiscipleshipMentor.Text = reader.GetString(15);
                                //}
                                //16 biblestudyparticipation.
                                if (reader.IsDBNull(16))
                                {
                                    imgPicture.ImageUrl = "No picture Available";
                                }
                                else
                                {
                                    imgPicture.ImageUrl = reader.GetString(16);
                                }
                                if (reader.IsDBNull(22))
                                {
                                    lblLastUpdatedBy.Text = "N/A";
                                }
                                else
                                {
                                    lblLastUpdatedBy.Text = "LastUpdatedBy: " + reader.GetString(22) + " On: " + reader.GetSqlValue(21).ToString();
                                }
                                if (reader.IsDBNull(23))
                                {
                                    txtCity.Text = "N/A";
                                }
                                else
                                {
                                    txtCity.Text = reader.GetString(23);
                                }
                                //txtCity.Text = "Pittsburgh";//#24.
                                txtState.Text = "PA";//#25.
                                if (reader.IsDBNull(25))
                                {
                                    txtZip.Text = "N/A";
                                }
                                else
                                {
                                    txtZip.Text = reader.GetString(25);
                                }
                                if (reader.IsDBNull(26))
                                {
                                    chbBackgroundCheck.Checked = false;
                                }
                                else
                                {
                                    chbBackgroundCheck.Checked = reader.GetBoolean(26);
                                    if (chbBackgroundCheck.Checked)
                                    {
                                        ddlBackgroundDay.Enabled = true;
                                        ddlBackgroundMonth.Enabled = true;
                                        ddlBackgroundYear.Enabled = true;
                                    }
                                    else
                                    {
                                        ddlBackgroundDay.Enabled = false;
                                        ddlBackgroundMonth.Enabled = false;
                                        ddlBackgroundYear.Enabled = false;
                                    }
                                }
                                chbSpiritualJourney.Checked = reader.GetBoolean(27);
                                chbVehichleInsurance.Checked = reader.GetBoolean(28);
                                chbReleaseWaiver.Checked = reader.GetBoolean(29);
                                chbGeneralInformation.Checked = reader.GetBoolean(30);
                                chbNewVolunteerTraining.Checked = reader.GetBoolean(31);
                                chbDiscipleshipmentorTraining.Checked = reader.GetBoolean(32);
                                chbDiscipleshipmentorTrainingDone.Checked = reader.GetBoolean(33);
                                if (chbDiscipleshipmentorTraining.Checked)
                                {
                                    ddlDayBirth2.Enabled = true;
                                    ddlDayBirth3.Enabled = true;
                                    ddlMonthBirth2.Enabled = true;
                                    ddlMonthBirth3.Enabled = true;
                                    ddlYearBirth2.Enabled = true;
                                    ddlYearBirth3.Enabled = true;
                                }
                                else
                                {
                                    ddlDayBirth2.Enabled = false;
                                    ddlDayBirth3.Enabled = false;
                                    ddlMonthBirth2.Enabled = false;
                                    ddlMonthBirth3.Enabled = false;
                                    ddlYearBirth2.Enabled = false;
                                    ddlYearBirth3.Enabled = false;
                                }
                                if (reader.IsDBNull(34))
                                {
                                    ddlMonthBirth2.Text = "00";
                                    ddlDayBirth2.Text = "00";
                                    ddlYearBirth2.Text = "0000";
                                }
                                else
                                {
                                    ddlMonthBirth2.Text = reader.GetSqlValue(34).ToString().Substring(0, 2);
                                    ddlDayBirth2.Text = reader.GetSqlValue(34).ToString().Substring(3, 2);
                                    ddlYearBirth2.Text = reader.GetSqlValue(34).ToString().Substring(6, 4);
                                }
                                if (reader.IsDBNull(35))
                                {
                                    ddlMonthBirth3.Text = "00";
                                    ddlDayBirth3.Text = "00";
                                    ddlYearBirth3.Text = "0000";
                                }
                                else
                                {
                                    ddlMonthBirth3.Text = reader.GetSqlValue(35).ToString().Substring(0, 2);
                                    ddlDayBirth3.Text = reader.GetSqlValue(35).ToString().Substring(3, 2);
                                    ddlYearBirth3.Text = reader.GetSqlValue(35).ToString().Substring(6, 4);
                                }
                                if (reader.IsDBNull(36))
                                {
                                    chbMSHSChoir.Checked = false;
                                }
                                else
                                {
                                    chbMSHSChoir.Checked = reader.GetBoolean(36);
                                }
                                if (reader.IsDBNull(37))
                                {
                                    chbChildrensChoir.Checked = false;
                                }
                                else
                                {
                                    chbChildrensChoir.Checked = reader.GetBoolean(37);
                                }
                                if (reader.IsDBNull(38))
                                {
                                    chbPerformingArts.Checked = false;
                                }
                                else
                                {
                                    chbPerformingArts.Checked = reader.GetBoolean(38);
                                }
                                if (chbPerformingArts.Checked)
                                {
                                    lbPerforingArts.Enabled = true;
                                }
                                else
                                {
                                    lbPerforingArts.Enabled = false;
                                }
                                if (reader.IsDBNull(39))
                                {
                                    chbShakes.Checked = false;
                                }
                                else
                                {
                                    chbShakes.Checked = reader.GetBoolean(39);
                                }
                                if (reader.IsDBNull(40))
                                {
                                    chbSingers.Checked = false;
                                }
                                else
                                {
                                    chbSingers.Checked = reader.GetBoolean(40);
                                }
                                if (reader.IsDBNull(44))
                                {
                                    chbMondayNights.Checked = false;
                                }
                                else
                                {
                                    chbMondayNights.Checked = reader.GetBoolean(44);
                                }
                                //new stuff..
                                if (reader.IsDBNull(45))
                                {
                                    chb3on3Basketball.Checked = false;
                                }
                                else
                                {
                                    chb3on3Basketball.Checked = reader.GetBoolean(45);
                                }
                                if (reader.IsDBNull(46))
                                {
                                    chbBasketballTEAMS.Checked = false;
                                }
                                else
                                {
                                    chbBasketballTEAMS.Checked = reader.GetBoolean(46);
                                }
                                if (reader.IsDBNull(47))
                                {
                                    chbSoccerInterMurals.Checked = false;
                                }
                                else
                                {
                                    chbSoccerInterMurals.Checked = reader.GetBoolean(47);
                                }
                                if (reader.IsDBNull(48))
                                {
                                    chbSoccerLgTravel.Checked = false;
                                }
                                else
                                {
                                    chbSoccerLgTravel.Checked = reader.GetBoolean(48);
                                }
                                if (reader.IsDBNull(49))
                                {
                                    chbLittleLeagueBaseball.Checked = false;
                                }
                                else
                                {
                                    chbLittleLeagueBaseball.Checked = reader.GetBoolean(49);
                                }
                                if (reader.IsDBNull(50))
                                {
                                    chbOliverFootballBible.Checked = false;
                                }
                                else
                                {
                                    chbOliverFootballBible.Checked = reader.GetBoolean(50);
                                }
                                if (reader.IsDBNull(51))
                                {
                                    chbHSBasketLeague.Checked = false;
                                }
                                else
                                {
                                    chbHSBasketLeague.Checked = reader.GetBoolean(51);
                                }
                                if (reader.IsDBNull(52))
                                {
                                    chbMSBasketLeague.Checked = false;
                                }
                                else
                                {
                                    chbMSBasketLeague.Checked = reader.GetBoolean(52);
                                }
                                if (reader.IsDBNull(53))
                                {
                                    chbBoysOutreachBball.Checked = false;
                                }
                                else
                                {
                                    chbBoysOutreachBball.Checked = reader.GetBoolean(53);
                                }
                                if (reader.IsDBNull(54))
                                {
                                    chbGirlsOutreachBball.Checked = false;
                                }
                                else
                                {
                                    chbGirlsOutreachBball.Checked = reader.GetBoolean(54);
                                }
                                if (reader.IsDBNull(55))
                                {
                                    txbDiscipleshipMentorNotes.Text = "N/A";
                                }
                                else
                                {
                                    txbDiscipleshipMentorNotes.Text = reader.GetString(55);
                                }
                                if (reader.IsDBNull(56))
                                {
                                    chbDiscipleshipmentorWaitingList.Checked = false;
                                }
                                else
                                {
                                    chbDiscipleshipmentorWaitingList.Checked = reader.GetBoolean(56);
                                }
                                if (reader.IsDBNull(57))
                                {
                                    ddlBackgroundMonth.Text = "00";
                                    ddlBackgroundDay.Text = "00";
                                    ddlBackgroundYear.Text = "0000";
                                }
                                else
                                {
                                    if (chbBackgroundCheck.Checked)
                                    {
                                        ddlBackgroundMonth.Text = reader.GetSqlValue(57).ToString().Substring(0, 2);
                                        ddlBackgroundDay.Text = reader.GetSqlValue(57).ToString().Substring(3, 2);
                                        ddlBackgroundYear.Text = reader.GetSqlValue(57).ToString().Substring(6, 4);
                                        //ddlBackgroundMonth.Enabled = false;
                                        //ddlBackgroundDay.Enabled = false;
                                        //ddlBackgroundYear.Enabled = false;
                                    }
                                }
                                if (reader.IsDBNull(58))
                                {
                                    chbMailingList.Checked = false;
                                }
                                else
                                {
                                    chbMailingList.Checked = reader.GetBoolean(58);
                                }
                                if (reader.IsDBNull(59))
                                {
                                    ddlMailingListCodes.Text = "Use Current Address";
                                }
                                else
                                {
                                    ddlMailingListCodes.Text = reader.GetString(59);
                                    if (chbMailingList.Checked)
                                    {
                                        ddlMailingListCodes.Text = "Use Current Address";
                                        ddlMailingListCodes.Enabled = false;
                                    }
                                }
                                if (reader.IsDBNull(60))
                                {
                                    chbDiscipleshipMentorPotentials.Checked = false;
                                }
                                else
                                {
                                    chbDiscipleshipMentorPotentials.Checked = reader.GetBoolean(60);
                                }
                                if (reader.IsDBNull(61))
                                {
                                    chbOfficeVolunteer.Checked = false;
                                }
                                else
                                {
                                    chbOfficeVolunteer.Checked = reader.GetBoolean(61);
                                }
                                if (reader.IsDBNull(62))
                                {
                                    chbSummerDayCamp.Checked = false;
                                }
                                else
                                {
                                    chbSummerDayCamp.Checked = reader.GetBoolean(62);
                                }
                                if (reader.IsDBNull(63))
                                {
                                    txbEmergencyRelationship.Text = "N/A";
                                }
                                else
                                {
                                    txbEmergencyRelationship.Text = reader.GetString(63);
                                }
                                if (reader.IsDBNull(64))
                                {
                                    txbEmergRelationship.Text = "NULL";
                                }
                                else
                                {
                                    txbEmergRelationship.Text = reader.GetSqlValue(64).ToString();
                                }
                                if (reader.IsDBNull(65))
                                {
                                    txbEmergencyPhone.Text = "N/A";
                                }
                                else
                                {
                                    txbEmergencyPhone.Text = reader.GetSqlValue(65).ToString();
                                }
                                if (reader.IsDBNull(66))
                                {
                                    chbSpecialEvents.Checked = false;
                                }
                                else
                                {
                                    chbSpecialEvents.Checked = reader.GetBoolean(66);
                                }
                                if (reader.IsDBNull(67))
                                {
                                    chbStaff.Checked = false;
                                }
                                else
                                {
                                    chbStaff.Checked = reader.GetBoolean(67);
                                }
                                if (reader.IsDBNull(68))
                                {
                                }
                                else
                                {
                                    ddlMostRecentSeason.Text = reader.GetString(68);
                                }
                                if (reader.IsDBNull(69))
                                {
                                }
                                else
                                {
                                    ddlMostRecentSeasonYear.Text = reader.GetString(69);
                                }
                                if (reader.IsDBNull(70))
                                {
                                    chbImpactUrbanSchools.Checked = false;
                                    chbImpactUrbanSchoolsPA.Checked = false;
                                    chbImpactUrbanSchoolsAcademics.Checked = false;
                                }
                                else
                                {
                                    if (Department == "Athletics")
                                    {
                                        chbImpactUrbanSchools.Checked = reader.GetBoolean(70);
                                    }
                                    else if (Department == "PerformingArts")
                                    {
                                        chbImpactUrbanSchoolsPA.Checked = reader.GetBoolean(70);
                                    }
                                    else if (Department == "Education")
                                    {
                                        chbImpactUrbanSchoolsAcademics.Checked = reader.GetBoolean(70);
                                    }
                                }
                                if (reader.IsDBNull(71))
                                {
                                    chbReadingProgram.Checked = false;
                                }
                                else
                                {
                                    chbReadingProgram.Checked = reader.GetBoolean(71);
                                }
                                //chbBoysBaseball.Checked = reader.GetBoolean(43);
                                ddlTextPhone.Text = "No";
                                reader.Close();
                            }
                            CleanCharacters();

                            if (((Request.QueryString["LastName"] == "Sims-Reed") && (Request.QueryString["FirstName"] == "Donna")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Boll") && (Request.QueryString["FirstName"] == "Becky")) || ((Request.QueryString["LastName"] == "Churchill") && (Request.QueryString["FirstName"] == "Andrew")))
                            {
                                //Give these people access to the Discipleshipmentor related controls.. No one else..RCM..8/11/11.
                            }
                            else
                            {
                                txbDiscipleshipMentorNotes.Enabled = false;
                                chbDiscipleshipMentorPotentials.Enabled = false;
                                chbDiscipleshipmentorTraining.Enabled = false;
                                chbDiscipleshipmentorWaitingList.Enabled = false;
                                chbDiscipleshipmentorTrainingDone.Enabled = false;

                                ddlDayBirth2.Enabled = false;
                                ddlDayBirth3.Enabled = false;
                                ddlMonthBirth2.Enabled = false;
                                ddlMonthBirth3.Enabled = false;
                                ddlYearBirth2.Enabled = false;
                                ddlYearBirth3.Enabled = false;
                            }
                        }
                        catch (Exception lkjl)
                        {
                            //lblInformation.Enabled = true;
                            //lblInformation.Text = "The SELECT from the DB failed.  Please fix and try again MSG: " + lkjl.Message.ToString();

                        }
                        finally
                        {
                            reader.Close();
                            //reader2.Close();
                        }
                        cmbNewPerson2.Enabled = false;
                        //btnNewPerson1.Enabled = false;
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

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
    	{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
            if (!this.DesignMode)
            {



            }
	    }
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void TextBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtState_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtZip_TextChanged(object sender, System.EventArgs e)
		{
		
		}


		protected void ddlGender_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtDateBirth_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtCity_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtAge_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtFirstName_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtLastName_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtAddress1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtHomePhone_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		//protected void ddlSchool_TextChanged(object sender, System.EventArgs e)
		//{
		
		//}

		protected void txtStudentEmail_TextChanged(object sender, System.EventArgs e)
		{
		
		}


		protected void ddlGrade_TextChanged(object sender, System.EventArgs e)
		{
			//ddlGrade.Enabled = true;
		}

		protected void chbZip_CheckedChanged(object sender, System.EventArgs e)
		{	
			txtZip.Enabled = true;
		}

		protected void chbCareerGoal_CheckedChanged(object sender, System.EventArgs e)
		{
			//txtCareerGoal.Enabled = true;
		}

		protected void chbChurch_CheckedChanged(object sender, System.EventArgs e)
		{
			txtChurch.Enabled = true;
		}

		protected void chbTShirtSize_CheckedChanged(object sender, System.EventArgs e)
		{
			ddlTShirtSize.Enabled = true;
		}

		protected void chbStudentEmail_CheckedChanged(object sender, System.EventArgs e)
		{
			txtStudentEmail.Enabled = true;
		}

		protected void chbSchool_CheckedChanged(object sender, System.EventArgs e)
		{
			//ddlSchool.Enabled = true;		
		}

		protected void chbHomePhone_CheckedChanged(object sender, System.EventArgs e)
		{
			txtHomePhone.Enabled = true;
		}

		protected void chbAddress_CheckedChanged(object sender, System.EventArgs e)
		{
			txtAddress1.Enabled = true;		
		}

		protected void chbLastName_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chbLastName.Checked = true)
			{	
				chbLastName.Checked = false;
				txtLastName.Enabled = true;
			}
			else
			{
				chbLastName.Checked = true;
				txtLastName.Enabled = false;
			}
		}

		protected void chbFirstName_CheckedChanged(object sender, System.EventArgs e)
		{
			txtFirstName.Enabled = true;		
		}

		protected void chbGrade_CheckedChanged(object sender, System.EventArgs e)
		{
			//ddlGrade.Enabled = true;
		}

		protected void chbAge_CheckedChanged(object sender, System.EventArgs e)
		{
			ddlAge.Enabled = true;		
		}

		protected void chbDateBirth_CheckedChanged(object sender, System.EventArgs e)
		{
			//txtDateBirth.Enabled = true;
		}

		protected void chbGender_CheckedChanged(object sender, System.EventArgs e)
		{
			ddlGender.Enabled = true;		
		}

		protected void chbState_CheckedChanged(object sender, System.EventArgs e)
		{
			txtState.Enabled = true;		
		}

		protected void chbCity_CheckedChanged(object sender, System.EventArgs e)
		{
			txtCity.Enabled = true;
		}

		protected void chbStudentCellPhone_CheckedChanged(object sender, System.EventArgs e)
		{
			txtStudentCellPhone.Enabled = true;		
		}

        protected void PopulateChurchDropdown()
        {
            try
            {
                con.Open();

                string selectSQL = "";

                selectSQL = "select church " +
                            "from UIF_PerformingArts.dbo.ChurchNames " +
                            "group by church " +
                            "order by church ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ddlChurch.Items.Add("Attend Church?");
                    do
                    {
                        ddlChurch.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    ddlChurch.Text = "Attend Church?";
                }
            }
            catch (Exception lkjlkabb)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void StartingSettings()
        {
            //Set initial colors, sizes, etc...RCM..
            chbAddress.Enabled = true;
            chbAge.Enabled = true;
            chbDateBirth.Enabled = true;
            //chbGender.Enabled = true;
            chbHomePhone.Enabled = true;
            chbTShirtSize.Enabled = true;
            chbStudentEmail.Enabled = true;
            chbState.Enabled = true;
            chbLastName.Enabled = true;
            //chbSchool.Enabled = true;
            chbStudentCellPhone.Enabled = true;
            chbGrade.Enabled = true;
            chbGrade.Checked = true;

            //Set the values for the MailingList code dropdown..RCM..8/3/11.
            ddlMailingListCodes.Items.Add("Use Current Address");
            ddlMailingListCodes.Items.Add("Wrong Address");
            ddlMailingListCodes.Items.Add("No Address");
            ddlMailingListCodes.Items.Add("Not Deliverable");
            ddlMailingListCodes.Items.Add("Moved Away");
            ddlMailingListCodes.Items.Add("Deceased");
            ddlMailingListCodes.Text = "Use Current Address";

            //Set the values for the dropdownlist for 
            //Most Recent Season, per Andrew request..
            //Ryan C Manners...8/10/12.
            ddlMostRecentSeason.Items.Add("ProgramSeason");
            ddlMostRecentSeason.Items.Add("Fall");
            ddlMostRecentSeason.Items.Add("Summer");
            ddlMostRecentSeason.Items.Add("WinterSpring");
            ddlMostRecentSeason.Items.Add("Spring");
            ddlMostRecentSeason.Text = "ProgramSeason";

            PopulateChurchDropdown();

            //Set the values for the dropdownlist for 
            //Most Recent Season, per Andrew request..
            //Ryan C Manners...8/10/12.
            ddlMostRecentSeasonYear.Items.Add("Year");
            ddlMostRecentSeasonYear.Items.Add("2022");
            ddlMostRecentSeasonYear.Items.Add("2021");
            ddlMostRecentSeasonYear.Items.Add("2020");
            ddlMostRecentSeasonYear.Items.Add("2019");
            ddlMostRecentSeasonYear.Items.Add("2018");
            ddlMostRecentSeasonYear.Items.Add("2017");
            ddlMostRecentSeasonYear.Items.Add("2016");
            ddlMostRecentSeasonYear.Items.Add("2015");
            ddlMostRecentSeasonYear.Items.Add("2014");
            ddlMostRecentSeasonYear.Items.Add("2013");
            ddlMostRecentSeasonYear.Items.Add("2012");
            ddlMostRecentSeasonYear.Items.Add("2011");
            ddlMostRecentSeasonYear.Items.Add("2010");
            ddlMostRecentSeasonYear.Items.Add("2009");
            ddlMostRecentSeasonYear.Items.Add("2008");
            ddlMostRecentSeasonYear.Items.Add("2007");
            ddlMostRecentSeasonYear.Items.Add("2006");
            ddlMostRecentSeasonYear.Items.Add("2005");
            ddlMostRecentSeasonYear.Text = "Year";

            ddlAge.Items.Add("14");
            ddlAge.Items.Add("15");
            ddlAge.Items.Add("16");
            ddlAge.Items.Add("17");
            ddlAge.Items.Add("18");
            ddlAge.Items.Add("19");
            ddlAge.Items.Add("20");
            ddlAge.Items.Add("21");
            ddlAge.Items.Add("22");
            ddlAge.Items.Add("23");
            ddlAge.Items.Add("24");
            ddlAge.Items.Add("25");
            ddlAge.Items.Add("26");
            ddlAge.Items.Add("27");
            ddlAge.Items.Add("28");
            ddlAge.Items.Add("29");
            ddlAge.Items.Add("30");
            ddlAge.Items.Add("31");
            ddlAge.Items.Add("32");
            ddlAge.Items.Add("33");
            ddlAge.Items.Add("34");
            ddlAge.Items.Add("35");
            ddlAge.Items.Add("36");
            ddlAge.Items.Add("37");
            ddlAge.Items.Add("38");
            ddlAge.Items.Add("39");
            ddlAge.Items.Add("40");
            ddlAge.Items.Add("41");
            ddlAge.Items.Add("42");
            ddlAge.Items.Add("43");
            ddlAge.Items.Add("44");
            ddlAge.Items.Add("45");

            ddlAge.Items.Add("46");
            ddlAge.Items.Add("47");
            ddlAge.Items.Add("48");
            ddlAge.Items.Add("49");
            ddlAge.Items.Add("50");
            ddlAge.Items.Add("51");
            ddlAge.Items.Add("52");
            ddlAge.Items.Add("53");
            ddlAge.Items.Add("54");
            ddlAge.Items.Add("55");
            ddlAge.Items.Add("56");
            ddlAge.Items.Add("57");
            ddlAge.Items.Add("58");
            ddlAge.Items.Add("59");
            ddlAge.Items.Add("60");
            ddlAge.Items.Add("61");
            ddlAge.Items.Add("62");
            ddlAge.Items.Add("63");
            ddlAge.Items.Add("64");
            ddlAge.Items.Add("65");
            ddlAge.Items.Add("66");
            ddlAge.Items.Add("67");
            ddlAge.Items.Add("68");
            ddlAge.Items.Add("69");
            ddlAge.Items.Add("70");
            ddlAge.Items.Add("71");
            ddlAge.Items.Add("72");
            ddlAge.Items.Add("73");
            ddlAge.Items.Add("74");
            ddlAge.Items.Add("75");
            ddlAge.Items.Add("76");
            ddlAge.Items.Add("77");
            ddlAge.Items.Add("78");
            ddlAge.Items.Add("79");
            ddlAge.Items.Add("80");
            ddlAge.Items.Add("81");
            ddlAge.Items.Add("82");

            ddlBackgroundMonth.Items.Add("01");
            ddlBackgroundMonth.Items.Add("02");
            ddlBackgroundMonth.Items.Add("03");
            ddlBackgroundMonth.Items.Add("04");
            ddlBackgroundMonth.Items.Add("05");
            ddlBackgroundMonth.Items.Add("06");
            ddlBackgroundMonth.Items.Add("07");
            ddlBackgroundMonth.Items.Add("08");
            ddlBackgroundMonth.Items.Add("09");
            ddlBackgroundMonth.Items.Add("10");
            ddlBackgroundMonth.Items.Add("11");
            ddlBackgroundMonth.Items.Add("12");

            ddlBackgroundDay.Items.Add("01");
            ddlBackgroundDay.Items.Add("02");
            ddlBackgroundDay.Items.Add("03");
            ddlBackgroundDay.Items.Add("04");
            ddlBackgroundDay.Items.Add("05");
            ddlBackgroundDay.Items.Add("06");
            ddlBackgroundDay.Items.Add("07");
            ddlBackgroundDay.Items.Add("08");
            ddlBackgroundDay.Items.Add("09");
            ddlBackgroundDay.Items.Add("10");
            ddlBackgroundDay.Items.Add("11");
            ddlBackgroundDay.Items.Add("12");
            ddlBackgroundDay.Items.Add("13");
            ddlBackgroundDay.Items.Add("14");
            ddlBackgroundDay.Items.Add("15");
            ddlBackgroundDay.Items.Add("16");
            ddlBackgroundDay.Items.Add("17");
            ddlBackgroundDay.Items.Add("18");
            ddlBackgroundDay.Items.Add("19");
            ddlBackgroundDay.Items.Add("20");
            ddlBackgroundDay.Items.Add("21");
            ddlBackgroundDay.Items.Add("22");
            ddlBackgroundDay.Items.Add("23");
            ddlBackgroundDay.Items.Add("24");
            ddlBackgroundDay.Items.Add("25");
            ddlBackgroundDay.Items.Add("26");
            ddlBackgroundDay.Items.Add("27");
            ddlBackgroundDay.Items.Add("28");
            ddlBackgroundDay.Items.Add("29");
            ddlBackgroundDay.Items.Add("30");
            ddlBackgroundDay.Items.Add("31");

            ddlBackgroundYear.Items.Add("1920");
            ddlBackgroundYear.Items.Add("1921");
            ddlBackgroundYear.Items.Add("1922");
            ddlBackgroundYear.Items.Add("1923");
            ddlBackgroundYear.Items.Add("1924");
            ddlBackgroundYear.Items.Add("1925");
            ddlBackgroundYear.Items.Add("1926");
            ddlBackgroundYear.Items.Add("1927");
            ddlBackgroundYear.Items.Add("1928");
            ddlBackgroundYear.Items.Add("1929");
            ddlBackgroundYear.Items.Add("1930");
            ddlBackgroundYear.Items.Add("1931");
            ddlBackgroundYear.Items.Add("1932");
            ddlBackgroundYear.Items.Add("1933");
            ddlBackgroundYear.Items.Add("1934");
            ddlBackgroundYear.Items.Add("1935");
            ddlBackgroundYear.Items.Add("1936");
            ddlBackgroundYear.Items.Add("1937");
            ddlBackgroundYear.Items.Add("1938");
            ddlBackgroundYear.Items.Add("1939");
            ddlBackgroundYear.Items.Add("1940");
            ddlBackgroundYear.Items.Add("1941");
            ddlBackgroundYear.Items.Add("1942");
            ddlBackgroundYear.Items.Add("1943");
            ddlBackgroundYear.Items.Add("1944");
            ddlBackgroundYear.Items.Add("1945");
            ddlBackgroundYear.Items.Add("1946");
            ddlBackgroundYear.Items.Add("1947");
            ddlBackgroundYear.Items.Add("1948");
            ddlBackgroundYear.Items.Add("1949");
            ddlBackgroundYear.Items.Add("1950");
            ddlBackgroundYear.Items.Add("1951");
            ddlBackgroundYear.Items.Add("1952");
            ddlBackgroundYear.Items.Add("1953");
            ddlBackgroundYear.Items.Add("1954");
            ddlBackgroundYear.Items.Add("1955");
            ddlBackgroundYear.Items.Add("1956");
            ddlBackgroundYear.Items.Add("1957");
            ddlBackgroundYear.Items.Add("1958");
            ddlBackgroundYear.Items.Add("1959");
            ddlBackgroundYear.Items.Add("1960");
            ddlBackgroundYear.Items.Add("1961");
            ddlBackgroundYear.Items.Add("1962");
            ddlBackgroundYear.Items.Add("1963");
            ddlBackgroundYear.Items.Add("1964");
            ddlBackgroundYear.Items.Add("1965");
            ddlBackgroundYear.Items.Add("1966");
            ddlBackgroundYear.Items.Add("1967");
            ddlBackgroundYear.Items.Add("1968");
            ddlBackgroundYear.Items.Add("1969");
            ddlBackgroundYear.Items.Add("1970");
            ddlBackgroundYear.Items.Add("1971");
            ddlBackgroundYear.Items.Add("1972");
            ddlBackgroundYear.Items.Add("1973");
            ddlBackgroundYear.Items.Add("1974");
            ddlBackgroundYear.Items.Add("1975");
            ddlBackgroundYear.Items.Add("1976");
            ddlBackgroundYear.Items.Add("1977");
            ddlBackgroundYear.Items.Add("1978");
            ddlBackgroundYear.Items.Add("1979");
            ddlBackgroundYear.Items.Add("1980");
            ddlBackgroundYear.Items.Add("1981");
            ddlBackgroundYear.Items.Add("1982");
            ddlBackgroundYear.Items.Add("1983");
            ddlBackgroundYear.Items.Add("1984");
            ddlBackgroundYear.Items.Add("1985");
            ddlBackgroundYear.Items.Add("1986");
            ddlBackgroundYear.Items.Add("1987");
            ddlBackgroundYear.Items.Add("1988");
            ddlBackgroundYear.Items.Add("1989");
            ddlBackgroundYear.Items.Add("1990");
            ddlBackgroundYear.Items.Add("1991");
            ddlBackgroundYear.Items.Add("1992");
            ddlBackgroundYear.Items.Add("1993");
            ddlBackgroundYear.Items.Add("1994");
            ddlBackgroundYear.Items.Add("1995");
            ddlBackgroundYear.Items.Add("1996");
            ddlBackgroundYear.Items.Add("1997");
            ddlBackgroundYear.Items.Add("1998");
            ddlBackgroundYear.Items.Add("1999");
            ddlBackgroundYear.Items.Add("2000");
            ddlBackgroundYear.Items.Add("2001");
            ddlBackgroundYear.Items.Add("2002");
            ddlBackgroundYear.Items.Add("2003");
            ddlBackgroundYear.Items.Add("2004");
            ddlBackgroundYear.Items.Add("2005");
            ddlBackgroundYear.Items.Add("2006");
            ddlBackgroundYear.Items.Add("2007");
            ddlBackgroundYear.Items.Add("2008");
            ddlBackgroundYear.Items.Add("2009");
            ddlBackgroundYear.Items.Add("2010");
            ddlBackgroundYear.Items.Add("2011");
            ddlBackgroundYear.Items.Add("2012");
            ddlBackgroundYear.Items.Add("2013");
            ddlBackgroundYear.Items.Add("2014");
            ddlBackgroundYear.Items.Add("2015");
            ddlBackgroundYear.Items.Add("2016");
            ddlBackgroundYear.Items.Add("2017");
            ddlBackgroundYear.Items.Add("2018");
            ddlBackgroundYear.Items.Add("2019");
            ddlBackgroundYear.Items.Add("2020");


            ddlMonthBirth.Items.Add("01");
            ddlMonthBirth.Items.Add("02");
            ddlMonthBirth.Items.Add("03");
            ddlMonthBirth.Items.Add("04");
            ddlMonthBirth.Items.Add("05");
            ddlMonthBirth.Items.Add("06");
            ddlMonthBirth.Items.Add("07");
            ddlMonthBirth.Items.Add("08");
            ddlMonthBirth.Items.Add("09");
            ddlMonthBirth.Items.Add("10");
            ddlMonthBirth.Items.Add("11");
            ddlMonthBirth.Items.Add("12");

            ddlMonthBirth2.Items.Add("01");
            ddlMonthBirth2.Items.Add("02");
            ddlMonthBirth2.Items.Add("03");
            ddlMonthBirth2.Items.Add("04");
            ddlMonthBirth2.Items.Add("05");
            ddlMonthBirth2.Items.Add("06");
            ddlMonthBirth2.Items.Add("07");
            ddlMonthBirth2.Items.Add("08");
            ddlMonthBirth2.Items.Add("09");
            ddlMonthBirth2.Items.Add("10");
            ddlMonthBirth2.Items.Add("11");
            ddlMonthBirth2.Items.Add("12");

            ddlMonthBirth3.Items.Add("01");
            ddlMonthBirth3.Items.Add("02");
            ddlMonthBirth3.Items.Add("03");
            ddlMonthBirth3.Items.Add("04");
            ddlMonthBirth3.Items.Add("05");
            ddlMonthBirth3.Items.Add("06");
            ddlMonthBirth3.Items.Add("07");
            ddlMonthBirth3.Items.Add("08");
            ddlMonthBirth3.Items.Add("09");
            ddlMonthBirth3.Items.Add("10");
            ddlMonthBirth3.Items.Add("11");
            ddlMonthBirth3.Items.Add("12");




            //Ryan C Manners..12/16/11.
            //Customize the tab indexes for Becky per her request...
            //if ((Request.QueryString["LastName"] == "Boll") && (Request.QueryString["FirstName"] == "Becky"))
            //{
            txtFirstName.TabIndex = 1;
            //txbMiddleName.TabIndex = 2;
            txtLastName.TabIndex = 3;
            ddlAge.TabIndex = 5;
            ddlMonthBirth.TabIndex = 6;
            ddlDayBirth.TabIndex = 7;
            ddlYearBirth.TabIndex = 8;
            ddlGender.TabIndex = 9;
            txtAddress1.TabIndex = 10;
            txtCity.TabIndex = 11;
            txtState.TabIndex = 12;
            txtZip.TabIndex = 13;
            txtHomePhone.TabIndex = 14;
            txtStudentCellPhone.TabIndex = 15;
            ddlTextPhone.TabIndex = 16;
            txtStudentEmail.TabIndex = 17;
            txtChurch.TabIndex = 18;
            ddlTShirtSize.TabIndex = 19;
            //txtStudentEmail.TabIndex = 20;
            txbHealthConditions.TabIndex = 20;
            txbNotes.TabIndex = 21;
            txbDiscipleshipMentorNotes.TabIndex = 22;

            //txtParentGuardian1.TabIndex = 27;
            //ddlParentGuardian1Relationship.TabIndex = 28;
            //txbParentGuardian1WrkPh.TabIndex = 29;
            //txbParentGuardian1CellPhone.TabIndex = 30;
            //txbParentGuardian1Email.TabIndex = 31;
            //txbParentGuardian2.TabIndex = 32;
            //ddlParentGuardian2Relationship.TabIndex = 33;
            //txbParentGuardian2WrkPh.TabIndex = 34;
            //txbParentGuardian2CellPhone.TabIndex = 35;
            //txbParentGuardian2Email.TabIndex = 36;
            //txbEmergencyRelationship.TabIndex = 37;
            //txbEmergRelationship.TabIndex = 38;
            //txbEmergencyPhone.TabIndex = 39;
            //}



            ddlDayBirth.Items.Add("01");
            ddlDayBirth.Items.Add("02");
            ddlDayBirth.Items.Add("03");
            ddlDayBirth.Items.Add("04");
            ddlDayBirth.Items.Add("05");
            ddlDayBirth.Items.Add("06");
            ddlDayBirth.Items.Add("07");
            ddlDayBirth.Items.Add("08");
            ddlDayBirth.Items.Add("09");
            ddlDayBirth.Items.Add("10");
            ddlDayBirth.Items.Add("11");
            ddlDayBirth.Items.Add("12");
            ddlDayBirth.Items.Add("13");
            ddlDayBirth.Items.Add("14");
            ddlDayBirth.Items.Add("15");
            ddlDayBirth.Items.Add("16");
            ddlDayBirth.Items.Add("17");
            ddlDayBirth.Items.Add("18");
            ddlDayBirth.Items.Add("19");
            ddlDayBirth.Items.Add("20");
            ddlDayBirth.Items.Add("21");
            ddlDayBirth.Items.Add("22");
            ddlDayBirth.Items.Add("23");
            ddlDayBirth.Items.Add("24");
            ddlDayBirth.Items.Add("25");
            ddlDayBirth.Items.Add("26");
            ddlDayBirth.Items.Add("27");
            ddlDayBirth.Items.Add("28");
            ddlDayBirth.Items.Add("29");
            ddlDayBirth.Items.Add("30");
            ddlDayBirth.Items.Add("31");

            ddlDayBirth2.Items.Add("01");
            ddlDayBirth2.Items.Add("02");
            ddlDayBirth2.Items.Add("03");
            ddlDayBirth2.Items.Add("04");
            ddlDayBirth2.Items.Add("05");
            ddlDayBirth2.Items.Add("06");
            ddlDayBirth2.Items.Add("07");
            ddlDayBirth2.Items.Add("08");
            ddlDayBirth2.Items.Add("09");
            ddlDayBirth2.Items.Add("10");
            ddlDayBirth2.Items.Add("11");
            ddlDayBirth2.Items.Add("12");
            ddlDayBirth2.Items.Add("13");
            ddlDayBirth2.Items.Add("14");
            ddlDayBirth2.Items.Add("15");
            ddlDayBirth2.Items.Add("16");
            ddlDayBirth2.Items.Add("17");
            ddlDayBirth2.Items.Add("18");
            ddlDayBirth2.Items.Add("19");
            ddlDayBirth2.Items.Add("20");
            ddlDayBirth2.Items.Add("21");
            ddlDayBirth2.Items.Add("22");
            ddlDayBirth2.Items.Add("23");
            ddlDayBirth2.Items.Add("24");
            ddlDayBirth2.Items.Add("25");
            ddlDayBirth2.Items.Add("26");
            ddlDayBirth2.Items.Add("27");
            ddlDayBirth2.Items.Add("28");
            ddlDayBirth2.Items.Add("29");
            ddlDayBirth2.Items.Add("30");
            ddlDayBirth2.Items.Add("31");

            ddlDayBirth3.Items.Add("01");
            ddlDayBirth3.Items.Add("02");
            ddlDayBirth3.Items.Add("03");
            ddlDayBirth3.Items.Add("04");
            ddlDayBirth3.Items.Add("05");
            ddlDayBirth3.Items.Add("06");
            ddlDayBirth3.Items.Add("07");
            ddlDayBirth3.Items.Add("08");
            ddlDayBirth3.Items.Add("09");
            ddlDayBirth3.Items.Add("10");
            ddlDayBirth3.Items.Add("11");
            ddlDayBirth3.Items.Add("12");
            ddlDayBirth3.Items.Add("13");
            ddlDayBirth3.Items.Add("14");
            ddlDayBirth3.Items.Add("15");
            ddlDayBirth3.Items.Add("16");
            ddlDayBirth3.Items.Add("17");
            ddlDayBirth3.Items.Add("18");
            ddlDayBirth3.Items.Add("19");
            ddlDayBirth3.Items.Add("20");
            ddlDayBirth3.Items.Add("21");
            ddlDayBirth3.Items.Add("22");
            ddlDayBirth3.Items.Add("23");
            ddlDayBirth3.Items.Add("24");
            ddlDayBirth3.Items.Add("25");
            ddlDayBirth3.Items.Add("26");
            ddlDayBirth3.Items.Add("27");
            ddlDayBirth3.Items.Add("28");
            ddlDayBirth3.Items.Add("29");
            ddlDayBirth3.Items.Add("30");
            ddlDayBirth3.Items.Add("31");

            ddlYearBirth.Items.Add("1920");
            ddlYearBirth.Items.Add("1921");
            ddlYearBirth.Items.Add("1922");
            ddlYearBirth.Items.Add("1923");
            ddlYearBirth.Items.Add("1924");
            ddlYearBirth.Items.Add("1925");
            ddlYearBirth.Items.Add("1926");
            ddlYearBirth.Items.Add("1927");
            ddlYearBirth.Items.Add("1928");
            ddlYearBirth.Items.Add("1929");
            ddlYearBirth.Items.Add("1930");
            ddlYearBirth.Items.Add("1931");
            ddlYearBirth.Items.Add("1932");
            ddlYearBirth.Items.Add("1933");
            ddlYearBirth.Items.Add("1934");
            ddlYearBirth.Items.Add("1935");
            ddlYearBirth.Items.Add("1936");
            ddlYearBirth.Items.Add("1937");
            ddlYearBirth.Items.Add("1938");
            ddlYearBirth.Items.Add("1939");
            ddlYearBirth.Items.Add("1940");
            ddlYearBirth.Items.Add("1941");
            ddlYearBirth.Items.Add("1942");
            ddlYearBirth.Items.Add("1943");
            ddlYearBirth.Items.Add("1944");
            ddlYearBirth.Items.Add("1945");
            ddlYearBirth.Items.Add("1946");
            ddlYearBirth.Items.Add("1947");
            ddlYearBirth.Items.Add("1948");
            ddlYearBirth.Items.Add("1949");
            ddlYearBirth.Items.Add("1950");
            ddlYearBirth.Items.Add("1951");
            ddlYearBirth.Items.Add("1952");
            ddlYearBirth.Items.Add("1953");
            ddlYearBirth.Items.Add("1954");
            ddlYearBirth.Items.Add("1955");
            ddlYearBirth.Items.Add("1956");
            ddlYearBirth.Items.Add("1957");
            ddlYearBirth.Items.Add("1958");
            ddlYearBirth.Items.Add("1959");
            ddlYearBirth.Items.Add("1960");
            ddlYearBirth.Items.Add("1961");
            ddlYearBirth.Items.Add("1962");            
            ddlYearBirth.Items.Add("1963");
            ddlYearBirth.Items.Add("1964");
            ddlYearBirth.Items.Add("1965");
            ddlYearBirth.Items.Add("1966");
            ddlYearBirth.Items.Add("1967");
            ddlYearBirth.Items.Add("1968");
            ddlYearBirth.Items.Add("1969");
            ddlYearBirth.Items.Add("1970");
            ddlYearBirth.Items.Add("1971");
            ddlYearBirth.Items.Add("1972");
            ddlYearBirth.Items.Add("1973");
            ddlYearBirth.Items.Add("1974");
            ddlYearBirth.Items.Add("1975");
            ddlYearBirth.Items.Add("1976");
            ddlYearBirth.Items.Add("1977");
            ddlYearBirth.Items.Add("1978");
            ddlYearBirth.Items.Add("1979");
            ddlYearBirth.Items.Add("1980");
            ddlYearBirth.Items.Add("1981");
            ddlYearBirth.Items.Add("1982");
            ddlYearBirth.Items.Add("1983");
            ddlYearBirth.Items.Add("1984");
            ddlYearBirth.Items.Add("1985");
            ddlYearBirth.Items.Add("1986");
            ddlYearBirth.Items.Add("1987");
            ddlYearBirth.Items.Add("1988");
            ddlYearBirth.Items.Add("1989");
            ddlYearBirth.Items.Add("1990");
            ddlYearBirth.Items.Add("1991");
            ddlYearBirth.Items.Add("1992");
            ddlYearBirth.Items.Add("1993");
            ddlYearBirth.Items.Add("1994");
            ddlYearBirth.Items.Add("1995");
            ddlYearBirth.Items.Add("1996");
            ddlYearBirth.Items.Add("1997");
            ddlYearBirth.Items.Add("1998");
            ddlYearBirth.Items.Add("1999");
            ddlYearBirth.Items.Add("2000");
            ddlYearBirth.Items.Add("2001");
            ddlYearBirth.Items.Add("2002");
            ddlYearBirth.Items.Add("2003");
            ddlYearBirth.Items.Add("2004");
            ddlYearBirth.Items.Add("2005");
            ddlYearBirth.Items.Add("2006");
            ddlYearBirth.Items.Add("2007");
            ddlYearBirth.Items.Add("2008");
            ddlYearBirth.Items.Add("2009");
            ddlYearBirth.Items.Add("2010");
            ddlYearBirth.Items.Add("2011");
            ddlYearBirth.Items.Add("2012");
            ddlYearBirth.Items.Add("2013");
            ddlYearBirth.Items.Add("2014");
            ddlYearBirth.Items.Add("2015");
            ddlYearBirth.Items.Add("2016");
            ddlYearBirth.Items.Add("2017");
            ddlYearBirth.Items.Add("2018");
            ddlYearBirth.Items.Add("2019");
            ddlYearBirth.Items.Add("2020");
            ddlYearBirth.Items.Add("2021");
            ddlYearBirth.Items.Add("2022");
            ddlYearBirth.Items.Add("2023");
            ddlYearBirth.Items.Add("2024");
            ddlYearBirth.Items.Add("2025");
            ddlYearBirth.Items.Add("2026");
            ddlYearBirth.Items.Add("2027");
            ddlYearBirth.Items.Add("2028");
            ddlYearBirth.Items.Add("2029");
            ddlYearBirth.Items.Add("2030");
            ddlYearBirth.Items.Add("2031");
            ddlYearBirth.Items.Add("2032");
            ddlYearBirth.Items.Add("2033");

            ddlYearBirth2.Items.Add("1920");
            ddlYearBirth2.Items.Add("1921");
            ddlYearBirth2.Items.Add("1922");
            ddlYearBirth2.Items.Add("1923");
            ddlYearBirth2.Items.Add("1924");
            ddlYearBirth2.Items.Add("1925");
            ddlYearBirth2.Items.Add("1926");
            ddlYearBirth2.Items.Add("1927");
            ddlYearBirth2.Items.Add("1928");
            ddlYearBirth2.Items.Add("1929");
            ddlYearBirth2.Items.Add("1930");
            ddlYearBirth2.Items.Add("1931");
            ddlYearBirth2.Items.Add("1932");
            ddlYearBirth2.Items.Add("1933");
            ddlYearBirth2.Items.Add("1934");
            ddlYearBirth2.Items.Add("1935");
            ddlYearBirth2.Items.Add("1936");
            ddlYearBirth2.Items.Add("1937");
            ddlYearBirth2.Items.Add("1938");
            ddlYearBirth2.Items.Add("1939");
            ddlYearBirth2.Items.Add("1940");
            ddlYearBirth2.Items.Add("1941");
            ddlYearBirth2.Items.Add("1942");
            ddlYearBirth2.Items.Add("1943");
            ddlYearBirth2.Items.Add("1944");
            ddlYearBirth2.Items.Add("1945");
            ddlYearBirth2.Items.Add("1946");
            ddlYearBirth2.Items.Add("1947");
            ddlYearBirth2.Items.Add("1948");
            ddlYearBirth2.Items.Add("1949");
            ddlYearBirth2.Items.Add("1950");
            ddlYearBirth2.Items.Add("1951");
            ddlYearBirth2.Items.Add("1952");
            ddlYearBirth2.Items.Add("1953");
            ddlYearBirth2.Items.Add("1954");
            ddlYearBirth2.Items.Add("1955");
            ddlYearBirth2.Items.Add("1956");
            ddlYearBirth2.Items.Add("1957");
            ddlYearBirth2.Items.Add("1958");
            ddlYearBirth2.Items.Add("1959");
            ddlYearBirth2.Items.Add("1960");
            ddlYearBirth2.Items.Add("1961");
            ddlYearBirth2.Items.Add("1962");
            ddlYearBirth2.Items.Add("1963");
            ddlYearBirth2.Items.Add("1964");
            ddlYearBirth2.Items.Add("1965");
            ddlYearBirth2.Items.Add("1966");
            ddlYearBirth2.Items.Add("1967");
            ddlYearBirth2.Items.Add("1968");
            ddlYearBirth2.Items.Add("1969");
            ddlYearBirth2.Items.Add("1970");
            ddlYearBirth2.Items.Add("1971");
            ddlYearBirth2.Items.Add("1972");
            ddlYearBirth2.Items.Add("1973");
            ddlYearBirth2.Items.Add("1974");
            ddlYearBirth2.Items.Add("1975");
            ddlYearBirth2.Items.Add("1976");
            ddlYearBirth2.Items.Add("1977");
            ddlYearBirth2.Items.Add("1978");
            ddlYearBirth2.Items.Add("1979");
            ddlYearBirth2.Items.Add("1980");
            ddlYearBirth2.Items.Add("1981");
            ddlYearBirth2.Items.Add("1982");
            ddlYearBirth2.Items.Add("1983");
            ddlYearBirth2.Items.Add("1984");
            ddlYearBirth2.Items.Add("1985");
            ddlYearBirth2.Items.Add("1986");
            ddlYearBirth2.Items.Add("1987");
            ddlYearBirth2.Items.Add("1988");
            ddlYearBirth2.Items.Add("1989");
            ddlYearBirth2.Items.Add("1990");
            ddlYearBirth2.Items.Add("1991");
            ddlYearBirth2.Items.Add("1992");
            ddlYearBirth2.Items.Add("1993");
            ddlYearBirth2.Items.Add("1994");
            ddlYearBirth2.Items.Add("1995");
            ddlYearBirth2.Items.Add("1996");
            ddlYearBirth2.Items.Add("1997");
            ddlYearBirth2.Items.Add("1998");
            ddlYearBirth2.Items.Add("1999");
            ddlYearBirth2.Items.Add("2000");
            ddlYearBirth2.Items.Add("2001");
            ddlYearBirth2.Items.Add("2002");
            ddlYearBirth2.Items.Add("2003");
            ddlYearBirth2.Items.Add("2004");
            ddlYearBirth2.Items.Add("2005");
            ddlYearBirth2.Items.Add("2006");
            ddlYearBirth2.Items.Add("2007");
            ddlYearBirth2.Items.Add("2008");
            ddlYearBirth2.Items.Add("2009");
            ddlYearBirth2.Items.Add("2010");
            ddlYearBirth2.Items.Add("2011");
            ddlYearBirth2.Items.Add("2012");
            ddlYearBirth2.Items.Add("2013");
            ddlYearBirth2.Items.Add("2014");
            ddlYearBirth2.Items.Add("2015");
            ddlYearBirth2.Items.Add("2016");
            ddlYearBirth2.Items.Add("2017");
            ddlYearBirth2.Items.Add("2018");
            ddlYearBirth2.Items.Add("2019");
            ddlYearBirth2.Items.Add("2020");
            ddlYearBirth2.Items.Add("2021");
            ddlYearBirth2.Items.Add("2022");
            ddlYearBirth2.Items.Add("2023");
            ddlYearBirth2.Items.Add("2024");
            ddlYearBirth2.Items.Add("2025");
            ddlYearBirth2.Items.Add("2026");
            ddlYearBirth2.Items.Add("2027");
            ddlYearBirth2.Items.Add("2028");
            ddlYearBirth2.Items.Add("2029");
            ddlYearBirth2.Items.Add("2030");
            ddlYearBirth2.Items.Add("2031");
            ddlYearBirth2.Items.Add("2032");
            ddlYearBirth2.Items.Add("2033");
            ddlYearBirth2.Items.Add("2034");

            ddlYearBirth3.Items.Add("1920");
            ddlYearBirth3.Items.Add("1921");
            ddlYearBirth3.Items.Add("1922");
            ddlYearBirth3.Items.Add("1923");
            ddlYearBirth3.Items.Add("1924");
            ddlYearBirth3.Items.Add("1925");
            ddlYearBirth3.Items.Add("1926");
            ddlYearBirth3.Items.Add("1927");
            ddlYearBirth3.Items.Add("1928");
            ddlYearBirth3.Items.Add("1929");
            ddlYearBirth3.Items.Add("1930");
            ddlYearBirth3.Items.Add("1931");
            ddlYearBirth3.Items.Add("1932");
            ddlYearBirth3.Items.Add("1933");
            ddlYearBirth3.Items.Add("1934");
            ddlYearBirth3.Items.Add("1935");
            ddlYearBirth3.Items.Add("1936");
            ddlYearBirth3.Items.Add("1937");
            ddlYearBirth3.Items.Add("1938");
            ddlYearBirth3.Items.Add("1939");
            ddlYearBirth3.Items.Add("1940");
            ddlYearBirth3.Items.Add("1941");
            ddlYearBirth3.Items.Add("1942");
            ddlYearBirth3.Items.Add("1943");
            ddlYearBirth3.Items.Add("1944");
            ddlYearBirth3.Items.Add("1945");
            ddlYearBirth3.Items.Add("1946");
            ddlYearBirth3.Items.Add("1947");
            ddlYearBirth3.Items.Add("1948");
            ddlYearBirth3.Items.Add("1949");
            ddlYearBirth3.Items.Add("1950");
            ddlYearBirth3.Items.Add("1951");
            ddlYearBirth3.Items.Add("1952");
            ddlYearBirth3.Items.Add("1953");
            ddlYearBirth3.Items.Add("1954");
            ddlYearBirth3.Items.Add("1955");
            ddlYearBirth3.Items.Add("1956");
            ddlYearBirth3.Items.Add("1957");
            ddlYearBirth3.Items.Add("1958");
            ddlYearBirth3.Items.Add("1959");
            ddlYearBirth3.Items.Add("1960");
            ddlYearBirth3.Items.Add("1961");
            ddlYearBirth3.Items.Add("1962");
            ddlYearBirth3.Items.Add("1963");
            ddlYearBirth3.Items.Add("1964");
            ddlYearBirth3.Items.Add("1965");
            ddlYearBirth3.Items.Add("1966");
            ddlYearBirth3.Items.Add("1967");
            ddlYearBirth3.Items.Add("1968");
            ddlYearBirth3.Items.Add("1969");
            ddlYearBirth3.Items.Add("1970");
            ddlYearBirth3.Items.Add("1971");
            ddlYearBirth3.Items.Add("1972");
            ddlYearBirth3.Items.Add("1973");
            ddlYearBirth3.Items.Add("1974");
            ddlYearBirth3.Items.Add("1975");
            ddlYearBirth3.Items.Add("1976");
            ddlYearBirth3.Items.Add("1977");
            ddlYearBirth3.Items.Add("1978");
            ddlYearBirth3.Items.Add("1979");
            ddlYearBirth3.Items.Add("1980");
            ddlYearBirth3.Items.Add("1981");
            ddlYearBirth3.Items.Add("1982");
            ddlYearBirth3.Items.Add("1983");
            ddlYearBirth3.Items.Add("1984");
            ddlYearBirth3.Items.Add("1985");
            ddlYearBirth3.Items.Add("1986");
            ddlYearBirth3.Items.Add("1987");
            ddlYearBirth3.Items.Add("1988");
            ddlYearBirth3.Items.Add("1989");
            ddlYearBirth3.Items.Add("1990");
            ddlYearBirth3.Items.Add("1991");
            ddlYearBirth3.Items.Add("1992");
            ddlYearBirth3.Items.Add("1993");
            ddlYearBirth3.Items.Add("1994");
            ddlYearBirth3.Items.Add("1995");
            ddlYearBirth3.Items.Add("1996");
            ddlYearBirth3.Items.Add("1997");
            ddlYearBirth3.Items.Add("1998");
            ddlYearBirth3.Items.Add("1999");
            ddlYearBirth3.Items.Add("2000");
            ddlYearBirth3.Items.Add("2001");
            ddlYearBirth3.Items.Add("2002");
            ddlYearBirth3.Items.Add("2003");
            ddlYearBirth3.Items.Add("2004");
            ddlYearBirth3.Items.Add("2005");
            ddlYearBirth3.Items.Add("2006");
            ddlYearBirth3.Items.Add("2007");
            ddlYearBirth3.Items.Add("2008");
            ddlYearBirth3.Items.Add("2009");
            ddlYearBirth3.Items.Add("2010");
            ddlYearBirth3.Items.Add("2011");
            ddlYearBirth3.Items.Add("2012");
            ddlYearBirth3.Items.Add("2013");
            ddlYearBirth3.Items.Add("2014");
            ddlYearBirth3.Items.Add("2015");
            ddlYearBirth3.Items.Add("2016");
            ddlYearBirth3.Items.Add("2017");
            ddlYearBirth3.Items.Add("2018");
            ddlYearBirth3.Items.Add("2019");
            ddlYearBirth3.Items.Add("2020");

            ddlGender.Items.Add("N/A");
            ddlGender.Items.Add("M");
            ddlGender.Items.Add("F");

            ddlTShirtSize.Items.Add("N/A");
            ddlTShirtSize.Items.Add("S (Child)");
            ddlTShirtSize.Items.Add("M (Child)");
            ddlTShirtSize.Items.Add("L (Child)");
            ddlTShirtSize.Items.Add("S (Adult)");
            ddlTShirtSize.Items.Add("M (Adult)");
            ddlTShirtSize.Items.Add("L (Adult)");
            ddlTShirtSize.Items.Add("XL (Adult)");
            ddlTShirtSize.Items.Add("2X (Adult)");
            ddlTShirtSize.Items.Add("3X (Adult)");
            ddlTShirtSize.Items.Add("4X (Adult)");

            ddlTextPhone.Items.Add("Yes");
            ddlTextPhone.Items.Add("No");
            ddlTextPhone.Items.Add("N/A");

            txtLastName.Enabled = true;
            txtFirstName.Enabled = true;
            txtAddress1.Enabled = true;
            txtCity.Enabled = true;
            txtState.Enabled = true;
            txtZip.Enabled = true;
            txtHomePhone.Enabled = true;
            txtStudentCellPhone.Enabled = true;
            txtStudentEmail.Enabled = true;
            //ddlSchool.Enabled = true;
            //ddlGrade.Enabled = true;
            //txtGrade.Enabled = true;
            ddlAge.Enabled = true;
            //txtAge.Enabled = true;
            ddlGender.Enabled = true;
            txtChurch.Enabled = true;
            //txtCareerGoal.Enabled = true;
            ddlTShirtSize.Enabled = true;
            //txbDiscipleshipMentor.Enabled = true;
            txbHealthConditions.Enabled = true;
            txbNotes.Enabled = true;
            //txbDMEstablishedStart.Enabled = true;
            lblDMEstablishedStartDate.Enabled = true;
            lblDMEstablishedStartDate.Visible = true;
            //txbSoloSong.Enabled = true;

            txtLastName.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtFirstName.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtAddress1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtCity.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtState.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtZip.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtHomePhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtStudentCellPhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtStudentEmail.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            //ddlSchool.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            //ddlGrade.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlAge.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            //txtDateBirth.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlGender.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtChurch.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            //txtCareerGoal.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlTShirtSize.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            //txbDiscipleshipMentor.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbHealthConditions.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbNotes.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            //txbDMEstablishedStart.BackColor
            //txbSoloSong.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");

            lblHealthConditions.Font.Size = 8;
            //lblVoicePart.Font.Size = 8;
            //Label.Font.Size = 8;
            //Label1.Font.Size = 8;
            Label3.Font.Size = 8;
            Label4.Font.Size = 8;
            Label5.Font.Size = 8;
            Label6.Font.Size = 8;
            Label2.Font.Size = 8;
            Label10.Font.Size = 8;
            Label7.Font.Size = 8;
            Label18.Font.Size = 8;
            //Label19.Font.Size = 8;
            Label20.Font.Size = 8;
            Label21.Font.Size = 8;
            Label24.Font.Size = 8;
            Label25.Font.Size = 8;
            //Label26.Font.Size = 8;
            Label28.Font.Size = 8;
            //Label29.Font.Size = 8;
            //Label30.Font.Size = 8;
            Label4.Font.Size = 8;
            Label8.Font.Size = 8;
            Label9.Font.Size = 8;
            lblDiscipleshipMentorNotes.Font.Size = 8;

            txtLastName.Font.Size = 10;
            txtFirstName.Font.Size = 10;
            txtAddress1.Font.Size = 10;
            //ddlSchool.Font.Size = 10;
            txtState.Font.Size = 10;
            txtStudentEmail.Font.Size = 10;
            ddlTShirtSize.Font.Size = 10;
            txtZip.Font.Size = 10;
            txtHomePhone.Font.Size = 10;
            //ddlGrade.Font.Size = 10;
            //txtGrade.Font.Size = 10;
            ddlAge.Font.Size = 10;
            ddlGender.Font.Size = 10;
            txtChurch.Font.Size = 10;
            txtStudentCellPhone.Font.Size = 10;
            //txtCareerGoal.Font.Size = 10;
            //txbDMEstablishedStart.Font.Size = 10;
            //txbSoloSong.Font.Size = 10;

            chbAddress.Font.Size = 8;
            chbAge.Font.Size = 8;
            //chbBibleOwnership.Font.Size = 8;
            chbAge.Font.Size = 8;
            chbCity.Font.Size = 8;
            chbDateBirth.Font.Size = 8;
            //chbGender.Font.Size = 8;
            chbHomePhone.Font.Size = 8;
            chbTShirtSize.Font.Size = 8;
            //chbSchool.Font.Size = 8;
            //chbGender.Font.Size = 8;
            chbStudentEmail.Font.Size = 8;
            chbStudentCellPhone.Font.Size = 8;
            chbFirstName.Font.Size = 8;
            chbLastName.Font.Size = 8;

            chbAddress.Checked = true;
            chbAge.Checked = true;
            //chbBibleOwnership.Checked = true;
            chbAge.Checked = true;
            chbCity.Checked = true;
            chbDateBirth.Checked = true;
            //chbGender.Checked = true;
            chbHomePhone.Checked = true;
            chbTShirtSize.Checked = true;
            //chbSchool.Checked = true;
            //chbGender.Checked = true;
            chbStudentEmail.Checked = true;
            chbStudentCellPhone.Checked = true;
            chbLastName.Checked = true;
            chbFirstName.Checked = true;

            chbAddress.Visible = false;
            chbAge.Visible = false;
            //chbBibleOwnership.Visible = false;
            chbAge.Visible = false;
            chbCity.Visible = false;
            chbDateBirth.Visible = false;
            //chbGender.Visible = false;
            chbHomePhone.Visible = false;
            chbTShirtSize.Visible = false;
            //chbSchool.Visible = false;
            //chbGender.Visible = false;
            chbStudentEmail.Visible = false;
            chbStudentCellPhone.Visible = false;
            chbLastName.Visible = false;
            chbFirstName.Visible = false;
            chbNotes.Visible = false;
            chbGrade.Visible = false;
            chbState.Visible = false;
            //ddlTextPhone.Visible = false;

            chbAddress.Height = 9;
            chbAge.Height = 9;
            //chbBibleOwnership.Height = 9;
            chbAge.Height = 9;
            chbCity.Height = 9;
            chbDateBirth.Height = 9;
            //chbGender.Height = 9;
            chbHomePhone.Height = 9;
            chbTShirtSize.Height = 9;
            //chbSchool.Height = 9;
            //chbGender.Height = 9;
            chbStudentEmail.Height = 9;
            chbStudentCellPhone.Height = 9;
            chbLastName.Height = 9;
            chbFirstName.Height = 9;

            chbAddress.Text = "(Edit)";
            chbCity.Text = "(Edit)";
            //chbGender.Text = "(Edit)";
            chbHomePhone.Text = "(Edit)";
            chbTShirtSize.Text = "(Edit)";
            //chbSchool.Text = "(Edit)";
            chbStudentEmail.Text = "(Edit)";
            chbStudentCellPhone.Text = "(Edit)";
            chbLastName.Text = "(Edit)";
            chbFirstName.Text = "(Edit)";

            chbVehichleInsurance.Attributes.Add("onclick", "return false;");
                        
            //Ryan C Manners..10/15/10.
            //Disable certain functionality based on login of person (staff member)
            if (Department == "Athletics")
            {
                chbMSHSChoir.Enabled = false;
                chbChildrensChoir.Enabled = false;
                chbPerformingArts.Enabled = false;
                chbShakes.Enabled = false;
                chbSingers.Enabled = false;
                lbMSHSChoir.Enabled = false;
                lbChildrensChoir.Enabled = false;
                lbSingers.Enabled = false;
                lbShakes.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;

                //Education department fields..
                chbSummerDayCamp.Enabled = false;
                chbSATPrepClass.Enabled = false;
                
                lbSummerDayCamp.Enabled = false;

                chbImpactUrbanSchoolsAcademics.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;
                chbImpactUrbanSchoolsPA.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;
                chbReadingProgram.Enabled = false;
                lbAcademicReadingSupport.Enabled = false;

                lbPerforingArts.Visible = false;
                //lbPerforingArts.Enabled = false;


                //chbMSHSChoir.Attributes.Add("onclick", "return false;");
                //chbChildrensChoir.Attributes.Add("onclick", "return false;");
                //chbPerformingArts.Attributes.Add("onclick", "return false;");
                //chbShakes.Attributes.Add("onclick", "return false;");
                //chbSingers.Attributes.Add("onclick", "return false;");
            }
            else if (Department == "PerformingArts")
            {
                chbMondayNights.Enabled = false;
                chb3on3Basketball.Enabled = false;
                chbBasketballTEAMS.Enabled = false;
                chbBoysOutreachBball.Enabled = false;
                chbSoccerInterMurals.Enabled = false;
                chbSoccerLgTravel.Enabled = false;
                chbGirlsOutreachBball.Enabled = false;
                chbMSBasketLeague.Enabled = false;
                chbLittleLeagueBaseball.Enabled = false;
                chbOliverFootballBible.Enabled = false;
                chbHSBasketLeague.Enabled = false;
                chbSpecialEvents.Enabled = false;
                lbMondayNights.Enabled = false;
                lb3on3Basketball.Enabled = false;
                lbBasketballTEAMS.Enabled = false;
                lbOutreachBasketball.Enabled = false;
                lbSoccerIntraMurals.Enabled = false;
                lbSoccerTEAMS.Enabled = false;
                lbMSBasketballLeague.Enabled = false;
                lbBaseball.Enabled = false;
                lbBibleStudy.Enabled = false;
                lbHSBasketballLeague.Enabled = false;
                lbSpecialEvents.Enabled = false;
                chbImpactUrbanSchoolsAcademics.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;
                chbImpactUrbanSchools.Enabled = false;
                lbImpactUrbanSchools.Enabled = false;
                chbReadingProgram.Enabled = false;
                lbAcademicReadingSupport.Enabled = false;
                lbSummerDayCamp.Enabled = false;

                


                //Handle the initial settings for the link buttons for Athletics
                //Programs..RCM..8/14/12.

                if (chbPerformingArts.Checked)
                {
                    lbPerforingArts.Enabled = true;
                }

                //Education department fields..
                chbSummerDayCamp.Enabled = false;
                chbSATPrepClass.Enabled = false;

                //chbMondayNights.Attributes.Add("onclick", "return false;");
                //chb3on3Basketball.Attributes.Add("onclick", "return false;");
                //chbBasketballTEAMS.Attributes.Add("onclick", "return false;");
                //chbBoysOutreachBball.Attributes.Add("onclick", "return false;");
                //chbSoccerInterMurals.Attributes.Add("onclick", "return false;");
                //chbSoccerLgTravel.Attributes.Add("onclick", "return false;");
                //chbGirlsOutreachBball.Attributes.Add("onclick", "return false;");
                //chbMSBasketLeague.Attributes.Add("onclick", "return false;");
                //chbLittleLeagueBaseball.Attributes.Add("onclick", "return false;");
                //chbOliverFootballBible.Attributes.Add("onclick", "return false;");
                //chbHSBasketLeague.Attributes.Add("onclick", "return false;");
            }
            else if (Department == "Education")
            {
                //PerformingArts fields..
                chbMSHSChoir.Enabled = false;
                chbChildrensChoir.Enabled = false;
                chbPerformingArts.Enabled = false;
                chbShakes.Enabled = false;
                chbSingers.Enabled = false;
                lbMSHSChoir.Enabled = false;
                lbChildrensChoir.Enabled = false;
                lbSingers.Enabled = false;
                lbShakes.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;

                //Athletics fields..
                chbMondayNights.Enabled = false;
                chb3on3Basketball.Enabled = false;
                chbBasketballTEAMS.Enabled = false;
                chbBoysOutreachBball.Enabled = false;
                chbSoccerInterMurals.Enabled = false;
                chbSoccerLgTravel.Enabled = false;
                chbGirlsOutreachBball.Enabled = false;
                chbMSBasketLeague.Enabled = false;
                chbLittleLeagueBaseball.Enabled = false;
                chbOliverFootballBible.Enabled = false;
                chbHSBasketLeague.Enabled = false;
                chbSpecialEvents.Enabled = false;
                lbMondayNights.Enabled = false;
                lb3on3Basketball.Enabled = false;
                lbBasketballTEAMS.Enabled = false;
                lbOutreachBasketball.Enabled = false;
                lbSoccerIntraMurals.Enabled = false;
                lbSoccerTEAMS.Enabled = false;
                lbMSBasketballLeague.Enabled = false;
                lbBaseball.Enabled = false;
                lbBibleStudy.Enabled = false;
                lbHSBasketballLeague.Enabled = false;
                lbSpecialEvents.Enabled = false;

                chbImpactUrbanSchools.Enabled = false;
                lbImpactUrbanSchools.Enabled = false;
                chbImpactUrbanSchoolsPA.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;

                //Handle the initial settings for the link buttons for Athletics
                //Programs..RCM..8/14/12.

                lbPerforingArts.Visible = false;
                lbPerforingArts.Enabled = false;
                
                //Education department fields..
                chbSummerDayCamp.Enabled = true;
                chbSATPrepClass.Enabled = true;
            }
            else if (Department == "BusinessOffice")
            {
                //PerformingArts fields..
                chbMSHSChoir.Enabled = false;
                chbChildrensChoir.Enabled = false;
                chbPerformingArts.Enabled = false;
                chbShakes.Enabled = false;
                chbSingers.Enabled = false;
                lbMSHSChoir.Enabled = false;
                lbChildrensChoir.Enabled = false;
                lbSingers.Enabled = false;
                lbShakes.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;

                //Athletics fields..
                chbMondayNights.Enabled = false;
                chb3on3Basketball.Enabled = false;
                chbBasketballTEAMS.Enabled = false;
                chbBoysOutreachBball.Enabled = false;
                chbSoccerInterMurals.Enabled = false;
                chbSoccerLgTravel.Enabled = false;
                chbGirlsOutreachBball.Enabled = false;
                chbMSBasketLeague.Enabled = false;
                chbLittleLeagueBaseball.Enabled = false;
                chbOliverFootballBible.Enabled = false;
                chbHSBasketLeague.Enabled = false;
                chbSpecialEvents.Enabled = false;
                lbMondayNights.Enabled = false;
                lb3on3Basketball.Enabled = false;
                lbBasketballTEAMS.Enabled = false;
                lbOutreachBasketball.Enabled = false;
                lbSoccerIntraMurals.Enabled = false;
                lbSoccerTEAMS.Enabled = false;
                lbMSBasketballLeague.Enabled = false;
                lbBaseball.Enabled = false;
                lbBibleStudy.Enabled = false;
                lbHSBasketballLeague.Enabled = false;
                lbSpecialEvents.Enabled = false;
                chbImpactUrbanSchoolsAcademics.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;
                chbImpactUrbanSchools.Enabled = false;
                lbImpactUrbanSchools.Enabled = false;
                chbImpactUrbanSchoolsPA.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;
                chbReadingProgram.Enabled = false;
                lbAcademicReadingSupport.Enabled = false;
                lbSummerDayCamp.Enabled = false;
                //Handle the initial settings for the link buttons for Athletics
                //Programs..RCM..8/14/12.

                //Education department fields..
                chbSummerDayCamp.Enabled = false;
                chbSATPrepClass.Enabled = false;

                lbPerforingArts.Visible = false;
                lbPerforingArts.Enabled = false;

                //chbMSHSChoir.Attributes.Add("onclick", "return false;");
                //chbChildrensChoir.Attributes.Add("onclick", "return false;");
                //chbPerformingArts.Attributes.Add("onclick", "return false;");
                //chbShakes.Attributes.Add("onclick", "return false;");
                //chbSingers.Attributes.Add("onclick", "return false;");

                //chbMondayNights.Attributes.Add("onclick", "return false;");
                //chb3on3Basketball.Attributes.Add("onclick", "return false;");
                //chbBasketballTEAMS.Attributes.Add("onclick", "return false;");
                //chbBoysOutreachBball.Attributes.Add("onclick", "return false;");
                //chbSoccerInterMurals.Attributes.Add("onclick", "return false;");
                //chbSoccerLgTravel.Attributes.Add("onclick", "return false;");
                //chbGirlsOutreachBball.Attributes.Add("onclick", "return false;");
                //chbMSBasketLeague.Attributes.Add("onclick", "return false;");
                //chbLittleLeagueBaseball.Attributes.Add("onclick", "return false;");
                //chbOliverFootballBible.Attributes.Add("onclick", "return false;");
                //chbHSBasketLeague.Attributes.Add("onclick", "return false;");
            }
            else
            {
                ////PerformingArts fields.
                chbMSHSChoir.Enabled = false;
                chbChildrensChoir.Enabled = false;
                chbPerformingArts.Enabled = false;
                chbShakes.Enabled = false;
                chbSingers.Enabled = false;
                lbMSHSChoir.Enabled = false;
                lbChildrensChoir.Enabled = false;
                lbSingers.Enabled = false;
                lbShakes.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;

                //Athletics fields..
                chbMondayNights.Enabled = false;
                chb3on3Basketball.Enabled = false;
                chbBasketballTEAMS.Enabled = false;
                chbBoysOutreachBball.Enabled = false;
                chbSoccerInterMurals.Enabled = false;
                chbSoccerLgTravel.Enabled = false;
                chbGirlsOutreachBball.Enabled = false;
                chbMSBasketLeague.Enabled = false;
                chbLittleLeagueBaseball.Enabled = false;
                chbOliverFootballBible.Enabled = false;
                chbHSBasketLeague.Enabled = false;
                chbSpecialEvents.Enabled = false;
                lbMondayNights.Enabled = false;
                lb3on3Basketball.Enabled = false;
                lbBasketballTEAMS.Enabled = false;
                lbOutreachBasketball.Enabled = false;
                lbSoccerIntraMurals.Enabled = false;
                lbSoccerTEAMS.Enabled = false;
                lbMSBasketballLeague.Enabled = false;
                lbBaseball.Enabled = false;
                lbBibleStudy.Enabled = false;
                lbHSBasketballLeague.Enabled = false;
                lbSpecialEvents.Enabled = false;
                chbImpactUrbanSchoolsAcademics.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;
                chbReadingProgram.Enabled = false;
                lbAcademicReadingSupport.Enabled = false;
                lbSummerDayCamp.Enabled = false;
                //Handle the initial settings for the link buttons for Athletics
                //Programs..RCM..8/14/12.

                lbPerforingArts.Visible = false;
                lbPerforingArts.Enabled = false;

                //Education department fields..
                chbSummerDayCamp.Enabled = false;
                chbSATPrepClass.Enabled = false;
                
                //chbMSHSChoir.Attributes.Add("onclick", "return false;");
                //chbChildrensChoir.Attributes.Add("onclick", "return false;");
                //chbPerformingArts.Attributes.Add("onclick", "return false;");
                //chbShakes.Attributes.Add("onclick", "return false;");
                //chbSingers.Attributes.Add("onclick", "return false;");

                //chbMondayNights.Attributes.Add("onclick", "return false;");
                //chb3on3Basketball.Attributes.Add("onclick", "return false;");
                //chbBasketballTEAMS.Attributes.Add("onclick", "return false;");
                //chbBoysOutreachBball.Attributes.Add("onclick", "return false;");
                //chbSoccerInterMurals.Attributes.Add("onclick", "return false;");
                //chbSoccerLgTravel.Attributes.Add("onclick", "return false;");
                //chbGirlsOutreachBball.Attributes.Add("onclick", "return false;");
                //chbMSBasketLeague.Attributes.Add("onclick", "return false;");
                //chbLittleLeagueBaseball.Attributes.Add("onclick", "return false;");
                //chbOliverFootballBible.Attributes.Add("onclick", "return false;");
                //chbHSBasketLeague.Attributes.Add("onclick", "return false;");
            }
            //chbSATPrepClass.Attributes.Add("onclick", "return false;");
        }

        protected void CleanCharacters()
        {
            txtLastName.Text = txtLastName.Text.Replace("'", "");
            //txbMiddleName.Text = txbMiddleName.Text.Replace("'", "");
            txtFirstName.Text = txtFirstName.Text.Replace("'", "");
            txtAddress1.Text = txtAddress1.Text.Replace("'", "");
            txtCity.Text = txtCity.Text.Replace("'", "");
            txtState.Text = txtState.Text.Replace("'", "");
            txtZip.Text = txtZip.Text.Replace("'", "");
            txtHomePhone.Text = txtHomePhone.Text.Replace("'", "");
            txtStudentEmail.Text = txtStudentEmail.Text.Replace("'", "");
            txtChurch.Text = txtChurch.Text.Replace("'", "");
            txbNotes.Text = txbNotes.Text.Replace("'", "");
            txbHealthConditions.Text = txbHealthConditions.Text.Replace("'", "");
            txtStudentCellPhone.Text = txtStudentCellPhone.Text.Replace("'", "");
            txbDiscipleshipMentorNotes.Text = txbDiscipleshipMentorNotes.Text.Replace("'", "");
        }



        protected void UpdateAllTables()
        {
            try
            {
                //con.Open();

                //BasketballTEAMS
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "BasketballTEAMSEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //SoccerTEAMS
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "SoccerTEAMSEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //SoccerIntraMurals
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "SoccerIntraMuralsEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //Baseball
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "BaseballEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //MSBasketballLeague
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "MSBasketballLeagueEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //HSBasketballLeague
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "HSBasketballLeagueEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //OutreachBasketballLeague
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "OutreachBasketballEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newvolunteer"] == "newvolunteer"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //biblestudy
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "biblestudyEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //----new ones..

                //mshschoir
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "mshschoirEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //childrenschoir
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "childrenschoirEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //shakes
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "shakesEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //Singers
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "SingersEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }


                //ImpactUrbanSchools
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "ImpactUrbanSchoolsEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }


                //SummerDayCamp
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "SummerDayCampEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //-------------
                //MondayNights
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "MondayNightsEnrollment";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //3on3Basketball
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "[3on3BasketballEnrollment]";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND volunteer = 1";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                        + " AND volunteer = 1";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //VolunteerDetails
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "[VolunteerDetails]";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    //if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "' , "
                        + " firstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND firstname = '" + txtFirstName.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "' , "
                        + " firstname = '" + txtFirstName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE lastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND firstname = '" + Request.QueryString["VolunteerFirstName"] + "' ";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //DiscipleshipMentor....
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "[DiscipleshipMentorAssignedMentors]";
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " discipleshipmentor = '" + txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE discipleshipmentor = '" + txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " discipleshipmentor = '" + txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE discipleshipmentor = '" + Request.QueryString["VolunteerFirstName"] + " " + Request.QueryString["VolunteerLastName"] + "' ";
                    }
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
                    lblInformation.Enabled = true;
                    lblInformation.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    //throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }

                //VolunteerProgramAttendance....???   Should or not?...RCM... A bit dangerous....

                //Options tables....???   Should or not?...RCM...
            }
            catch (Exception lkjlkjaa)
            {

            }
            finally
            {
                //con.Close();
            }
        }

        protected Boolean UpdateVolunteerTable()
        {
            try
            {
                string sqlUpdateStatement = "";
                string secondupdate = "";
                try
                {
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    {
                        sqlUpdateStatement = " UPDATE VolunteerInformation "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "' , "
                        + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                        + " address = '" + txtAddress1.Text.Trim() + "' , "
                        + " city = '" + txtCity.Text.Trim() + "' , "
                        + " state = '" + txtState.Text.Trim() + "' , "
                        + " zip = '" + txtZip.Text.Trim() + "' , "
                        + " homephone = '" + txtHomePhone.Text.Trim() + "' , "
                        + " cellphone = '" + txtStudentCellPhone.Text.Trim() + "' , "
                        + " email = '" + txtStudentEmail.Text.Trim() + "' , "
                        + " age = '" + ddlAge.Text.Trim() + "' , "
                        + " dob = '" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "' , "
                        + " sex = '" + ddlGender.Text.Trim() + "' , "
                        + " church = '" + ddlChurch.Text.Trim() + "' , "
                        + " healthconditions = '" + txbHealthConditions.Text.Trim() + "' , "
                        + " notes = '" + txbNotes.Text.Trim() + "' , "
                        + " tshirtsize = '" + ddlTShirtSize.Text.Trim() + "' , "
                        + " whenreceivedchrist = '1900-01-01'" + ","//when received christ.
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                        //-----------------------------------------------------------------------------------------------------------------------
                        + " emergencycontact = '" + txbEmergencyRelationship.Text.Trim() + "' , "
                        + " emergencyrelationship = '" + txbEmergRelationship.Text.Trim() + "' , "
                        + " emergencycontactphone = '" + txbEmergencyPhone.Text.Trim() + "' , "
                        //-----------------------------------------------------------------------------------------------                        
                        + " staff = " + Convert.ToInt32(chbStaff.Checked) + ", "
                        //--------------------------------------------------------------------------------------------------
                        + " mailinglistcodes = '" + ddlMailingListCodes.Text.Trim() + "' , "
                        + " mailinglistinclude = " + Convert.ToInt32(chbMailingList.Checked) + ", "
                        //---------------------------------------------------------------------------------------------------
                        + " mostrecentseason = '" + ddlMostRecentSeason.Text.Trim() + "', "
                        + " mostrecentseasonyear = '" + ddlMostRecentSeasonYear.Text.Trim() + "', "
                        //---------------------------------------------------------------------------------------------------
                        + " discipleshipmentorpotentials = " + Convert.ToInt32(chbDiscipleshipMentorPotentials.Checked) + ", "
                        + " officevolunteer = " + Convert.ToInt32(chbOfficeVolunteer.Checked) + ", "
                        + " discipleshipmentorwaitinglist = " + Convert.ToInt32(chbDiscipleshipmentorWaitingList.Checked) + ", "
                        + " discipleshipmentornotes = '" + txbDiscipleshipMentorNotes.Text.Trim() + "' , "
                        + " discipleshipmentortraining = " + Convert.ToInt32(chbDiscipleshipmentorTrainingDone.Checked) + ", "
                        + " discipleshipmentorparticipation = " + Convert.ToInt32(chbDiscipleshipmentorTraining.Checked) + ", "
                        + " discipleshipmentortraineddate = '" + ddlMonthBirth2.Text.Trim() + "-" + ddlDayBirth2.Text.Trim() + "-" + ddlYearBirth2.Text.Trim() + "', "
                        + " discipleshipmentorstartdate = '" + ddlMonthBirth3.Text.Trim() + "-" + ddlDayBirth3.Text.Trim() + "-" + ddlYearBirth3.Text.Trim() + "'  "
                        + "  "
                        + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND firstname = '" + txtFirstName.Text.Trim() + "' ";

                        // Ryan C Manners..4/26/12.
                        // These fields are now being maintained in a different table named "VolunteerDetails"..
                        secondupdate = "UPDATE VolunteerDetails "
                                               + "SET spiritualjourney = " + Convert.ToInt32(chbSpiritualJourney.Checked) + ", "
                                               + " vehichleinsurance = " + Convert.ToInt32(chbVehichleInsurance.Checked) + ", "
                                               + " releasewaiver = " + Convert.ToInt32(chbReleaseWaiver.Checked) + ", "
                                               + " generalinformation = " + Convert.ToInt32(chbGeneralInformation.Checked) + ", "
                                               + " newvolunteertraining = " + Convert.ToInt32(chbNewVolunteerTraining.Checked) + " "
                                               + "  "
                                               + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                                               + " AND firstname = '" + txtFirstName.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE VolunteerInformation "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "' , "
                        + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                        + " address = '" + txtAddress1.Text.Trim() + "' , "
                        + " city = '" + txtCity.Text.Trim() + "' , "
                        + " state = '" + txtState.Text.Trim() + "' , "
                        + " zip = '" + txtZip.Text.Trim() + "' , "
                        + " homephone = '" + txtHomePhone.Text.Trim() + "' , "
                        + " cellphone = '" + txtStudentCellPhone.Text.Trim() + "' , "
                        + " email = '" + txtStudentEmail.Text.Trim() + "' , "
                        + " age = '" + ddlAge.Text.Trim() + "' , "
                        + " dob = '" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "' , "
                        + " sex = '" + ddlGender.Text.Trim() + "' , "
                        + " church = '" + ddlChurch.Text.Trim() + "' , "
                        + " healthconditions = '" + txbHealthConditions.Text.Trim() + "' , "
                        + " notes = '" + txbNotes.Text.Trim() + "' , "
                        + " tshirtsize = '" + ddlTShirtSize.Text.Trim() + "' , "
                        + " whenreceivedchrist = '1900-01-01'" + ","//when received christ.
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                        //-----------------------------------------------------------------------------------------------------------------------
                        + " emergencycontact = '" + txbEmergencyRelationship.Text.Trim() + "', "
                        + " emergencyrelationship = '" + txbEmergRelationship.Text.Trim() + "', "
                        + " emergencycontactphone = '" + txbEmergencyPhone.Text.Trim() + "', "
                        //-----------------------------------------------------------------------------------------------                        
                        + " staff = " + Convert.ToInt32(chbStaff.Checked) + ", "
                        //--------------------------------------------------------------------------------------------------
                        + " mailinglistcodes = '" + ddlMailingListCodes.Text.Trim() + "' , "
                        + " mailinglistinclude = " + Convert.ToInt32(chbMailingList.Checked) + ", "
                        //---------------------------------------------------------------------------------------------------
                        + " mostrecentseason = '" + ddlMostRecentSeason.Text.Trim() + "', "
                        + " mostrecentseasonyear = '" + ddlMostRecentSeasonYear.Text.Trim() + "', "
                        //---------------------------------------------------------------------------------------------------
                        + " discipleshipmentorpotentials = " + Convert.ToInt32(chbDiscipleshipMentorPotentials.Checked) + ", "
                        + " officevolunteer = " + Convert.ToInt32(chbOfficeVolunteer.Checked) + ", "
                        + " discipleshipmentorwaitinglist = " + Convert.ToInt32(chbDiscipleshipmentorWaitingList.Checked) + ", "
                        + " discipleshipmentornotes = '" + txbDiscipleshipMentorNotes.Text.Trim() + "' , "
                        + " discipleshipmentortraining = " + Convert.ToInt32(chbDiscipleshipmentorTrainingDone.Checked) + ", "
                        + " discipleshipmentorparticipation = " + Convert.ToInt32(chbDiscipleshipmentorTraining.Checked) + ", "
                        + " discipleshipmentortraineddate = '" + ddlMonthBirth2.Text.Trim() + "-" + ddlDayBirth2.Text.Trim() + "-" + ddlYearBirth2.Text.Trim() + "', "
                        + " discipleshipmentorstartdate = '" + ddlMonthBirth3.Text.Trim() + "-" + ddlDayBirth3.Text.Trim() + "-" + ddlYearBirth3.Text.Trim() + "'  "
                        + "  "
                        + " WHERE lastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                        + " AND firstname = '" + Request.QueryString["VolunteerFirstName"] + "' ";

                        // Ryan C Manners..4/26/12.
                        // These fields are now being maintained in a different table named "VolunteerDetails"..
                        secondupdate = "UPDATE VolunteerDetails "
                                               + "SET spiritualjourney = " + Convert.ToInt32(chbSpiritualJourney.Checked) + ", "
                                               + " vehichleinsurance = " + Convert.ToInt32(chbVehichleInsurance.Checked) + ", "
                                               + " releasewaiver = " + Convert.ToInt32(chbReleaseWaiver.Checked) + ", "
                                               + " generalinformation = " + Convert.ToInt32(chbGeneralInformation.Checked) + ", "
                                               + " newvolunteertraining = " + Convert.ToInt32(chbNewVolunteerTraining.Checked) + " "
                                               + "  "
                                               + " WHERE lastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                               + " AND firstname = '" + Request.QueryString["VolunteerFirstName"] + "' ";
                    }

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

                    //create a SQL command to update record
                    SqlCommand sqlUpdateCommand2 = new SqlCommand(secondupdate, con);
                    if (sqlUpdateCommand2.ExecuteNonQuery() > 0)
                    {
                    }
                    else
                    {
                        //Didn't find a record to update..RCM..
                    }
                    UpdateAllTables();
                }
                catch (Exception lkjaaa)
                {
                    //lblInformation.Enabled = true;
                    //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                }

                string sqlUpdateStatement_ProgramsList = "";
                try
                {
                    //Update the ParentGuardian information...RCM..10/7/10.
                    if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                    {
                        sqlUpdateStatement_ProgramsList = " UPDATE ProgramsList " +
                            "SET "
                            + " lastname = '" + txtLastName.Text.Trim() + "' , "
                            + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                            //PerformingArts fields..RCM..--------------------------------------------------------------
                            + " mshschoir = " + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                            + " performingarts = " + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                            + " childrenschoir = " + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                            + " shakes = " + Convert.ToInt32(chbShakes.Checked) + ", "
                            + " singers = " + Convert.ToInt32(chbSingers.Checked) + ", "
                            //Athletic fields..RCM..--------------------------------------------------------------------
                            + " OutreachBasketBall = " + Convert.ToInt32(chbBoysOutreachBball.Checked) + ", "
                            + " mondaynights = " + Convert.ToInt32(chbMondayNights.Checked) + ", "
                            + " [3on3basketball] = " + Convert.ToInt32(chb3on3Basketball.Checked) + ", "
                            + " BasketballTEAMS = " + Convert.ToInt32(chbBasketballTEAMS.Checked) + ", "
                            + " SoccerIntraMurals = " + Convert.ToInt32(chbSoccerInterMurals.Checked) + ", "
                            + " SoccerTEAMS = " + Convert.ToInt32(chbSoccerLgTravel.Checked) + ", "
                            + " Baseball = " + Convert.ToInt32(chbLittleLeagueBaseball.Checked) + ", "
                            + " biblestudy = " + Convert.ToInt32(chbOliverFootballBible.Checked) + ", "
                            + " HSBasketballLg = " + Convert.ToInt32(chbHSBasketLeague.Checked) + ", "
                            + " MSBasketballLg = " + Convert.ToInt32(chbMSBasketLeague.Checked) + ", "
                            //-----------------------------------------------------------------------------------------
                            //Education fields..RCM.
                            + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                            //----------------------------------------------------------------------------------------
                            + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                            + " academicreadingsupport = " + Convert.ToInt32(chbReadingProgram.Checked) + ", "
                            //--------------------------------------------------------------------
                            //+ " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchoolsPA.Checked) + ", "
                            //--------------------------------------------------------------------
                            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                            + " "
                            + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                            + " AND firstname = '" + txtFirstName.Text.Trim() + "' "
                            + " AND staffvolunteer = 1 ";
                    }
                    else
                    {
                        if (Department == "Athletics")
                        {
                            sqlUpdateStatement_ProgramsList = " UPDATE ProgramsList " +
                                "SET "
                                + " lastname = '" + txtLastName.Text.Trim() + "' , "
                                + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                //PerformingArts fields..RCM..--------------------------------------------------------------
                                + " mshschoir = " + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                + " performingarts = " + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                + " childrenschoir = " + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                + " shakes = " + Convert.ToInt32(chbShakes.Checked) + ", "
                                + " singers = " + Convert.ToInt32(chbSingers.Checked) + ", "
                                //Athletic fields..RCM..--------------------------------------------------------------------
                                + " OutreachBasketBall = " + Convert.ToInt32(chbBoysOutreachBball.Checked) + ", "
                                + " mondaynights = " + Convert.ToInt32(chbMondayNights.Checked) + ", "
                                + " [3on3basketball] = " + Convert.ToInt32(chb3on3Basketball.Checked) + ", "
                                + " BasketballTEAMS = " + Convert.ToInt32(chbBasketballTEAMS.Checked) + ", "
                                + " SoccerIntraMurals = " + Convert.ToInt32(chbSoccerInterMurals.Checked) + ", "
                                + " SoccerTEAMS = " + Convert.ToInt32(chbSoccerLgTravel.Checked) + ", "
                                + " Baseball = " + Convert.ToInt32(chbLittleLeagueBaseball.Checked) + ", "
                                + " biblestudy = " + Convert.ToInt32(chbOliverFootballBible.Checked) + ", "
                                + " HSBasketballLg = " + Convert.ToInt32(chbHSBasketLeague.Checked) + ", "
                                + " MSBasketballLg = " + Convert.ToInt32(chbMSBasketLeague.Checked) + ", "
                                //---------------------------------------------------------------------------------------------
                                //Education fields..RCM.
                                + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                //--------------------------------------------------------------------
                                + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                + " academicreadingsupport = " + Convert.ToInt32(chbReadingProgram.Checked) + ", "
                                //-------------------------------------------------------------------------------------------
                                + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchools.Checked) + ", "
                                //--------------------------------------------------------------------
                                + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                + " "
                                + " WHERE lastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                + " AND firstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                                + " AND staffvolunteer = 1 ";
                        }
                        else if (Department == "PerformingArts")
                        {
                            sqlUpdateStatement_ProgramsList = " UPDATE ProgramsList " +
                                "SET "
                                + " lastname = '" + txtLastName.Text.Trim() + "' , "
                                + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                //PerformingArts fields..RCM..--------------------------------------------------------------
                                + " mshschoir = " + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                + " performingarts = " + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                + " childrenschoir = " + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                + " shakes = " + Convert.ToInt32(chbShakes.Checked) + ", "
                                + " singers = " + Convert.ToInt32(chbSingers.Checked) + ", "
                                //Athletic fields..RCM..--------------------------------------------------------------------
                                + " OutreachBasketBall = " + Convert.ToInt32(chbBoysOutreachBball.Checked) + ", "
                                + " mondaynights = " + Convert.ToInt32(chbMondayNights.Checked) + ", "
                                + " [3on3basketball] = " + Convert.ToInt32(chb3on3Basketball.Checked) + ", "
                                + " BasketballTEAMS = " + Convert.ToInt32(chbBasketballTEAMS.Checked) + ", "
                                + " SoccerIntraMurals = " + Convert.ToInt32(chbSoccerInterMurals.Checked) + ", "
                                + " SoccerTEAMS = " + Convert.ToInt32(chbSoccerLgTravel.Checked) + ", "
                                + " Baseball = " + Convert.ToInt32(chbLittleLeagueBaseball.Checked) + ", "
                                + " biblestudy = " + Convert.ToInt32(chbOliverFootballBible.Checked) + ", "
                                + " HSBasketballLg = " + Convert.ToInt32(chbHSBasketLeague.Checked) + ", "
                                + " MSBasketballLg = " + Convert.ToInt32(chbMSBasketLeague.Checked) + ", "
                                //---------------------------------------------------------------------------------------------
                                //Education fields..RCM.
                                + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                + " academicreadingsupport = " + Convert.ToInt32(chbReadingProgram.Checked) + ", "
                                //--------------------------------------------------------------------
                                + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                //-------------------------------------------------------------------------------------------
                                + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchoolsPA.Checked) + ", "
                                //--------------------------------------------------------------------
                                + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                + " "
                                + " WHERE lastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                + " AND firstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                                + " AND staffvolunteer = 1 ";
                        }
                        else if (Department == "Education")
                        {
                            sqlUpdateStatement_ProgramsList = " UPDATE ProgramsList " +
                                "SET "
                                + " lastname = '" + txtLastName.Text.Trim() + "' , "
                                + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                //PerformingArts fields..RCM..--------------------------------------------------------------
                                + " mshschoir = " + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                + " performingarts = " + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                + " childrenschoir = " + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                + " shakes = " + Convert.ToInt32(chbShakes.Checked) + ", "
                                + " singers = " + Convert.ToInt32(chbSingers.Checked) + ", "
                                //Athletic fields..RCM..--------------------------------------------------------------------
                                + " OutreachBasketBall = " + Convert.ToInt32(chbBoysOutreachBball.Checked) + ", "
                                + " mondaynights = " + Convert.ToInt32(chbMondayNights.Checked) + ", "
                                + " [3on3basketball] = " + Convert.ToInt32(chb3on3Basketball.Checked) + ", "
                                + " BasketballTEAMS = " + Convert.ToInt32(chbBasketballTEAMS.Checked) + ", "
                                + " SoccerIntraMurals = " + Convert.ToInt32(chbSoccerInterMurals.Checked) + ", "
                                + " SoccerTEAMS = " + Convert.ToInt32(chbSoccerLgTravel.Checked) + ", "
                                + " Baseball = " + Convert.ToInt32(chbLittleLeagueBaseball.Checked) + ", "
                                + " biblestudy = " + Convert.ToInt32(chbOliverFootballBible.Checked) + ", "
                                + " HSBasketballLg = " + Convert.ToInt32(chbHSBasketLeague.Checked) + ", "
                                + " MSBasketballLg = " + Convert.ToInt32(chbMSBasketLeague.Checked) + ", "
                                //---------------------------------------------------------------------------------------------
                                //Education fields..RCM.
                                + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                + " academicreadingsupport = " + Convert.ToInt32(chbReadingProgram.Checked) + ", "
                                //--------------------------------------------------------------------
                                + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                //-------------------------------------------------------------------------------------------
                                + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                //--------------------------------------------------------------------
                                + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                + " "
                                + " WHERE lastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                + " AND firstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                                + " AND staffvolunteer = 1 ";
                        }
                    }
                    //create a SQL command to update record
                    SqlCommand sqlUpdateCommand_ProgramsList = new SqlCommand(sqlUpdateStatement_ProgramsList, con);
                    if (sqlUpdateCommand_ProgramsList.ExecuteNonQuery() > 0)
                    {
                    }
                    else
                    {
                        //Didn't find a record to update..RCM.
                    }
                }
                catch (Exception lkjlk)
                {
                    //lblInformation.Enabled = true;
                    //lblInformation.Text = "The update to ProgramsList table failed.  Please fix and try again MSG: " + lkjlk.Message.ToString();
                }

                UpdateAllTables();//Keeps the tables primary keys in synch.
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
            return true;
        }
        
        protected void btnSubmitInformation_Click(object sender, EventArgs e)
        {
            bool GoodUpdate = false;
            GoodUpdate = UpdateVolunteerTable();
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void cmbNewPerson_Click(object sender, EventArgs e)
        {
            //Here we want to capture all the information from the various different fields and
            //use them to update the data within the database table..RCM..8/3/10.

            //Collects the various different pieces of information.

            string lk = Request.QueryString["StudentLastName"];
            string aa = Request.QueryString["StudentFirstName"];

            try
            {                

                string sqlInsertStatement = "";

                //sqlInsertStatement = "INSERT into PerformingArtsAcademyStudents "
                //    + "values ( "
                //    + "'6F9619FF-8B86-D011-B42D-00C04FC964FF', "
                //    + "'" + txtLastName.Text.Trim() + "',"
                //    + "'" + txtFirstName.Text.Trim() + "',"
                //    + "'" + txtAddress1.Text.Trim() + "',"
                //    + "'" + txtCity.Text.Trim() + "',"
                //    + "'" + txtState.Text.Trim() + "',"
                //    + "'" + txtZip.Text.Trim() + "',"
                //    + "'" + txtHomePhone.Text.Trim() + "',"
                //    + "'" + txtStudentCellPhone.Text.Trim() + "',"
                //    //+ Convert.ToInt32(ddlTextPhone.Text) + ","
                //    + "'" + txtStudentEmail.Text.Trim() + "',"
                //    + Convert.ToInt32(chbMSHSChoir.Checked) + ","
                //    + Convert.ToInt32(chbPerformingArts.Checked) + ","
                //    + "'" + ddlSchool.Text.Trim() + "',"
                //    + "'" + ddlGrade.Text.Trim() + "',"
                //    + "'" + ddlAge.Text.Trim() + "',"
                //    //+ "'" + txtDateBirth.Text.Trim() + "',"
                //    + "'" + ddlGender.Text.Trim() + "',"
                //    + "'" + txtChurch.Text.Trim() + "',"
                //    + "'" + txtCareerGoal.Text.Trim() + "',"
                //    + "'" + txbHealthConditions.Text.Trim() + "',"
                //    + "'" + txbNotes.Text.Trim() + "',"
                //    + "'" + ddlTShirtSize.Text.Trim() + "',"
                //    + Convert.ToInt32(chbMeetCCGF.Checked) + ","
                //    + "'" + txbDiscipleshipMentor.Text.Trim() + "',"
                //    + Convert.ToInt32(chbSoloist.Checked) + ","
                //    + "'" + txbSoloSong.Text.Trim() + "',"
                //    + Convert.ToInt32(chbDance.Checked) + ","
                //    + "'1900-01-01'" + ","//danceyear
                //    + "'" + txtCareerGoal.Text.Trim() + "',"//schoolform2
                //    + Convert.ToInt32(chbBibleOwnership.Checked) + ","
                //    + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ","
                //    + "'picture',"//PictureIdentification.
                //    + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ","
                //    + "'1900-01-01'" + ","//when received christ.
                //    + "'" + System.DateTime.Now.ToString() + "',"
                //    + "'" + System.DateTime.Now.ToString() + "',"
                //    + Convert.ToInt32(chbBackgroundCheck.Checked) + ","
                //    + Convert.ToInt32(chbSpiritualJourney.Checked) + ","
                //    + Convert.ToInt32(chbVehichleInsurance.Checked) + ","
                //    + Convert.ToInt32(chbReleaseWaiver.Checked) + ","
                //    + Convert.ToInt32(chbGeneralInformation.Checked) + ","
                //    + Convert.ToInt32(chbNewVolunteerTraining.Checked) + ","
                //    + Convert.ToInt32(txbID.Text.Trim()) + ","
                //    + "'" + ddlVoicePart.Text.Trim() + "')";
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                if (sqlInsertCommand.ExecuteNonQuery() > 0)
                {
                    //maybe display a message confirming update has been successful
                }
                else
                {
                    //Didn't find a record to update..RCM..
                }
            }
            catch (Exception ex)
            {
                //lblInformation.Enabled = true;
                //lblInformation.Text = "There was an exception INSERTING NEW data into the tables..  Please fix and try again MSG: " + ex.Message.ToString();                
                
                //add code to send error to admin via email
                //Session["Exception"] = ex.Message.ToString();
                //Response.Redirect("Error.aspx");
            }
            finally
            {
                //Close connection
                con.Close();
                con.Dispose();
            }
        }

        protected void rbRegistrationForm_CheckedChanged1(object sender, EventArgs e)
        {

        }

        protected void txbNotes_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txbHealthConditions_TextChanged(object sender, EventArgs e)
        {

        }

        protected void rbStudentChoirQuestForm_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbRetreatForm_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbParentalConsent_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbMeetCCGF_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbSoloist_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbDance_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbBibleOwnership_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbBibleStudyParticipation_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbHaveReceivedChrist_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbElementaryChoir_CheckedChanged(object sender, EventArgs e)
        {
           // rbElementaryChoir.Checked = false;

        }

        protected void rbPerformingArtsProgram_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbMSHSChoirProgram_CheckedChanged(object sender, EventArgs e)
        {

        
        }

        protected void txbDiscipleshipMentor_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txbSoloSong_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chbChildrensChoir_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbChildrensChoir.Checked)
            {
                cmbChildrensChoirConfirm.Visible = true;
                cmbChildrensChoirConfirm.Style.Add("z-index", "99999");

                cmbChildrensChoirCancel.Enabled = true;
                cmbChildrensChoirCancel.Visible = true;
                cmbChildrensChoirCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("ChildrensChoir");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbChildrensChoir.Checked)
                {
                    cmbChildrensChoirRemove.Visible = true;
                    cmbChildrensChoirRemove.Style.Add("z-index", "99999");

                    cmbChildrensChoirCancel.Enabled = true;
                    cmbChildrensChoirCancel.Visible = true;
                    cmbChildrensChoirCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("ChildrensChoir");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbMSHSChoir_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbMSHSChoir.Checked)
            {
                cmbMSHSChoirConfirm.Visible = true;
                cmbMSHSChoirConfirm.Style.Add("z-index", "99999");

                cmbMSHSChoirCancel.Enabled = true;
                cmbMSHSChoirCancel.Visible = true;
                cmbMSHSChoirCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("MSHSChoir");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbMSHSChoir.Checked)
                {
                    cmbMSHSChoirRemove.Visible = true;
                    cmbMSHSChoirRemove.Style.Add("z-index", "99999");

                    cmbMSHSChoirCancel.Enabled = true;
                    cmbMSHSChoirCancel.Visible = true;
                    cmbMSHSChoirCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("MSHSChoir");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbPerformingArts_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPerformingArts.Checked)
            {
                lbPerforingArts.Enabled = true;

                bool ValidUpdate = UpdateVolunteerTable();

                Response.Redirect("VolunteerProgramMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + txtLastName.Text + "&VolunteerFirstName=" + txtFirstName.Text + "&Dept=" + Request.QueryString["Dept"] + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
            }
            else
            {
                if (!chbPerformingArts.Checked)
                {
                    lbPerforingArts.Enabled = false;

                    //Message("Danger!  This will remove this volunteer from Academy Class Enrollment.  Do you wish to proceed?  ", this);
                    //Message99(sender, this);   ///   Danger!   This will remove students from class enrollemnt.  Do you wish to proceed..

                    cmbDeletePerformingArts.Enabled = true;
                    cmbDeletePerformingArts.Visible = true;
                    cmbDeletePerformingArts.Style.Add("z-index", "99999");

                    cmbCancelPerformArts.Enabled = true;
                    cmbCancelPerformArts.Visible = true;
                    cmbCancelPerformArts.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will REMOVE the individual from this program!";

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
            //-------------
            if (chbNewVolunteerFlag.Checked == true)
            {
                //Do Nothing.
            }
            else
            {
                //if not a new person, update automatically...RCM..
                bool ValidUpdate = UpdateVolunteerTable();
            }

            //-------------------------------------------------
            //Ryan C Manners..9/19/11.
            //if (chbPerformingArts.Checked)
            //{
            //    lbPerforingArts.Enabled = true;
            //}
            //else
            //{
            //    if (!chbPerformingArts.Checked)
            //    {
            //        lbPerforingArts.Enabled = false;

            //        //Message("Danger!  This will remove this volunteer from Academy Class Enrollment.  Do you wish to proceed?  ", this);
            //        //Message99(sender, this);   ///   Danger!   This will remove students from class enrollemnt.  Do you wish to proceed..

            //        cmbDeletePerformingArts.Enabled = true;
            //        cmbDeletePerformingArts.Visible = true;
            //        cmbDeletePerformingArts.Style.Add("z-index", "99999");

            //        cmbCancelPerformArts.Enabled = true;
            //        cmbCancelPerformArts.Visible = true;
            //        cmbCancelPerformArts.Style.Add("z-index", "99999");

            //        lblProgramManagement.Style.Add("z-index", "99999");
            //        lblProgramManagement.Visible = true;
            //        lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

            //        pnlProgramManagement.Style.Add("z-index", "9999");
            //        pnlProgramManagement.Visible = true;
            //    }
            //}

            //if (chbNewVolunteerFlag.Checked == true)
            //{
            //    //Do Nothing.
            //}
            //else
            //{
            //    //lbPerforingArts.Enabled = false;
            //    bool ValidUpdate = UpdateVolunteerTable();
            //}
        }

        protected void RemoveVolunteersFromClasses()
        {
            //Remove volunteer from classes..   
            string sql_DeleteFromClass2 = "";
            try
            {
                if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                {
                    sql_DeleteFromClass2 = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyClassEnrollment "
                                         + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                         + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                         + "AND volunteer = 1 ";
                }
                else
                {
                    sql_DeleteFromClass2 = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyClassEnrollment "
                                         + " WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                         + " AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                                         + " AND volunteer = 1 ";
                }
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromClass2 = new SqlCommand(sql_DeleteFromClass2, con);
                if (sqlDeleteFromClass2.ExecuteNonQuery() > 0)
                {
                    //cmbNewEnrollment2.Text = "Class2 New Enrollment";
                    //ddlClass2.Text = " ";
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

        //public static void Message2(String message, Control cntrl) { ScriptManager.RegisterStartupScript(cntrl, cntrl.GetType(), "alert", "alert('" + message + "');", true); }
        public static void Message(String message, Control cntrl)
        {
            //ScriptManager.RegisterStartupScript(cntrl, cntrl., "alert", "alert('" + message + "');", true);
            
            //ScriptManager.RegisterStartupScript(cntrl, Message,"alert", "message this",true);

            //ScriptManager.RegisterStartupScript(cntrl, cntrl.GetType(), "alert", "alert('" + message + "');", false);
            
            //ScriptManager.RegisterStartupScript(
        }


        //private void btnmsgbox_Click(object sender, EventArgs e)
        //{
        //    DialogResult result = MessageBox.Show("Select yes or No.", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
 
        //    if (result == DialogResult.Yes)
        //    {
        //        MessageBox.Show("You chose Yes.");
        //    } 
        //    else if (result == DialogResult.No)
        //    {
        //        MessageBox.Show("You chose No.");
        //    }
        //}

        protected void Message99(object sender, EventArgs e)    
        {        //Do Something        
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "ConfirmMessage();", true);    
        }    
    //    protected void btnSaveHidden_Click(object sender, EventArgs e)    {        //Do Something        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "alert('You clicked Hidden Save');", true);    }    protected void btnCancelHidden_Click(object sender, EventArgs e)    {        //Do Something        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "alert('You clicked Hidden Cancel');", true);    }

        protected void Message2(object sender, EventArgs e)    {        //Do Something        
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "ConfirmMessage();", true);    }    
        protected void Message3(object sender, EventArgs e)    {        //Do Something        
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "alert('You clicked Hidden Save');", true);    }    
        protected void Message4(object sender, EventArgs e)    {        //Do Something       
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "alert('You clicked Hidden Cancel');", true);    }

        protected void chbParentalConsentForm_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbRetreatForm_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbStudentQuestionareForm_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void cmbReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("volunteers.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }


        protected void NewStudent()
        {
            //Blank out the volunteer information..RCM..1/16/11.
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtAddress1.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            txtHomePhone.Text = "";
            txtStudentCellPhone.Text = "";
            txtStudentEmail.Text = "";
            //ddlSchool.Text = "";
            //ddlGrade.ClearSelection();
            ddlAge.ClearSelection();
            ddlDayBirth.ClearSelection();
            ddlMonthBirth.ClearSelection();
            ddlYearBirth.ClearSelection();
            ddlGender.ClearSelection();
            txtChurch.Text = "";
            //txtCareerGoal.Text = "";
            ddlTShirtSize.ClearSelection();
            //txbDiscipleshipMentor.Text = "";
            txbHealthConditions.Text = "";
            txbNotes.Text = "";
            //txbSoloSong.Text = "";
            //txbDMEstablishedStart.Text = "";
            txbID.Text = "";
            //ddlVoicePart.ClearSelection();
            chbMSHSChoir.Checked = false;
            chbChildrensChoir.Checked = false;
            chbPerformingArts.Checked = false;
            //chbDiscipleshipmentorparticipation.Checked = false;
            chbDiscipleshipmentorTraining.Checked = false;
            //txbDMEstablishedStart.Text = "";
            //txbDiscipleshipmentorTrainedDate.Text = "";

            //chbRegistrationForm.Checked = false;
            //chbRetreatForm.Checked = false;
            //chbParentalConsentForm.Checked = false;
            //chbStudentQuestionareForm.Checked = false;

            //Blank out ParentGuardian information...RCM..1/16/11.
            txbParentGuardian1Email.Text = "";
            ddlParentGuardian1Relationship.Text = "N/A";
            txbParentGuardian1WrkPh.Text = "";
            txbParentGuardian1CellPhone.Text = "";
            txtParentGuardian1.Text = "";
            txbParentGuardian2.Text = "";
            txbParentGuardian2Email.Text = "";
            ddlParentGuardian2Relationship.Text = "N/A";
            txbParentGuardian2WrkPh.Text = "";
            txbParentGuardian2CellPhone.Text = "";

            //Blank out the EmergencyContact information..RCM..1/16/11.
            txbEmergencyPhone.Text = "";
            txbEmergRelationship.Text = "";
            txbEmergencyRelationship.Text = "";

            imgPicture.ImageUrl = "";
            //lbClassesEnrollment.Enabled = false;
            btnSubmitInformation.Enabled = false;
            cmbNewPerson2.Enabled = true;
            //btnNewPerson1.Enabled = true;
        }

        protected void cmbClearPage_Click(object sender, EventArgs e)
        {

            Response.Redirect("VolunteerInformation.aspx?Security=Good&newvolunteer=newvolunteer" + "&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + "&VolunteerFirstName=" + "&Dept=" + Request.QueryString["Dept"]);

            //Response.Redirect("VolunteerInformation.aspx?Security=Good&newvolunteer=newvolunteer" + "&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);

            //StartingSettings();
            //NewStudent();
        }

        protected void lbClassesEnrollment_Click(object sender, EventArgs e)
        {
            //Response.Redirect("PerformingArtsClasses.aspx?    "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
            Response.Redirect("PerformingArtsClasses.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
        }


        protected void lbChildrensAttendance_Click(object sender, EventArgs e)
        {
            //Response.Redirect("StudentAttendanceOptions.aspx");
            Response.Redirect("StudentAttendanceOptions.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void lbMSHSChoir_Click(object sender, EventArgs e)
        {
            //Response.Redirect("uifadmin2.aspx");
            Response.Redirect("StudentAttendanceOptions.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
            //Response.Redirect("uifadmin2.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
        }

        protected void chbParentGuardianEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (chbParentGuardianEdit.Checked)
            {
                txbParentGuardian1CellPhone.Enabled = false;
                txbParentGuardian1Email.Enabled = false;
                ddlParentGuardian1Relationship.Enabled = false;
                txbParentGuardian1WrkPh.Enabled = false;
                txtParentGuardian1.Enabled = false;
                txbParentGuardian2.Enabled = false;
                txbParentGuardian2CellPhone.Enabled = false;
                txbParentGuardian2Email.Enabled = false;
                ddlParentGuardian2Relationship.Enabled = false;
                txbParentGuardian2WrkPh.Enabled = false;
            }
            else
            {
                txbParentGuardian1CellPhone.Enabled = true;
                txbParentGuardian1Email.Enabled = true;
                ddlParentGuardian1Relationship.Enabled = true;
                txbParentGuardian1WrkPh.Enabled = true;
                txtParentGuardian1.Enabled = true;
                txbParentGuardian2.Enabled = true;
                txbParentGuardian2CellPhone.Enabled = true;
                txbParentGuardian2Email.Enabled = true;
                ddlParentGuardian2Relationship.Enabled = true;
                txbParentGuardian2WrkPh.Enabled = true;
            }
        }

        protected void chbEmergencyContactEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEmergencyContactEdit.Checked)
            {
                txbEmergencyPhone.Enabled = false;
                txbEmergencyRelationship.Enabled = false;
                txbEmergRelationship.Enabled = false;
            }
            else
            {
                txbEmergencyPhone.Enabled = true;
                txbEmergencyRelationship.Enabled = true;
                txbEmergRelationship.Enabled = true;
            }
        }

        protected void ddlMonthBirth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmdAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentAttendanceOptions.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void ddlTShirtSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlVoicePart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chbNewStudentFlag_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void lbOptionsProgram_Click(object sender, EventArgs e)
        {
            Response.Redirect("Options.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void lbStudentPictures_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentPictures.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void ddlParentGuardian1Relationship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlParentGuardian2Relationship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbDiscipleshipMentor_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + txtLastName.Text
                            + "&StudentFirstName=" + txtFirstName.Text);
        }

        protected void chbDiscipleshipMentor_CheckedChanged(object sender, EventArgs e)
        {

            if (chbDiscipleshipmentorTraining.Checked)
            {
                Message("This will enroll this volunteer into the DiscipleshipMentor Program.  Do you wish to proceed?  ", this);
                //lbMSHSChoir.Enabled = true;
                //Insert into the Discipleship Mentor Program table...RCM..3/22/11.
                try
                {
                    string sql_Insert = "";

                    //sql_Insert = "INSERT into DiscipleshipMentorProgram "
                    //                                + "values ("
                    //                                + "'" + txtLastName.Text.Trim() + "' , "
                    //                                + "'" + txtFirstName.Text.Trim() + "' , "
                    //                                + "'N/A' , "
                    //                                + "'N/A' , "
                    //                                + "'N/A' , "
                    //                                + "0,"
                    //                                + "'N/A' , "
                    //                                + "'" + System.DateTime.Now.ToString() + "',"
                    //                                + "'" + System.DateTime.Now.ToString() + "',"
                    //                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                    con.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand = new SqlCommand(sql_Insert, con);
                    if (sqlInsertCommand.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        //con.Dispose();
                        UpdateVolunteerTable();
                        //chbDiscipleshipMentor.Enabled = false;
                        //lbDiscipleshipMentor.Enabled = true;
                    }
                    else
                    {
                        //display message that record was NOT updated
                        //	btnContinue.Visible = false;
                        //	lblAlert.Visible = true;
                        //	lblAlert.Text = "No update. Error has occurred.";
                    }

                    string sql_InsertDescription = "";
                    //sql_InsertDescription = "INSERT into DiscipleshipMentorDescription "
                    //                                + "values ("
                    //                                + "'" + txtLastName.Text.Trim() + "' , "
                    //                                + "'" + txtFirstName.Text.Trim() + "' , "
                    //                                + "'Beginning Entry' , "
                    //                                + "'" + System.DateTime.Now.ToString() + "',"
                    //                                + "'" + System.DateTime.Now.ToString() + "',"
                    //                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                    SqlConnection con2 = new SqlConnection(connectionString);
                    con2.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand2 = new SqlCommand(sql_InsertDescription, con2);
                    if (sqlInsertCommand2.ExecuteNonQuery() > 0)
                    {
                        con2.Close();
                        //UpdateVolunteerTable();
                        //chbDiscipleshipMentor.Enabled = false;
                        //lbDiscipleshipMentor.Enabled = true;
                    }
                    else
                    {
                        //display message that record was NOT updated
                        //	btnContinue.Visible = false;
                        //	lblAlert.Visible = true;
                        //	lblAlert.Text = "No update. Error has occurred.";
                    }
                }
                catch (Exception alskdjaa)
                {

                }
                finally
                {
                    //con.Close();
                }
            }
            else
            {
                //lbMSHSChoir.Enabled = false;
            }
        }

        protected void chbRegistrationForm_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbDiscipleshipmentorTraining_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDiscipleshipmentorTraining.Checked)
            {
                //txbDiscipleshipmentorTrainedDate.Enabled = true;
                //ddlDayBirth2.Enabled = true;
                ddlDayBirth3.Enabled = true;
                //ddlMonthBirth2.Enabled = true;
                ddlMonthBirth3.Enabled = true;
                //ddlYearBirth2.Enabled = true;
                ddlYearBirth3.Enabled = true;
            }
            //else
            //{
            //    txbDiscipleshipmentorTrainedDate.Enabled = false;
            //}
        }

        protected void lbDM_Click(object sender, EventArgs e)
        {
            //Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + txtLastName.Text
            //                + "&StudentFirstName=" + txtFirstName.Text);
            Response.Clear();
            Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=");
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (lblErrorMessage.Visible)
            {
                //Don't attempt to do the update if there was an error.... RCM..10/15/12.
            }
            else
            {
                UpdateVolunteerTable();//Update to make sure things get saved...RCM..9/7/11.
            }

            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void chbNotes_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("Volunteers.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=&StudentFirstName=");
        }

        protected void chbMondayNights_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chbMondayNights.Checked)
            {

                cmbMondayNightsConfirm.Visible = true;
                cmbMondayNightsConfirm.Style.Add("z-index", "99999");

                cmbMondayNightsCancel.Enabled = true;
                cmbMondayNightsCancel.Visible = true;
                cmbMondayNightsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("MondayNights");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbMondayNights.Checked)
                {
                    cmbMondayNightsRemove.Visible = true;
                    cmbMondayNightsRemove.Style.Add("z-index", "99999");

                    cmbMondayNightsCancel.Enabled = true;
                    cmbMondayNightsCancel.Visible = true;
                    cmbMondayNightsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("MondayNights");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void PopulateRadioButtonLists(string Program)
        {
            string tablename = "";
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
            else if (Program == "Special Events")
            {
                tablename = "SpecialEvents";
            }
            else if (Program == "MSHSChoir")
            {
                tablename = "MSHSChoir";
            }
            else if (Program == "ChildrensChoir")
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
            //else if (Program == "PAA")
            //{
            //    tablename = "PerformingArtsAcademy";
            //}
            else if (Program == "SummerDayCamp")
            {
                tablename = "SummerDayCamp";
            }
            else if (Program == "ImpactUrbanSchools")
            {
                tablename = "ImpactUrbanSchools";
            }
            else if (Program == "AcademicReadingSupport")
            {
                tablename = "AcademicReadingSupport";
            }

            //Load the dropdown list for the sections.
            string sql = "Select sectionname "
                       + "from UIF_PerformingArts.dbo." + "[" + tablename + "ProgramSections] "
                       + "group by sectionname "
                       + "order by sectionname ";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA = new SqlDataAdapter();
            custDA.SelectCommand = cmd;
            DataSet custDS = new DataSet();
            custDA.Fill(custDS, "[" + tablename + "ProgramSections]");

            cblProgramManagement.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS.Tables["[" + tablename + "ProgramSections]"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                cblProgramManagement.Items.Add(myDataRowPO[0].ToString());
                cblProgramManagement.Enabled = true;
            }
            custDS.Clear();
        }

        protected void PopulateRadioButtonListsAndChoice(string Program)
        {
            //First do a lookup and determine which sections there are to delete and then automatically
            //select them in the radiobuttonlist....RCM..6/22/12.
            //Load the dropdown list for the sections.
            string tablename = "";

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
            else if (Program == "Special Events")
            {
                tablename = "SpecialEvents";
            }
            else if (Program == "MSHSChoir")
            {
                tablename = "MSHSChoir";
            }
            else if (Program == "ChildrensChoir")
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
            else if (Program == "PAA")
            {
                tablename = "PerformingArtsAcademy";
            }
            else if (Program == "SummerDayCamp")
            {
                tablename = "SummerDayCamp";
            }
            else if (Program == "ImpactUrbanSchools")
            {
                tablename = "ImpactUrbanSchools";
            }
            else if (Program == "AcademicReadingSupport")
            {
                tablename = "AcademicReadingSupport";
            }

            //----------------------------------------------------------------------------------------------
            //Load the dropdown list for the sections.
            string sql = "Select sectionname "
                       + "from UIF_PerformingArts.dbo." + "[" + tablename + "ProgramSections] "
                       + "group by sectionname "
                       + "order by sectionname ";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA = new SqlDataAdapter();
            custDA.SelectCommand = cmd;
            DataSet custDS = new DataSet();
            custDA.Fill(custDS, "[" + tablename + "ProgramSections]");

            cblProgramManagement.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS.Tables["[" + tablename + "ProgramSections]"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                cblProgramManagement.Items.Add(myDataRowPO[0].ToString());
            }
            custDS.Clear();

            //-----------------------------------------------------------------------
            string sql2 = "Select sectionname "
                       + "from UIF_PerformingArts.dbo." + "[" + tablename + "Enrollment] "
                       + "where studentlastname = '" + txtLastName.Text.Trim() + "' "
                       + "and studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                       + "and volunteer = 1 "
                       + "group by sectionname "
                       + "order by sectionname ";

            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA2 = new SqlDataAdapter();
            custDA2.SelectCommand = cmd2;
            DataSet custDS2 = new DataSet();
            custDA2.Fill(custDS2, "[" + tablename + "Enrollment]");

            //cblProgramManagement.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS2.Tables["[" + tablename + "Enrollment]"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                cblProgramManagement.Items.FindByText(myDataRowPO[0].ToString()).Selected = true;

                if (Program == "BasketballTEAMS")
                {
                    cmbConfirmDelete.Enabled = true;
                }
                else if (Program == "Outreach Basketball")
                {
                    cmbOutreachBasketballRemove.Enabled = true;
                }
                else if (Program == "3on3 Basketball")
                {
                    cmb3on3BasketballRemove.Enabled = true;
                }
                else if (Program == "Baseball")
                {
                    cmbRemoveBaseball.Enabled = true;
                }
                else if (Program == "MS Basketball League")
                {
                    cmbMSBasketballLeagueRemove.Enabled = true;
                }
                else if (Program == "HS Basketball League")
                {
                    cmbHSBasketballLeagueRemove.Enabled = true;
                }
                else if (Program == "SoccerTEAMS")
                {
                    cmbSoccerTEAMSRemove.Enabled = true;
                }
                else if (Program == "SoccerIntraMurals")
                {
                    cmbSoccerIntraMuralsRemove.Enabled = true;
                }
                else if (Program == "MondayNights")
                {
                    cmbMondayNightsRemove.Enabled = true;
                }
                else if (Program == "Bible Study")
                {
                    cmbBibleStudyRemove.Enabled = true;
                }
                else if (Program == "Special Events")
                {
                    cmbSpecialEventsRemove.Enabled = true;
                }
                else if (Program == "MSHSChoir")
                {
                    cmbMSHSChoirRemove.Enabled = true;
                }
                else if (Program == "ChildrensChoir")
                {
                    cmbChildrensChoirRemove.Enabled = true;
                }
                else if (Program == "Singers")
                {
                    cmbSingersRemove.Enabled = true;
                }
                else if (Program == "Shakes")
                {
                    cmbShakesRemove.Enabled = true;
                }
                //else if (Program == "PAA")
                //{
                //    cmbPAARemove.Enabled = true;
                //}
                else if (Program == "SummerDayCamp")
                {
                    cmbSummerDayCampRemove.Enabled = true;
                }
                else if (Program == "ImpactUrbanSchools")
                {
                    cmbImpactUrbanSchoolsRemove.Enabled = true;
                }
                else if (Program == "AcademicReadingSupport")
                {
                    cmbAcademicReadingSupportRemove.Enabled = true;
                }
                //cblProgramManagement.Enabled = false;
            }
            custDS2.Clear();
        }

        protected void PopulateRadioButtonListsAndChoiceForAdds(string Program)
        {
            //First do a lookup and determine which sections there are to delete and then automatically
            //select them in the radiobuttonlist....RCM..6/22/12.
            //Load the dropdown list for the sections.
            string tablename = "";

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
            else if (Program == "Special Events")
            {
                tablename = "SpecialEvents";
            }
            else if (Program == "MSHSChoir")
            {
                tablename = "MSHSChoir";
            }
            else if (Program == "ChildrensChoir")
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
            else if (Program == "PAA")
            {
                tablename = "PerformingArtsAcademy";
            }
            else if (Program == "SummerDayCamp")
            {
                tablename = "SummerDayCamp";
            }
            else if (Program == "ImpactUrbanSchools")
            {
                tablename = "ImpactUrbanSchools";
            }
            else if (Program == "AcademicReadingSupport")
            {
                tablename = "AcademicReadingSupport";
            }

            //----------------------------------------------------------------------------------------------
            //Load the dropdown list for the sections.
            string sql = "Select sectionname "
                       + "from UIF_PerformingArts.dbo." + "[" + tablename + "ProgramSections] "
                       + "group by sectionname "
                       + "order by sectionname ";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA = new SqlDataAdapter();
            custDA.SelectCommand = cmd;
            DataSet custDS = new DataSet();
            custDA.Fill(custDS, "[" + tablename + "ProgramSections]");

            cblProgramManagement.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS.Tables["[" + tablename + "ProgramSections]"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                cblProgramManagement.Items.Add(myDataRowPO[0].ToString());
            }
            custDS.Clear();

            //-----------------------------------------------------------------------
            string sql2 = "Select sectionname "
                       + "from UIF_PerformingArts.dbo." + "[" + tablename + "Enrollment] "
                       + "where studentlastname = '" + txtLastName.Text.Trim() + "' "
                       + "and studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                       + "and volunteer = 1 "
                       + "group by sectionname "
                       + "order by sectionname ";

            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA2 = new SqlDataAdapter();
            custDA2.SelectCommand = cmd2;
            DataSet custDS2 = new DataSet();
            custDA2.Fill(custDS2, "[" + tablename + "Enrollment]");

            //cblProgramManagement.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS2.Tables["[" + tablename + "Enrollment]"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                cblProgramManagement.Items.FindByText(myDataRowPO[0].ToString()).Selected = true;
                cblProgramManagement.Items.FindByText(myDataRowPO[0].ToString()).Enabled = false;

                if (Program == "BasketballTEAMS")
                {
                    cmbConfirmDelete.Enabled = true;
                }
                else if (Program == "Outreach Basketball")
                {
                    cmbOutreachBasketballRemove.Enabled = true;
                }
                else if (Program == "3on3 Basketball")
                {
                    cmb3on3BasketballRemove.Enabled = true;
                }
                else if (Program == "Baseball")
                {
                    cmbRemoveBaseball.Enabled = true;
                }
                else if (Program == "MS Basketball League")
                {
                    cmbMSBasketballLeagueRemove.Enabled = true;
                }
                else if (Program == "HS Basketball League")
                {
                    cmbHSBasketballLeagueRemove.Enabled = true;
                }
                else if (Program == "SoccerTEAMS")
                {
                    cmbSoccerTEAMSRemove.Enabled = true;
                }
                else if (Program == "SoccerIntraMurals")
                {
                    cmbSoccerIntraMuralsRemove.Enabled = true;
                }
                else if (Program == "MondayNights")
                {
                    cmbMondayNightsRemove.Enabled = true;
                }
                else if (Program == "Bible Study")
                {
                    cmbBibleStudyRemove.Enabled = true;
                }
                else if (Program == "Special Events")
                {
                    cmbSpecialEventsRemove.Enabled = true;
                }
                else if (Program == "MSHSChoir")
                {
                    cmbMSHSChoirRemove.Enabled = true;
                }
                else if (Program == "ChildrensChoir")
                {
                    cmbChildrensChoirRemove.Enabled = true;
                }
                else if (Program == "Singers")
                {
                    cmbSingersRemove.Enabled = true;
                }
                else if (Program == "Shakes")
                {
                    cmbShakesRemove.Enabled = true;
                }
                //else if (Program == "PAA")
                //{
                    //cmbPAARemove.Enabled = true;
                //}
                else if (Program == "SummerDayCamp")
                {
                    cmbSummerDayCampRemove.Enabled = true;
                }
                else if (Program == "ImpactUrbanSchools")
                {
                    cmbImpactUrbanSchoolsRemove.Enabled = true;
                }
                else if (Program == "AcademicReadingSupport")
                {
                    cmbAcademicReadingSupportRemove.Enabled = true;
                }
                //cblProgramManagement.Enabled = false;
            }
            custDS2.Clear();
        }


        protected void PopulateRadioButtonListsAndChoiceForRemoval(string Program)
        {
            //First do a lookup and determine which sections there are to delete and then automatically
            //select them in the radiobuttonlist....RCM..6/22/12.
            //Load the dropdown list for the sections.
            string tablename = "";

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
            else if (Program == "Special Events")
            {
                tablename = "SpecialEvents";
            }
            else if (Program == "MSHSChoir")
            {
                tablename = "MSHSChoir";
            }
            else if (Program == "ChildrensChoir")
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
            else if (Program == "PAA")
            {
                tablename = "PerformingArtsAcademy";
            }
            else if (Program == "SummerDayCamp")
            {
                tablename = "SummerDayCamp";
            }
            else if (Program == "ImpactUrbanSchools")
            {
                tablename = "ImpactUrbanSchools";
            }
            else if (Program == "AcademicReadingSupport")
            {
                tablename = "AcademicReadingSupport";
            }

            //----------------------------------------------------------------------------------------------
            //Load the dropdown list for the sections.
            string sql = "Select sectionname "
                       + "from UIF_PerformingArts.dbo." + "[" + tablename + "ProgramSections] "
                       + "group by sectionname "
                       + "order by sectionname ";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA = new SqlDataAdapter();
            custDA.SelectCommand = cmd;
            DataSet custDS = new DataSet();
            custDA.Fill(custDS, "[" + tablename + "ProgramSections]");

            cblProgramManagement.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS.Tables["[" + tablename + "ProgramSections]"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                cblProgramManagement.Items.Add(myDataRowPO[0].ToString());
            }
            custDS.Clear();

            //-----------------------------------------------------------------------
            string sql2 = "Select sectionname "
                       + "from UIF_PerformingArts.dbo." + "[" + tablename + "Enrollment] "
                       + "where studentlastname = '" + txtLastName.Text.Trim() + "' "
                       + "and studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                       + "and volunteer = 1 "
                       + "group by sectionname "
                       + "order by sectionname ";

            SqlCommand cmd2 = new SqlCommand(sql2, con);
            cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA2 = new SqlDataAdapter();
            custDA2.SelectCommand = cmd2;
            DataSet custDS2 = new DataSet();
            custDA2.Fill(custDS2, "[" + tablename + "Enrollment]");

            //cblProgramManagement.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS2.Tables["[" + tablename + "Enrollment]"].Rows)
            {

                //Adding options to the drop downs for a new entry.
                cblProgramManagement.Items.FindByText(myDataRowPO[0].ToString()).Selected = true;

                cblProgramManagement.Items.FindByText(myDataRowPO[0].ToString()).Enabled = false;

                if (Program == "BasketballTEAMS")
                {
                    cmbConfirmDelete.Enabled = true;
                }
                else if (Program == "Outreach Basketball")
                {
                    cmbOutreachBasketballRemove.Enabled = true;
                }
                else if (Program == "3on3 Basketball")
                {
                    cmb3on3BasketballRemove.Enabled = true;
                }
                else if (Program == "Baseball")
                {
                    cmbRemoveBaseball.Enabled = true;
                }
                else if (Program == "MS Basketball League")
                {
                    cmbMSBasketballLeagueRemove.Enabled = true;
                }
                else if (Program == "HS Basketball League")
                {
                    cmbHSBasketballLeagueRemove.Enabled = true;
                }
                else if (Program == "SoccerTEAMS")
                {
                    cmbSoccerTEAMSRemove.Enabled = true;
                }
                else if (Program == "SoccerIntraMurals")
                {
                    cmbSoccerIntraMuralsRemove.Enabled = true;
                }
                else if (Program == "MondayNights")
                {
                    cmbMondayNightsRemove.Enabled = true;
                }
                else if (Program == "Bible Study")
                {
                    cmbBibleStudy.Enabled = true;
                }
                else if (Program == "Special Events")
                {
                    cmbSpecialEventsConfirm.Enabled = true;
                }
                else if (Program == "MSHSChoir")
                {
                    cmbMSHSChoirRemove.Enabled = true;
                }
                else if (Program == "ChildrensChoir")
                {
                    cmbChildrensChoirRemove.Enabled = true;
                }
                else if (Program == "Singers")
                {
                    cmbSingersRemove.Enabled = true;
                }
                else if (Program == "Shakes")
                {
                    cmbShakesRemove.Enabled = true;
                }
                //else if (Program == "PAA")
                //{
                //    cmbPAARemove.Enabled = true;
                //}
                else if (Program == "SummerDayCamp")
                {
                    cmbSummerDayCampRemove.Enabled = true;
                }
                else if (Program == "ImpactUrbanSchools")
                {
                    cmbImpactUrbanSchoolsRemove.Enabled = true;
                }
                //cblProgramManagement.Enabled = false;
            }
            custDS2.Clear();
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            if (lblErrorMessage.Visible)
            {
                //Don't attempt to do the update if there was an error.... RCM..10/15/12.
            }
            else
            {
                UpdateVolunteerTable();//Update to make sure things get saved...RCM..9/7/11.
            }
            Response.Redirect("menutest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbNewPerson2_Click(object sender, EventArgs e)
        {
            //Here we want to capture all the information from the various different fields and
            //use them to update the data within the database table..RCM..8/3/10.
            try
            {
                CleanCharacters();
                cmbNewPerson2.Enabled = false;
                //btnNewPerson1.Enabled = false;
                if (txtLastName.Text == "")
                {
                    //Student LastName is blank..  Abort..

                    //Throw an exception...RCM..
                    string except = "EXCEPTION";
                }
                else
                {
                    string sqlInsertStatement = "";

                    try
                    {
                        sqlInsertStatement = "INSERT into VolunteerInformation "
                            + "values ( "
                            + "'" + txtLastName.Text.Trim() + "',"
                            + "'" + txtFirstName.Text.Trim() + "',"
                            + "'" + txtAddress1.Text.Trim() + "', "
                            + "'" + txtCity.Text.Trim() + "', "
                            + "'" + txtState.Text.Trim() + "', "
                            + "'" + txtZip.Text.Trim() + "', "
                            + "'" + txtHomePhone.Text.Trim() + "', "
                            + "'" + txtStudentCellPhone.Text.Trim() + "', "
                            + "0, "
                            + "'" + txtStudentEmail.Text.Trim() + "', "
                            + "'" + ddlAge.Text.Trim() + "', "
                            + "'" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "', "
                            + "'" + ddlGender.Text.Trim() + "', "
                            + "'" + txtChurch.Text.Trim() + "', "
                            + "'CareerGoal', "
                            + "'" + txbHealthConditions.Text.Trim() + "', "
                            + "'" + txbNotes.Text.Trim() + "', "
                            + "'" + ddlTShirtSize.Text.Trim() + "', "
                            + "0, "
                            + "'~/VolunteerPictures/" + txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim() + ".jpg', "
                            + "0, "
                            + "'1900-01-01', " //when received christ.
                            + "0, "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            + Convert.ToInt32(chbDiscipleshipmentorTrainingDone.Checked) + ", "
                            + Convert.ToInt32(chbDiscipleshipmentorTraining.Checked) + ", "
                            + "'" + ddlMonthBirth2.Text.Trim() + "-" + ddlDayBirth2.Text.Trim() + "-" + ddlYearBirth2.Text.Trim() + "', "
                            + "'" + ddlMonthBirth3.Text.Trim() + "-" + ddlDayBirth3.Text.Trim() + "-" + ddlYearBirth3.Text.Trim() + "', "
                            + "'" + txbDiscipleshipMentorNotes.Text.Trim() + "', "
                            + Convert.ToInt32(chbDiscipleshipmentorWaitingList.Checked) + ", "
                            + "'" + ddlBackgroundMonth.Text.Trim() + "-" + ddlBackgroundDay.Text.Trim() + "-" + ddlBackgroundYear.Text.Trim() + "', "
                            + Convert.ToInt32(chbMailingList.Checked) + ", "
                            + "'" + ddlMailingListCodes.Text.Trim() + "',"
                            + Convert.ToInt32(chbDiscipleshipMentorPotentials.Checked) + ", "
                            + Convert.ToInt32(chbOfficeVolunteer.Checked) + ", "
                            + "'" + txbEmergencyRelationship.Text.Trim() + "', "
                            + "'" + txbEmergRelationship.Text.Trim() + "', "
                            + "'" + txbEmergencyPhone.Text.Trim() + "', "
                            + Convert.ToInt32(chbStaff.Checked) + ", "
                            + "'" + ddlMostRecentSeason.Text.Trim() + "', "
                            + "'" + ddlMostRecentSeasonYear.Text.Trim() + "') ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                        if (sqlInsertCommand.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful
                            con.Close();
                        }
                        else
                        {
                            //display message that record was NOT updated
                            //	btnContinue.Visible = false;
                            //	lblAlert.Visible = true;
                            //	lblAlert.Text = "No update. Error has occurred.";
                        }
                    }
                    catch (Exception lkjlk)
                    {
                        if (lkjlk.Message.ToString().Contains("duplicate"))
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Style.Add("z-index", "99999");
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to create a DUPLICATE of a volunteer that already exists!  Please EXIT the page.  Thankyou  ";
                            throw new Exception(lblErrorMessage.Text);
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Style.Add("z-index", "99999");
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = "There was an exception INSERTING NEW data into the VolunteerDetails table..  Please fix and try again MSG: " + lkjlk.Message.ToString();
                            throw new Exception(lblErrorMessage.Text);
                        }
                    }

                    try
                    {
                        string sqlInsertPrograms = "";

                        //More Programs to come..
                        if (Department == "Athletics")
                        {
                            sqlInsertPrograms = "INSERT into ProgramsList "
                                + "values ( "
                                + "'" + txtLastName.Text.Trim() + "',"
                                + "'" + txtFirstName.Text.Trim() + "',"
                                + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + Convert.ToInt32(chbShakes.Checked) + ", "
                                + Convert.ToInt32(chbSingers.Checked) + ", "
                                + "0, "
                                + "1, "
                                + Convert.ToInt32(chbMondayNights.Checked) + ", "
                                + Convert.ToInt32(chb3on3Basketball.Checked) + ", "
                                + Convert.ToInt32(chbBasketballTEAMS.Checked) + ", "
                                + Convert.ToInt32(chbSoccerInterMurals.Checked) + ", "
                                + Convert.ToInt32(chbSoccerLgTravel.Checked) + ", "
                                + Convert.ToInt32(chbLittleLeagueBaseball.Checked) + ", "
                                + Convert.ToInt32(chbOliverFootballBible.Checked) + ", "
                                + Convert.ToInt32(chbHSBasketLeague.Checked) + ", "
                                + Convert.ToInt32(chbMSBasketLeague.Checked) + ", "
                                + Convert.ToInt32(chbBoysOutreachBball.Checked) + ", "
                                + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                + "'Staff/Volunteer', "
                                + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                + Convert.ToInt32(chbReadingProgram.Checked) + ") ";
                        }
                        else if (Department == "PerformingArts")
                        {
                            sqlInsertPrograms = "INSERT into ProgramsList "
                                + "values ( "
                                + "'" + txtLastName.Text.Trim() + "',"
                                + "'" + txtFirstName.Text.Trim() + "',"
                                + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + Convert.ToInt32(chbShakes.Checked) + ", "
                                + Convert.ToInt32(chbSingers.Checked) + ", "
                                + "0, "
                                + "1, "
                                + Convert.ToInt32(chbMondayNights.Checked) + ", "
                                + Convert.ToInt32(chb3on3Basketball.Checked) + ", "
                                + Convert.ToInt32(chbBasketballTEAMS.Checked) + ", "
                                + Convert.ToInt32(chbSoccerInterMurals.Checked) + ", "
                                + Convert.ToInt32(chbSoccerLgTravel.Checked) + ", "
                                + Convert.ToInt32(chbLittleLeagueBaseball.Checked) + ", "
                                + Convert.ToInt32(chbOliverFootballBible.Checked) + ", "
                                + Convert.ToInt32(chbHSBasketLeague.Checked) + ", "
                                + Convert.ToInt32(chbMSBasketLeague.Checked) + ", "
                                + Convert.ToInt32(chbBoysOutreachBball.Checked) + ", "
                                + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                + "'Staff/Volunteer', "
                                + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                + Convert.ToInt32(chbReadingProgram.Checked) + ") ";

                        }
                        else if (Department == "Education")
                        {
                            sqlInsertPrograms = "INSERT into ProgramsList "
                                + "values ( "
                                + "'" + txtLastName.Text.Trim() + "',"
                                + "'" + txtFirstName.Text.Trim() + "',"
                                + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + Convert.ToInt32(chbShakes.Checked) + ", "
                                + Convert.ToInt32(chbSingers.Checked) + ", "
                                + "0, "
                                + "1, "
                                + Convert.ToInt32(chbMondayNights.Checked) + ", "
                                + Convert.ToInt32(chb3on3Basketball.Checked) + ", "
                                + Convert.ToInt32(chbBasketballTEAMS.Checked) + ", "
                                + Convert.ToInt32(chbSoccerInterMurals.Checked) + ", "
                                + Convert.ToInt32(chbSoccerLgTravel.Checked) + ", "
                                + Convert.ToInt32(chbLittleLeagueBaseball.Checked) + ", "
                                + Convert.ToInt32(chbOliverFootballBible.Checked) + ", "
                                + Convert.ToInt32(chbHSBasketLeague.Checked) + ", "
                                + Convert.ToInt32(chbMSBasketLeague.Checked) + ", "
                                + Convert.ToInt32(chbBoysOutreachBball.Checked) + ", "
                                + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                + "'Staff/Volunteer', "
                                + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                + Convert.ToInt32(chbReadingProgram.Checked) + ") ";
                        }
                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlInsertProgramsCommand = new SqlCommand(sqlInsertPrograms, con);
                        if (sqlInsertProgramsCommand.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful
                            con.Close();
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
                        if (lkjlaa.Message.ToString().Contains("duplicate"))
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Style.Add("z-index", "99999");
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to create a DUPLICATE of a volunteer that already exists!  Please EXIT the page.  Thankyou  ";
                            throw new Exception(lblErrorMessage.Text);
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Style.Add("z-index", "99999");
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = "There was an exception INSERTING NEW data into the ProgramsList table..  Please fix and try again MSG: " + lkjlaa.Message.ToString();
                            throw new Exception(lblErrorMessage.Text);
                        }
                    }
                    finally
                    {
                        con.Close();
                    }

                    string sqlInsertStatement2 = "";                    
                    try
                    {
                        //Handle the Insert for VolunteerDetails table..RCM..4/13/12.
                        sqlInsertStatement2 = "INSERT into VolunteerDetails "
                            + "values ( "
                            + "'" + txtLastName.Text.Trim() + "', "
                            + "'" + txtFirstName.Text.Trim() + "', "
                            + Convert.ToInt32(chbGeneralInformation.Checked) + ", "
                            + Convert.ToInt32(chbSpiritualJourney.Checked) + ", "
                            + Convert.ToInt32(chbReleaseWaiver.Checked) + ", "
                            + Convert.ToInt32(chbNewVolunteerTraining.Checked) + ", "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + Convert.ToInt32(chbVehichleInsurance.Checked) + ", "
                            + "'Check Not Run', "
                            + "'1900-01-01', "
                            //+ "'" + System.DateTime.Now.ToString() + "', "//VehichleInsurance date.
                            + "0, "
                            + "0, "
                            + "'Check Not Run', "
                            + "'1900-01-01', "
                            //+ "'" + System.DateTime.Now.ToString() + "', "
                            + "0, "
                            + "'Check Not Run', "
                            + "'1900-01-01', "
                            //+ "'" + System.DateTime.Now.ToString() + "', "
                            + "0, "
                            + "'Check Not Run', "
                            + "'1900-01-01', "
                            //+ "'" + System.DateTime.Now.ToString() + "', "
                            + "0, "
                            + "'Checks Not Run', "//BackgroundCheckPaidCodes..
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'N/A', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + System.DateTime.Now.ToString() + "', "
                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            + "'N/A') ";

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand2 = new SqlCommand(sqlInsertStatement2, con);
                        if (sqlInsertCommand2.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful
                            con.Close();
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
                        if (lkjlaa.Message.ToString().Contains("duplicate"))
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Style.Add("z-index", "99999");
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to create a DUPLICATE of a volunteer that already exists!  Please EXIT the page.  Thankyou  ";
                            throw new Exception(lblErrorMessage.Text);
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Style.Add("z-index", "99999");
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = "There was an exception INSERTING NEW data into the VolunteerDetails table..  Please fix and try again MSG: " + lkjlaa.Message.ToString();
                            throw new Exception(lblErrorMessage.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //Close connection
                con.Close();
                con.Dispose();
            }
        }

        protected void chbNewVolunteerTraining_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbBackgroundCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (chbBackgroundCheck.Checked)
            {
                ddlBackgroundYear.Enabled = true;
                ddlBackgroundMonth.Enabled = true;
                ddlBackgroundDay.Enabled = true;
            }
            else
            {
                ddlBackgroundYear.Enabled = false;
                ddlBackgroundMonth.Enabled = false;
                ddlBackgroundDay.Enabled = false;
            }
        }

        protected void chbMailingList_CheckedChanged(object sender, EventArgs e)
        {
            if (chbMailingList.Checked)
            {
                ddlMailingListCodes.Text = "Use Current Address";
                ddlMailingListCodes.Enabled = false;
            }
            else
            {
                ddlMailingListCodes.Enabled = true;
            }
        }

        protected void lbPerforingArts_Click(object sender, EventArgs e)
        {
            UpdateVolunteerTable();
            //Response.Redirect("PerformingArtsClasses.aspx?    "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
            Response.Redirect("VolunteerProgramMaintenance.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + txtLastName.Text + "&VolunteerFirstName=" + txtFirstName.Text + "&Dept=" + Request.QueryString["Dept"] + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
        }

        protected void chbBasketballTEAMS_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            rblBaseball.Visible = false;
            cmbBaseballCancel.Visible = false;
            cmbBaseball.Visible = false;
            if (chbBasketballTEAMS.Checked)
            {
                cmbProgramManagement.Visible = true;
                cmbProgramManagement.Style.Add("z-index", "99999");

                cmbProgramManageCancel.Enabled = true;
                cmbProgramManageCancel.Visible = true;
                cmbProgramManageCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("BasketballTEAMS");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbBasketballTEAMS.Checked)
                {
                    cmbConfirmDelete.Visible = true;
                    cmbConfirmDelete.Style.Add("z-index", "99999");

                    cmbProgramManageCancel.Enabled = true;
                    cmbProgramManageCancel.Visible = true;
                    cmbProgramManageCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("BasketballTEAMS");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void DeleteFromBasketballTEAMS()
        {
            string sql_DeleteFromBasketballTEAMS = "";
            try
            {
                if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                {
                    sql_DeleteFromBasketballTEAMS = "Delete from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
                                                  + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                  + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                  + "AND volunteer = 1 ";
                }
                else
                {
                    sql_DeleteFromBasketballTEAMS = "Delete from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
                                                  + " WHERE lastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                                  + " AND firstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                                                  + "AND volunteer = 1 ";
                }
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromBasketballTEAMS = new SqlCommand(sql_DeleteFromBasketballTEAMS, con);
                if (sqlDeleteFromBasketballTEAMS.ExecuteNonQuery() > 0)
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

        protected void DeleteFromSoccer()
        {
            string sql_DeleteFromBasketballTEAMS = "";
            try
            {
                if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                {
                    sql_DeleteFromBasketballTEAMS = "Delete from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
                                                    + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                    + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                    + "AND volunteer = 1 "
                                                    + "AND sectionname = '" + cblProgramManagement.SelectedItem.Text + "' ";
                }
                else
                {
                    sql_DeleteFromBasketballTEAMS = "Delete from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
                                                  + "WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                                  + "AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                                                  + "AND volunteer = 1 "
                                                  + "AND sectionname = '" + cblProgramManagement.SelectedItem.Text + "' ";
                }
                //+ "AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromBasketballTEAMS = new SqlCommand(sql_DeleteFromBasketballTEAMS, con);
                if (sqlDeleteFromBasketballTEAMS.ExecuteNonQuery() > 0)
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


        protected void cmbProgramManagement_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("BasketballTEAMS");
        }

        protected void cmbProgramManageCancel_Click(object sender, EventArgs e)
        {
            //BasketballTEAMS Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("BasketballTEAMS");

            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbProgramManagement.Visible = false;
            cmbConfirmDelete.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }


        protected void DetermineCheckBoxStatus(string Program)
        {
            SqlDataReader reader = null;
            con.Open();

            string tablename = "";

            try
            {
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
                else if (Program == "Special Events")
                {
                    tablename = "SpecialEvents";
                }
                else if (Program == "MSHSChoir")
                {
                    tablename = "MSHSChoir";
                }
                else if (Program == "ChildrensChoir")
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
                else if (Program == "PAA")
                {
                    tablename = "PAA";
                }
                else if (Program == "SummerDayCamp")
                {
                    tablename = "SummerDayCamp";
                }
                else if (Program == "ImpactUrbanSchools")
                {
                    tablename = "ImpactUrbanSchools";
                }
                else if (Program == "AcademicReadingSupport")
                {
                    tablename = "AcademicReadingSupport";
                }

                //Determine if the volunteer is still enrolled in at least 1 section.   If so,
                //leave the checkbox checked.  Else allow it to be unchecked..RCM..8/13/12.
                string CheckPartialEnrollment = "Select * from UIF_PerformingArts.dbo." + "[" + tablename + "Enrollment] "
                                              + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                              + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                              + "AND volunteer = 1 ";

                //create a SQL command to update record
                SqlCommand CheckPartialEnrollmentCommand = new SqlCommand(CheckPartialEnrollment, con);
                CheckPartialEnrollmentCommand.Connection = con;
                reader = CheckPartialEnrollmentCommand.ExecuteReader();
                if (reader.Read())
                {
                    if (Program == "BasketballTEAMS")
                    {
                        chbBasketballTEAMS.Checked = true;
                    }
                    else if (Program == "Outreach Basketball")
                    {
                        chbBoysOutreachBball.Checked = true;
                    }
                    else if (Program == "3on3 Basketball")
                    {
                        chb3on3Basketball.Checked = true;
                    }
                    else if (Program == "Baseball")
                    {
                        chbLittleLeagueBaseball.Checked = true;
                    }
                    else if (Program == "MS Basketball League")
                    {
                        chbMSBasketLeague.Checked = true;
                    }
                    else if (Program == "HS Basketball League")
                    {
                        chbHSBasketLeague.Checked = true;
                    }
                    else if (Program == "SoccerTEAMS")
                    {
                        chbSoccerLgTravel.Checked = true;
                    }
                    else if (Program == "SoccerIntraMurals")
                    {
                        chbSoccerInterMurals.Checked = true;
                    }
                    else if (Program == "MondayNights")
                    {
                        chbMondayNights.Checked = true;
                    }
                    else if (Program == "Bible Study")
                    {
                        chbOliverFootballBible.Checked = true;
                    }
                    else if (Program == "Special Events")
                    {
                        chbSpecialEvents.Checked = true;
                    }
                    else if (Program == "MSHSChoir")
                    {
                        chbMSHSChoir.Checked = true;
                    }
                    else if (Program == "ChildrensChoir")
                    {
                        chbChildrensChoir.Checked = true;
                    }
                    else if (Program == "Singers")
                    {
                        chbSingers.Checked = true;
                    }
                    else if (Program == "Shakes")
                    {
                        chbShakes.Checked = true;
                    }
                    else if (Program == "PAA")
                    {
                        chbPerformingArts.Checked = true;
                    }
                    else if (Program == "SummerDayCamp")
                    {
                        chbSummerDayCamp.Checked = true;
                    }
                    else if (Program == "ImpactUrbanSchools")
                    {
                        if (Department == "Athletics")
                        {
                            chbImpactUrbanSchools.Checked = true;
                        }
                        else if (Department == "PerformingArts")
                        {
                            chbImpactUrbanSchoolsPA.Checked = true;
                        }
                        else if (Department == "Education")
                        {
                            chbImpactUrbanSchoolsAcademics.Checked = true;
                        }
                        else
                        {
                            //chbImpactUrbanSchoolsAcademics.Checked = true;
                            //chbImpactUrbanSchoolsPA.Checked = true;
                            //chbImpactUrbanSchools.Checked = true;
                        }
                    }
                    else if (Program == "AcademicReadingSupport")
                    {
                        chbReadingProgram.Checked = true;
                    }
                }
                else
                {
                    if (Program == "BasketballTEAMS")
                    {
                        chbBasketballTEAMS.Checked = false;
                    }
                    else if (Program == "Outreach Basketball")
                    {
                        chbBoysOutreachBball.Checked = false;
                    }
                    else if (Program == "3on3 Basketball")
                    {
                        chb3on3Basketball.Checked = false;
                    }
                    else if (Program == "Baseball")
                    {
                        chbLittleLeagueBaseball.Checked = false;
                    }
                    else if (Program == "MS Basketball League")
                    {
                        chbMSBasketLeague.Checked = false;
                    }
                    else if (Program == "HS Basketball League")
                    {
                        chbHSBasketLeague.Checked = false;
                    }
                    else if (Program == "SoccerTEAMS")
                    {
                        chbSoccerLgTravel.Checked = false;
                    }
                    else if (Program == "SoccerIntraMurals")
                    {
                        chbSoccerInterMurals.Checked = false;
                    }
                    else if (Program == "MondayNights")
                    {
                        chbMondayNights.Checked = false;
                    }
                    else if (Program == "Bible Study")
                    {
                        chbOliverFootballBible.Checked = false;
                    }
                    else if (Program == "Special Events")
                    {
                        chbSpecialEvents.Checked = false;
                    }
                    else if (Program == "MSHSChoir")
                    {
                        chbMSHSChoir.Checked = false;
                    }
                    else if (Program == "ChildrensChoir")
                    {
                        chbChildrensChoir.Checked = false;
                    }
                    else if (Program == "Singers")
                    {
                        chbSingers.Checked = false;
                    }
                    else if (Program == "Shakes")
                    {
                        chbShakes.Checked = false;
                    }
                    else if (Program == "PAA")
                    {
                        chbPerformingArts.Checked = false;
                    }
                    else if (Program == "SummerDayCamp")
                    {
                        chbSummerDayCamp.Checked = false;
                    }
                    else if (Program == "ImpactUrbanSchools")
                    {
                        if (Department == "Athletics")
                        {
                            chbImpactUrbanSchools.Checked = false;
                        }
                        else if (Department == "PerformingArts")
                        {
                            chbImpactUrbanSchoolsPA.Checked = false;
                        }
                        else if (Department == "Education")
                        {
                            chbImpactUrbanSchoolsAcademics.Checked = false;
                        }
                        else
                        {
                            //chbImpactUrbanSchoolsAcademics.Checked = true;
                            //chbImpactUrbanSchoolsPA.Checked = true;
                            //chbImpactUrbanSchools.Checked = true;
                        }
                    }
                    else if (Program == "AcademicReadingSupport")
                    {
                        chbReadingProgram.Checked = false;
                    }
                }
            }
            catch (Exception lkjlkaabb)
            {

            }
            finally
            {
                con.Close();
            }
        }

        protected void cmbConfirmDelete_Click(object sender, EventArgs e)
        {
            DeleteFromBasketballTEAMS();
            UpdateVolunteerTable();
            System.Threading.Thread.Sleep(2000);//Wait 2 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbProgramManagement.Visible = false;
            cmbConfirmDelete.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cblProgramManagement.Visible = false;
        }

        protected void cmbCancelPerformArts_Click(object sender, EventArgs e)
        {
            Boolean GoodUpdate = false;

            System.Threading.Thread.Sleep(1000);//Wait 2 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbDeletePerformingArts.Visible = false;
            cmbCancelPerformArts.Visible = false;

            //Reset the checkbox value back to what it was...RCM..9/19/11.
            chbPerformingArts.Checked = true;
            lbPerforingArts.Enabled = true;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbDeletePerformingArts_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromClasses();
            UpdateVolunteerTable();
            System.Threading.Thread.Sleep(2000);//Wait 2 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbDeletePerformingArts.Visible = false;
            cmbCancelPerformArts.Visible = false;
        }


        protected void RemoveVolunteersFromHSBasketballLeague()
        {
            //Remove volunteer from classes..  
            string sql_DeleteFromPerformingArts = "";
            try
            {
                if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                {
                    sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.HSBasketballLeagueEnrollment "
                                                 + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                 + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                 + "AND volunteer = 1 ";
                }
                else
                {
                    sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.HSBasketballLeagueEnrollment "
                                                 + "WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                                 + "AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                                                 + "AND volunteer = 1 ";
                }
                //+ "AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
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


        protected void RemoveVolunteersFromMSBasketballLeague()
        {
            //Remove volunteer from classes..  
            string sql_DeleteFromPerformingArts = "";
            try
            {
                if ((Request.QueryString["VolunteerLastName"] == "") || (Request.QueryString["VolunteerFirstName"] == ""))
                {
                    sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.MSBasketballLeagueEnrollment "
                                                    + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                    + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                    + "AND volunteer = 1 ";
                }
                else
                {
                    sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.MSBasketballLeagueEnrollment "
                                                + "WHERE studentlastname = '" + Request.QueryString["VolunteerLastName"] + "' "
                                                + "AND studentfirstname = '" + Request.QueryString["VolunteerFirstName"] + "' "
                                                + "AND volunteer = 1 ";
                }
                //+ "AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
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


        protected void chbMSBasketLeague_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chbMSBasketLeague.Checked)
            {
                cmbMSBasketballLeagueConfirm.Visible = true;
                cmbMSBasketballLeagueConfirm.Style.Add("z-index", "99999");

                cmbMSBasketballLeagueCancel.Enabled = true;
                cmbMSBasketballLeagueCancel.Visible = true;
                cmbMSBasketballLeagueCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer. Please choose.";

                PopulateRadioButtonLists("MS Basketball League");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbMSBasketLeague.Checked)
                {
                    cmbMSBasketballLeagueRemove.Visible = true;
                    cmbMSBasketballLeagueRemove.Style.Add("z-index", "99999");

                    cmbMSBasketballLeagueCancel.Enabled = true;
                    cmbMSBasketballLeagueCancel.Visible = true;
                    cmbMSBasketballLeagueCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("MS Basketball League");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void DisableAllConfirmButtons()
        {
            cmbProgramManagement.Enabled = false;
            cmbMondayNightsConfirm.Enabled = false;
            cmbMSBasketballLeagueConfirm.Enabled = false;
            cmbHSBasketballLeagueConfirm.Enabled = false;
            cmbSoccerIntraMuralsConfirm.Enabled = false;
            cmbSoccerTEAMSConfirm.Enabled = false;
            cmb3on3BasketballConfirm.Enabled = false;
            cmbBibleStudy.Enabled = false;
            cmbBaseball.Enabled = false;
            cmbOutreachBasketball.Enabled = false;
            cmbSpecialEventsConfirm.Enabled = false;

            cmbMSHSChoirConfirm.Enabled = false;
            cmbChildrensChoirConfirm.Enabled = false;
            cmbSingersConfirm.Enabled = false;
            cmbShakesConfirm.Enabled = false;
            
            cmbImpactUrbanSchoolsConfirm.Enabled = false;
            cmbSummerDayCampConfirm.Enabled = false;
            cmbAcademicReadingSupportConfirm.Enabled = false;
        }


        protected void cblProgramManagement_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbProgramManagement.Enabled = true;
            cmbMondayNightsConfirm.Enabled = true;
            cmbMSBasketballLeagueConfirm.Enabled = true;
            cmbHSBasketballLeagueConfirm.Enabled = true;
            cmbSoccerIntraMuralsConfirm.Enabled = true;
            cmbSoccerTEAMSConfirm.Enabled = true;
            cmb3on3BasketballConfirm.Enabled = true;
            cmbBibleStudy.Enabled = true;
            cmbBaseball.Enabled = true;
            cmbOutreachBasketball.Enabled = true;
        }

        protected void DeleteOutReachBasketball()
        {
            try
            {
                string sql_DeleteFromOutreachBasketball = "Delete from UIF_PerformingArts.dbo.OutreachBasketball "
                                                        + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                        + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                        + "AND sectionname = '" + rblOutreachBasketball.SelectedItem.Text + "' "
                                                        + "AND volunteer = 1 ";
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromOutreachBasketball = new SqlCommand(sql_DeleteFromOutreachBasketball, con);
                if (sqlDeleteFromOutreachBasketball.ExecuteNonQuery() > 0)
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

        protected void chbBoysOutreachBball_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblBaseball.Visible = false;
            cmbBaseball.Visible = false;
            cmbBaseballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            if (chbBoysOutreachBball.Checked)
            {
                cmbOutreachBasketball.Enabled = false;
                cmbOutreachBasketball.Visible = true;
                cmbOutreachBasketball.Style.Add("z-index", "99999");

                cmbOutreachBasketballCancel.Enabled = true;
                cmbOutreachBasketballCancel.Visible = true;
                cmbOutreachBasketballCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("Outreach Basketball");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbBoysOutreachBball.Checked)
                {
                    cmbOutreachBasketballRemove.Visible = true;
                    cmbOutreachBasketballRemove.Style.Add("z-index", "99999");

                    cmbOutreachBasketballCancel.Enabled = true;
                    cmbOutreachBasketballCancel.Visible = true;
                    cmbOutreachBasketballCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Outreach Basketball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void rblOutreachBasketball_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chbBoysOutreachBball.Checked)
            {
                cmbOutreachBasketball.Enabled = true;
            }
            else
            {
                if (!chbBoysOutreachBball.Checked)
                {
                    cmbConfirmDeleteOutreach.Enabled = true;
                }
            }
        }

        protected void cmbOutreachBasketball_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Outreach Basketball");
        }

        protected void cmbOutreachBasketballCancel_Click(object sender, EventArgs e)
        {
            //OutreachBasketball Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("Outreach Basketball");

            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cmbOutreachBasketballRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbConfirmDeleteOutreach_Click(object sender, EventArgs e)
        {
            DeleteOutReachBasketball();
            UpdateVolunteerTable();
            System.Threading.Thread.Sleep(2000);//Wait 2 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbConfirmDeleteOutreach.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
        }

        protected void chbHSBasketLeague_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chbHSBasketLeague.Checked)
            {
                cmbHSBasketballLeagueConfirm.Visible = true;
                cmbHSBasketballLeagueConfirm.Style.Add("z-index", "99999");

                cmbHSBasketballLeagueCancel.Enabled = true;
                cmbHSBasketballLeagueCancel.Visible = true;
                cmbHSBasketballLeagueCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer. Please choose.";

                PopulateRadioButtonLists("HS Basketball League");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbHSBasketLeague.Checked)
                {
                    cmbHSBasketballLeagueRemove.Visible = true;
                    cmbHSBasketballLeagueRemove.Style.Add("z-index", "99999");

                    cmbHSBasketballLeagueCancel.Enabled = true;
                    cmbHSBasketballLeagueCancel.Visible = true;
                    cmbHSBasketballLeagueCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("HS Basketball League");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbDiscipleshipmentorWaitingList_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbDiscipleshipmentorTrainingDone_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDiscipleshipmentorTrainingDone.Checked)
            {
                //txbDiscipleshipmentorTrainedDate.Enabled = true;
                ddlDayBirth2.Enabled = true;
                //ddlDayBirth3.Enabled = true;
                ddlMonthBirth2.Enabled = true;
                //ddlMonthBirth3.Enabled = true;
                ddlYearBirth2.Enabled = true;
                //ddlYearBirth3.Enabled = true;
            }
            //else

        }

        protected void chbSummerDayCamp_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            DisableEntireScreen();

            if (chbSummerDayCamp.Checked)
            {
                cmbSummerDayCampConfirm.Visible = true;
                cmbSummerDayCampConfirm.Style.Add("z-index", "99999");

                cmbSummerDayCampCancel.Enabled = true;
                cmbSummerDayCampCancel.Visible = true;
                cmbSummerDayCampCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Style.Add("height", "700");

                PopulateRadioButtonLists("SummerDayCamp");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSummerDayCamp.Checked)
                {
                    cmbSummerDayCampRemove.Visible = true;
                    cmbSummerDayCampRemove.Style.Add("z-index", "99999");

                    cmbSummerDayCampCancel.Enabled = true;
                    cmbSummerDayCampCancel.Visible = true;
                    cmbSummerDayCampCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";
                    lblProgramManagement.Style.Add("height", "700");

                    PopulateRadioButtonListsAndChoice("SummerDayCamp");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }


        protected void PopulateBaseballRadioButton()
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

            rblBaseball.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS.Tables["BaseballProgramSections"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                rblBaseball.Items.Add(myDataRowPO[0].ToString());
            }
            custDS.Clear();
        }


        protected void PopulateOutreachBasketbllRadioButton()
        {
            //Load the dropdown list for the sections.
            string sql = "Select sectionname "
                       + "from UIF_PerformingArts.dbo.OutreachBasketballProgramSections "
                       + "group by sectionname "
                       + "order by sectionname ";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            SqlDataAdapter custDA = new SqlDataAdapter();
            custDA.SelectCommand = cmd;
            DataSet custDS = new DataSet();
            custDA.Fill(custDS, "OutreachBasketballProgramSections");

            rblOutreachBasketball.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS.Tables["OutreachBasketballProgramSections"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                rblOutreachBasketball.Items.Add(myDataRowPO[0].ToString());
            }
            custDS.Clear();
        }


        protected void PopulateBasketbllTEAMSRadioButton()
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

            cblProgramManagement.Items.Clear();
            //Iterate over setup records and call method to do the work on each one...RCM..
            foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSProgramSections"].Rows)
            {
                //Adding options to the drop downs for a new entry.
                cblProgramManagement.Items.Add(myDataRowPO[0].ToString());
            }
            custDS.Clear();
        }

        protected void chbLittleLeagueBaseball_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chbLittleLeagueBaseball.Checked)
            {
                cmbBaseball.Visible = true;
                cmbBaseball.Style.Add("z-index", "99999");

                cmbBaseballCancel.Enabled = true;
                cmbBaseballCancel.Visible = true;
                cmbBaseballCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("Baseball");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbLittleLeagueBaseball.Checked)
                {
                    cmbRemoveBaseball.Visible = true;
                    cmbRemoveBaseball.Style.Add("z-index", "99999");

                    cmbBaseballCancel.Enabled = true;
                    cmbBaseballCancel.Visible = true;
                    cmbBaseballCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Baseball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void RemoveVolunteersFromProgram(string Program)
        {
            string tablename = "";

            try
            {
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
                else if (Program == "Special Events")
                {
                    tablename = "SpecialEvents";
                }
                else if (Program == "MSHSChoir")
                {
                    tablename = "MSHSChoir";
                }
                else if (Program == "ChildrensChoir")
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
                else if (Program == "PAA")
                {
                    tablename = "PAA";
                }
                else if (Program == "SummerDayCamp")
                {
                    tablename = "SummerDayCamp";
                }
                else if (Program == "ImpactUrbanSchools")
                {
                    tablename = "ImpactUrbanSchools";
                }
                else if (Program == "AcademicReadingSupport")
                {
                    tablename = "AcademicReadingSupport";
                }

                con.Open();
                foreach (ListItem checkboxitem in cblProgramManagement.Items)
                {
                    //Only remove sections that are checked in the 
                    //checkboxlist....RCM..8/13/12.
                    if (checkboxitem.Selected == true)
                    {
                        string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo." + "[" + tablename + "Enrollment] "
                                                             + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                             + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                             + "AND sectionname = '" + checkboxitem.Text.Trim() + "' "
                                                             + "AND volunteer = 1 ";

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
                }
                con.Close();
                DetermineCheckBoxStatus(Program);
            }
            catch (Exception lkllaa)
            {

            }
            finally
            {
                //con.Close();
                //UpdateStudentTable();
            }
        }

        protected void RemoveVolunteersFrom3on3BasketballLeague()
        {
            //Remove volunteer from LittleLeagueBaseball  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.[3on3BasketballEnrollment] "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     //+ "AND sectionname = '" + rblBaseball.SelectedItem.Text + "' "
                                                     + "AND volunteer = 1 ";
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

        protected void RemoveVolunteersFromBaseball()
        {
            //Remove volunteer from LittleLeagueBaseball  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.BaseballEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND sectionname = '" + rblBaseball.SelectedItem.Text + "' "
                                                     + "AND volunteer = 1 ";
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


        protected void RemoveVolunteersFromSoccerIntraMurals()
        {
            //Remove volunteer from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.SoccerIntraMuralsEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND volunteer = 1 ";
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

        protected void RemoveVolunteersFrombiblestudy()
        {
            //Remove volunteer from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.biblestudyEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND volunteer = 1 ";
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

        protected void RemoveVolunteersFrom3on3Basketball()
        {
            //Remove volunteer from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.[3on3BasketballEnrollment] "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND volunteer = 1 ";
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

        protected void RemoveVolunteersFromSoccerTEAMS()
        {
            //Remove volunteer from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.SoccerTEAMSEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND volunteer = 1 ";
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

        protected void RemoveVolunteersFromMondayNights()
        {
            //Remove volunteer from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.MondayNightsEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND volunteer = 1 ";
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

        protected void cmbRemoveBaseball_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("Baseball");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbRemoveBaseball.Visible = false;
            cmbBaseballCancel.Visible = false;
        }

        protected void cmbBaseballCancel_Click(object sender, EventArgs e)
        {
            //Baseball Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("Baseball");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbBaseball.Visible = false;
            cmbBaseballCancel.Visible = false;
            cmbRemoveBaseball.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void EnableAllControls()
        {

        }

        protected void DisableAllControls()
        {
            txtAddress1.Enabled = false;
            txtChurch.Enabled = false;
            txtCity.Enabled = false;
            txtCity.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txbEmergencyPhone.Enabled = false;
            txbEmergencyRelationship.Enabled = false;
            txbEmergRelationship.Enabled = false;
            txbHealthConditions.Enabled = false;
            txbNotes.Enabled = false;
            txbDiscipleshipMentorNotes.Enabled = false;
            txbAllergies.Enabled = false;
            ddlAge.Enabled = false;
            ddlDayBirth.Enabled = false;
            ddlDayBirth2.Enabled = false;
            ddlDayBirth3.Enabled = false;
            ddlGender.Enabled = false;
            ddlMailingListCodes.Enabled = false;
            ddlYearBirth.Enabled = false;
            ddlTShirtSize.Enabled = false;
            chbMailingList.Enabled = false;
            chbOfficeVolunteer.Enabled = false;

        }


        protected void cmbBaseball_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Baseball");
        }

        protected void lbVolunteerDetails_Click(object sender, EventArgs e)
        {
            //Script..have you comitted any changes you've made?
            UpdateVolunteerTable();//Update the information to save..RCM..9/7/11.

            Response.Redirect("VolunteerDetails.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + txtLastName.Text + "&VolunteerFirstName=" + txtFirstName.Text + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void chbGeneralInformation_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbSpiritualJourney_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chb3on3Basketball_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chb3on3Basketball.Checked)
            {
                cmb3on3BasketballConfirm.Visible = true;
                cmb3on3BasketballConfirm.Style.Add("z-index", "99999");

                cmb3on3BasketballCancel.Enabled = true;
                cmb3on3BasketballCancel.Visible = true;
                cmb3on3BasketballCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer. Please choose.";

                PopulateRadioButtonLists("3on3 Basketball");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chb3on3Basketball.Checked)
                {
                    cmb3on3BasketballRemove.Visible = true;
                    cmb3on3BasketballRemove.Style.Add("z-index", "99999");

                    cmb3on3BasketballCancel.Enabled = true;
                    cmb3on3BasketballCancel.Visible = true;
                    cmb3on3BasketballCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("3on3 Basketball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbSoccerInterMurals_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chbSoccerInterMurals.Checked)
            {
                cmbSoccerIntraMuralsConfirm.Visible = true;
                cmbSoccerIntraMuralsConfirm.Style.Add("z-index", "99999");

                cmbSoccerIntraMuralsCancel.Enabled = true;
                cmbSoccerIntraMuralsCancel.Visible = true;
                cmbSoccerIntraMuralsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("SoccerIntraMurals");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSoccerInterMurals.Checked)
                {
                    cmbSoccerIntraMuralsRemove.Visible = true;
                    cmbSoccerIntraMuralsRemove.Style.Add("z-index", "99999");

                    cmbSoccerIntraMuralsCancel.Enabled = true;
                    cmbSoccerIntraMuralsCancel.Visible = true;
                    cmbSoccerIntraMuralsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("SoccerIntraMurals");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbShakes_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbShakes.Checked)
            {
                cmbShakesConfirm.Visible = true;
                cmbShakesConfirm.Style.Add("z-index", "99999");

                cmbShakesCancel.Enabled = true;
                cmbShakesCancel.Visible = true;
                cmbShakesCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("Shakes");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbShakes.Checked)
                {
                    cmbShakesRemove.Visible = true;
                    cmbShakesRemove.Style.Add("z-index", "99999");

                    cmbShakesCancel.Enabled = true;
                    cmbShakesCancel.Visible = true;
                    cmbShakesCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Shakes");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbOliverFootballBible_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chbOliverFootballBible.Checked)
            {
                cmbBibleStudy.Visible = true;
                cmbBibleStudy.Style.Add("z-index", "99999");

                cmbBibleStudyCancel.Enabled = true;
                cmbBibleStudyCancel.Visible = true;
                cmbBibleStudyCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer. Please choose.";

                PopulateRadioButtonLists("Bible Study");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbOliverFootballBible.Checked)
                {
                    cmbBibleStudyRemove.Visible = true;
                    cmbBibleStudyRemove.Style.Add("z-index", "99999");

                    cmbBibleStudyCancel.Enabled = true;
                    cmbBibleStudyCancel.Visible = true;
                    cmbBibleStudyCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Bible Study");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbSoccerLgTravel_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chbSoccerLgTravel.Checked)
            {
                cmbSoccerTEAMSConfirm.Visible = true;
                cmbSoccerTEAMSConfirm.Style.Add("z-index", "99999");

                cmbSoccerTEAMSCancel.Enabled = true;
                cmbSoccerTEAMSCancel.Visible = true;
                cmbSoccerTEAMSCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer. Please choose.";

                PopulateRadioButtonLists("SoccerTEAMS");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSoccerLgTravel.Checked)
                {
                    cmbSoccerTEAMSRemove.Visible = true;
                    cmbSoccerTEAMSRemove.Style.Add("z-index", "99999");

                    cmbSoccerTEAMSCancel.Enabled = true;
                    cmbSoccerTEAMSCancel.Visible = true;
                    cmbSoccerTEAMSCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("SoccerTEAMS");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void cmbSoccerIntraMuralsConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("SoccerIntraMurals");
        }

        protected void cmbSoccerTEAMSConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("SoccerTEAMS");
        }

        protected void AddStudentsToProgram(string Program)
        {
            bool GoodUpdate = false;
            string tablename = "";
            string sqlInsertStatement_ProgramsList = "";

            if (cblProgramManagement.SelectedItem.Selected)
            {
                try
                {
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
                    else if (Program == "Special Events")
                    {
                        tablename = "SpecialEvents";
                    }
                    else if (Program == "MSHSChoir")
                    {
                        tablename = "MSHSChoir";
                    }
                    else if (Program == "ChildrensChoir")
                    {
                        tablename = "ChildrensChoir";
                    }
                    else if (Program == "PAA")
                    {
                        tablename = "PAA";
                    }
                    else if (Program == "Singers")
                    {
                        tablename = "Singers";
                    }
                    else if (Program == "Shakes")
                    {
                        tablename = "Shakes";
                    }
                    else if (Program == "ImpactUrbanSchools")
                    {
                        tablename = "ImpactUrbanSchools";
                    }
                    else if (Program == "SummerDayCamp")
                    {
                        tablename = "SummerDayCamp";
                    }
                    else if (Program == "SATPrep")
                    {
                        tablename = "SATPrep";
                    }
                    else if (Program == "AcademicReadingSupport")
                    {
                        tablename = "AcademicReadingSupport";
                    }

                    con2.Open();
                    foreach (ListItem item in cblProgramManagement.Items)
                    {
                        if (item.Selected == true)//Only the chosen sections.
                        {
                            if (item.Enabled == true)//Prevents duplicate sections.
                            {
                                if (Request.QueryString["newstudent"] == "newstudent")
                                {
                                    sqlInsertStatement_ProgramsList = "INSERT INTO UIF_PerformingArts.dbo.[" + tablename + "Enrollment] "
                                                                    + "values ( "
                                                                    + "'" + txtLastName.Text.Trim() + "', "
                                                                    + "'" + txtFirstName.Text.Trim() + "', "
                                                                    + "'" + item.Text + "', "
                                                                    + "'Meet Time', "
                                                                    + "'Thursday', "
                                                                    + "'Gym', "
                                                                    + "0, "
                                                                    + "1, "
                                                                    + "'N/A', "
                                                                    + "'Instructor', "
                                                                    + "'DevotionalLeader', "
                                                                    + "'" + System.DateTime.Now.ToString() + "', "
                                                                    + "'" + System.DateTime.Now.ToString() + "', "
                                                                    + "333, "
                                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                                    + "0, "
                                                                    + "0, "
                                                                    + "'', "
                                                                    + "0, "
                                                                    + "'Volunteer', "
                                                                    + "0, "
                                                                    + "1, "
                                                                    + "'Select a team?') ";
                                }
                                else
                                {
                                    //here..  could make it work off the Request.QueryString variables....RCM..1/23/12.
                                    sqlInsertStatement_ProgramsList = "INSERT INTO UIF_PerformingArts.dbo.[" + tablename + "Enrollment] "
                                                                    + "values ( "
                                                                    + "'" + txtLastName.Text.Trim() + "', "
                                                                    + "'" + txtFirstName.Text.Trim() + "', "
                                                                    + "'" + item.Text + "', "
                                                                    + "'Meet Time', "
                                                                    + "'Thursday', "
                                                                    + "'Gym', "
                                                                    + "0, "
                                                                    + "1, "
                                                                    + "'N/A', "
                                                                    + "'Instructor', "
                                                                    + "'DevotionalLeader', "
                                                                    + "'" + System.DateTime.Now.ToString() + "', "
                                                                    + "'" + System.DateTime.Now.ToString() + "', "
                                                                    + "333, "
                                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                                    + "0, "
                                                                    + "0, "
                                                                    + "'', "
                                                                    + "0, "
                                                                    + "'Volunteer', "
                                                                    + "0, "
                                                                    + "1, "
                                                                    + "'Select a team?') ";
                                }

                                //create a SQL command to update record
                                SqlCommand sqlInsertCommand_ProgramsList = new SqlCommand(sqlInsertStatement_ProgramsList, con2);
                                if (sqlInsertCommand_ProgramsList.ExecuteNonQuery() > 0)
                                {
                                    if (Program == "BasketballTEAMS")
                                    {
                                        chbBasketballTEAMS.Checked = true;
                                    }
                                    else if (Program == "Outreach Basketball")
                                    {
                                        chbBoysOutreachBball.Checked = true;
                                    }
                                    else if (Program == "3on3 Basketball")
                                    {
                                        chb3on3Basketball.Checked = true;
                                    }
                                    else if (Program == "Baseball")
                                    {
                                        chbLittleLeagueBaseball.Checked = true;
                                    }
                                    else if (Program == "MS Basketball League")
                                    {
                                        chbMSBasketLeague.Checked = true;
                                    }
                                    else if (Program == "HS Basketball League")
                                    {
                                        chbHSBasketLeague.Checked = true;
                                    }
                                    else if (Program == "SoccerTEAMS")
                                    {
                                        chbSoccerLgTravel.Checked = true;
                                    }
                                    else if (Program == "SoccerIntraMurals")
                                    {
                                        chbSoccerInterMurals.Checked = true;
                                    }
                                    else if (Program == "MondayNights")
                                    {
                                        chbMondayNights.Checked = true;
                                    }
                                    else if (Program == "Bible Study")
                                    {
                                        chbOliverFootballBible.Checked = true;
                                    }
                                    else if (Program == "Special Events")
                                    {
                                        chbSpecialEvents.Checked = true;
                                    }
                                    else if (Program == "MSHSChoir")
                                    {
                                        chbMSHSChoir.Checked = true;
                                    }
                                    else if (Program == "ChildrensChoir")
                                    {
                                        chbChildrensChoir.Checked = true;
                                    }
                                    else if (Program == "PAA")
                                    {
                                        chbPerformingArts.Checked = true;
                                    }
                                    else if (Program == "Singers")
                                    {
                                        chbSingers.Checked = true;
                                    }
                                    else if (Program == "Shakes")
                                    {
                                        chbShakes.Checked = true;
                                    }
                                    else if (Program == "ImpactUrbanSchools")
                                    {
                                        if (Department == "Athletics")
                                        {
                                            chbImpactUrbanSchools.Checked = true;
                                        }
                                        else if (Department == "PerformingArts")
                                        {
                                            chbImpactUrbanSchoolsPA.Checked = true;
                                        }
                                        else if (Department == "Education")
                                        {
                                            chbImpactUrbanSchoolsAcademics.Checked = true;
                                        }
                                    }
                                    else if (Program == "SummerDayCamp")
                                    {
                                        chbSummerDayCamp.Checked = true;
                                    }
                                    else if (Program == "SATPrep")
                                    {
                                        chbSATPrepClass.Checked = true;
                                    }
                                    else if (Program == "AcademicReadingSupport")
                                    {
                                        chbReadingProgram.Checked = true;
                                    }
                                }
                                else
                                {
                                    if (Program == "BasketballTEAMS")
                                    {
                                        chbBasketballTEAMS.Checked = false;
                                    }
                                    else if (Program == "Outreach Basketball")
                                    {
                                        chbBoysOutreachBball.Checked = false;
                                    }
                                    else if (Program == "3on3 Basketball")
                                    {
                                        chb3on3Basketball.Checked = false;
                                    }
                                    else if (Program == "Baseball")
                                    {
                                        chbLittleLeagueBaseball.Checked = false;
                                    }
                                    else if (Program == "MS Basketball League")
                                    {
                                        chbMSBasketLeague.Checked = false;
                                    }
                                    else if (Program == "HS Basketball League")
                                    {
                                        chbHSBasketLeague.Checked = false;
                                    }
                                    else if (Program == "SoccerTEAMS")
                                    {
                                        chbSoccerLgTravel.Checked = false;
                                    }
                                    else if (Program == "SoccerIntraMurals")
                                    {
                                        chbSoccerInterMurals.Checked = false;
                                    }
                                    else if (Program == "MondayNights")
                                    {
                                        chbMondayNights.Checked = false;
                                    }
                                    else if (Program == "Bible Study")
                                    {
                                        chbOliverFootballBible.Checked = false;
                                    }
                                    else if (Program == "Special Events")
                                    {
                                        chbSpecialEvents.Checked = false;
                                    }
                                    else if (Program == "MSHSChoir")
                                    {
                                        chbMSHSChoir.Checked = false;
                                    }
                                    else if (Program == "ChildrensChoir")
                                    {
                                        chbChildrensChoir.Checked = false;
                                    }
                                    else if (Program == "PAA")
                                    {
                                        chbPerformingArts.Checked = false;
                                    }
                                    else if (Program == "Singers")
                                    {
                                        chbSingers.Checked = false;
                                    }
                                    else if (Program == "Shakes")
                                    {
                                        chbShakes.Checked = false;
                                    }
                                    else if (Program == "ImpactUrbanSchools")
                                    {
                                        if (Department == "Athletics")
                                        {
                                            chbImpactUrbanSchools.Checked = false;
                                        }
                                        else if (Department == "PerformingArts")
                                        {
                                            chbImpactUrbanSchoolsPA.Checked = false;
                                        }
                                        else if (Department == "Education")
                                        {
                                            chbImpactUrbanSchoolsAcademics.Checked = false;
                                        }
                                    }
                                    else if (Program == "SummerDayCamp")
                                    {
                                        chbSummerDayCamp.Checked = false;
                                    }
                                    else if (Program == "SATPrep")
                                    {
                                        chbSATPrepClass.Checked = false;
                                    }
                                    else if (Program == "AcademicReadingSupport")
                                    {
                                        chbReadingProgram.Checked = false;
                                    }
                                }
                                //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
                                lblProgramManagement.Visible = false;
                                pnlProgramManagement.Visible = false;
                                if (Program == "BasketballTEAMS")
                                {
                                    cmbProgramManagement.Visible = false;
                                    cmbProgramManageCancel.Visible = false;
                                }
                                else if (Program == "Outreach Basketball")
                                {
                                    cmbOutreachBasketball.Visible = false;
                                    cmbOutreachBasketballCancel.Visible = false;
                                }
                                else if (Program == "3on3 Basketball")
                                {
                                    cmb3on3BasketballConfirm.Visible = false;
                                    cmb3on3BasketballCancel.Visible = false;
                                }
                                else if (Program == "Baseball")
                                {
                                    cmbBaseball.Visible = false;
                                    cmbBaseballCancel.Visible = false;
                                }
                                else if (Program == "MS Basketball League")
                                {
                                    cmbMSBasketballLeagueConfirm.Visible = false;
                                    cmbMSBasketballLeagueCancel.Visible = false;
                                }
                                else if (Program == "HS Basketball League")
                                {
                                    cmbHSBasketballLeagueConfirm.Visible = false;
                                    cmbHSBasketballLeagueCancel.Visible = false;
                                }
                                else if (Program == "SoccerTEAMS")
                                {
                                    cmbSoccerTEAMSConfirm.Visible = false;
                                    cmbSoccerTEAMSCancel.Visible = false;
                                }
                                else if (Program == "SoccerIntraMurals")
                                {
                                    cmbSoccerIntraMuralsConfirm.Visible = false;
                                    cmbSoccerIntraMuralsCancel.Visible = false;
                                }
                                else if (Program == "MondayNights")
                                {
                                    cmbMondayNightsConfirm.Visible = false;
                                    cmbMondayNightsCancel.Visible = false;
                                }
                                else if (Program == "Bible Study")
                                {
                                    cmbBibleStudy.Visible = false;
                                    cmbBibleStudyCancel.Visible = false;
                                }
                                else if (Program == "Special Events")
                                {
                                    cmbSpecialEventsConfirm.Visible = false;
                                    cmbSpecialEventsCancel.Visible = false;
                                }
                                else if (Program == "MSHSChoir")
                                {
                                    cmbMSHSChoirConfirm.Visible = false;
                                    cmbMSHSChoirCancel.Visible = false;
                                }
                                else if (Program == "ChildrensChoir")
                                {
                                    cmbChildrensChoirConfirm.Visible = false;
                                    cmbChildrensChoirCancel.Visible = false;
                                }
                                else if (Program == "PAA")
                                {
                                    cmbPAAConfirm.Visible = false;
                                    cmbPAACancel.Visible = false;
                                }
                                else if (Program == "Singers")
                                {
                                    cmbSingersConfirm.Visible = false;
                                    cmbSingersCancel.Visible = false;
                                }
                                else if (Program == "Shakes")
                                {
                                    cmbShakesConfirm.Visible = false;
                                    cmbShakesCancel.Visible = false;
                                }
                                else if (Program == "ImpactUrbanSchools")
                                {
                                    cmbImpactUrbanSchoolsConfirm.Visible = false;
                                    cmbImpactUrbanSchoolsCancel.Visible = false;
                                }
                                else if (Program == "SummerDayCamp")
                                {
                                    cmbSummerDayCampConfirm.Visible = false;
                                    cmbSummerDayCampCancel.Visible = false;
                                }
                                else if (Program == "SATPrep")
                                {
                                    //cmbSATPrepConfirm.Visible = false;
                                    //cmbSATPrepCancel.Visible = false;
                                }
                                else if (Program == "AcademicReadingSupport")
                                {
                                    cmbAcademicReadingSupportConfirm.Visible = false;
                                    cmbAcademicReadingSupportCancel.Visible = false;
                                }
                            }
                            cblProgramManagement.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblInformation.Enabled = true;
                    lblInformation.Text = "There was an exception INSERTING NEW data into the tables..  Please fix and try again MSG: " + ex.Message.ToString();

                    //add code to send error to admin via email
                    //Session["Exception"] = ex.Message.ToString();
                    //Response.Redirect("Error.aspx");
                }
                finally
                {
                    //Close connection
                    con2.Close();
                    con2.Dispose();
                    GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
                }
            }
            else
            {

            }
        }

        protected void cmbMondayNightsConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("MondayNights");
        }

        protected void cmbHSBasketballLeagueConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("HS Basketball League");
        }

        protected void cmb3on3BasketballConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("3on3 Basketball");
        }

        protected void cmbBibleStudy_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Bible Study");
        }

        protected void cmb3on3BasketballCancel_Click(object sender, EventArgs e)
        {
            //3on3 Basketball Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("3on3 Basketball");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmb3on3BasketballConfirm.Visible = false;
            cmb3on3BasketballCancel.Visible = false;
            cmb3on3BasketballRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbHSBasketballLeagueCancel_Click(object sender, EventArgs e)
        {
            //HS Basketball League Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("HS Basketball League");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbHSBasketballLeagueConfirm.Visible = false;
            cmbHSBasketballLeagueCancel.Visible = false;
            cmbHSBasketballLeagueRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbMSBasketballLeagueCancel_Click(object sender, EventArgs e)
        {
            //MS Basketball League Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("MS Basketball League");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbMSBasketballLeagueConfirm.Visible = false;
            cmbMSBasketballLeagueCancel.Visible = false;
            cmbMSBasketballLeagueRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbSoccerIntraMuralsCancel_Click(object sender, EventArgs e)
        {
            //SoccerIntraMurals Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("SoccerIntraMurals");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbSoccerIntraMuralsConfirm.Visible = false;
            cmbSoccerIntraMuralsCancel.Visible = false;
            cmbSoccerIntraMuralsRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbSoccerTEAMSCancel_Click(object sender, EventArgs e)
        {
            //SoccerTEAMS Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("SoccerTEAMS");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbSoccerTEAMSConfirm.Visible = false;
            cmbSoccerTEAMSCancel.Visible = false;
            cmbSoccerTEAMSRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbMondayNightsCancel_Click(object sender, EventArgs e)
        {
            //MondayNights Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("MondayNights");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbMondayNightsConfirm.Visible = false;
            cmbMondayNightsCancel.Visible = false;
            cmbMondayNightsRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbBibleStudyCancel_Click(object sender, EventArgs e)
        {
            //BibleStudy Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("Bible Study");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbBibleStudy.Visible = false;
            cmbBibleStudyCancel.Visible = false;
            cmbBibleStudyRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmb3on3BasketballRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("3on3 Basketball");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmb3on3BasketballCancel.Visible = false;
            cmb3on3BasketballRemove.Visible = false;
        }

        protected void cmbBibleStudyRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("Bible Study");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbBibleStudyRemove.Visible = false;
            cmbBibleStudyCancel.Visible = false;
        }

        protected void cmbHSBasketballLeagueRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("HS Basketball League");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbHSBasketballLeagueCancel.Visible = false;
            cmbHSBasketballLeagueRemove.Visible = false;
        }

        protected void cmbMSBasketballLeagueRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("MS Basketball League");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbMSBasketballLeagueRemove.Visible = false;
            cmbMSBasketballLeagueCancel.Visible = false;
        }

        protected void cmbOutreachBasketballRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("Outreach Basketball");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbOutreachBasketballRemove.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
        }

        protected void cmbSoccerIntraMuralsRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("SoccerIntraMurals");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbSoccerIntraMuralsRemove.Visible = false;
            cmbSoccerIntraMuralsCancel.Visible = false;
        }

        protected void cmbSoccerTEAMSRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("SoccerTEAMS");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbSoccerTEAMSRemove.Visible = false;
            cmbSoccerTEAMSCancel.Visible = false;
        }

        protected void cmbMSBasketballLeagueConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("MS Basketball League");
        }

        protected void cmbMondayNightsRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("MondayNights");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbMondayNightsRemove.Visible = false;
            cmbMondayNightsCancel.Visible = false;
        }

        protected void chbStaff_CheckedChanged(object sender, EventArgs e)
        {
            if (chbStaff.Checked)
            {
                chbStaff.Attributes.Add("onclick", "return false;");
            }
        }

        protected void chbSpecialEvents_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            if (chbSpecialEvents.Checked)
            {
                cmbSpecialEventsConfirm.Visible = true;
                cmbSpecialEventsConfirm.Style.Add("z-index", "99999");

                cmbSpecialEventsCancel.Enabled = true;
                cmbSpecialEventsCancel.Visible = true;
                cmbSpecialEventsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("Special Events");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSpecialEvents.Checked)
                {
                    cmbSpecialEventsRemove.Visible = true;
                    cmbSpecialEventsRemove.Style.Add("z-index", "99999");

                    cmbSpecialEventsCancel.Enabled = true;
                    cmbSpecialEventsCancel.Visible = true;
                    cmbSpecialEventsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Special Events");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void cblProgramManagement_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cmbProgramManagement.Enabled = true;
            cmbMondayNightsConfirm.Enabled = true;
            cmbMSBasketballLeagueConfirm.Enabled = true;
            cmbHSBasketballLeagueConfirm.Enabled = true;
            cmbSoccerIntraMuralsConfirm.Enabled = true;
            cmbSoccerTEAMSConfirm.Enabled = true;
            cmb3on3BasketballConfirm.Enabled = true;
            cmbBibleStudy.Enabled = true;
            cmbBaseball.Enabled = true;
            cmbOutreachBasketball.Enabled = true;
            cmbSpecialEventsConfirm.Enabled = true;

            cmbMSHSChoirConfirm.Enabled = true;
            cmbChildrensChoirConfirm.Enabled = true;
            cmbSingersConfirm.Enabled = true;
            cmbShakesConfirm.Enabled = true;

            cmbImpactUrbanSchoolsConfirm.Enabled = true;
            cmbAcademicReadingSupportConfirm.Enabled = true;
            cmbSummerDayCampConfirm.Enabled = true;
        }

        protected void rblProgramManagement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbSpecialEventsCancel_Click(object sender, EventArgs e)
        {
            //SpecialEvents Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("Special Events");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbSpecialEventsConfirm.Visible = false;
            cmbSpecialEventsCancel.Visible = false;
            cmbSpecialEventsRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
        }

        protected void cmbSpecialEventsConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Special Events");
        }

        protected void lbBaseball_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbLittleLeagueBaseball.Checked)
            {
                //Add volunteer to another section.
                cmbBaseball.Visible = true;
                cmbBaseball.Style.Add("z-index", "99999");

                cmbBaseballCancel.Enabled = true;
                cmbBaseballCancel.Visible = true;
                cmbBaseballCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("Baseball");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbLittleLeagueBaseball.Checked)
                {
                    //Act like a normal add..
                    cmbBaseball.Visible = true;
                    cmbBaseball.Style.Add("z-index", "99999");

                    cmbBaseballCancel.Enabled = true;
                    cmbBaseballCancel.Visible = true;
                    cmbBaseballCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("Baseball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbBibleStudy_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbOliverFootballBible.Checked)
            {
                //Add volunteer to another section.
                cmbBibleStudy.Visible = true;
                cmbBibleStudy.Style.Add("z-index", "99999");

                cmbBibleStudyCancel.Enabled = true;
                cmbBibleStudyCancel.Visible = true;
                cmbBibleStudyCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("Bible Study");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbOliverFootballBible.Checked)
                {
                    //Act like a normal add..
                    cmbBibleStudy.Visible = true;
                    cmbBibleStudy.Style.Add("z-index", "99999");

                    cmbBibleStudyCancel.Enabled = true;
                    cmbBibleStudyCancel.Visible = true;
                    cmbBibleStudyCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("Bible Study");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbMondayNights_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbMondayNights.Checked)
            {
                //Add student to another section.
                cmbMondayNightsConfirm.Visible = true;
                cmbMondayNightsConfirm.Style.Add("z-index", "99999");

                cmbMondayNightsCancel.Enabled = true;
                cmbMondayNightsCancel.Visible = true;
                cmbMondayNightsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("MondayNights");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbMondayNights.Checked)
                {
                    //Act like a normal add..
                    cmbMondayNightsConfirm.Visible = true;
                    cmbMondayNightsConfirm.Style.Add("z-index", "99999");

                    cmbMondayNightsCancel.Enabled = true;
                    cmbMondayNightsCancel.Visible = true;
                    cmbMondayNightsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("MondayNights");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbSpecialEvents_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbSpecialEvents.Checked)
            {
                //Add student to another section.
                cmbSpecialEventsConfirm.Visible = true;
                cmbSpecialEventsConfirm.Style.Add("z-index", "99999");

                cmbSpecialEventsCancel.Enabled = true;
                cmbSpecialEventsCancel.Visible = true;
                cmbSpecialEventsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("Special Events");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSpecialEvents.Checked)
                {
                    //Act like a normal add..
                    cmbSpecialEventsConfirm.Visible = true;
                    cmbSpecialEventsConfirm.Style.Add("z-index", "99999");

                    cmbSpecialEventsCancel.Enabled = true;
                    cmbSpecialEventsCancel.Visible = true;
                    cmbSpecialEventsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("Special Events");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }

        }

        protected void cmbSpecialEventsRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("Special Events");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbSpecialEventsRemove.Visible = false;
            cmbSpecialEventsCancel.Visible = false;
        }

        protected void lbOutreachBasketball_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbBoysOutreachBball.Checked)
            {
                //Add student to another section.
                cmbOutreachBasketball.Visible = true;
                cmbOutreachBasketball.Style.Add("z-index", "99999");

                cmbOutreachBasketballCancel.Enabled = true;
                cmbOutreachBasketballCancel.Visible = true;
                cmbOutreachBasketballCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("Outreach Basketball");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbBoysOutreachBball.Checked)
                {
                    //Act like a normal add..
                    cmbOutreachBasketball.Visible = true;
                    cmbOutreachBasketball.Style.Add("z-index", "99999");

                    cmbOutreachBasketballCancel.Enabled = true;
                    cmbOutreachBasketballCancel.Visible = true;
                    cmbOutreachBasketballCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("Outreach Basketball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbBasketballTEAMS_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbBasketballTEAMS.Checked)
            {
                //Add student to another section.
                cmbProgramManagement.Visible = true;
                cmbProgramManagement.Style.Add("z-index", "99999");

                cmbProgramManageCancel.Enabled = true;
                cmbProgramManageCancel.Visible = true;
                cmbProgramManageCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("BasketballTEAMS");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbBasketballTEAMS.Checked)
                {
                    //Act like a normal add..
                    cmbMondayNightsConfirm.Visible = true;
                    cmbMondayNightsConfirm.Style.Add("z-index", "99999");

                    cmbProgramManageCancel.Enabled = true;
                    cmbProgramManageCancel.Visible = true;
                    cmbProgramManageCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("BasketballTEAMS");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbMSBasketballLeague_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbMSBasketLeague.Checked)
            {
                //Add student to another section.
                cmbMSBasketballLeagueConfirm.Visible = true;
                cmbMSBasketballLeagueConfirm.Style.Add("z-index", "99999");

                cmbMSBasketballLeagueCancel.Enabled = true;
                cmbMSBasketballLeagueCancel.Visible = true;
                cmbMSBasketballLeagueCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("MS Basketball League");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbMSBasketLeague.Checked)
                {
                    //Act like a normal add..
                    cmbMSBasketballLeagueConfirm.Visible = true;
                    cmbMSBasketballLeagueConfirm.Style.Add("z-index", "99999");

                    cmbMSBasketballLeagueCancel.Enabled = true;
                    cmbMSBasketballLeagueCancel.Visible = true;
                    cmbMSBasketballLeagueCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("MS Basketball League");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }


        protected void lbHSBasketballLeague_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbHSBasketLeague.Checked)
            {
                //Add student to another section.
                cmbHSBasketballLeagueConfirm.Visible = true;
                cmbHSBasketballLeagueConfirm.Style.Add("z-index", "99999");

                cmbHSBasketballLeagueCancel.Enabled = true;
                cmbHSBasketballLeagueCancel.Visible = true;
                cmbHSBasketballLeagueCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("HS Basketball League");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbHSBasketLeague.Checked)
                {
                    //Act like a normal add..
                    cmbHSBasketballLeagueConfirm.Visible = true;
                    cmbHSBasketballLeagueConfirm.Style.Add("z-index", "99999");

                    cmbHSBasketballLeagueCancel.Enabled = true;
                    cmbHSBasketballLeagueCancel.Visible = true;
                    cmbHSBasketballLeagueCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("HS Basketball League");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void cmbMSHSChoirRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("MSHSChoir");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbMSHSChoirRemove.Visible = false;
            cmbMSHSChoirCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbChildrensChoirRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("ChildrensChoir");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbChildrensChoirRemove.Visible = false;
            cmbChildrensChoirCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbSingersRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("Singers");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbSingersRemove.Visible = false;
            cmbSingersCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbShakesRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("Shakes");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbShakesRemove.Visible = false;
            cmbShakesCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbPAARemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("PAA");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbPAARemove.Visible = false;
            cmbPAACancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbSummerDayCampRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("SummerDayCamp");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbSummerDayCampRemove.Visible = false;
            cmbSummerDayCampCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbImpactUrbanSchoolsRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("ImpactUrbanSchools");
            UpdateVolunteerTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbImpactUrbanSchoolsRemove.Visible = false;
            cmbImpactUrbanSchoolsCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbMSHSChoirCancel_Click(object sender, EventArgs e)
        {
            //MSHSChoir Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("MSHSChoir");
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbMSHSChoirConfirm.Visible = false;
            cmbMSHSChoirCancel.Visible = false;
            cmbMSHSChoirRemove.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmbChildrensChoirCancel_Click(object sender, EventArgs e)
        {
            //ChildrensChoir Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("ChildrensChoir");
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbChildrensChoirConfirm.Visible = false;
            cmbChildrensChoirCancel.Visible = false;
            cmbChildrensChoirRemove.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmbSingersCancel_Click(object sender, EventArgs e)
        {
            //Singers Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("Singers");
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbSingersConfirm.Visible = false;
            cmbSingersCancel.Visible = false;
            cmbSingersRemove.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmbShakesCancel_Click(object sender, EventArgs e)
        {
            //Shakes Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("Shakes");
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbShakesConfirm.Visible = false;
            cmbShakesCancel.Visible = false;
            cmbShakesRemove.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmbPAACancel_Click(object sender, EventArgs e)
        {
            //PAA Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("PAA");
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbPAAConfirm.Visible = false;
            cmbPAACancel.Visible = false;
            cmbPAARemove.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmbSummerDayCampCancel_Click(object sender, EventArgs e)
        {
            //SummerDayCamp Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("SummerDayCamp");
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbSummerDayCampConfirm.Visible = false;
            cmbSummerDayCampCancel.Visible = false;
            cmbSummerDayCampRemove.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmbImpactUrbanSchoolsCancel_Click(object sender, EventArgs e)
        {
            //ImpactUrbanSchools Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("ImpactUrbanSchools");
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbImpactUrbanSchoolsConfirm.Visible = false;
            cmbImpactUrbanSchoolsCancel.Visible = false;
            cmbImpactUrbanSchoolsRemove.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void lbSoccerTEAMS_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbSoccerLgTravel.Checked)
            {
                //Add student to another section.
                cmbSoccerTEAMSConfirm.Visible = true;
                cmbSoccerTEAMSConfirm.Style.Add("z-index", "99999");

                cmbSoccerTEAMSCancel.Enabled = true;
                cmbSoccerTEAMSCancel.Visible = true;
                cmbSoccerTEAMSCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("SoccerTEAMS");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSoccerLgTravel.Checked)
                {
                    //Act like a normal add..
                    cmbSoccerTEAMSConfirm.Visible = true;
                    cmbSoccerTEAMSConfirm.Style.Add("z-index", "99999");

                    cmbSoccerTEAMSCancel.Enabled = true;
                    cmbSoccerTEAMSCancel.Visible = true;
                    cmbSoccerTEAMSCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("SoccerTEAMS");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }


        protected void lbSoccerIntraMurals_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbSoccerInterMurals.Checked)
            {
                //Add student to another section.
                cmbSoccerIntraMuralsConfirm.Visible = true;
                cmbSoccerIntraMuralsConfirm.Style.Add("z-index", "99999");

                cmbSoccerIntraMuralsCancel.Enabled = true;
                cmbSoccerIntraMuralsCancel.Visible = true;
                cmbSoccerIntraMuralsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("SoccerIntraMurals");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSoccerInterMurals.Checked)
                {
                    //Act like a normal add..
                    cmbSoccerIntraMuralsConfirm.Visible = true;
                    cmbSoccerIntraMuralsConfirm.Style.Add("z-index", "99999");

                    cmbSoccerIntraMuralsCancel.Enabled = true;
                    cmbSoccerIntraMuralsCancel.Visible = true;
                    cmbSoccerIntraMuralsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("SoccerIntraMurals");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lb3on3Basketball_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chb3on3Basketball.Checked)
            {
                //Add student to another section.
                cmb3on3BasketballConfirm.Visible = true;
                cmb3on3BasketballConfirm.Style.Add("z-index", "99999");

                cmb3on3BasketballCancel.Enabled = true;
                cmb3on3BasketballCancel.Visible = true;
                cmb3on3BasketballCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("3on3 Basketball");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chb3on3Basketball.Checked)
                {
                    //Act like a normal add..
                    cmb3on3BasketballConfirm.Visible = true;
                    cmb3on3BasketballConfirm.Style.Add("z-index", "99999");

                    cmb3on3BasketballCancel.Enabled = true;
                    cmb3on3BasketballCancel.Visible = true;
                    cmb3on3BasketballCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("3on3 Basketball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbImpactUrbanSchoolsAcademics_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (Department == "Athletics")
            {
                if (chbImpactUrbanSchools.Checked)
                {
                    //Add student to another section.
                    cmbImpactUrbanSchoolsConfirm.Visible = true;
                    cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                    cmbImpactUrbanSchoolsCancel.Enabled = true;
                    cmbImpactUrbanSchoolsCancel.Visible = true;
                    cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonListsAndChoiceForAdds("ImpactUrbanSchools");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
                else
                {
                    if (!chbImpactUrbanSchools.Checked)
                    {
                        //Act like a normal add..
                        cmbImpactUrbanSchoolsConfirm.Visible = true;
                        cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                        cmbImpactUrbanSchoolsCancel.Enabled = true;
                        cmbImpactUrbanSchoolsCancel.Visible = true;
                        cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                        lblProgramManagement.Style.Add("z-index", "99999");
                        lblProgramManagement.Visible = true;
                        lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                        PopulateRadioButtonLists("ImpactUrbanSchools");
                        cblProgramManagement.Style.Add("z-index", "99999");
                        cblProgramManagement.Visible = true;

                        pnlProgramManagement.Style.Add("z-index", "9999");
                        pnlProgramManagement.Visible = true;
                    }
                }
            }
            else if (Department == "PerformingArts")
            {
                if (chbImpactUrbanSchoolsPA.Checked)
                {
                    //Add student to another section.
                    cmbImpactUrbanSchoolsConfirm.Visible = true;
                    cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                    cmbImpactUrbanSchoolsCancel.Enabled = true;
                    cmbImpactUrbanSchoolsCancel.Visible = true;
                    cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonListsAndChoiceForAdds("ImpactUrbanSchools");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
                else
                {
                    if (!chbImpactUrbanSchoolsPA.Checked)
                    {
                        //Act like a normal add..
                        cmbImpactUrbanSchoolsConfirm.Visible = true;
                        cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                        cmbImpactUrbanSchoolsCancel.Enabled = true;
                        cmbImpactUrbanSchoolsCancel.Visible = true;
                        cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                        lblProgramManagement.Style.Add("z-index", "99999");
                        lblProgramManagement.Visible = true;
                        lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                        PopulateRadioButtonLists("ImpactUrbanSchools");
                        cblProgramManagement.Style.Add("z-index", "99999");
                        cblProgramManagement.Visible = true;

                        pnlProgramManagement.Style.Add("z-index", "9999");
                        pnlProgramManagement.Visible = true;
                    }
                }
            }
            else if (Department == "Education")
            {
                if (chbImpactUrbanSchoolsAcademics.Checked)
                {
                    //Add student to another section.
                    cmbImpactUrbanSchoolsConfirm.Visible = true;
                    cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                    cmbImpactUrbanSchoolsCancel.Enabled = true;
                    cmbImpactUrbanSchoolsCancel.Visible = true;
                    cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonListsAndChoiceForAdds("ImpactUrbanSchools");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
                else
                {
                    if (!chbImpactUrbanSchoolsAcademics.Checked)
                    {
                        //Act like a normal add..
                        cmbImpactUrbanSchoolsConfirm.Visible = true;
                        cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                        cmbImpactUrbanSchoolsCancel.Enabled = true;
                        cmbImpactUrbanSchoolsCancel.Visible = true;
                        cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                        lblProgramManagement.Style.Add("z-index", "99999");
                        lblProgramManagement.Visible = true;
                        lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                        PopulateRadioButtonLists("ImpactUrbanSchools");
                        cblProgramManagement.Style.Add("z-index", "99999");
                        cblProgramManagement.Visible = true;

                        pnlProgramManagement.Style.Add("z-index", "9999");
                        pnlProgramManagement.Visible = true;
                    }
                }
            }
        }

        protected void chbImpactUrbanSchoolsAcademics_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbImpactUrbanSchoolsAcademics.Checked)
            {
                cmbImpactUrbanSchoolsConfirm.Visible = true;
                cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                cmbImpactUrbanSchoolsCancel.Enabled = true;
                cmbImpactUrbanSchoolsCancel.Visible = true;
                cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("ImpactUrbanSchools");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbImpactUrbanSchoolsAcademics.Checked)
                {
                    cmbImpactUrbanSchoolsRemove.Visible = true;
                    cmbImpactUrbanSchoolsRemove.Style.Add("z-index", "99999");

                    cmbImpactUrbanSchoolsCancel.Enabled = true;
                    cmbImpactUrbanSchoolsCancel.Visible = true;
                    cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("ImpactUrbanSchools");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbImpactUrbanSchoolsPA_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbImpactUrbanSchoolsPA.Checked)
            {
                cmbImpactUrbanSchoolsConfirm.Visible = true;
                cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                cmbImpactUrbanSchoolsCancel.Enabled = true;
                cmbImpactUrbanSchoolsCancel.Visible = true;
                cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("ImpactUrbanSchools");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbImpactUrbanSchoolsPA.Checked)
                {
                    cmbImpactUrbanSchoolsRemove.Visible = true;
                    cmbImpactUrbanSchoolsRemove.Style.Add("z-index", "99999");

                    cmbImpactUrbanSchoolsCancel.Enabled = true;
                    cmbImpactUrbanSchoolsCancel.Visible = true;
                    cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("ImpactUrbanSchools");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void DisableEntireScreen()
        {
            btnSubmitInformation.Enabled = false;
            //btnNewPerson1.Enabled = false;
            cmbClearPage.Enabled = false;

            //txbMiddleName.Enabled = false;
            ddlAge.Enabled = false;
            ddlYearBirth.Enabled = false;
            ddlMonthBirth.Enabled = false;
            txtState.Enabled = false;
            //ddlCareerGoal.Enabled = false;
            //chbDiscipleshipMentor.Enabled = false;
            //lbDiscipleshipMentor.Enabled = false;
            //chbBibleStudyParticipation.Enabled = false;
            //chbBibleOwnership.Enabled = false;

            txbParentGuardian1WrkPh.Enabled = false;
            txtParentGuardian1.Enabled = false;
            txbParentGuardian1CellPhone.Enabled = false;
            txbParentGuardian1Email.Enabled = false;
            ddlParentGuardian1Relationship.Enabled = false;
            //ddlTextGuard1.Enabled = false;
            txbParentGuardian2.Enabled = false;
            txbParentGuardian2CellPhone.Enabled = false;
            txbParentGuardian2Email.Enabled = false;
            txbParentGuardian2WrkPh.Enabled = false;
            ddlParentGuardian2Relationship.Enabled = false;
            //ddlTextGuard2.Enabled = false;
            txtHomePhone.Enabled = false;

            //chbStudentVolunteer.Enabled = false;
            //chbHaveReceivedChrist.Enabled = false;

            txbEmergencyPhone.Enabled = false;
            txbEmergencyRelationship.Enabled = false;
            txbEmergRelationship.Enabled = false;

            ddlMailingListCodes.Enabled = false;
            chbMailingList.Enabled = false;
            //chbIncludePromotionalMailing.Enabled = false;
            txbAllergies.Enabled = false;
            txtAddress1.Enabled = false;
            txtCity.Enabled = false;
            txtZip.Enabled = false;
            //txbSoloSong.Enabled = false;
            txtStudentCellPhone.Enabled = false;
            txtStudentEmail.Enabled = false;
            txtLastName.Enabled = false;
            txtFirstName.Enabled = false;
            //ddlSchool.Enabled = false;
            //ddlChurch.Enabled = false;
            ddlTShirtSize.Enabled = false;
            ddlTextPhone.Enabled = false;
            ddlGender.Enabled = false;
            ddlDayBirth.Enabled = false;
            //ddlGrade.Enabled = false;

            txbHealthConditions.Enabled = false;
            txbNotes.Enabled = false;
            //chbOptionsTurnOn.Enabled = false;

            //chbRegistrationForm.Enabled = false;
            //chbRetreatForm.Enabled = false;
            //chbStudentQuestionareForm.Enabled = false;
            //chbParentalConsentForm.Enabled = false;
            //chbPromotionalRelease.Enabled = false;
            //chbPermissionTransport.Enabled = false;
            chbMailingList.Enabled = false;

        }

        protected void EnableEntireScreen()
        {
            btnSubmitInformation.Enabled = true;
            //btnNewPerson1.Enabled = true;
            cmbClearPage.Enabled = true;

            //txbMiddleName.Enabled = true;
            ddlAge.Enabled = true;
            ddlYearBirth.Enabled = true;
            ddlMonthBirth.Enabled = true;
            txtState.Enabled = true;
            //ddlCareerGoal.Enabled = true;
            //chbDiscipleshipMentor.Enabled = true;
            //lbDiscipleshipMentor.Enabled = true;
            //chbBibleStudyParticipation.Enabled = true;
            //chbBibleOwnership.Enabled = true;
            txtHomePhone.Enabled = true;

            txbParentGuardian1WrkPh.Enabled = true;
            txtParentGuardian1.Enabled = true;
            txbParentGuardian1CellPhone.Enabled = true;
            txbParentGuardian1Email.Enabled = true;
            ddlParentGuardian1Relationship.Enabled = true;
            //ddlTextGuard1.Enabled = true;
            txbParentGuardian2.Enabled = true;
            txbParentGuardian2CellPhone.Enabled = true;
            txbParentGuardian2Email.Enabled = true;
            txbParentGuardian2WrkPh.Enabled = true;
            ddlParentGuardian2Relationship.Enabled = true;
            //ddlTextGuard2.Enabled = true;

            //chbStudentVolunteer.Enabled = true;
            //chbHaveReceivedChrist.Enabled = true;

            txbEmergencyPhone.Enabled = true;
            txbEmergencyRelationship.Enabled = true;
            txbEmergRelationship.Enabled = true;

            ddlMailingListCodes.Enabled = true;
            chbMailingList.Enabled = true;
            //chbIncludePromotionalMailing.Enabled = true;
            txbAllergies.Enabled = true;
            txtAddress1.Enabled = true;
            txtCity.Enabled = true;
            txtZip.Enabled = true;
            //txbSoloSong.Enabled = true;
            txtStudentCellPhone.Enabled = true;
            txtStudentEmail.Enabled = true;
            txtLastName.Enabled = true;
            txtFirstName.Enabled = true;
            //ddlSchool.Enabled = true;
            //ddlChurch.Enabled = true;
            ddlTShirtSize.Enabled = true;
            ddlTextPhone.Enabled = true;
            ddlGender.Enabled = true;
            ddlDayBirth.Enabled = true;
            //ddlGrade.Enabled = true;

            txbHealthConditions.Enabled = true;
            txbNotes.Enabled = true;
            //chbOptionsTurnOn.Enabled = false;

            //chbRegistrationForm.Enabled = true;
            //chbRetreatForm.Enabled = true;
            //chbStudentQuestionareForm.Enabled = true;
            //chbParentalConsentForm.Enabled = true;
            //chbPromotionalRelease.Enabled = true;
            //chbPermissionTransport.Enabled = true;
            chbMailingList.Enabled = true;
        }


        protected void lbMSHSChoir_Click1(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbMSHSChoir.Checked)
            {
                //Add student to another section.
                cmbMSHSChoirConfirm.Visible = true;
                cmbMSHSChoirConfirm.Style.Add("z-index", "99999");

                cmbMSHSChoirCancel.Enabled = true;
                cmbMSHSChoirCancel.Visible = true;
                cmbMSHSChoirCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("MSHSChoir");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbMSHSChoir.Checked)
                {
                    //Act like a normal add..
                    cmbMSHSChoirConfirm.Visible = true;
                    cmbMSHSChoirConfirm.Style.Add("z-index", "99999");

                    cmbMSHSChoirCancel.Enabled = true;
                    cmbMSHSChoirCancel.Visible = true;
                    cmbMSHSChoirCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the volunteer.  Please choose.";

                    PopulateRadioButtonLists("MSHSChoir");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbChildrensChoir_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbChildrensChoir.Checked)
            {
                //Add student to another section.
                cmbChildrensChoirConfirm.Visible = true;
                cmbChildrensChoirConfirm.Style.Add("z-index", "99999");

                cmbChildrensChoirCancel.Enabled = true;
                cmbChildrensChoirCancel.Visible = true;
                cmbChildrensChoirCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("ChildrensChoir");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbChildrensChoir.Checked)
                {
                    //Act like a normal add..
                    cmbChildrensChoirConfirm.Visible = true;
                    cmbChildrensChoirConfirm.Style.Add("z-index", "99999");

                    cmbChildrensChoirCancel.Enabled = true;
                    cmbChildrensChoirCancel.Visible = true;
                    cmbChildrensChoirCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the volunteer.  Please choose.";

                    PopulateRadioButtonLists("ChildrensChoir");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbShakes_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbShakes.Checked)
            {
                //Add student to another section.
                cmbShakesConfirm.Visible = true;
                cmbShakesConfirm.Style.Add("z-index", "99999");

                cmbShakesCancel.Enabled = true;
                cmbShakesCancel.Visible = true;
                cmbShakesCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("Shakes");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbShakes.Checked)
                {
                    //Act like a normal add..
                    cmbShakesConfirm.Visible = true;
                    cmbShakesConfirm.Style.Add("z-index", "99999");

                    cmbShakesCancel.Enabled = true;
                    cmbShakesCancel.Visible = true;
                    cmbShakesCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the volunteer.  Please choose.";

                    PopulateRadioButtonLists("Shakes");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbSingers_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbSingers.Checked)
            {
                //Add student to another section.
                cmbSingersConfirm.Visible = true;
                cmbSingersConfirm.Style.Add("z-index", "99999");

                cmbSingersCancel.Enabled = true;
                cmbSingersCancel.Visible = true;
                cmbSingersCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("Singers");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSingers.Checked)
                {
                    //Act like a normal add..
                    cmbSingersConfirm.Visible = true;
                    cmbSingersConfirm.Style.Add("z-index", "99999");

                    cmbSingersCancel.Enabled = true;
                    cmbSingersCancel.Visible = true;
                    cmbSingersCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the volunteer.  Please choose.";

                    PopulateRadioButtonLists("Singers");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void cmbMSHSChoirConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("MSHSChoir");
            EnableEntireScreen();
        }

        protected void cmbChildrensChoirConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("ChildrensChoir");
            EnableEntireScreen();
        }

        protected void cmbSingersConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Singers");
            EnableEntireScreen();
        }

        protected void cmbShakesConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Shakes");
            EnableEntireScreen();
        }

        protected void cmbPAAConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("PAA");
            EnableEntireScreen();
        }

        protected void cmbSummerDayCampConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("SummerDayCamp");
            EnableEntireScreen();
        }

        protected void cmbImpactUrbanSchoolsConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("ImpactUrbanSchools");
            EnableEntireScreen();
        }
                
        protected void chbImpactUrbanSchools_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbImpactUrbanSchools.Checked)
            {
                cmbImpactUrbanSchoolsConfirm.Visible = true;
                cmbImpactUrbanSchoolsConfirm.Style.Add("z-index", "99999");

                cmbImpactUrbanSchoolsCancel.Enabled = true;
                cmbImpactUrbanSchoolsCancel.Visible = true;
                cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("ImpactUrbanSchools");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbImpactUrbanSchools.Checked)
                {
                    cmbImpactUrbanSchoolsRemove.Visible = true;
                    cmbImpactUrbanSchoolsRemove.Style.Add("z-index", "99999");

                    cmbImpactUrbanSchoolsCancel.Enabled = true;
                    cmbImpactUrbanSchoolsCancel.Visible = true;
                    cmbImpactUrbanSchoolsCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("ImpactUrbanSchools");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbAcademicReadingSupport_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbReadingProgram.Checked)
            {
                //Add student to another section.
                cmbAcademicReadingSupportConfirm.Visible = true;
                cmbAcademicReadingSupportConfirm.Style.Add("z-index", "99999");

                cmbAcademicReadingSupportCancel.Enabled = true;
                cmbAcademicReadingSupportCancel.Visible = true;
                cmbAcademicReadingSupportCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("AcademicReadingSupport");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbReadingProgram.Checked)
                {
                    //Act like a normal add..
                    cmbAcademicReadingSupportConfirm.Visible = true;
                    cmbAcademicReadingSupportConfirm.Style.Add("z-index", "99999");

                    cmbAcademicReadingSupportCancel.Enabled = true;
                    cmbAcademicReadingSupportCancel.Visible = true;
                    cmbAcademicReadingSupportCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("AcademicReadingSupport");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbReadingProgram_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbReadingProgram.Checked)
            {
                cmbAcademicReadingSupportConfirm.Visible = true;
                cmbAcademicReadingSupportConfirm.Style.Add("z-index", "99999");

                cmbAcademicReadingSupportCancel.Enabled = true;
                cmbAcademicReadingSupportCancel.Visible = true;
                cmbAcademicReadingSupportCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("AcademicReadingSupport");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbReadingProgram.Checked)
                {
                    cmbAcademicReadingSupportRemove.Visible = true;
                    cmbAcademicReadingSupportRemove.Style.Add("z-index", "99999");

                    cmbAcademicReadingSupportCancel.Enabled = true;
                    cmbAcademicReadingSupportCancel.Visible = true;
                    cmbAcademicReadingSupportCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("AcademicReadingSupport");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void cmbAcademicReadingSupportConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("AcademicReadingSupport");
            EnableEntireScreen();
        }


        protected void cmbAcademicReadingSupportRemove_Click(object sender, EventArgs e)
        {
            RemoveVolunteersFromProgram("AcademicReadingSupport");
            UpdateVolunteerTable();
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbAcademicReadingSupportRemove.Visible = false;
            cmbAcademicReadingSupportCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbAcademicReadingSupportCancel_Click(object sender, EventArgs e)
        {
            //AcademicReadingSupport Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("AcademicReadingSupport");
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbAcademicReadingSupportConfirm.Visible = false;
            cmbAcademicReadingSupportCancel.Visible = false;
            cmbAcademicReadingSupportRemove.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateVolunteerTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void chbSingers_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbSingers.Checked)
            {
                cmbSingersConfirm.Visible = true;
                cmbSingersConfirm.Style.Add("z-index", "99999");

                cmbSingersCancel.Enabled = true;
                cmbSingersCancel.Visible = true;
                cmbSingersCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;

                PopulateRadioButtonLists("Singers");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSingers.Checked)
                {
                    cmbSingersRemove.Visible = true;
                    cmbSingersRemove.Style.Add("z-index", "99999");

                    cmbSingersCancel.Enabled = true;
                    cmbSingersCancel.Visible = true;
                    cmbSingersCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Singers");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbSummerDayCamp_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbSummerDayCamp.Checked)
            {
                //Add student to another section.
                cmbSummerDayCampConfirm.Visible = true;
                cmbSummerDayCampConfirm.Style.Add("z-index", "99999");

                cmbSummerDayCampCancel.Enabled = true;
                cmbSummerDayCampCancel.Visible = true;
                cmbSummerDayCampCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                PopulateRadioButtonListsAndChoiceForAdds("SummerDayCamp");
                cblProgramManagement.Style.Add("z-index", "99999");
                cblProgramManagement.Visible = true;

                pnlProgramManagement.Style.Add("z-index", "9999");
                pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbSummerDayCamp.Checked)
                {
                    //Act like a normal add..
                    cmbSummerDayCampConfirm.Visible = true;
                    cmbSummerDayCampConfirm.Style.Add("z-index", "99999");

                    cmbSummerDayCampCancel.Enabled = true;
                    cmbSummerDayCampCancel.Visible = true;
                    cmbSummerDayCampCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to add the volunteer.  Please choose.";

                    PopulateRadioButtonLists("SummerDayCamp");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }
    }
}
