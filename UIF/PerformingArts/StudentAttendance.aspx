<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAttendance.aspx.cs" Inherits="UIF.PerformingArts.StudentAttendance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="C#" type="C#">
   string llkjlaaaa = DropDownList.SelectedItem.Value;
   

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            position: absolute;
            top: 149px;
            left: 454px;
            z-index: 1;
            width: 395px;
            right: 166px;
            text-decoration: underline;
        }
        .style2
        {
            position: absolute;
            top: 174px;
            left: 244px;
            z-index: 1;
            width: 189px;
        }
        .style3
        {
            position: absolute;
            top: 177px;
            left: 46px;
            z-index: 1;
            width: 222px;
            font-weight: bold;
        }
        .style4
        {
            position: absolute;
            top: 195px;
            left: 465px;
            z-index: 1;
            width: 237px;
            right: 444px;
        }
        .style5
        {
            width: 432px;
            height: 178px;
            position: absolute;
            top: 484px;
            left: 60px;
            z-index: 1;
        }
        .style6
        {
            height: 950px;
        }
        .style7
        {
            position: absolute;
            top: 256px;
            left: 663px;
            z-index: 1;
            width: 309px;
            height: 39px;
        }
        .style9
        {
            position: absolute;
            top: 301px;
            left: 666px;
            z-index: 1;
            width: 667px;
            height: 45px;
            font-weight: bold;
        }
        .style10
        {
            position: absolute;
            top: 459px;
            left: 82px;
            z-index: 1;
            font-weight: bold;
            text-decoration: underline;
        }
        .style15
        {
            position: absolute;
            top: 411px;
            left: 308px;
            z-index: 1;
            width: 188px;
            height: 24px;
        }
        .style16
        {
            position: absolute;
            top: 411px;
            left: 72px;
            z-index: 1;
            width: 189px;
            height: 24px;
        }
        .style17
        {
            position: absolute;
            top: 391px;
            left: 74px;
            z-index: 1;
            width: 225px;
            font-weight: bold;
            right: 555px;
        }
        .style18
        {
            position: absolute;
            top: 391px;
            left: 311px;
            z-index: 1;
            width: 254px;
            font-weight: bold;
        }
        .style20
        {
            position: absolute;
            top: 183px;
            left: 115px;
            z-index: 1;
            width: 356px;
            font-weight: bold;
            text-decoration: underline;
        }
        .style21
        {
            position: absolute;
            top: 362px;
            left: 172px;
            z-index: 1;
            width: 259px;
            height: 125px;
            font-weight: bold;
        }
        .style23
        {
            position: absolute;
            top: 696px;
            left: 40px;
            z-index: 1;
            width: 1083px;
            height: 100px;
        }
        .style24
        {
            position: absolute;
            top: 435px;
            left: 28px;
            z-index: 1;
            width: 447px;
            right: 379px;
        }
        </style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="Orange">
    <form id="form1" runat="server" class="style6" visible="True">

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


    <div style="margin-left: 40px">
    
    </div>
    <asp:Label ID="Label1" runat="server" CssClass="style1" Font-Bold="False" 
        Font-Size="23pt" Text="Enter Student Attendance " Font-Italic="True"></asp:Label>
    <asp:DropDownList ID="ddlProgram" runat="server" CssClass="style2" 
        onselectedindexchanged="ddlProgram_SelectedIndexChanged" 
        BackColor="#FFCC00" AutoPostBack="True" CausesValidation="True">
    </asp:DropDownList>
    <asp:Button ID="cmbEnterAttendance" runat="server" CssClass="style4" 
        onclick="cmbEnterAttendance_Click" Text="Proceed to Enter Attendance." 
        Visible="False" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="Data Source=UIF-Server;Initial Catalog=UIF_PerformingArts;Integrated Security=True" 
        ProviderName="System.Data.SqlClient" SelectCommand="select lastname, firstname from MSHSChoir_Attendance
