using JobOpportunities.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JobOpportunities.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var list = db.Categories.ToList();
            return View(list);
        }



        public ActionResult Details(int id)
        {
            var job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
           
            return View(job);
        }



        public ActionResult Search()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Search(string SearchName)
        {
            var result = db.Jobs.Where
             (a => a.JobTitle.Contains(SearchName)
           || a.JobContent.Contains(SearchName)
           || a.Category.CategoryName.Contains(SearchName)
           || a.Category.CategoryDescription.Contains(SearchName)).ToList();

            return View(result);
        }








        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("di.hamdan55@gmail.com", "d39261330h");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("di.hamdan55@gmail.com"));
            mail.Subject = contact.Subject;
            string body = "Sender Name : " + contact.Name + "<br/>" +
                          "Sender Email : " + contact.Email + "<br/>" +
                          "Message Subject : " + contact.Subject + "<br/>" +
                          "Message : " + contact.Message + "<b>";
            mail.Body = body;


            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }














        // $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        // Action for Just Admins $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        [Authorize(Users ="Daif")]
        public ActionResult Publishers()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id != userId && a.UserType == "Publisher").ToList();

            return View(user);
        }


        [Authorize(Users = "Daif")]
        public ActionResult Searchers()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id != userId && a.UserType == "Searcher").ToList();

            return View(user);
        }

        [Authorize(Users = "Daif")]
        public ActionResult Admins()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(a => a.Id == userId || a.UserType == "Admins").ToList();

            return View(user);
        }
    }
}