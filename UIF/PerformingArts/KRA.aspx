<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KRA.aspx.cs" Inherits="UIF.PerformingArts.KRA" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            position: absolute;
            top: 145px;
            left: 458px;
            z-index: 999;
            width: 521px;
            text-decoration: underline;
        }
    </style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="#ffa500">
    <form id="form1" runat="server">
    <div>
    
    </div>


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
        style="z-index: 1; left: 213px; top: 97px; position: absolute; height: 37px; width: 947px" 
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



    <asp:Label ID="lblKRAReporting" runat="server" CssClass="style1" 
        Font-Bold="False" Font-Size="25pt" 
        Text="Key Result Area (KRA)"></asp:Label>
    <asp:Button ID="cmbGospelMealsReport" runat="server" 
        onclick="cmbGospelMealsReport_Click" 
        style="z-index: 1; left: 987px; top: 491px; position: absolute; height: 45px; width: 260px; right: -93px;" 
        Text="Student Gospel/Meal Count" Visible="False" />
    <asp:GridView ID="gvGeneralReports" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" 
        onrowdatabound="gvGeneralReports_RowDataBound"
        
        
        style="z-index: 1; left: 34px; top: 348px; position: absolute; height: 133px; width: 313px">
        <AlternatingRowStyle Wrap="False" BackColor="Silver" />
        <HeaderStyle BackColor="Black" ForeColor="White" Wrap="True" />
        <RowStyle Wrap="False" />
    </asp:GridView>
    <asp:Button ID="cmbVolunteerMealCount" runat="server" 
        onclick="cmbVolunteerMealCount_Click" 
        style="z-index: 1; left: 961px; top: 580px; position: absolute; height: 45px; width: 260px" 
        Text="Volunteer Meal Count" Visible="False" />
    <asp:Button ID="cmbStudentCount" runat="server" onclick="cmbStudentCount_Click" 
        style="z-index: 1; left: 940px; top: 349px; position: absolute; height: 45px; width: 233px" 
        Text="Student Count per Program" Visible="False" />
    <asp:Button ID="cmbDistinctStudents" runat="server" 
        onclick="cmbDistinctStudents_Click" 
        style="z-index: 1; left: 1013px; top: 445px; position: absolute; height: 44px; width: 234px" 
        Text="Distinct Student Total" Visible="False" />
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" 
        style="z-index: 1; left: 404px; top: 391px; position: absolute; height: 67px; width: 125px" 
        Width="125px">
    </asp:DetailsView>
    <asp:Chart ID="chtGeneral" runat="server" BackColor="255, 221, 32" 
        BorderlineColor="Black" BorderlineDashStyle="Solid" BorderlineWidth="2" 
        onload="chtGeneral_Load" 
        style="z-index: 1; left: 440px; top: 653px; position: absolute; right: 32px" 
        Visible="False">
        <series>
            <asp:Series Name="Series1">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </chartareas>
        <BorderSkin BackColor="Orange" />
    </asp:Chart>
    <asp:Button ID="cmbStudentProgramSection" runat="server" 
        onclick="cmbStudentProgramSection_Click" 
        style="z-index: 1; left: 936px; top: 322px; position: absolute; height: 45px; width: 234px" 
        Text="Student Count per Program/Section" Visible="False" />
    <asp:Button ID="cmbChart" runat="server" onclick="cmbChart_Click" 
        style="z-index: 1; left: 615px; top: 310px; position: absolute; height: 45px; width: 214px" 
        Text="Chart" Visible="False" />
    <asp:Button ID="cmbVolunteerBreakdown" runat="server" 
        onclick="cmbVolunteerBreakdown_Click" 
        style="z-index: 1; left: 964px; top: 534px; position: absolute; height: 45px; width: 260px" 
        Text="Volunteer Count ProgramSection" Visible="False" />
    <asp:Button ID="cmbVolunteerBasics" runat="server" 
        onclick="cmbVolunteerBasics_Click" 
        style="z-index: 1; left: 941px; top: 392px; position: absolute; height: 45px; width: 260px" 
        Text="Volunteer Count per Program" Visible="False" />
    <asp:Button ID="cmbStudentGospelMealProgram" runat="server" 
        onclick="cmbStudentGospelMealProgram_Click" 
        style="z-index: 1; left: 1015px; top: 624px; position: absolute; height: 45px; width: 260px" 
        Text="Student Gospel/Meal per Program" Visible="False" />
    <asp:Button ID="cmbStudentGospelProgramSection" runat="server" 
        onclick="cmbStudentGospelProgramSection_Click" 
        style="z-index: 1; left: 977px; top: 667px; position: absolute; height: 45px; width: 260px" 
        Text="Student Gosepl/Meal per Program/Section" Visible="False" />
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 974px; top: 197px; position: absolute; height: 46px; width: 157px" 
        Text="Export to Excel" />
    <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlReports_SelectedIndexChanged" 
        
        
        style="z-index: 1; left: 249px; top: 250px; position: absolute; width: 216px" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlProgramSeason" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlProgramSeason_SelectedIndexChanged" 
        style="z-index: 1; left: 321px; top: 219px; position: absolute; width: 216px">
    </asp:DropDownList>
    <asp:Label ID="lblReportLabel" runat="server" Font-Size="19pt" 
        Font-Underline="True" 
        style="z-index: 1; left: 38px; top: 315px; position: absolute; width: 607px; height: 30px" 
        Visible="False"></asp:Label>
    <asp:Label ID="lblProgramSeasonYear" runat="server" Font-Size="14pt" 
        style="z-index: 1; left: 37px; top: 218px; position: absolute; height: 23px; width: 346px; right: 589px" 
        Text="Please Select a Year/ProgramSeason:"></asp:Label>
    <asp:Label ID="lblReport" runat="server" Font-Size="14pt" 
        style="z-index: 1; left: 37px; top: 251px; position: absolute; height: 25px; width: 240px" 
        Text="Please Select a Report:"></asp:Label>
    </form>
</body>
</html>
