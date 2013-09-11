<%@ Page language="c#" Codebehind="VolunteerInformation.aspx.cs" AutoEventWireup="True" Inherits="UIF.PerformingArts.VolunteerInformation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
		<title>Volunteer Information</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"></meta>
		<meta name="CODE_LANGUAGE" content="C#"></meta>
		<meta name="vs_defaultClientScript" content="JavaScript"></meta>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"></meta>
	    <style type="text/css">
            #Form1
            {
                height: 1423px;
                width: 1072px;
            }
            #TextArea1
            {
                height: 102px;
                width: 225px;
            }
            .style1
            {
                width: 1177px;
            }
            .style19
            {
                position: absolute;
                top: 437px;
                left: 56px;
                z-index: 1;
                width: 607px;
                height: 30px;
            }
            .style20
            {
                position: absolute;
                top: 468px;
                left: 57px;
                z-index: 1;
                width: 132px;
                height: 9px;
                }
            .style21
            {
                position: absolute;
                top: 490px;
                left: 55px;
                z-index: 1;
                height: 45px;
                right: 283px;
                bottom: 95px;
                width: 620px;
            }
            .style22
            {
                position: absolute;
                top: 539px;
                left: 56px;
                z-index: 1;
                width: 38px;
                height: 18px;
                bottom: 161px;
            }
            .style27
            {
                position: absolute;
                top: 584px;
                left: 1053px;
                z-index: 1;
                width: 132px;
                height: 15px;
                right: 116px;
            }
            .style28
            {
                position: absolute;
                top: 184px;
                left: 521px;
                z-index: 1;
                width: 179px;
            }
            .style29
            {
                position: absolute;
                top: 164px;
                left: 521px;
                z-index: 1;
                width: 136px;
                height: 5px;
            }
            .style30
            {
                position: absolute;
                top: 204px;
                left: 521px;
                z-index: 1;
                width: 212px;
            }
            .style43
            {
                position: absolute;
                top: 258px;
                left: 1048px;
                z-index: 1;
                height: 5px;
                width: 205px;
                font-weight: bold;
                bottom: 245px;
            }
            .style44
            {
                position: absolute;
                top: 5px;
                left: 831px;
                z-index: 1;
            }
            .style45
            {
                position: absolute;
                top: 31px;
                left: 872px;
                z-index: 1;
                width: 61px;
                height: 20px;
            }
            .style49
            {
                z-index: 129;
                position: absolute;
                top: 137px;
                left: 198px;
                height: 29px;
                text-decoration: underline;
                font-weight: bold;
                right: 810px;
                width: 123px;
            }
            .style50
            {
                position: absolute;
                top: 530px;
                left: 856px;
                z-index: 1;
                width: 82px;
            }
            .style53
            {
                position: absolute;
                top: 618px;
                left: 805px;
                z-index: 1;
                width: 220px;
            }
            .style55
            {
                position: absolute;
                top: 789px;
                left: 813px;
                z-index: 1;
                width: 221px;
            }
            .style56
            {
                position: absolute;
                left: 820px;
                z-index: 1;
                width: 281px;
                top: 708px;
            }
            .style57
            {
                z-index: 134;
                position: absolute;
                top: 801px;
                left: 540px;
                width: 222px;
                }
            .style59
            {
                position: absolute;
                top: 808px;
                left: 761px;
                z-index: 1;
                width: 221px;
            }
            .style62
            {
                position: absolute;
                top: 815px;
                left: 885px;
                z-index: 1;
                width: 280px;
            }
            .style63
            {
                position: absolute;
                top: 709px;
                left: 314px;
                z-index: 1;
                width: 216px;
            }
            .style64
            {
                position: absolute;
                top: 709px;
                left: 53px;
                z-index: 1;
                width: 235px;
                right: 661px;
            }
            .style65
            {
                position: absolute;
                top: 709px;
                left: 548px;
                z-index: 1;
                width: 227px;
            }
            .style66
            {
                position: absolute;
                top: 1018px;
                left: 50px;
                z-index: 1;
                width: 727px;
            }
            .style67
            {
                height: 1505px;
            }
            .style68
            {
                position: absolute;
                top: 1061px;
                left: 243px;
                z-index: 1;
                width: 533px;
            }
            .style69
            {
                z-index: 125;
                position: absolute;
                top: 79px;
                left: 491px;
                width: 214px;
                text-decoration: underline;
                right: 232px;
            }
            .style73
            {
                z-index: 130;
                position: absolute;
                top: 137px;
                left: 520px;
                text-decoration: underline;
                font-weight: bold;
                width: 227px;
            }
            .style74
            {
                z-index: 131;
                position: absolute;
                top: 137px;
                left: 813px;
                text-decoration: underline;
                font-weight: bold;
                width: 166px;
            }
            .style75
            {
                z-index: 114;
                position: absolute;
                top: 248px;
                left: 54px;
                font-weight: bold;
            }
            .style76
            {
                z-index: 101;
                position: absolute;
                top: 268px;
                left: 156px;
                width: 173px;
                }
            .style77
            {
                z-index: 102;
                position: absolute;
                top: 268px;
                left: 58px;
                width: 94px;
                }
            .style78
            {
                z-index: 122;
                position: absolute;
                top: 291px;
                left: 159px;
                height: 9px;
            }
            .style79
            {
                z-index: 148;
                position: absolute;
                top: 291px;
                left: 104px;
            }
            .style80
            {
                z-index: 124;
                position: absolute;
                top: 291px;
                left: 59px;
            }
            .style81
            {
                z-index: 103;
                position: absolute;
                top: 311px;
                left: 58px;
                right: 743px;
                width: 269px;
            }
            .style82
            {
                z-index: 149;
                position: absolute;
                top: 329px;
                left: 91px;
            }
            .style83
            {
                z-index: 123;
                position: absolute;
                top: 332px;
                left: 57px;
            }
            .style84
            {
                z-index: 104;
                position: absolute;
                top: 350px;
                left: 57px;
                width: 270px;
                height: 22px;
            }
            .style85
            {
                z-index: 132;
                position: absolute;
                top: 371px;
                left: 57px;
                height: 5px;
            }
            .style86
            {
                z-index: 150;
                position: absolute;
                top: 370px;
                left: 119px;
                height: 15px;
                right: 823px;
            }
            .style90
            {
                z-index: 106;
                position: absolute;
                top: 389px;
                left: 56px;
                width: 272px;
            }
            .style91
            {
                z-index: 135;
                position: absolute;
                top: 413px;
                left: 57px;
            }
            .style92
            {
                z-index: 152;
                position: absolute;
                top: 413px;
                left: 158px;
                bottom: 92px;
                height: 16px;
            }
            .style96
            {
                z-index: 153;
                position: absolute;
                top: 289px;
                left: 382px;
            }
            .style97
            {
                z-index: 116;
                position: absolute;
                top: 294px;
                left: 359px;
                height: 20px;
            }
            .style99
            {
                z-index: 117;
                position: absolute;
                top: 294px;
                left: 483px;
            }
            .style100
            {
                z-index: 154;
                position: absolute;
                top: 289px;
                left: 437px;
            }
            .style102
            {
                z-index: 155;
                position: absolute;
                top: 290px;
                left: 542px;
            }
            .style103
            {
                z-index: 118;
                position: absolute;
                top: 293px;
                left: 620px;
            }
            .style105
            {
                z-index: 109;
                position: absolute;
                top: 310px;
                left: 359px;
                width: 114px;
                right: 488px;
            }
            .style106
            {
                z-index: 110;
                position: absolute;
                top: 310px;
                left: 486px;
                width: 45px;
                right: 724px;
            }
            .style107
            {
                z-index: 111;
                position: absolute;
                top: 311px;
                left: 545px;
                right: 452px;
            }
            .style108
            {
                z-index: 119;
                position: absolute;
                top: 332px;
                left: 359px;
                height: 17px;
                right: 685px;
            }
            .style109
            {
                z-index: 158;
                position: absolute;
                top: 328px;
                left: 379px;
            }
            .style110
            {
                z-index: 120;
                position: absolute;
                top: 333px;
                left: 488px;
            }
            .style111
            {
                z-index: 157;
                position: absolute;
                top: 330px;
                left: 513px;
            }
            .style112
            {
                z-index: 121;
                position: absolute;
                top: 335px;
                left: 549px;
                width: 16px;
                height: 19px;
                right: 510px;
            }
            .style113
            {
                z-index: 136;
                position: absolute;
                top: 350px;
                left: 359px;
            }
            .style115
            {
                z-index: 139;
                position: absolute;
                top: 371px;
                left: 560px;
                height: 21px;
                font-weight: bold;
            }
            .style116
            {
                z-index: 138;
                position: absolute;
                top: 373px;
                left: 361px;
            }
            .style117
            {
                z-index: 159;
                position: absolute;
                top: 369px;
                left: 456px;
            }
            .style118
            {
                z-index: 141;
                position: absolute;
                top: 389px;
                left: 359px;
                width: 214px;
            }
            .style119
            {
                z-index: 143;
                position: absolute;
                top: 411px;
                left: 360px;
                right: 667px;
                width: 44px;
            }
            .style121
            {
                z-index: 144;
                position: absolute;
                top: 412px;
                left: 601px;
            }
            .style122
            {
                z-index: 161;
                position: absolute;
                top: 407px;
                left: 659px;
            }
            .style126
            {
                z-index: 147;
                position: absolute;
                top: 528px;
                left: 687px;
                width: 157px;
                height: 29px;
            }
            .style127
            {
                margin-left: 40px;
            }
            .style128
            {
                position: absolute;
                top: 291px;
                left: 284px;
                z-index: 1;
            }
            .style129
            {
                position: absolute;
                top: 538px;
                left: 96px;
                z-index: 1;
            }
            .style130
            {
                margin-left: 80px;
            }
            .style134
            {
                position: absolute;
                top: 220px;
                left: 805px;
                z-index: 1;
                width: 223px;
            }
            .style135
            {
                position: absolute;
                top: 893px;
                left: 796px;
                z-index: 1;
                width: 301px;
                font-weight: bold;
            }
            .style136
            {
                position: absolute;
                top: 665px;
                left: 829px;
                z-index: 1;
                width: 183px;
            }
            .style137
            {
                position: absolute;
                top: 751px;
                left: 960px;
                z-index: 1;
                width: 182px;
            }
            .style138
            {
                position: absolute;
                top: 793px;
                left: 797px;
                bottom: 100px;
                z-index: 1;
                width: 173px;
            }
            .style139
            {
                position: absolute;
                top: 692px;
                left: 779px;
                z-index: 1;
                width: 61px;
            }
            .style140
            {
                position: absolute;
                top: 687px;
                left: 945px;
                z-index: 1;
                width: 97px;
                right: 107px;
            }
            .style141
            {
                position: absolute;
                top: 787px;
                left: 533px;
                z-index: 1;
                width: 177px;
            }
            .style142
            {
                position: absolute;
                top: 709px;
                left: 772px;
                z-index: 1;
                width: 101px;
            }
            .style143
            {
                position: absolute;
                top: 857px;
                left: 728px;
                z-index: 1;
            }
            .style144
            {
                position: absolute;
                top: 827px;
                left: 575px;
                z-index: 1;
                width: 269px;
                right: 105px;
            }
            .style145
            {
                position: absolute;
                top: 843px;
                left: 898px;
                z-index: 1;
                width: 47px;
                height: 18px;
                bottom: 218px;
            }
            .style146
            {
                position: absolute;
                top: 785px;
                left: 805px;
                z-index: 1;
                right: 263px;
                width: 60px;
            }
            .style147
            {
                position: absolute;
                top: 782px;
                left: 962px;
                z-index: 1;
                width: 95px;
            }
            .style148
            {
                position: absolute;
                top: 641px;
                left: 55px;
                z-index: 1;
                width: 317px;
            }
            .style149
            {
                position: absolute;
                top: 732px;
                left: 56px;
                z-index: 1;
                width: 75px;
            }
            .style150
            {
                position: absolute;
                top: 659px;
                left: 56px;
                z-index: 1;
                width: 600px;
            }
            .style151
            {
                position: absolute;
                top: 733px;
                left: 317px;
                z-index: 1;
                right: 550px;
                width: 82px;
            }
            .style152
            {
                position: absolute;
                top: 732px;
                left: 552px;
                z-index: 1;
                width: 77px;
            }
            .style153
            {
                position: absolute;
                top: 965px;
                left: 52px;
                z-index: 1;
                width: 636px;
            }
            .style154
            {
                position: absolute;
                top: 1060px;
                left: 51px;
                z-index: 1;
                width: 216px;
            }
            .style155
            {
                position: absolute;
                top: 1110px;
                left: 52px;
                z-index: 1;
                width: 113px;
            }
            .style156
            {
                position: absolute;
                top: 1112px;
                left: 327px;
                z-index: 1;
                width: 168px;
            }
            .style157
            {
                position: absolute;
                top: 1108px;
                left: 114px;
                z-index: 1;
                width: 198px;
            }
            .style158
            {
                position: absolute;
                top: 1109px;
                left: 479px;
                z-index: 1;
                width: 299px;
            }
            .style161
            {
                position: absolute;
                top: 725px;
                left: 766px;
                z-index: 1;
            }
            .style162
            {
                position: absolute;
                top: 833px;
                left: 367px;
                z-index: 1;
            }
            .style163
            {
                position: absolute;
                top: 272px;
                left: 685px;
                z-index: 1;
                height: 241px;
                width: 332px;
            }
            .style166
            {
                position: absolute;
                top: 268px;
                left: 428px;
                z-index: 1;
                width: 41px;
                right: 692px;
            }
            .style167
            {
                position: absolute;
                top: 268px;
                left: 482px;
                z-index: 1;
                width: 41px;
            }
            .style168
            {
                position: absolute;
                top: 268px;
                left: 537px;
                right: 434px;
                z-index: 1;
                width: 57px;
                height: 14px;
            }
            .style171
            {
                position: absolute;
                top: 268px;
                left: 359px;
                z-index: 1;
                width: 48px;
                right: 691px;
            }
            .style172
            {
                position: absolute;
                top: 388px;
                left: 584px;
                z-index: 1;
                width: 87px;
            }
            .style173
            {
                position: absolute;
                top: 268px;
                left: 619px;
                z-index: 1;
                width: 44px;
            }
            .style174
            {
                position: absolute;
                top: 266px;
                left: 471px;
                font-weight: bold;
                z-index: 1;
                width: 10px;
                height: 22px;
            }
            .style175
            {
                position: absolute;
                top: 267px;
                left: 526px;
                z-index: 1;
                width: 49px;
            }
            .style177
            {
                position: absolute;
                top: 349px;
                left: 556px;
                z-index: 1;
                width: 106px;
            }
            </style>

    <style type="text/css"> 
       div { z-index: 9999; } 
    </style>

	</HEAD>
	<body bgColor="#ffa500" MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server" class="style67">

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
        style="z-index: 1; left: 213px; top: 92px; position: absolute; height: 37px; width: 947px" 
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

			<div class="style130">
                <asp:TextBox id="txtLastName"
				runat="server" Enabled="False" 
                ontextchanged="txtLastName_TextChanged" CssClass="style76" TabIndex="2" 
                    BackColor="#FFD200" ToolTip="Volunteer LastName"></asp:TextBox>
            </div>
			<asp:TextBox id="txtFirstName"
				runat="server" Enabled="False" ontextchanged="txtFirstName_TextChanged" 
                CssClass="style77" TabIndex="1" BackColor="#FFD200" 
                ToolTip="Volunteer FirstName"></asp:TextBox>
			<asp:TextBox id="txtAddress1"
				runat="server" Enabled="False" ontextchanged="txtAddress1_TextChanged" 
                CssClass="style81" TabIndex="3" AutoCompleteType="LastName" 
                ToolTip="Volunteer Address"></asp:TextBox>
			<asp:TextBox id="txtHomePhone"
				runat="server" Enabled="True" ontextchanged="txtHomePhone_TextChanged" 
                CssClass="style84" TabIndex="4" ToolTip="Volunteer HomePhone"></asp:TextBox>
			<asp:TextBox id="txtStudentEmail"
				runat="server" Enabled="False" ontextchanged="txtStudentEmail_TextChanged" 
                CssClass="style90" TabIndex="5" ToolTip="Volunteer Email"></asp:TextBox>
			<asp:TextBox 
                id="txtCity" runat="server" Enabled="False" 
                ontextchanged="txtCity_TextChanged" CssClass="style105" TabIndex="12">Volunteer City Address</asp:TextBox>
			<asp:TextBox id="txtState"
				runat="server" Enabled="False" ontextchanged="txtState_TextChanged" 
                CssClass="style106" TabIndex="13">Volunteer State</asp:TextBox>
			<asp:TextBox 
                id="txtZip" runat="server"
				Width="72px" Enabled="False" ontextchanged="txtZip_TextChanged" CssClass="style107" 
                TabIndex="14">Volunteer Zip</asp:TextBox>
			<asp:Label id="lblStudentInfo"
				runat="server" CssClass="style75">VOLUNTEER INFORMATION:</asp:Label>
			<asp:Label id="Label2" runat="server" CssClass="style97">Age</asp:Label>
			<asp:Label id="Label3" runat="server" CssClass="style99">Date of Birth</asp:Label>
			<asp:Label id="Label4" runat="server" CssClass="style103">Gender</asp:Label>
			<asp:Label id="Label5" runat="server" CssClass="style108">City</asp:Label>
			<asp:Label id="Label6" runat="server" CssClass="style110">State</asp:Label>
			<asp:Label 
                id="Label7" runat="server" CssClass="style112">Zip</asp:Label>
			<asp:Label id="Label8" runat="server" CssClass="style78" Font-Size="XX-Small">LastName</asp:Label>
			<asp:Label id="Label9" runat="server" CssClass="style83">Address</asp:Label>
			<asp:Label 
                id="Label10" runat="server" CssClass="style80">FirstName</asp:Label>
			<asp:Label 
                id="Label11" runat="server" Font-Bold="True" Font-Size="16pt" 
                CssClass="style69">Volunteer Information</asp:Label>
			<asp:Label 
                id="Label15" runat="server" CssClass="style49" Font-Size="Large">Athletics</asp:Label>
			<asp:Label 
                id="Label16" runat="server" CssClass="style73" Font-Size="Large">Performing Arts</asp:Label>
			<asp:Label 
                id="Label17" runat="server" CssClass="style74" Font-Size="Large">Education</asp:Label>
			<asp:Label id="Label18" runat="server" CssClass="style85">Home Phone</asp:Label>
			<asp:Label id="Label20" runat="server" CssClass="style91">Email</asp:Label>
			<asp:TextBox id="txtStudentCellPhone"
				runat="server" Width="182px" Enabled="False" CssClass="style113" TabIndex="15">Volunteer Cell Phone</asp:TextBox>
			<asp:Label id="Label21" runat="server" CssClass="style116">Cell Phone:</asp:Label>
			<asp:Label 
                id="Label22" runat="server" CssClass="style115" Font-Size="8pt">Text Phone?</asp:Label>
			<asp:TextBox id="txtChurch"
				runat="server" Enabled="False" CssClass="style118" TabIndex="17" Visible="False">Volunteer Church</asp:TextBox>
			<asp:Label 
                id="Label24" runat="server" CssClass="style119">Church</asp:Label>
			<asp:Label 
                id="Label25" runat="server" CssClass="style121">T-shirt size</asp:Label>
			<asp:Button id="btnSubmitInformation"
				runat="server" Text="Update Information" onclick="btnSubmitInformation_Click" 
                CssClass="style126" TabIndex="24"></asp:Button>
			<asp:CheckBox id="chbLastName"
				runat="server" AutoPostBack="True" Checked="True" 
                oncheckedchanged="chbLastName_CheckedChanged" CssClass="style79" 
                Text="(Edit)"></asp:CheckBox>
			<div class="style127">
                <br />
                <asp:CheckBox id="chbAddress"
				runat="server" oncheckedchanged="chbAddress_CheckedChanged" CssClass="style82">
                </asp:CheckBox>
                <asp:TextBox ID="txbID" runat="server" CssClass="style44" Enabled="False" 
                    Visible="False"></asp:TextBox>
                <br />
                <br />
                <br />
                <asp:Label ID="lblLastUpdatedBy" runat="server" Enabled="False" 
                    style="z-index: 1; left: 1096px; top: 19px; position: absolute; height: 23px; width: 161px" 
                    Text="LastUpdatedBy: "></asp:Label>
            </div>
			<asp:CheckBox id="chbHomePhone"
				runat="server" oncheckedchanged="chbHomePhone_CheckedChanged" CssClass="style86"></asp:CheckBox>
			<asp:CheckBox id="chbStudentEmail"
				runat="server" oncheckedchanged="chbStudentEmail_CheckedChanged" CssClass="style92"></asp:CheckBox>
			<asp:CheckBox id="chbGrade"
				runat="server" oncheckedchanged="chbGrade_CheckedChanged" CssClass="style96"></asp:CheckBox>
			<asp:CheckBox id="chbAge" runat="server" 
                oncheckedchanged="chbAge_CheckedChanged" CssClass="style100"></asp:CheckBox>
			<asp:CheckBox id="chbDateBirth"
				runat="server" oncheckedchanged="chbDateBirth_CheckedChanged" CssClass="style102"></asp:CheckBox>
			<asp:CheckBox id="chbState"
				runat="server" oncheckedchanged="chbState_CheckedChanged" CssClass="style111"></asp:CheckBox>
			<asp:CheckBox id="chbCity" runat="server" 
                oncheckedchanged="chbCity_CheckedChanged" CssClass="style109"></asp:CheckBox>
			<asp:CheckBox id="chbStudentCellPhone"
				runat="server" Width="160px" oncheckedchanged="chbStudentCellPhone_CheckedChanged" 
                CssClass="style117"></asp:CheckBox>
			<asp:CheckBox id="chbTShirtSize"
				runat="server" oncheckedchanged="chbTShirtSize_CheckedChanged" CssClass="style122"></asp:CheckBox>
            <asp:DropDownList ID="ddlTShirtSize" runat="server" BackColor="#FFD200" 
                CssClass="style172" 
                onselectedindexchanged="ddlTShirtSize_SelectedIndexChanged" TabIndex="18" 
                ToolTip="Volunteer TShirt Size">
            </asp:DropDownList>
            <asp:Label ID="lblhmma" runat="server" CssClass="style175" Font-Bold="True" 
                Font-Size="18pt" Text="/"></asp:Label>
            <asp:CheckBox ID="chbShakes" runat="server" AutoPostBack="True" 
                CausesValidation="True" 
                
            style="z-index: 1; left: 521px; top: 222px; position: absolute; width: 144px" 
            oncheckedchanged="chbShakes_CheckedChanged" />
            <asp:CheckBox ID="chbSingers" runat="server" AutoPostBack="True" 
                CausesValidation="True" 
                
            
            style="z-index: 1; left: 521px; top: 240px; position: absolute; width: 166px; right: 67px;" 
            oncheckedchanged="chbSingers_CheckedChanged" />
            <asp:LinkButton ID="lbBasketballTEAMS" runat="server" ForeColor="Black" 
            onclick="lbBasketballTEAMS_Click" 
            style="z-index: 1; left: 34px; top: 182px; position: absolute">Basketball TEAMS</asp:LinkButton>
        <asp:LinkButton ID="lbMSBasketballLeague" runat="server" ForeColor="Black" 
            onclick="lbMSBasketballLeague_Click" 
            style="z-index: 1; left: 33px; top: 204px; position: absolute">MS Basketball Lg</asp:LinkButton>
            <asp:LinkButton ID="lbHSBasketballLeague" runat="server" ForeColor="Black" 
            onclick="lbHSBasketballLeague_Click" 
            
            style="z-index: 1; left: 34px; top: 225px; position: absolute; width: 140px">HS Basketball Lg</asp:LinkButton>
            <asp:LinkButton ID="lbImpactUrbanSchools" runat="server" ForeColor="Black" 
            onclick="lbImpactUrbanSchoolsAcademics_Click" 
            style="z-index: 1; left: 366px; top: 163px; position: absolute">Impact Urban Schools</asp:LinkButton>
            <asp:LinkButton ID="lbShakes" runat="server" ForeColor="Black" 
            
            style="z-index: 1; left: 541px; top: 223px; position: absolute; width: 105px" 
            onclick="lbShakes_Click">Shakes</asp:LinkButton>
            <p>
                <asp:DropDownList ID="ddlAge" runat="server" BackColor="#FFD200" 
                    CssClass="style171" TabIndex="7" ToolTip="Volunteer Age">
                </asp:DropDownList>
                <asp:Label ID="lblhmm" runat="server" CssClass="style174" Font-Size="18pt" 
                    Text="/"></asp:Label>
                <asp:CheckBox ID="chbMondayNights" runat="server" 
                    oncheckedchanged="chbMondayNights_CheckedChanged" 
                    
                    style="z-index: 1; left: 345px; top: 203px; position: absolute; width: 158px" 
                    AutoPostBack="True" CausesValidation="True" />
                <asp:CheckBox ID="chb3on3Basketball" runat="server" 
                    style="z-index: 1; left: 188px; top: 182px; position: absolute; width: 166px;" 
                    oncheckedchanged="chb3on3Basketball_CheckedChanged" AutoPostBack="True" 
                    CausesValidation="True" />
                <asp:CheckBox ID="chbBoysOutreachBball" runat="server" 
                    
                    style="z-index: 1; left: 13px; top: 162px; position: absolute; right: 544px; width: 177px;" 
                    AutoPostBack="True" CausesValidation="True" 
                    oncheckedchanged="chbBoysOutreachBball_CheckedChanged" />
                <asp:CheckBox ID="chbGirlsOutreachBball" runat="server" 
                    style="z-index: 1; left: 13px; top: 183px; position: absolute" 
                    Text="Girls Outreach Bball" Visible="False" />
                <asp:CheckBox ID="chbMailingList" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbMailingList_CheckedChanged" 
                    style="z-index: 1; left: 1047px; top: 168px; position: absolute; width: 175px" 
                    Text="Include in Mailing Lists" />
                <asp:DropDownList ID="ddlMailingListCodes" runat="server" BackColor="#FFD200" 
                    style="z-index: 1; left: 1049px; top: 194px; position: absolute; width: 163px">
                </asp:DropDownList>
                <asp:CheckBox ID="chbOfficeVolunteer" runat="server" 
                    style="z-index: 1; left: 1047px; top: 217px; position: absolute; width: 156px" 
                    Text="Office Volunteer" />
                <asp:CheckBox ID="chbDiscipleshipMentorPotentials" runat="server" 
                    Font-Size="10pt" 
                    style="z-index: 1; left: 1047px; top: 452px; position: absolute; width: 245px" 
                    Text="DiscipleshipMentor WaitingList" />
                <asp:Button ID="cmbDeletePerformingArts" runat="server" 
                    style="z-index: 9999; left: 252px; top: 374px; position: absolute; height: 41px; width: 184px" 
                    Text="Remove From PAA" Visible="False" 
                    onclick="cmbDeletePerformingArts_Click" />
                <asp:Button ID="cmbCancelPerformArts" runat="server" 
                    style="z-index: 9999; left: 252px; top: 437px; position: absolute; height: 41px; width: 183px" 
                    Text="Cancel PAA Removal" Visible="False" 
                    onclick="cmbCancelPerformArts_Click" />
                <asp:LinkButton ID="lbBaseball" runat="server" ForeColor="Black" 
                    onclick="lbBaseball_Click" 
                    
                    style="z-index: 1; left: 209px; top: 163px; position: absolute; width: 78px">Baseball</asp:LinkButton>
                <asp:LinkButton ID="lbBibleStudy" runat="server" ForeColor="Black" 
                    onclick="lbBibleStudy_Click" 
                    
                    style="z-index: 1; left: 366px; top: 183px; position: absolute; right: 419px">Bible Study</asp:LinkButton>
                <asp:LinkButton ID="lbMondayNights" runat="server" ForeColor="Black" 
                    
                    style="z-index: 1; left: 365px; top: 203px; position: absolute; right: 373px; width: 117px" 
                    onclick="lbMondayNights_Click">Monday Nights</asp:LinkButton>
                <asp:LinkButton ID="lbSpecialEvents" runat="server" ForeColor="Black" 
                    
                    style="z-index: 1; left: 366px; top: 224px; position: absolute; width: 107px; bottom: 370px" 
                    onclick="lbSpecialEvents_Click">Special Events</asp:LinkButton>
                <asp:LinkButton ID="lbOutreachBasketball" runat="server" ForeColor="Black" 
                    onclick="lbOutreachBasketball_Click" 
                    style="z-index: 1; left: 34px; top: 162px; position: absolute">Outreach Basketball</asp:LinkButton>
                <asp:LinkButton ID="lbSoccerTEAMS" runat="server" ForeColor="Black" 
                    onclick="lbSoccerTEAMS_Click" 
                    
                    style="z-index: 1; left: 209px; top: 224px; position: absolute; width: 130px">Soccer TEAMS</asp:LinkButton>
                <asp:LinkButton ID="lbSoccerIntraMurals" runat="server" ForeColor="Black" 
                    onclick="lbSoccerIntraMurals_Click" 
                    
                    style="z-index: 1; left: 209px; top: 203px; position: absolute; right: 390px; width: 135px">Soccer IntraMurals</asp:LinkButton>
                <asp:LinkButton ID="lb3on3Basketball" runat="server" ForeColor="Black" 
                    onclick="lb3on3Basketball_Click" 
                    style="z-index: 1; left: 210px; top: 183px; position: absolute; width: 134px">3on3 Basketball</asp:LinkButton>
                <asp:Label ID="lblErrorMessage" runat="server" BackColor="White" 
                    BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" Font-Size="18pt" 
                    ForeColor="Red" 
                    style="z-index: 1; left: 531px; top: 270px; position: absolute; height: 230px; width: 404px" 
                    Text="ErrorMessage:" Visible="False"></asp:Label>
                <asp:CheckBox ID="chbImpactUrbanSchoolsAcademics" runat="server" 
                    oncheckedchanged="chbImpactUrbanSchoolsAcademics_CheckedChanged" 
                    
                    
                    style="z-index: 1; left: 805px; top: 201px; position: absolute; width: 291px; height: 11px" 
                    AutoPostBack="True" CausesValidation="True" />
                <asp:LinkButton ID="lbImpactUrbanSchoolsAcademics" runat="server" 
                    ForeColor="Black" onclick="lbImpactUrbanSchoolsAcademics_Click" 
                    
                    style="z-index: 1; left: 826px; top: 201px; position: absolute; width: 240px">Impact Urban Schools</asp:LinkButton>
                <asp:CheckBox ID="chbImpactUrbanSchoolsPA" runat="server" 
                    oncheckedchanged="chbImpactUrbanSchoolsPA_CheckedChanged" 
                    
                    
                    style="z-index: 1; top: 169px; position: absolute; width: 260px; right: 1235px" 
                    AutoPostBack="True" CausesValidation="True" />
                <asp:LinkButton ID="lbAcademicReadingSupport" runat="server" ForeColor="Black" 
                    onclick="lbAcademicReadingSupport_Click" 
                    
                    
                    style="z-index: 1; left: 826px; top: 162px; position: absolute; width: 216px">Academic Reading Support</asp:LinkButton>
                <asp:LinkButton ID="lbChildrensChoir" runat="server" ForeColor="Black" 
                    onclick="lbChildrensChoir_Click" 
                    
                    style="z-index: 1; left: 540px; top: 185px; position: absolute; width: 195px; right: 209px">Children&#39;s Choir</asp:LinkButton>
                <asp:DropDownList ID="ddlChurch" runat="server" BackColor="#FFD200" 
                    style="z-index: 1; left: 358px; top: 388px; position: absolute; width: 214px">
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button ID="cmbClearPage" runat="server" CssClass="style50" 
                    onclick="cmbClearPage_Click" Text="Clear Page" />
                <asp:CheckBox ID="chbSATPrepClass" runat="server" CssClass="style134" 
                    Text="SAT Prep Class" />
                <asp:DropDownList ID="ddlMonthBirth" runat="server" BackColor="#FFD200" 
                    CssClass="style166" 
                    onselectedindexchanged="ddlMonthBirth_SelectedIndexChanged" TabIndex="8" 
                    ToolTip="Volunteer Birth Month">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlDayBirth" runat="server" BackColor="#FFD200" 
                    CssClass="style167" TabIndex="9" ToolTip="Volunteer Birth Day">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlTextPhone" runat="server" BackColor="#FFD200" 
                    CssClass="style177" TabIndex="16" ToolTip="Volunteer TextPhone">
                </asp:DropDownList>
                <asp:CheckBox ID="chbBackgroundCheck" runat="server" 
                    style="z-index: 1; left: 1047px; top: 419px; position: absolute; width: 211px; right: 43px;" 
                    Text="Background Check/Date" AutoPostBack="True" CausesValidation="True" 
                    oncheckedchanged="chbBackgroundCheck_CheckedChanged" Visible="False" />
                <asp:CheckBox ID="chbSpiritualJourney" runat="server" 
                    style="z-index: 1; left: 1047px; top: 307px; position: absolute; width: 192px" 
                    Text="Spiritual Journey" 
                    oncheckedchanged="chbSpiritualJourney_CheckedChanged" />
                <asp:CheckBox ID="chbReleaseWaiver" runat="server" 
                    style="z-index: 1; left: 1047px; top: 347px; position: absolute; width: 142px" 
                    Text="Release Waiver" />
                <asp:CheckBox ID="chbNewVolunteerTraining" runat="server" 
                    style="z-index: 1; left: 1047px; top: 368px; position: absolute; width: 216px" 
                    Text="New Volunteer Training" 
                    oncheckedchanged="chbNewVolunteerTraining_CheckedChanged" />
                <asp:DropDownList ID="ddlBackgroundMonth" runat="server" BackColor="#FFD200" 
                    
                    style="z-index: 1; left: 911px; top: 682px; position: absolute; width: 43px; right: 202px" 
                    Visible="False">
                </asp:DropDownList>
                <asp:CheckBox ID="chbNewVolunteerFlag" runat="server" 
                    style="z-index: 1; left: 861px; top: 229px; position: absolute; width: 170px" 
                    Visible="False" />
                <asp:LinkButton ID="lbSummerDayCamp" runat="server" ForeColor="Black" 
                    onclick="lbSummerDayCamp_Click" 
                    style="z-index: 1; left: 826px; top: 182px; position: absolute; width: 194px">SummerDay Camp</asp:LinkButton>
                <asp:Label ID="lblInformation" runat="server" Enabled="False" 
                    style="z-index: 1; left: 698px; top: 221px; position: absolute; right: 135px; height: 92px; width: 298px" 
                    Visible="False"></asp:Label>
                <asp:CheckBox ID="chbSoccerInterMurals" runat="server" 
                    style="z-index: 1; left: 188px; top: 202px; position: absolute; width: 180px;" 
                    oncheckedchanged="chbSoccerInterMurals_CheckedChanged" AutoPostBack="True" 
                    CausesValidation="True" />
                <asp:CheckBox ID="chbOliverFootballBible" runat="server" 
                    style="z-index: 1; left: 345px; top: 183px; position: absolute; width: 172px;" 
                    oncheckedchanged="chbOliverFootballBible_CheckedChanged" 
                    AutoPostBack="True" CausesValidation="True" />
                <asp:CheckBox ID="chbHSBasketLeague" runat="server" 
                    
                    style="z-index: 1; left: 13px; top: 225px; position: absolute; width: 177px;" 
                    AutoPostBack="True" CausesValidation="True" 
                    oncheckedchanged="chbHSBasketLeague_CheckedChanged" />
                <asp:CheckBox ID="chbBasketballTEAMS" runat="server" 
                    
                    style="z-index: 1; left: 13px; top: 183px; position: absolute; width: 187px;" 
                    AutoPostBack="True" CausesValidation="True" 
                    oncheckedchanged="chbBasketballTEAMS_CheckedChanged" />
                <asp:CheckBox ID="chbMSBasketLeague" runat="server" 
                    
                    style="z-index: 1; left: 13px; top: 203px; position: absolute; width: 164px;" 
                    AutoPostBack="True" CausesValidation="True" 
                    oncheckedchanged="chbMSBasketLeague_CheckedChanged" />
                <asp:LinkButton ID="lbPerforingArts" runat="server" 
                    onclick="lbPerforingArts_Click" 
                    
                    
                    
                    
                    style="z-index: 1; left: 683px; top: 203px; position: absolute; width: 174px; right: 207px">(View Classes)</asp:LinkButton>
                <asp:CheckBox ID="chbSummerDayCamp" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbSummerDayCamp_CheckedChanged" 
                    
                    style="z-index: 1; left: 805px; top: 182px; position: absolute; width: 186px" />
            </p>
            <p>
                <asp:DropDownList ID="ddlYearBirth" runat="server" BackColor="#FFD200" 
                    CssClass="style168" TabIndex="10" ToolTip="Volunteer Birth Year">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlGender" runat="server" BackColor="#FFD200" 
                    CssClass="style173" 
                    onselectedindexchanged="ddlGender_SelectedIndexChanged" TabIndex="11" 
                    ToolTip="Volunteer Gender">
                </asp:DropDownList>

                <asp:CheckBox ID="chbVehichleInsurance" runat="server" 
                    style="z-index: 1; left: 1047px; top: 327px; position: absolute; width: 222px" 
                    Text="Approved to Transport" />
                <asp:CheckBox ID="chbGeneralInformation" runat="server" 
                    style="z-index: 1; left: 1047px; top: 286px; position: absolute; width: 201px; bottom: 224px;" 
                    Text="General Information" 
                    oncheckedchanged="chbGeneralInformation_CheckedChanged" />

                <asp:CheckBox ID="chbDiscipleshipmentorTraining" runat="server" 
                    AutoPostBack="True" CausesValidation="True" Font-Size="10pt" 
                    oncheckedchanged="chbDiscipleshipmentorTraining_CheckedChanged" 
                    style="z-index: 1; left: 1047px; top: 429px; position: absolute; right: 58px; width: 196px; height: 16px;" 
                    Text="DiscipleshipMentor Participant" />

            &nbsp;&nbsp;&nbsp;

                <asp:CheckBox ID="chbSoccerLgTravel" runat="server" 
                    
                    style="z-index: 1; left: 188px; top: 224px; position: absolute; width: 165px;" oncheckedchanged="chbSoccerLgTravel_CheckedChanged" 
                    AutoPostBack="True" CausesValidation="True" />
                <asp:CheckBox ID="chbLittleLeagueBaseball" runat="server" 
                    
                    style="z-index: 1; left: 188px; top: 163px; position: absolute; width: 196px;" 
                    AutoPostBack="True" CausesValidation="True" 
                    oncheckedchanged="chbLittleLeagueBaseball_CheckedChanged" />

                <asp:CheckBox ID="chbStaff" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbStaff_CheckedChanged" 
                    style="z-index: 1; left: 1047px; top: 238px; position: absolute; width: 144px" 
                    Text="Staff" />
                <asp:CheckBox ID="chbSpecialEvents" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbSpecialEvents_CheckedChanged" 
                    
                    style="z-index: 1; left: 345px; top: 224px; position: absolute; width: 156px" />

                <asp:CheckBox ID="chbImpactUrbanSchools" runat="server" 
                    oncheckedchanged="chbImpactUrbanSchools_CheckedChanged" 
                    
                    style="z-index: 1; left: 345px; top: 162px; position: absolute; width: 257px" 
                    AutoPostBack="True" CausesValidation="True" />

                <asp:LinkButton ID="lbImpactUrbanSchoolsPA" runat="server" ForeColor="Black" 
                    onclick="lbImpactUrbanSchoolsAcademics_Click" 
                    
                    
                    
                    style="z-index: 1; left: 650px; top: 164px; position: absolute; width: 169px;">Impact Urban Schools</asp:LinkButton>

                <asp:CheckBox ID="chbReadingProgram" runat="server" 
                    oncheckedchanged="chbReadingProgram_CheckedChanged" 
                    
                    style="z-index: 1; left: 805px; top: 162px; position: absolute; width: 188px" 
                    AutoPostBack="True" CausesValidation="True" />

                <asp:LinkButton ID="lbSingers" runat="server" ForeColor="Black" 
                    onclick="lbSingers_Click" 
                    
                    style="z-index: 1; left: 541px; top: 240px; position: absolute; width: 137px">Singers</asp:LinkButton>
                <asp:LinkButton ID="lbMSHSChoir" runat="server" ForeColor="Black" 
                    onclick="lbMSHSChoir_Click1" 
                    
                    style="z-index: 1; left: 541px; top: 164px; position: absolute; width: 116px">MS/HS Choir</asp:LinkButton>

            <p style="margin-bottom: 86px"></p>
			    <asp:CheckBox ID="chbFirstName" runat="server" CssClass="style128" />
			    <asp:Image ID="imgPicture" runat="server" CssClass="style163"  
                    ImageUrl="~/1.jpg" BorderColor="Black" BorderStyle="Double" 
                    BorderWidth="5px"/>
			    </p>
            <asp:LinkButton ID="lbVolunteerDetails" runat="server" 
            onclick="lbVolunteerDetails_Click" 
            
            style="z-index: 1; left: 1055px; top: 387px; position: absolute; width: 158px">(Background Details)</asp:LinkButton>
            <p>
                &nbsp;</p>
            <p class="style1">
                <asp:Label ID="Label27" runat="server" CssClass="style43" Font-Size="18pt" 
                    Font-Underline="True" Text="Various Paperwork" Font-Italic="True"></asp:Label>
                <asp:TextBox ID="txbHealthConditions" runat="server" CssClass="style19" 
                    ontextchanged="txbHealthConditions_TextChanged" TabIndex="6" 
                    ToolTip="Volunteer Health Conditions" BackColor="#FFD200"></asp:TextBox>
                <asp:Label ID="lblHealthConditions" runat="server" CssClass="style20" 
                    Text="Health Conditions"></asp:Label>
                <asp:Label ID="Label28" runat="server" CssClass="style22" Text="Notes"></asp:Label>
                <asp:Label ID="lblDMEstablishedStartDate" runat="server" CssClass="style27" Text="DiscMentor Start Date" 
                    Visible="False" Font-Size="8pt"></asp:Label>
                <asp:CheckBox ID="chbChildrensChoir" runat="server" CssClass="style28" 
                    oncheckedchanged="chbChildrensChoir_CheckedChanged" AutoPostBack="True" 
                    CausesValidation="True" />


            <asp:Panel ID="pnlProgramManagement" runat="server" BackColor="Orange" 
            style="z-index: 1; left: 100px; top: 254px; position: absolute; width: 485px; height: 431px;"
            Visible="false" BorderStyle="Solid" BorderWidth="2px">
                <asp:RadioButtonList ID="rblOutreachBasketball" runat="server" 
    style="z-index: 1; left: 43px; top: 76px; position: absolute; height: 159px; width: 125px" 
                    AutoPostBack="True" CausesValidation="True" 
                    onselectedindexchanged="rblOutreachBasketball_SelectedIndexChanged" 
                    Visible="False" BackColor="#FFD200" BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="2px">
                </asp:RadioButtonList>
                <asp:Button ID="cmbOutreachBasketball" runat="server" 
                    onclick="cmbOutreachBasketball_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm Basketball Section" Visible="False" />
                <asp:Button ID="cmbOutreachBasketballCancel" runat="server" 
                    onclick="cmbOutreachBasketballCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel Outreach" Visible="False" />
                <asp:Button ID="cmbConfirmDeleteOutreach" runat="server" 
                    onclick="cmbConfirmDeleteOutreach_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm Delete Outreach" Visible="False" />
                <asp:RadioButtonList ID="rblBaseball" runat="server" AutoPostBack="True" 
                    BackColor="#FFD200" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
                    CausesValidation="True" 
                    style="z-index: 1; left: 44px; top: 87px; position: absolute; height: 141px; width: 129px; right: 308px" 
                    Visible="False">
                </asp:RadioButtonList>
                <asp:Button ID="cmbBaseball" runat="server" onclick="cmbBaseball_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm Baseball" Visible="False" />
                <asp:Button ID="cmbBaseballCancel" runat="server" 
                    onclick="cmbBaseballCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel Baseball" Visible="False" />
                <asp:Button ID="cmbRemoveBaseball" runat="server" 
                    onclick="cmbRemoveBaseball_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove from Baseball" Visible="False" />


                <asp:Button ID="cmbSoccerIntraMuralsCancel" runat="server" 
                    onclick="cmbSoccerIntraMuralsCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel SoccerIntraMurals" Visible="False" />
                <asp:Button ID="cmbSoccerTEAMSCancel" runat="server" 
                    onclick="cmbSoccerTEAMSCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel SoccerTEAMS" Visible="False" />
                <asp:Button ID="cmbMSBasketballLeagueCancel" runat="server" 
                    onclick="cmbMSBasketballLeagueCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel MS Bball League" Visible="False" />
                <asp:Button ID="cmbHSBasketballLeagueCancel" runat="server" 
                    onclick="cmbHSBasketballLeagueCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel HS Bball League" Visible="False" />
                <asp:Button ID="cmb3on3BasketballCancel" runat="server" 
                    onclick="cmb3on3BasketballCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel 3on3 Bball" Visible="False" />
                <asp:Button ID="cmbMondayNightsCancel" runat="server" 
                    onclick="cmbMondayNightsCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel MondayNights" Visible="False" />
                <asp:Button ID="cmbBibleStudyCancel" runat="server" 
                    onclick="cmbBibleStudyCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel Bible Study" Visible="False" />
                <asp:Button ID="cmbMondayNightsRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove ModayNights" Visible="False" 
                    onclick="cmbMondayNightsRemove_Click" />
                <asp:Button ID="cmbBibleStudyRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove Bible Study" Visible="False" 
                    onclick="cmbBibleStudyRemove_Click" />
                <asp:Button ID="cmbSoccerTEAMSRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove SoccerTEAMS" Visible="False" 
                    onclick="cmbSoccerTEAMSRemove_Click" />
                <asp:Button ID="cmbSoccerIntraMuralsRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove SoccerIntraMurals" Visible="False" 
                    onclick="cmbSoccerIntraMuralsRemove_Click" />
                <asp:Button ID="cmb3on3BasketballRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove 3on3 Bball" Visible="False" 
                    onclick="cmb3on3BasketballRemove_Click" />
                <asp:Button ID="cmbMSBasketballLeagueRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove MS Bball League" Visible="False" 
                    onclick="cmbMSBasketballLeagueRemove_Click" />
                <asp:Button ID="cmbHSBasketballLeagueRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove HS Bball League" Visible="False" 
                    onclick="cmbHSBasketballLeagueRemove_Click" />
                <asp:Button ID="cmbOutreachBasketballRemove" runat="server" 
                    onclick="cmbOutreachBasketballRemove_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove Outreach Basketball" Visible="False" />
                <asp:Button ID="cmbBibleStudy" runat="server" onclick="cmbBibleStudy_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm Bible Study" Visible="False" Enabled="False" />
                <asp:Button ID="cmbSoccerIntraMuralsConfirm" runat="server" 
                    onclick="cmbSoccerIntraMuralsConfirm_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm SoccerIntraMurals" Visible="False" />
                <asp:Button ID="cmbSoccerTEAMSConfirm" runat="server" 
                    onclick="cmbSoccerTEAMSConfirm_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm SoccerTEAMS" Visible="False" Enabled="False" />
                <asp:Button ID="Button1" runat="server" onclick="cmbBibleStudy_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm Bible Study" Visible="False" />
                <asp:Button ID="cmbMSBasketballLeagueConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm MS Bball League" Visible="False" 
                    onclick="cmbMSBasketballLeagueConfirm_Click" />
                <asp:Button ID="cmbHSBasketballLeagueConfirm" runat="server" 
                    onclick="cmbHSBasketballLeagueConfirm_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm HS Bball League" Visible="False" />
                <asp:Button ID="cmb3on3BasketballConfirm" runat="server" 
                    onclick="cmb3on3BasketballConfirm_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm 3on3 Bball" Visible="False" />
                <asp:Button ID="cmbMondayNightsConfirm" runat="server" 
                    onclick="cmbMondayNightsConfirm_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm MondayNights" Visible="False" />
                <asp:Button ID="cmbProgramManagement" runat="server" Enabled="False" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm BasketballTEAMS" Visible="False" 
                    onclick="cmbProgramManagement_Click" />
                <asp:Button ID="cmbProgramManageCancel" runat="server" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel BasketballTEAMS" onclick="cmbProgramManageCancel_Click" 
                    Visible="False" />
                <asp:Button ID="cmbConfirmDelete" runat="server" 
                    onclick="cmbConfirmDelete_Click" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove BasketballTEAMS" Visible="False" />

                <asp:CheckBoxList ID="cblProgramManagement" runat="server" 
                    style="z-index: 1; left: 16px; top: 84px; position: absolute; height: 167px; width: 204px" 
                    onselectedindexchanged="cblProgramManagement_SelectedIndexChanged1" 
                    BackColor="#FFD200" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="2px" AutoPostBack="True" CausesValidation="True">
                </asp:CheckBoxList>
                <asp:Button ID="cmbSpecialEventsConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm Special Events" Enabled="False" 
                    onclick="cmbSpecialEventsConfirm_Click" Visible="False" />
                <asp:Button ID="cmbSpecialEventsCancel" runat="server" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel Special Events" onclick="cmbSpecialEventsCancel_Click" 
                    Visible="False" />
                <asp:Button ID="cmbSpecialEventsRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove Special Events" onclick="cmbSpecialEventsRemove_Click" 
                    Visible="False" />

                <asp:Button ID="cmbMSHSChoirConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm MSHSChoir" Visible="False" 
                    onclick="cmbMSHSChoirConfirm_Click" />
                <asp:Button ID="cmbChildrensChoirConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm ChildrensChoir" Visible="False" 
                    onclick="cmbChildrensChoirConfirm_Click" />
                <asp:Button ID="cmbSingersConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm Singers" Visible="False" 
                    onclick="cmbSingersConfirm_Click" />
                <asp:Button ID="cmbShakesConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm Shakes" Visible="False" 
                    onclick="cmbShakesConfirm_Click" />
                <asp:Button ID="cmbPAAConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm PAA" Visible="False" 
                    onclick="cmbPAAConfirm_Click" />
                <asp:Button ID="cmbSummerDayCampConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm SummerDayCamp" Visible="False" 
                    onclick="cmbSummerDayCampConfirm_Click" />
                <asp:Button ID="cmbImpactUrbanSchoolsConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm ImpactUrbanSchools" Visible="False" 
                    onclick="cmbImpactUrbanSchoolsConfirm_Click" />
                <asp:Button ID="cmbAcademicReadingSupportConfirm" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Confirm AcademicReadingSupport" Visible="False" 
                    onclick="cmbAcademicReadingSupportConfirm_Click" />

                <asp:Button ID="cmbMSHSChoirRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove MSHSChoir" Visible="False" 
                    onclick="cmbMSHSChoirRemove_Click" />
                <asp:Button ID="cmbChildrensChoirRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove ChildrensChoir" Visible="False" 
                    onclick="cmbChildrensChoirRemove_Click" />
                <asp:Button ID="cmbSingersRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove Singers" Visible="False" 
                    onclick="cmbSingersRemove_Click" />
                <asp:Button ID="cmbShakesRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove Shakes" Visible="False" 
                    onclick="cmbShakesRemove_Click" />
                <asp:Button ID="cmbPAARemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove PerformingArtsAcademy" Visible="False" 
                    onclick="cmbPAARemove_Click" />
                <asp:Button ID="cmbSummerDayCampRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove SummerDayCamp" Visible="False" 
                    onclick="cmbSummerDayCampRemove_Click" />
                <asp:Button ID="cmbImpactUrbanSchoolsRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove ImpactUrbanSchools" Visible="False" 
                    onclick="cmbImpactUrbanSchoolsRemove_Click" />

                <asp:Button ID="cmbAcademicReadingSupportRemove" runat="server" 
                    style="z-index: 1; left: 236px; top: 85px; position: absolute; height: 58px; width: 203px" 
                    Text="Remove AcademicReadingSupport" Visible="False" 
                    onclick="cmbAcademicReadingSupportRemove_Click" />

                <asp:Button ID="cmbMSHSChoirCancel" runat="server" 
                    onclick="cmbMSHSChoirCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbChildrensChoirCancel" runat="server" 
                    onclick="cmbChildrensChoirCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbSingersCancel" runat="server" 
                    onclick="cmbSingersCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbShakesCancel" runat="server" 
                    onclick="cmbShakesCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbPAACancel" runat="server" 
                    onclick="cmbPAACancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbSummerDayCampCancel" runat="server" 
                    onclick="cmbSummerDayCampCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbImpactUrbanSchoolsCancel" runat="server" 
                    onclick="cmbImpactUrbanSchoolsCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbAcademicReadingSupportCancel" runat="server" 
                    onclick="cmbAcademicReadingSupportCancel_Click" 
                    style="z-index: 1; left: 235px; top: 196px; position: absolute; height: 55px; width: 203px" 
                    Text="Cancel" Visible="False" />
            </asp:Panel>

			<asp:Label ID="lblProgramManagement" runat="server" Font-Bold="True" 
            Font-Size="17pt" 
            style="z-index: 99999; left: 128px; top: 265px; position: absolute; height: 30px; width: 427px" 
            
            Text="To which section(s) of the Program, would you like to add the student.  Please choose." 
            Visible="False"></asp:Label>
			<asp:RadioButtonList ID="rblProgramManagement" runat="server" 
            BackColor="#FFD200" 
            style="z-index: 1; left: 118px; top: 341px; position: absolute; height: 145px; width: 191px" 
            Visible="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" 
            onselectedindexchanged="rblProgramManagement_SelectedIndexChanged" 
            AutoPostBack="True" CausesValidation="True">
            </asp:RadioButtonList>

			</p>
            <p>


                <asp:CheckBox ID="chbMSHSChoir" runat="server" CssClass="style29" 
                    oncheckedchanged="chbMSHSChoir_CheckedChanged" 
                    AutoPostBack="True" CausesValidation="True" />

                <asp:CheckBox ID="chbPerformingArts" runat="server" CssClass="style30" 
                    oncheckedchanged="chbPerformingArts_CheckedChanged" Text="Performing Arts Acad." 
                    AutoPostBack="True" CausesValidation="True" />

                <script type="text/javascript">
                    function show_confirm() {
                        var r = confirm("Press a button");
                        if (r == true) {
                            alert("You pressed OK!");
                        }
                        else {
                            alert("You pressed Cancel!");
                        }
                    }
                </script>

                <asp:Label ID="lblID" runat="server" CssClass="style45" Text="ID" 
                    Enabled="False" Visible="False"></asp:Label>
                <asp:TextBox ID="txbNotes" runat="server" CssClass="style21" 
                    ontextchanged="txbNotes_TextChanged" TabIndex="19" TextMode="MultiLine" 
                    Wrap="False" ToolTip="Volunteer Notes" BackColor="#FFD200"></asp:TextBox>
                <asp:CheckBox ID="chbNotes" runat="server" CssClass="style129" Font-Size="8pt" 
                    Text="(Edit)" oncheckedchanged="chbNotes_CheckedChanged" />
			<asp:TextBox id="txbParentGuardian2CellPhone"
				runat="server" CssClass="style57" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtParentGuardian1" runat="server" CssClass="style53" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian2" runat="server" CssClass="style55" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian1CellPhone" runat="server" CssClass="style59" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian2Email" runat="server" CssClass="style62" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="txbEmergencyRelationship" runat="server" CssClass="style64" 
                    BackColor="#FFD200"></asp:TextBox>
                <asp:TextBox ID="txbEmergencyPhone" runat="server" CssClass="style65" 
                    BackColor="#FFD200"></asp:TextBox>
                <asp:TextBox ID="txbAllergies" runat="server" CssClass="style66" 
                    Enabled="False" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txbInsuranceCompany" runat="server" CssClass="style68" 
                    Enabled="False" Visible="False"></asp:TextBox>
                <asp:Label ID="lblParentGuardian" runat="server" CssClass="style135" 
                    Text="PARENT/GUARDIAN INFORMATION:" Font-Bold="True" Visible="False"></asp:Label>
                <asp:TextBox ID="txbParentGuardian1WrkPh" runat="server" CssClass="style136" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian1Email" runat="server" CssClass="style56" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian2WrkPh" runat="server" CssClass="style137" 
                    Visible="False"></asp:TextBox>
                <asp:Label ID="Label31" runat="server" CssClass="style138" 
                    Text="Parent/Legal Guardian #1" Visible="False"></asp:Label>
                <asp:Label ID="Label32" runat="server" CssClass="style139" Text="Relationship1" 
                    Visible="False"></asp:Label>
                <asp:Label ID="Label33" runat="server" CssClass="style140" Text="Work Phone1" 
                    Visible="False"></asp:Label>
                <asp:Label ID="Label34" runat="server" CssClass="style141" 
                    Text="Parent/Legal Guardian #2" Visible="False"></asp:Label>
                <asp:Label ID="Label35" runat="server" CssClass="style142" Text="Email1" 
                    Visible="False"></asp:Label>
                <asp:Label ID="Label36" runat="server" CssClass="style143" 
                    Text="Cell Phone: may we text phone?  Y/N" Visible="False"></asp:Label>
                <asp:Label ID="Label37" runat="server" CssClass="style144" 
                    Text="Cell Phone: may we text this phone? Y/N" Visible="False"></asp:Label>
                <asp:Label ID="Label38" runat="server" CssClass="style145" Text="Email2" 
                    Visible="False"></asp:Label>
                <asp:Label ID="Label39" runat="server" CssClass="style146" Text="Relationship2" 
                    Visible="False"></asp:Label>
                <asp:Label ID="Label40" runat="server" CssClass="style147" Text="Work Phone2" 
                    Visible="False"></asp:Label>
                <asp:Label ID="Label41" runat="server" CssClass="style148" Font-Bold="True" 
                    Text="EMERGENCY CONTACT INFORMATION:"></asp:Label>
                <asp:Label ID="Label42" runat="server" CssClass="style149" Text="Name"></asp:Label>
                <asp:Label ID="Label43" runat="server" CssClass="style150" 
                    
                    
                    Text="In the event of an emergency and you cannot be reached please give a name and phone number of an Authorized/Designated individual to make emergency decisions:"></asp:Label>
                <asp:Label ID="Label44" runat="server" CssClass="style151" Text="Relationship"></asp:Label>
                <asp:Label ID="Label45" runat="server" CssClass="style152" Text="Phone #"></asp:Label>
                <asp:Label ID="Label46" runat="server" CssClass="style153" 
                    
                    Text="Please list any allergies or health concerns which may be relevant to a physician in the event of an emergency and indicate any activity restrictions (including previous injuries)." 
                    Visible="False"></asp:Label>
                <asp:Label ID="Label47" runat="server" CssClass="style154" 
                    Text="Medical Insurance Company:" Visible="False"></asp:Label>
                <asp:Label ID="Label48" runat="server" CssClass="style155" Text="Policy #:" 
                    Visible="False"></asp:Label>
                <asp:Label ID="Label49" runat="server" CssClass="style156" 
                    Text="Primary Care Physician:" Visible="False"></asp:Label>
                <asp:TextBox ID="TextBox12" runat="server" CssClass="style157" Enabled="False" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="TextBox13" runat="server" CssClass="style158" Enabled="False" 
                    Visible="False"></asp:TextBox>
                <asp:TextBox ID="txbEmergRelationship" runat="server" CssClass="style63" 
                    BackColor="#FFD200"></asp:TextBox>
                <asp:CheckBox ID="chbParentGuardianEdit" runat="server" Checked="True" 
                    CssClass="style161" Text="(Edit ParentGuardian)" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbParentGuardianEdit_CheckedChanged" Visible="False" />
                <asp:CheckBox ID="chbEmergencyContactEdit" runat="server" Checked="True" 
                    CssClass="style162" Text="(Edit EmergencyContact)" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbEmergencyContactEdit_CheckedChanged" 
                    Visible="False" />
                <asp:DropDownList ID="ddlParentGuardian1Relationship" runat="server" 
                    BackColor="#FFD200" 
                    onselectedindexchanged="ddlParentGuardian1Relationship_SelectedIndexChanged" 
                    
                    style="z-index: 1; left: 717px; top: 658px; position: absolute; width: 123px; height: 24px" 
                    Visible="False">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlParentGuardian2Relationship" runat="server" 
                    BackColor="#FFD200" 
                    onselectedindexchanged="ddlParentGuardian2Relationship_SelectedIndexChanged" 
                    
                    style="z-index: 1; left: 793px; top: 750px; position: absolute; width: 129px" 
                    Visible="False">

                </asp:DropDownList>
                <asp:Label ID="lblDiscipleshipmentortraineddate" runat="server" Font-Size="8pt" 
                    style="z-index: 1; left: 1053px; position: absolute; top: 535px; width: 134px; height: 11px" 
                    Text="DiscMentor Trained Date"></asp:Label>
                <asp:DropDownList ID="ddlMonthBirth2" runat="server" BackColor="#FFD200" 
                    
                    
                    style="z-index: 1; left: 1050px; top: 510px; position: absolute; width: 48px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlDayBirth2" runat="server" BackColor="#FFD200" 
                    
                    
                    style="z-index: 1; left: 1107px; top: 511px; position: absolute; width: 47px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlYearBirth2" runat="server" BackColor="#FFD200" 
                    
                    
                    style="z-index: 1; left: 1162px; top: 511px; position: absolute; width: 56px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMonthBirth3" runat="server" BackColor="#FFD200" 
                    
                    
                    
                    style="z-index: 1; left: 1049px; top: 560px; position: absolute; width: 47px; height: 76px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlDayBirth3" runat="server" BackColor="#FFD200" 
                    
                    
                    style="z-index: 1; left: 1107px; top: 560px; position: absolute; width: 44px; right: 150px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlYearBirth3" runat="server" BackColor="#FFD200" 
                    
                    
                    style="z-index: 1; left: 1161px; top: 560px; position: absolute; width: 58px; right: 82px">
                </asp:DropDownList>
            </p>
            <asp:Panel ID="Panel1" runat="server" 
                style="z-index: 1; left: 10px; top: 414px; position: absolute; height: 19px; width: 940px">
            </asp:Panel>
            <asp:Panel ID="pnlPanel" runat="server" BorderColor="Black" BorderStyle="Solid" 
                BorderWidth="2px" 
                
            
            
            
            style="z-index: 1; left: 1041px; top: 417px; position: absolute; height: 188px; width: 214px">
            </asp:Panel>
        <asp:Button ID="cmbNewPerson2" runat="server" onclick="cmbNewPerson2_Click" 
            style="z-index: 1; left: 943px; top: 530px; position: absolute; width: 87px" 
            Text="New Person" />
        <asp:TextBox ID="txbDiscipleshipMentorNotes" runat="server" BackColor="#FFD200" 
            CssClass="style21" 
            style="z-index: 1; left: 54px; top: 564px; position: absolute" TabIndex="20" 
            TextMode="MultiLine" ToolTip="DiscipleshipMentor Notes" Wrap="False"></asp:TextBox>
        <asp:Label ID="lblDiscipleshipMentorNotes" runat="server" CssClass="style20" 
            style="z-index: 1; left: 60px; top: 612px; position: absolute; height: 19px; width: 184px" 
            Text="DiscipleshipMentor Notes"></asp:Label>
        <asp:CheckBox ID="chbDiscipleshipmentorWaitingList" runat="server" 
            Font-Size="10pt" 
            style="z-index: 1; left: 1047px; top: 460px; position: absolute; width: 252px" 
            Text="DiscipleshipMentor WaitingList" Enabled="False" Visible="False" 
            oncheckedchanged="chbDiscipleshipmentorWaitingList_CheckedChanged" />
        <asp:CheckBox ID="chbDiscipleshipmentorTrainingDone" runat="server" 
            Font-Size="10pt" 
            style="z-index: 1; left: 1047px; top: 476px; position: absolute; width: 226px" 
            Text="Discipleshipmentor Trained?" AutoPostBack="True" 
            CausesValidation="True" 
            oncheckedchanged="chbDiscipleshipmentorTrainingDone_CheckedChanged" />
        <asp:DropDownList ID="ddlBackgroundDay" runat="server" BackColor="#FFD200" 
            
            style="z-index: 1; left: 1071px; top: 650px; position: absolute; width: 45px" 
            Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlBackgroundYear" runat="server" BackColor="#FFD200" 
            
            style="z-index: 1; left: 1124px; top: 650px; position: absolute; width: 55px" 
            Visible="False">
        </asp:DropDownList>
        <asp:Label ID="lblOfficeInformation" runat="server" Font-Bold="True" 
            Font-Italic="True" Font-Size="18pt" 
            style="z-index: 1; left: 1048px; top: 139px; position: absolute; height: 33px; width: 211px; text-decoration: underline" 
            Text="Office Information"></asp:Label>
        <asp:DropDownList ID="ddlMostRecentSeason" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 710px; top: 577px; position: absolute; width: 125px">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlMostRecentSeasonYear" runat="server" 
            BackColor="#FFD200" 
            style="z-index: 1; left: 839px; top: 577px; position: absolute">
        </asp:DropDownList>
        </form>
	</body>
</HTML>

