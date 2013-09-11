<%@ Page language="c#" Codebehind="PerformingArts.aspx.cs" AutoEventWireup="True" Inherits="UIF.PerformingArts.PerformingArts" enableViewStateMac="True"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PerformingArts</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"></meta>
		<meta name="CODE_LANGUAGE" content="C#"></meta>
		<meta name="vs_defaultClientScript" content="JavaScript"></meta>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"></meta>
	    <style type="text/css">
            .style9
            {
                text-decoration: underline;
                font-weight: bold;
                z-index: 101;
                position: absolute;
                top: 136px;
                left: 408px;
                width: 418px;
            }
            </style>

    <style type="text/css"> 
       div { z-index: 9999; } 
    </style>


	</HEAD>
	<body MS_POSITIONING="GridLayout"  
		bgProperties="fixed" bgColor="#ffa500">
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
        style="z-index: 1; left: 213px; top: 93px; position: absolute; height: 37px; width: 947px" 
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










			<asp:Label id="lblPerformingArts"
				runat="server" Font-Size="X-Large" CssClass="style9">Performing Arts Department</asp:Label>
			&nbsp;</form>
	</body>
</HTML>
