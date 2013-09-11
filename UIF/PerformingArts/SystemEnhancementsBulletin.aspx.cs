using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UrbanImpactCommon;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;


namespace UIF.PerformingArts
{
    public partial class SystemEnhancementsBulletin : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public Boolean flag = false;
        public int irowNum = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["security"] == "Good")
            {

                if (!Page.IsPostBack)
                {
                    //Ryan C Manners...6/16/11.
                    UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                    MenuBest = BuildMenu.BuildMenuControl(MenuBest);

                    //Retrieve the class lists
                    con.Open();
                    try
                    {
                        string sql_LoadGrid = "select * from systemenhancements order by degreeofimportance desc ";

                        SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "systemenhancements");
                        gvSystemEnhancements.DataSource = ds.Tables[0];
                        gvSystemEnhancements.DataBind();
                        con.Close();
                    }
                    catch (Exception lkjl_)
                    {

                        string lkjl = "";
                    }
                }
            }
            else
            {
                //Ryan C Manners..1/5/11
                //Do NOT ALLOW ACCESS TO THE PAGE!
                Response.Redirect("ErrorAccess.aspx");
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

        protected void gvSystemEnhancements_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvSystemEnhancements_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvSystemEnhancements.SelectedRow;
            irowNum = gvSystemEnhancements.SelectedIndex;
            bind();
        }

        protected void gvSystemEnhancements_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvSystemEnhancements.Rows[e.NewSelectedIndex];
        }

        public void bind()
        {
            con.Open();

            string sql_LoadGrid = "select * from systemenhancements ";

            SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "systemenhancements");
            gvSystemEnhancements.DataSource = ds.Tables[0];
            gvSystemEnhancements.DataBind();
            con.Close();
        }

        protected void gvSystemEnhancements_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSystemEnhancements.EditIndex = e.NewEditIndex;
            bind();
        }

        protected void gvSystemEnhancements_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //gvVolunteerList.DeleteRow(e.RowIndex);
            gvSystemEnhancements.DataBind();
        }

        protected void gvSystemEnhancements_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSystemEnhancements.EditIndex = -1;
            bind();
        }

        protected void gvSystemEnhancements_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvSystemEnhancements.Rows[e.RowIndex];
            //Label lbl = (Label)row.FindControl("lblid");
            TextBox descriptionofwork = (TextBox)row.FindControl("textbox1");
            TextBox reasonforneed = (TextBox)row.FindControl("textbox2");
            TextBox degreeofimportance = (TextBox)row.FindControl("textbox3");
            TextBox suggestedby = (TextBox)row.FindControl("textbox4");
            TextBox department = (TextBox)row.FindControl("textbox5");
            TextBox comment = (TextBox)row.FindControl("textbox6");
            TextBox workscompleted = (TextBox)row.FindControl("textbox7");
            TextBox ID = (TextBox)row.FindControl("textbox8");

            gvSystemEnhancements.EditIndex = -1;
            con.Open();
            SqlCommand cmd = new SqlCommand("Update systemenhancements set "
                                          + "  descriptionofwork='" + descriptionofwork.Text
                                          + "' , reasonforneed='" + reasonforneed.Text
                                          + "' , degreeofimportance='" + degreeofimportance.Text
                                          + "' , suggestedby='" + suggestedby.Text
                                          + "' , department='" + department.Text
                                          + "' , comment='" + comment.Text
                                          + "' , workscompleted='" + workscompleted.Text
                                          + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                          + " where ID = '" + ID.Text + "' ");
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
            Response.Redirect("menutest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }
    }
}