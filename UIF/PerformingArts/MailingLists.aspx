<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailingLists.aspx.cs" Inherits="UIF.PerformingArts.MailingLists" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #Checkbox1
        {
            z-index: 999;
            left: 731px;
            top: 210px;
            position: absolute;
            width: 81px;
        }
    </style>
    <script language="javascript"  type="text/C#">

        function Checkbox1_onclick() {

        }

    </script>


<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="Orange">
    <form id="form2" runat="server">
    <div>
    
    </div>
    <p>
        </p>

    
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
        style="z-index: 1; left: 213px; top: 97px; position: absolute; height: 37px; width: 950px" 
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

    <asp:Label ID="lblMailingLists" runat="server" Font-Size="25pt" 
        style="z-index: 1; left: 462px; top: 151px; position: absolute; height: 45px; width: 358px; text-decoration: underline; font-weight: 700;" 
        Text="Student Mailing Lists"></asp:Label>
    <asp:GridView ID="gvMailingList" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 117px; top: 418px; position: absolute; height: 111px; width: 1008px">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="#999999" />
    </asp:GridView>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 994px; top: 293px; position: absolute; height: 35px; width: 111px" 
        Text="Export to Excel" />


    <asp:CheckBox ID="chbBoysOutreachBball" runat="server" 
        style="z-index: 1; left: 17px; top: 218px; position: absolute" 
        Text="Boys Outreach Bball" 
        oncheckedchanged="chbBoysOutreachBball_CheckedChanged" />
    <asp:CheckBox ID="chbGirlsOutreachBball" runat="server" 
        style="z-index: 1; left: 17px; top: 242px; position: absolute" 
        Text="Girls Outreach Bball" 
        oncheckedchanged="chbGirlsOutreachBball_CheckedChanged" />

    <asp:CheckBox ID="chbMondayNights" runat="server" 
        oncheckedchanged="chbMondayNights_CheckedChanged" 
        style="z-index: 1; left: 364px; top: 261px; position: absolute; width: 124px; bottom: 216px;" 
        Text="Monday Nights" />
    <asp:CheckBox ID="chbLittleLeagueBaseball" runat="server" 
        style="z-index: 1; left: 364px; top: 217px; position: absolute" 
        Text="Little Lg Baseball" 
        oncheckedchanged="chbLittleLeagueBaseball_CheckedChanged" />
    <asp:CheckBox ID="chbSoccerLgTravel" runat="server" 
        style="z-index: 1; left: 197px; top: 287px; position: absolute" 
        Text="Soccer Lg Travel" 
        oncheckedchanged="chbSoccerLgTravel_CheckedChanged" />

    <asp:CheckBox ID="chbMSBasketLeague" runat="server" 
        style="z-index: 1; left: 197px; top: 217px; position: absolute" 
        Text="MS Basketball Lg" 
        oncheckedchanged="chbMSBasketLeague_CheckedChanged" />
    <asp:CheckBox ID="chbBasketballTEAMS" runat="server" 
        style="z-index: 1; left: 17px; top: 265px; position: absolute" 
        Text="Basketball TEAMS" AutoPostBack="True" CausesValidation="True" 
        oncheckedchanged="chbBasketballTEAMS_CheckedChanged" />
    <asp:CheckBox ID="chbSoccerInterMurals" runat="server" 
        style="z-index: 1; left: 197px; top: 263px; position: absolute" 
        Text="Soccer IntraMurals" 
        oncheckedchanged="chbSoccerInterMurals_CheckedChanged" />
    <asp:CheckBox ID="chb3on3Basketball" runat="server" 
        style="z-index: 1; left: 197px; top: 240px; position: absolute" 
        Text="3on3 Basketball" 
        oncheckedchanged="chb3on3Basketball_CheckedChanged" />
    <asp:CheckBox ID="chbHSBasketLeague" runat="server" 
        style="z-index: 1; left: 17px; top: 289px; position: absolute" 
        Text="HS Basketball Lg" 
        oncheckedchanged="chbHSBasketLeague_CheckedChanged" />
    <asp:CheckBox ID="chbOliverFootballBible" runat="server" 
        style="z-index: 1; left: 364px; top: 240px; position: absolute" 
        Text="Oliver Football" 
        oncheckedchanged="chbOliverFootballBible_CheckedChanged" />

    <asp:CheckBox ID="chbChildrensChoir" runat="server" CssClass="style28"  
        style="z-index: 1; left: 552px; top: 282px; position: absolute" 
        oncheckedchanged="chbChildrensChoir_CheckedChanged" 
        Text="Children's Choir" AutoPostBack="True" CausesValidation="True" />


    <asp:CheckBox ID="chbMSHSChoir" runat="server" CssClass="style29" 
        style="z-index: 1; left: 552px; top: 302px; position: absolute" 
        oncheckedchanged="chbMSHSChoir_CheckedChanged" Text="MS/HS Choir" 
        AutoPostBack="True" CausesValidation="True" />

    <asp:CheckBox ID="chbPerformingArts" runat="server" CssClass="style30" 
        style="z-index: 1; left: 552px; top: 262px; position: absolute" 
        oncheckedchanged="chbPerformingArts_CheckedChanged" Text="Performing Arts" 
        AutoPostBack="True" CausesValidation="True" />
    <asp:CheckBox ID="chbSingers" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 552px; top: 242px; position: absolute; width: 127px;" 
        Text="Singers" oncheckedchanged="chbSingers_CheckedChanged" />
    <asp:CheckBox ID="chbShakes" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 552px; top: 224px; position: absolute; right: 306px;" 
        Text="Shakes" oncheckedchanged="chbShakes_CheckedChanged" />



    <asp:Button ID="cmbRetrieveData" runat="server" onclick="cmbRetrieveData_Click" 
        style="z-index: 1; left: 543px; top: 351px; position: absolute; height: 44px; width: 154px" 
        Text="Retreive Mailing Data" />
    <asp:Label ID="lblMailingList2" runat="server" 
        style="z-index: 1; left: 203px; top: 350px; position: absolute; height: 39px; width: 310px" 
        Text="Build your mailing list by selecting the programs."></asp:Label>


    <asp:Label ID="lblAthletics" runat="server" Font-Bold="True" Font-Size="Large" 
        style="z-index: 1; left: 161px; top: 195px; position: absolute; height: 26px; width: 77px; text-decoration: underline" 
        Text="Athletics"></asp:Label>
    <asp:Label ID="lblPerformingArts" runat="server" Font-Bold="True" 
        Font-Size="Large" 
        style="z-index: 1; left: 535px; top: 199px; position: absolute; width: 149px; text-decoration: underline" 
        Text="PerformingArts"></asp:Label>


    </form>
    </body>
</html>
