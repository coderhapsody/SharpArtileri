using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SharpArtileri.Data;
using SharpArtileri.Services.Base;
using SharpArtileri.Services.Extensions;
using SharpArtileri.Services.Helpers;
using SharpArtileri.Services.ViewModels;

namespace SharpArtileri.Services
{
    public class SecurityProvider : BaseProvider
    {
        public SecurityProvider(ArtileriDataContext dataContext, IPrincipal principal)
            : base(dataContext, principal)
        {
        }

        public bool ValidateUser(string userName, string password, string cryptographyKey)
        {

            var employee = context.Employees.SingleOrDefault(emp => emp.UserName == userName && emp.IsAllowLogin);
            if (employee != null)
            {
                var clearPassword = RijndaelHelper.Decrypt(employee.Password, cryptographyKey);
                return clearPassword == password;
            }
            return false;
        }

        public void ChangePassword(string userName, string newPassword, string cryptographyKey)
        {
            var employee = context.Employees.SingleOrDefault(emp => emp.UserName == userName);
            if (employee != null)
            {
                employee.Password = RijndaelHelper.Encrypt(newPassword, cryptographyKey);                
                EntityHelper.SetAuditFieldsForUpdate(employee, userName);
            }
            context.SubmitChanges();
        }        

        #region Role
        public bool CanDeleteRoles(int[] arrayOfID)
        {
            // TODO: add logic to validate whether a role can be safely deleted or not
            return true;
        }

        public void AddOrUpdateRole(int id, string roleName, bool isActive)
        {
            var role = id == 0 ? new Role() : context.Roles.SingleOrDefault(r => r.ID == id);
            if (role != null)
            {
                role.Name = roleName;
                role.IsActive = isActive;
                EntityHelper.SetAuditFields(id, role, CurrentUserName);

                if(id == 0)
                    context.Roles.InsertOnSubmit(role);

                context.SubmitChanges();
            }
        }

        public void DeleteRoles(int[] arrayOfID)
        {
            context.Roles.DeleteAllOnSubmit(context.Roles.Where(id => arrayOfID.Contains(id.ID)));
            context.SubmitChanges();
        }

        public int GetRoleID()
        {
            var employee = context.Employees.SingleOrDefault(emp => emp.UserName == CurrentUserName);
            return employee != null ? employee.RoleID : 0;
        }

        public Role GetRole(string userName)
        {
            var employee = context.Employees.SingleOrDefault(emp => emp.UserName == userName);
            return employee == null ? null : employee.Role;
        }

        public string GetRoleName(string userName)
        {
            var role = GetRole(userName);
            if (role == null)
                return String.Empty;

            return role.Name;
        }

        public Role GetRole(int id)
        {
            var role = context.Roles.SingleOrDefault(r => r.ID == id);
            return role;
        }

        #endregion

        #region Menus

        public MenuPrivilege GetPrivilege(string connectionString, string pageName)
        {
            MenuPrivilege priv = null;
            using (var ctx = new ArtileriDataContext(connectionString))
            {
                var currentMenu = ctx.Menus.SingleOrDefault(menu => menu.NavigationTo == pageName);
                if (currentMenu != null)
                {
                    int roleID = ctx.Employees.SingleOrDefault(emp => emp.UserName == CurrentUserName).RoleID;
                    var roleMenu = ctx.RoleMenus.SingleOrDefault(rm => rm.RoleID == roleID && rm.MenuID == currentMenu.ID);
                    if (roleMenu != null)
                    {
                        priv = new MenuPrivilege(pageName, roleMenu);
                    }
                }
            }
            return priv;
        }

        public IEnumerable<Menu> GetAllMenus(int? parentMenuID)
        {
            int roleID = GetRoleID();

            if (parentMenuID.HasValue)
                return from menu in context.Menus
                       join roleMenu in context.RoleMenus.Where(rm => rm.RoleID == roleID) on menu.ID equals roleMenu.MenuID
                       where menu.IsActive && menu.ParentMenuID.Value == parentMenuID.Value
                       orderby menu.Seq
                       select menu;

            return from menu in context.Menus
                   join roleMenu in context.RoleMenus.Where(rm => rm.RoleID == roleID) on menu.ID equals roleMenu.MenuID
                   where menu.IsActive && !menu.ParentMenuID.HasValue
                   orderby menu.Seq
                   select menu;
        }


        #endregion

        public RoleMenu GetRolePrivilege(int menuID, int roleID)
        {
            return context.RoleMenus.SingleOrDefault(rm => rm.RoleID == roleID && rm.MenuID == menuID);
        }

        public void SetRolePrivilege(int menuID, int roleID, bool allowAddNew, bool allowUpdate, bool allowDelete)
        {
            var roleMenu = GetRolePrivilege(menuID, roleID);
            if (roleMenu != null)
            {
                roleMenu.AllowAddNew = allowAddNew;
                roleMenu.AllowUpdate = allowUpdate;
                roleMenu.AllowDelete = allowDelete;
                context.SubmitChanges();
            }
        }

        public IEnumerable<Menu> GetAllMenusForRole(int roleID, int? parentMenuID)
        {
            if (parentMenuID.HasValue)
                return from menu in context.Menus
                       join roleMenu in context.RoleMenus.Where(rm => rm.RoleID == roleID) on menu.ID equals roleMenu.MenuID
                       where menu.IsActive && menu.ParentMenuID.Value == parentMenuID.Value
                       orderby menu.Seq
                       select menu;

            return from menu in context.Menus
                   join roleMenu in context.RoleMenus.Where(rm => rm.RoleID == roleID) on menu.ID equals roleMenu.MenuID
                   where menu.IsActive && !menu.ParentMenuID.HasValue
                   orderby menu.Seq
                   select menu;
        }

        public IEnumerable<Menu> GetAllMenusIgnoringRole(int? parentMenuID)
        {
            if (parentMenuID.HasValue)
                return from menu in context.Menus
                       where menu.IsActive && menu.ParentMenuID.Value == parentMenuID.Value
                       orderby menu.Seq
                       select menu;

            return from menu in context.Menus
                   where menu.IsActive && !menu.ParentMenuID.HasValue
                   orderby menu.Seq
                   select menu;
        }

        public IEnumerable<int> GetRolesForMenu(int menuID)
        {
            return context.RoleMenus.Where(rm => rm.MenuID == menuID)
                .Select(rm => rm.RoleID);
        }

        public IEnumerable<Role> GetAllRoles(bool forAddNew = false)
        {
            return forAddNew ? context.Roles.Where(role => role.IsActive) : context.Roles;
        }

        public void UpdateRoleMenu(int menuID, int[] roles)
        {
            context.RoleMenus.DeleteAllOnSubmit(context.RoleMenus.Where(rm => rm.MenuID == menuID));
            foreach (var roleID in roles)
            {
                var rm = new RoleMenu
                         {
                             MenuID = menuID,
                             RoleID = roleID,
                             CreatedWhen = DateTime.Now,
                             CreatedWho = CurrentUserName
                         };
                context.RoleMenus.InsertOnSubmit(rm);               
            }
            context.SubmitChanges();
        }

        public void SetDefaultCompanyForUser(string companyCode, string userName)
        {
            var employee = context.Employees.Single(emp => emp.UserName == userName);
            employee.CurrentCompanyCode = companyCode;
            context.SubmitChanges();
        }
    }
}
