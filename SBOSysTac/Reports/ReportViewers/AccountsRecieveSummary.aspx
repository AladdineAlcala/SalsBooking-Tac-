﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsRecieveSummary.aspx.cs" Inherits="SBOSysTac.Reports.ReportViewers.AccountsRecieveSummary" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <CR:CrystalReportViewer ID="CRViewerAccnRecievableSummary" runat="server"  Width="100%" Height="100%"  AutoDataBind="True" ToolPanelView="None" EnableParameterPrompt="false"  />
        </div>
    </div>
    </form>
</body>
</html>
