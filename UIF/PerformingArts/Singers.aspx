<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Singers.aspx.cs" Inherits="UIF.PerformingArts.Singers" %>

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

    <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/PerformingArts/Picture1.png"
        style="z-index: 1; left: 25px; top: 13px; position: absolute; height: 114px; width: 172px" 
        onclick="imgButton_Click" />

    <asp:Label ID="lblChildrensChoir" runat="server" Font-Bold="False" 
        Font-Size="25pt" 
        style="z-index: 1; left: 422px; top: 151px; position: absolute; height: 49px; width: 453px; text-decoration: underline" 
        Text="Singers Maintenance"></asp:Label>

    <asp:GridView ID="gvVolunteerAttendance" runat="server" BackColor="#FFD200" 
        onrowdatabound="gvVolunteerAttendance_RowDataBound"
        style="z-index: 1; left: 60px; top: 317px; position: absolute; height: 159px; width: 306px" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
    </asp:GridView>

    <asp:GridView ID="gvGeneralUse" runat="server" BackColor="#FFD200" 
        onrowdatabound="gvGeneralUse_RowDataBound"
        style="z-index: 1; left: 59px; top: 316px; position: absolute; height: 193px; width: 483px" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
        <AlternatingRowStyle BackColor="Silver" BorderStyle="Solid" BorderWidth="4px" 
            Wrap="True" />
        <EditRowStyle Wrap="False" Font-Size="16pt" />
        <HeaderStyle BackColor="Black" ForeColor="White" Font-Size="Large"
            HorizontalAlign="Center" VerticalAlign="Middle" />
        <RowStyle Font-Size="11pt" HorizontalAlign="Left" Wrap="False" />
    </asp:GridView>

    <asp:ImageButton ID="imgButton" runat="server"  ImageUrl="~/PerformingArts/Picture1.png"
        style="z-index: 1; left: 25px; top: 13px; position: absolute; height: 114px; width: 172px" 
        onclick="imgButton_Click" />

    <asp:Button ID="cmbWeeklyAttendanceSheets" runat="server" 
        onclick="cmbWeeklyAttendanceSheets_Click" 
        style="z-index: 1; left: 64px; top: 259px; position: absolute; height: 45px; width: 193px" 
        Text="Student Attendance Sheets" />
    <asp:Label ID="lblStudentAttendance" runat="server" Font-Bold="True" 
        Font-Size="20pt" 
        style="z-index: 1; left: 438px; top: 218px; position: absolute; height: 36px; width: 296px" 
        Text="Student Attendance List" Visible="False"></asp:Label>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 272px; top: 279px; position: absolute; width: 140px;" 
        Text="Export to Excel" />
    <asp:Button ID="cmbVolunteerAttendance" runat="server" 
        onclick="cmbVolunteerAttendance_Click" 
        style="z-index: 1; left: 65px; top: 196px; position: absolute; height: 44px; width: 192px" 
        Text="Volunteer Attendance Sheets" />
    <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
        style="z-index: 1; left: 992px; top: 158px; position: absolute; height: 40px; width: 131px" 
        Text="Reset" />
    <asp:Button ID="cmbEmergencyContact" runat="server" 
        onclick="cmbEmergencyContact_Click" 
        style="z-index: 1; left: 946px; top: 210px; position: absolute; height: 42px; width: 178px" 
        Text="Emergency Information" />
    <asp:Button ID="cmbHealthConditions" runat="server" 
        onclick="cmbHealthConditions_Click" 
        style="z-index: 1; left: 946px; top: 260px; position: absolute; height: 38px; width: 179px" 
        Text="Health Conditions" />
    </form>


</body>
</html>

