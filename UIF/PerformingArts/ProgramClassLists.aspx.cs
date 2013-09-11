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
    public partial class ProgramClassLists : System.Web.UI.Page
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
                    //lblProgram.Visible = false;
                    //cmbProgram.Visible = false;
                    //ddlProgram.Enabled = false;
                    //ddlProgram.Visible = false;

                    ddlTime.Items.Add("Select a time.");
                    ddlTime.Items.Add("4:30-6:00 Class");
                    ddlTime.Items.Add("6:30-8:00 Class");
                    ddlTime.Text = "Select a time.";

                    con.Open();//Opens the db connection.
                    string sql_LoadGrid = "";

                    sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', sizelimit as 'SizeLimit', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                                 + "FROM PerformingArtsAcademyAvailableClasses  order by classname";

                    SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "PerformingArtsAcademyAvailableClasses");
                    gvStudentList.DataSource = ds.Tables[0];
                    gvStudentList.DataBind();
                    con.Close(); 

                    //ddlProgram.Items.Add("Childrens Choir");
                    //ddlProgram.Items.Add("MSHS Choir");
                    //ddlProgram.Items.Add("PerformingArtsAcademy");
                    //ddlProgram.Text = "PerformingArtsAcademy";
                }
                else
                {
                    //Ryan C Manners..1/5/11
                    //Do NOT ALLOW ACCESS TO THE PAGE!
                    Response.Redirect("ErrorAccess.aspx");
                }
            }
        }

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
             sql_LoadGrid = "select classname as 'Name', meettime as 'Time', meetday as 'Day', location as 'Location', sizelimit as 'SizeLimit', comments as 'Comments', instructor as 'Instructor', devotionalleader as 'DevotionalLeader', id as 'id' "
                          + "FROM PerformingArtsAcademyAvailableClasses  order by classname";
            
             SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);  
             DataSet ds = new DataSet();  
             da.Fill(ds, "PerformingArtsAcademyAvailableClasses");  
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
            string ID = gvStudentList.DataKeys[e.RowIndex].Values[0].ToString();
            RemoveFromTable(sender, e, ID);
        }

        protected void RemoveFromTable(object sender, GridViewDeleteEventArgs e, string ID)
        {
            string sql_DeleteFromClass1 = "";
            try
            {
                sql_DeleteFromClass1 = "Delete from UIF_PerformingArts.dbo.PerformingArtsAcademyAvailableClasses "
                                     + "WHERE classname = '" + ID + "' ";

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
            SqlCommand cmd = new SqlCommand("Update PerformingArtsAcademyAvailableClasses set "
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
            Response.Redirect("MenuTest.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        }

        protected void MenuBest_MenuItemClick(object sender, MenuEventArgs e)
        {
            UIFCommon menucontrol = new UIFCommon();
            menucontrol.MenuControlBehavior(e, Request, Response);
        }

        protected void cmbExcelExport_Click(object sender, EventArgs e)
        {
            ExcelUtility.ExcelUtility ExcelExport = new ExcelUtility.ExcelUtility();
            ExcelExport.ExportGridView(gvStudentList, Response);
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                Random randomNumber = new Random();
                
                try
                {
                    sqlInsertStatement = "INSERT into UIF_PerformingArts.dbo.PerformingArtsAcademyAvailableClasses " +
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
                        + "'" + randomNumber.Next(1, 500) +"', "
                        + System.Convert.ToInt32(txbSizeLimit.Text.Trim()) + ") ";

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
            Response.Redirect("ProgramClassLists.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);
        } 
    }
}