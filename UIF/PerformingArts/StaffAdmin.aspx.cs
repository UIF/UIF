using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using UrbanImpactCommon;
using System.Resources;
using System.Net;
using System.Data.Sql;
using System.Web.Script.Services;


namespace UIF
{
    public partial class StaffAdmin : System.Web.UI.Page
    {
                

        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlConnection con2 = new SqlConnection(connectionString);
        public Boolean flag = false;
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
                    if (((Request.QueryString["LastName"] == "Manners") && (Request.QueryString["FirstName"] == "Ryan")))
                    {
                        //Full-Control.

                        //if (ReadOnly())
                        //{
                            //Make this page, READ ONLY access!...for sections... level 2, READ ONLY!!!
                        //    lbAddNewEntry.Enabled = false;
                        //    gvStudentList.Enabled = false;
                        //}
                        DisplayTheGrid();

                    }
                    else
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


                //con.Open();

                //string selectSQL2 = "";

                //selectSQL2 = "select lastname, firstname, dob " +
                //            "from UIF_PerformingArts.dbo.studentinformation " +
                //            "group by lastname, firstname, dob " +
                //            "order by firstname, lastname ";

                //SqlDataReader reader = null;
                //SqlDataReader reader2 = null;
                //SqlCommand cmd2 = new SqlCommand(selectSQL2);

                //cmd2.Connection = con;
                //reader2 = cmd2.ExecuteReader();
                //if (reader2.Read())
                //{	//Retrieve the first record only
                //    do
                //    {
                //        DateTime d1 = DateTime.Now;
                //        DateTime d2 = new DateTime(System.Convert.ToInt32(reader2.GetSqlValue(2).ToString().Substring(6, 4)), System.Convert.ToInt32(reader2.GetSqlValue(2).ToString().Substring(0, 2)), System.Convert.ToInt32(reader2.GetSqlValue(2).ToString().Substring(3, 2)));
                //        TimeSpan difference = d1.Subtract(d2);

                //        string age = (difference.Days / 365).ToString();
                //        string lastname = reader2.GetSqlValue(0).ToString();
                //        string firstname = reader2.GetSqlValue(1).ToString();
                        
                //        reader2.Close();


                //        con2.Open();
                //        string sqlupdate = "update UIF_PerformingArts.dbo.StudentInformation "
                //                        +  "set age = '" + age + "' "
                //                        +  "where lastname = '" + lastname + "' "
                //                        +  "and firstname = '" + firstname + "' ";

                //        SqlCommand cmd = new SqlCommand(sqlupdate);
                //        cmd.Connection = con;
                //        cmd.ExecuteNonQuery();
                //        con.Close();
                    
                //    } while (reader2.Read());
                //    reader2.Close();
                //}


