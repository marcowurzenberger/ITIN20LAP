using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methodes e.g. Load all Bookings, ...
    /// </summary>
    public class BookingAdministration
    {
        /// <summary>
        /// Method to load all bookings
        /// </summary>
        /// <returns>List of all bookings</returns>
        public static List<booking> LoadAllBookings()
        {
            Debug.WriteLine("bookingAdministration - LoadAllBookings()");
            Debug.Indent();

            List<booking> allBookings = new List<booking>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var b in context.bookings.Include("bookingdetails").Include("rooms").Include("companies"))
                    {
                        allBookings.Add(new booking()
                        {
                            id = b.id,
                            bookingdetails = b.bookingdetails,
                            company = b.company,
                            company_id = b.company_id,
                            room = b.room,
                            room_id = b.room_id
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Loading all bookings");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allBookings;
        }
    }
}
