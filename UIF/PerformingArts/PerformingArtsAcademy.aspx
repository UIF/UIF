<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformingArtsAcademy.aspx.cs" Inherits="UIF.PerformingArts.PerformingArtsAcademy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<style type="text/css"> 
   div { z-index: 9999; } 
    </style>

</head>
<body bgcolor="Orange">
    <form id="form1" runat="server">
    <div>
    
        <br />
    
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
    
    
    
    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Size="22pt" 
        style="z-index: 1; left: 374px; top: 158px; position: absolute; height: 48px; width: 578px; text-decoration: underline" 
        Text="Welcome to the Performing Arts Academy" Font-Italic="True"></asp:Label>
    <asp:GridView ID="gvGeneralUse" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="4px" 
        onrowdatabound="gvGeneralUse_RowDataBound"
        style="z-index: 1; left: 37px; top: 408px; position: absolute; height: 151px; width: 504px; right: 408px;" 
        onselectedindexchanged="gvGeneralUse_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="Silver" />
        <EditRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
    </asp:GridView>
    <asp:Button ID="cmbStudentList" runat="server" onclick="cmbStudentList_Click" 
        style="z-index: 1; left: 35px; top: 144px; position: absolute; height: 36px; width: 207px; right: 672px;" 
        Text="View Student Class Enrollment" />
    <asp:Button ID="cmbWeeklyAttendance" runat="server" 
        onclick="cmbWeeklyAttendance_Click" 
        style="z-index: 1; left: 1033px; top: 484px; position: absolute; height: 36px; width: 203px; bottom: 187px;" 
        Text="Weekly Attendance" Visible="False" />
    <asp:Button ID="cmbClearStudentEnrollment" runat="server" Enabled="False" 
        ForeColor="#FF3300" onclick="cmbClearStudentEnrollment_Click" 
        style="z-index: 1; left: 976px; top: 409px; position: absolute; height: 56px; width: 271px" 
        Text="Clear ALL Student Enrollment   CAUTION!!!!" Visible="False" />
    <asp:Button ID="cmbInventory" runat="server" onclick="cmbInventory_Click" 
        style="z-index: 1; left: 738px; top: 472px; position: absolute; height: 39px; width: 160px" 
        Text="Academy Inventory" Visible="False" />













    <asp:GridView ID="grvInventory" runat="server" AllowSorting="True" 
        AutoGenerateEditButton="True" AutoGenerateSelectButton="True" 
        onselectedindexchanged="grvInventory_SelectedIndexChanged"  OnSorting="grvInventory_Sorting"
        
        style="z-index: 1; left: 35px; top: 489px; position: absolute; height: 133px; width: 187px">
    </asp:GridView>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1087px; top: 186px; position: absolute; height: 36px; width: 161px" 
        Text="Export to Excel" />
    <asp:Button ID="cmbAdministerAcademyClasses" runat="server" 
        onclick="cmbAdministerAcademyClasses_Click" 
        style="z-index: 1; left: 1027px; top: 705px; position: absolute; height: 39px; width: 206px" 
        Text="Maintain Academy Classes List" Visible="False" />
    <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
        style="z-index: 1; left: 1090px; top: 146px; position: absolute; height: 36px; width: 157px" 
        Text="Reset" />
    <asp:Button ID="cmbVolunteerAttendance" runat="server" 
        onclick="cmbVolunteerAttendance_Click" 
        style="z-index: 1; left: 34px; top: 181px; position: absolute; height: 36px; width: 208px" 
        Text="View Program Volunteers" />
    <asp:Button ID="cmbRetrieveClassLists" runat="server" 
        onclick="cmbRetrieveClassLists_Click" 
        style="z-index: 1; left: 795px; top: 707px; position: absolute; height: 36px; width: 206px" 
        Text="Retrieve Class Lists" Visible="False" />
    <asp:DropDownList ID="ddlClass1" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlClass1_SelectedIndexChanged" 
        style="z-index: 1; left: 269px; top: 259px; position: absolute; width: 180px; right: 465px;" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlClass2" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlClass2_SelectedIndexChanged" 
        style="z-index: 1; left: 466px; top: 258px; position: absolute; width: 186px" 
        Visible="False">
    </asp:DropDownList>

    <asp:GridView ID="gvClassList" runat="server" BackColor="#FFD200"
        onrowdatabound="gvClassList_RowDataBound"
        onselectedindexchanged="gvClassList_SelectedIndexChanged" 
        style="z-index: 1; left: 36px; position: absolute; height: 205px; width: 767px; top: 404px" 
        BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="2px" AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
    </asp:GridView>    

    <asp:Label ID="lblClassLists" runat="server" Font-Size="18pt" 
        style="z-index: 1; left: 295px; top: 367px; position: absolute; height: 27px; width: 406px; text-decoration: underline" 
        Text="Student Class Lists" Visible="False"></asp:Label>


    <asp:Button ID="cmbAcademyAvailability" runat="server" 
        onclick="cmbAcademyAvailability_Click" 
        style="z-index: 1; left: 1067px; top: 265px; position: absolute; height: 40px; width: 181px" 
        Text="View Academy Availability" />


    <asp:GridView ID="gvAcademyAvailability" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
        style="z-index: 1; left: 830px; top: 404px; position: absolute; height: 133px; width: 417px">
        <HeaderStyle BackColor="Silver" />
    </asp:GridView>
    <asp:Label ID="lblClassAvailability" runat="server" Font-Size="20pt" 
        style="z-index: 1; left: 947px; top: 367px; position: absolute; height: 28px; width: 287px; text-decoration: underline" 
        Text="Class Availability" Visible="False"></asp:Label>

    <asp:GridView ID="gvAttendanceReport" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
        style="z-index: 1; left: 476px; top: 405px; position: absolute; height: 133px; width: 274px" 
        Visible="False">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
    </asp:GridView>
    <asp:Button ID="cmbAttendanceReport" runat="server" 
        onclick="cmbAttendanceReport_Click" 
        style="z-index: 1; left: 1068px; top: 227px; position: absolute; height: 33px; width: 180px" 
        Text="Attendance Report" />


    <asp:Button ID="cmbEmergencyContact" runat="server" 
        onclick="cmbEmergencyContact_Click" 
        style="z-index: 1; left: 840px; top: 657px; position: absolute; height: 36px; width: 184px" 
        Text="Emergency Information UP" Visible="False" />
    <asp:Button ID="cmbHealthConditions" runat="server" 
        onclick="cmbHealthConditions_Click" 
        style="z-index: 1; left: 841px; top: 618px; position: absolute; height: 38px; width: 184px" 
        Text="Health Conditions UP" Visible="False" />


    <asp:Button ID="cmbEmergencyContact2" runat="server" 
        onclick="cmbEmergencyContact2_Click" 
        style="z-index: 1; left: 1045px; top: 601px; position: absolute; width: 185px; height: 34px" 
        Text="Emergency Information 4AC" Visible="False" />
    <asp:Button ID="cmbHealthConditions2" runat="server" 
        onclick="cmbHealthConditions2_Click" 
        style="z-index: 1; left: 1044px; top: 650px; position: absolute; height: 38px; width: 185px" 
        Text="Health Conditions 4AC" Visible="False" />


    <asp:DropDownList ID="ddlFunctions" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlFunctions_SelectedIndexChanged" 
        style="z-index: 1; left: 39px; top: 222px; position: absolute; width: 196px">
    </asp:DropDownList>
    <asp:GridView ID="gvEmergency" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
        onrowdatabound="gvEmergency_RowDataBound"
        style="z-index: 1; left: 36px; top: 404px; position: absolute; height: 133px; width: 187px" 
        Visible="False">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
    </asp:GridView>


    <asp:Button ID="cmbAdminPageLink" runat="server" 
        onclick="cmbAdminPageLink_Click" 
        style="z-index: 1; left: 314px; top: 213px; position: absolute; height: 32px; width: 187px" 
        Text="Maintain Program Sections" Visible="False" />


    <asp:Button ID="cmbAutomateRosters" runat="server" 
        onclick="cmbAutomateRosters_Click" 
        style="z-index: 1; left: 712px; top: 200px; position: absolute; height: 47px; width: 176px" 
        Text="Automate Rosters" />
    
    <asp:GridView ID="gvRosterDump" runat="server" 
        onrowdatabound="gvRosterDump_RowDataBound"
        onselectedindexchanged="gvRosterDump_SelectedIndexChanged" 
        style="z-index: 1; left: 36px; position: absolute; height: 205px; width: 767px; top: 404px" 
        BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="2px" AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
    </asp:GridView>

    </form>
</body>
</html>
