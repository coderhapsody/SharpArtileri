using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Ninject;
using SharpArtileri.Services;
using SharpArtileri.Web.Base;
using SharpArtileri.Web.Helpers;

namespace SharpArtileri.Web
{
    public partial class ReportPreview : BaseForm
    {
        [Inject]
        public ReportProvider ReportService { get; set; }

        [Inject]
        public ManagementProvider ManagementService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var company = ManagementService.GetCompany(this.GetCurrentCompanyCode());
            ReportService.CompanyName = company.Name;
            ReportService.CompanyAddress1 = company.Address1;
            ReportService.CompanyAddress2 = company.Address2;
            ReportService.POApproverName = company.POApproverName;
            if (!IsPostBack)
            {
                rptReport.InteractivityPostBackMode = InteractivityPostBackMode.AlwaysAsynchronous;
                string reportName = Request.QueryString["ReportName"];
                var keys = Request.QueryString.AllKeys.Where(param => param != "ReportName");

                var parameters = keys.ToDictionary(key => key, key => Request.QueryString[key]);


                var action =
                    Delegate.CreateDelegate(
                        typeof(Action<string, Dictionary<string, string>>),
                        this,
                        reportName);

                action.DynamicInvoke(reportName, parameters);

                //ShowReport(reportName, parameters);
            }
        }

        public void ListPurchaseOrder(string reportName, Dictionary<string, string> parameters)
        {
            rptReport.LocalReport.ReportPath = String.Format(@"{0}/{1}", ConfigurationManager.AppSettings[ApplicationSettingKeys.ReportFolder], reportName + ".rdlc");
            ReportService.ConnectionString =
                ManagementService.GetConnectionString(UserSessionHelper.GetCurrentCompanyCode());
            DateTime fromDate = Convert.ToDateTime(parameters["FromDate"]);
            DateTime toDate = Convert.ToDateTime(parameters["ToDate"]);
            var reportData = ReportService.ListPurchaseOrder(fromDate, toDate);
            var rds = new ReportDataSource("ListPurchaseOrder", reportData);

            var reportParameters = new List<ReportParameter>();
            reportParameters.Add(new ReportParameter("FromDate", fromDate.ToString("dddd, dd MMMM yyyy")));
            reportParameters.Add(new ReportParameter("ToDate", toDate.ToString("dddd, dd MMMM yyyy")));

            rptReport.LocalReport.DataSources.Add(rds);
            rptReport.LocalReport.SetParameters(reportParameters);
            rptReport.LocalReport.Refresh();
        }

        public void SlipPurchaseOrder(string reportName, Dictionary<string, string> parameters)
        {
            rptReport.LocalReport.ReportPath = String.Format(@"{0}/{1}", ConfigurationManager.AppSettings[ApplicationSettingKeys.ReportFolder], reportName + ".rdlc");
            ReportService.ConnectionString =
                ManagementService.GetConnectionString(UserSessionHelper.GetCurrentCompanyCode());
            var detail = ReportService.GetPurchaseOrderDetail(parameters["DocumentNo"]);
            var header = ReportService.GetPurchaseOrderHeader(parameters["DocumentNo"]);
            var rdsHeader = new ReportDataSource("Header", header);
            var rdsDetail = new ReportDataSource("Detail", detail);
            rptReport.LocalReport.DataSources.Add(rdsHeader);
            rptReport.LocalReport.DataSources.Add(rdsDetail);
            rptReport.LocalReport.Refresh();
        }

        //public void ShowReport(string reportName, List<ReportParameter> parameters)
        //{
        //    rptReport.ServerReport.ReportPath = String.Format(@"{0}/{1}", ConfigurationManager.AppSettings[ApplicationSettingKeys.ReportFolder], reportName);
        //    rptReport.ServerReport.SetParameters(parameters);
        //    rptReport.ServerReport.Refresh();
        //}
    }
}