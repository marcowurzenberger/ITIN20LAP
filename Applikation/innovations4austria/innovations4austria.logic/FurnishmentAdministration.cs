using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. to load all furnishments
    /// </summary>
    public class FurnishmentAdministration
    {
        /// <summary>
        /// Load all furnishments incl. their roomfurnishments
        /// </summary>
        /// <returns>List of all furnishments</returns>
        public static List<furnishment> LoadAllFurnishments()
        {
            Debug.WriteLine("furnishmentAdministration - LoadAllFurnishments()");
            Debug.Indent();

            List<furnishment> allFurnishments = new List<furnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var f in context.furnishments.Include("roomfurnishments"))
                    {
                        allFurnishments.Add(new furnishment()
                        {
                            id = f.id,
                            description = f.description,
                            roomfurnishments = f.roomfurnishments
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at loading all furnishments");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allFurnishments;
        }
    }
}
