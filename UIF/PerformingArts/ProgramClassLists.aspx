<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="ProgramClassLists.aspx.cs" Inherits="UIF.PerformingArts.ProgramClassLists" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ProgramClassLists</title>
    <style type="text/css">
        .style1
        {
            position: absolute;
            top: 55px;
            left: 389px;
            z-index: 1;
            width: 431px;
        }
        .style2
        {
            text-align: center;
        }
        .style3
        {
            position: absolute;
            top: 153px;
            left: 164px;
            z-index: 1;
            width: 144px;
        }
        .style4
        {
            position: absolute;
            top: 158px;
            left: 91px;
            z-index: 1;
            width: 403px;
            height: 30px;
            right: 410px;
        }
        .style5
        {
            position: absolute;
            top: 157px;
            left: 162px;
            z-index: 1;
            width: 228px;
            height: 25px;
        }
        .style6
        {
            width: 1199px;
            height: 166px;
            position: absolute;
            top: 269px;
            left: 37px;
            z-index: 1;
        }
        .style10
        {
            width: 640px;
            height: 70px;
            position: absolute;
            top: 380px;
            left: 576px;
            z-index: 1;
        }
        .style11
        {
            position: absolute;
            top: 683px;
            left: 36px;
            z-index: 1;
            height: 126px;
            width: 1202px;
        }
        </style>

<style type="text/css"> 
   div { z-index: 9999; } 
</style>

