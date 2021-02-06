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
using SharpArtileri.Services.ViewModels;

namespace SharpArtileri.Services
{
    public class PurchaseOrderProvider : BaseProvider
    {
        public PurchaseOrderProvider(ArtileriDataContext dataContext, IPrincipal principal) : base(dataContext, principal)
        {
        }

        public void NotApprovedPurchaseOrder(string documentNo, string reason)
        {
            var poHeader = context.PurchaseOrderHeaders.SingleOrDefault(po => po.DocumentNo == documentNo);

            if (poHeader == null) return;

            poHeader.Status = "N"; //Not Approve
            EntityHelper.SetAuditFieldsForUpdate(poHeader, this.CurrentUserName);
            //poHeader.VoidDate = DateTime.Today;
            //poHeader.VoidReason = reason;

            context.SubmitChanges();
        }

        public void VoidPurchaseOrder(string documentNo, string reason)
        {
            var poHeader = context.PurchaseOrderHeaders.SingleOrDefault(po => po.DocumentNo == documentNo);

            if (poHeader == null) return;

            poHeader.Status = "V";
            poHeader.VoidDate = DateTime.Today;
            poHeader.VoidReason = reason;

            context.SubmitChanges();
        }

        public void ApprovedPurchaseOrder(string documentNo, int employeeID , string approveReason)
        {
            var poHeader = context.PurchaseOrderHeaders.SingleOrDefault(po => po.DocumentNo == documentNo);

            if (poHeader == null) return;

            poHeader.Status = "A";
            poHeader.ApprovedByEmployeeID = employeeID;
            poHeader.ApprovedDate = DateTime.Today;
            poHeader.ApproveReason = approveReason;

            context.SubmitChanges();
        }   

        public string AddOrUpdatePurchaseOrder(int id, int branchID, DateTime date, DateTime expectedDate, int supplierID, string notes,
            decimal discValue, string terms,
            List<PurchaseOrderDetailViewModel> detail)
        {            
            var poHeader = id == 0
                ? new PurchaseOrderHeader()
                : context.PurchaseOrderHeaders.Single(po => po.ID == id);

            var autoNo = new AutoNumberProvider(context, principal);
            var emp = new EmployeeProvider(context, principal);
            if(id == 0)
                poHeader.DocumentNo = autoNo.GeneratePurchaseOrderRunningNumber(CurrentCompanyCode, date);
            poHeader.BranchID = branchID;            
            poHeader.Date = date;
            poHeader.ExpectedDate = expectedDate;
            poHeader.SupplierID = supplierID;
            poHeader.Notes = notes;
            poHeader.DiscountValue = discValue;
            poHeader.Terms = terms;
            poHeader.EmployeeID = emp.GetEmployee(CurrentUserName).ID;
            poHeader.Status = "O";
            poHeader.StatusReceiving = "O";
            EntityHelper.SetAuditFields(id, poHeader, CurrentUserName);

            //context.SubmitChanges();

            context.PurchaseOrderDetails.DeleteAllOnSubmit(poHeader.PurchaseOrderDetails);            

            foreach (var detailLine in detail)
            {
                PurchaseOrderDetailViewModel line = detailLine;
                var itemProvider = new ItemProvider(context, principal);
                decimal ratio = itemProvider.GetItemUnitRatio(detailLine.ItemID, detailLine.UnitName);
                
                var poDetail = new PurchaseOrderDetail
                               {
                                   ItemID = detailLine.ItemID,
                                   UnitPrice = detailLine.UnitPrice,
                                   Quantity = detailLine.Quantity,
                                   IsTaxed = detailLine.IsTaxed,
                                   DiscountRate = detailLine.DiscountRate,
                                   UnitName = detailLine.UnitName,
                                   UnitRatio = ratio,
                                   Notes = String.Empty
                               };
                poDetail.PurchaseOrderHeader = poHeader;
                //poHeader.PurchaseOrderDetails.Add(poDetail);
                context.PurchaseOrderDetails.InsertOnSubmit(poDetail);
            }

            if (id == 0)
                context.PurchaseOrderHeaders.InsertOnSubmit(poHeader);
            context.SubmitChanges();

            return poHeader.DocumentNo;
        }

        public PurchaseOrderHeader GetPurchaseOrder(int id)
        {
            return context.PurchaseOrderHeaders.Single(po => po.ID == id);
        }

        public string TranslateStatus(string status)
        {
            switch (status)
            {
                case "O":
                    return "Open";
                case "C":
                    return "Closed";
                case "V":
                    return "Void";
            }

            return String.Empty;            
        }

        public IEnumerable<PurchaseOrderDetailViewModel> GetPurchaseOrderDetail(string documentNo)
        {
            var purchaseOrder = context.PurchaseOrderHeaders.SingleOrDefault(po => po.DocumentNo == documentNo);
            return GetPurchaseOrderDetail(purchaseOrder.ID);
        }

        public IEnumerable<PurchaseOrderDetailViewModel> GetPurchaseOrderDetail(int id)
        {
            var query = from poh in context.PurchaseOrderHeaders
                        join pod in context.PurchaseOrderDetails on poh.ID equals pod.PurchaseOrderID
                        where poh.ID == id
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
                                   Total = ((pod.Quantity * pod.UnitPrice) - 
                                            (pod.Quantity * pod.UnitPrice * pod.DiscountRate / 100)) * (pod.IsTaxed ? 1.1M : 1M)                                         
                               };
            return query;
        }

        public decimal GetTotalDetail(IEnumerable<PurchaseOrderDetail> detail)
        {
            return detail.Sum(
                    d =>
                        (d.Quantity * d.UnitPrice - (d.Quantity * d.UnitPrice * d.DiscountRate / 100)) *
                        (d.IsTaxed ? 1.1M : 1M));
        }

        public bool IsReceive(int id)
        {
            return context.ReceivingHeaders.Where(x => x.PurchaseOrderID == id && (x.Status == "O" || x.Status == "F")).Count() > 0;
        }
    }
}
