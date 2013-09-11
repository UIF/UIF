using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Net;
using System.Net.Mail;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Threading;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;


namespace ExcelUtility
{
	/// <summary>
	/// Author: Ryan C Manners  May 12th, 2009.
	/// Wrote this to automate the report that I generate each morning.
	/// </summary>
	/// 
	public class ExcelUtility
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
			//ReadSetupTable(connectionString);
						
			//Declare the email variables to be used..
			MailMessage Message = new MailMessage();
            Message.To.Add("rmanners@verizon.net");

			//Message.To = CoPA_Email;
			//Message.From = From_EmailAddress;
			//Message.Cc = CC_EmailAddress;
			//Message.Subject = "CoPA Invoices " + System.DateTime.Today.DayOfWeek;
			//Message.Attachments.Add(new MailAttachment(Log_Location + "CoPA Invoice Report-" + PreviousDaysDate.ToString("MM-dd-yy") + ".WP"));

			try
			{
				//SmtpMail.SmtpServer = Mail_ServerName;//Initializes the MailServer Name.
				//SmtpMail.Send(Message);//Actually sends the email.
			}
            //catch(System.Web.HttpException ehttp)
            //{
            //    Console.WriteLine("{0}", ehttp.Message);
            //    Console.WriteLine("Here is the full error message output");
            //    Console.Write("{0}", ehttp.ToString());
            //}
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


        public void ExportGridView(GridView TheGridview, System.Web.HttpResponse Response, int lkj)
        {
            PrepareGridViewForExport(TheGridview);

            string attachment = "attachment; filename=Contacts.xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            TheGridview.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }


        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}