</head>
<body bgcolor="Orange" >
    <form id="form1" runat="server"  defaultfocus="txbClassName">
    <div class="style2">
    <div>
    
    </div>
        <asp:Label ID="lblProgramClassLists" runat="server" CssClass="style1" 
            Font-Bold="True" Font-Size="36pt" Text="Program/Class Lists"></asp:Label>
    </div>

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
        style="z-index: 1; left: 213px; top: 97px; position: absolute; height: 37px; width: 947px" 
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


    <p class="style2">
        &nbsp;</p>
    <asp:Button ID="cmbProgram" runat="server" CssClass="style3" 
        onclick="cmbProgram_Click" Text="View Program" Visible="False" />
    <asp:Label ID="lblProgram" runat="server" CssClass="style4" 
        Text="Please select a program to administer." Font-Bold="True" 
        Font-Size="18pt" TabIndex="23" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlProgram" runat="server" CssClass="style5" 
        BackColor="#FFD200" 
        onselectedindexchanged="ddlProgram_SelectedIndexChanged" TabIndex="24" 
        Visible="False">
    </asp:DropDownList>

    <asp:Image ID="imgUIF" runat="server" CssClass="style11"  ImageUrl="~/Picture3.png"/>

    <asp:GridView ID="gvStudentList" runat="server" AllowSorting="True" 
        BorderStyle="Solid" BorderWidth="4px" CssClass="style6" 
        OnSelectedIndexChanged="gvStudentList_SelectedIndexChanged"
        OnSelectedIndexChanging="gvStudentList_SelectedIndexChanging"
        OnRowDataBound="gvStudentList_RowDataBound"
        ShowHeaderWhenEmpty="True" AutoGenerateEditButton="True" OnRowEditing="gvStudentList_RowEditing" 
        OnRowUpdating="gvStudentList_RowUpdating"
        OnRowCancelingEdit="gvStudentList_RowCancelingEdit"
        OnRowDeleting="gvStudentList_RowDeleting"  DataKeyNames="Name"
        BorderColor="Black" 
        AutoGenerateDeleteButton="True" AutoGenerateColumns="False" 
        EnablePersistedSelection="True" 
        BackColor="#FFD200">
        <AlternatingRowStyle BackColor="Silver" />
        <RowStyle Wrap="False" />
        <EditRowStyle Wrap="False" />
        <EmptyDataRowStyle BorderStyle="Solid" />
        <HeaderStyle Font-Bold="True" Font-Overline="False" Font-Size="18pt" 
            Font-Underline="False" BackColor="Black" ForeColor="White" />

        <Columns >  
            <asp:TemplateField HeaderText ="Name">  
            <ItemTemplate><%#Eval("name") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox1" runat="server" Text='<%#Eval("name") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Time">  
            <ItemTemplate><%#Eval("time") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox2" runat="server" Text='<%#Eval("time") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Day">  
            <ItemTemplate><%#Eval("day") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox3" runat="server" Text='<%#Eval("day") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Location">  
            <ItemTemplate><%#Eval("location") %></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox4" runat="server" Text='<%#Eval("location") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="SizeLimit">  
            <ItemTemplate><%#Eval("sizelimit") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox20" runat="server" Text='<%#Eval("sizelimit") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Comments">  
            <ItemTemplate><%#Eval("comments") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox5" runat="server" Text='<%#Eval("comments") %>'></asp:TextBox>
            </EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="Instructor">  
            <ItemTemplate> <%#Eval("instructor")%></ItemTemplate>  
            <EditItemTemplate><asp:TextBox ID="textbox6" runat="server" Text='<%#Eval("instructor") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>
                  
            <asp:TemplateField HeaderText="DevotionalLeader" >  
            <ItemTemplate > <%#Eval("devotionalleader") %></ItemTemplate>   
            <EditItemTemplate><asp:TextBox ID="textbox7" runat="server" Text='<%#Eval("devotionalleader") %>'></asp:TextBox></EditItemTemplate>
            </asp:TemplateField>  

            <asp:TemplateField HeaderText ="ID" InsertVisible="False" ShowHeader="False">  
            <ItemTemplate><%#Eval("id") %></ItemTemplate>
            <EditItemTemplate><asp:TextBox ID="textbox8" runat="server"  Visible="false" ReadOnly="True" Text='<%#Eval("id") %>'></asp:TextBox> 
            </EditItemTemplate>
            </asp:TemplateField>  
        </Columns>  
    </asp:GridView>

    <asp:Menu ID="menMenu" runat="server" BorderColor="Black" BorderStyle="Solid" 
        BorderWidth="3px" CssClass="style10">
    </asp:Menu>
    <asp:Button ID="cmbExcelExport" runat="server" onclick="cmbExcelExport_Click" 
        style="z-index: 1; left: 1124px; top: 154px; position: absolute; height: 26px; width: 112px" 
        Text="Export to Excel" />
    <asp:Button ID="cmbAddRecord" runat="server" onclick="cmbAddRecord_Click" 
        style="z-index: 1; left: 18px; top: 214px; position: absolute; height: 43px; width: 135px" 
        TabIndex="10" Text="Commit New Class" Visible="False" />
    <asp:TextBox ID="txbClassName" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 159px; top: 221px; position: absolute; width: 164px; right: 609px" 
        Visible="False" TabIndex="2"></asp:TextBox>
    <asp:TextBox ID="txbInstructor" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 863px; top: 222px; position: absolute; width: 127px" 
        TabIndex="8" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txbDevotionalLeader" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 1011px; top: 222px; position: absolute; width: 202px" 
        TabIndex="9" Visible="False"></asp:TextBox>
    <p>
        <asp:TextBox ID="txbSizeLimit" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 608px; top: 222px; position: absolute; width: 92px" 
            TabIndex="6" Visible="False"></asp:TextBox>
    </p>
    <asp:TextBox ID="txbComments" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 715px; top: 222px; position: absolute; width: 129px" 
        TabIndex="7" Visible="False"></asp:TextBox>
    <asp:DropDownList ID="ddlTime" runat="server" BackColor="#FFD200" Height="22px" 
        style="z-index: 1; left: 333px; top: 222px; position: absolute; width: 114px" 
        TabIndex="3" Visible="False">
    </asp:DropDownList>
    <asp:LinkButton ID="lbAddNewEntry" runat="server" onclick="lbAddNewEntry_Click" 
        style="z-index: 1; left: 38px; top: 239px; position: absolute; width: 158px">(Add a New Class)</asp:LinkButton>
    <asp:TextBox ID="txbDay" runat="server" BackColor="#FFD200" 
        style="z-index: 1; left: 446px; top: 222px; position: absolute; width: 55px" 
        TabIndex="4" Visible="False"></asp:TextBox>
    <p>
        <asp:TextBox ID="txbClassLocation" runat="server" BackColor="#FFD200" 
            style="z-index: 1; left: 510px; top: 222px; position: absolute; width: 87px" 
            TabIndex="5" Visible="False"></asp:TextBox>
    </p>
    <asp:Label ID="lblClassName" runat="server" 
        style="z-index: 1; left: 161px; top: 244px; position: absolute; width: 142px" 
        Text="ClassName" Visible="False"></asp:Label>
    <asp:Label ID="lblTime" runat="server" 
        style="z-index: 1; left: 333px; top: 246px; position: absolute; width: 78px" 
        Text="Time" Visible="False"></asp:Label>
    <asp:Label ID="lblDay" runat="server" 
        style="z-index: 1; left: 448px; top: 245px; position: absolute; width: 79px" 
        Text="Day" Visible="False"></asp:Label>
    <asp:Label ID="lblClassLocation" runat="server" 
        style="z-index: 1; left: 512px; top: 245px; position: absolute; width: 82px; right: 35px" 
        Text="Location" Visible="False"></asp:Label>
    <asp:Label ID="lblSizeLimit" runat="server" 
        style="z-index: 1; left: 610px; top: 245px; position: absolute" 
        Text="SizeLimit" Visible="False"></asp:Label>
    <asp:Label ID="lblComments" runat="server" 
        style="z-index: 1; left: 717px; top: 244px; position: absolute; width: 113px" 
        Text="Comments" Visible="False"></asp:Label>
    <asp:Label ID="lblClassInstructor" runat="server" 
        style="z-index: 1; left: 864px; top: 244px; position: absolute; width: 108px" 
        Text="ClassInstructor" Visible="False"></asp:Label>
    <asp:Label ID="lblDevotionalLeader" runat="server" 
        style="z-index: 1; left: 1014px; top: 243px; position: absolute; width: 202px" 
        Text="DevotionalLeader" Visible="False"></asp:Label>
    <asp:Button ID="cmbReset" runat="server" onclick="cmbReset_Click" 
        style="z-index: 1; left: 38px; top: 153px; position: absolute; height: 28px; width: 93px" 
        TabIndex="22" Text="Reset Page" />
    <asp:Label ID="lblMainpage" runat="server" Font-Size="22pt" 
        style="z-index: 1; left: 483px; top: 151px; position: absolute; height: 36px; width: 369px; text-decoration: underline" 
        Text="Maintain PAA Class Lists"></asp:Label>
    </form>
</body>
</html>
