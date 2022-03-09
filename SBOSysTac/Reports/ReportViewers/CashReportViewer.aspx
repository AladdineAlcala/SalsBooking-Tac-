﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashReportViewer.aspx.cs" Inherits="SBOSysTac.Reports.ReportViewers.CashReportViewer" %>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CRViewerCashReport" runat="server"  Width="100%" Height="100%"  AutoDataBind="True" ToolPanelView="None" EnableParameterPrompt="false"  />
    </div>
    </form>
</body>
</html>
