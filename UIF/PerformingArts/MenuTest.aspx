<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuTest.aspx.cs" Inherits="UIF.PerformingArts.MenuTest" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <asp:GridView ID="gvBillboardInformation" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
        style="z-index: 1; left: 53px; top: 226px; position: absolute; height: 114px; width: 329px">
    </asp:GridView>
    <asp:Label ID="lblMainBillboard" runat="server" Font-Italic="True" 
        Font-Size="20pt" 
        style="z-index: 1; left: 337px; top: 155px; position: absolute; height: 70px; width: 633px" 
        Text="Welcome to the Urban Impact Administrative Site."></asp:Label>
    <asp:Label ID="lblBirthdays" runat="server" 
        style="z-index: 1; left: 78px; top: 195px; position: absolute; height: 30px; width: 281px" 
        Text="Birthdays this month!"></asp:Label>

    <asp:Label ID="lblNewFeatures" runat="server" Font-Size="18pt" 
        style="z-index: 1; left: 562px; top: 254px; position: absolute; height: 34px; width: 344px; font-weight: 700; text-decoration: underline" 
        Text="Check out New Features!"></asp:Label>
    <asp:Label ID="lblNewFeatureInformation" runat="server" 
        style="z-index: 1; left: 559px; top: 297px; position: absolute; height: 20px; width: 399px" 
        Text="1)  RSVP Gospel Tracking System (found under systems)"></asp:Label>

    </form>
    </body>
</html>
