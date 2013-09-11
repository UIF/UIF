<%@ Page language="c#" Codebehind="ManualRoutingMaintenance3.aspx.cs" AutoEventWireup="false" Inherits="EDISupport.ManualRoutingMaintenance" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ManualRoutingMaintenance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="App_Themes/Default/StyleSheet.css" type="text/css" rel="stylesheet">
	    <style type="text/css">
            .style1
            {
                position: absolute;
                top: 23px;
                left: 19px;
                z-index: 1;
                width: 227px;
                height: 137px;
            }
            .style2
            {
                text-decoration: underline;
            }
        </style>
	</HEAD>
	<body bgcolor="#ff9900">
		<form id="Form1" runat="server">
			<div align="center">
				<TABLE>
					<TR>
						<TD align="center"><asp:label id="lblManualRouting" runat="server" Font-Bold="True" 
                                CssClass="style2" Font-Size="X-Large">Student Attendance Reporting</asp:label></TD>
					</TR>
					<TR>
						<TD vAlign="top"><asp:panel id="PanelSearch" runat="server" BorderWidth="1" Visible="False" Height="24px">
								<P>&nbsp;</P>
								<TABLE>
									<TR width="100%">
										<TD style="HEIGHT: 12px">
											<asp:Label id="lblPortalSource" runat="server">Portal Source</asp:Label></TD>
										<TD style="HEIGHT: 12px">
											<asp:Label id="lblCustomerName" runat="server">Customer Name</asp:Label></TD>
										<TD style="HEIGHT: 12px">
											<asp:Label id="lblCustomerID" runat="server">Customer ID</asp:Label></TD>
										<TD style="HEIGHT: 12px">
											<asp:Label id="lblShipToCode" runat="server">Ship To Code</asp:Label></TD>
										<TD style="HEIGHT: 12px">
											<asp:Label id="lblServicingBranch" runat="server">Branch</asp:Label></TD>
										<TD style="HEIGHT: 12px">
											<asp:Label id="lblCustomerDPC" runat="server">DPC</asp:Label></TD>
										<TD style="HEIGHT: 12px">
											<asp:Label id="lblIsDefault" runat="server">Is Default</asp:Label></TD>
									</TR>
									<TR>
										<TD>
											<asp:DropDownList id="ddlPortal" runat="server" AutoPostBack="True">
												<asp:ListItem Value="Select">Select</asp:ListItem>
											</asp:DropDownList></TD>
										<TD>
											<asp:DropDownList id="ddlCustName" runat="server" AutoPostBack="True"></asp:DropDownList></TD>
										<TD>
											<asp:DropDownList id="ddlCustId" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlCustId_SelectedIndexChanged1"></asp:DropDownList></TD>
										<TD>
											<asp:DropDownList id="ddlShipToCode" runat="server" AutoPostBack="True"></asp:DropDownList></TD>
										<TD>
											<asp:DropDownList id="ddlBranch" runat="server" AutoPostBack="True"></asp:DropDownList></TD>
										<TD>
											<asp:DropDownList id="ddlCustDpc" runat="server" AutoPostBack="True"></asp:DropDownList></TD>
										<TD>
											<asp:DropDownList id="ddlIsDefault" runat="server" AutoPostBack="True"></asp:DropDownList></TD>
									</TR>
									<TR>
										<TD>
											<asp:Button id="btnSearch" runat="server" Visible="False" 
                                                CausesValidation="False" Text="Search" onclick="btnSearch_Click1"></asp:Button>
											<asp:Button id="btnNewSearch" runat="server" CausesValidation="False" 
                                                Text="New Search" onclick="btnNewSearch_Click1"></asp:Button></TD>
										<TD></TD>
										<TD></TD>
										<TD></TD>
										<TD></TD>
										<TD></TD>
										<TD></TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD id="links" align="center"><asp:panel id="PanelChoice" runat="server" BorderWidth="1" Visible="False">
								<P align="center">What would you like to do?</P>
								<TABLE>
									<TR>
										<TD>
											<asp:LinkButton id="LinkBtnUpdate" runat="server">Update Record</asp:LinkButton></TD>
									</TR>
									<TR>
										<TD>
											<asp:LinkButton id="LinkBtnDelete" runat="server">Delete Record</asp:LinkButton>
                                            <asp:Image ID="imgUIF" runat="server" CssClass="style1"  ImageUrl="~/Picture1.png"/>
                                        </TD>
									</TR>
									<TR>
										<TD>
											<asp:LinkButton id="LinkBtnNew" runat="server">Insert New Record</asp:LinkButton></TD>
									</TR>
								</TABLE>
								<P>&nbsp;</P>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="left">
							<P>&nbsp;<asp:validationsummary id="ValidationSummary1" runat="server" Font-Bold="True" HeaderText="Please correct the following:"></asp:validationsummary></P>
						</TD>
					</TR>
					<TR>
						<TD align="center"><asp:panel id="PanelModifyRecords" runat="server" BorderWidth="1" Visible="False" HorizontalAlign="Center">
								<P>&nbsp;</P>
								<asp:Label id="lblRequired" runat="server" Font-Bold="True" Visible="False" ForeColor="Red">*Required</asp:Label>
								<TABLE>
									<TR>
										<TD style="HEIGHT: 26px">
											<asp:Label id="lblReqPortalSource" runat="server" Font-Bold="True" ForeColor="Red">*</asp:Label></TD>
										<TD style="HEIGHT: 26px">Portal Source</TD>
										<TD style="WIDTH: 25px; HEIGHT: 26px">
											<asp:RequiredFieldValidator id="ReqFldValPortal" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtPortal"
												ErrorMessage="Portal Source Required">*</asp:RequiredFieldValidator></TD>
										<TD style="HEIGHT: 26px">
											<asp:TextBox id="txtPortal" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxPortal" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD style="HEIGHT: 26px">&nbsp;</TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 26px">
											<asp:Label id="lblReqCustomerName" runat="server" Font-Bold="True" ForeColor="Red">*</asp:Label></TD>
										<TD style="HEIGHT: 26px">Customer Name</TD>
										<TD style="WIDTH: 25px; HEIGHT: 26px">
											<asp:RequiredFieldValidator id="ReqFldValCustName" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtCustName"
												ErrorMessage="Customer Name Required">*</asp:RequiredFieldValidator></TD>
										<TD style="HEIGHT: 26px">
											<asp:TextBox id="txtCustName" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxCustName" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD style="HEIGHT: 26px"></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 26px">
											<asp:Label id="lblReqCustomerID" runat="server" Font-Bold="True" ForeColor="Red">*</asp:Label></TD>
										<TD style="HEIGHT: 26px">Customer ID</TD>
										<TD style="WIDTH: 25px; HEIGHT: 26px">
											<asp:RequiredFieldValidator id="ReqFldValCustId" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtCustId"
												ErrorMessage="Customer ID Required">*</asp:RequiredFieldValidator></TD>
										<TD style="HEIGHT: 26px">
											<asp:TextBox id="txtCustId" runat="server" MaxLength="100" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxCustId" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD style="HEIGHT: 26px"></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 26px">
											<asp:Label id="lblReqShipToCode" runat="server" Font-Bold="True" ForeColor="Red">*</asp:Label></TD>
										<TD style="HEIGHT: 26px">Ship To Code</TD>
										<TD style="WIDTH: 25px; HEIGHT: 26px">
											<asp:RequiredFieldValidator id="ReqFldValShipToCode" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtShipToCode"
												ErrorMessage="Ship To Code Required">*</asp:RequiredFieldValidator></TD>
										<TD style="HEIGHT: 26px">
											<asp:TextBox id="txtShipToCode" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxShipToCode" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD style="HEIGHT: 26px"></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 26px">
											<asp:Label id="lblReqServicingBranch" runat="server" Font-Bold="True" ForeColor="Red">*</asp:Label></TD>
										<TD style="HEIGHT: 26px">Branch</TD>
										<TD style="WIDTH: 25px; HEIGHT: 26px">
											<asp:RequiredFieldValidator id="ReqFldValBranch" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtBranch"
												ErrorMessage="Servicing Branch  Required">*</asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="RegExpValBranch" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtBranch"
												ErrorMessage="Please enter only 4 branch numbers." ValidationExpression="\d{4}">*</asp:RegularExpressionValidator></TD>
										<TD style="HEIGHT: 26px">
											<asp:TextBox id="txtBranch" runat="server" MaxLength="4" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxBranch" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD style="HEIGHT: 26px"></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 26px">
											<asp:Label id="lblReqCustomerDPC" runat="server" Font-Bold="True" ForeColor="Red">*</asp:Label></TD>
										<TD style="HEIGHT: 26px">DPC</TD>
										<TD style="WIDTH: 25px; HEIGHT: 26px">
											<asp:RequiredFieldValidator id="ReqFldValCustDpc" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtCustDpc"
												ErrorMessage="DPC Required">*</asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="RegExpValDpc" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtCustDpc"
												ErrorMessage="Please enter only 5 DPC numbers." ValidationExpression="\d{5}">*</asp:RegularExpressionValidator></TD>
										<TD style="HEIGHT: 26px">
											<asp:TextBox id="txtCustDpc" runat="server" MaxLength="5" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxCustDpc" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD style="HEIGHT: 26px"></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 26px">
											<asp:Label id="lblReqIsDefault" runat="server" Font-Bold="True" ForeColor="Red">*</asp:Label></TD>
										<TD style="HEIGHT: 26px">Is Default
											<asp:label id="lblAlertIsDefault" runat="server" Font-Bold="True" Visible="False" ForeColor="Red"></asp:label></TD>
										<TD style="WIDTH: 25px; HEIGHT: 26px">
											<asp:RequiredFieldValidator id="ReqFldValIsDefault" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtIsDefault"
												ErrorMessage="IsDefault Required">*</asp:RequiredFieldValidator></TD>
										<TD style="HEIGHT: 26px">
											<asp:TextBox id="txtIsDefault" runat="server" MaxLength="5" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxIsDefault" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD style="HEIGHT: 26px"></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblReqLocation" runat="server" Font-Bold="True" Visible="False" ForeColor="Red">*</asp:Label></TD>
										<TD>Location</TD>
										<TD style="WIDTH: 25px">
											<asp:RequiredFieldValidator id="ReqFldValLocation" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtCustLocation"
												ErrorMessage="Location Required">*</asp:RequiredFieldValidator></TD>
										<TD>
											<asp:TextBox id="txtCustLocation" runat="server" MaxLength="100" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxCustLocation" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD>
											<asp:Label id="lblReqWesnetShipCode" runat="server" Font-Bold="True" Visible="False" ForeColor="Red">*</asp:Label></TD>
										<TD>Wesnet Ship Code
											<asp:label id="lblAlertWesnetShipCode" runat="server" Font-Bold="True" Visible="False" ForeColor="Red"></asp:label></TD>
										<TD style="WIDTH: 25px">
											<asp:RegularExpressionValidator id="ReqFldValWesnetShipCode" runat="server" Font-Bold="True" Display="Dynamic" ControlToValidate="txtWesnetShipCode"
												ErrorMessage="Please enter only 2 numbers for Wesnet Ship Code." ValidationExpression="\d{2}">*</asp:RegularExpressionValidator></TD>
										<TD>
											<asp:TextBox id="txtWesnetShipCode" runat="server" MaxLength="2" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxWesnetShipCode" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>&nbsp;Zip</TD>
										<TD style="WIDTH: 25px"></TD>
										<TD>
											<asp:TextBox id="txtCustShipZip" runat="server" MaxLength="5" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxCustShipZip" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>Ghost ID</TD>
										<TD style="WIDTH: 25px"></TD>
										<TD>
											<asp:TextBox id="txtGhostId" runat="server" MaxLength="40" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxGhostId" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>Customer ID Type</TD>
										<TD style="WIDTH: 25px"></TD>
										<TD>
											<asp:TextBox id="txtCustIdType" runat="server" MaxLength="20" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxCustIdType" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>Blanket PO</TD>
										<TD style="WIDTH: 25px"></TD>
										<TD>
											<asp:TextBox id="txtBlanketPo" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxBlanketPo" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>Blanket PO 2</TD>
										<TD style="WIDTH: 25px"></TD>
										<TD>
											<asp:TextBox id="txtBlanketPo2" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxBlanketPo2" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>Buyer Code</TD>
										<TD style="WIDTH: 25px"></TD>
										<TD>
											<asp:TextBox id="txtBuyerCode" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
											<asp:CheckBox id="cbxBuyerCode" runat="server" AutoPostBack="True" Text="Edit"></asp:CheckBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>Date Created</TD>
										<TD></TD>
										<TD>
											<asp:TextBox id="txtCreatedDate" runat="server" MaxLength="50" Enabled="False"></asp:TextBox></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD>Last Updated</TD>
										<TD></TD>
										<TD>
											<asp:TextBox id="txtLastUpdated" runat="server" MaxLength="50" Enabled="False"></asp:TextBox></TD>
										<TD></TD>
									</TR>
								</TABLE>
								<P>
									<asp:Button id="btnUpdateRecord" runat="server" Visible="False" Text="Update Record"></asp:Button>
									<asp:Button id="btnNewRecord" runat="server" Visible="False" Text="Insert New Record"></asp:Button>
									<asp:Button id="btnDeleteRecord" runat="server" Visible="False" CausesValidation="False" Text="Delete Record"></asp:Button></P>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD align="center">
							<P>&nbsp;</P>
							<P><asp:label id="lblAlert" runat="server" Font-Bold="True" Visible="False" ForeColor="Red"></asp:label><asp:label id="lblAlertGrid" runat="server" Font-Bold="True" Visible="False" ForeColor="Red"></asp:label></P>
							<P><asp:button id="btnContinue" runat="server" Visible="False" Text="Continue"></asp:button></P>
							<P><asp:button id="btnCancel" runat="server" Visible="False" CausesValidation="False" Text="Cancel"></asp:button></P>
						</TD>
					</TR>
					<TR>
						<TD id="gridlinks"><asp:datagrid id="dgrdReturnValues" runat="server" Visible="False" Width="100%" BorderColor="Black"
								BorderStyle="None" BackColor="White" AllowPaging="True" PageSize="25">
								<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="Gray"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="#000066"></AlternatingItemStyle>
								<ItemStyle ForeColor="#000066"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="MidnightBlue"></HeaderStyle>
								<Columns>
									<asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
								</Columns>
								<PagerStyle NextPageText="Next" PrevPageText="Back" HorizontalAlign="Left" ForeColor="#000066"
									Position="TopAndBottom" BackColor="White"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</div>
		</form>
	</body>
</HTML>

