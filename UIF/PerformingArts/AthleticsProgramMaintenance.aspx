<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AthleticsProgramMaintenance.aspx.cs" Inherits="UIF.PerformingArts.AthleticsProgramMaintenance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style8
        {
            position: absolute;
            top: 219px;
            left: 464px;
            z-index: 1;
            width: 204px;
        }
        .style12
        {
            position: absolute;
            top: 142px;
            left: 426px;
            z-index: 1;
            width: 426px;
            text-decoration: underline;
        }
        .style15
        {
            position: absolute;
            top: 311px;
            left: 696px;
            z-index: 1;
            width: 284px;
            height: 37px;
            bottom: 163px;
        }
        .style16
        {
            position: absolute;
            top: 354px;
            left: 699px;
            z-index: 1;
            height: 19px;
            width: 122px;
        }
        .style17
        {
            position: absolute;
            top: 364px;
            left: 963px;
            z-index: 1;
            right: 330px;
        }
        </style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="#ff9900">
    <form id="form2" runat="server">
    <div>
    
    </div>
    <p>
        <asp:Label ID="Label3" runat="server" CssClass="style12" Font-Size="XX-Large" 
            Text="Athletics Student Maintenance"></asp:Label>
    </p>
    <p>
        &nbsp;</p>

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
        style="z-index: 1; left: 214px; top: 98px; position: absolute; height: 37px; width: 947px" 
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



    <asp:Label ID="lblLastUpdatedBy" runat="server" Enabled="False" 
        style="z-index: 1; left: 975px; top: 22px; position: absolute; height: 76px; width: 173px" 
        Text="LastUpdatedBy: "></asp:Label>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblName" runat="server" CssClass="style8" Text="Label" 
            Font-Size="Large" BackColor="#FFD200" BorderColor="#333333" 
            BorderStyle="Solid" BorderWidth="2px" Visible="False"></asp:Label>
        <asp:DropDownList ID="ddlProgram" runat="server" BackColor="#FFD200" 
            onselectedindexchanged="ddlProgram_SelectedIndexChanged" 
            
            style="z-index: 1; left: 36px; top: 223px; position: absolute; width: 193px; " 
            AutoPostBack="True" CausesValidation="True">
        </asp:DropDownList>
        <asp:Label ID="lblPrograms" runat="server" 
            style="z-index: 1; left: 39px; top: 244px; position: absolute; height: 20px; width: 167px" 
            Text="Programs" Font-Size="10pt"></asp:Label>
        <asp:Button ID="cmbRetreiveProgram" runat="server" 
            onclick="cmbRetreiveProgram_Click" 
            style="z-index: 1; left: 49px; top: 153px; position: absolute; height: 24px; width: 131px" 
            Text="Retrieve Program" Visible="False" />
        <asp:LinkButton ID="lbAttendanceHistory" runat="server" 
            onclick="lbAttendanceHistory_Click" 
            
            style="z-index: 1; left: 1085px; top: 247px; position: absolute; width: 209px">Student Attendance History</asp:LinkButton>
    </p>
    <p>
        <asp:TextBox ID="txbComments" runat="server" CssClass="style15" 
            BackColor="#FFD200" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txbCoachTeam" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 696px; top: 219px; position: absolute; width: 284px; height: 19px;" 
            Visible="False"></asp:TextBox>
        <asp:Label ID="lblTeamColor" runat="server" Font-Size="10pt" 
            style="z-index: 1; left: 699px; top: 288px; position: absolute; width: 224px" 
            Text="Assign Team Name" Visible="False"></asp:Label>
        <asp:LinkButton ID="lbSectionMaintenance" runat="server" 
            onclick="lbSectionMaintenance_Click" 
            style="z-index: 1; left: 252px; top: 251px; position: absolute">(Go to Section Maintenance)</asp:LinkButton>
    </p>
    <p>
        <asp:CheckBox ID="chbPaid" runat="server" 
            style="z-index: 1; left: 461px; top: 316px; position: absolute; width: 68px" 
            Text="Paid" Visible="False" />
        <asp:DropDownList ID="ddlTeamSectionUpdate" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 696px; top: 266px; position: absolute; width: 188px" 
            Visible="False" 
            onselectedindexchanged="ddlTeamSectionUpdate_SelectedIndexChanged">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Label ID="lblComments" runat="server" CssClass="style16" Font-Size="10pt" 
            Text="Athletics Comments" Visible="False"></asp:Label>
    </p>
    <p>
        <asp:CheckBox ID="chbContract" runat="server" 
            style="z-index: 1; left: 461px; top: 281px; position: absolute; right: 318px" 
            Text="Contract" Visible="False" />
        <asp:CheckBox ID="chbGotPicture" runat="server" 
            style="z-index: 1; left: 461px; top: 298px; position: absolute; width: 107px" 
            Text="Got Picture?" Visible="False" />
        <asp:Label ID="lblStudentNames" runat="server" Font-Size="10pt" 
            style="z-index: 1; left: 248px; top: 346px; position: absolute; width: 116px" 
            Text="Students" Visible="False"></asp:Label>
    </p>
    <asp:Button ID="cmbCallingListReport" runat="server" 
        onclick="cmbCallingListReport_Click" 
        style="z-index: 1; left: 1101px; top: 275px; position: absolute; height: 35px; width: 138px" 
        Text="Calling List Report" Enabled="False" />
    <p>
        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="style17" Font-Size="8pt" 
            Text="(Edit Comments)" Visible="False" />
        <asp:Button ID="cmbReport" runat="server" onclick="cmbReport_Click" 
            style="z-index: 1; left: 1102px; top: 204px; position: absolute; height: 35px; width: 138px; bottom: 274px;" 
            Text="Attendance Report" Enabled="False" />
        <asp:DropDownList ID="ddlProgramSection" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlProgramSection_SelectedIndexChanged" 
            style="z-index: 1; left: 36px; top: 270px; position: absolute; width: 193px" 
            Visible="False">
        </asp:DropDownList>
        <asp:Label ID="lblProgramSections" runat="server" 
            style="z-index: 1; left: 38px; top: 296px; position: absolute; height: 19px; width: 144px" 
            Text="Program Sections" Visible="False" Font-Size="10pt"></asp:Label>
        <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
            style="z-index: 1; left: 1101px; top: 165px; position: absolute; height: 33px; width: 138px" 
            Text="Reset Page" />
        <asp:Button ID="cmbUpdate" runat="server" onclick="cmbUpdate_Click" 
            style="z-index: 1; left: 500px; top: 326px; position: absolute; height: 52px; width: 138px" 
            Text="Save/Update" Visible="False" />
        <asp:Label ID="lblCoachTeam" runat="server" Font-Size="10pt" 
            style="z-index: 1; left: 699px; top: 244px; position: absolute; width: 173px" 
            Text="Volunteer/Contact" Visible="False"></asp:Label>
        <asp:LinkButton ID="lbStudentProfileLink" runat="server" Font-Size="10pt" 
            onclick="lbStudentProfileLink_Click" 
            style="z-index: 1; left: 313px; top: 346px; position: absolute; width: 108px" 
            Visible="False">(Student Profile)</asp:LinkButton>
    </p>
    <p>
        <asp:Image ID="imgStudent" runat="server" ImageUrl="~/1.jpg" 
            style="z-index: 1; left: 244px; top: 364px; position: absolute; width: 194px; height: 136px" 
            Visible="False" BorderColor="Black" BorderStyle="Solid" 
            BorderWidth="1px" />
        <asp:GridView ID="gvReport" runat="server" BackColor="#FFD200"
            
            
            
            style="z-index: 1; left: 33px; top: 364px; position: absolute; height: 133px; width: 390px">
            <AlternatingRowStyle BackColor="Silver" />
            <HeaderStyle BackColor="Black" ForeColor="White" />
        </asp:GridView>
        <asp:CheckBox ID="chbRegistrationForm" runat="server" 
            style="z-index: 1; left: 461px; top: 245px; position: absolute; right: 209px" 
            Text="Current Registration Form" Visible="False" AutoPostBack="True" 
            oncheckedchanged="chbRegistrationForm_CheckedChanged" />
        <asp:CheckBox ID="chbParentalConsentForm" runat="server" 
            style="z-index: 1; left: 461px; top: 263px; position: absolute; width: 258px; bottom: 247px;" 
            Text="Academic/Parental Consent Form" Visible="False" />
        <asp:Label ID="lblName2" runat="server" 
            style="z-index: 1; left: 73px; top: 530px; position: absolute; height: 22px; width: 201px" 
            Text="Label" Visible="False"></asp:Label>
        <asp:Button ID="cmbExcelExport" runat="server" Enabled="False" 
            onclick="cmbExcelExport_Click" 
            style="z-index: 1; left: 1101px; top: 315px; position: absolute; height: 36px; width: 138px" 
            Text="Excel Export" />
        <asp:Label ID="lblTeamColors" runat="server" Font-Size="10pt" 
            style="z-index: 1; left: 39px; top: 345px; position: absolute; width: 146px" 
            Text="Team Name" Visible="False"></asp:Label>
    </p>
    <asp:Button ID="cmbStudentPage" runat="server" onclick="cmbStudentPage_Click" 
        style="z-index: 1; left: 1116px; top: 522px; position: absolute; height: 35px; width: 112px" 
        Text="Student Page" Enabled="False" Visible="False" />
    <p>
        <asp:DropDownList ID="ddlStudents" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlStudents_SelectedIndexChanged" 
            style="z-index: 1; left: 242px; top: 323px; position: absolute; width: 211px" 
            Visible="False">
        </asp:DropDownList>
    </p>
    <p>
        <asp:TextBox ID="txbSPComments" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 696px; top: 375px; position: absolute; height: 61px; width: 284px" 
            Visible="False"></asp:TextBox>
        <asp:DropDownList ID="ddlTeamSections" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            style="z-index: 1; left: 35px; top: 323px; position: absolute; width: 193px" 
            Visible="False" 
            onselectedindexchanged="ddlTeamSections_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="cmbSectionMaintenance" runat="server" 
            onclick="cmbSectionMaintenance_Click" 
            style="z-index: 1; left: 1096px; top: 383px; position: absolute; height: 42px; width: 140px" 
            Text="Section Maintenance" Visible="False" />
    </p>
    <asp:Button ID="cmbTESTRYAN" runat="server" onclick="cmbTESTRYAN_Click" 
        style="z-index: 1; left: 971px; top: 479px; position: absolute; height: 30px; width: 108px" 
        Text="TEST" Visible="False" />
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblSPNotes" runat="server" Font-Size="10pt" 
            style="z-index: 1; left: 699px; position: absolute; height: 20px; width: 195px; top: 442px" 
            Text="Student Profile Notes" Visible="False"></asp:Label>
        <asp:GridView ID="gvAttendanceList" runat="server" BackColor="#FFD200" 
            
            style="z-index: 1; left: 34px; top: 365px; position: absolute; height: 133px; width: 187px">
            <AlternatingRowStyle BackColor="Gray" />
            <HeaderStyle BackColor="Black" ForeColor="White" />
        </asp:GridView>
        <asp:Button ID="cmbRetreiveClassList" runat="server" 
            onclick="cmbRetreiveClassList_Click" 
            style="z-index: 1; left: 1067px; top: 427px; position: absolute; height: 40px; width: 168px" 
            Text="Retrieve TeamColor Roster" Visible="False" />
    </p>
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
