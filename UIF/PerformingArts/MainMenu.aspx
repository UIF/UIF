<%@ Page language="c#" Codebehind="MainMenu.aspx.cs" AutoEventWireup="True" Inherits="UIF.PerformingArts.MainMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MainMenu</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	    
	    <style type="text/css">
            .style1
            {
                width: 279px;
                height: 118px;
                position: absolute;
                top: 317px;
                left: 421px;
                z-index: 1;
                margin-left: 62px;
            }
            .style2
            {
                position: absolute;
                top: 52px;
                left: 7px;
                z-index: 1;
                width: 242px;
                height: 176px;
            }
            .style7
            {
                position: absolute;
                top: 243px;
                left: 32px;
                z-index: 1;
                width: 387px;
                height: 259px;
            }
            .style8
            {
                position: absolute;
                top: 178px;
                left: 819px;
                z-index: 1;
                width: 417px;
                height: 302px;
            }
            .style9
            {
                position: absolute;
                top: 520px;
                left: 21px;
                z-index: 1;
                width: 1204px;
                height: 109px;
            }


            .label1024
            {
            font-size: 11pt;
            color: red;
            font-family: Arial;
            }

            .textBox1024
            {
            width: 120;
            height: 40; 
            }

            .button1024
            {
            width: 120;
            height: 40;
            }
        </style>
	</HEAD>
	            
    <body MS_POSITIONING="GridLayout" bgColor="#ffa500">
		<form id="Form1" method="post" runat="server" defaultfocus="lgnUIFLogin">

            <script language="javascript" type="text/C#">
                    UrbanImpactCommon.HTML BuildMenu = new UrbanImpactCommon.HTML();
                    Menu TheMenu = BuildMenu.BuildMenuControl();
            </script>

            <script language="javascript" type="text/C#">
                if ((screen.width == 1280)) 
                {
                    document.getElementById("ClientResolution").setAttribute("value", "1280Res");
                }

                //if ((screen.width == 1280) && (screen.height == 720))
                //{
                //    document.getElementById("ClientResolution").setAttribute("value", "1280Res");
                //}

                var thisform = document.Form1;

                if (clientRes != "")
                // if the client resolution is captured,
                // then postback to server for changing.
                {
                    thisform.submit();
                } 
            </script>

			<asp:Label style="Z-INDEX: 101; POSITION: absolute; TOP: 128px; LEFT: 248px" id="lblMenu" runat="server"
				Width="832px" Font-Size="X-Large">Welcome to the Urban Impact Foundation Administrative Site.</asp:Label>
			<asp:Label style="Z-INDEX: 102; POSITION: absolute; TOP: 40px; LEFT: 376px" id="lbMenu" runat="server"
				Width="568px" Font-Size="XX-Large" Font-Bold="True">Urban Impact Foundation</asp:Label>
		    <asp:Login ID="lgnUIFLogin" runat="server" BorderStyle="Double" 
                CssClass="style1" DestinationPageUrl="~/MainMenu.aspx" 
                LoginButtonText="Enter" onauthenticate="lgnUIFLogin_Authenticate" 
                TitleText="UIF Member Login" BorderColor="Black" BorderWidth="6px" 
                BackColor="#FFD200" DisplayRememberMe="False" TabIndex="1">
                <TextBoxStyle BackColor="Silver" Width="150px" />
                <TitleTextStyle HorizontalAlign="Center" />
            </asp:Login>
		    <asp:Image ID="imgUIF" runat="server" CssClass="style2"  ImageUrl="~/Picture1.png"/>
		    <asp:Image ID="imgAthletics" runat="server" CssClass="style7" ImageUrl="~/IMG_9138.JPG" />
            <asp:Image ID="imgPerformingArts" runat="server" CssClass="style8"  ImageUrl="~/2010-05-07 ArtsNight 001.JPG"/>
            <asp:Image ID="imgUIF2" runat="server" CssClass="style9"  ImageUrl="~/Picture3.png"/>
		</form>
	</body>
</HTML>
