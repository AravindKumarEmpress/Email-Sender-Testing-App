using Email_Sender_Testing_App.SendMail;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Email_Sender_Testing_App.Common_Functions;
using System.Web.Mvc;

namespace Email_Sender_Testing_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult EmailTemplate()
        {
            return PartialView("~/Views/Home/EmailTemplate.cshtml");
        }

        public ActionResult SendMail(string To, string From)
        {
            string body = CommonFunction.Convert_Partial_View_To_String(EmailTemplate(), this.ControllerContext);
            CommonFunction.Send_Mail(To, From, body);
            return View("Index");
        }

        public ActionResult TemplateEditorConsole(string FileName)
        {
            return View("~/Views/Home/TemplateEditor.cshtml");
        }
    }
}