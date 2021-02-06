using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Data;
using SharpArtileri.Services;
using SharpArtileri.Web.Base;
using SharpArtileri.Web.Helpers;
using Telerik.Web.UI;

namespace SharpArtileri.Web
{
    public partial class ManageRolePrivilege : BaseForm
    {
        [Inject]
        public SecurityProvider SecurityService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int roleID = String.IsNullOrEmpty(Request["RoleID"]) ? 1 : Convert.ToInt32(Request["RoleID"]);
            RowID = Convert.ToInt32(roleID);
            if (!IsPostBack)
            {
                lblRoleName.Text = SecurityService.GetRole(roleID).Name;
                PopulateMenus();
                cblPrivilege.Visible = btnSave.Visible = false;
            }
        }

        private void PopulateMenus()
        {
            var menus = SecurityService.GetAllMenusForRole(RowID, null);
            foreach (var menu in menus)
            {
                var menuItem = new RadTreeNode(menu.Title, menu.ID.ToString());
                menuItem.Category = menu.Type.GetValueOrDefault(0).ToString();

                PopulateChildMenu(menuItem, menu.ID);

                tvwMenus.Nodes.Add(menuItem);
                menuItem.Expanded = false;
            }

        }

        private void PopulateChildMenu(RadTreeNode parentMenu, int menuID)
        {
            var childMenus = SecurityService.GetAllMenusForRole(RowID, menuID);
            foreach (var childMenu in childMenus)
            {
                var menuItem = new RadTreeNode(childMenu.Title, childMenu.ID.ToString());
                menuItem.Category = childMenu.Type.GetValueOrDefault(0).ToString();
                parentMenu.Nodes.Add(menuItem);

                PopulateChildMenu(menuItem, childMenu.ID);
            }
        }

        protected void tvwMenus_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (e.Node.Nodes.Count == 0 && Convert.ToInt32(e.Node.Category) != 0)
            {
                int menuID = Convert.ToInt32(e.Node.Value);
                var roleMenu = SecurityService.GetRolePrivilege(menuID, RowID);
                SetPrivilegeOptions(roleMenu);
                cblPrivilege.Visible = btnSave.Visible = true;

                cblPrivilege.Items.FindByValue("C").Enabled = Convert.ToInt32(e.Node.Category) != 2;
                cblPrivilege.Items.FindByValue("D").Enabled = Convert.ToInt32(e.Node.Category) != 2;
            }
            else cblPrivilege.Visible = btnSave.Visible = false;
        }

        private void SetPrivilegeOptions(RoleMenu roleMenu)
        {
            AllowAddNew = roleMenu.AllowAddNew.GetValueOrDefault(false);
            AllowUpdate = roleMenu.AllowUpdate.GetValueOrDefault(false);
            AllowDelete = roleMenu.AllowDelete.GetValueOrDefault(false);
        }

        public bool AllowAddNew
        {
            get { return cblPrivilege.Items.FindByValue("C").Selected; }
            set
            {
                cblPrivilege.Items.FindByValue("C").Selected = value;
            }
        }

        public bool AllowUpdate
        {
            get { return cblPrivilege.Items.FindByValue("U").Selected; }
            set
            {
                cblPrivilege.Items.FindByValue("U").Selected = value;
            }
        }

        public bool AllowDelete
        {
            get { return cblPrivilege.Items.FindByValue("D").Selected; }
            set
            {
                cblPrivilege.Items.FindByValue("D").Selected = value;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SecurityService.SetRolePrivilege(
                    Convert.ToInt32(tvwMenus.SelectedValue),
                    RowID,
                    AllowAddNew,
                    AllowUpdate,
                    AllowDelete);

                WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit,
                    String.Format("Privileges have been set for menu <b>{0}</b> of role <b>{1}</b>",
                        tvwMenus.SelectedNode.Text,
                        lblRoleName.Text),
                    LabelStyleNames.InfoMessage);
            }
            catch (Exception ex)
            {
                WebFormHelper.SetLabelTextWithCssClass(lblStatusAddEdit, ex.Message, LabelStyleNames.ErrorMessage);
            }
        }


    }
}