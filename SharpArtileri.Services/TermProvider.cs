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
    public class TermProvider : BaseProvider
    {
        public TermProvider(ArtileriDataContext dataContext, IPrincipal principal)
            : base(dataContext, principal)
        {
        }

        public Term GetTerm(int id)
        {
            var term = context.Terms.SingleOrDefault(r => r.ID == id);
            return term;
        }

        public Term GetTerm(string name)
        {
            return context.Terms.SingleOrDefault(term => term.Name == name);
        }

        public IEnumerable<Term> GetTerms(int rowID = 0)
        {
            return rowID == 0 ? context.Terms.Where(term => term.IsActive) : context.Terms;
        }

        public IEnumerable<Term> GetTerms(string name)
        {
            return context.Terms.Where(term => term.Name.Contains(name));
        }

        public void DeleteTerms(int[] arrayOfID)
        {
            context.Terms.DeleteAllOnSubmit(context.Terms.Where(id => arrayOfID.Contains(id.ID)));
            context.SubmitChanges();
        }

        public void AddOrUpdateTerm(int id, string name, bool isActive)
        {
            var term = id == 0 ? new Term() : context.Terms.SingleOrDefault(r => r.ID == id);
            if (term != null)
            {
                term.Name = name;
                term.IsActive = isActive;
                EntityHelper.SetAuditFields(id, term, CurrentUserName);
            }

            if (id == 0)
                context.Terms.InsertOnSubmit(term);

            context.SubmitChanges();
        }

        public bool CanDeleteTerms(int[] arrayOfID)
        {
            // TODO: add logic to validate whether a role can be safely deleted or not
            return true;
        }
    }
}