        public void PrepareDataSetForExport(Control ds)
        {

            LinkButton lb = new LinkButton();
            Literal l = new Literal();

            string name = String.Empty;
            for (int i = 0; i < ds.Controls.Count; i++)
            {
                if (ds.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (ds.Controls[i] as LinkButton).Text;
                    ds.Controls.Remove(ds.Controls[i]);
                    ds.Controls.AddAt(i, l);
                }
                else if (ds.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (ds.Controls[i] as DropDownList).SelectedItem.Text;
                    ds.Controls.Remove(ds.Controls[i]);
                    ds.Controls.AddAt(i, l);
                }
                else if (ds.Controls[i].GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                {
                    l.Text = (ds.Controls[i] as System.Web.UI.WebControls.CheckBox).Checked ? "True" : "False";
                    ds.Controls.Remove(ds.Controls[i]);
                    ds.Controls.AddAt(i, l);
                }

                if (ds.Controls[i].HasControls())
                {
                    PrepareDataSetForExport(ds.Controls[i]);
                }
            }
        }
                       
        public void PrepareGridViewForExport(Control gv)
        {

            LinkButton lb = new LinkButton();
            Literal l = new Literal();

            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                {
                    l.Text = (gv.Controls[i] as System.Web.UI.WebControls.CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }

                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }
        }
        //Implementation Options: 

        //In quite a few cases, developers face an error in the Export functionality - typically the error message is "RegisterForEventValidation can only be called during Render();". 
        //Our website readers have contributed some good suggestions in the article comments below. I would particularly like to highlight the suggestion by Marianna, who provides an alternative implementation to the VerifyRenderingInServerForm override. This approach is described below: 

        //Step 1: Implement the Export functionality as described above. 
        //Step 2: Remove the code to override the VerifyRenderingInServerForm method. 
        //Step 3: Modify the code for the ExportGridView function as below. The code highlighted in green creates and HtmlForm on the fly, before exporting the gridview, adds the gridview to this new form and renders the form (instead of rendering the gridview in our original implementation) 

        public void ExportGridView(GridView TheGridview, System.Web.HttpResponse Response, string FileName, string WorkSheetName)
        {
            PrepareGridViewForExport(TheGridview);

            string attachment = "attachment; filename=" + FileName + ".xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();
            TheGridview.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(TheGridview);
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }


        public void ExportGridView(GridView TheGridview, System.Web.HttpResponse Response, string FileName)
        {
            PrepareGridViewForExport(TheGridview);

            string attachment = "attachment; filename=" + FileName + ".xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();
            TheGridview.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(TheGridview);
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportGridView(GridView TheGridview, System.Web.HttpResponse Response)
        {
            PrepareGridViewForExport(TheGridview);

            string attachment = "attachment; filename=Contacts.xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();
            TheGridview.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(TheGridview);
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportDataSet(DataSet TheDataset, System.Web.HttpResponse Response)
        {
            //PrepareDataSetForExport(TheDataset);

            string attachment = "attachment; filename=Contacts.xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();
            //TheDataset.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            //frm.Controls.Add(TheDataset);
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public void AddWorksheetToExcelWorkbook(string fullFilename, string worksheetName, GridView gridview)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Workbook xlWorkbook = null;
            Sheets xlSheets = null;
            Worksheet xlNewSheet = null;

            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                //if (xlApp == null)
                //    return;

                // Uncomment the line below if you want to see what's happening in Excel
                // xlApp.Visible = true;

                xlWorkbook = xlApp.Workbooks.Open(fullFilename, 0, false, 5, "", "",
                        false, XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);

                xlSheets = xlWorkbook.Sheets as Sheets;

                // The first argument below inserts the new worksheet as the first one
                xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlNewSheet.Name = worksheetName;


                xlNewSheet.Cells.Insert(0, 0);

                //---------------------
                int i = 0;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.Text = String.Format(worksheetName, i);
                row.Cells.Add(cell);
                gridview.Controls[0].Controls.AddAt(i, row);

                //---------------------
                xlWorkbook.Save();
                xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
            catch (Exception lkjlklkabb)
            {

            }
            finally
            {
                //Marshal.ReleaseComObject(xlNewSheet);
                //Marshal.ReleaseComObject(xlSheets);
                //Marshal.ReleaseComObject(xlWorkbook);
                //Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }
        }


        //public void GenerateExcelFile(System.Data.DataTable dt, string paramFileFullPath)
        //{
        //    // Global missing reference for objects we are not defining...
        //    object missing = System.Reflection.Missing.Value;

        //    // Create an Excel object and add workbook...
        //    ApplicationClass excel = new ApplicationClass();
        //    Workbook workbook = excel.Application.Workbooks.Add(true);

        //    //Rename the first/default sheet name
        //    if (dt.TableName.ToString().Trim() != string.Empty)
        //    {
        //        Worksheet ws = (Worksheet)excel.Worksheets.get_Item(1);
        //        ws.Name = dt.TableName.ToString();
        //        //(Worksheet)excel.Worksheets.Add(1,
        //    }

        //        //---------------------
        //            //If you don't already, you must have add a COM reference in your project to the "Microsoft Excel 11.0 Object Library" - or whatever version is appropriate.

        //            //This code works for me...

        //        //---------------------

        //    int iCol = 0;
        //    // Add column headings...
        //    if (_IsHeaderIncluded == true)
        //    {
        //        foreach (DataColumn c in dt.Columns)
        //        {
        //            iCol++;
        //            excel.Cells[1, iCol] = c.ColumnName;
        //        }
        //    }

        //    // for each row of data...
        //    int iRow = 0;
        //    foreach (DataRow r in dt.Rows)
        //    {
        //        iRow++;
        //        // add each row's cell data...
        //        iCol = 0;
        //        foreach (DataColumn c in dt.Columns)
        //        {
        //            iCol++;
        //            if (_IsHeaderIncluded == true)
        //            {
        //                excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
        //            }
        //            else
        //            {
        //                excel.Cells[iRow, iCol] = r[c.ColumnName];
        //            }
        //        }
        //    }

        //    // If wanting to Save the workbook...
        //    workbook.SaveAs(paramFileFullPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, missing, missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);

        //    // If wanting to make Excel visible and activate the worksheet...
        //    excel.Visible = true;
        //    Worksheet worksheet = (Worksheet)excel.ActiveSheet;
        //    ((_Worksheet)worksheet).Activate();

        //    // If wanting excel to shutdown...
        //    //((_Application)excel).Quit();
        //}


	}
}
