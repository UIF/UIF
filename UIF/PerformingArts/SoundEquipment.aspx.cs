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
    public partial class SoundEquipment : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public event GridViewEditEventHandler RowEditing;
        public Boolean flag = false;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["security"] == "Good")
            {

                string sql = "";
                sql = "Select * from PerformingArtsSoundEquipment ";

                con.Open();//Opens the db connection.
                string sql_LoadGrid = "";

                sql = "Select * from PerformingArtsSoundEquipment ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(sql);

                cmd.Connection = con;
                gvSoundEquipment.DataSource = cmd.ExecuteReader();
                gvSoundEquipment.DataBind();
                lblSoundEquipment.Visible = true;
                lblSoundEquipment.Enabled = true;
            }
            else
            {
                //Ryan C Manners..1/5/11
                //Do NOT ALLOW ACCESS TO THE PAGE!
                Response.Redirect("ErrorAccess.aspx");
            }
        }
    }
}