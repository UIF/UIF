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
	/// <summary>
	/// Summary description for RegistrationFormAdmin.
	/// </summary>
	public partial class RegistrationFormAdmin : System.Web.UI.Page
	{
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);        
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Request.QueryString["security"] == "Good")
            {
			    // Put user code to initialize the page here
			    string lk = Request.QueryString["LastName"];
			    string aa = Request.QueryString["FirstName"];

			    SqlConnection con = new SqlConnection(connectionString);
			    con.Open();

			    string sql = "select [last name], [first name], address, [home phone], school, grade, age, " 
				    + "city, dob, state, zip, sex, church, [student cell phone], [t-shirt size] " 
				    + "FROM dbo.choir "
				    + "WHERE [last name]=@lastname "
				    + "AND [first name]=@firstname";							

			    //Perform database lookup based on the chosen child..RCM..
			    SqlDataReader reader = null;
			    SqlCommand cmd = new SqlCommand(sql);
		    //	cmd.Parameters.Add("@lastname", Request.QueryString["LastName"]);
		    //	cmd.Parameters.Add("@firstname",Request.QueryString["FirstName"]);	
		
			    cmd.Connection = con;
			    reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    do
                    {
                        string lkjal = "";

                        //ddlNames.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                        //txtStudentEmail.Text = reader.GetString(0);
                        txtLastName.Text = reader.GetString(0);
                        txtFirstName.Text = reader.GetString(1);
                        txtAddress1.Text = reader.GetString(2);
                        txtHomePhone.Text = reader.GetString(3);
                        if (reader.IsDBNull(4))
                        {
                            txtSchool.Text = "missing";
                        }
                        else
                        {
                            //reader.IsDBNull(0)
                            txtSchool.Text = reader.GetString(4);
                        }
                        if (reader.IsDBNull(12))
                        {
                            txtChurch.Text = "missing";
                        }
                        else
                        {
                            txtChurch.Text = reader.GetString(12);
                        }
                        //txtCity.Text = reader.GetString(6);
                        //					if (reader.IsDBNull(5)
                        //					{
                        //
                        //					}
                        //					else
                        //					{
                        //
                        //					}
                        txtGrade.Text = reader.GetString(5);
                        txtAge.Text = reader.GetString(6);
                        txtCity.Text = "Pittsburgh";
                        txtDateBirth.Text = reader.GetString(8);
                        txtTShirtSize.Text = reader.GetString(14);
                        txtState.Text = reader.GetString(9);
                        txtZip.Text = reader.GetString(10);
                        if (reader.IsDBNull(13))
                        {
                            txtStudentCellPhone.Text = "NULL";
                        }
                        else
                        {
                            //txtStudentCellPhone.Text = reader.GetString(13);
                            txtStudentCellPhone.Text = reader.GetSqlValue(13).ToString();
                        }
                        txtGender.Text = reader.GetString(11);
                    } while (reader.Read());
            }
            else
            {


            }
            }
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
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

		private void txtState_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtZip_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtGender_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtDateBirth_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtCity_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtAge_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtFirstName_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtLastName_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtAddress1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtHomePhone_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtSchool_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void txtStudentEmail_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnSubmitInformation_Click(object sender, System.EventArgs e)
		{
			//Here we want to capture all the information from the various different fields and
			//use them to update the data within the database table..RCM..8/3/10.

			//Collects the various different pieces of information.

			string alk = "";

			//			txtStudentEmail.Text;
			//			txtSchool.Text;
			//			txtHomePhone.Text;
			//			txtAddress1.Text;
			//			txtLastName.Text;
			//			txtFirstName.Text;
			//			txtCity.Text;
			//			txtState.Text;
			//			txtZip.Text;
			//			txtStudentCellPhone.Text;
			//			txtAge.Text;
			//			txtDateBirth.Text;
			//			txtGender.Text;

			//UPDATE table with new fields..

		}

		private void txtGrade_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
