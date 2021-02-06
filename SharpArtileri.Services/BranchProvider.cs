using System.Security.Principal;
using SharpArtileri.Data;
using SharpArtileri.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpArtileri.Services.Helpers;

namespace SharpArtileri.Services
{
    public class BranchProvider : BaseProvider
    {
        public BranchProvider(ArtileriDataContext dataContext, IPrincipal principal) : base(dataContext, principal)
        {
        }

        public void AddOrUpdateBranch(int id, string name, string address, string phone, string email)
        {
            var branch = id == 0 ? new Branch() : context.Branches.Single(br => br.ID == id);
            branch.Name = name;
            branch.Address = address;
            branch.Phone = phone;
            branch.Email = email;
            EntityHelper.SetAuditFields(id, branch, principal.Identity.Name);

            if (id == 0)
                context.Branches.InsertOnSubmit(branch);

            context.SubmitChanges();
        }

        public Branch GetBranch(int id)
        {
            return context.Branches.SingleOrDefault(item => item.ID == id);
        }


        public bool CanDeleteItem(int[] arrayOfID)
        {
            return true;
        }

        public void DeleteBranch(int[] arrayOfID)
        {
            context.Branches.DeleteAllOnSubmit(context.Branches.Where(item => arrayOfID.Contains(item.ID)));
            context.SubmitChanges();
        }

        public IEnumerable<Branch> GetBranches()
        {
            return context.Branches;
        }
    }
}
