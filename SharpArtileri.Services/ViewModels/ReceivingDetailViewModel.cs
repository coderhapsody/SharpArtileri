using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpArtileri.Services.ViewModels
{
    [Serializable]
    public class ReceivingDetailViewModel
    {
        public int ID { get; set; }
        public int ReceivingID { get; set; }
        public int PurchaseOrderID { get; set; }
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public decimal QtyReceived { get; set; }
        public decimal QtyPO { get; set; }
        public string Notes { get; set; }

    }
}
