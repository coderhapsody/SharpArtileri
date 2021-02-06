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
using System.Collections;


namespace SharpArtileri.Services
{
    public class ReceivingProvider : BaseProvider
    {
        public ReceivingProvider(ArtileriDataContext dataContext, IPrincipal principal)
            : base(dataContext, principal)
        {

        }

        public void VoidReceiving(int RowID, string reason)
        {
            var receiveHeader = context.ReceivingHeaders.SingleOrDefault(x => x.ID == RowID);

            if (receiveHeader == null) return;

            receiveHeader.Status = "V";
            receiveHeader.VoidDate = DateTime.Today;
            receiveHeader.VoidReason = reason;

            var order = receiveHeader.PurchaseOrderHeader;
            if (
                context.ReceivingHeaders.Any(
                    rcv => rcv.PurchaseOrderID == order.ID && rcv.ID != RowID && !rcv.VoidDate.HasValue))
            {
                order.StatusReceiving = "P";
                order.IsFullReceived = false;
            }
            else
            {
                order.StatusReceiving = "O";
                order.IsFullReceived = false;
            }

            context.SubmitChanges();
        }

        public string AddOrUpdateReceiving(int id, int poID, DateTime date, string goodIssueNo,
            string freightInfo, string notes, List<ReceivingDetailViewModel> detail)
        {

            List<ReceivingDetail> receivingDetails =
                context.ReceivingHeaders
                       .Where(rcv => rcv.PurchaseOrderID == poID && rcv.ID != id && !rcv.VoidDate.HasValue)
                       .SelectMany(x => x.ReceivingDetails)
                       .ToList();
            var order = context.PurchaseOrderHeaders.Single(po => po.ID == poID && !po.VoidDate.HasValue);
            List<PurchaseOrderDetail> orderDetails = order.PurchaseOrderDetails.ToList();

            var receiveHeader = id == 0
              ? new ReceivingHeader()
              : context.ReceivingHeaders.Single(rh => rh.ID == id);

            var autoNo = new AutoNumberProvider(context, principal);
            var emp = new EmployeeProvider(context, principal);
            if (id == 0)
                receiveHeader.DocumentNo = autoNo.GenerateReceivingRunningNumber(CurrentCompanyCode, date);

            receiveHeader.PurchaseOrderID = poID;
            receiveHeader.Date = date;
            receiveHeader.GoodIssueNo = goodIssueNo;
            receiveHeader.FreightInfo = freightInfo;
            receiveHeader.Notes = notes;
            receiveHeader.Status = "O";
            EntityHelper.SetAuditFields(id, receiveHeader, CurrentUserName);                     


            context.ReceivingDetails.DeleteAllOnSubmit(receiveHeader.ReceivingDetails);

            bool isFullReceived = true;
            foreach (var newReceivingDetail in detail)
            {
                var totalQtyPO = orderDetails.Where(po => po.ItemID == newReceivingDetail.ItemID).Sum(po => po.Quantity);
                var totalQtyRcv = receivingDetails.Where(rcv => rcv.ItemID == newReceivingDetail.ItemID).Sum(rcv => rcv.QtyReceived);
                if (totalQtyPO - totalQtyRcv - newReceivingDetail.QtyReceived < 0)
                    throw new Exception(String.Format("Qty for item {0} exceeds Qty in Purchase Order. Item:{1}, QtyPO:{2}, QtyRcv:{3}", newReceivingDetail.ItemCode, newReceivingDetail.ItemCode, Convert.ToInt32(totalQtyPO), totalQtyRcv + newReceivingDetail.QtyReceived));                
                else if (totalQtyPO - totalQtyRcv - newReceivingDetail.QtyReceived > 0)
                    isFullReceived = false;

                var receiveDetail = new ReceivingDetail
                {
                    ItemID = newReceivingDetail.ItemID,
                    UnitName = newReceivingDetail.UnitName,
                    QtyReceived = newReceivingDetail.QtyReceived,
                    Notes = newReceivingDetail.Notes,
                    ReceivingHeader = receiveHeader
                };
                context.ReceivingDetails.InsertOnSubmit(receiveDetail);
            }

            if (id == 0)
                context.ReceivingHeaders.InsertOnSubmit(receiveHeader);
            
            order.IsFullReceived = isFullReceived;
            order.StatusReceiving = isFullReceived ? "F" : "P";

            
            //var receiveDetails = new List<ReceivingDetailViewModel>();
            //foreach (var item in query)
            //    receiveDetails.Add(new ReceivingDetailViewModel { ItemID = item.ItemID, QtyReceived = item.QtyReceived });

            //foreach (var lineDetail in detail)
            //    receiveDetails.Add(lineDetail);


            //var sumReceiveUnit = detail.GroupBy(x => x.ItemID).Select(y => new { item = y.Key, qty = receiveDetails.Where(x => x.ItemID == y.Key).Sum(x => x.QtyReceived) });


            //var sumPOUnit = context.PurchaseOrderHeaders.Where(x => x.ID == poID).SelectMany(x => x.PurchaseOrderDetails).
            //                                                GroupBy(x => x.ItemID).Select(z => new { item = z.Key, qty = z.Sum(p => p.Quantity) });

            //bool isAllqtySame = true;
            //bool isItemCountSame = true;

            //foreach (var itemReceive in sumReceiveUnit)
            //{
            //    var itemPO = sumPOUnit.SingleOrDefault(x => x.item == itemReceive.item);
            //    if (itemPO.qty != itemReceive.qty)
            //        isAllqtySame = false;
            //}

            //if (sumReceiveUnit.Count() != sumPOUnit.Count())
            //    isItemCountSame = false;

            //PurchaseOrderHeader purchaseOrderHeader = context.PurchaseOrderHeaders.SingleOrDefault(x => x.ID == poID);
            //if (purchaseOrderHeader != null)
            //{
            //    if (isAllqtySame && isItemCountSame)
            //    {
            //        var allReceiving = context.ReceivingHeaders.Where(x => x.PurchaseOrderID == poID && x.Status == "O");
            //        foreach (var item in allReceiving)
            //        {
            //            ReceivingHeader headerReceive = context.ReceivingHeaders.SingleOrDefault(x => x.ID == item.ID);
            //            headerReceive.Status = "F";
            //            EntityHelper.SetAuditFieldsForUpdate(headerReceive, CurrentUserName);
            //        }
            //        purchaseOrderHeader.IsFullReceived = true;
            //        purchaseOrderHeader.StatusReceiving = "F";
            //        receiveHeader.Status = "F";
            //        EntityHelper.SetAuditFieldsForUpdate(purchaseOrderHeader, CurrentUserName);
            //    }
            //    else
            //    {
            //        purchaseOrderHeader.IsFullReceived = false;
            //        purchaseOrderHeader.StatusReceiving = "P";
            //    }
            //}
            //#endregion

            context.SubmitChanges();

            return receiveHeader.DocumentNo;
        }

