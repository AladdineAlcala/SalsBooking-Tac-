<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerContractPrint.aspx.cs" Inherits="SBOSys.Reports.ReportViewers.ReportViewerContractPrint" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
       
    <form id="formprintcontract" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CRViewerContract" runat="server"  Width="100%" Height="100%"  AutoDataBind="True" ToolPanelView="None" EnableParameterPrompt="false"  />
        </div>
    </form>
</body>
</html> 
