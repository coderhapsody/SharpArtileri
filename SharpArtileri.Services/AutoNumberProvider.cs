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

namespace SharpArtileri.Services
{
    public class AutoNumberProvider : BaseProvider
    {
        private IDictionary<int, string> bulanRomawi;


        public AutoNumberProvider(ArtileriDataContext dataContext, IPrincipal principal) : base(dataContext, principal)
        {
            bulanRomawi = new Dictionary<int, string>();
            bulanRomawi.Add(1, "I");
            bulanRomawi.Add(2, "II");
            bulanRomawi.Add(3, "III");
            bulanRomawi.Add(4, "IV");
            bulanRomawi.Add(5, "V");
            bulanRomawi.Add(6, "VI");
            bulanRomawi.Add(7, "VII");
            bulanRomawi.Add(8, "VIII");
            bulanRomawi.Add(9, "IX");
            bulanRomawi.Add(10, "X");
            bulanRomawi.Add(11, "XI");
            bulanRomawi.Add(12, "XII");

        }

        public RunningNumber CreatePurchaseOrderRunningNumber(int month, int year)
        {
            var runNumber = new RunningNumber();            
            runNumber.Month = month;
            runNumber.Year = year;
            runNumber.FormCode = "PO";
            runNumber.Prefix = runNumber.FormCode;
            runNumber.CurrentNo = 1;
            context.RunningNumbers.InsertOnSubmit(runNumber);
            context.SubmitChanges();

            return runNumber;
        }

        public string GeneratePurchaseOrderRunningNumber(string companyPrefix, DateTime documentDate)
        {
            string documentNo = String.Empty;
            var runNumber =
                 context.RunningNumbers.SingleOrDefault(
                    rn =>
                        rn.Month == documentDate.Month && rn.Year == documentDate.Year &&
                        rn.FormCode == "PO");
            runNumber = runNumber ?? CreatePurchaseOrderRunningNumber(documentDate.Month, documentDate.Year);

            documentNo = String.Format("{0}/{1}/{2}/{3}/{4}",
                runNumber.CurrentNo.ToString("000"),
                runNumber.Prefix,
                companyPrefix,
                bulanRomawi[documentDate.Month],
                documentDate.Year);

            runNumber.CurrentNo++;
            //context.SubmitChanges();

            return documentNo;
        }

        public string GenerateReceivingRunningNumber(string companyPrefix, DateTime documentDate)
        {
            string documentNo = String.Empty;
            var runNumber =
                 context.RunningNumbers.SingleOrDefault(
                    rn =>
                        rn.Month == documentDate.Month && rn.Year == documentDate.Year &&
                        rn.FormCode == "RCV");
            runNumber = runNumber ?? CreateReceivingRunningNumber(documentDate.Month, documentDate.Year);

            documentNo = String.Format("{0}/{1}/{2}/{3}/{4}",
                runNumber.CurrentNo.ToString("000"),
                runNumber.Prefix,
                companyPrefix,
                bulanRomawi[documentDate.Month],
                documentDate.Year);

            runNumber.CurrentNo++;
            //context.SubmitChanges();

            return documentNo;
        }

        public RunningNumber CreateReceivingRunningNumber(int month, int year)
        {
            var runNumber = new RunningNumber();
            runNumber.Month = month;
            runNumber.Year = year;
            runNumber.FormCode = "RCV";
            runNumber.Prefix = runNumber.FormCode;
            runNumber.CurrentNo = 1;
            context.RunningNumbers.InsertOnSubmit(runNumber);
            context.SubmitChanges();

            return runNumber;
        }
    }
}
