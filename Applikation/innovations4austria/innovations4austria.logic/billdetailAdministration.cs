using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. Load all Billdetails,...
    /// </summary>
    public class billdetailAdministration
    {
        /// <summary>
        /// Loading all billdetails
        /// </summary>
        /// <returns>List of all billdetails</returns>
        public static List<billdetail> LoadAllBilldetails()
        {
            Debug.WriteLine("billAdministration - LoadAllBilldetails()");
            Debug.Indent();

            List<billdetail> allBilldetails = new List<billdetail>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var billdetail in context.billdetails.Include("bills").Include("bookingdetails"))
                    {
                        allBilldetails.Add(new billdetail()
                        {
                            bill_id = billdetail.bill_id,
                            bill = billdetail.bill,
                            bookingdetail = billdetail.bookingdetail,
                            bookingdetail_id = billdetail.bookingdetail_id,
                            id = billdetail.id
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Loading all Billdetails");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allBilldetails;
        }
    }
}
