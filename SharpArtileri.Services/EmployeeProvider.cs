using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Principal;
using SharpArtileri.Data;
using SharpArtileri.Services.Base;
using SharpArtileri.Services.Helpers;

namespace SharpArtileri.Services
{
    public class EmployeeProvider : BaseProvider
    {
        public EmployeeProvider(ArtileriDataContext dataContext, IPrincipal principal) : base(dataContext, principal)
        {
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.SingleOrDefault(emp => emp.ID == id);
        }

        public Employee GetEmployee(string userName)
        {
            return context.Employees.SingleOrDefault(emp => emp.UserName == userName);
        }

        public IEnumerable<Employee> GetAccountManagers()
        {
            return context.Employees;
        }

        public void AddOrUpdateEmployee(int id,
                                        string userName,
                                        string name,
                                        string gender,
                                        string email,
                                        int roleID,
                                        bool isAllowLogin,
                                        bool isActive,
                                        IDictionary<int, string> cellPhones)
        {
            var employee = id == 0 ? new Employee() : context.Employees.Single(emp => emp.ID == id);
            employee.UserName = userName;
            employee.RoleID = roleID;
            employee.Name = name;
            employee.Gender = gender;
            employee.Email = email;
            employee.IsAllowLogin = isAllowLogin;
            employee.IsActive = isActive;
            employee.CellPhone1 = cellPhones[1];
            employee.CellPhone2 = cellPhones[2];
            EntityHelper.SetAuditFields(id, employee, CurrentUserName);

            if (id == 0)
            {
                employee.Password = isAllowLogin ? RijndaelHelper.Encrypt("mustchange", cryptographyKey) : String.Empty;
                context.Employees.InsertOnSubmit(employee);
            }

            context.SubmitChanges();
        }



        public bool CanDeleteEmployee(int[] arrayOfID)
        {
            return true;
        }

        public void DeleteEmployee(int[] arrayOfID)
        {
            context.Employees.DeleteAllOnSubmit(
                context.Employees.Where(emp => arrayOfID.Contains(emp.ID))
                );            
            context.SubmitChanges();
        }
    }
}
