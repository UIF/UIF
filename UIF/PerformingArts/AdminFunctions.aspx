<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFunctions.aspx.cs" Inherits="UIF.PerformingArts.AdminFunctions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ProgramClassLists</title>
    <style type="text/css">
        .style1
        {
            position: absolute;
            top: 55px;
            left: 389px;
            z-index: 1;
            width: 431px;
        }
        .style2
        {
            text-align: center;
        }
        .style3
        {
            position: absolute;
            top: 153px;
            left: 164px;
            z-index: 1;
            width: 144px;
        }
        .style4
        {
            position: absolute;
            top: 192px;
            left: 42px;
            z-index: 1;
            width: 403px;
            height: 30px;
            right: 142px;
        }
        .style5
        {
            position: absolute;
            top: 242px;
            left: 728px;
            z-index: 1;
            width: 228px;
            height: 25px;
            right: -31px;
        }
        .style6
        {
            width: 686px;
            height: 166px;
            position: absolute;
            top: 363px;
            left: 38px;
            z-index: 1;
        }
        .style10
        {
            width: 640px;
            height: 70px;
            position: absolute;
            top: 452px;
            left: 562px;
            z-index: 1;
        }
        </style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="Orange" >
    <form id="form2" runat="server"  defaultfocus="txbClassName">
    <div class="style2">
    <div>
    
    </div>
        <asp:Label ID="lblProgramClassLists" runat="server" CssClass="style1" 
            Font-Bold="True" Font-Size="36pt" Text="Program/Class Lists"></asp:Label>
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


    <p class="style2">
        &nbsp;</p>
    <asp:Button ID="cmbProgram" runat="server" CssClass="style3" 
        onclick="cmbProgram_Click" Text="View Program" Visible="False" />

    <asp:Label ID="lblRemoveStudents" runat="server" Font-Size="18pt" 
        style="z-index: 1; left: 51px; top: 334px; position: absolute; height: 30px; width: 411px; font-weight: 700; text-decoration: underline" 
        Text="Delete STUDENTs from the System" Visible="False"></asp:Label>

    <asp:Label ID="lblProgram" runat="server" CssClass="style4" 
        Text="Please select an Admin Function." Font-Bold="True" 
        Font-Size="17pt" TabIndex="23"></asp:Label>
    <asp:DropDownList ID="ddlProgram" runat="server" CssClass="style5" 
        BackColor="#FFD200" 
        onselectedindexchanged="ddlProgram_SelectedIndexChanged" TabIndex="24" 
        AutoPostBack="True" CausesValidation="True" Visible="False">
    </asp:DropDownList>

    <asp:GridView ID="gvStudentList" runat="server" AllowSorting="True" 
        BorderStyle="Solid" BorderWidth="4px" CssClass="style6" 
        OnSelectedIndexChanged="gvStudentList_SelectedIndexChanged"
        OnSelectedIndexChanging="gvStudentList_SelectedIndexChanging"
        OnRowDataBound="gvStudentList_RowDataBound"
        ShowHeaderWhenEmpty="True" OnRowEditing="gvStudentList_RowEditing" 
        OnRowUpdating="gvStudentList_RowUpdating"
        OnRowCancelingEdit="gvStudentList_RowCancelingEdit"
        OnRowDeleting="gvStudentList_RowDeleting"  DataKeyNames="ID"
        BorderColor="Black" 
        AutoGenerateDeleteButton="True" AutoGenerateColumns="False" 
        EnablePersistedSelection="True" 
        BackColor="#FFD200">
        <AlternatingRowStyle BackColor="Silver" />
        <RowStyle Wrap="False" />
        <EditRowStyle Wrap="False" />
        <EmptyDataRowStyle BorderStyle="Solid" />
        <HeaderStyle Font-Bold="True" Font-Overline="False" Font-Size="18pt" 
            Font-Underline="False" BackColor="Black" ForeColor="White" />

        <Columns >  
            <asp:TemplateField HeaderText ="LastName">  
            <ItemTemplate><%#Eval("lastname") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox1" runat="server" Text='<%#Eval("lastname") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="FirstName">  
            <ItemTemplate><%#Eval("firstname") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox2" runat="server" Text='<%#Eval("firstname") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="MiddleName">  
            <ItemTemplate><%#Eval("middlename") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox3" runat="server" Text='<%#Eval("middlename") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="ID">  
            <ItemTemplate><%#Eval("id") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox4" runat="server"  Visible="false" ReadOnly="True" Text='<%#Eval("id") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  
        </Columns>  
    </asp:GridView>

    <asp:Menu ID="menMenu" runat="server" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px" CssClass="style10">
    </asp:Menu>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1124px; top: 154px; position: absolute; height: 26px; width: 112px" 
        Text="Export to Excel" />
    <asp:Button ID="cmbAddRecord" runat="server" onclick="cmbAddRecord_Click" 
        style="z-index: 1; left: 14px; top: 345px; position: absolute; height: 32px; width: 103px" 
        TabIndex="10" Text="Add New Entry" Visible="False" />
    <asp:TextBox ID="txbClassName" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 762px; top: 215px; position: absolute; width: 147px; right: 17px" 
        Visible="False" TabIndex="2"></asp:TextBox>
    <asp:TextBox ID="txbInstructor" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 871px; top: 346px; position: absolute; width: 127px" 
        TabIndex="8" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txbDevotionalLeader" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1025px; top: 348px; position: absolute; width: 202px" 
        TabIndex="9" Visible="False"></asp:TextBox>
    <p>
        <asp:TextBox ID="txbSizeLimit" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 743px; top: 308px; position: absolute; width: 92px" 
            TabIndex="6" Visible="False"></asp:TextBox>
    </p>
    <asp:TextBox ID="txbComments" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 716px; top: 341px; position: absolute; width: 129px" 
        TabIndex="7" Visible="False"></asp:TextBox>
    <asp:DropDownList ID="ddlTime" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 806px; top: 284px; position: absolute; width: 71px; height: 79px;" 
        TabIndex="3" Visible="False">
    </asp:DropDownList>
    <asp:LinkButton ID="lbAddNewEntry" runat="server" onclick="lbAddNewEntry_Click" 
        
        style="z-index: 1; left: 38px; top: 381px; position: absolute; width: 158px" 
        Visible="False" Enabled="False">(Add a New Section)</asp:LinkButton>
    <asp:TextBox ID="txbDay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 837px; top: 310px; position: absolute; width: 84px" 
        TabIndex="4" Visible="False"></asp:TextBox>
    <p>
        <asp:TextBox ID="txbClassLocation" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 845px; top: 290px; position: absolute; width: 96px" 
            TabIndex="5" Visible="False"></asp:TextBox>
    </p>
    <asp:Label ID="lblClassName" runat="server" 
        style="z-index: 1; left: 122px; top: 361px; position: absolute; width: 142px" 
        Text="SectionName" Visible="False"></asp:Label>
    <asp:Label ID="lblTime" runat="server" 
        style="z-index: 1; left: 408px; top: 361px; position: absolute; width: 78px" 
        Text="Time" Visible="False"></asp:Label>
    <asp:Label ID="lblDay" runat="server" 
        style="z-index: 1; left: 493px; top: 363px; position: absolute; width: 79px" 
        Text="Day" Visible="False"></asp:Label>
    <asp:Label ID="lblClassLocation" runat="server" 
        style="z-index: 1; left: 598px; top: 364px; position: absolute; width: 82px; right: 235px" 
        Text="Location" Visible="False"></asp:Label>
    <asp:Label ID="lblSizeLimit" runat="server" 
        style="z-index: 1; left: 741px; top: 285px; position: absolute; right: 130px;" 
        Text="SizeLimit" Visible="False"></asp:Label>
    <asp:Label ID="lblComments" runat="server" 
        style="z-index: 1; left: 729px; top: 361px; position: absolute; width: 113px" 
        Text="Comments" Visible="False"></asp:Label>
    <asp:Label ID="lblClassInstructor" runat="server" 
        style="z-index: 1; left: 877px; top: 367px; position: absolute; width: 108px" 
        Text="Instructor" Visible="False"></asp:Label>
    <asp:Label ID="lblDevotionalLeader" runat="server" 
        style="z-index: 1; left: 1026px; top: 369px; position: absolute; width: 202px" 
        Text="DevotionalLeader" Visible="False"></asp:Label>
    <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
        style="z-index: 1; left: 38px; top: 151px; position: absolute; height: 28px; width: 93px" 
        TabIndex="22" Text="Reset Page" />
    <asp:Label ID="lblMainpage" runat="server" Font-Size="23pt" 
        style="z-index: 1; left: 487px; top: 151px; position: absolute; height: 36px; width: 470px; text-decoration: underline; font-weight: 700;" 
        Text="Admin Functions" Font-Italic="True"></asp:Label>

    <asp:GridView ID="gvTeamSections" runat="server" 
        BorderStyle="Solid" BorderWidth="4px" CssClass="style6" 
        OnSelectedIndexChanged="gvTeamSections_SelectedIndexChanged"
        OnSelectedIndexChanging="gvTeamSections_SelectedIndexChanging"
        OnRowDataBound="gvTeamSections_RowDataBound"
        ShowHeaderWhenEmpty="True" OnRowEditing="gvTeamSections_RowEditing" 
        OnRowUpdating="gvTeamSections_RowUpdating"
        OnRowCancelingEdit="gvTeamSections_RowCancelingEdit"
        OnRowDeleting="gvTeamSections_RowDeleting"  DataKeyNames="ID"
        BorderColor="Black" 
        AutoGenerateDeleteButton="True" AutoGenerateColumns="False" 
        EnablePersistedSelection="True" 
        BackColor="#FFD200">
        <AlternatingRowStyle BackColor="Silver" />
        <RowStyle Wrap="False" />
        <EditRowStyle Wrap="False" />
        <EmptyDataRowStyle BorderStyle="Solid" />
        <HeaderStyle Font-Bold="True" Font-Overline="False" Font-Size="18pt" 
            Font-Underline="False" BackColor="Black" ForeColor="White" />
        <Columns >  
            <asp:TemplateField HeaderText ="SectionName">  
            <ItemTemplate><%#Eval("sectionname") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox1" runat="server" Enabled="false" ReadOnly="True" Text='<%#Eval("sectionname") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="TeamName">  
            <ItemTemplate><%#Eval("teamname") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox2" runat="server" Text='<%#Eval("teamname") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Time">  
            <ItemTemplate><%#Eval("time") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox3" runat="server" Text='<%#Eval("time") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Day">  
            <ItemTemplate><%#Eval("day") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox4" runat="server" Text='<%#Eval("day") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Location">  
            <ItemTemplate><%#Eval("location") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox5" runat="server" Text='<%#Eval("location") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Comments">  
            <ItemTemplate><%#Eval("comments") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox6" runat="server" Text='<%#Eval("comments") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="SuperVisor">  
            <ItemTemplate> <%#Eval("supervisor")%></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox7" runat="server" Text='<%#Eval("supervisor") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
                  
            <asp:TemplateField HeaderText="DevotionalLeader" >  
            <ItemTemplate > <%#Eval("devotionalleader") %></ItemTemplate>   
            <EditItemTemplate><asp:TextBox ID="textbox8" runat="server" Text='<%#Eval("devotionalleader") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="ID" InsertVisible="False" ShowHeader="False">  
            <ItemTemplate><%#Eval("id") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox9" runat="server"  Visible="false" ReadOnly="True" Text='<%#Eval("id") %>'></asp:TextBox> 
            </EditItemTemplate>
            </asp:TemplateField>  
        </Columns>  
    </asp:GridView>
    <asp:DropDownList ID="ddlTeamNames" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" CssClass="style5" 
        onselectedindexchanged="ddlTeamNames_SelectedIndexChanged" 
        style="z-index: 1; left: 729px; top: 263px; position: absolute; width: 228px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Font-Size="17pt" 
        Font-Underline="True" 
        style="z-index: 1; left: 558px; top: 358px; position: absolute; height: 37px; width: 294px" 
        Text="Team Names" Visible="False"></asp:Label>
    <asp:Label ID="lblTeamName" runat="server" 
        style="z-index: 1; left: 278px; top: 357px; position: absolute; width: 124px" 
        Text="TeamName" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlAdminFunctions" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlAdminFunctions_SelectedIndexChanged" 
        
        
        style="z-index: 1; left: 43px; top: 224px; position: absolute; width: 306px">
    </asp:DropDownList>
    <asp:FileUpload ID="flupAddPicture" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 230px; top: 368px; position: absolute; height: 29px;" 
        Visible="False" />
    <asp:Button ID="cmbUploadPicture" runat="server" 
        onclick="cmbUploadPicture_Click" 
        style="z-index: 1; left: 231px; top: 405px; position: absolute; width: 243px; height: 40px" 
        Text="Save/Update Picture" Visible="False" />
    <asp:Label ID="lblCopyPicture" runat="server" Font-Size="15pt" 
        style="z-index: 1; left: 232px; top: 333px; position: absolute; width: 261px" 
        Text="Upload STUDENT Picture" Visible="False"></asp:Label>
    <p>
    <asp:TextBox ID="txbTeamName" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 787px; top: 194px; position: absolute; width: 117px" 
        Visible="False"></asp:TextBox>
    </p>
    <asp:DropDownList ID="ddlStudentNames" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlStudentNames_SelectedIndexChanged" 
        style="z-index: 1; left: 57px; top: 274px; position: absolute; width: 200px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlVolunteerNames" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlVolunteerNames_SelectedIndexChanged" 
        style="z-index: 1; left: 57px; top: 274px; position: absolute; width: 200px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblMessage" runat="server" Font-Size="20pt" 
        style="z-index: 1; left: 242px; top: 377px; position: absolute; height: 67px; width: 588px" 
        Visible="False"></asp:Label>
    <asp:Label ID="lblSelectStudent" runat="server" 
        style="z-index: 1; left: 58px; top: 298px; position: absolute; width: 291px" 
        Text="Select a STUDENT for picture updating." Visible="False"></asp:Label>
    <asp:Image ID="imgPicture" runat="server" BorderColor="Black" 
        BorderStyle="Solid" BorderWidth="2px" 
        style="z-index: 1; left: 57px; top: 325px; position: absolute; height: 125px; width: 149px" 
        Visible="False" />
    <asp:LinkButton ID="lbStudentInfo" runat="server" onclick="lbStudentInfo_Click" 
        style="z-index: 1; left: 318px; top: 300px; position: absolute; width: 126px" 
        Visible="False">Student Profile</asp:LinkButton>
    <asp:LinkButton ID="lbVolunteerInfo" runat="server" 
        onclick="lbVolunteerInfo_Click" 
        style="z-index: 1; left: 331px; top: 299px; position: absolute; width: 143px" 
        Visible="False">Volunteer Profile</asp:LinkButton>
    </form>
</body>
</html>
