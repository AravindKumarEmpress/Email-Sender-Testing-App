using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Email_Sender_Testing_App.Common_Functions
{
    public class CommonFunction
    {
        public static string Convert_Partial_View_To_String(PartialViewResult partialView, ControllerContext controllerContext)
        {
            using (var sw = new StringWriter())
            {
                partialView.View = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

                var vc = new ViewContext(controllerContext, partialView.View, partialView.ViewData, partialView.TempData, sw);
                partialView.View.Render(vc, sw);

                var partialViewString = sw.GetStringBuilder().ToString();

                return partialViewString;
            }
        }

        public static void Send_Mail(string To, string From, string body)
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;
            string emailFromAddress = "empressemailtemplatetesting@gmail.com";
            string password = "empress123";
            string emailToAddress = To;
            string subject = "Email template Testing";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress,"Email template Testing");
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
        public void Html_File_Template(string Filename)
        {
            string defaultString = "<!DOCTYPE html><html><head><title>TemplateEditor</title></head><body><div></div></body></html>";
            // string pathToFiles = 

            File.WriteAllText(@"E:\Files\DataGridView.htm", defaultString);
        }
    }
}