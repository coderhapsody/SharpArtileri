using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SharpArtileri.Services;
using SharpArtileri.Web.Base;

namespace SharpArtileri.Web
{
    public partial class MasterRoleMenu : BaseForm
    {
        [Inject]
        public SecurityProvider SecurityService { get; set; }

        [Inject]
        public ManagementProvider ManagementService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //sdsMaster.ConnectionString = ManagementService.GetUserSessionConnectionString(User.Identity.Name);
            if (!this.IsPostBack)
            {
                PopulateMenus();
                btnSave.Visible = false;
            }


        }

        private void PopulateMenus()
        {
            var menus = SecurityService.GetAllMenusIgnoringRole(null);
            foreach (var menu in menus)
            {
                TreeNode menuItem = new TreeNode(menu.Title, menu.ID.ToString());

                PopulateChildMenu(menuItem, menu.ID);

                tvwMenus.Nodes.Add(menuItem);
                menuItem.Expanded = false;
            }

        }

        private void PopulateChildMenu(TreeNode parentMenu, int menuID)
        {
            var childMenus = SecurityService.GetAllMenusIgnoringRole(menuID);
            foreach (var childMenu in childMenus)
            {
                var menuItem = new TreeNode(childMenu.Title, childMenu.ID.ToString());
                parentMenu.ChildNodes.Add(menuItem);

                PopulateChildMenu(menuItem, childMenu.ID);
            }
        }

        protected void tvwMenus_SelectedNodeChanged(object sender, EventArgs e)
        {
            int menuID = Convert.ToInt32(tvwMenus.SelectedValue);
            RowID = menuID;

            lblMenuName.Text = tvwMenus.SelectedNode.Text;

            cblRoles.DataSource = SecurityService.GetAllRoles();
            cblRoles.DataValueField = "ID";
            cblRoles.DataTextField = "Name";
            cblRoles.DataBind();

            btnSave.Visible = true;

            var roles = SecurityService.GetRolesForMenu(menuID);
            foreach (var role in roles)
                cblRoles.Items.FindByValue(Convert.ToString(role)).Selected = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int[] selectedRoles = Array.ConvertAll(
                cblRoles.Items.Cast<ListItem>().Where(item => item.Selected).Select(item => item.Value).ToArray(),
                Convert.ToInt32);
            SecurityService.UpdateRoleMenu(RowID, selectedRoles);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "_save", "alert('Menu settings saved')", true);
        }
    }
}