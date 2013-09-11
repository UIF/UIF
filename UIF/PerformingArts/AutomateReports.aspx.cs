using System;
using System.IO;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data;
using System.Text;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web.Mail;
using System.Data.OleDb;
using System.Threading;
using System.Diagnostics;
using UrbanImpactCommon;


namespace AutomateReports

{
	/// <summary>
	/// Author: Ryan C Manners  May 12th, 2009.
	/// Wrote this to automate the report that I generate each morning.
	/// </summary>
	/// 
	public class AutomateReports
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		///
		//Parameterized global variables...RCM...6/25/04.
		private static string connectionString = "UID=xmlorder;PWD=mightyone;Data Source=ECOMM-SQLCLU01;";//OrderHistory db connection string.
		private static string TeraData_connectionString = "DSN=Teradata;UID=catmatch;PWD=catmatch";//Teradata db connection string.
		private static string CoPA_Email = "rmanners@wescodist.com";//2ND TradePower Email Address.
		private static string CC_EmailAddress = "rmanners@wescodist.com;tschneider@wescodist.com;jdegrano@wescodist.com;lizjones@wescodist.com;mkrause@wescodist.com";//CC Email Address.
		private static string From_EmailAddress = "rmanners@wescodist.com";//From Email Address.
		private static string Mail_ServerName = "email.wescodist.com";//Mail Server to be used for SMTP.
		public static string Log_Location = "e:\\CoPADailyReport\\DataFiles\\";//Directory location of files.
		public static string Log_FTP_Location = "e:\\CoPADailyReport\\DataFiles\\OutboundFTP\\";//Directory location of outgoing ftp files..

		//Determine the date to use for running..Always uses the previous days date..RCM.
		public static System.DateTime PreviousDaysDate = System.DateTime.Today.Subtract(System.TimeSpan.FromDays(1));
		//The sql statements need the date in a different format..
		public static string run_date = PreviousDaysDate.ToString("yyyy-MM-dd");

		//Declaration of globally used FileStream writer, and StreamWriter..RCM.
		public static FileStream sb = null;
		public static StreamWriter sw = null;

		public static void Main(string []args)
		{				
			ReadSetupTable(connectionString);
						
			//Declare the email variables to be used..
			MailMessage Message = new MailMessage();
			Message.To = CoPA_Email;
			Message.From = From_EmailAddress;
			Message.Cc = CC_EmailAddress;
			Message.Subject = "CoPA Invoices " + System.DateTime.Today.DayOfWeek;
			Message.Attachments.Add(new MailAttachment(Log_Location + "CoPA Invoice Report-" + PreviousDaysDate.ToString("MM-dd-yy") + ".WP"));

			try
			{
				SmtpMail.SmtpServer = Mail_ServerName;//Initializes the MailServer Name.
				SmtpMail.Send(Message);//Actually sends the email.
			}
			catch(System.Web.HttpException ehttp)
			{
				Console.WriteLine("{0}", ehttp.Message);
				Console.WriteLine("Here is the full error message output");
				Console.Write("{0}", ehttp.ToString());
			}
			catch(IndexOutOfRangeException)
			{
				//usage use = new usage();
				//use.DisplayUsage();
			}
			catch(System.Exception e)
			{
				Console.WriteLine("Unknown Exception occurred {0}", e.Message);
				Console.WriteLine("Here is the Full Message output");
				Console.WriteLine("{0}", e.ToString());
			}
		}
		
		public static string ReadSetupTable(string connString)
		{
			string jipp = "";//return nothing.

			SqlConnection ecommConn = new SqlConnection(connString);//Establishes the db connection.
			ecommConn.Open();//Opens the db connection.

			//Setup filestream to be written to flat file..RCM.
			sb = new FileStream(Log_Location + "CoPA Invoice Report-" + PreviousDaysDate.ToString("MM-dd-yy") + ".WP", FileMode.OpenOrCreate);
			sw = new StreamWriter(sb);

			try 
			{	//Read the records out of the setup table..and then iterate over them..RCM.
				string sqlStr = "";
				sqlStr = "select * from xcblordertest.dbo.xmltransactionstatus " +
					     "where portalsource = 'CoPA' " +
						 "and syscreate > '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' " +
					     "and transactiontype = 'Invoice_acknowledgment' " +
						 "order by syscreate desc ";
									
				SqlCommand cmd = new SqlCommand(sqlStr,ecommConn);
				cmd.CommandTimeout = 120000;//Connection Timeout 2 minutes.
				SqlDataAdapter custDA = new SqlDataAdapter();
				custDA.SelectCommand = cmd;
				DataSet custDS = new DataSet();
				custDA.Fill(custDS,"xcblordertest.dbo.XmlTransactionStatus");
						
				sw.WriteLine("          Successfully Acknowledged      " + ("\r\n"));

				//Iterate over setup records and call method to do the work on each one...RCM..
				foreach (DataRow myDataRowPO in custDS.Tables["xcblordertest.dbo.xmltransactionstatus"].Rows)
				{					
					sw.WriteLine(myDataRowPO["portalsource"].ToString() + " | " 
					+ myDataRowPO["transactiontype"].ToString() + " | "
					+ myDataRowPO["relativepo"].ToString() + " | "
					+ myDataRowPO["payloadmessage"].ToString() + " | "
					+ myDataRowPO["syscreate"].ToString());
				}
				custDS.Clear();
				//custDS == null;
			//	sw.Close();//Closes the writer.
			//	ecommConn.Close();//Closes the OrderHistory db connection..
			//	ecommConn.Dispose();//Destroys the OrderHistory db connection.
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{

			}


			try 
			{	//Read the records out of the setup table..and then iterate over them..RCM.
				string sqlStr2 = "";
				sqlStr2 = "select * from xcblordertest.dbo.outboundinvoicerejects " +
					      "where portalsource = 'CoPA' " +
					      "and syscreate > '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' " +
					      "order by syscreate desc ";
									
				SqlCommand cmd2 = new SqlCommand(sqlStr2,ecommConn);
				cmd2.CommandTimeout = 120000;//Connection Timeout 2 minutes.
				SqlDataAdapter custDA2 = new SqlDataAdapter();
				custDA2.SelectCommand = cmd2;
				DataSet custDS2 = new DataSet();
				custDA2.Fill(custDS2,"xcblordertest.dbo.OutboundInvoiceRejects");
						
				sw.WriteLine(("\r\n") + "          Rejected Invoices      " + ("\r\n"));

				//Iterate over setup records and call method to do the work on each one...RCM..
				foreach (DataRow myDataRowPO2 in custDS2.Tables["xcblordertest.dbo.outboundinvoicerejects"].Rows)
				{					
					sw.WriteLine(myDataRowPO2["portalsource"].ToString() + " | " 
						+ myDataRowPO2["customernum"].ToString() + " | "
						+ myDataRowPO2["invoicenum"].ToString() + " | "
						+ myDataRowPO2["relativeponum"].ToString() + " | "
						+ myDataRowPO2["error"].ToString() + " | "
						+ myDataRowPO2["syscreate"].ToString());
				}
				custDS2.Clear();
				//custDS == null;
				sw.Close();//Closes the writer.
				ecommConn.Close();//Closes the OrderHistory db connection..
				ecommConn.Dispose();//Destroys the OrderHistory db connection.
			}
			catch(Exception eg)
			{
				throw eg;
			}
			finally
			{

			}
			return jipp;
		}
	}
}
