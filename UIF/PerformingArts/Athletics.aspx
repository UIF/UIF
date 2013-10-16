<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Athletics.aspx.cs" Inherits="UIF.PerformingArts.Athletics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #Checkbox1
        {
            z-index: 999;
            left: 731px;
            top: 210px;
            position: absolute;
            width: 81px;
        }
    </style>
    <script language="javascript"  type="text/C#">

        function Checkbox1_onclick() {

        }

    </script>


    <style type="text/css"> 
       div { z-index: 9999; } 
    </style>

</head>
<body bgcolor="Orange">
    <form id="form1" runat="server">
    <div>
    
    </div>
    <p>
        </p>

    
    <asp:Panel ID="pnlBackground" runat="server" BackColor="White"  
        style="z-index: 1; left: -12px; top: 1px; position: absolute; height: 133px; width: 1598px" 
        ViewStateMode="Enabled" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px">
        <asp:Label ID="lblUrbanImpact" runat="server" Font-Bold="True" Font-Size="36pt" 
            style="z-index: 1; left: 354px; top: 24px; position: absolute; height: 62px; width: 547px" 
            Text="Urban Impact Foundation"></asp:Label>
    </asp:Panel>

    <asp:Menu ID="MenuBest" runat="server" BackColor="White" BorderColor="Black"  ForeColor="Black"
        BorderWidth="0px" Orientation="Horizontal" 
        style="z-index: 1; left: 213px; top: 97px; position: absolute; height: 37px; width: 950px" 
        MaximumDynamicDisplayLevels="8" StaticEnableDefaultPopOutImage="False" 
        Height="15px" Font-Bold="True" onmenuitemclick="MenuBest_MenuItemClick">

        <DynamicHoverStyle BackColor="White" Font-Bold="False" 
            Font-Strikeout="False" Height="20px" Font-Italic="False" 
            Font-Size="15pt" />
        <DynamicMenuItemStyle ForeColor="Black" ItemSpacing="4px" 
            VerticalPadding="4px" BackColor="#FFD200" />

        <DynamicMenuStyle BackColor="#FFD200" />
        <DynamicSelectedStyle BackColor="#FFD200" VerticalPadding="4px" Width="40px" 
            BorderColor="Black" BorderStyle="Solid" />

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

    <asp:panel ID="pnl1" runat="server" 
        style="z-index: 1; left: 53px; top: 226px; position: absolute; height: 114px; width: 329px">
            <h2>Athletics</h2>
    <ul>
        <li><a href="AthleticsProgramMaintenance.aspx?Security=Good&lastname=<%: Request.QueryString["lastname"] %>&firstname=<%: Request.QueryString["firstname"] %>&Dept=<%: Request.QueryString["Dept"] %>">Student Program Maintenance</a></li>
        <li><a href="AthleticsVolunteerMaintenance.aspx?Security=Good&lastname=<%: Request.QueryString["lastname"] %>&firstname=<%: Request.QueryString["firstname"] %>&Dept=<%: Request.QueryString["Dept"] %>">Volunteer Program Maintenance</a></li>
         <li><a href="AthleticsAttendanceHistory.aspx?Security=Good&lastname=<%: Request.QueryString["lastname"] %>firstname=<%: Request.QueryString["firstname"] %>&Dept=" + <%: Request.QueryString["Dept"] %>">Student Attendance History</a></li>
       <li><a href="VolunteerAttendanceHistory.aspx?Security=Good&lastname=<%: Request.QueryString["lastname"] %>&firstname=<%: Request.QueryString["firstname"] %>&Dept=" + <%: Request.QueryString["Dept"] %>">Volunteer Attendance History</a></li>
       <li><a href="AthleticsProgramSectionMaintenance.aspx?Security=Good&lastname=<%: Request.QueryString["lastname"] %>&firstname=<%: Request.QueryString["firstname"] %>&Dept=" + <%: Request.QueryString["Dept"] %>">Athletics Program Maintenance</a></li>
    </ul>
    </asp:panel>

    </form>
    </body>
</html>
