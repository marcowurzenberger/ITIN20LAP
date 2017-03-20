using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;
using System.Net.Mail;
using System.Net;

namespace innovation4austria.logic
{
    public class MailDelivery
    {
        private const string FROM_MAIL = "marco.wurzenberger@qualifizierung.or.at";
        private const string USERNAME = "marco.wurzenberger@qualifizierung.or.at";
        private const string PASSWORD = "AndreA12!!";
        private const string CLIENT = "srv08.itccn.loc";

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool SendBookingConfirmation(string email, booking b)
        {
            log.Info("MailDelivery - SendBookingConfirmation(string email, booking b)");

            bool success = false;

            List<bookingdetail> bDetails = new List<bookingdetail>();

            try
            {
                bDetails = BookingdetailAdministration.GetAllBookingdetailsByBookingId(b.id);

                SmtpClient client = new SmtpClient(CLIENT);
                client.Credentials = new NetworkCredential(USERNAME, PASSWORD);

                MailMessage mail = new MailMessage(FROM_MAIL, email);
                mail.Subject = "Buchungsbestätigung Nr." + b.id;

                portaluser user = new portaluser();
                user = PortaluserAdministration.GetUserByEmail(email);

                mail.IsBodyHtml = false;
                mail.Body = "Sehr geehrte/r Frau/Herr " + user.lastname + 
                    "!\n\n\nNachfolgend die Daten Ihrer Buchung, die Sie getätigt haben:\n\nBuchungsnr.:\t\t\t\t" + b.id +
                    "\nBuchungszeitraum:\t\t" + b.bookingdetails.Select(x => x.booking_date).FirstOrDefault().ToShortDateString() +
                    " - " + b.bookingdetails.Select(x => x.booking_date).LastOrDefault().ToShortDateString() + 
                    "\nGebuchter Raum:\t\t\t" + b.room.description +
                    "\nPreis/Tag in Euro:\t\t\t" + b.room.price +
                    " €\n\nWenn diese Daten nicht mit Ihrer Buchung übereinstimmen sollten, wenden Sie sich bitte an unseren Support via Email an:\noffice@i4a.at\nIhre Anfrage wird dann schnellstmöglich bearbeitet.\n\nMit freundlichen Grüßen,\n\nMarco Wurz\nCEO innovations4austria GmbH\n@: office@i4a.at\nTel.: 01 23456 DW 78\nFax: 01 23456 DW 99";

                client.Send(mail);
                return success = true;
            }
            catch (Exception ex)
            {
                log.Error("Error sending booking confirmation", ex);
            }

            return success;
        }

        public static bool SendPaidBookingConfirmation(string email, booking b)
        {
            log.Info("MailDelivery - SendPaidBookingConfirmation(string email, booking b)");

            bool success = false;

            List<bookingdetail> bDetails = new List<bookingdetail>();

            try
            {
                bDetails = BookingdetailAdministration.GetAllBookingdetailsByBookingId(b.id);

                SmtpClient client = new SmtpClient(CLIENT);
                client.Credentials = new NetworkCredential(USERNAME, PASSWORD);

                MailMessage mail = new MailMessage(FROM_MAIL, email);
                mail.Subject = "Buchungsbestätigung Nr." + b.id;

                portaluser user = new portaluser();
                user = PortaluserAdministration.GetUserByEmail(email);

                mail.IsBodyHtml = false;
                mail.Body = "Sehr geehrte/r Frau/Herr " + user.lastname +
                    "!\n\n\nNachfolgend die Daten Ihrer Buchung, die Sie getätig haben:\n\nBuchungsnr.:\t\t\t\t" + b.id +
                    "\nBuchungszeitraum:\t\t" + b.bookingdetails.Select(x => x.booking_date).FirstOrDefault().ToShortDateString() +
                    " - " + b.bookingdetails.Select(x => x.booking_date).LastOrDefault().ToShortDateString() +
                    "\nGebuchter Raum:\t\t\t" + b.room.description +
                    "\nPreis/Tag in Euro:\t\t\t" + b.room.price + " €" +
                    "\n\nBezahlt am: " + DateTime.Today.ToShortDateString() + 
                    "\n\nWenn diese Daten nicht mit Ihrer Buchung übereinstimmen sollten, wenden Sie sich bitte an unseren Support via Email an:\noffice@i4a.at\nIhre Anfrage wird dann schnellstmöglich bearbeitet.\n\nMit freundlichen Grüßen,\n\nMarco Wurz\nCEO innovations4austria GmbH\n@: office@i4a.at\nTel.: 01 23456 DW 78\nFax: 01 23456 DW 99";

                client.Send(mail);
                return success = true;
            }
            catch (Exception ex)
            {
                log.Error("Error sending booking confirmation", ex);
            }

            return success;
        }
    }
}
