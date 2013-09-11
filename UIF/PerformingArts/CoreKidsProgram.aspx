<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoreKidsProgram.aspx.cs" Inherits="UIF.PerformingArts.CoreKidsProgram" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="Orange">
    <form id="form1" runat="server">

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

    <asp:Label ID="lblCoreKids" runat="server" Font-Size="25pt" 
        style="z-index: 1; left: 465px; top: 143px; position: absolute; height: 47px; width: 271px; text-decoration: underline" 
        Text="Core Kids Program"></asp:Label>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1079px; top: 165px; position: absolute; height: 37px; width: 118px" 
        Text="Export to Excel" />
    <asp:Button ID="cmbRetrieveReport" runat="server" 
        onclick="cmbRetrieveReport_Click" 
        style="z-index: 1; left: 90px; top: 218px; position: absolute; height: 31px; width: 187px" 
        Text="Rebuild/Refresh the Report" Visible="False" />

    <asp:Label ID="lblCoreKidsList" runat="server" Font-Size="20pt" 
        style="z-index: 1; left: 24px; top: 278px; position: absolute; height: 49px; width: 431px" 
        Text="Here is the current list of:  CoreKids: " Visible="False"></asp:Label>
    <asp:Label ID="lblReportCard" runat="server" 
        style="z-index: 1; left: 294px; top: 308px; position: absolute; bottom: 98px; width: 149px;" 
        Text="Please click student name for more details." Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlReports_SelectedIndexChanged" 
        style="z-index: 1; left: 630px; top: 254px; position: absolute; width: 183px">
    </asp:DropDownList>
    <asp:GridView ID="gvReports" runat="server" BackColor="#FFD200" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px"
        onrowdatabound="gvReports_RowDataBound"
        style="z-index: 1; left: 614px; top: 363px; position: absolute; height: 159px; width: 251px">
        <AlternatingRowStyle BackColor="Silver" />
        <EditRowStyle Wrap="False" />
        <EmptyDataRowStyle Wrap="False" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
        <PagerStyle Wrap="False" />
        <RowStyle Wrap="False" />
        <SelectedRowStyle Wrap="False" />
    </asp:GridView>
    <asp:DropDownList ID="ddlReportProgram" runat="server" AutoPostBack="True" 
        BackColor="#FFD200" CausesValidation="True" 
        onselectedindexchanged="ddlReportProgram_SelectedIndexChanged" 
        style="z-index: 1; left: 628px; top: 306px; position: absolute; width: 193px">
    </asp:DropDownList>
    <asp:Label ID="lblProgramReport" runat="server" 
        style="z-index: 1; left: 632px; top: 329px; position: absolute; height: 21px; width: 203px" 
        Text="CoreKids By Program"></asp:Label>

    <asp:GridView ID="gvGeneralReport" runat="server" BorderColor="Black" DataKeyNames="Name"
        style="z-index: 1; left: 52px; top: 324px; position: absolute; height: 175px; width: 232px" 
        onselectedindexchanged="gvGeneralReport_RowCommand" 
        EnableViewState="True" ViewStateMode="Inherit"
        OnRowCommand="gvGeneralReport_RowCommand" 
        BackColor="#FFD200" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="Silver" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
        <Columns>
                <asp:ButtonField ButtonType="Link" DataTextField="Name" HeaderText="Name" SortExpression="Name"   CausesValidation="true"  CommandName="Name"  />
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblReports" runat="server" Font-Bold="True" Font-Size="16pt" 
        Font-Underline="True" 
        style="z-index: 1; left: 638px; top: 219px; position: absolute; height: 35px; width: 262px" 
        Text="CoreKid Reports"></asp:Label>
    <asp:Label ID="lblGeneralReports" runat="server" 
        style="z-index: 1; left: 632px; top: 276px; position: absolute; width: 181px" 
        Text="General Reports"></asp:Label>
    </form>
</body>
</html>
