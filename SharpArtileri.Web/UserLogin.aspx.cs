using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Data;
using SharpArtileri.Services;
using SharpArtileri.Web.Helpers;

namespace SharpArtileri.Web
{
    public partial class UserLogin : Page
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        private readonly string cryptographyKey = ConfigurationManager.AppSettings[ApplicationSettingKeys.CryptographyKey];


        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                txtUserName.Focus();
                FillDropDown();                
            }            
        }

        private void FillDropDown()
        {
            ddlCompany.DataSource = ManagementService.GetCompanies();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "Code";
            ddlCompany.DataBind();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {            
            var connectionString = ManagementService.GetConnectionString(ddlCompany.SelectedValue);
            bool canLogin = false;
            using (var dataContext = new ArtileriDataContext(connectionString))
            {
                var securityProvider = new SecurityProvider(dataContext, User);
                canLogin = securityProvider.ValidateUser(txtUserName.Text, txtPassword.Text, cryptographyKey);
            }

            if (!canLogin)
            {
                WebFormHelper.SetLabelTextWithCssClass(lblStatus, "Invalid User Name or Password", LabelStyleNames.ErrorMessage);
                txtUserName.Focus();
            }
            else
            {                
                FormsAuthentication.SetAuthCookie(txtUserName.Text + "|" + ddlCompany.SelectedValue, false);                          
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
        }
    }
}