<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VolunteerDetails.aspx.cs" Inherits="UIF.PerformingArts.VolunteerDetails" %>

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
            top: 177px;
            left: 29px;
            z-index: 1;
            width: 403px;
            height: 30px;
            right: 566px;
        }
        .style5
        {
            position: absolute;
            top: 157px;
            left: 162px;
            z-index: 1;
            width: 228px;
            height: 25px;
        }
        .style6
        {
            width: 1199px;
            height: 166px;
            position: absolute;
            top: 470px;
            left: 39px;
            z-index: 1;
        }
        .style10
        {
            width: 640px;
            height: 70px;
            position: absolute;
            top: 381px;
            left: 576px;
            z-index: 1;
        }
        .style11
        {
            position: absolute;
            top: 1019px;
            left: 61px;
            z-index: 1;
            height: 126px;
            width: 1202px;
        }
        </style>

<style type="text/css"> 
   div { z-index: 1;
        margin-left: 40px;
        left: 16px;
        top: 178px;
        position: absolute;
        height: 392px;
        width: 1036px;
    } 
</style>

</head>
<body bgcolor="Orange" >
    <form id="form2" runat="server"  defaultfocus="txbClassName">
    <div class="style2">
    <div>
        
    </div>
        <asp:Label ID="lblProgramClassLists" runat="server" CssClass="style1" 
            Font-Bold="True" Font-Size="36pt" Text="Program/Class Lists" 
            Visible="False"></asp:Label>
    </div>

    <asp:Panel ID="pnlBackground" runat="server" BackColor="White"  
        style="z-index: 1; left: -41px; top: 1px; position: absolute; height: 133px; width: 1327px" 
        ViewStateMode="Enabled" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px">
        <asp:Label ID="lblUrbanImpact" runat="server" Font-Bold="True" Font-Size="36pt" 
            style="z-index: 1; left: 354px; top: 24px; position: absolute; height: 62px; width: 547px" 
            Text="Urban Impact Foundation"></asp:Label>
        <asp:Label ID="lblLastUpdatedBy" runat="server" 
            style="z-index: 1; left: 1069px; top: 21px; position: absolute; height: 87px; width: 162px" 
            Text="LastUpdatedBy: " Visible="False"></asp:Label>
        <asp:ImageButton ID="imbAdvancedSearch" runat="server" 
            ImageUrl="~/MagnifiyingGlass.bmp" onclick="imbAdvancedSearch_Click" 
            
            style="z-index: 1; left: 444px; top: 224px; position: absolute; height: 34px; width: 42px" 
            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
            ToolTip="Retreive Reports for Background Checks" />
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
        <DynamicSelectedStyle BackColor="#FFD200" VerticalPadding="4px" Width="20px" />

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
    <asp:Label ID="lblProgram" runat="server" CssClass="style4" 
        Text="Please select a program to administer." Font-Bold="True" 
        Font-Size="18pt" TabIndex="23" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlProgram" runat="server" CssClass="style5" 
        BackColor="#FFD200" 
        onselectedindexchanged="ddlProgram_SelectedIndexChanged" TabIndex="24" 
        AutoPostBack="True" CausesValidation="True" Visible="False">
    </asp:DropDownList>

    <asp:GridView ID="gvReports" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 40px; top: 411px; position: absolute; height: 133px; width: 672px; right: 207px" 
        Visible="False">
        <AlternatingRowStyle BackColor="#999999" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
    </asp:GridView>

    <asp:DropDownList ID="ddlChooseField1" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        style="z-index: 1; left: 48px; top: 732px; position: absolute; width: 122px" 
        Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlNationalCheckReport" runat="server" 
        AutoPostBack="True" BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlNationalCheckReport_SelectedIndexChanged" 
        style="z-index: 1; left: 41px; top: 523px; position: absolute; width: 168px" 
        Visible="False">
    </asp:DropDownList>

    <asp:Image ID="imgUIF" runat="server" CssClass="style11"  
        ImageUrl="~/Picture3.png" Visible="False"/>

    <asp:GridView ID="gvStudentList" runat="server" AllowSorting="True" 
        BorderStyle="Solid" BorderWidth="4px" CssClass="style6" 
        OnSelectedIndexChanged="gvStudentList_SelectedIndexChanged"
        OnSelectedIndexChanging="gvStudentList_SelectedIndexChanging"
        OnRowDataBound="gvStudentList_RowDataBound"
        ShowHeaderWhenEmpty="True" AutoGenerateEditButton="True" OnRowEditing="gvStudentList_RowEditing" 
        OnRowUpdating="gvStudentList_RowUpdating"
        OnRowCancelingEdit="gvStudentList_RowCancelingEdit"
        OnRowDeleting="gvStudentList_RowDeleting"  DataKeyNames="Name"
        BorderColor="Black" 
        AutoGenerateDeleteButton="True" AutoGenerateColumns="False" 
        EnablePersistedSelection="True" 
        BackColor="#FFD200">
        <AlternatingRowStyle BackColor="Silver" />
        <RowStyle Wrap="False" />
        <EditRowStyle Wrap="False" />
        <EmptyDataRowStyle BorderStyle="Solid" />
        <HeaderStyle Font-Bold="True" Font-Overline="False" Font-Size="14pt" 
            Font-Underline="False" BackColor="Black" ForeColor="White" />

        <Columns >  
            <asp:TemplateField HeaderText ="Name">  
            <ItemTemplate><%#Eval("name") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox1" runat="server" Text='<%#Eval("name") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="GeneralInfo">  
            <ItemTemplate><%#Eval("generalinfo") %></ItemTemplate>  
            <EditItemTemplate><asp:CheckBox ID="checkGeneralInformation" runat="server" Text='<%#Eval("generalinfo") %>'></asp:CheckBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="SpirJourney">  
            <ItemTemplate><%#Eval("spirjourney") %></ItemTemplate>  
            <EditItemTemplate><asp:CheckBox ID="checkSpiritualJourney" runat="server" Text='<%#Eval("spirjourney") %>'></asp:CheckBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="ReleaseWaiver">  
            <ItemTemplate><%#Eval("releasewaiver") %></ItemTemplate>  
            <EditItemTemplate><asp:CheckBox ID="checkReleaseWaiver" runat="server" Text='<%#Eval("releasewaiver") %>'></asp:CheckBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="NewVolTrain">  
            <ItemTemplate><%#Eval("newvoltrain") %></ItemTemplate>
            <EditItemTemplate><asp:CheckBox ID="checkNVT" runat="server" Text='<%#Eval("newvoltrain") %>'></asp:CheckBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="NewVolTrainDate">  
            <ItemTemplate><%#Eval("newvoltraindate") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textNVTD" runat="server" Text='<%#Eval("newvoltraindate") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="VehInsur">  
            <ItemTemplate> <%#Eval("vehinsur")%></ItemTemplate>  
            <EditItemTemplate><asp:CheckBox ID="textbox6" runat="server" Text='<%#Eval("vehinsur") %>'></asp:CheckBox></EditItemTemplate>
            </asp:TemplateField>
                  
            <asp:TemplateField HeaderText="VehInsurCodes" >  
            <ItemTemplate > <%#Eval("vehinsurcodes") %></ItemTemplate>   
            <EditItemTemplate><asp:TextBox ID="textbox7" runat="server" Text='<%#Eval("vehinsurcodes") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="VehInsurDate">  
            <ItemTemplate><%#Eval("vehinsurdate") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox3" runat="server" Text='<%#Eval("vehinsurdate") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="BckgrndCk">  
            <ItemTemplate><%#Eval("bckgrndck") %></ItemTemplate>  
            <EditItemTemplate><asp:CheckBox ID="textbox4" runat="server" Text='<%#Eval("bckgrndck") %>'></asp:CheckBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="NatCk">  
            <ItemTemplate><%#Eval("natck") %></ItemTemplate>
            <EditItemTemplate><asp:CheckBox ID="textbox20" runat="server" Text='<%#Eval("natck") %>'></asp:CheckBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="NatCkCodes">  
            <ItemTemplate><%#Eval("natckcodes") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox5" runat="server" Text='<%#Eval("natckcodes") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="NatCkDate">  
            <ItemTemplate> <%#Eval("NatCkDate")%></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox6" runat="server" Text='<%#Eval("NatCkDate") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
                  
            <asp:TemplateField HeaderText="DMVCk" >  
            <ItemTemplate > <%#Eval("dmvck") %></ItemTemplate>   
            <EditItemTemplate><asp:CheckBox ID="textbox7" runat="server" Text='<%#Eval("dmvck") %>'></asp:CheckBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="DMVCkCodes">  
            <ItemTemplate><%#Eval("dmvckcodes") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox3" runat="server" Text='<%#Eval("dmvckcodes") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="DMVCkDate">  
            <ItemTemplate><%#Eval("dmvckdate") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox4" runat="server" Text='<%#Eval("dmvckdate") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="PACrimCk">  
            <ItemTemplate><%#Eval("pacrimck") %></ItemTemplate>
            <EditItemTemplate><asp:CheckBox ID="textbox20" runat="server" Text='<%#Eval("pacrimck") %>'></asp:CheckBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="PACrimCkCodes">  
            <ItemTemplate><%#Eval("pacrimckcodes") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox5" runat="server" Text='<%#Eval("pacrimckcodes") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="PACrimCkDate">  
            <ItemTemplate> <%#Eval("PACrimCkDate")%></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox6" runat="server" Text='<%#Eval("PACrimCkDate") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
                  
            <asp:TemplateField HeaderText="BckgrndCkPd" >  
            <ItemTemplate > <%#Eval("BckgrndCkPd")%></ItemTemplate>   
            <EditItemTemplate><asp:CheckBox ID="textbox7" runat="server" Text='<%#Eval("BckgrndCkPd") %>'></asp:CheckBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="BckgrndCkCodes">  
            <ItemTemplate><%#Eval("BckgrndCkCodes")%></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox3" runat="server" Text='<%#Eval("BckgrndCkCodes") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="BckgrndCkPdDate">  
            <ItemTemplate><%#Eval("BckgrndCkPdDate")%></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox4" runat="server" Text='<%#Eval("BckgrndCkPdDate") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Comments">  
            <ItemTemplate><%#Eval("comments") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox20" runat="server" Text='<%#Eval("comments") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="LastUpdatedBy">  
            <ItemTemplate><%#Eval("lastupdatedby") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox5" runat="server" Text='<%#Eval("lastupdatedby") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="ID" InsertVisible="False" ShowHeader="False">  
            <ItemTemplate><%#Eval("id") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox8" runat="server"  Visible="false" ReadOnly="True" Text='<%#Eval("id") %>'></asp:TextBox> 
            </EditItemTemplate>
            </asp:TemplateField>  
        </Columns>  
    </asp:GridView>

    <asp:Menu ID="menMenu" runat="server" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px" CssClass="style10">
    </asp:Menu>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1041px; top: 154px; position: absolute; height: 36px; width: 167px" 
        Text="Export to Excel" Visible="False" />
    <asp:Button ID="cmbAddRecord" runat="server" onclick="cmbAddRecord_Click" 
        style="z-index: 1; left: 4px; top: 524px; position: absolute; height: 43px; width: 135px" 
        TabIndex="10" Text="Commit New Class" Visible="False" />
    <asp:TextBox ID="txbClassName" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 154px; top: 454px; position: absolute; width: 164px; right: 615px" 
        Visible="False" TabIndex="2"></asp:TextBox>
    <asp:TextBox ID="txbInstructor" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 851px; top: 456px; position: absolute; width: 127px" 
        TabIndex="8" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txbDevotionalLeader" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 996px; top: 454px; position: absolute; width: 202px" 
        TabIndex="9" Visible="False"></asp:TextBox>
    <p>
        <asp:TextBox ID="txbSizeLimit" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 603px; top: 453px; position: absolute; width: 92px; bottom: 99px;" 
            TabIndex="6" Visible="False"></asp:TextBox>
    </p>
    <asp:TextBox ID="txbComments" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 714px; top: 452px; position: absolute; width: 129px" 
        TabIndex="7" Visible="False"></asp:TextBox>
    <asp:DropDownList ID="ddlTime" runat="server" BackColor="#FFD200" Height="22px" 
        style="z-index: 1; left: 322px; top: 456px; position: absolute; width: 114px" 
        TabIndex="3" Visible="False">
    </asp:DropDownList>
    <asp:LinkButton ID="lbAddNewEntry" runat="server" onclick="lbAddNewEntry_Click" 
        
        style="z-index: 1; left: 9px; top: 466px; position: absolute; width: 158px" 
        Visible="False">(Add a New Section)</asp:LinkButton>
    <asp:TextBox ID="txbDay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 444px; top: 451px; position: absolute; width: 55px" 
        TabIndex="4" Visible="False"></asp:TextBox>
    <p>
        <asp:TextBox ID="txbClassLocation" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 510px; top: 453px; position: absolute; width: 87px" 
            TabIndex="5" Visible="False"></asp:TextBox>
    </p>
    <asp:Label ID="lblClassName" runat="server" 
        style="z-index: 1; left: 156px; top: 475px; position: absolute; width: 142px" 
        Text="ClassName" Visible="False"></asp:Label>
    <asp:Label ID="lblTime" runat="server" 
        style="z-index: 1; left: 332px; top: 476px; position: absolute; width: 78px" 
        Text="Time" Visible="False"></asp:Label>
    <asp:Label ID="lblDay" runat="server" 
        style="z-index: 1; left: 448px; top: 471px; position: absolute; width: 79px" 
        Text="Day" Visible="False"></asp:Label>
    <asp:Label ID="lblClassLocation" runat="server" 
        style="z-index: 1; left: 514px; top: 475px; position: absolute; width: 82px; right: 337px" 
        Text="Location" Visible="False"></asp:Label>
    <asp:Label ID="lblSizeLimit" runat="server" 
        style="z-index: 1; left: 618px; top: 476px; position: absolute" 
        Text="SizeLimit" Visible="False"></asp:Label>
    <asp:Label ID="lblComments" runat="server" 
        style="z-index: 1; left: 718px; top: 476px; position: absolute; width: 113px" 
        Text="Comments" Visible="False"></asp:Label>
    <asp:Label ID="lblClassInstructor" runat="server" 
        style="z-index: 1; left: 862px; top: 477px; position: absolute; width: 108px" 
        Text="ClassInstructor" Visible="False"></asp:Label>
    <asp:Label ID="lblDevotionalLeader" runat="server" 
        style="z-index: 1; left: 1009px; top: 477px; position: absolute; width: 202px" 
        Text="DevotionalLeader" Visible="False"></asp:Label>
    <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
        style="z-index: 1; left: 1041px; top: 196px; position: absolute; height: 34px; width: 169px" 
        TabIndex="22" Text="Reset Page" />
    <asp:Label ID="lblMainpage" runat="server" Font-Size="24pt" 
        style="z-index: 1; left: 426px; top: 151px; position: absolute; height: 36px; width: 538px; text-decoration: underline; right: 678px;" 
        Text="Volunteer Background Details" Font-Bold="True"></asp:Label>
    <asp:CheckBox ID="chbGeneralInformation" runat="server" 
        style="z-index: 1; left: 226px; top: 241px; position: absolute; width: 166px" 
        Text="General Information" Visible="False" />
    <asp:CheckBox ID="chbSpiritualJourney" runat="server" 
        style="z-index: 1; left: 226px; top: 262px; position: absolute; width: 148px" 
        Text="Spiritual Journey" Visible="False" />
    <asp:CheckBox ID="chbReleaseWaiver" runat="server" 
        style="z-index: 1; left: 226px; top: 282px; position: absolute" 
        Text="Release Waiver" Visible="False" />
    <asp:CheckBox ID="chbNewVolunteerTraining" runat="server" 
        style="z-index: 1; left: 226px; top: 303px; position: absolute; width: 192px" 
        Text="New Volunteer Training" Visible="False" />
    <asp:CheckBox ID="chbVehichleInsurance" runat="server" 
        style="z-index: 1; left: 574px; top: 289px; position: absolute; width: 193px; right: 134px;" 
        Text="Vehichle Insurance" Visible="False" />
    <asp:CheckBox ID="chbBackgroundCheck" runat="server" 
        style="z-index: 1; left: 918px; top: 172px; position: absolute; width: 164px" 
        Text="Background Check" Visible="False" />
    <asp:CheckBox ID="chbNationalCheck" runat="server" 
        style="z-index: 1; left: 730px; top: 290px; position: absolute; width: 184px" 
        Text="National Check" Visible="False" />
    <asp:CheckBox ID="chbDMVCheck" runat="server" 
        style="z-index: 1; left: 884px; top: 290px; position: absolute; width: 152px" 
        Text="DMV Check" Visible="False" />
    <asp:CheckBox ID="chbPACriminalCheck" runat="server" 
        style="z-index: 1; left: 1038px; top: 290px; position: absolute; width: 212px" 
        Text="PA Criminal Check" Visible="False" />
    <asp:CheckBox ID="chbBackgroundCheckPAID" runat="server" 
        style="z-index: 1; left: 413px; top: 305px; position: absolute; width: 193px" 
        Text="Bckgrnd Ck PAID" Visible="False" />
    <asp:DropDownList ID="ddlDMVCheckCodes" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 887px; top: 310px; position: absolute; width: 130px" 
        Visible="False" 
        onselectedindexchanged="ddlDMVCheckCodes_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlNationalCheckCodes" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 733px; top: 310px; position: absolute; width: 130px" 
        Visible="False" 
        onselectedindexchanged="ddlNationalCheckCodes_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True">
    </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValueBool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValueBool_SelectedIndexChanged" 
            style="z-index: 1; left: 349px; top: 269px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValue3Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue3Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 349px; top: 360px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValue4Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue4Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 349px; top: 406px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValue5Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue5Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 349px; top: 456px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:Label ID="lblErrorMessage" runat="server" BackColor="White" 
            BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" Font-Size="16pt" 
            ForeColor="Red" 
            style="z-index: 1; left: 550px; top: 363px; position: absolute; height: 199px; width: 397px" 
            Text="Correction Required: " Visible="False"></asp:Label>
        <asp:DropDownList ID="ddlOperatorCharacter3" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlOperatorCharacter3_SelectedIndexChanged" 
            style="z-index: 1; left: 224px; top: 360px; position: absolute; width: 123px" 
            Visible="False">
        </asp:DropDownList>


        <asp:DropDownList ID="ddlOperatorCharacter2" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlOperatorCharacter2_SelectedIndexChanged" 
            style="z-index: 1; left: 224px; top: 380px; position: absolute; width: 123px" 
            Visible="False">
        </asp:DropDownList>


    <asp:DropDownList ID="ddlGrades2" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades2_SelectedIndexChanged" 
        style="z-index: 1; left: 348px; top: 317px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSearchValue2Bool" runat="server" 
        style="z-index: 1; left: 349px; top: 318px; position: absolute; width: 179px" 
        Visible="False" BackColor="#FFD200" AutoPostBack="True" 
        CausesValidation="True" 
        onselectedindexchanged="ddlSearchValue2Bool_SelectedIndexChanged">
    </asp:DropDownList>



    <asp:Label ID="lblMonth" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 745px; top: 300px; position: absolute; width: 61px" 
        Text="Month" Visible="False"></asp:Label>
    <asp:Label ID="Label1" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 828px; top: 300px; position: absolute; width: 41px;" 
        Text="Day" Visible="False"></asp:Label>
    <asp:Label ID="lblYear" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 881px; top: 300px; position: absolute; height: 12px; width: 51px;" 
        Text="Year" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlPickDateRangeMonth1" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 269px; position: absolute; width: 74px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeMonth1_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <asp:DropDownList ID="ddlPickDateRangeDay1" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 421px; top: 269px; position: absolute; width: 48px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeDay1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear1" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 469px; top: 269px; position: absolute; width: 59px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeYear1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label ID="lblTo" runat="server" Font-Bold="True" 
        style="z-index: 1; left: 944px; top: 272px; position: absolute" 
        Text="To: " Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlPickDateRangeMonth2" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 977px; top: 269px; position: absolute; width: 76px; right: 18px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeMonth2_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1059px; top: 269px; position: absolute; width: 47px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeDay2_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1112px; top: 269px; position: absolute; width: 69px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeYear2_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:Label ID="lblStartDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 740px; top: 320px; position: absolute; width: 68px" 
        visible="false"
        Text="(Start Date)"></asp:Label>
    <asp:Label ID="lblEndDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 1056px; top: 321px; position: absolute; height: 17px; width: 62px" 
        visible="false"
        Text="(End Date)"></asp:Label>
    <asp:Label ID="lblSetDateRange" runat="server" Font-Bold="True" 
        style="z-index: 1; left: 866px; top: 330px; position: absolute; width: 163px" 
        visible="false"
        Text="(Specify a Date/Range)"></asp:Label>
    <asp:Label ID="lblAnotherMonth" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 1220px; top: 196px; position: absolute; width: 55px;" 
        Text="Month" Visible="False"></asp:Label>
    <asp:Label ID="lblAnotherDay" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 1236px; top: 246px; position: absolute; width: 50px;" 
        Text="Day" Visible="False"></asp:Label>
    <asp:Label ID="lblAnotherYear" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 1237px; top: 221px; position: absolute; width: 46px;" 
        Text="Year" Visible="False"></asp:Label>
    <asp:Label ID="lblAnotherOptional" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 1048px; top: 267px; position: absolute; width: 87px;" 
        visible="false"
        Text="(Optional)"></asp:Label>
    <asp:Label ID="lblAnotherOne" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 809px; top: 268px; position: absolute" 
        visible="false"
        Text="(Optional)"></asp:Label>
    <asp:Label ID="lblAnotherOneYet" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 1204px; top: 526px; position: absolute" 
        Text="(Optional)" Visible="False"></asp:Label>
    <asp:Label ID="lblOptionalAnother" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 930px; top: 313px; position: absolute" 
        visible="false"
        Text="(Optional)"></asp:Label>


    <asp:DropDownList ID="ddlCriminalCheckCodes" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1041px; top: 310px; position: absolute; width: 130px" 
        Visible="False" 
        onselectedindexchanged="ddlCriminalCheckCodes_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True">
    </asp:DropDownList>
    <asp:TextBox ID="txbComments2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 577px; top: 222px; position: absolute; height: 44px; width: 552px; bottom: 323px;" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" Visible="False"></asp:TextBox>
    <asp:DropDownList ID="ddlVehichleInsuranceCodes" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 577px; top: 310px; position: absolute; width: 130px" 
        Visible="False" 
        onselectedindexchanged="ddlVehichleInsuranceCodes_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlBackgroundCheckCodes" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 920px; top: 157px; position: absolute; width: 130px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Image ID="imgImage" runat="server" 
        style="z-index: 1; left: 31px; top: 258px; position: absolute; height: 124px; width: 176px" 
        Visible="False" />
    <asp:DropDownList ID="ddlNames" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlNames_SelectedIndexChanged" 
        style="z-index: 1; left: 32px; top: 233px; position: absolute; width: 175px">
    </asp:DropDownList>
    <asp:Label ID="lblComments2" runat="server" 
        style="z-index: 1; left: 581px; top: 204px; position: absolute; width: 105px" 
        Text="Comments" Visible="False"></asp:Label>

    <asp:Calendar ID="calDate" runat="server" 
        onselectionchanged="calDate_SelectionChanged" 
        style="z-index: 1; left: 358px; top: 350px; position: absolute; height: 95px; width: 168px" 
        Visible="False" BackColor="#FFD200" ShowGridLines="True">
    </asp:Calendar>
     
    <div>
        <asp:ImageButton ID="imgCalender" runat="server" onclick="imgCalender_Click" 
        
            style="z-index: 1; left: 881px; top: 168px; position: absolute; height: 19px; width: 28px; right: 205px;" 
            BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" Visible="False" />
    </div>
    <asp:TextBox ID="txbBackgroundCheckDate" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 943px; top: 181px; position: absolute; width: 107px; right: 68px;" 
        Enabled="False" BorderColor="#666666" BorderStyle="Solid" 
        BorderWidth="2px" Visible="False"></asp:TextBox>
    <asp:Button ID="cmbUpdateInformation" runat="server" 
        onclick="cmbUpdateInformation_Click" 
        style="z-index: 1; left: 229px; top: 354px; position: absolute; height: 44px; width: 160px" 
        Text="Update Information" Visible="False" />
    <asp:Calendar ID="calNationalCheckDate" runat="server" BackColor="#FFD200" 
        onselectionchanged="calNationalCheckDate_SelectionChanged" ShowGridLines="True" 
        style="z-index: 1; left: 684px; top: 772px; position: absolute; height: 69px; width: 153px" 
        Visible="False" BorderColor="#666666" BorderStyle="Solid" 
        BorderWidth="2px"></asp:Calendar>
    <asp:TextBox ID="txbVehichleInsurDate" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 576px; top: 383px; position: absolute; width: 108px; right: 567px" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" ReadOnly="True" 
        Visible="False" ontextchanged="txbVehichleInsurDate_TextChanged">Select a date.</asp:TextBox>
    <asp:ImageButton ID="imbVehichleInsurDate" runat="server" 
        onclick="imbVehichleInsurDate_Click" 
        
        style="z-index: 1; left: 688px; top: 392px; position: absolute; width: 28px; height: 19px" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
        ToolTip="Vehichle Insurance Date" Visible="False" />
    <asp:TextBox ID="txbNationalCheckDate" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 729px; top: 395px; position: absolute; width: 108px" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" ReadOnly="True" 
        Visible="False">Select a date.</asp:TextBox>
    <asp:Calendar ID="calVehichleInsuranceDate" runat="server" BackColor="#FFD200" 
        onselectionchanged="calVehichleInsuranceDate_SelectionChanged" 
        ShowGridLines="True" 
        style="z-index: 1; left: 544px; top: 773px; position: absolute; height: 63px; width: 221px" 
        Visible="False" BorderColor="#666666" BorderStyle="Solid" 
        BorderWidth="2px"></asp:Calendar>
    <asp:TextBox ID="txbDMVCheckDate" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 885px; top: 401px; position: absolute; width: 108px; right: 258px;" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" ReadOnly="True" 
        Visible="False">Select a date.</asp:TextBox>
    <asp:TextBox ID="txbPACriminalCheckDate" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1041px; top: 407px; position: absolute; width: 108px; right: 102px;" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" ReadOnly="True" 
        Visible="False">Select a date.</asp:TextBox>
    <asp:ImageButton ID="imbNationalCheckDate" runat="server" 
        onclick="imbNationalCheckDate_Click" 
        
        style="z-index: 1; left: 842px; top: 390px; position: absolute; height: 19px; width: 28px" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
        ToolTip="National Check Date" Visible="False" />
    <asp:ImageButton ID="imbDMVCheckDate" runat="server" 
        onclick="imbDMVCheckDate_Click" 
        
        style="z-index: 1; left: 1002px; top: 394px; position: absolute; height: 19px; width: 28px; right: 217px;" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
        ToolTip="DMV Check Date" Visible="False" />
    <asp:ImageButton ID="imbPACriminalCheckDate" runat="server" 
        onclick="imbPACriminalCheckDate_Click" 
        
        style="z-index: 1; left: 1152px; top: 405px; position: absolute; height: 19px; width: 28px" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
        ToolTip="PA Criminal Check Date" Visible="False" />
    <asp:Calendar ID="calPACriminalDate" runat="server" BackColor="#FFD200" 
        onselectionchanged="calPACriminalDate_SelectionChanged" ShowGridLines="True" 
        style="z-index: 1; left: 1076px; top: 774px; position: absolute; height: 97px; width: 184px" 
        Visible="False" BorderColor="#666666" BorderStyle="Solid" 
        BorderWidth="2px"></asp:Calendar>
    <asp:Calendar ID="calDMVCheckDate" runat="server" BackColor="#FFD200" 
        onselectionchanged="calDMVCheckDate_SelectionChanged" 
        style="z-index: 1; left: 866px; top: 774px; position: absolute; height: 151px; width: 167px" 
        Visible="False" BorderColor="#666666" BorderStyle="Solid" 
        BorderWidth="2px" ShowGridLines="True"></asp:Calendar>
    <asp:TextBox ID="txbNewVolunteerTrainingDate" runat="server" 
        BackColor="#FFD200" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
        ReadOnly="True" 
        style="z-index: 1; left: 229px; top: 328px; position: absolute; width: 115px" 
        Visible="False">Select a date.</asp:TextBox>
    <asp:TextBox ID="txbBackgroundCheckPAIDDate" runat="server" BackColor="#FFD200" 
        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" ReadOnly="True" 
        style="z-index: 1; left: 415px; top: 329px; position: absolute; width: 107px" 
        Visible="False">Select a date.</asp:TextBox>
    <asp:ImageButton ID="imbNewVolunteerTrainingDate" runat="server" 
        onclick="imbNewVolunteerTrainingDate_Click" 
        style="z-index: 1; left: 350px; top: 329px; position: absolute; width: 28px; height: 19px" 
        ToolTip="New Volunteer Training Date" Visible="False" />
    <asp:Calendar ID="calNewVolunteerTrainingDate" runat="server" 
        BackColor="#FFD200" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
        onselectionchanged="calNewVolunteerTrainingDate_SelectionChanged" 
        ShowGridLines="True" 
        style="z-index: 1; left: 205px; top: 348px; position: absolute; height: 152px; width: 119px" 
        Visible="False"></asp:Calendar>
    <asp:ImageButton ID="imbBackgroundCheckPAIDDate" runat="server" 
        onclick="imbBackgroundCheckPAIDDate_Click" 
        style="z-index: 1; left: 526px; top: 330px; position: absolute; width: 28px; height: 19px" 
        ToolTip="Background Check PAID Date" Visible="False" />
    <asp:Calendar ID="calBackgroundCheckPAIDDate" runat="server" 
        BackColor="#FFD200" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
        onselectionchanged="calBackgroundCheckPAIDDate_SelectionChanged" 
        ShowGridLines="True" 
        style="z-index: 1; left: 414px; top: 774px; position: absolute; height: 106px; width: 141px" 
        Visible="False"></asp:Calendar>
    <asp:LinkButton ID="lbVolunteerProfilePage" runat="server" 
        onclick="lbVolunteerProfilePage_Click" 
        style="z-index: 1; left: 42px; top: 385px; position: absolute" 
        Visible="False">(Volunteer Profile Page)</asp:LinkButton>
    <asp:Button ID="cmbNationalCheckReport" runat="server" 
        onclick="cmbNationalCheckReport_Click" 
        style="z-index: 1; left: 1038px; top: 200px; position: absolute; height: 37px; width: 170px" 
        Text="National Check Report" Visible="False" />
    <asp:DropDownList ID="ddlDateOfBirthDay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 89px; top: 409px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlDateOfBirthMonth" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 39px; top: 409px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlDateOfBirthYear" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 137px; top: 409px; position: absolute; width: 57px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblDateOfBirth" runat="server" 
        style="z-index: 1; left: 66px; top: 432px; position: absolute; width: 137px" 
        Text="Date Of Birth" Visible="False"></asp:Label>
    <asp:Button ID="cmbDMVCheckReport" runat="server" 
        onclick="cmbDMVCheckReport_Click" 
        style="z-index: 1; left: 1038px; top: 246px; position: absolute; height: 39px; width: 169px" 
        Text="DMV Check Report" Visible="False" />
    <asp:DropDownList ID="ddlPACriminalCodes" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlPACriminalCodes_SelectedIndexChanged" 
        style="z-index: 1; left: 41px; top: 578px; position: absolute; width: 168px" 
        Visible="False">
    </asp:DropDownList>


    <asp:DropDownList ID="ddlChooseField" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 19px; top: 269px; position: absolute; width: 187px" 
        TabIndex="2" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField_SelectedIndexChanged1" 
        Visible="False">
    </asp:DropDownList>
    <br />
    <asp:DropDownList ID="ddlChooseOperator" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 216px; top: 269px; position: absolute; width: 123px; right: 579px;" 
        TabIndex="3" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseDate" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 216px; top: 269px; position: absolute; width: 123px; right: 579px;" 
        TabIndex="3" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseDate_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>

    <asp:TextBox ID="txbSearchValue" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 269px; position: absolute; width: 173px" 
        TabIndex="4" AutoPostBack="True" CausesValidation="True" 
        ontextchanged="txbSearchValue_TextChanged" Visible="False"></asp:TextBox>

    <asp:DropDownList ID="ddlPickDateRangeMonth22" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 317px; position: absolute; width: 74px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeMonth22_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay22" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 421px; top: 317px; position: absolute; width: 48px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeDay22_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear22" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 469px; top: 317px; position: absolute; width: 59px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeYear22_SelectedIndexChanged">
    </asp:DropDownList>


    <asp:TextBox ID="txbSearchValue2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 317px; position: absolute; width: 173px" 
        ontextchanged="txbSearchValue2_TextChanged" TabIndex="8" 
        AutoPostBack="True" CausesValidation="True" Enabled="False" 
        Visible="False"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 365px; position: absolute; width: 173px" 
        TabIndex="12" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue3_TextChanged" Visible="False"></asp:TextBox>

    <asp:DropDownList ID="ddlPickDateRangeMonth3" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 365px; position: absolute; width: 74px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeMonth3_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 421px; top: 365px; position: absolute; width: 48px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeDay3_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 469px; top: 365px; position: absolute; width: 59px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeYear3_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:TextBox ID="txbSearchValue4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 410px; position: absolute; width: 173px" 
        TabIndex="16" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue4_TextChanged" Visible="False"></asp:TextBox>

    <asp:DropDownList ID="ddlPickDateRangeMonth4" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 410px; position: absolute; width: 74px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeMonth4_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 421px; top: 410px; position: absolute; width: 48px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeDay4_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 469px; top: 410px; position: absolute; width: 59px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeYear4_SelectedIndexChanged">
    </asp:DropDownList>


    <asp:TextBox ID="txbSearchValue5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 346px; top: 458px; position: absolute; width: 173px" 
        TabIndex="20" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue5_TextChanged" Visible="False"></asp:TextBox>

    <asp:DropDownList ID="ddlPickDateRangeMonth5" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 458px; position: absolute; width: 74px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeMonth5_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 421px; top: 458px; position: absolute; width: 48px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeDay5_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 469px; top: 458px; position: absolute; width: 59px" 
        visible="false"
        onselectedindexchanged="ddlPickDateRangeYear5_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:RadioButtonList ID="rblNumber1" runat="server" 
        style="z-index: 1; left: 17px; top: 283px; position: absolute; height: 41px; width: 113px" 
        RepeatDirection="Horizontal" TabIndex="5" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber1_SelectedIndexChanged" Visible="False">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:DropDownList ID="ddlChooseField2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 19px; top: 317px; position: absolute; width: 187px" 
        TabIndex="6" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField2_SelectedIndexChanged" 
        Enabled="False" Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 216px; top: 317px; position: absolute; width: 123px" 
        TabIndex="7" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator2_SelectedIndexChanged" Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseDate2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 216px; top: 317px; position: absolute; width: 123px" 
        TabIndex="7" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseDate2_SelectedIndexChanged" Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseField3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 18px; top: 364px; position: absolute; width: 187px" 
        TabIndex="10" 
        onselectedindexchanged="ddlChooseField3_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True" Enabled="False" Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 17px; top: 410px; position: absolute; width: 187px" 
        TabIndex="14" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField4_SelectedIndexChanged1" 
        Enabled="False" Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 16px; top: 459px; position: absolute; width: 187px" 
        TabIndex="18" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField5_SelectedIndexChanged1" 
        Enabled="False" Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 215px; top: 364px; position: absolute; width: 123px; " 
        TabIndex="11" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator3_SelectedIndexChanged" Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseDate3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 215px; top: 364px; position: absolute; width: 123px; " 
        TabIndex="11" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseDate3_SelectedIndexChanged" Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlGrades3" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades3_SelectedIndexChanged" 
        style="z-index: 1; left: 348px; top: 364px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseOperator4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 214px; top: 410px; position: absolute; width: 123px; " 
        TabIndex="15" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator4_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseDate4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 214px; top: 410px; position: absolute; width: 123px; " 
        TabIndex="15" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseDate4_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseOperator5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 213px; top: 458px; position: absolute; width: 123px; right: 563px;" 
        TabIndex="19" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator5_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseDate5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 213px; top: 458px; position: absolute; width: 123px; right: 563px;" 
        TabIndex="19" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseDate5_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>


    <asp:RadioButtonList ID="rblNumber2" runat="server" 
        style="z-index: 1; left: 17px; top: 332px; position: absolute; height: 41px; width: 116px" 
        RepeatDirection="Horizontal" TabIndex="9" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber2_SelectedIndexChanged" Visible="False">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber3" runat="server" 
        style="z-index: 1; left: 17px; top: 385px; position: absolute; height: 27px; width: 122px" 
        RepeatDirection="Horizontal" TabIndex="13" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber3_SelectedIndexChanged" Visible="False">
        <asp:ListItem Value="AND"></asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber4" runat="server" 
        style="z-index: 1; left: 17px; top: 432px; position: absolute; height: 27px; width: 122px" 
        RepeatDirection="Horizontal" TabIndex="17" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber4_SelectedIndexChanged" Visible="False">
        <asp:ListItem Value="AND"></asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>


    <asp:DropDownList ID="ddlGrades" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades_SelectedIndexChanged" 
        style="z-index: 1; left: 349px; top: 269px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGrades4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades4_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 411px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGrades5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades5_SelectedIndexChanged" 
        style="z-index: 1; left: 346px; top: 459px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean_SelectedIndexChanged" 
        style="z-index: 1; left: 216px; top: 269px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean2" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean2_SelectedIndexChanged" 
        style="z-index: 1; left: 215px; top: 317px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean3" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean3_SelectedIndexChanged" 
        style="z-index: 1; left: 215px; top: 364px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean4_SelectedIndexChanged" 
        style="z-index: 1; left: 214px; top: 410px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean5_SelectedIndexChanged" 
        style="z-index: 1; left: 214px; top: 459px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorCharacter_SelectedIndexChanged" 
        style="z-index: 1; left: 216px; top: 269px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorCharacter4_SelectedIndexChanged" 
        style="z-index: 1; left: 214px; top: 410px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorCharacter5_SelectedIndexChanged" 
        style="z-index: 1; left: 213px; top: 459px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>


    <asp:GridView ID="gvCustomView" runat="server" 
        BackColor="#FFD200"
        style="z-index: 1; left: -41px; top: 313px; position: absolute; height: 191px; width: 495px;" 
        onselectedindexchanged="gvCustomView_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvCustomView_RowCommand" 
        AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>

    <asp:GridView ID="gvAddressView" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: 6px; top: 255px; position: absolute; height: 153px; width: 1255px" 
        onselectedindexchanged="gvAddressView_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvAddressView_RowCommand" 
        AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
        <Columns>
                <asp:ButtonField ButtonType="Link" DataTextField="Name" HeaderText="Name" SortExpression="Name"   CausesValidation="true"  CommandName="Name"  />
                <asp:BoundField  DataField="Address" HeaderText="Address" ReadOnly="true"  SortExpression="Address"  />
                <asp:BoundField  DataField="City" HeaderText="City" ReadOnly="true"  SortExpression="City"  />
                <asp:BoundField  DataField="State" HeaderText="State" ReadOnly="true"  SortExpression="State"  />
                <asp:BoundField  DataField="Zip" HeaderText="Zip" ReadOnly="true"  SortExpression="Zip"  />
                <asp:BoundField  DataField="HomePhone" HeaderText="HomePhone" ReadOnly="true"  SortExpression="HomePhone"  />
                <asp:BoundField  DataField="CellPhone" HeaderText="CellPhone" ReadOnly="true"  SortExpression="CellPhone"  />
                <asp:BoundField  DataField="School" HeaderText="School" ReadOnly="true"  SortExpression="School"  />                
        </Columns>
    </asp:GridView>


    <asp:Label ID="lblFindRecords" runat="server" 
        style="z-index: 1; left: 21px; top: 247px; position: absolute; width: 274px" 
        Text="Find volunteer records where: " Visible="False"></asp:Label>

    <asp:Label ID="lblViewChoices" runat="server" 
    style="z-index: 1; left: 47px; top: 186px; position: absolute; width: 183px" 
    Text="OR" Visible="False"></asp:Label>

    <asp:DropDownList ID="ddlAdvancedSearchView" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 20px; top: 206px; position: absolute; width: 187px" 
        Visible="False" 
        onselectedindexchanged="ddlAdvancedSearchView_SelectedIndexChanged">
    </asp:DropDownList>


    <asp:Label ID="lblReporting" runat="server" Font-Bold="False" Font-Size="22pt" 
        Font-Underline="True" 
        style="z-index: 1; left: 500px; top: 230px; position: absolute; height: 41px; width: 372px" 
        Text="Background Reporting" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlDMVCheckReport" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlDMVCheckReport_SelectedIndexChanged" 
        style="z-index: 1; left: 40px; top: 500px; position: absolute; width: 168px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblDMVCheckReport" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 42px; top: 586px; position: absolute; width: 126px" 
        Text="DMV Check Codes" Visible="False"></asp:Label>
    <asp:Label ID="lblNationalCheckReport" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 42px; top: 545px; position: absolute; width: 193px; bottom: 346px" 
        Text="National Check Codes" Visible="False"></asp:Label>
    <asp:Label ID="lblPACriminalCheckReport" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 42px; top: 503px; position: absolute; width: 148px" 
        Text="PA Criminal Check Codes" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlBackgroundCheckPaidCode" runat="server" 
        AutoPostBack="True" BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlBackgroundCheckPaidCode_SelectedIndexChanged" 
        style="z-index: 1; left: 416px; top: 329px; position: absolute; width: 138px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlNationalMonth" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 733px; top: 331px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlNationalDay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 771px; top: 331px; position: absolute; width: 41px; right: 439px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlNationalYear" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 810px; top: 331px; position: absolute; width: 54px; height: 22px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlDMVMonth" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 887px; top: 331px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlDMVDay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 926px; top: 331px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlDMVYear" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 965px; top: 331px; position: absolute; width: 54px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPACriminalMonth" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1041px; top: 331px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPACriminalDay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1080px; top: 331px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPACriminalYear" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1118px; top: 331px; position: absolute; width: 54px; right: 79px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlVehichleMonth" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 577px; top: 331px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlVehichleDay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 616px; top: 331px; position: absolute; width: 41px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlVehichleYear" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 655px; top: 331px; position: absolute; width: 54px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Panel ID="pnlCustomList" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="4px" Visible="False">
        <asp:Button ID="cmbConfirmCustomViewFields" runat="server" 
            onclick="cmbConfirmCustomViewFields_Click" 
            style="z-index: 1; left: 835px; top: 166px; position: absolute; height: 61px; width: 161px" 
            Text="Confirm Fields" Visible="False" />
        <asp:CheckBoxList ID="chbCustomList" runat="server" 
            style="z-index: 1; left: 34px; top: 65px; position: absolute; height: 242px; width: 801px" 
            Visible="False" RepeatColumns="4">
            <asp:ListItem>LastName</asp:ListItem>
            <asp:ListItem>FirstName</asp:ListItem>
            <asp:ListItem>Address</asp:ListItem>
            <asp:ListItem>City</asp:ListItem>
            <asp:ListItem>State</asp:ListItem>
            <asp:ListItem>Zip</asp:ListItem>
            <asp:ListItem>HomePhone</asp:ListItem>
            <asp:ListItem>CellPhone</asp:ListItem>
            <asp:ListItem>Church</asp:ListItem>
            <asp:ListItem>GeneralInformation</asp:ListItem>
            <asp:ListItem>SpiritualJourney</asp:ListItem>
            <asp:ListItem>ReleaseWaiver</asp:ListItem>
            <asp:ListItem>NewVolunteerTraining</asp:ListItem>
            <asp:ListItem>BackgroundCheckPAID</asp:ListItem>
            <asp:ListItem>BackgroundCheckPAIDCode</asp:ListItem>
            <asp:ListItem>VehichleInsurance</asp:ListItem>
            <asp:ListItem>VehichleInsuranceCodes</asp:ListItem>
            <asp:ListItem>DMVCheckCodesVehichleInsuranceDate</asp:ListItem>
            <asp:ListItem>DMVCheck</asp:ListItem>
            <asp:ListItem>DMVCheckCodes</asp:ListItem>
            <asp:ListItem>DMVCheckDate</asp:ListItem>
            <asp:ListItem>PACriminalCheck</asp:ListItem>
            <asp:ListItem>PACriminalCheckCodes</asp:ListItem>
            <asp:ListItem>PACriminalCheckDate</asp:ListItem>
            <asp:ListItem>NationalCheck</asp:ListItem>
            <asp:ListItem>NationalCheckCodes</asp:ListItem>
            <asp:ListItem>NationalCheckDate</asp:ListItem>
            <asp:ListItem>MSHSChoir</asp:ListItem>
            <asp:ListItem>ChildrensChoir</asp:ListItem>
            <asp:ListItem>PerformingArts</asp:ListItem>
            <asp:ListItem>Shakes</asp:ListItem>
            <asp:ListItem>Singers</asp:ListItem>
            <asp:ListItem>OutreachBasketball</asp:ListItem>
            <asp:ListItem>BasketballTEAMS</asp:ListItem>
            <asp:ListItem>HSBasketballLg</asp:ListItem>
            <asp:ListItem>MSBasketballLg</asp:ListItem>
            <asp:ListItem>Baseball</asp:ListItem>
            <asp:ListItem>3on3Basketball</asp:ListItem>
            <asp:ListItem>SoccerIntraMurals</asp:ListItem>
            <asp:ListItem>SoccerTEAMS</asp:ListItem>
            <asp:ListItem>BibleStudy</asp:ListItem>
            <asp:ListItem>MondayNights</asp:ListItem>
            <asp:ListItem>SpecialEvents</asp:ListItem>
            <asp:ListItem>ImpactUrbanSchools</asp:ListItem>
            <asp:ListItem>AcademicReadingSupport</asp:ListItem>
            <asp:ListItem>SummerDayCamp</asp:ListItem>
        </asp:CheckBoxList>
        <asp:Button ID="cmbClearViewFields" runat="server" 
            onclick="cmbClearViewFields_Click" 
            style="z-index: 1; left: 837px; top: 73px; position: absolute; height: 58px; width: 160px" 
            Text="Clear Selections" Visible="False" />
        <asp:Button ID="cmbCancelCustomViewFields" runat="server" 
            onclick="cmbCancelCustomViewFields_Click" 
            style="z-index: 1; left: 834px; top: 261px; position: absolute; height: 57px; width: 158px" 
            Text="Cancel Field Selection" Visible="False" />
        <asp:Label ID="lblCustomView" runat="server" Font-Bold="True" Font-Size="24pt" 
            style="z-index: 1; left: 61px; top: 18px; position: absolute; height: 47px; width: 923px; text-decoration: underline" 
            Text="Create your custom View by checking fields below" Visible="False"></asp:Label>
    </asp:Panel>
    <asp:Button ID="cmbCreateCustomView" runat="server" 
        onclick="cmbCreateCustomView_Click1" 
        style="z-index: 1; left: 20px; top: 155px; position: absolute; height: 28px; width: 176px" 
        Text="Create a Custom View" Visible="False" />
    <asp:CheckBox ID="chbActiveNonActive" runat="server" 
        style="z-index: 1; left: 918px; top: 743px; position: absolute; width: 186px" 
        Visible="False" />
    <asp:Button ID="cmbRetrieveResults" runat="server" 
        onclick="cmbRetrieveResults_Click" 
        style="z-index: 1; left: 549px; top: 269px; position: absolute; height: 39px; width: 175px" 
        Text="Retrieve Query Results" Visible="False" />
    </form>
</body>
</html>