order by lastname, firstname" onselecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
            <div style="margin-left: 280px">
                <asp:Button ID="cmbAddAnotherClass" runat="server" Enabled="False" 
                    onclick="cmbAddAnotherClass_Click" 
                    style="z-index: 1; left: 711px; top: 208px; position: absolute; width: 162px; height: 43px;" 
                    Text="Enter Another Class" Visible="False" />
            </div>
    <asp:Calendar ID="calCalender2" runat="server" BackColor="#FFD200" onselectionchanged="calCalender2_SelectionChanged"
        BorderColor="Black" BorderStyle="Solid" BorderWidth="4px" 
        NextPrevFormat="ShortMonth" ShowGridLines="True" 
        style="z-index: 1; left: 661px; top: 332px; position: absolute; height: 330px; width: 447px" 
        Visible="False"></asp:Calendar>




    <asp:Label ID="lblSetAttendance" runat="server" CssClass="style24" 
        Font-Bold="True" Font-Size="14pt" 
        Text="1)  Please set each students attendance accordingly." Visible="False"></asp:Label>
    <asp:Image ID="imgUIF2" runat="server" CssClass="style23"  ImageUrl="~/Picture3.png"/>
    <asp:Label ID="lblConfirmation" runat="server" CssClass="style21" 
        Enabled="False" Font-Size="X-Large" 
        Text="The attendance has been successfully entered into the database.  Thankyou." 
        Visible="False"></asp:Label>
    <asp:GridView ID="gvStudentList" runat="server" CssClass="style5" 
        onselectedindexchanged="gvStudentList_SelectedIndexChanged" OnRowEditing="gvStudentList_RowEditing"
        BorderStyle="Double" AutoGenerateColumns="False" BackColor="#FFD200" 
        BorderColor="Black" BorderWidth="8px">
        <Columns>
            <asp:BoundField  DataField="LastName" HeaderText="LastName" ReadOnly="true"  SortExpression="LastName"  />
            <asp:BoundField  DataField="FirstName" HeaderText="FirstName" ReadOnly="true"  SortExpression="FirstName"  />
            <asp:BoundField  DataField="MiddleName" HeaderText="MiddleName" ReadOnly="true"  SortExpression="MiddleName"  />
            <asp:TemplateField HeaderText="Attended" >
                <ItemTemplate>
                    <asp:DropDownList id="dropdownlist1"  runat="server"  AutoPostBack="false" BackColor="Orange"
                         >                        
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                    </asp:DropDownList>                
                </ItemTemplate>            
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Exempt" >
                <ItemTemplate>
                    <asp:DropDownList id="dropdownlist2"  runat="server"  AutoPostBack="false" BackColor="Orange"
                         >                        
                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                    </asp:DropDownList>                
                </ItemTemplate>            
            </asp:TemplateField>
            <asp:TemplateField HeaderText="RSVG" >
                <ItemTemplate>
                    <asp:DropDownList id="dropdownlist4"  runat="server"  AutoPostBack="false" BackColor="Orange"
                            >                        
                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                    </asp:DropDownList>                
                </ItemTemplate>            
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bible?" >
                <ItemTemplate>
                    <asp:DropDownList id="dropdownlist5"  runat="server"  AutoPostBack="false" BackColor="Orange"
                            >                        
                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                    </asp:DropDownList>                
                </ItemTemplate>            
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="Black" BorderColor="Black" ForeColor="White" />
    </asp:GridView>
     <asp:Label ID="lblPAATracking" runat="server" CssClass="style20" 
        Enabled="False" Text="PerformingArtsAcademy Attendance" Visible="False" 
        Font-Size="16pt"></asp:Label>
    <asp:Label ID="lblBiblesGiven" runat="server" 
        style="z-index: 1; left: 354px; top: 371px; position: absolute; font-weight: 700" 
        Text="(# Bibles)" Visible="False"></asp:Label>
    <asp:Label ID="lblClass2" runat="server" CssClass="style18" Enabled="False" 
        Text="6:30-8:00pm Class" Visible="False"></asp:Label>
    <asp:Label ID="lblClass1" runat="server" CssClass="style17" Enabled="False" 
        Text="4:30-6:00pm Class" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlClassSelection" runat="server" AutoPostBack="True" 
        CausesValidation="True" CssClass="style16" Enabled="False" 
        onselectedindexchanged="ddlClassSelection_SelectedIndexChanged" 
        Visible="False" BackColor="#FFD200">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlClassSelection2" runat="server" AutoPostBack="True" 
        CausesValidation="True" CssClass="style15" Enabled="False" 
        onselectedindexchanged="ddlClassSelection2_SelectedIndexChanged" 
        Visible="False" BackColor="#FFD200" ForeColor="Black">
    </asp:DropDownList>
    <asp:Label ID="Label2" runat="server" CssClass="style3" 
        Text="Please select a program." Font-Size="Large"></asp:Label>
    <asp:Label ID="lblInformation" runat="server" CssClass="style10" 
        Enabled="False" Font-Size="Large" Text="Student Names in Program" 
        Visible="False"></asp:Label>
    <asp:Label ID="lblPleaseChoose" runat="server" CssClass="style9" 
        Text="2)  Please select an event date for the attendance." 
        Enabled="False" Visible="False" Font-Size="14pt" Font-Bold="True"></asp:Label>
    <asp:Button ID="cmbCommittAttendance" runat="server" CssClass="style7" 
        onclick="cmbCommittAttendance_Click" Text="Committ the Attendance Data" />
            <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
                style="z-index: 1; top: 191px; position: absolute; left: 890px; width: 190px;" 
                Text="Export Student List to Excel" Visible="False" />
            <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click1" 
                style="z-index: 1; left: 940px; top: 156px; position: absolute; width: 139px;" 
                Text="Reset the Page" />
    <asp:DropDownList ID="ddlBasketballTEAMS" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlBasketballTEAMS_SelectedIndexChanged" 
        style="z-index: 1; left: 72px; position: absolute; width: 189px; top: 412px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlIndividualStudentAttendance" runat="server" 
        AutoPostBack="True" BackColor="#FFD200" CausesValidation="True" 
        style="z-index: 1; left: 244px; top: 234px; position: absolute; width: 189px; right: 516px;" 
        Visible="False" 
        
        onselectedindexchanged="ddlIndividualStudentAttendance_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label ID="lblIndividualStudent" runat="server" Font-Size="Large" 
        style="z-index: 1; left: 132px; top: 213px; position: absolute; width: 250px; font-weight: 700; height: 44px" 
        Text="Individual Student Attendance.   (OPTIONAL)" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlOutreachBasketball" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOutreachBasketball_SelectedIndexChanged" 
        style="z-index: 1; left: 71px; top: 412px; position: absolute; width: 189px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlBaseballSections" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlBaseballSections_SelectedIndexChanged" 
        style="z-index: 1; left: 71px; top: 412px; position: absolute; width: 189px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSoccerTEAMS" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlSoccerTEAMS_SelectedIndexChanged" 
        style="z-index: 1; left: 71px; top: 413px; position: absolute; width: 189px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlTeamName" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlTeamName_SelectedIndexChanged" 
        style="z-index: 1; left: 243px; top: 280px; position: absolute; width: 189px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblTeamName" runat="server" Font-Bold="True" Font-Size="Large" 
        style="z-index: 1; left: 131px; top: 260px; position: absolute; width: 251px; height: 42px" 
        Text="Record by TeamName   (OPTIONAL)" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlMealsYesNo" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlMealsYesNo_SelectedIndexChanged" 
        style="z-index: 1; left: 232px; top: 309px; position: absolute; width: 68px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlMealCount" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlMealCount_SelectedIndexChanged" 
        style="z-index: 1; left: 424px; top: 309px; position: absolute; width: 53px" 
        Visible="False" Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="lblMealsServed" runat="server" 
        style="z-index: 1; left: 24px; top: 310px; position: absolute; height: 24px; width: 370px; font-weight: 700" 
        Text="Meals served at your activity?" Visible="False"></asp:Label>
    <asp:Label ID="lblHowMany" runat="server" 
        style="z-index: 1; left: 310px; top: 310px; position: absolute; width: 133px; font-weight: 700" 
        Text="(# Meal Times?)" Visible="False"></asp:Label>
    <asp:Label ID="lblGospelGiven" runat="server" 
        style="z-index: 1; left: 24px; top: 341px; position: absolute; width: 280px; font-weight: 700" 
        Text="Was the Gospel presented?" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlGospelGiven" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGospelGiven_SelectedIndexChanged" 
        style="z-index: 1; left: 215px; top: 340px; position: absolute; width: 85px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGospelAccepted" runat="server" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlGospelAccepted_SelectedIndexChanged" 
        style="z-index: 1; left: 423px; top: 340px; position: absolute" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlBiblesGiven" runat="server" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlBiblesGiven_SelectedIndexChanged" 
        style="z-index: 1; left: 423px; top: 370px; position: absolute" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlMiscellaneousMeals" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        style="z-index: 1; left: 592px; top: 309px; position: absolute; width: 52px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblGospelAccepted" runat="server" 
        style="z-index: 1; left: 319px; top: 340px; position: absolute; font-weight: 700; width: 118px;" 
        Text="(# Responded)" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlAudience" runat="server" BackColor="#FFD200" 
        Enabled="False" 
        style="z-index: 1; left: 592px; top: 371px; position: absolute; width: 52px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblAudience" runat="server" 
        style="z-index: 1; left: 487px; top: 372px; position: absolute; width: 141px; font-weight: 700" 
        Text="(AudienceSize)" Visible="False"></asp:Label>
    <asp:Label ID="lblMiscMeals" runat="server" 
        style="z-index: 1; left: 491px; top: 310px; position: absolute; width: 112px; font-weight: 700" 
        Text="(# MiscMeals)" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlHours" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 592px; top: 341px; position: absolute" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblProgramHours" runat="server" 
        style="z-index: 1; left: 485px; top: 341px; position: absolute; width: 138px" 
        Text="(Program Hrs.)" Visible="False" Font-Bold="True"></asp:Label>
    <asp:Label ID="lblErrorMessage" runat="server" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" Font-Size="18pt" 
        ForeColor="Red" 
        style="z-index: 1; left: 137px; top: 244px; position: absolute; height: 307px; width: 473px" 
        Text="ErrorMessage:" Visible="False"></asp:Label>
    </form>
</body>
</html>
