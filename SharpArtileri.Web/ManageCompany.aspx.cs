using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Services;
using SharpArtileri.Web.Helpers;

namespace SharpArtileri.Web
{
    public partial class ManageCompany : System.Web.UI.Page
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mvwForm.SetActiveView(viwRead);   
            }
        }

        protected void lnbAddNew_Click(object sender, EventArgs e)
        {
            mvwForm.SetActiveView(viwAddEdit);
            ViewState["Code"] = "";
            //txtCode.Text = txtDatabase.Text = txtName.Text = txtPassword.Text = txtServer.Text = txtUserID.Text = String.Empty;            
            txtCode.ReadOnly = false;
            txtCode.Focus();
        }

        protected void grdMaster_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                string code = Convert.ToString(e.CommandArgument);
                ViewState["Code"] = code;
                mvwForm.SetActiveView(viwAddEdit);
                var company = ManagementService.GetCompany(code);
                if (company != null)
                {
                    string[] connectionStringElements = company.ConnectionString.Split(';');
                    txtCode.Text = company.Code;
                    txtName.Text = company.Name;
                    txtUserID.Text = connectionStringElements[2].Split('=')[1];
                    txtPassword.Text = connectionStringElements[3].Split('=')[1];
                    txtServer.Text = connectionStringElements[0].Split('=')[1];
                    txtDatabase.Text = connectionStringElements[1].Split('=')[1]; 
                    txtCode.ReadOnly = true;
                    txtCode.Focus();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (String.IsNullOrEmpty(Convert.ToString(ViewState["Code"])))
                {
                    ManagementService.CreateCompany(txtCode.Text,
                        txtName.Text,
                        txtServer.Text,
                        txtUserID.Text,
                        txtPassword.Text,
                        txtDatabase.Text);
                }
                else
                {
                    ManagementService.UpdateCompany(txtCode.Text,
                        txtName.Text,
                        txtServer.Text,
                        txtUserID.Text,
                        txtPassword.Text,
                        txtDatabase.Text);
                }
            }
            btnCancel_Click(sender, e);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path), true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ManagementService.DeleteCompany(txtCode.Text);
            btnCancel_Click(sender, e);
        }
    }
}