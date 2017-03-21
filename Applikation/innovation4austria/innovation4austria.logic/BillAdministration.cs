using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;
using System.Transactions;

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

                    var companyId = context.companies.Where(x => x.name == companyName).Select(x => x.id).FirstOrDefault();

                    var bookings = context.bookings.Where(x => x.company_id == companyId).ToList();

                    foreach (var booking in bookings)
                    {
                        foreach (var bookingdetail in booking.bookingdetails)
                        {
                            if (bookingdetail.bill != null)
                            {
                                var doppelt = filteredList.Where(x => x.id == bookingdetail.bill.id).FirstOrDefault();

                                if (doppelt == null)
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

        /// <summary>
        /// Get bill with specific id
        /// </summary>
        /// <param name="id">id of bill</param>
        /// <returns>whole Bill</returns>
        public static bill GetBillById(int id)
        {
            log.Info("BillAdministration - GetBillById(id)");

            bill foundbill = new bill();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var b in context.bills.Include("bookingdetails"))
                    {
                        if (b.id == id)
                        {
                            foundbill = b;
                        }
                    }
                }

                return foundbill;
            }
            catch (Exception ex)
            {
                log.Error("Error getting Bill by id", ex);
            }

            return foundbill;
        }

        /// <summary>
        /// Generates bills foreach company and update bill_id in bookingdetails
        /// </summary>
        /// <returns>true if all ok, false if anything went wrong</returns>
        public static bool GenerateBills()
        {
            log.Info("BillAdministration - GenerateBills()");

            DateTime end = DateTime.Now.Subtract(new TimeSpan(DateTime.Now.Day, 0, 0, 0));
            DateTime start = new DateTime(end.Year, end.Month, 1);

            DateTime billDate = new DateTime(start.Year, start.Month, start.Day);
            billDate = billDate.AddMonths(1);

            List<bookingdetail> bdList = new List<bookingdetail>();
            bdList = BookingdetailAdministration.GetAllBookingdetails(start, end);
            bdList = bdList.Where(x => x.bill_id == null).ToList();

            bool success = false;

            try
            {
                using (var transaction = new TransactionScope())
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        //wenn keine Buchungsdetails vorhanden sind,
                        // die noch nicht abgerechnet wurden, gib false zurück
                        if (bdList == null || bdList.Count == 0)
                        {
                            return success;
                        }

                        // wenn Rechnungen vom vorigen Monats existieren, dann gib false zurück
                        if (context.bills.Any(x => x.billdate.Month == billDate.Month && x.billdate.Year == billDate.Year))
                        {
                            return success;
                        }
                        else
                        {
                            foreach (var comp in context.companies.Include("bookings").Include("discounts"))
                            {
                                if (bdList.Any(x => x.booking.company_id == comp.id))
                                {

                                    bill newBill = new bill();
                                    newBill.billdate = billDate;
                                    context.bills.Add(newBill);
                                    context.SaveChanges();


                                    List<bookingdetail> bookingdetails = context.bookingdetails.Where(x => x.bill_id == null && x.booking.company_id == comp.id && x.booking_date >= start && x.booking_date <= end).ToList();


                                    foreach (var bd in bookingdetails)
                                    {
                                        if (bd.bill_id == null)
                                        {
                                            bd.bill_id = newBill.id;
                                        }
                                    }
                                }
                            }
                        }
                        success = true;
                        context.SaveChanges();
                    }

                    transaction.Complete(); 
                }
            }
            catch (Exception ex)
            {
                log.Error("Error generating Bills", ex);
            }

            return success;
        }
    }
}
