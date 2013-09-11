<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformingArtsClasses.aspx.cs" Inherits="UIF.PerformingArts.PerformingArtsClasses" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 488px;
            height: 141px;
            position: absolute;
            top: 407px;
            left: 353px;
            z-index: 1;
        }
        .style4
        {
            position: absolute;
            top: 205px;
            left: 487px;
            z-index: 1;
        }
        .style5
        {
            position: absolute;
            top: 206px;
            z-index: 1;
            bottom: 245px;
            left: 758px;
            width: 105px;
        }
        .style6
        {
            position: absolute;
            top: 205px;
            left: 376px;
            z-index: 1;
            width: 139px;
            height: 10px;
        }
        .style7
        {
            position: absolute;
            top: 206px;
            left: 649px;
            z-index: 1;
            width: 154px;
        }
        .style8
        {
            position: absolute;
            top: 227px;
            left: 43px;
            z-index: 1;
            width: 290px;
        }
        .style9
        {
            position: absolute;
            top: 347px;
            left: 343px;
            z-index: 1;
            width: 105px;
            right: 552px;
        }
        .style10
        {
            position: absolute;
            top: 304px;
            left: 555px;
            z-index: 1;
            width: 106px;
            height: 28px;
            }
        .style12
        {
            position: absolute;
            top: 139px;
            left: 426px;
            z-index: 1;
            width: 363px;
            text-decoration: underline;
        }
        .style13
        {
            position: absolute;
            top: 251px;
            left: 367px;
            z-index: 1;
            right: 464px;
        }
        .style14
        {
            position: absolute;
            top: 253px;
            left: 642px;
            z-index: 1;
        }
        .style15
        {
            position: absolute;
            top: 195px;
            left: 911px;
            z-index: 1;
            width: 211px;
            height: 131px;
        }
        .style16
        {
            position: absolute;
            top: 334px;
            left: 978px;
            z-index: 1;
            height: 19px;
            width: 83px;
        }
        .style17
        {
            position: absolute;
            top: 335px;
            left: 1097px;
            z-index: 1;
            right: 114px;
            height: 64px;
        }
        .style18
        {
            position: absolute;
            top: 227px;
            left: 371px;
            z-index: 1;
            width: 195px;
            right: 728px;
            bottom: 282px;
        }
        .style19
        {
            position: absolute;
            top: 228px;
            left: 644px;
            z-index: 1;
            width: 203px;
        }
        .style20
        {
            position: absolute;
            top: 256px;
            left: 447px;
            z-index: 1;
        }
        .style21
        {
            position: absolute;
            top: 256px;
            left: 721px;
            z-index: 1;
            height: 16px;
        }
        .style22
        {
            position: absolute;
            top: 303px;
            left: 374px;
            z-index: 1;
            width: 159px;
            height: 29px;
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
    <p>
        <asp:Label ID="Label3" runat="server" CssClass="style12" Font-Size="XX-Large" 
            Text="Academy Class Enrollment"></asp:Label>
    </p>
    <p>
        &nbsp;</p>

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
        style="z-index: 1; left: 214px; top: 98px; position: absolute; height: 37px; width: 947px" 
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



    <asp:Label ID="lblLastUpdatedBy" runat="server" Enabled="False" 
        style="z-index: 1; left: 975px; top: 22px; position: absolute; height: 76px; width: 173px" 
        Text="LastUpdatedBy: "></asp:Label>
    <p>
        &nbsp;</p>
    <asp:GridView ID="GridView1" runat="server" CssClass="style1" 
        BackColor="#FFD200" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
        Visible="False">
        <HeaderStyle BackColor="Silver" />
    </asp:GridView>
    <asp:Label ID="Label2" runat="server" CssClass="style7" 
        Text="6:30-8:00 Class"></asp:Label>
    <p>
        <asp:Label ID="lblName" runat="server" CssClass="style8" Text="Label" 
            Font-Size="Large" BackColor="#FFD200" BorderColor="#333333" 
            BorderStyle="Solid" BorderWidth="2px"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" CssClass="style6" 
            Text="4:30-6:00 Class"></asp:Label>
        <asp:CheckBox ID="chbClass2" runat="server" CssClass="style14" Font-Size="8pt" 
            Text="(Edit Class)" AutoPostBack="True" CausesValidation="True" 
            oncheckedchanged="chbClass2_CheckedChanged" />
        <asp:TextBox ID="txbComments" runat="server" CssClass="style15" 
            BackColor="#FFD200" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="cmbViewAvailability" runat="server" 
            onclick="cmbViewAvailability_Click" 
            style="z-index: 1; left: 774px; top: 346px; position: absolute; height: 26px; width: 160px" 
            Text="View Class Availability" />
    </p>
    <p>
        <asp:CheckBox ID="chbPaidClass1" runat="server" CssClass="style4" Text="Paid" />
        <asp:CheckBox ID="chbClass1" runat="server" CssClass="style13" Font-Size="8pt" 
            Text="(Edit Class)" AutoPostBack="True" CausesValidation="True" 
            oncheckedchanged="chbClass1_CheckedChanged" />
        <asp:Button ID="cmbNewEnrollment2" runat="server" 
            onclick="cmbNewEnrollment2_Click" 
            style="z-index: 1; left: 686px; top: 304px; position: absolute; height: 29px; width: 159px" 
            Text="Class2 New Enrollment" />
    </p>
    <p>
        <asp:Label ID="lblComments" runat="server" CssClass="style16" Font-Size="8pt" 
            Text="Comments"></asp:Label>
        <asp:Image ID="imgPic1" runat="server"  ImageUrl="~/Picture1.png"
            
            
            
            
            
            
            style="z-index: 1; left: 849px; top: 397px; position: absolute; height: 115px; width: 193px" />
    </p>
    <p>
        <asp:Button ID="cmdBack" runat="server" CssClass="style9" 
            onclick="cmdBack_Click" Text="Back" />
        <asp:Button ID="cmdUpdate" runat="server" CssClass="style10" 
            onclick="cmdUpdate_Click" Text="Update Info" />
        <asp:Button ID="cmbNewEnrollment" runat="server" CssClass="style22" 
            onclick="cmbNewEnrollment_Click" Text="Class1 New Enrollment" />
    </p>
    <p>
        <asp:CheckBox ID="chbPaidClass2" runat="server" CssClass="style5" Text="Paid" />
        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="style17" Font-Size="8pt" 
            Text="(Edit Comments)" Visible="False" />
        <asp:DropDownList ID="ddlClass1" runat="server" CssClass="style18" 
            BackColor="#FFD200" AutoPostBack="True" CausesValidation="True" 
            onselectedindexchanged="ddlClass1_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlClass2" runat="server" CssClass="style19" 
            BackColor="#FFD200" AutoPostBack="True" CausesValidation="True" 
            onselectedindexchanged="ddlClass2_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:LinkButton ID="lbClass1Attendance" runat="server" CssClass="style20" 
            Font-Size="8pt" onclick="lbClass1Attendance_Click">(4:30-6:00 Class Attendance)</asp:LinkButton>
        <asp:LinkButton ID="lbClass2Attendance" runat="server" CssClass="style21" 
            Font-Size="8pt" onclick="lbClass2Attendance_Click">(6:30-8:00 Class Attendance)</asp:LinkButton>
        <asp:Label ID="lblClass1MeetLocation" runat="server" 
            style="z-index: 1; left: 344px; top: 186px; position: absolute; width: 124px" 
            Visible="False"></asp:Label>
        <asp:Label ID="lblClass2MeetLocation" runat="server" 
            style="z-index: 1; left: 638px; top: 187px; position: absolute; width: 122px" 
            Visible="False"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblClass2MeetDay" runat="server" 
            style="z-index: 1; left: 774px; top: 187px; position: absolute; width: 144px" 
            Visible="False"></asp:Label>
        <asp:Image ID="imgStudent" runat="server" ImageUrl="~/1.jpg" 
            
            
            
            style="z-index: 1; left: 42px; top: 265px; position: absolute; width: 284px; height: 224px" 
            BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" />
        <asp:Label ID="lblErrorInsert" runat="server" Font-Bold="True" 
            style="z-index: 1; left: 466px; top: 343px; position: absolute; height: 73px; width: 321px" 
            Text="The Student was NOT inserted!  The Class is already full.  Sorry.  Please try another class.   Thankyou!" 
            Visible="False"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblClass1MeetDay" runat="server" 
            style="z-index: 1; left: 478px; top: 186px; position: absolute; width: 147px" 
            Visible="False"></asp:Label>
    </p>
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
