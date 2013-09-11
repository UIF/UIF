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
	/// Summary description for UIFAdmin2.
	/// </summary>
	public partial class UIFAdmin2 : System.Web.UI.Page
	{
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public static string Department = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
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
                    try
                    {
                        con.Open();

                        string selectSQL = "";

                        selectSQL = "select lastname, firstname, middlename " +
                                    "from UIF_PerformingArts.dbo.studentinformation " +
                                    "group by lastname, firstname, middlename " +
                                    "order by lastname, firstname ";

                        SqlDataReader reader = null;
                        SqlCommand cmd = new SqlCommand(selectSQL);

                        cmd.Connection = con;
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {	//Retrieve the first record only
                            ddlNames.Items.Add("Please select a student");
                            do
                            {
                                ddlNames.Items.Add(reader.GetString(0) + "," + reader.GetString(1) + " (" + reader.GetString(2) + ") ");
                            } while (reader.Read());
                            reader.Close();
                            ddlNames.Text = "Please select a student";
                        }
                        //btnSearch.Enabled = false;

                        string selectSQL2 = "";

                        selectSQL2 = "select lastname, firstname, middlename " +
                                    "from UIF_PerformingArts.dbo.studentinformation " +
                                    "group by lastname, firstname, middlename " +
                                    "order by firstname, lastname ";

                        SqlDataReader reader2 = null;
                        SqlCommand cmd2 = new SqlCommand(selectSQL2);

                        cmd2.Connection = con;
                        reader2 = cmd2.ExecuteReader();
                        if (reader2.Read())
                        {	//Retrieve the first record only
                            ddlNames2.Items.Add("Please select a student");
                            do
                            {
                                ddlNames2.Items.Add(reader2.GetString(1) + " " + reader2.GetString(0) + " (" + reader2.GetString(2) + ") ");
                                //ddlNames2.Items.Add(reader2.GetString(1) + " (" + reader2.GetString(2) + ") " + reader2.GetString(0));
                            } while (reader2.Read());
                            reader2.Close();
                            ddlNames2.Text = "Please select a student";
                        }
                    }
                    catch (Exception lkj)
                    {


                    }
                    finally
                    {


                    }
                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!
                    //Response.Redirect("ErrorAccess.aspx");
                    Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
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

		protected void ddlNames_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            btnSearch.Enabled = true;
            btnSearch.Focus();
            cmbSearch2.Enabled = false;
            ddlNames2.Text = "Please select a student";
		}

        protected void ddlNames2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cmbSearch2.Enabled = true;
            cmbSearch2.Focus();
            btnSearch.Enabled = false;
            ddlNames.Text = "Please select a student";
        }

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
            if (ddlNames.Text != "Please select a student")
            {
                Response.Clear();
                //Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlNames.SelectedValue.Substring(0,ddlNames.SelectedValue.IndexOf(","))
				//			    + "&StudentFirstName=" + ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",")+ 1,ddlNames.SelectedValue.Length - (ddlNames.SelectedValue.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);

                //Now includes middlename.
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentLastName=" + ddlNames.SelectedValue.Substring(0, ddlNames.SelectedValue.IndexOf(","))
                                + "&StudentFirstName=" + ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf(",") + 1, ((ddlNames.SelectedValue.IndexOf("(")) - (ddlNames.SelectedValue.IndexOf(",") + 2))) + "&StudentMiddleName=" + ddlNames.SelectedValue.Substring(ddlNames.SelectedValue.IndexOf("(") + 1, ((ddlNames.SelectedValue.IndexOf(")")) - (ddlNames.SelectedValue.IndexOf("("))) - 1) + "&Dept=" + Request.QueryString["Dept"]);
            }
        }

        protected void cmbSearch2_Click(object sender, EventArgs e)
        {
            if (ddlNames2.Text != "Please select a student")
            {
                Response.Clear();
                //Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentFirstName=" + ddlNames2.SelectedValue.Substring(0, ddlNames2.SelectedValue.IndexOf(" "))
                //                + "&StudentLastName=" + ddlNames2.SelectedValue.Substring(ddlNames2.SelectedValue.IndexOf(" ") + 1, ddlNames2.SelectedValue.Length - (ddlNames2.SelectedValue.IndexOf(" ") + 1)) + "&Dept=" + Request.QueryString["Dept"]);

                //Now includes middlename.
                Response.Redirect("RegistrationForm.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&StudentFirstName=" + ddlNames2.SelectedValue.Substring(0, ddlNames2.SelectedValue.IndexOf(" "))
                                + "&StudentLastName=" + ddlNames2.SelectedValue.Substring(ddlNames2.SelectedValue.IndexOf(" ") + 1, ((ddlNames2.SelectedValue.IndexOf("(")) - (ddlNames2.SelectedValue.IndexOf(" ") + 2))) + "&StudentMiddleName=" + ddlNames2.SelectedValue.Substring(ddlNames2.SelectedValue.IndexOf("(") + 1, ((ddlNames2.SelectedValue.IndexOf(")")) - (ddlNames2.SelectedValue.IndexOf("("))) - 1) + "&Dept=" + Request.QueryString["Dept"]);
            }
        }
        
        protected void dgrdData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

        protected void cmdSearchChildrensChoir_Click(object sender, EventArgs e)
        {

        }

        protected void cmdSearchMSHSChoir_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


        }

        protected void ddlSearchMSHSChoir_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSearchPerformingArtsAcademy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmbSoundEquipment_Click(object sender, EventArgs e)
        {
            Response.Redirect("SoundEquipment.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmbNewStudent_Click(object sender, EventArgs e)
        {
            //New Student...RCM.1/16/11.
            Response.Clear();
            Response.Redirect("RegistrationForm.aspx?Security=Good&newstudent=newstudent" + "&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbMSHSChoir_Click(object sender, EventArgs e)
        {
            Response.Redirect("MSHSChoir.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmbPerformingArtsAcademy_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArtsAcademy.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void cmbChildrensChoir_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChildrensChoir.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
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
	}
}

