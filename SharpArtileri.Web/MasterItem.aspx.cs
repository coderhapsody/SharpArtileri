using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Data;
using SharpArtileri.Services;
using SharpArtileri.Services.ViewModels;
using SharpArtileri.Web.Base;
using SharpArtileri.Web.Helpers;
using Telerik.Web.UI;

namespace SharpArtileri.Web
{
    public partial class MasterItem : BaseForm
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        [Inject]
        public SecurityProvider SecurityService { get; set; }

        [Inject]
        public ItemProvider ItemService { get; set; }

        [Inject]
        public EmployeeProvider EmployeeService { get; set; }

        [Inject]
        public SupplierProvider SupplierService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {            
            sdsMaster.ConnectionString = ManagementService.GetConnectionString(this.GetCurrentCompanyCode());
            if (!Page.IsPostBack)
            {
                mvwForm.SetActiveView(viwRead);                
                RadHelper.SetUpGrid(grdMaster);
                //FillDropDown();
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

        protected void lnbAddNew_Click(object sender, EventArgs e)
        {
            mvwForm.SetActiveView(viwAddEdit);
            FillDropDown();
            txtCode.Text = "AUTO";
            txtName.Focus();
        }

        private void FillDropDown()
        {
            ddlSupplier.DataSource = SupplierService.GetSuppliers();
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "ID";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new DropDownListItem(String.Empty, String.Empty));
            ddlSupplier.SelectedIndex = 0;
        }

        protected void lnbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int[] arrayOfID = RadHelper.GetRowIdForDeletion(grdMaster);
                if (ItemService.CanDeleteItem(arrayOfID))
                {
                    ItemService.DeleteItem(arrayOfID);
                    btnCancel_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                WebFormHelper.SetLabelTextForException(lblStatus, ex, LabelStyleNames.ErrorMessage);
            }
        }

        protected void grdMaster_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                RowID = Convert.ToInt32(e.CommandArgument);
                mvwForm.SetActiveView(viwAddEdit);
                var item = ItemService.GetItem(RowID);
                FillDropDown();
                if(!String.IsNullOrEmpty(item.Type))
                    ddlType.SelectedValue = item.Type;
                txtCode.Text = item.Code;
                txtName.Text = item.Name;
                txtUnitName1.Text = item.UnitName1;
                txtUnitName2.Text = item.UnitName2;
                txtUnitName3.Text = item.UnitName3;
                ntbUnitRatio2.Value = Convert.ToDouble(item.UnitRatio2);
                ntbUnitRatio3.Value = Convert.ToDouble(item.UnitRatio3);
                chkIsTaxable.Checked = item.IsTaxed;
                if(item.SupplierID.HasValue)
                    ddlSupplier.SelectedValue = Convert.ToString(item.SupplierID.Value);
                else
                    ddlSupplier.SelectedIndex = 0;
                ItemPriceList1.LoadPriceList(RowID);

                txtCode.Focus();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("AddEdit");
            if (Page.IsValid)
            {
                try
                {
                    ItemService.AddOrUpdateItem(                        
                        RowID,
                        txtCode.Text,
                        txtName.Text,
                        ddlType.SelectedValue,
                        Convert.ToInt32(ddlSupplier.SelectedValue),
                        txtUnitName1.Text,
                        chkIsTaxable.Checked,
                        txtUnitName2.Text,
                        txtUnitName3.Text,
                        Convert.ToDecimal(ntbUnitRatio2.Value),
                        Convert.ToDecimal(ntbUnitRatio3.Value));
                    mvwForm.SetActiveView(viwRead);

                    ReloadCurrentPage();
                }
                catch (Exception ex)
                {
                    WebFormHelper.SetLabelTextForException(lblStatusAddEdit, ex, LabelStyleNames.ErrorMessage);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReloadCurrentPage();
        }

        protected void sdsMaster_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@Code"].Value = txtFindCode.Text;
            e.Command.Parameters["@Name"].Value = txtFindName.Text;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdMaster.DataBind();                        
        }
    }
}