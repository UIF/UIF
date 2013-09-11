<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VolunteerAdmin.aspx.cs" Inherits="UIF.PerformingArts.VolunteerAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            position: absolute;
            top: 165px;
            left: 161px;
            z-index: 1;
            width: 203px;
            height: 11px;
        }
        .style2
        {
            position: absolute;
            top: 161px;
            left: 382px;
            z-index: 1;
            right: 401px;
        }
    </style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="#ff9900">
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="style1">
    </asp:DropDownList>
    <asp:datagrid style="Z-INDEX: 105; POSITION: absolute; TOP: 248px; LEFT: 448px" id="dgrdVolunteers"
				runat="server" onselectedindexchanged="dgrdData_SelectedIndexChanged"></asp:datagrid>
    <asp:Button ID="Button1" runat="server" CssClass="style2" Text="Button" />
    </form>
</body>
</html>