        public ReceivingHeader GetReceiving(int id)
        {
            return context.ReceivingHeaders.Single(po => po.ID == id);
        }

        public IEnumerable<ReceivingDetailViewModel> GetreceiveDetail(int RowID)
        {
            var query = (from rh in context.ReceivingHeaders
                         join rd in context.ReceivingDetails on rh.ID equals rd.ReceivingID
                         where rh.ID == RowID
                         select new ReceivingDetailViewModel
                         {
                             ID = rd.ID,
                             ItemID = rd.ItemID,
                             ItemCode = rd.Item.Code,
                             ItemName = rd.Item.Name,
                             QtyReceived = rd.QtyReceived,
                             PurchaseOrderID = rh.PurchaseOrderID,
                             UnitName = rd.UnitName,
                             Notes = rd.Notes
                         }).ToList();


            foreach (ReceivingDetailViewModel item in query)
            {
                item.QtyPO = context.PurchaseOrderDetails.Where(x => x.PurchaseOrderID == item.PurchaseOrderID && x.ItemID == item.ItemID).Sum(x => x.Quantity);
            }

            return query;
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
                case "F":
                    return "Full Received";
            }

            return String.Empty;
        }



        public IEnumerable<PurchaseOrderHeader> GetPurchaseOrderForReceiving()
        {
            return context.PurchaseOrderHeaders.Where(po => po.Status == "O" && !po.IsFullReceived);
        }

