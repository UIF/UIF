<%@ Page language="c#" Codebehind="RegistrationForm.aspx.cs" AutoEventWireup="True" Inherits="UIF.PerformingArts.RegistrationForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
		<title>RegistrationForm</title>
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

            

            .style11
            {
                position: absolute;
                top: 497px;
                left: 1041px;
                z-index: 1;
                height: 5px;
                width: 204px;
                font-weight: bold;
            }
            .style19
            {
                position: absolute;
                top: 473px;
                left: 56px;
                z-index: 1;
                width: 600px;
            }
            .style20
            {
                position: absolute;
                top: 495px;
                left: 57px;
                z-index: 1;
                width: 132px;
                height: 9px;
                right: 695px;
            }
            .style21
            {
                position: absolute;
                top: 522px;
                left: 56px;
                z-index: 1;
                height: 48px;
                right: 518px;
                bottom: 114px;
                width: 603px;
            }
            .style22
            {
                position: absolute;
                top: 571px;
                left: 59px;
                z-index: 1;
                width: 38px;
                height: 18px;
                bottom: 94px;
            }
            .style24
            {
                position: absolute;
                top: 809px;
                left: 807px;
                z-index: 1;
                width: 159px;
            }
            .style25
            {
                position: absolute;
                top: 831px;
                left: 821px;
                z-index: 1;
                width: 129px;
                height: 3px;
            }
            .style26
            {
                position: absolute;
                top: 661px;
                left: 1050px;
                z-index: 1;
                width: 127px;
            }
            .style27
            {
                position: absolute;
                top: 684px;
                left: 1055px;
                z-index: 1;
                width: 140px;
                height: 12px;
            }
            .style28
            {
                position: absolute;
                top: 184px;
                left: 552px;
                z-index: 1;
                bottom: 245px;
                width: 193px;
            }
            .style29
            {
                position: absolute;
                top: 166px;
                left: 552px;
                z-index: 1;
                width: 180px;
                height: 5px;
            }
            .style30
            {
                position: absolute;
                top: 203px;
                left: 552px;
                z-index: 1;
                width: 184px;
                right: 624px;
            }
            .style32
            {
                position: absolute;
                top: 281px;
                left: 1043px;
                z-index: 1;
                width: 217px;
                height: 12px;
            }
            .style33
            {
                position: absolute;
                top: 336px;
                left: 1043px;
                z-index: 1;
                bottom: 201px;
                width: 273px;
            }
            .style34
            {
                position: absolute;
                top: 355px;
                left: 1043px;
                z-index: 1;
                height: 8px;
                bottom: 182px;
                right: 134px;
                width: 141px;
            }
            .style35
            {
                position: absolute;
                top: 373px;
                left: 1043px;
                z-index: 1;
                width: 294px;
            }
            .style36
            {
                position: absolute;
                top: 711px;
                left: 1045px;
                z-index: 1;
            }
            .style37
            {
                position: absolute;
                top: 758px;
                left: 1045px;
                z-index: 1;
                height: 4px;
                bottom: 94px;
            }
            .style38
            {
                position: absolute;
                top: 733px;
                left: 1045px;
                z-index: 1;
                width: 107px;
                height: 11px;
                bottom: 119px;
            }
            .style39
            {
                position: absolute;
                top: 697px;
                left: 1042px;
                z-index: 1;
                height: 14px;
                }
            .style40
            {
                position: absolute;
                top: 544px;
                left: 1042px;
                z-index: 1;
                width: 273px;
            }
            .style41
            {
                position: absolute;
                top: 567px;
                left: 1042px;
                z-index: 1;
                bottom: 65px;
                width: 176px;
            }
            .style43
            {
                position: absolute;
                top: 254px;
                left: 1046px;
                z-index: 1;
                height: 5px;
                width: 172px;
                font-weight: bold;
                bottom: 333px;
            }
            .style44
            {
                position: absolute;
                top: 5px;
                left: 735px;
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
            .style47
            {
                position: absolute;
                top: 636px;
                left: 1056px;
                z-index: 1;
                width: 102px;
            }
            .style49
            {
                z-index: 129;
                position: absolute;
                top: 138px;
                left: 195px;
                height: 29px;
                text-decoration: underline;
                font-weight: bold;
                right: 596px;
                width: 133px;
            }
            .style50
            {
                position: absolute;
                top: 529px;
                left: 856px;
                z-index: 1;
                width: 82px;
                right: 235px;
            }
            .style51
            {
                position: absolute;
                top: 205px;
                left: 715px;
                z-index: 1;
                width: 152px;
                text-decoration: none;
            }
            .style53
            {
                position: absolute;
                top: 628px;
                left: 56px;
                z-index: 1;
                width: 220px;
            }
            .style55
            {
                position: absolute;
                top: 729px;
                left: 54px;
                z-index: 1;
                width: 221px;
            }
            .style56
            {
                position: absolute;
                left: 317px;
                z-index: 1;
                width: 281px;
                top: 671px;
            }
            .style57
            {
                z-index: 134;
                position: absolute;
                top: 776px;
                left: 54px;
                width: 111px;
                }
            .style59
            {
                position: absolute;
                top: 669px;
                left: 56px;
                z-index: 1;
                width: 111px;
            }
            .style62
            {
                position: absolute;
                top: 777px;
                left: 317px;
                z-index: 1;
                width: 260px;
            }
            .style63
            {
                position: absolute;
                top: 907px;
                left: 314px;
                z-index: 1;
                width: 216px;
            }
            .style64
            {
                position: absolute;
                top: 907px;
                left: 49px;
                z-index: 1;
                width: 235px;
                right: 736px;
            }
            .style65
            {
                position: absolute;
                top: 909px;
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
                top: 81px;
                left: 500px;
                width: 180px;
                text-decoration: underline;
            }
            .style73
            {
                z-index: 130;
                position: absolute;
                top: 139px;
                left: 554px;
                text-decoration: underline;
                font-weight: bold;
                width: 222px;
            }
            .style74
            {
                z-index: 131;
                position: absolute;
                top: 139px;
                left: 931px;
                text-decoration: underline;
                font-weight: bold;
                width: 158px;
            }
            .style75
            {
                z-index: 114;
                position: absolute;
                top: 250px;
                left: 54px;
                font-weight: bold;
            }
            .style76
            {
                z-index: 101;
                position: absolute;
                top: 270px;
                left: 213px;
                width: 116px;
                }
            .style77
            {
                z-index: 102;
                position: absolute;
                top: 270px;
                left: 58px;
                width: 88px;
                right: 854px;
            }
            .style78
            {
                z-index: 122;
                position: absolute;
                top: 293px;
                left: 217px;
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
                top: 292px;
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
            .style88
            {
                z-index: 133;
                position: absolute;
                top: 411px;
                left: 56px;
            }
            .style89
            {
                z-index: 151;
                position: absolute;
                top: 410px;
                left: 87px;
                height: 14px;
            }
            .style90
            {
                z-index: 106;
                position: absolute;
                top: 433px;
                left: 56px;
                width: 272px;
            }
            .style91
            {
                z-index: 135;
                position: absolute;
                top: 454px;
                left: 56px;
            }
            .style92
            {
                z-index: 152;
                position: absolute;
                top: 454px;
                left: 118px;
                bottom: 177px;
                height: 16px;
            }
            .style95
            {
                z-index: 115;
                position: absolute;
                top: 294px;
                left: 361px;
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
                left: 409px;
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
                top: 296px;
                left: 630px;
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
                right: 271px;
            }
            .style115
            {
                z-index: 139;
                position: absolute;
                top: 372px;
                left: 599px;
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
            .style123
            {
                z-index: 145;
                position: absolute;
                top: 432px;
                left: 359px;
                width: 126px;
            }
            .style124
            {
                z-index: 146;
                position: absolute;
                top: 453px;
                left: 360px;
            }
            .style125
            {
                position: absolute;
                top: 528px;
                left: 892px;
                z-index: 1;
                width: 132px;
                height: 47px;
            }
            .style126
            {
                z-index: 147;
                position: absolute;
                top: 528px;
                left: 686px;
                width: 199px;
                height: 47px;
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
                top: 572px;
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
                top: 226px;
                left: 906px;
                z-index: 1;
                width: 253px;
            }
            .style135
            {
                position: absolute;
                top: 606px;
                left: 54px;
                z-index: 1;
                width: 301px;
                font-weight: bold;
            }
            .style136
            {
                position: absolute;
                top: 628px;
                left: 477px;
                z-index: 1;
                width: 183px;
            }
            .style137
            {
                position: absolute;
                top: 729px;
                left: 476px;
                z-index: 1;
                width: 182px;
            }
            .style138
            {
                position: absolute;
                top: 649px;
                left: 56px;
                bottom: 168px;
                z-index: 1;
                width: 173px;
            }
            .style139
            {
                position: absolute;
                top: 650px;
                left: 319px;
                z-index: 1;
                width: 61px;
            }
            .style140
            {
                position: absolute;
                top: 649px;
                left: 480px;
                z-index: 1;
                width: 97px;
                right: 329px;
            }
            .style141
            {
                position: absolute;
                top: 751px;
                left: 57px;
                z-index: 1;
                width: 177px;
            }
            .style142
            {
                position: absolute;
                top: 693px;
                left: 320px;
                z-index: 1;
                width: 101px;
            }
            .style143
            {
                position: absolute;
                top: 692px;
                left: 54px;
                z-index: 1;
            }
            .style144
            {
                position: absolute;
                top: 799px;
                left: 54px;
                z-index: 1;
                width: 269px;
                right: 492px;
            }
            .style145
            {
                position: absolute;
                top: 800px;
                left: 320px;
                z-index: 1;
                width: 47px;
                height: 18px;
                bottom: 102px;
            }
            .style146
            {
                position: absolute;
                top: 751px;
                left: 317px;
                z-index: 1;
                right: 417px;
                width: 60px;
            }
            .style147
            {
                position: absolute;
                top: 751px;
                left: 479px;
                z-index: 1;
                width: 95px;
            }
            .style148
            {
                position: absolute;
                top: 835px;
                left: 51px;
                z-index: 1;
                width: 361px;
            }
            .style149
            {
                position: absolute;
                top: 936px;
                left: 51px;
                z-index: 1;
                width: 75px;
            }
            .style150
            {
                position: absolute;
                top: 861px;
                left: 54px;
                z-index: 1;
                width: 600px;
            }
            .style151
            {
                position: absolute;
                top: 935px;
                left: 316px;
                z-index: 1;
                right: 570px;
                width: 82px;
            }
            .style152
            {
                position: absolute;
                top: 933px;
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
                top: 605px;
                left: 339px;
                z-index: 1;
            }
            .style162
            {
                position: absolute;
                top: 833px;
                left: 437px;
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
            .style164
            {
                position: absolute;
                top: 232px;
                left: 726px;
                z-index: 1;
                width: 276px;
                height: 60px;
            }
            .style166
            {
                position: absolute;
                top: 271px;
                left: 465px;
                z-index: 1;
                width: 41px;
                right: 306px;
            }
            .style167
            {
                position: absolute;
                top: 271px;
                left: 516px;
                z-index: 1;
                right: 653px;
                width: 41px;
            }
            .style168
            {
                position: absolute;
                top: 271px;
                left: 566px;
                right: 252px;
                z-index: 1;
                width: 57px;
            }
            .style170
            {
                position: absolute;
                top: 271px;
                left: 357px;
                right: 410px;
                z-index: 1;
                width: 45px;
            }
            .style171
            {
                position: absolute;
                top: 271px;
                left: 406px;
                z-index: 1;
                width: 48px;
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
                top: 271px;
                left: 631px;
                z-index: 1;
                width: 44px;
            }
            .style174
            {
                position: absolute;
                top: 269px;
                left: 507px;
                font-weight: bold;
                z-index: 1;
                width: 77px;
                height: 22px;
            }
            .style175
            {
                position: absolute;
                top: 268px;
                left: 557px;
                z-index: 1;
                width: 21px;
            }
            .style176
            {
                position: absolute;
                top: 614px;
                left: 1051px;
                z-index: 1;
                width: 114px;
            }
            .style177
            {
                position: absolute;
                top: 350px;
                left: 567px;
                z-index: 1;
                width: 106px;
            }
            </style>

    <style type="text/css"> 
       div { z-index: 9999; } 
    </style>




        <script type="text/javascript">
            function Backward(oSpyID)
            {
               // The hidden post-back spy or counter field
               var spy = null;
               // Total number of post-backs
               var refreshes = new Number(0);
               // Allows the actual previous page to be selected
               var offset = new Number(1);
   
               spy = document.getElementById(oSpyID);
       
               refreshes = new Number(spy.value) + offset;
                           
               history.go(-refreshes);
               // Redirects to the actual previous page
            }

            function Forward()
            {
               history.forward(1);
               // Redirects if the next page exists,
               // including the post-back versions.
            }
        </script>



	</HEAD>
    
    <body bgColor="#ffa500" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server" class="style67" defaultfocus="txtFirstName" defaultbutton="btnSubmitInformation" >


    <asp:HyperLink CssClass="navPages" ID="hpBackward" Runat="server">
          <img src="~/PerformingArts/Picture1.png" /></asp:HyperLink>
    <asp:HyperLink CssClass="navPages" ID="hpForward" Runat="server">
          <img src="~/PerformingArts/Picture1.png" /></asp:HyperLink>
    <input type="hidden" id="inputPostBackSpy" runat="server" />


    <asp:Panel ID="pnlBackground" runat="server" BackColor="White"  
        style="z-index: 1; left: -12px; top: 1px; position: absolute; height: 133px; width: 1298px" 
        ViewStateMode="Enabled" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px">
        <asp:Label ID="lblUrbanImpact" runat="server" Font-Bold="True" Font-Size="36pt" 
            style="z-index: 1; left: 354px; top: 24px; position: absolute; height: 62px; width: 547px" 
            Text="Urban Impact Foundation"></asp:Label>
        <asp:CheckBox ID="chbBoysOutreachBball" runat="server" 
            
            style="z-index: 1; left: 26px; top: 161px; position: absolute; width: 198px;" 
            AutoPostBack="True" CausesValidation="True" 
            oncheckedchanged="chbBoysOutreachBball_CheckedChanged" TabIndex="99" />
        <asp:CheckBox ID="chbGirlsOutreachBball" runat="server" 
            style="z-index: 1; left: 26px; top: 182px; position: absolute" 
            Text="Girls Outreach Bball" AutoPostBack="True" CausesValidation="True" 
            oncheckedchanged="chbGirlsOutreachBball_CheckedChanged" TabIndex="99" 
            Enabled="False" Visible="False" />
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

			<div class="style130">
                <asp:TextBox id="txtLastName"
				runat="server" Enabled="False" 
                ontextchanged="txtLastName_TextChanged" CssClass="style76" TabIndex="2" 
                    BackColor="#FFD200"></asp:TextBox>
			<div class="style127">
                <br />
                <asp:CheckBox id="chbAddress"
				runat="server" oncheckedchanged="chbAddress_CheckedChanged" CssClass="style82">
                </asp:CheckBox>
                <asp:TextBox ID="txbID" runat="server" CssClass="style44" Enabled="False" 
                    Visible="False"></asp:TextBox>
                <br />
                <br />
                <asp:CheckBox ID="chbSingers" runat="server" AutoPostBack="True" 
                    CausesValidation="True" 
                    
                    style="z-index: 1; left: 552px; top: 239px; position: absolute; width: 133px;" 
                    oncheckedchanged="chbSingers_CheckedChanged" />
                <br />
                <asp:Label ID="lblLastUpdatedBy" runat="server" 
                    style="z-index: 1; left: 1091px; top: 19px; position: absolute; height: 23px; width: 161px" 
                    Text="Last Updated By: " ForeColor="Black"></asp:Label>
            </div>
            </div>
			<asp:TextBox id="txtFirstName"
				runat="server" Enabled="False" ontextchanged="txtFirstName_TextChanged" 
                CssClass="style77" TabIndex="1" BackColor="#FFD200"></asp:TextBox>
			<asp:TextBox id="txtAddress1"
				runat="server" Enabled="False" ontextchanged="txtAddress1_TextChanged" 
                CssClass="style81" TabIndex="3" AutoCompleteType="LastName"></asp:TextBox>
			<asp:TextBox id="txtHomePhone"
				runat="server" Enabled="True" ontextchanged="txtHomePhone_TextChanged" 
                CssClass="style84" TabIndex="4"></asp:TextBox>
			<asp:TextBox id="txtStudentEmail"
				runat="server" Enabled="False" ontextchanged="txtStudentEmail_TextChanged" 
                CssClass="style90" TabIndex="6"></asp:TextBox>
			<asp:TextBox 
                id="txtCity" runat="server" Enabled="False" 
                ontextchanged="txtCity_TextChanged" CssClass="style105" TabIndex="14"></asp:TextBox>
			<asp:TextBox id="txtState"
				runat="server" Enabled="False" ontextchanged="txtState_TextChanged" 
                CssClass="style106" TabIndex="15"></asp:TextBox>
			<asp:TextBox 
                id="txtZip" runat="server"
				Width="72px" Enabled="False" ontextchanged="txtZip_TextChanged" CssClass="style107" 
                TabIndex="16"></asp:TextBox>
			<asp:Label id="lblStudentInfo"
				runat="server" CssClass="style75">STUDENT INFORMATION:</asp:Label>
			<asp:Label id="Label1" runat="server" CssClass="style95">Grade</asp:Label>
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
                id="Label11" runat="server" Font-Bold="True" Font-Size="15pt" 
                CssClass="style69">Student Information</asp:Label>
			<asp:Label 
                id="Label15" runat="server" CssClass="style49" Font-Size="Large">Athletics</asp:Label>
			<asp:Label 
                id="Label16" runat="server" CssClass="style73" Font-Size="Large">Performing Arts</asp:Label>
			<asp:Label 
                id="Label17" runat="server" CssClass="style74" Font-Size="Large">Education</asp:Label>
			<asp:Label id="Label18" runat="server" CssClass="style85">Home Phone</asp:Label>
			<asp:Label id="Label19" runat="server" CssClass="style88">School</asp:Label>
			<asp:Label id="Label20" runat="server" CssClass="style91">Student email</asp:Label>
			<asp:TextBox id="txtStudentCellPhone"
				runat="server" Width="182px" Enabled="False" CssClass="style113" TabIndex="17" 
            ontextchanged="txtStudentCellPhone_TextChanged"></asp:TextBox>
			<asp:Label id="Label21" runat="server" CssClass="style116">Student Cell Phone:</asp:Label>
			<asp:Label 
                id="Label22" runat="server" CssClass="style115" Font-Size="8pt">Text Phone?</asp:Label>
			<asp:TextBox id="txtChurch"
				runat="server" Enabled="False" CssClass="style118" TabIndex="19" 
            ontextchanged="txtChurch_TextChanged1" Visible="False"></asp:TextBox>
			<asp:Label 
                id="Label24" runat="server" CssClass="style119">Church</asp:Label>
			<asp:Label 
                id="Label25" runat="server" CssClass="style121">T-shirt size</asp:Label>
			<asp:TextBox id="txtCareerGoal"
				runat="server" Enabled="False" CssClass="style123" TabIndex="21" Visible="False"></asp:TextBox>
			<asp:Label id="Label26" runat="server" CssClass="style124">Career Goal</asp:Label>
			<asp:Button id="btnSubmitInformation"
				runat="server" Text="Save/Update Information" onclick="btnSubmitInformation_Click" 
                CssClass="style126" TabIndex="99"></asp:Button>
			<asp:CheckBox id="chbLastName"
				runat="server" AutoPostBack="True" Checked="True" 
                oncheckedchanged="chbLastName_CheckedChanged" CssClass="style79" 
                Text="(Edit)"></asp:CheckBox>
			<asp:CheckBox id="chbHomePhone"
				runat="server" oncheckedchanged="chbHomePhone_CheckedChanged" CssClass="style86"></asp:CheckBox>
			<asp:CheckBox id="chbSchool"
				runat="server" oncheckedchanged="chbSchool_CheckedChanged" CssClass="style89"></asp:CheckBox>
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
                onselectedindexchanged="ddlTShirtSize_SelectedIndexChanged" TabIndex="20">
            </asp:DropDownList>
            <asp:Label ID="lblhmma" runat="server" CssClass="style175" Font-Bold="True" 
                Font-Size="18pt" Text="/"></asp:Label>
            <asp:LinkButton ID="lbOptionsProgram" runat="server" Font-Size="8pt" 
                onclick="lbOptionsProgram_Click" 
                
                
            
            style="z-index: 1; left: 823px; top: 618px; position: absolute; width: 112px; bottom: 209px;" 
            Enabled="False">(Options Program)</asp:LinkButton>
            <asp:DropDownList ID="ddlSchool" runat="server" BackColor="#FFD200" 
                
                style="z-index: 1; left: 55px; top: 390px; position: absolute; right: 589px; width: 274px; height: 17px" 
                TabIndex="5">
            </asp:DropDownList>
            <asp:CheckBox ID="chbShakes" runat="server" AutoPostBack="True" 
                CausesValidation="True" 
                
                
            style="z-index: 1; left: 552px; top: 222px; position: absolute; right: 673px; width: 135px;" 
            oncheckedchanged="chbShakes_CheckedChanged" />
            <p>
                <asp:DropDownList ID="ddlAge" runat="server" BackColor="#FFD200" 
                    CssClass="style171" TabIndex="9">
                </asp:DropDownList>
                <asp:Label ID="lblhmm" runat="server" CssClass="style174" Font-Size="18pt" 
                    Text="/"></asp:Label>
                <asp:CheckBox ID="chbNewStudentFlag" runat="server" 
                    oncheckedchanged="chbNewStudentFlag_CheckedChanged" 
                    style="z-index: 1; left: 826px; top: 54px; position: absolute" 
                    Visible="False" />
                <asp:CheckBox ID="chbMSBasketLeague" runat="server" 
                    style="z-index: 1; left: 17px; top: 204px; position: absolute" 
                    TabIndex="99" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbMSBasketLeague_CheckedChanged" />
                <asp:CheckBox ID="chbBasketballTEAMS" runat="server" 
                    style="z-index: 1; left: 17px; top: 185px; position: absolute" 
                    AutoPostBack="True" CausesValidation="True" 
                    oncheckedchanged="chbBasketballTEAMS_CheckedChanged3" TabIndex="99" />
                <asp:CheckBox ID="chbSoccerInterMurals" runat="server" 
                    style="z-index: 1; left: 197px; top: 206px; position: absolute; width: 177px;" 
                    oncheckedchanged="chbSoccerInterMurals_CheckedChanged1" TabIndex="99" 
                    AutoPostBack="True" CausesValidation="True" />
                <asp:Button ID="cmbDeletePerformingArts" runat="server" 
                    onclick="cmbDeletePerformingArts_Click" 
                    style="z-index: 9999; left: 249px; top: 357px; position: absolute; height: 41px; width: 183px" 
                    Text="Remove From PAA" Visible="False" />
                <asp:CheckBox ID="chbPromotionalRelease" runat="server" 
                    oncheckedchanged="chbPromotionalRelease_CheckedChanged" 
                    style="z-index: 1; left: 1060px; top: 317px; position: absolute; width: 231px" 
                    Text="Promotional Release" />
                <asp:CheckBox ID="chbCAMPPickUp" runat="server" 
                    style="z-index: 1; left: 1163px; top: 139px; position: absolute; width: 164px; right: 58px;" 
                    Text="Camp PickUp" Visible="False" />
                <asp:LinkButton ID="lbBaseball" runat="server" ForeColor="Black" 
                    onclick="lbBaseball_Click" 
                    style="z-index: 1; left: 218px; top: 164px; position: absolute; width: 89px">Baseball</asp:LinkButton>
                <asp:LinkButton ID="lbSpecialEvents" runat="server" ForeColor="Black" 
                    onclick="lbSpecialEvents_Click" 
                    style="z-index: 1; left: 384px; top: 224px; position: absolute; width: 123px">Special Events</asp:LinkButton>
                <asp:LinkButton ID="lbBasketballTEAMS" runat="server" ForeColor="Black" 
                    onclick="lbBasketballTEAMS_Click" 
                    style="z-index: 1; left: 38px; top: 186px; position: absolute; width: 154px">Basketball TEAMS</asp:LinkButton>
                <asp:LinkButton ID="lbMSBasketballLeague" runat="server" ForeColor="Black" 
                    onclick="lbMSBasketballLeague_Click" 
                    style="z-index: 1; top: 205px; position: absolute; width: 151px; left: 38px">MS Basketball Lg</asp:LinkButton>
                <asp:LinkButton ID="lbHSBasketballLeague" runat="server" ForeColor="Black" 
                    onclick="lbHSBasketballLeague_Click" 
                    style="z-index: 1; left: 38px; top: 226px; position: absolute; width: 117px">HS Basketball Lg</asp:LinkButton>
                <asp:LinkButton ID="lbSoccerTEAMS" runat="server" ForeColor="Black" 
                    onclick="lbSoccerTEAMS_Click" 
                    style="z-index: 1; left: 218px; top: 226px; position: absolute; width: 143px">Soccer TEAMS</asp:LinkButton>
                <asp:LinkButton ID="lbSoccerIntraMurals" runat="server" ForeColor="Black" 
                    onclick="lbSoccerIntraMurals_Click" 
                    style="z-index: 1; left: 218px; top: 206px; position: absolute; width: 158px">Soccer IntraMurals</asp:LinkButton>
                <asp:LinkButton ID="lb3on3Basketball" runat="server" ForeColor="Black" 
                    onclick="lb3on3Basketball_Click" 
                    style="z-index: 1; left: 219px; top: 184px; position: absolute; width: 150px">3on3 Basketball</asp:LinkButton>
                <asp:LinkButton ID="lbPerformingArtsAcademy" runat="server" ForeColor="Black" 
                    onclick="lbPerformingArtsAcademy_Click" 
                    
                    style="z-index: 1; left: 574px; top: 203px; position: absolute; width: 210px" 
                    Visible="False">Performing Arts Acad.</asp:LinkButton>
                <asp:Label ID="lblLastScrubbed" runat="server" Font-Bold="True" ForeColor="Red" 
                    style="z-index: 1; left: 932px; top: 19px; position: absolute; height: 70px; width: 146px" 
                    Text="Last Scrubbed By: " Visible="False"></asp:Label>
                <asp:CheckBox ID="chbScrubbed" runat="server" AutoPostBack="True" 
                    CausesValidation="True" ForeColor="Red" 
                    oncheckedchanged="chbScrubbed_CheckedChanged" 
                    style="z-index: 1; left: 909px; top: 18px; position: absolute; width: 196px" />
                <asp:CheckBox ID="chbReadingSupport" runat="server" 
                    oncheckedchanged="chbReadingSupport_CheckedChanged" 
                    
                    
                    style="z-index: 1; left: 906px; top: 166px; position: absolute; width: 157px" 
                    AutoPostBack="True" CausesValidation="True" />
                <asp:LinkButton ID="lbAcademicReadingSupport" runat="server" ForeColor="Black" 
                    onclick="lbAcademicReadingSupport_Click" 
                    
                    
                    
                    style="z-index: 1; left: 928px; top: 166px; position: absolute; height: 2px; width: 182px;">Academic Reading Support</asp:LinkButton>
                <asp:DropDownList ID="ddlAdministerMedicine" runat="server" AutoPostBack="True" 
                    BackColor="#FFD200" CausesValidation="True" 
                    onselectedindexchanged="ddlAdministerMedicine_SelectedIndexChanged" 
                    
                    style="z-index: 1; left: 615px; top: 1267px; position: absolute; width: 151px">
                </asp:DropDownList>
            </p>
            <asp:CheckBox ID="chbImpactUrbanSchoolsAcademics" runat="server" 
            AutoPostBack="True" CausesValidation="True" 
            oncheckedchanged="chbImpactUrbanSchoolsAcademics_CheckedChanged" 
            
            
            
            
            style="z-index: 1; left: 905px; top: 186px; position: absolute; width: 285px" />
            <p>
                <asp:Button ID="cmbClearPage" runat="server" CssClass="style50" 
                    onclick="cmbClearPage_Click" Text="Clear Page" Visible="False" />
                <asp:LinkButton ID="lbClassesEnrollment" runat="server" CssClass="style51" 
                    onclick="lbClassesEnrollment_Click" Font-Size="X-Small" Enabled="False">(View Class Enrollment)</asp:LinkButton>
                <asp:CheckBox ID="chbSATPrepClass" runat="server" CssClass="style134" 
                    Text="SAT Prep Class" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbSATPrepClass_CheckedChanged" />
                <asp:DropDownList ID="ddlMonthBirth" runat="server" BackColor="#FFD200" 
                    CssClass="style166" 
                    onselectedindexchanged="ddlMonthBirth_SelectedIndexChanged" TabIndex="10">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlDayBirth" runat="server" BackColor="#FFD200" 
                    CssClass="style167" TabIndex="11">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlVoicePart" runat="server" BackColor="#FFD200" 
                    CssClass="style176" 
                    onselectedindexchanged="ddlVoicePart_SelectedIndexChanged" TabIndex="23">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlTextPhone" runat="server" BackColor="#FFD200" 
                    CssClass="style177" TabIndex="18">
                </asp:DropDownList>
                <asp:CheckBox ID="chbMondayNights" runat="server" 
                    oncheckedchanged="chbMondayNights_CheckedChanged" 
                    
                    style="z-index: 1; left: 364px; top: 204px; position: absolute; width: 185px" 
                    TabIndex="99" AutoPostBack="True" 
                    CausesValidation="True" />
                <asp:CheckBox ID="chbLittleLeagueBaseball" runat="server" 
                    
                    style="z-index: 1; left: 197px; top: 164px; position: absolute; width: 207px;" 
                    AutoPostBack="True" CausesValidation="True" 
                    oncheckedchanged="chbLittleLeagueBaseball_CheckedChanged" TabIndex="99" />
                <asp:CheckBox ID="chbSoccerLgTravel" runat="server" 
                    
                    style="z-index: 1; left: 197px; top: 226px; position: absolute; width: 164px;" 
                    TabIndex="99" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbSoccerLgTravel_CheckedChanged" />
                <asp:TextBox ID="txbDropOffPickUp" runat="server" BackColor="#FFD200" 
                    
                    
                    style="z-index: 1; left: 1056px; top: 208px; position: absolute; width: 180px;"></asp:TextBox>
                <asp:Label ID="lblDropOffPickUp" runat="server" Font-Size="10pt" 
                    style="z-index: 1; left: 1056px; top: 230px; position: absolute; width: 180px" 
                    Text="(PickUp/DropOff) Person"></asp:Label>
                <asp:CheckBox ID="chbCAMPDropOff" runat="server" 
                    style="z-index: 1; left: 1184px; top: 148px; position: absolute; width: 174px" 
                    Text="Camp DropOff" Visible="False" />
                <asp:LinkButton ID="lbBibleStudy" runat="server" ForeColor="Black" 
                    onclick="lbBibleStudy_Click" 
                    style="z-index: 1; left: 384px; top: 185px; position: absolute; width: 107px">Bible Study</asp:LinkButton>
                <asp:LinkButton ID="lbOutreachBasketball" runat="server" ForeColor="Black" 
                    onclick="lbOutreachBasketball_Click" 
                    style="z-index: 1; left: 37px; top: 165px; position: absolute; width: 159px">Outreach Basketball</asp:LinkButton>
                <asp:CheckBox ID="chbImpactUrbanSchools" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbImpactUrbanSchools_CheckedChanged" 
                    
                    style="z-index: 1; left: 364px; top: 165px; position: absolute; width: 238px" />
                <asp:LinkButton ID="lbMSHSChoir" runat="server" ForeColor="Black" 
                    onclick="lbMSHSChoir_Click1" 
                    
                    style="z-index: 1; left: 573px; top: 166px; position: absolute; width: 165px">MS/HS Choir</asp:LinkButton>
                <asp:CheckBox ID="chbImpactUrbanSchoolsPA" runat="server" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbImpactUrbanSchoolsPA_CheckedChanged" 
                    
                    
                    style="z-index: 1; left: 683px; top: 166px; position: absolute; width: 219px" />
            </p>
            <p>
                <asp:DropDownList ID="ddlYearBirth" runat="server" BackColor="#FFD200" 
                    CssClass="style168" TabIndex="12">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlGrade" runat="server" BackColor="#FFD200" 
                    CssClass="style170" TabIndex="8">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlGender" runat="server" BackColor="#FFD200" 
                    CssClass="style173" 
                    onselectedindexchanged="ddlGender_SelectedIndexChanged" TabIndex="13">
                </asp:DropDownList>

                <asp:CheckBox ID="chb3on3Basketball" runat="server" 
                    
                    style="z-index: 1; left: 197px; top: 184px; position: absolute; right: 581px; width: 171px;" 
                    TabIndex="99" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chb3on3Basketball_CheckedChanged" />
                <asp:CheckBox ID="chbHSBasketLeague" runat="server" 
                    
                    style="z-index: 1; left: 17px; top: 226px; position: absolute; height: 21px; width: 178px;" 
                    TabIndex="99" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbHSBasketLeague_CheckedChanged" />
                <asp:CheckBox ID="chbOliverFootballBible" runat="server" 
                    
                    style="z-index: 1; left: 364px; top: 185px; position: absolute; width: 185px;" 
                    TabIndex="99" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbOliverFootballBible_CheckedChanged" />

                <asp:CheckBox ID="chbMailingList" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbMailingList_CheckedChanged1" 
                    style="z-index: 1; left: 1043px; top: 422px; position: absolute; width: 179px" 
                    Text="Include in Mailing Lists" />
                <asp:DropDownList ID="ddlMailingListCodes" runat="server" BackColor="#FFD200" 
                    style="z-index: 1; left: 1046px; top: 446px; position: absolute; width: 156px">
                </asp:DropDownList>


                <asp:Button ID="cmbCancelPerformArts" runat="server" 
                    style="z-index: 9999; left: 250px; top: 436px; position: absolute; height: 41px; width: 183px" 
                    Text="Cancel PAA Removal" Visible="False" 
                    onclick="cmbCancelPerformArts_Click" />


                <asp:CheckBox ID="chbSummerDayCamp" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbSummerDayCamp_CheckedChanged" 
                    
                    style="z-index: 1; left: 906px; top: 207px; position: absolute; width: 221px" />
                <asp:CheckBox ID="chbPermissionTransport" runat="server" 
                    oncheckedchanged="chbPermissionTransport_CheckedChanged" 
                    style="z-index: 1; left: 1060px; top: 299px; position: absolute; width: 240px" 
                    Text="Permission to Transport" />


                <asp:CheckBox ID="chbSpecialEvents" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbSpecialEvents_CheckedChanged" 
                    
                    style="z-index: 1; left: 364px; top: 224px; position: absolute; width: 187px" />


                <asp:LinkButton ID="lbMondayNights" runat="server" ForeColor="Black" 
                    onclick="lbMondayNights_Click" 
                    style="z-index: 1; left: 384px; top: 204px; position: absolute; width: 131px">Monday Nights</asp:LinkButton>


                <asp:LinkButton ID="lbShakes" runat="server" ForeColor="Black" 
                    onclick="lbShakes_Click" 
                    
                    style="z-index: 1; left: 574px; top: 222px; position: absolute; width: 101px; bottom: 209px">Shakes</asp:LinkButton>

                <asp:LinkButton ID="lbImpactUrbanSchoolsPA" runat="server" ForeColor="Black" 
                    onclick="lbImpactUrbanSchools_Click" 
                    
                    
                    style="z-index: 1; left: 704px; top: 166px; position: absolute; width: 199px; right: 457px;">Impact Urban Schools</asp:LinkButton>

                <asp:LinkButton ID="lbImpactUrbanSchools" runat="server" ForeColor="Black" 
                    onclick="lbImpactUrbanSchools_Click" 
                    
                    style="z-index: 1; left: 384px; top: 165px; position: absolute; width: 196px">Impact Urban Schools</asp:LinkButton>

                <asp:LinkButton ID="lbImpactUrbanSchoolsAcademics" runat="server" ForeColor="Black" 
                    onclick="lbImpactUrbanSchools_Click" 
                    
                    
                    
                    
                    style="z-index: 1; left: 928px; top: 187px; position: absolute; width: 145px">Impact Urban Schools</asp:LinkButton>


            <asp:Panel ID="pnlProgramManagement" runat="server" BackColor="Orange" 
            style="z-index: 1; left: 20px; top: 254px; position: absolute; width: 633px; height: 465px;"
            Visible="false" BorderStyle="Solid" BorderWidth="5px" BorderColor="Black">
                <asp:Button ID="cmbProgramManagement" runat="server" Enabled="False" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm BasketballTEAMS" Visible="False" 
                    onclick="cmbProgramManagement_Click" />
                <asp:Button ID="cmbOutreachBasketball" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm Outreach Basketball" onclick="cmbOutreachBasketball_Click" 
                    Visible="False" />
                <asp:Button ID="cmbProgramManageCancel" runat="server" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" onclick="cmbProgramManageCancel_Click" 
                    Visible="False" />
                <asp:Button ID="cmbConfirmDelete" runat="server" Enabled="False" 
                    onclick="cmbConfirmDelete_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm Removal" Visible="False" />
                <asp:Button ID="cmbOutreachBasketballCancel" runat="server" 
                    onclick="cmbOutreachBasketballCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbConfirmDeleteOutreach" runat="server" 
                    onclick="cmbConfirmDeleteOutreach_Click" 
                    style="z-index: 1; left: 341px; top: 88px; position: absolute; width: 260px; height: 57px" 
                    Text="Confirm Outreach Delete" Visible="False" />
                <asp:RadioButtonList ID="rblOutreachBasketball" runat="server" 
                    AutoPostBack="True" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                    BackColor="#FFD200"
                    CausesValidation="True" 
                    onselectedindexchanged="rblOutreachBasketball_SelectedIndexChanged" 
                    style="z-index: 1; left: 34px; top: 107px; position: absolute; height: 91px; width: 112px" 
                    Visible="False">
                </asp:RadioButtonList>
                <asp:Button ID="cmbRemoveBaseball" runat="server" Enabled="False" 
                    onclick="cmbRemoveBaseball_Click" 
                    style="z-index: 1; left: 341px; top: 102px; position: absolute; height: 61px; width: 260px" 
                    Text="Remove From Baseball" Visible="False" />
                <asp:RadioButtonList ID="rblBaseball" runat="server" AutoPostBack="True" 
                    BackColor="#FFD200" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                    CausesValidation="True" 
                    onselectedindexchanged="rblBaseball_SelectedIndexChanged" 
                    style="z-index: 1; left: 31px; top: 105px; position: absolute; height: 129px; width: 111px">
                </asp:RadioButtonList>
                <asp:Button ID="cmbBaseball" runat="server" Enabled="False" 
                    onclick="cmbBaseball_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm Baseball" Visible="False" />
                <asp:Button ID="cmbBaseballCancel" runat="server" Enabled="False" 
                    onclick="cmbBaseballCancel_Click" 
                    style="z-index: 1; left: 341px; top: 189px; position: absolute; height: 58px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbSoccerIntraMuralsConfirm" runat="server" 
                    onclick="cmbSoccerIntraMuralsConfirm_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm SoccerIntraMurals" Visible="False" />
                <asp:Button ID="cmbSpecialEventsConfirm" runat="server" 
                    onclick="cmbSpecialEventsConfirm_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm SpecialEvents" Visible="False" />
                <asp:Button ID="cmbSoccerTEAMSConfirm" runat="server" 
                    onclick="cmbSoccerTEAMSConfirm_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm SoccerTEAMS" Visible="False" />
                <asp:Button ID="cmbBibleStudy" runat="server" onclick="cmbBibleStudy_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm Bible Study" Visible="False" />
                <asp:Button ID="cmbMSBasketballLeagueConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm MS Bball League" Visible="False" 
                    onclick="cmbMSBasketballLeagueConfirm_Click" />
                <asp:Button ID="cmbHSBasketballLeagueConfirm" runat="server" 
                    onclick="cmbHSBasketballLeagueConfirm_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm HS Bball League" Visible="False" />
                <asp:Button ID="cmb3on3BasketballConfirm" runat="server" 
                    onclick="cmb3on3BasketballConfirm_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm 3on3 Bball" Visible="False" />
                <asp:Button ID="cmbMondayNightsConfirm" runat="server" 
                    onclick="cmbMondayNightsConfirm_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm MondayNights" Visible="False" />
                <asp:Button ID="cmbSoccerIntraMuralsCancel" runat="server" 
                    onclick="cmbSoccerIntraMuralsCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbSpecialEventsCancel" runat="server" 
                    onclick="cmbSpecialEventsCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbSoccerTEAMSCancel" runat="server" 
                    onclick="cmbSoccerTEAMSCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbMSBasketballLeagueCancel" runat="server" 
                    onclick="cmbMSBasketballLeagueCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbHSBasketballLeagueCancel" runat="server" 
                    onclick="cmbHSBasketballLeagueCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmb3on3BasketballCancel" runat="server" 
                    onclick="cmb3on3BasketballCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbMondayNightsCancel" runat="server" 
                    onclick="cmbMondayNightsCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbBibleStudyCancel" runat="server" 
                    onclick="cmbBibleStudyCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbMondayNightsRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove ModayNights" Visible="False" 
                    onclick="cmbMondayNightsRemove_Click" />
                <asp:Button ID="cmbBibleStudyRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove Bible Study" Visible="False" 
                    onclick="cmbBibleStudyRemove_Click" />
                <asp:Button ID="cmbSoccerTEAMSRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove SoccerTEAMS" Visible="False" 
                    onclick="cmbSoccerTEAMSRemove_Click" />
                <asp:Button ID="cmbSoccerIntraMuralsRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove SoccerIntraMurals" Visible="False" 
                    onclick="cmbSoccerIntraMuralsRemove_Click" />
                <asp:Button ID="cmbSpecialEventsRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove SpecialEvents" Visible="False" 
                    onclick="cmbSpecialEventsRemove_Click" />
                <asp:Button ID="cmb3on3BasketballRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove 3on3 Bball" Visible="False" 
                    onclick="cmb3on3BasketballRemove_Click" />
                <asp:Button ID="cmbMSBasketballLeagueRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove MS Bball League" Visible="False" 
                    onclick="cmbMSBasketballLeagueRemove_Click" />
                <asp:Button ID="cmbHSBasketballLeagueRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove HS Bball League" Visible="False" 
                    onclick="cmbHSBasketballLeagueRemove_Click" />
                <asp:Button ID="cmbOutreachBasketballRemove" runat="server" 
                    onclick="cmbOutreachBasketballRemove_Click" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove Outreach Basketball" Visible="False" />

                <asp:Button ID="cmbMSHSChoirConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm MSHSChoir" Visible="False" 
                    onclick="cmbMSHSChoirConfirm_Click" />
                <asp:Button ID="cmbChildrensChoirConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm ChildrensChoir" Visible="False" 
                    onclick="cmbChildrensChoirConfirm_Click" />
                <asp:Button ID="cmbSingersConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm Singers" Visible="False" 
                    onclick="cmbSingersConfirm_Click" />
                <asp:Button ID="cmbShakesConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm Shakes" Visible="False" 
                    onclick="cmbShakesConfirm_Click" />
                <asp:Button ID="cmbPAAConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm PAA" Visible="False" 
                    onclick="cmbPAAConfirm_Click" />
                <asp:Button ID="cmbSummerDayCampConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm SummerDayCamp" Visible="False" 
                    onclick="cmbSummerDayCampConfirm_Click" />
                <asp:Button ID="cmbImpactUrbanSchoolsConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm ImpactUrbanSchools" Visible="False" 
                    onclick="cmbImpactUrbanSchoolsConfirm_Click" />
                <asp:Button ID="cmbAcademicReadingSupportConfirm" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Confirm AcademicReadingSupport" Visible="False" 
                    onclick="cmbAcademicReadingSupportConfirm_Click" />
                <asp:Button ID="cmbMSHSChoirRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove MSHSChoir" Visible="False" 
                    onclick="cmbMSHSChoirRemove_Click" />
                <asp:Button ID="cmbChildrensChoirRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove ChildrensChoir" Visible="False" 
                    onclick="cmbChildrensChoirRemove_Click" />
                <asp:Button ID="cmbSingersRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove Singers" Visible="False" 
                    onclick="cmbSingersRemove_Click" />
                <asp:Button ID="cmbShakesRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove Shakes" Visible="False" 
                    onclick="cmbShakesRemove_Click" />
                <asp:Button ID="cmbPAARemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove PerformingArtsAcademy" Visible="False" 
                    onclick="cmbPAARemove_Click" />
                <asp:Button ID="cmbSummerDayCampRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove SummerDayCamp" Visible="False" 
                    onclick="cmbSummerDayCampRemove_Click" />
                <asp:Button ID="cmbImpactUrbanSchoolsRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove ImpactUrbanSchools" Visible="False" 
                    onclick="cmbImpactUrbanSchoolsRemove_Click" />
                <asp:Button ID="cmbAcademicReadingSupportRemove" runat="server" 
                    style="z-index: 1; left: 341px; top: 85px; position: absolute; height: 58px; width: 260px" 
                    Text="Remove AcademicReadingSupport" Visible="False" 
                    onclick="cmbAcademicReadingSupportRemove_Click" />

                <asp:Button ID="cmbMSHSChoirCancel" runat="server" 
                    onclick="cmbMSHSChoirCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbChildrensChoirCancel" runat="server" 
                    onclick="cmbChildrensChoirCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbSingersCancel" runat="server" 
                    onclick="cmbSingersCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbShakesCancel" runat="server" 
                    onclick="cmbShakesCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbPAACancel" runat="server" 
                    onclick="cmbPAACancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbSummerDayCampCancel" runat="server" 
                    onclick="cmbSummerDayCampCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbImpactUrbanSchoolsCancel" runat="server" 
                    onclick="cmbImpactUrbanSchoolsCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />
                <asp:Button ID="cmbAcademicReadingSupportCancel" runat="server" 
                    onclick="cmbAcademicReadingSupportCancel_Click" 
                    style="z-index: 1; left: 341px; top: 196px; position: absolute; height: 55px; width: 260px" 
                    Text="Cancel" Visible="False" />





                <asp:CheckBoxList ID="cblProgramManagement" runat="server" AutoPostBack="True" 
                    BackColor="#FFD200" BorderColor="Black" BorderWidth="1px" 
                    CausesValidation="True" 
                    style="z-index: 1; left: 33px; top: 85px; position: absolute; height: 167px; width: 260px" 
                    Visible="False" 
                    onselectedindexchanged="cblProgramManagement_SelectedIndexChanged1">
                </asp:CheckBoxList>
            </asp:Panel>

            <p style="margin-bottom: 86px">
                <asp:CheckBox ID="chbOptionsTurnOn" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbOptionsTurnOn_CheckedChanged" 
                    style="z-index: 1; left: 803px; top: 593px; position: absolute; height: 25px; width: 187px" 
                    Text="(Options Program)" />
                <asp:LinkButton ID="lbSingers" runat="server" ForeColor="Black" 
                    onclick="lbSingers_Click" 
                    
                    style="z-index: 1; left: 574px; top: 239px; position: absolute; width: 128px">Singers</asp:LinkButton>
                <asp:LinkButton ID="lbSummerDayCamp" runat="server" ForeColor="Black" 
                    onclick="lbSummerDayCamp_Click" 
                    
                    
                    style="z-index: 1; left: 927px; top: 207px; position: absolute; width: 195px; right: 231px;">SummerDay Camp</asp:LinkButton>
        </p>
        &nbsp;</p>
                <asp:Button ID="btnNewPerson1" runat="server" CssClass="style125" 
                    onclick="btnNewPerson1_Click" Text="Enter New Person" />
			    <asp:CheckBox ID="chbFirstName" runat="server" CssClass="style128" />
			    <asp:Image ID="imgPicture" runat="server" CssClass="style163"  
                    ImageUrl="~/1.jpg" BorderColor="Black" BorderStyle="Double" 
                    BorderWidth="5px"/>
			    <asp:LinkButton ID="lbChildrensChoir" runat="server" ForeColor="Black" 
            onclick="lbChildrensChoir_Click" 
            
            style="z-index: 1; left: 573px; top: 185px; position: absolute; width: 165px">Children&#39;s Choir</asp:LinkButton>
			    <asp:Label ID="lblInformation" runat="server" CssClass="style164" 
                    Enabled="False"></asp:Label>


			<asp:Label ID="lblProgramManagement" runat="server" Font-Bold="True" 
            Font-Size="17pt" 
            style="z-index: 99999; left: 61px; top: 271px; position: absolute; height: 30px; width: 568px" 
            
            Text="To which section(s) of the Program, would you like to add the student.  Please choose." 
            Visible="False"></asp:Label>
			
            <asp:RadioButtonList ID="rblProgramManagement" runat="server" 
            BackColor="#FFD200" 
            style="z-index: 1; left: 118px; top: 341px; position: absolute; height: 145px; width: 199px" 
            Visible="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
            AutoPostBack="True" CausesValidation="True" 
            onselectedindexchanged="rblProgramManagement_SelectedIndexChanged">
            </asp:RadioButtonList>

			</p>
            <p>


                &nbsp;</p>
            <p class="style1">
                <asp:Label ID="Label27" runat="server" CssClass="style43" Font-Size="Large" 
                    Font-Underline="True" Text="Required Forms" Font-Bold="True" 
                    Font-Italic="True"></asp:Label>
                <asp:TextBox ID="txbHealthConditions" runat="server" CssClass="style19" 
                    ontextchanged="txbHealthConditions_TextChanged" TabIndex="22"></asp:TextBox>
                <asp:Label ID="lblHealthConditions" runat="server" CssClass="style20" 
                    Text="Health Conditions"></asp:Label>
                <asp:Label ID="Label28" runat="server" CssClass="style22" Text="Notes"></asp:Label>
                <asp:TextBox ID="txbDiscipleshipMentor" runat="server" CssClass="style24" 
                    ontextchanged="txbDiscipleshipMentor_TextChanged" TabIndex="22" 
                    Visible="False"></asp:TextBox>
                <asp:Label ID="Label29" runat="server" CssClass="style25" 
                    Text="Discipleship Mentor" Visible="False"></asp:Label>
                <asp:TextBox ID="txbSoloSong" runat="server" CssClass="style26" 
                    ontextchanged="txbSoloSong_TextChanged" TabIndex="7"></asp:TextBox>
                <asp:Label ID="Label30" runat="server" CssClass="style27" Text="Solo Song"></asp:Label>
                <asp:CheckBox ID="chbChildrensChoir" runat="server" CssClass="style28"  
                    oncheckedchanged="chbChildrensChoir_CheckedChanged" AutoPostBack="True" 
                    CausesValidation="True" />
                <asp:CheckBox ID="chbBibleOwnership" runat="server" CssClass="style39" 
                    Text="Bible Ownership" />
                <asp:CheckBox ID="chbBibleStudyParticipation" runat="server" CssClass="style40" 
                    Text="Bible Study Participation" />
                <asp:CheckBox ID="chbSoloist" runat="server" CssClass="style37" 
                    Text="Soloist" />
                <asp:CheckBox ID="chbStudentQuestionareForm" runat="server" CssClass="style35" 
                    oncheckedchanged="chbStudentQuestionareForm_CheckedChanged" 
                    Text="Student Choir Questionare Form" />
                <asp:CheckBox ID="chbHaveReceivedChrist" runat="server" CssClass="style41" 
                    Text="Have Received Christ" />
                <asp:CheckBox ID="chbParentalConsentForm" runat="server" CssClass="style33" 
                    oncheckedchanged="chbParentalConsentForm_CheckedChanged" 
                    Text="Academic/Parental Consent Form" />
                <asp:CheckBox ID="chbDance" runat="server" CssClass="style38" Text="Dance" />
                <asp:CheckBox ID="chbRetreatForm" runat="server" CssClass="style34" 
                    oncheckedchanged="chbRetreatForm_CheckedChanged" Text="Retreat Form" />
                <asp:CheckBox ID="chbMSHSChoir" runat="server" CssClass="style29" 
                    oncheckedchanged="chbMSHSChoir_CheckedChanged" 
                    AutoPostBack="True" CausesValidation="True" />

                <asp:CheckBox ID="chbPerformingArts" runat="server" CssClass="style30" 
                    oncheckedchanged="chbPerformingArts_CheckedChanged" 
                    AutoPostBack="True" CausesValidation="True" Text="Performing Arts Acad." />

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

                <asp:CheckBox ID="chbRegistrationForm" runat="server" CssClass="style32" 
                    oncheckedchanged="chbRegistrationForm_CheckedChanged" 
                    Text="Registration Form" AutoPostBack="True" CausesValidation="True" />
                <asp:CheckBox ID="chbMeetCCGF" runat="server" CssClass="style36" 
                    Text="Meet At CCGF" />
                <asp:Label ID="lblMisc" runat="server" CssClass="style11" Font-Italic="True" 
                    Font-Size="Large" Font-Underline="True" Text="Misc. Information" 
                    Font-Bold="True"></asp:Label>
                <asp:Label ID="lblID" runat="server" CssClass="style45" Text="ID" 
                    Enabled="False" Visible="False"></asp:Label>
                <asp:Label ID="lblVoicePart" runat="server" CssClass="style47" Text="VoicePart"></asp:Label>
                <asp:TextBox ID="txbNotes" runat="server" CssClass="style21" 
                    ontextchanged="txbNotes_TextChanged" TabIndex="24" TextMode="MultiLine" 
                    Wrap="False"></asp:TextBox>
                <asp:CheckBox ID="chbNotes" runat="server" CssClass="style129" Font-Size="8pt" 
                    Text="(Edit)" />
			<asp:TextBox id="txbParentGuardian2CellPhone"
				runat="server" CssClass="style57" TabIndex="34"></asp:TextBox>
                <asp:TextBox ID="txtParentGuardian1" runat="server" CssClass="style53" 
                    TabIndex="25"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian2" runat="server" CssClass="style55" 
                    TabIndex="31"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian1CellPhone" runat="server" CssClass="style59" 
                    TabIndex="28"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian2Email" runat="server" CssClass="style62" 
                    TabIndex="36"></asp:TextBox>
                <asp:TextBox ID="txbEmergencyRelationship" runat="server" CssClass="style64" 
                    TabIndex="37"></asp:TextBox>
                <asp:TextBox ID="txbEmergencyPhone" runat="server" CssClass="style65" 
                    TabIndex="39"></asp:TextBox>
                <asp:TextBox ID="txbAllergies" runat="server" CssClass="style66" 
                    Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txbInsuranceCompany" runat="server" CssClass="style68" 
                    Enabled="False"></asp:TextBox>
                <asp:Label ID="lblParentGuardian" runat="server" CssClass="style135" 
                    Text="PARENT/GUARDIAN INFORMATION:" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="txbParentGuardian1WrkPh" runat="server" CssClass="style136" 
                    TabIndex="27"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian1Email" runat="server" CssClass="style56" 
                    TabIndex="30"></asp:TextBox>
                <asp:TextBox ID="txbParentGuardian2WrkPh" runat="server" CssClass="style137" 
                    TabIndex="33"></asp:TextBox>
                <asp:Label ID="Label31" runat="server" CssClass="style138" 
                    Text="Parent/Legal Guardian #1"></asp:Label>
                <asp:Label ID="Label32" runat="server" CssClass="style139" Text="Relationship1"></asp:Label>
                <asp:Label ID="Label33" runat="server" CssClass="style140" Text="Work Phone1"></asp:Label>
                <asp:Label ID="Label34" runat="server" CssClass="style141" 
                    Text="Parent/Legal Guardian #2"></asp:Label>
                <asp:Label ID="Label35" runat="server" CssClass="style142" Text="Email1"></asp:Label>
                <asp:Label ID="Label36" runat="server" CssClass="style143" 
                    Text="Cell Phone: may we text phone?  Y/N"></asp:Label>
                <asp:Label ID="Label37" runat="server" CssClass="style144" 
                    Text="Cell Phone: may we text this phone? Y/N"></asp:Label>
                <asp:Label ID="Label38" runat="server" CssClass="style145" Text="Email2"></asp:Label>
                <asp:Label ID="Label39" runat="server" CssClass="style146" Text="Relationship2"></asp:Label>
                <asp:Label ID="Label40" runat="server" CssClass="style147" Text="Work Phone2"></asp:Label>
                <asp:Label ID="Label41" runat="server" CssClass="style148" Font-Bold="True" 
                    Text="EMERGENCY CONTACT INFORMATION:"></asp:Label>
                <asp:Label ID="Label42" runat="server" CssClass="style149" Text="Name"></asp:Label>
                <asp:Label ID="Label43" runat="server" CssClass="style150" 
                    Text="In the event of an emergency and you cannot be reached please give a name and phone number of an Authorized/Designated individual to make emergency decisions:"></asp:Label>
                <asp:Label ID="Label44" runat="server" CssClass="style151" Text="Relationship"></asp:Label>
                <asp:Label ID="Label45" runat="server" CssClass="style152" Text="Phone #"></asp:Label>
                <asp:Label ID="Label46" runat="server" CssClass="style153" 
                    Text="Please list any allergies or health concerns which may be relevant to a physician in the event of an emergency and indicate any activity restrictions (including previous injuries)."></asp:Label>
                <asp:Label ID="Label47" runat="server" CssClass="style154" 
                    Text="Medical Insurance Company:"></asp:Label>
                <asp:Label ID="Label48" runat="server" CssClass="style155" Text="Policy #:"></asp:Label>
                <asp:Label ID="Label49" runat="server" CssClass="style156" 
                    Text="Primary Care Physician:"></asp:Label>
                <asp:TextBox ID="TextBox12" runat="server" CssClass="style157" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="TextBox13" runat="server" CssClass="style158" Enabled="False"></asp:TextBox>
                <asp:TextBox ID="txbEmergRelationship" runat="server" CssClass="style63" 
                    TabIndex="38"></asp:TextBox>
                <asp:CheckBox ID="chbParentGuardianEdit" runat="server" Checked="True" 
                    CssClass="style161" Text="(Edit ParentGuardian)" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbParentGuardianEdit_CheckedChanged" Visible="False" />
                <asp:CheckBox ID="chbEmergencyContactEdit" runat="server" Checked="True" 
                    CssClass="style162" Text="(Edit EmergencyContact)" AutoPostBack="True" 
                    CausesValidation="True" 
                    oncheckedchanged="chbEmergencyContactEdit_CheckedChanged" 
                    Visible="False" />
                <asp:LinkButton ID="lbStudentPictures" runat="server" 
                    onclick="lbStudentPictures_Click" 
                    style="z-index: 1; left: 1055px; top: 591px; position: absolute">StudentPictureProfile</asp:LinkButton>
                <asp:DropDownList ID="ddlParentGuardian1Relationship" runat="server" 
                    BackColor="#FFD200" 
                    onselectedindexchanged="ddlParentGuardian1Relationship_SelectedIndexChanged" 
                    
                    style="z-index: 1; left: 316px; top: 626px; position: absolute; width: 123px; height: 24px; right: 373px;" 
                    TabIndex="26">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlParentGuardian2Relationship" runat="server" 
                    BackColor="#FFD200" 
                    onselectedindexchanged="ddlParentGuardian2Relationship_SelectedIndexChanged" 
                    
                    style="z-index: 1; left: 316px; top: 729px; position: absolute; width: 129px" 
                    TabIndex="32">
                </asp:DropDownList>
                <asp:LinkButton ID="lbDiscipleshipMentor" runat="server" Enabled="False" 
                    onclick="lbDiscipleshipMentor_Click" 
                    style="z-index: 1; left: 512px; top: 448px; position: absolute">(View DiscipleshipMentor)</asp:LinkButton>
                <asp:CheckBox ID="chbDiscipleshipMentor" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbDiscipleshipMentor_CheckedChanged" 
                    style="z-index: 1; left: 525px; top: 427px; position: absolute" 
                    Text="DiscipleShipMentor" />
                <asp:Label ID="lblMailingLists" runat="server" Font-Bold="True" 
                    Font-Italic="True" Font-Size="Large" 
                    style="z-index: 1; left: 1043px; top: 392px; position: absolute; height: 29px; width: 161px; text-decoration: underline" 
                    Text="Mailing Lists"></asp:Label>
                <asp:TextBox ID="txbMiddleName" runat="server" BackColor="#FFD200" 
                    
                    
                    
                    style="z-index: 1; left: 151px; top: 270px; position: absolute; width: 57px; right: 646px;" 
                    ontextchanged="txbMiddleName_TextChanged"></asp:TextBox>
                <asp:Label ID="lblMiddleName" runat="server" 
                    style="z-index: 1; left: 154px; top: 293px; position: absolute; width: 115px" 
                    Text="Middle"></asp:Label>
                <asp:DropDownList ID="ddlCareerGoal" runat="server" BackColor="#FFD200" 
                    style="z-index: 1; left: 360px; top: 432px; position: absolute; width: 152px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlChurch" runat="server" BackColor="#FFD200" 
                    
                    style="z-index: 1; left: 359px; top: 388px; position: absolute; width: 214px">
                </asp:DropDownList>
                <asp:CheckBox ID="chbIncludePromotionalMailing" runat="server" 
                    AutoPostBack="True" CausesValidation="True" 
                    style="z-index: 1; left: 1043px; top: 469px; position: absolute; width: 241px" 
                    Text="Requested Promotional Mail" 
                    oncheckedchanged="chbIncludePromotionalMailing_CheckedChanged" />
                <asp:DropDownList ID="ddlTextGuard1" runat="server" BackColor="#FFD200" 
                    
                    style="z-index: 1; left: 177px; top: 670px; position: absolute; width: 99px" 
                    TabIndex="29">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlTextGuard2" runat="server" BackColor="#FFD200" 
                    
                    style="z-index: 1; left: 176px; top: 776px; position: absolute; width: 99px" 
                    TabIndex="35">
                </asp:DropDownList>
                <asp:CheckBox ID="chbStudentVolunteer" runat="server" 
                    style="z-index: 1; left: 1042px; top: 524px; position: absolute; width: 167px" 
                    Text="Student/Volunteer" />
                <asp:Label ID="lblErrorMessage" runat="server" BackColor="White" 
                    BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" Font-Size="16pt" 
                    ForeColor="Red" 
                    style="z-index: 1; left: 672px; top: 281px; position: absolute; height: 181px; width: 366px" 
                    Text="Correction Required: " Visible="False"></asp:Label>
                <asp:CheckBoxList ID="cblMedications" runat="server" BackColor="#FFD200" 
                    BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" Enabled="False" 
                    RepeatColumns="4" 
                    style="z-index: 1; left: 50px; top: 1323px; position: absolute; height: 77px; width: 722px">
                    <asp:ListItem>Aspirin</asp:ListItem>
                    <asp:ListItem>Tylenol</asp:ListItem>
                    <asp:ListItem>Ibuprofen</asp:ListItem>
                    <asp:ListItem>Advil</asp:ListItem>
                    <asp:ListItem>Antacids</asp:ListItem>
                    <asp:ListItem>Benadryl</asp:ListItem>
                    <asp:ListItem>Antiseptic Ointment</asp:ListItem>
                    <asp:ListItem>Anesthetic Ointment</asp:ListItem>
                    <asp:ListItem>Iodine Prep Pad</asp:ListItem>
                    <asp:ListItem>Acetaminophen</asp:ListItem>
                    <asp:ListItem>Rubbing Alcohol</asp:ListItem>
                    <asp:ListItem>Other _______________________</asp:ListItem>
                </asp:CheckBoxList>
                <asp:Label ID="lblAuthorizationMedication" runat="server" Font-Bold="True" 
                    Font-Size="15pt" 
                    style="z-index: 1; left: 51px; top: 1157px; position: absolute; width: 410px" 
                    Text="Authorization for Administering Medication:"></asp:Label>
                <asp:Label ID="lblMedicationInformation" runat="server" 
                    style="z-index: 1; left: 51px; top: 1299px; position: absolute; height: 28px; width: 785px" 
                    Text="If Yes, please check the following over-the-counter medication that Urban Impact is allowed to administer: "></asp:Label>
                <asp:Label ID="lblMedicalInformation2" runat="server" 
                    style="z-index: 1; left: 51px; top: 1182px; position: absolute; width: 840px" 
                    Text="In order for Urban Impact staff to administer over-the-counter medication, please complete the following section: "></asp:Label>
                <asp:Label ID="lblAdministerMedicine" runat="server" 
                    style="z-index: 1; left: 401px; top: 1268px; position: absolute; width: 242px" 
                    Text="Ok to administer medicine?"></asp:Label>
                <asp:Label ID="lblAnotherMedication3" runat="server" 
                    style="z-index: 1; left: 52px; top: 1216px; position: absolute; height: 61px; width: 723px" 
                    Text="I, _______________________________________, give my consent to Urban Impact staff and volunteers to administer the following over-the-counter medication to the above named child in the prescribed dosage and time increments indicated by the medication's package label: "></asp:Label>
                <asp:CheckBox ID="chbMedication" runat="server" AutoPostBack="True" 
                    CausesValidation="True" oncheckedchanged="chbMedication_CheckedChanged" 
                    style="z-index: 1; left: 837px; top: 1203px; position: absolute; width: 333px" 
                    Text="Check if Yes  (If Yes, Check medication below)" Enabled="False" 
                    Visible="False" />
                <asp:CheckBox ID="chbMedicationNo" runat="server" 
                    style="z-index: 1; left: 835px; top: 1202px; position: absolute; width: 305px" 
                    Text="No           (Check medication below)" Visible="False" />
                <asp:TextBox ID="txbMedicationsOtherNotes" runat="server" BackColor="Orange" 
                    Enabled="False" 
                    
                    
                    style="z-index: 1; left: 511px; top: 1374px; position: absolute; width: 210px"></asp:TextBox>
            </p>
        </form>
	</body>
</HTML>
