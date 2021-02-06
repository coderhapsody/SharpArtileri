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
    public partial class ItemPriceList : System.Web.UI.UserControl
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            sdsMaster.ConnectionString = ManagementService.GetConnectionString(UserSessionHelper.GetCurrentCompanyCode());
            grdMaster.Columns[0].Visible = false;
        }

        public void LoadPriceList(int itemID)
        {
            ViewState["_ItemID"] = itemID;
            grdMaster.DataBind();
        }

        protected void sdsMaster_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@ItemID"].Value = ViewState["_ItemID"];
            e.Command.Parameters["@SupplierID"].Value = 0;
        }
    }
}