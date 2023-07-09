using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobOpportunities.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace JobOpportunities.Controllers
{
    public class ApplyForJobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [Authorize]
        public ActionResult Apply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = id;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        // Here When User Click Apply  We Will have  4 things    (UserId - JobId  -  Message  -  ApplyDate )
        public ActionResult Apply([Bind(Include = "Message,Resume")] ApplyForJob job, HttpPostedFileBase upload)
        {
            // GetUserId   is a method  return   user id but he must be login
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];      // Here I did casting to session to save int from it

            // check if the user applied for this job  or  not
            var check = db.ApplyForJobs.Where(a => a.JobId == jobId && a.UserId == userId).ToList();
            if (check.Count < 1)
            {
                var path = Path.Combine(Server.MapPath("~/Images/CV"), upload.FileName);
                upload.SaveAs(path);
                job.Resume = upload.FileName;


                // create an  object  from   ApplyForJob  Class     and   give its properties  ==>> there values
                job.UserId = userId;
                job.JobId = jobId;
                job.ApplyDate = DateTime.Now;

                if (ModelState.IsValid)
                {
                    // Add the   object to database    and save changes
                    db.ApplyForJobs.Add(job);
                    db.SaveChanges();
                    ViewBag.Result = "Apllying is succeeded";
                }

            }
            else
            {
                ViewBag.Result = "Sorry , You already applied for this job";
            }



            return View();
        }




        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == userId);

            return View(jobs.ToList());


        }


        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        public ActionResult EditJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }
        [HttpPost]

        public ActionResult EditJob(ApplyForJob job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Images/CV"), job.Resume);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Images/CV"), upload.FileName);
                    upload.SaveAs(path);
                    job.Resume = upload.FileName;

                }
            
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");

            }
            return View(job);
        }
        //public ActionResult EditJob([Bind(Include = "Message,ApplyDate,Resume")] ApplyForJob job , HttpPostedFileBase upload)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        string oldPath = Path.Combine(Server.MapPath("~/Images/CV"), job.Resume);

        //        if (upload != null)
        //        {
        //            System.IO.File.Delete(oldPath);
        //            string path = Path.Combine(Server.MapPath("~/Images/CV"), upload.FileName);
        //            upload.SaveAs(path);
        //            job.Resume = upload.FileName;

        //        }

        //        job.ApplyDate = DateTime.Now;
        //        db.Entry(job).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("GetJobsByUser");
        //    }

        //    return View(job);

        //}


        public ActionResult DeleteJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);

        }

        [HttpPost, ActionName("DeleteJob")]

        public ActionResult DeleteConfirm(int id)
        {
            var myJob = db.ApplyForJobs.Find(id);
            db.ApplyForJobs.Remove(myJob);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");
        }





        // For Publisher
        //=======================================================================================


        [Authorize]
        public ActionResult GetJobsByPublisher()
        {

            var userId = User.Identity.GetUserId();


            var jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.JobID
                       where job.UserId == userId
                       select app;


            var grooped = from j in jobs
                          group j by j.Job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };
            return View(grooped.ToList());
        }





        public ActionResult DetailsApplyer(int id)
        {


            ApplyForJob job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }








        [Authorize]
        public ActionResult AllMyPostedJobs()
        {

            var userId = User.Identity.GetUserId();
            var jobs = db.Jobs.Where(a => a.UserId == userId).ToList();

            return View(jobs);
        }
























        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
