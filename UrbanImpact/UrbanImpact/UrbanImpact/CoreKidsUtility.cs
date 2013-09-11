using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
//using System.Web.Script.Services;


namespace UrbanImpactCommon
{
    public class CoreKidsUtility
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public static string Department = "";

        public void BuildStudentReportCard(string lastname, string firstname)
        {

            // Put user code to initialize the page here
		    SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            
            try
            {
                string sqlUpdateStatement = "";
                con.Open();

                string sql_select = "Select * from CoreKidsProgram";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql_select);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only

                    do
                    {
                        //            ddlProgram.Items.Add(reader.GetString(0));
                    } while (reader.Read());
                    reader.Close();
                    //      ddlProgram.Text = "Choose a program";

                    //    foreach (DataRow myDataRowPO in custDS.Tables["studentprogramattendance"].Rows)
                    //  {
                    //Adding options to the drop downs for a new entry.
                    //      ddlStudentName.Items.Add(myDataRowPO[0].ToString() + "," + myDataRowPO[1].ToString());
                    //  }



                    if (reader.IsDBNull(0))
                    {
                        //txtLastName.Text = "N/A";
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception lkjlkjaaa)
            {

            }
        }
        
        public void UpdateCoreKidsInfo()
        {
            // Put user code to initialize the page here
		    SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            
            try
            {
                string sqlUpdateStatement = "";
                con.Open();

                string sql_select = "Select * from CoreKidsProgram";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql_select);

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
                    }
                }

                //foreach (GridLines)
                //{


                //    string sql = "Select * " + 
                //                 "from studentprogramattendance " +
                //                 "where lastname = ‘’ " +
                //                 "and firstname = ‘’ " +
                //                 "and program = ‘MSHSChoir’ " + 
                //                 "and day >  ‘2011-09-01’ " +
                //                 "and day < ‘2011-12-20’ ";

                //            //Perform database lookup based on the chosen child..RCM..
                //            SqlCommand cmd = new SqlCommand(sql);
                //            cmd.Parameters.Add(new SqlParameter("@lastname", Request.QueryString["StudentLastName"].Trim()));
                //            cmd.Parameters.Add(new SqlParameter("@firstname", Request.QueryString["StudentFirstName"].Trim()));


                //            cmd.Connection = con;
                //            reader = cmd.ExecuteReader();
                //            if (reader.Read())
                //            {	//Retrieve the first record only
                //                if (reader.IsDBNull(0))
                //                {
                //                    txtLastName.Text = "N/A";
                //                }
                //                else
                //                {
                //                    txtLastName.Text = reader.GetString(0);
                //                }
                //                if (reader.IsDBNull(1))
                //                {


                //CleanCharacters();

                //sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.StudentInformation "
                //+ "SET "
                //+ " lastname = '" + txtLastName.Text.Trim() + "' , "
                //+ " firstname = '" + txtFirstName.Text.Trim() + "' , "
                //+ " address = '" + txtAddress1.Text.Trim() + "' , "
                //+ " city = '" + txtCity.Text.Trim() + "' , "
                //+ " state = '" + txtState.Text.Trim() + "' , "
                //+ " zip = '" + txtZip.Text.Trim() + "' , "
                //+ " homephone = '" + txtHomePhone.Text.Trim() + "' , "
                //+ " studentcellphone = '" + txtStudentCellPhone.Text.Trim() + "' , "
                //    //+ " textphone  = " + Convert.ToInt32(ddlTextPhone.Text) + ", "
                //+ " studentemail = '" + txtStudentEmail.Text.Trim() + "' , "
                //+ " school = '" + ddlSchool.Text.Trim() + "' , "
                //+ " grade = '" + ddlGrade.Text.Trim() + "' , "
                //+ " age = '" + ddlAge.Text.Trim() + "' , "
                //+ " dob = '" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "' , "
                //+ " sex = '" + ddlGender.Text.Trim() + "' , "
                //+ " church = '" + txtChurch.Text.Trim() + "' , "
                //+ " careergoal = '" + txtCareerGoal.Text.Trim() + "' , "
                //+ " healthconditions = '" + txbHealthConditions.Text.Trim() + "' , "
                //+ " notes = '" + txbNotes.Text.Trim() + "' , "
                //+ " tshirtsize = '" + ddlTShirtSize.Text.Trim() + "' , "
                //+ " meetccgf = " + Convert.ToInt32(chbMeetCCGF.Checked) + ","
                //+ " schoolform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ","
                //+ " descipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "',"
                //+ " soloist = " + Convert.ToInt32(chbSoloist.Checked) + ","
                //+ " solosong = '" + txbSoloSong.Text.Trim() + "',"
                //+ " dance = " + Convert.ToInt32(chbDance.Checked) + ","
                //+ " danceyear = " + "'1900-01-01'" + ","//danceyear
                //+ " schoolform2 = " + "'" + txtCareerGoal.Text.Trim() + "',"//schoolform2
                //+ " bibleownership = " + Convert.ToInt32(chbBibleOwnership.Checked) + ","
                //+ " biblestudyparticipation = " + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ","
                //+ " havereceivedchrist = " + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ","
                //+ " whenreceivedchrist = '1900-01-01'" + ","//when received christ.
                //+ " sysupdate = '" + System.DateTime.Now.ToString() + "',"
                //+ " currentregistrationform = " + Convert.ToInt32(chbRegistrationForm.Checked) + ","
                //+ " parentalconsentform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ","
                //+ " retreatconsentform = " + Convert.ToInt32(chbRetreatForm.Checked) + ","
                //+ " studentchoirquestionareform = " + Convert.ToInt32(chbStudentQuestionareForm.Checked) + ","
                //+ " middlename = '" + txbMiddleName.Text.Trim() + "' , "
                //+ " mailinglistinclude = " + Convert.ToInt32(chbMailingList.Checked) + ", "
                //+ " mailinglistcodes = '" + ddlMailingListCodes.Text.Trim() + "', "
                //+ " ID = " + Convert.ToInt32(txbID.Text.Trim()) + ", "
                //+ " voicepart = '" + ddlVoicePart.Text.Trim() + "', "
                //+ " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                //+ " discipleshipmentorprogram = " + Convert.ToInt32(chbDiscipleshipMentor.Checked) + " "
                //+ "  "
                //+ " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
                //+ " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' ";

                
                //con.Open();

                //create a SQL command to update record
                //SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
                //if (sqlUpdateCommand.ExecuteNonQuery() > 0)
               // {
                //}
                //else
                //{
                    //Didn't find a record to update..RCM..
                //}
            }
            catch (Exception lkjaaa)
            {
                //lblInformation.Enabled = true;
                //lblInformation.Text = "The update to SECTION1, the DB failed.  Please fix and try again MSG: " + lkjaaa.Message.ToString();
            }
        }


        public void UpdateStudentMultipleProgramsColumn(string lastname, string firstname)
        {
            // Put user code to initialize the page here
		    SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            

            
            
            
            //try
            //{
            //    con.Open();

            //    string PopulateClassName_sql = "Select * from CoreKidsProgram ";

            //    SqlCommand cmd = new SqlCommand(PopulateClassName_sql, con);
            //    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
            //    SqlDataAdapter custDA = new SqlDataAdapter();
            //    custDA.SelectCommand = cmd;
            //    DataSet custDS = new DataSet();
            //    custDA.Fill(custDS, "corekidsprogram");

            //    //Iterate over setup records and call method to do the work on each one...RCM..
            //    foreach (DataRow myDataRowPO in custDS.Tables["corekidsprogram"].Rows)
            //    {



            //        //Adding options to the drop downs for a new entry.
            //       //ddlClassName.Items.Add(myDataRowPO[0].ToString());
            //    }
            //    ddlClassName.Text = currentstudent;
            //    //ddlClassName.Text = "Choose a classname";
            //    custDS.Clear();
                


                
                
            //    string sqlUpdateStatement = "";
            //    con.Open();

            //    string sql_select = "Select * from CoreKidsProgram";

            //    //Perform database lookup based on the chosen child..RCM..
            //    SqlCommand cmd = new SqlCommand(sql_select);

            //    cmd.Connection = con;
            //    reader = cmd.ExecuteReader();
            //    if (reader.Read())
            //    {	//Retrieve the first record only
            //        if (reader.IsDBNull(0))
            //        {
            //            //txtLastName.Text = "N/A";
            //        }
            //        else
            //        {
            //        }
            //    }

            //    //foreach (GridLines)
            //    //{


            //    //    string sql = "Select * " + 
            //    //                 "from studentprogramattendance " +
            //    //                 "where lastname = ‘’ " +
            //    //                 "and firstname = ‘’ " +
            //    //                 "and program = ‘MSHSChoir’ " + 
            //    //                 "and day >  ‘2011-09-01’ " +
            //    //                 "and day < ‘2011-12-20’ ";

            //    //            //Perform database lookup based on the chosen child..RCM..
            //    //            SqlCommand cmd = new SqlCommand(sql);
            //    //            cmd.Parameters.Add(new SqlParameter("@lastname", Request.QueryString["StudentLastName"].Trim()));
            //    //            cmd.Parameters.Add(new SqlParameter("@firstname", Request.QueryString["StudentFirstName"].Trim()));


            //    //            cmd.Connection = con;
            //    //            reader = cmd.ExecuteReader();
            //    //            if (reader.Read())
            //    //            {	//Retrieve the first record only
            //    //                if (reader.IsDBNull(0))
            //    //                {
            //    //                    txtLastName.Text = "N/A";
            //    //                }
            //    //                else
            //    //                {
            //    //                    txtLastName.Text = reader.GetString(0);
            //    //                }
            //    //                if (reader.IsDBNull(1))
            //    //                {


            //    //CleanCharacters();

            //    //sqlUpdateStatement = " UPDATE UIF_PerformingArts.dbo.StudentInformation "
            //    //+ "SET "
            //    //+ " lastname = '" + txtLastName.Text.Trim() + "' , "
            //    //+ " firstname = '" + txtFirstName.Text.Trim() + "' , "
            //    //+ " address = '" + txtAddress1.Text.Trim() + "' , "
            //    //+ " city = '" + txtCity.Text.Trim() + "' , "
            //    //+ " state = '" + txtState.Text.Trim() + "' , "
            //    //+ " zip = '" + txtZip.Text.Trim() + "' , "
            //    //+ " homephone = '" + txtHomePhone.Text.Trim() + "' , "
            //    //+ " studentcellphone = '" + txtStudentCellPhone.Text.Trim() + "' , "
            //    //    //+ " textphone  = " + Convert.ToInt32(ddlTextPhone.Text) + ", "
            //    //+ " studentemail = '" + txtStudentEmail.Text.Trim() + "' , "
            //    //+ " school = '" + ddlSchool.Text.Trim() + "' , "
            //    //+ " grade = '" + ddlGrade.Text.Trim() + "' , "
            //    //+ " age = '" + ddlAge.Text.Trim() + "' , "
            //    //+ " dob = '" + ddlMonthBirth.Text.Trim() + "-" + ddlDayBirth.Text.Trim() + "-" + ddlYearBirth.Text.Trim() + "' , "
            //    //+ " sex = '" + ddlGender.Text.Trim() + "' , "
            //    //+ " church = '" + txtChurch.Text.Trim() + "' , "
            //    //+ " careergoal = '" + txtCareerGoal.Text.Trim() + "' , "
            //    //+ " healthconditions = '" + txbHealthConditions.Text.Trim() + "' , "
            //    //+ " notes = '" + txbNotes.Text.Trim() + "' , "
            //    //+ " tshirtsize = '" + ddlTShirtSize.Text.Trim() + "' , "
            //    //+ " meetccgf = " + Convert.ToInt32(chbMeetCCGF.Checked) + ","
            //    //+ " schoolform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ","
            //    //+ " descipleshipmentor = '" + txbDiscipleshipMentor.Text.Trim() + "',"
            //    //+ " soloist = " + Convert.ToInt32(chbSoloist.Checked) + ","
            //    //+ " solosong = '" + txbSoloSong.Text.Trim() + "',"
            //    //+ " dance = " + Convert.ToInt32(chbDance.Checked) + ","
            //    //+ " danceyear = " + "'1900-01-01'" + ","//danceyear
            //    //+ " schoolform2 = " + "'" + txtCareerGoal.Text.Trim() + "',"//schoolform2
            //    //+ " bibleownership = " + Convert.ToInt32(chbBibleOwnership.Checked) + ","
            //    //+ " biblestudyparticipation = " + Convert.ToInt32(chbBibleStudyParticipation.Checked) + ","
            //    //+ " havereceivedchrist = " + Convert.ToInt32(chbHaveReceivedChrist.Checked) + ","
            //    //+ " whenreceivedchrist = '1900-01-01'" + ","//when received christ.
            //    //+ " sysupdate = '" + System.DateTime.Now.ToString() + "',"
            //    //+ " currentregistrationform = " + Convert.ToInt32(chbRegistrationForm.Checked) + ","
            //    //+ " parentalconsentform = " + Convert.ToInt32(chbParentalConsentForm.Checked) + ","
            //    //+ " retreatconsentform = " + Convert.ToInt32(chbRetreatForm.Checked) + ","
            //    //+ " studentchoirquestionareform = " + Convert.ToInt32(chbStudentQuestionareForm.Checked) + ","
            //    //+ " middlename = '" + txbMiddleName.Text.Trim() + "' , "
            //    //+ " mailinglistinclude = " + Convert.ToInt32(chbMailingList.Checked) + ", "
            //    //+ " mailinglistcodes = '" + ddlMailingListCodes.Text.Trim() + "', "
            //    //+ " ID = " + Convert.ToInt32(txbID.Text.Trim()) + ", "
            //    //+ " voicepart = '" + ddlVoicePart.Text.Trim() + "', "
            //    //+ " lastupdatedby = '" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
            //    //+ " discipleshipmentorprogram = " + Convert.ToInt32(chbDiscipleshipMentor.Checked) + " "
            //    //+ "  "
            //    //+ " WHERE lastname = '" + Request.QueryString["StudentLastName"] + "' "
            //    //+ " AND firstname = '" + Request.QueryString["StudentFirstName"] + "' ";

                
            //    //con.Open();

            //    //create a SQL command to update record
            //    //SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);
            //    //if (sqlUpdateCommand.ExecuteNonQuery() > 0)
            //   // {
            //    //}
            //    //else
                //{


        }

        public static void InsertCoreKidsInfo()
        {


        }

        public static void CollectCoreKidsInfo()
        {



        }
    }
}
