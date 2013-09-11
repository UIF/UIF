<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="UIF.PerformingArts.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main</title>
    </head>
<body bgcolor="Orange">
    <form id="form1" runat="server">


    <script runat="server" type="text/C#">
        void page_Load()
        {
            UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
            Menu TheMenu = BuildMenu.BuildMenuControl();   
        }    
    </script>   
    

    </form>
</body>
</html>
