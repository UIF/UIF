
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Options.aspx.cs" Inherits="UIF.PerformingArts.Options" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

<style type="text/css"> 
   div { z-index: 1;
        left: 446px;
        top: 209px;
        position: absolute;
        height: 335px;
        width: 823px;
        margin-left: 40px;
    } 
</style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>


</head>
<body bgcolor="Orange">
    <form id="form2" runat="server">
    <div>
    
    </div>

    <asp:Panel ID="pnlBackground" runat="server" BackColor="White"  
        style="z-index: 1; left: -41px; top: 1px; position: absolute; height: 133px; width: 1327px" 
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
            x <%# Eval("Text") %>
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
        style="z-index: 1; left: 38px; top: 231px; position: absolute; width: 168px" 
        Text="Enrolled Students"></asp:Label>
    <asp:Label ID="lblDiscipleshipmentor" runat="server" Font-Size="28pt" 
        style="z-index: 1; left: 463px; top: 142px; position: absolute; height: 42px; width: 346px; text-decoration: underline; right: 206px; font-weight: 700; font-style: italic;" 
        Text="Welcome to Options" Font-Bold="False" CssClass="style10" 
        Font-Italic="True"></asp:Label>
    <asp:Button ID="cmbStudentPage2" runat="server" Enabled="False" 
        onclick="cmbStudentPage2_Click" 
        style="z-index: 1; left: 312px; top: 495px; position: absolute; height: 27px; width: 136px" 
        Text="Student Page" Visible="False" />
    <asp:CheckBox ID="chbPromise" runat="server" 
        
        style="z-index: 1; left: 1081px; top: 674px; position: absolute; width: 112px;" Text="Promise" 
        Visible="False" />
    <p>
        <asp:CheckBox ID="chbACT" runat="server" 
            style="z-index: 1; left: 534px; top: 379px; position: absolute; width: 117px;" 
            Text="ACT Exam" Visible="False" />
        <asp:TextBox ID="txbPostGraduatePlans" runat="server" BackColor="#FFD200" 
            
            style="z-index: 1; left: 825px; top: 683px; position: absolute; width: 194px; right: 86px; height: 17px" 
            Visible="False"></asp:TextBox>
        <asp:Button ID="cmbComprehensiveReport" runat="server" 
            onclick="cmbComprehensiveReport_Click" 
            style="z-index: 1; left: 312px; top: 509px; position: absolute; width: 136px;" 
            Text="View All" Visible="False" />
    </p>
    <asp:TextBox ID="txbNotes" runat="server" BackColor="#FFD200" 
        BorderStyle="Solid" 
        style="z-index: 1; left: 486px; top: 207px; position: absolute; height: 346px; width: 824px" 
        TextMode="MultiLine" ontextchanged="txbNotes_TextChanged" Visible="False"></asp:TextBox>
    <asp:Label ID="lblNotes" runat="server" 
        style="z-index: 1; left: 43px; top: 553px; position: absolute; width: 101px; right: 884px;" 
        Text="Activity History" Font-Size="10pt" Visible="False"></asp:Label>
    <asp:Button ID="cmbUpdate" runat="server" onclick="cmbUpdate_Click" 
        style="z-index: 1; left: 249px; top: 417px; position: absolute; width:214px; height: 38px;"  
        Text="Update Student Information" Enabled="False" Visible="False" />
    <asp:Image ID="Image1" runat="server" 
        style="z-index: 1; left: 34px; top: 265px; position: absolute; height: 140px; width: 196px" 
        Visible="False" />
    <asp:TextBox ID="txbDiscipleshipMentor" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 870px; top: 621px; position: absolute; width: 209px" 
        ontextchanged="txbDiscipleshipMentor_TextChanged" Visible="False"></asp:TextBox>
    <asp:Label ID="lblDiscipleMentor" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 907px; top: 620px; position: absolute; width: 191px" 
        Text="DiscipleshipMentor" Visible="False"></asp:Label>
    <asp:TextBox ID="txbStaffCoordinator" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 821px; top: 571px; position: absolute; width: 209px" 
        Visible="False"></asp:TextBox>
    <asp:Label ID="lblStaffCoordinator" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 826px; top: 603px; position: absolute; width: 120px" 
        Text="Staff Coordinator" Visible="False"></asp:Label>
    <asp:CheckBox ID="chbHasGraduated" runat="server" 
        oncheckedchanged="chbHasGraduated_CheckedChanged" 
        style="z-index: 1; left: 34px; top: 514px; position: absolute; width: 147px" 
        Text="Has Graduated?" Visible="False" BorderColor="#FFD200" />
    <asp:TextBox ID="txbProgramEnrollment" runat="server" BackColor="#FFD200" 
        ontextchanged="txbProgramEnrollment_TextChanged" 
        
        style="z-index: 1; left: 824px; top: 637px; position: absolute; width: 193px" 
        Visible="False"></asp:TextBox>
    <asp:Label ID="lblProgramEnrollment" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 829px; top: 660px; position: absolute" 
        Text="Program Enrollment" Visible="False"></asp:Label>
    <asp:TextBox ID="txbComments" runat="server" BackColor="#FFD200" 
        ontextchanged="txbComments_TextChanged" 
        style="z-index: 1; left: 248px; top: 268px; position: absolute; height: 135px; width: 210px" 
        TextMode="MultiLine" Visible="False"></asp:TextBox>
    <asp:Label ID="lblLastUpdatedBy" runat="server" Enabled="False" 
        style="z-index: 1; left: 1044px; position: absolute; height: 82px; width: 186px; top: 18px" 
        Text="Last Updated By:"></asp:Label>
    <p>
        <asp:DropDownList ID="ddlOptions" runat="server" BackColor="#FFD200" 
            
            
            style="z-index: 1; left: 35px; top: 207px; position: absolute; width: 193px; right: 757px;" 
            AutoPostBack="True" 
            onselectedindexchanged="ddlOptions_SelectedIndexChanged1" 
            CausesValidation="True">
        </asp:DropDownList>
    </p>
    <asp:Label ID="lblComments" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 250px; top: 253px; position: absolute; height: 22px; width: 88px" 
        Text="Comments" Visible="False"></asp:Label>
    <asp:GridView ID="gvStudentHistory" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderWidth="3px" 
        onselectedindexchanged="gvStudentHistory_SelectedIndexChanged" 
        
        
        
        
        style="z-index: 1; left: -453px; top: 360px; position: absolute; height: 147px; width: 1022px">
    </asp:GridView>
    <asp:LinkButton ID="lbAddNewEntry" runat="server" Font-Size="10pt" 
        ForeColor="Black" onclick="lbAddNewEntry_Click" 
        style="z-index: 1; left: 139px; top: 553px; position: absolute" 
        Visible="False">(Add a new entry)</asp:LinkButton>
    <asp:Label ID="lblNewEntry" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 498px; top: 189px; position: absolute; height: 18px; width: 93px" 
        Text="New Entry" Visible="False"></asp:Label>
    <asp:Button ID="cmbOptions" runat="server" onclick="cmbOptions_Click1" 
        style="z-index: 1; left: 249px; top: 158px; position: absolute; width: 148px" 
        Text="Review Student Info" Visible="False" />
    <asp:CheckBox ID="chbFAFSA" runat="server" 
        style="z-index: 1; left: 1091px; top: 376px; position: absolute; height: 26px; width: 235px;" 
        Text="FAFSA Completed" Visible="False" />
    <asp:Label ID="lblPostGraduatePlans" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 833px; top: 705px; position: absolute" 
        Text="Post Graduate Plans" Visible="False"></asp:Label>
    <asp:GridView ID="gvOptionsComprehensive" runat="server" BackColor="#FFD200" 
        
        
        
        
        style="z-index: 1; left: 4px; top: 578px; position: absolute; height: 52px; width: 253px">
        <AlternatingRowStyle BackColor="#999999" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
        <RowStyle Wrap="False" />
    </asp:GridView>
    <asp:Button ID="cmbResetPage" runat="server" onclick="cmbResetPage_Click" 
        style="z-index: 1; left: 59px; top: 157px; position: absolute; width: 135px" 
        Text="Reset Page" />
    <asp:Button ID="cmbExcelExport" runat="server" Enabled="False" 
        onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 310px; top: 539px; position: absolute; width: 136px" 
        Text="Export to Excel" Visible="False" />
    <asp:Button ID="cmbBaseReport" runat="server" onclick="cmbBaseReport_Click" 
        style="z-index: 1; left: 350px; top: 474px; position: absolute; height: 30px; width: 113px;" 
        Text="Base Report" Visible="False" />
    <asp:Button ID="cmbStudentDescription" runat="server" 
        onclick="cmbStudentDescription_Click" 
        style="z-index: 1; left: 310px; top: 514px; position: absolute; width: 133px" 
        Text="Description Report" Visible="False" />
    <asp:DropDownList ID="ddlBuses" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlBuses_SelectedIndexChanged" 
        style="z-index: 1; left: 248px; top: 207px; position: absolute; width: 146px" 
        Visible="False">
    </asp:DropDownList>
    <asp:CheckBox ID="chbHSTranscript" runat="server" 
        style="z-index: 1; left: 34px; top: 477px; position: absolute; width: 150px" 
        Text="HS Transcript" Visible="False" />
    <asp:CheckBox ID="chbBirthCertificate" runat="server" 
        style="z-index: 1; left: 34px; top: 458px; position: absolute; width: 183px" 
        Text="BirthCertificate" Visible="False" />
    <asp:CheckBox ID="chbPersonalSSI" runat="server" 
        style="z-index: 1; left: 823px; top: 736px; position: absolute; width: 182px" 
        Text="PersonalSSI" Visible="False" />
    <asp:CheckBox ID="chbBankAccount" runat="server" 
        style="z-index: 1; left: 158px; top: 478px; position: absolute; width: 159px" 
        Text="BankAccount" Visible="False" />
    <div>
        <asp:CheckBox ID="chbHealthInsurance" runat="server" 
        style="z-index: 1; left: 533px; top: 423px; position: absolute; width: 205px" 
        Text="HealthInsurance" Visible="False" />
    </div>





    <asp:ImageButton ID="imbCollegeSATExamDate" runat="server" 
        style="z-index: 1; left: 509px; top: 245px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College SATExam Date" Visible="False" 
        onclick="imbCollegeSATExamDate_Click" />
    <asp:ImageButton ID="imbCollegeACTExamDate" runat="server" 
        style="z-index: 1; left: 509px; top: 270px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College ACTExam Date" Visible="False" 
        onclick="imbCollegeACTExamDate_Click" />
    <asp:ImageButton ID="imbCollegeGameFilmDate" runat="server" 
        style="z-index: 1; left: 509px; top: 295px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College GameFilm Date" Visible="False" 
        onclick="imbCollegeGameFilmDate_Click" />
    <asp:ImageButton ID="imbCollegeAudtionPortfolioDate" runat="server" 
        style="z-index: 1; left: 509px; top: 320px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College AuditionPortfolioDate" Visible="False" 
        onclick="imbCollegeAudtionPortfolioDate_Click" />
    <asp:ImageButton ID="imbCollegeFAFSADate" runat="server" 
        style="z-index: 1; left: 509px; top: 345px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College FAFSA Date" Visible="False" 
        onclick="imbCollegeFAFSADate_Click" />
    <asp:ImageButton ID="imbCollegeApplicationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 370px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College Application Date" Visible="False" 
        onclick="imbCollegeApplicationDate_Click" />
    <asp:ImageButton ID="imbCollegeHealthInsurance" runat="server" 
        style="z-index: 1; left: 509px; top: 395px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College Health Insurance" Visible="False" 
        onclick="imbCollegeHealthInsurance_Click" />
    <asp:ImageButton ID="imbCollegePittsburghPromiseDate" runat="server" 
        style="z-index: 1; left: 509px; top: 420px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College PittsburghPromiseDate" Visible="False" 
        onclick="imbCollegePittsburghPromiseDate_Click" />
    <asp:ImageButton ID="imbCollegeDate" runat="server" 
        style="z-index: 1; left: 509px; top: 445px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College Date" Visible="False" 
        onclick="imbCollegeDate_Click" />
    <asp:ImageButton ID="imbCollegeNCAAEligibilityDate" runat="server" 
        style="z-index: 1; left: 509px; top: 470px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College NCAAEligibility Date" Visible="False" 
        onclick="imbCollegeNCAAEligibilityDate_Click" />
    <asp:ImageButton ID="imbCollegeScholarshipGrantLoanDate" runat="server" 
        style="z-index: 1; left: 509px; top: 495px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set College ScholarshipGrantLoan Date" Visible="False" 
        onclick="imbCollegeScholarshipGrantLoanDate_Click" />
    <asp:ImageButton ID="imbCollegeVisitDate" runat="server" 
        style="z-index: 1; top: 520px; position: absolute; height: 19px; width: 20px; left: 509px;" 
        ToolTip="Set College Visit date" Visible="False" />


    <asp:CheckBox ID="chbCollegeSAT" runat="server" 
        style="z-index: 1; left: 533px; top: 245px; position: absolute; width: 194px" 
        Text="SAT" Visible="False" />
    <asp:CheckBox ID="chbCollegeACT" runat="server" 
        style="z-index: 1; left: 533px; top: 270px; position: absolute; width: 194px" 
        Text="ACT" Visible="False" />
    <asp:CheckBox ID="chbCollegeGameFilm" runat="server" 
        style="z-index: 1; left: 533px; top: 295px; position: absolute; width: 194px" 
        Text="GameFilm" Visible="False" />
    <asp:CheckBox ID="chbCollegeAuditionPortfolio" runat="server" 
        style="z-index: 1; left: 533px; top: 320px; position: absolute; width: 194px" 
        Text="AudtionPortfolio" Visible="False" />
    <asp:CheckBox ID="chbCollegeFAFSACompleted" runat="server" 
        style="z-index: 1; left: 533px; top: 345px; position: absolute; width: 194px" 
        Text="FAFSACompleted" Visible="False" />
    <asp:CheckBox ID="chbCollegeApplicationAccepted" runat="server" 
        style="z-index: 1; left: 533px; top: 370px; position: absolute; width: 282px" 
        Text="ApplicationAccepted" Visible="False" />
    <asp:CheckBox ID="chbCollegeHealthInsurance" runat="server" 
        style="z-index: 1; left: 533px; top: 395px; position: absolute; width: 194px" 
        Text="HealthInsurance" Visible="False" />
    <asp:CheckBox ID="chbCollegePittsburghPromiseEligible" runat="server" 
        style="z-index: 1; left: 533px; top: 420px; position: absolute; right: 422px; height: 18px" 
        Text="Pittsburgh Promise Eligible" Visible="False" />
    <asp:CheckBox ID="chbCollegeFair" runat="server" 
        style="z-index: 1; left: 533px; top: 445px; position: absolute; right: 422px; height: 18px" 
        Text="College Fair" Visible="False" />
    <asp:CheckBox ID="chbNCAA" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 533px; top: 470px; position: absolute; width: 225px" 
        Text="NCAA Eligibility Center" Visible="False" />
    <asp:CheckBox ID="chbCollegeScholarshipGrantLoanDate" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 533px; top: 495px; position: absolute; width: 283px" 
        Text="ScholarshipGrantLoan" Visible="False" />
    <asp:CheckBox ID="chbCollegeVisitDate" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 533px; top: 520px; position: absolute; width: 225px" 
        Text="College Visit" Visible="False" />


    <asp:TextBox ID="chbCollegeSATDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 603px; top: 245px; position: absolute; width: 82px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="chbCollegeACTDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 603px; top: 270px; position: absolute; width: 82px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeGameFilmDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 640px; top: 295px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeAuditionPortfolioDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 665px; top: 320px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeFAFSADate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 685px; top: 345px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeApplicationDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 690px; top: 370px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeHealthInsuranceDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 670px; top: 395px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegePittsburghPromiseDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 725px; top: 420px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 665px; top: 445px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeNCAAEligiblityCenter" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 715px; top: 470px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeScholarshipGrantLoanDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 730px; top: 495px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeVisitationDate" runat="server" BackColor="#FFD200" 
        Enabled="False" style="z-index: 1; left: 690px; top: 520px; position: absolute" 
        Visible="False"></asp:TextBox>



    <asp:TextBox ID="txbCollegeNCAAEligiblityDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 245px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeTourSchool" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 270px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeSATReadingScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 913px; top: 295px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeACTEnglishScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 320px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeSATMathScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 345px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeSATWritingScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 370px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeSATTotalScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 395px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeSATExamDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 420px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeACTReadingScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 445px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeACTMathScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 470px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeACTCompositeScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 495px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeACTScienceScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 900px; top: 520px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeACTExamDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 1106px; top: 246px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeACTTotalScore" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 1107px; top: 277px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>




    <asp:ImageButton ID="imbMinistryVolunteerOrganizationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 245px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Ministry VolunteerOrganization Date" Visible="False" 
        onclick="imbMinistryVolunteerOrganizationDate_Click" />
    <asp:ImageButton ID="imbMinistryMissionTripLocationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 270px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Ministry MissionTripLocation Date" Visible="False" 
        onclick="imbMinistryMissionTripLocationDate_Click" />
    <asp:ImageButton ID="imbMinistryChurchActivityDate" runat="server" 
        style="z-index: 1; left: 509px; top: 295px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Ministry ChurchActivity Date" Visible="False" 
        onclick="imbMinistryChurchActivityDate_Click" />
    <asp:ImageButton ID="imbMinistryBibleStudyDate" runat="server" 
        style="z-index: 1; left: 509px; top: 320px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Ministry BibleStudy Date" Visible="False" 
        onclick="imbMinistryBibleStudyDate_Click" />
    <asp:ImageButton ID="imbMinistryMentorDate" runat="server" 
        style="z-index: 1; left: 509px; top: 345px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Ministry Mentor Date" Visible="False" 
        onclick="imbMinistryMentorDate_Click" />
    <asp:ImageButton ID="imbMinistryInternshipOrganizationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 370px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Ministry Internship Organization Date" Visible="False" 
        onclick="imbMinistryInternshipOrganizationDate_Click" />

    <asp:CheckBox ID="chbMinistryVolunteerOrganizationDate" runat="server" 
        style="z-index: 1; left: 533px; top: 245px; position: absolute; width: 236px" 
        Text="Volunteer Organization Date" Visible="False" />
    <asp:CheckBox ID="chbMinistryMissionTripLocationDate" runat="server" 
        style="z-index: 1; left: 533px; top: 270px; position: absolute; width: 210px" 
        Text="Mission Trip Location Date" Visible="False" />
    <asp:CheckBox ID="chbMinistryChurchActivityDate" runat="server" 
        style="z-index: 1; left: 533px; top: 295px; position: absolute; width: 194px" 
        Text="Church Activity Date" Visible="False" />
    <asp:CheckBox ID="chbMinistryBibleStudy" runat="server" 
        style="z-index: 1; left: 533px; top: 320px; position: absolute; width: 156px" 
        Text="BibleStudy?"  Visible="False" />
    <asp:CheckBox ID="chbMinistryMentorDate" runat="server" 
        style="z-index: 1; left: 533px; top: 345px; position: absolute; width: 194px" 
        Text="Mentor Date" Visible="False" />
    <asp:CheckBox ID="chbMinistryInternshipOrganizationDate" runat="server" 
        style="z-index: 1; left: 533px; top: 370px; position: absolute; width: 210px" 
        Text="Internship Organization Date" Visible="False" />

    <asp:CheckBox ID="chbParticipation" runat="server" 
        style="z-index: 1; left: 533px; top: 395px; position: absolute; width: 152px" 
        Text="Participation" Visible="False"/>
    <asp:CheckBox ID="chbMinistryParticipation" runat="server" 
        style="z-index: 1; left: 533px; top: 420px; position: absolute; width: 194px" 
        Text="Ministry Participation" Visible="False" />

    <asp:TextBox ID="txbMinistryVolunteerOrganizationDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 745px; top: 245px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryMissionTripLocationDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 735px; top: 270px; position: absolute; width: 70px; bottom: 772px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryChurchActivityDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 695px; top: 295px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryBibleStudyDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 635px; top: 320px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryMentorDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 645px; top: 345px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryInternshipOrganizationDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 745px; top: 370px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryBibleStudy" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 715px; top: 459px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>

    <asp:TextBox ID="txbMinistryVolunteerOrganization" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 920px; top: 245px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryMissionTripLocation" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 920px; top: 270px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryChurchActivityDescription" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 920px; top: 295px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryMentorName" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 920px; top: 320px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryInternshipOrganization" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 920px; top: 345px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryParticipation" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 700px; top: 445px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMinistryChurchAffiliation" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 700px; top: 470px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>


    <asp:ImageButton ID="imbMilitaryASVABPreparationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 245px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military ASVABPreparation Date" Visible="False" 
        onclick="imbMilitaryASVABPreparationDate_Click" />
    <asp:ImageButton ID="imbMilitaryMedicalHistoryReviewDate" runat="server" 
        style="z-index: 1; left: 509px; top: 270px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military MedicalHistoryReview Date" Visible="False" 
        onclick="imbMilitaryMedicalHistoryReviewDate_Click" />
    <asp:ImageButton ID="imbMilitaryLegalHistoryReviewDate" runat="server" 
        style="z-index: 1; left: 509px; top: 295px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military LegalHistoryReview Date" Visible="False" 
        onclick="imbMilitaryLegalHistoryReviewDate_Click" />
    <asp:ImageButton ID="imbMilitaryRecruiterAppointmentDate" runat="server" 
        style="z-index: 1; left: 509px; top: 320px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military RecruiterAppointment Date" Visible="False" 
        onclick="imbMilitaryRecruiterAppointmentDate_Click" />
    <asp:ImageButton ID="imbMilitaryMEPSAppointmentAcceptance" runat="server" 
        style="z-index: 1; left: 509px; top: 345px; position: absolute; height: 19px; width: 20px; bottom: 282px;" 
        ToolTip="Set Military MEPSAppointmentAcceptance Date" Visible="False" 
        onclick="imbMilitaryMEPSAppointmentAcceptance_Click" />
    <asp:ImageButton ID="imbMilitaryCareerAssigned" runat="server" 
        style="z-index: 1; left: 509px; top: 370px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military CareerAssigned Date" Visible="False" 
        onclick="imbMilitaryCareerAssigned_Click" />
    <asp:ImageButton ID="imbMilitaryEnlistmentService" runat="server" 
        style="z-index: 1; left: 509px; top: 395px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military EnlistmentService Date" Visible="False" 
        onclick="imbMilitaryEnlistmentServiceDate_Click" />
    <asp:ImageButton ID="imbMilitaryEnlistmentDate" runat="server" 
        style="z-index: 1; left: 509px; top: 420px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military Enlistment Date" Visible="False" 
        onclick="imbMilitaryEnlistmentDate_Click" />
    <asp:ImageButton ID="imbMilitaryBasicTrainingDeparture" runat="server" 
        style="z-index: 1; left: 509px; top: 445px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military BasicTrainingDeparture" Visible="False" 
        onclick="imbMilitaryBasicTrainingDeparture_Click" />
    <asp:ImageButton ID="imbMilitaryMEPSPreparationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 470px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military MEPSPreparation Date" Visible="False" 
        onclick="imbMilitaryMEPSPreparationDate_Click" />
    <asp:ImageButton ID="imbMilitaryCitizenshipDate" runat="server" 
        style="z-index: 1; left: 509px; top: 495px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Military Citizenship Date" Visible="False" 
        onclick="imbMilitaryCitizenshipDate_Click" />


    <asp:CheckBox ID="chbMilitaryASVABPreparation" runat="server" 
        style="z-index: 1; left: 533px; top: 245px; position: absolute; width: 194px" 
        Text="ASVAB Preparation" Visible="False" />
    <asp:CheckBox ID="chbMilitaryMedicalHistoryReview" runat="server" 
        style="z-index: 1; left: 533px; top: 270px; position: absolute; width: 238px" 
        Text="Medical History Review" Visible="False" />
    <asp:CheckBox ID="chbMilitaryLegalHistoryReview" runat="server" 
        style="z-index: 1; left: 533px; top: 295px; position: absolute; width: 194px" 
        Text="History Review" Visible="False" />
    <asp:CheckBox ID="chbMilitaryRecruiterAppointmentDate" runat="server" 
        style="z-index: 1; left: 533px; top: 320px; position: absolute; width: 210px" 
        Text="Recruiter Appointment Date" Visible="False" />
    <asp:CheckBox ID="chbMilitaryMEPSAppointment" runat="server" 
        style="z-index: 1; left: 533px; top: 345px; position: absolute; width: 194px" 
        Text="MEPS Appointment" Visible="False" />
    <asp:Label ID="lblMilitaryMEPSAppointmentBranch" runat="server" Font-Size="13pt" 
        style="z-index: 1; left: 795px; top: 348px; position: absolute; width: 208px" 
        Text="MEPS Appointment Branch" Visible="False"></asp:Label>
    <asp:CheckBox ID="chbMilitaryCareerAssignedDate" runat="server" 
        style="z-index: 1; left: 533px; top: 370px; position: absolute; width: 194px" 
        Text="Career Assigned Date" Visible="False" />
    <asp:CheckBox ID="chbMilitaryEnlistmentServiceDate" runat="server" 
        style="z-index: 1; left: 533px; top: 395px; position: absolute; width: 194px" 
        Text="Enlistment Service Date" Visible="False" />
    <asp:CheckBox ID="chbMilitaryEnlistmentDate" runat="server" 
        style="z-index: 1; left: 533px; top: 420px; position: absolute; width: 194px" 
        Text="Enlistment Date" Visible="False" />
    <asp:CheckBox ID="chbMilitaryBasicTrainingDepartureDate" runat="server" 
        style="z-index: 1; left: 533px; top: 445px; position: absolute; width: 229px" 
        Text="Basic TrainingDeparture Date" Visible="False" />
    <asp:CheckBox ID="chbMilitaryMEPSPreparation" runat="server" 
        style="z-index: 1; left: 533px; top: 470px; position: absolute; width: 208px" 
        Text="MEPS Preparation" Visible="False" />
    <asp:CheckBox ID="chbMilitaryCitizenship" runat="server" 
        style="z-index: 1; left: 533px; top: 495px; position: absolute; width: 194px" 
        Text="Citizenship" Visible="False" />


    <asp:TextBox ID="txbMilitaryASVABPreparationDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 690px; top: 245px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryMedicalHistoryReviewDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 715px; top: 270px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryLegalHistoryReviewDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 660px; top: 295px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryRecruiterAppointmentDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 740px; top: 320px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryMEPSAppointmentAcceptance" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 690px; top: 345px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryMEPSAppointmentBranch" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 1000px; top: 345px; position: absolute; width: 200px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryCareerAssigned" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 700px; top: 370px; position: absolute; width: 82px; bottom: 319px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryEnlistmentService" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 675px; top: 395px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryEnlistmentDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 665px; top: 420px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryBasicTrainingDeparture" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 715px; top: 445px; position: absolute; width: 82px; bottom: 413px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryMEPSPreparationDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 680px; top: 470px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbMilitaryCitizenshipDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 630px; top: 495px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>


    <asp:ImageButton ID="imbCareerHSActivitiesListDate" runat="server" 
        style="z-index: 1; left: 509px; top: 270px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Career HSActivitiesList Date" Visible="False" 
        onclick="imbCareerHSActivitiesListDate_Click" />
    <asp:ImageButton ID="imbCareerHSHonorsListDate" runat="server" 
        style="z-index: 1; left: 509px; top: 295px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Career HSHonorsList Date" Visible="False" 
        onclick="imbCareerHSHonorsListDate_Click" />
    <asp:ImageButton ID="imbCareerHSLeadershipListDate" runat="server" 
        style="z-index: 1; left: 509px; top: 320px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Career HSLeadershipList Date" Visible="False" 
        onclick="imbCareerHSLeadershipListDate_Click" />
    <asp:ImageButton ID="imbCareerCommunityActivitiesListDate" runat="server" 
        style="z-index: 1; left: 509px; top: 345px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Career CommunityActivitiesList Date" Visible="False" 
        onclick="imbCareerCommunityActivitiesListDate_Click" />
    <asp:ImageButton ID="imbCareerResumeCompletedDate" runat="server" 
        style="z-index: 1; left: 509px; top: 370px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Career ResumeCompleted Date" Visible="False" 
        onclick="imbCareerResumeCompletedDate_Click" />
    <asp:ImageButton ID="imbCareerApplicationSubmittedDate" runat="server" 
        style="z-index: 1; left: 509px; top: 395px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Career ApplicationSubmitted Date" Visible="False" 
        onclick="imbCareerApplicationSubmittedDate_Click" />
    <asp:ImageButton ID="imbCareerInterviewPreparationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 420px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Career InterviewPreparation Date" Visible="False" 
        onclick="imbCareerInterviewPreparationDate_Click" />
    <asp:ImageButton ID="imbCareerInterviewScheduledDate" runat="server" 
        style="z-index: 1; left: 509px; top: 445px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Career InterviewScheduled Date" Visible="False" 
        onclick="imbCareerInterviewScheduledDate_Click" />

    <asp:CheckBox ID="chbCareerHealthInsurance" runat="server" 
        style="z-index: 1; left: 533px; top: 245px; position: absolute; width: 194px" 
        Text="Health Insurance" Visible="False" />
    <asp:CheckBox ID="chbCareerHSActivitiesList" runat="server" 
        style="z-index: 1; left: 533px; top: 270px; position: absolute; width: 194px" 
        Text="HS Activities List" Visible="False" />
    <asp:CheckBox ID="chbCareerHonorsList" runat="server" 
        style="z-index: 1; left: 533px; top: 295px; position: absolute; width: 194px" 
        Text="HS Honors List" Visible="False" />
    <asp:CheckBox ID="chbCareerHSLeadershipList" runat="server" 
        style="z-index: 1; left: 533px; top: 320px; position: absolute; width: 194px" 
        Text="HS Leadership List" Visible="False" />
    <asp:CheckBox ID="chbCareerCommunityActivitiesList" runat="server" 
        style="z-index: 1; left: 533px; top: 345px; position: absolute; width: 230px" 
        Text="Community Activities List" Visible="False" />
    <asp:CheckBox ID="chbCareerResumeCompleted" runat="server" 
        style="z-index: 1; left: 533px; top: 370px; position: absolute; width: 194px" 
        Text="ResumeCompleted" Visible="False" />
    <asp:CheckBox ID="chbCareerApplicationSubmittedDate" runat="server" 
        style="z-index: 1; left: 533px; top: 395px; position: absolute; width: 236px" 
        Text="Application Submitted Date" Visible="False" />
    <asp:CheckBox ID="chbCareerInterviewPreparation" runat="server" 
        style="z-index: 1; left: 533px; top: 420px; position: absolute; width: 236px" 
        Text="InterviewPreparation" Visible="False" />
    <asp:CheckBox ID="chbCareerInterviewScheduleAccepted" runat="server" 
        style="z-index: 1; left: 533px; top: 445px; position: absolute; width: 255px" 
        Text="InterviewScheduleAccepted" Visible="False" />
    <asp:CheckBox ID="chbCareerParticipation" runat="server" 
        style="z-index: 1; left: 533px; top: 470px; position: absolute; width: 194px" 
        Text="Participation" Visible="False" />
    <asp:CheckBox ID="chbCareerBibleStudy" runat="server" 
        style="z-index: 1; left: 533px; top: 495px; position: absolute; width: 194px" 
        Text="BibleStudy" Visible="False" />


    <asp:TextBox ID="txbCareerHSActivitiesListDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 670px; top: 270px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCareerHSHonorsListDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 660px; top: 295px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCareerHSLeadershipListDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 680px; top: 320px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCareerCommunityActivitiesListDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 730px; top: 345px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCareerResumeCompletedDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 680px; top: 370px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCareerApplicationSubmittedDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 740px; top: 395px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCareerInterviewPreparationDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 690px; top: 420px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCareerInterviewScheduledDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 740px; top: 445px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCareerBibleStudyDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 700px; top: 470px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>



    <asp:ImageButton ID="imbTradeWatchTradeVideo" runat="server" 
        style="z-index: 1; left: 509px; top: 245px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Trade WatchTradeVideo Date" Visible="False" 
        onclick="imbTradeWatchTradeVideo_Click" />
    <asp:ImageButton ID="imbTradeDrugTestDate" runat="server" 
        style="z-index: 1; left: 509px; top: 270px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Trade DrugTest Date" Visible="False" 
        onclick="imbTradeDrugTestDate_Click" />
    <asp:ImageButton ID="imbTradeCollegeApplicationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 295px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Trade CollegeApplication Date" Visible="False" 
        onclick="imbTradeCollegeApplicationDate_Click" />
    <asp:ImageButton ID="imbTradeCollegeDeadlineDate" runat="server" 
        style="z-index: 1; left: 509px; top: 320px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Trade CollegeDeadline Date" Visible="False" 
        onclick="imbTradeCollegeDeadlineDate_Click" />
    <asp:ImageButton ID="imbTradeCollegeVisitationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 345px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Trade CollegeVisitation Date" Visible="False" 
        onclick="imbTradeCollegeVisitationDate_Click" />
    <asp:ImageButton ID="imbTradeApplicationDate" runat="server" 
        style="z-index: 1; left: 509px; top: 370px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Trade Application Date" Visible="False" 
        onclick="imbTradeApplicationDate_Click" />
    <asp:ImageButton ID="imbTradePittsburghPromiseDate" runat="server" 
        style="z-index: 1; left: 509px; top: 370px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Trade PittsburghPromise Date" Visible="False" 
        onclick="imbTradePittsburghPromiseDate_Click" />
    <asp:ImageButton ID="imbTradeFAFSACompletedDate" runat="server" 
        style="z-index: 1; left: 509px; top: 370px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set Trade FAFSACompleted Date" Visible="False" 
        onclick="imbTradeFAFSACompletedDate_Click" />


    <asp:CheckBox ID="chbTradeDrugTest" runat="server" 
        style="z-index: 1; left: 534px; top: 245px; position: absolute; width: 178px" 
        Text="Trade Drug Test?" Enabled="False" Visible="False" />
    <asp:CheckBox ID="chbTradeHealthInsur" runat="server" 
        style="z-index: 1; left: 743px; top: 270px; position: absolute; width: 188px" 
        Text="Health Insurance?" Visible="False" />
    <asp:CheckBox ID="chbTradePghProm" runat="server" 
        style="z-index: 1; left: 535px; top: 295px; position: absolute; width: 236px" 
        Text="Pittsburgh Promise Eligible?" Visible="False" />
    <asp:CheckBox ID="chbTradeCollegeTour" runat="server" 
        style="z-index: 1; left: 534px; top: 320px; position: absolute; width: 177px" 
        Text="Trade College Tour?" Enabled="False" Visible="False" />
    <asp:CheckBox ID="chbTradeFAFSACompleted" runat="server" 
        style="z-index: 1; left: 533px; top: 345px; position: absolute; width: 218px; bottom: 312px;" 
        Text="Trade FAFSA Completed" Visible="False" />
    <asp:CheckBox ID="chbWtchTrdVideo" runat="server" 
        style="z-index: 1; left: 534px; top: 370px; position: absolute; width: 199px" 
        Text="Watch Trade Video?" Visible="False" />
    <asp:CheckBox ID="chbFAFSACompleted" runat="server" 
        style="z-index: 1; left: 536px; top: 395px; position: absolute; width: 178px" 
        Text="FAFSA Completed?" Visible="False" />



    <asp:CheckBox ID="chbDriversLicense" runat="server" 
        style="z-index: 1; left: 34px; top: 439px; position: absolute; width: 163px" 
        Text="Drivers License" Visible="False" />
    <asp:Button ID="cmbAnotherCollegeTour" runat="server" 
        onclick="cmbAnotherCollegeTour_Click" 
        style="z-index: 1; left: 1089px; top: 462px; position: absolute; width: 187px; height: 28px" 
        Text="Another College Tour" Visible="False" />
    <asp:Button ID="cmbAnotherCollegeApplication" runat="server" 
        onclick="cmbAnotherCollegeApplication_Click" 
        style="z-index: 1; left: 1086px; top: 495px; position: absolute; width: 194px; height: 27px" 
        Text="Another College Application" Visible="False" />
    <asp:DropDownList ID="ddlScholarshipType1" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1102px; top: 403px; position: absolute; width: 130px" 
        Visible="False">
    </asp:DropDownList>
    <asp:CheckBox ID="chbGameFilm" runat="server" 
        style="z-index: 1; left: 533px; top: 359px; position: absolute; width: 181px" 
        Text="Game Film" Visible="False" />
    <asp:CheckBox ID="chbAuditionPortfolio" runat="server" 
        style="z-index: 1; left: 533px; top: 455px; position: absolute; width: 194px" 
        Visible="False" />
    <asp:CheckBox ID="chbDrugTest" runat="server" 
        style="z-index: 1; left: 533px; top: 230px; position: absolute; width: 190px" 
        Text="Drug Test  (Pass/Fail)" Visible="False" />
    <asp:CheckBox ID="chbWatchTradeVideo" runat="server" 
        style="z-index: 1; left: 533px; top: 263px; position: absolute; width: 193px" 
        Text="Watch Trade Video" Visible="False" />
    <asp:Label ID="lblBusOption" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 251px; top: 230px; position: absolute; width: 168px" 
        Text="Bus Option" Visible="False"></asp:Label>
    <asp:Label ID="lblGeneralInformation" runat="server" 
        style="z-index: 1; left: 37px; top: 414px; position: absolute; width: 181px; font-weight: 700; text-decoration: underline" 
        Text="General Information" Visible="False"></asp:Label>
    <asp:Label ID="lblBusInformation" runat="server" 
        style="z-index: 1; left: 528px; top: 221px; position: absolute; width: 193px; font-weight: 700; text-decoration: underline" 
        Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        
        
        
        
        
        style="z-index: 1; left: 294px; top: 474px; position: absolute; width: 162px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Panel ID="pnlBuses" runat="server" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="2px" Visible="False">
    </asp:Panel>
    <asp:Panel ID="pnlGeneralItems" runat="server" BorderColor="Black" 
        BorderStyle="Solid" BorderWidth="3px" Visible="False">
        <asp:CheckBox ID="chbPaid" runat="server" 
    style="z-index: 1; left: -331px; top: 301px; position: absolute" Text="Paid?" 
            Visible="False" />
    </asp:Panel>
    <asp:CheckBox ID="chbCitizenship" runat="server" 
        style="z-index: 1; left: 129px; top: 670px; position: absolute; width: 139px" 
        Text="Citizenship" Visible="False" />
    <asp:CheckBox ID="chbASVABPreparation" runat="server" 
        style="z-index: 1; left: 129px; top: 691px; position: absolute; width: 191px" 
        Text="ASVAB Preparation" Visible="False" />
    <asp:CheckBox ID="chbMedicalHistoryReview" runat="server" 
        style="z-index: 1; left: 129px; top: 715px; position: absolute; width: 210px" 
        Text="Medical History Review" Visible="False" />
    <asp:CheckBox ID="chbLegalHistoryReview" runat="server" 
        style="z-index: 1; left: 128px; top: 736px; position: absolute; width: 206px" 
        Text="Legal History Review" Visible="False" />
    <asp:CheckBox ID="chbMEPSPreparation" runat="server" 
        style="z-index: 1; left: 533px; top: 270px; position: absolute; width: 178px" 
        Text="MEPS Preparation" Visible="False" />
    <asp:CheckBox ID="chbMEPSAppointment" runat="server" 
        style="z-index: 1; left: 533px; top: 245px; position: absolute; height: 33px; width: 189px" 
        Text="MEPS Appointment" Visible="False" />
    <asp:Label ID="lblMilitary" runat="server" 
        style="z-index: 1; left: 137px; top: 644px; position: absolute; width: 165px; font-weight: 700; text-decoration: underline;" 
        Text="Military" Visible="False"></asp:Label>


    <asp:TextBox ID="txbWatchVideoDate" runat="server" BackColor="#FFD200" 
        Enabled="True" 
        style="z-index: 1; left: 688px; top: 261px; position: absolute; width: 58px" 
        Visible="False" ReadOnly="True"></asp:TextBox>



    <asp:TextBox ID="txbDrugTestDate" runat="server" BackColor="#FFD200" 
        Enabled="True" 
        style="z-index: 1; left: 676px; top: 245px; position: absolute; width: 69px" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollegeAppDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 1109px; top: 306px; position: absolute; width: 70px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbCollgDeadlineDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 1108px; top: 345px; position: absolute" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbTradeApplicationDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 700px; top: 391px; position: absolute" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbSchlrshipLoanDate" runat="server" BackColor="#FFD200" 
        Enabled="True" 
        style="z-index: 1; left: 691px; top: 501px; position: absolute; height: 22px; width: 81px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="txbPittsbghPromiseDate" runat="server" BackColor="#FFD200" 
        Enabled="True" style="z-index: 1; left: 700px; top: 412px; position: absolute; width: 75px;" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:ImageButton ID="imbCalDrugTestDate" runat="server" 
        onclick="imbCalDrugTestDate_Click" 
        style="z-index: 1; left: 509px; top: 245px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set DrugTest date" Visible="False" />
    <asp:ImageButton ID="imbCalCollegeAppDate" runat="server" 
        onclick="imbCalCollegeAppDate_Click" 
        style="z-index: 1; left: 509px; top: 448px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set CollegeApp date" Visible="False" />
    <asp:CheckBox ID="chbPghPromiseEligible" runat="server" 
        style="z-index: 1; left: 533px; top: 495px; position: absolute; width: 222px" 
        Text="Pgh Promise Eligible" Visible="False" />
    <asp:CheckBox ID="chbSAT" runat="server" 
        style="z-index: 1; left: 688px; top: 445px; position: absolute; width: 187px;" 
        Text="SAT Exam" Visible="False" />
    <asp:DropDownList ID="ddlCollegeAcceptStatus" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 1102px; top: 432px; position: absolute; width: 150px" 
        Visible="False">
    </asp:DropDownList>
    <asp:TextBox ID="txbTradeAccepted" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 763px; top: 313px; position: absolute" 
        Visible="False"></asp:TextBox>
    <asp:TextBox ID="txbSchlrshipLoanAmount" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 681px; top: 463px; position: absolute" 
        Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:Calendar ID="calDrugTestDate" runat="server" 
        onselectionchanged="calDrugTestDate_SelectionChanged" 
        style="z-index: 1; left: 570px; top: 946px; position: absolute; height: 188px; width: 259px" 
        Visible="False" BackColor="#FFD200"></asp:Calendar>
    <asp:CheckBox ID="chbSocSecurityCard" runat="server" 
        style="z-index: 1; left: 158px; top: 496px; position: absolute; width: 159px" 
        Text="SocSecurity Card" Visible="False" />
    <asp:CheckBox ID="chbInterviewed" runat="server" 
        style="z-index: 1; left: 34px; top: 496px; position: absolute; width: 128px" 
        Text="Interviewed?" Visible="False" />
    <p>
    </p>
    <p>
    </p>
    <asp:Calendar ID="calGeneralCalender" runat="server" 
        onselectionchanged="calGeneralCalender_SelectionChanged" 
        style="z-index: 1; left: 768px; top: 292px; position: absolute; height: 273px; width: 447px" 
        Visible="False" BackColor="#FFD200"></asp:Calendar>
    <asp:TextBox ID="txbIdentifier" runat="server" 
        style="z-index: 1; left: 866px; top: 154px; position: absolute" 
        Visible="False"></asp:TextBox>
    <p>
    <asp:ImageButton ID="imbCalTradeVideoDate" runat="server" 
        style="z-index: 1; left: 509px; top: 518px; position: absolute; height: 19px; width: 20px" 
        ToolTip="Set TradeVideo date" Visible="False" 
        onclick="imbCalTradeVideoDate_Click" />
    </p>
    <asp:Label ID="lblReports" runat="server" Font-Bold="False" 
        style="z-index: 1; left: 308px; top: 497px; position: absolute; width: 142px" 
        Text="Option Reports" Visible="False"></asp:Label>
    </form>
</body>
</html>
