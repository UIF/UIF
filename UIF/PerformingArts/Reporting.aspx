<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporting.aspx.cs" Inherits="UIF.PerformingArts.Reporting" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body bgcolor="Orange">
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
        style="z-index: 1; left: 14px; top: 151px; position: absolute; height: 400px; width: 737px" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="PerformingArts\Try1Report.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="PAA_PROD_DS" 
                    Name="PAA_StudentInformation_DS" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="PAA_PROD_DS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:UIF_PerformingArtsConnectionString %>" 
        SelectCommand="SELECT [LastName], [FirstName], [School] FROM [StudentInformation] WHERE ([School] = @School)">
        <SelectParameters>
            <asp:Parameter DefaultValue="Oliver" Name="School" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
