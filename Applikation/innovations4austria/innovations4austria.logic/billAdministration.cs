using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. to load all bills
    /// </summary>
    public class billAdministration
    {
        /// <summary>
        /// Loading all bills including their billdetails
        /// </summary>
        /// <returns>List of all bills incl. billdetails</returns>
        public static List<bill> LoadAllBills()
        {
            Debug.WriteLine("billAdministration - LoadAllBills()");
            Debug.Indent();

            List<bill> allBills = new List<bill>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var bill in context.bills.Include("billdetails"))
                    {
                        allBills.Add(new bill()
                        {
                            billdate = bill.billdate,
                            billdetails = bill.billdetails
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Loading all Bills");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allBills;
        }
    }
}
