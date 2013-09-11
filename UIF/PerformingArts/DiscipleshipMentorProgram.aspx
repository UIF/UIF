<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiscipleshipMentorProgram.aspx.cs" Inherits="UIF.PerformingArts.DiscipleshipMentorProgram" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<style type="text/css"> 
   div { z-index: 1;
        left: 209px;
        top: 261px;
        position: absolute;
        height: 175px;
        width: 805px;
    } 
</style>


<style type="text/css"> 
   div { z-index: 99999999; } 
</style>


</head>
<body bgcolor="Orange" style="width: 1168px; margin-left: 4px">
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
        style="z-index: 999; left: 213px; top: 97px; position: absolute; height: 37px; width: 947px" 
        MaximumDynamicDisplayLevels="8" StaticEnableDefaultPopOutImage="False" 
        Height="15px" Font-Bold="True" onmenuitemclick="MenuBest_MenuItemClick">

        <DynamicHoverStyle BackColor="White" Font-Bold="False" 
            Font-Strikeout="False" Height="20px" Font-Italic="False" 
            Font-Size="15pt" />
        <DynamicMenuItemStyle ForeColor="Black" ItemSpacing="4px" 
            VerticalPadding="4px" BackColor="#FFD200" />

        <DynamicMenuStyle BackColor="#FFD200" Height="150px" Width="200px" />
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

    <asp:Label ID="lblEnrolledStudents" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 57px; top: 250px; position: absolute; width: 168px" 
        Text="Enrolled Students"></asp:Label>
    <asp:TextBox ID="txbCovenantSentDate" runat="server" BackColor="#FFBB3E" 
        ReadOnly="True" style="z-index: 1; left: 573px; top: 450px; position: absolute; width: 112px;" 
        Visible="False"></asp:TextBox>
    <asp:Label ID="lblDiscipleshipmentor" runat="server" Font-Size="25pt" 
        style="z-index: 1; left: 425px; top: 151px; position: absolute; height: 42px; width: 394px; text-decoration: underline" 
        Text="DiscipleshipMentor Program" Font-Bold="False"></asp:Label>
    <asp:DropDownList ID="ddlDiscipleshipMentor" runat="server" 
        style="z-index: 1; left: 50px; top: 227px; position: absolute; width: 213px; bottom: 282px;" 
        AutoPostBack="True" BackColor="#FFD200" 
        onselectedindexchanged="ddlDiscipleshipMentor_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Button ID="cmbDiscipleshipMentor" runat="server" 
        onclick="cmbDiscipleshipMentor_Click" 
        style="z-index: 1; left: 251px; top: 157px; position: absolute; width: 149px" 
        Text="Retrieve Student Info" Visible="False" />
    <p>
        &nbsp;</p>
    <asp:DropDownList ID="ddlVolunteerActiveMentees" runat="server" 
        BackColor="#FFD200" 
        
        style="z-index: 1; left: 761px; top: 292px; position: absolute; width: 204px" 
        AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlVolunteerActiveMentees_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:TextBox ID="txbNotes" runat="server" BackColor="Silver" 
        BorderStyle="Solid" 
        style="z-index: 1; left: 541px; top: 265px; position: absolute; height: 284px; width: 575px" 
        TextMode="MultiLine" ontextchanged="txbNotes_TextChanged" Visible="False"></asp:TextBox>
    <asp:Label ID="lblNotes" runat="server" 
        style="z-index: 1; left: 40px; top: 555px; position: absolute; width: 101px; right: 826px;" 
        Text="Activity History" Font-Size="10pt" Visible="False"></asp:Label>
    <asp:Button ID="cmbUpdate" runat="server" onclick="cmbUpdate_Click" 
        style="z-index: 1; left: 322px; top: 497px; position: absolute; width:197px; height: 48px;"  
        Text="Update Student Information" Enabled="False" Visible="False" />
    <asp:Image ID="imgImage" runat="server" 
        style="z-index: 1; left: 49px; top: 366px; position: absolute; height: 147px; width: 210px" 
        Visible="False" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="1px" />
    <asp:Label ID="lblDiscipleMentor" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 341px; top: 294px; position: absolute; " 
        Text="DiscipleshipMentor" Visible="False"></asp:Label>
    <asp:TextBox ID="txbStaffCoordinator" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 523px; top: 144px; position: absolute; width: 209px" 
        Visible="False"></asp:TextBox>
    <asp:Label ID="lblStaffCoordinator" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 525px; top: 153px; position: absolute; width: 120px" 
        Text="Staff Coordinator" Visible="False"></asp:Label>
    <asp:CheckBox ID="chbHasGraduated" runat="server" 
        oncheckedchanged="chbHasGraduated_CheckedChanged" 
        style="z-index: 1; left: 261px; top: 285px; position: absolute; width: 147px" 
        Text="Has Graduated?" Visible="False" />
    <asp:TextBox ID="txbProgramEnrollment" runat="server" BackColor="#FFD200" 
        ontextchanged="txbProgramEnrollment_TextChanged" 
        style="z-index: 1; left: 339px; top: 441px; position: absolute; width: 192px; height: 20px;" 
        Visible="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="lblProgramEnrollment" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 342px; top: 467px; position: absolute; width: 224px;" 
        Text="Mentor Program Affiliation" Visible="False"></asp:Label>
    <asp:TextBox ID="txbComments" runat="server" BackColor="#FFD200" 
        ontextchanged="txbComments_TextChanged" 
        style="z-index: 1; left: 49px; top: 311px; position: absolute; height: 33px; width: 208px" 
        TextMode="MultiLine" Visible="False"></asp:TextBox>
    <asp:Label ID="lblLastUpdatedBy" runat="server" Enabled="False" 
        style="z-index: 1; left: 1024px; position: absolute; height: 82px; width: 181px; top: 18px" 
        Text="Last Updated By:"></asp:Label>
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
    </asp:Panel>
    <p>
        <asp:Label ID="lblMentorMaintenance" runat="server" Font-Size="17pt" 
            style="z-index: 1; left: 765px; top: 262px; position: absolute; height: 31px; width: 200px; text-decoration: underline" 
            Text="Mentor Maintenance"></asp:Label>
        <asp:GridView ID="gvViewAll" runat="server" BackColor="#FFCB5E" 
            style="z-index: 1; left: -158px; top: 180px; position: absolute; height: 133px; width: 1126px" 
            onselectedindexchanged="gvViewAll_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="#999999" />
            <HeaderStyle BackColor="Black" ForeColor="White" />
        </asp:GridView>
        <asp:LinkButton ID="lbVolunteerInformation" runat="server" Font-Size="8pt" 
            Font-Underline="False" onclick="lbVolunteerInformation_Click" 
            style="z-index: 1; left: 453px; top: 294px; position: absolute" 
            Visible="False">(Mentor Profile)</asp:LinkButton>
        <asp:CheckBox ID="chbWaitingListInactive" runat="server" AutoPostBack="True" 
            CausesValidation="True" 
            style="z-index: 1; left: 174px; top: 291px; position: absolute; width: 221px;" 
            Visible="False" oncheckedchanged="chbWaitingListInactive_CheckedChanged" />
    </p>
    <asp:Label ID="lblComments" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 57px; top: 350px; position: absolute; height: 18px; width: 88px" 
        Text="Comments" Visible="False"></asp:Label>
    <asp:GridView ID="gvStudentHistory" runat="server" BackColor="#FFCB5E" 
        BorderColor="Black" BorderWidth="3px" 
        onselectedindexchanged="gvStudentHistory_SelectedIndexChanged" 
        
        style="z-index: 1; left: -188px; top: 314px; position: absolute; height: 147px; width: 990px">
    </asp:GridView>
    <asp:LinkButton ID="lbAddNewEntry" runat="server" Font-Size="10pt" 
        ForeColor="Black" onclick="lbAddNewEntry_Click" 
        style="z-index: 1; left: 133px; top: 555px; position: absolute" 
        Visible="False">(Add a new entry)</asp:LinkButton>
    <asp:Label ID="lblNewEntry" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 526px; top: 358px; position: absolute; height: 17px; width: 93px" 
        Text="New Entry" Visible="False"></asp:Label>
    <p>
        <asp:Button ID="cmbBackToStudentPage" runat="server" 
            onclick="cmbBackToStudentPage_Click" 
            style="z-index: 1; left: 828px; top: 149px; position: absolute; width: 142px" 
            Text="Student Page" Enabled="False" Visible="False" />
        <asp:Button ID="cmbViewEnrolledStudents" runat="server" 
            onclick="cmbViewEnrolledStudents_Click" 
            style="z-index: 1; left: 1031px; top: 320px; position: absolute; width: 157px" 
            Text="View Active Mentees" />
    </p>
    <asp:Button ID="cmbViewAll" runat="server" onclick="cmbViewAll_Click" 
        style="z-index: 1; left: 1031px; top: 408px; position: absolute; width: 157px" 
        Text="View All" />
    <asp:Button ID="cmbResetPage" runat="server" onclick="cmbResetPage_Click" 
        style="z-index: 1; left: 1031px; top: 232px; position: absolute; width: 157px" 
        Text="Reset Page" />
    <asp:Button ID="cmbEditVolunteerAll" runat="server" 
        onclick="cmbEditVolunteerAll_Click" 
        style="z-index: 1; left: 1031px; top: 438px; position: absolute; width: 157px;" 
        Text="Edit All Mentors" />
    <p>
        <asp:Button ID="cmbVolunteerReport" runat="server" 
            onclick="cmbVolunteerReport_Click" 
            style="z-index: 1; left: 1030px; top: 262px; position: absolute; width: 157px" 
            Text="Mentor Report" />
        <asp:DropDownList ID="ddlAssignedMentors" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            style="z-index: 1; left: 341px; top: 271px; position: absolute; width: 195px" 
            Visible="False" 
            onselectedindexchanged="ddlAssignedMentors_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:LinkButton ID="lbStudentInfo2" runat="server" Font-Size="9pt" 
            onclick="lbStudentInfo2_Click" 
            style="z-index: 1; left: 201px; top: 293px; position: absolute" Visible="False">(Student Profile)</asp:LinkButton>
        <asp:Button ID="cmbExpandedMentorReport" runat="server" 
            onclick="cmbExpandedMentorReport_Click" 
            style="z-index: 1; left: 1031px; top: 290px; position: absolute; width: 157px" 
            Text="Expanded Mentor Report" />
        <asp:Label ID="lblReportsFunctions" runat="server" Font-Italic="True" 
            Font-Size="16pt" 
            style="z-index: 1; left: 1035px; top: 205px; position: absolute; height: 22px; width: 136px; text-decoration: underline" 
            Text="Reports/Functions"></asp:Label>
    </p>
    <asp:CheckBox ID="chbCovenantLetter" runat="server" AutoPostBack="True" 
        CausesValidation="True" oncheckedchanged="chbCovenantLetter_CheckedChanged" 
        style="z-index: 1; left: 565px; top: 423px; position: absolute; width: 187px; right: 421px" 
        Text="Covenant Letter Received" Visible="False" />
    <asp:Label ID="lblCovenantSentDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 576px; top: 433px; position: absolute; width: 108px" 
        Text="Covenant Sent Date" Visible="False"></asp:Label>
    <asp:Label ID="lblCovenantReceivedDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 576px; top: 472px; position: absolute; width: 151px" 
        Text="Covenant Received Date" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlWaitingListInactiveStudents" runat="server" 
        AutoPostBack="True" BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlWaitingListInactiveStudents_SelectedIndexChanged" 
        
        
        
        
        
        
        style="z-index: 1; left: 50px; top: 269px; position: absolute; width: 213px">
    </asp:DropDownList>
    <asp:Label ID="lblWaitingListStudents" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 56px; top: 293px; position: absolute; width: 134px" 
        Text="Waiting List Students"></asp:Label>
    <asp:DropDownList ID="ddlCovRecMonth" runat="server" BackColor="#FFBB3E" 
        Enabled="False" 
        style="z-index: 1; left: 555px; top: 449px; position: absolute; width: 45px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovRecDay" runat="server" BackColor="#FFBB3E" 
        Enabled="False" 
        style="z-index: 1; left: 608px; top: 449px; position: absolute; width: 48px; " 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovRecYear" runat="server" BackColor="#FFBB3E" 
        Enabled="False" 
        style="z-index: 1; left: 663px; top: 449px; position: absolute; width: 59px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovSentMonth" runat="server" BackColor="#FFBB3E" 
        Enabled="False" 
        style="z-index: 1; left: 556px; top: 410px; position: absolute; width: 46px; height: 22px;" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovSentDay" runat="server" BackColor="#FFBB3E" 
        Enabled="False" 
        style="z-index: 1; left: 610px; top: 410px; position: absolute; width: 46px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovSentYear" runat="server" BackColor="#FFBB3E" 
        Enabled="False" 
        style="z-index: 1; left: 662px; top: 410px; position: absolute; width: 59px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Panel ID="pnlPanel" runat="server" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="2px" 
        style="z-index: 1; left: 323px; top: 243px; position: absolute; height: 243px; width: 683px" 
        Visible="False" BackColor="#FFCB5E">
        <asp:Image ID="imgMentor" runat="server" 
    
            style="z-index: 1; left: 14px; top: 67px; position: absolute; width: 194px; height: 127px;" 
            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
            Visible="False" />
        <asp:TextBox ID="txbDiscipleshipMentorNotes" runat="server" Enabled="False" 
            style="z-index: 1; left: 411px; top: 144px; position: absolute; height: 82px; width: 254px" 
            TextMode="MultiLine" Visible="False"></asp:TextBox>
        <asp:CheckBox ID="chbBackGroundCheck" runat="server" 
            style="z-index: 1; left: 244px; top: 40px; position: absolute" 
            Text="BackGroundCheck" Visible="False" AutoPostBack="True" 
            CausesValidation="True" Enabled="False" />
        <asp:CheckBox ID="chbSpiritualJourney" runat="server" 
            style="z-index: 1; left: 244px; top: 60px; position: absolute" 
            Text="SpiritualJourney" Enabled="False" Visible="False" />
        <asp:CheckBox ID="chbVehichleInsurance" runat="server" 
            style="z-index: 1; left: 244px; top: 120px; position: absolute" 
            Text="Vehichle Insurance" Visible="False" AutoPostBack="True" 
            CausesValidation="True" Enabled="False" />
        <asp:CheckBox ID="chbReleaseWaiver" runat="server" 
            style="z-index: 1; left: 244px; top: 80px; position: absolute" 
            Text="Release Waiver" Visible="False" AutoPostBack="True" 
            CausesValidation="True" Enabled="False" />
        <asp:CheckBox ID="chbGeneralInformation" runat="server" 
            style="z-index: 1; left: 244px; top: 100px; position: absolute" 
            Text="General Information" Visible="False" AutoPostBack="True" 
            CausesValidation="True" Enabled="False" />
        <asp:CheckBox ID="chbTrained" runat="server" 
            style="z-index: 1; left: 244px; top: 140px; position: absolute" Text="Trained" 
            Visible="False" AutoPostBack="True" CausesValidation="True" 
            Enabled="False" />
        <asp:DropDownList ID="ddlProgramEnrollment" runat="server" BackColor="Orange" 
            Font-Size="9pt" 
            style="z-index: 1; left: 14px; top: 195px; position: absolute; width: 198px; height: 22px" 
            Visible="False">
        </asp:DropDownList>
        <asp:TextBox ID="txbCovenantReceivedDate" runat="server" BackColor="#FFBB3E" 
            ReadOnly="True" 
            style="z-index: 1; left: 248px; top: 165px; position: absolute; width: 112px" 
            Visible="False" ontextchanged="txbCovenantReceivedDate_TextChanged"></asp:TextBox>
        <asp:ImageButton ID="imbCalCovenantReceiveDate" runat="server" 
            onclick="imbCalCovenantReceiveDate_Click1" 
            style="z-index: 1; left: 368px; top: 167px; position: absolute; height: 19px; width: 18px" 
            ToolTip="Covenant Sent Date" Visible="False" />
        <asp:ImageButton ID="imbCalCovenantSentDate" runat="server" 
            onclick="imbCalCovenantSentDate_Click1" 
            style="z-index: 1; left: 368px; top: 207px; position: absolute; height: 19px; width: 18px" 
            ToolTip="Covenant Received Date" Visible="False" />
        <asp:LinkButton ID="lbVolunteerDetails" runat="server" 
            onclick="lbVolunteerDetails_Click" 
            style="z-index: 1; left: 249px; top: 40px; position: absolute" Visible="False">(Background Details)</asp:LinkButton>
    </asp:Panel>
    <asp:Button ID="cmbExcelExport" runat="server" Enabled="False" 
        onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1031px; top: 380px; position: absolute; width: 157px" 
        Text="Export to Excel" />
    <asp:DropDownList ID="ddlDiscipleShipMentorAvailable" runat="server" 
        BackColor="Silver" 
        style="z-index: 1; left: 218px; top: 309px; position: absolute; width: 189px" 
        Visible="False" AutoPostBack="True" 
        
        
        
        
        onselectedindexchanged="ddlDiscipleShipMentorAvailable_SelectedIndexChanged" 
        Width="35px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlVolunteerWaitingList" runat="server" 
        BackColor="#FFD200" 
        
        
        
        
        
        
        style="z-index: 1; left: 761px; top: 330px; position: absolute; width: 204px" 
        AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlVolunteerWaitingList_SelectedIndexChanged1">
    </asp:DropDownList>
    <asp:Label ID="lblActiveMentors" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 574px; top: 340px; position: absolute" 
        Text="Active Mentors" Visible="False"></asp:Label>
    <asp:Label ID="lblVolunteerWaiting" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 766px; top: 354px; position: absolute; width: 140px; height: 20px;" 
        Text="Waiting List Volunteers"></asp:Label>
    <asp:CheckBox ID="chbVolunteerWaitingList" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        
        style="z-index: 1; left: 897px; top: 354px; position: absolute; width: 232px" 
        Visible="False" />
    <p>
        &nbsp;</p>
    <asp:Panel ID="pnlAssignMentor" runat="server" BackColor="#FFCB5E" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" Visible="False">
        <asp:Label ID="lblAssignMenetor" runat="server" 
    
            style="z-index: 1; left: 19px; top: 12px; position: absolute; height: 27px; width: 244px;" 
            Text="Add a Mentor" Font-Bold="True" Font-Size="18pt" Font-Underline="True" 
            Visible="False"></asp:Label>
        <asp:Button ID="cmbAssignMentor" runat="server" onclick="cmbAssignMentor_Click" 
            style="z-index: 1; left: 18px; top: 70px; position: absolute; height: 26px; width: 166px" 
            Text="Commit Mentor Choice" Visible="False" />
        <asp:Button ID="cmbCancelAssignMentor" runat="server" 
            onclick="cmbCancelAssignMentor_Click" 
            style="z-index: 1; left: 309px; top: 123px; position: absolute; width: 235px; height: 31px;" 
            Text="Cancel" Visible="False" />
        <asp:Label ID="lblUpdateMentor" runat="server" Font-Bold="True" 
            Font-Size="18pt" Font-Underline="True" 
            style="z-index: 1; left: 334px; top: 11px; position: absolute; height: 28px; width: 186px; right: 314px;" 
            Text="Update a Mentor"></asp:Label>
        <asp:Label ID="lblRemoveMentor" runat="server" Font-Bold="True" 
            Font-Size="18pt" Font-Underline="True" 
            style="z-index: 1; left: 613px; top: 10px; position: absolute; height: 30px; width: 229px" 
            Text="Remove a Mentor"></asp:Label>
        <asp:Button ID="cmbUpdateMentor" runat="server" onclick="cmbUpdateMentor_Click" 
            style="z-index: 1; left: 341px; top: 70px; position: absolute; height: 26px; width: 173px" 
            Text="Update Mentor" Visible="False" />
        <asp:Button ID="cmbRemoveMentor" runat="server" onclick="cmbRemoveMentor_Click" 
            style="z-index: 1; left: 618px; top: 41px; position: absolute; height: 27px; width: 171px" 
            Text="Remove Current Mentor" Visible="False" />
        <asp:DropDownList ID="ddlDiscipleshipMentorOptions2" runat="server" 
            AutoPostBack="True" BackColor="Silver" CausesValidation="True" 
            onselectedindexchanged="ddlDiscipleshipMentorOptions2_SelectedIndexChanged" 
            style="z-index: 1; left: 332px; top: 42px; position: absolute; width: 189px; height: 22px" 
            Visible="False">
        </asp:DropDownList>
        <asp:Button ID="cmbClearMentors" runat="server" onclick="cmbClearMentors_Click" 
            style="z-index: 1; left: 620px; top: 72px; position: absolute; width: 171px; height: 27px" 
            Text="Clear Mentor List" Visible="False" />
        <asp:LinkButton ID="lbViewMentorProfile" runat="server" Font-Size="10pt" 
            onclick="lbViewMentorProfile_Click" 
            style="z-index: 1; left: 43px; top: 100px; position: absolute; width: 155px" 
            Visible="False">(View Mentor Profile)</asp:LinkButton>
        <asp:Panel ID="Panel6" runat="server">
        </asp:Panel>
    </asp:Panel>
    <asp:Label ID="lblReportTitle" runat="server" Font-Size="20pt"
        style="z-index: 9999999; left: 436px; top: 384px; position: absolute; height: 35px; width: 513px; text-decoration: underline;" 
        Visible="False"></asp:Label>
    <asp:Label ID="lblVariousPaperwork" runat="server" Font-Italic="True" 
        style="z-index: 1; left: 573px; top: 264px; position: absolute; height: 29px; width: 166px; text-decoration: underline" 
        Text="Various Paperwork" Visible="False" Font-Size="14pt"></asp:Label>
    <asp:LinkButton ID="lbAddMentor" runat="server" Font-Size="10pt" 
        onclick="lbAddMentor_Click" 
        style="z-index: 1; left: 355px; top: 254px; position: absolute; width: 216px; height: 13px; bottom: 270px;" 
        Visible="False">(Add/Update/Remove a mentor)</asp:LinkButton>
    <asp:Button ID="cmbWaitListReport" runat="server" 
        onclick="cmbWaitListReport_Click" 
        style="z-index: 1; left: 1031px; top: 350px; position: absolute; width: 157px" 
        Text="View Waiting List" />
    <asp:Button ID="cmbRecentUpdates" runat="server" 
        onclick="cmbRecentUpdates_Click" 
        style="z-index: 1; left: 1031px; top: 439px; position: absolute; width: 157px; bottom: 64px;" 
        Text="Most Recent Updates" Visible="False" />
    <asp:Label ID="lblMenteeMaintenance" runat="server" Font-Size="16pt" 
        style="z-index: 1; left: 61px; top: 199px; position: absolute; height: 25px; width: 180px; text-decoration: underline" 
        Text="Mentee Maintenance"></asp:Label>

    <asp:Panel ID="Panel5" runat="server"  
        style="z-index: 1; left: 23px; top: 193px; position: absolute; height: 329px; width: 265px" 
        ViewStateMode="Enabled" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="2px" Visible="False" BackColor="#FFCB5E">
    </asp:Panel>

    <asp:LinkButton ID="lbStudentInfo" runat="server" Font-Size="9pt" 
        onclick="lbStudentInfo_Click" 
        style="z-index: 1; left: 157px; top: 250px; position: absolute" Visible="False">(Student Profile)</asp:LinkButton>

    <asp:GridView ID="gvEditVolunteersAll" runat="server" AllowSorting="False" 
    BorderStyle="Solid" BorderWidth="4px" CssClass="style6" 
    OnSelectedIndexChanged="gvEditVolunteersAll_SelectedIndexChanged"
    OnSelectedIndexChanging="gvEditVolunteersAll_SelectedIndexChanging"
    OnRowDataBound="gvEditVolunteersAll_RowDataBound"
    ShowHeaderWhenEmpty="True" AutoGenerateEditButton="True" OnRowEditing="gvEditVolunteersAll_RowEditing" 
    OnRowUpdating="gvEditVolunteersAll_RowUpdating"
    OnRowCancelingEdit="gvEditVolunteersAll_RowCancelingEdit"
    OnRowDeleting="gvEditVolunteersAll_RowDeleting"  DataKeyNames="LastName"
    BorderColor="Black" AutoGenerateColumns="True" 
    EnablePersistedSelection="True" 
    BackColor="#FFCB5E" Visible="False">
        <AlternatingRowStyle BackColor="Silver" />
        <RowStyle Wrap="False" />
        <EditRowStyle Wrap="False" />
        <EmptyDataRowStyle BorderStyle="Solid" />
        <HeaderStyle Font-Bold="True" Font-Overline="False" Font-Size="18pt" 
        Font-Underline="False" BackColor="Black" ForeColor="White" />
        <Columns >
            <asp:TemplateField HeaderText ="LastName">
                <ItemTemplate>
                    <%#Eval("name") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="textbox1" runat="server" 
                Text='<%#Eval("name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText ="FirstName">
                <ItemTemplate>
                    <%#Eval("name") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="textbox1" runat="server" 
                Text='<%#Eval("name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Calendar ID="calCovenantSentDate" runat="server" BackColor="#FFCB5E" 
        onselectionchanged="calCovenantSentDate_SelectionChanged1" ShowGridLines="True" 
        style="z-index: 1; left: 714px; top: 204px; position: absolute; height: 188px; width: 259px" 
        Visible="False"></asp:Calendar>
    <asp:Calendar ID="calCovenantReceiveDate" runat="server" BackColor="#FFCB5E" 
        onselectionchanged="calCovenantReceiveDate_SelectionChanged1" 
        ShowGridLines="True" 
        style="z-index: 1; left: 729px; top: 206px; position: absolute; height: 188px; width: 259px" 
        Visible="False"></asp:Calendar>

    </form>
</body>
</html>
