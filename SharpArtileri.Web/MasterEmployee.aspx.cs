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
    public partial class MasterEmployee : BaseForm
    {
        [Inject]
        public ManagementProvider ManagementService { get; set; }

        [Inject]
        public SecurityProvider SecurityService { get; set; }

        [Inject]
        public EmployeeProvider EmployeeService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            sdsMaster.ConnectionString = ManagementService.GetConnectionString(this.GetCurrentCompanyCode());
            if (!Page.IsPostBack)
            {
                mvwForm.SetActiveView(viwRead);
                RadHelper.SetUpGrid(RadGrid1);
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
            ddlRole.DataSource = SecurityService.GetAllRoles(RowID == 0);
            ddlRole.DataTextField = "Name";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();

            ddlFindRole.DataSource = SecurityService.GetAllRoles();
            ddlFindRole.DataTextField = "Name";
            ddlFindRole.DataValueField = "ID";
            ddlFindRole.DataBind();
            ddlFindRole.Items.Insert(0, new DropDownListItem("All", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("AddEdit");
            if (Page.IsValid)
            {
                try
                {
                    EmployeeService.AddOrUpdateEmployee(RowID,
                    txtCode.Text,
                    txtName.Text,
                    rblGender.SelectedValue,
                    txtEmail.Text,
                    Convert.ToInt32(ddlRole.SelectedValue),
                    chkIsAllowLogin.Checked,
                    chkIsActive.Checked,
                    new Dictionary<int, string> { { 1, txtCellPhone1.Text }, { 2, txtCellPhone2.Text } });

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

        protected void lnbAddNew_Click(object sender, EventArgs e)
        {
            mvwForm.SetActiveView(viwAddEdit);
            FillDropDown();
            txtCode.Focus();
        }

        protected void lnbDelete_Click(object sender, EventArgs e)
        {
            int[] arrayOfID = RadHelper.GetRowIdForDeletion(RadGrid1);
            if (EmployeeService.CanDeleteEmployee(arrayOfID))
            {
                EmployeeService.DeleteEmployee(arrayOfID);
                btnCancel_Click(sender, e);
            }
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                RowID = Convert.ToInt32(e.CommandArgument);
                mvwForm.SetActiveView(viwAddEdit);
                FillDropDown();


                var emp = EmployeeService.GetEmployee(RowID);
                if (emp != null)
                {
                    txtCode.Text = emp.UserName;
                    txtEmail.Text = emp.Email;
                    txtName.Text = emp.Name;
                    txtCellPhone1.Text = emp.CellPhone1;
                    txtCellPhone2.Text = emp.CellPhone2;
                    ddlRole.SelectedValue = Convert.ToString(emp.RoleID);
                    chkIsActive.Checked = emp.IsActive;
                    chkIsAllowLogin.Checked = emp.IsAllowLogin;

                    txtCode.Focus();
                }
            }
        }

        protected void odsFilterRoleName_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = SecurityService;
        }

        protected void odsFilterRoleName_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["forAddNew"] = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGrid1.DataBind();
        }

        protected void sdsMaster_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@UserName"].Value = txtFindCode.Text;
            e.Command.Parameters["@Name"].Value = txtFindName.Text;
            e.Command.Parameters["@RoleID"].Value = String.IsNullOrEmpty(ddlFindRole.SelectedValue) ? 0 : Convert.ToInt32(ddlFindRole.SelectedValue);
        }
    }
}