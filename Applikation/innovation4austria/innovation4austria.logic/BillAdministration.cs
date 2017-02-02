using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class BillAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all bills from database
        /// </summary>
        /// <returns>List of all bills</returns>
        public static List<bill> GetAllBills()
        {
            log.Info("GetAllBills()");

            List<bill> allBills = new List<bill>();

            List<bookingdetail> allBookingdetails = new List<bookingdetail>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allBookingdetails = BookingdetailAdministration.GetAllBookingdetails();

                    foreach (var b in context.bills)
                    {
                        allBills.Add(new bill()
                        {
                            billdate = b.billdate,
                            id = b.id,
                            bookingdetails = allBookingdetails.Where(x => x.bill_id == b.id).ToList()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bills", ex);
            }

            return allBills;
        }

        /// <summary>
        /// Get all bills from database where company matches
        /// </summary>
        /// <param name="companyName">name of company</param>
        /// <returns>List of all bills by company</returns>
        public static List<bill> GetAllBillsByCompany(string companyName)
        {
            log.Info("GetAllBillsByCompany(string company)");

            List<bill> allBills = new List<bill>();
            List<bill> filteredList = new List<bill>();

            List<company> allCompanies = CompanyAdministration.GetAllCompanies();
            
            try
            {
                using (var context = new innovations4austriaEntities())
                {

                    var companyId = context.companies.Where(x => x.name == companyName).Select(x=>x.id).FirstOrDefault();

                    var bookings = context.bookings.Where(x => x.company_id == companyId).ToList();

                    foreach (var booking in bookings)
                    {
                        foreach (var bookingdetail in booking.bookingdetails)
                        {
                            if (bookingdetail.bill != null)
                            {
                                var doppelt = filteredList.Where(x => x.id == bookingdetail.bill.id).FirstOrDefault();

                                if(doppelt == null)
                                    filteredList.Add(new bill() { id = bookingdetail.bill.id, billdate = bookingdetail.bill.billdate });
                            }
                        }                        
                    }


                    //filteredList = filteredList.Distinct().ToList();
                    

                    //object v = billIds[0]?.GetType().GetProperty("Date")?.GetValue(billIds[0], null);

                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bills by company", ex);
            }

            return filteredList;
        }
    }
}
