using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharpArtileri.Web.Helpers;

namespace VIPDC.Web.MasterPages
{
    public partial class MasterWorkspace : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BrowserCompatibility.IsUplevel)
            {
                Page.ClientTarget = "uplevel";
            }                   
        }

        protected override void AddedControl(Control control, int index)
        {
            // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
                this.Page.ClientTarget = "uplevel";

            base.AddedControl(control, index);
        } 
    }
}