<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewVolunteerAttendance.aspx.cs" Inherits="UIF.PerformingArts.ViewVolunteerAttendance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>


</head>
<body bgColor="#ffa500">
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
        style="z-index: 1; left: 213px; top: 97px; position: absolute; height: 37px; width: 950px" 
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

    <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlProgram_SelectedIndexChanged" 
        style="z-index: 1; left: 30px; top: 211px; position: absolute; width: 191px; right: 763px">
    </asp:DropDownList>
    <asp:Label ID="lblProgram" runat="server" 
        style="z-index: 1; left: 234px; top: 212px; position: absolute; width: 107px" 
        Text="Pick a Program."></asp:Label>
    <asp:DropDownList ID="ddlStudentName" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlStudentName_SelectedIndexChanged" 
        
        style="z-index: 1; left: 29px; top: 258px; position: absolute; width: 192px">
    </asp:DropDownList>
    <asp:Label ID="lblStudentName" runat="server" 
        style="z-index: 1; left: 234px; top: 258px; position: absolute; width: 124px" 
        Text="Pick a volunteer."></asp:Label>
    <asp:DropDownList ID="ddlClassName" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        
        style="z-index: 1; left: 29px; top: 301px; position: absolute; width: 192px" 
        onselectedindexchanged="ddlClassName_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label ID="lblClassName" runat="server" 
        style="z-index: 1; left: 233px; top: 303px; position: absolute; width: 138px" 
        Text="Pick a Class Name."></asp:Label>
    <asp:Button ID="cmbGetResults" runat="server" onclick="cmbGetResults_Click" 
        style="z-index: 1; left: 491px; top: 357px; position: absolute; height: 47px; width: 166px" 
        Text="Get Results" />
    <asp:Button ID="cmbExcelExport" runat="server" Enabled="False" 
        style="z-index: 1; left: 1087px; top: 357px; position: absolute; height: 48px; width: 136px" 
        Text="Export to Excel" onclick="cmbExcelExport_Click" />
    <asp:GridView ID="gvAttendanceResults" runat="server" 
        onselectedindexchanged="gvAttendanceResults_SelectedIndexChanged" 
        style="z-index: 1; left: 25px; position: absolute; height: 205px; width: 1250px; top: 439px" 
        BackColor="#FFD200" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="2px" AutoGenerateColumns="False">

        <Columns>
                <asp:BoundField  DataField="VolunteerLastName" HeaderText="VolunteerLastName" ReadOnly="true"  SortExpression="VolunteerLastName"  />
                <asp:BoundField  DataField="VolunteerFirstName" HeaderText="VolunteerFirstName" ReadOnly="true"  SortExpression="VolunteerFirstName"  />
                <asp:BoundField  DataField="Program" HeaderText="Program" ReadOnly="true"  SortExpression="Program"  />
                <asp:BoundField  DataField="Section" HeaderText="Section" ReadOnly="true"  SortExpression="Section"  />
                <asp:BoundField  DataField="Day" HeaderText="Day" ReadOnly="true"  SortExpression="Day"  />
                
                <asp:CheckBoxField DataField="Attended" HeaderText = "Attended"  ReadOnly="false"  Visible="true"  ControlStyle-BackColor="Gray" ControlStyle-Font-Bold="true" 
                ControlStyle-ForeColor="AliceBlue"   ItemStyle-Font-Bold="true" ItemStyle-ForeColor="AliceBlue"   />

                <asp:CheckBoxField DataField="Exempt" HeaderText = "Exempt"  ReadOnly="false"  Visible="true"  ControlStyle-BackColor="Gray" ControlStyle-Font-Bold="true" 
                ControlStyle-ForeColor="AliceBlue"   ItemStyle-Font-Bold="true" ItemStyle-ForeColor="AliceBlue"   />

                <asp:BoundField  DataField="Hours" HeaderText="Hours" ReadOnly="true"  SortExpression="Hours"  />
                <asp:BoundField  DataField="ProgramSeason" HeaderText="ProgramSeason" ReadOnly="true"  SortExpression="ProgramSeason"  />
                <asp:BoundField  DataField="Comment" HeaderText="Comment" ReadOnly="true"  SortExpression="Comment"  />
                <asp:BoundField  DataField="sysupdate" HeaderText="sysupdate" ReadOnly="true"  SortExpression="sysupdate"  />
                <asp:BoundField  DataField="lastupdatedby" HeaderText="lastupdatedby" ReadOnly="true"  SortExpression="lastupdatedby"  />
            </Columns>
    </asp:GridView>    
    
    <asp:Label ID="lblViewStudentAttendance" runat="server" Font-Size="25pt" 
        style="z-index: 1; left: 451px; top: 152px; position: absolute; height: 48px; width: 504px; text-decoration: underline" 
        Text="View Volunteer Attendance"></asp:Label>

    <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
        style="z-index: 1; left: 31px; top: 148px; position: absolute; height: 28px; width: 96px" 
        Text="Reset Filters" />
    <asp:Label ID="lblFilter" runat="server" 
        style="z-index: 1; left: 30px; top: 180px; position: absolute; width: 342px; height: 23px; font-weight: 700" 
        Text="Filter your Query with dropdowns"></asp:Label>

    <asp:Label ID="lblProgramName" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 31px; top: 233px; position: absolute; width: 122px" 
        Text="Program Name"></asp:Label>
    <asp:Label ID="lblStudentNameslabel" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 30px; top: 280px; position: absolute; width: 145px" 
        Text="Student Name (Optional)"></asp:Label>
    <asp:Label ID="lblClassNames" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 29px; top: 323px; position: absolute; width: 128px" 
        Text="Class Name (Optional)"></asp:Label>


    <asp:Label ID="lblMonth" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 30px; top: 372px; position: absolute; width: 61px" 
        Text="Month"></asp:Label>
    <asp:Label ID="lblDay" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 114px; top: 372px; position: absolute" Text="Day"></asp:Label>
    <asp:Label ID="lblYear" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 170px; top: 372px; position: absolute; height: 12px" 
        Text="Year"></asp:Label>
    <asp:DropDownList ID="ddlPickDateRangeMonth1" runat="server" 
        BackColor="#FFD200" 
        
        style="z-index: 1; left: 29px; top: 348px; position: absolute; width: 74px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay1" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 108px; top: 348px; position: absolute; width: 48px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear1" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 162px; top: 348px; position: absolute; width: 59px">
    </asp:DropDownList>
    <asp:Label ID="lblTo" runat="server" Font-Bold="True" 
        style="z-index: 1; left: 230px; top: 350px; position: absolute" Text="To: "></asp:Label>
    <asp:DropDownList ID="ddlPickDateRangeMonth2" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 260px; top: 348px; position: absolute; width: 76px; right: 646px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 344px; top: 348px; position: absolute; width: 47px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 395px; top: 348px; position: absolute; width: 69px">
    </asp:DropDownList>
    <asp:Label ID="lblStartDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 28px; top: 393px; position: absolute; width: 68px" 
        Text="(Start Date)"></asp:Label>
    <asp:Label ID="lblEndDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 336px; top: 393px; position: absolute; height: 17px; width: 62px" 
        Text="(End Date)"></asp:Label>
    <asp:Label ID="lblSetDateRange" runat="server" Font-Bold="True" 
        style="z-index: 1; left: 163px; top: 395px; position: absolute; width: 163px" 
        Text="(Specify a Date/Range)"></asp:Label>
    <asp:Label ID="lblAnotherMonth" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 264px; top: 372px; position: absolute" 
        Text="Month"></asp:Label>
    <asp:Label ID="lblAnotherDay" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 347px; top: 372px; position: absolute" Text="Day"></asp:Label>
    <asp:Label ID="lblAnotherYear" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 398px; top: 372px; position: absolute" 
        Text="Year"></asp:Label>
    <asp:Label ID="lblAnotherOptional" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 394px; top: 393px; position: absolute" 
        Text="(Optional)"></asp:Label>
    <asp:Label ID="lblAnotherOne" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 91px; top: 393px; position: absolute" 
        Text="(Optional)"></asp:Label>
    <asp:Label ID="lblAnotherOneYet" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 210px; top: 411px; position: absolute" 
        Text="(Optional)"></asp:Label>
    <asp:Label ID="lblOptionalAnother" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 109px; top: 232px; position: absolute" 
        Text="(Optional)"></asp:Label>


    <asp:Label ID="lblTooMuchData" runat="server" Font-Size="18pt" 
        style="z-index: 1; left: 387px; top: 461px; position: absolute; height: 154px; width: 443px" 
        Text="You must select at least one filter parameter as the following would result in too much data being displayed.  Please reset and try again!  Thankyou..." 
        Visible="False"></asp:Label>


    </form>
</body>
</html>
