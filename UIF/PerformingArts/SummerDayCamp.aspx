<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummerDayCamp.aspx.cs" Inherits="UIF.PerformingArts.SummerDayCamp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="Orange">
    <form id="form2" runat="server">
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

    <asp:Label ID="lblMSHSChoir" runat="server" EnableViewState="False" 
        Font-Bold="False" Font-Size="23pt" 
        style="z-index: 1; left: 381px; top: 147px; position: absolute; height: 42px; width: 528px; text-decoration: underline" 
        Text="SummerDay Camp Maintenance"></asp:Label>
    <asp:Button ID="cmbAttendanceSheets" runat="server" 
        onclick="cmbAttendanceSheets_Click" 
        style="z-index: 1; left: 62px; top: 233px; position: absolute; height: 42px; width: 204px" 
        Text="Student Attendance Sheets" />
    <asp:GridView ID="gvGeneralDisplay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 59px; top: 300px; position: absolute; height: 193px; width: 483px" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
        onrowdatabound="gvGeneralDisplay_RowDataBound"
        onselectedindexchanged="gvGeneralDisplay_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="Silver" BorderStyle="Solid" BorderWidth="4px" 
            Wrap="True" />
        <EditRowStyle Wrap="False" Font-Size="16pt" />
        <HeaderStyle BackColor="Black" ForeColor="White" Height="15px" Font-Size="18pt" 
            HorizontalAlign="Center" VerticalAlign="Middle" />
        <RowStyle Font-Size="11pt" HorizontalAlign="Left" Wrap="False" />
    </asp:GridView>

    <asp:Label ID="lblStudentAttendance" runat="server" Font-Bold="True" 
        Font-Size="20pt" 
        style="z-index: 1; left: 452px; top: 215px; position: absolute; height: 33px; width: 544px" 
        Text="Student Attendance List" Visible="False"></asp:Label>
    <asp:Button ID="cmbResetPage" runat="server" onclick="cmbResetPage_Click" 
        style="z-index: 1; left: 1069px; top: 145px; position: absolute; height: 27px; width: 96px" 
        Text="Reset Page" />
    <asp:GridView ID="gvViewAttendance" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
        onrowdatabound="gvViewAttendance_RowDataBound"
        style="z-index: 1; left: 59px; top: 299px; position: absolute; height: 133px; width: 187px; right: 310px;">
        <AlternatingRowStyle BackColor="Silver" BorderColor="Black" BorderStyle="Solid" 
            BorderWidth="4px" />
        <HeaderStyle BackColor="Black" ForeColor="White" Font-Size="18pt" />
    </asp:GridView>
    <asp:Label ID="lblAttendance" runat="server" 
        style="z-index: 1; left: 59px; top: 279px; position: absolute; width: 292px; height: 43px" 
        Text="(Attendance:    Present=1, Absent=0, Exempt=E)" Visible="False"></asp:Label>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1020px; top: 178px; position: absolute; width: 144px;" 
        Text="Export to Excel" />
    <asp:Button ID="cmbProgramVolunteers" runat="server" 
        onclick="cmbProgramVolunteers_Click" 
        style="z-index: 1; left: 63px; top: 171px; position: absolute; height: 46px; width: 201px" 
        Text="Volunteer Attendance Sheets" />
    <asp:Button ID="cmbEmergencyContact" runat="server" 
        onclick="cmbEmergencyContact_Click" 
        style="z-index: 1; left: 965px; top: 311px; position: absolute; height: 36px; width: 196px" 
        Text="Emergency Information" Visible="False" />
    <asp:Button ID="cmbHealthConditions" runat="server" 
        onclick="cmbHealthConditions_Click" 
        style="z-index: 1; left: 948px; top: 354px; position: absolute; height: 34px; width: 215px" 
        Text="Health Conditions" Visible="False" />
    <asp:Button ID="cmbPickUpDropOff" runat="server" 
        onclick="cmbPickUpDropOff_Click" 
        style="z-index: 1; left: 964px; top: 210px; position: absolute; height: 44px; width: 199px" 
        Text="PickUp/DropOff Report" />
    </form>
</body>
</html>
