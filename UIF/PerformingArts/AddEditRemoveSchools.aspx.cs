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
    public partial class AddEditRemoveSchools : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public string OldUpdateValue = "";
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
                    string sql_LoadGrid = "";

                    gvStudentList.Visible = false;
                    gvTeamSections.Visible = false;
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

                    //lbAddNewEntry.Visible = true;
                    lbAddNewEntry.Enabled = false;
            
                    try
                    {
                        sql_LoadGrid = "select School, Neighborhood, NeighborhoodRegion, ZipCode, id as 'id' "
                                     + "FROM SchoolNames  "
                                     + "GROUP BY School, neighborhood, neighborhoodregion, zipcode, id "
                                     + "ORDER by School";

                        con.Open();//Opens the db connection.

                        SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                        DataSet ds = new DataSet();

                        da.Fill(ds, "SchoolNames");
                        gvStudentList.DataSource = ds.Tables[0];
                        gvStudentList.DataBind();
                        gvStudentList.Visible = true;
                        lbAddNewEntry.Text = "Add a New School";
                        lbAddNewEntry.Visible = true;
                        lbAddNewEntry.Enabled = true;
                        //lblHeading.Visible = true;
                        //lblHeading.Text = "School Names";

                        //PopulateTeamSectionDropDown();
                        //ddlTeamNames.Visible = true;
                        con.Close();
                    }
                    catch (Exception lklaabb)
                    {
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


        //protected Boolean ReadOnly()
        //{
        //    //Make this a table lookup...
        //    Boolean found = false;

        //    //Make it READ ONLY, for these people on the Section Maintenance part..RCM..1/16/13.
        //    //if (((Request.QueryString["LastName"] == "Churchill") && (Request.QueryString["FirstName"] == "Andrew")) || ((Request.QueryString["LastName"] == "Sims-Reed") && (Request.QueryString["FirstName"] == "Donna")))
        //    //{
        //    //    found = true;
        //    //}

        //    return found; 
        //}

        protected void cmbProgram_Click(object sender, EventArgs e)
        {

            //Retrieve the class lists
            try
            {
                con.Open();//Opens the db connection.
                string sql_LoadGrid = "";

                if (ddlProgram.Text == "MSHS Choir")
                {
                    sql_LoadGrid = "select Lastname, Firstname, Grade "
                                        + "FROM PerformingArtsAcademyStudents "
                                        + "WHERE mshschoir = 1 "
                                        + "order by lastname, firstname ";

                    //Perform database lookup based on the chosen child..RCM..
                    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                    cmd.Connection = con;
                    gvStudentList.DataSource = cmd.ExecuteReader();
                    gvStudentList.DataBind();
                    gvStudentList.Columns[1].HeaderText = "Test";
                }
                else if (ddlProgram.Text == "Childrens Choir")
                {
                    sql_LoadGrid = "select Lastname, Firstname, Grade"
                                        + "FROM PerformingArtsAcademyStudents "
                                        + "WHERE childrenschoir = 1 "
                                        + "order by lastname, firstname ";

                    //Perform database lookup based on the chosen child..RCM..
                    SqlCommand cmd = new SqlCommand(sql_LoadGrid);

                    cmd.Connection = con;
                    gvStudentList.DataSource = cmd.ExecuteReader();
                    gvStudentList.DataBind();
                }
                else if (ddlProgram.Text == "PerformingArtsAcademy")
                {
                    lblProgram.Visible = false;
                    cmbProgram.Visible = false;
                    ddlProgram.Enabled = false;
                    ddlProgram.Visible = false;

                    sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', sizelimit as 'SizeLimit', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM PerformingArtsAcademyAvailableClasses  order by classname";

                    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "PerformingArtsAcademyAvailableClasses");
                    gvStudentList.DataSource = ds.Tables[0];
                    gvStudentList.DataBind();
                    con.Close();
                }
            }
            catch (Exception lkjl_)
            {

                string lkjl = "";
            }
        }

        protected void gvStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow && row.RowIndex == irowNum)
            {

                DataRowView drv = (DataRowView)e.Row.DataItem;

                string School = drv.Row.ItemArray.GetValue(0).ToString();
                string Neighborhood = drv.Row.ItemArray.GetValue(1).ToString();
                string NeighborhoodRegion = drv.Row.ItemArray.GetValue(2).ToString();
                string ZipCode = drv.Row.ItemArray.GetValue(3).ToString();
                string IDValue = drv.Row.ItemArray.GetValue(4).ToString();
            }
        }

        protected void gvStudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvStudentList.SelectedRow;
            irowNum = gvStudentList.SelectedIndex;
            bind();
        }

        protected void SynchUpOtherTables(string ProgramName, string newvalue, string oldvalue, string SectionName)
        {
            string sql_Update = "";
            sql_Update = "Update " + ProgramName.Replace(" ", "") + "Enrollment "
                       + "Set TeamColor = '" + newvalue + "' "
                       + "where SectionName = '" + SectionName + "' "
                       + "And TeamColor = '" + oldvalue + "' ";

            con.Open();

            SqlCommand cmd = new SqlCommand(sql_Update);

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected string RetrieveOldValue(string Program, string SectionName, string ID)
        {
            string sql_Retrieve = "";
            string oldvalue = "";

            sql_Retrieve = "Select teamname from " + Program.Replace(" ", "") + "TeamNameSections where SectionName = '" + SectionName + "' "
                            + "And ID = '" + ID + "' ";

            try
            {
                SqlDataReader reader = null;
                con.Open();
                SqlCommand cmd = new SqlCommand(sql_Retrieve);
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
                        oldvalue = reader.GetString(0);
                    }
                }
            }
            catch (Exception lkjlkjaabb)
            {

            }
            finally
            {
                con.Close();
            }
            return oldvalue;
        }

        protected void gvStudentList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvStudentList.Rows[e.NewSelectedIndex];
        }

        public void bind()
        {
            string sql_LoadGrid = "";

            try
            {
                sql_LoadGrid = "select School, Neighborhood, NeighborhoodRegion, ZipCode, id as 'id' "
                             + "FROM SchoolNames  "
                             + "GROUP BY School, neighborhood, neighborhoodregion, zipcode, id "
                             + "ORDER by School";

                con.Open();//Opens the db connection.

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();

                da.Fill(ds, "SchoolNames");
                gvStudentList.DataSource = ds.Tables[0];
                gvStudentList.DataBind();
                gvStudentList.Visible = true;
            }
            catch (Exception lklaabb)
            {
                Response.Redirect("ErrorAccess.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
            }
            finally
            {
                con.Close();
            }
        }

        protected void gvTeamSections_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvTeamSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvTeamSections.SelectedRow;
            irowNum = gvTeamSections.SelectedIndex;
            bind2();

            //GridViewRow row = e.Row;
            //if (e.Row.RowType == DataControlRowType.DataRow && row.RowIndex == irowNum)
            //{

            DataRowView drv = (DataRowView)row.DataItem;
            //DataRowView drv = (DataRowView)e.Row.DataItem;

                string School = drv.Row.ItemArray.GetValue(0).ToString();
                string Neighborhood = drv.Row.ItemArray.GetValue(1).ToString();
                string NeighborhoodRegion = drv.Row.ItemArray.GetValue(2).ToString();
                string ZipCode = drv.Row.ItemArray.GetValue(3).ToString();
                string IDValue = drv.Row.ItemArray.GetValue(4).ToString();
                //string Instructor = drv.Row.ItemArray.GetValue(5).ToString();
                //string devotional = drv.Row.ItemArray.GetValue(6).ToString();
            //}
        }

        protected void gvTeamSections_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvTeamSections.Rows[e.NewSelectedIndex];
        }

        public void bind2()
        {
            string sql_LoadGrid = "";
            try
            {
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BasketballTEAMSTeamNameSections  "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BaseballTeamNameSections  "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM OutreachBasketballTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MSBasketballLeagueTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM HSBasketballLeagueTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM [3on3BasketballTeamNameSections]  "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SoccerIntraMuralsTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SoccerTEAMSTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BibleStudyTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MondayNightsTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SpecialEventsTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ImpactUrbanSchoolsTeamNameSections "
                                 + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
                                 + "order by sectionName, teamname ";
                }

                con.Open();//Opens the db connection.

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    da.Fill(ds, "BasketballTEAMSTeamNameSections");
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    da.Fill(ds, "BaseballTeamNameSections");
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    da.Fill(ds, "OutreachBasketballTeamNameSections");
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    da.Fill(ds, "HSBasketballLeagueTeamNameSections");
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    da.Fill(ds, "MSBasketballLeagueTeamNameSections");
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    da.Fill(ds, "SoccerIntraMuralsTeamNameSections");
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    da.Fill(ds, "SoccerTEAMSTeamNameSections");
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    da.Fill(ds, "[3on3BasketballTeamNameSections]");
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    da.Fill(ds, "[BibleStudyTeamNameSections]");
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    da.Fill(ds, "[MondayNightsTeamNameSections]");
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    da.Fill(ds, "[SpecialEventsTeamNameSections]");
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    da.Fill(ds, "[ImpactUrbanSchoolsTeamNameSections]");
                }
                gvTeamSections.DataSource = ds.Tables[0];
                gvTeamSections.DataBind();
                gvTeamSections.Visible = true;
                gvStudentList.Visible = false;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
            }
        }  

        protected void gvStudentList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStudentList.EditIndex = e.NewEditIndex;
            bind();
        }

        protected void gvStudentList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gvStudentList.DataKeys[e.RowIndex].Values[0].ToString();
            RemoveFromTable(sender, e, ID);
        }

        protected void RemoveFromTable(object sender, GridViewDeleteEventArgs e, string ID)
        {
            string sql_DeleteFromClass1 = "";
            try
            {
                sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SchoolNames "
                                     + "WHERE id = '" + ID + "' ";
                
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromClass1 = new SqlCommand(sql_DeleteFromClass1, con);
                if (sqlDeleteFromClass1.ExecuteNonQuery() > 0)
                {
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
                bind();
            }
        }

        protected void gvTeamSections_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
            
            
            int index = e.NewEditIndex;
            GridViewRow row = gvTeamSections.Rows[index];
            OldUpdateValue = row.Cells[1].Text;

            gvTeamSections.EditIndex = e.NewEditIndex;
            bind2();

            // The update operation was successful. Retrieve the row being edited.
            //int index = CustomersGridView.EditIndex;
            //GridViewRow row = gvTeamSections.Rows[gvTeamSections.EditIndex];

            // Notify the user that the update was successful.
            //Message.Text = "Updated record " + row.Cells[1].Text + ".";

            //OldUpdateValue = row.Cells[2].Text;

            //-----------------------------------------------------------------------
            // The update operation was successful. Retrieve the row being edited.
            //int index = CustomersGridView.EditIndex;
            //GridViewRow row = CustomersGridView.Rows[index];

            // Notify the user that the update was successful.
            //Message.Text = "Updated record " + row.Cells[1].Text + ".";
        }

        protected void gvTeamSections_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gvTeamSections.DataKeys[e.RowIndex].Values[0].ToString();
            RemoveFromTeamSectionsTable(sender, e, ID);
        }

        protected void RemoveFromTeamSectionsTable(object sender, GridViewDeleteEventArgs e, string ID)
        {
            string sql_DeleteFromClass1 = "";
            try
            {
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BasketballTEAMSTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BaseballTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.OutreachBasketballTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.MSBasketballLeagueTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.HSBasketballLeagueTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.[3on3BasketballTeamNameSections] "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SoccerIntraMuralsTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SoccerTEAMSTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.MondayNightsTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.BibleStudyTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.SpecialEventsTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.ImpactUrbanSchoolsTeamNameSections "
                                         + "WHERE id = '" + ID + "' ";
                }

                con.Open();

                //create a SQL command to update record
                SqlCommand sqlDeleteFromClass1 = new SqlCommand(sql_DeleteFromClass1, con);
                if (sqlDeleteFromClass1.ExecuteNonQuery() > 0)
                {
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
                bind2();
            }
        }


        protected void gvStudentList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
             gvStudentList.EditIndex = -1;  
             bind();  
        }

        protected void gvStudentList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvStudentList.Rows[e.RowIndex];
            TextBox school = (TextBox)row.FindControl("textbox1");
            TextBox neighborhood = (TextBox)row.FindControl("textbox2");
            TextBox neighborhoodregion = (TextBox)row.FindControl("textbox3");
            TextBox zipcode = (TextBox)row.FindControl("textbox4");
            TextBox ID = (TextBox)row.FindControl("textbox5");

            school.Text = school.Text.Replace("'", "");
            neighborhood.Text = neighborhood.Text.Replace("'", "");
            neighborhoodregion.Text = neighborhoodregion.Text.Replace("'", "");
            zipcode.Text = zipcode.Text.Replace("'", "");

            try
            {
                gvStudentList.EditIndex = -1;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("Update SchoolNames set "
                                    + "  school='" + school.Text
                                    + "' , neighborhood='" + neighborhood.Text
                                    + "' , neighborhoodregion='" + neighborhoodregion.Text
                                    + "' , zipcode='" + zipcode.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");

                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                bind();
            }
            catch (Exception lkjlkjaabb)
            {

            }
            finally
            {
            }
        }

        protected void KeepEnrollmentTablesInSynch()
        {

        }

        protected void KeepTablesInSynch(string tablename, string newsectionname, string oldsectionname)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd = new SqlCommand("Update " + tablename + "TeamNameSections  "
                            + "set sectionname='" + newsectionname + "' "
                            + "where sectionname = '" + oldsectionname + "' ");
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception lkjlkaa)
            {

            }
            finally
            {

            }
        }

        protected void gvTeamSections_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTeamSections.EditIndex = -1;
            bind2();
        }
       

        
        protected void gvTeamSections_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvTeamSections.Rows[e.RowIndex];
            TextBox sectionname = (TextBox)row.FindControl("textbox1");
            TextBox teamname = (TextBox)row.FindControl("textbox2");
            TextBox meettime = (TextBox)row.FindControl("textbox3");
            TextBox meetday = (TextBox)row.FindControl("textbox4");
            TextBox location = (TextBox)row.FindControl("textbox5");
            TextBox comments = (TextBox)row.FindControl("textbox6");
            TextBox supervisor = (TextBox)row.FindControl("textbox7");
            TextBox devotionalleader = (TextBox)row.FindControl("textbox8");
            TextBox ID = (TextBox)row.FindControl("textbox9");
          
            sectionname.Text = sectionname.Text.Replace("'","");
            teamname.Text = teamname.Text.Replace("'", "");
            meettime.Text = meettime.Text.Replace("'", "");
            meetday.Text = meetday.Text.Replace("'", "");
            location.Text = location.Text.Replace("'", "");
            comments.Text = comments.Text.Replace("'", "");
            supervisor.Text = supervisor.Text.Replace("'", "");
            devotionalleader.Text = devotionalleader.Text.Replace("'", "");            

            //Retrieves the value from before the update...RCM..2/27/13.
            //string OldValue = RetrieveOldValue(ddlProgram.Text, sectionname.Text, ID.Text);

            gvTeamSections.EditIndex = -1;
            //gvStudentList.EditIndex = -1;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            if (ddlProgram.Text == "BasketballTEAMS")
            {
                cmd = new SqlCommand("Update BasketballTEAMSTeamNameSections set "
                                    + "  supervisor='" + supervisor.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , teamname='" + teamname.Text
                                    + "' , meettime='" + meettime.Text
                                    + "' , meetday='" + meetday.Text
                                    + "' , location='" + location.Text
                                    + "' , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Baseball")
            {
                cmd = new SqlCommand("Update BaseballTeamNameSections set "
                                    + "  supervisor='" + supervisor.Text
                                    + "' , devotionalleader='" + devotionalleader.Text
                                    + "' , teamname='" + teamname.Text
                                    + "' , meettime='" + meettime.Text
                                    + "' , meetday='" + meetday.Text
                                    + "' , location='" + location.Text
                                    + "' , comments='" + comments.Text
                                    + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Outreach Basketball")
            {
                cmd = new SqlCommand("Update OutreachBasketballTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SoccerIntraMurals")
            {
                cmd = new SqlCommand("Update SoccerIntraMuralsTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "SoccerTEAMS")
            {
                cmd = new SqlCommand("Update SoccerTEAMSTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "MS Basketball League")
            {
                cmd = new SqlCommand("Update MSBasketballLeagueTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "HS Basketball League")
            {
                cmd = new SqlCommand("Update HSBasketballLeagueTeamNameSections set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "3on3 Basketball")
            {
                cmd = new SqlCommand("Update [3on3BasketballTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "MondayNights")
            {
                cmd = new SqlCommand("Update [MondayNightsTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Bible Study")
            {
                cmd = new SqlCommand("Update [BibleStudyTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Special Events")
            {
                cmd = new SqlCommand("Update [SpecialEventsTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            else if (ddlProgram.Text == "Impact Urban Schools")
            {
                cmd = new SqlCommand("Update [ImpactUrbanSchoolsTeamNameSections] set "
                                     + "  supervisor='" + supervisor.Text
                                     + "' , devotionalleader='" + devotionalleader.Text
                                     + "' , teamname='" + teamname.Text
                                     + "' , meettime='" + meettime.Text
                                     + "' , meetday='" + meetday.Text
                                     + "' , location='" + location.Text
                                     + "' , comments='" + comments.Text
                                     + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                    + " where ID = '" + ID.Text + "' ");
            }
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            //SynchUpOtherTables(ddlProgram.Text, teamname.Text, OldValue, sectionname.Text);
            bind2();

            PopulateTeamNamesDropDown();
        }
        
        protected void cmbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerformingArts.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"]);
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            if ((gvTeamSections.Rows.Count != 0))
            {
                gvTeamSections.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvTeamSections, Response);
            }
            else if (gvStudentList.Rows.Count != 0)
            {
                gvStudentList.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
                ExcelExport.ExportGridView(gvStudentList, Response);
            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql_LoadGrid = "";

            gvStudentList.Visible = false;
            gvTeamSections.Visible = false;
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
            lbAddNewEntry.Enabled = false;
            
            //if (!ReadOnly())
            //{
            //    lbAddNewEntry.Enabled = true;
            //}

            try
            {
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BasketballTEAMSProgramSections  order by sectionName";
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BaseballProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM OutreachBasketballProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MSBasketballLeagueProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM HSBasketballLeagueProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM [3on3BasketballProgramSections]  order by sectionname";
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SoccerIntraMuralsProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SoccerTEAMSProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM MondayNightsProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM BibleStudyProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM SpecialEventsProgramSections  order by sectionname";
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', StaffLeader as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM ImpactUrbanSchoolsProgramSections  order by sectionname";
                }

                con.Open();//Opens the db connection.

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();

                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    da.Fill(ds, "BasketballTEAMSProgramSections");
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    da.Fill(ds, "BaseballProgramSections");
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    da.Fill(ds, "OutreachBasketballProgramSections");
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    da.Fill(ds, "HSBasketballLeagueProgramSections");
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    da.Fill(ds, "MSBasketballLeagueProgramSections");
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    da.Fill(ds, "SoccerIntraMuralsProgramSections");
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    da.Fill(ds, "SoccerTEAMSProgramSections");
                }
                else if (ddlProgram.Text == "3on3 Basketball")
                {
                    da.Fill(ds, "[3on3BasketballProgramSections]");
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    da.Fill(ds, "MondayNightsProgramSections");
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    da.Fill(ds, "BibleStudyProgramSections");
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    da.Fill(ds, "SpecialEventsProgramSections");
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    da.Fill(ds, "ImpactUrbanSchoolsProgramSections");
                }
                gvStudentList.DataSource = ds.Tables[0];
                gvStudentList.DataBind();
                gvStudentList.Visible = true;
                lbAddNewEntry.Text = "Add a New Section";
                lblHeading.Visible = true;
                lblHeading.Text = "Program Sections";

                PopulateTeamSectionDropDown();
                ddlTeamNames.Visible = true;
                con.Close();
            }
            catch (Exception lkjl)
            {

            }
            finally
            {
            }
        }

        protected void PopulateTeamSectionDropDown()
        {
            if (ddlProgram.Text != "Please select a program")
            {
                if (ddlProgram.Text == "BasketballTEAMS")
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

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Baseball")
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

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BaseballProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.MSBasketballLeagueProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MSBasketballLeagueProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MSBasketballLeagueProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.HSBasketballLeagueProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "HSBasketballLeagueProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["HSBasketballLeagueProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Outreach Basketball")
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

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["OutreachBasketballProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SoccerIntraMuralsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SoccerIntraMuralsProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SoccerIntraMuralsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SoccerTEAMSProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SoccerTEAMSProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SoccerTEAMSProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.BibleStudyProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BibleStudyProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BibleStudyProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.MondayNightsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MondayNightsProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MondayNightsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.SpecialEventsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SpecialEventsProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SpecialEventsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select sectionname "
                               + "from UIF_PerformingArts.dbo.ImpactUrbanSchoolsProgramSections "
                               + "group by sectionname "
                               + "order by sectionname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "ImpactUrbanSchoolsProgramSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["ImpactUrbanSchoolsProgramSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
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

        protected void PopulateTeamNamesDropDown()
        {
            if (ddlProgram.Text != "Please select a program")
            {
                if (ddlProgram.Text == "BasketballTEAMS")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.BasketballTEAMSTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BasketballTEAMSTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BasketballTEAMSTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Baseball")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.BaseballTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BaseballTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BaseballTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "MS Basketball League")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.MSBasketballLeagueTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MSBasketballLeagueTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MSBasketballLeagueTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "HS Basketball League")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.HSBasketballLeagueTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "HSBasketballLeagueTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["HSBasketballLeagueTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Outreach Basketball")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.OutreachBasketballTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "OutreachBasketballTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["OutreachBasketballTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "SoccerIntraMurals")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.SoccerIntraMuralsTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SoccerIntraMuralsTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SoccerIntraMuralsTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "SoccerTEAMS")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.SoccerTEAMSTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SoccerTEAMSTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SoccerTEAMSTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Bible Study")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.BibleStudyTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "BibleStudyTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["BibleStudyTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "MondayNights")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.MondayNightsTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "MondayNightsTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["MondayNightsTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Special Events")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.SpecialEventsTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "SpecialEventsTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["SpecialEventsTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
                else if (ddlProgram.Text == "Impact Urban Schools")
                {
                    //Load the dropdown list for the sections.
                    string sql = "Select teamname "
                               + "from UIF_PerformingArts.dbo.ImpactUrbanSchoolsTEAMNameSections "
                               + "group by teamname "
                               + "order by teamname ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
                    SqlDataAdapter custDA = new SqlDataAdapter();
                    custDA.SelectCommand = cmd;
                    DataSet custDS = new DataSet();
                    custDA.Fill(custDS, "ImpactUrbanSchoolsTEAMNameSections");

                    ddlTeamNames.Items.Clear();
                    ddlTeamNames.Items.Add("Please select a Section");
                    //Iterate over setup records and call method to do the work on each one...RCM..
                    foreach (DataRow myDataRowPO in custDS.Tables["ImpactUrbanSchoolsTEAMNameSections"].Rows)
                    {
                        //Adding options to the drop downs for a new entry.
                        ddlTeamNames.Items.Add(myDataRowPO[0].ToString());
                    }
                    custDS.Clear();
                    ddlTeamNames.Text = "Please select a Section";
                    //lblProgramSections.Visible = true;
                    ddlTeamNames.Visible = true;
                    ddlTeamNames.Enabled = true;
                    //cmbRetreiveProgram.Enabled = false;
                }
            }
        }

        protected void cmbAddRecord_Click(object sender, EventArgs e)
        {
            //Insert a new class record.
            string sqlInsertStatement = "";
            txbClassName.Text = txbClassName.Text.Replace("'", "");
            txbClassLocation.Text = txbClassLocation.Text.Replace("'", "");
            txbInstructor.Text = txbInstructor.Text.Replace("'", "");
            txbDevotionalLeader.Text = txbDevotionalLeader.Text.Replace("'", "");
            Random randomNumber = new Random();
            string NewNumber = System.Guid.NewGuid().ToString();
                
            try
            {
                sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.SchoolNames " +
                    "values ( "
                    + "'" + txbClassName.Text.Trim() + "',"
                    + "'" + txbClassLocation.Text.Trim() + "', "
                    + "'" + txbInstructor.Text.Trim() + "', "
                    + "'" + txbDevotionalLeader.Text.Trim() + "', "
                    + "'" + System.DateTime.Now.ToString() + "', "
                    + "'" + System.DateTime.Now.ToString() + "', "
                    + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                    + "'" + NewNumber.Substring(0, 10) + "') ";
                    
                con.Open();

                //create a SQL command to update record
                SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
                if (sqlInsertCommand.ExecuteNonQuery() > 0)
                {
                    con.Close();
                    txbClassName.Visible = false;
                    txbClassLocation.Visible = false;
                    txbInstructor.Visible = false;
                    txbDevotionalLeader.Visible = false;
                    cmbAddRecord.Visible = false;
                    lblClassLocation.Visible = false;
                    lblClassInstructor.Visible = false;
                    lblClassName.Visible = false;
                    lblDevotionalLeader.Visible = false;
                    lblClassInstructor.Visible = false;
                    lbAddNewEntry.Visible = true;

                    //lbAddNewEntry.Visible = true;
                    //ResetNewPerson();
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

        protected void DisplayTheGrid()
        {
            bind();
        }

        protected void DisplayTheGrid2()
        {
            bind2();
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
            txbTeamName.Text = "";
            txbTeamName.Visible = false;
            //lblTeamName.Visible = false;            
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
            if ((ddlTeamNames.Text != "Please select a Section") && (ddlTeamNames.Visible))
            {
                txbClassName.Text = ddlTeamNames.Text.Trim();
                txbClassName.ReadOnly = true;
                txbTeamName.Visible = true;
                lblTeamName.Visible = true;
            }
            lbAddNewEntry.Visible = false;
            cmbAddRecord.Visible = true;
            //ddlTime.Visible = true;
            txbClassLocation.Visible = true;
            txbClassName.Visible = true;
            //txbComments.Visible = true;
            txbDevotionalLeader.Visible = true;
            txbInstructor.Visible = true;
            //txbDay.Visible = true;

            lblClassInstructor.Visible = true;
            lblClassLocation.Visible = true;
            lblClassName.Visible = true;
            //lblComments.Visible = true;
            //lblDay.Visible = true;
            lblDevotionalLeader.Visible = true;
            //lblTime.Visible = true;

            lblHeading.Visible = false;
        }

        protected void cmbReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditRemoveSchools.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void ddlTeamNames_SelectedIndexChanged(object sender, EventArgs e)
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
            lbAddNewEntry.Text = "Add a New Team";

            lblHeading.Visible = true;
            lblHeading.Text = "Team Names";

            bind2();
                        
            //try
            //{
            //    if (ddlProgram.Text == "BasketballTEAMS")
            //    {
            //        sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM BasketballTEAMSTeamNameSections  "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "Baseball")
            //    {
            //        sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM BaseballTeamNameSections  "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "Outreach Basketball")
            //    {
            //        sql_LoadGrid = "select SectionName, TeamName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM OutreachBasketballTeamNameSections "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "MS Basketball League")
            //    {
            //        sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM MSBasketballLeagueTeamNameSections "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "HS Basketball League")
            //    {
            //        sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM HSBasketballLeagueTeamNameSections "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "3on3 Basketball")
            //    {
            //        sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM [3on3BasketballTeamNameSections]  "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "SoccerIntraMurals")
            //    {
            //        sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM SoccerIntraMuralsTeamNameSections "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "SoccerTEAMS")
            //    {
            //        sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM SoccerTEAMSTeamNameSections "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "Bible Study")
            //    {
            //        sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM SoccerTEAMSTeamNameSections "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }
            //    else if (ddlProgram.Text == "MondayNights")
            //    {
            //        sql_LoadGrid = "select SectionName, meettime as 'Time', meetday as 'Day', location as 'Location', comments as 'Comments', SuperVisor, devotionalleader as 'DevotionalLeader', id as 'id' "
            //                     + "FROM SoccerTEAMSTeamNameSections "
            //                     + "WHERE sectionname = '" + ddlTeamNames.Text.Trim() + "' "
            //                     + "order by sectionName, teamname ";
            //    }

            //    con.Open();//Opens the db connection.

            //    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
            //    DataSet ds = new DataSet();

            //    if (ddlProgram.Text == "BasketballTEAMS")
            //    {
            //        da.Fill(ds, "BasketballTEAMSTeamNameSections");
            //    }
            //    else if (ddlProgram.Text == "Baseball")
            //    {
            //        da.Fill(ds, "BaseballTeamNameSections");
            //    }
            //    else if (ddlProgram.Text == "Outreach Basketball")
            //    {
            //        da.Fill(ds, "OutreachBasketballTeamNameSections");
            //    }
            //    else if (ddlProgram.Text == "HS Basketball League")
            //    {
            //        da.Fill(ds, "HSBasketballLeagueTeamNameSections");
            //    }
            //    else if (ddlProgram.Text == "MS Basketball League")
            //    {
            //        da.Fill(ds, "MSBasketballLeagueTeamNameSections");
            //    }
            //    else if (ddlProgram.Text == "SoccerIntraMurals")
            //    {
            //        da.Fill(ds, "SoccerIntraMuralsTeamNameSections");
            //    }
            //    else if (ddlProgram.Text == "SoccerTEAMS")
            //    {
            //        da.Fill(ds, "SoccerTEAMSTeamNameSections");
            //    }
            //    else if (ddlProgram.Text == "3on3 Basketball")
            //    {
            //        da.Fill(ds, "[3on3BasketballTeamNameSections]");
            //    }
            //    else if (ddlProgram.Text == "Bible Study")
            //    {
            //        da.Fill(ds, "[BibleStudyTeamNameSections]");
            //    }
            //    else if (ddlProgram.Text == "Monday Nights")
            //    {
            //        da.Fill(ds, "[MondayNightsTeamNameSections]");
            //    }
            //    gvTeamSections.DataSource = ds.Tables[0];
            //    gvTeamSections.DataBind();
            //    gvTeamSections.Visible = true;
            //    gvStudentList.Visible = false;
            //    con.Close();
            //}
            //catch (Exception lkjl)
            //{

            //}
            //finally
            //{
            //}
        }

        protected void cmbLoadData_Click(object sender, EventArgs e)
        {
            string sqlInsertStatement = "";

            try
            {
                con.Open();

                string selectSQL = "";

                selectSQL = "select school, neighborhood, neighborhoodregion, zipcode, syscreate, sysupdate, ID " +
                            "from UIF_PerformingArts.dbo.schoolnames " +
                            "group by school, neighborhood, neighborhoodregion, zipcode, syscreate, sysupdate, ID " +
                            "order by school ";

                SqlDataReader reader = null;
                SqlCommand cmd = new SqlCommand(selectSQL);

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {	//Retrieve the first record only
                    //ddlNames.Items.Add("Please select a student");
                    do
                    {

                        //Insert a new class record.
                        //string sqlInsertStatement = "";
                        txbClassName.Text = reader.GetString(0);
                        txbClassLocation.Text = reader.GetString(1);
                        txbInstructor.Text = reader.GetString(2);
                        txbDevotionalLeader.Text = reader.GetString(3);
                        Random randomNumber = new Random();
                        string NewNumber = System.Guid.NewGuid().ToString();


                        try
                        {
                            sqlInsertStatement = "";
                            sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.SchoolNames2 " +
                                "values ( "
                                + "'" + txbClassName.Text.Trim() + "',"
                                + "'" + txbClassLocation.Text.Trim() + "', "
                                + "'" + txbInstructor.Text.Trim() + "', "
                                + "'" + txbDevotionalLeader.Text.Trim() + "', "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + System.DateTime.Now.ToString() + "', "
                                + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
                                + "'" + NewNumber.Substring(0, 10) + "') ";

                            con2.Open();

                            //create a SQL command to update record
                            SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con2);
                            if (sqlInsertCommand.ExecuteNonQuery() > 0)
                            {
                                con2.Close();
                                
                                //txbClassName.Visible = false;
                                //txbClassLocation.Visible = false;
                                //txbInstructor.Visible = false;
                                //txbDevotionalLeader.Visible = false;
                                //cmbAddRecord.Visible = false;
                                //lblClassLocation.Visible = false;
                                //lblClassInstructor.Visible = false;
                                //lblClassName.Visible = false;
                                //lblDevotionalLeader.Visible = false;
                                //lblClassInstructor.Visible = false;
                                //lbAddNewEntry.Visible = true;

                                //lbAddNewEntry.Visible = true;
                                //ResetNewPerson();
                                //                                gvStudentList.DataBind();
                                //                                DisplayTheGrid();
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

                        //ddlNames.Items.Add(reader.GetString(0) + "," + reader.GetString(1) + " (" + reader.GetString(2) + ") ");
                    } while (reader.Read());
                    reader.Close();
                    //ddlNames.Text = "Please select a student";
                }
                //btnSearch.Enabled = false;

            }
            catch (Exception lkjlbbaacc)
            {

            }
            finally
            {
                con.Close();
            }


            ////Insert a new class record.
            //string sqlInsertStatement = "";
            //txbClassName.Text = txbClassName.Text.Replace("'", "");
            //txbClassLocation.Text = txbClassLocation.Text.Replace("'", "");
            //txbInstructor.Text = txbInstructor.Text.Replace("'", "");
            //txbDevotionalLeader.Text = txbDevotionalLeader.Text.Replace("'", "");
            //Random randomNumber = new Random();
            //string NewNumber = System.Guid.NewGuid().ToString();

            //try
            //{
            //    sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.SchoolNames " +
            //        "values ( "
            //        + "'" + txbClassName.Text.Trim() + "',"
            //        + "'" + txbClassLocation.Text.Trim() + "', "
            //        + "'" + txbInstructor.Text.Trim() + "', "
            //        + "'" + txbDevotionalLeader.Text.Trim() + "', "
            //        + "'" + System.DateTime.Now.ToString() + "', "
            //        + "'" + System.DateTime.Now.ToString() + "', "
            //        + "'" + Request.QueryString["lastname"] + "," + Request.QueryString["firstname"] + "', "
            //        + "'" + NewNumber.Substring(0, 10) + "') ";

            //    con.Open();

            //    //create a SQL command to update record
            //    SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);
            //    if (sqlInsertCommand.ExecuteNonQuery() > 0)
            //    {
            //        con.Close();
            //        txbClassName.Visible = false;
            //        txbClassLocation.Visible = false;
            //        txbInstructor.Visible = false;
            //        txbDevotionalLeader.Visible = false;
            //        cmbAddRecord.Visible = false;
            //        lblClassLocation.Visible = false;
            //        lblClassInstructor.Visible = false;
            //        lblClassName.Visible = false;
            //        lblDevotionalLeader.Visible = false;
            //        lblClassInstructor.Visible = false;
            //        lbAddNewEntry.Visible = true;

            //        //lbAddNewEntry.Visible = true;
            //        //ResetNewPerson();
            //        gvStudentList.DataBind();
            //        DisplayTheGrid();
            //    }
            //    else
            //    {
            //        //display message that record was NOT updated
            //        //	btnContinue.Visible = false;
            //        //	lblAlert.Visible = true;
            //        //	lblAlert.Text = "No update. Error has occurred.";
            //    }
            //}
            //catch (Exception lkjlaa)
            //{

            //}
            //finally
            //{
            //    //con.Close();
            //}
        
        }        
    }
}