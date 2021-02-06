using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Services;

namespace SharpArtileri.Web.UserControls
{
    public partial class SupplierInformation : System.Web.UI.UserControl
    {
        [Inject]
        public SupplierProvider SupplierService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadSupplierInformation(int supplierID)
        {
            ViewState["SupplierID"] = supplierID;
            dtvSupplier.DataBind();            
        }

        protected void odsSupplier_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = SupplierService;
        }

        protected void odsSupplier_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["id"] = ViewState["SupplierID"];
        }
    }
}