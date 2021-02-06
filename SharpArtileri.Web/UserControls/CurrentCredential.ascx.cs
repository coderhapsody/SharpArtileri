using System;
using Ninject;
using SharpArtileri.Services;

namespace SharpArtileri.Web.UserControls
{
    public partial class CurrentCredential : System.Web.UI.UserControl
    {
        [Inject]
        public SecurityProvider SecurityService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userName = Page.User.Identity.Name.Split('|')[0];
            lblCurrentUserName.Text = String.Format("{0} [{1}]",
                userName,
                SecurityService.GetRoleName(userName));
        }
    }
}