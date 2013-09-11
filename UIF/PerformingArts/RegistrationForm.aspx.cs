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
	/// Summary description for RegistrationForm.
    /// Ryan C Manners 9/1/10.
    /// Building this page to update information in the database for students..
	/// </summary>
	public partial class RegistrationForm : System.Web.UI.Page
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
            
            //Ryan C Manners...6/23/11.
            Department = Request.QueryString["Dept"];

            if (!Page.IsPostBack)
            {
                //Ryan C Manners...6/16/11.
                UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                MenuBest = BuildMenu.BuildMenuControl(MenuBest);

                chbNewStudentFlag.Checked = false;
                if (Request.QueryString["security"] == "Good")
                {
                    if (Request.QueryString["newstudent"] == "newstudent")
                    {
                        chbNewStudentFlag.Checked = true;
                        StartingSettings();
                        NewStudent();
                    }
                    else
                    {
                        try
                        {
                            string sql = "";
                            if (Request.QueryString.ToString().Contains("StudentMiddleName"))
                            {
                                sql = "select si.LastName, si.FirstName, si.address, si.HomePhone, si.school, si.grade, si.age, "
                                            + "si.city, si.dob, si.state, si.zip, si.sex, si.church, si.studentcellphone, si.tshirtsize "
                                            + ", si.studentemail "
                                            + ",si.meetccgf, si.descipleshipmentor, si.soloist, si.solosong, si.dance"
                                            + ",si.bibleownership, si.biblestudyparticipation, si.pictureidentification, si.havereceivedchrist, "
                                            + "si.currentregistrationform, si.parentalconsentform, si.retreatconsentform, si.studentchoirquestionareform, pl.childrenschoir, pl.mshschoir, pl.performingarts "
                                            + ",si.id, si.voicepart "
                                            + ", si.studentemail, si.careergoal, si.healthconditions, si.notes, si.lastupdatedby, si.sysupdate, si.discipleshipmentorprogram "
                                            + ", pl.shakes, pl.singers, pl.mondaynights "
                                            + ", pl.[3on3basketball], pl.basketballTEAMS, pl.soccerintramurals, pl.soccerteams, pl.baseball, pl.biblestudy, pl.hsbasketballlg, pl.msbasketballlg, pl.outreachbasketball, pl.outreachbasketball "
                                            + ",si.MiddleName, si.MailingListInclude, si.MailingListCodes, pl.summerdaycamp, si.promotionalrelease, si.permissiontotransport, si.campdropoff, si.camppickup, si.campcomments, si.includepromotionalmailing, si.textphone, pl.SpecialEvents, si.studentvolunteer "
                                            + ",pl.impacturbanschools, sm.AdministerYesNo, sm.Aspirin, sm.Tylenol, sm.Ibuprofen, sm.Advil, sm.Antacids, sm.Benadryl, sm.[Antiseptic Ointment], sm.[Anesthetic Ointment], sm.IodinePrepPad, sm.Acetaminophen, sm.RubbingAlcohol, sm.Other, sm.OtherNotes "
                                            + ",si.Scrubbed, si.ScrubbedDate, si.LastScrubbedBy, pl.academicreadingsupport "
                                            + "FROM UIF_PerformingArts.dbo.studentinformation  si "
                                            + "LEFT OUTER JOIN dbo.programslist pl "
                                            + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                                            + "LEFT OUTER JOIN dbo.StudentMedications sm "
                                            + "ON (si.LastName = sm.LastName AND si.FirstName = sm.FirstName AND si.MiddleName = sm.MiddleName) "
                                            + "WHERE si.lastname=@lastname "
                                            + "AND si.firstname=@firstname "
                                            + "AND si.middlename=@middlename "
                                            + "AND pl.student = 1 ";
                            }
                            else
                            {
                                sql = "select si.LastName, si.FirstName, si.address, si.HomePhone, si.school, si.grade, si.age, "
                                            + "si.city, si.dob, si.state, si.zip, si.sex, si.church, si.studentcellphone, si.tshirtsize "
                                            + ", si.studentemail "
                                            + ",si.meetccgf, si.descipleshipmentor, si.soloist, si.solosong, si.dance"
                                            + ",si.bibleownership, si.biblestudyparticipation, si.pictureidentification, si.havereceivedchrist, "
                                            + "si.currentregistrationform, si.parentalconsentform, si.retreatconsentform, si.studentchoirquestionareform, pl.childrenschoir, pl.mshschoir, pl.performingarts "
                                            + ",si.id, si.voicepart "
                                            + ", si.studentemail, si.careergoal, si.healthconditions, si.notes, si.lastupdatedby, si.sysupdate, si.discipleshipmentorprogram "
                                            + ", pl.shakes, pl.singers, pl.mondaynights "
                                            + ", pl.[3on3basketball], pl.basketballTEAMS, pl.soccerintramurals, pl.soccerteams, pl.baseball, pl.biblestudy, pl.hsbasketballlg, pl.msbasketballlg, pl.outreachbasketball, pl.outreachbasketball "
                                            + ",si.MiddleName, si.MailingListInclude, si.MailingListCodes, pl.summerdaycamp, si.promotionalrelease, si.permissiontotransport, si.campdropoff, si.camppickup, si.campcomments, si.includepromotionalmailing, si.textphone, pl.SpecialEvents, si.studentvolunteer "
                                            + ",pl.impacturbanschools, sm.AdministerYesNo, sm.Aspirin, sm.Tylenol, sm.Ibuprofen, sm.Advil, sm.Antacids, sm.Benadryl, sm.[Antiseptic Ointment], sm.[Anesthetic Ointment], sm.IodinePrepPad, sm.Acetaminophen, sm.RubbingAlcohol, sm.Other, sm.OtherNotes "
                                            +  ",si.Scrubbed, si.ScrubbedDate, si.LastScrubbedBy "
                                            + "FROM UIF_PerformingArts.dbo.studentinformation  si "
                                            + "LEFT OUTER JOIN dbo.programslist pl "
                                            + "ON (si.LastName = pl.LastName AND si.FirstName = pl.FirstName AND si.MiddleName = pl.MiddleName) "
                                            + "LEFT OUTER JOIN dbo.StudentMedications sm "
                                            + "ON (si.LastName = sm.LastName AND si.FirstName = sm.FirstName AND si.MiddleName = sm.MiddleName) "
                                            + "WHERE si.lastname=@lastname "
                                            + "AND si.firstname=@firstname "
                                            + "AND pl.student = 1 ";
                            }

                            SqlCommand cmd = new SqlCommand(sql);
                            if (Request.QueryString.ToString().Contains("StudentMiddleName"))
                            {
                                //Perform database lookup based on the chosen child..RCM..
                                cmd.Parameters.Add(new SqlParameter("@lastname", Request.QueryString["StudentLastName"].Trim()));
                                cmd.Parameters.Add(new SqlParameter("@firstname", Request.QueryString["StudentFirstName"].Trim()));
                                cmd.Parameters.Add(new SqlParameter("@middlename", Request.QueryString["StudentMiddleName"].Trim()));
                            }
                            else
                            {
                                //Perform database lookup based on the chosen child..RCM..
                                cmd.Parameters.Add(new SqlParameter("@lastname", Request.QueryString["StudentLastName"].Trim()));
                                cmd.Parameters.Add(new SqlParameter("@firstname", Request.QueryString["StudentFirstName"].Trim()));
                            }

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
                                    ddlSchool.Text = "N/A";
                                }
                                else
                                {
                                    ddlSchool.Items.Add(reader.GetString(4));
                                    ddlSchool.Text = reader.GetString(4);
                                }
                                if (reader.IsDBNull(5))
                                {
                                }
                                else
                                {
                                    ddlGrade.Text = reader.GetString(5);
                                }
                                if (reader.IsDBNull(6))
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
                                if (reader.IsDBNull(7))
                                {
                                    txtCity.Text = "N/A";
                                }
                                else
                                {
                                    txtCity.Text = reader.GetString(7);
                                }
                                if (reader.IsDBNull(8))
                                {
                                    ddlMonthBirth.Text = "01";
                                    ddlDayBirth.Text = "01";
                                    ddlYearBirth.Text = "1900";
                                }
                                else
                                {
                                    ddlMonthBirth.Text = reader.GetSqlValue(8).ToString().Substring(0, 2);
                                    ddlDayBirth.Text = reader.GetSqlValue(8).ToString().Substring(3, 2);
                                    ddlYearBirth.Text = reader.GetSqlValue(8).ToString().Substring(6, 4);
                                }
                                if (reader.IsDBNull(9))
                                {
                                    txtState.Text = "N/A";
                                }
                                else
                                {
                                    txtState.Text = reader.GetString(9);
                                }
                                if (reader.IsDBNull(10))
                                {
                                    txtZip.Text = "N/A";
                                }
                                else
                                {
                                    txtZip.Text = reader.GetString(10);
                                }
                                if (reader.IsDBNull(11))
                                {
                                }
                                else
                                {
                                    ddlGender.Text = reader.GetString(11);
                                }
                                if (reader.IsDBNull(12))
                                {
                                    //txtChurch.Text = "N/A";
                                    ddlChurch.Text = "Attend Church?";
                                }
                                else
                                {
                                    //txtChurch.Text = reader.GetString(12);
                                    ddlChurch.Text = reader.GetString(12);
                                }
                                if (reader.IsDBNull(13))
                                {
                                    txtStudentCellPhone.Text = "NULL";
                                }
                                else
                                {
                                    txtStudentCellPhone.Text = reader.GetSqlValue(13).ToString();
                                }
                                //txtCity.Text = "Pittsburgh";
                                if (reader.IsDBNull(14))
                                {
                                }
                                else
                                {
                                    ddlTShirtSize.Text = reader.GetString(14);
                                }
                                //#15....?
                                if (reader.IsDBNull(16))
                                {
                                    chbMeetCCGF.Checked = false;
                                }
                                else
                                {
                                    chbMeetCCGF.Checked = reader.GetBoolean(16);
                                }
                                if (reader.IsDBNull(17))
                                {
                                    txbDiscipleshipMentor.Text = "N/A";
                                }
                                else
                                {
                                    txbDiscipleshipMentor.Text = reader.GetString(17);
                                }
                                if (reader.IsDBNull(18))
                                {
                                    chbSoloist.Checked = false;
                                }
                                else
                                {
                                    chbSoloist.Checked = reader.GetBoolean(18);
                                }
                                if (reader.IsDBNull(19))
                                {
                                    txbSoloSong.Text = "N/A";
                                }
                                else
                                {
                                    txbSoloSong.Text = reader.GetString(19);
                                }
                                if (reader.IsDBNull(20))
                                {
                                    chbDance.Checked = false;
                                }
                                else
                                {
                                    chbDance.Checked = reader.GetBoolean(20);
                                }
                                //danceyear
                                //schoolform2
                                if (reader.IsDBNull(21))
                                {
                                    chbBibleOwnership.Checked = false;
                                }
                                else
                                {
                                    chbBibleOwnership.Checked = reader.GetBoolean(21);
                                }
                                if (reader.IsDBNull(22))
                                {
                                    chbBibleStudyParticipation.Checked = false;
                                }
                                else
                                {
                                    chbBibleStudyParticipation.Checked = reader.GetBoolean(22);
                                }
                                if (reader.IsDBNull(23))
                                {
                                    imgPicture.ImageUrl = "No picture Available";
                                }
                                else
                                {
                                    imgPicture.ImageUrl = reader.GetString(23);
                                }
                                //whenreceivedchrist.
                                if (reader.IsDBNull(24))
                                {
                                    chbHaveReceivedChrist.Checked = false;
                                }
                                else
                                {
                                    chbHaveReceivedChrist.Checked = reader.GetBoolean(24);
                                }
                                if (reader.IsDBNull(25))
                                {
                                    chbRegistrationForm.Checked = false;
                                }
                                else
                                {
                                    chbRegistrationForm.Checked = reader.GetBoolean(25);
                                }
                                if (reader.IsDBNull(26))
                                {
                                    chbParentalConsentForm.Checked = false;
                                }
                                else
                                {
                                    chbParentalConsentForm.Checked = reader.GetBoolean(26);
                                }
                                if (reader.IsDBNull(27))
                                {
                                    chbRetreatForm.Checked = false;
                                }
                                else
                                {
                                    chbRetreatForm.Checked = reader.GetBoolean(27);
                                }
                                if (reader.IsDBNull(28))
                                {
                                    chbStudentQuestionareForm.Checked = false;
                                }
                                else
                                {
                                    chbStudentQuestionareForm.Checked = reader.GetBoolean(28);
                                }
                                if (reader.IsDBNull(29))
                                {
                                    chbChildrensChoir.Checked = false;
                                }
                                else
                                {
                                    chbChildrensChoir.Checked = reader.GetBoolean(29);
                                }
                                if (reader.IsDBNull(30))
                                {
                                    chbMSHSChoir.Checked = false;
                                }
                                else
                                {
                                    chbMSHSChoir.Checked = reader.GetBoolean(30);
                                }
                                if (reader.IsDBNull(31))
                                {
                                    chbPerformingArts.Checked = false;
                                }
                                else
                                {
                                    chbPerformingArts.Checked = reader.GetBoolean(31);
                                }
                                if (chbPerformingArts.Checked)
                                {
                                    lbClassesEnrollment.Enabled = true;
                                }
                                else
                                {
                                    lbClassesEnrollment.Enabled = false;
                                }
                                //if (chbChildrensChoir.Checked)
                                //{
                                //    lbChildrensAttendance.Enabled = true;
                                //}
                                //else
                                //{
                                //    lbChildrensAttendance.Enabled = false;
                                //}
                                //if (chbMSHSChoir.Checked)
                                //{
                                //    lbMSHSChoir.Enabled = true;
                                //}
                                //else
                                //{
                                //    lbMSHSChoir.Enabled = false;
                                //}
                                if (reader.IsDBNull(32))
                                {
                                    txbID.Text = "N/A";
                                }
                                else
                                {
                                    txbID.Text = reader.GetInt32(32).ToString();
                                }
                                if (reader.IsDBNull(33))
                                {
                                }
                                else
                                {
                                    ddlVoicePart.Text = reader.GetString(33);
                                }
                                if (reader.IsDBNull(34))
                                {
                                    txtStudentEmail.Text = "N/A";
                                }
                                else
                                {
                                    txtStudentEmail.Text = reader.GetString(34);
                                }
                                if (reader.IsDBNull(35))
                                {
                                    txtCareerGoal.Text = "N/A";
                                    ddlCareerGoal.Text = "Career Goal?";
                                }
                                else
                                {
                                    txtCareerGoal.Text = reader.GetString(35);
                                    ddlCareerGoal.Text = reader.GetString(35);
                                }
                                if (reader.IsDBNull(36))
                                {
                                    txbHealthConditions.Text = "N/A";
                                }
                                else
                                {
                                    txbHealthConditions.Text = reader.GetString(36);
                                }
                                if (reader.IsDBNull(37))
                                {
                                    txbNotes.Text = "N/A";
                                }
                                else
                                {
                                    txbNotes.Text = reader.GetString(37);
                                }
                                if (reader.IsDBNull(38))
                                {
                                    lblLastUpdatedBy.Text = "N/A";
                                }
                                else
                                {
                                    lblLastUpdatedBy.Text = "Last Updated By: " + reader.GetString(38) + " On: " + reader.GetSqlValue(39).ToString();
                                }                                
                                if (reader.IsDBNull(40))
                                {
                                    chbDiscipleshipMentor.Checked = false;
                                }
                                else
                                {
                                    chbDiscipleshipMentor.Checked = reader.GetBoolean(40);
                                    if (chbDiscipleshipMentor.Checked)
                                    {
                                        chbDiscipleshipMentor.Enabled = false;
                                        lbDiscipleshipMentor.Enabled = true;
                                    }
                                }
                                if (((Request.QueryString["LastName"] == "Sims-Reed") && (Request.QueryString["FirstName"] == "Donna")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Tammy")) || ((Request.QueryString["LastName"] == "Boll") && (Request.QueryString["FirstName"] == "Becky")) || ((Request.QueryString["LastName"] == "Churchill") && (Request.QueryString["FirstName"] == "Andrew")))
                                {

                                }
                                else
                                {
                                    chbDiscipleshipMentor.Enabled = false;
                                    txbDiscipleshipMentor.Enabled = false;
                                    lbDiscipleshipMentor.Enabled = false;
                                }
                                if (reader.IsDBNull(41))
                                {
                                    chbShakes.Checked = false;
                                }
                                else
                                {
                                    chbShakes.Checked = reader.GetBoolean(41);
                                }
                                if (reader.IsDBNull(42))
                                {
                                    chbSingers.Checked = false;
                                }
                                else
                                {
                                    chbSingers.Checked = reader.GetBoolean(42);
                                }
                                if (reader.IsDBNull(43))
                                {
                                    chbMondayNights.Checked = false;
                                }
                                else
                                {
                                    chbMondayNights.Checked = reader.GetBoolean(43);
                                }
                                if (reader.IsDBNull(44))
                                {
                                    chb3on3Basketball.Checked = false;
                                }
                                else
                                {
                                    chb3on3Basketball.Checked = reader.GetBoolean(44);
                                }
                                if (reader.IsDBNull(45))
                                {
                                    chbBasketballTEAMS.Checked = false;
                                }
                                else
                                {
                                    chbBasketballTEAMS.Checked = reader.GetBoolean(45);
                                }
                                if (reader.IsDBNull(46))
                                {
                                    chbSoccerInterMurals.Checked = false;
                                }
                                else
                                {
                                    chbSoccerInterMurals.Checked = reader.GetBoolean(46);
                                }
                                if (reader.IsDBNull(47))
                                {
                                    chbSoccerLgTravel.Checked = false;
                                }
                                else
                                {
                                    chbSoccerLgTravel.Checked = reader.GetBoolean(47);
                                }
                                if (reader.IsDBNull(48))
                                {
                                    chbLittleLeagueBaseball.Checked = false;
                                }
                                else
                                {
                                    chbLittleLeagueBaseball.Checked = reader.GetBoolean(48);
                                }
                                if (reader.IsDBNull(49))
                                {
                                    chbOliverFootballBible.Checked = false;
                                }
                                else
                                {
                                    chbOliverFootballBible.Checked = reader.GetBoolean(49);
                                }
                                if (reader.IsDBNull(50))
                                {
                                    chbHSBasketLeague.Checked = false;
                                }
                                else
                                {
                                    chbHSBasketLeague.Checked = reader.GetBoolean(50);
                                }
                                if (reader.IsDBNull(51))
                                {
                                    chbMSBasketLeague.Checked = false;
                                }
                                else
                                {
                                    chbMSBasketLeague.Checked = reader.GetBoolean(51);
                                }
                                if (reader.IsDBNull(52))
                                {
                                    chbBoysOutreachBball.Checked = false;
                                }
                                else
                                {
                                    chbBoysOutreachBball.Checked = reader.GetBoolean(52);
                                }
                                ///Taking out.. consolidating into just "Outreach Basketball" and temporarily
                                ///using the boysoutreach column to handle that data...

                                //if (reader.IsDBNull(53))
                                //{
                                //    chbGirlsOutreachBball.Checked = false;
                                //}
                                //else
                                //{
                                //    chbGirlsOutreachBball.Checked = reader.GetBoolean(53);
                                //}
                                if (reader.IsDBNull(54))
                                {
                                    txbMiddleName.Text = "N/A";
                                }
                                else
                                {
                                    txbMiddleName.Text = reader.GetString(54);
                                }
                                if (reader.IsDBNull(55))
                                {
                                    chbMailingList.Checked = false;
                                }
                                else
                                {
                                    chbMailingList.Checked = reader.GetBoolean(55);
                                }
                                if (reader.IsDBNull(56))
                                {
                                    ddlMailingListCodes.Text = "No Address";
                                }
                                else
                                {
                                    if (chbMailingList.Checked)
                                    {
                                        ddlMailingListCodes.Items.Add("Use Current Address");
                                        ddlMailingListCodes.Text = "Use Current Address";
                                        ddlMailingListCodes.Enabled = false;
                                    }
                                    else
                                    {
                                        ddlMailingListCodes.Text = reader.GetString(56);
                                    }
                                }
                                if (reader.IsDBNull(57))
                                {
                                    chbSummerDayCamp.Checked = false;
                                }
                                else
                                {
                                    chbSummerDayCamp.Checked = reader.GetBoolean(57);
                                }
                                if (reader.IsDBNull(58))
                                {
                                    chbPromotionalRelease.Checked = false;
                                }
                                else
                                {
                                    chbPromotionalRelease.Checked = reader.GetBoolean(58);
                                }
                                if (reader.IsDBNull(59))
                                {
                                    chbPermissionTransport.Checked = false;
                                }
                                else
                                {
                                    chbPermissionTransport.Checked = reader.GetBoolean(59);
                                }
                                if (reader.IsDBNull(60))
                                {
                                    chbCAMPDropOff.Checked = false;
                                }
                                else
                                {
                                    chbCAMPDropOff.Checked = reader.GetBoolean(60);
                                }
                                if (reader.IsDBNull(61))
                                {
                                    chbCAMPPickUp.Checked = false;
                                }
                                else
                                {
                                    chbCAMPPickUp.Checked = reader.GetBoolean(61);
                                }
                                if (reader.IsDBNull(62))
                                {
                                    txbDropOffPickUp.Text = "N/A";
                                }
                                else
                                {
                                    txbDropOffPickUp.Text = reader.GetString(62);
                                }
                                if (reader.IsDBNull(63))
                                {
                                    chbIncludePromotionalMailing.Checked = false;
                                }
                                else
                                {
                                    chbIncludePromotionalMailing.Checked = reader.GetBoolean(63);
                                }
                                if (reader.IsDBNull(64))
                                {
                                    ddlTextPhone.Text = "No";
                                }
                                else
                                {
                                    ddlTextPhone.Text = ConvertBoolToYesNo(reader.GetBoolean(64));
                                    //ddlTextPhone.Text = Convert.ToString(reader.GetBoolean(64));
                                }
                                if (reader.IsDBNull(65))
                                {
                                    chbSpecialEvents.Checked = false;
                                }
                                else
                                {
                                    chbSpecialEvents.Checked = reader.GetBoolean(65);
                                }
                                if (reader.IsDBNull(66))
                                {
                                    chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    chbStudentVolunteer.Checked = reader.GetBoolean(66);
                                }
                                if (reader.IsDBNull(67))
                                {
                                    chbImpactUrbanSchools.Checked = false;
                                    chbImpactUrbanSchoolsPA.Checked = false;
                                    chbImpactUrbanSchoolsAcademics.Checked = false;
                                }
                                else
                                {
                                    if (Department == "Athletics")
                                    {
                                        chbImpactUrbanSchools.Checked = reader.GetBoolean(67);
                                    }
                                    else if (Department == "PerformingArts")
                                    {
                                        chbImpactUrbanSchoolsPA.Checked = reader.GetBoolean(67);
                                    }
                                    else if (Department == "Education")
                                    {
                                        chbImpactUrbanSchoolsAcademics.Checked = reader.GetBoolean(67);
                                    }
                                }

                                if (reader.IsDBNull(68))
                                {
                                    //chbMedication.Checked = false;
                                    //ddlAdministerMedicine.Text = "No";

                                    //Do nothing if it's NULL.  Leave it alone until it gets set.
                                    ddlAdministerMedicine.Text = "Administer Medicine?";
                                }
                                else
                                {
                                    ddlAdministerMedicine.Text = ConvertBoolToYesNo(reader.GetBoolean(68));
                                    if (ddlAdministerMedicine.Text == "Yes")
                                    {
                                        cblMedications.Enabled = true;
                                        txbMedicationsOtherNotes.Enabled = true;
                                    }
                                    //chbMedication.Checked = reader.GetBoolean(68);  //chbStudentVolunteer.Checked = false;
                                    //if (chbMedication.Checked)
                                    //{
                                    //    cblMedications.Enabled = true;
                                    //    txbMedicationsOtherNotes.Enabled = true;
                                    //}
                                }
                                if (reader.IsDBNull(69))
                                {
                                    cblMedications.Items[0].Selected = false;
                                }
                                else
                                {
                                    cblMedications.Items[0].Selected = reader.GetBoolean(69);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(70))
                                {
                                    cblMedications.Items[1].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[1].Selected = reader.GetBoolean(70);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(71))
                                {
                                    cblMedications.Items[2].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[2].Selected = reader.GetBoolean(71);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(72))
                                {
                                    cblMedications.Items[3].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[3].Selected = reader.GetBoolean(72);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(73))
                                {
                                    cblMedications.Items[4].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[4].Selected = reader.GetBoolean(73);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(74))
                                {
                                    cblMedications.Items[5].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[5].Selected = reader.GetBoolean(74);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(75))
                                {
                                    cblMedications.Items[6].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[6].Selected = reader.GetBoolean(75);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(76))
                                {
                                    cblMedications.Items[7].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[7].Selected = reader.GetBoolean(76);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(77))
                                {
                                    cblMedications.Items[8].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[8].Selected = reader.GetBoolean(77);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(78))
                                {
                                    cblMedications.Items[9].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[9].Selected = reader.GetBoolean(78);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(79))
                                {
                                    cblMedications.Items[10].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[10].Selected = reader.GetBoolean(79);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(80))
                                {
                                    cblMedications.Items[11].Selected = false;  //chbStudentVolunteer.Checked = false;
                                }
                                else
                                {
                                    cblMedications.Items[11].Selected = reader.GetBoolean(80);  //chbStudentVolunteer.Checked = false;
                                }
                                if (reader.IsDBNull(81))
                                {
                                    txbMedicationsOtherNotes.Text = "N/A";
                                }
                                else
                                {
                                    txbMedicationsOtherNotes.Text = reader.GetString(81);
                                }
                                if (reader.IsDBNull(82))
                                {
                                    chbScrubbed.Checked = false;
                                    chbScrubbed.Text = "";
                                    chbScrubbed.Enabled = true;
                                    lblLastScrubbed.Text = "Last Scrubbed?";
                                }
                                else
                                {
                                    chbScrubbed.Checked = reader.GetBoolean(82);
                                    if (chbScrubbed.Checked)
                                    {
                                        chbScrubbed.Text = "";
                                        chbScrubbed.Enabled = false;
                                        lblLastScrubbed.Text = "Last Scrubbed By: " + reader.GetString(84) + " On: " + reader.GetSqlValue(83).ToString();
                                        lblLastScrubbed.Visible = true;
                                        lblLastScrubbed.Enabled = true;
                                    }
                                    else
                                    {
                                        chbScrubbed.Text = "";
                                        chbScrubbed.Enabled = true;
                                        lblLastScrubbed.Text = "Last Scrubbed?";
                                        lblLastScrubbed.Visible = true;
                                        lblLastScrubbed.Enabled = true;
                                    }
                                }
                                if (reader.IsDBNull(85))
                                {
                                    chbReadingSupport.Checked = false;
                                }
                                else
                                {
                                    chbReadingSupport.Checked = reader.GetBoolean(85);
                                }
                                //chbPaid.Checked = reader.GetBoolean(34);
                                //ddlTextPhone.Items.Add("Yes");
                                //ddlTextPhone.Items.Add("No");
                                //ddlTextPhone.Text = "No";
                                reader.Close();
                            }

                            string sql_ParentGuardian = "";
                            if (Request.QueryString.ToString().Contains("StudentMiddleName"))
                            {
                                //Pull information for the ParentGuardian textboxes...
                                sql_ParentGuardian = "select ParentGuardianRelationship1, ParentGuardian1, ParentGuardian1Email, "
                                                    + "WorkPhone1, CellPhone1, TextPhone1, ParentGuardian2Relationship, ParentGuardian2, "
                                                    + "WorkPhone2, CellPhone2, TextPhone2, EmergencyContact, EmergRelationship, EmergencyContactPhone  "
                                                    + "FROM dbo.ParentGuardianContactInformation "
                                                    + "WHERE studentlastname=@studentlastname "
                                                    + "AND studentfirstname=@studentfirstname "
                                                    + "AND studentmiddlename=@studentmiddlename";
                            }
                            else
                            {
                                //Pull information for the ParentGuardian textboxes...
                                sql_ParentGuardian = "select ParentGuardianRelationship1, ParentGuardian1, ParentGuardian1Email, "
                                                    + "WorkPhone1, CellPhone1, TextPhone1, ParentGuardian2Relationship, ParentGuardian2, "
                                                    + "WorkPhone2, CellPhone2, TextPhone2, EmergencyContact, EmergRelationship, EmergencyContactPhone  "
                                                    + "FROM dbo.ParentGuardianContactInformation "
                                                    + "WHERE studentlastname=@studentlastname "
                                                    + "AND studentfirstname=@studentfirstname ";
                            }

                            SqlCommand cmd_ParentGuardian = new SqlCommand(sql_ParentGuardian);
                            if (Request.QueryString.ToString().Contains("StudentMiddleName"))
                            {
                                //Perform database lookup based on the chosen child..RCM..
                                cmd_ParentGuardian.Parameters.Add(new SqlParameter("@studentlastname", Request.QueryString["StudentLastName"]));
                                cmd_ParentGuardian.Parameters.Add(new SqlParameter("@studentfirstname", Request.QueryString["StudentFirstName"]));
                                cmd_ParentGuardian.Parameters.Add(new SqlParameter("@studentmiddlename", Request.QueryString["StudentMiddleName"]));
                            }
                            else
                            {
                                //Perform database lookup based on the chosen child..RCM..
                                cmd_ParentGuardian.Parameters.Add(new SqlParameter("@studentlastname", Request.QueryString["StudentLastName"]));
                                cmd_ParentGuardian.Parameters.Add(new SqlParameter("@studentfirstname", Request.QueryString["StudentFirstName"]));
                            }

                            cmd_ParentGuardian.Connection = con;
                            reader2 = cmd_ParentGuardian.ExecuteReader();
                            if (reader2.Read())
                            {
                                if (reader2.IsDBNull(0))
                                {
                                    ddlParentGuardian1Relationship.Text = "N/A";
                                }
                                else
                                {
                                    ddlParentGuardian1Relationship.Items.Add(reader2.GetString(0));
                                    ddlParentGuardian1Relationship.Text = reader2.GetString(0);
                                }
                                if (reader2.IsDBNull(1))
                                {
                                    txtParentGuardian1.Text = "N/A";
                                }
                                else
                                {
                                    txtParentGuardian1.Text = reader2.GetString(1);
                                }
                                if (reader2.IsDBNull(2))
                                {
                                    txbParentGuardian1Email.Text = "N/A";
                                }
                                else
                                {
                                    txbParentGuardian1Email.Text = reader2.GetString(2);
                                }
                                if (reader2.IsDBNull(3))
                                {
                                    txbParentGuardian1WrkPh.Text = "N/A";
                                }
                                else
                                {
                                    txbParentGuardian1WrkPh.Text = reader2.GetString(3);
                                }
                                if (reader2.IsDBNull(4))
                                {
                                    txbParentGuardian1CellPhone.Text = "N/A";
                                }
                                else
                                {
                                    txbParentGuardian1CellPhone.Text = reader2.GetString(4);
                                }
                                if (reader2.IsDBNull(5))
                                {
                                    ddlTextGuard1.Text = "Text Phone?";
                                }
                                else
                                {
                                    ddlTextGuard1.Text = ConvertBoolToYesNo(reader2.GetBoolean(5));
                                    //ddlTextGuard1.Text = Convert.ToString(reader2.GetBoolean(5));
                                }
                                if (reader2.IsDBNull(6))
                                {
                                    ddlParentGuardian2Relationship.Text = "N/A";
                                }
                                else
                                {
                                    ddlParentGuardian2Relationship.Items.Add(reader2.GetString(6));
                                    ddlParentGuardian2Relationship.Text = reader2.GetString(6);
                                }
                                if (reader2.IsDBNull(7))
                                {
                                    txbParentGuardian2.Text = "N/A";
                                }
                                else
                                {
                                    txbParentGuardian2.Text = reader2.GetSqlValue(7).ToString();
                                }
                                if (reader2.IsDBNull(8))
                                {
                                    txbParentGuardian2WrkPh.Text = reader2.GetSqlValue(8).ToString();
                                }
                                else
                                {
                                    txbParentGuardian2WrkPh.Text = reader2.GetSqlValue(8).ToString();
                                }
                                if (reader2.IsDBNull(9))
                                {
                                    txbParentGuardian2CellPhone.Text = "N/A";
                                }
                                else
                                {
                                    txbParentGuardian2CellPhone.Text = reader2.GetSqlValue(9).ToString();
                                }
                                if (reader2.IsDBNull(10))
                                {
                                    ddlTextGuard2.Text = "Text Phone?";
                                }
                                else
                                {
                                    ddlTextGuard2.Text = ConvertBoolToYesNo(reader2.GetBoolean(10));
                                    //ddlTextGuard2.Text = Convert.ToString(reader2.GetBoolean(10));
                                }
                                if (reader2.IsDBNull(11))
                                {
                                    txbEmergencyRelationship.Text = "N/A";
                                }
                                else
                                {
                                    txbEmergencyRelationship.Text = reader2.GetString(11);
                                }
                                if (reader2.IsDBNull(12))
                                {
                                    txbEmergRelationship.Text = "N/A";
                                }
                                else
                                {
                                    txbEmergRelationship.Text = reader2.GetString(12);
                                }
                                if (reader2.IsDBNull(13))
                                {
                                    txbEmergencyPhone.Text = "N/A";
                                }
                                else
                                {
                                    txbEmergencyPhone.Text = reader2.GetSqlValue(13).ToString();
                                }
                                reader2.Close();
                            }
                            CleanCharacters();
                        }
                        catch (Exception lkjl)
                        {
                            lblErrorMessage.Enabled = true;
                            lblErrorMessage.Text = "The SELECT from the DB failed.  Please fix and try again MSG: " + lkjl.Message.ToString();

                        }
                        finally
                        {
                            reader.Close();
                            //reader2.Close();
                        }
                        btnNewPerson1.Enabled = false;
                    }
                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!
                    Response.Redirect("ErrorAccess.aspx");
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
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txbMiddleName.Enabled = false;
        }

		protected void txtLastName_TextChanged(object sender, System.EventArgs e)
		{
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txbMiddleName.Enabled = false;
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
			ddlGrade.Enabled = true;
		}

		protected void chbZip_CheckedChanged(object sender, System.EventArgs e)
		{	
			txtZip.Enabled = true;
		}

		protected void chbCareerGoal_CheckedChanged(object sender, System.EventArgs e)
		{
			txtCareerGoal.Enabled = true;
		}

		protected void chbChurch_CheckedChanged(object sender, System.EventArgs e)
		{
			ddlChurch.Enabled = true;
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
			ddlSchool.Enabled = true;		
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
			ddlGrade.Enabled = true;
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
            chbSchool.Enabled = true;
            chbStudentCellPhone.Enabled = true;
            chbGrade.Enabled = true;
            chbGrade.Checked = true;

            //Set the values for the MailingList code dropdown..RCM..8/3/11.
            //ddlMailingListCodes.Items.Add("Use Current Address");
            ddlMailingListCodes.Items.Add("Wrong Address");
            ddlMailingListCodes.Items.Add("No Address");
            ddlMailingListCodes.Items.Add("Not Deliverable");
            ddlMailingListCodes.Items.Add("Out of Town");
            ddlMailingListCodes.Items.Add("Deceased");
            //ddlMailingListCodes.Text = "Use Current Address";

            //Set the default values for the Administration of Medicine..RCM..03/25/13.
            ddlAdministerMedicine.Items.Add("Administer Medicine?");
            ddlAdministerMedicine.Items.Add("Yes");
            ddlAdministerMedicine.Items.Add("No");
            ddlAdministerMedicine.Text = "Administer Medicine?";

            //Set values for Church dropdown...RCM..3/6/12.
            PopulateChurchDropdown();
            //ddlChurch.Items.Add("Attend Church?");
            //ddlChurch.Items.Add("1st Baptist Church Bridgeville");
            //ddlChurch.Items.Add("1st Church of God & Christ");
            //ddlChurch.Items.Add("1st church of God and Christ");
            //ddlChurch.Items.Add("1st Baptist Church of Penn Hills");
            //ddlChurch.Items.Add("1st Emmanuel");
            //ddlChurch.Items.Add("2nd Baptist");
            //ddlChurch.Items.Add("A Touch of God Ministries");
            //ddlChurch.Items.Add("ACAC");
            //ddlChurch.Items.Add("ACAC and Trinity Lutheran");
            //ddlChurch.Items.Add("AHC");
            //ddlChurch.Items.Add("Alliance");
            //ddlChurch.Items.Add("Allegheny Presbyterian");
            //ddlChurch.Items.Add("Allegheny Unitarian");
            //ddlChurch.Items.Add("Allen Chapel AME");
            //ddlChurch.Items.Add("Allison Park Assembly of God");
            //ddlChurch.Items.Add("ACCIC");
            //ddlChurch.Items.Add("AME Zion");
            //ddlChurch.Items.Add("An-Nur Islamic Center");
            //ddlChurch.Items.Add("Ascension");
            //ddlChurch.Items.Add("Ascension Lutheran");
            //ddlChurch.Items.Add("Assumption");
            //ddlChurch.Items.Add("Assumption Catholic");
            //ddlChurch.Items.Add("Assumption Church");
            //ddlChurch.Items.Add("AYD");
            //ddlChurch.Items.Add("Baptist");
            //ddlChurch.Items.Add("Baptist Church");
            //ddlChurch.Items.Add("Baptist Temple");
            //ddlChurch.Items.Add("Bellefield Presbyterian");
            //ddlChurch.Items.Add("Bellevue Christian");
            //ddlChurch.Items.Add("Beth Shalom Synagogue");
            //ddlChurch.Items.Add("Bethany");
            //ddlChurch.Items.Add("Bethany Baptist");
            //ddlChurch.Items.Add("Bethel AME");
            //ddlChurch.Items.Add("Bethel Assembly");
            //ddlChurch.Items.Add("Bethesada");
            //ddlChurch.Items.Add("Bethesda Church of God");
            //ddlChurch.Items.Add("Bethesda COGIC");
            //ddlChurch.Items.Add("Bethesda Presbyterian");
            //ddlChurch.Items.Add("Bethlehem Lutheran");
            //ddlChurch.Items.Add("Bethlehem Temple");
            //ddlChurch.Items.Add("Bethlehem Temple Church");
            //ddlChurch.Items.Add("Bethsida Church of God");
            //ddlChurch.Items.Add("Beverly Heights");
            //ddlChurch.Items.Add("Bible Center");
            //ddlChurch.Items.Add("Bidwell");
            //ddlChurch.Items.Add("Bidwell Presbyterian");
            //ddlChurch.Items.Add("Brentwood");
            //ddlChurch.Items.Add("Bridgeville 1st UMC");
            //ddlChurch.Items.Add("Brighton McClure Presby");
            //ddlChurch.Items.Add("Brighton-McClure Presbyterian");
            //ddlChurch.Items.Add("Brookline as of Radiant Life");
            //ddlChurch.Items.Add("Brookline Memorial");
            //ddlChurch.Items.Add("Brown Chapel");
            //ddlChurch.Items.Add("Brown Chapel AME");
            //ddlChurch.Items.Add("Calvary");
            //ddlChurch.Items.Add("Calvary Baptist");
            //ddlChurch.Items.Add("Calvary United Methodist");
            //ddlChurch.Items.Add("Catholic");
            //ddlChurch.Items.Add("CCGF");
            //ddlChurch.Items.Add("CCOP");
            //ddlChurch.Items.Add("Central Assembly");
            //ddlChurch.Items.Add("Central Baptist");
            //ddlChurch.Items.Add("Christ Church");
            //ddlChurch.Items.Add("Christ Missionary");
            //ddlChurch.Items.Add("Christ Missionary Baptist");
            //ddlChurch.Items.Add("Christ Temple");
            //ddlChurch.Items.Add("Christian Fellowship");
            //ddlChurch.Items.Add("Christian Fellowship Baptist");
            //ddlChurch.Items.Add("Christian Fellowship Center");
            //ddlChurch.Items.Add("Christian LightHouse");
            //ddlChurch.Items.Add("Christian Tabernacle");
            //ddlChurch.Items.Add("Church of Christ");
            //ddlChurch.Items.Add("Church of God");
            //ddlChurch.Items.Add("Church of God in Christ");
            //ddlChurch.Items.Add("Church of our Savior");
            //ddlChurch.Items.Add("Church of the Living God");
            //ddlChurch.Items.Add("City Reach Church Pittsburgh");
            //ddlChurch.Items.Add("City Reformed Presbytarian");
            //ddlChurch.Items.Add("City View");
            //ddlChurch.Items.Add("Clark Memorial Baptist");
            //ddlChurch.Items.Add("COGIC");
            //ddlChurch.Items.Add("COGIC McClure Street");
            //ddlChurch.Items.Add("Commission Baptist Church");
            //ddlChurch.Items.Add("Community House");
            //ddlChurch.Items.Add("Congregation of YESHUA BEN DAVID");
            //ddlChurch.Items.Add("Cornerstone Baptist");
            //ddlChurch.Items.Add("Cornerstone Church of God in Christ");
            //ddlChurch.Items.Add("COTS");
            //ddlChurch.Items.Add("Covanent");
            //ddlChurch.Items.Add("Covenant");
            //ddlChurch.Items.Add("Covenant Church of Pgh");
            //ddlChurch.Items.Add("Covenant Church on the Hill");
            //ddlChurch.Items.Add("Covenent Church of Pittsburgh");
            //ddlChurch.Items.Add("Damascus Church");
            //ddlChurch.Items.Add("Damascus GOGIC");
            //ddlChurch.Items.Add("DBC");
            //ddlChurch.Items.Add("Deliverance");
            //ddlChurch.Items.Add("Deliverance Baptist");
            //ddlChurch.Items.Add("Destiny of Faith");
            //ddlChurch.Items.Add("Destiny International");
            //ddlChurch.Items.Add("Destiny International Ministries");
            //ddlChurch.Items.Add("Don't Go");
            //ddlChurch.Items.Add("East Liberty PC");
            //ddlChurch.Items.Add("Eastminster");
            //ddlChurch.Items.Add("Ebanezer");
            //ddlChurch.Items.Add("Ecclesia");
            //ddlChurch.Items.Add("Emmanual");
            //ddlChurch.Items.Add("Emmanuel");
            //ddlChurch.Items.Add("Emmaus");
            //ddlChurch.Items.Add("Emmus");
            //ddlChurch.Items.Add("Enon");
            //ddlChurch.Items.Add("Enon Baptist");
            //ddlChurch.Items.Add("Faith Evangelical");
            //ddlChurch.Items.Add("Faith Evangelical Lutheran");
            //ddlChurch.Items.Add("Family Bible");
            //ddlChurch.Items.Add("Fellowship Temple");
            //ddlChurch.Items.Add("First Bapt. Penn Hills");
            //ddlChurch.Items.Add("First Baptist Penn Hills");
            //ddlChurch.Items.Add("First Presbyterian");
            //ddlChurch.Items.Add("First Muslim Mosque");
            //ddlChurch.Items.Add("Forest Avenue Presbyterian");
            //ddlChurch.Items.Add("Free Indeed Ministries");
            //ddlChurch.Items.Add("Friendship");
            //ddlChurch.Items.Add("Friendship Community");
            //ddlChurch.Items.Add("Gaurdian Angels");
            //ddlChurch.Items.Add("Gladeron Luthern Services");
            //ddlChurch.Items.Add("Good Hope Baptist");
            //ddlChurch.Items.Add("Grace Community");
            //ddlChurch.Items.Add("Grace Memorial");
            //ddlChurch.Items.Add("Grace Presbyterian");
            //ddlChurch.Items.Add("Grace Evangelical");
            //ddlChurch.Items.Add("GRCC");
            ////ddlChurch.Items.Add("Greater Allen");
            ////ddlChurch.Items.Add("Greater Allen AME");
            //ddlChurch.Items.Add("Greater Allen AME Church");
            ////ddlChurch.Items.Add("Greater Allen Chapel");
            //ddlChurch.Items.Add("Greater Deliverance");
            //ddlChurch.Items.Add("Greater Deliverance Temple");
            //ddlChurch.Items.Add("Greater Revival Ctr");
            //ddlChurch.Items.Add("Greater Works");
            //ddlChurch.Items.Add("Greater Works Outreach");
            //ddlChurch.Items.Add("Greens Temple");
            //ddlChurch.Items.Add("Guardian Angels");
            //ddlChurch.Items.Add("Guardian Angels Parish");
            //ddlChurch.Items.Add("Guardian Angels/St Martin");
            //ddlChurch.Items.Add("HCWOC Higher Call");
            //ddlChurch.Items.Add("Higher Call");
            //ddlChurch.Items.Add("Higher Call World Outreach");
            //ddlChurch.Items.Add("Hillcrest Baptist");
            //ddlChurch.Items.Add("Hillside Christian");
            //ddlChurch.Items.Add("Hilltop");
            //ddlChurch.Items.Add("Holiday Memorial AME");
            //ddlChurch.Items.Add("Holliest SDA Church");
            //ddlChurch.Items.Add("Holy Cross");
            //ddlChurch.Items.Add("Holy Innocen");
            //ddlChurch.Items.Add("Holy Rosary");
            //ddlChurch.Items.Add("Holy Spirit");
            //ddlChurch.Items.Add("Homewood");
            //ddlChurch.Items.Add("Hope Chapel");
            //ddlChurch.Items.Add("Hosanna");
            //ddlChurch.Items.Add("Imani");
            //ddlChurch.Items.Add("Ingram Presbyterian");
            //ddlChurch.Items.Add("Ist Emmanuel");
            //ddlChurch.Items.Add("Ist Immanuele");
            //ddlChurch.Items.Add("Jerusalem");
            //ddlChurch.Items.Add("Jerusalem Baptist");
            //ddlChurch.Items.Add("John Wesley");
            //ddlChurch.Items.Add("Jubilee International Ministries");
            //ddlChurch.Items.Add("KCM");
            //ddlChurch.Items.Add("Keystone Church of Hazelwood");
            //ddlChurch.Items.Add("Keystone/Center of Life");
            //ddlChurch.Items.Add("King of Kings");
            //ddlChurch.Items.Add("King of Kings Baptist");
            //ddlChurch.Items.Add("Kingdom C.O.M.E.");
            //ddlChurch.Items.Add("Kingdom Come Ministries");
            //ddlChurch.Items.Add("Kingdom Hall");
            //ddlChurch.Items.Add("Kingdom Hall J.W.");
            //ddlChurch.Items.Add("Laketon heights");
            //ddlChurch.Items.Add("Lamb of God Lion of Judah");
            //ddlChurch.Items.Add("Lighthouse");
            //ddlChurch.Items.Add("Living Bridge Community");
            //ddlChurch.Items.Add("Living Word Ministries");
            //ddlChurch.Items.Add("Lord's Church");
            //ddlChurch.Items.Add("Lutheran Trinity");
            //ddlChurch.Items.Add("Macedamia Baptist Church");
            //ddlChurch.Items.Add("Macedonia Baptist");
            //ddlChurch.Items.Add("Mana");
            //ddlChurch.Items.Add("Manna from on High");
            //ddlChurch.Items.Add("MASJID NUR");
            //ddlChurch.Items.Add("Metropolitan Baptist");
            //ddlChurch.Items.Add("Millenial Reign Family Worship Center");
            //ddlChurch.Items.Add("Miracle COGIC");
            //ddlChurch.Items.Add("Miracle House");
            //ddlChurch.Items.Add("Missionary Alliance");
            //ddlChurch.Items.Add("Missionary Temple COGIC");
            //ddlChurch.Items.Add("MIUPC");
            //ddlChurch.Items.Add("Morningside");
            //ddlChurch.Items.Add("Morningside Church of God in Christ");
            //ddlChurch.Items.Add("Morningside COGIC");
            //ddlChurch.Items.Add("Morning Star Baptist");
            //ddlChurch.Items.Add("Mosaic");
            //ddlChurch.Items.Add("Mosaic Community");
            //ddlChurch.Items.Add("Most High Name");
            //ddlChurch.Items.Add("Most Holy Name");
            //ddlChurch.Items.Add("Moumental Baptist");
            //ddlChurch.Items.Add("Mount Ararat");
            //ddlChurch.Items.Add("Mount Rise");
            //ddlChurch.Items.Add("Mount Zion Baptist");
            //ddlChurch.Items.Add("Mt Carmel");
            //ddlChurch.Items.Add("Mt Carmel Baptist");
            //ddlChurch.Items.Add("Mt Gilead");
            //ddlChurch.Items.Add("Mt Lebanon Presbyterian");
            //ddlChurch.Items.Add("Mt Zion");
            //ddlChurch.Items.Add("Mt Zion Baptist");
            //ddlChurch.Items.Add("Mt. Aarat Baptist Church");
            //ddlChurch.Items.Add("Mt. Arart");
            //ddlChurch.Items.Add("Mt. Arat");
            //ddlChurch.Items.Add("Mt. Carmel");
            //ddlChurch.Items.Add("Mt. Lebanon U.P. Church");
            //ddlChurch.Items.Add("muslim");
            //ddlChurch.Items.Add("N.S.I.C.O.G.I.C.");
            //ddlChurch.Items.Add("Nazarene Baptist Church");
            //ddlChurch.Items.Add("Nazareth Baptist Church");
            //ddlChurch.Items.Add("NCGIC");
            //ddlChurch.Items.Add("New Bethel");
            //ddlChurch.Items.Add("New Bethel Baptist");
            //ddlChurch.Items.Add("New Christian Fellowship");
            //ddlChurch.Items.Add("New Community");
            //ddlChurch.Items.Add("New Community Church");
            //ddlChurch.Items.Add("New Dawn");
            //ddlChurch.Items.Add("New Destiny");
            //ddlChurch.Items.Add("New Grace");
            //ddlChurch.Items.Add("New Hope");
            //ddlChurch.Items.Add("New Hope Baptist");
            //ddlChurch.Items.Add("New Hope Church");
            //ddlChurch.Items.Add("New Life");
            //ddlChurch.Items.Add("New Life Baptist");
            //ddlChurch.Items.Add("New Life Community Church");
            //ddlChurch.Items.Add("New Light");
            //ddlChurch.Items.Add("New Light Temple");
            //ddlChurch.Items.Add("New Light Temple Baptist");
            //ddlChurch.Items.Add("New Testament Church");
            //ddlChurch.Items.Add("New Zion");
            //ddlChurch.Items.Add("New Zion Baptist");
            //ddlChurch.Items.Add("Newlite Baptist");
            //ddlChurch.Items.Add("NICGIC");
            //ddlChurch.Items.Add("NMt. Arart Baptist Church");
            //ddlChurch.Items.Add("Norhtside Institutinal");
            //ddlChurch.Items.Add("North Commons");
            //ddlChurch.Items.Add("North Gate");
            //ddlChurch.Items.Add("North Hills Christian");
            //ddlChurch.Items.Add("North Minster Presbyterian Church");
            //ddlChurch.Items.Add("North Side");
            //ddlChurch.Items.Add("North Side Church of God");
            //ddlChurch.Items.Add("North Side Church of God in Christ");
            //ddlChurch.Items.Add("North Side Institution");
            //ddlChurch.Items.Add("North Side Institutional");
            //ddlChurch.Items.Add("Northgate");
            //ddlChurch.Items.Add("Northman");
            //ddlChurch.Items.Add("Northminster");
            //ddlChurch.Items.Add("Northminster Presb");
            //ddlChurch.Items.Add("Northmount UP");
            //ddlChurch.Items.Add("Northside");
            //ddlChurch.Items.Add("Northside Alliance");
            //ddlChurch.Items.Add("Northside Church of God");
            //ddlChurch.Items.Add("Northside COG");
            //ddlChurch.Items.Add("Northside Instalational");
            //ddlChurch.Items.Add("Northside Institute");
            //ddlChurch.Items.Add("Northside Institutional");
            //ddlChurch.Items.Add("Northside Institutional Church");
            //ddlChurch.Items.Add("Northside Institutional Church of God in Christ");
            //ddlChurch.Items.Add("Northside Institutional COG and Christ");
            //ddlChurch.Items.Add("Northside Institutional COGIC");
            //ddlChurch.Items.Add("Northview");
            //ddlChurch.Items.Add("Northway");
            //ddlChurch.Items.Add("Northway Christian Community Wexford");
            //ddlChurch.Items.Add("Northway Community");
            //ddlChurch.Items.Add("Northway Oakland");
            //ddlChurch.Items.Add("NS Institutional COGIC");
            //ddlChurch.Items.Add("NSICOGIC");
            //ddlChurch.Items.Add("Olivant Baptist");
            //ddlChurch.Items.Add("Open Door");
            //ddlChurch.Items.Add("Orchard Hill");
            //ddlChurch.Items.Add("Original Church");
            //ddlChurch.Items.Add("Original Church of God");
            //ddlChurch.Items.Add("Original Church of God Deliverance Center");
            //ddlChurch.Items.Add("Our Lady of Grace");
            //ddlChurch.Items.Add("P Temple");
            //ddlChurch.Items.Add("PCO");
            //ddlChurch.Items.Add("Pentecostal Temple");
            //ddlChurch.Items.Add("Perry");
            //ddlChurch.Items.Add("Petra");
            //ddlChurch.Items.Add("Petra International Ministries");
            //ddlChurch.Items.Add("Petra International Ministry");
            //ddlChurch.Items.Add("Pgh City Outreach");
            //ddlChurch.Items.Add("Pgh Revival Center");
            //ddlChurch.Items.Add("Pgh SDA");
            //ddlChurch.Items.Add("Pilgrim");
            //ddlChurch.Items.Add("Pilgrim Baptist");
            //ddlChurch.Items.Add("Pillgram Baptist");
            //ddlChurch.Items.Add("Pittsburgh City Outreach");
            //ddlChurch.Items.Add("Pittsburgh Project");
            //ddlChurch.Items.Add("Pittsburgh Revival Center");
            //ddlChurch.Items.Add("Pneuma Center");
            //ddlChurch.Items.Add("Potter's House");
            //ddlChurch.Items.Add("Potters House Ministries");
            //ddlChurch.Items.Add("PRC");
            //ddlChurch.Items.Add("Presb.");
            //ddlChurch.Items.Add("Presbyterian");
            //ddlChurch.Items.Add("Presbyterian Church");
            //ddlChurch.Items.Add("Radiant Life");
            //ddlChurch.Items.Add("Rainbow Temple Assembly of God");
            //ddlChurch.Items.Add("Refuge Church of God in Christ");
            //ddlChurch.Items.Add("Redeemer Community");
            //ddlChurch.Items.Add("Rehoboth");
            //ddlChurch.Items.Add("Resurrection Baptist");
            //ddlChurch.Items.Add("Risen Lord");
            //ddlChurch.Items.Add("Rising Star Baptist");
            //ddlChurch.Items.Add("Riverside - Oakmont");
            //ddlChurch.Items.Add("Riverview");
            ////ddlChurch.Items.Add("Rodman");
            ////ddlChurch.Items.Add("Rodman Missionary Baptist");
            //ddlChurch.Items.Add("Rodman St Missionary Baptist Church");
            ////ddlChurch.Items.Add("Rodman Street");
            ////ddlChurch.Items.Add("Rodmen");
            //ddlChurch.Items.Add("Rose of Sharon");
            ////ddlChurch.Items.Add("Rudman Street");
            //ddlChurch.Items.Add("Sacred Heart");
            //ddlChurch.Items.Add("Salvation Army");
            //ddlChurch.Items.Add("Second Baptist");
            //ddlChurch.Items.Add("Second Baptist of Carnegie");
            //ddlChurch.Items.Add("Seeds of Hope");
            ////ddlChurch.Items.Add("Sharpersburg Fam Worship Center");
            ////ddlChurch.Items.Add("Sharpsburg Fam Worship Center");
            //ddlChurch.Items.Add("Sharpsburg Family Worship Center");
            //ddlChurch.Items.Add("Seed and Harvest");
            //ddlChurch.Items.Add("SHOP Ministries");
            //ddlChurch.Items.Add("SHBC");
            //ddlChurch.Items.Add("Sister Branch Baptist");
            //ddlChurch.Items.Add("Sixth Mount Zion");
            //ddlChurch.Items.Add("Smithfield UCC");
            //ddlChurch.Items.Add("Sons of God Ministry");
            //ddlChurch.Items.Add("South Hill Baptist");
            //ddlChurch.Items.Add("South Hills Assembly");
            //ddlChurch.Items.Add("South Hills Baptist");
            //ddlChurch.Items.Add("South Minister Presbyterian");
            //ddlChurch.Items.Add("St Alexis");
            //ddlChurch.Items.Add("St Altonsis");
            //ddlChurch.Items.Add("St Anne");
            //ddlChurch.Items.Add("St Auslem");
            //ddlChurch.Items.Add("St Benedict");
            //ddlChurch.Items.Add("St Benedict D Moor");
            //ddlChurch.Items.Add("St Benedict the Moor");
            //ddlChurch.Items.Add("St Bernard");
            //ddlChurch.Items.Add("St Bernards");
            //ddlChurch.Items.Add("St Bonaventure");
            //ddlChurch.Items.Add("St Columbkille");
            //ddlChurch.Items.Add("St Cyril");
            //ddlChurch.Items.Add("St Cyril's");
            //ddlChurch.Items.Add("St James");
            //ddlChurch.Items.Add("St James AME");
            //ddlChurch.Items.Add("St James Baptist");
            //ddlChurch.Items.Add("St Joes");
            //ddlChurch.Items.Add("St John Newman");
            //ddlChurch.Items.Add("St John of God");
            //ddlChurch.Items.Add("St John Vianney");
            //ddlChurch.Items.Add("St Joseph");
            //ddlChurch.Items.Add("St Justin");
            //ddlChurch.Items.Add("St Malachy");
            //ddlChurch.Items.Add("St Margaret of Scot");
            //ddlChurch.Items.Add("St Maurice");
            //ddlChurch.Items.Add("St Michaels");
            //ddlChurch.Items.Add("St Paul");
            //ddlChurch.Items.Add("St Paul AME Zion");
            //ddlChurch.Items.Add("St Paul Baptist");
            //ddlChurch.Items.Add("St Pauls");
            //ddlChurch.Items.Add("St Peter");
            //ddlChurch.Items.Add("St Peters");
            //ddlChurch.Items.Add("St Phillip");
            //ddlChurch.Items.Add("St Raphael Church");
            //ddlChurch.Items.Add("St Rosalia");
            //ddlChurch.Items.Add("St Sebastian Church");
            //ddlChurch.Items.Add("St Stephens");
            //ddlChurch.Items.Add("St Ursula");
            //ddlChurch.Items.Add("St Vladimir");
            //ddlChurch.Items.Add("St. Benedict the Moor");
            //ddlChurch.Items.Add("St. Cyril");
            //ddlChurch.Items.Add("St. Cyrils");
            //ddlChurch.Items.Add("St. John");
            //ddlChurch.Items.Add("St. Johns");
            //ddlChurch.Items.Add("St. Malauly");
            //ddlChurch.Items.Add("St. Margarets");
            //ddlChurch.Items.Add("St. Margarets of Scotland");
            //ddlChurch.Items.Add("St. Matthew");
            //ddlChurch.Items.Add("St. Michaels");
            //ddlChurch.Items.Add("St. Paul Baptist");
            //ddlChurch.Items.Add("St. Nicholas Orth");
            //ddlChurch.Items.Add("St. Paul Episcopal");
            //ddlChurch.Items.Add("St. Peter/Paul");
            //ddlChurch.Items.Add("St. Peters");
            //ddlChurch.Items.Add("St. Peters Northside");
            //ddlChurch.Items.Add("St. Phillips Moon");
            //ddlChurch.Items.Add("St. Stephens");
            ////ddlChurch.Items.Add("St. Thomas Luther");
            //ddlChurch.Items.Add("St. Thomas Lutheran");
            //ddlChurch.Items.Add("St. Thomas More");
            //ddlChurch.Items.Add("St. Vladimeirs");
            //ddlChurch.Items.Add("St. Vladimirs Ukranian Chuch");
            //ddlChurch.Items.Add("St. Winifred");
            //ddlChurch.Items.Add("Tabernacle");
            //ddlChurch.Items.Add("Tabernacle Baptist");
            //ddlChurch.Items.Add("Tabernacle Baptist (TCBC)");
            //ddlChurch.Items.Add("Tabernacle Baptist Church");
            //ddlChurch.Items.Add("Tabernacle Cosmopolitan Baptist Church");
            //ddlChurch.Items.Add("Tabernacle Cosmopolitin Baptist");
            //ddlChurch.Items.Add("TCBC");
            //ddlChurch.Items.Add("Temple");
            //ddlChurch.Items.Add("Temple Emanuel");
            //ddlChurch.Items.Add("Tetelestai Church");
            //ddlChurch.Items.Add("The Kingdom Hall");
            //ddlChurch.Items.Add("The Way The Truth The Light Ministries");
            //ddlChurch.Items.Add("Triedstone Baptist");
            //ddlChurch.Items.Add("Trinity");
            //ddlChurch.Items.Add("Trinity A.M.E. Zion");
            //ddlChurch.Items.Add("Trinity AME");
            //ddlChurch.Items.Add("Trinity Cathedral");
            //ddlChurch.Items.Add("Trinity Church");
            //ddlChurch.Items.Add("Trinity Lutheran");
            //ddlChurch.Items.Add("Trinity Lutheran Church");
            //ddlChurch.Items.Add("Trinity Northside");
            //ddlChurch.Items.Add("Triumph");
            //ddlChurch.Items.Add("Triumph Baptist");
            //ddlChurch.Items.Add("True Ministry of Pittsburgh");
            //ddlChurch.Items.Add("TYD");
            //ddlChurch.Items.Add("Union Baptist");
            //ddlChurch.Items.Add("Unitarian Universalist of N.H.");
            //ddlChurch.Items.Add("United Methodist");
            //ddlChurch.Items.Add("Unity");
            //ddlChurch.Items.Add("Unity Baptist");
            //ddlChurch.Items.Add("Urban Impact");
            //ddlChurch.Items.Add("US church of God");
            //ddlChurch.Items.Add("Valley View");
            //ddlChurch.Items.Add("Victory");
            //ddlChurch.Items.Add("Victory Family Church");
            //ddlChurch.Items.Add("Victory Temple");
            //ddlChurch.Items.Add("Visitor of Bethel Baptist");
            //ddlChurch.Items.Add("Way Truth Life Ministry");
            //ddlChurch.Items.Add("Waymen AME");
            ////ddlChurch.Items.Add("Wesley Center");
            //ddlChurch.Items.Add("Wesley Center AME Zion");
            //ddlChurch.Items.Add("West Side Corps");
            ////ddlChurch.Items.Add("Westley Center");
            //ddlChurch.Items.Add("Westminster Presbyterian");
            //ddlChurch.Items.Add("White Lily Baptist");
            //ddlChurch.Items.Add("Whole Truth COGIC");
            ////ddlChurch.Items.Add("Whosoever Will's");
            ////ddlChurch.Items.Add("Whosoever Will's CMA");
            //ddlChurch.Items.Add("Whosoever Wills CMA");
            //ddlChurch.Items.Add("Word of God");
            //ddlChurch.Items.Add("Word of Faith");
            //ddlChurch.Items.Add("Word of Truth");
            //ddlChurch.Items.Add("WVC Mars");
            //ddlChurch.Items.Add("Zion Hill Baptist Church");
            //ddlChurch.Text = "Attend Church?";

            //Set values for CareerGoals..RCM..2/20/12..
            ddlCareerGoal.Items.Add("Career Goal?");
            ddlCareerGoal.Items.Add("Acting");
            ddlCareerGoal.Items.Add("Actress");
            ddlCareerGoal.Items.Add("Accounting");
            ddlCareerGoal.Items.Add("Artist");
            ddlCareerGoal.Items.Add("Author");
            ddlCareerGoal.Items.Add("Basketball");
            ddlCareerGoal.Items.Add("Business");
            ddlCareerGoal.Items.Add("Business Manager");
            ddlCareerGoal.Items.Add("Cake Decorator");
            ddlCareerGoal.Items.Add("Carpenter");
            ddlCareerGoal.Items.Add("Chemistry");
            ddlCareerGoal.Items.Add("Coach");
            ddlCareerGoal.Items.Add("Computer Engineer");
            ddlCareerGoal.Items.Add("Cosmotology");
            ddlCareerGoal.Items.Add("Dentist");
            ddlCareerGoal.Items.Add("Doctor");
            ddlCareerGoal.Items.Add("Engineer");
            ddlCareerGoal.Items.Add("Fashion Design");
            ddlCareerGoal.Items.Add("Hair stylist");
            ddlCareerGoal.Items.Add("Harvard Business School");
            ddlCareerGoal.Items.Add("Lawyer");
            ddlCareerGoal.Items.Add("LifeGuard");
            ddlCareerGoal.Items.Add("Medicine");
            ddlCareerGoal.Items.Add("Military");
            ddlCareerGoal.Items.Add("Ministry");
            ddlCareerGoal.Items.Add("NFL");
            ddlCareerGoal.Items.Add("Nursing");
            ddlCareerGoal.Items.Add("Police enforcement");
            ddlCareerGoal.Items.Add("Teacher, Firefighter");
            ddlCareerGoal.Items.Add("Police Officer");
            ddlCareerGoal.Items.Add("fashion designer, singer");
            ddlCareerGoal.Items.Add("CSI");
            ddlCareerGoal.Items.Add("docter/lawyer");
            ddlCareerGoal.Items.Add("Writer");
            ddlCareerGoal.Items.Add("Music Therapy");
            ddlCareerGoal.Items.Add("Pediatrition");
            ddlCareerGoal.Items.Add("Writer");
            ddlCareerGoal.Items.Add("Nursing");
            ddlCareerGoal.Items.Add("Robotics");
            ddlCareerGoal.Items.Add("Singer/Actress");
            ddlCareerGoal.Items.Add("Psychology");
            ddlCareerGoal.Items.Add("Sports Medicine");
            ddlCareerGoal.Items.Add("Music Ed.");
            ddlCareerGoal.Items.Add("General contractor");
            ddlCareerGoal.Items.Add("teacher, lawyer");
            ddlCareerGoal.Items.Add("writer/sniper");
            ddlCareerGoal.Items.Add("scientist");
            ddlCareerGoal.Items.Add("music");
            ddlCareerGoal.Items.Add("Hockey Player");
            ddlCareerGoal.Items.Add("pastry chef, vocal teacher");
            ddlCareerGoal.Items.Add("Social work");
            ddlCareerGoal.Items.Add("Physical Therapy");
            ddlCareerGoal.Items.Add("engineering");
            ddlCareerGoal.Items.Add("Massage Therapist");
            ddlCareerGoal.Items.Add("surgeon or vet");
            ddlCareerGoal.Items.Add("Supreme Court Judge");
            ddlCareerGoal.Items.Add("Fashion Designer");
            ddlCareerGoal.Items.Add("Musician");
            ddlCareerGoal.Items.Add("designer");
            ddlCareerGoal.Items.Add("business/law");
            ddlCareerGoal.Items.Add("Vet");
            ddlCareerGoal.Items.Add("Movie Producer");
            ddlCareerGoal.Items.Add("Voice or Dancing");
            ddlCareerGoal.Items.Add("Marine Biologist");
            ddlCareerGoal.Items.Add("Fashion Designer");
            ddlCareerGoal.Items.Add("Model");
            ddlCareerGoal.Items.Add("Engineer");
            ddlCareerGoal.Items.Add("Counselor");
            ddlCareerGoal.Items.Add("media broadcaster");
            ddlCareerGoal.Items.Add("Lawyer, fashion stylist, therapist");
            ddlCareerGoal.Items.Add("AV Engineering");
            ddlCareerGoal.Items.Add("Singer");
            ddlCareerGoal.Items.Add("follow Jesus");
            ddlCareerGoal.Items.Add("Lawyer");
            ddlCareerGoal.Items.Add("super model");
            ddlCareerGoal.Items.Add("Social Work");
            ddlCareerGoal.Items.Add("Auto Design");
            ddlCareerGoal.Items.Add("Traum Doctor");
            ddlCareerGoal.Items.Add("Actress");
            ddlCareerGoal.Items.Add("Brain Surgeon");
            ddlCareerGoal.Items.Add("scientist");
            ddlCareerGoal.Items.Add("Nurse/Teacher");
            ddlCareerGoal.Items.Add("Interior Design");
            ddlCareerGoal.Items.Add("Teacher");
            ddlCareerGoal.Items.Add("Psychologist");
            ddlCareerGoal.Items.Add("MLB");
            ddlCareerGoal.Items.Add("Vet or Dancer");
            ddlCareerGoal.Items.Add("Medical");
            ddlCareerGoal.Items.Add("Elementary Ed.");
            ddlCareerGoal.Items.Add("Sports medicine, football player");
            ddlCareerGoal.Items.Add("Interior/Clothes Design");
            ddlCareerGoal.Items.Add("Actress/Singer");
            ddlCareerGoal.Items.Add("missionary/Brain surgeon");
            ddlCareerGoal.Items.Add("WMBA Superstar");
            ddlCareerGoal.Items.Add("Missions, Engineering");
            ddlCareerGoal.Items.Add("Science, Business");
            ddlCareerGoal.Items.Add("Acting");
            ddlCareerGoal.Text = "Career Goal?";
            
            //Populating the dropdown for Schools...RCM..
            PopulateSchoolsDropdown();
            //ddlSchool.Items.Add("Select a school");
            //ddlSchool.Items.Add("Agora Cyber Charter School");
            //ddlSchool.Items.Add("Allderdice High School");
            //ddlSchool.Items.Add("Allegheny Intermediate Unit");
            //ddlSchool.Items.Add("Allegheny Traditional Academy");
            //ddlSchool.Items.Add("Art Institute of Pittsburgh");
            //ddlSchool.Items.Add("Arsenal");
            //ddlSchool.Items.Add("Barak Obama Academy of International Studies");
            //ddlSchool.Items.Add("Bellevue Elementary");
            //ddlSchool.Items.Add("Bethel Park High School");
            //ddlSchool.Items.Add("Burchfield Primary School");
            //ddlSchool.Items.Add("Blackburn/Home");
            //ddlSchool.Items.Add("Brashear High School");
            //ddlSchool.Items.Add("CAPA 6-12");
            //ddlSchool.Items.Add("Carmalt Math and Science");
            //ddlSchool.Items.Add("Carlynton Junior/Senior High School");
            //ddlSchool.Items.Add("Carson Middle School");
            //ddlSchool.Items.Add("CCAC");
            //ddlSchool.Items.Add("Center Area High School");
            //ddlSchool.Items.Add("Central Catholic");
            //ddlSchool.Items.Add("Chatham University");
            //ddlSchool.Items.Add("Cheswick Christian Academy");
            //ddlSchool.Items.Add("City High Charter High");
            //ddlSchool.Items.Add("Eden Christian Academy");
            //ddlSchool.Items.Add("Falk");
            //ddlSchool.Items.Add("Faslon");
            //ddlSchool.Items.Add("Fulton Academy");
            //ddlSchool.Items.Add("Graduated");
            //ddlSchool.Items.Add("Hillcrest Christian Academy");
            //ddlSchool.Items.Add("Homeschooled");
            //ddlSchool.Items.Add("Imani Christian Academy");
            //ddlSchool.Items.Add("Ingomar Elementary");
            //ddlSchool.Items.Add("Jefferson Elementary");
            //ddlSchool.Items.Add("Jubilee Christian School");
            //ddlSchool.Items.Add("Keystone Oaks High School");
            //ddlSchool.Items.Add("Keystone Oaks Middle School");
            ////ddlSchool.Items.Add("King Elementary");
            //ddlSchool.Items.Add("Liberty Elementary");
            //ddlSchool.Items.Add("Lincoln Park Performing Arts");
            //ddlSchool.Items.Add("Lincoln Academy");
            //ddlSchool.Items.Add("Manchester Academic Charter School");
            //ddlSchool.Items.Add("McKnight Elementary");
            ////ddlSchool.Items.Add("Martin Luther King ALA");
            //ddlSchool.Items.Add("Minadeo Elementary");
            //ddlSchool.Items.Add("Montour HS");
            //ddlSchool.Items.Add("MonValley");
            //ddlSchool.Items.Add("Moon Area High School");
            //ddlSchool.Items.Add("Moon Area Middle School");
            //ddlSchool.Items.Add("Mt. Nazareth (PreK)");
            //ddlSchool.Items.Add("North Allegheny High School");
            //ddlSchool.Items.Add("North Catholic High School");
            //ddlSchool.Items.Add("North Hills High School");
            //ddlSchool.Items.Add("Northgate Middle School");
            //ddlSchool.Items.Add("Northgate High School");
            //ddlSchool.Items.Add("NorthSide Catholic");
            //ddlSchool.Items.Add("Nyack College");
            //ddlSchool.Items.Add("Oliver High School");
            //ddlSchool.Items.Add("Pa Cyber");
            //ddlSchool.Items.Add("Pa Leadership Charter School");
            //ddlSchool.Items.Add("Perry Traditional Academy");
            //ddlSchool.Items.Add("Pittsburgh Dilworth");
            //ddlSchool.Items.Add("Pittsburgh King");
            //ddlSchool.Items.Add("Pgh Montessori");
            //ddlSchool.Items.Add("Pgh Spring Hill");
            //ddlSchool.Items.Add("Pittsburgh Urban Christian School");
            //ddlSchool.Items.Add("Phillips Elementary School");
            //ddlSchool.Items.Add("Pine Richland High School");
            //ddlSchool.Items.Add("Pittsburgh Fulton");
            //ddlSchool.Items.Add("Pittsburgh Langley K-8");
            //ddlSchool.Items.Add("Pittsburgh Morrow");
            //ddlSchool.Items.Add("Pittsburgh Northview");
            //ddlSchool.Items.Add("Pittsburgh Schiller Classical Academy");
            //ddlSchool.Items.Add("Pittsburgh Spring Hill");
            //ddlSchool.Items.Add("Propel Andrew St. High School");
            //ddlSchool.Items.Add("Propel Braddock Hils High School");
            //ddlSchool.Items.Add("Propel Braddock Hills Elem.");
            //ddlSchool.Items.Add("Propel East");
            //ddlSchool.Items.Add("Propel Homestead");
            //ddlSchool.Items.Add("Propel McKeesport");
            //ddlSchool.Items.Add("Propel Montour");
            //ddlSchool.Items.Add("Propel Northside");
            //ddlSchool.Items.Add("Providence");
            //ddlSchool.Items.Add("Quaker Valley Middle School");
            //ddlSchool.Items.Add("Ridge");
            //ddlSchool.Items.Add("Rhema");
            //ddlSchool.Items.Add("Ross Elementary");
            ////ddlSchool.Items.Add("Schiller");
            //ddlSchool.Items.Add("Science Technology");
            //ddlSchool.Items.Add("Schaeffer School");
            //ddlSchool.Items.Add("Schenley High School");
            //ddlSchool.Items.Add("Seton LaSalle");
            //ddlSchool.Items.Add("Sewickley Academy");
            //ddlSchool.Items.Add("Shaler Elementary School");
            //ddlSchool.Items.Add("Shaler High School");
            //ddlSchool.Items.Add("South Hills Middle School");
            //ddlSchool.Items.Add("Spring Garden");
            //ddlSchool.Items.Add("St Benedict the Moor");
            //ddlSchool.Items.Add("St. Agnes");
            //ddlSchool.Items.Add("Stem School");
            //ddlSchool.Items.Add("Sterrett");
            //ddlSchool.Items.Add("Sto-Rox High School");
            //ddlSchool.Items.Add("Sto-Rox Middle School");
            //ddlSchool.Items.Add("Sto-Rox Elementary School");
            //ddlSchool.Items.Add("The Ellis School");
            //ddlSchool.Items.Add("TLC Learning Center (PreK)");
            //ddlSchool.Items.Add("Trinity Christian");
            //ddlSchool.Items.Add("Urban League Greater Pittsburgh Charter School");
            //ddlSchool.Items.Add("Urban Pathways");
            //ddlSchool.Items.Add("University Prep");
            //ddlSchool.Items.Add("Urban League");
            //ddlSchool.Items.Add("West Liberty");
            //ddlSchool.Items.Add("Woodland Hills");
            //ddlSchool.Text = "Select a school";

            ddlGrade.Items.Add("PreK");
            ddlGrade.Items.Add("K");
            ddlGrade.Items.Add("1");
            ddlGrade.Items.Add("2");
            ddlGrade.Items.Add("3");
            ddlGrade.Items.Add("4");
            ddlGrade.Items.Add("5");
            ddlGrade.Items.Add("6");
            ddlGrade.Items.Add("7");
            ddlGrade.Items.Add("8");
            ddlGrade.Items.Add("9");
            ddlGrade.Items.Add("10");
            ddlGrade.Items.Add("11");
            ddlGrade.Items.Add("12");
            ddlGrade.Items.Add("GR");
            ddlGrade.Items.Add("GR09");
            ddlGrade.Items.Add("GR10");
            ddlGrade.Items.Add("GR11");
            ddlGrade.Items.Add("GR12");
            ddlGrade.Items.Add("GR13");
            ddlGrade.Items.Add("GR14");
            ddlGrade.Items.Add("GR15");
            ddlGrade.Items.Add("GR16");
            ddlGrade.Items.Add("GR17");
            ddlGrade.Items.Add("GR18");
            ddlGrade.Items.Add("GR19");
            ddlGrade.Items.Add("GR20");
            ddlGrade.Items.Add("SV");

            ddlAge.Items.Add("4");
            ddlAge.Items.Add("5");
            ddlAge.Items.Add("6");
            ddlAge.Items.Add("7");
            ddlAge.Items.Add("8");
            ddlAge.Items.Add("9");
            ddlAge.Items.Add("10");
            ddlAge.Items.Add("11");
            ddlAge.Items.Add("12");
            ddlAge.Items.Add("13");
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

            ddlGender.Items.Add("N/A");
            ddlGender.Items.Add("M");
            ddlGender.Items.Add("F");

            ddlTShirtSize.Items.Add("N/A");
            ddlTShirtSize.Items.Add("S (Child)");
            ddlTShirtSize.Items.Add("M (Child)");
            ddlTShirtSize.Items.Add("L (Child)");
            ddlTShirtSize.Items.Add("XS (Youth)");
            ddlTShirtSize.Items.Add("S (Adult)");
            ddlTShirtSize.Items.Add("M (Adult)");
            ddlTShirtSize.Items.Add("L (Adult)");
            ddlTShirtSize.Items.Add("XL (Adult)");
            ddlTShirtSize.Items.Add("2X (Adult)");
            ddlTShirtSize.Items.Add("3X (Adult)");
            ddlTShirtSize.Items.Add("4X (Adult)");

            ddlVoicePart.Items.Add("N/A");
            ddlVoicePart.Items.Add("Soprano");
            ddlVoicePart.Items.Add("Alto");
            ddlVoicePart.Items.Add("Tenor");
            ddlVoicePart.Items.Add("Bass");

            ddlTextPhone.Items.Add("Text Phone?");
            ddlTextPhone.Items.Add("Yes");
            ddlTextPhone.Items.Add("No");
            ddlTextPhone.Text = "Text Phone?";

            ddlTextGuard1.Items.Add("Text Phone?");
            ddlTextGuard1.Items.Add("Yes");
            ddlTextGuard1.Items.Add("No");
            ddlTextGuard1.Text = "Text Phone?";

            ddlTextGuard2.Items.Add("Text Phone?");
            ddlTextGuard2.Items.Add("Yes");
            ddlTextGuard2.Items.Add("No");
            ddlTextGuard2.Text = "Text Phone?";

            //Ryan C Manners..12/16/11.
            //Customize the tab indexes for Becky per her request...
            //if ((Request.QueryString["LastName"] == "Boll") && (Request.QueryString["FirstName"] == "Becky"))
            //{
                txtFirstName.TabIndex = 1;
                txbMiddleName.TabIndex = 2;
                txtLastName.TabIndex = 3;
                ddlGrade.TabIndex = 4;
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
                ddlSchool.TabIndex = 17;
                ddlChurch.TabIndex = 18;
                ddlTShirtSize.TabIndex = 19;
                txtStudentEmail.TabIndex = 20;
                txtCareerGoal.TabIndex = 21;
                chbDiscipleshipMentor.TabIndex = 22;
                //txbSoloSong.TabIndex = 23;
                txbHealthConditions.TabIndex = 24;
                //ddlVoicePart.TabIndex = 25;
                txbNotes.TabIndex = 26;
                txtParentGuardian1.TabIndex = 27;
                ddlParentGuardian1Relationship.TabIndex = 28;
                txbParentGuardian1WrkPh.TabIndex = 29;
                txbParentGuardian1CellPhone.TabIndex = 30;
                ddlTextGuard1.TabIndex = 31;
                txbParentGuardian1Email.TabIndex = 32;
                txbParentGuardian2.TabIndex = 33;
                ddlParentGuardian2Relationship.TabIndex = 34;
                txbParentGuardian2WrkPh.TabIndex = 35;
                txbParentGuardian2CellPhone.TabIndex = 36;
                ddlTextGuard2.TabIndex = 37;
                txbParentGuardian2Email.TabIndex = 38;
                txbEmergencyRelationship.TabIndex = 39;
                txbEmergRelationship.TabIndex = 40;
                txbEmergencyPhone.TabIndex = 41;
            //}

            ddlVoicePart.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbEmergencyPhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbEmergencyRelationship.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbEmergRelationship.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtParentGuardian1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbParentGuardian1CellPhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbParentGuardian1Email.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlParentGuardian1Relationship.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbParentGuardian1WrkPh.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbParentGuardian2.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbParentGuardian2CellPhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbParentGuardian2Email.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlParentGuardian2Relationship.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbParentGuardian2WrkPh.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbInsuranceCompany.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbAllergies.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            TextBox13.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            TextBox12.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");

            //Create dropdown for ParentGuardian relationship field..RCM..3/8/11
            ddlParentGuardian1Relationship.Items.Add("Father");
            ddlParentGuardian1Relationship.Items.Add("Mother");
            ddlParentGuardian1Relationship.Items.Add("Grandmother");
            ddlParentGuardian1Relationship.Items.Add("Grandfather");
            ddlParentGuardian1Relationship.Items.Add("Step-Mother");
            ddlParentGuardian1Relationship.Items.Add("Step-Father");
            ddlParentGuardian1Relationship.Items.Add("Brother");
            ddlParentGuardian1Relationship.Items.Add("Sister");
            ddlParentGuardian1Relationship.Items.Add("Brother-In-Law");
            ddlParentGuardian1Relationship.Items.Add("Sister-In-Law");
            ddlParentGuardian1Relationship.Items.Add("Guardian");
            ddlParentGuardian1Relationship.Items.Add("Great-Grandmother");
            ddlParentGuardian1Relationship.Items.Add("Great-Grandfather");
            ddlParentGuardian1Relationship.Items.Add("God-Mother");
            ddlParentGuardian1Relationship.Items.Add("God-Father");
            ddlParentGuardian1Relationship.Items.Add("Friend");
            ddlParentGuardian1Relationship.Items.Add("Foster-Father");
            ddlParentGuardian1Relationship.Items.Add("Foster-Mother");
            ddlParentGuardian1Relationship.Items.Add("Foster-GrandFather");
            ddlParentGuardian1Relationship.Items.Add("Foster-Grandmother");
            ddlParentGuardian1Relationship.Items.Add("Aunt");
            ddlParentGuardian1Relationship.Items.Add("Uncle");
            ddlParentGuardian1Relationship.Items.Add("Counselor");
            ddlParentGuardian1Relationship.Items.Add("Cousin");
            ddlParentGuardian1Relationship.Items.Add("N/A");


            //Create dropdown for ParentGuardian relationship field..RCM..3/8/11
            ddlParentGuardian2Relationship.Items.Add("Father");
            ddlParentGuardian2Relationship.Items.Add("Mother");
            ddlParentGuardian2Relationship.Items.Add("Grandmother");
            ddlParentGuardian2Relationship.Items.Add("Grandfather");
            ddlParentGuardian2Relationship.Items.Add("Step-Mother");
            ddlParentGuardian2Relationship.Items.Add("Step-Father");
            ddlParentGuardian2Relationship.Items.Add("Brother");
            ddlParentGuardian2Relationship.Items.Add("Sister");
            ddlParentGuardian2Relationship.Items.Add("Sister-In-Law");
            ddlParentGuardian2Relationship.Items.Add("Brother-In-Law");
            ddlParentGuardian2Relationship.Items.Add("Guardian");
            ddlParentGuardian2Relationship.Items.Add("Great-Grandmother");
            ddlParentGuardian2Relationship.Items.Add("Great-Grandfather");
            ddlParentGuardian2Relationship.Items.Add("God-Mother");
            ddlParentGuardian2Relationship.Items.Add("God-Father");
            ddlParentGuardian2Relationship.Items.Add("Friend");
            ddlParentGuardian2Relationship.Items.Add("Foster-Father");
            ddlParentGuardian2Relationship.Items.Add("Foster-Mother");
            ddlParentGuardian2Relationship.Items.Add("Foster-GrandFather");
            ddlParentGuardian2Relationship.Items.Add("Foster-Grandmother");
            ddlParentGuardian2Relationship.Items.Add("Aunt");
            ddlParentGuardian2Relationship.Items.Add("Uncle");
            ddlParentGuardian2Relationship.Items.Add("Counselor");
            ddlParentGuardian2Relationship.Items.Add("Cousin");
            ddlParentGuardian2Relationship.Items.Add("N/A");
            
            txtLastName.Enabled = true;
            txtFirstName.Enabled = true;
            txtAddress1.Enabled = true;
            txtCity.Enabled = true;
            txtState.Enabled = true;
            txtZip.Enabled = true;
            txtHomePhone.Enabled = true;
            txtStudentCellPhone.Enabled = true;
            txtStudentEmail.Enabled = true;
            ddlSchool.Enabled = true;
            ddlGrade.Enabled = true;
            //txtGrade.Enabled = true;
            ddlAge.Enabled = true;
            //txtAge.Enabled = true;
            ddlGender.Enabled = true;
            ddlChurch.Enabled = true;
            txtCareerGoal.Enabled = true;
            ddlTShirtSize.Enabled = true;
            txbDiscipleshipMentor.Enabled = true;
            txbHealthConditions.Enabled = true;
            txbNotes.Enabled = true;
            txbSoloSong.Enabled = true;


            txtLastName.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtFirstName.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtAddress1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtCity.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtState.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtZip.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtHomePhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtStudentCellPhone.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtStudentEmail.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlSchool.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlGrade.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlAge.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            //txtDateBirth.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlGender.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlChurch.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txtCareerGoal.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            ddlTShirtSize.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbDiscipleshipMentor.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbHealthConditions.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbNotes.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");
            txbSoloSong.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD200");

            lblHealthConditions.Font.Size = 8;
            lblVoicePart.Font.Size = 8;
            //Label.Font.Size = 8;
            Label1.Font.Size = 8;
            Label3.Font.Size = 8;
            Label4.Font.Size = 8;
            Label5.Font.Size = 8;
            Label6.Font.Size = 8;
            Label2.Font.Size = 8;
            Label10.Font.Size = 8;
            Label7.Font.Size = 8;
            Label18.Font.Size = 8;
            Label19.Font.Size = 8;
            Label20.Font.Size = 8;
            Label21.Font.Size = 8;
            Label24.Font.Size = 8;
            Label25.Font.Size = 8;
            Label26.Font.Size = 8;
            Label28.Font.Size = 8;
            Label29.Font.Size = 8;
            Label30.Font.Size = 8;
            Label4.Font.Size = 8;
            Label8.Font.Size = 8;
            Label9.Font.Size = 8;
            lblMiddleName.Font.Size = 8;


            txtLastName.Font.Size = 10;
            txtFirstName.Font.Size = 10;
            txtAddress1.Font.Size = 10;
            ddlSchool.Font.Size = 10;
            txtState.Font.Size = 10;
            txtStudentEmail.Font.Size = 10;
            ddlTShirtSize.Font.Size = 10;
            txtZip.Font.Size = 10;
            txtHomePhone.Font.Size = 10;
            ddlGrade.Font.Size = 10;
            //txtGrade.Font.Size = 10;
            ddlAge.Font.Size = 10;
            ddlGender.Font.Size = 10;
            ddlChurch.Font.Size = 10;
            txtStudentCellPhone.Font.Size = 10;
            txtCareerGoal.Font.Size = 10;
            txbSoloSong.Font.Size = 10;

            chbAddress.Font.Size = 8;
            chbAge.Font.Size = 8;
            //chbBibleOwnership.Font.Size = 8;
            chbAge.Font.Size = 8;
            chbCity.Font.Size = 8;
            chbDateBirth.Font.Size = 8;
            //chbGender.Font.Size = 8;
            chbHomePhone.Font.Size = 8;
            chbTShirtSize.Font.Size = 8;
            chbSchool.Font.Size = 8;
            //chbGender.Font.Size = 8;
            chbStudentEmail.Font.Size = 8;
            chbStudentCellPhone.Font.Size = 8;
            chbFirstName.Font.Size = 8;
            chbLastName.Font.Size = 8;

            chbAddress.Checked = true;
            chbAge.Checked = true;
            chbBibleOwnership.Checked = true;
            chbAge.Checked = true;
            chbCity.Checked = true;
            chbDateBirth.Checked = true;
            //chbGender.Checked = true;
            chbHomePhone.Checked = true;
            chbTShirtSize.Checked = true;
            chbSchool.Checked = true;
            //chbGender.Checked = true;
            chbStudentEmail.Checked = true;
            chbStudentCellPhone.Checked = true;
            chbLastName.Checked = true;
            chbFirstName.Checked = true;

            chbAddress.Visible = false;
            chbAge.Visible = false;
            chbBibleOwnership.Visible = false;
            chbAge.Visible = false;
            chbCity.Visible = false;
            chbDateBirth.Visible = false;
            //chbGender.Visible = false;
            chbHomePhone.Visible = false;
            chbTShirtSize.Visible = false;
            chbSchool.Visible = false;
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
            chbBibleOwnership.Height = 9;
            chbAge.Height = 9;
            chbCity.Height = 9;
            chbDateBirth.Height = 9;
            //chbGender.Height = 9;
            chbHomePhone.Height = 9;
            chbTShirtSize.Height = 9;
            chbSchool.Height = 9;
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
            chbSchool.Text = "(Edit)";
            chbStudentEmail.Text = "(Edit)";
            chbStudentCellPhone.Text = "(Edit)";
            chbLastName.Text = "(Edit)";
            chbFirstName.Text = "(Edit)";

            //Ryan C Manners..10/15/10.
            //Disable certain functionality based on login of person (staff member)
            if (Department == "Athletics")
            {
                chbMSHSChoir.Enabled = false;
                chbChildrensChoir.Enabled = false;
                chbPerformingArts.Enabled = false;
                chbShakes.Enabled = false;
                chbSingers.Enabled = false;
                chbImpactUrbanSchoolsPA.Enabled = false;
                lbMSHSChoir.Enabled = false;
                lbChildrensChoir.Enabled = false;
                lbPerformingArtsAcademy.Enabled = false;
                lbSingers.Enabled = false;
                lbShakes.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;
                chbReadingSupport.Enabled = false;
                lbAcademicReadingSupport.Enabled = false;

                //lbClassesEnrollment.Enabled = false;
                lbClassesEnrollment.Visible = false;

                ddlVoicePart.Enabled = false;
                txbSoloSong.Enabled = false;

                //Education department fields..
                chbSummerDayCamp.Enabled = false;
                chbSATPrepClass.Enabled = false;
                chbCAMPDropOff.Enabled = false;
                chbCAMPPickUp.Enabled = false;
                txbDropOffPickUp.Enabled = false;
                lblDropOffPickUp.Enabled = false;
                chbImpactUrbanSchoolsAcademics.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;
                lbSummerDayCamp.Enabled = false;

                //Hide these from Athletics viewing...RCM..
                chbSoloist.Visible = false;
                chbMeetCCGF.Visible = false;
                txbSoloSong.Visible = false;
                lblVoicePart.Visible = false;
                ddlVoicePart.Visible = false;
                Label30.Visible = false;
                chbDance.Visible = false;

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
                chbImpactUrbanSchools.Enabled = false;
                lbImpactUrbanSchools.Enabled = false;
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
                chbReadingSupport.Enabled = false;
                lbAcademicReadingSupport.Enabled = false;
                lbSummerDayCamp.Enabled = false;
                //Handle the initial settings for the link buttons for Athletics
                //Programs..RCM..8/14/12.


                if (chbPerformingArts.Checked)
                {
                    lbClassesEnrollment.Enabled = true;
                }

                //Education department fields..
                chbImpactUrbanSchoolsAcademics.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;
                chbSummerDayCamp.Enabled = false;
                chbSATPrepClass.Enabled = false;
                chbCAMPDropOff.Enabled = false;
                chbCAMPPickUp.Enabled = false;
                txbDropOffPickUp.Enabled = false;
                lblDropOffPickUp.Enabled = false;
                
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
                //Education department fields..
                chbSummerDayCamp.Enabled = true;
                chbSATPrepClass.Enabled = true;
                chbCAMPDropOff.Enabled = true;
                chbCAMPPickUp.Enabled = true;
                txbDropOffPickUp.Enabled = true;
                lblDropOffPickUp.Enabled = true;
                chbImpactUrbanSchoolsAcademics.Enabled = true;
                lbImpactUrbanSchoolsAcademics.Enabled = true;
                lbSummerDayCamp.Enabled = true;
                //txbDropOffPickUp.Enabled = true;

                ////PerformingArts fields.
                chbMSHSChoir.Enabled = false;
                chbChildrensChoir.Enabled = false;
                chbPerformingArts.Enabled = false;
                chbShakes.Enabled = false;
                chbSingers.Enabled = false;
                chbImpactUrbanSchoolsPA.Enabled = false;
                lbMSHSChoir.Enabled = false;
                lbChildrensChoir.Enabled = false;
                lbPerformingArtsAcademy.Enabled = false;
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
                chbImpactUrbanSchools.Enabled = false;
                lbImpactUrbanSchools.Enabled = false;
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

                lbClassesEnrollment.Visible = false;
                lbClassesEnrollment.Enabled = false;
            }
            else if (Department == "BusinessOffice")
            {
                chbMSHSChoir.Enabled = false;
                chbChildrensChoir.Enabled = false;
                chbPerformingArts.Enabled = false;
                chbShakes.Enabled = false;
                chbSingers.Enabled = false;
                chbImpactUrbanSchoolsPA.Enabled = false;
                lbMSHSChoir.Enabled = false;
                lbChildrensChoir.Enabled = false;
                lbPerformingArtsAcademy.Enabled = false;
                lbSingers.Enabled = false;
                lbShakes.Enabled = false;
                lbImpactUrbanSchoolsPA.Enabled = false;


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
                chbImpactUrbanSchools.Enabled = false;
                lbImpactUrbanSchools.Enabled = false;
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
                chbReadingSupport.Enabled = false;
                lbAcademicReadingSupport.Enabled = false;
                lbSummerDayCamp.Enabled = false;

                //Education department fields..
                chbSummerDayCamp.Enabled = false;
                chbSATPrepClass.Enabled = false;
                chbCAMPDropOff.Enabled = false;
                chbCAMPPickUp.Enabled = false;
                chbImpactUrbanSchoolsAcademics.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;

                lbClassesEnrollment.Visible = false;
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
                chbImpactUrbanSchoolsPA.Enabled = false;
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
                chbImpactUrbanSchools.Enabled = false;
                lbImpactUrbanSchools.Enabled = false;
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
                chbReadingSupport.Enabled = false;
                lbAcademicReadingSupport.Enabled = false;
                lbSummerDayCamp.Enabled = false;

                //Education department fields..
                chbSummerDayCamp.Enabled = false;
                chbSATPrepClass.Enabled = false;
                chbCAMPDropOff.Enabled = false;
                chbCAMPPickUp.Enabled = false;
                chbImpactUrbanSchoolsAcademics.Enabled = false;
                lbImpactUrbanSchoolsAcademics.Enabled = false;

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
                //chbHSBasketLeague.Attributes.Add("onclick", "return false;");                .
                lbClassesEnrollment.Enabled = false;
                lbClassesEnrollment.Visible = false;
            }
            //chbSATPrepClass.Attributes.Add("onclick", "return false;");
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


        protected void PopulateSchoolsDropdown()
        {
            try
            {
                con.Open();

                string selectSQL = "";

                selectSQL = "select school " +
                            "from UIF_PerformingArts.dbo.SchoolNames " +
                            "group by school " +
                            "order by school ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	
                    ddlSchool.Items.Add("Select a school");
                    do
                    {
                        ddlSchool.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    ddlSchool.Text = "Select a school";
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


        protected void CleanCharacters()
        {
            //Strips out all apostrophes from all fields...RCM..3/21/12.

            //Student related fields..takes out apostrophes..
            txtLastName.Text = txtLastName.Text.Replace("'", "");
            txbMiddleName.Text = txbMiddleName.Text.Replace("'", "");
            txtFirstName.Text = txtFirstName.Text.Replace("'", "");
            //Takes out commas..
            txtLastName.Text = txtLastName.Text.Replace(",", "");
            txbMiddleName.Text = txbMiddleName.Text.Replace(",", "");
            txtFirstName.Text = txtFirstName.Text.Replace(",", "");
            //Takes out (
            txtLastName.Text = txtLastName.Text.Replace("(", "");
            txbMiddleName.Text = txbMiddleName.Text.Replace("(", "");
            txtFirstName.Text = txtFirstName.Text.Replace("(", "");
            //Takes out )
            txtLastName.Text = txtLastName.Text.Replace(")", "");
            txbMiddleName.Text = txbMiddleName.Text.Replace(")", "");
            txtFirstName.Text = txtFirstName.Text.Replace(")", "");

            txtAddress1.Text = txtAddress1.Text.Replace("'", "");
            txtCity.Text = txtCity.Text.Replace("'", "");
            txtState.Text = txtState.Text.Replace("'", "");
            txtZip.Text = txtZip.Text.Replace("'", "");
            txtStudentEmail.Text = txtStudentEmail.Text.Replace("'", "");
            ddlChurch.Text = ddlChurch.Text.Replace("'", "");
            txbSoloSong.Text = txbSoloSong.Text.Replace("'", "");
            txbNotes.Text = txbNotes.Text.Replace("'", "");
            txbHealthConditions.Text = txbHealthConditions.Text.Replace("'", "");
            txtCareerGoal.Text = txtCareerGoal.Text.Replace("'", "");
            txtStudentCellPhone.Text = txtStudentCellPhone.Text.Replace("'", "");
            txtHomePhone.Text = txtHomePhone.Text.Replace("'", "");

            //Parent related fields..
            txtParentGuardian1.Text = txtParentGuardian1.Text.Replace("'", "");
            txbParentGuardian2.Text = txbParentGuardian2.Text.Replace("'", "");
            txbEmergencyRelationship.Text = txbEmergencyRelationship.Text.Replace("'", "");
            txbEmergRelationship.Text = txbEmergRelationship.Text.Replace("'", "");
            txbEmergencyPhone.Text = txbEmergencyPhone.Text.Replace("'", "");
            txbParentGuardian1CellPhone.Text = txbParentGuardian1CellPhone.Text.Replace("'", "");
            txbParentGuardian1Email.Text = txbParentGuardian1Email.Text.Replace("'", "");
            txbParentGuardian1WrkPh.Text = txbParentGuardian1WrkPh.Text.Replace("'", "");
            txbParentGuardian2CellPhone.Text = txbParentGuardian2CellPhone.Text.Replace("'", "");
            txbParentGuardian2Email.Text = txbParentGuardian2Email.Text.Replace("'", "");
            txbParentGuardian2WrkPh.Text = txbParentGuardian2WrkPh.Text.Replace("'", "");
        }



        protected void UpdateAllTables()
        {
            try
            {
                //con.Open();
                //BasketballTEAMS
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "BasketballTEAMSEnrollment";
                //    if ((Request.QueryString["StudentLastName"] == "") || (Request.QueryString["StudentFirstName"] == ""))
                //    //if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////SoccerTEAMS
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "SoccerTEAMSEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////SoccerIntraMurals
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "SoccerIntraMuralsEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////Baseball
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "BaseballEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////MSBasketballLeague
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "MSBasketballLeagueEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////HSBasketballLeague
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "HSBasketballLeagueEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////OutreachBasketballLeague
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "OutreachBasketballEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////BibleStudy
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "BibleStudyEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////MondayNights
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "MondayNightsEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////3on3Basketball
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "[3on3BasketballEnrollment]";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////SpecialEvents
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "SpecialEventsEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////MSHSChoir
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "MSHSChoirEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}


                ////ChildrensChoir
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "ChildrensChoirEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////Singers
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "SingersEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////Shakes
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "ShakesEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}


                ////ImpactUrbanSchools
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "ImpactUrbanSchoolsEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////SummerDayCamp
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "SummerDayCampEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////SATPrep
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "SATPrepEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////PerformingArtsAcademy
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "PerformingArtsAcademyClassEnrollment";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                //        + " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + " AND student = 1";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "',  "
                //        + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' "
                //        + " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                //        + " AND student = 1";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////DiscipleshipMentorProgram
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "DiscipleshipMentorProgram";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' ";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' ";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////DiscipleshipMentorDescription
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "DiscipleshipMentorDescription";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' ";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' ";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                ////DiscipleshipMentorAssignedMentors
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "DiscipleshipMentorAssignedMentors";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                //        + "  "
                //        + " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' ";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " studentfirstname = '" + txtFirstName.Text.Trim() + "'  "
                //        + "  "
                //        + " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' ";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}

                //Ryan C Manners...3/13/16.
                //All other tables updated with a trigger.. This table would have multiple
                //records for a student though..  so it's done via code.. here....
                            
                //-------------------------------------------------------------
                //AttendanceHistory...
                //StudentProgramAttendance....???   Should or not?...RCM...
                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "StudentProgramAttendance";
                    if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "', "
                        + " firstname = '" + txtFirstName.Text.Trim() + "',  "
                        + " middlename = '" + txbMiddleName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND firstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "', "
                        + " firstname = '" + txtFirstName.Text.Trim() + "',  "
                        + " middlename = '" + txbMiddleName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                        + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                        + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
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
                    lblErrorMessage.Enabled = true;
                    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }
                //------------------------------------------------------------------------------------------------------------


                //-----------------------------------------------------------------------------------------------------------
                ////CoreKids....???   Should or not?...RCM...
                //try
                //{
                //    string sqlUpdateStatement = "";
                //    string tablename = "CoreKidsProgram";
                //    if ((Request.QueryString["newstudent"] == "newstudent"))
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " lastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " firstname = '" + txtFirstName.Text.Trim() + "'  "
                //        //+ " middlename = '" + txbMiddleName.Text.Trim() + "'  "
                //        + "  "
                //        + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                //        + " AND firstname = '" + txtFirstName.Text.Trim() + "' ";
                //        //+ " AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
                //    }
                //    else
                //    {
                //        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                //        + "SET "
                //        + " lastname = '" + txtLastName.Text.Trim() + "' , "
                //        + " firstname = '" + txtFirstName.Text.Trim() + "'  "
                //        //+ " middlename = '" + txbMiddleName.Text.Trim() + "'  "
                //        + "  "
                //        + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                //        + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' ";
                //        //+ " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
                //    }
                //    //create a SQL command to update record
                //    SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //    if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                //    {
                //    }
                //    else
                //    {
                //        //Didn't find a record to update..RCM..
                //    }
                //}
                //catch (Exception lkjaaa)
                //{
                //    lblErrorMessage.Enabled = true;
                //    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                //    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                //}


                try
                {
                    string sqlUpdateStatement = "";
                    string tablename = "CoreKidsDetail";
                    if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "' , "
                        + " firstname = '" + txtFirstName.Text.Trim() + "'  "
                            //+ " middlename = '" + txbMiddleName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND firstname = '" + txtFirstName.Text.Trim() + "' ";
                        //+ " AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
                    }
                    else
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo." + tablename + " "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "' , "
                        + " firstname = '" + txtFirstName.Text.Trim() + "'  "
                            //+ " middlename = '" + txbMiddleName.Text.Trim() + "'  "
                        + "  "
                        + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                        + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' ";
                        //+ " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
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
                    lblErrorMessage.Enabled = true;
                    lblErrorMessage.Text = "An error occurred during the update: " + lkjaaa.Message.ToString();
                    throw new Exception("An error occurred during the update: " + lkjaaa.Message.ToString());
                }
                //------------------------------------------------------


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

        protected Boolean UpdateScrubbedField()
        {
            try
            {
                string sqlUpdateStatement = "";

                if ((Request.QueryString["newstudent"] == "newstudent"))
                {
                    sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.StudentInformation "
                    + "SET "
                    + " Scrubbed =  " + Convert.ToInt32(chbScrubbed.Checked) + ", "
                    + " ScrubbedDate = '" + System.DateTime.Now.ToString() + "', "
                    + " LastScrubbedBy = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                    + "  "
                    + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                    + " AND firstname = '" + txtFirstName.Text.Trim() + "' "
                    + " AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
                }
                else
                {
                    sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.StudentInformation "
                    + "SET "
                    + " Scrubbed =  " + Convert.ToInt32(chbScrubbed.Checked) + ", "
                    + " ScrubbedDate = '" + System.DateTime.Now.ToString() + "', "
                    + " LastScrubbedBy = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "' "
                    + "  "
                    + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                    + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                    + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
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
            }
            catch (Exception lkjlkaabbdc)
            {

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return true;
        }

        protected Boolean UpdateStudentTable()
        {
            lblInformation.Enabled = false;
            lblInformation.Text = "";
            try
            {
                try
                {
                    string sqlUpdateStatement = "";

                    CleanCharacters();

                    if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.StudentInformation "
                        + "SET "
                        + " lastname = '" + txtLastName.Text.Trim() + "', "
                        + " firstname = '" + txtFirstName.Text.Trim() + "', "
                        + " middlename = '" + txbMiddleName.Text.Trim() + "', "
                        + " address = '" + txtAddress1.Text.Trim() + "', "
                        + " city = '" + txtCity.Text.Trim() + "', "
                        + " state = '" + txtState.Text.Trim() + "', "
                        + " zip = '" + txtZip.Text.Trim() + "', "
                        + " homephone = '" + txtHomePhone.Text.Trim() + "', "
                        + " studentcellphone = '" + txtStudentCellPhone.Text.Trim() + "', "
                            //--------------------------------------------------------------------------
                        + " textphone = " + ConvertYesNoToInt(ddlTextPhone.Text) + ", "
                            //----------------------------------------------------------------------
                        + " studentemail = '" + txtStudentEmail.Text.Trim() + "', "
                        + " school = '" + ddlSchool.Text.Trim() + "', "
                        + " grade = '" + ddlGrade.Text.Trim() + "', "
                        + " age = '" + ddlAge.Text.Trim() + "', "
                        + " dob = '" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "' , "
                        + " sex = '" + ddlGender.Text.Trim() + "', "
                        + " church = '" + ddlChurch.Text.Trim() + "', "
                        + " careergoal = '" + ddlCareerGoal.Text.Trim() + "', "
                        + " healthconditions = '" + txbHealthConditions.Text.Trim() + "' , "
                        + " notes = '" + txbNotes.Text.Trim() + "' , "
                        + " tshirtsize = '" + ddlTShirtSize.Text.Trim() + "' , "
                        + " meetccgf = " + Convert.ToInt32(chbMeetCCGF.Checked) + ","
                        + " schoolform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ","
                        + " descipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "',"
                        + " soloist = " + Convert.ToInt32(chbSoloist.Checked) + ","
                        + " solosong = '" + txbSoloSong.Text.Trim() + "',"
                        + " dance = " + Convert.ToInt32(chbDance.Checked) + ","
                            //+ " danceyear = " + "'1900-01-01'" + ","//danceyear
                            //+ " schoolform2 = " + "'" + txtCareerGoal.Text.Trim() + "',"//schoolform2
                            //+ " bibleownership = " + Convert.ToInt32(chbBibleOwnership.Checked) + ","
                        + " biblestudyparticipation = " + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ","
                        + " havereceivedchrist = " + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ", "
                            //+ " whenreceivedchrist = '1900-01-01'" + ", "//when received christ.
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                        + " currentregistrationform = " + Convert.ToInt32(chbRegistrationForm.Checked) + ","
                        + " parentalconsentform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ","
                        + " retreatconsentform = " + Convert.ToInt32(chbRetreatForm.Checked) + ","
                        + " studentchoirquestionareform = " + Convert.ToInt32(chbStudentQuestionareForm.Checked) + ","
                            //--------new fields...3/21/12.-------------------------------------------------------
                        + " promotionalrelease = " + Convert.ToInt32(chbPromotionalRelease.Checked) + ","
                        + " permissiontotransport = " + Convert.ToInt32(chbPermissionTransport.Checked) + ","
                        + " campdropoff = " + Convert.ToInt32(chbCAMPDropOff.Checked) + ", "
                        + " camppickup = " + Convert.ToInt32(chbCAMPPickUp.Checked) + ", "
                        + " campcomments = '" + txbDropOffPickUp.Text.Trim() + "', "
                        + " includepromotionalmailing = " + Convert.ToInt32(chbIncludePromotionalMailing.Checked) + ", "
                            //------------------------------------------------------------------------------------                        
                        + " studentvolunteer = " + Convert.ToInt32(chbStudentVolunteer.Checked) + ", "
                        + " mailinglistinclude = " + Convert.ToInt32(chbMailingList.Checked) + ", "
                        + " mailinglistcodes = '" + ddlMailingListCodes.Text.Trim() + "', "
                        //------------------------------------------------------------------------------------------
                        //+ " Scrubbed =  " + Convert.ToInt32(chbScrubbed.Checked) + ", "
                        //+ " ScrubbedDate = '" + System.DateTime.Now.ToString() + "', "
                        //+ " LastScrubbedBy = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                        //----------------------------------------------------------------------------------------------
                        //////+ " ID = " + Convert.ToInt32(txbID.Text.Trim()) + ", "
                        + " voicepart = '" + ddlVoicePart.Text.Trim() + "', "
                        + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                        + " discipleshipmentorprogram = " + Convert.ToInt32(chbDiscipleshipMentor.Checked) + " "
                        + "  "
                        + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND firstname = '" + txtFirstName.Text.Trim() + "' "
                        + " AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
                    }
                    else
                    {
                        //if lastname and firstname contain data, then update them using QueryString parameters...RCM.
                        if (txtLastName.Text.Trim() != "" && txtFirstName.Text.Trim() != "")
                        {
                            sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.StudentInformation "
                            + "SET "
                            + " lastname = '" + txtLastName.Text.Trim() + "', "
                            + " firstname = '" + txtFirstName.Text.Trim() + "', "
                            + " middlename = '" + txbMiddleName.Text.Trim() + "', "
                            + " address = '" + txtAddress1.Text.Trim() + "', "
                            + " city = '" + txtCity.Text.Trim() + "' , "
                            + " state = '" + txtState.Text.Trim() + "' , "
                            + " zip = '" + txtZip.Text.Trim() + "', "
                            + " homephone = '" + txtHomePhone.Text.Trim() + "', "
                            + " studentcellphone = '" + txtStudentCellPhone.Text.Trim() + "', "
                                //--------------------------------------------------------------------------
                            + " textphone = " + ConvertYesNoToInt(ddlTextPhone.Text) + ", "
                                //----------------------------------------------------------------------
                            + " studentemail = '" + txtStudentEmail.Text.Trim() + "', "
                            + " school = '" + ddlSchool.Text.Trim() + "', "
                            + " grade = '" + ddlGrade.Text.Trim() + "', "
                            + " age = '" + ddlAge.Text.Trim() + "', "
                            + " dob = '" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "' , "
                            + " sex = '" + ddlGender.Text.Trim() + "', "
                            + " church = '" + ddlChurch.Text.Trim() + "', "
                            + " careergoal = '" + ddlCareerGoal.Text.Trim() + "', "
                            + " healthconditions = '" + txbHealthConditions.Text.Trim() + "', "
                            + " notes = '" + txbNotes.Text.Trim() + "', "
                            + " tshirtsize = '" + ddlTShirtSize.Text.Trim() + "', "
                            + " meetccgf = " + Convert.ToInt32(chbMeetCCGF.Checked) + ", "
                            + " schoolform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ", "
                            + " descipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "', "
                            + " soloist = " + Convert.ToInt32(chbSoloist.Checked) + ", "
                            + " solosong = '" + txbSoloSong.Text.Trim() + "', "
                            + " dance = " + Convert.ToInt32(chbDance.Checked) + ", "
                                //+ " danceyear = " + "'1900-01-01'" + ","//danceyear
                            + " schoolform2 = " + "'" + txtCareerGoal.Text.Trim() + "', "//schoolform2
                            + " bibleownership = " + Convert.ToInt32(chbBibleOwnership.Checked) + ", "
                            + " biblestudyparticipation = " + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ", "
                            + " havereceivedchrist = " + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ", "
                            + " whenreceivedchrist = '1900-01-01'" + ", "//when received christ.
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                            + " currentregistrationform = " + Convert.ToInt32(chbRegistrationForm.Checked) + ", "
                            + " parentalconsentform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ", "
                            + " retreatconsentform = " + Convert.ToInt32(chbRetreatForm.Checked) + ", "
                            + " studentchoirquestionareform = " + Convert.ToInt32(chbStudentQuestionareForm.Checked) + ", "
                                //--------new fields...3/21/12.-------------------------------------------------------
                            + " promotionalrelease = " + Convert.ToInt32(chbPromotionalRelease.Checked) + ", "
                            + " permissiontotransport = " + Convert.ToInt32(chbPermissionTransport.Checked) + ", "
                            + " campdropoff = " + Convert.ToInt32(chbCAMPDropOff.Checked) + ", "
                            + " camppickup = " + Convert.ToInt32(chbCAMPPickUp.Checked) + ", "
                            + " campcomments = '" + txbDropOffPickUp.Text.Trim() + "', "
                            + " includepromotionalmailing = " + Convert.ToInt32(chbIncludePromotionalMailing.Checked) + ", "
                            //------------------------------------------------------------------------------------------
                            //+ " Scrubbed =  " + Convert.ToInt32(chbScrubbed.Checked) + ", "
                            //+ " ScrubbedDate = '" + System.DateTime.Now.ToString() + "', "
                            //+ " LastScrubbedBy = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            //----------------------------------------------------------------------------------------------
                            //------------------------------------------------------------------------------------                        
                            + " studentvolunteer = " + Convert.ToInt32(chbStudentVolunteer.Checked) + ", "
                            + " mailinglistinclude = " + Convert.ToInt32(chbMailingList.Checked) + ", "
                            + " mailinglistcodes = '" + ddlMailingListCodes.Text.Trim() + "', "
                            + " ID = " + Convert.ToInt32(txbID.Text.Trim()) + ", "
                            + " voicepart = '" + ddlVoicePart.Text.Trim() + "', "
                            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            + " discipleshipmentorprogram = " + Convert.ToInt32(chbDiscipleshipMentor.Checked) + " "
                            + "  "
                            + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                            + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                            + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
                        }
                        else
                        {
                            sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.StudentInformation "
                            + "SET "
                            //+ " lastname = '" + txtLastName.Text.Trim() + "', "
                            //+ " firstname = '" + txtFirstName.Text.Trim() + "', "
                            //+ " middlename = '" + txbMiddleName.Text.Trim() + "', "
                            + " address = '" + txtAddress1.Text.Trim() + "', "
                            + " city = '" + txtCity.Text.Trim() + "' , "
                            + " state = '" + txtState.Text.Trim() + "' , "
                            + " zip = '" + txtZip.Text.Trim() + "', "
                            + " homephone = '" + txtHomePhone.Text.Trim() + "', "
                            + " studentcellphone = '" + txtStudentCellPhone.Text.Trim() + "', "
                                //--------------------------------------------------------------------------
                            + " textphone = " + ConvertYesNoToInt(ddlTextPhone.Text) + ", "
                                //----------------------------------------------------------------------
                            + " studentemail = '" + txtStudentEmail.Text.Trim() + "', "
                            + " school = '" + ddlSchool.Text.Trim() + "', "
                            + " grade = '" + ddlGrade.Text.Trim() + "', "
                            + " age = '" + ddlAge.Text.Trim() + "', "
                            + " dob = '" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "' , "
                            + " sex = '" + ddlGender.Text.Trim() + "', "
                            + " church = '" + ddlChurch.Text.Trim() + "', "
                            + " careergoal = '" + ddlCareerGoal.Text.Trim() + "', "
                            + " healthconditions = '" + txbHealthConditions.Text.Trim() + "', "
                            + " notes = '" + txbNotes.Text.Trim() + "', "
                            + " tshirtsize = '" + ddlTShirtSize.Text.Trim() + "', "
                            + " meetccgf = " + Convert.ToInt32(chbMeetCCGF.Checked) + ", "
                            + " schoolform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ", "
                            + " descipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "', "
                            + " soloist = " + Convert.ToInt32(chbSoloist.Checked) + ", "
                            + " solosong = '" + txbSoloSong.Text.Trim() + "', "
                            + " dance = " + Convert.ToInt32(chbDance.Checked) + ", "
                            //+ " danceyear = " + "'1900-01-01'" + ","//danceyear
                            + " schoolform2 = " + "'" + txtCareerGoal.Text.Trim() + "', "//schoolform2
                            + " bibleownership = " + Convert.ToInt32(chbBibleOwnership.Checked) + ", "
                            + " biblestudyparticipation = " + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ", "
                            + " havereceivedchrist = " + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ", "
                            + " whenreceivedchrist = '1900-01-01'" + ", "//when received christ.
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                            + " currentregistrationform = " + Convert.ToInt32(chbRegistrationForm.Checked) + ", "
                            + " parentalconsentform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ", "
                            + " retreatconsentform = " + Convert.ToInt32(chbRetreatForm.Checked) + ", "
                            + " studentchoirquestionareform = " + Convert.ToInt32(chbStudentQuestionareForm.Checked) + ", "
                            //--------new fields...3/21/12.-------------------------------------------------------
                            + " promotionalrelease = " + Convert.ToInt32(chbPromotionalRelease.Checked) + ", "
                            + " permissiontotransport = " + Convert.ToInt32(chbPermissionTransport.Checked) + ", "
                            + " campdropoff = " + Convert.ToInt32(chbCAMPDropOff.Checked) + ", "
                            + " camppickup = " + Convert.ToInt32(chbCAMPPickUp.Checked) + ", "
                            + " campcomments = '" + txbDropOffPickUp.Text.Trim() + "', "
                            + " includepromotionalmailing = " + Convert.ToInt32(chbIncludePromotionalMailing.Checked) + ", "
                            //------------------------------------------------------------------------------------------
                            //+ " Scrubbed =  " + Convert.ToInt32(chbScrubbed.Checked) + ", "
                            //+ " ScrubbedDate = '" + System.DateTime.Now.ToString() + "', "
                            //+ " LastScrubbedBy = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            //----------------------------------------------------------------------------------------------
                            //------------------------------------------------------------------------------------                        
                            + " studentvolunteer = " + Convert.ToInt32(chbStudentVolunteer.Checked) + ", "
                            + " mailinglistinclude = " + Convert.ToInt32(chbMailingList.Checked) + ", "
                            + " mailinglistcodes = '" + ddlMailingListCodes.Text.Trim() + "', "
                            + " ID = " + Convert.ToInt32(txbID.Text.Trim()) + ", "
                            + " voicepart = '" + ddlVoicePart.Text.Trim() + "', "
                            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            + " discipleshipmentorprogram = " + Convert.ToInt32(chbDiscipleshipMentor.Checked) + " "
                            + "  "
                            + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                            + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                            + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
                        }
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
                }
                catch (Exception lkjaaa)
                {
                    lblErrorMessage.Enabled = true;
                    lblErrorMessage.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
                    throw new Exception("The Update to the Student table failed.   Please fix and try again MSG: " + lkjaaa.Message.ToString());
                }

                try
                {
                    //Update the ParentGuardian information...RCM..10/7/10.
                    string sqlUpdateStatement_ParentGuardian = "";

                    //if ((Request.QueryString["StudentLastName"] == "") || (Request.QueryString["StudentFirstName"] == ""))
                    if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement_ParentGuardian = " UPDATE UIF_PerformingArts.dbo.ParentGuardianContactInformation " +
                            "SET "
                            + " studentlastname = '" + txtLastName.Text.Trim() + "', "
                            + " studentfirstname = '" + txtFirstName.Text.Trim() + "', "
                            + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "', "
                            + " parentguardianrelationship1 = '" + ddlParentGuardian1Relationship.Text.Trim() + "' , "
                            + " parentguardian1 = '" + txtParentGuardian1.Text.Trim() + "' , "
                            + " parentguardian1email = '" + txbParentGuardian1Email.Text.Trim() + "' , "
                            + " workphone1 = '" + txbParentGuardian1WrkPh.Text.Trim() + "' , "
                            + " cellphone1 = '" + txbParentGuardian1CellPhone.Text.Trim() + "' , "
                            //-------------------------------------------------------------------------------------------
                            + " textphone1 = " + ConvertYesNoToInt(ddlTextGuard1.Text) + ", "
                            //------------------------------------------------------------------------------------------
                            + " parentguardian2relationship  = '" + ddlParentGuardian2Relationship.Text.Trim() + "', "
                            + " parentguardian2 = '" + txbParentGuardian2.Text.Trim() + "' , "
                            + " workphone2 = '" + txbParentGuardian2WrkPh.Text.Trim() + "',"
                            + " cellphone2 = '" + txbParentGuardian2CellPhone.Text.Trim() + "',"
                            //-------------------------------------------------------------------------------------------
                            + " textphone2 = " + ConvertYesNoToInt(ddlTextGuard2.Text) + ", "
                            //------------------------------------------------------------------------------------------
                            + " emergencycontact = '" + txbEmergencyRelationship.Text.Trim() + "' , "
                            + " emergrelationship = '" + txbEmergRelationship.Text.Trim() + "' , "
                            + " emergencycontactphone = '" + txbEmergencyPhone.Text.Trim() + "' , "
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                            + " lastfirstnamekey = '" + txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim() + " (" + txbMiddleName.Text.Trim() + ")' "
                            + " " +
                            " WHERE studentlastname = '" + txtLastName.Text.Trim() + "' " +
                            " AND studentfirstname = '" + txtFirstName.Text.Trim() + "' " +
                            " AND studentmiddlename = '" + txbMiddleName.Text.Trim() + "' "; 
                    }
                    else
                    {
                        //if lastname and firstname contain data, then update them using QueryString parameters...RCM.
                        if (txtLastName.Text.Trim() != "" && txtFirstName.Text.Trim() != "")
                        {
                            sqlUpdateStatement_ParentGuardian = " UPDATE UIF_PerformingArts.dbo.ParentGuardianContactInformation " +
                                "SET "
                                + " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                                + " studentfirstname = '" + txtFirstName.Text.Trim() + "' , "
                                + " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' , "
                                + " parentguardianrelationship1 = '" + ddlParentGuardian1Relationship.Text.Trim() + "' , "
                                + " parentguardian1 = '" + txtParentGuardian1.Text.Trim() + "' , "
                                + " parentguardian1email = '" + txbParentGuardian1Email.Text.Trim() + "' , "
                                + " workphone1 = '" + txbParentGuardian1WrkPh.Text.Trim() + "' , "
                                + " cellphone1 = '" + txbParentGuardian1CellPhone.Text.Trim() + "' , "
                                //-------------------------------------------------------------------------------------------
                                + " textphone1 = " + ConvertYesNoToInt(ddlTextGuard1.Text) + ", "
                                //------------------------------------------------------------------------------------------
                                + " parentguardian2relationship  = '" + ddlParentGuardian2Relationship.Text.Trim() + "', "
                                + " parentguardian2 = '" + txbParentGuardian2.Text.Trim() + "' , "
                                + " workphone2 = '" + txbParentGuardian2WrkPh.Text.Trim() + "',"
                                + " cellphone2 = '" + txbParentGuardian2CellPhone.Text.Trim() + "',"
                                //-------------------------------------------------------------------------------------------
                                + " textphone2 = " + ConvertYesNoToInt(ddlTextGuard2.Text) + ", "
                                //------------------------------------------------------------------------------------------
                                + " emergencycontact = '" + txbEmergencyRelationship.Text.Trim() + "' , "
                                + " emergrelationship = '" + txbEmergRelationship.Text.Trim() + "' , "
                                + " emergencycontactphone = '" + txbEmergencyPhone.Text.Trim() + "' , "
                                + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                                + " lastfirstnamekey = '" + txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim() + " (" + txbMiddleName.Text.Trim() + ")' "
                                + " " +
                                " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' " +
                                " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' " +
                                " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
                        }
                        else
                        {
                            sqlUpdateStatement_ParentGuardian = " UPDATE UIF_PerformingArts.dbo.ParentGuardianContactInformation " +
                                "SET "
                                //+ " studentlastname = '" + txtLastName.Text.Trim() + "' , "
                                //+ " studentfirstname = '" + txtFirstName.Text.Trim() + "' , "
                                //+ " studentmiddlename = '" + txbMiddleName.Text.Trim() + "' , "
                                + " parentguardianrelationship1 = '" + ddlParentGuardian1Relationship.Text.Trim() + "' , "
                                + " parentguardian1 = '" + txtParentGuardian1.Text.Trim() + "' , "
                                + " parentguardian1email = '" + txbParentGuardian1Email.Text.Trim() + "' , "
                                + " workphone1 = '" + txbParentGuardian1WrkPh.Text.Trim() + "' , "
                                + " cellphone1 = '" + txbParentGuardian1CellPhone.Text.Trim() + "' , "
                                //-------------------------------------------------------------------------------------------
                                + " textphone1 = " + ConvertYesNoToInt(ddlTextGuard1.Text) + ", "
                                //------------------------------------------------------------------------------------------
                                + " parentguardian2relationship  = '" + ddlParentGuardian2Relationship.Text.Trim() + "', "
                                + " parentguardian2 = '" + txbParentGuardian2.Text.Trim() + "' , "
                                + " workphone2 = '" + txbParentGuardian2WrkPh.Text.Trim() + "',"
                                + " cellphone2 = '" + txbParentGuardian2CellPhone.Text.Trim() + "',"
                                //-------------------------------------------------------------------------------------------
                                + " textphone2 = " + ConvertYesNoToInt(ddlTextGuard2.Text) + ", "
                                //------------------------------------------------------------------------------------------
                                + " emergencycontact = '" + txbEmergencyRelationship.Text.Trim() + "' , "
                                + " emergrelationship = '" + txbEmergRelationship.Text.Trim() + "' , "
                                + " emergencycontactphone = '" + txbEmergencyPhone.Text.Trim() + "' , "
                                + " sysupdate = '" + System.DateTime.Now.ToString() + "', "
                                + " lastfirstnamekey = '" + txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim() + " (" + txbMiddleName.Text.Trim() + ")' "
                                + " " +
                                " WHERE studentlastname = '" + Request.QueryString["StudentLastName"] + "' " +
                                " AND studentfirstname = '" + Request.QueryString["StudentFirstName"] + "' " +
                                " AND studentmiddlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
                        }
                    }

                    //create a SQL command to update record
                    SqlCommand sqlUpdateCommand_ParentGuardian = new SqlCommand(sqlUpdateStatement_ParentGuardian, con);
                    if (sqlUpdateCommand_ParentGuardian.ExecuteNonQuery() > 0)
                    {
                    }
                    else
                    {
                        //Didn't find a record to update..RCM.
                    }
                }
                catch (Exception lkjlk)
                {
                    lblErrorMessage.Enabled = true;
                    lblErrorMessage.Text = "The update to SECTION2, the DB failed.  Please fix and try again MSG: " + lkjlk.Message.ToString();
                    throw new Exception("The Update to the ParentGuardian table failed.   Please fix and try again MSG: " + lkjlk.Message.ToString());
                }

                try
                {
                    //Update the ParentGuardian information...RCM..10/7/10.
                    string sqlUpdateStatement_ProgramsList = "";

                    //if ((Request.QueryString["StudentLastName"] == "") || (Request.QueryString["StudentFirstName"] == ""))
                    if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement_ProgramsList = " UPDATE UIF_PerformingArts.dbo.ProgramsList " +
                            "SET "
                            + " lastname = '" + txtLastName.Text.Trim() + "' , "
                            + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                            + " middlename = '" + txbMiddleName.Text.Trim() + "' , "
                            //PerformingArts fields..RCM.
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
                            //--------------------------------------------------------------------
                            //Education..RCM.
                            + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                            //---------------------------------------------------------------------------------
                            + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                            + " academicreadingsupport = " + Convert.ToInt32(chbReadingSupport.Checked) + ", "
                            //---------------------------------------------------------------------------------------
                            + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchoolsPA.Checked) + ", "
                            //---------------------------------------------------------------------------------------
                            + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                            + " "
                            + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                            + " AND firstname = '" + txtFirstName.Text.Trim() + "' "
                            + " AND middlename = '" + txbMiddleName.Text.Trim() + "' "
                            + " AND student = 1 ";
                    }
                    else
                    {
                        if (Department == "Athletics")
                        {
                            //if lastname and firstname contain data, then update them using QueryString parameters...RCM.
                            if (txtLastName.Text.Trim() != "" && txtFirstName.Text.Trim() != "")
                            {
                                sqlUpdateStatement_ProgramsList = " UPDATE UIF_PerformingArts.dbo.ProgramsList " +
                                    "SET "
                                    + " lastname = '" + txtLastName.Text.Trim() + "' , "
                                    + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                    + " middlename = '" + txbMiddleName.Text.Trim() + "' , "
                                    //PerformingArts fields..RCM.
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
                                    //--------------------------------------------------------------------
                                    //Education..RCM.
                                    + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                    + " academicreadingsupport = " + Convert.ToInt32(chbReadingSupport.Checked) + ", "
                                    //---------------------------------------------------------------------------------
                                    + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchools.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                    + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                    + " "
                                    + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                    + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                    + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                                    + " AND student = 1 ";
                            }
                            else
                            {
                                sqlUpdateStatement_ProgramsList = " UPDATE UIF_PerformingArts.dbo.ProgramsList " +
                                    "SET "
                                    //+ " lastname = '" + txtLastName.Text.Trim() + "' , "
                                    //+ " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                    //+ " middlename = '" + txbMiddleName.Text.Trim() + "' , "
                                    //PerformingArts fields..RCM.
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
                                    //--------------------------------------------------------------------
                                    //Education..RCM.
                                    + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                    + " academicreadingsupport = " + Convert.ToInt32(chbReadingSupport.Checked) + ", "
                                    //---------------------------------------------------------------------------------
                                    + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchools.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                    + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                    + " "
                                    + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                    + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                    + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                                    + " AND student = 1 ";
                            }
                        }
                        else if (Department == "PerformingArts")
                        {
                            //if lastname and firstname contain data, then update them using QueryString parameters...RCM.
                            if (txtLastName.Text.Trim() != "" && txtFirstName.Text.Trim() != "")
                            {
                                sqlUpdateStatement_ProgramsList = " UPDATE UIF_PerformingArts.dbo.ProgramsList " +
                                    "SET "
                                    + " lastname = '" + txtLastName.Text.Trim() + "' , "
                                    + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                    + " middlename = '" + txbMiddleName.Text.Trim() + "' , "
                                    //PerformingArts fields..RCM.
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
                                    //--------------------------------------------------------------------
                                    //Education..RCM.
                                    + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                    //---------------------------------------------------------------------------------
                                    + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                    + " academicreadingsupport = " + Convert.ToInt32(chbReadingSupport.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchoolsPA.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                    + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                    + " "
                                    + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                    + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                    + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                                    + " AND student = 1 ";
                            }
                            else
                            {
                                sqlUpdateStatement_ProgramsList = " UPDATE UIF_PerformingArts.dbo.ProgramsList " +
                                    "SET "
                                    //+ " lastname = '" + txtLastName.Text.Trim() + "' , "
                                    //+ " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                    //+ " middlename = '" + txbMiddleName.Text.Trim() + "' , "
                                    //PerformingArts fields..RCM.
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
                                    //--------------------------------------------------------------------
                                    //Education..RCM.
                                    + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                    + " academicreadingsupport = " + Convert.ToInt32(chbReadingSupport.Checked) + ", "
                                    //---------------------------------------------------------------------------------
                                    + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchoolsPA.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                    + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                    + " "
                                    + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                    + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                    + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                                    + " AND student = 1 ";
                            }

                        }
                        else if (Department == "Education")
                        {
                            //if lastname and firstname contain data, then update them using QueryString parameters...RCM.
                            if (txtLastName.Text.Trim() != "" && txtFirstName.Text.Trim() != "")
                            {
                                sqlUpdateStatement_ProgramsList = " UPDATE UIF_PerformingArts.dbo.ProgramsList " +
                                    "SET "
                                    + " lastname = '" + txtLastName.Text.Trim() + "' , "
                                    + " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                    + " middlename = '" + txbMiddleName.Text.Trim() + "' , "
                                    //PerformingArts fields..RCM.
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
                                    //--------------------------------------------------------------------
                                    //Education..RCM.
                                    + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                    + " academicreadingsupport = " + Convert.ToInt32(chbReadingSupport.Checked) + ", "
                                    //---------------------------------------------------------------------------------
                                    + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                    + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                    + " "
                                    + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                    + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                    + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                                    + " AND student = 1 ";
                            }
                            else
                            {
                                sqlUpdateStatement_ProgramsList = " UPDATE UIF_PerformingArts.dbo.ProgramsList " +
                                    "SET "
                                    //+ " lastname = '" + txtLastName.Text.Trim() + "' , "
                                    //+ " firstname = '" + txtFirstName.Text.Trim() + "' , "
                                    //+ " middlename = '" + txbMiddleName.Text.Trim() + "' , "
                                    //PerformingArts fields..RCM.
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
                                    //--------------------------------------------------------------------
                                    //Education..RCM.
                                    + " summerdaycamp = " + Convert.ToInt32(chbSummerDayCamp.Checked) + ", "
                                    + " academicreadingsupport = " + Convert.ToInt32(chbReadingSupport.Checked) + ", "
                                    //---------------------------------------------------------------------------------
                                    + " specialevents = " + Convert.ToInt32(chbSpecialEvents.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " impacturbanschools = " + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                    //---------------------------------------------------------------------------------------
                                    + " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                    + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                    + " "
                                    + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                    + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                    + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' "
                                    + " AND student = 1 ";
                            }
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
                    lblErrorMessage.Enabled = true;
                    lblErrorMessage.Text = "The update to ProgramsList table failed.  Please fix and try again MSG: " + lkjlk.Message.ToString();
                    throw new Exception("The Update to the ProgramsList table failed.   Please fix and try again MSG: " + lkjlk.Message.ToString());
                }

                try
                {
                    //Update the ParentGuardian information...RCM..10/7/10.
                    string sqlUpdateStatement_StudentMedications = "";
                    string AdministerMedicine = "";

                    if (ddlAdministerMedicine.Text == "Administer Medicine?")
                    {
                        AdministerMedicine = " ";                                                
                    }
                    else
                    {
                        AdministerMedicine = " administeryesno = " + ConvertYesNoToInt(ddlAdministerMedicine.Text) + ", ";
                    }

                    //if ((Request.QueryString["StudentLastName"] == "") || (Request.QueryString["StudentFirstName"] == ""))
                    if ((Request.QueryString["newstudent"] == "newstudent"))
                    {
                        sqlUpdateStatement_StudentMedications = " UPDATE UIF_PerformingArts.dbo.StudentMedications " +
                            "SET "
                            + " lastname = '" + txtLastName.Text.Trim() + "', "
                            + " firstname = '" + txtFirstName.Text.Trim() + "', "
                            + " middlename = '" + txbMiddleName.Text.Trim() + "', "
                            + AdministerMedicine
                            //+ " administeryesno = " + ConvertYesNoToInt(ddlAdministerMedicine.Text) + ", "
                            ////////+ " administeryesno = " + Convert.ToInt32(chbMedication.Checked) + ", "
                            + " aspirin = " + Convert.ToInt32(cblMedications.Items[0].Selected) + ", "
                            + " tylenol = " + Convert.ToInt32(cblMedications.Items[1].Selected) + ", "
                            + " ibuprofen = " + Convert.ToInt32(cblMedications.Items[2].Selected) + ", "
                            + " advil = " + Convert.ToInt32(cblMedications.Items[3].Selected) + ", "
                            + " antacids = " + Convert.ToInt32(cblMedications.Items[4].Selected) + ", "
                            + " benadryl = " + Convert.ToInt32(cblMedications.Items[5].Selected) + ", "
                            + " [antiseptic ointment] = " + Convert.ToInt32(cblMedications.Items[6].Selected) + ", "
                            + " [anesthetic ointment] = " + Convert.ToInt32(cblMedications.Items[7].Selected) + ", "
                            + " iodinepreppad = " + Convert.ToInt32(cblMedications.Items[8].Selected) + ", "
                            + " acetaminophen = " + Convert.ToInt32(cblMedications.Items[9].Selected) + ", "
                            + " rubbingalcohol = " + Convert.ToInt32(cblMedications.Items[10].Selected) + ", "
                            + " other = " + Convert.ToInt32(cblMedications.Items[11].Selected) + ", "
                            + " othernotes = '" + txbMedicationsOtherNotes.Text.Trim() + "', "
                            + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                            + " "
                            + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                            + " AND firstname = '" + txtFirstName.Text.Trim() + "' "
                            + " AND middlename = '" + txbMiddleName.Text.Trim() + "' ";
                    }
                    else
                    {
                        //if lastname and firstname contain data, then update them using QueryString parameters...RCM.
                        if (txtLastName.Text.Trim() != "" && txtFirstName.Text.Trim() != "")
                        {
                            sqlUpdateStatement_StudentMedications = " UPDATE UIF_PerformingArts.dbo.StudentMedications " +
                                "SET "
                                + " lastname = '" + txtLastName.Text.Trim() + "', "
                                + " firstname = '" + txtFirstName.Text.Trim() + "', "
                                + " middlename = '" + txbMiddleName.Text.Trim() + "', "
                                + AdministerMedicine
                                //+ " administeryesno = " + ConvertYesNoToInt(ddlAdministerMedicine.Text) + ", "
                                //////////+ " administeryesno = " + Convert.ToInt32(chbMedication.Checked) + ", "
                                + " aspirin = " + Convert.ToInt32(cblMedications.Items[0].Selected) + ", "
                                + " tylenol = " + Convert.ToInt32(cblMedications.Items[1].Selected) + ", "
                                + " ibuprofen = " + Convert.ToInt32(cblMedications.Items[2].Selected) + ", "
                                + " advil = " + Convert.ToInt32(cblMedications.Items[3].Selected) + ", "
                                + " antacids = " + Convert.ToInt32(cblMedications.Items[4].Selected) + ", "
                                + " benadryl = " + Convert.ToInt32(cblMedications.Items[5].Selected) + ", "
                                + " [antiseptic ointment] = " + Convert.ToInt32(cblMedications.Items[6].Selected) + ", "
                                + " [anesthetic ointment] = " + Convert.ToInt32(cblMedications.Items[7].Selected) + ", "
                                + " iodinepreppad = " + Convert.ToInt32(cblMedications.Items[8].Selected) + ", "
                                + " acetaminophen = " + Convert.ToInt32(cblMedications.Items[9].Selected) + ", "
                                + " rubbingalcohol = " + Convert.ToInt32(cblMedications.Items[10].Selected) + ", "
                                + " other = " + Convert.ToInt32(cblMedications.Items[11].Selected) + ", "
                                + " othernotes = '" + txbMedicationsOtherNotes.Text.Trim() + "', "
                                + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                + " "
                                + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
                        }
                        else
                        {
                            sqlUpdateStatement_StudentMedications = " UPDATE UIF_PerformingArts.dbo.StudentMedications " +
                                "SET "
                                //+ " lastname = '" + txtLastName.Text.Trim() + "', "
                                //+ " firstname = '" + txtFirstName.Text.Trim() + "', "
                                //+ " middlename = '" + txbMiddleName.Text.Trim() + "', "
                                + AdministerMedicine
                                //+ " administeryesno = " + ConvertYesNoToInt(ddlAdministerMedicine.Text) + ", "
                                /////////+ " administeryesno = " + Convert.ToInt32(chbMedication.Checked) + ", "
                                + " aspirin = " + Convert.ToInt32(cblMedications.Items[0].Selected) + ", "
                                + " tylenol = " + Convert.ToInt32(cblMedications.Items[1].Selected) + ", "
                                + " ibuprofen = " + Convert.ToInt32(cblMedications.Items[2].Selected) + ", "
                                + " advil = " + Convert.ToInt32(cblMedications.Items[3].Selected) + ", "
                                + " antacids = " + Convert.ToInt32(cblMedications.Items[4].Selected) + ", "
                                + " benadryl = " + Convert.ToInt32(cblMedications.Items[5].Selected) + ", "
                                + " [antiseptic ointment] = " + Convert.ToInt32(cblMedications.Items[6].Selected) + ", "
                                + " [anesthetic ointment] = " + Convert.ToInt32(cblMedications.Items[7].Selected) + ", "
                                + " iodinepreppad = " + Convert.ToInt32(cblMedications.Items[8].Selected) + ", "
                                + " acetaminophen = " + Convert.ToInt32(cblMedications.Items[9].Selected) + ", "
                                + " rubbingalcohol = " + Convert.ToInt32(cblMedications.Items[10].Selected) + ", "
                                + " other = " + Convert.ToInt32(cblMedications.Items[11].Selected) + ", "
                                + " othernotes = '" + txbMedicationsOtherNotes.Text.Trim() + "', "
                                + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                                + " "
                                + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                                + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' "
                                + " AND middlename = '" + Request.QueryString["StudentMiddleName"] + "' ";
                        }
                    }
                    //create a SQL command to update record
                    SqlCommand sqlUpdateCommand_ProgramsList = new SqlCommand(sqlUpdateStatement_StudentMedications, con);
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
                    lblErrorMessage.Enabled = true;
                    lblErrorMessage.Text = "The update to StudentMedications table failed.  Please fix and try again MSG: " + lkjlk.Message.ToString();
                    throw new Exception("The Update to the StudentMedications table failed.   Please fix and try again MSG: " + lkjlk.Message.ToString());
                }

                UpdateAllTables();//Call to update other tables to ensure that the Primary key (names) stay in synch...RCM..
            }
            catch (Exception ex)
            {
                //add code to send error to admin via email
                //Session["Exception"] = ex.Message.ToString();
                //Response.Redirect("Error.aspx");

                lblErrorMessage.Enabled = true;
                lblErrorMessage.Text = ex.Message.ToString();
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
            GoodUpdate = UpdateStudentTable();
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {

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
            //if (chbNewStudentFlag.Checked == true)
            //{
            //    //Do Nothing.
            //}
            //else
            //{
            //    bool ValidUpdate = UpdateStudentTable();
            //}

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
                lbClassesEnrollment.Enabled = true;
                lbPerformingArtsAcademy.Enabled = true;

                bool ValidUpdate = UpdateStudentTable();

                Response.Redirect("PerformingArtsClasses.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&StudentMiddleName=" + txbMiddleName.Text + "&Dept=" + Request.QueryString["Dept"] + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
                
                //UpdateStudentTable(); 
   
                ////cmbProgramManagement.Enabled = true;
                //cmbProgramManagement.Visible = true;
                //cmbProgramManagement.Style.Add("z-index", "99999");

                //cmbProgramManageCancel.Enabled = true;
                //cmbProgramManageCancel.Visible = true;
                //cmbProgramManageCancel.Style.Add("z-index", "99999");

                //lblProgramManagement.Style.Add("z-index", "99999");
                //lblProgramManagement.Visible = true;

                //cblProgramManagement.Style.Add("z-index", "99999");
                //cblProgramManagement.Visible = true;

                //pnlProgramManagement.Style.Add("z-index", "9999");
                //pnlProgramManagement.Visible = true;
            }
            else
            {
                if (!chbPerformingArts.Checked)
                {
                    lbClassesEnrollment.Enabled = false;

                    Message("Danger!  This will remove this student from Academy Class Enrollment.  Do you wish to proceed?  ", this);
                    //Message99(sender, this);   ///   Danger!   This will remove students from class enrollemnt.  Do you wish to proceed..

                    cmbDeletePerformingArts.Enabled = true;
                    cmbDeletePerformingArts.Visible = true;
                    cmbDeletePerformingArts.Style.Add("z-index", "99999");

                    cmbCancelPerformArts.Enabled = true;
                    cmbCancelPerformArts.Visible = true;
                    cmbCancelPerformArts.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "This will remove the individual from this program!";

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;

                    //Message and Remove from the BasketballTEAMSEnrollment table.
                    //Message("This will remove this student from BasketballTEAMS Enrollment.  Do you wish to proceed? ", Label8);
                    //DeleteFromBasketballTEAMS();
                    //UpdateStudentTable();
                }
            }
                //RemoveStudentsFromClasses();
                //-------------
            if (chbNewStudentFlag.Checked == true)
            {
                //Do Nothing.
            }
            else
            {
                //if not a new person, update automatically...RCM..
                bool ValidUpdate = UpdateStudentTable();
            }

            //DisableAllConfirmButtons();
            //rblOutreachBasketball.Visible = false;
            //cmbOutreachBasketball.Visible = false;
            //cmbOutreachBasketballCancel.Visible = false;
            //cblProgramManagement.Visible = false;
            //cmbProgramManageCancel.Visible = false;
            //cmbProgramManagement.Visible = false;
            //if (chbPerformingArts.Checked)
            //{
            //    cmbPAAConfirm.Visible = true;
            //    cmbPAAConfirm.Style.Add("z-index", "99999");

            //    cmbPAACancel.Enabled = true;
            //    cmbPAACancel.Visible = true;
            //    cmbPAACancel.Style.Add("z-index", "99999");

            //    lblProgramManagement.Style.Add("z-index", "99999");
            //    lblProgramManagement.Visible = true;

            //    PopulateRadioButtonLists("PAA");
            //    cblProgramManagement.Style.Add("z-index", "99999");
            //    cblProgramManagement.Visible = true;

            //    pnlProgramManagement.Style.Add("z-index", "9999");
            //    pnlProgramManagement.Visible = true;
            //}
            //else
            //{
            //    if (!chbPerformingArts.Checked)
            //    {
            //        cmbPAARemove.Visible = true;
            //        cmbPAARemove.Style.Add("z-index", "99999");

            //        cmbPAACancel.Enabled = true;
            //        cmbPAACancel.Visible = true;
            //        cmbPAACancel.Style.Add("z-index", "99999");

            //        lblProgramManagement.Style.Add("z-index", "99999");
            //        lblProgramManagement.Visible = true;
            //        lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

            //        PopulateRadioButtonListsAndChoice("PAA");
            //        cblProgramManagement.Style.Add("z-index", "99999");
            //        cblProgramManagement.Visible = true;

            //        pnlProgramManagement.Style.Add("z-index", "9999");
            //        pnlProgramManagement.Visible = true;
            //    }
            //}
        }

        protected void RemoveStudentsFromBaseball()
        {
            //Remove student from LittleLeagueBaseball  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.BaseballEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND sectionname = '" + rblBaseball.SelectedItem.Text + "' "
                                                     + "AND student = 1 ";
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

        protected void RemoveStudentsFromProgram(string Program)
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
                                                             + "AND middlename = '" + txbMiddleName.Text.Trim() + "' "
                                                             + "AND sectionname = '" + checkboxitem.Text.Trim() + "' "
                                                             + "AND student = 1 ";

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
            
                //Determine if the student is still enrolled in at least 1 section.   If so,
                //leave the checkbox checked.  Else allow it to be unchecked..RCM..8/13/12.
                string CheckPartialEnrollment = "Select * from UIF_PerformingArts.dbo." + "[" + tablename + "Enrollment] "
                                              + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                              + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                              + "AND middlename = '" + txbMiddleName.Text.Trim() + "' "
                                              + "AND student = 1 ";

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
                        chbReadingSupport.Checked = true;
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
                        chbReadingSupport.Checked = false;
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

        protected void RemoveStudentsFromMSBasketballLeague()
        {
            //Remove student from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.MSBasketballLeagueEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND middlename = '" + txbMiddleName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
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

        protected void RemoveStudentsFromSoccerIntraMurals()
        {
            //Remove student from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.SoccerIntraMuralsEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
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

        protected void RemoveStudentsFromBibleStudy()
        {
            //Remove student from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.BibleStudyEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
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

        protected void RemoveStudentsFrom3on3Basketball()
        {
            //Remove student from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.[3on3BasketballEnrollment] "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
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

        protected void RemoveStudentsFromSoccerTEAMS()
        {
            //Remove student from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.SoccerTEAMSEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
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

        protected void RemoveStudentsFromMondayNights()
        {
            //Remove student from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.MondayNightsEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
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

        protected void RemoveStudentsFromHSBasketballLeague()
        {
            //Remove student from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.HSBasketballLeagueEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
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
        

        protected void RemoveStudentsFromClasses()
        {
            //Remove student from classes..  
            try
            {
                string sql_DeleteFromPerformingArts = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyClassEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
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

        //public static void Message2(String message, Control cntrl) { ScriptManager.RegisterStartupScript(cntrl, cntrl.GetType(), "alert", "alert('" + message + "');", true); }
        public static void Message(String message, Control cntrl)
        {
          //  ScriptManager.RegisterStartupScript(cntrl, cntrl., "alert", "alert('" + message + "');", true);
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

        protected void Message2(object sender, EventArgs e)    {        //Do Something        
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "ConfirmMessage();", true);    }    
        protected void Message3(object sender, EventArgs e)    {        //Do Something        
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "alert('You clicked Hidden Save');", true);    }    
        protected void Message4(object sender, EventArgs e)    {        //Do Something       
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "alert('You clicked Hidden Cancel');", true);    }

        protected void chbRegistrationForm_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRegistrationForm.Checked)
            {
                chbPromotionalRelease.Checked = true;
                chbPermissionTransport.Checked = true;
            }
            else
            {
                //if (!chbRegistrationForm)
                //{
                //}
            }
        }

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
            Response.Redirect("uifadmin2.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void CoreKidsInsert()
        {
            //Blank out the student information..RCM..1/16/11.

            try
            {
                CleanCharacters();

                btnNewPerson1.Enabled = false;
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
                        sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.CoreKidsProgram " +
                            "values ( "
                            + "'" + txtLastName.Text.Trim() + "',"
                            + "'" + txtFirstName.Text.Trim() + "',"
                            + "0,"  //MultiplePrograms?  (Calculate)
                            + "'Select a department', "  //EnrolledPrograms (Calculate,Determine)
                            + "0," //DiscipleshipMentor?  (Calculate)            
                            + "1," //Consistent Attendance?  (Calculate)
                            + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ","  //BibleStudyProgram?
                            + "0," //StudentAssistant?   (Determine)
                            + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ","
                            + "'" + txbNotes.Text.Trim() + "',"  //Comments..
                            + "'" + System.DateTime.Now.ToString() + "', "  //Syscreate
                            + "'" + System.DateTime.Now.ToString() + "', "  //SysUpdate
                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";  //LastUpdatedBy

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
                    catch (Exception lkjlaa)
                    {
                        lblErrorMessage.Enabled = true;
                        lblErrorMessage.Text = "There was an exception INSERTING NEW data into the CoreKids table..  Please fix and try again MSG: " + lkjlaa.Message.ToString();
                        throw new Exception(lblErrorMessage.Text);
                    }
                }
            }
            catch (Exception lkjlaaa)
            {

            }
            finally
            {

            }
        }

        protected void NewStudent()
        {
            //Blank out the student information..RCM..1/16/11.
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtAddress1.Text = "";
            txtCity.Text = "Pittsburgh";
            txtState.Text = "PA";
            txtZip.Text = "";
            txtHomePhone.Text = "";
            txtStudentCellPhone.Text = "";
            txtStudentEmail.Text = "";
            //ddlSchool.Text = "";
            ddlGrade.ClearSelection();
            ddlAge.ClearSelection();
            ddlDayBirth.ClearSelection();
            ddlMonthBirth.ClearSelection();
            ddlYearBirth.ClearSelection();
            ddlGender.ClearSelection();
            ddlChurch.Text = "";
            txtCareerGoal.Text = "";
            ddlTShirtSize.ClearSelection();
            txbDiscipleshipMentor.Text = "";
            txbHealthConditions.Text = "";
            txbNotes.Text = "";
            txbSoloSong.Text = "";
            txbID.Text = "";
            ddlVoicePart.ClearSelection();
            chbMSHSChoir.Checked = false;
            chbChildrensChoir.Checked = false;
            chbPerformingArts.Checked = false;
            chbRegistrationForm.Checked = false;
            chbRetreatForm.Checked = false;
            chbParentalConsentForm.Checked = false;
            chbStudentQuestionareForm.Checked = false;


            cmbClearPage.Enabled = false;

            lbBaseball.Enabled = false;
            lb3on3Basketball.Enabled = false;
            lbSoccerIntraMurals.Enabled = false;
            lbSoccerTEAMS.Enabled = false;
            lbBibleStudy.Enabled = false;
            lbSpecialEvents.Enabled = false;
            lbMondayNights.Enabled = false;
            lbOutreachBasketball.Enabled = false;
            lbBasketballTEAMS.Enabled = false;
            lbMSBasketballLeague.Enabled = false;
            lbHSBasketballLeague.Enabled = false;
            lbMSHSChoir.Enabled = false;
            lbChildrensChoir.Enabled = false;
            lbPerformingArtsAcademy.Enabled = false;
            lbShakes.Enabled = false;
            lbSingers.Enabled = false;
            lbImpactUrbanSchools.Enabled = false;
            lbImpactUrbanSchoolsPA.Enabled = false;
            lbImpactUrbanSchoolsAcademics.Enabled = false;


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
            lbClassesEnrollment.Enabled = false;
            btnSubmitInformation.Enabled = false;
            btnNewPerson1.Enabled = true;

            chbDiscipleshipMentor.Enabled = false;
            chbOptionsTurnOn.Enabled = false;

            chbMSHSChoir.Enabled = false;
            chbChildrensChoir.Enabled = false;
            chbPerformingArts.Enabled = false;
            chbShakes.Enabled = false;
            chbSingers.Enabled = false;
            chbImpactUrbanSchoolsPA.Enabled = false;

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
            chbImpactUrbanSchools.Enabled = false;

            chbSummerDayCamp.Enabled = false;
            chbSATPrepClass.Enabled = false;
            chbImpactUrbanSchoolsAcademics.Enabled = false;
            lbSummerDayCamp.Enabled = false;
            lbAcademicReadingSupport.Enabled = false;
            txbDropOffPickUp.Enabled = false;

            lbClassesEnrollment.Enabled = false;

            lblLastScrubbed.Text = "Last Scrubbed?";
            lblLastScrubbed.Visible = true;
            lblLastScrubbed.Enabled = true;

            ////Ryan C Manners..10/15/10. 
            ////Disable certain functionality based on login of person (staff member)
            //if (Department == "Athletics")
            //{
            //    chbMSHSChoir.Enabled = false;
            //    chbChildrensChoir.Enabled = false;
            //    chbPerformingArts.Enabled = false;
            //    chbShakes.Enabled = false;
            //    chbSingers.Enabled = false;

            //    lbClassesEnrollment.Enabled = false;

            //    ddlVoicePart.Enabled = false;
            //    txbSoloSong.Enabled = false;

            //    //Disable these 3 athletics checkboxes until they insert new student information
            //    //into the database....RCM..1/23/12..
            //    chbBasketballTEAMS.Enabled = false;
            //    chbMSBasketLeague.Enabled = false;
            //    chbHSBasketLeague.Enabled = false;

            //    //chbMSHSChoir.Attributes.Add("onclick", "return false;");
            //    //chbChildrensChoir.Attributes.Add("onclick", "return false;");
            //    //chbPerformingArts.Attributes.Add("onclick", "return false;");
            //    //chbShakes.Attributes.Add("onclick", "return false;");
            //    //chbSingers.Attributes.Add("onclick", "return false;");
            //}
            //else if (Department == "PerformingArts")
            //{
            //    chbMondayNights.Enabled = false;
            //    chb3on3Basketball.Enabled = false;
            //    chbBasketballTEAMS.Enabled = false;
            //    chbBoysOutreachBball.Enabled = false;
            //    chbSoccerInterMurals.Enabled = false;
            //    chbSoccerLgTravel.Enabled = false;
            //    chbGirlsOutreachBball.Enabled = false;
            //    chbMSBasketLeague.Enabled = false;
            //    chbLittleLeagueBaseball.Enabled = false;
            //    chbOliverFootballBible.Enabled = false;
            //    chbHSBasketLeague.Enabled = false;

            //    if (chbPerformingArts.Checked)
            //    {
            //        lbClassesEnrollment.Enabled = true;
            //    }
            //    //chbMondayNights.Attributes.Add("onclick", "return false;");
            //    //chb3on3Basketball.Attributes.Add("onclick", "return false;");
            //    //chbBasketballTEAMS.Attributes.Add("onclick", "return false;");
            //    //chbBoysOutreachBball.Attributes.Add("onclick", "return false;");
            //    //chbSoccerInterMurals.Attributes.Add("onclick", "return false;");
            //    //chbSoccerLgTravel.Attributes.Add("onclick", "return false;");
            //    //chbGirlsOutreachBball.Attributes.Add("onclick", "return false;");
            //    //chbMSBasketLeague.Attributes.Add("onclick", "return false;");
            //    //chbLittleLeagueBaseball.Attributes.Add("onclick", "return false;");
            //    //chbOliverFootballBible.Attributes.Add("onclick", "return false;");
            //    //chbHSBasketLeague.Attributes.Add("onclick", "return false;");
            //}
            //else if (Department == "Education")
            //{

            //}
            //else if (Department == "BusinessOffice")
            //{
            //    //chbMSHSChoir.Enabled = false;
            //    //chbChildrensChoir.Enabled = false;
            //    //chbPerformingArts.Enabled = false;
            //    //chbShakes.Enabled = false;
            //    //chbSingers.Enabled = false;

            //    //chbMondayNights.Enabled = false;
            //    //chb3on3Basketball.Enabled = false;
            //    //chbBasketballTEAMS.Enabled = false;
            //    //chbBoysOutreachBball.Enabled = false;
            //    //chbSoccerInterMurals.Enabled = false;
            //    //chbSoccerLgTravel.Enabled = false;
            //    //chbGirlsOutreachBball.Enabled = false;
            //    //chbMSBasketLeague.Enabled = false;
            //    //chbLittleLeagueBaseball.Enabled = false;
            //    //chbOliverFootballBible.Enabled = false;
            //    //chbHSBasketLeague.Enabled = false;


            //    chbMSHSChoir.Attributes.Add("onclick", "return false;");
            //    chbChildrensChoir.Attributes.Add("onclick", "return false;");
            //    chbPerformingArts.Attributes.Add("onclick", "return false;");
            //    chbShakes.Attributes.Add("onclick", "return false;");
            //    chbSingers.Attributes.Add("onclick", "return false;");

            //    chbMondayNights.Attributes.Add("onclick", "return false;");
            //    chb3on3Basketball.Attributes.Add("onclick", "return false;");
            //    chbBasketballTEAMS.Attributes.Add("onclick", "return false;");
            //    chbBoysOutreachBball.Attributes.Add("onclick", "return false;");
            //    chbSoccerInterMurals.Attributes.Add("onclick", "return false;");
            //    chbSoccerLgTravel.Attributes.Add("onclick", "return false;");
            //    chbGirlsOutreachBball.Attributes.Add("onclick", "return false;");
            //    chbMSBasketLeague.Attributes.Add("onclick", "return false;");
            //    chbLittleLeagueBaseball.Attributes.Add("onclick", "return false;");
            //    chbOliverFootballBible.Attributes.Add("onclick", "return false;");
            //    chbHSBasketLeague.Attributes.Add("onclick", "return false;");
            //}
            //else
            //{
            //    ////PerformingArts fields.
            //    chbMSHSChoir.Enabled = false;
            //    chbChildrensChoir.Enabled = false;
            //    chbPerformingArts.Enabled = false;
            //    chbShakes.Enabled = false;
            //    chbSingers.Enabled = false;

            //    //Athletics fields..
            //    chbMondayNights.Enabled = false;
            //    chb3on3Basketball.Enabled = false;
            //    chbBasketballTEAMS.Enabled = false;
            //    chbBoysOutreachBball.Enabled = false;
            //    chbSoccerInterMurals.Enabled = false;
            //    chbSoccerLgTravel.Enabled = false;
            //    chbGirlsOutreachBball.Enabled = false;
            //    chbMSBasketLeague.Enabled = false;
            //    chbLittleLeagueBaseball.Enabled = false;
            //    chbOliverFootballBible.Enabled = false;
            //    chbHSBasketLeague.Enabled = false;


            //    //chbMSHSChoir.Attributes.Add("onclick", "return false;");
            //    //chbChildrensChoir.Attributes.Add("onclick", "return false;");
            //    //chbPerformingArts.Attributes.Add("onclick", "return false;");
            //    //chbShakes.Attributes.Add("onclick", "return false;");
            //    //chbSingers.Attributes.Add("onclick", "return false;");

            //    //chbMondayNights.Attributes.Add("onclick", "return false;");
            //    //chb3on3Basketball.Attributes.Add("onclick", "return false;");
            //    //chbBasketballTEAMS.Attributes.Add("onclick", "return false;");
            //    //chbBoysOutreachBball.Attributes.Add("onclick", "return false;");
            //    //chbSoccerInterMurals.Attributes.Add("onclick", "return false;");
            //    //chbSoccerLgTravel.Attributes.Add("onclick", "return false;");
            //    //chbGirlsOutreachBball.Attributes.Add("onclick", "return false;");
            //    //chbMSBasketLeague.Attributes.Add("onclick", "return false;");
            //    //chbLittleLeagueBaseball.Attributes.Add("onclick", "return false;");
            //    //chbOliverFootballBible.Attributes.Add("onclick", "return false;");
            //    //chbHSBasketLeague.Attributes.Add("onclick", "return false;");                .
            //    lbClassesEnrollment.Enabled = false;
            //}
        }

        protected void cmbClearPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrationForm.aspx?Security=Good&newstudent=newstudent" + "&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            
            //chbNewStudentFlag.Checked = true;
            //StartingSettings();
            //NewStudent();
        }

        protected void lbClassesEnrollment_Click(object sender, EventArgs e)
        {
            UpdateStudentTable();
            //Response.Redirect("PerformingArtsClasses.aspx?    "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
            Response.Redirect("PerformingArtsClasses.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&StudentMiddleName=" + txbMiddleName.Text + "&Dept=" + Request.QueryString["Dept"] + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
        }

        protected void btnNewPerson1_Click(object sender, EventArgs e)
        {
            //Here we want to capture all the information from the various different fields and
            //use them to update the data within the database table..RCM..8/3/10.
            try
            {
                CleanCharacters();

                if ((txtLastName.Text == "") || (txtFirstName.Text == ""))
                {
                    //Student LastName is blank..  Abort..
                    
                    //Throw an exception...RCM..
                    string except = "EXCEPTION";
                }
                else
                {
                    string sqlInsertStatement = "";


                    if (txbMiddleName.Text == "")
                    {
                        txbMiddleName.Text = "N/A";
                    }

                    try
                    {
                        if ((txbMiddleName.Text.Trim() == "") || (txbMiddleName.Text.Trim() == "N/A"))
                        {
                            sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.StudentInformation " +
                                "values ( "
                                + "'" + txtLastName.Text.Trim() + "',"
                                + "'" + txtFirstName.Text.Trim() + "',"
                                + "'" + txtAddress1.Text.Trim() + "',"
                                + "'" + txtCity.Text.Trim() + "',"
                                + "'" + txtState.Text.Trim() + "',"
                                + "'" + txtZip.Text.Trim() + "',"
                                + "'" + txtHomePhone.Text.Trim() + "',"
                                + "'" + txtStudentCellPhone.Text.Trim() + "',"
                                + "0,"
                                //+ Convert.ToInt32(ddlTextPhone.Text) + ","
                                + "'" + txtStudentEmail.Text.Trim() + "',"
                                //+ Convert.ToInt32(chbMSHSChoir.Checked) + ","
                                //+ Convert.ToInt32(chbPerformingArts.Checked) + ","
                                + "'" + ddlSchool.Text.Trim() + "',"
                                + "'" + ddlGrade.Text.Trim() + "',"
                                + "'" + ddlAge.Text.Trim() + "',"
                                + "'" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "',"
                                + "'" + ddlGender.Text.Trim() + "',"
                                + "'" + ddlChurch.Text.Trim() + "',"
                                + "'" + ddlCareerGoal.Text.Trim() + "',"
                                + "'" + txbHealthConditions.Text.Trim() + "',"
                                + "'" + txbNotes.Text.Trim() + "',"
                                + "'" + ddlTShirtSize.Text.Trim() + "',"
                                + Convert.ToInt32(chbMeetCCGF.Checked) + ","
                                + Convert.ToInt32(chbParentalConsentForm.Checked) + ","
                                + "'" + txbDiscipleshipMentor.Text.Trim() + "',"
                                + Convert.ToInt32(chbSoloist.Checked) + ","
                                + "'" + txbSoloSong.Text.Trim() + "',"
                                + Convert.ToInt32(chbDance.Checked) + ","
                                + "'1900-01-01'" + ","//danceyear
                                + "'" + txtCareerGoal.Text.Trim() + "',"//schoolform2
                                + Convert.ToInt32(chbBibleOwnership.Checked) + ","
                                + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ","
                                //+ "'~/StudentPictures/" + txtFirstName.Text.Trim() + txtLastName.Text.Trim() + ".jpg', "
                                + "'~/StudentPictures/" + txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim() + ".jpg', "
                                + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ", "
                                + "'1900-01-01', " //when received christ.
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + Convert.ToInt32(chbRegistrationForm.Checked) + ", "
                                + Convert.ToInt32(chbParentalConsentForm.Checked) + ", "
                                + Convert.ToInt32(chbRetreatForm.Checked) + ", "
                                + Convert.ToInt32(chbStudentQuestionareForm.Checked) + ", "
                                //+ Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                + 333 + ", "  //Gonna be removing this altogether.. unneeded..RCM..1/27/11.
                                + "'" + ddlVoicePart.Text.Trim() + "', "
                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + Convert.ToInt32(chbDiscipleshipMentor.Checked) + ", "
                                + "'" + txbMiddleName.Text.Trim() + "', "
                                + Convert.ToInt32(chbMailingList.Checked) + ", "
                                + "'" + ddlMailingListCodes.Text.Trim() + "', "
                                //--------new fields...3/21/12.-------------------------------------------------------
                                + Convert.ToInt32(chbPromotionalRelease.Checked) + ", "
                                + Convert.ToInt32(chbPermissionTransport.Checked) + ", "
                                + Convert.ToInt32(chbCAMPDropOff.Checked) + ", "
                                + Convert.ToInt32(chbCAMPPickUp.Checked) + ", "
                                + "'" + txbDropOffPickUp.Text.Trim() + "', "
                                + Convert.ToInt32(chbIncludePromotionalMailing.Checked) + ", "
                                + Convert.ToInt32(chbStudentVolunteer.Checked) + ", "
                                + Convert.ToInt32(chbScrubbed.Checked) + ", "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                        }
                        else
                        {
                            sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.StudentInformation " +
                                "values ( "
                                + "'" + txtLastName.Text.Trim() + "',"
                                + "'" + txtFirstName.Text.Trim() + "',"
                                + "'" + txtAddress1.Text.Trim() + "',"
                                + "'" + txtCity.Text.Trim() + "',"
                                + "'" + txtState.Text.Trim() + "',"
                                + "'" + txtZip.Text.Trim() + "',"
                                + "'" + txtHomePhone.Text.Trim() + "',"
                                + "'" + txtStudentCellPhone.Text.Trim() + "',"
                                + "0,"
                                //+ Convert.ToInt32(ddlTextPhone.Text) + ","
                                + "'" + txtStudentEmail.Text.Trim() + "',"
                                //+ Convert.ToInt32(chbMSHSChoir.Checked) + ","
                                //+ Convert.ToInt32(chbPerformingArts.Checked) + ","
                                + "'" + ddlSchool.Text.Trim() + "',"
                                + "'" + ddlGrade.Text.Trim() + "',"
                                + "'" + ddlAge.Text.Trim() + "',"
                                + "'" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "',"
                                + "'" + ddlGender.Text.Trim() + "',"
                                + "'" + ddlChurch.Text.Trim() + "',"
                                + "'" + ddlCareerGoal.Text.Trim() + "',"
                                + "'" + txbHealthConditions.Text.Trim() + "',"
                                + "'" + txbNotes.Text.Trim() + "',"
                                + "'" + ddlTShirtSize.Text.Trim() + "',"
                                + Convert.ToInt32(chbMeetCCGF.Checked) + ","
                                + Convert.ToInt32(chbParentalConsentForm.Checked) + ","
                                + "'" + txbDiscipleshipMentor.Text.Trim() + "',"
                                + Convert.ToInt32(chbSoloist.Checked) + ","
                                + "'" + txbSoloSong.Text.Trim() + "',"
                                + Convert.ToInt32(chbDance.Checked) + ","
                                + "'1900-01-01'" + ","//danceyear
                                + "'" + txtCareerGoal.Text.Trim() + "',"//schoolform2
                                + Convert.ToInt32(chbBibleOwnership.Checked) + ","
                                + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ","
                                //+ "'~/StudentPictures/" + txtFirstName.Text.Trim() + txtLastName.Text.Trim() + ".jpg', "
                                + "'~/StudentPictures/" + txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim() + txbMiddleName.Text.Trim() + ".jpg', "
                                + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ", "
                                + "'1900-01-01', " //when received christ.
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + Convert.ToInt32(chbRegistrationForm.Checked) + ", "
                                + Convert.ToInt32(chbParentalConsentForm.Checked) + ", "
                                + Convert.ToInt32(chbRetreatForm.Checked) + ", "
                                + Convert.ToInt32(chbStudentQuestionareForm.Checked) + ", "
                                //+ Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                + 333 + ", "  //Gonna be removing this altogether.. unneeded..RCM..1/27/11.
                                + "'" + ddlVoicePart.Text.Trim() + "', "
                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + Convert.ToInt32(chbDiscipleshipMentor.Checked) + ", "
                                + "'" + txbMiddleName.Text.Trim() + "', "
                                + Convert.ToInt32(chbMailingList.Checked) + ", "
                                + "'" + ddlMailingListCodes.Text.Trim() + "', "
                                //--------new fields...3/21/12.-------------------------------------------------------
                                + Convert.ToInt32(chbPromotionalRelease.Checked) + ", "
                                + Convert.ToInt32(chbPermissionTransport.Checked) + ", "
                                + Convert.ToInt32(chbCAMPDropOff.Checked) + ", "
                                + Convert.ToInt32(chbCAMPPickUp.Checked) + ", "
                                + "'" + txbDropOffPickUp.Text.Trim() + "', "
                                + Convert.ToInt32(chbIncludePromotionalMailing.Checked) + ", "
                                + Convert.ToInt32(chbStudentVolunteer.Checked) + ", "
                                + Convert.ToInt32(chbScrubbed.Checked) + ", "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";
                        }

                        con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                        if (sqlInsertCommand.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful
                            //con.Close();
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
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to create a DUPLICATE of a student that already exists!  Please EXIT the page.  Thankyou  ";
                            throw new Exception(lblErrorMessage.Text);
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = "There was an exception INSERTING NEW data into the ParentGuardian table..  Please fix and try again MSG: " + lkjlaa.Message.ToString();
                            throw new Exception(lblErrorMessage.Text);
                        }
                    }

                    try
                    {
                        string sqlInsertStatement_ParentGuard = "";

                        sqlInsertStatement_ParentGuard = "INSERT into UIF_PerformingArts.dbo.ParentGuardianContactInformation "
                                                        + "values ("
                                                        + "'" + txtLastName.Text.Trim() + "' , "
                                                        + "'" + txtFirstName.Text.Trim() + "' , "
                                                        + "'" + ddlParentGuardian1Relationship.Text.Trim() + "' , "
                                                        + "'" + txtParentGuardian1.Text.Trim() + "' , "
                                                        + "'" + txbParentGuardian1Email.Text.Trim() + "' , "
                                                        + "'" + txbParentGuardian1WrkPh.Text.Trim() + "' , "
                                                        + "'" + txbParentGuardian1CellPhone.Text.Trim() + "' , "
                                                        + "0,"
                                                        + "'" + ddlParentGuardian2Relationship.Text.Trim() + "', "
                                                        + "'" + txbParentGuardian2.Text.Trim() + "' , "
                                                        + "'" + txbParentGuardian2WrkPh.Text.Trim() + "',"
                                                        + "'" + txbParentGuardian2CellPhone.Text.Trim() + "',"
                                                        + "0, "
                                                        + "'" + txbEmergencyRelationship.Text.Trim() + "', "
                                                        + "'" + txbEmergRelationship.Text.Trim() + "', "
                                                        + "'" + txbEmergencyPhone.Text.Trim() + "', "
                                                        + "'" + System.DateTime.Now.ToString() + "', "
                                                        + "'" + System.DateTime.Now.ToString() + "', "
                                                        + "'" + txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim() + ", (" + txbMiddleName.Text.Trim() + ")', "
                                                        + "'" + txbMiddleName.Text.Trim() + "') ";
                        //con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand_ParentGuard = new SqlCommand(sqlInsertStatement_ParentGuard, con);
                        if (sqlInsertCommand_ParentGuard.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful
                            //con.Close();
                        }
                        else
                        {
                            //display message that record was NOT updated
                            //	btnContinue.Visible = false;
                            //	lblAlert.Visible = true;
                            //	lblAlert.Text = "No update. Error has occurred.";
                        }
                    }
                    catch (Exception elkj)
                    {
                        if (elkj.Message.ToString().Contains("duplicate"))
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to create a DUPLICATE of a student that already exists!  Please EXIT the page.  Thankyou  ";
                            throw new Exception(lblErrorMessage.Text);
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = "There was an exception INSERTING NEW data into the ParentGuardian table..  Please fix and try again MSG: " + elkj.Message.ToString();
                            throw new Exception(lblErrorMessage.Text);
                        }
                    }

                    try
                    {
                        string sqlInsertStatement_ProgramsList = "";

                        if (Department == "Athletics")
                        {
                            sqlInsertStatement_ProgramsList = "INSERT into UIF_PerformingArts.dbo.ProgramsList "
                                                            + "values ("
                                                            + "'" + txtLastName.Text.Trim() + "' , "
                                                            + "'" + txtFirstName.Text.Trim() + "' , "
                                                            + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                                            + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                                            + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                                            + "'" + System.DateTime.Now.ToString() + "', "
                                                            + "'" + System.DateTime.Now.ToString() + "', "
                                                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                            + Convert.ToInt32(chbShakes.Checked) + ", "
                                                            + Convert.ToInt32(chbSingers.Checked) + ", "
                                                            + "1, "//Permanent value.
                                                            + "0, "//Permanent value.
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
                                                            + "'" + txbMiddleName.Text.Trim() + "', "
                                                            + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                                            + Convert.ToInt32(chbReadingSupport.Checked) + ") ";
                        }
                        else if (Department == "PerformingArts")
                        {
                            sqlInsertStatement_ProgramsList = "INSERT into UIF_PerformingArts.dbo.ProgramsList "
                                                            + "values ("
                                                            + "'" + txtLastName.Text.Trim() + "' , "
                                                            + "'" + txtFirstName.Text.Trim() + "' , "
                                                            + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                                            + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                                            + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                                            + "'" + System.DateTime.Now.ToString() + "', "
                                                            + "'" + System.DateTime.Now.ToString() + "', "
                                                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                            + Convert.ToInt32(chbShakes.Checked) + ", "
                                                            + Convert.ToInt32(chbSingers.Checked) + ", "
                                                            + "1, "//Permanent value.
                                                            + "0, "//Permanent value.
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
                                                            + "'" + txbMiddleName.Text.Trim() + "', "
                                                            + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                                            + Convert.ToInt32(chbReadingSupport.Checked) + ") ";
                        }
                        else if (Department == "Education")
                        {
                            sqlInsertStatement_ProgramsList = "INSERT into UIF_PerformingArts.dbo.ProgramsList "
                                                            + "values ("
                                                            + "'" + txtLastName.Text.Trim() + "' , "
                                                            + "'" + txtFirstName.Text.Trim() + "' , "
                                                            + Convert.ToInt32(chbMSHSChoir.Checked) + ", "
                                                            + Convert.ToInt32(chbChildrensChoir.Checked) + ", "
                                                            + Convert.ToInt32(chbPerformingArts.Checked) + ", "
                                                            + "'" + System.DateTime.Now.ToString() + "', "
                                                            + "'" + System.DateTime.Now.ToString() + "', "
                                                            + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                            + Convert.ToInt32(chbShakes.Checked) + ", "
                                                            + Convert.ToInt32(chbSingers.Checked) + ", "
                                                            + "1, "//Permanent value.
                                                            + "0, "//Permanent value.
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
                                                            + "'" + txbMiddleName.Text.Trim() + "', "
                                                            + Convert.ToInt32(chbImpactUrbanSchoolsAcademics.Checked) + ", "
                                                            + Convert.ToInt32(chbReadingSupport.Checked) + ") ";
                        }
                        //con.Open();

                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand_ProgramsList = new SqlCommand(sqlInsertStatement_ProgramsList, con);
                        if (sqlInsertCommand_ProgramsList.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful
                            btnSubmitInformation.Enabled = true;//Enable the Update button..RCM.
                            btnNewPerson1.Enabled = false;//Disable the "new person" button...RCM..


                            //Discipleshipmentor turn on..RCM..4/3/12.
                            if (((Request.QueryString["LastName"] == "Sims-Reed") && (Request.QueryString["FirstName"] == "Donna")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Tammy")) || ((Request.QueryString["LastName"] == "Boll") && (Request.QueryString["FirstName"] == "Becky")))
                            {
                                chbDiscipleshipMentor.Enabled = true;
                            }

                            //Options turn on..RCM..4/3/12.
                            if (((Request.QueryString["LastName"] == "Malsch") && (Request.QueryString["FirstName"] == "RuthAnn")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Tammy")) || ((Request.QueryString["LastName"] == "Braunersrither") && (Request.QueryString["FirstName"] == "Chad")))
                            {
                                chbOptionsTurnOn.Enabled = true;
                            }

                            if (Department == "Athletics")
                            {
                                //Enable Athletics Programs.
                                chbMondayNights.Enabled = true;
                                chb3on3Basketball.Enabled = true;
                                chbBasketballTEAMS.Enabled = true;
                                chbBoysOutreachBball.Enabled = true;
                                chbSoccerInterMurals.Enabled = true;
                                chbSoccerLgTravel.Enabled = true;
                                chbGirlsOutreachBball.Enabled = true;
                                chbMSBasketLeague.Enabled = true;
                                chbLittleLeagueBaseball.Enabled = true;
                                chbOliverFootballBible.Enabled = true;
                                chbHSBasketLeague.Enabled = true;
                                chbSpecialEvents.Enabled = true;
                                lbMondayNights.Enabled = true;
                                lb3on3Basketball.Enabled = true;
                                lbBasketballTEAMS.Enabled = true;
                                lbOutreachBasketball.Enabled = true;
                                lbSoccerIntraMurals.Enabled = true;
                                lbSoccerTEAMS.Enabled = true;
                                lbMSBasketballLeague.Enabled = true;
                                lbBaseball.Enabled = true;
                                lbBibleStudy.Enabled = true;
                                lbHSBasketballLeague.Enabled = true;
                                lbSpecialEvents.Enabled = true;
                            }
                            else if (Department == "PerformingArts")
                            {
                                //Enable PerformingArts Programs.
                                chbMSHSChoir.Enabled = true;
                                chbChildrensChoir.Enabled = true;
                                chbPerformingArts.Enabled = true;
                                chbShakes.Enabled = true;
                                chbSingers.Enabled = true;
                                lbClassesEnrollment.Enabled = true;
                            }
                            else if (Department == "Education")
                            {
                                chbSummerDayCamp.Enabled = true;
                                chbSATPrepClass.Enabled = true;
                                txbDropOffPickUp.Enabled = true;
                                lblDropOffPickUp.Enabled = true;
                            }
                        }
                        else
                        {
                            //display message that record was NOT updated
                            //	btnContinue.Visible = false;
                            //	lblAlert.Visible = true;
                            //	lblAlert.Text = "No update. Error has occurred.";
                        }
                    }
                    catch (Exception elkj)
                    {
                        if (elkj.Message.ToString().Contains("duplicate"))
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to create a DUPLICATE of a student that already exists!  Please EXIT the page.  Thankyou ";
                            throw new Exception(lblErrorMessage.Text);
                        }
                    }

                    try
                    {
                        string sqlInsertStatement_StudentMedications = "";
                        sqlInsertStatement_StudentMedications = "INSERT into UIF_PerformingArts.dbo.StudentMedications "
                                                        + "values ("
                                                        + "'" + txtLastName.Text.Trim() + "' , "
                                                        + "'" + txtFirstName.Text.Trim() + "' , "
                                                        + "'" + txbMiddleName.Text.Trim() + "' , "
                                                        + Convert.ToInt32(chbMedication.Checked) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[0].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[1].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[2].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[3].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[4].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[5].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[6].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[7].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[8].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[9].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[10].Selected) + ", "
                                                        + Convert.ToInt32(cblMedications.Items[11].Selected) + ", "
                                                        + "'" + txbMedicationsOtherNotes.Text.Trim() + "' , "
                                                        + "'" + System.DateTime.Now.ToString() + "', "
                                                        + "'" + System.DateTime.Now.ToString() + "', "
                                                        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";

                        //create a SQL command to update record
                        SqlCommand sqlInsertCommand_ProgramsList = new SqlCommand(sqlInsertStatement_StudentMedications, con);
                        if (sqlInsertCommand_ProgramsList.ExecuteNonQuery() > 0)
                        {
                            //maybe display a message confirming update has been successful
                            btnSubmitInformation.Enabled = true;//Enable the Update button..RCM.
                            btnNewPerson1.Enabled = false;//Disable the "new person" button...RCM..

                            //Discipleshipmentor turn on..RCM..4/3/12.
                            if (((Request.QueryString["LastName"] == "Sims-Reed") && (Request.QueryString["FirstName"] == "Donna")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Tammy")) || ((Request.QueryString["LastName"] == "Boll") && (Request.QueryString["FirstName"] == "Becky")))
                            {
                                chbDiscipleshipMentor.Enabled = true;
                            }

                            //Options turn on..RCM..4/3/12.
                            if (((Request.QueryString["LastName"] == "Malsch") && (Request.QueryString["FirstName"] == "RuthAnn")) || ((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")) || ((Request.QueryString["LastName"] == "Glover") && (Request.QueryString["FirstName"] == "Tammy")) || ((Request.QueryString["LastName"] == "Braunersrither") && (Request.QueryString["FirstName"] == "Chad")))
                            {
                                chbOptionsTurnOn.Enabled = true;
                            }

                            if (Department == "Athletics")
                            {
                                //Enable Athletics Programs.
                                chbMondayNights.Enabled = true;
                                chb3on3Basketball.Enabled = true;
                                chbBasketballTEAMS.Enabled = true;
                                chbBoysOutreachBball.Enabled = true;
                                chbSoccerInterMurals.Enabled = true;
                                chbSoccerLgTravel.Enabled = true;
                                chbGirlsOutreachBball.Enabled = true;
                                chbMSBasketLeague.Enabled = true;
                                chbLittleLeagueBaseball.Enabled = true;
                                chbOliverFootballBible.Enabled = true;
                                chbHSBasketLeague.Enabled = true;
                                chbSpecialEvents.Enabled = true;
                                chbImpactUrbanSchools.Enabled = true;
                                lbMondayNights.Enabled = true;
                                lb3on3Basketball.Enabled = true;
                                lbBasketballTEAMS.Enabled = true;
                                lbOutreachBasketball.Enabled = true;
                                lbSoccerIntraMurals.Enabled = true;
                                lbSoccerTEAMS.Enabled = true;
                                lbMSBasketballLeague.Enabled = true;
                                lbBaseball.Enabled = true;
                                lbBibleStudy.Enabled = true;
                                lbHSBasketballLeague.Enabled = true;
                                lbSpecialEvents.Enabled = true;
                                lbImpactUrbanSchools.Enabled = true;
                            }
                            else if (Department == "PerformingArts")
                            {
                                //Enable PerformingArts Programs.
                                chbMSHSChoir.Enabled = true;
                                chbChildrensChoir.Enabled = true;
                                chbPerformingArts.Enabled = true;
                                chbShakes.Enabled = true;
                                chbSingers.Enabled = true;
                                lbClassesEnrollment.Enabled = true;
                                lbMSHSChoir.Enabled = true;
                                lbChildrensChoir.Enabled = true;
                                lbPerformingArtsAcademy.Enabled = true;
                                lbShakes.Enabled = true;
                                lbSingers.Enabled = true;
                                lbImpactUrbanSchoolsPA.Enabled = true;
                                chbImpactUrbanSchoolsPA.Enabled = true;
                            }
                            else if (Department == "Education")
                            {
                                chbSummerDayCamp.Enabled = true;
                                chbSATPrepClass.Enabled = true;
                                txbDropOffPickUp.Enabled = true;
                                lblDropOffPickUp.Enabled = true;
                                chbImpactUrbanSchoolsAcademics.Enabled = true;
                                lbImpactUrbanSchoolsAcademics.Enabled = true;
                                lbSummerDayCamp.Enabled = true;
                                lbAcademicReadingSupport.Enabled = true;
                            }
                        }
                        else
                        {
                            //display message that record was NOT updated
                            //	btnContinue.Visible = false;
                            //	lblAlert.Visible = true;
                            //	lblAlert.Text = "No update. Error has occurred.";
                        }
                    }
                    catch (Exception elkj)
                    {
                        if (elkj.Message.ToString().Contains("duplicate"))
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "";
                            lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to create a DUPLICATE of a student that already exists!  Please EXIT the page.  Thankyou ";
                            throw new Exception(lblErrorMessage.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("duplicate"))
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "";
                    lblErrorMessage.Text = lblErrorMessage.Text + "Error:  You are attempting to create a DUPLICATE of a student that already exists!  Please EXIT the page.  Thankyou  ";
                }
                DisableEntireScreen();
            }
            finally
            {
                //Close connection
                con.Close();    // RCM..1/23/12.
                con.Dispose();
            }
        }

        protected void DisableEntireScreen()
        {
            btnSubmitInformation.Enabled = false;
            btnNewPerson1.Enabled = false;
            cmbClearPage.Enabled = false;

            txbMiddleName.Enabled = false;
            ddlAge.Enabled = false;
            ddlYearBirth.Enabled = false;
            ddlMonthBirth.Enabled = false;
            txtState.Enabled = false;
            ddlCareerGoal.Enabled = false;
            chbDiscipleshipMentor.Enabled = false;
            lbDiscipleshipMentor.Enabled = false;
            chbBibleStudyParticipation.Enabled = false;
            chbBibleOwnership.Enabled = false;
            
            txbParentGuardian1WrkPh.Enabled = false;
            txtParentGuardian1.Enabled = false;
            txbParentGuardian1CellPhone.Enabled = false;
            txbParentGuardian1Email.Enabled = false;
            ddlParentGuardian1Relationship.Enabled = false;
            ddlTextGuard1.Enabled = false;
            txbParentGuardian2.Enabled = false;
            txbParentGuardian2CellPhone.Enabled = false;
            txbParentGuardian2Email.Enabled = false;
            txbParentGuardian2WrkPh.Enabled = false;
            ddlParentGuardian2Relationship.Enabled = false;
            ddlTextGuard2.Enabled = false;
            txtHomePhone.Enabled = false;

            chbStudentVolunteer.Enabled = false;
            chbHaveReceivedChrist.Enabled = false;

            txbEmergencyPhone.Enabled = false;
            txbEmergencyRelationship.Enabled = false;
            txbEmergRelationship.Enabled = false;

            ddlMailingListCodes.Enabled = false;
            chbMailingList.Enabled = false;
            chbIncludePromotionalMailing.Enabled = false;
            txbAllergies.Enabled = false;
            txtAddress1.Enabled = false;
            txtCity.Enabled = false;
            txtZip.Enabled = false;
            txbSoloSong.Enabled = false;
            txtStudentCellPhone.Enabled = false;
            txtStudentEmail.Enabled = false;
            txtLastName.Enabled = false;
            txtFirstName.Enabled = false;
            ddlSchool.Enabled = false;
            ddlChurch.Enabled = false;
            ddlTShirtSize.Enabled = false;
            ddlTextPhone.Enabled = false;
            ddlGender.Enabled = false;
            ddlDayBirth.Enabled = false;
            ddlGrade.Enabled = false;

            txbHealthConditions.Enabled = false;
            txbNotes.Enabled = false;
            chbOptionsTurnOn.Enabled = false;

            chbRegistrationForm.Enabled = false;
            chbRetreatForm.Enabled = false;
            chbStudentQuestionareForm.Enabled = false;
            chbParentalConsentForm.Enabled = false;
            chbPromotionalRelease.Enabled = false;
            chbPermissionTransport.Enabled = false;
            chbMailingList.Enabled = false;

        }

        protected void EnableEntireScreen()
        {
            btnSubmitInformation.Enabled = true;
            //btnNewPerson1.Enabled = true;
            cmbClearPage.Enabled = true;

            txbMiddleName.Enabled = true;
            ddlAge.Enabled = true;
            ddlYearBirth.Enabled = true;
            ddlMonthBirth.Enabled = true;
            txtState.Enabled = true;
            ddlCareerGoal.Enabled = true;
            //chbDiscipleshipMentor.Enabled = true;
            lbDiscipleshipMentor.Enabled = true;
            chbBibleStudyParticipation.Enabled = true;
            chbBibleOwnership.Enabled = true;
            txtHomePhone.Enabled = true;

            txbParentGuardian1WrkPh.Enabled = true;
            txtParentGuardian1.Enabled = true;
            txbParentGuardian1CellPhone.Enabled = true;
            txbParentGuardian1Email.Enabled = true;
            ddlParentGuardian1Relationship.Enabled = true;
            ddlTextGuard1.Enabled = true;
            txbParentGuardian2.Enabled = true;
            txbParentGuardian2CellPhone.Enabled = true;
            txbParentGuardian2Email.Enabled = true;
            txbParentGuardian2WrkPh.Enabled = true;
            ddlParentGuardian2Relationship.Enabled = true;
            ddlTextGuard2.Enabled = true;

            chbStudentVolunteer.Enabled = true;
            chbHaveReceivedChrist.Enabled = true;

            txbEmergencyPhone.Enabled = true;
            txbEmergencyRelationship.Enabled = true;
            txbEmergRelationship.Enabled = true;

            ddlMailingListCodes.Enabled = true;
            chbMailingList.Enabled = true;
            chbIncludePromotionalMailing.Enabled = true;
            txbAllergies.Enabled = true;
            txtAddress1.Enabled = true;
            txtCity.Enabled = true;
            txtZip.Enabled = true;
            txbSoloSong.Enabled = true;
            txtStudentCellPhone.Enabled = true;
            txtStudentEmail.Enabled = true;
            txtLastName.Enabled = true;
            txtFirstName.Enabled = true;
            ddlSchool.Enabled = true;
            ddlChurch.Enabled = true;
            ddlTShirtSize.Enabled = true;
            ddlTextPhone.Enabled = true;
            ddlGender.Enabled = true;
            ddlDayBirth.Enabled = true;
            ddlGrade.Enabled = true;

            txbHealthConditions.Enabled = true;
            txbNotes.Enabled = true;
            //chbOptionsTurnOn.Enabled = false;

            chbRegistrationForm.Enabled = true;
            chbRetreatForm.Enabled = true;
            chbStudentQuestionareForm.Enabled = true;
            chbParentalConsentForm.Enabled = true;
            chbPromotionalRelease.Enabled = true;
            chbPermissionTransport.Enabled = true;
            chbMailingList.Enabled = true;
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
            //if (chbParentGuardianEdit.Checked)
            //{
            //    txbParentGuardian1CellPhone.Enabled = false;
            //    txbParentGuardian1Email.Enabled = false;
            //    ddlParentGuardian1Relationship.Enabled = false;
            //    txbParentGuardian1WrkPh.Enabled = false;
            //    txtParentGuardian1.Enabled = false;
            //    txbParentGuardian2.Enabled = false;
            //    txbParentGuardian2CellPhone.Enabled = false;
            //    txbParentGuardian2Email.Enabled = false;
            //    ddlParentGuardian2Relationship.Enabled = false;
            //    txbParentGuardian2WrkPh.Enabled = false;
            //}
            //else
            //{
            //    txbParentGuardian1CellPhone.Enabled = true;
            //    txbParentGuardian1Email.Enabled = true;
            //    ddlParentGuardian1Relationship.Enabled = true;
            //    txbParentGuardian1WrkPh.Enabled = true;
            //    txtParentGuardian1.Enabled = true;
            //    txbParentGuardian2.Enabled = true;
            //    txbParentGuardian2CellPhone.Enabled = true;
            //    txbParentGuardian2Email.Enabled = true;
            //    ddlParentGuardian2Relationship.Enabled = true;
            //    txbParentGuardian2WrkPh.Enabled = true;
            //}
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
            //Response.Redirect("Options.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
            Response.Redirect("Options.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] +"&StudentLastName=" + txtLastName.Text
                            + "&StudentFirstName=" + txtFirstName.Text + "&StudentMiddleName=" + txbMiddleName.Text);
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
            UpdateStudentTable();

            //Determine which list the student is on before Calling the DiscipleshipMentor page...RCM..2/3/12.
            if (OnWaitingList())
            {
                Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&WaitList=True" + "&StudentLastName=" + txtLastName.Text
                                + "&StudentFirstName=" + txtFirstName.Text + "&StudentMiddleName=" + txbMiddleName.Text);
            }
            else if (ActiveMentee())
            {
                Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"] + "&WaitList=False" + "&StudentLastName=" + txtLastName.Text
                                + "&StudentFirstName=" + txtFirstName.Text + "&StudentMiddleName=" + txbMiddleName.Text);
            }
            else
            {
                Response.Redirect("DiscipleshipMentorProgram.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + "&StudentFirstName=" + "&StudentMiddleName=" + "&Dept=" + Request.QueryString["Dept"]);
            }
        }

        protected Boolean OnWaitingList()
        {
            Boolean WaitingList = false;

            try
            {
                con2.Open();//Opens the db connection.

                string sql1 = "select studentlastname "
                            + "from DiscipleshipMentorProgram "
                            + "where studentlastname = '" + txtLastName.Text.Trim() + "' "
                            + "and studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                            + "and waitinglistinactive = 1 ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql1);
                SqlDataReader reader = null;
                cmd.Connection = con2;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    WaitingList = true;
                }
            }
            catch (Exception lkjaaffccss)
            {
                con2.Close();
            }
            finally
            {
                con2.Close();
            }
            return WaitingList;
        }

        protected Boolean ActiveMentee()
        {
            Boolean ActiveMenteeOrNot = false;
            try
            {
                con2.Open();//Opens the db connection.

                string sql1 = "select studentlastname "
                            + "from DiscipleshipMentorProgram "
                            + "where studentlastname = '" + txtLastName.Text.Trim() + "' "
                            + "and studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                            + "and waitinglistinactive = 0 ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql1);
                SqlDataReader reader = null;
                cmd.Connection = con2;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    ActiveMenteeOrNot = true;
                }
            }
            catch (Exception lkjaaffccgg)
            {
                con2.Close();
            }
            finally
            {
                con2.Close();
            }
            return ActiveMenteeOrNot;
        }

        protected void chbDiscipleshipMentor_CheckedChanged(object sender, EventArgs e)
        {

            if (chbDiscipleshipMentor.Checked)
            {
                //Message("This will enroll this student into the DiscipleshipMentor Program.  Do you wish to proceed?  ", this);
                
                //lbMSHSChoir.Enabled = true;
                //Insert into the Discipleship Mentor Program table...RCM..3/22/11.
                try
                {
                    string sql_Insert = "";

                    sql_Insert = "INSERT into DiscipleshipMentorProgram "
                                                    + "values ("
                                                    + "'" + txtLastName.Text.Trim() + "' , "
                                                    + "'" + txtFirstName.Text.Trim() + "' , "
                                                    + "'N/A' , "
                                                    + "'N/A' , "
                                                    + "'N/A' , "
                                                    + "0, "
                                                    + "'N/A' , "
                                                    + "'" + System.DateTime.Now.ToString() + "', "
                                                    + "'" + System.DateTime.Now.ToString() + "', "
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "0, "
                                                    + "'01-01-2011', "
                                                    + "'01-01-2011', "
                                                    + "0, "
                                                    + "'" + txbMiddleName.Text.Trim() + "') ";
                    con.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand = new SqlCommand(sql_Insert, con);
                    if (sqlInsertCommand.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        //con.Dispose();
                        ///////UpdateStudentTable();  took out...  1/9/12..RCM..
                        chbDiscipleshipMentor.Enabled = false;
                        lbDiscipleshipMentor.Enabled = true;
                    }
                    else
                    {
                        //display message that record was NOT updated
                        //	btnContinue.Visible = false;
                        //	lblAlert.Visible = true;
                        //	lblAlert.Text = "No update. Error has occurred.";
                    }

                    string sql_InsertDescription = "";
                    sql_InsertDescription = "INSERT into DiscipleshipMentorDescription "
                                                    + "values ("
                                                    + "'" + txtLastName.Text.Trim() + "' , "
                                                    + "'" + txtFirstName.Text.Trim() + "' , "
                                                    + "'Beginning Entry' , "
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + txbMiddleName.Text.Trim() + "') ";
                    SqlConnection con2 = new SqlConnection(connectionString);
                    con2.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand2 = new SqlCommand(sql_InsertDescription, con2);
                    if (sqlInsertCommand2.ExecuteNonQuery() > 0)
                    {
                        con2.Close();
                        //UpdateStudentTable();
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
            UpdateStudentTable();
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (lblErrorMessage.Visible)
            {
                //Don't attempt to do the update if there was an error.... RCM..10/15/12.
            }
            else
            {
                UpdateStudentTable();//Update to make sure things get saved...RCM..9/7/11.
            }

            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("uifadmin2.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            if (lblErrorMessage.Visible)
            {
                //Don't attempt to do the update if there was an error.... RCM..10/15/12.
            }
            else
            {
                UpdateStudentTable();//Update to make sure things get saved...RCM..9/7/11.
            }
            Response.Redirect("menutest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
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
            DisableEntireScreen();

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("MondayNights");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbSoccerInterMurals_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbMailingList_CheckedChanged1(object sender, EventArgs e)
        {
            if (chbMailingList.Checked)
            {
                try
                {
                    ddlMailingListCodes.Text = "Use Current Address";
                }
                catch (Exception lkjlaaffbbcc)
                {
                    ddlMailingListCodes.Items.Add("Use Current Address");
                    ddlMailingListCodes.Text = "Use Current Address";
                }
                ddlMailingListCodes.Enabled = false;
            }
            else
            {
                if (!chbMailingList.Checked)
                {
                    ddlMailingListCodes.Items.Remove("Use Current Address");
                    ddlMailingListCodes.Enabled = true;
                }
            }
        }

        protected void chbBasketballTEAMS_CheckedChanged(object sender, EventArgs e)
        {
            //Message message = new mbox();

            if (chbBasketballTEAMS.Checked)
            {

                lblProgramManagement.Visible = true;
                cmbProgramManagement.Visible = true;
                cmbProgramManagement.Enabled = true;
                //pnlProgramManagement2.Visible = true;
                //pnlProgramManagement2.Style.Add("z-index", "9999");

                cblProgramManagement.Visible = true;
                cblProgramManagement.Enabled = true;
                cblProgramManagement.Style.Add("z-index", "99999");

                //chblBasketballTEAMS.Visible = true;
                //chblBasketballTEAMS.Enabled = true;
                //chblBasketballTEAMS.Style.Add("z-index", "99999");
                
                //chbGrade.Enabled = false;
                //ddlDayBirth.Enabled = false;
                //ddlMonthBirth.Enabled = false;
                //ddlYearBirth.Enabled = false;
                //ddlTextPhone.Enabled = false;
                //txbHealthConditions.Enabled = false;
                //ddlVoicePart.Enabled = false;
                //lbDiscipleshipMentor.Enabled = false;
                //lblHealthConditions.Enabled = false;
                //lblVoicePart.Enabled = false;
                //txtCity.Enabled = false;
                //Label22.Enabled = false;
                //txbNotes.Enabled = false;

                //chbMondayNights.Enabled = false;
                //chb3on3Basketball.Enabled = false;
                ////chbBasketballTEAMS.Enabled = false;
                //chbBoysOutreachBball.Enabled = false;
                //chbSoccerInterMurals.Enabled = false;
                //chbSoccerLgTravel.Enabled = false;
                //chbGirlsOutreachBball.Enabled = false;
                //chbMSBasketLeague.Enabled = false;
                //chbLittleLeagueBaseball.Enabled = false;
                //chbOliverFootballBible.Enabled = false;
                //chbHSBasketLeague.Enabled = false;

                //txtLastName.Enabled = false;
                //txtFirstName.Enabled = false;
                //txtAddress1.Enabled = false;
                //ddlSchool.Enabled = false;
                //txtState.Enabled = false;
                //txtStudentEmail.Enabled = false;
                //ddlTShirtSize.Enabled = false;
                //txtZip.Enabled = false;
                //txtHomePhone.Enabled = false;
                //ddlGrade.Enabled = false;
                ////txtGrade.Enabled = false;
                //ddlAge.Enabled = false;
                //ddlGender.Enabled = false;
                //ddlChurch.Enabled = false;
                //txtStudentCellPhone.Enabled = false;
                //txtCareerGoal.Enabled = false;
                //txbSoloSong.Enabled = false;

                //chbAddress.Enabled = false;
                //chbAge.Enabled = false;
                ////chbBibleOwnership.Enabled = false;
                //chbAge.Enabled = false;
                //chbCity.Enabled = false;
                //chbDateBirth.Enabled = false;
                ////chbGender.Enabled = false;
                //chbHomePhone.Enabled = false;
                //chbTShirtSize.Enabled = false;
                //chbSchool.Enabled = false;
                ////chbGender.Enabled = false;
                //chbStudentEmail.Enabled = false;
                //chbStudentCellPhone.Enabled = false;
                //chbFirstName.Enabled = false;
                //chbLastName.Enabled = false;

                //lblStudentInfo.Enabled = false;
                //txbMiddleName.Enabled = false;

                //Label1.Enabled = false;
                //Label3.Enabled = false;
                //Label4.Enabled = false;
                //Label5.Enabled = false;
                //Label6.Enabled = false;
                //Label2.Enabled = false;
                //Label10.Enabled = false;
                //Label7.Enabled = false;
                //Label18.Enabled = false;
                //Label19.Enabled = false;
                //Label20.Enabled = false;
                //Label21.Enabled = false;
                //Label24.Enabled = false;
                //Label25.Enabled = false;
                //Label26.Enabled = false;
                //Label28.Enabled = false;
                //Label29.Enabled = false;
                //Label30.Enabled = false;
                //Label4.Enabled = false;
                //Label8.Enabled = false;
                //Label9.Enabled = false;

                //chbNotes.Visible = true;
                //chbGrade.Visible = true;
                //chbState.Visible = true;
            }
            else
            {
                if (cblProgramManagement.SelectedItem.Selected)
                {
                    cblProgramManagement.SelectedItem.Selected = false;
                }
                cblProgramManagement.Visible = false;

                chbGrade.Enabled = true;
                ddlDayBirth.Enabled = true;
                ddlMonthBirth.Enabled = true;
                ddlYearBirth.Enabled = true;
                ddlTextPhone.Enabled = true;
                txbHealthConditions.Enabled = true;
                ddlVoicePart.Enabled = true;
                lbDiscipleshipMentor.Enabled = true;
                lblHealthConditions.Enabled = true;
                lblVoicePart.Enabled = true;
                txtCity.Enabled = true;
                Label22.Enabled = true;
                txbNotes.Enabled = true;

                chbMondayNights.Enabled = true;
                chb3on3Basketball.Enabled = true;
                //chbBasketballTEAMS.Enabled = true;
                chbBoysOutreachBball.Enabled = true;
                chbSoccerInterMurals.Enabled = true;
                chbSoccerLgTravel.Enabled = true;
                chbGirlsOutreachBball.Enabled = true;
                chbMSBasketLeague.Enabled = true;
                chbLittleLeagueBaseball.Enabled = true;
                chbOliverFootballBible.Enabled = true;
                chbHSBasketLeague.Enabled = true;

                txtLastName.Enabled = true;
                txtFirstName.Enabled = true;
                txtAddress1.Enabled = true;
                ddlSchool.Enabled = true;
                txtState.Enabled = true;
                txtStudentEmail.Enabled = true;
                ddlTShirtSize.Enabled = true;
                txtZip.Enabled = true;
                txtHomePhone.Enabled = true;
                ddlGrade.Enabled = true;
                //txtGrade.Enabled = true;
                ddlAge.Enabled = true;
                ddlGender.Enabled = true;
                ddlChurch.Enabled = true;
                txtStudentCellPhone.Enabled = true;
                txtCareerGoal.Enabled = true;
                txbSoloSong.Enabled = true;

                chbAddress.Enabled = true;
                chbAge.Enabled = true;
                //chbBibleOwnership.Enabled = true;
                chbAge.Enabled = true;
                chbCity.Enabled = true;
                chbDateBirth.Enabled = true;
                //chbGender.Enabled = true;
                chbHomePhone.Enabled = true;
                chbTShirtSize.Enabled = true;
                chbSchool.Enabled = true;
                //chbGender.Enabled = true;
                chbStudentEmail.Enabled = true;
                chbStudentCellPhone.Enabled = true;
                chbFirstName.Enabled = true;
                chbLastName.Enabled = true;
                chbNotes.Visible = true;
                chbGrade.Visible = true;
                chbState.Visible = true;

                lblStudentInfo.Enabled = true;
                txbMiddleName.Enabled = true;

                Label1.Enabled = true;
                Label3.Enabled = true;
                Label4.Enabled = true;
                Label5.Enabled = true;
                Label6.Enabled = true;
                Label2.Enabled = true;
                Label10.Enabled = true;
                Label7.Enabled = true;
                Label18.Enabled = true;
                Label19.Enabled = true;
                Label20.Enabled = true;
                Label21.Enabled = true;
                Label24.Enabled = true;
                Label25.Enabled = true;
                Label26.Enabled = true;
                Label28.Enabled = true;
                Label29.Enabled = true;
                Label30.Enabled = true;
                Label4.Enabled = true;
                Label8.Enabled = true;
                Label9.Enabled = true;
                
                //Message.   Removing this student will remove them from program enrollment.  Do you wish to proceed?"
                
            }
        
        }

        protected void chblBasketballTEAMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cblProgramManagement.SelectedItem.Selected)
            {
                //System.Threading.Thread.Sleep(2000);
                //chblBasketballTEAMS.Visible = false;

                //chbMondayNights.Enabled = true;
                //chb3on3Basketball.Enabled = true;
                ////chbBasketballTEAMS.Enabled = true;
                //chbBoysOutreachBball.Enabled = true;
                //chbSoccerInterMurals.Enabled = true;
                //chbSoccerLgTravel.Enabled = true;
                //chbGirlsOutreachBball.Enabled = true;
                //chbMSBasketLeague.Enabled = true;
                //chbLittleLeagueBaseball.Enabled = true;
                //chbOliverFootballBible.Enabled = true;
                //chbHSBasketLeague.Enabled = true;

                //txtLastName.Enabled = true;
                //txtFirstName.Enabled = true;
                //txtAddress1.Enabled = true;
                //ddlSchool.Enabled = true;
                //txtState.Enabled = true;
                //txtStudentEmail.Enabled = true;
                //ddlTShirtSize.Enabled = true;
                //txtZip.Enabled = true;
                //txtHomePhone.Enabled = true;
                //ddlGrade.Enabled = true;
                ////txtGrade.Enabled = true;
                //ddlAge.Enabled = true;
                //ddlGender.Enabled = true;
                //ddlChurch.Enabled = true;
                //txtStudentCellPhone.Enabled = true;
                //txtCareerGoal.Enabled = true;
                //txbSoloSong.Enabled = true;

                //chbAddress.Enabled = true;
                //chbAge.Enabled = true;
                ////chbBibleOwnership.Enabled = true;
                //chbAge.Enabled = true;
                //chbCity.Enabled = true;
                //chbDateBirth.Enabled = true;
                ////chbGender.Enabled = true;
                //chbHomePhone.Enabled = true;
                //chbTShirtSize.Enabled = true;
                //chbSchool.Enabled = true;
                ////chbGender.Enabled = true;
                //chbStudentEmail.Enabled = true;
                //chbStudentCellPhone.Enabled = true;
                //chbFirstName.Enabled = true;
                //chbLastName.Enabled = true;
                //chbNotes.Visible = true;
                //chbGrade.Visible = true;
                //chbState.Visible = true;

                //lblStudentInfo.Enabled = true;
                //txbMiddleName.Enabled = true;

                //Label1.Enabled = true;
                //Label3.Enabled = true;
                //Label4.Enabled = true;
                //Label5.Enabled = true;
                //Label6.Enabled = true;
                //Label2.Enabled = true;
                //Label10.Enabled = true;
                //Label7.Enabled = true;
                //Label18.Enabled = true;
                //Label19.Enabled = true;
                //Label20.Enabled = true;
                //Label21.Enabled = true;
                //Label24.Enabled = true;
                //Label25.Enabled = true;
                //Label26.Enabled = true;
                //Label28.Enabled = true;
                //Label29.Enabled = true;
                //Label30.Enabled = true;
                //Label4.Enabled = true;
                //Label8.Enabled = true;
                //Label9.Enabled = true;
            }
            else if (!cblProgramManagement.SelectedItem.Selected)
            {



            }
            else
            {


            }
        }

        protected void cmbProgramManagement_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("BasketballTEAMS");
            EnableEntireScreen();
        }

        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtChurch_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chbBasketballTEAMS_CheckedChanged1(object sender, EventArgs e)
        {
            //Message message = new mbox();

            if (chbBasketballTEAMS.Checked)
            {
                lblProgramManagement.Visible = true;
                cmbProgramManagement.Visible = true;
                cmbProgramManagement.Enabled = true;
                //pnlProgramManagement2.Visible = true;
                //pnlProgramManagement2.Style.Add("z-index", "9999");

                cblProgramManagement.Visible = true;
                cblProgramManagement.Enabled = true;
                cblProgramManagement.Style.Add("z-index", "99999");

                //chblBasketballTEAMS.Visible = true;
                //chblBasketballTEAMS.Enabled = true;
                //chblBasketballTEAMS.Style.Add("z-index", "99999");

                //chbGrade.Enabled = false;
                //ddlDayBirth.Enabled = false;
                //ddlMonthBirth.Enabled = false;
                //ddlYearBirth.Enabled = false;
                //ddlTextPhone.Enabled = false;
                //txbHealthConditions.Enabled = false;
                //ddlVoicePart.Enabled = false;
                //lbDiscipleshipMentor.Enabled = false;
                //lblHealthConditions.Enabled = false;
                //lblVoicePart.Enabled = false;
                //txtCity.Enabled = false;
                //Label22.Enabled = false;
                //txbNotes.Enabled = false;


                //chbMondayNights.Enabled = false;
                //chb3on3Basketball.Enabled = false;
                ////chbBasketballTEAMS.Enabled = false;
                //chbBoysOutreachBball.Enabled = false;
                //chbSoccerInterMurals.Enabled = false;
                //chbSoccerLgTravel.Enabled = false;
                //chbGirlsOutreachBball.Enabled = false;
                //chbMSBasketLeague.Enabled = false;
                //chbLittleLeagueBaseball.Enabled = false;
                //chbOliverFootballBible.Enabled = false;
                //chbHSBasketLeague.Enabled = false;


                //txtLastName.Enabled = false;
                //txtFirstName.Enabled = false;
                //txtAddress1.Enabled = false;
                //ddlSchool.Enabled = false;
                //txtState.Enabled = false;
                //txtStudentEmail.Enabled = false;
                //ddlTShirtSize.Enabled = false;
                //txtZip.Enabled = false;
                //txtHomePhone.Enabled = false;
                //ddlGrade.Enabled = false;
                ////txtGrade.Enabled = false;
                //ddlAge.Enabled = false;
                //ddlGender.Enabled = false;
                //ddlChurch.Enabled = false;
                //txtStudentCellPhone.Enabled = false;
                //txtCareerGoal.Enabled = false;
                //txbSoloSong.Enabled = false;

                //chbAddress.Enabled = false;
                //chbAge.Enabled = false;
                ////chbBibleOwnership.Enabled = false;
                //chbAge.Enabled = false;
                //chbCity.Enabled = false;
                //chbDateBirth.Enabled = false;
                ////chbGender.Enabled = false;
                //chbHomePhone.Enabled = false;
                //chbTShirtSize.Enabled = false;
                //chbSchool.Enabled = false;
                ////chbGender.Enabled = false;
                //chbStudentEmail.Enabled = false;
                //chbStudentCellPhone.Enabled = false;
                //chbFirstName.Enabled = false;
                //chbLastName.Enabled = false;

                //lblStudentInfo.Enabled = false;
                //txbMiddleName.Enabled = false;

                //Label1.Enabled = false;
                //Label3.Enabled = false;
                //Label4.Enabled = false;
                //Label5.Enabled = false;
                //Label6.Enabled = false;
                //Label2.Enabled = false;
                //Label10.Enabled = false;
                //Label7.Enabled = false;
                //Label18.Enabled = false;
                //Label19.Enabled = false;
                //Label20.Enabled = false;
                //Label21.Enabled = false;
                //Label24.Enabled = false;
                //Label25.Enabled = false;
                //Label26.Enabled = false;
                //Label28.Enabled = false;
                //Label29.Enabled = false;
                //Label30.Enabled = false;
                //Label4.Enabled = false;
                //Label8.Enabled = false;
                //Label9.Enabled = false;

                //chbNotes.Visible = true;
                //chbGrade.Visible = true;
                //chbState.Visible = true;
            }
            else
            {
                if (cblProgramManagement.SelectedItem.Selected)
                {
                    cblProgramManagement.SelectedItem.Selected = false;
                }
                cblProgramManagement.Visible = false;

                chbGrade.Enabled = true;
                ddlDayBirth.Enabled = true;
                ddlMonthBirth.Enabled = true;
                ddlYearBirth.Enabled = true;
                ddlTextPhone.Enabled = true;
                txbHealthConditions.Enabled = true;
                ddlVoicePart.Enabled = true;
                lbDiscipleshipMentor.Enabled = true;
                lblHealthConditions.Enabled = true;
                lblVoicePart.Enabled = true;
                txtCity.Enabled = true;
                Label22.Enabled = true;
                txbNotes.Enabled = true;

                chbMondayNights.Enabled = true;
                chb3on3Basketball.Enabled = true;
                //chbBasketballTEAMS.Enabled = true;
                chbBoysOutreachBball.Enabled = true;
                chbSoccerInterMurals.Enabled = true;
                chbSoccerLgTravel.Enabled = true;
                chbGirlsOutreachBball.Enabled = true;
                chbMSBasketLeague.Enabled = true;
                chbLittleLeagueBaseball.Enabled = true;
                chbOliverFootballBible.Enabled = true;
                chbHSBasketLeague.Enabled = true;

                txtLastName.Enabled = true;
                txtFirstName.Enabled = true;
                txtAddress1.Enabled = true;
                ddlSchool.Enabled = true;
                txtState.Enabled = true;
                txtStudentEmail.Enabled = true;
                ddlTShirtSize.Enabled = true;
                txtZip.Enabled = true;
                txtHomePhone.Enabled = true;
                ddlGrade.Enabled = true;
                //txtGrade.Enabled = true;
                ddlAge.Enabled = true;
                ddlGender.Enabled = true;
                ddlChurch.Enabled = true;
                txtStudentCellPhone.Enabled = true;
                txtCareerGoal.Enabled = true;
                txbSoloSong.Enabled = true;

                chbAddress.Enabled = true;
                chbAge.Enabled = true;
                //chbBibleOwnership.Enabled = true;
                chbAge.Enabled = true;
                chbCity.Enabled = true;
                chbDateBirth.Enabled = true;
                //chbGender.Enabled = true;
                chbHomePhone.Enabled = true;
                chbTShirtSize.Enabled = true;
                chbSchool.Enabled = true;
                //chbGender.Enabled = true;
                chbStudentEmail.Enabled = true;
                chbStudentCellPhone.Enabled = true;
                chbFirstName.Enabled = true;
                chbLastName.Enabled = true;
                chbNotes.Visible = true;
                chbGrade.Visible = true;
                chbState.Visible = true;

                lblStudentInfo.Enabled = true;
                txbMiddleName.Enabled = true;

                Label1.Enabled = true;
                Label3.Enabled = true;
                Label4.Enabled = true;
                Label5.Enabled = true;
                Label6.Enabled = true;
                Label2.Enabled = true;
                Label10.Enabled = true;
                Label7.Enabled = true;
                Label18.Enabled = true;
                Label19.Enabled = true;
                Label20.Enabled = true;
                Label21.Enabled = true;
                Label24.Enabled = true;
                Label25.Enabled = true;
                Label26.Enabled = true;
                Label28.Enabled = true;
                Label29.Enabled = true;
                Label30.Enabled = true;
                Label4.Enabled = true;
                Label8.Enabled = true;
                Label9.Enabled = true;

                //Message.   Removing this student will remove them from program enrollment.  Do you wish to proceed?"
            }
        }

        protected void chbBasketballTEAMS_CheckedChanged2(object sender, EventArgs e)
        {
            if (chbBasketballTEAMS.Checked)
            {

                lblProgramManagement.Visible = true;
                cmbProgramManagement.Visible = true;
                cmbProgramManagement.Enabled = true;
                //pnlProgramManagement2.Visible = true;
                //pnlProgramManagement2.Style.Add("z-index", "9999");

                cblProgramManagement.Visible = true;
                cblProgramManagement.Enabled = true;
                cblProgramManagement.Style.Add("z-index", "99999");

                //chblBasketballTEAMS.Visible = true;
                //chblBasketballTEAMS.Enabled = true;
                //chblBasketballTEAMS.Style.Add("z-index", "99999");

                //chbGrade.Enabled = false;
                //ddlDayBirth.Enabled = false;
                //ddlMonthBirth.Enabled = false;
                //ddlYearBirth.Enabled = false;
                //ddlTextPhone.Enabled = false;
                //txbHealthConditions.Enabled = false;
                //ddlVoicePart.Enabled = false;
                //lbDiscipleshipMentor.Enabled = false;
                //lblHealthConditions.Enabled = false;
                //lblVoicePart.Enabled = false;
                //txtCity.Enabled = false;
                //Label22.Enabled = false;
                //txbNotes.Enabled = false;


                //chbMondayNights.Enabled = false;
                //chb3on3Basketball.Enabled = false;
                ////chbBasketballTEAMS.Enabled = false;
                //chbBoysOutreachBball.Enabled = false;
                //chbSoccerInterMurals.Enabled = false;
                //chbSoccerLgTravel.Enabled = false;
                //chbGirlsOutreachBball.Enabled = false;
                //chbMSBasketLeague.Enabled = false;
                //chbLittleLeagueBaseball.Enabled = false;
                //chbOliverFootballBible.Enabled = false;
                //chbHSBasketLeague.Enabled = false;


                //txtLastName.Enabled = false;
                //txtFirstName.Enabled = false;
                //txtAddress1.Enabled = false;
                //ddlSchool.Enabled = false;
                //txtState.Enabled = false;
                //txtStudentEmail.Enabled = false;
                //ddlTShirtSize.Enabled = false;
                //txtZip.Enabled = false;
                //txtHomePhone.Enabled = false;
                //ddlGrade.Enabled = false;
                ////txtGrade.Enabled = false;
                //ddlAge.Enabled = false;
                //ddlGender.Enabled = false;
                //ddlChurch.Enabled = false;
                //txtStudentCellPhone.Enabled = false;
                //txtCareerGoal.Enabled = false;
                //txbSoloSong.Enabled = false;

                //chbAddress.Enabled = false;
                //chbAge.Enabled = false;
                ////chbBibleOwnership.Enabled = false;
                //chbAge.Enabled = false;
                //chbCity.Enabled = false;
                //chbDateBirth.Enabled = false;
                ////chbGender.Enabled = false;
                //chbHomePhone.Enabled = false;
                //chbTShirtSize.Enabled = false;
                //chbSchool.Enabled = false;
                ////chbGender.Enabled = false;
                //chbStudentEmail.Enabled = false;
                //chbStudentCellPhone.Enabled = false;
                //chbFirstName.Enabled = false;
                //chbLastName.Enabled = false;

                //lblStudentInfo.Enabled = false;
                //txbMiddleName.Enabled = false;

                //Label1.Enabled = false;
                //Label3.Enabled = false;
                //Label4.Enabled = false;
                //Label5.Enabled = false;
                //Label6.Enabled = false;
                //Label2.Enabled = false;
                //Label10.Enabled = false;
                //Label7.Enabled = false;
                //Label18.Enabled = false;
                //Label19.Enabled = false;
                //Label20.Enabled = false;
                //Label21.Enabled = false;
                //Label24.Enabled = false;
                //Label25.Enabled = false;
                //Label26.Enabled = false;
                //Label28.Enabled = false;
                //Label29.Enabled = false;
                //Label30.Enabled = false;
                //Label4.Enabled = false;
                //Label8.Enabled = false;
                //Label9.Enabled = false;

                //chbNotes.Visible = true;
                //chbGrade.Visible = true;
                //chbState.Visible = true;
            }
            else
            {
                if (cblProgramManagement.SelectedItem.Selected)
                {
                    cblProgramManagement.SelectedItem.Selected = false;
                }
                cblProgramManagement.Visible = false;

                chbGrade.Enabled = true;
                ddlDayBirth.Enabled = true;
                ddlMonthBirth.Enabled = true;
                ddlYearBirth.Enabled = true;
                ddlTextPhone.Enabled = true;
                txbHealthConditions.Enabled = true;
                ddlVoicePart.Enabled = true;
                lbDiscipleshipMentor.Enabled = true;
                lblHealthConditions.Enabled = true;
                lblVoicePart.Enabled = true;
                txtCity.Enabled = true;
                Label22.Enabled = true;
                txbNotes.Enabled = true;

                chbMondayNights.Enabled = true;
                chb3on3Basketball.Enabled = true;
                //chbBasketballTEAMS.Enabled = true;
                chbBoysOutreachBball.Enabled = true;
                chbSoccerInterMurals.Enabled = true;
                chbSoccerLgTravel.Enabled = true;
                chbGirlsOutreachBball.Enabled = true;
                chbMSBasketLeague.Enabled = true;
                chbLittleLeagueBaseball.Enabled = true;
                chbOliverFootballBible.Enabled = true;
                chbHSBasketLeague.Enabled = true;

                txtLastName.Enabled = true;
                txtFirstName.Enabled = true;
                txtAddress1.Enabled = true;
                ddlSchool.Enabled = true;
                txtState.Enabled = true;
                txtStudentEmail.Enabled = true;
                ddlTShirtSize.Enabled = true;
                txtZip.Enabled = true;
                txtHomePhone.Enabled = true;
                ddlGrade.Enabled = true;
                //txtGrade.Enabled = true;
                ddlAge.Enabled = true;
                ddlGender.Enabled = true;
                ddlChurch.Enabled = true;
                txtStudentCellPhone.Enabled = true;
                txtCareerGoal.Enabled = true;
                txbSoloSong.Enabled = true;

                chbAddress.Enabled = true;
                chbAge.Enabled = true;
                //chbBibleOwnership.Enabled = true;
                chbAge.Enabled = true;
                chbCity.Enabled = true;
                chbDateBirth.Enabled = true;
                //chbGender.Enabled = true;
                chbHomePhone.Enabled = true;
                chbTShirtSize.Enabled = true;
                chbSchool.Enabled = true;
                //chbGender.Enabled = true;
                chbStudentEmail.Enabled = true;
                chbStudentCellPhone.Enabled = true;
                chbFirstName.Enabled = true;
                chbLastName.Enabled = true;
                chbNotes.Visible = true;
                chbGrade.Visible = true;
                chbState.Visible = true;

                lblStudentInfo.Enabled = true;
                txbMiddleName.Enabled = true;

                Label1.Enabled = true;
                Label3.Enabled = true;
                Label4.Enabled = true;
                Label5.Enabled = true;
                Label6.Enabled = true;
                Label2.Enabled = true;
                Label10.Enabled = true;
                Label7.Enabled = true;
                Label18.Enabled = true;
                Label19.Enabled = true;
                Label20.Enabled = true;
                Label21.Enabled = true;
                Label24.Enabled = true;
                Label25.Enabled = true;
                Label26.Enabled = true;
                Label28.Enabled = true;
                Label29.Enabled = true;
                Label30.Enabled = true;
                Label4.Enabled = true;
                Label8.Enabled = true;
                Label9.Enabled = true;

                //Message.   Removing this student will remove them from program enrollment.  Do you wish to proceed?"

            }
        }

        protected void chbBasketballTEAMS_CheckedChanged3(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            rblBaseball.Visible = false;
            cmbBaseballCancel.Visible = false;
            cmbBaseball.Visible = false;
            DisableEntireScreen();

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

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
            try
            {
                string sql_DeleteFromBasketballTEAMS = "Delete from UIF_PerformingArts.dbo.BasketballTEAMSEnrollment "
                                                     + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                     + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                     + "AND student = 1 ";
                                                     //+"AND sectionname = '" + cblProgramManagement.SelectedItem.Selected + "' ";
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
            cmbPAAConfirm.Enabled = false;
            
            cmbSummerDayCampConfirm.Enabled = false;
            cmbImpactUrbanSchoolsConfirm.Enabled = false;
            cmbAcademicReadingSupportConfirm.Enabled = false;
        }
        
        protected void chbBoysOutreachBball_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();

            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            //rblBaseball.Visible = false;
            //cmbBaseball.Visible = false;
            //cmbBaseballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            DisableEntireScreen();
            
            if (chbBoysOutreachBball.Checked)
            {
                //cmbOutreachBasketball.Enabled = false;
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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Outreach Basketball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbGirlsOutreachBball_CheckedChanged(object sender, EventArgs e)
        {

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
                       + "and student = 1 "
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
                //cblProgramManagement.Items.FindByText(myDataRowPO[0].ToString()).Enabled = false;

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
                    cmbMSHSChoirConfirm.Enabled = true;
                }
                else if (Program == "ChildrensChoir")
                {
                    cmbChildrensChoirConfirm.Enabled = true;
                }
                else if (Program == "Singers")
                {
                    cmbSingersConfirm.Enabled = true;
                }
                else if (Program == "Shakes")
                {
                    cmbShakesConfirm.Enabled = true;
                }
                else if (Program == "PAA")
                {
                    cmbPAAConfirm.Enabled = true;
                }
                else if (Program == "SummerDayCamp")
                {
                    cmbSummerDayCampConfirm.Enabled = true;
                }
                else if (Program == "ImpactUrbanSchools")
                {
                    cmbImpactUrbanSchoolsConfirm.Enabled = true;
                }
                else if (Program == "AcademicReadingSupport")
                {
                    cmbAcademicReadingSupportConfirm.Enabled = true;
                }
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
                       + "and student = 1 "
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
                else if (Program == "PAA")
                {
                    cmbPAARemove.Enabled = true;
                }
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
                       + "and student = 1 "
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
                else if (Program == "PAA")
                {
                    cmbPAARemove.Enabled = true;
                }
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
            }
            custDS2.Clear();
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
            DisableEntireScreen();

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Baseball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
        }
        
        protected void txtChurch_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void cmbConfirmDelete_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("BasketballTEAMS");
            UpdateStudentTable();
            System.Threading.Thread.Sleep(750);//Wait 2 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbConfirmDelete.Visible = false;
            cmbProgramManageCancel.Visible = false;
            EnableEntireScreen();
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
            cmbSpecialEventsConfirm.Enabled = true;
        }

        protected void chbSoccerInterMurals_CheckedChanged1(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("SoccerIntraMurals");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void cmbDeletePerformingArts_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromClasses();
            UpdateStudentTable();
            System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbDeletePerformingArts.Visible = false;
            cmbCancelPerformArts.Visible = false;
            EnableEntireScreen();
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
            lbClassesEnrollment.Enabled = true;
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
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
            DisableEntireScreen();

            if (chbMSBasketLeague.Checked)
            {
                cmbMSBasketballLeagueConfirm.Visible = true;
                cmbMSBasketballLeagueConfirm.Style.Add("z-index", "99999");

                cmbMSBasketballLeagueCancel.Enabled = true;
                cmbMSBasketballLeagueCancel.Visible = true;
                cmbMSBasketballLeagueCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("MS Basketball League");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
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
            DisableEntireScreen();

            if (chbHSBasketLeague.Checked)
            {
                cmbHSBasketballLeagueConfirm.Visible = true;
                cmbHSBasketballLeagueConfirm.Style.Add("z-index", "99999");

                cmbHSBasketballLeagueCancel.Enabled = true;
                cmbHSBasketballLeagueCancel.Visible = true;
                cmbHSBasketballLeagueCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("HS Basketball League");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void txtStudentCellPhone_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmbOutreachBasketball_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Outreach Basketball");
            EnableEntireScreen();
        }

        protected void cmbOutreachBasketballCancel_Click(object sender, EventArgs e)
        {
            //OutreachBasketball Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("Outreach Basketball");
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbConfirmDeleteOutreach.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void DeleteOutReachBasketball()
        {
            try
            {
                string sql_DeleteFromOutreachBasketball = "Delete from UIF_PerformingArts.dbo.OutreachBasketballEnrollment "
                                                        + "WHERE studentlastname = '" + txtLastName.Text.Trim() + "' "
                                                        + "AND studentfirstname = '" + txtFirstName.Text.Trim() + "' "
                                                        + "AND sectionname = '" + rblOutreachBasketball.SelectedItem.Text + "' "
                                                        + "AND student = 1 ";
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

        protected void cmbConfirmDeleteOutreach_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("Outreach Basketball");
            UpdateStudentTable();
            System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbConfirmDeleteOutreach.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            EnableEntireScreen();
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

        protected void chbSoccerLgTravel_CheckedChanged(object sender, EventArgs e)
        {//Really renamed as "SoccerTEAMS"...RCM..3/21/12.
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbSoccerLgTravel.Checked)
            {
                cmbSoccerTEAMSConfirm.Visible = true;
                cmbSoccerTEAMSConfirm.Style.Add("z-index", "99999");

                cmbSoccerTEAMSCancel.Enabled = true;
                cmbSoccerTEAMSCancel.Visible = true;
                cmbSoccerTEAMSCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("SoccerTEAMS");
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
            DisableEntireScreen();
            
            if (chbOliverFootballBible.Checked)
            {
                cmbBibleStudy.Visible = true;
                cmbBibleStudy.Style.Add("z-index", "99999");

                cmbBibleStudyCancel.Enabled = true;
                cmbBibleStudyCancel.Visible = true;
                cmbBibleStudyCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Bible Study");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
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

            DisableEntireScreen();

            if (chb3on3Basketball.Checked)
            {
                cmb3on3BasketballConfirm.Visible = true;
                cmb3on3BasketballConfirm.Style.Add("z-index", "99999");

                cmb3on3BasketballCancel.Enabled = true;
                cmb3on3BasketballCancel.Visible = true;
                cmb3on3BasketballCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("3on3 Basketball");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void chbOptionsTurnOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chbOptionsTurnOn.Checked)
            {
                //Insert into the Options Program table...RCM..2/29/12.
                try
                {
                    string sql_Insert = "";

                    sql_Insert = "INSERT into OptionsProgramNEW "
                                                    + "values ("
                                                    + "'" + txtLastName.Text.Trim() + "', "
                                                    + "'" + txtFirstName.Text.Trim() + "', "
                                                    + "'" + txbMiddleName.Text.Trim() + "', "
                                                    + "'Select a Bus', "
                                                    + "0, "
                                                    + "0, "
                                                    + "0, "
                                                    + "0, "
                                                    + "0, "
                                                    + "0, "
                                                    + "'GPA', "
                                                    + "'01-01-1990', "//CHANGE THIS DEFAULT.
                                                    + "0, "
                                                    + "'01-01-2011', "//CHANGE THIS DEFAULT.
                                                    + "0, "
                                                    + "'01-01-2011', "//CHANGE THIS DEFAULT.
                                                    + "'Comments' , "
                                                    + "'" + System.DateTime.Now.ToString() + "', "
                                                    + "'" + System.DateTime.Now.ToString() + "', "
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "') ";


                    //sql_Insert = "INSERT into OptionsProgram "
                    //                                + "values ("
                    //                                + "'" + txtLastName.Text.Trim() + "' , "
                    //                                + "'" + txtFirstName.Text.Trim() + "' , "
                    //                                + "'N/A' , "
                    //                                + "0, "
                    //                                + "0, "
                    //                                + "'01-01-2011', "
                    //                                + "0, "
                    //                                + "'01-01-2011', "
                    //                                + "'N/A' , "
                    //                                + "'ProgramEnrollment' , "
                    //                                + "0, "
                    //                                + "'N/A' , "
                    //                                + "'" + System.DateTime.Now.ToString() + "', "
                    //                                + "'" + System.DateTime.Now.ToString() + "', "
                    //                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                    //                                + "'N/A', "
                    //                                + "0, "
                    //                                + "0, "
                    //                                + "'" + txbMiddleName.Text.Trim() + "') ";
                    con.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand = new SqlCommand(sql_Insert, con);
                    if (sqlInsertCommand.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        //con.Dispose();
                        ///////UpdateStudentTable();  took out...  1/9/12..RCM..
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
                    sql_InsertDescription = "INSERT into OptionsDescription "
                                                    + "values ("
                                                    + "'" + txtLastName.Text.Trim() + "' , "
                                                    + "'" + txtFirstName.Text.Trim() + "' , "
                                                    + "'Beginning Entry' , "
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + System.DateTime.Now.ToString() + "',"
                                                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                                    + "'" + txbMiddleName.Text.Trim() + "') ";
                    SqlConnection con2 = new SqlConnection(connectionString);
                    con2.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand2 = new SqlCommand(sql_InsertDescription, con2);
                    if (sqlInsertCommand2.ExecuteNonQuery() > 0)
                    {
                        con2.Close();
                        //UpdateStudentTable();
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
                    lbOptionsProgram.Enabled = true;
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

        protected void chbSATPrepClass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbNewStudentFlag.Checked == true)
            {
                //Do Nothing.                
            }
            else
            {
                bool ValidUpdate = UpdateStudentTable();
            }
        }

        protected void chbPermissionTransport_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chbPromotionalRelease_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void cmbRemoveBaseball_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("Baseball");
            UpdateStudentTable();
            System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbRemoveBaseball.Visible = false;
            cmbBaseballCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void rblBaseball_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chbLittleLeagueBaseball.Checked)
            {
                cmbBaseball.Enabled = true;
            }
            else
            {
                if (!chbLittleLeagueBaseball.Checked)
                {
                    cmbRemoveBaseball.Enabled = true;
                }
            }
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
                                                                    + "'" + txbMiddleName.Text.Trim() + "', "
                                                                    + "1, "
                                                                    + "0, "
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
                                                                    + "'" + txbMiddleName.Text.Trim() + "', "
                                                                    + "1, "
                                                                    + "0, "
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
                                    //else if (Program == "PAA")
                                    //{
                                    //    chbPerformingArts.Checked = true;
                                    //}
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
                                        else if (Department == "Academics")
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
                                        chbReadingSupport.Checked = true;
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
                                        else if (Department == "Academics")
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
                                        chbReadingSupport.Checked = false;
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
                    lblErrorMessage.Enabled = true;
                    lblErrorMessage.Text = "There was an exception INSERTING NEW data into the tables..  Please fix and try again MSG: " + ex.Message.ToString();

                    //add code to send error to admin via email
                    //Session["Exception"] = ex.Message.ToString();
                    //Response.Redirect("Error.aspx");
                }
                finally
                {
                    //Close connection
                    con2.Close();
                    con2.Dispose();
                    GoodUpdate = UpdateStudentTable();//Automatically do the update.
                }
            }
            else
            {

            }
        }

        protected void cmbBaseball_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Baseball");
            EnableEntireScreen();
        }

        protected void cmbBaseballCancel_Click(object sender, EventArgs e)
        {
            //Baseball Cancel option..RCM..9/28/11.
            Boolean GoodUpdate = false;
            DetermineCheckBoxStatus("Baseball");
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbBaseball.Visible = false;
            cmbRemoveBaseball.Visible = false;
            cmbBaseballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void chbIncludePromotionalMailing_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void cmbSoccerIntraMuralsConfirm_Click(object sender, EventArgs e)
        {

            AddStudentsToProgram("SoccerIntraMurals");
            EnableEntireScreen();
        }

        protected void cmbSoccerTEAMSConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("SoccerTEAMS");
            EnableEntireScreen();
        }

        protected void cmbMondayNightsConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("MondayNights");
            EnableEntireScreen();
        }

        protected void cmbHSBasketballLeagueConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("HS Basketball League");
            EnableEntireScreen();
        }

        protected void cmb3on3BasketballConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("3on3 Basketball");
            EnableEntireScreen();
        }

        protected void cmbBibleStudy_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Bible Study");
            EnableEntireScreen();
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

            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmbHSBasketballLeagueCancel_Click(object sender, EventArgs e)
        {
            //HS Basketball League Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("HS Basketball League");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbHSBasketballLeagueCancel.Visible = false;
            cmbHSBasketballLeagueConfirm.Visible = false;
            cmbHSBasketballLeagueRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmbMSBasketballLeagueCancel_Click(object sender, EventArgs e)
        {
            //MS Basketball League Cancel option..RCM..9/28/11.

            Boolean GoodUpdate = false;

            DetermineCheckBoxStatus("MS Basketball League");

            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cmbMSBasketballLeagueCancel.Visible = false;
            cmbMSBasketballLeagueConfirm.Visible = false;
            cmbMSBasketballLeagueRemove.Visible = false;
            cblProgramManagement.Visible = false;

            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
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

            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
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

            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
        }

        protected void cmb3on3BasketballRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("3on3 Basketball");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmb3on3BasketballCancel.Visible = false;
            cmb3on3BasketballRemove.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbBibleStudyRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("Bible Study");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbBibleStudyRemove.Visible = false;
            cmbBibleStudyCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbHSBasketballLeagueRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("HS Basketball League");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbHSBasketballLeagueCancel.Visible = false;
            cmbHSBasketballLeagueRemove.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbMSBasketballLeagueRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("MS Basketball League");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbMSBasketballLeagueRemove.Visible = false;
            cmbMSBasketballLeagueCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbOutreachBasketballRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("Outreach Basketball");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbOutreachBasketballRemove.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbSoccerIntraMuralsRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("SoccerIntraMurals");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbSoccerIntraMuralsRemove.Visible = false;
            cmbSoccerIntraMuralsCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbSoccerTEAMSRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("SoccerTEAMS");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbSoccerTEAMSRemove.Visible = false;
            cmbSoccerTEAMSCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbMSBasketballLeagueConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("MS Basketball League");
            EnableEntireScreen();
        }

        protected void cmbMondayNightsRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("MondayNights");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbMondayNightsRemove.Visible = false;
            cmbMondayNightsCancel.Visible = false;
            EnableEntireScreen();
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
            DisableEntireScreen();

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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("Special Events");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void cmbSpecialEventsRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("Special Events");
            UpdateStudentTable();
            //System.Threading.Thread.Sleep(750);//Wait .75 seconds before disappearing..RCM.
            lblProgramManagement.Visible = false;
            pnlProgramManagement.Visible = false;
            cblProgramManagement.Visible = false;
            cmbSpecialEventsRemove.Visible = false;
            cmbSpecialEventsCancel.Visible = false;
            EnableEntireScreen();
        }

        protected void cmbSpecialEventsConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("Special Events");
            EnableEntireScreen();
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

            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
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
            cmbPAAConfirm.Enabled = true;
            cmbSingersConfirm.Enabled = true;
            cmbShakesConfirm.Enabled = true;

            cmbImpactUrbanSchoolsConfirm.Enabled = true;
            cmbSummerDayCampConfirm.Enabled = true;
            cmbAcademicReadingSupportConfirm.Enabled = true;
        }

        protected void rblProgramManagement_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            DisableEntireScreen();
            
            if (chbLittleLeagueBaseball.Checked)
            {
                //Add student to another section.
                cmbBaseball.Visible = true;
                cmbBaseball.Style.Add("z-index", "99999");

                cmbBaseballCancel.Enabled = true;
                cmbBaseballCancel.Visible = true;
                cmbBaseballCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
            DisableEntireScreen();

            if (chbOliverFootballBible.Checked)
            {
                //Add student to another section.
                cmbBibleStudy.Visible = true;
                cmbBibleStudy.Style.Add("z-index", "99999");

                cmbBibleStudyCancel.Enabled = true;
                cmbBibleStudyCancel.Visible = true;
                cmbBibleStudyCancel.Style.Add("z-index", "99999");

                lblProgramManagement.Style.Add("z-index", "99999");
                lblProgramManagement.Visible = true;
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

                    PopulateRadioButtonLists("Special Events");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }

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
            DisableEntireScreen();
            
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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
            DisableEntireScreen();

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    cmbProgramManagement.Visible = true;
                    cmbProgramManagement.Style.Add("z-index", "99999");

                    cmbProgramManageCancel.Enabled = true;
                    cmbProgramManageCancel.Visible = true;
                    cmbProgramManageCancel.Style.Add("z-index", "99999");

                    lblProgramManagement.Style.Add("z-index", "99999");
                    lblProgramManagement.Visible = true;
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
            DisableEntireScreen();

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
            DisableEntireScreen();

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

                    PopulateRadioButtonLists("HS Basketball League");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
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
            DisableEntireScreen();

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
            DisableEntireScreen();

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
            DisableEntireScreen();

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

                    PopulateRadioButtonLists("3on3 Basketball");
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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("ImpactUrbanSchools");
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
            if (!ProgramFull("ChildrensChoir"))
            {
                AddStudentsToProgram("ChildrensChoir");
                EnableEntireScreen();
            }
            else
            {
                lblErrorMessage.Text = "This Program IS FULL.  The Student will not be added";
                lblErrorMessage.Visible = true;
                System.Threading.Thread.Sleep(1000);//Wait 1 second before disappearing..RCM.
                lblErrorMessage.Visible = false;
                EnableEntireScreen();
            }
        }

        protected Boolean ProgramFull(string Program)
        {
            Boolean ProgramFull = false;

            try
            {
                con2.Open();//Opens the db connection.

                string sql1 = "select COUNT(*) "
                            + "from " + Program + "Enrollment "
                            + "where Student = 1 ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql1);
                SqlDataReader reader = null;
                cmd.Connection = con2;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only...The count.

                    //ChildrensChoir capped at 85.. (better way to do this, like table driven)
                    if ((Program == "ChildrensChoir") && (System.Convert.ToInt32(reader.GetSqlValue(0).ToString()) >= 28))
                    {
                        ProgramFull = true;
                    }
                }
            }
            catch (Exception lkjklabb)
            {

            }
            finally
            {
                con2.Close();
            }
            return ProgramFull;
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

        protected void cmbMSHSChoirRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("MSHSChoir");
            UpdateStudentTable();
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
            RemoveStudentsFromProgram("ChildrensChoir");
            UpdateStudentTable();
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
            RemoveStudentsFromProgram("Singers");
            UpdateStudentTable();
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
            RemoveStudentsFromProgram("Shakes");
            UpdateStudentTable();
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
            RemoveStudentsFromProgram("PAA");
            UpdateStudentTable();
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
            RemoveStudentsFromProgram("SummerDayCamp");
            UpdateStudentTable();
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
            RemoveStudentsFromProgram("ImpactUrbanSchools");
            UpdateStudentTable();
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

                    PopulateRadioButtonLists("ChildrensChoir");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void lbPerformingArtsAcademy_Click(object sender, EventArgs e)
        {
            UpdateStudentTable();
            //Response.Redirect("PerformingArtsClasses.aspx?    "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));
            Response.Redirect("PerformingArtsClasses.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + txtLastName.Text + "&StudentFirstName=" + txtFirstName.Text + "&StudentMiddleName=" + txbMiddleName.Text + "&Dept=" + Request.QueryString["Dept"] + "&PerformingArts=" + Convert.ToInt32(chbPerformingArts.Checked));

            //DisableAllConfirmButtons();
            //rblOutreachBasketball.Visible = false;
            //cmbOutreachBasketball.Visible = false;
            //cmbOutreachBasketballCancel.Visible = false;
            //cblProgramManagement.Visible = false;
            //cmbProgramManageCancel.Visible = false;
            //cmbProgramManagement.Visible = false;
            //DisableEntireScreen();

            //if (chbPerformingArts.Checked)
            //{
            //    Add student to another section.
            //    cmbPAAConfirm.Visible = true;
            //    cmbPAAConfirm.Style.Add("z-index", "99999");

            //    cmbPAACancel.Enabled = true;
            //    cmbPAACancel.Visible = true;
            //    cmbPAACancel.Style.Add("z-index", "99999");

            //    lblProgramManagement.Style.Add("z-index", "99999");
            //    lblProgramManagement.Visible = true;
            //    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

            //    PopulateRadioButtonListsAndChoiceForAdds("PAA");
            //    cblProgramManagement.Style.Add("z-index", "99999");
            //    cblProgramManagement.Visible = true;

            //    pnlProgramManagement.Style.Add("z-index", "9999");
            //    pnlProgramManagement.Visible = true;
            //}
            //else
            //{
            //    if (!chbPerformingArts.Checked)
            //    {
            //        Act like a normal add..
            //        cmbPAAConfirm.Visible = true;
            //        cmbPAAConfirm.Style.Add("z-index", "99999");

            //        cmbPAACancel.Enabled = true;
            //        cmbPAACancel.Visible = true;
            //        cmbPAACancel.Style.Add("z-index", "99999");

            //        lblProgramManagement.Style.Add("z-index", "99999");
            //        lblProgramManagement.Visible = true;
            //        lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

            //        PopulateRadioButtonLists("PAA");
            //        cblProgramManagement.Style.Add("z-index", "99999");
            //        cblProgramManagement.Visible = true;

            //        pnlProgramManagement.Style.Add("z-index", "9999");
            //        pnlProgramManagement.Visible = true;
            //    }
            //}
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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

                    PopulateRadioButtonLists("Singers");
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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("ImpactUrbanSchools");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
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
                    lblProgramManagement.Text = "This will REMOVE the individual from this program/section(s)!";

                    PopulateRadioButtonListsAndChoice("ImpactUrbanSchools");
                    cblProgramManagement.Style.Add("z-index", "99999");
                    cblProgramManagement.Visible = true;

                    pnlProgramManagement.Style.Add("z-index", "9999");
                    pnlProgramManagement.Visible = true;
                }
            }
        }

        protected void ClearMedications()
        {
            try
            {
                //Update the ParentGuardian information...RCM..10/7/10.
                string sqlUpdateStatement_StudentMedications = "";

                //if ((Request.QueryString["StudentLastName"] == "") || (Request.QueryString["StudentFirstName"] == ""))
                if ((Request.QueryString["newstudent"] == "newstudent"))
                {
                    sqlUpdateStatement_StudentMedications = " UPDATE UIF_PerformingArts.dbo.StudentMedications " +
                        "SET "
                        + " administeryesno = 0, "
                        + " aspirin = 0, "
                        + " tylenol = 0, "
                        + " ibuprofen = 0, "
                        + " advil = 0, "
                        + " antacids = 0, "
                        + " benadryl = 0, "
                        + " [antiseptic ointment] = 0, "
                        + " [anesthetic ointment] = 0, "
                        + " iodinepreppad = 0, "
                        + " acetaminophen = 0, "
                        + " rubbingalcohol = 0, "
                        + " other = 0, "
                        + " othernotes = 'N/A', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                        + " "
                        + " WHERE lastname = '" + txtLastName.Text.Trim() + "' "
                        + " AND firstname = '" + txtFirstName.Text.Trim() + "' ";
                }
                else
                {
                    sqlUpdateStatement_StudentMedications = " UPDATE UIF_PerformingArts.dbo.StudentMedications " +
                        "SET "
                        + " administeryesno = 0, "
                        + " aspirin = 0, "
                        + " tylenol = 0, "
                        + " ibuprofen = 0, "
                        + " advil = 0, "
                        + " antacids = 0, "
                        + " benadryl = 0, "
                        + " [antiseptic ointment] = 0, "
                        + " [anesthetic ointment] = 0, "
                        + " iodinepreppad = 0, "
                        + " acetaminophen = 0, "
                        + " rubbingalcohol = 0, "
                        + " other = 0, "
                        + " othernotes = 'N/A', "
                        + " sysupdate = '" + System.DateTime.Now.ToString() + "' "
                        + " "
                        + " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                        + " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' ";
                }
                con.Open();
                //create a SQL command to update record
                SqlCommand sqlUpdateCommand_ProgramsList = new SqlCommand(sqlUpdateStatement_StudentMedications, con);
                if (sqlUpdateCommand_ProgramsList.ExecuteNonQuery() > 0)
                {
                    cblMedications.ClearSelection();
                    txbMedicationsOtherNotes.Text = "N/A";
                }
                else
                {
                    //Didn't find a record to update..RCM.
                }
            }
            catch (Exception lkjlk)
            {
                lblErrorMessage.Enabled = true;
                lblErrorMessage.Text = "The update to StudentMedications table failed.  Please fix and try again MSG: " + lkjlk.Message.ToString();
                throw new Exception("The Update to the StudentMedications table failed.   Please fix and try again MSG: " + lkjlk.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        protected void chbMedication_CheckedChanged(object sender, EventArgs e)
        {
            if (chbMedication.Checked)
            {
                cblMedications.Enabled = true;
                txbMedicationsOtherNotes.Enabled = true;
            }
            else
            {
                if (!chbMedication.Checked)
                {
                    cblMedications.Enabled = false;
                    txbMedicationsOtherNotes.Enabled = false;
                    ClearMedications();
                }
            }
        }

        protected void lbImpactUrbanSchools_Click(object sender, EventArgs e)
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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                        lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                        lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                    lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

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
                        lblProgramManagement.Text = "To which section(s) of the Program, would you like to ADD the student.  Please choose.";

                        PopulateRadioButtonLists("ImpactUrbanSchools");
                        cblProgramManagement.Style.Add("z-index", "99999");
                        cblProgramManagement.Visible = true;

                        pnlProgramManagement.Style.Add("z-index", "9999");
                        pnlProgramManagement.Visible = true;
                    }
                }
            }
        }

        protected void chbScrubbed_CheckedChanged(object sender, EventArgs e)
        {
            if (chbScrubbed.Checked)
            {
                chbScrubbed.Text = "";
                chbScrubbed.Enabled = false;
                lblLastScrubbed.Text = "Last Scrubbed By: " + Request.QueryString["LastName"] + "," + Request.QueryString["FirstName"] + " On: " + DateTime.Now.ToString();
                lblLastScrubbed.Enabled = true;
                lblLastScrubbed.Visible = true;
                UpdateScrubbedField();
                UpdateStudentTable();            
            }
        }

        protected void chbReadingSupport_CheckedChanged(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;
            DisableEntireScreen();

            if (chbReadingSupport.Checked)
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
                if (!chbReadingSupport.Checked)
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

        protected void lbAcademicReadingSupport_Click(object sender, EventArgs e)
        {
            DisableAllConfirmButtons();
            rblOutreachBasketball.Visible = false;
            cmbOutreachBasketball.Visible = false;
            cmbOutreachBasketballCancel.Visible = false;
            cblProgramManagement.Visible = false;
            cmbProgramManageCancel.Visible = false;
            cmbProgramManagement.Visible = false;

            if (chbReadingSupport.Checked)
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
                if (!chbReadingSupport.Checked)
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

        protected void cmbAcademicReadingSupportConfirm_Click(object sender, EventArgs e)
        {
            AddStudentsToProgram("AcademicReadingSupport");
            EnableEntireScreen();
        }

        protected void cmbAcademicReadingSupportRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentsFromProgram("AcademicReadingSupport");
            UpdateStudentTable();
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
            GoodUpdate = UpdateStudentTable();//Automatically do the update.
            EnableEntireScreen();
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

        protected void txbMiddleName_TextChanged(object sender, EventArgs e)
        {
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txbMiddleName.Enabled = false;
        }

        protected void ddlAdministerMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdministerMedicine.Text == "Yes")
            {
                cblMedications.Enabled = true;
                txbMedicationsOtherNotes.Enabled = true;
            }
            else
            {
                if (ddlAdministerMedicine.Text == "No")
                {
                    cblMedications.Enabled = false;
                    txbMedicationsOtherNotes.Enabled = false;
                    ClearMedications();
                }
            }
        }


	}
}
