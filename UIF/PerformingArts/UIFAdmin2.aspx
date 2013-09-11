<%@ Page language="c#" Codebehind="UIFAdmin2.aspx.cs" AutoEventWireup="True" Inherits="UIF.PerformingArts.UIFAdmin2" debug="True"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
		<title>UIFAdmin2</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

<style type="text/css"> 
   div { z-index: 9999; } 
</style>


</HEAD>
	<body bgColor="orange" MS_POSITIONING="GridLayout">
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
        style="z-index: 1; left: 213px; top: 90px; position: absolute; height: 37px; width: 947px" 
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

			<asp:label style="Z-INDEX: 101; POSITION: absolute; TOP: 160px; LEFT: 478px; width: 298px; font-weight: 700; text-decoration: underline;" 
                id="Label1" runat="server" Height="32px" ForeColor="Black" 
                Font-Size="28pt">Student Directory</asp:label>
            <asp:button style="Z-INDEX: 104; POSITION: absolute; TOP: 309px; LEFT: 304px; width: 187px;" id="btnSearch"
				runat="server" Text="Retrieve Student Information" onclick="btnSearch_Click"></asp:button>
            <asp:dropdownlist style="Z-INDEX: 102; POSITION: absolute; TOP: 310px; LEFT: 86px; right: 916px;" 
                id="ddlNames" runat="server"
				Width="200px" Height="32px" onselectedindexchanged="ddlNames_SelectedIndexChanged" 
                AutoPostBack="True" CausesValidation="True" BackColor="#FFD200"></asp:dropdownlist>
            <p>
                &nbsp;</p>
            <asp:Button ID="cmbNewStudent" runat="server" onclick="cmbNewStudent_Click" 
                style="z-index: 1; left: 784px; top: 328px; position: absolute; height: 35px; width: 149px" 
                Text="New Student" />
            <asp:Label ID="lblNewStudent" runat="server" 
                style="z-index: 1; left: 736px; top: 296px; position: absolute; width: 259px" 
                Text="Student not found?  Enter a new student.."></asp:Label>
            <asp:Label ID="lblStudent" runat="server" 
                style="z-index: 1; left: 88px; top: 283px; position: absolute; width: 464px" 
                Text="Search for a student by:   LastName,FirstName (MiddleName)"></asp:Label>
            <asp:DropDownList ID="ddlNames2" runat="server" AutoPostBack="True" 
                CausesValidation="True" onselectedindexchanged="ddlNames2_SelectedIndexChanged" 
                
                style="z-index: 1; left: 85px; top: 400px; position: absolute; width: 197px" 
                BackColor="#FFD200">
            </asp:DropDownList>
            <asp:Button ID="cmbSearch2" runat="server" onclick="cmbSearch2_Click" 
                style="z-index: 1; left: 303px; top: 398px; position: absolute; width: 187px" 
                Text="Retrieve Student Information" />
            <asp:Label ID="lblStudent2" runat="server" 
                style="z-index: 1; left: 86px; top: 377px; position: absolute; width: 470px" 
                Text="Search for a student by:  FirstName LastName (MiddleName)"></asp:Label>
        </form>
	</body>
</HTML>
