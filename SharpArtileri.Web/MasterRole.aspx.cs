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
    public partial class MasterRole : BaseForm
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }      

        [Inject]
        public SecurityProvider SecurityService { get; set; }        


        protected void Page_Load(object sender, EventArgs e)
        {
            
            sdsMaster.ConnectionString = ManagementService.GetConnectionString(this.GetCurrentCompanyCode());
            if (!IsPostBack)
            {
                mvwForm.SetActiveView(viwRead);                
                RadHelper.SetUpGrid(RadGrid1);
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

        protected void gvwMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            WebFormHelper.HideGridViewRowId(e);
            WebFormHelper.ChangeBackgroundColorRowOnHover(e);
        }

        protected void lnbAddNew_Click(object sender, EventArgs e)
        {
            mvwForm.SetActiveView(viwAddEdit);
            txtName.Text = String.Empty;
            chkIsActive.Checked = true;            
            txtName.Focus();
        }

        protected void lnbDelete_Click(object sender, EventArgs e)
        {
            int[] arrayOfID = RadHelper.GetRowIdForDeletion(RadGrid1);
            if (SecurityService.CanDeleteRoles(arrayOfID))
            {
                SecurityService.DeleteRoles(arrayOfID);
                btnCancel_Click(sender, e);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReloadCurrentPage();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate(WebFormHelper.AddEditValidationGroup);

            if (!Page.IsValid) return;

            try
            {
                SecurityService.AddOrUpdateRole(RowID, txtName.Text, chkIsActive.Checked);
                btnCancel_Click(sender, e);
            }
            catch (Exception ex)
            {
                WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, ex.Message, LabelStyleNames.ErrorMessage);
            }
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                mvwForm.SetActiveView(viwAddEdit);
                RowID = Convert.ToInt32(e.CommandArgument);
                var role = SecurityService.GetRole(RowID);
                txtName.Text = role.Name;
                chkIsActive.Checked = role.IsActive;
                txtName.Focus();
            }
        }
    }
}
