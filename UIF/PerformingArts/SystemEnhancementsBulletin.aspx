<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemEnhancementsBulletin.aspx.cs" Inherits="UIF.PerformingArts.SystemEnhancementsBulletin" %>

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
        style="z-index: 1; left: 213px; top: 93px; position: absolute; height: 37px; width: 947px" 
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



    <asp:Label ID="lblSystemEnhancements" runat="server" Font-Size="25pt" 
        style="z-index: 1; left: 430px; top: 153px; position: absolute; height: 50px; width: 432px; text-decoration: underline" 
        Text="System Enhancements Bulletin"></asp:Label>
    <asp:GridView ID="gvSystemEnhancements" runat="server" 
        AutoGenerateEditButton="True" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="4px" 
        onselectedindexchanged="gvSystemEnhancements_SelectedIndexChanged" OnRowEditing="gvSystemEnhancements_RowEditing" 
        OnRowUpdating="gvSystemEnhancements_RowUpdating"
        
        style="z-index: 1; left: 235px; top: 246px; position: absolute; height: 133px; width: 187px" 
        BackColor="#FFD200">
        <AlternatingRowStyle BackColor="#999999" ForeColor="Black" />
    </asp:GridView>
    </form>
</body>
</html>
