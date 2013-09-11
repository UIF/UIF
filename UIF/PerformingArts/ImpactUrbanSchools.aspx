<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpactUrbanSchools.aspx.cs" Inherits="UIF.PerformingArts.ImpactUrbanSchools" %>

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



    <asp:Label ID="lblEnrolledStudents" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 38px; top: 231px; position: absolute; width: 168px" 
        Text="Enrolled Students"></asp:Label>
    <asp:Label ID="lblDiscipleshipmentor" runat="server" Font-Size="25pt" 
        style="z-index: 1; top: 150px; position: absolute; height: 42px; width: 444px; text-decoration: underline; right: 373px; font-weight: 700;" 
        Text="Impact Urban Schools" Font-Bold="False"></asp:Label>
    <asp:Button ID="cmbStudentPage2" runat="server" Enabled="False" 
        onclick="cmbStudentPage2_Click" 
        style="z-index: 1; left: 1040px; top: 195px; position: absolute; height: 27px; width: 136px" 
        Text="Student Page" />
    <asp:CheckBox ID="chbPromise" runat="server" 
        style="z-index: 1; left: 869px; top: 314px; position: absolute" Text="Promise" 
        Visible="False" />
    <p>
        <asp:CheckBox ID="chbACT" runat="server" 
            style="z-index: 1; left: 521px; top: 294px; position: absolute" 
            Text="ACT Exam" Visible="False" />
        <asp:TextBox ID="txbPostGraduatePlans" runat="server" BackColor="#FFD200" 
            
            style="z-index: 1; left: 760px; top: 250px; position: absolute; width: 194px; right: 232px; height: 17px" 
            Visible="False"></asp:TextBox>
        <asp:Button ID="cmbComprehensiveReport" runat="server" 
            onclick="cmbComprehensiveReport_Click" 
            style="z-index: 1; left: 1040px; top: 228px; position: absolute; width: 136px;" 
            Text="View All" />
    </p>
    <asp:TextBox ID="txbNotes" runat="server" BackColor="#FFD200" 
        BorderStyle="Solid" 
        style="z-index: 1; left: 522px; top: 342px; position: absolute; height: 103px; width: 600px" 
        TextMode="MultiLine" ontextchanged="txbNotes_TextChanged" Visible="False"></asp:TextBox>
    <asp:Label ID="lblNotes" runat="server" 
        style="z-index: 1; left: 40px; top: 444px; position: absolute; width: 101px; " 
        Text="Activity History" Font-Size="10pt" Visible="False"></asp:Label>
    <asp:Button ID="cmbUpdate" runat="server" onclick="cmbUpdate_Click" 
        style="z-index: 1; left: 249px; top: 428px; position: absolute; width:214px"  
        Text="Update Student Information" Enabled="False" Visible="False" />
    <asp:Image ID="Image1" runat="server" 
        style="z-index: 1; left: 39px; top: 268px; position: absolute; height: 136px; width: 188px" 
        Visible="False" />
    <asp:TextBox ID="txbDiscipleshipMentor" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 523px; top: 204px; position: absolute; width: 209px" 
        ontextchanged="txbDiscipleshipMentor_TextChanged" Visible="False"></asp:TextBox>
    <asp:Label ID="lblDiscipleMentor" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 524px; top: 229px; position: absolute; width: 67px" 
        Text="DiscipleshipMentor" Visible="False"></asp:Label>
    <asp:TextBox ID="txbStaffCoordinator" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 523px; top: 249px; position: absolute; width: 209px" 
        Visible="False"></asp:TextBox>
    <asp:Label ID="lblStaffCoordinator" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 525px; top: 273px; position: absolute; width: 120px" 
        Text="Staff Coordinator" Visible="False"></asp:Label>
    <asp:CheckBox ID="chbHasGraduated" runat="server" 
        oncheckedchanged="chbHasGraduated_CheckedChanged" 
        style="z-index: 1; left: 621px; top: 294px; position: absolute; width: 147px" 
        Text="Has Graduated?" Visible="False" BorderColor="#FFD200" />
    <asp:TextBox ID="txbProgramEnrollment" runat="server" BackColor="#FFD200" 
        ontextchanged="txbProgramEnrollment_TextChanged" 
        
        style="z-index: 1; left: 761px; top: 205px; position: absolute; width: 193px" 
        Visible="False"></asp:TextBox>
    <asp:Label ID="lblProgramEnrollment" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 763px; top: 228px; position: absolute" 
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
            
            
            style="z-index: 1; left: 35px; top: 207px; position: absolute; width: 193px" 
            AutoPostBack="True" 
            onselectedindexchanged="ddlOptions_SelectedIndexChanged1" 
            CausesValidation="True">
        </asp:DropDownList>
    </p>
    <asp:Label ID="lblComments" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 250px; top: 253px; position: absolute; height: 22px; width: 88px" 
        Text="Comments" Visible="False"></asp:Label>
    <asp:GridView ID="gvStudentHistory" runat="server" BackColor="#FFD200" 
        onrowdatabound="gvStudentHistory_RowDataBound"
        BorderColor="Black" BorderWidth="3px" 
        onselectedindexchanged="gvStudentHistory_SelectedIndexChanged" 
        style="z-index: 1; left: 37px; top: 462px; position: absolute; height: 147px; width: 770px">
    </asp:GridView>
    <asp:LinkButton ID="lbAddNewEntry" runat="server" Font-Size="10pt" 
        ForeColor="Black" onclick="lbAddNewEntry_Click" 
        style="z-index: 1; left: 133px; top: 444px; position: absolute" 
        Visible="False">(Add a new entry)</asp:LinkButton>
    <asp:Label ID="lblNewEntry" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 524px; top: 322px; position: absolute; height: 17px; width: 93px" 
        Text="New Entry" Visible="False"></asp:Label>
    <asp:Button ID="cmbOptions" runat="server" onclick="cmbOptions_Click1" 
        style="z-index: 1; left: 249px; top: 207px; position: absolute; width: 148px" 
        Text="Review Student Info" Visible="False" />
    <asp:CheckBox ID="chbSAT" runat="server" 
        style="z-index: 1; left: 760px; top: 293px; position: absolute" 
        Text="SAT Exam" Visible="False" />
    <asp:CheckBox ID="chbFAFSA" runat="server" 
        style="z-index: 1; left: 869px; top: 293px; position: absolute" 
        Text="FAFSA" Visible="False" />
    <asp:Label ID="lblPostGraduatePlans" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 763px; top: 274px; position: absolute" 
        Text="Post Graduate Plans" Visible="False"></asp:Label>
    <asp:GridView ID="gvOptionsComprehensive" runat="server" BackColor="#FFD200" 
        onrowdatabound="gvOptionsComprehensive_RowDataBound"
        style="z-index: 1; left: 37px; top: 331px; position: absolute; height: 52px; width: 253px">
        <AlternatingRowStyle BackColor="#999999" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
        <RowStyle Wrap="False" />
    </asp:GridView>
    <asp:Button ID="cmbResetPage" runat="server" onclick="cmbResetPage_Click" 
        style="z-index: 1; left: 1040px; top: 261px; position: absolute; width: 135px" 
        Text="Reset Page" />
    <asp:CheckBox ID="chbCollegeFair" runat="server" 
        style="z-index: 1; left: 760px; top: 314px; position: absolute; right: 166px; height: 18px" 
        Text="College Fair" Visible="False" />
    <asp:Button ID="cmbExcelExport" runat="server" Enabled="False" 
        onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1040px; top: 294px; position: absolute; width: 136px" 
        Text="Export to Excel" />
    <asp:Button ID="cmbStudentDescription" runat="server" 
        onclick="cmbStudentDescription_Click" 
        style="z-index: 1; left: 1042px; top: 157px; position: absolute; width: 133px" 
        Text="Description Report" />
    </form>
</body>
</html>
