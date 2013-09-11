<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoreKidsDetail.aspx.cs" Inherits="UIF.PerformingArts.CoreKidsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

<style type="text/css"> 
   div { z-index: 9999; } 
    #form2
    {
        width: 539px;
    }
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
        style="z-index: 999; left: 213px; top: 97px; position: absolute; height: 37px; width: 947px" 
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

    <asp:Label ID="lblEnrolledStudents" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 39px; top: 220px; position: absolute; width: 168px" 
        Text="Enrolled Students" Visible="False"></asp:Label>
    <asp:CheckBox ID="chbDiscipleShipMentor" runat="server" 
        style="z-index: 1; left: 31px; top: 518px; position: absolute; width: 256px" 
        Text="Discipleship Mentor Participant" />
    <asp:Label ID="lblDiscipleshipmentor" runat="server" Font-Size="25pt" 
        style="z-index: 1; left: 425px; top: 151px; position: absolute; height: 42px; width: 394px; text-decoration: underline" 
        Text="CoreKids Detail Information" Font-Bold="False"></asp:Label>

    <asp:GridView ID="gvCoreKidsDetail" runat="server" BorderColor="Black"
        style="z-index: 1; left: 257px; top: 318px; position: absolute; height: 175px; width: 342px" 
        onselectedindexchanged="gvCoreKidsDetail_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        BackColor="#FFD200" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="Silver" ForeColor="Black" />
        <Columns>
                <asp:BoundField  DataField="ProgramName" HeaderText="ProgramName" ReadOnly="true" SortExpression="ProgramName" />
                <asp:BoundField  DataField="Attend %" HeaderText="Attend %" ReadOnly="true"  SortExpression="Attend %"  />
                <asp:BoundField  DataField="# of entries" HeaderText="# of entries" ReadOnly="true"  SortExpression="# of entries"  />
                <asp:BoundField  DataField="ProgramSeason" HeaderText="ProgramSeason" ReadOnly="true"  SortExpression="ProgramSeason"  />
        </Columns>
    </asp:GridView>

    <asp:DropDownList ID="ddlDiscipleshipMentor" runat="server" 
        style="z-index: 1; left: 36px; top: 198px; position: absolute; width: 195px; bottom: 265px;" 
        AutoPostBack="True" BackColor="#FFD200" 
        onselectedindexchanged="ddlDiscipleshipMentor_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>
    <asp:Button ID="cmbDiscipleshipMentor" runat="server" 
        onclick="cmbDiscipleshipMentor_Click" 
        style="z-index: 1; left: 268px; top: 205px; position: absolute; width: 149px" 
        Text="Retrieve Student Info" Visible="False" />
    <p>
        &nbsp;</p>
    <asp:DropDownList ID="ddlVolunteerActiveMentees" runat="server" 
        BackColor="#FFD200" 
        
        
        style="z-index: 1; left: 35px; top: 287px; position: absolute; width: 195px" 
        Visible="False">
    </asp:DropDownList>
    <asp:TextBox ID="txbNotes" runat="server" BackColor="#FFD200" 
        BorderStyle="Solid" 
        style="z-index: 1; left: 522px; top: 376px; position: absolute; height: 182px; width: 600px" 
        TextMode="MultiLine" ontextchanged="txbNotes_TextChanged" Visible="False"></asp:TextBox>
    <asp:Label ID="lblNotes" runat="server" 
        style="z-index: 1; left: 40px; top: 557px; position: absolute; width: 101px; " 
        Text="Activity History" Font-Size="10pt" Visible="False"></asp:Label>
    <asp:Button ID="cmbUpdate" runat="server" onclick="cmbUpdate_Click" 
        style="z-index: 1; left: 266px; top: 541px; position: absolute; width:214px"  
        Text="Update Student Information" Enabled="False" Visible="False" />
    <asp:Image ID="imgImage" runat="server" 
        style="z-index: 1; left: 32px; top: 320px; position: absolute; height: 147px; width: 210px" 
        Visible="False" />
    <asp:Label ID="lblDiscipleMentor" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 524px; top: 241px; position: absolute; " 
        Text="DiscipleshipMentor" Visible="False"></asp:Label>
    <asp:TextBox ID="txbStaffCoordinator" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 523px; top: 265px; position: absolute; width: 209px" 
        Visible="False"></asp:TextBox>
    <asp:Label ID="lblStaffCoordinator" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 525px; top: 290px; position: absolute; width: 120px" 
        Text="Staff Coordinator" Visible="False"></asp:Label>
    <asp:CheckBox ID="chbHasGraduated" runat="server" 
        oncheckedchanged="chbHasGraduated_CheckedChanged" 
        style="z-index: 1; left: 261px; top: 272px; position: absolute; width: 147px" 
        Text="Has Graduated?" Visible="False" />
    <asp:TextBox ID="txbProgramEnrollment" runat="server" BackColor="#FFD200" 
        ontextchanged="txbProgramEnrollment_TextChanged" 
        
        style="z-index: 1; left: 523px; top: 310px; position: absolute; width: 207px" 
        Visible="False"></asp:TextBox>
    <asp:Label ID="lblProgramEnrollment" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 527px; top: 332px; position: absolute" 
        Text="Program Enrollment" Visible="False"></asp:Label>
    <asp:TextBox ID="txbComments" runat="server" BackColor="#FFD200" 
        ontextchanged="txbComments_TextChanged" 
        style="z-index: 1; left: 259px; top: 376px; position: absolute; height: 144px; width: 229px" 
        TextMode="MultiLine" Visible="False"></asp:TextBox>
    <asp:Label ID="lblLastUpdatedBy" runat="server" Enabled="False" 
        style="z-index: 1; left: 1024px; position: absolute; height: 82px; width: 181px; top: 18px" 
        Text="Last Updated By:"></asp:Label>
    <p>
        <asp:GridView ID="gvViewAll" runat="server" BackColor="#FFD200" 
            
            style="z-index: 1; left: 39px; top: 415px; position: absolute; height: 133px; width: 187px" 
            onselectedindexchanged="gvViewAll_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="#999999" />
            <HeaderStyle BackColor="Black" ForeColor="White" />
        </asp:GridView>
        <asp:LinkButton ID="lbVolunteerInformation" runat="server" Font-Size="8pt" 
            Font-Underline="False" onclick="lbVolunteerInformation_Click" 
            style="z-index: 1; left: 634px; top: 241px; position: absolute" 
            Visible="False">(View Mentor Info)</asp:LinkButton>
        <asp:CheckBox ID="chbWaitingListInactive" runat="server" AutoPostBack="True" 
            CausesValidation="True" 
            style="z-index: 1; left: 261px; top: 240px; position: absolute; width: 221px;" 
            Text="Student (WaitingList/Inactive)" Visible="False" />
        <asp:Button ID="cmbViewWaitingList" runat="server" 
            onclick="cmbViewWaitingList_Click" 
            style="z-index: 1; left: 1029px; top: 276px; position: absolute; width: 139px" 
            Text="View Waiting List" Visible="False" />
    </p>
    <asp:Label ID="lblComments" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 266px; top: 359px; position: absolute; height: 22px; width: 88px" 
        Text="Comments" Visible="False"></asp:Label>
    <asp:GridView ID="gvStudentHistory" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderWidth="3px" 
        onselectedindexchanged="gvStudentHistory_SelectedIndexChanged" 
        
        
        
        
        style="z-index: 1; left: 37px; top: 576px; position: absolute; height: 147px; width: 770px">
    </asp:GridView>
    <asp:LinkButton ID="lbAddNewEntry" runat="server" Font-Size="10pt" 
        ForeColor="Black" onclick="lbAddNewEntry_Click" 
        style="z-index: 1; left: 133px; top: 557px; position: absolute" 
        Visible="False">(Add a new entry)</asp:LinkButton>
    <asp:Label ID="lblNewEntry" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 526px; top: 358px; position: absolute; height: 17px; width: 93px" 
        Text="New Entry" Visible="False"></asp:Label>
    <p>
        <asp:Button ID="cmbBackToStudentPage" runat="server" 
            onclick="cmbBackToStudentPage_Click" 
            style="z-index: 1; left: 1061px; top: 375px; position: absolute; width: 142px" 
            Text="Student Page" Enabled="False" Visible="False" />
        <asp:Button ID="cmbViewEnrolledStudents" runat="server" 
            onclick="cmbViewEnrolledStudents_Click" 
            style="z-index: 1; left: 1028px; top: 245px; position: absolute; width: 139px" 
            Text="View Active Mentees" Visible="False" />
        <asp:Label ID="lblReportCard" runat="server" Font-Size="20pt" 
            style="z-index: 1; left: 50px; top: 262px; position: absolute; height: 30px; width: 626px; text-decoration: underline;" 
            Text="ReportCard for:     " Visible="False"></asp:Label>
    </p>
    <asp:Button ID="cmbViewAll" runat="server" onclick="cmbViewAll_Click" 
        style="z-index: 1; left: 1058px; top: 409px; position: absolute; width: 141px" 
        Text="View All" Visible="False" />
    <asp:Button ID="cmbResetPage" runat="server" onclick="cmbResetPage_Click" 
        style="z-index: 1; left: 1028px; top: 306px; position: absolute; width: 140px" 
        Text="Reset Page" Visible="False" />
    <p>
        <asp:CheckBox ID="chbStudentVolunteer" runat="server" 
            style="z-index: 1; left: 31px; top: 496px; position: absolute; width: 254px" 
            Text="Student/Staff/Assistant" />
    </p>
    <asp:CheckBox ID="chbCovenantLetter" runat="server" AutoPostBack="True" 
        CausesValidation="True" oncheckedchanged="chbCovenantLetter_CheckedChanged" 
        style="z-index: 1; left: 783px; top: 226px; position: absolute; width: 187px; right: 213px" 
        Text="Covenant Letter Received" Visible="False" />
    <asp:Label ID="lblCovenantSentDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 811px; top: 333px; position: absolute; width: 108px" 
        Text="Covenant Sent Date" Visible="False"></asp:Label>
    <asp:Label ID="lblCovenantReceivedDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 798px; top: 290px; position: absolute; width: 151px" 
        Text="Covenant Received Date" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlWaitingListInactiveStudents" runat="server" 
        AutoPostBack="True" BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlWaitingListInactiveStudents_SelectedIndexChanged" 
        
        
        
        
        style="z-index: 1; left: 36px; top: 241px; position: absolute; width: 194px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblWaitingListStudents" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 38px; top: 263px; position: absolute; width: 134px" 
        Text="Waiting List Students" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlCovRecMonth" runat="server" BackColor="#FFD200" 
        Enabled="False" 
        style="z-index: 1; left: 775px; top: 264px; position: absolute; width: 45px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovRecDay" runat="server" BackColor="#FFD200" 
        Enabled="False" 
        style="z-index: 1; left: 843px; top: 264px; position: absolute; width: 48px; right: 98px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovRecYear" runat="server" BackColor="#FFD200" 
        Enabled="False" 
        style="z-index: 1; left: 911px; top: 264px; position: absolute; width: 59px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovSentMonth" runat="server" BackColor="#FFD200" 
        Enabled="False" 
        style="z-index: 1; left: 773px; top: 310px; position: absolute; width: 46px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovSentDay" runat="server" BackColor="#FFD200" 
        Enabled="False" 
        style="z-index: 1; left: 843px; top: 310px; position: absolute; width: 46px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlCovSentYear" runat="server" BackColor="#FFD200" 
        Enabled="False" 
        style="z-index: 1; left: 910px; top: 310px; position: absolute; width: 59px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Panel ID="pnlPanel" runat="server" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="2px" Enabled="False" 
        style="z-index: 1; left: 763px; top: 208px; position: absolute; height: 141px; width: 223px" 
        Visible="False">
    </asp:Panel>
    <asp:Button ID="cmbExcelExport" runat="server" Enabled="False" 
        onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1029px; top: 213px; position: absolute; width: 138px" 
        Text="Export to Excel" />
    <asp:DropDownList ID="ddlDiscipleShipMentorAvailable" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 522px; top: 218px; position: absolute; width: 212px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlVolunteerWaitingList" runat="server" 
        BackColor="#FFD200" 
        
        
        style="z-index: 1; left: 35px; top: 331px; position: absolute; width: 195px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblActiveMentors" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 38px; top: 308px; position: absolute" 
        Text="Active Mentors" Visible="False"></asp:Label>
    <asp:Label ID="lblVolunteerWaiting" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 37px; top: 354px; position: absolute; width: 140px" 
        Text="Waiting List Volunteers" Visible="False"></asp:Label>
    <asp:CheckBox ID="chbVolunteerWaitingList" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 261px; top: 307px; position: absolute; width: 232px" 
        Text="Volunteers (WaitingList/InActive)" Visible="False" />
    <asp:Label ID="lblReportExplanation" runat="server" 
        
        
        
        style="z-index: 1; left: 654px; top: 262px; position: absolute; height: 49px; width: 520px; font-weight: 700; text-decoration: underline;" Text="6 Criteria (6 points Potentially)  Any given student must receive at least 3 points out of the 6 categories available below, in order to make the “CoreKids” list.
                                
 "></asp:Label>
    <asp:Label ID="lblPoint1" runat="server" 
        style="z-index: 1; left: 654px; top: 319px; position: absolute; width: 605px" 
        
        
        
        Text="1)	 Consistent Attendance:   To receive the point for this category, the student must have an attendance percentage of at least 70.0 % in at least any one program."></asp:Label>
    <asp:Label ID="lblPoint2" runat="server" 
        style="z-index: 1; left: 654px; top: 359px; position: absolute; width: 527px" 
        
        
        Text="2)	 Longevity:    To receive the point for this category, the student must maintain an attendance program percentage of 70.0 % over the course of the 2 most recent years in any programs.    "></asp:Label>
    <asp:Label ID="lblPoint3" runat="server" 
        style="z-index: 1; left: 655px; top: 422px; position: absolute; width: 520px" 
        
        
        Text="3)	Multiple Program Participation:   To receive the point for this category, the student must be found to participate in 2 distinctly different programs.  "></asp:Label>
    <asp:Label ID="lblPoint4" runat="server" 
        style="z-index: 1; left: 655px; top: 566px; position: absolute; width: 520px" 
        
        
        Text="6)	Discipleship Mentor Program:     To receive the point for this category, the student must be found to be enrolled in the Discipleship Mentor program."></asp:Label>
    <asp:Label ID="lblPoint5" runat="server" 
        style="z-index: 1; left: 655px; top: 527px; position: absolute; width: 520px" 
        
        Text="5)	Student/Staff/Assistant:    To receive the point for this category, the student must be categorized as a Student/Staff/Volunteer with involvement in a program."></asp:Label>
    <asp:Label ID="lblPoint6" runat="server" 
        style="z-index: 1; left: 654px; top: 483px; position: absolute; width: 520px" 
        
        
        Text="4)	RSVP Gospel:   To receive the point for this category, it must be recorded that the student received the Gospel message in at least one program function."></asp:Label>
    <asp:Label ID="lblExplainPoints" runat="server" 
        style="z-index: 1; left: 655px; top: 738px; position: absolute; width: 520px" 
        Text="Label"></asp:Label>
    <asp:CheckBox ID="chbRSVPGospel" runat="server" 
        style="z-index: 1; left: 31px; top: 473px; position: absolute; width: 170px" 
        Text="RSVP Gospel" />
    <asp:LinkButton ID="lbDiscipleshipMentor" runat="server" Enabled="False" 
        onclick="lbDiscipleshipMentor_Click" 
        style="z-index: 1; left: 77px; top: 538px; position: absolute; width: 170px">(DiscipleShip Mentor)</asp:LinkButton>
    </form>
</body>
</html>
