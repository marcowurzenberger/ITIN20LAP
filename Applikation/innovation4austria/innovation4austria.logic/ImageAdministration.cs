using innovation4austria.dataAccess;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class ImageAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all images from Database
        /// </summary>
        /// <returns>List of images</returns>
        public static List<image> GetAllImages()
        {
            log.Info("ImageAdministration - GetAllImages");

            List<image> images = new List<image>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    images = context.images.Include("furnishment").ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all images", ex);
            }

            return images;
        }

        /// <summary>
        /// Get all images from Database by furnishment Id
        /// </summary>
        /// <param name="furnishmentId">id of furnishment</param>
        /// <returns>List of images</returns>
        public static List<image> GetAllImagesByFurnishmentId(int furnishmentId)
        {
            log.Info("ImageAdministration - GetAllImagesByFurnishmentId(int furnishmentId)");

            List<image> images = new List<image>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    images = context.images.Where(x => x.furnishment_id == furnishmentId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all images by furnishment Id", ex);
            }

            return images;
        }
    }
}
