using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SharpArtileri.Data;
using SharpArtileri.Services.Base;
using SharpArtileri.Services.Helpers;

namespace SharpArtileri.Services
{
    public class ItemProvider : BaseProvider
    {
        public ItemProvider(ArtileriDataContext dataContext, IPrincipal principal) : base(dataContext, principal)
        {
        }

        public string GenerateItemCode(int supplierID, string type)
        {
            var supplier = context.Suppliers.SingleOrDefault(supp => supp.ID == supplierID);
            if(supplier != null)
            {
                if(String.IsNullOrEmpty(supplier.Code.Trim()))
                    throw new Exception("Supplier code is empty");

                var autoNumber =
                    context.AutoNumbers.SingleOrDefault(autoNo => autoNo.SupplierID == supplierID && autoNo.Type == type);
                if(autoNumber == null)
                {
                    autoNumber = new AutoNumber();
                    autoNumber.SupplierID = supplierID;
                    autoNumber.Type = type;
                    autoNumber.LastNumber = 0;

                    context.AutoNumbers.InsertOnSubmit(autoNumber);
                    context.SubmitChanges();
                }

                string itemCode = String.Empty;

                autoNumber.LastNumber++;

                bool valid = false;
                while (!valid)
                {                    
                    itemCode = String.Format("{0}-{1}{2}", type, supplier.Code, autoNumber.LastNumber.ToString("0000"));
                    valid = !context.Items.Any(item => item.Code == itemCode);
                    if(valid)
                        break;
                    autoNumber.LastNumber++;
                }


                //context.SubmitChanges();

                return itemCode;
            }

            return String.Empty;
        }

        public void AddOrUpdateItem(int id, string code, string name, string type, int supplierID, string unitName1, bool isTaxed, string unitName2 = null, string unitName3 = null, decimal unitRatio2 = 0, decimal unitRatio3 = 0)
        {
            var item = id == 0 ? new Item() : context.Items.Single(supplier => supplier.ID == id);
            if(id == 0 && code.ToUpperInvariant() == "AUTO")
            {
                item.Code = GenerateItemCode(supplierID, type);
            }
            else if(id > 0)
                item.Code = code;
            else if(id == 0 && !String.IsNullOrEmpty(code.Trim()))
                item.Code = code;
            item.Name = name;
            item.Type = type;
            item.SupplierID = supplierID;
            item.UnitName1 = unitName1;
            item.UnitName2 = unitName2;
            item.UnitName3 = unitName3;
            item.UnitRatio2 = unitRatio2;
            item.UnitRatio3 = unitRatio3;
            item.IsTaxed = isTaxed;
            EntityHelper.SetAuditFields(id, item, CurrentUserName);

            if (id == 0)
                context.Items.InsertOnSubmit(item);
            context.SubmitChanges();
        }

        public IEnumerable<string> GetUnitsOfItem(int itemID)
        {
            var units = new List<string>();
            var item = context.Items.SingleOrDefault(it => it.ID == itemID);
            if (item == null) return null;

            if(!String.IsNullOrEmpty(item.UnitName1))
                units.Add(item.UnitName1);
            
            if(!String.IsNullOrEmpty(item.UnitName2))
                units.Add(item.UnitName2);

            if (!String.IsNullOrEmpty(item.UnitName3))
                units.Add(item.UnitName3);

            return units;
        }

        public Item GetItem(int id)
        {
            return context.Items.SingleOrDefault(item => item.ID == id);
        }

        public bool CanDeleteItem(int[] arrayOfID)
        {
            return true;
        }

        public void DeleteItem(int[] arrayOfID)
        {
            context.Items.DeleteAllOnSubmit(context.Items.Where(item => arrayOfID.Contains(item.ID)));
            context.SubmitChanges();
        }

        public decimal GetItemUnitRatio(int itemID, string unitName)
        {
            decimal ratio = 1;
            var item = context.Items.Single(i => i.ID == itemID);

            if (item != null)
            {
                if (unitName == item.UnitName1)
                    ratio = 1;
                else if (unitName == item.UnitName2)
                    ratio = item.UnitRatio2;
                else if (unitName == item.UnitName3)
                    ratio = item.UnitRatio3;
                else
                    ratio = 1;
            }

            return ratio;
        }

        public void AddOrUpdatePriceList(int id, int itemID, int supplierID, string unit, DateTime effectiveDate, decimal price)
        {
            var priceList = id == 0
                ? new ItemPriceList()
                : context.ItemPriceLists.Single(pl => pl.ID == id);
            priceList.ItemID = itemID;
            priceList.SupplierID = supplierID;
            priceList.UnitName = unit;
            priceList.EffectiveDate = effectiveDate;
            priceList.Price = price;
            EntityHelper.SetAuditFields(id, priceList, CurrentUserName);
            if(id == 0)
                context.ItemPriceLists.InsertOnSubmit(priceList);
            context.SubmitChanges();
        }

        public decimal GetDefaultPrice(int supplierID, int itemID, string unitName)
        {
            var price = context.ItemPriceLists.Where(
                it => it.SupplierID == supplierID && it.ItemID == itemID && it.UnitName == unitName)
                               .OrderByDescending(it => it.EffectiveDate)
                               .FirstOrDefault();
            return price == null ? 0 : price.Price;
        }               

        public void DeletePriceList(int id)
        {
            var pl = context.ItemPriceLists.Single(priceList => priceList.ID == id);
            context.ItemPriceLists.DeleteOnSubmit(pl);
            context.SubmitChanges();
        }

        public IEnumerable<Item> GetItems()
        {
            return context.Items;
        }

        public IEnumerable<Item> GetItems(string findExpression)
        {
            return context.Items.Where(item => item.Code.Contains(findExpression) ||
                                               item.Name.Contains(findExpression));
        }

        public ItemPriceList GetPriceList(int priceListID)
        {
            return context.ItemPriceLists.SingleOrDefault(pl => pl.ID == priceListID);
        }

        public void DeletePriceList(int[] arrayOfID)
        {
            Array.ForEach(arrayOfID, DeletePriceList);
        }
    }
}
