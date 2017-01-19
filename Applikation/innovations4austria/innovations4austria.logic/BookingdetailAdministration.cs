using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. to load all bookingdetails
    /// </summary>
    public class BookingdetailAdministration
    {
        /// <summary>
        /// Loading all bookingdetails incl. their bookings and billdetails
        /// </summary>
        /// <returns>List of bookingdetails</returns>
        public static List<bookingdetail> LoadAllBookingdetails()
        {
            Debug.WriteLine("bookingdetailAdministration - LoadAllBookingdetails()");
            Debug.Indent();

            List<bookingdetail> allBookingdetails = new List<bookingdetail>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var bd in context.bookingdetails.Include("billdetails").Include("bookings"))
                    {
                        allBookingdetails.Add(new bookingdetail()
                        {
                            id = bd.id,
                            bookingdate = bd.bookingdate,
                            booking_id = bd.booking_id,
                            price = bd.price,
                            booking = bd.booking,
                            billdetails = bd.billdetails
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Loading all Bookingdetails");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allBookingdetails;
        }
    }
}
