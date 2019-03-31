using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SBOSys.HtmlHelperClass;
using SBOSys.ViewModel;

namespace SBOSys.Reports.ReportViewers
{
    public partial class ReportViewerContractPrint : System.Web.UI.Page
    {
        private PrintContractDetails condetails = new PrintContractDetails();
        private BookMenusViewModel bm = new BookMenusViewModel();
        private AddonsViewModel add = new AddonsViewModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    var paramTransId = Request["transactionId"].Trim();


                    List<PrintContractDetails> conDetails = new List<PrintContractDetails>();
                    List<BookMenusViewModel> conBookMenus = new List<BookMenusViewModel>();
                    List<AddonsViewModel> addons = new List<AddonsViewModel>();

                    //var paramPrint_Option = Request["reportOption"].Trim();

                    var cryRep = new ReportDocument();
                    TableLogOnInfos tbloginfos = new TableLogOnInfos();
                    ConnectionInfo crConinfo = new ConnectionInfo();

                    string reportName = "ReportContract2Details";

                    string report = Utilities.ReportPath(reportName);

                    cryRep.Load(report);

                    SqlConnectionStringBuilder cnstrbuilding = new SqlConnectionStringBuilder(Utilities.DBGateway());


                    crConinfo.ServerName = cnstrbuilding.DataSource;
                    crConinfo.DatabaseName = cnstrbuilding.InitialCatalog;
                    crConinfo.UserID = cnstrbuilding.UserID;
                    crConinfo.Password = cnstrbuilding.Password;

                    var reportSections = cryRep.ReportDefinition.Sections;


                    foreach (Section section in reportSections)
                    {
                        var crReportObjects = section.ReportObjects;

                        foreach (ReportObject crreportObject in crReportObjects)
                        {

                            if (crreportObject.Kind != ReportObjectKind.SubreportObject)
                                continue;

                            var crSubreportObject = (SubreportObject)crreportObject;
                            var crsubReportDocument = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName);
                            var crDatabase = crsubReportDocument.Database;
                            var crTables = crDatabase.Tables;

                            //var tbloginfos = new TableLogOnInfos();


                            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                            {

                                var crTableLogOnInfo = crTable.LogOnInfo;
                                crTableLogOnInfo.ConnectionInfo = crConinfo;
                                crTableLogOnInfo.ConnectionInfo.IntegratedSecurity = true;
                                crTable.ApplyLogOnInfo(crTableLogOnInfo);

                            }

                        }
                    }



                    var cryTables = cryRep.Database.Tables;

                    foreach (CrystalDecisions.CrystalReports.Engine.Table cryTable in cryTables)
                    {
                        var tbloginfo = cryTable.LogOnInfo;
                        tbloginfo.ConnectionInfo = crConinfo;
                        tbloginfo.ConnectionInfo.IntegratedSecurity = true;
                        cryTable.ApplyLogOnInfo(tbloginfo);
                    }


                 
                    CRViewerContract.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

                   // ReportContract repcontract = new ReportContract();

                    conDetails = (from c in condetails.GetContractDetails() select c).ToList();

                    conDetails = conDetails.Where(x => x.transId == Convert.ToInt32(paramTransId)).ToList();
                    //where c.transId == Convert.ToInt32(paramTransId)
                    //select c).ToList();


                    conBookMenus = bm.LisofMenusBook().Where(x => x.transId == Convert.ToInt32(paramTransId)).ToList();

                    addons = add.ListofAddons().Where(x => x.TransId == Convert.ToInt32(paramTransId)).ToList();



                    //repcontract.SetDataSource(conDetails);



                    cryRep.Database.Tables[0].SetDataSource(conDetails);
                    cryRep.Database.Tables[1].SetDataSource(conBookMenus);
                    cryRep.Database.Tables[2].SetDataSource(addons);


                    CRViewerContract.ReportSource = cryRep;
                    CRViewerContract.RefreshReport();

                    //cryRep.PrintToPrinter(1,false,0,0);

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