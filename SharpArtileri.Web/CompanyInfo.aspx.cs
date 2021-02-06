using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Services;
using SharpArtileri.Web.Base;
using SharpArtileri.Web.Helpers;

namespace SharpArtileri.Web
{
    public partial class CompanyInfo : BaseForm
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var company = ManagementService.GetCompany(this.GetCurrentCompanyCode());
                if (company == null) return;
                txtCompanyName.Text = company.Name;
                txtAddress1.Text = company.Address1;
                txtAddress2.Text = company.Address2;
                txtPOApproverName.Text = company.POApproverName;
                lblCompanyCode.Text = company.Code;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ManagementService.UpdateCompanyInformation(
                    lblCompanyCode.Text,
                    txtCompanyName.Text,
                    txtAddress1.Text,
                    txtAddress2.Text,
                    txtPOApproverName.Text);

                ClientScript.RegisterStartupScript(this.GetType(), "save", 
                    String.Format("alert('Company Information for {0} has been saved');", lblCompanyCode.Text), true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "save",
                    String.Format("alert('{0}');", ex.Message), true);
            }
        }
    }
}