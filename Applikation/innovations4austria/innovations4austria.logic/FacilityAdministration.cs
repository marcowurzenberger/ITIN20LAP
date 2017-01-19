using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. to load all facilities
    /// </summary>
    public class FacilityAdministration
    {
        public static List<facility> LoadAllFacilities()
        {
            Debug.WriteLine("facilityAdministration - LoadAllFacilities()");
            Debug.Indent();

            List<facility> allFacilities = new List<facility>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var f in context.facilities.Include("rooms"))
                    {
                        allFacilities.Add(new facility()
                        {
                            city = f.city,
                            id = f.id,
                            name = f.name,
                            number = f.number,
                            rooms = f.rooms,
                            street = f.street,
                            zip = f.zip
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at loading all facilities");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allFacilities;
        }
    }
}
