<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="VolunteerSearchQueries.aspx.cs" Inherits="UIF.PerformingArts.VolunteerSearchQueries" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

<style type="text/css"> 
   div { z-index: 1;
        left: 24px;
        top: 249px;
        position: absolute;
        height: 291px;
        width: 1223px;
    } 
</style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="Orange">
    <form id="form2" runat="server" defaultfocus="txbSearch" defaultbutton="cmbSearch">
    <div>
    
    </div>

    <asp:Panel ID="pnlBackground" runat="server" BackColor="White"  
        style="z-index: 1; left: -12px; top: 1px; position: absolute; height: 133px; width: 1298px" 
        ViewStateMode="Enabled" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px">
        <asp:Label ID="lblUrbanImpact" runat="server" Font-Bold="True" Font-Size="36pt" 
            style="z-index: 1; left: 354px; top: 24px; position: absolute; height: 62px; width: 547px" 
            Text="Urban Impact Foundation"></asp:Label>
        <asp:DropDownList ID="ddlOperatorBoolean2" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" Enabled="False" 
            onselectedindexchanged="ddlOperatorBoolean2_SelectedIndexChanged" 
            style="z-index: 1; left: 222px; top: 328px; position: absolute; width: 128px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlOperatorBoolean3" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" Enabled="False" 
            onselectedindexchanged="ddlOperatorBoolean3_SelectedIndexChanged" 
            style="z-index: 1; left: 222px; top: 378px; position: absolute; width: 128px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlOperatorCharacter2" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlOperatorCharacter2_SelectedIndexChanged" 
            style="z-index: 1; left: 223px; top: 328px; position: absolute; width: 128px" 
            Visible="False">
        </asp:DropDownList>
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
            VerticalPadding="4px" />

        <DynamicMenuStyle BackColor="#FFD200" Width="200px" Height="175px" />
        <DynamicSelectedStyle VerticalPadding="4px" Width="40px" />

         <DynamicItemTemplate>
             <%# Eval("Text") %>
        </DynamicItemTemplate>

        <StaticHoverStyle BackColor="#FFD200" Font-Bold="True" Font-Italic="True" 
            Font-Size="15pt" Height="20px" />
        <StaticMenuItemStyle BackColor="White" />
        <StaticMenuStyle BackColor="White" />
    </asp:Menu>          

    <asp:DropDownList ID="ddlSearchValueBool" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlSearchValueBool_SelectedIndexChanged" 
        style="z-index: 1; left: 348px; top: 286px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSearchValue2Bool" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlSearchValue2Bool_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 333px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSearchValue3Bool" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlSearchValue3Bool_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 381px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSearchValue4Bool" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlSearchValue4Bool_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 435px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSearchValue5Bool" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlSearchValue5Bool_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 485px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>

    <asp:ImageButton ID="imgButton" runat="server"  ImageUrl="~/PerformingArts/Picture1.png"
        style="z-index: 1; left: 25px; top: 13px; position: absolute; height: 114px; width: 172px; right: 722px;" 
        onclick="imgButton_Click" />

    <asp:Label ID="lblVolunteerSearchQueries" runat="server" Font-Size="25pt" 
        style="z-index: 1; left: 454px; top: 165px; position: absolute; height: 50px; width: 372px; text-decoration: underline; right: 216px; font-weight: 700;" 
        Text="Volunteer Search/Queries"></asp:Label>
    <asp:TextBox ID="txbSearch" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 247px; top: 253px; position: absolute; width: 763px"></asp:TextBox>
    <asp:Button ID="cmbSearch" runat="server" 
        style="z-index: 1; left: 549px; top: 287px; position: absolute; width: 191px" 
        Text="Search" onclick="cmbSearch_Click" />

    <asp:GridView ID="gvVolunteerSearchResults" runat="server" BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: 155px; top: 155px; position: absolute; height: 153px; width: 949px" 
        onselectedindexchanged="gvVolunteerSearchResults_RowCommand" EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvVolunteerSearchResults_RowCommand" 
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
                <asp:BoundField  DataField="Email" HeaderText="Email" ReadOnly="true"  SortExpression="Email"  />                
        </Columns>
    </asp:GridView>

    <asp:DropDownList ID="ddlChooseField" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 23px; top: 286px; position: absolute; width: 187px" 
        TabIndex="2" onselectedindexchanged="ddlChooseField_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 22px; top: 332px; position: absolute; width: 187px" 
        TabIndex="6" onselectedindexchanged="ddlChooseField2_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True" Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 21px; top: 382px; position: absolute; width: 187px; right: 715px;" 
        TabIndex="10" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField3_SelectedIndexChanged1" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 20px; top: 434px; position: absolute; width: 187px" 
        TabIndex="14" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField4_SelectedIndexChanged" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 20px; top: 485px; position: absolute; width: 187px" 
        TabIndex="18" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField5_SelectedIndexChanged" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 215px; top: 286px; position: absolute; width: 128px" 
        TabIndex="3" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator_SelectedIndexChanged" Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 214px; top: 332px; position: absolute; width: 128px" 
        TabIndex="7" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator2_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 213px; top: 381px; position: absolute; width: 128px; " 
        TabIndex="11" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator3_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 213px; top: 434px; position: absolute; width: 128px; " 
        TabIndex="15" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator4_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 212px; top: 485px; position: absolute; width: 128px" 
        TabIndex="19" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator5_SelectedIndexChanged" 
        Visible="False">
    </asp:DropDownList>




    <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
        style="z-index: 1; left: 549px; top: 321px; position: absolute; height: 26px; width: 191px" 
        Text="Reset Search" />
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 549px; top: 357px; position: absolute; height: 26px; width: 191px" 
        Text="Export to Excel" Enabled="False" />


    <asp:TextBox ID="txbSearchValue" runat="server" BackColor="#FFD200" 
        
        
        style="z-index: 1; left: 347px; top: 286px; position: absolute; width: 173px" 
        TabIndex="4"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue2" runat="server" BackColor="#FFD200" 
        
        
        style="z-index: 1; left: 347px; top: 332px; position: absolute; width: 173px; " 
        TabIndex="8"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue3" runat="server" BackColor="#FFD200" 
        
        
        style="z-index: 1; left: 346px; top: 382px; position: absolute; width: 173px" 
        TabIndex="12"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 346px; top: 434px; position: absolute; width: 173px" 
        TabIndex="16"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue5" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 345px; top: 485px; position: absolute; width: 173px" 
        TabIndex="20"></asp:TextBox>
    <asp:RadioButtonList ID="rblNumber1" runat="server" 
        RepeatDirection="Horizontal" 
        style="z-index: 1; left: 23px; top: 307px; position: absolute; height: 20px; width: 182px" 
        TabIndex="5" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber1_SelectedIndexChanged">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem>OR</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber2" runat="server" BackColor="Orange" 
        RepeatDirection="Horizontal" 
        style="z-index: 1; left: 24px; top: 356px; position: absolute; height: 16px; width: 180px; bottom: 139px" 
        TabIndex="9" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber2_SelectedIndexChanged">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem>OR</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber3" runat="server" BackColor="Orange" 
        RepeatDirection="Horizontal" 
        style="z-index: 1; left: 26px; top: 404px; position: absolute; height: 29px; width: 176px" 
        TabIndex="13" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber3_SelectedIndexChanged">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem>OR</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber4" runat="server" BackColor="Orange" 
        RepeatDirection="Horizontal" 
        style="z-index: 1; left: 26px; top: 458px; position: absolute; height: 28px; width: 179px" 
        TabIndex="17" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber4_SelectedIndexChanged">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem>OR</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="lblFindRecords" runat="server" 
        style="z-index: 1; left: 23px; top: 261px; position: absolute; width: 274px" 
        Text="Find volunteer records where: "></asp:Label>
    <asp:Button ID="cmbAdvancedVolunteer" runat="server" 
        onclick="cmbAdvancedVolunteer_Click1" 
        style="z-index: 1; left: 549px; top: 287px; position: absolute; width: 191px;" 
        Text="Advanced Search" TabIndex="20" />
    <asp:CheckBox ID="chbAdvancedSearch" runat="server" AutoPostBack="True" 
        CausesValidation="True" Checked="True" 
        oncheckedchanged="chbAdvancedSearch_CheckedChanged" 
        style="z-index: 1; left: 746px; top: 285px; position: absolute" 
        Text="Advanced Search" />

    <asp:GridView ID="gvAdvancedVolunteerResults" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: 1px; top: 230px; position: absolute; height: 153px; width: 1249px" 
        onselectedindexchanged="gvAdvancedVolunteerResults_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvAdvancedVolunteerResults_RowCommand" 
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
                <asp:BoundField  DataField="Email" HeaderText="Email" ReadOnly="true"  SortExpression="Email"  />                
        </Columns>
    </asp:GridView>

    <asp:GridView ID="gvAddressView" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: -5px; top: 280px; position: absolute; height: 153px; width: 1249px" 
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
                <asp:BoundField  DataField="Email" HeaderText="Email" ReadOnly="true"  SortExpression="Email"  />                
        </Columns>
    </asp:GridView>

    <asp:GridView ID="gvPersonalView" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: -5px; top: 280px; position: absolute; height: 153px; width: 1249px" 
        onselectedindexchanged="gvPersonalView_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvPersonalView_RowCommand" 
        AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
        <Columns>
                <asp:ButtonField ButtonType="Link" DataTextField="Name" HeaderText="Name" SortExpression="Name"   CausesValidation="true"  CommandName="Name"  />
                <asp:BoundField  DataField="Email" HeaderText="Email" ReadOnly="true"  SortExpression="Email"  />
                <asp:BoundField  DataField="DOB" HeaderText="DOB" ReadOnly="true"  SortExpression="DOB"  />
                <asp:BoundField  DataField="Sex" HeaderText="Sex" ReadOnly="true"  SortExpression="Sex"  />
                <asp:BoundField  DataField="Church" HeaderText="Church" ReadOnly="true"  SortExpression="Church"  />
                <asp:BoundField  DataField="HealthConditions" HeaderText="HealthConditions" ReadOnly="true"  SortExpression="HealthConditions"  />
                <asp:BoundField  DataField="Notes" HeaderText="Notes" ReadOnly="true"  SortExpression="Notes"  />
        </Columns>
    </asp:GridView>

    <asp:GridView ID="gvProgramView" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: -5px; top: 280px; position: absolute; height: 153px; width: 1249px" 
        onselectedindexchanged="gvProgramView_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvProgramView_RowCommand" 
        AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
        <Columns>
                <asp:ButtonField ButtonType="Link" DataTextField="Name" HeaderText="Name" SortExpression="Name"   CausesValidation="true"  CommandName="Name"  />
                <asp:BoundField  DataField="BackgroundCheck" HeaderText="BackgroundCheck" ReadOnly="true"  SortExpression="BackgroundCheck"  />
                <asp:BoundField  DataField="SpiritualJourney" HeaderText="SpiritualJourney" ReadOnly="true"  SortExpression="SpiritualJourney"  />
                <asp:BoundField  DataField="VehichleInsurance" HeaderText="VehichleInsurance" ReadOnly="true"  SortExpression="VehichleInsurance"  />
                <asp:BoundField  DataField="ReleaseWaiver" HeaderText="ReleaseWaiver" ReadOnly="true"  SortExpression="ReleaseWaiver"  />
                <asp:BoundField  DataField="GeneralInformation" HeaderText="GeneralInformation" ReadOnly="true"  SortExpression="GeneralInformation"  />
                <asp:BoundField  DataField="NewVolunteerTraining" HeaderText="NewVolunteerTraining" ReadOnly="true"  SortExpression="NewVolunteerTraining"  />
        </Columns>
    </asp:GridView>

    <asp:GridView ID="gvDiscipleshipMentorView" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: -5px; top: 280px; position: absolute; height: 153px; width: 1249px" 
        onselectedindexchanged="gvDiscipleshipMentorView_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvDiscipleshipMentorView_RowCommand" 
        AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
        <Columns>
                <asp:ButtonField ButtonType="Link" DataTextField="Name" HeaderText="Name" SortExpression="Name"   CausesValidation="true"  CommandName="Name"  />
                <asp:BoundField  DataField="Discipleshipmentorparticipation" HeaderText="Discipleshipmentorparticipation" ReadOnly="true"  SortExpression="Discipleshipmentorparticipation"  />
                <asp:BoundField  DataField="Discipleshipmentortraining" HeaderText="Discipleshipmentortraining" ReadOnly="true"  SortExpression="Discipleshipmentortraining"  />
                <asp:BoundField  DataField="Discipleshipmentortraineddate" HeaderText="Discipleshipmentortraineddate" ReadOnly="true"  SortExpression="Discipleshipmentortraineddate"  />
                <asp:BoundField  DataField="Discipleshipmentorstartdate" HeaderText="Discipleshipmentorstartdate" ReadOnly="true"  SortExpression="Discipleshipmentorstartdate"  />
                <asp:BoundField  DataField="Discipleshipmentornotes" HeaderText="Discipleshipmentornotes" ReadOnly="true"  SortExpression="Discipleshipmentornotes"  />
                <asp:BoundField  DataField="Discipleshipmentorpotentials" HeaderText="Discipleshipmentorpotentials" ReadOnly="true"  SortExpression="Discipleshipmentorpotentials"  />
                <asp:BoundField  DataField="Discipleshipmentorwaitinglist" HeaderText="Discipleshipmentorwaitinglist" ReadOnly="true"  SortExpression="Discipleshipmentorwaitinglist"  />
        </Columns>
    </asp:GridView>

    <asp:GridView ID="gvViewAllInfo" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: -5px; top: 280px; position: absolute; height: 153px; width: 1249px" 
        onselectedindexchanged="gvViewAllInfo_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvViewAllInfo_RowCommand" 
        AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>

    <asp:GridView ID="gvCustomView" runat="server" 
        BackColor="#FFD200"
        style="z-index: 1; left: -5px; top: 270px; position: absolute; height: 153px" 
        onselectedindexchanged="gvCustomView_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvCustomView_RowCommand" 
        AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>

    <asp:Panel ID="pnlCustomView" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="4px" Visible="False">
        <asp:Button ID="cmbCancelCustomViewFields" runat="server" 
            onclick="cmbCancelCustomViewFields_Click" 
            style="z-index: 1; left: 1002px; top: 173px; position: absolute; height: 37px; width: 156px" 
            Text="Cancel Field Selection" Visible="False" />
        <asp:CheckBoxList ID="cblCreateCustomView" runat="server" 
    
            style="z-index: 1; left: 5px; top: 34px; position: absolute; height: 228px; width: 956px" 
            CausesValidation="True" Font-Size="12pt" Height="205px" 
            onselectedindexchanged="cblCreateCustomView_SelectedIndexChanged" 
            RepeatColumns="5" Visible="False">
            <asp:ListItem>LastName</asp:ListItem>
            <asp:ListItem>FirstName</asp:ListItem>
            <asp:ListItem>Address</asp:ListItem>
            <asp:ListItem>City</asp:ListItem>
            <asp:ListItem>State</asp:ListItem>
            <asp:ListItem>Zip</asp:ListItem>
            <asp:ListItem>HomePhone</asp:ListItem>
            <asp:ListItem>CellPhone</asp:ListItem>
            <asp:ListItem>Email</asp:ListItem>
            <asp:ListItem>DOB</asp:ListItem>
            <asp:ListItem>Sex</asp:ListItem>
            <asp:ListItem>Church</asp:ListItem>
            <asp:ListItem>HealthConditions</asp:ListItem>
            <asp:ListItem>Notes</asp:ListItem>
            <asp:ListItem>TShirtSize</asp:ListItem>
            <asp:ListItem>LastUpdatedBy</asp:ListItem>
            <asp:ListItem>MostRecentSeason</asp:ListItem>
            <asp:ListItem>MostRecentSeasonYear</asp:ListItem>
            <asp:ListItem>GeneralInformation</asp:ListItem>
            <asp:ListItem>SpiritualJourney</asp:ListItem>
            <asp:ListItem>VehichleInsurance</asp:ListItem>
            <asp:ListItem>ReleaseWaiver</asp:ListItem>
            <asp:ListItem>NewVolunteerTraining</asp:ListItem>
            <asp:ListItem>DMVCheck</asp:ListItem>
            <asp:ListItem>NationalCheck</asp:ListItem>
            <asp:ListItem>PACriminalCheck</asp:ListItem>
            <asp:ListItem>DiscipleshipMentorTraining</asp:ListItem>
            <asp:ListItem>DiscipleshipMentorParticipation</asp:ListItem>
            <asp:ListItem>DiscipleshipMentorTrainedDate</asp:ListItem>
            <asp:ListItem>DiscipleshipMentorStartDate</asp:ListItem>
            <asp:ListItem>MSHSChoir</asp:ListItem>
            <asp:ListItem>ChildrensChoir</asp:ListItem>
            <asp:ListItem>PerformingArts</asp:ListItem>
            <asp:ListItem>Shakes</asp:ListItem>
            <asp:ListItem>Singers</asp:ListItem>
            <asp:ListItem>OutreachBasketball</asp:ListItem>
            <asp:ListItem>BasketballTEAMS</asp:ListItem>
            <asp:ListItem>HSBasketballLg</asp:ListItem>
            <asp:ListItem>MSBasketballLg</asp:ListItem>
            <asp:ListItem>3on3Basketball</asp:ListItem>
            <asp:ListItem>SoccerIntraMurals</asp:ListItem>
            <asp:ListItem>SoccerTEAMS</asp:ListItem>
            <asp:ListItem>Baseball</asp:ListItem>
            <asp:ListItem>BibleStudy</asp:ListItem>
            <asp:ListItem>MondayNights</asp:ListItem>
            <asp:ListItem>SpecialEvents</asp:ListItem>
            <asp:ListItem>SummerDayCamp</asp:ListItem>
            <asp:ListItem>AcademicReadingSupport</asp:ListItem>
        </asp:CheckBoxList>
        <asp:Label ID="lblCustomView" runat="server" Font-Size="19pt" 
            style="z-index: 1; left: 9px; top: 3px; position: absolute; height: 34px; width: 708px; text-decoration: underline; font-weight: 700;" 
            Text="Create your custom View by checking fields below. "></asp:Label>
        <asp:Button ID="cmbConfirmCustomViewFields" runat="server" 
            onclick="cmbConfirmCustomViewFields_Click" 
            style="z-index: 1; left: 1003px; top: 109px; position: absolute; width: 156px; height: 37px" 
            Text="Confirm Fields" Visible="False" />
        <asp:Button ID="cmbClearViewFields" runat="server" 
            onclick="cmbClearViewFields_Click" 
            style="z-index: 1; left: 1004px; top: 47px; position: absolute; height: 37px; width: 156px" 
            Text="Clear Selections" Visible="False" />
    </asp:Panel>

    <asp:ImageButton ID="imbAdvancedSearch" runat="server" 
        ImageUrl="~/MagnifiyingGlass.bmp" onclick="imbAdvancedSearch_Click" 
        
        
        style="z-index: 1; left: 884px; top: 281px; position: absolute; height: 47px; width: 58px" />
    <asp:DropDownList ID="ddlAdvancedSearchView" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 23px; top: 214px; position: absolute; width: 187px" 
        Visible="False" 
        onselectedindexchanged="ddlAdvancedSearchView_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label ID="lblViewChoices" runat="server" 
        style="z-index: 1; left: 47px; top: 191px; position: absolute; width: 183px" 
        Text="OR" Visible="False"></asp:Label>
    <asp:LinkButton ID="lbCreateCustomView" runat="server" 
        onclick="lbCreateCustomView_Click" 
        style="z-index: 1; left: 204px; top: 158px; position: absolute; height: 24px; width: 146px" 
        Visible="False">(Create Custom View)</asp:LinkButton>
    <asp:Button ID="cmbCreateCustomView" runat="server" 
        onclick="cmbCreateCustomView_Click" 
        style="z-index: 1; left: 28px; top: 163px; position: absolute; height: 26px; width: 151px" 
        Text="Create a Custom View" Visible="False" />
    <asp:DropDownList ID="ddlOperatorBoolean" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlOperatorBoolean_SelectedIndexChanged" 
        style="z-index: 1; left: 214px; top: 286px; position: absolute; width: 128px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean4_SelectedIndexChanged" 
        style="z-index: 1; left: 212px; top: 434px; position: absolute; width: 128px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlOperatorBoolean5_SelectedIndexChanged" 
        style="z-index: 1; left: 210px; top: 488px; position: absolute; width: 128px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlOperatorCharacter_SelectedIndexChanged" 
        style="z-index: 1; left: 214px; top: 286px; position: absolute; width: 128px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter3" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlOperatorCharacter3_SelectedIndexChanged" 
        style="z-index: 1; left: 213px; top: 382px; position: absolute; width: 128px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorCharacter4_SelectedIndexChanged" 
        style="z-index: 1; left: 213px; top: 434px; position: absolute; width: 128px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlOperatorCharacter5_SelectedIndexChanged" 
        style="z-index: 1; left: 212px; top: 485px; position: absolute; width: 128px" 
        Visible="False">
    </asp:DropDownList>
    </form>
</body>
</html>
