using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpArtileri.Data;
using SharpArtileri.Services.ReportViewModels;
using SharpArtileri.Services.ViewModels;

namespace SharpArtileri.Services
{
    public class ReportProvider
    {
        public string ConnectionString { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string POApproverName { get; set; }

        public IEnumerable<ListPurchaseOrderViewModel> ListPurchaseOrder(DateTime fromDate, DateTime toDate)
        {
            IEnumerable<PurchaseOrderDetailViewModel> result = null;
            using (var context = new ArtileriDataContext(ConnectionString))
            {
                var query = from poh in context.PurchaseOrderHeaders
                            where poh.Date >= fromDate && poh.Date <= toDate
                            select poh;
                foreach (var poHeader in query)
                {
                    var model = new ListPurchaseOrderViewModel();
                    model.DocumentNo = poHeader.DocumentNo;
                    model.SupplierName = poHeader.Supplier.Name;
                    model.SupplierAddress = poHeader.Supplier.Address;
                    model.DocumentDate = poHeader.Date;
                    model.ExpectedDate = poHeader.ExpectedDate.GetValueOrDefault();

                    var detail = GetPurchaseOrderDetail(model.DocumentNo);
                    model.DiscountValue = poHeader.DiscountValue;
                    model.SubTotal = detail.Sum(pod => pod.Total);
                    model.GrandTotal = model.SubTotal - model.DiscountValue;

                    switch (poHeader.Status)
                    {
                        case "A":
                            model.Status = "Approved";
                            break;
                        case "O":
                            model.Status = "Open";
                            break;
                        case "C":
                            model.Status = "Closed";
                            break;
                        case "V":
                            model.Status = "Void";
                            break;                            
                        case "N":
                            model.Status = "Not Approved";                                           
                            break;
                    }

                    yield return model;
                }
            }
        }

        public IEnumerable<PurchaseOrderDetailViewModel> GetPurchaseOrderDetail(string documentNo)
        {
            IEnumerable<PurchaseOrderDetailViewModel> result = null;
            using (var context = new ArtileriDataContext(ConnectionString))
            {
                var query = from poh in context.PurchaseOrderHeaders
                            join pod in context.PurchaseOrderDetails on poh.ID equals pod.PurchaseOrderID
                            where poh.DocumentNo == documentNo
                            select new PurchaseOrderDetailViewModel
                                   {
                                       ID = pod.ID,
                                       DiscountRate = pod.DiscountRate,
                                       DiscountValue = pod.DiscountValue,
                                       IsTaxed = pod.IsTaxed,
                                       ItemCode = pod.Item.Code,
                                       ItemName = pod.Item.Name,
                                       UnitName = pod.UnitName,
                                       ItemID = pod.ItemID,
                                       Quantity = Convert.ToInt32(pod.Quantity),
                                       UnitPrice = pod.UnitPrice,
                                       TaxValue = pod.IsTaxed
                                           ? ((pod.Quantity * pod.UnitPrice) -
                                              (pod.Quantity * pod.UnitPrice * pod.DiscountRate / 100)) *
                                             0.1M
                                           : 0M,
                                       Total = ((pod.Quantity * pod.UnitPrice) -
                                                (pod.Quantity * pod.UnitPrice * pod.DiscountRate / 100)) *
                                               (pod.IsTaxed ? 1.1M : 1M)
                                   };
                result = query.ToList();
            }
            return result;
        }

        public IEnumerable<PurchaseOrderHeaderViewModel> GetPurchaseOrderHeader(string documentNo)
        {
            IEnumerable<PurchaseOrderHeaderViewModel> result = null;
            using (var context = new ArtileriDataContext(ConnectionString))
            {
                var poHeader = context.PurchaseOrderHeaders.SingleOrDefault(po => po.DocumentNo == documentNo);
                if (poHeader == null) return null;

                var header = new PurchaseOrderHeaderViewModel();
                header.DocumentNo = poHeader.DocumentNo;
                header.DocumentDate = poHeader.Date;
                header.ExpectedDate = poHeader.ExpectedDate.GetValueOrDefault(DateTime.Today);
                header.SupplierName = poHeader.Supplier.Name;
                header.SupplierNPWP = poHeader.Supplier.NPWP;
                header.SupplierAddress = poHeader.Supplier.Address;
                header.EmployeeName = context.Employees.Single(emp => emp.ID == poHeader.EmployeeID).Name;
                header.CompanyName = CompanyName;
                header.CompanyAddress1 = CompanyAddress1;
                header.CompanyAddress2 = CompanyAddress2;
                header.Terms = poHeader.Terms;
                header.DiscountValue = poHeader.DiscountValue;
                header.Notes = poHeader.Notes;
                header.ApproverName = POApproverName;
                header.SupplierTaxable = poHeader.Supplier.Taxable;
                header.SubTotal = poHeader.PurchaseOrderDetails.Sum(pod => ((pod.Quantity * pod.UnitPrice) -
                                                                            (pod.Quantity * pod.UnitPrice *
                                                                             pod.DiscountRate / 100)) *
                                                                           (pod.IsTaxed ? 1.1M : 1M));
                header.GrandTotal = header.SubTotal - header.DiscountValue;
                header.Terbilang = new Conversion().ConvertMoneyToWords(header.GrandTotal);
                header.ApprovedDate = poHeader.ApprovedDate;
                if (poHeader.Status == "O")
                    header.Status = "Open";
                else if (poHeader.Status == "A")
                    header.Status = "Approved";
                else if (poHeader.Status == "N")
                    header.Status = "Not Approved";
                else if (poHeader.Status == "C")
                    header.Status = "Closed";
                else if (poHeader.Status == "V")
                    header.Status = "Void";

                result = new List<PurchaseOrderHeaderViewModel>() { header };
            }

            return result;
        }


        public IEnumerable<ReceivingHeaderViewModel> GetReceiveHeader(int id)
        {
            IEnumerable<ReceivingHeaderViewModel> result = null;
            using (var context = new ArtileriDataContext(ConnectionString))
            {
                var receiveHeader = context.ReceivingHeaders.SingleOrDefault(rh => rh.ID == id);
                if (receiveHeader == null) return null;

                //var header = new PurchaseOrderHeaderViewModel();
                //header.PurchaseOrderID = receiveHeader.PurchaseOrderID;
                //header.Date = receiveHeader.Date;
                //header.GoodIssueNo = receiveHeader.GoodIssueNo;
                //header.FreightInfo = freightInfo;
                //header.Notes = notes;
                //header.Status = "O";
              
                //header.Terbilang = new Conversion().ConvertMoneyToWords(header.GrandTotal);

                //result = new List<PurchaseOrderHeaderViewModel>() { header };
            }

            return result;
        }
    }
}
