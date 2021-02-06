using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SharpArtileri.Data;

namespace SharpArtileri.Services.Base
{
    public class BaseProvider : IDisposable
    {
        protected readonly ArtileriDataContext context;
        protected readonly IPrincipal principal;

        protected readonly string cryptographyKey = ConfigurationManager.AppSettings[ApplicationSettingKeys.CryptographyKey];

        public string CurrentUserName
        {
            get
            {
                return principal != null ? principal.Identity.Name.Split('|')[0] : String.Empty;
            }
        }

        public string CurrentCompanyCode
        {
            get
            {
                return principal != null ? principal.Identity.Name.Split('|')[1] : String.Empty;
            }
        }

        public BaseProvider(ArtileriDataContext dataContext, IPrincipal principal)
        {
            this.context = dataContext;
            this.principal = principal;
        }

        public virtual void Dispose()
        {
            if(context != null)
                context.Dispose();
        }
    }
}
