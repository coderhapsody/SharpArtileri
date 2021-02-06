using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Services;
using SharpArtileri.Web.Base;
using SharpArtileri.Web.Helpers;
using Telerik.Web.UI;

namespace SharpArtileri.Web
{
    public partial class MasterTerms : BaseForm
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        [Inject]
        public TermProvider TermService { get; set; }

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

        protected void gvwMaster_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            WebFormHelper.HideGridViewRowId(e);
            WebFormHelper.ChangeBackgroundColorRowOnHover(e);
        }

        protected void lnbAddNew_OnClick(object sender, EventArgs e)
        {
            mvwForm.SetActiveView(viwAddEdit);
            txtName.Text = String.Empty;
            chkIsActive.Checked = true;
            txtName.Focus();
        }

        protected void lnbDelete_OnClick(object sender, EventArgs e)
        {
            int[] arrayOfID = RadHelper.GetRowIdForDeletion(RadGrid1);
            if (TermService.CanDeleteTerms(arrayOfID))
            {
                TermService.DeleteTerms(arrayOfID);
                btnCancel_OnClick(sender, e);
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            ReloadCurrentPage();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Page.Validate(WebFormHelper.AddEditValidationGroup);

            if (!Page.IsValid) return;

            try
            {
                TermService.AddOrUpdateTerm(RowID, txtName.Text, chkIsActive.Checked);
                btnCancel_OnClick(sender, e);
            }
            catch (Exception ex)
            {
                WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, ex.Message, LabelStyleNames.ErrorMessage);
            }
        }

        protected void RadGrid1_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                mvwForm.SetActiveView(viwAddEdit);
                RowID = Convert.ToInt32(e.CommandArgument);
                var term = TermService.GetTerm(RowID);
                txtName.Text = term.Name;
                chkIsActive.Checked = term.IsActive;
                txtName.Focus();
            }
        }
    }
}