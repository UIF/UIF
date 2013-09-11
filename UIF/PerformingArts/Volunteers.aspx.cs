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
using UrbanImpactCommon;
using System.Data.SqlClient;

namespace UIF.PerformingArts
{
	/// <summary>
	/// Summary description for Volunteers.
	/// </summary>
	public partial class Volunteers : System.Web.UI.Page
	{
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public Boolean flag = false;
        public int irowNum = 0;
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

                if (Request.QueryString["security"] == "Good")
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
                            ddlVolunteerList.Items.Add("Please select a volunteer");
                            do
                            {
                                ddlVolunteerList.Items.Add(reader.GetString(0) + "," + reader.GetString(1));
                            } while (reader.Read());
                            reader.Close();
                            ddlVolunteerList.Text = "Please select a volunteer";
                        }
                        //btnSearch.Enabled = false;

                        string selectSQL2 = "select lastname, firstname " +
                                           "from volunteerinformation " +
                                           "group by lastname, firstname " +
                                           "order by firstname, lastname ";

                        SqlDataReader reader2 = null;
                        SqlCommand cmd2 = new SqlCommand(selectSQL2);

                        cmd2.Connection = con;
                        reader2 = cmd2.ExecuteReader();
                        if (reader2.Read())
                        {	//Retrieve the first record only
                            ddlVolunteerList2.Items.Add("Please select a volunteer");
                            do
                            {
                                ddlVolunteerList2.Items.Add(reader2.GetString(1) + " " + reader2.GetString(0));
                            } while (reader2.Read());
                            reader2.Close();
                            ddlVolunteerList2.Text = "Please select a volunteer";
                        }
                        ////btnSearch.Enabled = false;
                    }
                    catch (Exception lkj)
                    {


                    }
                    finally
                    {


                    }


                    //try
                    //{
                    //    string sql_LoadGrid = "select * from VolunteerInformation order by lastname, firstname ";

                    //    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                    //    DataSet ds = new DataSet();
                    //    da.Fill(ds, "VolunteerInformation");
                    //    gvVolunteerList.DataSource = ds.Tables[0];
                    //    gvVolunteerList.DataBind();
                    //    con.Close();
                    //}
                    //catch (Exception lkjl_)
                    //{

                    //    string lkjl = "";
                    //}
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

        protected void gvVolunteerList_RowDataBound(object sender, GridViewRowEventArgs e)
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
            }
        }

        protected void gvVolunteerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = gvVolunteerList.SelectedRow;
            //irowNum = gvVolunteerList.SelectedIndex;
            //bind();
        }

        protected void gvVolunteerList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
           // GridViewRow row = gvVolunteerList.Rows[e.NewSelectedIndex];
        }

        public void bind()
        {
            con.Open();

            //string sql_LoadGrid = "";
            //sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
            //             + "FROM PerformingArtsAcademyAvailableClasses  order by classname";

            string sql_LoadGrid = "select * from VolunteerInformation order by lastname, firstname ";


            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "VolunteerInformation");
            //gvVolunteerList.DataSource = ds.Tables[0];
            //gvVolunteerList.DataBind();
            con.Close();
        }

        protected void gvVolunteerList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvVolunteerList.EditIndex = e.NewEditIndex;
            bind();
        }

        protected void gvVolunteerList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //gvVolunteerList.DeleteRow(e.RowIndex);
            //gvVolunteerList.DataBind();
        }

        protected void gvVolunteerList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //gvVolunteerList.EditIndex = -1;
            bind();
        }

        protected void gvVolunteerList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //GridViewRow row = (GridViewRow)gvVolunteerList.Rows[e.RowIndex];
            //Label lbl = (Label)row.FindControl("lblid");
            //TextBox classname = (TextBox)row.FindControl("textbox1");
            //TextBox classmeettime = (TextBox)row.FindControl("textbox2");
            //TextBox classmeetday = (TextBox)row.FindControl("textbox3");
            //TextBox classlocation = (TextBox)row.FindControl("textbox4");
            //TextBox comments = (TextBox)row.FindControl("textbox5");
            //TextBox instructor = (TextBox)row.FindControl("textbox6");
            //TextBox devotionalleader = (TextBox)row.FindControl("textbox7");
            //TextBox ID = (TextBox)row.FindControl("textbox8");

            //gvVolunteerList.EditIndex = -1;
            con.Open();
            //SqlCommand cmd = new SqlCommand("Update VolunteerInformation set "
            //                              + "  instructor='" + instructor.Text
            //                              + "' , devotionalleader='" + devotionalleader.Text
            //                              + "' , classname='" + classname.Text
            //                              + "' , meettime='" + classmeettime.Text
            //                              + "' , meetday='" + classmeetday.Text
            //                              + "' , location='" + classlocation.Text
            //                              + "' , comments='" + comments.Text
            //                              + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
            //                              + " where ID = '" + ID.Text + "' ");
            //cmd.Connection = con;
            //cmd.ExecuteNonQuery();
            con.Close();
            bind();
        }

        protected void ddlVolunteerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRetrieveVolunteerInformation.Enabled = true;
            cmbRetrieveVolunteerInformation.Focus();
            cmbRetrieveVolunteerInformation2.Enabled = false;
        }

        protected void cmbRetrieveVolunteerInformation_Click(object sender, EventArgs e)
        {
            //Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + Request.QueryString["firstname"] + "&VolunteerFirstName=" + Request.QueryString["firstname"]);
            Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + ddlVolunteerList.SelectedValue.Substring(0, ddlVolunteerList.SelectedValue.IndexOf(","))
                            + "&VolunteerFirstName=" + ddlVolunteerList.SelectedValue.Substring(ddlVolunteerList.SelectedValue.IndexOf(",") + 1, ddlVolunteerList.SelectedValue.Length - (ddlVolunteerList.SelectedValue.IndexOf(",") + 1)) + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbNewStudent_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Redirect("VolunteerInformation.aspx?Security=Good&newvolunteer=newvolunteer" + "&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerLastName=" + "&VolunteerFirstName=" + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void cmbRetrieveVolunteerInformation2_Click(object sender, EventArgs e)
        {
            Response.Redirect("VolunteerInformation.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&VolunteerFirstName=" + ddlVolunteerList2.SelectedValue.Substring(0, ddlVolunteerList2.SelectedValue.IndexOf(" "))
                            + "&VolunteerLastName=" + ddlVolunteerList2.SelectedValue.Substring(ddlVolunteerList2.SelectedValue.IndexOf(" ") + 1, ddlVolunteerList2.SelectedValue.Length - (ddlVolunteerList2.SelectedValue.IndexOf(" ") + 1)) + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void ddlVolunteerList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRetrieveVolunteerInformation.Enabled = false;
            cmbRetrieveVolunteerInformation2.Enabled = true;
            cmbRetrieveVolunteerInformation2.Focus();
        }

        protected void cmbBack_Click1(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
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
