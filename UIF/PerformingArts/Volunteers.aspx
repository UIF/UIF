<%@ Page language="c#" Codebehind="Volunteers.aspx.cs" AutoEventWireup="True" Inherits="UIF.PerformingArts.Volunteers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
		<title>Volunteers</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1"></meta>
		<meta name="CODE_LANGUAGE" Content="C#"></meta>
		<meta name="vs_defaultClientScript" content="JavaScript"></meta>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"></meta>
	    <style type="text/css">
            #Form1
            {
                height: 266px;
                margin-top: 309px;
            }
            </style>

    <style type="text/css"> 
       div { z-index: 9999; } 
    </style>

	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="orange">
		<form id="Form1" method="post" runat="server">

    <asp:Panel ID="pnlBackground" runat="server" BackColor="White"  
        style="z-index: 1; left: -12px; top: 1px; position: absolute; height: 133px; width: 1298px" 
        ViewStateMode="Enabled" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px">
        <asp:Label ID="lblUrbanImpact" runat="server" Font-Bold="True" Font-Size="36pt" 
            style="z-index: 1; left: 354px; top: 24px; position: absolute; height: 62px; width: 547px" 
            Text="Urban Impact Foundation"></asp:Label>
    </asp:Panel>

    <asp:Menu ID="MenuBest" runat="server" BackColor="White" BorderColor="Black"  ForeColor="Black"
        BorderWidth="0px" Orientation="Horizontal" 
        style="z-index: 1; left: 213px; top: 94px; position: absolute; height: 37px; width: 947px" 
        MaximumDynamicDisplayLevels="8" StaticEnableDefaultPopOutImage="False" 
        Height="15px" Font-Bold="True" onmenuitemclick="MenuBest_MenuItemClick">

        <DynamicHoverStyle BackColor="White" Font-Bold="False" 
            Font-Strikeout="False" Height="20px" Font-Italic="False" 
            Font-Size="15pt" />
        <DynamicMenuItemStyle ForeColor="Black" ItemSpacing="4px" 
            VerticalPadding="4px" BackColor="#FFD200" />

        <DynamicMenuStyle BackColor="#FFD200" />
        <DynamicSelectedStyle BackColor="#FFD200" VerticalPadding="4px" Width="40px" />

         <DynamicItemTemplate>
             <%# Eval("Text") %>
        </DynamicItemTemplate>

        <StaticHoverStyle BackColor="#FFD200" Font-Bold="True" Font-Italic="True" 
            Font-Size="15pt" Height="20px" />
        <StaticMenuItemStyle BackColor="White" />
        <StaticMenuStyle BackColor="White" />
        <StaticSelectedStyle BackColor="#FFD200" BorderColor="#FFD200" />
    </asp:Menu>          

    <asp:ImageButton ID="imgButton" runat="server"  ImageUrl="~/PerformingArts/Picture1.png"
        style="z-index: 1; left: 25px; top: 13px; position: absolute; height: 114px; width: 172px" 
        onclick="imgButton_Click" />

		    <asp:DropDownList ID="ddlVolunteerList" runat="server" 
                onselectedindexchanged="ddlVolunteerList_SelectedIndexChanged" 
                
                style="z-index: 1; left: 89px; top: 275px; position: absolute; width: 192px" 
                AutoPostBack="True" CausesValidation="True" BackColor="#FFD200">
            </asp:DropDownList>
		    <asp:Label ID="Label2" runat="server" 
                style="z-index: 1; left: 87px; top: 250px; position: absolute; width: 326px" 
                Text="Search for a volunteer by (LastName, FirstName)"></asp:Label>
            <asp:Button ID="cmbRetrieveVolunteerInformation2" runat="server" 
                onclick="cmbRetrieveVolunteerInformation2_Click" 
                style="z-index: 1; left: 302px; top: 377px; position: absolute; width: 161px" 
                Text="Retrieve Volunteer" />
            <asp:DropDownList ID="ddlVolunteerList2" runat="server" AutoPostBack="True" 
                CausesValidation="True" 
                onselectedindexchanged="ddlVolunteerList2_SelectedIndexChanged" 
                
                style="z-index: 1; left: 86px; top: 378px; position: absolute; width: 195px" 
                BackColor="#FFD200">
            </asp:DropDownList>
		    <asp:Label ID="Label1" runat="server" Font-Size="28pt" 
            style="z-index: 1; left: 469px; top: 167px; position: absolute; height: 63px; width: 367px; text-decoration: underline" 
            Text="Volunteer Directory" Font-Bold="True"></asp:Label>

            <asp:Button ID="cmbNewVolunteer" runat="server" onclick="cmbNewStudent_Click" 
                style="z-index: 1; left: 710px; top: 327px; position: absolute; height: 35px; width: 149px" 
                Text="New Volunteer" />
            <asp:Label ID="lblNewVolunteer" runat="server" 
                style="z-index: 1; left: 653px; top: 302px; position: absolute; width: 448px" 
                Text="Volunteer not found?  Enter a new volunteer.."></asp:Label>


		    <asp:Label ID="Label3" runat="server" 
                style="z-index: 1; left: 84px; top: 352px; position: absolute; width: 337px" 
                Text="Search for a volunteer by (FirstName, LastName)"></asp:Label>
		<asp:Button ID="cmbRetrieveVolunteerInformation" runat="server" 
            onclick="cmbRetrieveVolunteerInformation_Click" 
            style="z-index: 1; top: 274px; position: absolute; width: 160px; left: 304px" 
            Text="Retrieve Volunteer" />
		</form>
	</body>
</HTML>