        public bool CheckItemIsFullReceive(int RowID, int poID, List<ReceivingDetailViewModel> detail)
        {

            var query = context.ReceivingHeaders.Where(x => x.PurchaseOrderID == poID && x.ID != RowID).
                                                            SelectMany(x => x.ReceivingDetails);

            List<ReceivingDetailViewModel> receiveDetails = new List<ReceivingDetailViewModel>();
            foreach (var item in query)
                receiveDetails.Add(new ReceivingDetailViewModel { ItemID = item.ItemID, QtyReceived = item.QtyReceived });

            foreach (var lineDetail in detail)
                receiveDetails.Add(lineDetail);

            var sumReceiveUnit = detail.GroupBy(x => x.ItemID).Select(y => new { item = y.Key, Qty = receiveDetails.Where(x => x.ItemID == y.Key).Sum(x => x.QtyReceived) });

            var sumPOUnit = context.PurchaseOrderHeaders.Where(x => x.ID == poID).SelectMany(x => x.PurchaseOrderDetails).
                                                            GroupBy(x => x.ItemID).Select(z => new { item = z.Key, Qty = z.Sum(p => p.Quantity) });

            bool isNotValid = false;

            if (sumReceiveUnit.Count() > sumPOUnit.Count())
            {
                isNotValid = true;
                return isNotValid;
            }

            foreach (var itemReceive in sumReceiveUnit)
            {
                var isItemExists = sumPOUnit.Where(x => x.item == itemReceive.item);
                if (isItemExists.Count() == 1)
                {
                    var itemPO = isItemExists.SingleOrDefault();
                    if (itemPO.Qty < itemReceive.Qty)
                    {
                        isNotValid = true;
                        return isNotValid;
                    }
                }
                else
                {
                    isNotValid = true;
                    return isNotValid;
                }
            }

            return isNotValid;
        }



        public List<ReceivingDetailViewModel> GetUnReceivingPO(int poID , int receivingID)
        {
            var totalCurrentReceiving = context.ReceivingHeaders.Where(x => x.PurchaseOrderID == poID && x.Status == "O" && x.ID != receivingID).
                                               SelectMany(x => x.ReceivingDetails).GroupBy(x => x.ItemID).Select(y => new { item = y.Key, qty = y.Sum(p => p.QtyReceived) });

            var poItems = context.PurchaseOrderHeaders.Where(x => x.ID == poID).SelectMany(x => x.PurchaseOrderDetails).
                                                           GroupBy(x => x.ItemID).Select(z => new { item = z.Key, qty = z.Sum(p => p.Quantity) });

            List<ReceivingDetailViewModel> fullReceivings = new List<ReceivingDetailViewModel>();

            foreach (var item in poItems)
            {
                var masterItem = context.Items.SingleOrDefault(x => x.ID == item.item);
                var isItemExists = totalCurrentReceiving.Where(x => x.item == item.item);
                if (isItemExists.Count() == 1)
                {
                    var itemPO = isItemExists.SingleOrDefault();
                    decimal qty = item.qty  - itemPO.qty;
                    //if(qty>0)
                    fullReceivings.Add(new ReceivingDetailViewModel { ItemID = item.item, ItemCode = masterItem.Code, 
                        ItemName = masterItem.Name , UnitName = masterItem.UnitName1 , QtyPO = item.qty , QtyReceived = qty , ReceivingID = receivingID });
                }
                else
                {
                    fullReceivings.Add(new ReceivingDetailViewModel { ItemID = item.item, ItemCode = masterItem.Code, 
                        ItemName = masterItem.Name , UnitName = masterItem.UnitName1 , QtyPO =item.qty , QtyReceived = item.qty , ReceivingID = receivingID});
                }
            }

            return fullReceivings;
        }
    }
}
