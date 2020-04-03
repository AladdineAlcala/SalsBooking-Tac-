﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SBOSysTac.HtmlHelperClass;
using SBOSysTac.ServiceLayer;

namespace SBOSysTac.Reports.ReportViewers
{
    public partial class Admin_MonthCashReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    var paramfilterdatefrom = Request["month"].Trim();

                    ReportDocument cryRep = new ReportDocument();
                    TableLogOnInfos tbloginfos = new TableLogOnInfos();
                    ConnectionInfo crConinfo = new ConnectionInfo();

                    string reportName = "AdminMonthCateringReport";

                    string report = Utilities.ReportPath(reportName);

                    cryRep.Load(report);

                    SqlConnectionStringBuilder cnstrbuilding = new SqlConnectionStringBuilder(Utilities.DBGateway());

                    crConinfo.ServerName = cnstrbuilding.DataSource;
                    crConinfo.DatabaseName = cnstrbuilding.InitialCatalog;
                    crConinfo.UserID = cnstrbuilding.UserID;
                    crConinfo.Password = cnstrbuilding.Password;

                    var cryTables = cryRep.Database.Tables;

                    foreach (CrystalDecisions.CrystalReports.Engine.Table cryTable in cryTables)
                    {
                        var tbloginfo = cryTable.LogOnInfo;
                        tbloginfo.ConnectionInfo = crConinfo;
                        tbloginfo.ConnectionInfo.IntegratedSecurity = true;
                        cryTable.ApplyLogOnInfo(tbloginfo);
                    }



                    CRViewerAdminCateringReport.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

                    //var accnsummarybyClient = accnsum.GetAllAccnRecievables().ToList();

                    //var accountreceivablesClient = Utilities.TransRecievablesAccn;
                    //DateTime monthreport = Convert.ToDateTime(paramfilterdatefrom);

                    cryRep.Database.Tables[0].SetDataSource(ContainerClass.CateringReport);

                    cryRep.SetParameterValue("MonthSched", Convert.ToDateTime(paramfilterdatefrom).ToString("MMMM yyyy"));

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        cryRep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false,
                            "AdminMonthCateringReport");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }

            }
        }
    }
}