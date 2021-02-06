using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Services;
using SharpArtileri.Web.Helpers;

namespace SharpArtileri.Web.UserControls
{
    public partial class CurrentActiveCompany : System.Web.UI.UserControl
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //var userSession = ManagementService.GetUserSession(Page.User.Identity.Name);
            lblCurrentActiveCompany.Text = String.Format("{0}", UserSessionHelper.GetCurrentCompanyCode());
        }
    }
}