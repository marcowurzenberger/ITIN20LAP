using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. to load all roomfurnishments
    /// </summary>
    public class RoomfurnishmentAdministration
    {
        /// <summary>
        /// Loading all roomfurnishments incl. their rooms and furnishments
        /// </summary>
        /// <returns>List of all roomfurnishments</returns>
        public static List<roomfurnishment> LoadAllRoomfurnishments()
        {
            Debug.WriteLine("roomfurnishmentAdministration - LoadAllRoomfurnishments");
            Debug.Indent();

            List<roomfurnishment> allRoomfurnishments = new List<roomfurnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var rf in context.roomfurnishments.Include("furnishments").Include("rooms"))
                    {
                        allRoomfurnishments.Add(new roomfurnishment()
                        {
                            furnishment = rf.furnishment,
                            furnishment_id = rf.furnishment_id,
                            id = rf.id,
                            room = rf.room,
                            room_id = rf.room_id
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at loading all roomfurnishments");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allRoomfurnishments;
        }
    }
}
