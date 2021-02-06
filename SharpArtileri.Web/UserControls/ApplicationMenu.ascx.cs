using System;
using Ninject;
using SharpArtileri.Services;
using Telerik.Web.UI;

namespace SharpArtileri.Web.UserControls
{
    public partial class ApplicationMenu : System.Web.UI.UserControl
    {
        [Inject]
        public SecurityProvider SecurityService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            mnuMenu.Items.Clear();
            LoadMenus();
        }


        public void LoadMenus()
        {
            var menus = SecurityService.GetAllMenus(null);
            foreach (var menu in menus)
            {
                var menuItem = new RadMenuItem(menu.Title, "~/" + menu.NavigationTo);
                menuItem.Value = menu.ID.ToString();

                PopulateChildMenu(menuItem, menu.ID);

                mnuMenu.Items.Add(menuItem);
            }

        }

        private void PopulateChildMenu(RadMenuItem parentMenu, int menuID)
        {
            var childMenus = SecurityService.GetAllMenus(menuID);
            foreach (var childMenu in childMenus)
            {
                var menuItem = new RadMenuItem(childMenu.Title, "~/" + childMenu.NavigationTo);
                menuItem.Value = childMenu.ID.ToString();
                parentMenu.Items.Add(menuItem);

                PopulateChildMenu(menuItem, childMenu.ID);
            }
        }
    }
}