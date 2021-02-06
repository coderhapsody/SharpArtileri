using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpArtileri.Services.ReportViewModels
{
    public class ListPurchaseOrderViewModel
    {
        public string DocumentNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime ExpectedDate { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal GrandTotal { get; set; }
        public string Status { get; set; }
    }
}