        protected void cmbInsertNew_Click(object sender, EventArgs e)
        {
            if (cmbInsertNew.Text == "Add New Staff Member")
            {
                cmbInsertNew.Text = "Commit New Staff Info";
                txbPassword.Visible = true;
                txbUsername.Visible = true;
                txbComments.Visible = true;
                txbDepartment.Visible = true;
                txbRole.Visible = true;
                chbActiveStaffMember.Visible = true;
                txbLastName.Visible = true;
                txbFirstName.Visible = true;
                lblComments.Visible = true;
                lblDepartment.Visible = true;
                lblFirstName.Visible = true;
                lblLastName.Visible = true;
                lblRole.Visible = true;
                lblUserName.Visible = true;
                lblPassWord.Visible = true;
            }
            else if (cmbInsertNew.Text == "Commit New Staff Info")
            {
                try
                {
                    string sql_InsertNewStaff = "INSERT INTO staffmembers  values ("
                                              + "'888', "
                                              + "'" + txbLastName.Text.Trim() + "', "
                                              + "'" + txbFirstName.Text.Trim() + "', "
                                              + "'" + txbDepartment.Text.Trim() + "', "
                                              + Convert.ToInt32(chbActiveStaffMember.Checked) + ", "
                                              + "'" + txbRole.Text.Trim() + "', "
                                              + "'" + txbComments.Text.Trim() + "', "
                                              + "'" + System.DateTime.Now.ToString() + "', "
                                              + "'" + System.DateTime.Now.ToString() + "', "
                                              + "'" + txbUsername.Text.Trim() + "', "
                                              + "'" + txbPassword.Text.Trim() + "') ";
                    con.Open();

                    //create a SQL command to update record
                    SqlCommand sqlInsertCommand_ParentGuard = new SqlCommand(sql_InsertNewStaff, con);
                    if (sqlInsertCommand_ParentGuard.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        ResetNewPerson();
                        gvStaffAdmin.DataBind();
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
                catch
                {

                }
            }
        }


        protected void DisplayTheGrid()
        {
            Bind();
        }


        protected void Bind()
        {
            con.Open();
            try
            {
                string sql_LoadGrid = "select lastname, firstname, department, activestaffmember, role, comments, username, password, id " 
                                    + "from staffmembers order by lastname, firstname ";

                SqlDataAdapter da = new SqlDataAdapter(sql_LoadGrid, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Staffmembers");
                gvStaffAdmin.DataSource = ds.Tables[0];
                gvStaffAdmin.DataBind();
                con.Close();
            }
            catch (Exception lkjl_)
            {
                string lkjl = "";
            }
        }

        protected void ResetNewPerson()
        {
            cmbInsertNew.Text = "Add New Staff Member";
            txbPassword.Visible = false;
            txbUsername.Visible = false;
            txbComments.Visible = false;
            txbDepartment.Visible = false;
            txbRole.Visible = false;
            chbActiveStaffMember.Visible = false;
            txbLastName.Visible = false;
            txbFirstName.Visible = false;
            txbPassword.Text = "";
            txbUsername.Text = "";
            txbComments.Text = "";
            txbDepartment.Text = "";
            txbRole.Text = "";
            chbActiveStaffMember.Checked = false;
            txbLastName.Text = "";
            txbFirstName.Text = "";
            lblComments.Visible = false;
            lblDepartment.Visible = false;
            lblFirstName.Visible = false;
            lblLastName.Visible = false;
            lblRole.Visible = false;
            lblUserName.Visible = false;
            lblPassWord.Visible = false;
        }
        
        protected void txbUsername_TextChanged(object sender, EventArgs e)
        {

        }

        //protected void gvStaffAdmin_RowEditing(object sender, EventArgs e)
        //{
        //    gvStaffAdmin.EditIndex = e.NewEditIndex;
        //    Bind();
        //}

        protected void gvStaffAdmin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStaffAdmin.EditIndex = e.NewEditIndex;
            Bind();
        }

        protected void gvStaffAdmin_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            gvStaffAdmin.DeleteRow(e.RowIndex);
            gvStaffAdmin.DataBind();
        }

        protected void gvStaffAdmin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStaffAdmin.EditIndex = -1;
            Bind();
        }

        protected void gvStaffAdmin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvStaffAdmin.Rows[e.RowIndex];
            //Label lbl = (Label)row.FindControl("lblid");
            TextBox lastname = (TextBox)row.FindControl("textbox1");
            TextBox firstname = (TextBox)row.FindControl("textbox2");
            TextBox department = (TextBox)row.FindControl("textbox3");
            TextBox role = (TextBox)row.FindControl("textbox4");
            TextBox comments = (TextBox)row.FindControl("textbox5");
            TextBox username = (TextBox)row.FindControl("textbox6");
            TextBox password = (TextBox)row.FindControl("textbox7");
            TextBox ID = (TextBox)row.FindControl("textbox8");
            //TextBox Active = (CheckBox)row.FindControl("checkbox1");

            gvStaffAdmin.EditIndex = -1;
            con.Open();
            SqlCommand cmd = new SqlCommand("Update staffmembers set "
                                          + "  lastname='" + txbLastName.Text
                                          + "' , firstname='" + txbFirstName.Text
                                          + "' , department='" + txbDepartment.Text
                                          + "' , role='" + txbRole.Text
                                          + " ," + chbActiveStaffMember.Checked + " "
                                          + "' , comments='" + txbComments.Text
                                          + "' , username='" + txbUsername.Text
                                          + "' , password='" + txbPassword.Text
                                          + "' , sysupdate='" + System.DateTime.Now.ToString() + "' "
                                          + " where ID = '" + ID.Text + "' ");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            Bind();
        }

        protected void gvStaffAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        protected void gvVolunteerAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvVolunteerAttendance_RowDataBound(object sender, EventArgs e)
        {

        }

        protected void lbhistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistoricalReports.aspx?Security=Good&lastname=" + Request.QueryString["lastname"] + "&firstname=" + Request.QueryString["firstname"] + "&Dept=" + Request.QueryString["Dept"]);

        }
    }
}