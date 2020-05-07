using Email_Sender_Testing_App.SendMail;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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

        protected string ConvertPartialViewToString(PartialViewResult partialView)
        {
            using (var sw = new StringWriter())
            {
                partialView.View = ViewEngines.Engines.FindPartialView(ControllerContext, partialView.ViewName).View;

                var vc = new ViewContext(ControllerContext, partialView.View, partialView.ViewData, partialView.TempData, sw);
                partialView.View.Render(vc, sw);

                var partialViewString = sw.GetStringBuilder().ToString();

                return partialViewString;
            }
        }

        public ActionResult SendMail(string To, string From)
        {
            string body = ConvertPartialViewToString(EmailTemplate());
            Emailer Mailer = new Emailer();
            Mailer.SendMail(To, From, "Testing email template", body);
            return View("Index");
        }

        public ActionResult TemplateEditorConsole()
        {
            return View("~/Views/Home/TemplateEditor.cshtml");
        }
    }
}