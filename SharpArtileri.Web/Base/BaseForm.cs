using System;
using System.IO;
using System.Web;
using SharpArtileri.Data;

namespace SharpArtileri.Web.Base
{
    public abstract class BaseForm : System.Web.UI.Page
    {
        public int RowID { get { return Convert.ToInt32(ViewState["_ID"]); } set { ViewState["_ID"] = value; } }
        //public string ConnectionString { get; set; }

        public string CurrentPageName
        {
            get
            {
                return Path.GetFileName(HttpContext.Current.Request.PhysicalPath);
            }
        }

        //protected void SetPrivilege(SecurityProvider securityService, WebControl addNew, WebControl delete, WebControl save)
        //{
        //    var privilege = securityService.GetPrivilege(CurrentPageName);
        //    if (privilege != null)
        //    {
        //        addNew.Enabled = privilege.AllowAddNew.GetValueOrDefault(false);
        //        save.Enabled = RowID == 0
        //            ? privilege.AllowAddNew.GetValueOrDefault(false)
        //            : privilege.AllowUpdate.GetValueOrDefault(false);
        //        delete.Enabled = privilege.AllowDelete.GetValueOrDefault(false);
        //    }
        //    else
        //    {
        //        addNew.Enabled = save.Enabled = delete.Enabled = false;
        //    }
        //}

        protected virtual void ReloadCurrentPage()
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path), true);
        }

        
    }
}