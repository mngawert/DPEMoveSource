using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DPEMoveDAL.Helper
{
    public class MailManager
    {
        //private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        //private static string stationId = ConfigurationManager.AppSettings["stationId"];
        //private static string storeId = ConfigurationManager.AppSettings["storeId"];
        //private static string terminalCode = ConfigurationManager.AppSettings["terminalCode"];

        public static string getBody(string description)
        {
            string body = "Dear All, <br /> " +
                          "<p>Short error detail is " + description + "</p>  <br /> " +
                          "<p>You can check all error detail in Store FTP system. </p>  <br /> " +
                          "Best regards,  <br /> " +
                          "Register Kiosk System ";

            return body;
        }


        public static void QuickSend(
            string ToEmail,
            string Subject,
            string Body)
        {
            try
            {
                string fromName = "dpemove@bbss.co.th";
                MailMessage message = new MailMessage(fromName, ToEmail,
                   Subject,
                   Body);

                //log.Info("Send mail to " + ToEmail);

                //message.To.Add(ToEmail);
                //message.CC.Add("rushy14@gmail.com");

                string smtpServerName = "mail.bbss.co.th"; //ConfigurationManager.AppSettings["mailSMTPServerName"];

                SmtpClient client = new SmtpClient(smtpServerName);

                // Add credentials if the SMTP server requires them.
                string username = "dpemove@bbss.co.th"; // ConfigurationManager.AppSettings["mailSMTPUserName"];
                string password = "1qaz@WSX"; // ConfigurationManager.AppSettings["mailSMTPUserPassword"];
                string port = "0"; // ConfigurationManager.AppSettings["mailSMTPServerPort"];
                string ssl = "false"; // ConfigurationManager.AppSettings["mailSMTPSSL"];
                if (username != null)
                {
                    client.Credentials = new NetworkCredential(username, password);

                }
                if ("true".Equals(ssl))
                {
                    client.EnableSsl = true;
                }
                if (port != "0")
                {
                    client.Port = Convert.ToInt32(port);
                }

                try
                {
                    //log.Info("client.Credentials :" + client.Credentials.ToString());
                    //log.Info("client.EnableSsl :" + client.EnableSsl);
                    //log.Info("client.Port :" + client.Port);

                    message.IsBodyHtml = true;
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    //log.Error(ex.ToString());
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex.ToString());
                throw ex;
            }

        }

    }
}
