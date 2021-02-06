using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Services;
using SharpArtileri.Services.ViewModels;
using SharpArtileri.Web.Base;
using SharpArtileri.Web.Helpers;
using Telerik.Web.UI;

namespace SharpArtileri.Web
{
    public partial class MasterPriceList : BaseForm
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        [Inject]
        public SecurityProvider SecurityService { get; set; }

        [Inject]
        public ItemProvider ItemService { get; set; }

        [Inject]
        public SupplierProvider SupplierService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            sdsMaster.ConnectionString = ManagementService.GetConnectionString(this.GetCurrentCompanyCode());
            if (!Page.IsPostBack)
            {
                mvwForm.SetActiveView(viwRead);
                RadHelper.SetUpGrid(grdMaster);
                FillDropDown();
            }            
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            var privilege = SecurityService.GetPrivilege(
                ManagementService.GetConnectionString(this.GetCurrentCompanyCode()),
                CurrentPageName);

            if (mvwForm.GetActiveView() == viwRead)
            {
                lnbDelete.Enabled = privilege.AllowDelete;
                if (RowID == 0)
                    lnbAddNew.Enabled = privilege.AllowAddNew;
            }
            else
            {
                if (btnSave.Enabled)
                    btnSave.Enabled = RowID == 0 ? privilege.AllowAddNew : privilege.AllowUpdate;
            }
        }

        private void FillDropDown()
        {
            cboSupplier.DataSource = SupplierService.GetSuppliers();
            cboSupplier.DataTextField = "Name";
            cboSupplier.DataValueField = "ID";
            cboSupplier.DataBind();
            cboSupplier.Items.Insert(0, new RadComboBoxItem(String.Empty));

            cboFindSupplier.DataSource = SupplierService.GetSuppliers();
            cboFindSupplier.DataTextField = "Name";
            cboFindSupplier.DataValueField = "ID";
            cboFindSupplier.DataBind();
            cboFindSupplier.Items.Insert(0, new RadComboBoxItem(String.Empty));

            cboItem.DataSource = ItemService.GetItems("");
            cboItem.DataTextField = "Name";
            cboItem.DataValueField = "ID";
            cboItem.DataBind();
            cboItem.Items.Insert(0, new RadComboBoxItem(String.Empty));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdMaster.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("AddEdit");
            if (Page.IsValid)
            {
                try
                {
                    ItemService.AddOrUpdatePriceList(RowID,
                        Convert.ToInt32(cboItem.SelectedValue),
                        Convert.ToInt32(cboSupplier.SelectedValue),
                        ddlUnit.SelectedItem.Text,
                        dtpDate.SelectedDate.GetValueOrDefault(),
                        Convert.ToDecimal(ntbUnitPrice.Value));

                    ReloadCurrentPage();
                }
                catch (Exception ex)
                {
                    WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, ex.Message, LabelStyleNames.ErrorMessage);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReloadCurrentPage();
        }

        protected void grdMaster_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                RowID = Convert.ToInt32(e.CommandArgument);
                mvwForm.SetActiveView(viwAddEdit);
                var priceList = ItemService.GetPriceList(RowID);
                if (priceList == null) return;
                ViewState["ItemID"] = priceList.ItemID;
                cboItem.SelectedValue = priceList.ItemID.ToString();
                //cboItem.FindItemByValue(priceList.ItemID.ToString());
                cboItem_SelectedIndexChanged(null, null);
                cboSupplier.SelectedValue = priceList.SupplierID.ToString();
                ddlUnit.SelectedValue = priceList.UnitName;
                ntbUnitPrice.Value = Convert.ToDouble(priceList.Price);
                dtpDate.SelectedDate = priceList.EffectiveDate;

            }
        }

        protected void sdsMaster_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@SupplierID"].Value = String.IsNullOrEmpty(cboFindSupplier.SelectedValue)
                ? 0
                : Convert.ToInt32(cboFindSupplier.SelectedValue);
            e.Command.Parameters["@ItemID"].Value = String.IsNullOrEmpty(cboFindItem.SelectedValue)
                ? 0
                : Convert.ToInt32(cboFindItem.SelectedValue);
        }

        protected void cboItem_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                var units = e != null
                    ? ItemService.GetUnitsOfItem(Convert.ToInt32(e.Value))
                    : ItemService.GetUnitsOfItem(Convert.ToInt32(ViewState["ItemID"]));
                ddlUnit.DataSource = units;
                ddlUnit.DataBind();
            }
            catch (Exception ex)
            {
                WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, "Invalid Item", LabelStyleNames.ErrorMessage);
            }
        }

        protected void lnbAddNew_Click(object sender, EventArgs e)
        {
            mvwForm.SetActiveView(viwAddEdit);
            dtpDate.SelectedDate = DateTime.Today;
            cboItem.Focus();
        }

        protected void lnbDelete_Click(object sender, EventArgs e)
        {
            int[] arrayOfID = RadHelper.GetRowIdForDeletion(grdMaster);            
            ItemService.DeletePriceList(arrayOfID);
            ReloadCurrentPage();            
        }

        protected void cboFindItem_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            cboFindItem.DataSource = ItemService.GetItems(e.Text);
            cboFindItem.DataTextField = "Name";
            cboFindItem.DataValueField = "ID";
            cboFindItem.DataBind();
        }

        protected void cuvPrice_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !String.IsNullOrEmpty(args.Value) && Convert.ToDecimal(ntbUnitPrice.Value) > 0;
        }


    }
}