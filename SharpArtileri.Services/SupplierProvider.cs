using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SharpArtileri.Data;
using SharpArtileri.Services.Base;
using SharpArtileri.Services.Helpers;

namespace SharpArtileri.Services
{
    public class SupplierProvider : BaseProvider
    {
        public SupplierProvider(ArtileriDataContext dataContext, IPrincipal principal) : base(dataContext, principal)
        {
        }

        public void AddOrUpdateSupplier(int id,             
            string code,                   
                                        string name,
                                        string address,
                                        string npwp,
                                        string email,
                                        string phone1,
                                        string phone2,
                                        bool taxable)                                        
        {
            var supp = id == 0 ? new Supplier() : context.Suppliers.Single(supplier => supplier.ID == id);
            supp.Code = code;
            supp.Name = name;
            supp.Address = address;
            supp.NPWP = npwp;
            supp.Email = email;
            supp.Phone1 = phone1;
            supp.Phone2 = phone2;
            supp.Taxable = taxable;
            EntityHelper.SetAuditFields(id, supp, CurrentUserName);

            if (id == 0)
                context.Suppliers.InsertOnSubmit(supp);
            context.SubmitChanges();
        }

        public Supplier GetSupplier(int id)
        {
            return context.Suppliers.SingleOrDefault(supp => supp.ID == id);
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return context.Suppliers;
        }

        public IEnumerable<Supplier> GetSuppliers(string name)
        {
            return context.Suppliers.Where(supp => supp.Name.Contains(name));
        }


        public bool CanDeleteSupplier(int[] arrayOfID)
        {
            return true;
        }

        public void DeleteSupplier(int[] arrayOfID)
        {
            context.Suppliers.DeleteAllOnSubmit(context.Suppliers.Where(supp => arrayOfID.Contains(supp.ID)));
            context.SubmitChanges();            
        }
    }
}
