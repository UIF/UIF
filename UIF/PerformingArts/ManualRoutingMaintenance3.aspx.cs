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
using System.Data.SqlClient;
using UrbanImpactCommon;
//using Wesco.Ecomm.Integration.Common;
//using System.Windows.Forms;

namespace EDISupport
{
    public class ManualRoutingMaintenance : System.Web.UI.Page
    {
        public static string connectionString = UrbanImpactCommon.DatabaseUtility.UIF_Server_connectionString;
        public SqlConnection con = new SqlConnection(connectionString);        
        
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
            this.ddlPortal.SelectedIndexChanged += new System.EventHandler(this.ddlPortal_SelectedIndexChanged);
            this.ddlCustName.SelectedIndexChanged += new System.EventHandler(this.ddlCustName_SelectedIndexChanged);
            this.ddlCustId.SelectedIndexChanged += new System.EventHandler(this.ddlCustId_SelectedIndexChanged);
            this.ddlShipToCode.SelectedIndexChanged += new System.EventHandler(this.ddlShipToCode_SelectedIndexChanged);
            this.ddlBranch.SelectedIndexChanged += new System.EventHandler(this.ddlBranch_SelectedIndexChanged);
            this.ddlCustDpc.SelectedIndexChanged += new System.EventHandler(this.ddlCustDpc_SelectedIndexChanged);
            this.ddlIsDefault.SelectedIndexChanged += new System.EventHandler(this.ddlIsDefault_SelectedIndexChanged);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnNewSearch.Click += new System.EventHandler(this.btnNewSearch_Click);
            this.LinkBtnUpdate.Click += new System.EventHandler(this.LinkBtnUpdate_Click);
            this.LinkBtnDelete.Click += new System.EventHandler(this.LinkBtnDelete_Click);
            this.LinkBtnNew.Click += new System.EventHandler(this.LinkBtnNew_Click);
            this.cbxPortal.CheckedChanged += new System.EventHandler(this.cbxPortal_CheckedChanged);
            this.cbxCustName.CheckedChanged += new System.EventHandler(this.cbxCustName_CheckedChanged);
            this.cbxCustId.CheckedChanged += new System.EventHandler(this.cbxCustId_CheckedChanged);
            this.cbxShipToCode.CheckedChanged += new System.EventHandler(this.cbxShipToCode_CheckedChanged);
            this.cbxBranch.CheckedChanged += new System.EventHandler(this.cbxBranch_CheckedChanged);
            this.cbxCustDpc.CheckedChanged += new System.EventHandler(this.cbxCustDpc_CheckedChanged);
            this.cbxIsDefault.CheckedChanged += new System.EventHandler(this.cbxIsDefault_CheckedChanged);
            this.cbxCustLocation.CheckedChanged += new System.EventHandler(this.cbxCustLocation_CheckedChanged);
            this.cbxWesnetShipCode.CheckedChanged += new System.EventHandler(this.cbxWesnetShipCode_CheckedChanged);
            this.cbxCustShipZip.CheckedChanged += new System.EventHandler(this.cbxCustShipZip_CheckedChanged);
            this.cbxGhostId.CheckedChanged += new System.EventHandler(this.cbxGhostId_CheckedChanged);
            this.cbxCustIdType.CheckedChanged += new System.EventHandler(this.cbxCustIdType_CheckedChanged);
            this.cbxBlanketPo.CheckedChanged += new System.EventHandler(this.cbxBlanketPo_CheckedChanged);
            this.cbxBlanketPo2.CheckedChanged += new System.EventHandler(this.cbxBlanketPo2_CheckedChanged);
            this.cbxBuyerCode.CheckedChanged += new System.EventHandler(this.cbxBuyerCode_CheckedChanged);
            this.btnUpdateRecord.Click += new System.EventHandler(this.btnUpdateRecord_Click);
            this.btnNewRecord.Click += new System.EventHandler(this.btnNewRecord_Click);
            this.btnDeleteRecord.Click += new System.EventHandler(this.btnDeleteRecord_Click);
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.dgrdReturnValues.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrdReturnValues_PageIndexChanged);
            this.dgrdReturnValues.SelectedIndexChanged += new System.EventHandler(this.dgrdReturnValues_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        int intIsDefaultValue;
        string strPortalSource = "";
        string strCustName = "";
        string strCustId = "";
        string strShipToCode = "";
        string strServicingBranch = "";
        string strCustDpc = "";
        string strIsDefault = "";
        string strCustLocation = "";
        string strCustShipZip = "";
        string strwesnetshipcode = "";
        string strghostid = "";
        string strCustIDType = "";
        string strBlanketPO = "";
        string strBlanketPO2 = "";
        string strBuyerCode = "";
        string strsyscreate = "";
        string strAND = "";
        string SQL_Select_Statement = "SELECT PortalSource AS 'Portal Source' , CustName AS 'Customer Name' , CustId As 'Customer ID' , ShipToCode AS 'Ship To Code', ServicingBranch As 'Branch' , CustDpc As 'DPC' , IsDefault ,  CustLocation  AS 'Location', CustShipZip As 'Zip' ,  wesnetshipcode  As 'Wesnet Ship Code' , ghostid AS 'Ghost ID', CustIDType As 'Type' , BlanketPO  As 'Blanket PO' , BlanketPO2 As 'Blanket PO 2' , BuyerCode As 'Buyer Code' ,   syscreate AS 'Date Created', sysupdate AS 'Last Updated'  ";

        private static string conString = "Initial Catalog=XcblOrderTest;Data Source=ecomm-sqlclu01;User=xmlorder;Password=mightyone";
        //private static SqlConnection con = new SqlConnection();
        protected System.Web.UI.WebControls.Panel PanelSearch;
        protected System.Web.UI.WebControls.Label lblManualRouting;
        protected System.Web.UI.WebControls.DropDownList ddlPortal;
        protected System.Web.UI.WebControls.DropDownList ddlCustName;
        protected System.Web.UI.WebControls.DropDownList ddlCustId;
        protected System.Web.UI.WebControls.DropDownList ddlShipToCode;
        protected System.Web.UI.WebControls.DropDownList ddlBranch;
        protected System.Web.UI.WebControls.DropDownList ddlCustDpc;
        protected System.Web.UI.WebControls.DropDownList ddlIsDefault;
        protected System.Web.UI.WebControls.Button btnSearch;
        protected System.Web.UI.WebControls.Panel PanelModifyRecords;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValPortal;
        protected System.Web.UI.WebControls.TextBox txtPortal;
        protected System.Web.UI.WebControls.CheckBox cbxPortal;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValCustName;
        protected System.Web.UI.WebControls.TextBox txtCustName;
        protected System.Web.UI.WebControls.CheckBox cbxCustName;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValCustId;
        protected System.Web.UI.WebControls.TextBox txtCustId;
        protected System.Web.UI.WebControls.CheckBox cbxCustId;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValShipToCode;
        protected System.Web.UI.WebControls.TextBox txtShipToCode;
        protected System.Web.UI.WebControls.CheckBox cbxShipToCode;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValBranch;
        protected System.Web.UI.WebControls.RegularExpressionValidator RegExpValBranch;
        protected System.Web.UI.WebControls.TextBox txtBranch;
        protected System.Web.UI.WebControls.CheckBox cbxBranch;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValCustDpc;
        protected System.Web.UI.WebControls.RegularExpressionValidator RegExpValDpc;
        protected System.Web.UI.WebControls.TextBox txtCustDpc;
        protected System.Web.UI.WebControls.CheckBox cbxCustDpc;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValIsDefault;
        protected System.Web.UI.WebControls.TextBox txtIsDefault;
        protected System.Web.UI.WebControls.CheckBox cbxIsDefault;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValLocation;
        protected System.Web.UI.WebControls.TextBox txtCustLocation;
        protected System.Web.UI.WebControls.CheckBox cbxCustLocation;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqFldValShipZip;
        protected System.Web.UI.WebControls.TextBox txtCustShipZip;
        protected System.Web.UI.WebControls.CheckBox cbxCustShipZip;
        protected System.Web.UI.WebControls.TextBox txtWesnetShipCode;
        protected System.Web.UI.WebControls.CheckBox cbxWesnetShipCode;
        protected System.Web.UI.WebControls.TextBox txtGhostId;
        protected System.Web.UI.WebControls.TextBox txtCustIdType;
        protected System.Web.UI.WebControls.CheckBox cbxCustIdType;
        protected System.Web.UI.WebControls.TextBox txtBlanketPo;
        protected System.Web.UI.WebControls.CheckBox cbxBlanketPo;
        protected System.Web.UI.WebControls.TextBox txtBlanketPo2;
        protected System.Web.UI.WebControls.CheckBox cbxBlanketPo2;
        protected System.Web.UI.WebControls.TextBox txtBuyerCode;
        protected System.Web.UI.WebControls.CheckBox cbxBuyerCode;
        protected System.Web.UI.WebControls.TextBox txtCreatedDate;
        protected System.Web.UI.WebControls.TextBox txtLastUpdated;
        protected System.Web.UI.WebControls.Button btnUpdateRecord;
        protected System.Web.UI.WebControls.Button btnNewRecord;
        protected System.Web.UI.WebControls.Button btnDeleteRecord;
        protected System.Web.UI.WebControls.Button btnCancel;
        protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
        protected System.Web.UI.WebControls.Label lblAlert;
        protected System.Web.UI.WebControls.Button btnContinue;
        protected System.Web.UI.WebControls.Button btnNewSearch;
        protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidatorShipZip;
        protected System.Web.UI.WebControls.DataGrid dgrdReturnValues;
        protected System.Web.UI.WebControls.Label lblAlertGrid;
        protected System.Web.UI.WebControls.Label lblRequired;
        protected System.Web.UI.WebControls.Label lblReqPortalSource;
        protected System.Web.UI.WebControls.Label lblReqCustomerName;
        protected System.Web.UI.WebControls.Label lblReqCustomerID;
        protected System.Web.UI.WebControls.Label lblReqShipToCode;
        protected System.Web.UI.WebControls.Label lblReqServicingBranch;
        protected System.Web.UI.WebControls.Label lblReqCustomerDPC;
        protected System.Web.UI.WebControls.Label lblReqIsDefault;
        protected System.Web.UI.WebControls.Label lblReqLocation;
        protected System.Web.UI.WebControls.Label lblReqZip;
        protected System.Web.UI.WebControls.Label lblPortalSource;
        protected System.Web.UI.WebControls.Label lblCustomerName;
        protected System.Web.UI.WebControls.Label lblCustomerID;
        protected System.Web.UI.WebControls.Label lblShipToCode;
        protected System.Web.UI.WebControls.Label lblServicingBranch;
        protected System.Web.UI.WebControls.Label lblCustomerDPC;
        protected System.Web.UI.WebControls.Label lblIsDefault;
        protected System.Web.UI.WebControls.CheckBox cbxGhostId;
        protected System.Web.UI.WebControls.Label lblAlertIsDefault;
        protected System.Web.UI.WebControls.RegularExpressionValidator ReqFldValWesnetShipCode;
        protected System.Web.UI.WebControls.Label lblReqWesnetShipCode;
        protected System.Web.UI.WebControls.Label lblAlertWesnetShipCode;
        protected System.Web.UI.WebControls.RegularExpressionValidator RegExpValShipTo;
        protected System.Web.UI.WebControls.Panel PanelChoice;
        protected System.Web.UI.WebControls.LinkButton LinkBtnUpdate;
        protected System.Web.UI.WebControls.LinkButton LinkBtnNew;
        protected System.Web.UI.WebControls.LinkButton LinkBtnDelete;

        private void cbxPortal_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxPortal.Checked)
            {
                txtPortal.Enabled = true;
            }
            else
            {
                txtPortal.Enabled = false;
            }
        }

        private void cbxCustName_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxCustName.Checked)
            {
                txtCustName.Enabled = true;
            }
            else
            {
                txtCustName.Enabled = false;
            }
        }

        private void cbxBranch_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxBranch.Checked)
            {
                txtBranch.Enabled = true;
            }
            else
            {
                txtBranch.Enabled = false;
            }
        }

        private void cbxCustDpc_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxCustDpc.Checked)
            {
                txtCustDpc.Enabled = true;
            }
            else
            {
                txtCustDpc.Enabled = false;
            }
        }
        private void cbxShipToCode_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxShipToCode.Checked)
            {
                txtShipToCode.Enabled = true;
            }
            else
            {
                txtShipToCode.Enabled = false;
            }
        }

        private void cbxCustId_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxCustId.Checked)
            {
                txtCustId.Enabled = true;
            }
            else
            {
                txtCustId.Enabled = false;
            }
        }

        private void cbxCustLocation_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxCustLocation.Checked)
            {
                txtCustLocation.Enabled = true;
            }
            else
            {
                txtCustLocation.Enabled = false;
            }
        }

        private void cbxCustShipZip_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxCustShipZip.Checked)
            {
                txtCustShipZip.Enabled = true;
            }
            else
            {
                txtCustShipZip.Enabled = false;
            }
        }

        private void cbxCustIdType_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxCustIdType.Checked)
            {
                txtCustIdType.Enabled = true;
            }
            else
            {
                txtCustIdType.Enabled = false;
            }
        }

        private void cbxBlanketPo_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxBlanketPo.Checked)
            {
                txtBlanketPo.Enabled = true;
            }
            else
            {
                txtBlanketPo.Enabled = false;
            }
        }

        private void cbxBlanketPo2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxBlanketPo2.Checked)
            {
                txtBlanketPo2.Enabled = true;
            }
            else
            {
                txtBlanketPo2.Enabled = false;
            }
        }

        private void cbxBuyerCode_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxBuyerCode.Checked)
            {
                txtBuyerCode.Enabled = true;
            }
            else
            {
                txtBuyerCode.Enabled = false;
            }
        }

        private void cbxWesnetShipCode_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxWesnetShipCode.Checked)
            {
                txtWesnetShipCode.Enabled = true;
            }
            else
            {
                txtWesnetShipCode.Enabled = false;
            }
        }

        private void cbxGhostId_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxGhostId.Checked)
            {
                txtGhostId.Enabled = true;
            }
            else
            {
                txtGhostId.Enabled = false;
            }
        }

        private void cbxIsDefault_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbxIsDefault.Checked)
            {
                txtIsDefault.Enabled = true;
            }
            else
            {
                txtIsDefault.Enabled = false;
            }
        }

        //Search by one field
        private void Search_Portal()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        //Search by two fields 
        private void Search_Portal_CustName()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "'AND CustName = '" + ddlCustName.SelectedValue +
                "'AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_Branch()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustId()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "'AND CustId= '" + ddlCustId.SelectedValue +
                "'AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "'AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "'AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        //Search by three fields
        private void Search_Portal_CustName_ShipToCode()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_Branch()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_CustId()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_Branch()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_CustId()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND  ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }
        private void Search_Portal_ShipToCode_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }
        private void Search_Portal_Branch_CustId()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_Branch_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }
        private void Search_Portal_Branch_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }
        private void Search_Portal_CustId_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }
        private void Search_Portal_CustId_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }
        private void Search_Portal_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";
            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        //Search by 4 fields
        private void Search_Portal_CustName_ShipToCode_Branch()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_CustId()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_Branch_CustId()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_Branch_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_Branch_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_CustId_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_CustId_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND CustiD = '" + ddlCustId.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_Branch_CustId()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_Branch_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_Branch_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_CustId_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_CustId_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_Branch_CustId_CudtDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_Branch_CustId_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_Branch_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustId_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        //Search by 5 fields
        private void Search_Portal_CustName_ShipToCode_Branch_CustId()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_Branch_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustdPC = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_Branch_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_CustId_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_CustId_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_Branch_CustId_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_Branch_CustId_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_Branch_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_CustId_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_CustId_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_Branch_CustId_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_Branch_CustId_IsDefault()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_Branch_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_Branch_CustId_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND IsDefault = '" + ddlIsDefault.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        //Search by 6 fields
        private void Search_Portal_CustName_ShipToCode_Branch_CustId_CustDpc()
        {
            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_Branch_CustId_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_Branch_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_ShipToCode_CustId_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_CustName_CustId_Branch_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        private void Search_Portal_ShipToCode_Branch_CustId_CustDpc_IsDefault()
        {


            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId  = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }

        //Search by all fields
        private void Search_Portal_CustName_ShipToCode_Branch_CustId_CustDpc_IsDefault()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query =
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource = '" + ddlPortal.SelectedValue +
                "' AND CustName = '" + ddlCustName.SelectedValue +
                "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                "' AND CustId = '" + ddlCustId.SelectedValue +
                "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                "' AND IsDefault = '" + intIsDefaultValue +
                "' AND IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }


        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PanelSearch.Visible = true;
                //Load only the Portal Source dropdown
                Load_Portal_DropDownList();

                ddlCustName.AutoPostBack = true;
                ddlCustId.AutoPostBack = true;
                ddlBranch.AutoPostBack = true;
                ddlCustDpc.AutoPostBack = true;
                ddlShipToCode.AutoPostBack = true;
                ddlIsDefault.AutoPostBack = true;

                ddlCustName.Visible = false;
                ddlCustId.Visible = false;
                ddlBranch.Visible = false;
                ddlCustDpc.Visible = false;
                ddlShipToCode.Visible = false;
                ddlIsDefault.Visible = false;

                lblCustomerName.Visible = false;
                lblServicingBranch.Visible = false;
                lblCustomerDPC.Visible = false;
                lblShipToCode.Visible = false;
                lblCustomerID.Visible = false;
                lblIsDefault.Visible = false;

                ddlCustName.Items.Clear();
                ddlCustName.Items.Clear();
                ddlCustId.Items.Clear();
                ddlBranch.Items.Clear();
                ddlCustDpc.Items.Clear();
                ddlShipToCode.Items.Clear();
                ddlIsDefault.Items.Clear();

                ddlCustName.Items.Add("(Exempt from search)");
                ddlCustId.Items.Add("(Exempt from search)");
                ddlBranch.Items.Add("(Exempt from search)");
                ddlCustDpc.Items.Add("(Exempt from search)");
                ddlShipToCode.Items.Add("(Exempt from search)");
                ddlIsDefault.Items.Add("(Exempt from search)");

                btnNewSearch.Visible = false;
                btnContinue.Visible = false;
                btnCancel.Visible = false;
            }
        }

        private void Load_Portal_DropDownList()
        {
   //         //con = DatabaseUtility.GetSqlConnection(conString);

            string Populate_Portal_Dropdown = "SELECT PortalSource FROM xcblordertest.dbo.custidxref " +
                "WHERE PortalSource IS NOT NULL AND PortalSource <> '' AND IsActive = 1 " +
                "GROUP BY PortalSource " +
                "ORDER BY PortalSource ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(Populate_Portal_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(Populate_Portal_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlPortal.Items.Clear();
            ddlPortal.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlPortal.Items.Add(ds.Tables[0].Rows[i]["PortalSource"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Load_CustName_DropDownList()
        {
           // //con = DatabaseUtility.GetSqlConnection(conString);

            string Populate_CustName_Dropdown = "SELECT CustName " +
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE CustName IS NOT NULL AND CustName <> ''" +
                "AND PortalSource = '" + ddlPortal.SelectedValue +
                "' AND IsActive = 1 " +
                " GROUP BY CustName " +
                " ORDER BY CustName ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(Populate_CustName_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(Populate_CustName_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlCustName.Items.Clear();
            ddlCustName.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlCustName.Items.Add(ds.Tables[0].Rows[i]["CustName"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Load_ShipToCode_DropDownList()
        {
            //con = DatabaseUtility.GetSqlConnection(conString);

            string Populate_ShipToCode_Dropdown = "SELECT ShipToCode " +
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE ShipToCode IS NOT NULL AND ShipToCode <> '' AND PortalSource = '" + ddlPortal.SelectedValue + "' AND IsActive = 1 " +
                " GROUP BY ShipToCode " +
                " ORDER BY ShipToCode ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(Populate_ShipToCode_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(Populate_ShipToCode_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlShipToCode.Items.Clear();
            ddlShipToCode.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlShipToCode.Items.Add(ds.Tables[0].Rows[i]["ShipToCode"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Load_Branch_DropDownList()
        {
         //   //con = DatabaseUtility.GetSqlConnection(conString);

            string Populate_Branch_Dropdown = "SELECT ServicingBranch " +
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE ServicingBranch IS NOT NULL AND ServicingBranch <> '' AND PortalSource = '" + ddlPortal.SelectedValue + "' AND IsActive = 1 " +
                " GROUP BY ServicingBranch " +
                " ORDER BY ServicingBranch ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(Populate_Branch_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add("(Exempt from search)");

            SqlDataAdapter myda = new SqlDataAdapter(Populate_Branch_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlBranch.Items.Add(ds.Tables[0].Rows[i]["ServicingBranch"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Load_CustId_DropDownList()
        {
            //con = DatabaseUtility.GetSqlConnection(conString);

            string Populate_CustId_Dropdown = "SELECT CustId " +
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE CustId IS NOT NULL AND CustId <> ''  AND PortalSource = '" + ddlPortal.SelectedValue + "' AND IsActive = 1 " +
                " GROUP BY CustId " +
                " ORDER BY CustId ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(Populate_CustId_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(Populate_CustId_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlCustId.Items.Clear();
            ddlCustId.Items.Add("(Exempt from search)");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlCustId.Items.Add(ds.Tables[0].Rows[i]["CustId"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Load_CustDpc_DropDownList()
        {
         //   //con = DatabaseUtility.GetSqlConnection(conString);

            string Populate_CustDpc_Dropdown = "SELECT CustDpc " +
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE CustDpc IS NOT NULL AND CustDpc <> '' AND PortalSource = '" + ddlPortal.SelectedValue + "' AND IsActive = 1 " +
                " GROUP BY CustDpc " +
                " ORDER BY CustDpc ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(Populate_CustDpc_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(Populate_CustDpc_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlCustDpc.Items.Clear();
            ddlCustDpc.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlCustDpc.Items.Add(ds.Tables[0].Rows[i]["CustDpc"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Load_IsDefault_DropDownList()
        {
            //con = DatabaseUtility.GetSqlConnection(conString);

            string Populate_IsDefault_Dropdown = "SELECT IsDefault " +
                "FROM xcblordertest.dbo.custidxref " +
                "WHERE IsDefault IS NOT NULL  AND PortalSource = '" + ddlPortal.SelectedValue + "' AND IsActive = 1 " +
                " GROUP BY IsDefault " +
                " ORDER BY IsDefault ";
            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(Populate_IsDefault_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(Populate_IsDefault_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlIsDefault.Items.Clear();
            ddlIsDefault.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlIsDefault.Items.Add(ds.Tables[0].Rows[i]["IsDefault"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Method_Reload_DataGrid()
        {
            Method_Check_IsDefaultValue();

            string Standard_Lookup_Query = " FROM xcblordertest.dbo.custidxref WHERE " +
                " PortalSource = '" + txtPortal.Text.Trim() + "' AND " +
                " CustName = '" + txtCustName.Text.Trim() + "' AND " +
                " CustLocation = '" + txtCustLocation.Text.Trim() + "' AND " +
                " CustShipZip = '" + txtCustShipZip.Text.Trim() + "' AND " +
                " ServicingBranch = '" + txtBranch.Text.Trim() + "' AND " +
                " CustDpc = '" + txtCustDpc.Text.Trim() + "' AND " +
                " CustIDType = '" + txtCustIdType.Text.Trim() + "' AND " +
                " BlanketPO = '" + txtBlanketPo.Text.Trim() + "' AND " +
                " BlanketPO2 = '" + txtBlanketPo2.Text.Trim() + "' AND " +
                " BuyerCode = '" + txtBuyerCode.Text.Trim() + "' AND " +
                " IsDefault = '" + intIsDefaultValue + "' AND " +
                " wesnetshipcode = '" + txtWesnetShipCode.Text.Trim() + "' AND " +
                " ghostid = '" + txtGhostId.Text.Trim() + "' AND " +
                " sysUpdate = '" + DateTime.Now + "' AND " +
                " IsActive = 1 ";

            Method_GetSqlValue(SQL_Select_Statement + Standard_Lookup_Query);
        }
        private void Method_Load_CustName_DropDown(string strSQL_AND_Parameter)
        {
         //   //con = DatabaseUtility.GetSqlConnection(conString);

            string strPopulate_ShipToCode_Dropdown = "SELECT CustName FROM xcblordertest.dbo.custidxref " +
                " WHERE PortalSource = '" + ddlPortal.SelectedValue + "'" +
                " AND IsActive = 1 " +
                " AND CustName IS NOT NULL AND CustName <> ''" +
                strSQL_AND_Parameter +
                " GROUP BY CustName " +
                " ORDER BY CustName ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(strPopulate_ShipToCode_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(strPopulate_ShipToCode_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlCustName.Items.Clear();
            ddlCustName.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlCustName.Items.Add(ds.Tables[0].Rows[i]["CustName"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Method_Load_Branch_DropDown(string strSQL_AND_Parameter)
        {
            //con = DatabaseUtility.GetSqlConnection(conString);

            string strPopulate_ShipToCode_Dropdown = "SELECT ServicingBranch FROM xcblordertest.dbo.custidxref " +
                " WHERE PortalSource = '" + ddlPortal.SelectedValue + "'" +
                " AND IsActive = 1 " +
                " AND ServicingBranch IS NOT NULL AND ServicingBranch <> ''" +
                strSQL_AND_Parameter +
                " GROUP BY ServicingBranch " +
                " ORDER BY ServicingBranch ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(strPopulate_ShipToCode_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(strPopulate_ShipToCode_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlBranch.Items.Add(ds.Tables[0].Rows[i]["ServicingBranch"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Method_Load_CustDpc_DropDown(string strSQL_AND_Parameter)
        {
           // //con = DatabaseUtility.GetSqlConnection(conString);

            string strPopulate_ShipToCode_Dropdown = "SELECT CustDpc FROM xcblordertest.dbo.custidxref " +
                " WHERE PortalSource = '" + ddlPortal.SelectedValue + "'" +
                " AND IsActive = 1 " +
                " AND CustDpc IS NOT NULL AND CustDpc <> ''" +
                strSQL_AND_Parameter +
                " GROUP BY CustDpc " +
                " ORDER BY CustDpc ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(strPopulate_ShipToCode_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(strPopulate_ShipToCode_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlCustDpc.Items.Clear();
            ddlCustDpc.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlCustDpc.Items.Add(ds.Tables[0].Rows[i]["CustDpc"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Method_Load_CustId_DropDown(string strSQL_AND_Parameter)
        {
            //con = DatabaseUtility.GetSqlConnection(conString);

            string strPopulate_ShipToCode_Dropdown = "SELECT CustId FROM xcblordertest.dbo.custidxref " +
                " WHERE PortalSource = '" + ddlPortal.SelectedValue + "'" +
                " AND IsActive = 1 " +
                " AND CustId IS NOT NULL AND CustId <> ''" +
                strSQL_AND_Parameter +
                " GROUP BY CustId " +
                " ORDER BY CustId ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(strPopulate_ShipToCode_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(strPopulate_ShipToCode_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlCustId.Items.Clear();
            ddlCustId.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlCustId.Items.Add(ds.Tables[0].Rows[i]["CustId"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Method_Load_ShipToCode_DropDown(string strSQL_AND_Parameter)
        {
          //  //con = DatabaseUtility.GetSqlConnection(conString);

            string strPopulate_ShipToCode_Dropdown = "SELECT ShipToCode FROM xcblordertest.dbo.custidxref " +
                " WHERE PortalSource = '" + ddlPortal.SelectedValue + "'" +
                " AND IsActive = 1 " +
                " AND ShipToCode IS NOT NULL AND ShipToCode <> ''" +
                strSQL_AND_Parameter +
                " GROUP BY ShipToCode " +
                " ORDER BY ShipToCode ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(strPopulate_ShipToCode_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(strPopulate_ShipToCode_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlShipToCode.Items.Clear();
            ddlShipToCode.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlShipToCode.Items.Add(ds.Tables[0].Rows[i]["ShipToCode"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Method_Load_IsDefault_DropDown(string strSQL_AND_Parameter)
        {
            //con = DatabaseUtility.GetSqlConnection(conString);

            string strPopulate_ShipToCode_Dropdown = "SELECT IsDefault FROM xcblordertest.dbo.custidxref " +
                " WHERE PortalSource = '" + ddlPortal.SelectedValue + "'" +
                " AND IsActive = 1 " +
                strSQL_AND_Parameter +
                " GROUP BY IsDefault " +
                " ORDER BY IsDefault ";

            SqlCommand Populate_Portal_Dropdown_cmd = new SqlCommand(strPopulate_ShipToCode_Dropdown);
            Populate_Portal_Dropdown_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(strPopulate_ShipToCode_Dropdown, conString);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            ddlIsDefault.Items.Clear();
            ddlIsDefault.Items.Add("(Exempt from search)");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlIsDefault.Items.Add(ds.Tables[0].Rows[i]["IsDefault"].ToString());
            }
            con.Close();
            con.Dispose();
        }

        private void Method_Check_Dropdown()
        {
            try
            {
                if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ShipToCode = '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }

                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustDpc = '" + ddlCustDpc.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }

                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Method_Check_IsDefaultValue();

                    strAND = "";
                    strAND = " AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch = '" + ddlBranch.SelectedValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }

                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }

                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Method_Check_IsDefaultValue();

                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Method_Check_IsDefaultValue();

                    strAND = "";
                    strAND = " AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustId = '" + ddlCustId.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue + "' ";

                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ShipToCode = '" + ddlShipToCode.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue +
                        "' AND IsDefault = '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustId = '" + ddlCustId.SelectedValue +
                        "' AND ShipToCode = '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustDpc_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_ShipToCode_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue + "' ";

                    Method_Load_CustId_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustDpc = '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_Branch_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_CustId_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch = '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_ShipToCode_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    strAND = "";
                    strAND = " AND CustName = '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_IsDefault_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName =  '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue + "' ";

                    Method_Load_CustDpc_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName =  '" + ddlCustName.SelectedValue +
                        "' AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue + "' ";

                    Method_Load_CustId_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND CustName =  '" + ddlCustName.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue + "' ";

                    Method_Load_Branch_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    strAND = "";
                    strAND = " AND ServicingBranch =  '" + ddlBranch.SelectedValue +
                        "' AND CustDpc =  '" + ddlCustDpc.SelectedValue +
                        "' AND ShipToCode =  '" + ddlShipToCode.SelectedValue +
                        "' AND CustId =  '" + ddlCustId.SelectedValue +
                        "' AND IsDefault =  '" + intIsDefaultValue + "' ";

                    Method_Load_CustName_DropDown(strAND);
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Method_Search_Dropdown();
                    btnNewSearch.Visible = true;
                }
                else
                {
                    lblAlert.Visible = true;
                    lblAlert.Text = "No records found. Please try again.";
                }
            }
            catch (Exception ex)
            {
                //add code to send error to admin via email
                Session["Exception"] = ex.Message.ToString();
                Response.Redirect("Error.aspx");
            }
        }

        private void Method_Check_IsDefaultValue()
        {
            if (txtIsDefault.Text.ToLower().Trim() == "false" || txtIsDefault.Text.ToLower().Trim() == "False" || ddlIsDefault.SelectedValue == "False" || ddlIsDefault.SelectedValue == "false")
            {
                intIsDefaultValue = 0;
            }
            else if (txtIsDefault.Text.ToLower().Trim() == "true" || txtIsDefault.Text.ToLower().Trim() == "True" || ddlIsDefault.SelectedValue == "True" || ddlIsDefault.SelectedValue == "true")
            {
                intIsDefaultValue = 1;
            }
            else
            {
                //display message that IsDefault should be "False" or "True" NOT lower case "false" or "true"
                lblAlert.Visible = true;
                lblAlert.Text = "IsDefault should be False or True NOT lower case false or true.";
            }
        }

        private void Method_Check_Null_Textbox()
        {
            //If textboxes are null, the textbox will display "&nbsp;" which need to be converted to a blank space.
            if (txtPortal.Text == "&nbsp;") { txtPortal.Text = " ".Trim(); }
            if (txtCustName.Text == "&nbsp;") { txtCustName.Text = " ".Trim(); }
            if (txtCustId.Text == "&nbsp;") { txtCustId.Text = " ".Trim(); }
            if (txtShipToCode.Text == "&nbsp;") { txtShipToCode.Text = " ".Trim(); }
            if (txtBranch.Text == "&nbsp;") { txtBranch.Text = " ".Trim(); }
            if (txtCustDpc.Text == "&nbsp;") { txtCustDpc.Text = " ".Trim(); }
            if (txtIsDefault.Text == "&nbsp;") { txtIsDefault.Text = " ".Trim(); }
            if (txtCustLocation.Text == "&nbsp;") { txtCustLocation.Text = " ".Trim(); }
            if (txtCustShipZip.Text == "&nbsp;") { txtCustShipZip.Text = " ".Trim(); }
            if (txtWesnetShipCode.Text == "&nbsp;") { txtWesnetShipCode.Text = " ".Trim(); }
            if (txtGhostId.Text == "&nbsp;") { txtGhostId.Text = " ".Trim(); }
            if (txtCustIdType.Text == "&nbsp;") { txtCustIdType.Text = " ".Trim(); }
            if (txtBlanketPo.Text == "&nbsp;") { txtBlanketPo.Text = " ".Trim(); }
            if (txtBlanketPo2.Text == "&nbsp;") { txtBlanketPo2.Text = " ".Trim(); }
            if (txtBuyerCode.Text == "&nbsp;") { txtBuyerCode.Text = " ".Trim(); }
            if (txtCreatedDate.Text == "&nbsp;") { txtCreatedDate.Text = " ".Trim(); }
            if (txtLastUpdated.Text == "&nbsp;") { txtLastUpdated.Text = " ".Trim(); }
        }
        private void Method_GetSqlValue(string strSearch)
        {
          //  //con = DatabaseUtility.GetSqlConnection(conString);

            string Standard_Lookup_Query = strSearch;

            SqlCommand Standard_Lookup_Query_cmd = new SqlCommand(Standard_Lookup_Query);
            Standard_Lookup_Query_cmd.Connection = con;

            SqlDataAdapter myda = new SqlDataAdapter(Standard_Lookup_Query_cmd);
            DataSet ds = new DataSet();
            myda.Fill(ds, "xcblordertest.dbo.custidxref");

            dgrdReturnValues.DataSource = ds;
            dgrdReturnValues.DataBind();

            SqlDataReader Standard_Lookup_Query_reader = null;

            Standard_Lookup_Query_reader = Standard_Lookup_Query_cmd.ExecuteReader();
            if (Standard_Lookup_Query_reader.Read())
            {
                strPortalSource = "";
                strCustName = "";
                strShipToCode = "";
                strCustLocation = "";
                strCustShipZip = "";
                strServicingBranch = "";
                strCustDpc = "";
                strCustIDType = "";
                strBlanketPO = "";
                strBlanketPO2 = "";
                strBuyerCode = "";
                strIsDefault = "";
                strwesnetshipcode = "";
                strghostid = "";
                strsyscreate = "";
                strCustId = "";

                strPortalSource = Standard_Lookup_Query_reader.GetSqlValue(0).ToString();
                strCustName = Standard_Lookup_Query_reader.GetSqlValue(1).ToString();
                strCustId = Standard_Lookup_Query_reader.GetSqlValue(2).ToString();
                strShipToCode = Standard_Lookup_Query_reader.GetSqlValue(3).ToString();
                strServicingBranch = Standard_Lookup_Query_reader.GetSqlValue(4).ToString();
                strCustDpc = Standard_Lookup_Query_reader.GetSqlValue(5).ToString();
                strIsDefault = Standard_Lookup_Query_reader.GetSqlValue(6).ToString();
                strCustLocation = Standard_Lookup_Query_reader.GetSqlValue(7).ToString();
                strCustShipZip = Standard_Lookup_Query_reader.GetSqlValue(8).ToString();
                strwesnetshipcode = Standard_Lookup_Query_reader.GetSqlValue(9).ToString();
                strghostid = Standard_Lookup_Query_reader.GetSqlValue(10).ToString();
                strCustIDType = Standard_Lookup_Query_reader.GetSqlValue(11).ToString();
                strBlanketPO = Standard_Lookup_Query_reader.GetSqlValue(12).ToString();
                strBlanketPO2 = Standard_Lookup_Query_reader.GetSqlValue(13).ToString();
                strBuyerCode = Standard_Lookup_Query_reader.GetSqlValue(14).ToString();
                strsyscreate = Standard_Lookup_Query_reader.GetSqlValue(15).ToString();

                dgrdReturnValues.Visible = true;
                lblAlertGrid.Visible = true;
                lblAlertGrid.Text = "Please select row to update, delete or insert new records.";
            }
            else
            {
                lblAlertGrid.Visible = true;
                lblAlertGrid.Text = "No record(s) found. Please try again.";
                dgrdReturnValues.Visible = false;
                btnSearch.Visible = false;
            }

            con.Close();
            con.Dispose();
        }

        private void Method_Insert_New_Record()
        {
            try
            {
                Method_Check_Null_Textbox();
                Method_Check_IsDefaultValue();

                //Connect to database
                //con = DatabaseUtility.GetSqlConnection(conString);

                string sqlInsertStatement = "";

                sqlInsertStatement = "INSERT INTO xcblordertest.dbo.custidxref (PortalSource, CustName, CustID, ShipToCode,ServicingBranch, CustDpc,IsDefault,CustLocation, CustShipZip, wesnetshipcode, ghostid, CustIDType, BlanketPO, BlanketPO2, BuyerCode, syscreate, IsActive, sysUpdate)" +
                    "VALUES ('" + txtPortal.Text.Trim() + "'," +
                    "'" + txtCustName.Text.Trim() + "'," +
                    "'" + txtCustId.Text.Trim() + "'," +
                    "'" + txtShipToCode.Text.Trim() + "'," +
                    "'" + txtBranch.Text.Trim() + "'," +
                    "'" + txtCustDpc.Text.Trim() + "'," +
                    "'" + intIsDefaultValue + "'," +
                    "'" + txtCustLocation.Text.Trim() + "'," +
                    "'" + txtCustShipZip.Text.Trim() + "'," +
                    "'" + txtWesnetShipCode.Text.Trim() + "'," +
                    "'" + txtGhostId.Text.Trim() + "'," +
                    "'" + txtCustIdType.Text.Trim() + "'," +
                    "'" + txtBlanketPo.Text.Trim() + "'," +
                    "'" + txtBlanketPo2.Text.Trim() + "'," +
                    "'" + txtBuyerCode.Text.Trim() + "'," +
                    "'" + DateTime.Now + "'," + //syscreate
                    " 1 , " + //IsActive
                    "'" + DateTime.Now + "')"; //sysUpdate

                //create a SQL command to update record
                SqlCommand sqlInsertCommand = new SqlCommand(sqlInsertStatement, con);

                //Execute the command
                if (sqlInsertCommand.ExecuteNonQuery() > 0)
                {
                    //maybe display a message confirming update has been successful

                    //show datagrid with new updated row
                    Method_Reload_DataGrid();

                    PanelSearch.Visible = true;
                    dgrdReturnValues.Visible = true;
                    PanelModifyRecords.Visible = false;

                    txtPortal.Enabled = false;
                    txtCustName.Enabled = false;
                    txtBranch.Enabled = false;
                    txtCustDpc.Enabled = false;
                    txtCustIdType.Enabled = false;
                    txtBlanketPo.Enabled = false;
                    txtBlanketPo2.Enabled = false;
                    txtBuyerCode.Enabled = false;
                    txtWesnetShipCode.Enabled = false;
                    txtGhostId.Enabled = false;
                    txtCustShipZip.Enabled = false;
                    txtIsDefault.Enabled = false;
                    txtLastUpdated.Enabled = false;
                    txtCustLocation.Enabled = false;
                    txtShipToCode.Enabled = false;
                    txtCustId.Enabled = false;
                    txtCreatedDate.Enabled = false;

                    cbxPortal.Checked = false;
                    cbxCustName.Checked = false;
                    cbxBranch.Checked = false;
                    cbxCustDpc.Checked = false;
                    cbxCustIdType.Checked = false;
                    cbxBlanketPo.Checked = false;
                    cbxBlanketPo2.Checked = false;
                    cbxBuyerCode.Checked = false;
                    cbxWesnetShipCode.Checked = false;
                    cbxGhostId.Checked = false;
                    cbxCustShipZip.Checked = false;
                    cbxCustLocation.Checked = false;
                    cbxShipToCode.Checked = false;
                    cbxCustId.Checked = false;
                    cbxIsDefault.Checked = false;

                    btnContinue.Visible = false;
                    btnCancel.Visible = false;
                    lblAlert.Visible = false;
                }
                else
                {
                    //display message that new record was NOT added
                    btnContinue.Visible = false;
                    lblAlert.Visible = true;
                    lblAlert.Text = "New record was NOT added. An error has occurred.";
                }
            }
            catch (Exception ex)
            {
                //add code to send error to admin via email
                Session["Exception"] = ex.Message.ToString();
                Response.Redirect("Error.aspx");
            }
            finally
            {
                //Close connection
                con.Close();
                con.Dispose();
            }
        }

        private void Method_Update_Record()
        {
            try
            {
                strPortalSource = "";
                strCustId = "";
                strShipToCode = "";
                strServicingBranch = "";
                strCustDpc = "";
                strwesnetshipcode = "";

                //save the values of the textboxes before updating and use those values in the WHERE clause below.
                strPortalSource = Session["SessionPortalSource"].ToString();
                strCustId = Session["SessionCustId"].ToString();
                strShipToCode = Session["SessionShipToCode"].ToString();
                strServicingBranch = Session["SessionServicingBranch"].ToString();
                strCustDpc = Session["SessionCustDpc"].ToString();
                strwesnetshipcode = Session["Sessionwesnetshipcode"].ToString();

                //check if textboxes are null 
                if (strPortalSource == "&nbsp;") { strPortalSource = " ".Trim(); }
                if (strCustId == "&nbsp;") { strCustId = " ".Trim(); }
                if (strShipToCode == "&nbsp;") { strShipToCode = " ".Trim(); }
                if (strServicingBranch == "&nbsp;") { strServicingBranch = " ".Trim(); }
                if (strCustDpc == "&nbsp;") { strCustDpc = " ".Trim(); }
                if (strwesnetshipcode == "&nbsp;") { strwesnetshipcode = " ".Trim(); }

                Method_Check_IsDefaultValue();

                //Connect to database 
           //     //con = DatabaseUtility.GetSqlConnection(conString);

                string sqlUpdateStatement = "";

                sqlUpdateStatement = " UPDATE xcblordertest.dbo.custidxref " +
                    "SET " +
                    " PortalSource = '" + txtPortal.Text.Trim() + "' , " +
                    " CustName = '" + txtCustName.Text.Trim() + "' , " +
                    " CustID = '" + txtCustId.Text.Trim() + "' , " +
                    " ShipToCode = '" + txtShipToCode.Text.Trim() + "' , " +
                    " ServicingBranch = '" + txtBranch.Text.Trim() + "' , " +
                    " CustDpc = '" + txtCustDpc.Text.Trim() + "' , " +
                    " IsDefault = '" + intIsDefaultValue + "' , " +
                    " CustLocation = '" + txtCustLocation.Text.Trim() + "' , " +
                    " CustShipZip = '" + txtCustShipZip.Text.Trim() + "' , " +
                    " wesnetshipcode = '" + txtWesnetShipCode.Text.Trim() + "' , " +
                    " ghostid = '" + txtGhostId.Text.Trim() + "' , " +
                    " CustIDType = '" + txtCustIdType.Text.Trim() + "' , " +
                    " BlanketPO = '" + txtBlanketPo.Text.Trim() + "' , " +
                    " BlanketPO2 = '" + txtBlanketPo2.Text.Trim() + "' , " +
                    " BuyerCode = '" + txtBuyerCode.Text.Trim() + "' , " +
                    " sysUpdate = '" + DateTime.Now + "' " +

                    " WHERE PortalSource = '" + strPortalSource.ToString().Trim() + "' " +
                    " AND CustID = '" + strCustId.ToString().Trim() + "' " +
                    " AND ShipToCode = '" + strShipToCode.ToString().Trim() + "' " +
                    " AND ServicingBranch = '" + strServicingBranch.ToString().Trim() + "' " +
                    " AND CustDpc = '" + strCustDpc.ToString().Trim() + "' " +
                    " AND wesnetshipcode = '" + strwesnetshipcode.ToString().Trim() + "' " +
                    " AND IsActive = 1 ";

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);

                //Execute the command
                if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                {
                    //maybe display a message confirming update has been successful

                    //show datagrid with new updated row
                    Method_Reload_DataGrid();

                    PanelSearch.Visible = true;
                    dgrdReturnValues.Visible = true;
                    PanelModifyRecords.Visible = false;

                    txtPortal.Enabled = false;
                    txtCustName.Enabled = false;
                    txtBranch.Enabled = false;
                    txtCustDpc.Enabled = false;
                    txtCustIdType.Enabled = false;
                    txtBlanketPo.Enabled = false;
                    txtBlanketPo2.Enabled = false;
                    txtBuyerCode.Enabled = false;
                    txtWesnetShipCode.Enabled = false;
                    txtGhostId.Enabled = false;
                    txtCustShipZip.Enabled = false;
                    txtIsDefault.Enabled = false;
                    txtLastUpdated.Enabled = false;
                    txtCustLocation.Enabled = false;
                    txtShipToCode.Enabled = false;
                    txtCustId.Enabled = false;
                    txtCreatedDate.Enabled = false;

                    cbxPortal.Checked = false;
                    cbxCustName.Checked = false;
                    cbxBranch.Checked = false;
                    cbxCustDpc.Checked = false;
                    cbxCustIdType.Checked = false;
                    cbxBlanketPo.Checked = false;
                    cbxBlanketPo2.Checked = false;
                    cbxBuyerCode.Checked = false;
                    cbxWesnetShipCode.Checked = false;
                    cbxGhostId.Checked = false;
                    cbxCustShipZip.Checked = false;
                    cbxCustLocation.Checked = false;
                    cbxShipToCode.Checked = false;
                    cbxCustId.Checked = false;
                    cbxIsDefault.Checked = false;

                    btnContinue.Visible = false;
                    btnCancel.Visible = false;
                    lblAlert.Visible = false;
                    lblAlertIsDefault.Visible = false;
                }
                else
                {
                    //display message that record was NOT updated
                    btnContinue.Visible = false;
                    lblAlert.Visible = true;
                    lblAlert.Text = "No update. Error has occurred.";
                }
            }
            catch (Exception ex)
            {
                //add code to send error to admin via email
                Session["Exception"] = ex.Message.ToString();
                Response.Redirect("Error.aspx");
            }
            finally
            {
                //Close connection
                con.Close();
                con.Dispose();
            }
        }

        private void Method_Delete_Record()
        {
            //This procedure does not actually delete a record, but uses an UPDATE to set the IsActive = 0.
            try
            {
                strPortalSource = "";
                strCustId = "";
                strShipToCode = "";
                strServicingBranch = "";
                strCustDpc = "";
                strwesnetshipcode = "";

                //save the values of the textboxes before updating and use those values in the WHERE clause below.
                strPortalSource = Session["SessionPortalSource"].ToString();
                strCustId = Session["SessionCustId"].ToString();
                strShipToCode = Session["SessionShipToCode"].ToString();
                strServicingBranch = Session["SessionServicingBranch"].ToString();
                strCustDpc = Session["SessionCustDpc"].ToString();
                strwesnetshipcode = Session["Sessionwesnetshipcode"].ToString();

                //check to see if textboxes are null 
                if (strPortalSource == "&nbsp;") { strPortalSource = " ".Trim(); }
                if (strCustId == "&nbsp;") { strCustId = " ".Trim(); }
                if (strShipToCode == "&nbsp;") { strShipToCode = " ".Trim(); }
                if (strServicingBranch == "&nbsp;") { strServicingBranch = " ".Trim(); }
                if (strCustDpc == "&nbsp;") { strCustDpc = " ".Trim(); }
                if (strwesnetshipcode == "&nbsp;") { strwesnetshipcode = " ".Trim(); }

                Method_Check_IsDefaultValue();

                //Connect to database
                //con = DatabaseUtility.GetSqlConnection(conString);

                //create a SQL command to update record
                string sqlUpdateStatement = "";

                sqlUpdateStatement = " UPDATE xcblordertest.dbo.custidxref " +
                    "SET " +
                    " PortalSource = '" + txtPortal.Text.Trim() + "' , " +
                    " CustName = '" + txtCustName.Text.Trim() + "' , " +
                    " CustID = '" + txtCustId.Text.Trim() + "', " +
                    " ShipToCode = '" + strShipToCode.ToString().Trim() + "' , " +
                    " ServicingBranch = '" + strServicingBranch.ToString().Trim() + "' , " +
                    " CustDpc = '" + strCustDpc.ToString().Trim() + "' , " +
                    " IsDefault = " + intIsDefaultValue + " , " +
                    " CustLocation = '" + txtCustLocation.Text.Trim() + "' , " +
                    " CustShipZip = '" + txtCustShipZip.Text.Trim() + "' , " +
                    " wesnetshipcode = '" + txtWesnetShipCode.Text.Trim() + "' , " +
                    " ghostid = '" + txtGhostId.Text.Trim() + "',  " +
                    " CustIDType = '" + txtCustIdType.Text.Trim() + "' , " +
                    " BlanketPO = '" + txtBlanketPo.Text.Trim() + "' , " +
                    " BlanketPO2 = '" + txtBlanketPo2.Text.Trim() + "' , " +
                    " BuyerCode = '" + txtBuyerCode.Text.Trim() + "' , " +
                    " sysUpdate = '" + DateTime.Now + "', " +
                    " IsActive = 0 " +

                    " WHERE PortalSource = '" + strPortalSource.ToString().Trim() + "' " +
                    " AND CustID = '" + strCustId.ToString().Trim() + "' " +
                    " AND ShipToCode = '" + strShipToCode.ToString().Trim() + "' " +
                    " AND ServicingBranch = '" + strServicingBranch.ToString().Trim() + "' " +
                    " AND CustDpc = '" + strCustDpc.ToString().Trim() + "' " +
                    " AND wesnetshipcode = '" + strwesnetshipcode.ToString().Trim() + "' " +
                    " AND IsActive = 1 ";

                //create a SQL command to update record
                SqlCommand sqlUpdateCommand = new SqlCommand(sqlUpdateStatement, con);

                //Execute the command
                if (sqlUpdateCommand.ExecuteNonQuery() > 0)
                {
                    //maybe display a message confirming update has been successful

                    PanelSearch.Visible = true;
                    dgrdReturnValues.Visible = true;
                    PanelModifyRecords.Visible = false;

                    txtPortal.Enabled = false;
                    txtCustName.Enabled = false;
                    txtBranch.Enabled = false;
                    txtCustDpc.Enabled = false;
                    txtCustIdType.Enabled = false;
                    txtBlanketPo.Enabled = false;
                    txtBlanketPo2.Enabled = false;
                    txtBuyerCode.Enabled = false;
                    txtWesnetShipCode.Enabled = false;
                    txtGhostId.Enabled = false;
                    txtCustShipZip.Enabled = false;
                    txtIsDefault.Enabled = false;
                    txtLastUpdated.Enabled = false;
                    txtCustLocation.Enabled = false;
                    txtShipToCode.Enabled = false;
                    txtCustId.Enabled = false;
                    txtCreatedDate.Enabled = false;

                    cbxPortal.Checked = false;
                    cbxCustName.Checked = false;
                    cbxBranch.Checked = false;
                    cbxCustDpc.Checked = false;
                    cbxCustIdType.Checked = false;
                    cbxBlanketPo.Checked = false;
                    cbxBlanketPo2.Checked = false;
                    cbxBuyerCode.Checked = false;
                    cbxWesnetShipCode.Checked = false;
                    cbxGhostId.Checked = false;
                    cbxCustShipZip.Checked = false;
                    cbxCustLocation.Checked = false;
                    cbxShipToCode.Checked = false;
                    cbxCustId.Checked = false;
                    cbxIsDefault.Checked = false;

                    btnContinue.Visible = false;
                    btnCancel.Visible = false;
                    lblAlert.Visible = false;
                    dgrdReturnValues.Visible = false;
                    PanelSearch.Visible = true;
                    Method_Search_Dropdown();
                }
                else
                {
                    //display message that error occurred AND record was not deleted
                    btnContinue.Visible = false;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Record was NOT deleted. An error has occurred.";
                }
            }
            catch (Exception ex)
            {
                //add code to send error to admin via email
                Session["Exception"] = ex.Message.ToString();
                Response.Redirect("Error.aspx");
            }
            finally
            {
                //Close connection
                con.Close();
                con.Dispose();
            }
        }
        private void Method_Search_Dropdown()
        {
            try
            {
                if (ddlPortal.SelectedIndex == 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    lblAlert.Text = "Please select an option.";
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_ShipToCode();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_Branch();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustId();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_ShipToCode();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_Branch();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_CustId();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_ShipToCode_Branch();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_ShipToCode_CustId();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_ShipToCode_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_ShipToCode_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_Branch_CustId();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_Branch_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_Branch_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustId_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustId_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_ShipToCode_Branch();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_ShipToCode_CustId();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_ShipToCode_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_ShipToCode_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_Branch_CustId();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_Branch_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_Branch_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_CustId_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_CustId_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_ShipToCode_Branch_CustId();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_ShipToCode_Branch_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_ShipToCode_Branch_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_ShipToCode_CustId_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_ShipToCode_CustId_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_ShipToCode_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_Branch_CustId_CudtDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_Branch_CustId_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_Branch_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustId_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_ShipToCode_Branch_CustId();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_ShipToCode_Branch_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_ShipToCode_Branch_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_ShipToCode_CustId_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_ShipToCode_CustId_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_ShipToCode_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_Branch_CustId_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_Branch_CustId_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_CustId_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_Branch_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_ShipToCode_Branch_CustId_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_ShipToCode_Branch_CustId_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_ShipToCode_Branch_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_Branch_CustId_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex == 0)
                {
                    Search_Portal_CustName_ShipToCode_Branch_CustId_CustDpc();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex == 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_CustId_Branch_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex == 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_ShipToCode_Branch_CustId_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex == 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_ShipToCode_Branch_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex == 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_ShipToCode_CustId_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex == 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_ShipToCode_Branch_CustId_CustDpc_IsDefault();
                }
                else if (ddlPortal.SelectedIndex != 0 && ddlCustName.SelectedIndex != 0 && ddlBranch.SelectedIndex != 0 && ddlCustDpc.SelectedIndex != 0 && ddlShipToCode.SelectedIndex != 0 && ddlCustId.SelectedIndex != 0 && ddlIsDefault.SelectedIndex != 0)
                {
                    Search_Portal_CustName_ShipToCode_Branch_CustId_CustDpc_IsDefault();
                }
                else
                {
                    lblAlert.Visible = true;
                    lblAlert.Text = "Option does not exist. Please try again.";
                }
            }
            catch (Exception ex)
            {
                //add code to send error to admin via email
                Session["Exception"] = ex.Message.ToString();
                Response.Redirect("Error.aspx");
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }


        private void ddlPortal_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlPortal.SelectedValue == "(Exempt from search)")
            {
                PanelSearch.Visible = true;
                //Load only the Portal Source dropdown
                Load_Portal_DropDownList();

                ddlCustName.Visible = false;
                ddlCustId.Visible = false;
                ddlBranch.Visible = false;
                ddlCustDpc.Visible = false;
                ddlShipToCode.Visible = false;
                ddlIsDefault.Visible = false;

                lblCustomerName.Visible = false;
                lblServicingBranch.Visible = false;
                lblCustomerDPC.Visible = false;
                lblShipToCode.Visible = false;
                lblCustomerID.Visible = false;
                lblIsDefault.Visible = false;
                lblAlert.Visible = false;
                lblAlertGrid.Visible = false;

                ddlCustName.Items.Clear();
                ddlCustName.Items.Clear();
                ddlCustId.Items.Clear();
                ddlBranch.Items.Clear();
                ddlCustDpc.Items.Clear();
                ddlShipToCode.Items.Clear();
                ddlIsDefault.Items.Clear();

                ddlCustName.Items.Add("(Exempt from search)");
                ddlCustId.Items.Add("(Exempt from search)");
                ddlBranch.Items.Add("(Exempt from search)");
                ddlCustDpc.Items.Add("(Exempt from search)");
                ddlShipToCode.Items.Add("(Exempt from search)");
                ddlIsDefault.Items.Add("(Exempt from search)");

                dgrdReturnValues.Visible = false;

                btnNewSearch.Visible = false;
                btnSearch.Visible = false;
            }
            else
            {
                btnSearch.Visible = true;
                btnNewSearch.Visible = true;

                ddlCustName.Visible = true;
                ddlCustId.Visible = true;
                ddlBranch.Visible = true;
                ddlCustDpc.Visible = true;
                ddlShipToCode.Visible = true;
                ddlIsDefault.Visible = true;

                lblCustomerName.Visible = true;
                lblServicingBranch.Visible = true;
                lblCustomerDPC.Visible = true;
                lblShipToCode.Visible = true;
                lblCustomerID.Visible = true;
                lblIsDefault.Visible = true;
                lblAlertGrid.Visible = false;

                Load_CustName_DropDownList();
                Load_Branch_DropDownList();
                Load_CustDpc_DropDownList();
                Load_ShipToCode_DropDownList();
                Load_CustId_DropDownList();
                Load_IsDefault_DropDownList();

                ddlCustName.Enabled = true;
                ddlBranch.Enabled = true;
                ddlCustDpc.Enabled = true;
                ddlShipToCode.Enabled = true;
                ddlCustId.Enabled = true;
                ddlIsDefault.Enabled = true;
            }
        }

        private void ddlCustName_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlCustName.SelectedValue == "(Exempt from search)")
            {
                //do nothing and just refresh page
            }
            else
            {
                Method_Check_Dropdown();
                ddlCustName.Enabled = false;
            }
        }

        private void ddlBranch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlBranch.SelectedValue == "(Exempt from search)")
            {
                //do nothing and just refresh page
            }
            else
            {
                Method_Check_Dropdown();
                ddlBranch.Enabled = false;
            }
        }

        private void ddlCustId_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlCustId.SelectedValue == "(Exempt from search)")
            {
                //do nothing and just refresh page
            }
            else
            {
                Method_Check_Dropdown();
                ddlCustId.Enabled = false;
            }
        }

        private void ddlCustDpc_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlCustDpc.SelectedValue == "(Exempt from search)")
            {
                //do nothing and just refresh page
            }
            else
            {
                Method_Check_Dropdown();
                ddlCustDpc.Enabled = false;
            }
        }

        private void ddlShipToCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlShipToCode.SelectedValue == "(Exempt from search)")
            {
                //do nothing and just refresh page
            }
            else
            {
                Method_Check_Dropdown();
                ddlShipToCode.Enabled = false;
            }
        }

        private void ddlIsDefault_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlIsDefault.SelectedValue == "(Exempt from search)")
            {
                //do nothing and just refresh page
            }
            else
            {
                Method_Check_Dropdown();
                ddlIsDefault.Enabled = false;
            }
        }


        private void btnNewSearch_Click(object sender, System.EventArgs e)
        {
            PanelSearch.Visible = true;
            //Load only the Portal Source dropdown
            Load_Portal_DropDownList();

            ddlCustName.Visible = false;
            ddlCustId.Visible = false;
            ddlBranch.Visible = false;
            ddlCustDpc.Visible = false;
            ddlShipToCode.Visible = false;
            ddlIsDefault.Visible = false;

            lblCustomerName.Visible = false;
            lblServicingBranch.Visible = false;
            lblCustomerDPC.Visible = false;
            lblShipToCode.Visible = false;
            lblCustomerID.Visible = false;
            lblIsDefault.Visible = false;

            ddlCustName.Items.Clear();
            ddlCustName.Items.Clear();
            ddlCustId.Items.Clear();
            ddlBranch.Items.Clear();
            ddlCustDpc.Items.Clear();
            ddlShipToCode.Items.Clear();
            ddlIsDefault.Items.Clear();

            ddlCustName.Items.Add("(Exempt from search)");
            ddlCustId.Items.Add("(Exempt from search)");
            ddlBranch.Items.Add("(Exempt from search)");
            ddlCustDpc.Items.Add("(Exempt from search)");
            ddlShipToCode.Items.Add("(Exempt from search)");
            ddlIsDefault.Items.Add("(Exempt from search)");

            dgrdReturnValues.Visible = false;
            lblAlert.Visible = false;
            lblAlertGrid.Visible = false;

            btnNewSearch.Visible = false;
            btnSearch.Visible = false;
        }
        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            Method_Search_Dropdown();
            btnNewSearch.Visible = true;
        }

        private void btnContinue_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Session["Check_Button_Click"].ToString() == "btnUpdateRecord_Click")
                {
                    Method_Update_Record();
                }
                else if (Session["Check_Button_Click"].ToString() == "btnNewRecord_Click")
                {
                    Method_Insert_New_Record();
                }
                else if (Session["Check_Button_Click"].ToString() == "btnDeleteRecord_Click")
                {
                    Method_Delete_Record();
                }
                else
                {
                    lblAlert.Visible = true;
                    lblAlert.Text = "Cannot perform task. An error has occurred.";
                }
            }
            catch (Exception ex)
            {
                //add code to send error to admin via email
                Session["Exception"] = ex.Message.ToString();
                Response.Redirect("Error.aspx");
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            PanelSearch.Visible = true;
            PanelModifyRecords.Visible = false;
            dgrdReturnValues.Visible = true;
            PanelChoice.Visible = false;
            btnCancel.Visible = false;
            btnContinue.Visible = false;

            lblAlert.Visible = false;
            lblAlertGrid.Visible = true;
            lblAlertGrid.Text = "Please select row to update, delete or insert new records.";
            lblAlertIsDefault.Visible = false;
            lblAlertWesnetShipCode.Visible = false;

            lblReqPortalSource.Visible = false;
            lblReqCustomerName.Visible = false;
            lblReqCustomerID.Visible = false;
            lblReqShipToCode.Visible = false;
            lblReqServicingBranch.Visible = false;
            lblReqCustomerDPC.Visible = false;
            lblReqIsDefault.Visible = false;
            lblReqLocation.Visible = false;
            lblReqWesnetShipCode.Visible = false;

            txtPortal.Enabled = false;
            txtCustName.Enabled = false;
            txtBranch.Enabled = false;
            txtCustDpc.Enabled = false;
            txtCustIdType.Enabled = false;
            txtBlanketPo.Enabled = false;
            txtBlanketPo2.Enabled = false;
            txtBuyerCode.Enabled = false;
            txtWesnetShipCode.Enabled = false;
            txtGhostId.Enabled = false;
            txtCustShipZip.Enabled = false;
            txtIsDefault.Enabled = false;
            txtLastUpdated.Enabled = false;
            txtCustLocation.Enabled = false;
            txtShipToCode.Enabled = false;
            txtCustId.Enabled = false;
            txtCreatedDate.Enabled = false;

            cbxPortal.Checked = false;
            cbxCustName.Checked = false;
            cbxBranch.Checked = false;
            cbxCustDpc.Checked = false;
            cbxCustIdType.Checked = false;
            cbxBlanketPo.Checked = false;
            cbxBlanketPo2.Checked = false;
            cbxBuyerCode.Checked = false;
            cbxWesnetShipCode.Checked = false;
            cbxGhostId.Checked = false;
            cbxCustShipZip.Checked = false;
            cbxCustLocation.Checked = false;
            cbxShipToCode.Checked = false;
            cbxCustId.Checked = false;
            cbxIsDefault.Checked = false;
        }


        private void btnUpdateRecord_Click(object sender, System.EventArgs e)
        {
            //Make sure that only True or False for entered for txtIsDefault.Text
            if (txtIsDefault.Text.ToLower().Trim() == "false" || txtIsDefault.Text.ToLower().Trim() == "true")
            {
                //If txtWesnetShipCode.Text is empty, fill it with default value of 99.
                if (txtWesnetShipCode.Text.Trim() == "") { txtWesnetShipCode.Text = "99"; }

                Session["Check_Button_Click"] = "btnUpdateRecord_Click";
                PanelModifyRecords.Visible = false;
                lblAlert.Visible = true;
                lblAlertIsDefault.Visible = false;
                lblAlert.Text = "You are about to update a record.";
                btnContinue.Visible = true;

                lblReqPortalSource.Visible = false;
                lblReqCustomerName.Visible = false;
                lblReqCustomerID.Visible = false;
                lblReqShipToCode.Visible = false;
                lblReqServicingBranch.Visible = false;
                lblReqCustomerDPC.Visible = false;
                lblReqIsDefault.Visible = false;
                lblReqLocation.Visible = false;
                lblReqWesnetShipCode.Visible = false;
                lblRequired.Visible = false;
            }
            else
            {
                lblAlertIsDefault.Visible = true;
                lblAlertIsDefault.Text = "* Please enter only True or False for IsDefault";

                lblReqPortalSource.Visible = false;
                lblReqCustomerName.Visible = false;
                lblReqCustomerID.Visible = false;
                lblReqShipToCode.Visible = false;
                lblReqServicingBranch.Visible = false;
                lblReqCustomerDPC.Visible = false;
                lblReqIsDefault.Visible = false;
                lblReqLocation.Visible = false;
                lblReqWesnetShipCode.Visible = false;
                lblRequired.Visible = false;
            }
        }
        private void btnNewRecord_Click(object sender, System.EventArgs e)
        {
            //Make sure that only True or False for entered for txtIsDefault.Text
            if (txtIsDefault.Text.ToLower().Trim() == "false" || txtIsDefault.Text.ToLower().Trim() == "true")
            {
                //If txtWesnetShipCode.Text is empty, fill it with default value of 99.
                if (txtWesnetShipCode.Text.Trim() == "") { txtWesnetShipCode.Text = "99"; }

                Session["Check_Button_Click"] = "btnNewRecord_Click";
                PanelModifyRecords.Visible = false;
                lblAlertIsDefault.Visible = false;
                lblAlert.Visible = true;
                lblAlert.Text = "You are about to create a new record.";
                btnContinue.Visible = true;
                btnUpdateRecord.Visible = false;
                btnCancel.Visible = true;

                lblReqPortalSource.Visible = false;
                lblReqCustomerName.Visible = false;
                lblReqCustomerID.Visible = false;
                lblReqShipToCode.Visible = false;
                lblReqServicingBranch.Visible = false;
                lblReqCustomerDPC.Visible = false;
                lblReqIsDefault.Visible = false;
                lblReqLocation.Visible = false;
                lblReqWesnetShipCode.Visible = false;
                lblRequired.Visible = false;
            }
            else
            {
                lblAlertIsDefault.Visible = true;
                lblAlertIsDefault.Text = "* Please enter only True or False for IsDefault";

                lblReqPortalSource.Visible = false;
                lblReqCustomerName.Visible = false;
                lblReqCustomerID.Visible = false;
                lblReqShipToCode.Visible = false;
                lblReqServicingBranch.Visible = false;
                lblReqCustomerDPC.Visible = false;
                lblReqIsDefault.Visible = false;
                lblReqLocation.Visible = false;
                lblReqWesnetShipCode.Visible = false;
                lblRequired.Visible = false;
            }
        }

        private void btnDeleteRecord_Click(object sender, System.EventArgs e)
        {
            Session["Check_Button_Click"] = "btnDeleteRecord_Click";
            PanelModifyRecords.Visible = false;

            btnContinue.Visible = true;
            lblAlertIsDefault.Visible = false;
            lblAlert.Visible = true;
            lblAlert.Text = "You are about to delete this record.";
        }


        private void LinkBtnUpdate_Click(object sender, System.EventArgs e)
        {
            //Set txtWesnetShipCode.Text = 99 if it is empty
            if (txtWesnetShipCode.Text.Trim() == "") { txtWesnetShipCode.Text = "99"; }

            PanelModifyRecords.Visible = true;
            PanelChoice.Visible = false;
            btnUpdateRecord.Visible = true;
            btnDeleteRecord.Visible = false;
            btnNewRecord.Visible = false;

            lblRequired.Visible = true;

            cbxPortal.Visible = false;
            cbxCustName.Visible = true;
            cbxBranch.Visible = true;
            cbxShipToCode.Visible = true;
            cbxCustId.Visible = true;
            cbxCustDpc.Visible = true;
            cbxCustLocation.Visible = true;
            cbxCustShipZip.Visible = true;
            cbxCustIdType.Visible = true;
            cbxBlanketPo.Visible = true;
            cbxBlanketPo2.Visible = true;
            cbxWesnetShipCode.Visible = true;
            cbxBuyerCode.Visible = true;
            cbxGhostId.Visible = true;
            cbxIsDefault.Visible = true;

            txtPortal.Enabled = false;
            txtCustName.Enabled = false;
            txtBranch.Enabled = false;
            txtShipToCode.Enabled = false;
            txtCustId.Enabled = false;
            txtCustDpc.Enabled = false;
            txtCustLocation.Enabled = false;
            txtCustShipZip.Enabled = false;
            txtCustIdType.Enabled = false;
            txtBlanketPo.Enabled = false;
            txtBlanketPo2.Enabled = false;
            txtWesnetShipCode.Enabled = false;
            txtBuyerCode.Enabled = false;
            txtGhostId.Enabled = false;
            txtIsDefault.Enabled = false;
        }

        private void LinkBtnNew_Click(object sender, System.EventArgs e)
        {
            //Set txtWesnetShipCode.Text = 99 if it is empty
            if (txtWesnetShipCode.Text.Trim() == "") { txtWesnetShipCode.Text = "99"; }

            PanelModifyRecords.Visible = true;
            PanelChoice.Visible = false;
            btnNewRecord.Visible = true;
            btnDeleteRecord.Visible = false;
            btnUpdateRecord.Visible = false;

            cbxPortal.Visible = false;
            cbxCustName.Visible = true;
            cbxBranch.Visible = true;
            cbxShipToCode.Visible = true;
            cbxCustId.Visible = true;
            cbxCustDpc.Visible = true;
            cbxCustLocation.Visible = true;
            cbxCustShipZip.Visible = true;
            cbxCustIdType.Visible = true;
            cbxBlanketPo.Visible = true;
            cbxBlanketPo2.Visible = true;
            cbxWesnetShipCode.Visible = true;
            cbxBuyerCode.Visible = true;
            cbxGhostId.Visible = true;
            cbxIsDefault.Visible = true;

            txtPortal.Enabled = false;
            txtCustName.Enabled = false;
            txtBranch.Enabled = false;
            txtShipToCode.Enabled = false;
            txtCustId.Enabled = false;
            txtCustDpc.Enabled = false;
            txtCustLocation.Enabled = false;
            txtCustShipZip.Enabled = false;
            txtCustIdType.Enabled = false;
            txtBlanketPo.Enabled = false;
            txtBlanketPo2.Enabled = false;
            txtWesnetShipCode.Enabled = false;
            txtBuyerCode.Enabled = false;
            txtGhostId.Enabled = false;
            txtIsDefault.Enabled = false;
        }

        private void LinkBtnDelete_Click(object sender, System.EventArgs e)
        {
            //Set txtWesnetShipCode.Text = 99 if it is empty
            if (txtWesnetShipCode.Text.Trim() == "") { txtWesnetShipCode.Text = "99"; }

            PanelChoice.Visible = false;
            PanelModifyRecords.Visible = true;
            btnDeleteRecord.Visible = true;
            btnUpdateRecord.Visible = false;
            btnNewRecord.Visible = false;

            cbxPortal.Visible = false;
            cbxCustName.Visible = false;
            cbxBranch.Visible = false;
            cbxShipToCode.Visible = false;
            cbxCustId.Visible = false;
            cbxCustDpc.Visible = false;
            cbxCustLocation.Visible = false;
            cbxCustShipZip.Visible = false;
            cbxCustIdType.Visible = false;
            cbxBlanketPo.Visible = false;
            cbxBlanketPo2.Visible = false;
            cbxWesnetShipCode.Visible = false;
            cbxBuyerCode.Visible = false;
            cbxGhostId.Visible = false;
            cbxIsDefault.Visible = false;

            txtPortal.Enabled = false;
            txtCustName.Enabled = false;
            txtBranch.Enabled = false;
            txtShipToCode.Enabled = false;
            txtCustId.Enabled = false;
            txtCustDpc.Enabled = false;
            txtCustLocation.Enabled = false;
            txtCustShipZip.Enabled = false;
            txtCustIdType.Enabled = false;
            txtBlanketPo.Enabled = false;
            txtBlanketPo2.Enabled = false;
            txtWesnetShipCode.Enabled = false;
            txtBuyerCode.Enabled = false;
            txtGhostId.Enabled = false;
            txtIsDefault.Enabled = false;

            lblRequired.Visible = false;
            lblReqPortalSource.Visible = false;
            lblReqCustomerName.Visible = false;
            lblReqCustomerID.Visible = false;
            lblReqShipToCode.Visible = false;
            lblReqServicingBranch.Visible = false;
            lblReqCustomerDPC.Visible = false;
            lblReqIsDefault.Visible = false;
            lblReqLocation.Visible = false;
            lblReqWesnetShipCode.Visible = false;
        }


        private void dgrdReturnValues_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            strPortalSource = "";
            strCustName = "";
            strCustId = "";
            strShipToCode = "";
            strServicingBranch = "";
            strCustDpc = "";
            strIsDefault = "";
            strCustLocation = "";
            strCustShipZip = "";
            strwesnetshipcode = "";
            strghostid = "";
            strCustIDType = "";
            strBlanketPO = "";
            strBlanketPO2 = "";
            strBuyerCode = "";
            strsyscreate = "";

            //save the value of each cell for populating textbox for update, insert, and delete
            txtPortal.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[1].Text.Trim();
            txtCustName.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[2].Text.Trim();
            txtCustId.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[3].Text.Trim();
            txtShipToCode.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[4].Text.Trim();
            txtBranch.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[5].Text.Trim();
            txtCustDpc.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[6].Text.Trim();
            txtIsDefault.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[7].Text.Trim();
            txtCustLocation.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[8].Text.Trim();
            txtCustShipZip.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[9].Text.Trim();
            txtWesnetShipCode.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[10].Text.Trim();
            txtGhostId.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[11].Text.Trim();
            txtCustIdType.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[12].Text.Trim();
            txtBlanketPo.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[13].Text.Trim();
            txtBlanketPo2.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[14].Text.Trim();
            txtBuyerCode.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[15].Text.Trim();
            txtCreatedDate.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[16].Text.Trim();
            txtLastUpdated.Text = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[17].Text.Trim();

            Method_Check_Null_Textbox();

            //save the value of each cell for Method_Update_Record()
            Session["SessionPortalSource"] = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[1].Text.Trim();
            Session["SessionCustId"] = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[3].Text.Trim();
            Session["SessionShipToCode"] = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[4].Text.Trim();
            Session["SessionServicingBranch"] = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[5].Text.Trim();
            Session["SessionCustDpc"] = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[6].Text.Trim();
            Session["Sessionwesnetshipcode"] = dgrdReturnValues.Items[dgrdReturnValues.SelectedIndex].Cells[10].Text.Trim();

            btnCancel.Visible = true;
            PanelChoice.Visible = true;
            PanelModifyRecords.Visible = false;
            PanelSearch.Visible = false;
            dgrdReturnValues.Visible = false;

            lblAlertIsDefault.Visible = false;
            lblAlertGrid.Visible = false;
            lblReqPortalSource.Visible = true;
            lblReqCustomerName.Visible = true;
            lblReqCustomerID.Visible = true;
            lblReqShipToCode.Visible = true;
            lblReqServicingBranch.Visible = true;
            lblReqCustomerDPC.Visible = true;
            lblReqIsDefault.Visible = true;
            lblReqLocation.Visible = true;
            lblReqWesnetShipCode.Visible = true;
            lblRequired.Visible = true;
        }

        private void dgrdReturnValues_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgrdReturnValues.CurrentPageIndex = e.NewPageIndex;
            //dgrdReturnValues.DataBind();
            //Search_Portal();
        }

        protected void ddlCustId_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {

        }

        protected void btnNewSearch_Click1(object sender, EventArgs e)
        {

        }
    }
}

