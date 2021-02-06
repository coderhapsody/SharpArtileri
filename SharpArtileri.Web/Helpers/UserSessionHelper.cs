using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SharpArtileri.Web.Helpers
{
    public static class UserSessionHelper
    {
        //public static readonly string CurrentUserName = 
        

        public static string GetCurrentUserName()
        {
            return HttpContext.Current.User.Identity.Name.Split('|')[0];
        }

        public static string GetCurrentUserName(this Page page)
        {
            return HttpContext.Current.User.Identity.Name.Split('|')[0];
        }

        public static string GetCurrentCompanyCode(this Page page)
        {
            return HttpContext.Current.User.Identity.Name.Split('|')[1];
        }

        public static string GetCurrentCompanyCode()
        {
            return HttpContext.Current.User.Identity.Name.Split('|')[1];
        }
    }
}