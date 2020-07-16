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
            try
            {
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;
                string emailFromAddress = "empressemailtemplatetesting@gmail.com";
                string password = "Empress@123#";
                string emailToAddress = To;
                string subject = "Email template Testing";
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress, "Email template Testing");
                    mail.To.Add(emailToAddress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void Html_File_Template(string Filename, string FolderPath)
        {
            string defaultString = "<!DOCTYPE html>";
            defaultString += "<html>";
            defaultString += "<head>";
            defaultString += "    <title>" + Filename[0].ToString().ToUpper() + Filename.Substring(1) + "</title>";
            defaultString += "</head>";
            defaultString += "<body>";
            defaultString += "    <div></div>";
            defaultString += "</body>";
            defaultString += "</html>";
            string pathToFiles = FolderPath + Filename + ".html";
            File.WriteAllText(pathToFiles, defaultString);
        }
    }
}