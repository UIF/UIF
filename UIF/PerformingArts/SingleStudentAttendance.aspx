<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleStudentAttendance.aspx.cs" Inherits="UIF.PerformingArts.SingleStudentAttendance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body bgcolor="Orange">
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Label ID="lblSingleStudent" runat="server" Font-Size="40pt" 
        style="z-index: 1; left: 274px; top: 66px; position: absolute; height: 73px; width: 590px; text-decoration: underline" 
        Text="Single Student Attendance"></asp:Label>
    <asp:Label ID="lblPickProgram" runat="server" 
        style="z-index: 1; left: 115px; top: 164px; position: absolute; height: 34px; width: 220px" 
        Text="Please pick a program."></asp:Label>
    <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        onselectedindexchanged="ddlProgram_SelectedIndexChanged" 
        style="z-index: 1; left: 265px; top: 165px; position: absolute; width: 158px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlStudents" runat="server" AutoPostBack="True" 
        CausesValidation="True" 
        style="z-index: 1; left: 266px; top: 227px; position: absolute; width: 147px">
    </asp:DropDownList>
    </form>
</body>
</html>
