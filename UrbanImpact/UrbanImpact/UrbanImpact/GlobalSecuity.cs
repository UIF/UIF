using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Web;
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
    public class GlobalSecuity
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);

        public Boolean SetAFlag(Boolean waitinglist)
        {
            Boolean FromTheWaitingList = false;
            FromTheWaitingList = waitinglist;
            return FromTheWaitingList;
        }

        public Boolean RestrictAccess(ref string AccessLevel, ref Boolean partialaccess, ref Boolean fullaccess,string LastName, string FirstName, string WebPageName)
        {
            Boolean RestrictAccess = false;

            try
            {
                // Put user code to initialize the page here
                SqlDataReader reader = null;

                con.Open();//Opens the db connection.

                string SecurityCheck = "select AccessLevel, PartialReadONLYAccess, FullAccess, "
                                     + "Department, Role, Comment, SysCreate, sysupdate, LastUpdatedBy "
                                     + "from GlobalStaffSecurityProfiles "
                                     + "where LastName=@lastname "
                                     + "and FirstName=@firstname "
                                     + "and WebPageName=@webpagename ";

                //Perform database lookup based on the chosen child..RCM..
                SqlCommand cmd = new SqlCommand(SecurityCheck);
                cmd.Parameters.Add(new SqlParameter("@lastname", LastName.Trim()));
                cmd.Parameters.Add(new SqlParameter("@firstname", FirstName.Trim()));
                cmd.Parameters.Add(new SqlParameter("@webpagename", WebPageName.Trim()));
                //cmd.Parameters.Add(new SqlParameter("@lastname", Request.QueryString["StudentLastName"].Trim()));
                //cmd.Parameters.Add(new SqlParameter("@firstname", Request.QueryString["StudentFirstName"].Trim()));

                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    fullaccess = reader.GetBoolean(2);
                    partialaccess = reader.GetBoolean(1);
                    AccessLevel = reader.GetString(0);

                    if (fullaccess == false)
                    {
                        RestrictAccess = true;
                    }
                }

            }
            catch (Exception lkjl)
            {

            }
            finally
            {

            }
            return RestrictAccess;
        }
    }
}
