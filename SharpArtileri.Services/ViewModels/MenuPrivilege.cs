using System;
using SharpArtileri.Data;

namespace SharpArtileri.Services.ViewModels
{
    public class MenuPrivilege
    {
        public string PageName { get; private set; }        
        public bool AllowAddNew { get; private set; }
        public bool AllowUpdate { get; private set; }
        public bool AllowDelete { get; private set; }

        public MenuPrivilege(string pageName)
        {
            PageName = pageName;
            AllowAddNew = false;
            AllowDelete = false;
            AllowUpdate = false;
        }

        public MenuPrivilege(string pageName, RoleMenu roleMenu)
        {
            PageName = pageName;
            AllowAddNew = roleMenu.AllowAddNew.GetValueOrDefault(false);
            AllowUpdate = roleMenu.AllowUpdate.GetValueOrDefault(false);
            AllowDelete = roleMenu.AllowDelete.GetValueOrDefault(false);
        }
    }
}
