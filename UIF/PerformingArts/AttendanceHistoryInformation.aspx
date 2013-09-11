<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="AttendanceHistoryInformation.aspx.cs" Inherits="UIF.PerformingArts.AttendanceHistoryInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style6
        {
            position: absolute;
            top: 369px;
            left: 540px;
            z-index: 1;
            width: 191px;
        }
        .style10
        {
            position: absolute;
            top: 140px;
            z-index: 1;
            width: 508px;
            height: 58px;
            font-weight: bold;
            text-decoration: underline;
            right: 438px;
        }
        .style24
        {
            position: absolute;
            top: 331px;
            left: 539px;
            z-index: 1;
            width: 191px;
        }
        </style>

<style type="text/css"> 
   div { z-index: 1;
        margin-top: 0px;
        left: 8px;
        top: 215px;
        position: absolute;
        height: 315px;
        width: 1094px;
    } 
</style>

<style type="text/css"> 
   div { z-index: 1;
        margin-bottom: 0px;
    } 
</style>

<style type="text/css"> 
    div { z-index: 9999; } 
</style>

</head>
<body bgcolor=Orange>
    <form id="form2" runat="server" defaultfocus="txbSearch" defaultbutton="cmbSearch">
    <div>
    
    </div>

    <asp:Panel ID="pnlBackground" runat="server" BackColor="White"  
        style="z-index: 1; left: -12px; top: 1px; position: absolute; height: 133px; width: 1298px" 
        ViewStateMode="Enabled" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px">
        <asp:DropDownList ID="ddlOperatorCharacter2" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlOperatorCharacter2_SelectedIndexChanged" 
            style="z-index: 1; left: 224px; top: 353px; position: absolute; width: 123px" 
            Visible="False">
        </asp:DropDownList>
        <asp:Label ID="lblUrbanImpact" runat="server" Font-Bold="True" Font-Size="36pt" 
            style="z-index: 1; left: 354px; top: 24px; position: absolute; height: 62px; width: 547px" 
            Text="Urban Impact Foundation"></asp:Label>
        <asp:Button ID="cmbCreateCustomView" runat="server" 
            onclick="cmbCreateCustomView_Click" 
            style="z-index: 1; left: 33px; top: 195px; position: absolute; height: 24px; width: 173px" 
            Text="Create a Custom View" 
            ToolTip="Custom select your desired result fields." />
        <asp:DropDownList ID="ddlSearchValueBool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValueBool_SelectedIndexChanged" 
            style="z-index: 1; left: 349px; top: 312px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValue3Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue3Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 349px; top: 402px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>

        <asp:DropDownList ID="ddlSearchValue4Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue4Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 349px; top: 450px; position: absolute; width: 179px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSearchValue5Bool" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlSearchValue5Bool_SelectedIndexChanged" 
            style="z-index: 1; left: 349px; top: 499px; position: absolute; width: 179px"
            Visible="False">
        </asp:DropDownList>
        <asp:Label ID="lblErrorMessage" runat="server" BackColor="White" 
            BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" Font-Size="16pt" 
            ForeColor="Red" 
            style="z-index: 1; left: 799px; top: 454px; position: absolute; height: 199px; width: 397px" 
            Text="Correction Required: " Visible="False"></asp:Label>
        <asp:DropDownList ID="ddlOperatorCharacter3" runat="server" AutoPostBack="True" 
            BackColor="#FFD200" CausesValidation="True" 
            onselectedindexchanged="ddlOperatorCharacter3_SelectedIndexChanged" 
            style="z-index: 1; left: 224px; top: 402px; position: absolute; width: 123px" 
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


    <asp:CheckBox ID="chbOutreachBball" runat="server" 
        style="z-index: 1; left: 230px; top: 179px; position: absolute" 
        Text="Outreach Basketball" 
        oncheckedchanged="chbOutreachBball_CheckedChanged" Visible="False" 
        AutoPostBack="True" CausesValidation="True" />
    <asp:CheckBox ID="chbMondayNights" runat="server" 
        oncheckedchanged="chbMondayNights_CheckedChanged" 
        style="z-index: 1; left: 230px; top: 258px; position: absolute; width: 124px; bottom: 244px;" 
        Text="Monday Nights" Visible="False" AutoPostBack="True" 
        CausesValidation="True" />
    <asp:CheckBox ID="chbBaseball" runat="server" 
        style="z-index: 1; left: 385px; top: 178px; position: absolute; width: 133px;" 
        Text="Baseball" 
        oncheckedchanged="chbBaseball_CheckedChanged" Visible="False" 
        AutoPostBack="True" CausesValidation="True" />
    <asp:CheckBox ID="chbSoccerTEAMS" runat="server" 
        style="z-index: 1; left: 385px; top: 239px; position: absolute" 
        Text="SoccerTEAMS" 
        oncheckedchanged="chbSoccerTEAMS_CheckedChanged" Visible="False" 
        AutoPostBack="True" CausesValidation="True" />

    <asp:CheckBox ID="chbMSBasketLeague" runat="server" 
        style="z-index: 1; left: 230px; top: 219px; position: absolute; width: 149px;" 
        Text="MS Basketball Lg" 
        oncheckedchanged="chbMSBasketLeague_CheckedChanged" Visible="False" 
        AutoPostBack="True" CausesValidation="True" />
    <asp:CheckBox ID="chbBasketballTEAMS" runat="server" 
        style="z-index: 1; left: 230px; top: 199px; position: absolute" 
        Text="Basketball TEAMS" AutoPostBack="True" CausesValidation="True" 
        oncheckedchanged="chbBasketballTEAMS_CheckedChanged" />
    <asp:CheckBox ID="chbSoccerIntraMurals" runat="server" 
        style="z-index: 1; left: 385px; top: 220px; position: absolute" 
        Text="Soccer IntraMurals" 
        oncheckedchanged="chbSoccerIntraMurals_CheckedChanged" Visible="False" 
        AutoPostBack="True" CausesValidation="True" />
    <asp:CheckBox ID="chb3on3Basketball" runat="server" 
        style="z-index: 1; left: 385px; top: 200px; position: absolute; width: 151px;" 
        Text="3on3 Basketball" 
        oncheckedchanged="chb3on3Basketball_CheckedChanged" Visible="False" 
        AutoPostBack="True" CausesValidation="True" />
    <asp:CheckBox ID="chbHSBasketLeague" runat="server" 
        style="z-index: 1; left: 230px; top: 238px; position: absolute" 
        Text="HS Basketball Lg" 
        oncheckedchanged="chbHSBasketLeague_CheckedChanged" Visible="False" 
        AutoPostBack="True" CausesValidation="True" />
    <asp:CheckBox ID="chbOliverFootballBible" runat="server" 
        style="z-index: 1; left: 385px; top: 258px; position: absolute" 
        Text="Bible Study" 
        oncheckedchanged="chbOliverFootballBible_CheckedChanged" Visible="False" 
        AutoPostBack="True" CausesValidation="True" />
    <asp:CheckBox ID="chbIncludePromotionalMail" runat="server" 
        style="z-index: 1; left: 537px; top: 338px; position: absolute; width: 294px" 
        Text="Include Promotional Mailees" />
    <asp:CheckBox ID="chbChildrensChoir" runat="server" CssClass="style28"  
        style="z-index: 1; left: 543px; top: 199px; position: absolute; width: 185px;" 
        oncheckedchanged="chbChildrensChoir_CheckedChanged" 
        Text="Children's Choir" AutoPostBack="True" CausesValidation="True" 
        Visible="False" />
    <asp:CheckBox ID="chbMSHSChoir" runat="server" CssClass="style29" 
        style="z-index: 1; left: 543px; top: 180px; position: absolute; width: 136px;" 
        oncheckedchanged="chbMSHSChoir_CheckedChanged" Text="MS/HS Choir" 
        AutoPostBack="True" CausesValidation="True" Visible="False" />
    <asp:CheckBox ID="chbPerformingArts" runat="server" CssClass="style30" 
        style="z-index: 1; left: 543px; top: 217px; position: absolute; width: 155px;" 
        oncheckedchanged="chbPerformingArts_CheckedChanged" Text="Performing Arts" 
        AutoPostBack="True" CausesValidation="True" Visible="False" />
    <asp:CheckBox ID="chbSingers" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 543px; top: 252px; position: absolute; width: 127px; height: 21px;" 
        Text="Singers" oncheckedchanged="chbSingers_CheckedChanged" 
        Visible="False" />
    <asp:CheckBox ID="chbShakes" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 543px; top: 235px; position: absolute; right: 467px; height: 41px;" 
        Text="Shakes" oncheckedchanged="chbShakes_CheckedChanged" 
        Visible="False" />

    <asp:DropDownList ID="ddlGrades2" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades2_SelectedIndexChanged" 
        style="z-index: 1; left: 349px; top: 357px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>



    <asp:CheckBox ID="chbAllPrograms" runat="server" AutoPostBack="True" 
        CausesValidation="True" oncheckedchanged="chbAllPrograms_CheckedChanged" 
        style="z-index: 1; left: 537px; top: 313px; position: absolute; width: 245px" 
        Text="Select All Programs" />



    <asp:Label ID="lblMonth" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 745px; top: 300px; position: absolute; width: 61px" 
        Text="Month"></asp:Label>
    <asp:Label ID="lblDay" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 828px; top: 300px; position: absolute; width: 41px;" 
        Text="Day"></asp:Label>
    <asp:Label ID="lblYear" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 881px; top: 300px; position: absolute; height: 12px; width: 51px;" 
        Text="Year"></asp:Label>
    <asp:DropDownList ID="ddlPickDateRangeMonth1" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 742px; top: 296px; position: absolute; width: 74px" 
        onselectedindexchanged="ddlPickDateRangeMonth1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay1" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 823px; top: 297px; position: absolute; width: 48px" 
        onselectedindexchanged="ddlPickDateRangeDay1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear1" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 876px; top: 298px; position: absolute; width: 59px" 
        onselectedindexchanged="ddlPickDateRangeYear1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Label ID="lblTo" runat="server" Font-Bold="True" 
        style="z-index: 1; left: 946px; top: 350px; position: absolute" 
        Text="To: "></asp:Label>
    <asp:DropDownList ID="ddlPickDateRangeMonth2" runat="server" 
        BackColor="#FFD200" 
        style="z-index: 1; left: 977px; top: 297px; position: absolute; width: 76px; right: 18px" 
        onselectedindexchanged="ddlPickDateRangeMonth2_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeDay2" runat="server" BackColor="#FFD200" 
        
        
        style="z-index: 1; left: 1059px; top: 298px; position: absolute; width: 47px" 
        onselectedindexchanged="ddlPickDateRangeDay2_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlPickDateRangeYear2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1112px; top: 299px; position: absolute; width: 69px" 
        onselectedindexchanged="ddlPickDateRangeYear2_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:Label ID="lblStartDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 740px; top: 320px; position: absolute; width: 68px" 
        Text="(Start Date)"></asp:Label>
    <asp:Label ID="lblEndDate" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 1056px; top: 321px; position: absolute; height: 17px; width: 62px" 
        Text="(End Date)"></asp:Label>
    <asp:Label ID="lblSetDateRange" runat="server" Font-Bold="True" 
        style="z-index: 1; left: 866px; top: 330px; position: absolute; width: 163px" 
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
        Text="(Optional)"></asp:Label>
    <asp:Label ID="lblAnotherOne" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 809px; top: 268px; position: absolute" 
        Text="(Optional)"></asp:Label>
    <asp:Label ID="lblAnotherOneYet" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 1204px; top: 526px; position: absolute" 
        Text="(Optional)" Visible="False"></asp:Label>
    <asp:Label ID="lblOptionalAnother" runat="server" Font-Size="10pt" 
        style="z-index: 1; left: 112px; top: 734px; position: absolute" 
        Text="(Optional)"></asp:Label>




    <asp:Label ID="Label1" runat="server" CssClass="style10" Font-Size="25pt" 
        Text="Attendance History Reporting"></asp:Label>

    <asp:Button ID="cmdStudent" runat="server" CssClass="style6" 
        onclick="cmdStudent_Click" Text="Advanced Search" />

    <asp:Button ID="cmbSearch" runat="server" CssClass="style24" 
        onclick="cmbSearch_Click" Text="Quick Search" />
    <asp:TextBox ID="txbSearch" runat="server" BackColor="#FFD200" 
        
        
        style="z-index: 1; left: 436px; top: 426px; position: absolute; width: 765px" 
        TabIndex="1"></asp:TextBox>
    <asp:DropDownList ID="ddlChooseField" runat="server" BackColor="#FFD200" 
        
        
        
        style="z-index: 1; left: 19px; top: 312px; position: absolute; width: 187px; bottom: 233px;" 
        TabIndex="2" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField_SelectedIndexChanged1" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 216px; top: 312px; position: absolute; width: 123px; right: 815px;" 
        TabIndex="3" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:TextBox ID="txbSearchValue" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 347px; top: 312px; position: absolute; width: 173px" 
        TabIndex="4" AutoPostBack="True" CausesValidation="True" 
        ontextchanged="txbSearchValue_TextChanged"></asp:TextBox>
    <asp:RadioButtonList ID="rblNumber1" runat="server" 
        style="z-index: 1; left: 17px; top: 324px; position: absolute; height: 41px; width: 113px" 
        RepeatDirection="Horizontal" TabIndex="5" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber1_SelectedIndexChanged">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:DropDownList ID="ddlChooseField2" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 19px; top: 357px; position: absolute; width: 187px" 
        TabIndex="6" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField2_SelectedIndexChanged" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator2" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 216px; top: 357px; position: absolute; width: 123px" 
        TabIndex="7" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator2_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField3" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 18px; top: 406px; position: absolute; width: 187px" 
        TabIndex="10" 
        onselectedindexchanged="ddlChooseField3_SelectedIndexChanged" 
        AutoPostBack="True" CausesValidation="True" Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField4" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 17px; top: 452px; position: absolute; width: 187px" 
        TabIndex="14" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField4_SelectedIndexChanged1" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseField5" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 16px; top: 502px; position: absolute; width: 187px" 
        TabIndex="18" AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="ddlChooseField5_SelectedIndexChanged1" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator3" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 215px; top: 406px; position: absolute; width: 123px; " 
        TabIndex="11" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator3_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlGrades3" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades3_SelectedIndexChanged" 
        style="z-index: 1; left: 349px; top: 406px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>

    <asp:DropDownList ID="ddlChooseOperator4" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 214px; top: 452px; position: absolute; width: 123px; " 
        TabIndex="15" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator4_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlChooseOperator5" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 213px; top: 501px; position: absolute; width: 123px; right: 818px;" 
        TabIndex="19" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        onselectedindexchanged="ddlChooseOperator5_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:RadioButtonList ID="rblNumber2" runat="server" 
        style="z-index: 1; left: 17px; top: 373px; position: absolute; height: 41px; width: 116px" 
        RepeatDirection="Horizontal" TabIndex="9" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber2_SelectedIndexChanged">
        <asp:ListItem>AND</asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber3" runat="server" 
        style="z-index: 1; left: 17px; top: 426px; position: absolute; height: 27px; width: 122px" 
        RepeatDirection="Horizontal" TabIndex="13" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber3_SelectedIndexChanged">
        <asp:ListItem Value="AND"></asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:RadioButtonList ID="rblNumber4" runat="server" 
        style="z-index: 1; left: 17px; top: 476px; position: absolute; height: 27px; width: 122px" 
        RepeatDirection="Horizontal" TabIndex="17" AutoPostBack="True" 
        CausesValidation="True" Enabled="False" 
        onselectedindexchanged="rblNumber4_SelectedIndexChanged">
        <asp:ListItem Value="AND"></asp:ListItem>
        <asp:ListItem Value="OR"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="lblFindRecords" runat="server" 
        style="z-index: 1; left: 21px; top: 290px; position: absolute; width: 190px" 
        Text="Find student records where: "></asp:Label>
    <asp:TextBox ID="txbSearchValue2" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 347px; top: 358px; position: absolute; width: 173px" 
        ontextchanged="txbSearchValue2_TextChanged" TabIndex="8" 
        AutoPostBack="True" CausesValidation="True" Enabled="False"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue3" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 347px; top: 404px; position: absolute; width: 173px" 
        TabIndex="12" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue3_TextChanged"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue4" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 347px; top: 454px; position: absolute; width: 173px" 
        TabIndex="16" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue4_TextChanged"></asp:TextBox>
    <asp:TextBox ID="txbSearchValue5" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 347px; top: 499px; position: absolute; width: 173px"
        TabIndex="20" AutoPostBack="True" CausesValidation="True" Enabled="False" 
        ontextchanged="txbSearchValue5_TextChanged"></asp:TextBox>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 539px; top: 442px; position: absolute; width: 191px; height: 26px;" 
        Text="Export to Excel" Enabled="False" />
    <p>
        <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
            style="z-index: 1; left: 539px; top: 404px; position: absolute; width: 191px" 
            Text="Reset Search" />
    </p>
    <asp:CheckBox ID="chbAdvancedSearch" runat="server" AutoPostBack="True" 
        CausesValidation="True" Checked="True" 
        oncheckedchanged="chbAdvancedSearch_CheckedChanged" 
        style="z-index: 1; left: 750px; top: 277px; position: absolute; width: 143px" 
        Text="(Advanced Search)" Visible="False" />
    <asp:ImageButton ID="imbAdvancedSearch" runat="server" 
        ImageUrl="~/MagnifiyingGlass.bmp" onclick="imbAdvancedSearch_Click" 
        
        
        
        style="z-index: 1; left: 894px; top: 268px; position: absolute; height: 58px; width: 74px" 
        Visible="False" />
    <asp:GridView ID="gvAdvancedSearch" runat="server" BackColor="#FFD200" 
        
        
        style="z-index: 1; left: -11px; top: 40px; position: absolute; height: 133px; width: 1180px">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>

    <asp:GridView ID="gvViewAllInfo" runat="server" 
        BackColor="#FFD200" DataKeyNames="Name"
        style="z-index: 1; left: 1px; top: 340px; position: absolute; height: 153px; width: 1249px" 
        onselectedindexchanged="gvViewAllInfo_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvViewAllInfo_RowCommand" 
        AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>

    <asp:GridView ID="gvCustomView" runat="server" 
        BackColor="#FFD200"
        style="z-index: 1; left: 1px; top: 340px; position: absolute; height: 153px" 
        onselectedindexchanged="gvCustomView_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvCustomView_RowCommand" 
        AutoGenerateColumns="True">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>

    <asp:DropDownList ID="ddlAdvancedSearchView" runat="server" BackColor="#FFD200" 
        onselectedindexchanged="ddlAdvancedSearchView_SelectedIndexChanged" 
        style="z-index: 1; left: 23px; top: 249px; position: absolute; width: 175px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Label ID="lblViewChoices" runat="server" 
        style="z-index: 1; left: 56px; top: 227px; position: absolute; width: 145px; right: 718px" 
        Text="OR" Visible="False"></asp:Label>
    <asp:LinkButton ID="lbCreateCustomView" runat="server" 
        onclick="lbCreateCustomView_Click" 
        style="z-index: 1; left: 960px; top: 153px; position: absolute" 
        Visible="False">(Create Custom View)</asp:LinkButton>

    <asp:DropDownList ID="ddlSearchValue2Bool" runat="server" 
        style="z-index: 1; left: 349px; top: 358px; position: absolute; width: 179px" 
        Visible="False" BackColor="#FFD200" AutoPostBack="True" 
        CausesValidation="True" 
        onselectedindexchanged="ddlSearchValue2Bool_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:Panel ID="pnlCustomView" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="4px" Visible="False">
        <asp:CheckBoxList ID="cblCreateCustomView" runat="server" 
            style="z-index: 1; left: 8px; top: 53px; position: absolute; height: 215px; width: 831px; right: 258px;" 
            CausesValidation="True" 
            onselectedindexchanged="cblCreateCustomView_SelectedIndexChanged1" 
            RepeatColumns="5">
            <asp:ListItem>LastName</asp:ListItem>
            <asp:ListItem>FirstName</asp:ListItem>
            <asp:ListItem>MiddleName</asp:ListItem>
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
            <asp:ListItem>Program</asp:ListItem>
            <asp:ListItem>Section</asp:ListItem>
            <asp:ListItem>Attended</asp:ListItem>
            <asp:ListItem>Day</asp:ListItem>
            <asp:ListItem>Exempt</asp:ListItem>
            <asp:ListItem>ProgramSeason</asp:ListItem>
            <asp:ListItem>Hours</asp:ListItem>
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
        <asp:Label ID="lblCustomView" runat="server" Font-Size="19pt" 
            style="z-index: 1; left: 15px; top: 5px; position: absolute; height: 41px; width: 552px; text-decoration: underline; font-weight: 700" 
            Text="Create your custom View by checking fields below." Visible="False"></asp:Label>
        <asp:Button ID="cmbCancelCustomViewFields" runat="server" 
            onclick="cmbCancelCustomViewFields_Click1" 
            style="z-index: 1; left: 855px; top: 181px; position: absolute; height: 47px; width: 156px" 
            Text="Cancel Field Selection" Visible="False" />
        <asp:Button ID="cmbConfirmCustomViewFields" runat="server"  
            onclick="cmbConfirmCustomViewFields_Click1" 
            style="z-index: 1; left: 855px; top: 122px; position: absolute; height: 47px; width: 156px" 
            Text="Confirm Fields" Visible="False" />
        <asp:Button ID="cmbClearViewFields" runat="server" 
            onclick="cmbClearViewFields_Click1" 
            style="z-index: 1; left: 854px; top: 54px; position: absolute; height: 47px; width: 156px" 
            Text="Clear Sections" Visible="False" />
    </asp:Panel>
    <asp:DropDownList ID="ddlGrades" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 312px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGrades4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades4_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 454px; position: absolute; width: 179px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGrades5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGrades5_SelectedIndexChanged" 
            style="z-index: 1; left: 347px; top: 501px; position: absolute; width: 179px"
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean_SelectedIndexChanged" 
        style="z-index: 1; left: 216px; top: 313px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean2" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean2_SelectedIndexChanged" 
        style="z-index: 1; left: 215px; top: 357px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean3" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean3_SelectedIndexChanged" 
        style="z-index: 1; left: 215px; top: 406px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean4_SelectedIndexChanged" 
        style="z-index: 1; left: 214px; top: 455px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorBoolean5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorBoolean5_SelectedIndexChanged" 
        style="z-index: 1; left: 214px; top: 503px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorCharacter_SelectedIndexChanged" 
        style="z-index: 1; left: 216px; top: 313px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorCharacter4_SelectedIndexChanged" 
        style="z-index: 1; left: 214px; top: 455px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlOperatorCharacter5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlOperatorCharacter5_SelectedIndexChanged" 
        style="z-index: 1; left: 213px; top: 498px; position: absolute; width: 123px" 
        Visible="False">
    </asp:DropDownList>
    <asp:Panel ID="Panel1" runat="server">
        <asp:GridView ID="gvPersonalView" runat="server" AutoGenerateColumns="False" 
            BackColor="#FFD200" DataKeyNames="Name" EnableViewState="True" 
            OnRowCommand="gvPersonalView_RowCommand" 
            onselectedindexchanged="gvPersonalView_RowCommand" 
            style="z-index: 1; left: 1px; top: 339px; position: absolute; height: 153px; width: 1258px" 
            ViewStateMode="Inherit">
            <AlternatingRowStyle BackColor="Silver" />
            <HeaderStyle BackColor="#999999" />
            <Columns>
                <asp:ButtonField ButtonType="Link" CausesValidation="true" CommandName="Name" 
                    DataTextField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true" 
                    SortExpression="Email" />
                <asp:BoundField DataField="DOB" HeaderText="DOB" ReadOnly="true" 
                    SortExpression="DOB" />
                <asp:BoundField DataField="Sex" HeaderText="Sex" ReadOnly="true" 
                    SortExpression="Sex" />
                <asp:BoundField DataField="Church" HeaderText="Church" ReadOnly="true" 
                    SortExpression="Church" />
                <asp:BoundField DataField="HealthConditions" HeaderText="HealthConditions" 
                    ReadOnly="true" SortExpression="HealthConditions" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" ReadOnly="true" 
                    SortExpression="Notes" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:DropDownList ID="ddlPrograms" runat="server" BackColor="#FFD200" 
        
        style="z-index: 1; left: 20px; top: 268px; position: absolute; width: 185px" 
        Visible="False">
    </asp:DropDownList>
    <asp:RadioButtonList ID="rblProgramANDOR" runat="server" 
        RepeatDirection="Horizontal" 
        
        style="z-index: 1; left: 17px; top: 288px; position: absolute; height: 27px; width: 106px" 
        AutoPostBack="True" CausesValidation="True" 
        onselectedindexchanged="rblProgramANDOR_SelectedIndexChanged" 
        Visible="False">
        <asp:ListItem>AND</asp:ListItem>
    </asp:RadioButtonList>
    <asp:DropDownList ID="ddlGeneralUsage" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGeneralUsage_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 312px; position: absolute; width: 179px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGeneralUsage2" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGeneralUsage2_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 358px; position: absolute; bottom: 110px; width: 179px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGeneralUsage3" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGeneralUsage3_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 404px; position: absolute; width: 179px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGeneralUsage4" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGeneralUsage4_SelectedIndexChanged" 
        style="z-index: 1; left: 347px; top: 454px; position: absolute; width: 179px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlGeneralUsage5" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlGeneralUsage5_SelectedIndexChanged" 
            style="z-index: 1; left: 347px; top: 499px; position: absolute; width: 179px"> 
    </asp:DropDownList>
    <asp:Calendar ID="calCalenderSearch1" runat="server" BackColor="#FFD200" 
        ShowGridLines="True" 
        style="z-index: 1; left: 752px; top: 201px; position: absolute; height: 183px; width: 179px" 
        Visible="False" Width="179px"></asp:Calendar>
        <asp:GridView ID="gvAdvancedSearchResults" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px"   DataKeyNames="Name"
        style="z-index: 1; left: 10px; top: 340px; position: absolute; height: 213px; width: 936px" 
        AutoGenerateColumns="False" EnableViewState="True" ViewStateMode="Inherit" 
        onselectedindexchanged="gvAdvancedSearchResults_RowCommand"
         OnRowCommand="gvAdvancedSearchResults_RowCommand" >
            <AlternatingRowStyle BackColor="Silver" />
            <HeaderStyle BackColor="#999999" ForeColor="Black" />
            <RowStyle Wrap="False" />
            <Columns>
                <asp:ButtonField ButtonType="Link" DataTextField="Name" HeaderText="Name" 
                    SortExpression="Name"   CausesValidation="true"  CommandName="Name"  />
                <asp:BoundField  DataField="Address" HeaderText="Address" ReadOnly="true"  
                    SortExpression="Address"  />
                <asp:BoundField  DataField="City" HeaderText="City" ReadOnly="true"  
                    SortExpression="City"  />
                <asp:BoundField  DataField="State" HeaderText="State" ReadOnly="true"  
                    SortExpression="State"  />
                <asp:BoundField  DataField="Zip" HeaderText="Zip" ReadOnly="true"  
                    SortExpression="Zip"  />
                <asp:BoundField  DataField="HomePhone" HeaderText="HomePhone" ReadOnly="true"  
                    SortExpression="HomePhone"  />
                <asp:BoundField  DataField="CellPhone" HeaderText="CellPhone" ReadOnly="true"  
                    SortExpression="CellPhone"  />
                <asp:BoundField  DataField="School" HeaderText="School" ReadOnly="true"  
                    SortExpression="School"  />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvAddressView" runat="server" AutoGenerateColumns="False" 
            BackColor="#FFD200" DataKeyNames="Name" EnableViewState="True" 
            OnRowCommand="gvAddressView_RowCommand" 
            onselectedindexchanged="gvAddressView_RowCommand" 
            style="z-index: 1; left: 1px; top: 340px; position: absolute; height: 153px; width: 1255px" 
            ViewStateMode="Inherit">
            <AlternatingRowStyle BackColor="Silver" />
            <HeaderStyle BackColor="#999999" />
            <Columns>
                <asp:ButtonField ButtonType="Link" CausesValidation="true" CommandName="Name" 
                    DataTextField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="true" 
                    SortExpression="Address" />
                <asp:BoundField DataField="City" HeaderText="City" ReadOnly="true" 
                    SortExpression="City" />
                <asp:BoundField DataField="State" HeaderText="State" ReadOnly="true" 
                    SortExpression="State" />
                <asp:BoundField DataField="Zip" HeaderText="Zip" ReadOnly="true" 
                    SortExpression="Zip" />
                <asp:BoundField DataField="HomePhone" HeaderText="HomePhone" ReadOnly="true" 
                    SortExpression="HomePhone" />
                <asp:BoundField DataField="CellPhone" HeaderText="CellPhone" ReadOnly="true" 
                    SortExpression="CellPhone" />
                <asp:BoundField DataField="School" HeaderText="School" ReadOnly="true" 
                    SortExpression="School" />
            </Columns>
        </asp:GridView>
    <asp:CheckBox ID="chbSummerDayCamp" runat="server" AutoPostBack="True" 
        CausesValidation="True" oncheckedchanged="chbSummerDayCamp_CheckedChanged" 
        style="z-index: 1; left: 737px; top: 181px; position: absolute; width: 167px" 
        Text="SummerDay Camp" />
    <asp:CheckBox ID="chbSpecialEvents" runat="server" AutoPostBack="True" 
        CausesValidation="True" oncheckedchanged="chbSpecialEvents_CheckedChanged" 
        style="z-index: 1; left: 385px; top: 277px; position: absolute; width: 172px" 
        Text="SpecialEvents" />
    </form>
</body>
</html>
