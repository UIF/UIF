<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcademyClassEnrollment.aspx.cs" Inherits="UIF.PerformingArts.AcademyClassEnrollment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            position: absolute;
            top: 59px;
            left: 471px;
            z-index: 1;
            width: 238px;
            height: 61px;
            text-decoration: underline;
        }
        .style2
        {
            width: 488px;
            height: 221px;
            position: absolute;
            top: 214px;
            left: 346px;
            z-index: 1;
        }
        .style3
        {
            position: absolute;
            top: 66px;
            left: 998px;
            z-index: 1;
            width: 120px;
            height: 30px;
        }
    </style>
</head>
<body bgcolor="Orange">
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Label ID="lblClassEnrollment" runat="server" CssClass="style1" 
        Font-Bold="True" Font-Size="23pt" Text="ClassEnrollment"></asp:Label>
    <asp:GridView ID="gvClassEnrollment" runat="server" BorderColor="Black" 
        BorderStyle="Solid" BorderWidth="4px" CssClass="style2">
    </asp:GridView>
    <asp:Button ID="cmbBack" runat="server" CssClass="style3" 
        onclick="cmbBack_Click" Text="Back" />
    </form>
</body>
</html>
