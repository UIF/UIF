<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="StudentAttendanceReporting.aspx.cs" Inherits="UIF.PerformingArts.StudentAttendanceReporting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .style6
        {
            position: absolute;
            top: 269px;
            left: 539px;
            z-index: 1;
            width: 191px;
        }
        .style10
        {
            position: absolute;
            top: 164px;
            left: 459px;
            z-index: 1;
            width: 362px;
            height: 58px;
            font-weight: bold;
            text-decoration: underline;
            right: 221px;
        }
        .style24
        {
            position: absolute;
            top: 269px;
            left: 539px;
            z-index: 1;
            width: 191px;
        }
        </style>

<style type="text/css"> 
   div { z-index: 1;
        margin-top: 0px;
        left: 10px;
        top: 236px;
        position: absolute;
        height: 358px;
        width: 1100px;
    } 
</style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>


</head>
<body bgcolor=Orange>
    <form id="form1" runat="server" defaultfocus="txbSearch" defaultbutton="cmbSearch">
    <div>
    
    </div>

    <asp:Panel ID="pnlBackground" runat="server" BackColor="White"  
        style="z-index: 1; left: -12px; top: 1px; position: absolute; height: 133px; width: 1298px" 
        ViewStateMode="Enabled" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px">
        <asp:DropDownList ID="ddlOperatorCharacter2" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlOperatorCharacter2_SelectedIndexChanged" 
            style="z-index: 1; left: 224px; top: 313px; position: absolute; width: 123px" 
            Visible="False">
        </asp:DropDownList>
        <asp:Label ID="lblUrbanImpact" runat="server" Font-Bold="True" Font-Size="36pt" 
            style="z-index: 1; left: 354px; top: 24px; position: absolute; height: 62px; width: 547px" 
            Text="Urban Impact Foundation"></asp:Label>
        <asp:Button ID="cmbCreateCustomView" runat="server" 
            onclick="cmbCreateCustomView_Click" 
            style="z-index: 1; left: 29px; top: 155px; position: absolute; height: 24px; width: 173px" 
            Text="Create a Custom View" Visible="False" 
            ToolTip="Custom select your desired result fields." />
        <asp:DropDownList ID="ddlSearchValueBool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValueBool_SelectedIndexChanged" 
            style="z-index: 1; left: 358px; top: 266px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValue3Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue3Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 357px; top: 360px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValue4Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue4Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 357px; top: 406px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValue5Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue5Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 357px; top: 456px; position: absolute; width: 179px" 
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

    <asp:ImageButton ID="imgButton" runat="server"  ImageUrl="~/PerformingArts/Picture1.png"
        style="z-index: 1; left: 25px; top: 13px; position: absolute; height: 114px; width: 172px" 
        onclick="imgButton_Click" />

    <asp:DropDownList ID="ddlGrades2" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades2_SelectedIndexChanged" 
        style="z-index: 1; left: 348px; top: 317px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>

    <asp:Label ID="Label1" runat="server" CssClass="style10" Font-Size="25pt" 
        Text="Student Search/Queries"></asp:Label>

    <asp:Button ID="cmdStudent" runat="server" CssClass="style6" 
        onclick="cmdStudent_Click" Text="Advanced Search" />

    <asp:Button ID="cmbSearch" runat="server" CssClass="style24" 
        onclick="cmbSearch_Click" Text="Quick Search" />
    <asp:TextBox ID="txbSearch" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 257px; top: 242px; position: absolute; width: 765px" 
        TabIndex="1"></asp:TextBox>
    <asp:DropDownList ID="ddlChooseField" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 19px; top: 269px; position: absolute; width: 187px" 
        TabIndex="2" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField_SelectedIndexChanged1">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 216px; top: 269px; position: absolute; width: 123px; right: 579px;" 
        TabIndex="3" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:TextBox ID="txbSearchValue" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 269px; position: absolute; width: 173px" 
        TabIndex="4" AutoPostBack="True" CausesValidation="True" 
        ontextchanged="txbSearchValue_TextChanged"></asp:TextBox>
    <asp:RadioButtonList ID="rblNumber1" runat="server" 
        style="z-index: 1; left: 17px; top: 283px; position: absolute; height: 41px; width: 113px" 
        RepeatDirection="Horizontal" TabIndex="5" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber1_SelectedIndexChanged">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:DropDownList ID="ddlChooseField2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 19px; top: 317px; position: absolute; width: 187px" 
        TabIndex="6" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField2_SelectedIndexChanged" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator2" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 216px; top: 317px; position: absolute; width: 123px" 
        TabIndex="7" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator2_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField3" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 18px; top: 364px; position: absolute; width: 187px" 
        TabIndex="10" 
        onselectedindexchanged="ddlChooseField3_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True" Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField4" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 17px; top: 410px; position: absolute; width: 187px" 
        TabIndex="14" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField4_SelectedIndexChanged1" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField5" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 16px; top: 459px; position: absolute; width: 187px" 
        TabIndex="18" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField5_SelectedIndexChanged1" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator3" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 215px; top: 364px; position: absolute; width: 123px; " 
        TabIndex="11" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator3_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlGrades3" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades3_SelectedIndexChanged" 
        style="z-index: 1; left: 348px; top: 364px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>

    <asp:GridView ID="gvAdvancedSearchResults" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px"   DataKeyNames="Name"
        style="z-index: 1; left: 122px; top: 143px; position: absolute; height: 213px; width: 1001px" 
        AutoGenerateColumns="False" EnableViewState="True" ViewStateMode="Inherit" 
        onselectedindexchanged="gvAdvancedSearchResults_RowCommand"
         OnRowCommand="gvAdvancedSearchResults_RowCommand" >
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" ForeColor="Black" />
        <RowStyle Wrap="False" />
        <Columns>
                <asp:ButtonField ButtonType="Link" DataTextField="Name" HeaderText="Last,First (Middle) Name" SortExpression="Name"   CausesValidation="true"  CommandName="Name"  />
                <asp:BoundField  DataField="Address" HeaderText="Address" ReadOnly="true"  SortExpression="Address"  />
                <asp:BoundField  DataField="City" HeaderText="City" ReadOnly="true"  SortExpression="City"  />
                <asp:BoundField  DataField="State" HeaderText="State" ReadOnly="true"  SortExpression="State"  />
                <asp:BoundField  DataField="Zip" HeaderText="Zip" ReadOnly="true"  SortExpression="Zip"  />
                <asp:BoundField  DataField="HomePhone" HeaderText="HomePhone" ReadOnly="true"  SortExpression="HomePhone"  />
                <asp:BoundField  DataField="CellPhone" HeaderText="CellPhone" ReadOnly="true"  SortExpression="CellPhone"  />
                <asp:BoundField  DataField="School" HeaderText="School" ReadOnly="true"  SortExpression="School"  />                
        </Columns>
    </asp:GridView>
    <asp:DropDownList ID="ddlChooseOperator4" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 214px; top: 410px; position: absolute; width: 123px; " 
        TabIndex="15" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator4_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator5" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 213px; top: 458px; position: absolute; width: 123px; right: 563px;" 
        TabIndex="19" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator5_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:RadioButtonList ID="rblNumber2" runat="server" 
        style="z-index: 1; left: 17px; top: 332px; position: absolute; height: 41px; width: 116px" 
        RepeatDirection="Horizontal" TabIndex="9" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber2_SelectedIndexChanged">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber3" runat="server" 
        style="z-index: 1; left: 17px; top: 385px; position: absolute; height: 27px; width: 122px" 
        RepeatDirection="Horizontal" TabIndex="13" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber3_SelectedIndexChanged">
        <asp:ListItem Value="AND"></asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber4" runat="server" 
        style="z-index: 1; left: 17px; top: 432px; position: absolute; height: 27px; width: 122px" 
        RepeatDirection="Horizontal" TabIndex="17" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber4_SelectedIndexChanged">
        <asp:ListItem Value="AND"></asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="lblFindRecords" runat="server" 
        style="z-index: 1; left: 21px; top: 244px; position: absolute; width: 190px" 
        Text="Find student records where: "></asp:Label>
    <asp:TextBox ID="txbSearchValue2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 317px; position: absolute; width: 173px" 
        ontextchanged="txbSearchValue2_TextChanged" TabIndex="8" 
        AutoPostBack="True" CausesValidation="True" Enabled="False"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 347px; top: 365px; position: absolute; width: 173px" 
        TabIndex="12" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue3_TextChanged"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 347px; top: 410px; position: absolute; width: 173px" 
        TabIndex="16" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue4_TextChanged"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue5" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 346px; top: 458px; position: absolute; width: 173px" 
        TabIndex="20" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue5_TextChanged"></asp:TextBox>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 539px; top: 333px; position: absolute; width: 191px; height: 26px;" 
        Text="Export to Excel" Enabled="False" />
    <p>
        <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
            style="z-index: 1; left: 539px; top: 301px; position: absolute; width: 191px" 
            Text="Reset Search" />
    </p>
    <asp:CheckBox ID="chbAdvancedSearch" runat="server" AutoPostBack="True" 
        CausesValidation="True" Checked="True" 
        oncheckedchanged="chbAdvancedSearch_CheckedChanged" 
        style="z-index: 1; left: 750px; top: 277px; position: absolute; width: 143px" 
        Text="(Advanced Search)" />
    <asp:ImageButton ID="imbAdvancedSearch" runat="server" 
        ImageUrl="~/MagnifiyingGlass.bmp" onclick="imbAdvancedSearch_Click" 
        
        
        style="z-index: 1; left: 894px; top: 268px; position: absolute; height: 58px; width: 74px" />
    <asp:GridView ID="gvAdvancedSearch" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 7px; top: 255px; position: absolute; height: 133px; width: 1180px">
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

    <asp:GridView ID="gvPersonalView" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: 5px; top: 255px; position: absolute; height: 153px; width: 1258px" 
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

    <asp:GridView ID="gvViewAllInfo" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: 7px; top: 255px; position: absolute; height: 153px; width: 1249px" 
        onselectedindexchanged="gvViewAllInfo_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvViewAllInfo_RowCommand" 
        AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>

    <asp:GridView ID="gvCustomView" runat="server" 
        BackColor="#FFD200"
        style="z-index: 1; left: 5px; top: 255px; position: absolute; height: 153px" 
        onselectedindexchanged="gvCustomView_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvCustomView_RowCommand" 
        AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>

    <asp:DropDownList ID="ddlAdvancedSearchView" runat="server" BackColor="#FFD200" 
        onselectedindexchanged="ddlAdvancedSearchView_SelectedIndexChanged" 
        style="z-index: 1; left: 20px; top: 206px; position: absolute; width: 175px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblViewChoices" runat="server" 
        style="z-index: 1; left: 39px; top: 185px; position: absolute; width: 145px; right: 733px" 
        Text="OR" Visible="False"></asp:Label>
    <asp:LinkButton ID="lbCreateCustomView" runat="server" 
        onclick="lbCreateCustomView_Click" 
        style="z-index: 1; left: 378px; top: 210px; position: absolute" 
        Visible="False">(Create Custom View)</asp:LinkButton>

    <asp:DropDownList ID="ddlSearchValue2Bool" runat="server" 
        style="z-index: 1; left: 348px; top: 318px; position: absolute; width: 179px" 
        Visible="False" BackColor="#FFD200" AutoPostBack="True" 
        CausesValidation="True" 
        onselectedindexchanged="ddlSearchValue2Bool_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:Panel ID="pnlCustomView" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="4px" Visible="False">
        <asp:CheckBoxList ID="cblCreateCustomView" runat="server" 
            style="z-index: 1; left: 0px; top: 39px; position: absolute; height: 61px; width: 821px; right: 279px;" 
            CausesValidation="True" 
            onselectedindexchanged="cblCreateCustomView_SelectedIndexChanged1" 
            RepeatColumns="5">
            <asp:ListItem>LastName</asp:ListItem>
            <asp:ListItem>FirstName</asp:ListItem>
            <asp:ListItem>Address</asp:ListItem>
            <asp:ListItem>City</asp:ListItem>
            <asp:ListItem>State</asp:ListItem>
            <asp:ListItem>Zip</asp:ListItem>
            <asp:ListItem>HomePhone</asp:ListItem>
            <asp:ListItem>StudentCellPhone</asp:ListItem>
            <asp:ListItem>StudentEmail</asp:ListItem>
            <asp:ListItem>School</asp:ListItem>
            <asp:ListItem>Grade</asp:ListItem>
            <asp:ListItem>Age</asp:ListItem>
            <asp:ListItem>DOB</asp:ListItem>
            <asp:ListItem>Sex</asp:ListItem>
            <asp:ListItem>Church</asp:ListItem>
            <asp:ListItem>CareerGoal</asp:ListItem>
            <asp:ListItem>HealthConditions</asp:ListItem>
            <asp:ListItem>Notes</asp:ListItem>
            <asp:ListItem>TShirtSize</asp:ListItem>
            <asp:ListItem>MeetCCGF</asp:ListItem>
            <asp:ListItem>HasReceivedChrist</asp:ListItem>
            <asp:ListItem>CurrentRegistrationForm</asp:ListItem>
            <asp:ListItem>LastUpdatedBy</asp:ListItem>
            <asp:ListItem>CoreKid</asp:ListItem>
            <asp:ListItem>MSHSChoir</asp:ListItem>
            <asp:ListItem>ChildrensChoir</asp:ListItem>
            <asp:ListItem>PerformingArtsAcademy</asp:ListItem>
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
            <asp:ListItem>CampComments</asp:ListItem>
            <asp:ListItem>ImpactUrbanSchools</asp:ListItem>
            <asp:ListItem>AcademicReadingSupport</asp:ListItem>
            <asp:ListItem>ParentGuardian1</asp:ListItem>
            <asp:ListItem>ParentGuardianRelationShip1</asp:ListItem>
            <asp:ListItem>ParentGuardian1Email</asp:ListItem>
            <asp:ListItem>WorkPhone1</asp:ListItem>
            <asp:ListItem>CellPhone1</asp:ListItem>
            <asp:ListItem>TextPhone1</asp:ListItem>
            <asp:ListItem>ParentGuardian2</asp:ListItem>
            <asp:ListItem>ParentGuardian2RelationShip</asp:ListItem>
            <asp:ListItem>WorkPhone2</asp:ListItem>
            <asp:ListItem>CellPhone2</asp:ListItem>
            <asp:ListItem>TextPhone2</asp:ListItem>
        </asp:CheckBoxList>
        <asp:Label ID="lblCustomView" runat="server" Font-Size="19pt" 
            style="z-index: 1; left: 15px; top: 5px; position: absolute; height: 41px; width: 552px; text-decoration: underline; font-weight: 700" 
            Text="Create your custom View by checking fields below." Visible="False"></asp:Label>
        <asp:Button ID="cmbCancelCustomViewFields" runat="server" 
            onclick="cmbCancelCustomViewFields_Click1" 
            style="z-index: 1; left: 891px; top: 189px; position: absolute; height: 47px; width: 156px" 
            Text="Cancel Field Selection" Visible="False" />
        <asp:Button ID="cmbConfirmCustomViewFields" runat="server"  
            onclick="cmbConfirmCustomViewFields_Click1" 
            style="z-index: 1; left: 891px; top: 122px; position: absolute; height: 47px; width: 156px" 
            Text="Confirm Fields" Visible="False" />
        <asp:Button ID="cmbClearViewFields" runat="server" 
            onclick="cmbClearViewFields_Click1" 
            style="z-index: 1; left: 891px; top: 54px; position: absolute; height: 47px; width: 156px" 
            Text="Clear Sections" Visible="False" />
    </asp:Panel>
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
    <asp:DropDownList ID="ddlProgramSection" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 348px; top: 318px; position: absolute; width: 179px" 
        Visible="False" AutoPostBack="True" 
        CausesValidation="True" 
        onselectedindexchanged="ddlProgramSection_SelectedIndexChanged">
    </asp:DropDownList>
    </form>
</body>
</html>
