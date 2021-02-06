using System;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using SharpArtileri.Services;
using Telerik.Web.UI;

namespace SharpArtileri.Web.Helpers
{
    public static class RadHelper
    {
        public static int[] GetRowIdForDeletion(RadGrid radGrid)
        {
            return
                radGrid.Items.Cast<GridDataItem>()
                        .Where(row => (row.FindControl("chkDelete") as CheckBox).Checked)
                        .Select(row => Convert.ToInt32(row.GetDataKeyValue("ID")))
                        .ToArray();
        }

        public static void SetUpGrid(RadGrid radGrid)
        {
            radGrid.Columns[0].Display = false;
            radGrid.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings[ApplicationSettingKeys.PageSize]);
        }
    }
}