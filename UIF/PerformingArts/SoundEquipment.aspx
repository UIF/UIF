<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SoundEquipment.aspx.cs" Inherits="UIF.PerformingArts.SoundEquipment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 680px;
            height: 203px;
            position: absolute;
            top: 213px;
            left: 347px;
            z-index: 1;
        }
        .style2
        {
            position: absolute;
            top: 101px;
            left: 436px;
            z-index: 1;
            width: 518px;
            height: 32px;
        }
    </style>
</head>
<body bgcolor="Orange">
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:GridView ID="gvSoundEquipment" runat="server" BorderColor="Black" 
        BorderStyle="Solid" BorderWidth="4px" CssClass="style1" 
        AutoGenerateEditButton="True">
    </asp:GridView>


    <asp:Label ID="lblSoundEquipment" runat="server" CssClass="style2" 
        Font-Bold="True" Font-Size="20pt" Text="Performing Arts Sound Equipment"></asp:Label>
    </form>
</body>
</html>
