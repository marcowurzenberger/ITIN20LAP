using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    public class RoomAdministration
    {
        public static List<room> LoadAllRooms()
        {
            Debug.WriteLine("roomAdministration - LoadAllRooms");
            Debug.Indent();

            List<room> allRooms = new List<room>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var r in context.rooms.Include("bookings").Include("facilities").Include("roomfurnishments"))
                    {
                        allRooms.Add(new room()
                        {
                            bookings = r.bookings,
                            description = r.description,
                            facility = r.facility,
                            facility_id = r.facility_id,
                            id = r.id,
                            price = r.price,
                            roomfurnishments = r.roomfurnishments
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at loading all rooms");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allRooms;
        }
    }
}
