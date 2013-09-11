<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAttendanceOptions.aspx.cs" Inherits="UIF.PerformingArts.StudentAttendanceOptions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            position: absolute;
            top: 151px;
            left: 114px;
            z-index: 1;
            width: 214px;
            height: 38px;
        }
        .style2
        {
            position: absolute;
            top: 223px;
            left: 115px;
            z-index: 1;
            width: 214px;
            height: 36px;
        }
        .style3
        {
            position: absolute;
            top: 286px;
            left: 114px;
            z-index: 1;
            width: 214px;
            height: 38px;
        }
        .style4
        {
            position: absolute;
            top: 63px;
            left: 378px;
            z-index: 1;
            width: 286px;
            height: 55px;
            font-weight: bold;
            text-decoration: underline;
        }
        .style5
        {
            position: absolute;
            top: 66px;
            left: 1069px;
            z-index: 1;
            width: 85px;
            height: 41px;
        }
    </style>
</head>
<body bgcolor=Orange>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Button ID="cmdEnterEventAttendance" runat="server" CssClass="style1" 
        onclick="cmdEnterEventAttendance_Click" Text="Enter Event Attendance " />
    <asp:Button ID="cmdEnterSingleStudentAttend" runat="server" CssClass="style2" 
        onclick="cmdEnterSingleStudentAttend_Click" 
        Text="Enter Single Student Attendance" />
    <asp:Button ID="cmbViewAttendance" runat="server" CssClass="style3" 
        onclick="cmbViewAttendance_Click" Text="View Attendance/Reporting" />
    <asp:Label ID="Label1" runat="server" CssClass="style4" Font-Size="XX-Large" 
        Text="Attendance Options"></asp:Label>
    <asp:Button ID="cmdBack" runat="server" CssClass="style5" 
        onclick="cmdBack_Click" Text="Back" />
    </form>
</body>
</html>
