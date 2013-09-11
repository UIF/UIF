<%@ Page language="c#" Codebehind="RegistrationFormAdmin.aspx.cs" AutoEventWireup="True" Inherits="UIF.PerformingArts.RegistrationFormAdmin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RegistrationFormAdmin</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bgColor="#ffa500" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox style="Z-INDEX: 101; POSITION: absolute; TOP: 440px; LEFT: 120px" id="txtLastName"
				runat="server"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 102; POSITION: absolute; TOP: 440px; LEFT: 280px" id="txtFirstName"
				runat="server"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 103; POSITION: absolute; TOP: 496px; LEFT: 120px" id="txtAddress1"
				runat="server"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 104; POSITION: absolute; TOP: 552px; LEFT: 120px" id="txtHomePhone"
				runat="server"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 105; POSITION: absolute; TOP: 616px; LEFT: 120px" id="txtSchool"
				runat="server"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 106; POSITION: absolute; TOP: 680px; LEFT: 120px" id="txtStudentEmail"
				runat="server"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 107; POSITION: absolute; TOP: 440px; LEFT: 448px" id="txtGrade"
				runat="server" Width="40px"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 108; POSITION: absolute; TOP: 440px; LEFT: 496px" id="txtAge" runat="server"
				Width="40px"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 109; POSITION: absolute; TOP: 496px; LEFT: 448px" id="txtCity" runat="server"
				Width="120px"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 110; POSITION: absolute; TOP: 496px; LEFT: 576px" id="txtState"
				runat="server" Width="64px"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 111; POSITION: absolute; TOP: 496px; LEFT: 648px" id="txtZip" runat="server"
				Width="72px"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 112; POSITION: absolute; TOP: 440px; LEFT: 664px" id="txtGender"
				runat="server" Width="52px"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 113; POSITION: absolute; TOP: 440px; LEFT: 544px" id="txtDateBirth"
				runat="server" Width="112px"></asp:TextBox>
			<asp:Label style="Z-INDEX: 114; POSITION: absolute; TOP: 408px; LEFT: 96px" id="lblStudentInfo"
				runat="server">STUDENT INFORMATION:</asp:Label>
			<asp:Label style="Z-INDEX: 115; POSITION: absolute; TOP: 464px; LEFT: 448px" id="Label1" runat="server">Grade</asp:Label>
			<asp:Label style="Z-INDEX: 116; POSITION: absolute; TOP: 464px; LEFT: 496px" id="Label2" runat="server">Age</asp:Label>
			<asp:Label style="Z-INDEX: 117; POSITION: absolute; TOP: 464px; LEFT: 544px" id="Label3" runat="server">Date of Birth</asp:Label>
			<asp:Label style="Z-INDEX: 118; POSITION: absolute; TOP: 464px; LEFT: 664px" id="Label4" runat="server">Gender</asp:Label>
			<asp:Label style="Z-INDEX: 119; POSITION: absolute; TOP: 520px; LEFT: 448px" id="Label5" runat="server">City</asp:Label>
			<asp:Label style="Z-INDEX: 120; POSITION: absolute; TOP: 520px; LEFT: 576px" id="Label6" runat="server">State</asp:Label>
			<asp:Label style="Z-INDEX: 121; POSITION: absolute; TOP: 520px; LEFT: 648px" id="Label7" runat="server">Zip</asp:Label>
			<asp:Label style="Z-INDEX: 122; POSITION: absolute; TOP: 464px; LEFT: 120px" id="Label8" runat="server">LastName</asp:Label>
			<asp:Label style="Z-INDEX: 123; POSITION: absolute; TOP: 520px; LEFT: 120px" id="Label9" runat="server">Address</asp:Label>
			<asp:Label style="Z-INDEX: 124; POSITION: absolute; TOP: 464px; LEFT: 280px" id="Label10" runat="server">FirstName</asp:Label>
			<asp:Label style="Z-INDEX: 125; POSITION: absolute; TOP: 56px; LEFT: 224px" id="Label11" runat="server"
				Width="392px" Font-Bold="True" Font-Size="Large">REGISTRATION FORM 2010/2011</asp:Label>
			<asp:Label style="Z-INDEX: 126; POSITION: absolute; TOP: 96px; LEFT: 256px" id="Label12" runat="server"
				Width="288px">Parental Permission & Medical Release Form</asp:Label>
			<asp:Label style="Z-INDEX: 127; POSITION: absolute; TOP: 128px; LEFT: 120px" id="Label13" runat="server"
				Width="616px" Height="72px">This form must be filled out and signed by a parent/guardian to participate in Urban Impact Programs.  Fax, mail, or drop off form to 801 Union Ave, 4th floor, Pittsburgh PA 15212   Phone: 412-321-3811   Fax: 412-321-2369</asp:Label>
			<asp:Label style="Z-INDEX: 128; POSITION: absolute; TOP: 216px; LEFT: 120px" id="Label14" runat="server"
				Width="616px">PROGRAMS:  Please mark the program(s) for which your student is registering (checks payable to Urban Impact):</asp:Label>
			<asp:Label style="Z-INDEX: 129; POSITION: absolute; TOP: 280px; LEFT: 128px" id="Label15" runat="server">Athletics</asp:Label>
			<asp:Label style="Z-INDEX: 130; POSITION: absolute; TOP: 280px; LEFT: 376px" id="Label16" runat="server">Performing Arts</asp:Label>
			<asp:Label style="Z-INDEX: 131; POSITION: absolute; TOP: 280px; LEFT: 648px" id="Label17" runat="server">Academics</asp:Label>
			<asp:Label style="Z-INDEX: 132; POSITION: absolute; TOP: 576px; LEFT: 120px" id="Label18" runat="server">Home Phone</asp:Label>
			<asp:Label style="Z-INDEX: 133; POSITION: absolute; TOP: 640px; LEFT: 120px" id="Label19" runat="server">School</asp:Label>
			<asp:TextBox style="Z-INDEX: 134; POSITION: absolute; TOP: 1216px; LEFT: 120px" id="TextBox1"
				runat="server"></asp:TextBox>
			<asp:Label style="Z-INDEX: 135; POSITION: absolute; TOP: 704px; LEFT: 120px" id="Label20" runat="server">Student email</asp:Label>
			<asp:TextBox style="Z-INDEX: 136; POSITION: absolute; TOP: 552px; LEFT: 448px" id="txtStudentCellPhone"
				runat="server" Width="182px"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 137; POSITION: absolute; TOP: 552px; LEFT: 656px" id="txtSendText"
				runat="server" Width="62px"></asp:TextBox>
			<asp:Label style="Z-INDEX: 138; POSITION: absolute; TOP: 576px; LEFT: 448px" id="Label21" runat="server">Student Cell Phone:</asp:Label>
			<asp:Label style="Z-INDEX: 139; POSITION: absolute; TOP: 576px; LEFT: 656px" id="Label22" runat="server">Yes/No</asp:Label>
			<asp:Label style="Z-INDEX: 140; POSITION: absolute; TOP: 600px; LEFT: 584px" id="Label23" runat="server"
				Width="157px">May we text this phone?</asp:Label>
			<asp:TextBox style="Z-INDEX: 141; POSITION: absolute; TOP: 632px; LEFT: 448px" id="txtChurch"
				runat="server"></asp:TextBox>
			<asp:TextBox style="Z-INDEX: 142; POSITION: absolute; TOP: 632px; LEFT: 640px" id="txtTShirtSize"
				runat="server" Width="78px"></asp:TextBox>
			<asp:Label style="Z-INDEX: 143; POSITION: absolute; TOP: 656px; LEFT: 448px" id="Label24" runat="server">Church</asp:Label>
			<asp:Label style="Z-INDEX: 144; POSITION: absolute; TOP: 656px; LEFT: 640px" id="Label25" runat="server">T-shirt size</asp:Label>
			<asp:TextBox style="Z-INDEX: 145; POSITION: absolute; TOP: 704px; LEFT: 448px" id="txtCareerGoal"
				runat="server"></asp:TextBox>
			<asp:Label style="Z-INDEX: 146; POSITION: absolute; TOP: 728px; LEFT: 448px" id="Label26" runat="server">Career Goal</asp:Label>
			<asp:Button style="Z-INDEX: 147; POSITION: absolute; TOP: 816px; LEFT: 312px" id="btnSubmitInformation"
				runat="server" Text="Submit Information"></asp:Button></form>
	</body>
</HTML>
